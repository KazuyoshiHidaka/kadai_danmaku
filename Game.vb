Imports System.Numerics
Imports System.Threading
Imports System.ComponentModel

Public Class Game


    'ゲーム開始からの経過フレーム時間
    Dim ftime As Integer = 0

    '現在進行中のステージ
    'Nullチェック必要!!
    Dim stage As Stage

    Dim player_speed As Integer = 5
    Dim player_hp As Integer = 3
    'プレイヤーが無敵状態か
    ReadOnly Property Player_Invincible() As Boolean
        Get
            Return player_invincible_to IsNot Nothing
        End Get
    End Property
    'プレイヤーがダメージを受けた直後の無敵フレーム時間
    Dim player_damaged_invincible_fspan As Integer = 100
    'このカウントが一定の値になったとき、プレイヤーの無敵状態を解除する
    Dim player_invincible_tick_count As Integer = 0
    'プレイヤー無敵時間の終了時刻
    'Nothingの場合、無敵ではない状態
    Dim player_invincible_to As Integer?
    'プレイヤー無敵時間の開始時刻
    Dim player_invincible_from As Integer?

    Dim key_up_pressed As Boolean = False
    Dim key_right_pressed As Boolean = False
    Dim key_left_pressed As Boolean = False
    Dim key_down_pressed As Boolean = False

    'ゲームが開始したか
    Dim game_inited As Boolean = False
    'ゲームが一時停止中か
    Dim game_stopped As Boolean = False
    'ゲームクリア!
    Dim game_clear As Boolean = False

    Dim random As New Random

    Private Sub Main_Timer_Tick(sender As Object, e As EventArgs) Handles Main_Timer.Tick

        '=== Player移動
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


        If stage IsNot Nothing Then
            '進行中のステージがある時:
            '敵の追加, 移動, 削除
            stage.Update_Enemies()
        End If

        'プレイヤーと敵との衝突処理
        If Not Player_Invincible Then
            For Each _enemy In Panel_Enemy.Controls
                Dim enemy As Enemy = DirectCast(_enemy, Enemy)
                enemy.On_Player_Collision()
            Next
        End If


        If player_invincible_to IsNot Nothing Then
            'プレイヤー無敵中の場合
            If player_invincible_to < ftime Then
                '解除
                Player_Invincible_Stop()
            End If
        End If

        ftime += 1
    End Sub


    'プレイヤーがダメージを受けた時の処理
    Private Sub Player_Damaged(damage As Integer)
        Debug.Assert(Not Player_Invincible, "無敵時間中はダメージを負いません. ")

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
        Debug.Assert(Not Player_Invincible, "無敵状態でない時に呼ぶ関数です。")

        player_invincible_to = ftime + invincible_fspan
        player_invincible_from = ftime
    End Sub

    'プレイヤーの無敵状態を終了する
    Private Sub Player_Invincible_Stop()
        Debug.Assert(Player_Invincible, "無敵状態中に呼ぶ関数です。")

        player_invincible_to = Nothing
        player_invincible_from = Nothing
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

    'KeyDownイベントでは方向キーを押したときに発生しないため、
    '代わりにProcessCmdKeyを使う
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = Keys.Up Then
            key_up_pressed = True
        ElseIf keyData = Keys.Right Then
            key_right_pressed = True
        ElseIf keyData = Keys.Left Then
            key_left_pressed = True
        ElseIf keyData = Keys.Down Then
            key_down_pressed = True
        ElseIf game_inited And keyData = Keys.Escape Then
            'ゲーム 一時停止/再開
            If game_stopped Then
                Game_Start()
            Else
                Game_Stop()
            End If
        Else
            Return MyBase.ProcessCmdKey(msg, keyData)
        End If

        Return True
    End Function

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
        Main_Timer.Enabled = False
        game_stopped = True
    End Sub

    Private Sub Game_Init()
        'ゲームを開始する. 初めの一回のみ. 途中から再開するのは Game_Start
        game_inited = True

        Game_Start()
    End Sub

    Private Sub Game_Start()
        'ゲームを一時停止状態から再開する
        'ゲーム画面が動き始める
        Main_Timer.Enabled = True
        game_stopped = False
    End Sub
End Class
