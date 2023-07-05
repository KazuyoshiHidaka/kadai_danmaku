<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TopPage
    Inherits System.Windows.Forms.UserControl

    'UserControl はコンポーネント一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Title = New Label()
        Btn_Start = New Button()
        SuspendLayout()
        ' 
        ' Title
        ' 
        Title.AutoSize = True
        Title.Font = New Font("Arial Black", 24.0F, FontStyle.Regular, GraphicsUnit.Point)
        Title.ForeColor = Color.White
        Title.Location = New Point(258, 133)
        Title.Name = "Title"
        Title.Size = New Size(317, 68)
        Title.TabIndex = 0
        Title.Text = "Game Title"
        ' 
        ' Btn_Start
        ' 
        Btn_Start.BackColor = Color.Black
        Btn_Start.Cursor = Cursors.Hand
        Btn_Start.Font = New Font("Arial Black", 10.0F, FontStyle.Bold, GraphicsUnit.Point)
        Btn_Start.ForeColor = Color.White
        Btn_Start.Location = New Point(245, 235)
        Btn_Start.Name = "Btn_Start"
        Btn_Start.Size = New Size(343, 59)
        Btn_Start.TabIndex = 1
        Btn_Start.Text = "スタート"
        Btn_Start.UseVisualStyleBackColor = False
        ' 
        ' TopPage
        ' 
        AutoScaleDimensions = New SizeF(10.0F, 25.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.Black
        Controls.Add(Btn_Start)
        Controls.Add(Title)
        Name = "TopPage"
        Size = New Size(800, 750)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Title As Label
    Friend WithEvents Btn_Start As Button
End Class
