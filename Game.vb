Imports System.Numerics
Imports System.Threading
Imports System.ComponentModel

'ゲーム画面
Public Class GamePage
    'フォームのインスタンス
    Dim form As Form1

    'ゲーム開始からの経過フレーム時間
    Public ftime As Integer = 0

    '現在進行中のステージ
    'Nullチェック必要!!
    Public stage As Stage

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

    Public random As New Random

    Public Sub New(_form As Form1)
        InitializeComponent()
        form = _form
    End Sub

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

            If stage.Is_Cleared Then
                'ステージクリアした時
                Label_Stage_Clear.Visible = True

                'ステージクリア!!を数フレーム間 表示し、
                '終わったら次のステージへ
                If (ftime - stage.Cleared_At) >= 100 Then
                    Label_Stage_Clear.Visible = False
                ElseIf (ftime - stage.Cleared_At) >= 150 Then

                End If
            Else
                '敵の追加, 移動, 削除
                stage.Update_Enemies()
            End If
        End If

        'プレイヤーと敵との衝突処理
        '
        '今は、
        '- 無敵時間中は敵と衝突しても何も起こらない
        '- 衝突判定は、全て長方形同士の重なりチェックで対応できる
        'という前提で成り立つ記述をしている
        '
        If Not Player_Invincible Then
            For Each _enemy In Panel_Enemy.Controls
                Dim enemy As Enemy = DirectCast(_enemy, Enemy)

                If Player.Bounds.IntersectsWith(enemy.Bounds) Then
                    enemy.On_Player_Collision()
                End If
            Next
        End If


        'プレイヤーが有利になるように、
        '衝突処理などの後で無敵解除処理を行う
        '
        'こうすることで
        '無敵が解除される時の1フレーム内において、
        '敵からダメージを負ったりしなくなる

        If Player_Invincible Then
            'プレイヤー無敵中の場合
            If player_invincible_to < ftime Then
                '無敵時間を過ぎた場合、解除
                Player_Invincible_Stop()

                '点滅アニメーションにより、
                'Visible: Falseの状態で無敵時間を過ぎてしまった場合の対策
                Player.Visible = True
            Else
                '無敵時間内の時
                'プレイヤーを点滅させ、無敵状態であることを示す

                '無敵状態の経過時間
                Dim elapsed_time As Integer = ftime - player_invincible_from
                If elapsed_time Mod 5 = 0 Then
                    Player.Visible = Not Player.Visible
                End If
            End If
        End If

        'ftimeの更新が、この関数の最初にあると、
        '各処理において 時刻 0 が訪れなくなるため、
        '最後に書く
        ftime += 1
    End Sub


    'プレイヤーがダメージを受けた時の処理
    Public Sub Player_Damaged(damage As Integer)
        Debug.Assert(Not Player_Invincible, "無敵時間中はダメージを負いません. ")

        Update_Player_HP(player_hp - damage)
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
            'ポーズ画面を 開く/閉じる
            If game_stopped Then
                Close_Panel_Pause()
            Else
                Open_Panel_Pause()
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

    Private Sub Open_Panel_Pause()
        'ゲームを一時停止し、ポーズ画面を開く
        Game_Stop()
        Panel_Pause.BringToFront()
    End Sub
    Private Sub Close_Panel_Pause()
        'ポーズ画面を閉じ、ゲームを再開する
        Panel_Pause.SendToBack()
        Game_Start()
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

