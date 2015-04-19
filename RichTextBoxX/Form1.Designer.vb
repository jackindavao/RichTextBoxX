<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.mFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mFileOpen = New System.Windows.Forms.ToolStripMenuItem()
        Me.mFileSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.mSaveAs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mOpenRecent = New System.Windows.Forms.ToolStripMenuItem()
        Me.mFileExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.rtbMain = New System.Windows.Forms.RichTextBox()
        Me.ofdX = New System.Windows.Forms.OpenFileDialog()
        Me.sfdX = New System.Windows.Forms.SaveFileDialog()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbFindRx = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbReplaceStr = New System.Windows.Forms.TextBox()
        Me.bCancel = New System.Windows.Forms.Button()
        Me.bReplaceAll = New System.Windows.Forms.Button()
        Me.bReplaceOnce = New System.Windows.Forms.Button()
        Me.bFindNext = New System.Windows.Forms.Button()
        Me.lMsg = New System.Windows.Forms.Label()
        Me.bMark = New System.Windows.Forms.Button()
        Me.bClearHighlights = New System.Windows.Forms.Button()
        Me.bYellow = New System.Windows.Forms.Button()
        Me.bPink = New System.Windows.Forms.Button()
        Me.bGreen = New System.Windows.Forms.Button()
        Me.bBlue = New System.Windows.Forms.Button()
        Me.bPurple = New System.Windows.Forms.Button()
        Me.bWhite = New System.Windows.Forms.Button()
        Me.bZoomMinus = New System.Windows.Forms.Button()
        Me.bZoomPlus = New System.Windows.Forms.Button()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowHelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mFile, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(905, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'mFile
        '
        Me.mFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mFileOpen, Me.mFileSave, Me.mSaveAs, Me.mOpenRecent, Me.mFileExit})
        Me.mFile.Name = "mFile"
        Me.mFile.Size = New System.Drawing.Size(37, 20)
        Me.mFile.Text = "File"
        '
        'mFileOpen
        '
        Me.mFileOpen.Name = "mFileOpen"
        Me.mFileOpen.Size = New System.Drawing.Size(152, 22)
        Me.mFileOpen.Text = "&Open"
        '
        'mFileSave
        '
        Me.mFileSave.Name = "mFileSave"
        Me.mFileSave.Size = New System.Drawing.Size(152, 22)
        Me.mFileSave.Text = "&Save"
        '
        'mSaveAs
        '
        Me.mSaveAs.Name = "mSaveAs"
        Me.mSaveAs.Size = New System.Drawing.Size(152, 22)
        Me.mSaveAs.Text = "Save &As"
        '
        'mOpenRecent
        '
        Me.mOpenRecent.Name = "mOpenRecent"
        Me.mOpenRecent.Size = New System.Drawing.Size(152, 22)
        Me.mOpenRecent.Text = "Open &Recent"
        '
        'mFileExit
        '
        Me.mFileExit.Name = "mFileExit"
        Me.mFileExit.Size = New System.Drawing.Size(152, 22)
        Me.mFileExit.Text = "E&xit"
        '
        'rtbMain
        '
        Me.rtbMain.AcceptsTab = True
        Me.rtbMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtbMain.Location = New System.Drawing.Point(18, 56)
        Me.rtbMain.Name = "rtbMain"
        Me.rtbMain.Size = New System.Drawing.Size(875, 644)
        Me.rtbMain.TabIndex = 1
        Me.rtbMain.TabStop = False
        Me.rtbMain.Text = ""
        '
        'ofdX
        '
        Me.ofdX.FileName = "*.*"
        '
        'sfdX
        '
        Me.sfdX.FileName = "*.*"
        '
        'Timer1
        '
        Me.Timer1.Interval = 300000
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(471, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Find:"
        '
        'tbFindRx
        '
        Me.tbFindRx.Location = New System.Drawing.Point(507, 4)
        Me.tbFindRx.Name = "tbFindRx"
        Me.tbFindRx.Size = New System.Drawing.Size(345, 20)
        Me.tbFindRx.TabIndex = 2
        Me.tbFindRx.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(471, 31)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Replace with:"
        '
        'tbReplaceStr
        '
        Me.tbReplaceStr.Location = New System.Drawing.Point(549, 28)
        Me.tbReplaceStr.Name = "tbReplaceStr"
        Me.tbReplaceStr.Size = New System.Drawing.Size(303, 20)
        Me.tbReplaceStr.TabIndex = 4
        '
        'bCancel
        '
        Me.bCancel.Location = New System.Drawing.Point(444, 1)
        Me.bCancel.Name = "bCancel"
        Me.bCancel.Size = New System.Drawing.Size(25, 25)
        Me.bCancel.TabIndex = 11
        Me.bCancel.Text = "X"
        Me.bCancel.UseVisualStyleBackColor = True
        '
        'bReplaceAll
        '
        Me.bReplaceAll.Location = New System.Drawing.Point(420, 24)
        Me.bReplaceAll.Name = "bReplaceAll"
        Me.bReplaceAll.Size = New System.Drawing.Size(25, 25)
        Me.bReplaceAll.TabIndex = 10
        Me.bReplaceAll.Text = "∀"
        Me.bReplaceAll.UseVisualStyleBackColor = True
        '
        'bReplaceOnce
        '
        Me.bReplaceOnce.Location = New System.Drawing.Point(444, 24)
        Me.bReplaceOnce.Name = "bReplaceOnce"
        Me.bReplaceOnce.Size = New System.Drawing.Size(25, 25)
        Me.bReplaceOnce.TabIndex = 9
        Me.bReplaceOnce.Text = "1"
        Me.bReplaceOnce.UseVisualStyleBackColor = True
        '
        'bFindNext
        '
        Me.bFindNext.Location = New System.Drawing.Point(420, 0)
        Me.bFindNext.Name = "bFindNext"
        Me.bFindNext.Size = New System.Drawing.Size(25, 25)
        Me.bFindNext.TabIndex = 8
        Me.bFindNext.Text = "F"
        Me.bFindNext.UseVisualStyleBackColor = True
        '
        'lMsg
        '
        Me.lMsg.AutoSize = True
        Me.lMsg.Location = New System.Drawing.Point(97, 34)
        Me.lMsg.Name = "lMsg"
        Me.lMsg.Size = New System.Drawing.Size(14, 13)
        Me.lMsg.TabIndex = 12
        Me.lMsg.Text = "~"
        '
        'bMark
        '
        Me.bMark.Location = New System.Drawing.Point(397, 0)
        Me.bMark.Name = "bMark"
        Me.bMark.Size = New System.Drawing.Size(25, 25)
        Me.bMark.TabIndex = 13
        Me.bMark.Text = "M"
        Me.bMark.UseVisualStyleBackColor = True
        '
        'bClearHighlights
        '
        Me.bClearHighlights.Location = New System.Drawing.Point(397, 24)
        Me.bClearHighlights.Name = "bClearHighlights"
        Me.bClearHighlights.Size = New System.Drawing.Size(25, 25)
        Me.bClearHighlights.TabIndex = 14
        Me.bClearHighlights.Text = "C"
        Me.bClearHighlights.UseVisualStyleBackColor = True
        '
        'bYellow
        '
        Me.bYellow.BackColor = System.Drawing.Color.Yellow
        Me.bYellow.FlatAppearance.BorderColor = System.Drawing.Color.Red
        Me.bYellow.FlatAppearance.BorderSize = 2
        Me.bYellow.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bYellow.Location = New System.Drawing.Point(235, 6)
        Me.bYellow.Margin = New System.Windows.Forms.Padding(0)
        Me.bYellow.MinimumSize = New System.Drawing.Size(15, 15)
        Me.bYellow.Name = "bYellow"
        Me.bYellow.Size = New System.Drawing.Size(15, 15)
        Me.bYellow.TabIndex = 15
        Me.bYellow.UseVisualStyleBackColor = False
        '
        'bPink
        '
        Me.bPink.BackColor = System.Drawing.Color.Pink
        Me.bPink.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bPink.Location = New System.Drawing.Point(251, 6)
        Me.bPink.MinimumSize = New System.Drawing.Size(15, 15)
        Me.bPink.Name = "bPink"
        Me.bPink.Size = New System.Drawing.Size(15, 15)
        Me.bPink.TabIndex = 19
        Me.bPink.UseVisualStyleBackColor = False
        '
        'bGreen
        '
        Me.bGreen.BackColor = System.Drawing.Color.PaleGreen
        Me.bGreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bGreen.Location = New System.Drawing.Point(283, 6)
        Me.bGreen.MinimumSize = New System.Drawing.Size(15, 15)
        Me.bGreen.Name = "bGreen"
        Me.bGreen.Size = New System.Drawing.Size(15, 15)
        Me.bGreen.TabIndex = 22
        Me.bGreen.UseVisualStyleBackColor = False
        '
        'bBlue
        '
        Me.bBlue.BackColor = System.Drawing.Color.LightCyan
        Me.bBlue.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bBlue.Location = New System.Drawing.Point(267, 6)
        Me.bBlue.MinimumSize = New System.Drawing.Size(15, 15)
        Me.bBlue.Name = "bBlue"
        Me.bBlue.Size = New System.Drawing.Size(15, 15)
        Me.bBlue.TabIndex = 21
        Me.bBlue.UseVisualStyleBackColor = False
        '
        'bPurple
        '
        Me.bPurple.BackColor = System.Drawing.Color.Thistle
        Me.bPurple.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bPurple.Location = New System.Drawing.Point(299, 6)
        Me.bPurple.MinimumSize = New System.Drawing.Size(15, 15)
        Me.bPurple.Name = "bPurple"
        Me.bPurple.Size = New System.Drawing.Size(15, 15)
        Me.bPurple.TabIndex = 23
        Me.bPurple.UseVisualStyleBackColor = False
        '
        'bWhite
        '
        Me.bWhite.BackColor = System.Drawing.Color.White
        Me.bWhite.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bWhite.Location = New System.Drawing.Point(315, 6)
        Me.bWhite.MinimumSize = New System.Drawing.Size(15, 15)
        Me.bWhite.Name = "bWhite"
        Me.bWhite.Size = New System.Drawing.Size(15, 15)
        Me.bWhite.TabIndex = 24
        Me.bWhite.UseVisualStyleBackColor = False
        '
        'bZoomMinus
        '
        Me.bZoomMinus.Location = New System.Drawing.Point(858, 28)
        Me.bZoomMinus.Name = "bZoomMinus"
        Me.bZoomMinus.Size = New System.Drawing.Size(25, 25)
        Me.bZoomMinus.TabIndex = 26
        Me.bZoomMinus.Text = "-"
        Me.bZoomMinus.UseVisualStyleBackColor = True
        '
        'bZoomPlus
        '
        Me.bZoomPlus.Location = New System.Drawing.Point(858, 1)
        Me.bZoomPlus.Name = "bZoomPlus"
        Me.bZoomPlus.Size = New System.Drawing.Size(25, 25)
        Me.bZoomPlus.TabIndex = 25
        Me.bZoomPlus.Text = "+"
        Me.bZoomPlus.UseVisualStyleBackColor = True
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowHelpToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'ShowHelpToolStripMenuItem
        '
        Me.ShowHelpToolStripMenuItem.Name = "ShowHelpToolStripMenuItem"
        Me.ShowHelpToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ShowHelpToolStripMenuItem.Text = "Show help"
        '
        'Form1
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(905, 710)
        Me.Controls.Add(Me.bZoomMinus)
        Me.Controls.Add(Me.bZoomPlus)
        Me.Controls.Add(Me.bWhite)
        Me.Controls.Add(Me.bPurple)
        Me.Controls.Add(Me.bGreen)
        Me.Controls.Add(Me.bBlue)
        Me.Controls.Add(Me.bPink)
        Me.Controls.Add(Me.bYellow)
        Me.Controls.Add(Me.bClearHighlights)
        Me.Controls.Add(Me.bMark)
        Me.Controls.Add(Me.lMsg)
        Me.Controls.Add(Me.bCancel)
        Me.Controls.Add(Me.bReplaceAll)
        Me.Controls.Add(Me.bReplaceOnce)
        Me.Controls.Add(Me.bFindNext)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.tbReplaceStr)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tbFindRx)
        Me.Controls.Add(Me.rtbMain)
        Me.Controls.Add(Me.MenuStrip1)
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents mFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mFileOpen As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mFileSave As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mSaveAs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mOpenRecent As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents rtbMain As System.Windows.Forms.RichTextBox
    Friend WithEvents ofdX As System.Windows.Forms.OpenFileDialog
    Friend WithEvents sfdX As System.Windows.Forms.SaveFileDialog
    Friend WithEvents mFileExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tbFindRx As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tbReplaceStr As System.Windows.Forms.TextBox
    Friend WithEvents bCancel As System.Windows.Forms.Button
    Friend WithEvents bReplaceAll As System.Windows.Forms.Button
    Friend WithEvents bReplaceOnce As System.Windows.Forms.Button
    Friend WithEvents bFindNext As System.Windows.Forms.Button
    Friend WithEvents lMsg As System.Windows.Forms.Label
    Friend WithEvents bMark As System.Windows.Forms.Button
    Friend WithEvents bClearHighlights As System.Windows.Forms.Button
    Friend WithEvents bYellow As System.Windows.Forms.Button
    Friend WithEvents bPink As System.Windows.Forms.Button
    Friend WithEvents bGreen As System.Windows.Forms.Button
    Friend WithEvents bBlue As System.Windows.Forms.Button
    Friend WithEvents bPurple As System.Windows.Forms.Button
    Friend WithEvents bWhite As System.Windows.Forms.Button
    Friend WithEvents bZoomMinus As System.Windows.Forms.Button
    Friend WithEvents bZoomPlus As System.Windows.Forms.Button
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShowHelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
