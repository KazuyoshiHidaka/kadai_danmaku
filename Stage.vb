
Imports System.Media

Public MustInherit Class Stage

    'ステージごとに、1フレームごとの敵の追加/移動/削除を行う
    '衝突処理は別の所で行うため不要
    '
    '例えば、
    '敵の追加:
    '- ステージ1は、40フレームごとにランダムな位置にFireballを追加
    '- ステージ2は、初めに障害物を設置した後、100フレームごとにWaveを追加
    '
    '敵の移動:
    '- ステージ1は、1フレームごとに全てのFireballを下に移動
    '- ステージ2は、1フレームごとに全てのWaveを下に移動させ、
    '  さらに、100フレームごとに障害物を移動させる
    '
    '敵の削除:
    '- ステージ1は、画面外に出たFireballを削除
    '- ステージ2は、画面外に出たWaveを削除し、
    '  さらに、一定時間で障害物を削除する
    '
    'などステージごとに異なる
    Public MustOverride Sub On_F_Update()

    'ゲーム画面のインスタンス
    MustOverride Property Game As GamePage

    'ステージが開始された時刻
    'ステージが開始されていない場合は Nothing
    MustOverride Property Start_Ftime As Integer?

    'ステージがクリアされた時刻
    'ステージがクリアされていない場合は Nothing
    MustOverride Property Cleared_At As Integer?

    'ステージが開始されたかどうか
    ReadOnly Property Is_Started As Boolean
        Get
            Return Start_Ftime IsNot Nothing
        End Get
    End Property

    'ステージが開始されてからの経過フレーム時間
    ReadOnly Property Ftime As Integer
        Get
            Return Game.ftime - Start_Ftime
        End Get
    End Property

    'ステージがクリアされたかどうか
    ReadOnly Property Is_Cleared As Boolean
        Get
            Return Cleared_At IsNot Nothing
        End Get
    End Property

    'ステージを開始する時の処理
    Overridable Sub Start(_game_page As GamePage)
        Game = _game_page
        Start_Ftime = _game_page.ftime
    End Sub

    'ステージクリアした時の処理
    Overridable Sub Clear()
        Cleared_At = Game.ftime
    End Sub
End Class

Public Class Stage1
    Inherits Stage

    '===========================================================
    'Override
    '===========================================================

    Dim _game_page As GamePage
    Overrides Property Game As GamePage
        Get
            Return _game_page
        End Get
        Set(value As GamePage)
            _game_page = value
        End Set
    End Property

    Dim _start_ftime As Integer?
    Overrides Property Start_Ftime As Integer?
        Get
            Return _start_ftime
        End Get
        Set(value As Integer?)
            _start_ftime = value
        End Set
    End Property

    Dim _cleared_at As Integer?
    Overrides Property Cleared_At As Integer?
        Get
            Return _cleared_at
        End Get
        Set(value As Integer?)
            _cleared_at = value
        End Set
    End Property

    '===========================================================
    'ステージ1 固有
    '===========================================================

    'Fireballの速度
    Dim fireball_speed As Integer = 6

    '開始から中間イベントまでの時間
    Dim ftime_to_mid As Integer = 100

    Public Shared bg_color As Color = Color.FromArgb(255, 64, 0, 0)

    '1つの Column の横幅
    Private ReadOnly Property Col_Width As Double
        Get
            Return Game.Panel_Game.Width / 9
        End Get
    End Property

    'ステージ１固有の開始処理
    Public Overrides Sub Start(_game_page As GamePage)
        MyBase.Start(_game_page)

        '見た目を設定
        Game.Panel_Game.BackColor = bg_color
    End Sub

    'ステージ１固有のクリア処理
    Public Overrides Sub Clear()
        MyBase.Clear()

        'Fireballを全て削除
        Game.Panel_Enemy.Controls.Clear()
    End Sub


    '1フレームごとに、敵の追加、移動、削除を行う
    '衝突処理は別の所で行うため不要

    Public Overrides Sub On_F_Update()
        If Not Is_Started Or Is_Cleared Then
            Return
        End If

        '既存のEnemyの更新処理
        For Each _enemy In Game.Panel_Enemy.Controls
            Dim enemy As Enemy = DirectCast(_enemy, Enemy)
            enemy.On_F_Update()
        Next

        'ステージ1の流れ
        '
        '基本:
        '弱/中/強のFireballをランダムな位置に出現させる
        '
        '半分経過したとき:
        '手動で階段状にFireballを出現させる
        '

        If Ftime >= ftime_to_mid Then
            '中間イベント

            '中間より前とは異なる間隔でFireballを生成する
            Dim spawn_fspan = 19

            '中間イベントが始まった時刻
            Dim time = Ftime - ftime_to_mid

            'まず、左から右に階段状で生成 ->
            If time = 0 Then
                Spawn_Fireball_In_Col(0)
            ElseIf time = spawn_fspan Then
                Spawn_Fireball_In_Col(1)
            ElseIf time = spawn_fspan * 2 Then
                Spawn_Fireball_In_Col(2)
            ElseIf time = spawn_fspan * 3 Then
                Spawn_Fireball_In_Col(3)
            ElseIf time = spawn_fspan * 4 Then
                Spawn_Fireball_In_Col(4)
            ElseIf time = spawn_fspan * 5 Then
                Spawn_Fireball_In_Col(5)
            ElseIf time = spawn_fspan * 6 Then
                Spawn_Fireball_In_Col(6)
            ElseIf time = spawn_fspan * 7 Then
                Spawn_Fireball_In_Col(7)
            ElseIf time = spawn_fspan * 8 Then
                '右端を空ける

                '次は、右から左に階段状で生成 <-
            ElseIf time = spawn_fspan * 9 Then
                Spawn_Fireball_In_Col(8)
            ElseIf time = spawn_fspan * 10 Then
                Spawn_Fireball_In_Col(7)
            ElseIf time = spawn_fspan * 11 Then
                Spawn_Fireball_In_Col(6)
            ElseIf time = spawn_fspan * 12 Then
                Spawn_Fireball_In_Col(5)
            ElseIf time = spawn_fspan * 13 Then
                Spawn_Fireball_In_Col(4)
            ElseIf time = spawn_fspan * 14 Then
                Spawn_Fireball_In_Col(3)
            ElseIf time = spawn_fspan * 15 Then
                Spawn_Fireball_In_Col(2)
            ElseIf time = spawn_fspan * 16 Then
                Spawn_Fireball_In_Col(1)
            ElseIf time = spawn_fspan * 17 Then
                '左端を空ける

                '次は、左右から中央に向かって階段状で生成 -> <-
            ElseIf time = spawn_fspan * 18 Then
                Spawn_Fireball_In_Col(0)
                Spawn_Fireball_In_Col(8)
            ElseIf time = spawn_fspan * 19 Then
                Spawn_Fireball_In_Col(1)
                Spawn_Fireball_In_Col(7)
            ElseIf time = spawn_fspan * 20 Then
                Spawn_Fireball_In_Col(2)
                Spawn_Fireball_In_Col(6)
            ElseIf time = spawn_fspan * 21 Then
                Spawn_Fireball_In_Col(3)
                Spawn_Fireball_In_Col(5)
            ElseIf time = spawn_fspan * 22 Then
                '中央を空ける

                '次は、中央から左右に向かって階段状で生成 <- ->
            ElseIf time = spawn_fspan * 23 Then
                Spawn_Fireball_In_Col(4)
            ElseIf time = spawn_fspan * 24 Then
                Spawn_Fireball_In_Col(3)
                Spawn_Fireball_In_Col(5)
            ElseIf time = spawn_fspan * 25 Then
                Spawn_Fireball_In_Col(2)
                Spawn_Fireball_In_Col(6)
            ElseIf time = spawn_fspan * 26 Then
                Spawn_Fireball_In_Col(1)
                Spawn_Fireball_In_Col(7)
            ElseIf time = spawn_fspan * 27 Then
                '左右を空ける

                '次は、左右から中央に向かって階段状で生成 -> <-
            ElseIf time = spawn_fspan * 28 Then
                Spawn_Fireball_In_Col(0)
                Spawn_Fireball_In_Col(8)
            ElseIf time = spawn_fspan * 29 Then
                Spawn_Fireball_In_Col(1)
                Spawn_Fireball_In_Col(7)
            ElseIf time = spawn_fspan * 30 Then
                Spawn_Fireball_In_Col(2)
                Spawn_Fireball_In_Col(6)
            ElseIf time = spawn_fspan * 31 Then
                Spawn_Fireball_In_Col(3)
                Spawn_Fireball_In_Col(5)
            ElseIf time = spawn_fspan * 32 Then
                '中央を空ける

                '最後に中央に強いFireballを生成
            ElseIf time = spawn_fspan * 35 Then
                Spawn_Last_Fireball()
            ElseIf time >= spawn_fspan * 45 Then
                Clear()
            End If

        Else
            Dim spawn_fspan As Integer = 30

            '中間イベントより前のとき
            If Ftime > 0 And Ftime Mod spawn_fspan * 8 = 0 Then
                '8個のうち1つは強い玉.
                '一番最初に出さないように Ftime > 0 の時のみ.
                Spawn_Fireball_lv3()

            ElseIf Ftime > 0 And Ftime Mod spawn_fspan * 3 = 0 Then
                ''3個のうち1つは中玉.
                '一番最初に出さないように Ftime > 0 の時のみ.

                Spawn_Fireball_lv2()

            ElseIf Ftime Mod spawn_fspan = 0 Then
                '弱玉
                Spawn_Fireball_In_Col(Game.random.Next(9))
            End If
        End If

    End Sub

    '指定した列の中にFireballを出現させる
    '
    'column: 0 ～ 8
    'ゲーム画面を 9列に分割した時に、どの列にFireballを出現させるかを指定する

    'イメージ;
    '| | | | | | | | | |
    '|O| | | | | | | | |
    '| | | | | | |O| | |
    '| | | |O| | | | | |
    '| | | | | | | | |O|
    '| | | | | | | | | |

    Private Function Init_Fireball_In_Col(column As Integer) As Fireball
        Dim width As Integer = Col_Width()

        Dim fireball As New Fireball(Game) With {
            .Size = New Size(width, width),
            .Location = New Point(Col_Width() * column, 0),
            .speed = fireball_speed
        }
        Return fireball
    End Function

    'Init_Fireball_In_Colした後、画面に追加する
    Private Sub Spawn_Fireball_In_Col(column As Integer)
        Dim fireball As Fireball = Init_Fireball_In_Col(column)
        Game.Panel_Enemy.Controls.Add(fireball)
    End Sub

    Private Sub Spawn_Fireball_lv2()
        'プレイヤーがいる列に出現させる
        'プレイヤーの中心がどの列に含まれるかを考えている
        Dim player_center = Game.Player.Left + Game.Player.Width / 2
        Dim col = Math.Floor(player_center / Col_Width())

        Dim fireball As Fireball = Init_Fireball_In_Col(col)
        fireball.speed = fireball_speed
        Game.Panel_Enemy.Controls.Add(fireball)
    End Sub

    Private Sub Spawn_Fireball_lv3()
        Dim fireball As New Fireball(Game) With {
                .atk = 2,
                .Size = New Size(Col_Width * 1.5, Col_Width * 1.5),
                .speed = fireball_speed
            }
        'プレイヤーの真正面に出現させる
        Dim player_center = Game.Player.Left + Game.Player.Width / 2
        fireball.Left = player_center - fireball.Width / 2
        Game.Panel_Enemy.Controls.Add(fireball)
    End Sub

    '最後に出す特大の玉
    Private Sub Spawn_Last_Fireball()
        Dim fireball As New Fireball(Game) With {
                .atk = 3,
                .Size = New Size(Col_Width * 5, Col_Width * 5),
                .speed = fireball_speed
            }
        'プレイヤーの真正面に出現させる
        Dim player_center = Game.Player.Left + Game.Player.Width / 2
        fireball.Top = -fireball.Height
        fireball.Left = player_center - fireball.Width / 2
        Game.Panel_Enemy.Controls.Add(fireball)
    End Sub

End Class


Public Class Stage2
    Inherits Stage

    '===========================================================
    'Override
    '===========================================================

    Dim _game_page As GamePage
    Overrides Property Game As GamePage
        Get
            Return _game_page
        End Get
        Set(value As GamePage)
            _game_page = value
        End Set
    End Property

    Dim _start_ftime As Integer?
    Overrides Property Start_Ftime As Integer?
        Get
            Return _start_ftime
        End Get
        Set(value As Integer?)
            _start_ftime = value
        End Set
    End Property

    Dim _cleared_at As Integer?
    Overrides Property Cleared_At As Integer?
        Get
            Return _cleared_at
        End Get
        Set(value As Integer?)
            _cleared_at = value
        End Set
    End Property

    '===========================================================
    'ステージ2 固有
    '===========================================================

    Dim trench As Trench

    'ステージ開始からクリアまでの時間
    Dim duration As Integer = 2500

    Public Shared bg_color As Color = Color.FromArgb(255, 0, 0, 128)

    ReadOnly Property Col_Width As Integer
        Get
            Return Game.Panel_Game.Width / 9
        End Get
    End Property

    'ステージ２固有の開始処理
    Public Overrides Sub Start(_game_page As GamePage)
        MyBase.Start(_game_page)

        '見た目を設定
        Game.Panel_Game.BackColor = bg_color
    End Sub

    'ステージ２固有のクリア処理
    Public Overrides Sub Clear()
        MyBase.Clear()

        'Fireballを全て削除
        Game.Panel_Enemy.Controls.Clear()
    End Sub

    Public Overrides Sub On_F_Update()
        If Not Is_Started Or Is_Cleared Then
            Return
        End If

        'クリア処理
        If Ftime > duration Then
            Clear()
            Return
        End If

        '既存のEnemyの更新処理
        For Each _enemy In Game.Panel_Enemy.Controls
            Dim enemy As Enemy = DirectCast(_enemy, Enemy)
            enemy.On_F_Update()
        Next

        '指定したフレームごとにFireballを出現させる
        Dim start_spawn_ftime As Integer = 150
        Dim spawn_fspan As Integer = 80

        If trench Is Nothing Then
            'まず、Trenchを作成する
            trench = New Trench(Game) With {
                .Height = 0,
                .Top = Game.Panel_Game.Height
            }
            Game.Panel_Enemy.Controls.Add(trench)
        ElseIf Ftime <= 100 Then
            'Trenchを徐々に広げる
            trench.Top = Game.Panel_Game.Height * (1 - Ftime / 1000)
            trench.Height = (Game.Panel_Game.Height * 0.1) * (Ftime / 100)
        ElseIf Ftime < start_spawn_ftime Then
            'Trenchのアニメーション終了後、休憩時間

        ElseIf Ftime = start_spawn_ftime Then
            Game.Panel_Enemy.Controls.Add(Init_Wave(0, 6))

        ElseIf Ftime = start_spawn_ftime + spawn_fspan Then
            Game.Panel_Enemy.Controls.Add(Init_Wave(0, 3))
            Game.Panel_Enemy.Controls.Add(Init_Wave(5, 8))

        ElseIf Ftime = start_spawn_ftime + spawn_fspan * 2 Then
            Game.Panel_Enemy.Controls.Add(Init_Wave(2, 8))

        ElseIf Ftime = start_spawn_ftime + spawn_fspan * 3 Then
            Game.Panel_Enemy.Controls.Add(Init_Horizontal_Wave())

        ElseIf Ftime > start_spawn_ftime + spawn_fspan * 3 And Ftime < duration Then
            '以降は、ランダムでWaveを出現させる
            If Ftime Mod spawn_fspan * 5 = 0 Then
                Dim wave As HorizontalWave
                '速/遅のHorizontalWave
                wave = Init_Horizontal_Wave()
                wave.speed = 2 + Game.random.Next(5)
                Game.Panel_Enemy.Controls.Add(wave)

            ElseIf Ftime Mod spawn_fspan * 2 = 0 Then
                '速/遅の中央波
                Dim wave As Wave = Init_Wave(Game.random.Next(3), Game.random.Next(3) + 5)
                wave.speed = 3 + Game.random.Next(2)
                Game.Panel_Enemy.Controls.Add(wave)

            ElseIf Ftime Mod spawn_fspan = 0 Then
                '速/遅の左右波
                Dim wave_l As Wave = Init_Wave(0, Game.random.Next(2) + 2)
                wave_l.speed = 3 + Game.random.Next(2)
                Dim wave_r As Wave = Init_Wave(Game.random.Next(2) + 5, 8)
                wave_r.speed = 3 + Game.random.Next(2)
                Game.Panel_Enemy.Controls.AddRange({wave_l, wave_r})
            End If
        End If


    End Sub


    'ゲーム画面を 9列に分割したと考え、
    '指定した列から列までの範囲一体のWaveを出現させる
    '
    '_from: 0 ～ 8
    '_to: 0 ～ 8
    Private Function Init_Wave(_from As Integer, _to As Integer) As Wave
        Dim wave As New Wave(Game) With {
            .Size = New Size(Col_Width() * (_to - _from + 1), 80),
            .Location = New Point(Col_Width() * _from, 0),
            .speed = 4
        }
        Return wave
    End Function

    Private Function Init_Horizontal_Wave() As HorizontalWave
        '海溝のぎりぎり上で止まるように、removal_bottomを設定する
        Dim wave As New HorizontalWave(Game) With {
            .Height = 80,
            .Location = New Point(0, 0),
            .speed = 3,
            .removal_bottom = Game.Panel_Game.Height - trench.Height - Game.Player.Height - 10
        }
        Return wave
    End Function
End Class