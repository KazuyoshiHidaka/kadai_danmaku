Public Class Form1

    Dim game As Game

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        game = New Game With {
            .Location = New Point(0, 0)
        }
        Me.Controls.Add(game)
    End Sub

End Class


Public MustInherit Class Stage
    Inherits Panel

    '1フレームごとに、敵の追加、移動、削除を行う
    '衝突処理は別の所で行うため不要
    Public MustOverride Sub Update_Enemies()

End Class


Public Class Stage1
    Inherits Stage

    Dim game As Game

    'ステージが開始されてからの経過時間
    Dim ftime As Integer

    Sub New(ByRef _Game As Game)
        MyBase.New()
        game = _Game
        ftime = 0
    End Sub

    '1フレームごとに、敵の追加、移動、削除を行う
    '衝突処理は別の所で行うため不要
    Public Overrides Sub Update_Enemies()
        For Each _enemy In game.Panel_Enemy.Controls
            Dim enemy As Enemy = DirectCast(_enemy, Enemy)
            enemy.On_F_Update()
        Next
    End Sub
End Class

Public MustInherit Class Enemy
    Inherits PictureBox

    'プレイヤーと衝突したときに呼ばれる
    Public MustOverride Sub On_Player_Collision()

    '1フレームごとの移動、削除を行う
    '衝突処理は別の所で処理するため不要
    Public MustOverride Sub On_F_Update()

End Class


Public Class Fireball
    Inherits Enemy

    'このオブジェクトがゲーム画面に出現した時刻
    'フレームごとのアニメーションなどに使う
    Dim spawn_ftime As Integer

    'プレイヤーと衝突したときに与えるダメージ
    Dim atk As Integer = 1

    '1フレームごとの移動量
    Dim speed As Integer = 5

    Sub New(spawn_ftime As Integer)
        MyBase.New()
        Me.spawn_ftime = spawn_ftime

        'スタイル
        Me.Size = New Size(36, 41)
        Me.BorderStyle = BorderStyle.Fixed3D
        Me.Image = My.Resources.fire_ball
        Me.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub

    'プレイヤーと衝突したときに呼ばれる
    Public Overrides Sub On_Player_Collision()

    End Sub

    '1フレームごとの移動、削除を行う
    '衝突処理は別の所で処理するため不要
    Public Overrides Sub On_F_Update()

    End Sub
End Class