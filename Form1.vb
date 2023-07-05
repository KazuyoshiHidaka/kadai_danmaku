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

    Private Function Init_Game_Page() As GamePage
        Return New GamePage(Me) With {
            .Location = New Point(0, 0)
        }
    End Function


    Public Sub On_Btn_Start_Clicked()
        'Top画面でスタートボタンがクリックされた時の処理
        'ゲーム画面へ切り替え
        Me.Controls.Clear()
        Dim game As GamePage = Init_Game_Page()
        Dim stage As Stage = New Stage1()
        game.stage = stage

        current_page = game
        Me.Controls.Add(game)

        'ゲーム画面が、キーボード入力を受け取れるように Focus する
        current_page.Focus()

        stage.Start(game)
    End Sub
End Class


