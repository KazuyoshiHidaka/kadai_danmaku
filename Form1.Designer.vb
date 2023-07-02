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
        Player = New PictureBox()
        Timer1 = New Timer(components)
        Fireball_Group = New Panel()
        Panel_Game = New Panel()
        Panel_Info = New Panel()
        Label1 = New Label()
        Label_Stage_Level = New Label()
        Panel_Player_HP = New FlowLayoutPanel()
        Img_Player_HP = New PictureBox()
        PictureBox1 = New PictureBox()
        PictureBox2 = New PictureBox()
        Label_Score = New Label()
        Label_Score_Plus = New Label()
        CType(Player, ComponentModel.ISupportInitialize).BeginInit()
        Panel_Game.SuspendLayout()
        Panel_Info.SuspendLayout()
        Panel_Player_HP.SuspendLayout()
        CType(Img_Player_HP, ComponentModel.ISupportInitialize).BeginInit()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        CType(PictureBox2, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Player
        ' 
        Player.BackColor = Color.Transparent
        Player.BorderStyle = BorderStyle.FixedSingle
        Player.Image = My.Resources.Resources.player
        Player.Location = New Point(365, 395)
        Player.Name = "Player"
        Player.Size = New Size(40, 43)
        Player.SizeMode = PictureBoxSizeMode.StretchImage
        Player.TabIndex = 0
        Player.TabStop = False
        ' 
        ' Timer1
        ' 
        Timer1.Interval = 10
        ' 
        ' Fireball_Group
        ' 
        Fireball_Group.ForeColor = Color.CornflowerBlue
        Fireball_Group.Location = New Point(0, 0)
        Fireball_Group.Name = "Fireball_Group"
        Fireball_Group.Size = New Size(800, 600)
        Fireball_Group.TabIndex = 1
        ' 
        ' Panel_Game
        ' 
        Panel_Game.BorderStyle = BorderStyle.FixedSingle
        Panel_Game.Controls.Add(Player)
        Panel_Game.Controls.Add(Fireball_Group)
        Panel_Game.Location = New Point(0, 0)
        Panel_Game.Name = "Panel_Game"
        Panel_Game.Size = New Size(800, 600)
        Panel_Game.TabIndex = 0
        ' 
        ' Panel_Info
        ' 
        Panel_Info.Controls.Add(Label_Score_Plus)
        Panel_Info.Controls.Add(Label_Score)
        Panel_Info.Controls.Add(Label1)
        Panel_Info.Controls.Add(Label_Stage_Level)
        Panel_Info.Controls.Add(Panel_Player_HP)
        Panel_Info.Location = New Point(0, 600)
        Panel_Info.Name = "Panel_Info"
        Panel_Info.Size = New Size(800, 150)
        Panel_Info.TabIndex = 1
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point)
        Label1.Location = New Point(590, 34)
        Label1.Name = "Label1"
        Label1.Size = New Size(71, 32)
        Label1.TabIndex = 2
        Label1.Text = "レベル"
        ' 
        ' Label_Stage_Level
        ' 
        Label_Stage_Level.Font = New Font("Yu Gothic UI", 18F, FontStyle.Regular, GraphicsUnit.Point)
        Label_Stage_Level.Location = New Point(661, 21)
        Label_Stage_Level.Name = "Label_Stage_Level"
        Label_Stage_Level.Size = New Size(94, 51)
        Label_Stage_Level.TabIndex = 1
        Label_Stage_Level.Text = "1"
        ' 
        ' Panel_Player_HP
        ' 
        Panel_Player_HP.AutoSize = True
        Panel_Player_HP.BorderStyle = BorderStyle.FixedSingle
        Panel_Player_HP.Controls.Add(Img_Player_HP)
        Panel_Player_HP.Controls.Add(PictureBox1)
        Panel_Player_HP.Controls.Add(PictureBox2)
        Panel_Player_HP.Location = New Point(307, 17)
        Panel_Player_HP.Name = "Panel_Player_HP"
        Panel_Player_HP.Size = New Size(158, 55)
        Panel_Player_HP.TabIndex = 0
        ' 
        ' Img_Player_HP
        ' 
        Img_Player_HP.Image = My.Resources.Resources.fire_ball
        Img_Player_HP.InitialImage = My.Resources.Resources.fire_ball
        Img_Player_HP.Location = New Point(3, 3)
        Img_Player_HP.Name = "Img_Player_HP"
        Img_Player_HP.Size = New Size(45, 47)
        Img_Player_HP.SizeMode = PictureBoxSizeMode.StretchImage
        Img_Player_HP.TabIndex = 0
        Img_Player_HP.TabStop = False
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Image = My.Resources.Resources.fire_ball
        PictureBox1.Location = New Point(54, 3)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(45, 47)
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 1
        PictureBox1.TabStop = False
        ' 
        ' PictureBox2
        ' 
        PictureBox2.Image = My.Resources.Resources.fire_ball
        PictureBox2.Location = New Point(105, 3)
        PictureBox2.Name = "PictureBox2"
        PictureBox2.Size = New Size(45, 47)
        PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox2.TabIndex = 2
        PictureBox2.TabStop = False
        ' 
        ' Label_Score
        ' 
        Label_Score.AutoSize = True
        Label_Score.Font = New Font("Yu Gothic UI", 14F, FontStyle.Regular, GraphicsUnit.Point)
        Label_Score.Location = New Point(149, 34)
        Label_Score.Name = "Label_Score"
        Label_Score.RightToLeft = RightToLeft.Yes
        Label_Score.Size = New Size(62, 38)
        Label_Score.TabIndex = 3
        Label_Score.Text = "000"
        ' 
        ' Label_Score_Plus
        ' 
        Label_Score_Plus.AutoSize = True
        Label_Score_Plus.Location = New Point(149, 17)
        Label_Score_Plus.Name = "Label_Score_Plus"
        Label_Score_Plus.Size = New Size(54, 25)
        Label_Score_Plus.TabIndex = 4
        Label_Score_Plus.Text = "+500"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        AutoSize = True
        ClientSize = New Size(778, 694)
        Controls.Add(Panel_Info)
        Controls.Add(Panel_Game)
        KeyPreview = True
        Name = "Form1"
        Text = "Form1"
        CType(Player, ComponentModel.ISupportInitialize).EndInit()
        Panel_Game.ResumeLayout(False)
        Panel_Info.ResumeLayout(False)
        Panel_Info.PerformLayout()
        Panel_Player_HP.ResumeLayout(False)
        CType(Img_Player_HP, ComponentModel.ISupportInitialize).EndInit()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        CType(PictureBox2, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Player As PictureBox
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Fireball_Group As Panel
    Friend WithEvents Panel_Game As Panel
    Friend WithEvents Panel_Info As Panel
    Friend WithEvents Panel_Player_HP As FlowLayoutPanel
    Friend WithEvents Img_Player_HP As PictureBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Label_Stage_Level As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label_Score As Label
    Friend WithEvents Label_Score_Plus As Label
End Class
