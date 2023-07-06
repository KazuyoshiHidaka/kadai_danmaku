Public Class TopPage

    Dim form As Form1

    Public Sub New(_form As Form1)
        InitializeComponent()
        form = _form

        If form.thank_you_for_playing Then
            Img_Crown.BringToFront()
            Img_Crown.Visible = True
        End If
    End Sub

    Private Sub Toggle_Btn_Start(sender As Object, e As EventArgs) Handles Btn_Start.MouseEnter, Btn_Start.MouseLeave
        If Btn_Start.BackColor = Color.Black Then
            'On
            Btn_Start.ForeColor = Color.White
            Btn_Start.BackColor = Stage1.bg_color
        Else
            'Off
            Btn_Start.ForeColor = Color.White
            Btn_Start.BackColor = Color.Black
        End If
        'Stage1.bg_color
    End Sub

    Private Sub Toggle_Btn_Start2(sender As Object, e As EventArgs) Handles Btn_Start_2.MouseEnter, Btn_Start_2.MouseLeave
        If Btn_Start_2.BackColor = Color.Black Then
            'On
            Btn_Start_2.ForeColor = Color.White
            Btn_Start_2.BackColor = Stage2.bg_color
        Else
            'Off
            Btn_Start_2.ForeColor = Color.White
            Btn_Start_2.BackColor = Color.Black
        End If
    End Sub

    Private Sub Btn_Start_Click(sender As Object, e As EventArgs) Handles Btn_Start.Click
        form.Opan_Game_Page(0)
    End Sub

    Private Sub Btn_Start_2_Click(sender As Object, e As EventArgs) Handles Btn_Start_2.Click
        form.Opan_Game_Page(1)
    End Sub
End Class
