' Hindleware
' Copyright (c) 2025-6 Eric Hindle
' All rights reserved.
'
' Author Eric Hindle
'

Imports XStitchStudio.Domain.Objects
Imports XStitchStudio.MyStitchDataSet
Namespace Domain.Builders
    Public Class PaletteThreadBuilder
        Private _doubleThreadSymbolId As Integer
        Private _singleThreadSymbolId As Integer
        Private _PaletteId As Integer
        Private _threadId As Integer
        Private _isBead As Boolean
        Public Shared Function APaletteThread() As PaletteThreadBuilder
            Return New PaletteThreadBuilder
        End Function
        Public Function StartingWithNothing() As PaletteThreadBuilder
            _doubleThreadSymbolId = -1
            _singleThreadSymbolId = -1
            _threadId = -1
            _PaletteId = -1
            _isBead = False
            Return Me
        End Function
        Public Function StartingWith(ByRef pThread As PaletteThread) As PaletteThreadBuilder
            StartingWithNothing()
            If pThread IsNot Nothing Then
                With pThread
                    _PaletteId = .PaletteId
                    _threadId = .ThreadId
                    _doubleThreadSymbolId = .DoubleThreadSymbolId
                    _singleThreadSymbolId = .SingleThreadSymbolId
                    _isBead = .IsBead
                End With
            End If
            Return Me
        End Function
        Public Function StartingWith(ByRef oRow As PaletteThreadsRow) As PaletteThreadBuilder
            StartingWithNothing()
            If oRow IsNot Nothing Then
                With oRow
                    '_Palette = Nothing
                    '_thread = Nothing
                    _PaletteId = .palette_id
                    _threadId = .thread_id
                    _doubleThreadSymbolId = .symbol_id
                    _singleThreadSymbolId = .single_thread_symbol_id
                    _isBead = (.isbead = 1)
                End With
            End If
            Return Me
        End Function
        Public Function WithThreadId(pId As Integer) As PaletteThreadBuilder
            _threadId = pId
            Return Me
        End Function
        Public Function WithPaletteId(pId As Integer) As PaletteThreadBuilder
            _PaletteId = pId
            Return Me
        End Function
        Public Function WithDoubleThreadSymbolId(pSymbolId As Integer) As PaletteThreadBuilder
            _doubleThreadSymbolId = pSymbolId
            Return Me
        End Function
        Public Function WithSingleThreadSymbolId(pSymbolId As Integer) As PaletteThreadBuilder
            _singleThreadSymbolId = pSymbolId
            Return Me
        End Function
        Public Function WithIsBead(pIsBead As Boolean) As PaletteThreadBuilder
            _isBead = pIsBead
            Return Me
        End Function
        Public Function Build() As PaletteThread
            Return New PaletteThread(_PaletteId, _threadId, _doubleThreadSymbolId, _singleThreadSymbolId, _isBead)
        End Function
    End Class
End Namespace