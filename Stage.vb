
Public MustInherit Class Stage

    '1フレームごとに、敵の追加、移動、削除を行う
    '衝突処理は別の所で行うため不要
    Public MustOverride Sub Update_Enemies()

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
    '難易度調整用のパラメータ
    '===========================================================

    Dim duration As Integer = 1000

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
        Game.Panel_Game.BackColor = Color.FromArgb(255, 64, 0, 0)
    End Sub

    'ステージ１固有のクリア処理
    Public Overrides Sub Clear()
        MyBase.Clear()

        'Fireballを全て削除
        Game.Panel_Enemy.Controls.Clear()
    End Sub


    '1フレームごとに、敵の追加、移動、削除を行う
    '衝突処理は別の所で行うため不要
    Public Overrides Sub Update_Enemies()
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
        Dim spawn_fspan As Integer = 40

        If Ftime > 0 And Ftime Mod spawn_fspan * 20 = 0 Then
            '強い玉.
            '一番最初に出さないように Ftime > 0 の時のみ.

            Dim fireball As New Fireball(Game) With {
                .atk = 2,
                .Size = New Size(Col_Width * 1.5, Col_Width * 1.5)
            }
            'プレイヤーの真正面に出現させる
            Dim player_center = Game.Player.Left + Game.Player.Width / 2
            fireball.Left = player_center - fireball.Width / 2
            Game.Panel_Enemy.Controls.Add(fireball)

        ElseIf Ftime > 0 And Ftime Mod spawn_fspan * 5 = 0 Then
            '中玉
            '一番最初に出さないように Ftime > 0 の時のみ.

            'プレイヤーがいる列に出現させる
            'プレイヤーの中心がどの列に含まれるかを考えている
            Dim player_center = Game.Player.Left + Game.Player.Width / 2
            Dim col = Math.Floor(player_center / Col_Width())

            Dim fireball As Fireball = Init_Fireball_In_Col(col)
            Game.Panel_Enemy.Controls.Add(fireball)

        ElseIf Ftime Mod spawn_fspan = 0 Then
            '弱玉
            Dim fireball As Fireball = Init_Fireball_In_Col(Game.random.Next(9))
            Game.Panel_Enemy.Controls.Add(fireball)
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

        Dim fireball As New Fireball(game) With {
            .Size = New Size(width, width),
            .Location = New Point(Col_Width() * column, 0)
        }
        Return fireball
    End Function

End Class
