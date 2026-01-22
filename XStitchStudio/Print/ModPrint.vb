' Hindleware
' Copyright (c) 2025-6 Eric Hindle
' All rights reserved.
'
' Author Eric Hindle
'

Imports System.Drawing.Printing
Imports XStitchStudio.Domain.Objects

Module ModPrint
#Region "enum"
    Friend Enum Borders
        Top
        Right
        Bottom
        Left
    End Enum
    Friend Enum GroupType
        CrossSingle
        CrossDouble
        BackSingle
        BackDouble
        KnotSingle
        KnotDouble
        Bead
    End Enum
    Friend Enum PageType
        DesignPage
        KeyPage
    End Enum
#End Region
#Region "constants"
    Public Const PRINT_DPI As Integer = 300.0F
    Public Const FONT_PPI As Integer = 72.0F
    Public Const SCREEN_DPI As Integer = 93.0F
    Public Const A4_WIDTH As Integer = 2480
    Public Const A4_HEIGHT As Integer = 3508
    Public Const PRODUCT_TEXT As String = "created using CrossStitch Studio [@ XStitch-Studio.co.uk]"
    Public Const CROSS_DOUBLE_STRAND_HEADER As String = "CrossStitch in two strands"
    Public Const CROSS_SINGLE_STRAND_HEADER As String = "CrossStitch in one strand"
    Public Const BACK_DOUBLE_STRAND_HEADER As String = "BackStitch in two strands"
    Public Const BACK_SINGLE_STRAND_HEADER As String = "BackStitch in one strand"
    Public Const KNOT_SINGLE_STRAND_HEADER As String = "French Knot in one strand"
    Public Const KNOT_DOUBLE_STRAND_HEADER As String = "French Knot in two strands"
    Public Const BEAD_HEADER As String = "Beads"
#End Region
#Region "variables"
    Friend oPagesize As Size
    Friend isLandscape As Boolean
    Friend oWidthHeightDifference As Integer
    Friend oPrinterHardMarginX As Integer
    Friend oPrinterHardMarginY As Integer
    Friend oPrintablePageWidth As Integer
    Friend oPrintablePageHeight As Integer
    Friend oPrintDoc As New Printing.PrintDocument
    Friend oPageSettings As PageSettings
    Friend oLeftMargin As Integer                   ' Page adjusted left margin
    Friend oRightMargin As Integer                  ' Page adjusted right margin
    Friend oTopMargin As Integer                    ' Page adjusted top margin
    Friend oBottomMargin As Integer                 ' Page adjusted bottom margin
    Friend oPageTitleHeight As Integer
    Friend oPageTextHeight As Integer
    Friend oPageFooterHeight As Integer
    Friend oPageLeftMargin As Integer               ' Page minimum left margin
    Friend oPageRightMargin As Integer              ' Page minimum right margin
    Friend oPageTopMargin As Integer                ' Page minimum top margin
    Friend oPageBottomMargin As Integer             ' Page minimum bottom margin
    Friend oAvailableGridWidth As Integer
    Friend oAvailableGridHeight As Integer
    Friend oAvailablePixelWidth As Integer
    Friend oAvailablePixelHeight As Integer
    Friend oAvailableCellsWidth As Integer
    Friend oAvailableCellsHeight As Integer
    Friend oOnePageSqPerInch As Integer
    Friend oTitleHeight As Integer
    Friend oFooterHeight As Integer
    Friend oPrintGridOrigin As Point
    Friend oPageToFormRatio As Decimal
    Friend oTitlefont As Font
    Friend oTextfont As Font
    Friend oFooterfont As Font
    Friend oPrintTitlefont As Font
    Friend oPrintGroupfont As Font
    Friend oPrintTextfont As Font
    Friend oPrintFooterfont As Font
    Friend oPagePixelsPerCell As Integer
    Friend isPrintHeader As Boolean
    Friend isPrintFooter As Boolean
    Friend isPrintRowNumbers As Boolean
    Friend isPrintColumnNumbers As Boolean
    Friend oCrossDoubleThreadList As New ProjectThreadCollection
    Friend oCrossSingleThreadList As New ProjectThreadCollection
    Friend oBackDoubleThreadList As New ProjectThreadCollection
    Friend oBackSingleThreadList As New ProjectThreadCollection
    Friend oKnotDoubleThreadList As New ProjectThreadCollection
    Friend oKnotSingleThreadList As New ProjectThreadCollection
    Friend oBeadList As New ProjectThreadCollection
    Friend oRowHeight As Integer
    Friend oTableWidth As Integer
    Friend oTableHeight As Integer
    Friend oColumn1Width As Integer
    Friend oColumn2Width As Integer
    Friend oColumn3Width As Integer
    Friend oSymbolWidth As Integer
    Friend oBoxWidth As Integer
#End Region
#Region "subroutines"
    Friend Sub SetPrintPageMargins(pLeftMargin As Single, pRightMargin As Single, pTopMargin As Single, pBottomMargin As Single)
        ' Set print margins in dots
        oPageTitleHeight = If(isPrintHeader, (oPrintTitlefont.Size * PRINT_DPI) / FONT_PPI, 0) + 20
        oPageTextHeight = (oPrintTextfont.Size * PRINT_DPI) / FONT_PPI
        oPageFooterHeight = ((oPrintFooterfont.Size * PRINT_DPI) / FONT_PPI) + 20
        oPageLeftMargin = Math.Max((pLeftMargin * PRINT_DPI) - oPrinterHardMarginX, 0)
        oPageRightMargin = Math.Max((pRightMargin * PRINT_DPI) - oPrinterHardMarginX, 0)
        oPageTopMargin = Math.Max((pTopMargin * PRINT_DPI) - oPrinterHardMarginY, 0)
        oPageBottomMargin = Math.Max((pBottomMargin * PRINT_DPI) - oPrinterHardMarginY, 0)
        oLeftMargin = oPageLeftMargin
        oRightMargin = oPageRightMargin
        If isPrintRowNumbers Then
            oRightMargin += oPageFooterHeight
        End If
        oTopMargin = oPageTopMargin + oPageTitleHeight
        oBottomMargin = oPageBottomMargin + oPageFooterHeight
        If isPrintColumnNumbers Then
            oBottomMargin += oPageFooterHeight
        End If
        oTitleHeight = oPageTitleHeight
        oFooterHeight = oPageFooterHeight
    End Sub
    Friend Sub CalculatePrintGridSpace(pDesignSize As Size)
        oAvailablePixelWidth = oPrintablePageWidth - oLeftMargin - oRightMargin - oPagePixelsPerCell
        oAvailablePixelHeight = oPrintablePageHeight - oTopMargin - oBottomMargin - (oPagePixelsPerCell * 2)
        oAvailableCellsWidth = oAvailablePixelWidth / oPagePixelsPerCell
        oAvailableCellsHeight = oAvailablePixelHeight / oPagePixelsPerCell
        oPrintGridOrigin = New Point(oPageLeftMargin, oPageTopMargin + oPageTitleHeight)
        Dim dotspercell_w As Integer = Math.Floor(oAvailablePixelWidth / pDesignSize.Width)
        Dim dotspercell_h As Integer = Math.Floor(oAvailablePixelHeight / pDesignSize.Height)
        oOnePageSqPerInch = Math.Ceiling(PRINT_DPI / Math.Min(dotspercell_w, dotspercell_h))
    End Sub
    Friend Sub InitialisePrintDocument()
        Dim _paperKind As PaperKind = PaperKind.A4
        oPrintDoc = New PrintDocument
        ' Set default paper size
        For Each ps As Printing.PaperSize In oPrintDoc.PrinterSettings.PaperSizes
            If ps.RawKind = _paperKind Then
                oPrintDoc.DefaultPageSettings.PaperSize = ps
                Exit For
            End If
        Next
        oPageSettings = oPrintDoc.DefaultPageSettings
        ' Set default page settings
        oPrintDoc.DefaultPageSettings.Landscape = isLandscape
        oPrintDoc.DefaultPageSettings.Margins.Left = 0
        oPrintDoc.DefaultPageSettings.Margins.Right = 0
        oPrintDoc.DefaultPageSettings.Margins.Top = 0
        oPrintDoc.DefaultPageSettings.Margins.Bottom = 0
        If isLandscape Then
            oPrinterHardMarginY = oPageSettings.HardMarginX / 100 * PRINT_DPI
            oPrinterHardMarginX = oPageSettings.HardMarginY / 100 * PRINT_DPI
        Else
            oPrinterHardMarginX = oPageSettings.HardMarginX / 100 * PRINT_DPI
            oPrinterHardMarginY = oPageSettings.HardMarginY / 100 * PRINT_DPI
        End If
        oPrintablePageWidth = oPagesize.Width - (oPrinterHardMarginX * 2)
        oPrintablePageHeight = oPagesize.Height - (oPrinterHardMarginY * 2)
    End Sub
    Friend Sub SetThreadGroups()
        oCrossDoubleThreadList = New ProjectThreadCollection
        oCrossSingleThreadList = New ProjectThreadCollection
        oBackDoubleThreadList = New ProjectThreadCollection
        oBackSingleThreadList = New ProjectThreadCollection
        oKnotDoubleThreadList = New ProjectThreadCollection
        oKnotSingleThreadList = New ProjectThreadCollection
        oBeadList = New ProjectThreadCollection
        For Each _blockstitch As BlockStitch In oProjectDesign.BlockStitches
            If _blockstitch.Strands = 2 AndAlso Not oCrossDoubleThreadList.Exists(_blockstitch.ThreadId) Then
                oCrossDoubleThreadList.Add(_blockstitch.ProjThread)
            End If
            If _blockstitch.Strands = 1 AndAlso Not oCrossSingleThreadList.Exists(_blockstitch.ThreadId) Then
                oCrossSingleThreadList.Add(_blockstitch.ProjThread)
            End If
        Next
        For Each _backstitch As BackStitch In oProjectDesign.BackStitches
            If _backstitch.Strands = 2 AndAlso Not oBackDoubleThreadList.Exists(_backstitch.ThreadId) Then
                oBackDoubleThreadList.Add(_backstitch.ProjThread)
            End If
            If _backstitch.Strands = 1 AndAlso Not oBackSingleThreadList.Exists(_backstitch.ThreadId) Then
                oBackSingleThreadList.Add(_backstitch.ProjThread)
            End If
        Next
        For Each _knot As Knot In oProjectDesign.Knots
            If _knot.IsBead AndAlso Not oBeadList.Exists(_knot.ThreadId) Then
                oBeadList.Add(_knot.ProjThread)
            End If
            If Not _knot.IsBead Then
                If _knot.Strands = 2 AndAlso Not oKnotDoubleThreadList.Exists(_knot.ThreadId) Then
                    oKnotDoubleThreadList.Add(_knot.ProjThread)
                End If
                If _knot.Strands = 1 AndAlso Not oKnotSingleThreadList.Exists(_knot.ThreadId) Then
                    oKnotSingleThreadList.Add(_knot.ProjThread)
                End If
            End If
        Next

    End Sub
    Friend Sub InitialiseTable(pColumnsPerPage As Integer)
        oTableWidth = oAvailablePixelWidth / pColumnsPerPage
        oRowHeight = oPageTextHeight + 40
        oColumn1Width = oTableWidth / 8
        oColumn2Width = oTableWidth * 4 / 8
        oColumn3Width = oTableWidth * 1 / 8
        oSymbolWidth = oRowHeight * 0.75
        oBoxWidth = oSymbolWidth + 4
    End Sub
#End Region
End Module
