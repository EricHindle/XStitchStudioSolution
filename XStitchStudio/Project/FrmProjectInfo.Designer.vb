' Hindleware
' Copyright (c) 2025-6 Eric Hindle
' All rights reserved.
'
' Author Eric Hindle
'

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmProjectInfo
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.LblName = New System.Windows.Forms.Label()
        Me.LblFabricCount = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.LblTotalTime = New System.Windows.Forms.Label()
        Me.LblCentreY = New System.Windows.Forms.Label()
        Me.LblCentreX = New System.Windows.Forms.Label()
        Me.LblFabricColour = New System.Windows.Forms.Label()
        Me.LblFabricHeight = New System.Windows.Forms.Label()
        Me.LblFabricWidth = New System.Windows.Forms.Label()
        Me.LblDesignHeight = New System.Windows.Forms.Label()
        Me.LblDesignWidth = New System.Windows.Forms.Label()
        Me.LblFinished = New System.Windows.Forms.Label()
        Me.LblStarted = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.DgvWorkPeriods = New System.Windows.Forms.DataGridView()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.timeDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.timeStarted = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.timeEnded = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.timeMinutes = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DgvWorkPeriods, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LblName
        '
        Me.LblName.AutoSize = True
        Me.LblName.BackColor = System.Drawing.SystemColors.Control
        Me.LblName.Font = New System.Drawing.Font("Felix Titling", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(21, Byte), Integer))
        Me.LblName.Location = New System.Drawing.Point(14, 9)
        Me.LblName.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.LblName.Name = "LblName"
        Me.LblName.Size = New System.Drawing.Size(152, 23)
        Me.LblName.TabIndex = 137
        Me.LblName.Text = "PROJECT NAME"
        '
        'LblFabricCount
        '
        Me.LblFabricCount.AutoSize = True
        Me.LblFabricCount.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFabricCount.Location = New System.Drawing.Point(135, 309)
        Me.LblFabricCount.Name = "LblFabricCount"
        Me.LblFabricCount.Size = New System.Drawing.Size(75, 14)
        Me.LblFabricCount.TabIndex = 223
        Me.LblFabricCount.Text = "Fabric Count"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(50, 309)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(75, 14)
        Me.Label26.TabIndex = 222
        Me.Label26.Text = "Fabric Count"
        '
        'LblTotalTime
        '
        Me.LblTotalTime.AutoSize = True
        Me.LblTotalTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalTime.Location = New System.Drawing.Point(136, 100)
        Me.LblTotalTime.Name = "LblTotalTime"
        Me.LblTotalTime.Size = New System.Drawing.Size(94, 14)
        Me.LblTotalTime.TabIndex = 221
        Me.LblTotalTime.Text = "Total work time"
        '
        'LblCentreY
        '
        Me.LblCentreY.AutoSize = True
        Me.LblCentreY.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCentreY.Location = New System.Drawing.Point(136, 204)
        Me.LblCentreY.Name = "LblCentreY"
        Me.LblCentreY.Size = New System.Drawing.Size(95, 14)
        Me.LblCentreY.TabIndex = 220
        Me.LblCentreY.Text = "Centre Y co-ord"
        '
        'LblCentreX
        '
        Me.LblCentreX.AutoSize = True
        Me.LblCentreX.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCentreX.Location = New System.Drawing.Point(136, 178)
        Me.LblCentreX.Name = "LblCentreX"
        Me.LblCentreX.Size = New System.Drawing.Size(94, 14)
        Me.LblCentreX.TabIndex = 219
        Me.LblCentreX.Text = "Centre X co-ord"
        '
        'LblFabricColour
        '
        Me.LblFabricColour.AutoSize = True
        Me.LblFabricColour.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFabricColour.Location = New System.Drawing.Point(136, 282)
        Me.LblFabricColour.Name = "LblFabricColour"
        Me.LblFabricColour.Size = New System.Drawing.Size(76, 14)
        Me.LblFabricColour.TabIndex = 218
        Me.LblFabricColour.Text = "Fabric Colour"
        '
        'LblFabricHeight
        '
        Me.LblFabricHeight.AutoSize = True
        Me.LblFabricHeight.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFabricHeight.Location = New System.Drawing.Point(136, 256)
        Me.LblFabricHeight.Name = "LblFabricHeight"
        Me.LblFabricHeight.Size = New System.Drawing.Size(78, 14)
        Me.LblFabricHeight.TabIndex = 217
        Me.LblFabricHeight.Text = "Fabric Height"
        '
        'LblFabricWidth
        '
        Me.LblFabricWidth.AutoSize = True
        Me.LblFabricWidth.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFabricWidth.Location = New System.Drawing.Point(136, 230)
        Me.LblFabricWidth.Name = "LblFabricWidth"
        Me.LblFabricWidth.Size = New System.Drawing.Size(75, 14)
        Me.LblFabricWidth.TabIndex = 216
        Me.LblFabricWidth.Text = "Fabric Width"
        '
        'LblDesignHeight
        '
        Me.LblDesignHeight.AutoSize = True
        Me.LblDesignHeight.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDesignHeight.Location = New System.Drawing.Point(136, 152)
        Me.LblDesignHeight.Name = "LblDesignHeight"
        Me.LblDesignHeight.Size = New System.Drawing.Size(83, 14)
        Me.LblDesignHeight.TabIndex = 215
        Me.LblDesignHeight.Text = "Design Height"
        '
        'LblDesignWidth
        '
        Me.LblDesignWidth.AutoSize = True
        Me.LblDesignWidth.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDesignWidth.Location = New System.Drawing.Point(136, 126)
        Me.LblDesignWidth.Name = "LblDesignWidth"
        Me.LblDesignWidth.Size = New System.Drawing.Size(80, 14)
        Me.LblDesignWidth.TabIndex = 214
        Me.LblDesignWidth.Text = "Design Width"
        '
        'LblFinished
        '
        Me.LblFinished.AutoSize = True
        Me.LblFinished.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFinished.Location = New System.Drawing.Point(136, 74)
        Me.LblFinished.Name = "LblFinished"
        Me.LblFinished.Size = New System.Drawing.Size(50, 14)
        Me.LblFinished.TabIndex = 213
        Me.LblFinished.Text = "Finished"
        '
        'LblStarted
        '
        Me.LblStarted.AutoSize = True
        Me.LblStarted.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblStarted.Location = New System.Drawing.Point(136, 48)
        Me.LblStarted.Name = "LblStarted"
        Me.LblStarted.Size = New System.Drawing.Size(48, 14)
        Me.LblStarted.TabIndex = 212
        Me.LblStarted.Text = "Started"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(33, 100)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(94, 14)
        Me.Label14.TabIndex = 211
        Me.Label14.Text = "Total work time"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(32, 204)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(95, 14)
        Me.Label12.TabIndex = 210
        Me.Label12.Text = "Centre Y co-ord"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(33, 178)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(94, 14)
        Me.Label13.TabIndex = 209
        Me.Label13.Text = "Centre X co-ord"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(51, 282)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(76, 14)
        Me.Label8.TabIndex = 208
        Me.Label8.Text = "Fabric Colour"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(49, 256)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(78, 14)
        Me.Label4.TabIndex = 207
        Me.Label4.Text = "Fabric Height"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(52, 230)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(75, 14)
        Me.Label6.TabIndex = 206
        Me.Label6.Text = "Fabric Width"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(44, 152)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(83, 14)
        Me.Label5.TabIndex = 205
        Me.Label5.Text = "Design Height"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(47, 126)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(80, 14)
        Me.Label9.TabIndex = 204
        Me.Label9.Text = "Design Width"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(77, 74)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(50, 14)
        Me.Label10.TabIndex = 203
        Me.Label10.Text = "Finished"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(79, 48)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(48, 14)
        Me.Label11.TabIndex = 202
        Me.Label11.Text = "Started"
        '
        'BtnClose
        '
        Me.BtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(21, Byte), Integer))
        Me.BtnClose.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClose.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.BtnClose.Location = New System.Drawing.Point(554, 295)
        Me.BtnClose.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(80, 40)
        Me.BtnClose.TabIndex = 224
        Me.BtnClose.Text = "Close"
        Me.BtnClose.UseVisualStyleBackColor = False
        '
        'DgvWorkPeriods
        '
        Me.DgvWorkPeriods.AllowUserToAddRows = False
        Me.DgvWorkPeriods.AllowUserToDeleteRows = False
        Me.DgvWorkPeriods.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DgvWorkPeriods.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvWorkPeriods.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.timeDate, Me.timeStarted, Me.timeEnded, Me.timeMinutes})
        Me.DgvWorkPeriods.Location = New System.Drawing.Point(281, 42)
        Me.DgvWorkPeriods.Name = "DgvWorkPeriods"
        Me.DgvWorkPeriods.ReadOnly = True
        Me.DgvWorkPeriods.RowHeadersVisible = False
        Me.DgvWorkPeriods.Size = New System.Drawing.Size(354, 246)
        Me.DgvWorkPeriods.TabIndex = 225
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Felix Titling", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(21, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(277, 20)
        Me.Label7.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(138, 19)
        Me.Label7.TabIndex = 226
        Me.Label7.Text = "WORK PERIODS"
        '
        'timeDate
        '
        Me.timeDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.timeDate.HeaderText = "Date"
        Me.timeDate.Name = "timeDate"
        Me.timeDate.ReadOnly = True
        '
        'timeStarted
        '
        Me.timeStarted.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.timeStarted.HeaderText = "Started"
        Me.timeStarted.Name = "timeStarted"
        Me.timeStarted.ReadOnly = True
        '
        'timeEnded
        '
        Me.timeEnded.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.timeEnded.HeaderText = "Ended"
        Me.timeEnded.Name = "timeEnded"
        Me.timeEnded.ReadOnly = True
        '
        'timeMinutes
        '
        Me.timeMinutes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.timeMinutes.HeaderText = "Minutes"
        Me.timeMinutes.Name = "timeMinutes"
        Me.timeMinutes.ReadOnly = True
        Me.timeMinutes.Width = 50
        '
        'FrmProjectInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(647, 348)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.DgvWorkPeriods)
        Me.Controls.Add(Me.BtnClose)
        Me.Controls.Add(Me.LblFabricCount)
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.LblTotalTime)
        Me.Controls.Add(Me.LblCentreY)
        Me.Controls.Add(Me.LblCentreX)
        Me.Controls.Add(Me.LblFabricColour)
        Me.Controls.Add(Me.LblFabricHeight)
        Me.Controls.Add(Me.LblFabricWidth)
        Me.Controls.Add(Me.LblDesignHeight)
        Me.Controls.Add(Me.LblDesignWidth)
        Me.Controls.Add(Me.LblFinished)
        Me.Controls.Add(Me.LblStarted)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.LblName)
        Me.Name = "FrmProjectInfo"
        Me.Text = "Project Information"
        CType(Me.DgvWorkPeriods, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LblName As Label
    Friend WithEvents LblFabricCount As Label
    Friend WithEvents Label26 As Label
    Friend WithEvents LblTotalTime As Label
    Friend WithEvents LblCentreY As Label
    Friend WithEvents LblCentreX As Label
    Friend WithEvents LblFabricColour As Label
    Friend WithEvents LblFabricHeight As Label
    Friend WithEvents LblFabricWidth As Label
    Friend WithEvents LblDesignHeight As Label
    Friend WithEvents LblDesignWidth As Label
    Friend WithEvents LblFinished As Label
    Friend WithEvents LblStarted As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents BtnClose As Button
    Friend WithEvents DgvWorkPeriods As DataGridView
    Friend WithEvents Label7 As Label
    Friend WithEvents timeDate As DataGridViewTextBoxColumn
    Friend WithEvents timeStarted As DataGridViewTextBoxColumn
    Friend WithEvents timeEnded As DataGridViewTextBoxColumn
    Friend WithEvents timeMinutes As DataGridViewTextBoxColumn
End Class
