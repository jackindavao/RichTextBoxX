Imports System.Text
Imports System.Text.RegularExpressions
Imports System.IO
Public Class richTextBoxX
    Friend WithEvents rtb As RichTextBox
    Public Const indentwidth As Integer = 20
    Public Const maxindent As Integer = indentwidth * 20
    Public Const autosaveInterval As Integer = 5
    Public Const autosaveQ As Boolean = True
    Dim holdstart As Integer = -1
    Dim holdlen As Integer = -1
    Dim rtbtf As New RichTextBox
    Dim enforceLineIndentsQ As Boolean = True  'if true, does not allow line x+1 to be indented more than 1 indent from line x
    Dim claims As ArrayList
    Dim dateformat As String = "yyMMdd HH:mm"
    Dim basefont As Font
    Dim insertIdx As Integer = 0
    Dim currfpath As String = ""
    Public changedSinceSaveQ As Boolean = False
    Dim lastSave As DateTime
    Dim lastChange As DateTime
    Dim mainform As Form1
    Friend WithEvents bFindNext As Button
    Friend WithEvents bReplaceOnce As Button
    Friend WithEvents bReplaceAll As Button
    Friend WithEvents bCancel As Button
    Friend WithEvents bMark As Button
    Friend WithEvents bClearHighlights As Button

    Sub New(ByVal rtb_ As RichTextBox, ByVal mainform_ As Form1)
        rtb = rtb_
        rtb.AllowDrop = True
        basefont = rtb.SelectionFont
        mainform = mainform_
        bCancel = mainform.bCancel
        bFindNext = mainform.bFindNext
        bReplaceAll = mainform.bReplaceAll
        bReplaceOnce = mainform.bReplaceOnce
        bClearHighlights = mainform.bClearHighlights
        bMark = mainform.bMark
    End Sub
    Private Sub rtb_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles rtb.KeyDown
        If e.Control Then
            Select Case e.KeyCode
                Case Keys.B
                    boldSelection()
                Case Keys.D
                    timeStamp()
                Case Keys.F
                    mainform.tbFindRx.Focus()
                Case Keys.H
                    markAll()
                Case Keys.I
                    italicizeSelection()
                Case Keys.M
                    If e.Shift Then
                        setSelectionIndents(-1)
                    Else
                        setSelectionIndents(1)
                    End If
                    rtb.SelectionHangingIndent = 0
                    rtb.Invalidate()
                    rtb.Update()
                Case Keys.P
                    toggleProportionalFont()
                Case Keys.Q
                    highlightSelection()
                Case Keys.R
                    redSelection()
                Case Keys.S
                    strikeoutSelection()
                Case Keys.T
                    columnizeSelection()
                Case Keys.U
                    underlineSelection()
                Case Keys.V
                    doPaste()
                Case Keys.Oemplus
                    changeFontSize(1)
                Case Keys.OemMinus
                    changeFontSize(-1)
                Case Keys.F1
                    showHelp()
                Case Else
                    Return
            End Select
            e.SuppressKeyPress = True
            e.Handled = True
        Else
            Select Case e.KeyCode
                Case Keys.Tab
                    If e.Shift Then
                        setSelectionIndents(-1)
                    Else
                        setSelectionIndents(1)
                    End If
                    rtb.SelectionHangingIndent = 0
                    rtb.Invalidate()
                    rtb.Update()
                    e.SuppressKeyPress = True
                    e.Handled = True
                Case Keys.F1
                    showHelp()
                Case Keys.F3
                    findRx("", True)
                Case Keys.F4
                    bReplaceOnce.PerformClick()

            End Select
        End If
    End Sub
    Function findRx(ByVal s As String, ByVal loopbackQ As Boolean) As Boolean  'use s = "" to use tbfindrx.txt; if no loopbackQ stops at end of doc
        If s = "" Then s = mainform.tbFindRx.Text.Trim
        Dim startidx As Integer = rtb.SelectionStart + rtb.SelectionLength
        If startidx >= rtb.TextLength Then
            If loopbackQ = True Then
                startidx = 0
            Else
                Return False
            End If
        End If

        Dim m As Match = Regex.Match(rtb.Text.Substring(startidx), s, RegexOptions.IgnoreCase)
        If m.Success Then
            rtb.Select(m.Index + startidx, m.Length)
            rtb.Focus()
            Return True
        Else
            If loopbackQ = True Then startidx = 0 Else Return False
            m = Regex.Match(rtb.Text.Substring(startidx), s, RegexOptions.IgnoreCase)
            If m.Success Then
                rtb.Select(m.Index + startidx, m.Length)
                rtb.Focus()
                Return True
            End If
        End If
        rtb.Focus()
        Return False
    End Function
    Private Sub bCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bCancel.Click
        mainform.tbFindRx.Text = ""
        mainform.tbReplaceStr.Text = ""
    End Sub
    Private Sub bFindNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bFindNext.Click
        findRx("", True)
    End Sub
    Private Sub bReplaceAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bReplaceAll.Click
        Dim x0 As Integer = rtb.SelectionStart
        Dim past0Q As Boolean = False
        Dim nhits As Integer = 0
        While findRx("", True)
            nhits += 1
            If past0Q AndAlso rtb.SelectionStart >= x0 Then Exit While 'to avoid loop if replacement text is regex match for find text
            If rtb.SelectionStart <= x0 Then past0Q = True
            rtb.SelectedText = mainform.tbReplaceStr.Text.Trim
        End While
        mainform.lMsg.Text = nhits & "replacements"
        rtb.Focus()
    End Sub
    Dim hcolor() As Color = {Color.Pink, Color.PowderBlue, Color.LightGreen, Color.PeachPuff, Color.LightSalmon}
    Dim markhits As String = ""
    Sub markAll()
        Dim s As String = mainform.tbFindRx.Text.Trim
        s = s.Replace("^", ".+?\s")
        Dim toks() As String = s.Split(",")
        markhits = "Hits: "
        If toks.Count > 1 Then
            For i As Integer = 0 To toks.Count - 1
                Dim tok As String = toks(i).Trim
                markTok(tok, hcolor(i Mod hcolor.Count))
            Next
        Else
            markTok(s, mainform.currHighlightColor)
        End If
        mainform.lMsg.Text = markhits
    End Sub
    Sub markTok(ByVal tok As String, ByVal c As Color)
        holdSelection()
        rtb.Select(0, 0)
        Dim nhits As Integer = 0
        While findRx(tok, False)
            nhits += 1
            rtb.SelectionBackColor = c
        End While
        markhits = markhits & " " & nhits
        restoreSelection()
    End Sub
    Private Sub bMark_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bMark.Click
        markAll()
    End Sub
    Sub clearHighlights()
        holdSelection()
        rtb.SelectAll()
        rtb.SelectionBackColor = Color.White
        restoreSelection()
    End Sub
    Private Sub bClearHighlights_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bClearHighlights.Click
        clearHighlights()
    End Sub
    Private Sub bReplaceOnce_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bReplaceOnce.Click
        If rtb.SelectionLength > 0 Then  'there is a hit selected
            rtb.SelectedText = mainform.tbReplaceStr.Text.Trim  'replace the selection with replstr
        Else
            bFindNext.PerformClick()
        End If
        rtb.Focus()
    End Sub
    Sub safeReplaceAll(ByVal r As RichTextBox, ByVal s1 As String, ByVal s2 As String)
        'note this loses the previous caret position selection, those should be saved first if wanted to be kept
        Dim rx As New Regex(s1)
        Dim mc As MatchCollection = rx.Matches(r.Text)
        Dim lenadj As Integer = 0
        For Each m As Match In mc
            r.Select(m.Index + lenadj, m.Length)
            r.SelectedText = s2
            lenadj += s2.Length - s1.Length
        Next
    End Sub

    'Function getParagraphStartLineIndex()
    '    Dim lineidx As Integer = rtb.GetLineFromCharIndex(rtb.SelectionStart)
    '    Dim c As Char
    '    Do
    '        c = rtb.Lines(lineidx).Chars(0)
    '        If Char.IsUpper(c) Then Return lineidx
    '        lineidx -= 1
    '    Loop While lineidx >= 0
    '    Return -1
    'End Function
    Dim holdidx As Integer
    Sub holdSelection()
        holdstart = rtb.SelectionStart
        holdlen = rtb.SelectionLength
        holdidx = rtb.GetCharIndexFromPosition(New Point(0, 0))  'to allow restoring scroll position
    End Sub
    Sub restoreSelection()
        If holdstart < 0 OrElse holdlen < 0 Then Return
        rtb.Select(holdidx, 0)
        rtb.ScrollToCaret()
        rtb.Select(holdstart, holdlen)
        rtb.Focus()
    End Sub
    Function getLineIndent(ByVal lineidx As Integer) As Integer
        Dim start As Integer = getFirstCharIndexFromLogicalLine(rtb, lineidx)
        rtb.Select(start, 0)
        Dim ind As Integer = rtb.SelectionIndent / indentwidth
        Return ind
    End Function
    Function getLogicalLineFromCharIndex(ByVal rtbx As RichTextBox, ByVal chidx As Integer) As Integer
        Dim lensum As Integer = 0
        For i As Integer = 0 To rtbx.Lines.Count - 1
            lensum += rtbx.Lines(i).Length + 1
            If chidx < lensum Then Return i
        Next
        Return 0
    End Function
    Function getFirstCharIndexFromLogicalLine(ByVal rtbx As RichTextBox, ByVal lineidx As Integer) As Integer
        Dim lensum As Integer = 0
        For i As Integer = 0 To rtbx.Lines.Count - 1
            If lineidx = i Then Return lensum
            lensum += rtbx.Lines(i).Length + 1
        Next
        Return 0
    End Function
    Function getLineIndents(ByVal rtbx As RichTextBox) As Integer()
        Dim inds(rtbx.Lines.Count - 1) As Integer
        Dim x1 As Integer = 0
        For i As Integer = 0 To rtbx.Lines.Count - 1
            rtbx.Select(x1, 0)
            inds(i) = rtbx.SelectionIndent / indentwidth
            x1 += rtbx.Lines(i).Length + 1
        Next
        Return inds
    End Function
    Sub setLineIndent(ByVal lineidx As Integer, ByVal ind As Integer)
        Dim start As Integer = getFirstCharIndexFromLogicalLine(rtb, lineidx)
        rtb.Select(start, 0)
        rtb.SelectionIndent = ind * indentwidth
    End Sub
    Sub setSelectionIndents(ByVal delta As Integer)
        holdSelection()
        Dim s1 As Integer = rtb.SelectionStart
        Dim s2 As Integer = s1 + rtb.SelectionLength
        Dim line1 As Integer = getLogicalLineFromCharIndex(rtb, s1)
        Dim line2 As Integer = getLogicalLineFromCharIndex(rtb, s2)
        Dim x1 As Integer = 0
        Dim indmin As Integer = 20
        Dim indmax As Integer = 0
        For i As Integer = 0 To rtb.Lines.Count - 1
            If i >= line1 AndAlso i <= line2 Then
                rtb.Select(x1, 0)
                If indmin > rtb.SelectionIndent / indentwidth Then indmin = rtb.SelectionIndent / indentwidth
                If indmax < rtb.SelectionIndent / indentwidth Then indmax = rtb.SelectionIndent / indentwidth
                rtb.DeselectAll()
            End If
            x1 += rtb.Lines(i).Length + 1
        Next
        If indmin + delta < 0 OrElse indmax + delta > maxindent / indentwidth Then Return
        x1 = 0
        For i As Integer = 0 To rtb.Lines.Count - 1
            If i >= line1 AndAlso i <= line2 Then
                rtb.Select(x1, 0)
                rtb.SelectionIndent += delta * indentwidth
                rtb.DeselectAll()

            End If
            x1 += rtb.Lines(i).Length + 1
        Next
        ' enforceLineIndents()
    End Sub
    Sub setLineIndents(ByVal rtbx As RichTextBox, ByVal inds() As Integer, Optional ByVal delta As Integer = 0)
        '   holdSelection()
        If Not inds.Count = rtbx.Lines.Count Then Throw New Exception("indent list not same length as lines list")
        Dim x1 As Integer = 0
        For i As Integer = 0 To rtbx.Lines.Count - 1
            rtbx.Select(x1, 0)
            rtbx.SelectionIndent = (delta + inds(i)) * indentwidth
            x1 += rtbx.Lines(i).Length + 1
        Next
        '  restoreSelection()
    End Sub
    Sub enforceLineIndents()
        If Not enforceLineIndentsQ Then Return
        If rtb.Lines.Count = 0 Then Return
        ' holdSelection()
        Dim prevind As Integer = 0
        setLineIndent(0, 0)
        If rtb.Lines.Count = 1 Then Return
        For i As Integer = 1 To rtb.Lines.Count - 1
            If getLineIndent(i) > prevind + 1 Then
                setLineIndent(i, prevind + 1)
            End If
            prevind += 1
        Next
        restoreSelection()
    End Sub
    Dim rx As New Regex("^[^A-Za-z-\+]*", RegexOptions.Compiled)
    Sub doPaste()
        If Clipboard.ContainsText Then
            rtbtf.Clear()
            rtbtf.Paste()
            holdSelection()
            Dim inds() As Integer = getLineIndents(rtbtf)
            'Dim s1 As Integer = 0
            'Dim tlines(rtbtf.Lines.Count - 1) As String
            'For i As Integer = 0 To rtbtf.Lines.Count - 1   'this removes everything before first letter of line
            '    'tlines(i)
            '    Dim m As Match = rx.Match(rtbtf.Lines(i))
            '    If m.Success Then
            '        s1 = getFirstCharIndexFromLogicalLine(rtbtf, i)
            '        rtbtf.Select(s1, m.Length)
            '        rtbtf.SelectedText = ""
            '    End If
            'Next
            'rtbtf.Lines = tlines
            Dim prevind As Integer = rtb.SelectionIndent / indentwidth
            setLineIndents(rtbtf, inds, prevind)
            'enforceLineIndents()
            If rtbtf.Text.StartsWith("onenote:///") Then
                rtbtf.Text = "[[" & rtbtf.Text & "]]"
                rtbtf.Select(2, rtbtf.TextLength - 4)
                rtbtf.SelectionColor = Color.Red
                rtbtf.SelectionFont = New Font(rtbtf.SelectionFont, rtbtf.SelectionFont.Style Or FontStyle.Underline)
            End If
            rtbtf.SelectAll()
            rtbtf.Copy()
            rtb.Paste()
            rtb.Invalidate()
            rtb.Update()
            restoreSelection()
        Else
            rtb.Paste()
            rtb.Invalidate()
            rtb.Update()
        End If
    End Sub

    Private Sub rtb_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles rtb.DragDrop
        rtb.Select(insertIdx, 0)  'this makes it insert where the cursor was left instead of where the mouse is pointing
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            rtb.SelectedText = "file://" & e.Data.GetData(DataFormats.FileDrop)(0)
        ElseIf e.Data.GetDataPresent(DataFormats.Text) Then
            rtb.SelectedText = e.Data.GetData(DataFormats.Text)
        End If
    End Sub
    Private Sub rtb_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rtb.SelectionChanged
        insertIdx = rtb.SelectionStart + rtb.SelectionLength
    End Sub
    Private Sub rtb_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles rtb.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        ElseIf e.Data.GetDataPresent(DataFormats.Text) Then
            If e.Data.GetDataPresent("UniformResourceLocator") Then
                e.Effect = DragDropEffects.All
                rtb.DoDragDrop(e.Data.GetData(DataFormats.Text), DragDropEffects.All)
            Else
                e.Effect = DragDropEffects.Copy
            End If
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
    Function getRtf() As String
        Return rtb.Rtf
    End Function
    Sub setRtf(ByVal s As String)
        rtb.Rtf = s
    End Sub
    Function getText() As String
        Return rtb.Text
    End Function
    Sub setText(ByVal s As String)
        rtb.Text = s
    End Sub
    Sub loadFile(ByVal f As String)
        If Not clearToDiscard() Then Return
        Try
            rtb.LoadFile(f)
        Catch ex As Exception
            If File.Exists(f) Then
                Dim s As String = File.ReadAllText(f)
                rtb.Text = s
                Dim newf As String = Path.ChangeExtension(f, "rtf")
                saveFile(newf)
            End If
        End Try
        currfpath = f
        changedSinceSaveQ = False
        lastSave = Now
        lastChange = Now
    End Sub
    Sub saveFile(ByVal f As String)
        If f = "" Then Return
        rtb.SaveFile(f)
        currfpath = f
        changedSinceSaveQ = False
        lastSave = Now
        lastChange = Now
    End Sub
    Function clearToDiscard() As Boolean
        If Not changedSinceSaveQ Then Return True
        If autosaveQ Then
            saveFile(currfpath)
            Return True
        End If
        Dim rslt As DialogResult = MessageBox.Show("File " & currfpath & " has changed, save first?", _
                "Save before closing?", MessageBoxButtons.YesNoCancel)
        If rslt = DialogResult.Yes Then
            saveFile(currfpath)
            Return True
        End If
        Return False
    End Function
    Sub boldSelection()
        If rtb.SelectionFont.Style And FontStyle.Bold Then
            rtb.SelectionFont = New Font(rtb.SelectionFont, rtb.SelectionFont.Style And (FontStyle.Italic Or FontStyle.Strikeout Or FontStyle.Underline))
        Else
            rtb.SelectionFont = New Font(rtb.SelectionFont, rtb.SelectionFont.Style Or FontStyle.Bold)
        End If
    End Sub
    Sub italicizeSelection()
        If rtb.SelectionFont.Style And FontStyle.Italic Then
            rtb.SelectionFont = New Font(rtb.SelectionFont, rtb.SelectionFont.Style And (FontStyle.Bold Or FontStyle.Strikeout Or FontStyle.Underline))
        Else
            rtb.SelectionFont = New Font(rtb.SelectionFont, rtb.SelectionFont.Style Or FontStyle.Italic)
        End If
    End Sub
    Sub underlineSelection()
        If rtb.SelectionFont.Style And FontStyle.Underline Then
            rtb.SelectionFont = New Font(rtb.SelectionFont, rtb.SelectionFont.Style And (FontStyle.Bold Or FontStyle.Strikeout Or FontStyle.Italic))
        Else
            rtb.SelectionFont = New Font(rtb.SelectionFont, rtb.SelectionFont.Style Or FontStyle.Underline)
        End If
    End Sub
    Sub strikeoutSelection()
        If rtb.SelectionFont.Style And FontStyle.Strikeout Then
            rtb.SelectionFont = New Font(rtb.SelectionFont, rtb.SelectionFont.Style And (FontStyle.Bold Or FontStyle.Italic Or FontStyle.Underline))
        Else
            rtb.SelectionFont = New Font(rtb.SelectionFont, rtb.SelectionFont.Style Or FontStyle.Strikeout)
        End If
    End Sub
    Sub highlightSelection()
        If rtb.SelectionBackColor <> Color.White Then
            rtb.SelectionBackColor = Color.White
        Else
            rtb.SelectionBackColor = mainform.currHighlightColor
        End If
        rtb.Select(rtb.SelectionStart, 0)
    End Sub
    'Sub highlightSelection1()
    '    If rtb.SelectionBackColor = Color.Pink Then
    '        rtb.SelectionBackColor = Color.White
    '    Else
    '        rtb.SelectionBackColor = Color.Pink
    '    End If
    'End Sub
    Sub redSelection()
        If rtb.SelectionColor = Color.Red Then
            rtb.SelectionColor = Color.Black
        Else
            rtb.SelectionColor = Color.Red
        End If
    End Sub
    Sub changeFontSize(ByVal delta As Integer)
        rtb.SelectionFont = New Font(rtb.SelectionFont.FontFamily, rtb.SelectionFont.Size + delta, rtb.SelectionFont.Style)
    End Sub
    Sub toggleProportionalFont()
        If rtb.SelectionFont.FontFamily.Equals(basefont.FontFamily) Then
            rtb.SelectionFont = New Font(FontFamily.GenericMonospace, rtb.SelectionFont.Size, rtb.SelectionFont.Style)
        Else
            rtb.SelectionFont = New Font(basefont.FontFamily, rtb.SelectionFont.Size, rtb.SelectionFont.Style)
        End If
    End Sub

    Dim rxpre As New Regex("^.+?:")
    Dim blanks As String = "                                                  "
    Sub columnizeSelection()
        Dim lx1 As Integer = getLogicalLineFromCharIndex(rtb, rtb.SelectionStart)
        Dim lx2 As Integer = getLogicalLineFromCharIndex(rtb, rtb.SelectionStart + rtb.SelectionLength - 1)
        Dim s As String
        Dim maxpre As Integer = 0
        Dim m As Match
        For i As Integer = lx1 To lx2
            s = rtb.Lines(i)
            m = rxpre.Match(s)
            If m.Success Then
                If m.Length > maxpre Then maxpre = m.Length
            End If
        Next
        If maxpre = 0 Then Return
        For i As Integer = lx1 To lx2
            s = rtb.Lines(i)
            m = rxpre.Match(s)
            If m.Success Then
                Dim linestart As Integer = getFirstCharIndexFromLogicalLine(rtb, i)
                rtb.Select(linestart + m.Length, 0)
                Dim nspcs As Integer = maxpre - m.Length + 4
                If nspcs > 49 Then nspcs = 49
                rtb.SelectedText = blanks.Substring(0, nspcs)
            End If
        Next
    End Sub
    Sub timeStamp(Optional ByVal msg As String = "")
        rtb.SelectedText = "DT:: " & Now.ToString(dateformat) & msg
        rtb.Select(rtb.SelectionStart - 17, 17)
        boldSelection()
        rtb.Select(rtb.SelectionStart + 17, 0)
        boldSelection()
    End Sub
    Public Shared helptext As String = "Welcome to RTFDog, a lightweight RTF editor" & vbCrLf & _
            "F1: Show help" & vbCrLf & _
            "Cntl-M or tab: indent block by 1" & vbCrLf & _
            "Shift cntl-M or shift-tab: unindent block by 1" & vbCrLf & _
            "Cntl-V: Paste" & vbCrLf & _
            "Cntl-D: Time stamp" & vbCrLf & _
            "Cntl-F: Search for string or regex" & vbCrLf & _
            "F3: Repeat find" & vbCrLf & _
            "Cntl-B: Bold selection" & vbCrLf & _
            "Cntl-I: Italicize selection" & vbCrLf & _
            "Cntl-U: Underline selection" & vbCrLf & _
            "Cntl-S: Strikeout selection" & vbCrLf & _
            "Cntl-P: Toggle proportional/monospace font" & vbCrLf & _
            "Cntl-R: Toggle color selection red" & vbCrLf & _
            "Cntl-Q: Toggle highlight selection" & vbCrLf & _
            "Cntl + or cntl =: increase font size of selection" & vbCrLf & _
            "Cntl -: decrease font size" & vbCrLf & _
            "Cntl Z/Y: undo/redo" & vbCrLf & _
            "Cntl PgUp / PgDn: Same as next/prev button, see below" & vbCrLf & _
            "Buttons:" & vbCrLf & _
            "(Upper box is search, regex is ok, lower is replace. In upper box ^ at end is intepreted as .+?\s)" & vbCrLf & _
            "X -- clear search boxes" & vbCrLf & _
            "F -- find next" & vbCrLf & _
            "M -- highlight search term with selected color throughout document" & vbCrLf & _
            "1 -- replace selected hit. If no hit selected, find next" & vbCrLf & _
            "A -- replace all" & vbCrLf & _
            "C -- clear highlight from M" & vbCrLf & _
            "Color buttons -- selected highlight color" & vbCrLf & _
            "+- -- increase/decrease zoom" & vbCrLf
      

    Sub showHelp()
        MessageBox.Show(helptext, "Keyboard shortcuts")
    End Sub
    Private Sub rtb_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkClickedEventArgs) Handles rtb.LinkClicked
        System.Diagnostics.Process.Start(e.LinkText)
    End Sub
    Private Sub rtb_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rtb.TextChanged
        changedSinceSaveQ = True
        If (Now - lastSave).Minutes > autosaveInterval Then
            saveFile(currfpath)
            'timeStamp(" Resume")
        End If
    End Sub
    Dim rxlink As New Regex("\[\[(\w+?):///(.+?)\]\]", RegexOptions.IgnoreCase)
    Dim Gxdir As String = "C:\js\AAD\Dropbox\GxDb\FTSDir\"
    Dim AAAdir As String = "C:\js\AAD\Dropbox\AAA\"
    Private Sub rtb_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles rtb.MouseDoubleClick
        Dim sidx As Integer = rtb.SelectionStart
        Dim mpos As Point = e.Location
        Dim midx As Integer = rtb.GetCharIndexFromPosition(mpos)
        Dim m As Match
        Dim x0, x2 As Integer
        Dim ltype As String = ""
        Dim lpath As String = ""
        x2 = rtb.Find("]]", midx, RichTextBoxFinds.None) + 2
        If x2 > 0 Then
            While True
                x0 = rtb.Find("[[", 0, x2, RichTextBoxFinds.Reverse)
                If x0 < 0 Then Exit While
                m = rxlink.Match(rtb.Text, x0)
                If m.Success Then
                    ltype = m.Groups(1).Value
                    lpath = m.Groups(2).Value
                    Exit While
                End If
            End While
            Select Case ltype.ToLower
                Case "onenote"
                    System.Diagnostics.Process.Start("onenote:///" & lpath)
                Case "source"
                    If File.Exists(Path.Combine(AAAdir, lpath)) Then
                        System.Diagnostics.Process.Start(Path.Combine(AAAdir, lpath))
                    ElseIf File.Exists(Path.Combine(AAAdir, lpath)) Then
                        System.Diagnostics.Process.Start(Path.Combine(Gxdir, lpath))
                    End If
                Case "local"
                    Dim app As Process = System.Diagnostics.Process.Start("file://" & lpath)
            End Select
        End If
    End Sub



End Class
