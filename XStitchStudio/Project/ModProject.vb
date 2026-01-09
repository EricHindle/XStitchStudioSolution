' Hindleware
' Copyright (c) 2025-6 Eric Hindle
' All rights reserved.
'
' Author Eric Hindle
'

Imports System.Reflection
Imports HindlewareLib.Logging
Imports XStitchStudio.Domain
Imports XStitchStudio.Domain.Builders
Imports XStitchStudio.Domain.Objects
Module ModProject
    Public Const PROJECT_ID_FLD As Integer = 1
    Public Const PROJECT_NAME_FLD As Integer = 2
    Public Const DATE_STARTED_FLD As Integer = 3
    Public Const DATE_ENDED_FLD As Integer = 4
    Public Const DESIGN_WIDTH_FLD As Integer = 5
    Public Const DESIGN_HEIGHT_FLD As Integer = 6
    Public Const FABRIC_WIDTH_FLD As Integer = 7
    Public Const FABRIC_HEIGHT_FLD As Integer = 8
    Public Const FABRIC_COLOUR_FLD As Integer = 9
    Public Const FABRIC_COUNT_FLD As Integer = 10
    Public Const DESIGN_FILE_NAME_FLD As Integer = 11
    Public Const ORIGIN_X_FLD As Integer = 12
    Public Const ORIGIN_Y_FLD As Integer = 13
    Public Const TOTAL_MINUTES_FLD As Integer = 14
    Public Const PROJECT_ADDED As String = "Project Added"
    Public Const PROJECT_REMOVED As String = "Project Removed"
    Public Const PROJECT_UPDATED As String = "Project Updated"
    Public MIN_DATE As New DateTime(2001, 1, 1)
    Public oTimerForm As FrmProjectTimer
    Public isSaved As Boolean = True
    Public isProjectLoading As Boolean
    Public oFileProject As Project
    Public oFileProjectDesign As ProjectDesign
    Public oFileProjectThreadCollection As ProjectThreadCollection
    Public oFileProjectBeadCollection As ProjectBeadCollection
    Public Function SaveDesign() As ActionResponse
        Dim _reply As New ActionResponse
        If oProject.IsLoaded Then
            ShowMessage("Saving Design...", MethodBase.GetCurrentMethod.Name)
            If String.IsNullOrEmpty(oProject.DesignFileName) Then
                oProject.DesignFileName = MakeFilename(oProject)
                AmendProject(oProject)
            End If
            _reply = SaveDesignToFile(oProject.DesignFileName)
            RemoveMessage()
            ShowMessage(_reply.ResponseText, 1.5, MethodBase.GetCurrentMethod.Name)
        End If
        Return _reply
    End Function
    Public Function SaveDesignToFile(pFilename As String) As ActionResponse
        Dim _reply As New ActionResponse
        LogUtil.Info("Saving design", MethodBase.GetCurrentMethod.Name)
        If My.Settings.isAutoArchiveOnSave Then
            Try
                ArchiveExistingFile(pFilename, oDesignFolderName, DESIGN_ZIP_EXT, oDesignArchiveFolderName, DESIGN_ARC_EXT, AddDateTime.AddDate)
            Catch ex As Exception
                LogUtil.Exception("Problem archiving existing file", ex, MethodBase.GetCurrentMethod.Name)
            End Try
        End If
        _reply = SaveDesignDelimited(oProject, oProjectDesign, oProjectThreads, oProjectBeads, pFilename)
        If _reply.ResponseType <> ResponseType.Err Then
            isSaved = True
            LogUtil.LogInfo("Design saved to " & pFilename, MethodBase.GetCurrentMethod.Name)
        Else
            LogUtil.LogInfo(_reply.ResponseText, MethodBase.GetCurrentMethod.Name)
        End If
        Return _reply
    End Function
    Public Function SaveMotifToFile(pMotif As Motif, pFilename As String) As ActionResponse
        Dim _reply As ActionResponse = SaveMotifDelimited(pMotif, pFilename)
        If _reply.ResponseType = ResponseType.None Then
            LogUtil.LogInfo("Motif saved to " & pFilename, MethodBase.GetCurrentMethod.Name)
            _reply = New ActionResponse("Motif save complete", ResponseType.Info)
        End If
        Return _reply
    End Function
    Public Sub LoadProject(pDgv As DataGridView)
        LoadProjectTable(pDgv)
        UpdateThreadsFromDesign(oFileProjectDesign, oFileProjectThreadCollection)
        InsertProjectThreadCollection(oFileProjectThreadCollection)
        SelectProjectInList(pDgv, oFileProject.ProjectId)
        oProject = oFileProject
        oProjectDesign = oFileProjectDesign
        oProjectThreads = oFileProjectThreadCollection
        SaveDesign()
    End Sub
    Friend Sub ShowPrintSettingsForm()
        Using _printSettings As New FrmPrintOptions
            _printSettings.ShowDialog()
        End Using
    End Sub
    Public Sub ShowProjectInfo(pProject As Project)
        Using _ProjectInfo As New FrmProjectInfo()
            _ProjectInfo.SelectedProject = pProject
            _ProjectInfo.ShowDialog()
        End Using
    End Sub
    Private Sub UpdateThreadsFromDesign(oFileProjectDesign As ProjectDesign, oFileProjectThreadCollection As ProjectThreadCollection)
        LogUtil.LogInfo("Updating threads from design", MethodBase.GetCurrentMethod.Name)
        For Each _stitch As Stitch In oFileProjectDesign.BlockStitches
            UpdateThreadDatabaseTable(_stitch.ProjThread, oFileProjectThreadCollection)
        Next
        For Each _stitch As Stitch In oFileProjectDesign.BackStitches
            UpdateThreadDatabaseTable(_stitch.ProjThread, oFileProjectThreadCollection)
        Next
        For Each _stitch As Stitch In oFileProjectDesign.Knots
            UpdateThreadDatabaseTable(_stitch.ProjThread, oFileProjectThreadCollection)
        Next
    End Sub
    Private Sub UpdateThreadDatabaseTable(ByRef pProjectThread As ProjectThread, pFileProjectThreadCollection As ProjectThreadCollection)
        Dim _thread As Thread = FindThreadByNumber(pProjectThread.Thread.ThreadNo)
        If _thread.IsLoaded Then
            If _thread.ThreadId <> pProjectThread.ThreadId Then
                LogUtil.LogInfo("DMC" & pProjectThread.Thread.ThreadNo & " id different", MethodBase.GetCurrentMethod.Name)
                pProjectThread.ThreadId = _thread.ThreadId
            End If
        Else
            LogUtil.LogInfo("New thread DMC" & pProjectThread.Thread.ThreadNo & " found", MethodBase.GetCurrentMethod.Name)
            Dim _newThreadId As Integer = AddNewThread(_thread)
            pProjectThread.ThreadId = _newThreadId
        End If
        AddMissingProjectThread(pProjectThread, pFileProjectThreadCollection)
    End Sub
    Private Sub AddMissingProjectThread(pThread As ProjectThread, pFileProjectThreadCollection As ProjectThreadCollection)
        Dim _foundThread As ProjectThread = CType(pFileProjectThreadCollection.Threads.Find(Function(p) p.Thread.ThreadId = pThread.ThreadId), ProjectThread)
        If oFileProjectThreadCollection.Threads.Find(Function(p) p.Thread.ThreadId = pThread.ThreadId) Is Nothing Then
            LogUtil.LogInfo("Adding project thread to collection", MethodBase.GetCurrentMethod.Name)
            oFileProjectThreadCollection.Threads.Add(pThread)
        End If
    End Sub
    Public Sub SelectProjectInList(pDgv As DataGridView, _projectId As Integer)
        For Each orow As DataGridViewRow In pDgv.Rows
            If orow.Cells("projectId").Value = _projectId Then
                orow.Selected = True
                Exit For
            End If
        Next
    End Sub
    Public Sub LoadProjectTable(pDgv As DataGridView)
        isProjectLoading = True
        LoadProjectList(pDgv, MethodBase.GetCurrentMethod.Name)
        isProjectLoading = False
    End Sub
    'Public Sub OpenProjectFromFile(_filename As String, pDgv As DataGridView, pStatus As ToolStripStatusLabel)
    '    ModProject.OpenProjectFile(_filename, pStatus)
    '    If oFileProject IsNot Nothing AndAlso oFileProject.IsLoaded Then
    '        LogUtil.ShowStatus("Project file opened: " & _filename, pStatus, MethodBase.GetCurrentMethod.Name)
    '        Dim _existingProject As Project = FindProjectById(oFileProject.ProjectId)
    '        If _existingProject.IsLoaded Then
    '            If MsgBox("Project already exists. Do you want to update the existing project?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Update Project") = MsgBoxResult.Yes Then
    '                AmendProject(oFileProject)
    '                LoadProject(pDgv)
    '            Else
    '                LogUtil.ShowStatus("Project not updated: " & _existingProject.ProjectName, pStatus, MethodBase.GetCurrentMethod.Name)
    '            End If
    '        Else
    '            LogUtil.LogInfo("New project found: " & oFileProject.ProjectName, MethodBase.GetCurrentMethod.Name)
    '            Dim _newId As Integer = AddNewProject(oFileProject)
    '            SetNewProjectId(_newId, oFileProject, oFileProjectDesign, oFileProjectThreadCollection)
    '            LoadProject(pDgv)
    '        End If
    '    End If
    'End Sub
    Public Sub OpenProjectFile(pFilename As String, ByRef pStatus As ToolStripStatusLabel)
        If Not String.IsNullOrEmpty(pFilename) Then
            LogUtil.ShowStatus("Opening project file " & pFilename, pStatus, MethodBase.GetCurrentMethod.Name)
            If My.Computer.FileSystem.FileExists(pFilename) = True Then
                Try
                    CreateProjectArtifactsFromFileContents(pFilename)
                Catch ex As ApplicationException
                    LogUtil.Problem("Invalid project file " & pFilename, MethodBase.GetCurrentMethod.Name)
                    Throw ex
                End Try
                LogUtil.ShowStatus("Project Load Complete", pStatus, MethodBase.GetCurrentMethod.Name)
            Else
                LogUtil.ShowStatus("Project file not found", pStatus, MethodBase.GetCurrentMethod.Name)
            End If
        Else
            LogUtil.ShowStatus("No project file selected", pStatus)
        End If
    End Sub
    Private Sub CreateProjectArtifactsFromFileContents(pFilename As String)
        LogUtil.LogInfo("Extracting project artifacts from " & pFilename, MethodBase.GetCurrentMethod.Name)
        Try
            oFileProject = ProjectBuilder.AProject.StartingWithNothing.Build
            oFileProjectDesign = ProjectDesignBuilder.AProjectDesign.StartingWithNothing().Build
            oFileProjectThreadCollection = ProjectThreadCollectionBuilder.AProjectThreadCollection.StartingWithNothing().Build
            oFileProjectBeadCollection = ProjectBeadCollectionBuilder.AProjectBeadCollection.StartingWithNothing().Build
            Dim _projectStrings As List(Of String) = ExtractDesignStrings(pFilename)
            For Each _string As String In _projectStrings
                If _string Is Nothing Then
                    LogUtil.Problem("Unexpected null string", MethodBase.GetCurrentMethod.Name)
                    Throw New ApplicationException("Project Design file has null string", Nothing)
                End If
                Select Case True
                    Case _string.StartsWith(PROJECT_HDR)
                        Dim _projectValues As String() = _string.Split(DESIGN_DELIM)
                        oFileProject = ProjectBuilder.AProject.StartingWith(_projectValues).Build
                        LogUtil.Info("Project found", MethodBase.GetCurrentMethod.Name)
                    Case _string.StartsWith(DESIGN_HDR)
                        Dim _designValues As String() = _string.Split(DESIGN_DELIM)
                        oFileProjectDesign = ProjectDesignBuilder.AProjectDesign.StartingWith(_designValues).Build
                        LogUtil.Info("Design found", MethodBase.GetCurrentMethod.Name)
                    Case _string.StartsWith(PROJECT_THREADS_HDR)
                        Dim _threadStrings As String() = _string.Trim(BLOCK_DELIM).Split(BLOCK_DELIM)
                        oFileProjectThreadCollection = ProjectThreadCollectionBuilder.AProjectThreadCollection.StartingWith(_threadStrings).Build
                        LogUtil.Info("Threads found", MethodBase.GetCurrentMethod.Name)
                    Case _string.StartsWith(PROJECT_BEADS_HDR)
                        Dim _beadStrings As String() = _string.Trim(BLOCK_DELIM).Split(BLOCK_DELIM)
                        oFileProjectBeadCollection = ProjectBeadCollectionBuilder.AProjectBeadCollection.StartingWith(_beadStrings).Build
                        LogUtil.Info("Beads found", MethodBase.GetCurrentMethod.Name)
                    Case Else
                        LogUtil.Problem("Unexpected string", MethodBase.GetCurrentMethod.Name)
                        Throw New ApplicationException("Unknown data in project file")
                End Select
            Next
            If oFileProjectDesign.HasBeads AndAlso oFileProjectBeadCollection.Count = 0 Then
                LogUtil.Problem("Missing Project Beads", MethodBase.GetCurrentMethod.Name)
                oFileProjectBeadCollection = GenerateBeadCollection(oFileProjectDesign)
            End If
            If (oFileProjectDesign.BlockStitches.Count > 0 Or oFileProjectDesign.BackStitches.Count > 0 Or oFileProjectDesign.HasKnots) AndAlso oFileProjectThreadCollection.Count = 0 Then
                LogUtil.Problem("Missing Project Threads", MethodBase.GetCurrentMethod.Name)
                oFileProjectThreadCollection = GenerateThreadCollection(oFileProjectDesign)
            End If
            If Not oFileProject.IsLoaded OrElse Not IsValidProject(oFileProject) Then
                LogUtil.Problem("Invalid Project", MethodBase.GetCurrentMethod.Name)
                Throw New ApplicationException("Project File: No valid project record")
            End If
            If Not oFileProjectDesign.IsLoaded OrElse Not IsValidDesign(oFileProjectDesign) Then
                LogUtil.Problem("Invalid Design", MethodBase.GetCurrentMethod.Name)
                Throw New ApplicationException("Project File: No valid design record")
            End If
            If oFileProjectThreadCollection Is Nothing OrElse oFileProjectThreadCollection.Count = 0 Then
                LogUtil.Problem("No threads loaded", MethodBase.GetCurrentMethod.Name)
            End If
            If oFileProjectBeadCollection Is Nothing OrElse oFileProjectBeadCollection.Count = 0 Then
                LogUtil.Problem("No beads loaded", MethodBase.GetCurrentMethod.Name)
            End If
        Catch ex As ApplicationException
            Throw ex
        End Try
    End Sub
    Private Function GenerateThreadCollection(pDesign As ProjectDesign) As ProjectThreadCollection
        LogUtil.LogInfo("Generating Project Threads from project design", MethodBase.GetCurrentMethod.Name)
        Dim _threadCollection As ProjectThreadCollection = ProjectThreadCollectionBuilder.AProjectThreadCollection.StartingWithNothing.Build
        For Each _knot As Knot In pDesign.Knots
            If Not _knot.IsBead Then
                AddProjectThreadToCollection(pDesign.ProjectId, _knot.ThreadId, _threadCollection)
            End If
        Next
        For Each _blockstitch As BlockStitch In pDesign.BlockStitches
            For Each _qtr As BlockStitchQuarter In _blockstitch.Quarters
                AddProjectThreadToCollection(pDesign.ProjectId, _qtr.ThreadId, _threadCollection)
            Next
        Next
        For Each _backstitch As BackStitch In pDesign.BackStitches
            AddProjectThreadToCollection(pDesign.ProjectId, _backstitch.ThreadId, _threadCollection)
        Next
        Return _threadCollection
    End Function
    Private Function GenerateBeadCollection(pDesign As ProjectDesign) As ProjectBeadCollection
        LogUtil.LogInfo("Generating Project Beads from project design", MethodBase.GetCurrentMethod.Name)
        Dim _beadCollection As ProjectBeadCollection = ProjectBeadCollectionBuilder.AProjectBeadCollection.StartingWithNothing.Build
        For Each _knot As Knot In pDesign.Knots
            If _knot.IsBead Then
                AddProjectBeadToCollection(pDesign.ProjectId, _knot.ThreadId, _beadCollection)
            End If
        Next
        Return _beadCollection
    End Function
    Private Sub AddProjectThreadToCollection(pProjectId As Integer, pThreadId As Integer, _threadCollection As ProjectThreadCollection)
        Dim _projThread As ProjectThread = ProjectThreadBuilder.AProjectThread.StartingWithNothing _
                                            .WithProjectId(pProjectId) _
                                            .WithThreadId(pThreadId) _
                                            .WithIsUsed(True) _
                                            .Build
        If Not _threadCollection.Exists(_projThread.ThreadId) Then
            _threadCollection.Add(_projThread)
        End If
    End Sub
    Private Sub AddProjectBeadToCollection(pProjectId As Integer, pBeadId As Integer, _threadCollection As ProjectBeadCollection)
        Dim _projBead As ProjectBead = ProjectBeadBuilder.AProjectBead.StartingWithNothing _
                                            .WithProjectId(pProjectId) _
                                            .WithBeadId(pBeadId) _
                                            .WithIsUsed(True) _
                                            .Build
        If Not _threadCollection.Exists(_projBead.BeadId) Then
            _threadCollection.Add(_projBead)
        End If
    End Sub
    Private Function IsValidProject(pProject As Project) As Boolean
        Dim isValid As Boolean = True
        If String.IsNullOrWhiteSpace(pProject.ProjectName) Then
            isValid = False
        End If
        If pProject.DesignHeight < 1 Or pProject.DesignWidth < 1 Then
            isValid = False
        End If
        Return isValid
    End Function
    Private Function IsValidDesign(pDesign As ProjectDesign) As Boolean
        Dim isValid As Boolean = True

        Return isValid
    End Function
    Public Sub SetNewProjectId(pId As Integer, ByRef pProject As Project, ByRef pProjectDesign As ProjectDesign, ByRef pProjectThreadCollection As ProjectThreadCollection)
        pProject.ProjectId = pId
        pProjectDesign.ProjectId = pId
        For Each _stitch As Stitch In pProjectDesign.BackStitches
            _stitch._projectId = pId
        Next
        For Each _stitch As Stitch In pProjectDesign.BlockStitches
            _stitch._projectId = pId
        Next
        For Each _stitch As Stitch In pProjectDesign.Knots
            _stitch._projectId = pId
        Next
        For Each _thread As ProjectThread In pProjectThreadCollection.Threads
            _thread.ProjectId = pId
        Next
    End Sub
    Public Sub InsertProjectThreadCollection(pThreadCollection As ProjectThreadCollection)
        For Each _thread As ProjectThread In pThreadCollection.Threads
            If Not FindProjectThread(_thread.ProjectId, _thread.ThreadId).IsLoaded Then
                AddNewProjectThread(_thread)
            End If
        Next
    End Sub
    Public Sub LoadProjectList(ByRef pDgv As DataGridView, pBaseName As String)
        LogUtil.LogInfo("Load project list", pBaseName)
        pDgv.Rows.Clear()
        For Each oProject As Project In GetProjectList()
            AddProjectRow(pDgv, oProject)
        Next
        pDgv.ClearSelection()
    End Sub
    Private Sub AddProjectRow(ByRef pDgv As DataGridView, oProject As Project)
        Dim oRow As DataGridViewRow = pDgv.Rows(pDgv.Rows.Add())
        oRow.Cells(pDgv.Columns(0).Name).Value = oProject.ProjectId
        oRow.Cells(pDgv.Columns(1).Name).Value = oProject.ProjectName
    End Sub
    Public Function SelectProjectInList(ByRef pDgv As DataGridView, pColName As String, pProjectId As Integer) As Integer
        Dim _index As Integer = 0
        For Each orow As DataGridViewRow In pDgv.Rows
            If orow.Cells(pColName).Value = pProjectId Then
                orow.Selected = True
                pDgv.FirstDisplayedScrollingRowIndex = orow.Index
                _index = orow.Index
                Exit For
            End If
        Next
        Return _index
    End Function
    Public Function StartProjectTimer(pProject As Project) As String
        Dim _rtnMsg As String
        If pProject.IsLoaded Then
            If oTimerForm Is Nothing OrElse oTimerForm.IsDisposed Then
                oTimerForm = New FrmProjectTimer
            End If
            oTimerForm.Project = pProject
            oTimerForm.Show()
            oTimerForm.TopMost = True
            _rtnMsg = "Timer started"
        Else
            _rtnMsg = "No project selected"
        End If
        Return _rtnMsg
    End Function
    Public Sub CloseTimer()
        If oTimerForm IsNot Nothing AndAlso Not oTimerForm.IsDisposed Then
            oTimerForm.Close()
        End If
    End Sub
    Friend Sub OpenBrandListForm()
        Using _brands As New FrmBrands
            _brands.ShowDialog()
        End Using
    End Sub
    Friend Sub OpenPaletteForm()
        Using _palettes As New FrmPaletteMaint
            _palettes.ShowDialog()
        End Using
    End Sub
    Friend Sub OpenSettingsForm()
        Using _settings As New FrmGlobalSettings
            _settings.ShowDialog()
        End Using
    End Sub
End Module