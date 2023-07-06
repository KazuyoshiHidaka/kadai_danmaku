Public Class Form1

    '現在フォームに表示されているページ
    Dim current_page As Control

    Public thank_you_for_playing As Boolean = False

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Top画面からスタート
        Open_Top_Page()
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


    'ゲーム画面に移動する
    'start_stage_i: どのステージから開始するか
    Public Sub Opan_Game_Page(start_stage_i As Integer)
        'Top画面でスタートボタンがクリックされた時の処理
        'ゲーム画面へ切り替え
        Dim game As GamePage = Init_Game_Page()

        '画面のちらつきを防ぐため、
        '次のページをAddしてから、前のページをDisposeする
        Me.Controls.Add(game)
        current_page?.Dispose()
        current_page = game

        'ゲーム画面が、キーボード入力を受け取れるように Focus する
        current_page.Focus()

        game.Game_Init(start_stage_i)
    End Sub

    'Top画面へ切り替える
    Public Sub Open_Top_Page()
        'タイマーのイベントなども破棄したいので、
        'Controls.Remove ではなく Dispose を使う

        Dim top As TopPage = Init_Top_Page()

        '画面のちらつきを防ぐため、
        '次のページをAddしてから、前のページをDisposeする
        Me.Controls.Add(top)
        current_page?.Dispose()
        current_page = top
    End Sub
End Class


