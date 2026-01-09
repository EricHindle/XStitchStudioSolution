' Hindleware
' Copyright (c) 2025-6 Eric Hindle
' All rights reserved.
'
' Author Eric Hindle
'
Imports HindlewareLib.Logging
Imports XStitchStudio.Domain
Imports XStitchStudio.Domain.Objects
Public Class FrmProjectInfo
#Region "properties"
    Private _selectedProject As Project
    Public Property SelectedProject() As Project
        Get
            Return _selectedProject
        End Get
        Set(ByVal value As Project)
            _selectedProject = value
        End Set
    End Property
#End Region
#Region "variables"
    Private isLoading As Boolean
    Private FABRIC_COUNT As Integer
    Private isComponentInitialized As Boolean = False
#End Region
#Region "control handlers"
    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Close()
    End Sub
    Private Sub FrmDesignInfo_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        LogUtil.LogInfo("Closing", MyBase.Name)
        SaveFormLayout()
    End Sub
    Private Sub FrmDesignInfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LogUtil.LogInfo("Project information", MyBase.Name)
        RestoreFormLayout()
        isLoading = True
        InitialiseForm()
        isLoading = False
    End Sub
#End Region
#Region "subroutines"
    Private Sub InitialiseForm()
        isComponentInitialized = True
        FABRIC_COUNT = My.Settings.DefaultFabricCount
        If _selectedProject.IsLoaded Then
            LoadProjectDetails()
        End If
    End Sub
    Private Sub SaveFormLayout()
        My.Settings.ProjectInfoFormPos = SetFormPos(Me)
        'If SplitContainer1.SplitterDistance > 0 Then My.Settings.SplitDistInfo1 = SplitContainer1.SplitterDistance
        'If SplitContainer2.SplitterDistance > 0 Then My.Settings.SplitDistInfo2 = SplitContainer2.SplitterDistance
        'If SplitContainer3.SplitterDistance > 0 Then My.Settings.SplitDistInfo3 = SplitContainer3.SplitterDistance
        'If SplitContainer4.SplitterDistance > 0 Then My.Settings.SplitDistInfo4 = SplitContainer4.SplitterDistance
        My.Settings.Save()
    End Sub
    Private Sub RestoreFormLayout()
        GetFormPos(Me, My.Settings.ProjectInfoFormPos)
        'SplitContainer1.SplitterDistance = My.Settings.SplitDistInfo1
        'SplitContainer2.SplitterDistance = My.Settings.SplitDistInfo2
        'SplitContainer3.SplitterDistance = My.Settings.SplitDistInfo3
        'SplitContainer4.SplitterDistance = My.Settings.SplitDistInfo4
    End Sub
    Private Sub LoadProjectDetails()
        With _selectedProject
            FABRIC_COUNT = .FabricCount
            LblName.Text = .ProjectName
            LblDesignHeight.Text = .DesignHeight
            LblDesignWidth.Text = .DesignWidth
            LblCentreX.Text = .OriginX
            LblCentreY.Text = .OriginY
            LblFabricHeight.Text = .FabricHeight
            LblFabricWidth.Text = .FabricWidth
            LblFabricCount.Text = .FabricCount
            Select Case .FabricColour
                Case 1 To 4
                    LblFabricColour.Text = oFabricColourList(.FabricColour - 1).Name
                Case Else
                    LblFabricColour.Text = .FabricColour.ToString()
            End Select
            UpdateProjectTime()
        End With
    End Sub
    Private Sub UpdateProjectTime()
        Dim _workList As List(Of ProjectWorkTime) = FindWorkPeriodsForProject(_selectedProject.ProjectId)
        For Each _period As ProjectWorkTime In _workList
            AddWorkPeriodRow(_period)
        Next
        Dim _startDate As DateTime = Date.MinValue
        Dim _endDate As DateTime = Date.MinValue
        Dim _timeElapsed As TimeSpan = TimeSpan.FromMinutes(_selectedProject.TotalMinutes)
        If _workList.Count > 0 Then
            _startDate = _workList.First.TimeStarted
            _endDate = _workList.Last.TimeEnded
        End If
        LblStarted.Text = If(_startDate > Date.MinValue, Format(_startDate, "dd MMM yyyy"), String.Empty)
        LblFinished.Text = If(_startDate > Date.MinValue, Format(_endDate, "dd MMM yyyy"), String.Empty)
        LblTotalTime.Text = _timeElapsed.Hours.ToString("D2") & ":" & _timeElapsed.Minutes.ToString("D2")
    End Sub
    Private Sub AddWorkPeriodRow(pPeriod As ProjectWorkTime)
        Dim oRow As DataGridViewRow = DgvWorkPeriods.Rows(DgvWorkPeriods.Rows.Add())
        oRow.Cells(timeDate.Name).Value = Format(pPeriod.TimeStarted, "dd MMM yyyy")
        oRow.Cells(timeStarted.Name).Value = Format(pPeriod.TimeStarted, "HH:mm:ss")
        oRow.Cells(timeEnded.Name).Value = Format(pPeriod.TimeEnded, "HH:mm:ss")
        oRow.Cells(timeMinutes.Name).Value = pPeriod.Minutes
    End Sub
#End Region
End Class