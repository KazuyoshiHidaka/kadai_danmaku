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
        Btn_Start_2 = New Button()
        Img_Crown = New PictureBox()
        CType(Img_Crown, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Title
        ' 
        Title.AutoSize = True
        Title.Font = New Font("Arial Black", 24F, FontStyle.Regular, GraphicsUnit.Point)
        Title.ForeColor = Color.White
        Title.Location = New Point(299, 124)
        Title.Name = "Title"
        Title.Size = New Size(240, 68)
        Title.TabIndex = 0
        Title.Text = "避けろ！"
        ' 
        ' Btn_Start
        ' 
        Btn_Start.BackColor = Color.Black
        Btn_Start.Cursor = Cursors.Hand
        Btn_Start.Font = New Font("Arial Black", 10F, FontStyle.Bold, GraphicsUnit.Point)
        Btn_Start.ForeColor = Color.White
        Btn_Start.Location = New Point(245, 235)
        Btn_Start.Name = "Btn_Start"
        Btn_Start.Size = New Size(343, 59)
        Btn_Start.TabIndex = 1
        Btn_Start.Text = "ステージ１から"
        Btn_Start.UseVisualStyleBackColor = False
        ' 
        ' Btn_Start_2
        ' 
        Btn_Start_2.BackColor = Color.Black
        Btn_Start_2.Cursor = Cursors.Hand
        Btn_Start_2.Font = New Font("Arial Black", 10F, FontStyle.Bold, GraphicsUnit.Point)
        Btn_Start_2.ForeColor = Color.White
        Btn_Start_2.Location = New Point(245, 322)
        Btn_Start_2.Name = "Btn_Start_2"
        Btn_Start_2.Size = New Size(343, 59)
        Btn_Start_2.TabIndex = 2
        Btn_Start_2.Text = "ステージ２から"
        Btn_Start_2.UseVisualStyleBackColor = False
        ' 
        ' Img_Crown
        ' 
        Img_Crown.Image = My.Resources.Resources.crown
        Img_Crown.Location = New Point(524, 133)
        Img_Crown.Name = "Img_Crown"
        Img_Crown.Size = New Size(50, 50)
        Img_Crown.SizeMode = PictureBoxSizeMode.StretchImage
        Img_Crown.TabIndex = 3
        Img_Crown.TabStop = False
        Img_Crown.Visible = False
        ' 
        ' TopPage
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.Black
        Controls.Add(Img_Crown)
        Controls.Add(Btn_Start_2)
        Controls.Add(Btn_Start)
        Controls.Add(Title)
        Name = "TopPage"
        Size = New Size(800, 750)
        CType(Img_Crown, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Title As Label
    Friend WithEvents Btn_Start As Button
    Friend WithEvents Btn_Start_2 As Button
    Friend WithEvents Img_Crown As PictureBox
End Class
