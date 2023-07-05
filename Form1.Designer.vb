<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Timer = New Timer(components)
        Panel_Mask = New Panel()
        SuspendLayout()
        ' 
        ' Timer
        ' 
        ' 
        ' Panel_Mask
        ' 
        Panel_Mask.BackColor = Color.Black
        Panel_Mask.Location = New Point(0, 0)
        Panel_Mask.Name = "Panel_Mask"
        Panel_Mask.Size = New Size(800, 750)
        Panel_Mask.TabIndex = 0
        Panel_Mask.Visible = False
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(10.0F, 25.0F)
        AutoScaleMode = AutoScaleMode.Font
        AutoSize = True
        AutoSizeMode = AutoSizeMode.GrowAndShrink
        ClientSize = New Size(778, 694)
        Controls.Add(Panel_Mask)
        KeyPreview = True
        Name = "Form1"
        Text = "Form1"
        ResumeLayout(False)
    End Sub

    Friend WithEvents Timer As Timer
    Friend WithEvents Panel_Mask As Panel
End Class
