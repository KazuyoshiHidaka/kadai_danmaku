
Public MustInherit Class Enemy
    Inherits PictureBox

    MustOverride Property Game As GamePage

    'プレイヤーと衝突したときに呼ばれる Enemy 固有の処理
    '
    '例えば
    '- プレイヤーにダメージを与える
    '- プレイヤーを吹き飛ばす
    '- その両方
    'などの場合がある

    Public MustOverride Sub On_Player_Collision()

    '1フレームごとに、Enemy固有の移動、削除を行う
    '衝突処理は別の所で処理するため不要

    '例えば、
    '移動処理: 直進、斜め移動, 横移動 などの場合がある
    '削除処理:
    '    - 画面外に出たら削除
    '    - 決まった時間で削除
    'などの場合がある
    Public MustOverride Sub On_F_Update()

End Class


Public Class Fireball
    Inherits Enemy

    '======================================
    'Override
    '======================================

    'ゲーム画面のインスタンス
    Private _game As GamePage
    Overrides Property Game As GamePage
        Get
            Return _game
        End Get
        Set(value As GamePage)
            _game = value
        End Set

    End Property


    'プレイヤーと衝突したときに与えるダメージ
    Public atk As Integer = 1

    '1フレームごとの移動量
    Public speed As Integer = 5

    Sub New(_game As GamePage)
        MyBase.New()
        game = _game

        'スタイル
        Me.Size = New Size(36, 41)
        Me.BorderStyle = BorderStyle.Fixed3D
        Me.Image = My.Resources.fire_ball
        Me.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub

    'プレイヤーと衝突したときに、ダメージを与える
    Public Overrides Sub On_Player_Collision()
        game.Player_Damaged(atk)
    End Sub

    'Fireballの、1フレームごとの 移動/削除 処理
    '衝突処理は別の所で処理するため不要
    '
    '移動処理: 上から下に直進
    '削除処理: 画面の下に出ていったら削除
    Public Overrides Sub On_F_Update()
        '移動
        Me.Top += speed

        '削除
        If Me.Top > game.Panel_Game.Height Then
            game.Panel_Enemy.Controls.Remove(Me)
        End If
    End Sub
End Class


Public Class Wave
    Inherits Enemy

    '======================================
    'Override
    '======================================
    'ゲーム画面のインスタンス
    Private _game As GamePage
    Overrides Property Game As GamePage
        Get
            Return _game
        End Get
        Set(value As GamePage)
            _game = value
        End Set

    End Property


    '1フレームごとの移動量
    Public speed As Integer = 5

    Sub New(_game As GamePage)
        MyBase.New()
        Game = _game

        'スタイル
        Me.Size = New Size(36, 50)
        Me.BorderStyle = BorderStyle.Fixed3D
        Me.Image = My.Resources.wave
        Me.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub

    'プレイヤーと衝突したときに、プレイヤーを巻き込んで移動する
    Public Overrides Sub On_Player_Collision()
        Game.Player.Top = Math.Min(
            Game.Player.Top + Game.player_speed * 2,
            Game.Panel_Game.Height - Game.Player.Height '画面の下端
        )
    End Sub

    'Waveの、1フレームごとの 移動/削除 処理
    '衝突処理は別の所で処理するため不要
    '
    '移動処理: 上から下に直進
    '削除処理: 画面の下に出ていったら削除
    Public Overrides Sub On_F_Update()
        '移動
        Me.Top += speed

        '削除
        If Me.Top > Game.Panel_Game.Height Then
            Game.Panel_Enemy.Controls.Remove(Me)
        End If
    End Sub
End Class


'通常のWaveと違い、横全体にWaveが広がる
'プレイヤーを強制的に下に押すためなどに使う
Public Class HorizontalWave
    Inherits Wave

    '削除されるときのBottomの位置
    Public removal_bottom As Integer = 500

    '削除されるときのTopの位置. クラス内部の計算用
    Private ReadOnly Property Removal_Top As Integer
        Get
            Return removal_bottom - Me.Height
        End Get
    End Property

    Sub New(_game As GamePage)
        MyBase.New(_game)

        'スタイル
        Me.Size = New Size(Game.Panel_Game.Width, 50)
    End Sub

    '横全体のWaveのため、下端に行く前には消す
    Public Overrides Sub On_F_Update()
        '移動
        Me.Top += speed

        '削除
        If Me.Top > Removal_Top Then
            Game.Panel_Enemy.Controls.Remove(Me)
        End If
    End Sub

End Class



'ステージ２で出る、ダメージを与える領域. 海溝
Public Class Trench
    Inherits Enemy

    '======================================
    'Trench 固有
    '======================================

    Public atk As Integer = 1

    Public Sub New(_game As GamePage)
        MyBase.New()
        Game = _game

        'スタイル
        Me.BackColor = Color.FromArgb(255, 0, 0, 32)
        Me.Size = New Size(Game.Panel_Game.Width, 50)
        Me.Location = New Point(0, Game.Panel_Game.Height - Me.Height)
    End Sub

    '海にのまれた!! 大ダメージをくらう
    Overrides Sub On_Player_Collision()
        Game.Player_Damaged(atk)
    End Sub


    'ステージの一部のように扱う
    Overrides Sub On_F_Update()
        '移動しない
        '自動で削除もしない
    End Sub

    '======================================
    'Override
    '======================================

    'ゲーム画面のインスタンス
    Private _game As GamePage
    Overrides Property Game As GamePage
        Get
            Return _game
        End Get
        Set(value As GamePage)
            _game = value
        End Set

    End Property

End Class