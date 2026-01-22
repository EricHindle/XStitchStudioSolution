' Hindleware
' Copyright (c) 2025-6 Eric Hindle
' All rights reserved.
'
' Author Eric Hindle
'

Imports System.Drawing.Printing
Imports System.Security.Cryptography
Imports System.Text
Imports HindlewareLib.Imaging
Imports HindlewareLib.Logging
Imports XStitchStudio.Domain.Objects
Public Class FrmPrintProject
#Region "properties"
    Private oPrintProject As Project
    Public WriteOnly Property PrintProject() As Project
        Set(ByVal value As Project)
            oPrintProject = value
        End Set
    End Property
#End Region

#Region "constants"
#End Region
#Region "variables"
    Private gap As Integer
    Private isComponentInitialised As Boolean
    Private isPagesLoaded As Boolean = False
    Private isPrintKey As Boolean
    Private isPrintLoading As Boolean = False
    Private oFormImage As Bitmap
    Private oOverlapBrush As SolidBrush
    Private oPageList As List(Of Page)
    Private oPrintBitmap As Bitmap
    Private oPrintBorderBrush As Brush = Brushes.Black
    Private oPrintBorderPen = New Pen(oPrintBorderBrush, oPrintBorderwidth)
    Private oPrintBorderwidth As Integer = 5
    Private oPrintCentreBrush As Brush = Brushes.Red
    Private oPrintCentrePen = New Pen(oPrintCentreBrush, oPrintCentrewidth)
    Private oPrintCentrewidth As Integer = 3
    Private oPrintGraphics As Graphics
    Private oPrintGrid10Brush As Brush = Brushes.Black
    Private oPrintGrid10width As Integer = 2
    Private oPrintGrid10Pen As New Pen(oPrintGrid10Brush, oPrintGrid10width)
    Private oPrintGrid1Brush As Brush = Brushes.DarkGray
    Private oPrintGrid1width As Integer = 1
    Private oPrintGrid1Pen As New Pen(oPrintGrid1Brush, oPrintGrid1width)
    Private oPrintKeyBrush As Brush = Brushes.Black
    Private oPrintKeyPen As New Pen(oPrintKeyBrush, 1)
    Private oPrintGrid5Brush As Brush = Brushes.DimGray
    Private oPrintGrid5width As Integer = 1
    Private oPrintGrid5Pen As New Pen(oPrintGrid5Brush, oPrintGrid5width)
    Private oSelectedPage As New Page
    Private oTextBrush As Brush = Brushes.Black
#End Region
#Region "form control handlers"
    Private Sub FrmPrintProject_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LogUtil.LogInfo("Opening print", MyBase.Name)
        GetFormPos(Me, My.Settings.PrintFormPos)
        InitialiseForm()
    End Sub
    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub
    Private Sub FrmStitchDesign_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        LogUtil.LogInfo("Closing...", MyBase.Name)
        RbPortrait.Checked = True
        My.Settings.PrintFormPos = SetFormPos(Me)
        My.Settings.Save()
        PenDispose()
    End Sub
    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrintPage.Click
        LogUtil.ShowStatus("Printing page", LblStatus, MyBase.Name)
        InitialisePrintDocument()
        oPrintDoc.PrinterSettings.PrinterName = CmbInstalledPrinters.SelectedItem
        ' Set handler to print image 
        AddHandler oPrintDoc.PrintPage, AddressOf OnPrintImage
        ' Print the image (calls PrintPage handler (see above))
        oPrintDoc.Print()
    End Sub
    Private Sub BtnPrintAll_Click(sender As Object, e As EventArgs) Handles BtnPrintAll.Click
        If isPagesLoaded Then
            LogUtil.ShowStatus("Printing all pages", LblStatus, MyBase.Name)
            InitialisePrintDocument()
            oPrintDoc.PrinterSettings.PrinterName = CmbInstalledPrinters.SelectedItem
            AddHandler oPrintDoc.PrintPage, AddressOf OnPrintImage
            For Each _page As Page In oPageList
                If _page.PageType = PageType.DesignPage Then
                    oSelectedPage = _page
                    CreatePrintBitmap()
                    oPrintDoc.Print()
                End If
            Next
            If isPrintKey Then
                For Each _page As Page In oPageList
                    If _page.PageType = PageType.KeyPage Then
                        PrintKeyPage()
                    End If
                Next
            End If
        End If
    End Sub
    Private Sub BtnPrintKey_Click(sender As Object, e As EventArgs) Handles BtnPrintKey.Click
        PrintKeyPage()
    End Sub

    Private Sub PrintKeyPage()
        LogUtil.ShowStatus("Printing key", LblStatus, MyBase.Name)
        'isLandscape = False
        'SetPortraitValues()
        InitialisePrintDocument()
        oPrintDoc.PrinterSettings.PrinterName = CmbInstalledPrinters.SelectedItem
        ' Set handler to print image 
        AddHandler oPrintDoc.PrintPage, AddressOf OnPrintImage
        CreateKeyBitmap()
        ' Print the image (calls PrintPage handler (see above))
        oPrintDoc.Print()
    End Sub

    Private Sub BtnFont_Click(sender As Object, e As EventArgs) Handles BtnTitleFont.Click,
                                                                        BtnTextFont.Click,
                                                                        BtnFooterFont.Click
        Dim _button As Button = CType(sender, Button)
        SelectFont(_button)
    End Sub
    Private Sub BtnMoreSettings_Click(sender As Object, e As EventArgs) Handles BtnMoreSettings.Click
        ShowPrintSettingsForm()
    End Sub
    Private Sub BtnSaveSettings_Click(sender As Object, e As EventArgs) Handles BtnSaveSettings.Click
        SaveSettings()
    End Sub
    Private Sub ParameterValueChanged(sender As Object, e As EventArgs) Handles NudSqrPerInch.ValueChanged,
                                                                                ChkPrintGrid.CheckedChanged,
                                                                                ChkCentreMarks.CheckedChanged,
                                                                                ChkCentreLines.CheckedChanged,
                                                                                CbDisplayStyle.SelectedIndexChanged,
                                                                                NudBottomMargin.ValueChanged,
                                                                                NudLeftMargin.ValueChanged,
                                                                                NudRightMargin.ValueChanged,
                                                                                NudOverlap.ValueChanged,
                                                                                ChkPrintHeader.CheckedChanged,
                                                                                ChkPrintFooter.CheckedChanged,
                                                                                CbShading.SelectedIndexChanged,
                                                                                ChkShowPageOrder.CheckedChanged
        If isComponentInitialised And Not isPrintLoading Then
            LoadFormPages()
        End If
    End Sub
    Private Sub ChkPrintKey_CheckedChanged(sender As Object, e As EventArgs) Handles ChkPrintKey.CheckedChanged
        isPrintKey = ChkPrintKey.Checked
    End Sub
    Private Sub RbLandscape_CheckedChanged(sender As Object, e As EventArgs) Handles RbLandscape.CheckedChanged
        If isComponentInitialised Then
            isLandscape = RbLandscape.Checked
            If isLandscape Then
                SetLandscapeValues()
            Else
                SetPortraitValues()
            End If
            If Not isPrintLoading Then
                InitialisePrintDocument()
                LoadFormPages()
            End If
        End If
    End Sub

    Private Sub SetPortraitValues()
        oPagesize = New Size(A4_WIDTH, A4_HEIGHT)
        Me.Size = New Size(Me.Size.Width - oWidthHeightDifference, Me.Size.Height)
        PnlDesignPicture.Size = New Size(PnlDesignPicture.Size.Width - oWidthHeightDifference, PnlDesignPicture.Height + oWidthHeightDifference)
        PicDesign.Size = New Size(PicDesign.Size.Width - oWidthHeightDifference, PicDesign.Height + oWidthHeightDifference)
    End Sub

    Private Sub SetLandscapeValues()
        oPagesize = New Size(A4_HEIGHT, A4_WIDTH)
        Me.Size = New Size(Me.Size.Width + oWidthHeightDifference, Me.Size.Height)
        PnlDesignPicture.Size = New Size(PnlDesignPicture.Size.Width + oWidthHeightDifference, PnlDesignPicture.Height - oWidthHeightDifference)
        PicDesign.Size = New Size(PicDesign.Size.Width + oWidthHeightDifference, PicDesign.Height - oWidthHeightDifference)
    End Sub
#End Region
#Region "subroutines"
    Private Sub InitialiseForm()
        isComponentInitialised = True
        isPrintLoading = True
        oPagesize = New Size(A4_WIDTH, A4_HEIGHT)
        oWidthHeightDifference = PnlDesignPicture.Height - PnlDesignPicture.Width
        If oPrintProject.DesignHeight < oPrintProject.DesignWidth Then
            RbLandscape.Checked = True
        End If
        LoadInstalledPrinters()
        InitialisePrintDocument()
        oPageToFormRatio = Math.Ceiling(oPagesize.Width / PicDesign.Width)
        LoadFormFromSettings()
        SetPrintFonts()
        CbDisplayStyle.SelectedIndex = oStitchDisplayStyle
        TxtTitle.Text = oPrintProject.ProjectName
        SetThreadGroups()
        isPrintLoading = False
        LoadFormPages()
    End Sub
    Private Sub LoadFormFromSettings()
        BtnFooterFont.Font = My.Settings.PrintFooterFont
        BtnTextFont.Font = My.Settings.PrintTextFont
        BtnTitleFont.Font = My.Settings.PrintTitleFont
        CbKeyOrder.SelectedIndex = My.Settings.PrintKeyOrder
        CbShading.SelectedIndex = My.Settings.OverlapShading
        ChkCentreLines.Checked = My.Settings.PrintCentreLines
        ChkCentreMarks.Checked = My.Settings.PrintCentreMarks
        ChkPrintFooter.Checked = My.Settings.isPrintFooter
        ChkPrintGrid.Checked = My.Settings.PrintGrid
        ChkPrintHeader.Checked = My.Settings.isPrintHeader
        ChkPrintKey.Checked = My.Settings.isPrintKey
        isPrintKey = My.Settings.isPrintKey
        ChkShowPageOrder.Checked = My.Settings.isShowPageOrder
        NudBottomMargin.Value = My.Settings.PrintMarginBottom
        NudLeftMargin.Value = My.Settings.PrintMarginLeft
        NudOverlap.Value = My.Settings.TilingOverlap
        NudRightMargin.Value = My.Settings.PrintMarginRight
        NudSqrPerInch.Value = My.Settings.PrintSquaresPerInch
        NudTopMargin.Value = My.Settings.PrintMarginTop
        TxtCopyright.Text = My.Settings.CopyrightBy
        TxtDesignBy.Text = My.Settings.DesignBy
        isPrintColumnNumbers = My.Settings.PrintColumnNumbers
        isPrintRowNumbers = My.Settings.PrintRowNumbers
        SetPens()
    End Sub
    Private Sub SetPens()
        oPrintBorderBrush = Brushes.Black
        oPrintBorderwidth = My.Settings.PrintBorderThickness
        oPrintBorderPen = New Pen(oPrintBorderBrush, oPrintBorderwidth)
        oPrintCentreBrush = New SolidBrush(My.Settings.PrintCentreLineColour)
        oPrintCentrewidth = My.Settings.PrintCentreLineThickness
        oPrintCentrePen = New Pen(oPrintCentreBrush, oPrintCentrewidth)
        oPrintGrid1Brush = New SolidBrush(GetColourFromProject(My.Settings.PrintGrid1Colour, oGridColourList))
        oPrintGrid1width = 1
        oPrintGrid1Pen = New Pen(oPrintGrid1Brush, oPrintGrid1width)
        oPrintGrid5Brush = New SolidBrush(GetColourFromProject(My.Settings.PrintGrid5Colour, oGridColourList))
        oPrintGrid5width = 1
        oPrintGrid5Pen = New Pen(oPrintGrid5Brush, oPrintGrid5width)
        oPrintGrid10Brush = New SolidBrush(GetColourFromProject(My.Settings.PrintGrid10Colour, oGridColourList))
        oPrintGrid10width = 2
        oPrintGrid10Pen = New Pen(oPrintGrid10Brush, oPrintGrid10width)
    End Sub
    Private Sub SaveSettings()
        My.Settings.isPrintKey = ChkPrintKey.Checked
        My.Settings.PrintKeyOrder = CbKeyOrder.SelectedIndex
        My.Settings.PrintSquaresPerInch = NudSqrPerInch.Value
        My.Settings.TilingOverlap = NudOverlap.Value
        My.Settings.OverlapShading = CbShading.SelectedIndex
        My.Settings.isShowPageOrder = ChkShowPageOrder.Checked
        My.Settings.PrintMarginTop = NudTopMargin.Value
        My.Settings.PrintMarginBottom = NudBottomMargin.Value
        My.Settings.PrintMarginLeft = NudLeftMargin.Value
        My.Settings.PrintMarginRight = NudRightMargin.Value
        My.Settings.isPrintHeader = ChkPrintHeader.Checked
        My.Settings.DesignBy = TxtDesignBy.Text
        My.Settings.CopyrightBy = TxtCopyright.Text
        My.Settings.PrintGrid = ChkPrintGrid.Checked
        My.Settings.PrintCentreMarks = ChkCentreMarks.Checked
        My.Settings.PrintCentreLines = ChkCentreLines.Checked
        My.Settings.PrintTitleFont = BtnTitleFont.Font
        My.Settings.PrintTextFont = BtnTextFont.Font
        My.Settings.PrintFooterFont = BtnFooterFont.Font
        My.Settings.Save()
    End Sub
    Private Sub SelectFont(ByRef pButton As Button)
        FontDialog1.Font = pButton.Font
        FontDialog1.ShowDialog()
        pButton.Font = FontDialog1.Font
        SetPrintFonts()
        LoadFormPages()
    End Sub
    Private Sub PenDispose()
        oPrintBorderPen.dispose
        oPrintCentrePen.dispose
        oPrintGrid10Pen.Dispose()
        oPrintGrid1Pen.Dispose()
        oPrintGrid5Pen.Dispose()
        oPrintKeyPen.Dispose()
    End Sub
    Private Sub OnPrintImage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs)
        e.Graphics.DrawImage(oPrintBitmap, New Point(0, 0))
        LogUtil.ShowStatus("Printing done", LblStatus, MyBase.Name)
    End Sub
    Private Sub LoadFormPages()
        isPagesLoaded = False
        isPrintHeader = ChkPrintHeader.Checked
        isPrintFooter = ChkPrintFooter.Checked
        oPagePixelsPerCell = Math.Floor(PRINT_DPI / NudSqrPerInch.Value)
        SetPrintPageMargins(NudLeftMargin.Value, NudRightMargin.Value, NudTopMargin.Value, NudBottomMargin.Value)
        CalculatePrintGridSpace(New Size(oPrintProject.DesignWidth, oPrintProject.DesignHeight))
        oOverlapBrush = New SolidBrush(oOverlapColourList(CbShading.SelectedIndex))
        LblOnePage.Text = String.Format(LblOnePage.Text, oOnePageSqPerInch)
        InitialisePageLists()
    End Sub
    Private Sub SetPrintFonts()
        oPrintTitlefont = New Font(BtnTitleFont.Font.FontFamily, BtnTitleFont.Font.SizeInPoints, BtnTitleFont.Font.Style, GraphicsUnit.Point)
        oPrintGroupfont = New Font(BtnTitleFont.Font.FontFamily, BtnTextFont.Font.SizeInPoints, FontStyle.Bold, GraphicsUnit.Point)
        oPrintTextfont = New Font(BtnTextFont.Font.FontFamily, BtnTextFont.Font.SizeInPoints, BtnTextFont.Font.Style, GraphicsUnit.Point)
        oPrintFooterfont = New Font(BtnFooterFont.Font.FontFamily, BtnFooterFont.Font.SizeInPoints, BtnFooterFont.Font.Style, GraphicsUnit.Point)
    End Sub
    Private Sub CreatePageGraphics(pPage As Page, ByRef pPageGraphics As Graphics, pSize As Size)
        gap = oPagePixelsPerCell
        Dim _footerText As String = BuildFooter(pPage, ChkShowPageOrder.Checked)
        pPageGraphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        pPageGraphics.Clear(Color.White)
        If NudOverlap.Value > 0 Then
            PrintOverlapShade(pPage, pPageGraphics)
        End If
        For Each _blockstitch In oProjectDesign.BlockStitches
            Dim _actualPosition As New Point(_blockstitch.BlockPosition.X + oPrintProject.OriginX, _blockstitch.BlockPosition.Y + oPrintProject.OriginY)
            If _actualPosition.X >= pPage.TopLeft.X _
            And _actualPosition.X < pPage.BottomRight.X _
            And _actualPosition.Y >= pPage.TopLeft.Y _
            And _actualPosition.Y < pPage.BottomRight.Y Then
                If _blockstitch.IsLoaded Then
                    Select Case _blockstitch.StitchType
                        Case BlockStitchType.Full
                            PrintFullBlockStitch(_blockstitch, pPageGraphics, CbDisplayStyle.SelectedIndex, pPage)
                            'Case BlockStitchType.Half
                            '    PrintHalfBlockStitch(_blockstitch, True, pPageGraphics, pPage)
                            'Case BlockStitchType.Quarter
                            '    PrintQuarterBlockStitch(_blockstitch, pPageGraphics, pPage)
                            'Case BlockStitchType.ThreeQuarter
                            '    PrintThreeQuarterBlockStitch(_blockstitch, pPageGraphics, pPage)
                        Case Else
                            PrintQuarterBlockStitches(_blockstitch, pPageGraphics, CbDisplayStyle.SelectedIndex, pPage)
                    End Select
                End If
            End If
        Next
        PrintGrid(pPage, pPageGraphics, pSize)
        If My.Settings.IsShowBackstitches Then
            For Each _backstitch In oProjectDesign.BackStitches
                Dim _actualFromPosition As New Point(_backstitch.FromBlockPosition.X + oPrintProject.OriginX, _backstitch.FromBlockPosition.Y + oPrintProject.OriginY)
                Dim _actualtoPosition As New Point(_backstitch.ToBlockPosition.X + oPrintProject.OriginX, _backstitch.ToBlockPosition.Y + oPrintProject.OriginY)
                If (_actualFromPosition.X >= pPage.TopLeft.X _
                    And _actualFromPosition.X <= pPage.BottomRight.X _
                    And _actualFromPosition.Y >= pPage.TopLeft.Y _
                    And _actualFromPosition.Y < pPage.BottomRight.Y) _
                Or
                        (_actualtoPosition.X >= pPage.TopLeft.X _
                    And _actualtoPosition.X <= pPage.BottomRight.X _
                    And _actualtoPosition.Y >= pPage.TopLeft.Y _
                    And _actualtoPosition.Y < pPage.BottomRight.Y) Then
                    PrintBackstitch(_backstitch, pPageGraphics, pPage)
                End If
            Next
        End If
        If My.Settings.IsShowKnots Then
            For Each _knot As Knot In oProjectDesign.Knots
                Dim _actualPosition As New Point(_knot.BlockPosition.X + oPrintProject.OriginX, _knot.BlockPosition.Y + oPrintProject.OriginY)
                If _actualPosition.X >= pPage.TopLeft.X _
                    And _actualPosition.X <= pPage.BottomRight.X _
                    And _actualPosition.Y >= pPage.TopLeft.Y _
                    And _actualPosition.Y < pPage.BottomRight.Y Then
                    PrintKnot(_knot, pPageGraphics, pPage)
                End If
            Next
        End If
        ClearMargins(pPage, pPageGraphics)
        PrintHeaderFooter(pPageGraphics, pSize, _footerText)
        PrintRowColumnNumbers(pPage, pPageGraphics)
    End Sub
    Private Sub CreateKeyGraphics(pPageGraphics As Graphics, pSize As Size, pTopLeft As Point, pTableAreaSize As Size)
        Dim _footerText As String = BuildFooter(Nothing, False)
        Dim oCurrentTopLeft As Point = pTopLeft
        Dim oThreadCollection As New ProjectThreadCollection()
        InitialiseTable(If(isLandscape, 3, 2))
        pPageGraphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        pPageGraphics.Clear(Color.White)
        PrintHeaderFooter(pPageGraphics, pSize, _footerText)
        If oCrossDoubleThreadList.Count > 0 Then
            DrawGroup(oCrossDoubleThreadList, GroupType.CrossDouble, pPageGraphics, oCurrentTopLeft, pTableAreaSize)
        End If
        If oCrossSingleThreadList.Count > 0 Then
            DrawGroup(oCrossSingleThreadList, GroupType.CrossSingle, pPageGraphics, oCurrentTopLeft, pTableAreaSize)
        End If
        If oBackDoubleThreadList.Count > 0 Then
            DrawGroup(oBackDoubleThreadList, GroupType.BackDouble, pPageGraphics, oCurrentTopLeft, pTableAreaSize)
        End If
        If oBackSingleThreadList.Count > 0 Then
            DrawGroup(oBackSingleThreadList, GroupType.BackSingle, pPageGraphics, oCurrentTopLeft, pTableAreaSize)
        End If
        If oKnotDoubleThreadList.Count > 0 Then
            DrawGroup(oKnotDoubleThreadList, GroupType.KnotDouble, pPageGraphics, oCurrentTopLeft, pTableAreaSize)
        End If
        If oKnotSingleThreadList.Count > 0 Then
            DrawGroup(oKnotSingleThreadList, GroupType.KnotSingle, pPageGraphics, oCurrentTopLeft, pTableAreaSize)
        End If
    End Sub
    Private Sub DrawGroup(pThreadCollection As ProjectThreadCollection, pGroupType As GroupType, pPageGraphics As Graphics, ByRef pCurrentTopLeft As Point, pTableAreaSize As Size)
        Dim headerText As String = String.Empty
        If CbKeyOrder.SelectedIndex = 2 Then
            pThreadCollection.Threads.Sort(Function(x As ProjectThread, y As ProjectThread) x.Thread.SortNumber.CompareTo(y.Thread.SortNumber))
        Else
            pThreadCollection.Threads.Sort(Function(x As ProjectThread, y As ProjectThread) x.Thread.ColourName.CompareTo(y.Thread.ColourName))
        End If
        Select Case pGroupType
            Case GroupType.CrossSingle
                headerText = CROSS_SINGLE_STRAND_HEADER
            Case GroupType.CrossDouble
                headerText = CROSS_DOUBLE_STRAND_HEADER
            Case GroupType.BackSingle
                headerText = BACK_SINGLE_STRAND_HEADER
            Case GroupType.BackDouble
                headerText = BACK_DOUBLE_STRAND_HEADER
            Case GroupType.KnotSingle
                headerText = KNOT_SINGLE_STRAND_HEADER
            Case GroupType.KnotDouble
                headerText = KNOT_DOUBLE_STRAND_HEADER
        End Select
        oTableHeight = oRowHeight * (pThreadCollection.Count + 1)
        If pCurrentTopLeft.Y + (oRowHeight * 3) >= pTableAreaSize.Height Then
            pCurrentTopLeft = New Point(pCurrentTopLeft.X + oTableWidth, oTopMargin)
        End If
        If pCurrentTopLeft.X > pTableAreaSize.Width - oLeftMargin Then
            StartNewKeyPage()
        Else
            DrawGroupHeader(pPageGraphics, pCurrentTopLeft, headerText, oRowHeight)
            DrawKeyTable(pPageGraphics, oTableWidth, oRowHeight, oTableHeight, oColumn1Width, oColumn2Width, oColumn3Width, pCurrentTopLeft, pTableAreaSize)
            DrawThreadKey(pPageGraphics, pTableAreaSize, pThreadCollection, oTableWidth, oRowHeight, oTableHeight, oColumn1Width, oColumn2Width, oColumn3Width, oSymbolWidth, oBoxWidth, pCurrentTopLeft, pGroupType)
        End If
    End Sub
    Private Sub StartNewKeyPage()

    End Sub
    Private Sub DrawThreadKey(pPageGraphics As Graphics, pTableAreaSize As Size, oThreadCollection As ProjectThreadCollection, oTableWidth As Integer, oRowHeight As Integer, oTableHeight As Integer, oColumn1Width As Integer, oColumn2Width As Integer, oColumn3Width As Integer, oSymbolWidth As Integer, oBoxWidth As Integer, ByRef pCurrentTopLeft As Point, pGrouptype As GroupType)
        For Each _thread As ProjectThread In oThreadCollection.Threads
            If pCurrentTopLeft.Y + oRowHeight >= pTableAreaSize.Height Then
                pCurrentTopLeft = New Point(pCurrentTopLeft.X + oTableWidth, oTopMargin + oRowHeight)
                If pCurrentTopLeft.X + oTableWidth > pTableAreaSize.Width - oLeftMargin Then
                    Exit For
                End If
                DrawKeyTable(pPageGraphics, oTableWidth, oRowHeight, oTableHeight, oColumn1Width, oColumn2Width, oColumn3Width, pCurrentTopLeft, pTableAreaSize)
            End If
            Dim oColourBrush As Brush = New SolidBrush(_thread.Thread.Colour)
            Dim oKnotWidth As Integer = oSymbolWidth - 4
            Dim oSmallKnotWidth As Integer = oKnotWidth - 4
            Dim _symbol As Bitmap = Nothing
            If pGrouptype = GroupType.CrossDouble AndAlso _thread.DoubleThreadSymbolId >= 0 Then
                _symbol = ImageUtil.ResizeImage(_thread.FindDoubleThreadSymbolImage, oSymbolWidth, oSymbolWidth)
            End If
            If pGrouptype = GroupType.CrossSingle AndAlso _thread.SingleThreadSymbolId >= 0 Then
                _symbol = ImageUtil.ResizeImage(_thread.FindSingleThreadSymbolImage, oSymbolWidth, oSymbolWidth)
            End If
            If _symbol IsNot Nothing Then
                _symbol.SetResolution(PRINT_DPI, PRINT_DPI)
                pPageGraphics.DrawImage(_symbol, New Point(pCurrentTopLeft.X + ((oColumn1Width - oSymbolWidth) / 2), pCurrentTopLeft.Y + (oRowHeight - oSymbolWidth) / 2))
            End If
            Dim lineStart As New Point(pCurrentTopLeft.X + ((oColumn1Width - oBoxWidth) / 2), pCurrentTopLeft.Y + (oRowHeight / 2))
            Dim lineEnd As New Point(pCurrentTopLeft.X + ((oColumn1Width + oBoxWidth) / 2), pCurrentTopLeft.Y + (oRowHeight / 2))
            If pGrouptype = GroupType.BackDouble Then
                pPageGraphics.DrawLine(New Pen(oColourBrush, SetStitchPenWidth(True, oPagePixelsPerCell)), lineStart, lineEnd)
            End If
            If pGrouptype = GroupType.BackSingle Then
                pPageGraphics.DrawLine(New Pen(oColourBrush, SetStitchPenWidth(False, oPagePixelsPerCell)), lineStart, lineEnd)
            End If
            If pGrouptype = GroupType.KnotDouble Then
                pPageGraphics.FillEllipse(oColourBrush, New Rectangle(New Point(pCurrentTopLeft.X + ((oColumn1Width - oKnotWidth) / 2), pCurrentTopLeft.Y + (oRowHeight - oKnotWidth) / 2), New Size(oKnotWidth, oKnotWidth)))
            End If
            If pGrouptype = GroupType.KnotSingle Then
                pPageGraphics.FillEllipse(oColourBrush, New Rectangle(New Point(pCurrentTopLeft.X + ((oColumn1Width - oSmallKnotWidth) / 2), pCurrentTopLeft.Y + (oRowHeight - oSmallKnotWidth) / 2), New Size(oSmallKnotWidth, oSmallKnotWidth)))
            End If
            pPageGraphics.DrawRectangle(oPrintKeyPen, New Rectangle(New Point(pCurrentTopLeft.X + ((oColumn1Width - oBoxWidth) / 2), pCurrentTopLeft.Y + (oRowHeight - oBoxWidth) / 2), New Size(oBoxWidth, oBoxWidth)))
            pPageGraphics.DrawString(_thread.Thread.ColourName, oPrintTextfont, oPrintKeyBrush, New Point(pCurrentTopLeft.X + 3 + oColumn1Width, pCurrentTopLeft.Y + 3))
            pPageGraphics.FillRectangle(oColourBrush, New Rectangle(New Point(pCurrentTopLeft.X + oColumn1Width + oColumn2Width + ((oColumn3Width - oBoxWidth) / 2), pCurrentTopLeft.Y + (oRowHeight - oBoxWidth) / 2), New Size(oBoxWidth, oBoxWidth)))
            pPageGraphics.DrawString(_thread.Thread.ThreadNo, oPrintTextfont, oPrintKeyBrush, New Point(pCurrentTopLeft.X + 3 + oColumn1Width + oColumn2Width + oColumn3Width, pCurrentTopLeft.Y + 3))
            pCurrentTopLeft = New Point(pCurrentTopLeft.X, pCurrentTopLeft.Y + oRowHeight)
            pPageGraphics.DrawLine(oPrintKeyPen, New Point(pCurrentTopLeft.X, pCurrentTopLeft.Y), New Point(pCurrentTopLeft.X + oTableWidth, pCurrentTopLeft.Y))

        Next
    End Sub

    Private Sub DrawKeyTable(pPageGraphics As Graphics, oTableWidth As Integer, oRowHeight As Integer, oTableHeight As Integer, oColumn1Width As Integer, oColumn2Width As Integer, oColumn3Width As Integer, ByRef pCurrentTopLeft As Point, pTableAreaSize As Size)
        Dim iAvailableHeight As Integer = Math.Min(oTableHeight, (Math.Floor((pTableAreaSize.Height - pCurrentTopLeft.Y + oTopMargin) / oRowHeight) - 1) * oRowHeight)
        ' Top border
        pPageGraphics.DrawLine(oPrintKeyPen, pCurrentTopLeft, New Point(pCurrentTopLeft.X + oTableWidth, pCurrentTopLeft.Y))
        ' Left border
        pPageGraphics.DrawLine(oPrintKeyPen, pCurrentTopLeft, New Point(pCurrentTopLeft.X, pCurrentTopLeft.Y + iAvailableHeight))
        ' Right border
        pPageGraphics.DrawLine(oPrintKeyPen, New Point(pCurrentTopLeft.X + oTableWidth, pCurrentTopLeft.Y), New Point(pCurrentTopLeft.X + oTableWidth, pCurrentTopLeft.Y + iAvailableHeight))
        ' Bottom border
        pPageGraphics.DrawLine(oPrintKeyPen, New Point(pCurrentTopLeft.X, pCurrentTopLeft.Y + iAvailableHeight), New Point(pCurrentTopLeft.X + oTableWidth, pCurrentTopLeft.Y + iAvailableHeight))
        ' Heading floor
        pPageGraphics.DrawLine(oPrintKeyPen, New Point(pCurrentTopLeft.X, pCurrentTopLeft.Y + oRowHeight), New Point(pCurrentTopLeft.X + oTableWidth, pCurrentTopLeft.Y + oRowHeight))
        ' Col1 side
        pPageGraphics.DrawLine(oPrintKeyPen, New Point(pCurrentTopLeft.X + oColumn1Width, pCurrentTopLeft.Y), New Point(pCurrentTopLeft.X + oColumn1Width, pCurrentTopLeft.Y + iAvailableHeight))
        ' Col2 side
        pPageGraphics.DrawLine(oPrintKeyPen, New Point(pCurrentTopLeft.X + oColumn1Width + oColumn2Width, pCurrentTopLeft.Y), New Point(pCurrentTopLeft.X + oColumn1Width + oColumn2Width, pCurrentTopLeft.Y + iAvailableHeight))
        ' Col3 side
        pPageGraphics.DrawLine(oPrintKeyPen, New Point(pCurrentTopLeft.X + oColumn1Width + oColumn2Width + oColumn3Width, pCurrentTopLeft.Y), New Point(pCurrentTopLeft.X + oColumn1Width + oColumn2Width + oColumn3Width, pCurrentTopLeft.Y + iAvailableHeight))
        ' Heading text 1
        pPageGraphics.DrawString("Key", oPrintTextfont, oPrintKeyBrush, New Point(pCurrentTopLeft.X + 3, pCurrentTopLeft.Y + 3))
        ' Heading text 2
        pPageGraphics.DrawString("Colour Name", oPrintTextfont, oPrintKeyBrush, New Point(pCurrentTopLeft.X + 3 + oColumn1Width, pCurrentTopLeft.Y + 3))
        ' Heading tex 4
        pPageGraphics.DrawString("DMC code", oPrintTextfont, oPrintKeyBrush, New Point(pCurrentTopLeft.X + 3 + oColumn1Width + oColumn2Width + oColumn3Width, pCurrentTopLeft.Y + 3))
        pCurrentTopLeft = New Point(pCurrentTopLeft.X, pCurrentTopLeft.Y + oRowHeight)
    End Sub
    Private Function BuildFooter(pPage As Page, pIsShowPageOrder As Boolean) As String
        Dim _footerText As New StringBuilder
        _footerText.Append("Design By ") _
        .Append(TxtDesignBy.Text) _
        .Append(vbTab) _
        .Append(Chr(169) & " ") _
        .Append(TxtCopyright.Text) _
        .Append(vbTab)
        If pIsShowPageOrder Then
            _footerText.Append("Page ") _
            .Append(pPage.PageNo) _
            .Append(" of ") _
            .Append(CStr(oPageList.Count)) _
            .Append(" ") _
            .Append(vbTab) _
            .Append("Row ") _
            .Append(pPage.PagePosition.Y + 1) _
            .Append(" Column ") _
            .Append(pPage.PagePosition.X + 1)
        End If
        Return _footerText.ToString
    End Function
    Private Sub DrawGroupHeader(pPageGraphics As Graphics, ByRef pCurrentTopLeft As Point, pText As String, pRowHeight As Integer)
        pPageGraphics.DrawString(pText, oPrintGroupfont, oTextBrush, New Point(pCurrentTopLeft.X + 5, pCurrentTopLeft.Y + 8))
        pCurrentTopLeft = New Point(pCurrentTopLeft.X, pCurrentTopLeft.Y + pRowHeight)
    End Sub
    Private Sub PrintHeaderFooter(pPageGraphics As Graphics, pSize As Size, pFooterText As String)
        If isPrintHeader Then
            pPageGraphics.DrawString(TxtTitle.Text, oPrintTitlefont, oTextBrush, New Point(oLeftMargin, oPageTopMargin))
        End If
        Dim oFooterPos As Integer = pSize.Height - oBottomMargin
        If isPrintFooter Then
            pPageGraphics.DrawString(pFooterText, oPrintFooterfont, oTextBrush, New Point(oLeftMargin, oFooterPos))
        End If
        LblTextSizer.Font = BtnFooterFont.Font
        LblTextSizer.Text = PRODUCT_TEXT
        Dim _productTextWidth As Integer = (LblTextSizer.Width * PRINT_DPI) / SCREEN_DPI
        pPageGraphics.DrawString(PRODUCT_TEXT, oPrintFooterfont, oTextBrush, New Point(pSize.Width - oRightMargin - _productTextWidth, oFooterPos))
    End Sub
    Private Sub PrintRowColumnNumbers(pPage As Page, pPageGraphics As Graphics)
        Dim _widthInColumns As Integer = pPage.BottomRight.X - pPage.TopLeft.X
        Dim _heightInRows As Integer = pPage.BottomRight.Y - pPage.TopLeft.Y
        If isPrintRowNumbers Then
            For _row = 0 To _heightInRows
                If Math.IEEERemainder(_row + 1 + pPage.TopLeft.Y, 5) = 0 And _row < _heightInRows Then
                    pPageGraphics.DrawString(CStr(_row + 1 + pPage.TopLeft.Y), oPrintFooterfont, oTextBrush, New Point(oLeftMargin + (gap * _widthInColumns), oTopMargin + (gap * (_row))))
                End If
            Next
        End If
        If isPrintColumnNumbers Then
            For _column = 0 To _widthInColumns
                If Math.IEEERemainder(_column + 1 + pPage.TopLeft.X, 5) = 0 And _column < _widthInColumns Then
                    pPageGraphics.DrawString(CStr(_column + 1 + pPage.TopLeft.X), oPrintFooterfont, oTextBrush, New Point(oLeftMargin + (gap * (_column)), oTopMargin + (gap * _heightInRows)))
                End If
            Next
        End If
    End Sub
    Friend Sub PrintFullBlockStitch(pBlockStitch As BlockStitch, ByRef pDesignGraphics As Graphics, pStitchDisplayStyle As StitchDisplayStyle, pPage As Page)
        Dim _threadColour As Color = pBlockStitch.ProjThread.Thread.Colour
        Dim pX As Integer = ((pBlockStitch.BlockPosition.X + oPrintProject.OriginX - pPage.TopLeft.X) * oPagePixelsPerCell) + oLeftMargin
        Dim pY As Integer = ((pBlockStitch.BlockPosition.Y + oPrintProject.OriginY - pPage.TopLeft.Y) * oPagePixelsPerCell) + oTopMargin
        FullBlockStitch(pBlockStitch, pDesignGraphics, oPagePixelsPerCell, pStitchDisplayStyle, _threadColour, pX, pY)
    End Sub
    Friend Sub PrintQuarterBlockStitches(pBlockstitch As BlockStitch, ByRef pDesignGraphics As Graphics, pStitchDisplayStyle As StitchDisplayStyle, pPage As Page)
        Dim pX As Integer = ((pBlockstitch.BlockPosition.X + oPrintProject.OriginX - pPage.TopLeft.X) * oPagePixelsPerCell) + oLeftMargin
        Dim pY As Integer = ((pBlockstitch.BlockPosition.Y + oPrintProject.OriginY - pPage.TopLeft.Y) * oPagePixelsPerCell) + oTopMargin
        QuarterBlockStitches(pBlockstitch, pDesignGraphics, oPagePixelsPerCell, CbDisplayStyle.SelectedIndex, pX, pY)
    End Sub
    Friend Sub PrintBackstitch(pBackstitch As BackStitch, ByRef pDesignGraphics As Graphics, pPage As Page)
        SetStitchPenWidth(pBackstitch.Strands, oPagePixelsPerCell)

        Dim _fromCellLocation_x As Integer = ((pBackstitch.FromBlockPosition.X + oPrintProject.OriginX - pPage.TopLeft.X) * oPagePixelsPerCell) + oLeftMargin
        Dim _fromCellLocation_y As Integer = ((pBackstitch.FromBlockPosition.Y + oPrintProject.OriginY - pPage.TopLeft.Y) * oPagePixelsPerCell) + oTopMargin
        Dim _toCellLocation_x As Integer = ((pBackstitch.ToBlockPosition.X + oPrintProject.OriginX - pPage.TopLeft.X) * oPagePixelsPerCell) + oLeftMargin
        Dim _toCellLocation_y As Integer = ((pBackstitch.ToBlockPosition.Y + oPrintProject.OriginY - pPage.TopLeft.Y) * oPagePixelsPerCell) + oTopMargin
        Backstitch(pBackstitch, pDesignGraphics, oPagePixelsPerCell, _fromCellLocation_x, _fromCellLocation_y, _toCellLocation_x, _toCellLocation_y)
    End Sub
    Friend Sub PrintKnot(pKnot As Knot, pDesignGraphics As Graphics, pPage As Page)
        Dim _knotlocation_x As Integer = ((pKnot.BlockPosition.X + oPrintProject.OriginX - pPage.TopLeft.X) * oPagePixelsPerCell) - (oPagePixelsPerCell / 4) + oLeftMargin
        Dim _knotlocation_y As Integer = ((pKnot.BlockPosition.Y + oPrintProject.OriginY - pPage.TopLeft.Y) * oPagePixelsPerCell) - (oPagePixelsPerCell / 4) + oTopMargin
        Select Case pKnot.BlockQuarter
            Case BlockQuarter.BottomLeft
                _knotlocation_y += oPagePixelsPerCell / 2
            Case BlockQuarter.BottomRight
                _knotlocation_y += oPagePixelsPerCell / 2
                _knotlocation_x += oPagePixelsPerCell / 2
            Case BlockQuarter.TopRight
                _knotlocation_x += oPagePixelsPerCell / 2
        End Select
        Dim _rect As New Rectangle(_knotlocation_x, _knotlocation_y, oPagePixelsPerCell / 2, oPagePixelsPerCell / 2)
        Dim _brush As New SolidBrush(pKnot.ProjThread.Thread.Colour)
        Dim _pen As New Pen(Brushes.Black, 1)
        pDesignGraphics.FillEllipse(_brush, _rect)
        If pKnot.IsBead Then
            pDesignGraphics.DrawEllipse(_pen, _rect)
        End If
        _pen.Dispose()
    End Sub
    Public Sub InitialisePageLists()
        isPrintLoading = True
        TabControl1.TabPages.Clear()
        isPrintLoading = False
        oPageList = New List(Of Page)
        Dim _pagesAcross As Integer
        Dim _pagesDown As Integer = 0
        Dim _pagesTotal As Integer = 0
        Dim _pageTopLeft As New Point(0, 0)
        Dim _rowsLeft As Integer = oProjectDesign.Rows
        Dim _colsLeft As Integer
        Dim _overlap As Integer = NudOverlap.Value
        Dim _newPage As Page
        Dim _designPage As Page
        Dim _keyPage As Page
        Do Until _rowsLeft <= 0
            _colsLeft = oProjectDesign.Columns
            _pagesAcross = 0
            Dim _pageRows As Integer
            If _pagesDown = 0 Then
                _pageRows = Math.Min(oAvailableCellsHeight, _rowsLeft)
                _rowsLeft -= oAvailableCellsHeight
            Else
                _pageRows = Math.Min(oAvailableCellsHeight - _overlap, _rowsLeft)
                _pageRows += _overlap
                _rowsLeft -= oAvailableCellsHeight
                _rowsLeft += _overlap
            End If
            Do Until _colsLeft <= 0
                _newPage = CreateNewDesignPage(_pagesAcross, _pagesDown, _pagesTotal, _pageTopLeft, _rowsLeft, _colsLeft, _overlap, _pageRows)
                _designPage = _newPage.Clone
                Dim _pageColumns As Integer = _designPage.PageColumns
                oPageList.Add(_designPage)
                _pageTopLeft = New Point(_pageTopLeft.X + _pageColumns - _overlap, _pageTopLeft.Y)
                _pagesAcross += 1
            Loop
            _pageTopLeft = New Point(0, _pageTopLeft.Y + _pageRows - _overlap)
            _pagesDown += 1
        Loop
        Dim _keyPageNo As Integer = 0
        If ChkPrintKey.Checked Then
            _newPage = CreateNewKeyPage(_pagesTotal, _keyPageNo)
            _keyPage = _newPage.Clone
            oPageList.Add(_keyPage)
        End If
        LblPageCt.Text = CStr(_pagesTotal)
        isPagesLoaded = True
        TabControl1.SelectedIndex = 0
        DisplayPageImage()
    End Sub
    Private Function CreateNewKeyPage(ByRef _pagesTotal As Integer, ByRef _keyPageNo As Integer) As Page
        Dim oNewPage As New Page
        oNewPage.PageType = PageType.DesignPage
        _pagesTotal += 1
        _keyPageNo += 1
        oNewPage.PageNo = CStr(_pagesTotal)
        TabControl1.TabPages.Add("Key " & CStr(_keyPageNo))
        Return oNewPage
    End Function
    Private Function CreateNewDesignPage(_pagesAcross As Integer, _pagesDown As Integer, ByRef _pagesTotal As Integer, _pageTopLeft As Point, _rowsLeft As Integer, ByRef _colsLeft As Integer, _overlap As Integer, _pageRows As Integer) As Page
        Dim _designMiddleColumn As Integer = Math.Floor(oProjectDesign.Columns / 2)
        Dim _designMiddleRow As Integer = Math.Floor(oProjectDesign.Rows / 2)
        Dim oNewPage As New Page
        Dim _pageColumns As Integer
        oNewPage.PageType = PageType.KeyPage
        If _pagesAcross = 0 Then
            _pageColumns = Math.Min(oAvailableCellsWidth, _colsLeft)
            _colsLeft -= oAvailableCellsWidth
            oNewPage.Borders(Borders.Left) = True
        Else
            _pageColumns = Math.Min(oAvailableCellsWidth - _overlap, _colsLeft)
            _pageColumns += _overlap
            _colsLeft -= oAvailableCellsWidth
            _colsLeft += _overlap
        End If
        oNewPage.PageColumns = _pageColumns
        If _colsLeft <= 0 Then
            oNewPage.Borders(Borders.Right) = True
        End If
        oNewPage.PagePosition = New Point(_pagesAcross, _pagesDown)
        If _pagesDown = 0 Then
            oNewPage.Borders(Borders.Top) = True
        End If
        If _rowsLeft <= 0 Then
            oNewPage.Borders(Borders.Bottom) = True
        End If
        _pagesTotal += 1
        oNewPage.PageNo = CStr(_pagesTotal)
        TabControl1.TabPages.Add("Page " & CStr(_pagesTotal))
        oNewPage.TopLeft = _pageTopLeft
        oNewPage.BottomRight = New Point(_pageTopLeft.X + _pageColumns, _pageTopLeft.Y + _pageRows)
        If _designMiddleRow >= oNewPage.TopLeft.Y AndAlso _designMiddleRow <= oNewPage.BottomRight.Y Then
            oNewPage.MidRow = _designMiddleRow
        End If
        If _designMiddleColumn >= oNewPage.TopLeft.X AndAlso _designMiddleColumn <= oNewPage.BottomRight.X Then
            oNewPage.MidCol = _designMiddleColumn
        End If
        Return oNewPage
    End Function

    Private Sub PrintOverlapShade(pPage As Page, pPageGraphics As Graphics)
        Dim _widthInColumns As Integer = pPage.BottomRight.X - pPage.TopLeft.X
        Dim _heightInRows As Integer = pPage.BottomRight.Y - pPage.TopLeft.Y
        Dim tl As New Point(oLeftMargin, oTopMargin)
        If pPage.Borders(Borders.Top) = False Then
            pPageGraphics.FillRectangle(oOverlapBrush, New RectangleF(tl, New Size(_widthInColumns * gap, NudOverlap.Value * gap)))
        End If
        If pPage.Borders(Borders.Left) = False Then
            pPageGraphics.FillRectangle(oOverlapBrush, New RectangleF(tl, New Size(NudOverlap.Value * gap, _heightInRows * gap)))
        End If
    End Sub
    Private Sub ClearMargins(pPage As Page, pPageGraphics As Graphics)
        Dim oBottomGapY As Integer = oTopMargin + ((pPage.BottomRight.Y - pPage.TopLeft.Y) * oPagePixelsPerCell)
        Dim oRightGapX As Integer = oLeftMargin + ((pPage.BottomRight.X - pPage.TopLeft.X) * oPagePixelsPerCell)
        pPageGraphics.FillRectangle(New SolidBrush(Color.White), New Rectangle(New Point(0, 0), New Size(oPrintablePageWidth, oTopMargin - 2)))
        pPageGraphics.FillRectangle(New SolidBrush(Color.White), New Rectangle(New Point(0, oBottomGapY), New Size(oPrintablePageWidth, oPrintablePageHeight - oBottomGapY - 2)))
        pPageGraphics.FillRectangle(New SolidBrush(Color.White), New Rectangle(New Point(0, 0), New Size(oLeftMargin - 2, oPrintablePageHeight)))
        pPageGraphics.FillRectangle(New SolidBrush(Color.White), New Rectangle(New Point(oRightGapX, 0), New Size(oPrintablePageWidth - oRightGapX - 2, oPagesize.Height)))
    End Sub
    Private Sub PrintGrid(pPage As Page, ByRef pPageGraphics As Graphics, pSize As Size)
        Dim _widthInColumns As Integer = pPage.BottomRight.X - pPage.TopLeft.X
        Dim _heightInRows As Integer = pPage.BottomRight.Y - pPage.TopLeft.Y
        Dim _widthInPixels As Integer = Math.Min(gap * _widthInColumns, pSize.Width)
        Dim _heightInPixels As Integer = Math.Min(gap * _heightInRows, pSize.Height)
        Dim i5ColStart As Integer = 5
        Dim i10ColStart As Integer = 10
        If pPage.TopLeft.X > 0 Then
            Math.DivRem(pPage.TopLeft.X, 5, i5ColStart)
            i5ColStart = 5 - i5ColStart
            Math.DivRem(pPage.TopLeft.X, 10, i10ColStart)
            i10ColStart = 10 - i10ColStart
        End If
        Dim i5rowStart As Integer = 5
        Dim i10rowStart As Integer = 10
        If pPage.TopLeft.Y > 0 Then
            Math.DivRem(pPage.TopLeft.Y, 5, i5rowStart)
            i5rowStart = 5 - i5rowStart
            Math.DivRem(pPage.TopLeft.Y, 10, i10rowStart)
            i10rowStart = 10 - i10rowStart
        End If
        If ChkPrintGrid.Checked Then
            ' grid
            For _column = 0 To _widthInColumns
                DrawGridVerticalLine(pPageGraphics, oPrintGrid1Pen, _column, _heightInPixels)
            Next
            For _row = 0 To _heightInRows
                DrawGridHorizontalLine(pPageGraphics, oPrintGrid1Pen, _row, _widthInPixels)
            Next
            For _column = i5ColStart To _widthInColumns Step 10
                DrawGridVerticalLine(pPageGraphics, oPrintGrid5Pen, _column, _heightInPixels)
            Next
            For _row = i5rowStart To _heightInRows Step 10
                DrawGridHorizontalLine(pPageGraphics, oPrintGrid5Pen, _row, _widthInPixels)
            Next
            For _column = i10ColStart To _widthInColumns Step 10
                DrawGridVerticalLine(pPageGraphics, oPrintGrid10Pen, _column, _heightInPixels)
            Next
            For _row = i10rowStart To _heightInRows Step 10
                DrawGridHorizontalLine(pPageGraphics, oPrintGrid10Pen, _row, _widthInPixels)
            Next
        End If
        ' Centre lines and marks
        If ChkCentreMarks.Checked Or ChkCentreLines.Checked Then
            Dim _markHalfWidth As Integer = Math.Max(8, Math.Ceiling(oPagePixelsPerCell * 0.5))
            Dim _markHeight As Integer = Math.Max(12, Math.Ceiling(oPagePixelsPerCell * 0.75))
            Dim _middleRow As Integer = pPage.MidRow - pPage.TopLeft.Y
            Dim _middleColumn As Integer = pPage.MidCol - pPage.TopLeft.X
            Dim _middleColumnPos As Integer = (gap * _middleColumn) + oLeftMargin
            Dim _middleRowPos As Integer = (gap * _middleRow) + oTopMargin
            If pPage.MidRow > -1 Then
                If ChkCentreLines.Checked Then
                    DrawGridHorizontalLine(pPageGraphics, oPrintCentrePen, _middleRow, _widthInPixels)
                End If
                If ChkCentreMarks.Checked Then
                    Dim _leftMarkPoints As Point() = {New Point(gap - _markHeight + oLeftMargin, _middleRowPos - _markHalfWidth),
                                                      New Point(gap - _markHeight + oLeftMargin, _middleRowPos + _markHalfWidth),
                                                      New Point(gap + oLeftMargin, _middleRowPos)}

                    Dim _rightMarkPoints As Point() = {New Point(_widthInPixels - gap + _markHeight + oLeftMargin, (_middleRowPos - _markHalfWidth)),
                                                       New Point(_widthInPixels - gap + _markHeight + oLeftMargin, _middleRowPos + _markHalfWidth),
                                                       New Point(_widthInPixels - gap + oLeftMargin, _middleRowPos)}
                    oPrintGraphics.FillPolygon(oPrintCentreBrush, _leftMarkPoints)
                    oPrintGraphics.FillPolygon(oPrintCentreBrush, _rightMarkPoints)
                End If
            End If
            If pPage.MidCol > -1 Then
                If ChkCentreLines.Checked Then
                    DrawGridVerticalLine(pPageGraphics, oPrintCentrePen, _middleColumn, _heightInPixels)
                End If
                If ChkCentreMarks.Checked Then
                    Dim _topMarkPoints As Point() = {New Point(_middleColumnPos - _markHalfWidth, gap - _markHeight + oTopMargin),
                                                 New Point(_middleColumnPos + _markHalfWidth, gap - _markHeight + oTopMargin),
                                                 New Point(_middleColumnPos, gap + oTopMargin)}
                    Dim _bottomMarkPoints As Point() = {New Point(_middleColumnPos - _markHalfWidth, _heightInPixels - gap + _markHeight + oTopMargin),
                                                    New Point(_middleColumnPos + _markHalfWidth, _heightInPixels - gap + _markHeight + oTopMargin),
                                                    New Point(_middleColumnPos, _heightInPixels - gap + oTopMargin)}
                    oPrintGraphics.FillPolygon(oPrintCentreBrush, _topMarkPoints)
                    oPrintGraphics.FillPolygon(oPrintCentreBrush, _bottomMarkPoints)
                End If
            End If
        End If
        ' Borders
        If pPage.Borders(Borders.Top) = True Then
            DrawGridHorizontalLine(pPageGraphics, oPrintBorderPen, 0, _widthInPixels)
        End If
        If pPage.Borders(Borders.Bottom) = True Then
            DrawGridHorizontalLine(pPageGraphics, oPrintBorderPen, _heightInRows, _widthInPixels)
        End If
        If pPage.Borders(Borders.Left) = True Then
            DrawGridVerticalLine(pPageGraphics, oPrintBorderPen, 0, _heightInPixels)
        End If
        If pPage.Borders(Borders.Right) = True Then
            DrawGridVerticalLine(pPageGraphics, oPrintBorderPen, _widthInColumns, _heightInPixels)
        End If
    End Sub
    Private Sub DrawGridHorizontalLine(ByRef pPageGraphics As Graphics, pPen As Pen, pRow As Integer, pGridWidthInPixels As Integer)
        pPageGraphics.DrawLine(pPen, New Point(oLeftMargin, oTopMargin + (gap * pRow)), New Point(oLeftMargin + pGridWidthInPixels, oTopMargin + (gap * pRow)))
    End Sub
    Private Sub DrawGridVerticalLine(ByRef pPageGraphics As Graphics, pPen As Pen, pColumn As Integer, pGridHeightInpixels As Integer)
        pPageGraphics.DrawLine(pPen, New Point(oLeftMargin + (gap * pColumn), oTopMargin), New Point(oLeftMargin + (gap * pColumn), oTopMargin + pGridHeightInpixels))
    End Sub
    Private Sub LoadInstalledPrinters()
        For Each _installedPrinter As String In PrinterSettings.InstalledPrinters
            CmbInstalledPrinters.Items.Add(_installedPrinter)
            If (oPrintDoc.PrinterSettings.IsDefaultPrinter()) Then
                CmbInstalledPrinters.Text = oPrintDoc.PrinterSettings.PrinterName
            End If
        Next
    End Sub
    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        DisplayPageImage()
    End Sub
    Private Sub DisplayPageImage()
        If isPagesLoaded Then
            oSelectedPage = oPageList(TabControl1.SelectedIndex)
            If TabControl1.SelectedTab.Text.StartsWith("Page") Then
                CreatePrintBitmap()
            Else
                CreateKeyBitmap()
            End If
            PicDesign.Image = oPrintBitmap
            End If
    End Sub
    Private Sub CreatePrintBitmap()
        SetPrintFonts()
        oPrintBitmap = New Bitmap(oPrintablePageWidth, oPrintablePageHeight)
        oPrintBitmap.SetResolution(PRINT_DPI, PRINT_DPI)
        oPrintGraphics = Graphics.FromImage(oPrintBitmap)
        CreatePageGraphics(oSelectedPage, oPrintGraphics, oPrintBitmap.Size)
    End Sub
    Private Sub CreateKeyBitmap()
        SetPrintFonts()
        oPrintBitmap = New Bitmap(oPrintablePageWidth, oPrintablePageHeight)
        oPrintBitmap.SetResolution(PRINT_DPI, PRINT_DPI)
        oPrintGraphics = Graphics.FromImage(oPrintBitmap)
        Dim oTableAreaSize As New Size(oPrintBitmap.Width - oLeftMargin - oRightMargin, oPrintBitmap.Height - oTopMargin - oBottomMargin)
        Dim oTableAreaTopLeft As New Point(oLeftMargin, oTopMargin)
        CreateKeyGraphics(oPrintGraphics, oPrintBitmap.Size, oTableAreaTopLeft, oTableAreaSize)
    End Sub
#End Region
End Class