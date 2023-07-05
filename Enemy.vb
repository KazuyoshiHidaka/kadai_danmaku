
Public MustInherit Class Enemy
    Inherits PictureBox

    Public game As GamePage

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
    '  などの場合がある
    Public MustOverride Sub On_F_Update()

End Class


Public Class Fireball
    Inherits Enemy

    Dim game As GamePage

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