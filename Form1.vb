Imports System.Threading

Public Class Form1

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles Player.Click

    End Sub

    'ボスキャラの技の案
    '火の玉: 直進飛び道具
    '火の玉を一瞬見えなくする技
    '鉄球: 斜めに動く. ステージの壁で反射する


    Dim random As New Random
    Dim player_speed As Integer = 5
    Dim player_hp As Integer = 3
    'プレイヤーが無敵状態か
    Dim player_invincible As Boolean = False
    'プレイヤーがダメージを受けた直後の無敵フレーム時間
    Dim player_damaged_invincible_fspan As Integer = 100
    'このカウントが一定の値になったとき、プレイヤーの無敵状態を解除する
    Dim player_invincible_tick_count As Integer = 0

    Dim key_up_pressed As Boolean = False
    Dim key_right_pressed As Boolean = False
    Dim key_left_pressed As Boolean = False
    Dim key_down_pressed As Boolean = False

    Dim FireBalls As New List(Of PictureBox)
    'Fireballを生成するかどうか
    Public fireball_generatable As Boolean = False
    'Fireballを生成するフレーム間隔. fspanはフレーム単位のspan.
    Public fireball_gen_fspan As Integer = 20
    'このカウントが一定の値になったとき、Fireballを生成する
    Dim fireball_gen_tick_count As Integer = 0
    'Fireballの生成範囲. 範囲の中心はプレイヤー
    Public fireball_gen_range As Integer = 300
    Public fireball_speed As Integer = 6
    Public fireball_atk As Integer = 1
    'DEBUG用. Fireballに当たり判定があるかどうか
    Dim fireball_collidable As Boolean = True

    'ゲームが開始したか
    Dim game_inited As Boolean = False
    'ゲームが一時停止中か
    Dim game_stopped As Boolean = False
    'ゲームクリア!
    Dim game_clear As Boolean = False

    '現在のステージ
    Dim stage As Stage1



    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'Player移動
        'キー同時押しに対応するために、別個にIf文を使う
        If key_up_pressed Then
            Player.Top = Math.Max(
                Player.Top - player_speed,
                0 '上端より上に行かないように
            )
        End If
        If key_right_pressed Then
            Player.Left = Math.Min(
                Player.Left + player_speed,
                Panel_Game.Width - Player.Width '右端より右に行かないように
            )
        End If
        If key_left_pressed Then
            Player.Left = Math.Max(
                Player.Left - player_speed,
                0 '左端より左に行かないように
            )
        End If
        If key_down_pressed Then
            Player.Top = Math.Min(
                Player.Top + player_speed,
                Panel_Game.Height - Player.Height '下端より下に行かないように
            )
        End If

        'FireBall移動
        For Each fireball In Fireball_Group.Controls
            fireball.Top += fireball_speed

            '画面外に行ったボールを削除する
            If fireball.Top > Panel_Game.Height Then
                Fireball_Group.Controls.Remove(fireball)
            End If

            '衝突検出
            If fireball_collidable And
                Not player_invincible And
                Player.Bounds.IntersectsWith(fireball.Bounds) Then

                Player_Damaged(fireball_atk)
            End If
        Next

        'Fireball生成
        If fireball_generatable Then
            fireball_gen_tick_count += 1
            If fireball_gen_tick_count >= fireball_gen_fspan Then
                fireball_gen_tick_count = 0
                Generate_Fireball()
            End If
        End If

        '無敵状態の終了処理
        If player_invincible Then
            player_invincible_tick_count += 1
            If player_invincible_tick_count >= player_damaged_invincible_fspan Then
                player_invincible_tick_count = 0
                Player_Invincible_Stop()
            End If
        End If

        'ステージの残り時間による処理
        stage.tick_count += 1
        If stage.Ftime_left() <= 0 Then
            'ステージクリア
            Label_Stage_Time_Left.Text = "0"
            fireball_generatable = False
            fireball_atk = 0
            'Update_Player_HP(3)

            game_clear = True
            Game_Over()
        Else
            'ステージの残り時間を表示
            Label_Stage_Time_Left.Text = Math.Ceiling(
                stage.Ftime_left() / 0.7 / 100 '約70フレームで1秒
            )
            stage.Difficulty_Up_By_Ftime_left()
        End If
    End Sub


    'プレイヤーがダメージを受けた時の処理
    Private Sub Player_Damaged(damage As Integer)
        Debug.Assert(Not player_invincible, "無敵時間中はダメージを負いません. ")

        Update_Player_HP(player_hp - 1)
        Player_Invincible_Start(player_damaged_invincible_fspan)
        'HPが減った時のPlayerの見た目の変更は
        '無敵状態終了処理で行われるため、ここでは不要

        If player_hp <= 0 Then
            Game_Over()
        End If
    End Sub

    'プレイヤーを {ms} ミリ秒間 無敵状態にする
    Private Sub Player_Invincible_Start(invincible_fspan As Integer)
        Debug.Assert(Not player_invincible, "無敵状態でない時に呼ぶ関数です。")

        player_invincible = True

        'Playerの残り体力で、無敵状態中の見た目を切り替える
        If player_hp >= 3 Then
            Player.Image = My.Resources.player_invincible
        ElseIf player_hp = 2 Then
            Player.Image = My.Resources.player_warning_invincible
        ElseIf player_hp = 1 Then
            Player.Image = My.Resources.player_danger_invincible
        End If
    End Sub

    'プレイヤーの無敵状態を終了する
    Private Sub Player_Invincible_Stop()
        Debug.Assert(player_invincible, "無敵状態中に呼ぶ関数です。")

        player_invincible = False

        'Playerの残り体力で、無敵状態中の見た目を切り替える
        If player_hp >= 3 Then
            Player.Image = My.Resources.player
        ElseIf player_hp = 2 Then
            Player.Image = My.Resources.player_warning
        ElseIf player_hp = 1 Then
            Player.Image = My.Resources.player_danger
        End If
    End Sub

    'プレイヤーのHPを更新し、体力ゲージに反映する
    Private Sub Update_Player_HP(hp As Integer)
        player_hp = hp

        '一度、全部非表示にしてから
        For Each _Hp_Img In Panel_Player_HP.Controls
            Dim Hp_Img As PictureBox = DirectCast(_Hp_Img, PictureBox)
            Hp_Img.Visible = False
        Next

        '必要な数だけを表示する
        For i = 0 To player_hp - 1
            Panel_Player_HP.Controls.Item(i).Visible = True
        Next
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Up Then
            key_up_pressed = True
        ElseIf e.KeyCode = Keys.Right Then
            key_right_pressed = True
        ElseIf e.KeyCode = Keys.Left Then
            key_left_pressed = True
        ElseIf e.KeyCode = Keys.Down Then
            key_down_pressed = True
        ElseIf game_inited And e.KeyCode = Keys.Escape Then
            'ゲーム 一時停止/再開
            If game_stopped Then
                Game_Start()
            Else
                Game_Stop()
            End If
        End If
    End Sub

    Private Sub Form1_KeyUp(sender As Object, e As KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.Up Then
            key_up_pressed = False
        ElseIf e.KeyCode = Keys.Right Then
            key_right_pressed = False
        ElseIf e.KeyCode = Keys.Left Then
            key_left_pressed = False
        ElseIf e.KeyCode = Keys.Down Then
            key_down_pressed = False
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Game_Init()
    End Sub

    Private Sub Generate_Fireball()
        Dim width As Integer = 36
        Dim fireball As New PictureBox With {
            .Size = New Size(width, 41),
            .BorderStyle = BorderStyle.Fixed3D,
            .Image = My.Resources.fire_ball,
            .SizeMode = PictureBoxSizeMode.StretchImage,
            .Top = 0,
            .Left = Math.Min( 'プレイヤーの周辺200以内に生成する
                Panel_Game.Width - width,
                Math.Max(
                    0,
                    Player.Left - fireball_gen_range / 2 + random.Next(fireball_gen_range + Player.Width)
                )
            )
        }
        Fireball_Group.Controls.Add(fireball)
    End Sub

    Private Sub Game_Over()
        'ゲームが終了する
        Game_Stop()
        '成績処理を加える
        game_inited = False

        If game_clear Then
            MessageBox.Show("Game Clear")
        Else
            MessageBox.Show("Game Over")
        End If
    End Sub

    Private Sub Game_Stop()
        'ゲームを一時停止する
        'ゲーム画面の動きを止めるだけ
        Timer1.Enabled = False
        game_stopped = True
    End Sub

    Private Sub Game_Init()
        'ゲームを開始する. 初めの一回のみ. 途中から再開するのは Game_Start
        Timer1.Interval = 10
        game_inited = True


        'ゲームオブジェクト初期化
        stage = New Stage1(Me, 0)

        Game_Start()
    End Sub

    Private Sub Game_Start()
        'ゲームを一時停止状態から再開する
        'ゲーム画面が動き始める
        Timer1.Enabled = True
        game_stopped = False
    End Sub

    Private Sub Label_Stage_Time_Left_Click(sender As Object, e As EventArgs) Handles Label_Stage_Time_Left.Click

    End Sub
End Class


Public Class Stage1
    '難易度調整のために、親ゲームクラスのプロパティの書き換えに使う
    Dim Game As Form1
    'ステージ番号
    Public index As Integer = 0
    'ステージの継続フレーム時間
    Public fspan As Integer = 4200
    'このカウントが fspan と等しくなったとき、ステージクリア
    Public tick_count As Integer = 0
    'ステージの難易度
    Public stage_difficulty As Integer

    '難易度調整用の変数
    'ステージの難易度が４段階ある。
    '各難易度ごとに、残り時間に応じてさらに４段階ある。
    '

    '          残り時間に応じた難易度→
    '               0  1  2  3
    ' ステージ  0  低
    '    ↓     1
    '           2
    '           3　　　　　  高
    '
    '
    ''Fireballを生成するフレーム間隔. この値が小さいほど、多く生成される
    Dim Fireball_gen_fspan As Integer(,) = {
        {25, 24, 23, 22},
        {23, 22, 21, 20},
        {20, 17, 16, 15},
        {15, 13, 11, 10}
    }
    'Fireballを生成する範囲. この値が小さいほど、プレイヤーの近くに生成される
    Dim Fireball_gen_range As Integer(,) = {
        {500, 450, 400, 350},
        {400, 350, 300, 250},
        {300, 250, 200, 170},
        {170, 150, 120, 100}
    }
    'Fireballの速度. この値が大きいほど、速くなる
    Dim Fireball_speed As Integer(,) = {
        {3, 3, 4, 5},
        {4, 5, 6, 7},
        {6, 7, 8, 9},
        {8, 9, 10, 11}
    }

    '残りフレーム時間
    ReadOnly Property Ftime_left As Integer
        Get
            Return fspan - tick_count
        End Get
    End Property

    Sub New(ByRef _Game As Form1, stage_difficulty As Integer)
        Game = _Game
        Me.stage_difficulty = stage_difficulty
        Init_Difficulty_Params()
    End Sub

    '難易度を初期化する
    Private Sub Init_Difficulty_Params()
        Game.fireball_generatable = True
        Game.fireball_atk = 1
        '残り時間に応じた難易度は 0
        Game.fireball_gen_fspan = Fireball_gen_fspan(stage_difficulty, 0)
        Game.fireball_gen_range = Fireball_gen_range(stage_difficulty, 0)
        Game.fireball_speed = Fireball_speed(stage_difficulty, 0)
    End Sub

    'ステージの残り時間に応じて、難易度を上げる
    Public Sub Difficulty_Up_By_Ftime_left()
        '残り時間に応じた難易度をセット
        Dim time_left_difficulty As Integer
        If Ftime_left <= fspan * 0.2 Then
            time_left_difficulty = 3
        ElseIf Ftime_left <= fspan * 0.5 Then
            time_left_difficulty = 2
        ElseIf Ftime_left <= fspan * 0.75 Then
            time_left_difficulty = 1
        End If

        '難易度調整パラメータ更新
        Game.fireball_gen_fspan = Fireball_gen_fspan(stage_difficulty, time_left_difficulty)
        Game.fireball_gen_range = Fireball_gen_range(stage_difficulty, time_left_difficulty)
        Game.fireball_speed = Fireball_speed(stage_difficulty, time_left_difficulty)
    End Sub
End Class