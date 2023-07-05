Public Class Form1

    '現在フォームに表示されているページ
    Dim current_page As Control

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Top画面を表示
        current_page = New TopPage(Me) With {
            .Location = New Point(0, 0)
        }
        Controls.Add(current_page)
    End Sub

    Private Function Init_Top_Page() As TopPage
        Return New TopPage(Me) With {
            .Location = New Point(0, 0)
        }
    End Function

    Private Function Init_Game_Page() As Game
        Return New Game(Me) With {
            .Location = New Point(0, 0)
        }
    End Function


    Public Sub On_Btn_Start_Clicked()
        'Top画面でスタートボタンがクリックされた時の処理
        'ゲーム画面へ切り替え
        Me.Controls.Clear()
        current_page = Init_Game_Page()
        Me.Controls.Add(current_page)

        'ゲーム画面が、キーボード入力を受け取れるように Focus する
        current_page.Focus()
    End Sub
End Class



Public MustInherit Class Stage
    Inherits Panel

    '1フレームごとに、敵の追加、移動、削除を行う
    '衝突処理は別の所で行うため不要
    Public MustOverride Sub Update_Enemies()

End Class

