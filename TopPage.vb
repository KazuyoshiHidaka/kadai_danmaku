Public Class TopPage

    Dim form As Form1

    Public Sub New(_form As Form1)
        InitializeComponent()
        form = _form
    End Sub

    Private Sub Btn_Start_MouseEnter(sender As Object, e As EventArgs) Handles Btn_Start.MouseEnter
        Btn_Start.ForeColor = Color.Black
        Btn_Start.BackColor = Color.White
    End Sub

    Private Sub Btn_Start_MouseLeave(sender As Object, e As EventArgs) Handles Btn_Start.MouseLeave
        Btn_Start.ForeColor = Color.White
        Btn_Start.BackColor = Color.Black
    End Sub

    Private Sub Btn_Start_Click(sender As Object, e As EventArgs) Handles Btn_Start.Click
        form.On_Btn_Start_Clicked()
    End Sub
End Class
