' Hindleware
' Copyright (c) 2025-6 Eric Hindle
' All rights reserved.
'
' Author Eric Hindle
'

Imports System.Text
Namespace Domain.Objects
    Public Class ProjectThread
#Region "properties"
        Friend _thread As Thread
        Friend _doubleThreadSymbolId As Integer
        Friend _projectId As Integer
        Friend _threadId As Integer
        Friend _symbol As Image
        Friend _isUsed As Boolean
        Private _singleThreadSymbolId As Integer
        Public Property SingleThreadSymbolId() As Integer
            Get
                Return _singleThreadSymbolId
            End Get
            Set(ByVal value As Integer)
                _singleThreadSymbolId = value
            End Set
        End Property
        Public Property IsUsed() As Boolean
            Get
                Return _isUsed
            End Get
            Set(ByVal value As Boolean)
                _isUsed = value
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
        Public Property ProjectId() As Integer
            Get
                Return _projectId
            End Get
            Set(ByVal value As Integer)
                _projectId = value
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
        Public Overridable ReadOnly Property Thread() As Thread
            Get
                If _thread Is Nothing Then
                    _thread = FindThreadById(_threadId)
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
        Private Sub InitialiseProjectThread()
            _thread = Nothing
            _doubleThreadSymbolId = -1
            _singleThreadSymbolId = -1
            _projectId = -1
            _threadId = -1
            _symbol = Nothing
            _isUsed = False
        End Sub
        Public Sub New()
            InitialiseProjectThread()
        End Sub
        Public Sub New(pProjectId As Integer,
                       pThreadId As Integer,
                       pDoubleSymbolId As Integer,
                       pSingleSymbolId As Integer,
                       pIsUsed As Boolean)
            InitialiseProjectThread()
            _projectId = pProjectId
            _threadId = pThreadId
            _doubleThreadSymbolId = pDoubleSymbolId
            _singleThreadSymbolId = pSingleSymbolId
            _isUsed = pIsUsed
        End Sub
#End Region
#Region "methods"
        Public Function FindSingleThreadSymbolImage() As Image
            Return FindSymbolImage(_singleThreadSymbolId)
        End Function
        Public Function FindDoubleThreadSymbolImage() As Image
            Return FindSymbolImage(_doubleThreadSymbolId)
        End Function
        Public Function IsLoaded() As Boolean
            Return _projectId > -1 And _threadId > -1
        End Function
        Public Function Key() As String
            Return CStr(_projectId) & ":" & CStr(_threadId)
        End Function
        Public Overrides Function ToString() As String
            Dim _sb As New StringBuilder
            _sb.Append("ProjectThread=[") _
                .Append("ProjectId=[").Append(CStr(_projectId)).Append("], ") _
                .Append(Thread.ToString).Append(", ") _
                .Append("SymbolId=[").Append(CStr(_doubleThreadSymbolId)).Append("], ") _
                .Append("SingleThreadSymbolId=[").Append(CStr(_singleThreadSymbolId)).Append("], ") _
                .Append("Is used=[").Append(_isUsed).Append("]") _
                .Append("]")
            Return _sb.ToString()
        End Function
        Public Function ToSaveString() As String
            Dim _sb As New StringBuilder
            _sb _
            .Append(CStr(_projectId)).Append(DESIGN_DELIM) _
            .Append(CStr(_threadId)).Append(DESIGN_DELIM) _
            .Append(CStr(_doubleThreadSymbolId)).Append(DESIGN_DELIM) _
            .Append(CStr(_singleThreadSymbolId)).Append(DESIGN_DELIM) _
            .Append(CStr(_isUsed)).Append(DESIGN_DELIM) _
            .Append(Thread.ToSaveString)
            Return _sb.ToString()
        End Function
#End Region

    End Class
End Namespace