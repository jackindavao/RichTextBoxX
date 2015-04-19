Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Text
Public Class Form1
    Public appname As String = "RTFDog"  'used to save settings in registry, also in form title
    Public currfpath As String = Nothing
    Public Const maxrecent As Integer = 10 'no of recent files shown in file open menu
    '  Public rtfasopened As String 'used to compare on close whether needs saving
    Dim rtbX As richTextBoxX
    Public currHighlightColor As Color = Color.Yellow
    Public currColorButton As Button = bYellow

    Private Sub mFileOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mFileOpen.Click
        Dim tfile As String = ""
        ofdX.InitialDirectory = getInitialDir()
        ofdX.Filter = "RTF files (*.rtf)|*.rtf|All files (*.*)|*.*"
        ofdX.DefaultExt = ".rtf"
        ofdX.FileName = "*.rtf"
        ofdX.Multiselect = False
        If ofdX.ShowDialog = Windows.Forms.DialogResult.OK Then
            tfile = ofdX.FileName
            openFile(tfile)
        End If
    End Sub
    Sub openFile(ByVal f)
        If Not File.Exists(f) Then
            MessageBox.Show("Error, file does not exist: " & f)
            Return
        End If
        If currfpath IsNot Nothing AndAlso rtbX.changedSinceSaveQ Then
            Dim rslt As DialogResult = MessageBox.Show("Displayed text has changed, save first?", _
                "Save before closing?", MessageBoxButtons.YesNoCancel)
            If rslt = DialogResult.Yes Then
                mSaveAs.PerformClick()
            ElseIf rslt = DialogResult.Cancel Then
                Return
            End If
        End If
        Try
            rtbX.loadFile(f)
            '  rtfasopened = rtbX.getRtf
            Me.Text = appname & "  " & f
            currfpath = f
            mSaveAs.Enabled = True
            mFileSave.Enabled = True
        Catch ex As Exception
            MessageBox.Show("Error, unable to read file " & f)
        End Try
    End Sub

    Function getInitialDir() As String
        Dim initialDir As String = ""
        If currfpath IsNot Nothing AndAlso File.Exists(currfpath) Then
            initialDir = Path.GetDirectoryName(currfpath)
        Else
            initialDir = GetSetting(appname, "IO", "initialdir", "")
        End If
        If initialDir = "" Then initialDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments)
        Return initialDir
    End Function

    Private Sub mFileSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mFileSave.Click
        If currfpath Is Nothing Then
            mSaveAs.PerformClick()
        Else
            rtbX.SaveFile(currfpath)
            addToRecentFiles(currfpath)
        End If
    End Sub

    Private Sub mSaveAs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mSaveAs.Click
        Dim tfile As String = ""
        Try
            sfdX.InitialDirectory = getInitialDir()
            sfdX.Filter = "RTF files (*.rtf)|*.rtf|All files (*.*)|*.*"
            sfdX.DefaultExt = ".rtf"
            sfdX.FileName = "*.rtf"
            If sfdX.ShowDialog = Windows.Forms.DialogResult.OK Then
                tfile = sfdX.FileName
                rtbX.SaveFile(tfile)
                currfpath = tfile
                addToRecentFiles(tfile)
            End If
        Catch ex As Exception
            MessageBox.Show("Error, unable to save file " & tfile)
        End Try
    End Sub
    Dim nextracted As Integer = 0

    Function getndocs(f As String) As Integer
        If Not File.Exists(f) Then Return 0
        Dim ln As String
        Dim str As New StreamReader(f)
        Dim n As Integer = 0
        Do
            ln = str.ReadLine
            If ln.StartsWith("ENDREC") Then n += 1
        Loop While Not str.EndOfStream
        Return n
    End Function
    Private Sub mOpenRecent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mOpenRecent.Click
        If sender.Equals(mOpenRecent) Then Return 'avoid spurious attempts to open empty menuitem
        Dim m As ToolStripMenuItem
        m = DirectCast(sender, ToolStripMenuItem)
        Dim f As String = m.Tag
        Try
            rtbX.LoadFile(f)
            '   rtfasopened = rtbX.getRtf
            currfpath = f
            Me.Text = appname & "  " & f
        Catch ex As Exception
            MessageBox.Show("Error, unable to read file " & f)
        End Try
    End Sub

    Private Sub mFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mFile.Click
        populateRecentFilesList(mOpenRecent)
    End Sub
    Sub addToRecentFiles(ByVal f As String)
        Dim rstr As String = GetSetting(appname, "IO", "recentfiles", "")
        If rstr = "" Then rstr = f Else rstr = f & vbTab & rstr
        Dim fs As New ArrayList(rstr.Split(vbTab))
        Dim tf As String = ""
        For i As Integer = fs.Count - 1 To 1 Step -1
            tf = fs(i)
            If f.Trim = tf.Trim Then
                fs.RemoveAt(i)
            End If
        Next
        Dim top As Integer = maxrecent
        If top > fs.Count - 1 Then top = fs.Count - 1
        Dim sb As New StringBuilder
        For i As Integer = 0 To top
            sb.Append(fs(i) & vbTab)
        Next
        ' rtfasopened = rtbX.getRtf
        SaveSetting(appname, "IO", "recentfiles", sb.ToString)
        SaveSetting(appname, "IO", "initialdir", Path.GetDirectoryName(f))
    End Sub
    Sub populateRecentFilesList(ByVal m As ToolStripMenuItem)
        Dim rstr As String = GetSetting(appname, "IO", "recentfiles", "")
        If rstr = "" Then Return
        Dim fs() As String = rstr.Split(vbTab)
        Dim n As Integer = 0
        m.DropDownItems.Clear()
        Dim tempm As ToolStripMenuItem
        For Each f As String In fs
            If Not File.Exists(f) Then Continue For
            tempm = New ToolStripMenuItem(Path.GetFileName(f))
            tempm.Tag = f
            AddHandler tempm.Click, AddressOf mOpenRecent_Click
            m.DropDownItems.Add(tempm)
        Next
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Text = appname
        rtbX = New richTextBoxX(rtbMain, Me)
        Timer1.Start()
        rtbX.setText(richTextBoxX.helptext)
        currColorButton = bYellow
        currHighlightColor = Color.Yellow

    End Sub
    Private Sub Form1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
    Private Sub Form1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragDrop
        Dim ofile As String = ""
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            ofile = e.Data.GetData(DataFormats.FileDrop)(0)
            Try
                openFile(ofile)
            Catch ex As Exception
                MessageBox.Show("Unable to open file:" & vbCrLf & ofile & vbCrLf & ex.Message)
            End Try
        End If

    End Sub
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim args() As String = Environment.GetCommandLineArgs()
        Dim f As String = ""
        If args.Count > 1 Then
            f = args(1)
            If File.Exists(f) Then
                openFile(f)
            End If
        End If
        rtbX.rtb.TabStop = True
        rtbX.rtb.TabIndex = 1
        rtbX.rtb.BackColor = Color.White
    End Sub
    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If rtbX.changedSinceSaveQ AndAlso currfpath IsNot Nothing Then
            Dim rslt As DialogResult = MessageBox.Show("Displayed text has changed, save first?", _
                "Save before closing?", MessageBoxButtons.YesNoCancel)
            If rslt = DialogResult.Yes Then
                mSaveAs.PerformClick()
            ElseIf rslt = DialogResult.Cancel Then
                e.Cancel = True
                Return
            End If
        End If
    End Sub
    Private Sub mFileExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mFileExit.Click
        Me.Close()
    End Sub


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Not rtbX.changedSinceSaveQ Then Return
        rtbX.saveFile(currfpath)
    End Sub

    

    Sub resetColorButton(ByVal b As Button)
        currColorButton.FlatAppearance.BorderColor = Color.Black
        currColorButton.FlatAppearance.BorderSize = 1
        b.FlatAppearance.BorderColor = Color.Red
        b.FlatAppearance.BorderSize = 1
        currColorButton = b
    End Sub
    Private Sub bYellow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bYellow.Click
        currHighlightColor = Color.Yellow
        resetColorButton(bYellow)
    End Sub

    Private Sub bPink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bPink.Click
        currHighlightColor = Color.Pink
        resetColorButton(bPink)
    End Sub

    Private Sub bBlue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBlue.Click
        currHighlightColor = Color.LightCyan
        resetColorButton(bBlue)
    End Sub

    Private Sub bGreen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bGreen.Click
        currHighlightColor = Color.PaleGreen
        resetColorButton(bGreen)
    End Sub

    Private Sub bPurple_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bPurple.Click
        currHighlightColor = Color.Thistle
        resetColorButton(bPurple)
    End Sub

    Private Sub bWhite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bWhite.Click
        currHighlightColor = Color.White
        resetColorButton(bWhite)
    End Sub

    
    Private Sub bZoomPlus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bZoomPlus.Click
        rtbMain.ZoomFactor *= 1.25
    End Sub

    Private Sub bZoomMinus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bZoomMinus.Click
        rtbMain.ZoomFactor /= 1.25
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso Not e.Alt Then
            Select Case e.KeyCode
                Case Keys.OemMinus
                    bZoomMinus.PerformClick()
                    e.SuppressKeyPress() = True
                Case Keys.Oemplus
                    bZoomPlus.PerformClick()
                    e.SuppressKeyPress() = True
                
            End Select
        End If
    End Sub


    Private Sub ShowHelpToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ShowHelpToolStripMenuItem.Click
        rtbX.showHelp()
    End Sub
End Class
