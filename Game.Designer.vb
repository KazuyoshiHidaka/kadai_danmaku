<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Game
    Inherits System.Windows.Forms.UserControl

    'UserControl はコンポーネント一覧をクリーンアップするために dispose をオーバーライドします。
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

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Player = New PictureBox()
        Panel_Info = New Panel()
        Panel_Player_HP = New FlowLayoutPanel()
        Img_HP_1 = New PictureBox()
        Img_HP_2 = New PictureBox()
        Img_HP_3 = New PictureBox()
        Panel_Game = New Panel()
        Panel_Enemy = New Panel()
        Main_Timer = New Timer(components)
        CType(Player, ComponentModel.ISupportInitialize).BeginInit()
        Panel_Info.SuspendLayout()
        Panel_Player_HP.SuspendLayout()
        CType(Img_HP_1, ComponentModel.ISupportInitialize).BeginInit()
        CType(Img_HP_2, ComponentModel.ISupportInitialize).BeginInit()
        CType(Img_HP_3, ComponentModel.ISupportInitialize).BeginInit()
        Panel_Game.SuspendLayout()
        SuspendLayout()
        ' 
        ' Player
        ' 
        Player.BackColor = Color.Transparent
        Player.BorderStyle = BorderStyle.FixedSingle
        Player.Image = My.Resources.Resources.player
        Player.Location = New Point(385, 473)
        Player.Name = "Player"
        Player.Size = New Size(40, 43)
        Player.SizeMode = PictureBoxSizeMode.StretchImage
        Player.TabIndex = 1
        Player.TabStop = False
        ' 
        ' Panel_Info
        ' 
        Panel_Info.Controls.Add(Panel_Player_HP)
        Panel_Info.Location = New Point(0, 600)
        Panel_Info.Name = "Panel_Info"
        Panel_Info.Size = New Size(800, 150)
        Panel_Info.TabIndex = 2
        ' 
        ' Panel_Player_HP
        ' 
        Panel_Player_HP.AutoSize = True
        Panel_Player_HP.BorderStyle = BorderStyle.FixedSingle
        Panel_Player_HP.Controls.Add(Img_HP_1)
        Panel_Player_HP.Controls.Add(Img_HP_2)
        Panel_Player_HP.Controls.Add(Img_HP_3)
        Panel_Player_HP.Location = New Point(323, 36)
        Panel_Player_HP.Name = "Panel_Player_HP"
        Panel_Player_HP.Size = New Size(158, 55)
        Panel_Player_HP.TabIndex = 0
        ' 
        ' Img_HP_1
        ' 
        Img_HP_1.Image = My.Resources.Resources.fire_ball
        Img_HP_1.InitialImage = My.Resources.Resources.fire_ball
        Img_HP_1.Location = New Point(3, 3)
        Img_HP_1.Name = "Img_HP_1"
        Img_HP_1.Size = New Size(45, 47)
        Img_HP_1.SizeMode = PictureBoxSizeMode.StretchImage
        Img_HP_1.TabIndex = 0
        Img_HP_1.TabStop = False
        ' 
        ' Img_HP_2
        ' 
        Img_HP_2.Image = My.Resources.Resources.fire_ball
        Img_HP_2.Location = New Point(54, 3)
        Img_HP_2.Name = "Img_HP_2"
        Img_HP_2.Size = New Size(45, 47)
        Img_HP_2.SizeMode = PictureBoxSizeMode.StretchImage
        Img_HP_2.TabIndex = 1
        Img_HP_2.TabStop = False
        ' 
        ' Img_HP_3
        ' 
        Img_HP_3.Image = My.Resources.Resources.fire_ball
        Img_HP_3.Location = New Point(105, 3)
        Img_HP_3.Name = "Img_HP_3"
        Img_HP_3.Size = New Size(45, 47)
        Img_HP_3.SizeMode = PictureBoxSizeMode.StretchImage
        Img_HP_3.TabIndex = 2
        Img_HP_3.TabStop = False
        ' 
        ' Panel_Game
        ' 
        Panel_Game.BorderStyle = BorderStyle.FixedSingle
        Panel_Game.Controls.Add(Player)
        Panel_Game.Controls.Add(Panel_Enemy)
        Panel_Game.Location = New Point(0, 0)
        Panel_Game.Name = "Panel_Game"
        Panel_Game.Size = New Size(800, 600)
        Panel_Game.TabIndex = 3
        ' 
        ' Panel_Enemy
        ' 
        Panel_Enemy.ForeColor = Color.CornflowerBlue
        Panel_Enemy.Location = New Point(0, 0)
        Panel_Enemy.Name = "Panel_Enemy"
        Panel_Enemy.Size = New Size(800, 600)
        Panel_Enemy.TabIndex = 1
        ' 
        ' Main_Timer
        ' 
        Main_Timer.Interval = 13
        ' 
        ' Game
        ' 
        AutoScaleDimensions = New SizeF(10.0F, 25.0F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(Panel_Game)
        Controls.Add(Panel_Info)
        Name = "Game"
        Size = New Size(800, 750)
        CType(Player, ComponentModel.ISupportInitialize).EndInit()
        Panel_Info.ResumeLayout(False)
        Panel_Info.PerformLayout()
        Panel_Player_HP.ResumeLayout(False)
        CType(Img_HP_1, ComponentModel.ISupportInitialize).EndInit()
        CType(Img_HP_2, ComponentModel.ISupportInitialize).EndInit()
        CType(Img_HP_3, ComponentModel.ISupportInitialize).EndInit()
        Panel_Game.ResumeLayout(False)
        ResumeLayout(False)
    End Sub
    Friend WithEvents Player As PictureBox
    Friend WithEvents Panel_Info As Panel
    Friend WithEvents Panel_Player_HP As FlowLayoutPanel
    Friend WithEvents Img_HP_1 As PictureBox
    Friend WithEvents Img_HP_2 As PictureBox
    Friend WithEvents Img_HP_3 As PictureBox
    Friend WithEvents Panel_Game As Panel
    Friend WithEvents Panel_Enemy As Panel
    Friend WithEvents Main_Timer As Timer
End Class
