
Public Class Stage1
    Inherits Stage

    Dim game As Game

    'ステージが開始されたフレーム時刻
    Dim start_ftime As Integer

    'ステージが開始されてからの経過フレーム時間
    ReadOnly Property Ftime As Integer
        Get
            Return game.ftime - start_ftime
        End Get
    End Property


    Sub New(ByRef _Game As Game)
        MyBase.New()
        game = _Game
        start_ftime = _Game.ftime
    End Sub

    '1フレームごとに、敵の追加、移動、削除を行う
    '衝突処理は別の所で行うため不要
    Public Overrides Sub Update_Enemies()
        For Each _enemy In game.Panel_Enemy.Controls
            Dim enemy As Enemy = DirectCast(_enemy, Enemy)
            enemy.On_F_Update()
        Next

        If Ftime = 0 Then

        ElseIf Ftime <= 10 Then

        End If
    End Sub
End Class
