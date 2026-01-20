' Hindleware
' Copyright (c) 2025-6 Eric Hindle
' All rights reserved.
'
' Author Eric Hindle
'

Imports System.Text
Namespace Domain.Objects
    Public Class PaletteThread
#Region "properties"
        Private _thread As Thread
        Private _doubleThreadSymbolId As Integer
        Private _PaletteId As Integer
        Private _threadId As Integer
        Private _symbol As Image
        Private _isBead As Boolean
        Private _stitchCount As Integer
        Private _singleThreadSymbolId As Integer
        Public Property SingleThreadSymbolId() As Integer
            Get
                Return _singleThreadSymbolId
            End Get
            Set(ByVal value As Integer)
                _singleThreadSymbolId = value
            End Set
        End Property
        Public Property StitchCount() As Integer
            Get
                Return _stitchCount
            End Get
            Set(ByVal value As Integer)
                _stitchCount = value
            End Set
        End Property
        Public Property IsBead() As Boolean
            Get
                Return _isBead
            End Get
            Set(ByVal value As Boolean)
                _isBead = value
            End Set
        End Property
        Public Property ThreadId() As Integer
            Get
                Return _threadId
            End Get
            Set(ByVal value As Integer)
                _threadId = value
            End Set
        End Property
        Public Property PaletteId() As Integer
            Get
                Return _PaletteId
            End Get
            Set(ByVal value As Integer)
                _PaletteId = value
            End Set
        End Property
        Public Property DoubleThreadSymbolId() As Integer
            Get
                Return _doubleThreadSymbolId
            End Get
            Set(ByVal value As Integer)
                _doubleThreadSymbolId = value
            End Set
        End Property
        Public ReadOnly Property Thread() As Thread
            Get
                If _thread Is Nothing Then
                    If _isBead Then
                        _thread = FindBeadById(_threadId)
                    Else
                        _thread = FindThreadById(_threadId)
                    End If
                End If
                Return _thread
            End Get
        End Property
        Public ReadOnly Property Symbol() As Image
            Get
                If _symbol Is Nothing Then
                    _symbol = FindSymbolImage(_doubleThreadSymbolId)
                End If
                Return _symbol
            End Get
        End Property

#End Region
#Region "constructors"
        Private Sub InitialisePaletteThread()
            _thread = Nothing
            _doubleThreadSymbolId = -1
            _singleThreadSymbolId = -1
            _PaletteId = -1
            _threadId = -1
            _symbol = Nothing
            _isBead = False
            _stitchCount = 0
        End Sub
        Public Sub New()
            InitialisePaletteThread()
        End Sub
        Public Sub New(pPaletteId As Integer,
                       pThreadId As Integer,
                       pDoubleSymbolId As Integer,
                       pSingleSymbolId As Integer,
                       pIsBead As Boolean)
            InitialisePaletteThread()
            _PaletteId = pPaletteId
            _threadId = pThreadId
            _doubleThreadSymbolId = pDoubleSymbolId
            _singleThreadSymbolId = pSingleSymbolId
            _isBead = pIsBead
        End Sub
#End Region
#Region "methods"
        Public Function IsLoaded() As Boolean
            Return _PaletteId > -1 And _threadId > -1
        End Function
        Public Function AddStitch() As Integer
            _stitchCount += 1
            Return _stitchCount
        End Function
        Public Function Key() As String
            Return CStr(_PaletteId) & ":" & CStr(_threadId)
        End Function
        Public Overrides Function ToString() As String
            Dim _sb As New StringBuilder
            _sb.Append("PaletteThread=[") _
                .Append("PaletteId=[").Append(CStr(_PaletteId)).Append("], ") _
                .Append(Thread.ToString).Append(", ") _
                .Append("DoubleThreadSymbolId=[").Append(CStr(_doubleThreadSymbolId)).Append("],") _
                .Append("SingleThreadSymbolId=[").Append(CStr(_singleThreadSymbolId)).Append("],") _
                .Append(If(_isBead, "Bead", "Thread")) _
                .Append("]")
            Return _sb.ToString()
        End Function
#End Region

    End Class
End Namespace