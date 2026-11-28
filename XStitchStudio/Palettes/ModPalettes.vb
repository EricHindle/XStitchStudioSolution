' Hindleware
' Copyright (c) 2025-26 eric hindle
' All rights reserved.
'
' Author Eric Hindle
'

Imports HindlewareLib.Logging
Imports XStitchStudio.Domain.Builders
Imports XStitchStudio.Domain.Objects
Imports XStitchStudio.Domain
Imports System.Reflection
Module ModPalettes
    Friend Function GetPaletteName(pProjectName) As String
        Dim _result As DialogResult
        Dim _newName As String = pProjectName
        Using _paletteName As New FrmPaletteName
            _paletteName.PaletteName = _newName
            _result = _paletteName.ShowDialog()
            If _result = DialogResult.OK Then
                _newName = _paletteName.PaletteName
            Else
                _newName = String.Empty
            End If
        End Using
        Return _newName
    End Function
    Friend Function FindThreadInPaletteList(pDgv As DataGridView, pColName As String, pValue As Integer) As Integer
        Dim _index As Integer = -1
        For Each orow As DataGridViewRow In pDgv.Rows
            If orow.Cells(pColName).Value = pValue Then
                _index = orow.Index
                Exit For
            End If
        Next
        Return _index
    End Function
    Public Sub CreatePalette(pProjectName As String, pPaletteThreadList As List(Of Thread), pPaletteBeadList As List(Of Bead), pStatus As ToolStripStatusLabel)
        If pPaletteThreadList.Count > 0 OrElse pPaletteBeadList.Count > 0 Then
            Dim _name As String = GetPaletteName(pProjectName)
            If Not String.IsNullOrEmpty(_name) Then
                LogUtil.ShowStatus("Adding new Palette", pStatus)
                AddPalette(_name, pPaletteThreadList, pPaletteBeadList)
                ShowMessage("Palette " & _name & " stored", 1, MethodBase.GetCurrentMethod.Name)
            Else
                LogUtil.ShowStatus("Palette name missing. Palette not added.", pStatus, True)
            End If
        Else
            LogUtil.ShowStatus("No threads selected. Palette not added.", pStatus, True)
        End If

    End Sub
    Public Sub AddPalette(pName As String, pPaletteThreadList As List(Of Thread), pPaletteBeadList As List(Of Bead))
        Dim _paletteId As Integer = AddNewPalette(pName.Trim)
        For Each oThread In pPaletteThreadList
            Dim _paletteThread As PaletteThread = PaletteThreadBuilder.APaletteThread.StartingWithNothing _
                                    .WithThreadId(oThread.ThreadId) _
                                    .WithPaletteId(_paletteId) _
                                    .WithIsBead(False) _
                                    .Build
            AddNewPaletteThread(_paletteThread)
        Next
        For Each obead As Bead In pPaletteBeadList
            Dim _paletteThread As PaletteThread = PaletteThreadBuilder.APaletteThread.StartingWithNothing _
                                    .WithThreadId(obead.ThreadId) _
                                    .WithPaletteId(_paletteId) _
                                    .WithIsBead(True) _
                                    .Build
            AddNewPaletteThread(_paletteThread)
        Next
    End Sub
    Public Function SelectPaletteInList(ByRef pDgv As DataGridView, pColName As String, pPaletteNo As Integer) As Integer
        Dim _index As Integer = 0
        For Each orow As DataGridViewRow In pDgv.Rows
            If orow.Cells(pColName).Value = pPaletteNo Then
                orow.Selected = True
                _index = orow.Index
                pDgv.FirstDisplayedScrollingRowIndex = _index
                Exit For
            End If
        Next
        Return _index
    End Function
End Module
