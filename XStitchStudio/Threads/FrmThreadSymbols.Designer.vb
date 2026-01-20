' Hindleware
' Copyright (c) 2025 Eric Hindle
' All rights reserved.
'
' Author Eric Hindle
'

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmThreadSymbols
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmThreadSymbols))
        Me.FlpSymbols = New System.Windows.Forms.FlowLayoutPanel()
        Me.BtnAuto = New System.Windows.Forms.Button()
        Me.BtnUpdate = New System.Windows.Forms.Button()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.PnlThreads = New System.Windows.Forms.Panel()
        Me.DgvThreads = New System.Windows.Forms.DataGridView()
        Me.threadId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.threadName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.threadNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.threadSortNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.threadColour = New System.Windows.Forms.DataGridViewImageColumn()
        Me.threadsymbol1 = New System.Windows.Forms.DataGridViewImageColumn()
        Me.threadsymbol = New System.Windows.Forms.DataGridViewImageColumn()
        Me.threadsymbolid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.threadsymbol1id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.LblProjectName = New System.Windows.Forms.Label()
        Me.BtnClearSymbol = New System.Windows.Forms.Button()
        Me.PicSingle = New System.Windows.Forms.PictureBox()
        Me.PicDouble = New System.Windows.Forms.PictureBox()
        Me.RbSingle = New System.Windows.Forms.RadioButton()
        Me.RbDouble = New System.Windows.Forms.RadioButton()
        Me.LblColourName = New System.Windows.Forms.Label()
        Me.GrpSelectedColour = New System.Windows.Forms.GroupBox()
        Me.PnlThreads.SuspendLayout()
        CType(Me.DgvThreads, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicSingle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicDouble, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpSelectedColour.SuspendLayout()
        Me.SuspendLayout()
        '
        'FlpSymbols
        '
        Me.FlpSymbols.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FlpSymbols.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.FlpSymbols.Location = New System.Drawing.Point(466, 12)
        Me.FlpSymbols.Name = "FlpSymbols"
        Me.FlpSymbols.Size = New System.Drawing.Size(451, 516)
        Me.FlpSymbols.TabIndex = 161
        '
        'BtnAuto
        '
        Me.BtnAuto.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnAuto.BackColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(21, Byte), Integer))
        Me.BtnAuto.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.BtnAuto.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAuto.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAuto.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.BtnAuto.Location = New System.Drawing.Point(12, 554)
        Me.BtnAuto.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnAuto.Name = "BtnAuto"
        Me.BtnAuto.Size = New System.Drawing.Size(102, 37)
        Me.BtnAuto.TabIndex = 160
        Me.BtnAuto.Text = "Auto Allocate"
        Me.BtnAuto.UseVisualStyleBackColor = False
        '
        'BtnUpdate
        '
        Me.BtnUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnUpdate.BackColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(21, Byte), Integer))
        Me.BtnUpdate.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.BtnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnUpdate.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnUpdate.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.BtnUpdate.Location = New System.Drawing.Point(594, 535)
        Me.BtnUpdate.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnUpdate.Name = "BtnUpdate"
        Me.BtnUpdate.Size = New System.Drawing.Size(106, 59)
        Me.BtnUpdate.TabIndex = 159
        Me.BtnUpdate.Text = "Apply Changes"
        Me.BtnUpdate.UseVisualStyleBackColor = False
        '
        'BtnClose
        '
        Me.BtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(21, Byte), Integer))
        Me.BtnClose.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClose.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.BtnClose.Location = New System.Drawing.Point(837, 560)
        Me.BtnClose.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(80, 31)
        Me.BtnClose.TabIndex = 158
        Me.BtnClose.Text = "Close"
        Me.BtnClose.UseVisualStyleBackColor = False
        '
        'PnlThreads
        '
        Me.PnlThreads.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PnlThreads.Controls.Add(Me.DgvThreads)
        Me.PnlThreads.Controls.Add(Me.Label7)
        Me.PnlThreads.Location = New System.Drawing.Point(12, 42)
        Me.PnlThreads.Name = "PnlThreads"
        Me.PnlThreads.Size = New System.Drawing.Size(434, 433)
        Me.PnlThreads.TabIndex = 157
        '
        'DgvThreads
        '
        Me.DgvThreads.AllowUserToAddRows = False
        Me.DgvThreads.AllowUserToDeleteRows = False
        Me.DgvThreads.AllowUserToResizeRows = False
        Me.DgvThreads.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DgvThreads.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.DgvThreads.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvThreads.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DgvThreads.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvThreads.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.threadId, Me.threadName, Me.threadNo, Me.threadSortNumber, Me.threadColour, Me.threadsymbol1, Me.threadsymbol, Me.threadsymbolid, Me.threadsymbol1id})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvThreads.DefaultCellStyle = DataGridViewCellStyle2
        Me.DgvThreads.GridColor = System.Drawing.Color.FromArgb(CType(CType(93, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DgvThreads.Location = New System.Drawing.Point(0, 41)
        Me.DgvThreads.Margin = New System.Windows.Forms.Padding(4)
        Me.DgvThreads.MultiSelect = False
        Me.DgvThreads.Name = "DgvThreads"
        Me.DgvThreads.ReadOnly = True
        Me.DgvThreads.RowHeadersVisible = False
        Me.DgvThreads.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.DgvThreads.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.DgvThreads.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.DimGray
        Me.DgvThreads.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.DgvThreads.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.DgvThreads.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvThreads.Size = New System.Drawing.Size(430, 388)
        Me.DgvThreads.TabIndex = 140
        '
        'threadId
        '
        Me.threadId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.threadId.HeaderText = "Id"
        Me.threadId.Name = "threadId"
        Me.threadId.ReadOnly = True
        Me.threadId.Visible = False
        '
        'threadName
        '
        Me.threadName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.threadName.HeaderText = "Name"
        Me.threadName.Name = "threadName"
        Me.threadName.ReadOnly = True
        '
        'threadNo
        '
        Me.threadNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.threadNo.HeaderText = "No."
        Me.threadNo.Name = "threadNo"
        Me.threadNo.ReadOnly = True
        Me.threadNo.Width = 50
        '
        'threadSortNumber
        '
        Me.threadSortNumber.HeaderText = "Sort"
        Me.threadSortNumber.Name = "threadSortNumber"
        Me.threadSortNumber.ReadOnly = True
        Me.threadSortNumber.Visible = False
        '
        'threadColour
        '
        Me.threadColour.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.threadColour.HeaderText = "Colour"
        Me.threadColour.Name = "threadColour"
        Me.threadColour.ReadOnly = True
        Me.threadColour.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.threadColour.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.threadColour.Width = 60
        '
        'threadsymbol1
        '
        Me.threadsymbol1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.threadsymbol1.HeaderText = "sgl"
        Me.threadsymbol1.Name = "threadsymbol1"
        Me.threadsymbol1.ReadOnly = True
        Me.threadsymbol1.Width = 32
        '
        'threadsymbol
        '
        Me.threadsymbol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.threadsymbol.HeaderText = "dbl"
        Me.threadsymbol.Name = "threadsymbol"
        Me.threadsymbol.ReadOnly = True
        Me.threadsymbol.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.threadsymbol.Width = 32
        '
        'threadsymbolid
        '
        Me.threadsymbolid.HeaderText = "symbolid"
        Me.threadsymbolid.Name = "threadsymbolid"
        Me.threadsymbolid.ReadOnly = True
        Me.threadsymbolid.Visible = False
        '
        'threadsymbol1id
        '
        Me.threadsymbol1id.HeaderText = "symbolid1"
        Me.threadsymbol1id.Name = "threadsymbol1id"
        Me.threadsymbol1id.ReadOnly = True
        Me.threadsymbol1id.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Felix Titling", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(21, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(4, 14)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(98, 23)
        Me.Label7.TabIndex = 145
        Me.Label7.Text = "THREADS"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 605)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(930, 22)
        Me.StatusStrip1.TabIndex = 162
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'LblProjectName
        '
        Me.LblProjectName.AutoSize = True
        Me.LblProjectName.Font = New System.Drawing.Font("Felix Titling", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblProjectName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(21, Byte), Integer))
        Me.LblProjectName.Location = New System.Drawing.Point(8, 9)
        Me.LblProjectName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblProjectName.Name = "LblProjectName"
        Me.LblProjectName.Size = New System.Drawing.Size(152, 23)
        Me.LblProjectName.TabIndex = 163
        Me.LblProjectName.Text = "Project Name"
        '
        'BtnClearSymbol
        '
        Me.BtnClearSymbol.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnClearSymbol.BackColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(21, Byte), Integer))
        Me.BtnClearSymbol.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.BtnClearSymbol.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClearSymbol.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClearSymbol.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.BtnClearSymbol.Location = New System.Drawing.Point(143, 554)
        Me.BtnClearSymbol.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnClearSymbol.Name = "BtnClearSymbol"
        Me.BtnClearSymbol.Size = New System.Drawing.Size(102, 37)
        Me.BtnClearSymbol.TabIndex = 164
        Me.BtnClearSymbol.Text = "Clear Symbol"
        Me.BtnClearSymbol.UseVisualStyleBackColor = False
        '
        'PicSingle
        '
        Me.PicSingle.BackColor = System.Drawing.Color.White
        Me.PicSingle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PicSingle.Location = New System.Drawing.Point(58, 41)
        Me.PicSingle.Name = "PicSingle"
        Me.PicSingle.Size = New System.Drawing.Size(23, 22)
        Me.PicSingle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PicSingle.TabIndex = 165
        Me.PicSingle.TabStop = False
        '
        'PicDouble
        '
        Me.PicDouble.BackColor = System.Drawing.Color.White
        Me.PicDouble.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PicDouble.Location = New System.Drawing.Point(235, 41)
        Me.PicDouble.Name = "PicDouble"
        Me.PicDouble.Size = New System.Drawing.Size(23, 22)
        Me.PicDouble.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PicDouble.TabIndex = 166
        Me.PicDouble.TabStop = False
        '
        'RbSingle
        '
        Me.RbSingle.AutoSize = True
        Me.RbSingle.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbSingle.Location = New System.Drawing.Point(87, 43)
        Me.RbSingle.Name = "RbSingle"
        Me.RbSingle.Size = New System.Drawing.Size(138, 18)
        Me.RbSingle.TabIndex = 167
        Me.RbSingle.Text = "Single thread symbol"
        Me.RbSingle.UseVisualStyleBackColor = True
        '
        'RbDouble
        '
        Me.RbDouble.AutoSize = True
        Me.RbDouble.Checked = True
        Me.RbDouble.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbDouble.Location = New System.Drawing.Point(264, 43)
        Me.RbDouble.Name = "RbDouble"
        Me.RbDouble.Size = New System.Drawing.Size(144, 18)
        Me.RbDouble.TabIndex = 168
        Me.RbDouble.TabStop = True
        Me.RbDouble.Text = "Double thread symbol"
        Me.RbDouble.UseVisualStyleBackColor = True
        '
        'LblColourName
        '
        Me.LblColourName.AutoSize = True
        Me.LblColourName.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblColourName.Location = New System.Drawing.Point(10, 16)
        Me.LblColourName.Name = "LblColourName"
        Me.LblColourName.Size = New System.Drawing.Size(104, 18)
        Me.LblColourName.TabIndex = 169
        Me.LblColourName.Text = "Colour Name"
        '
        'GrpSelectedColour
        '
        Me.GrpSelectedColour.Controls.Add(Me.PicDouble)
        Me.GrpSelectedColour.Controls.Add(Me.LblColourName)
        Me.GrpSelectedColour.Controls.Add(Me.PicSingle)
        Me.GrpSelectedColour.Controls.Add(Me.RbDouble)
        Me.GrpSelectedColour.Controls.Add(Me.RbSingle)
        Me.GrpSelectedColour.Location = New System.Drawing.Point(12, 478)
        Me.GrpSelectedColour.Name = "GrpSelectedColour"
        Me.GrpSelectedColour.Size = New System.Drawing.Size(430, 69)
        Me.GrpSelectedColour.TabIndex = 170
        Me.GrpSelectedColour.TabStop = False
        '
        'FrmThreadSymbols
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(930, 627)
        Me.Controls.Add(Me.GrpSelectedColour)
        Me.Controls.Add(Me.BtnClearSymbol)
        Me.Controls.Add(Me.LblProjectName)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.FlpSymbols)
        Me.Controls.Add(Me.BtnAuto)
        Me.Controls.Add(Me.BtnUpdate)
        Me.Controls.Add(Me.BtnClose)
        Me.Controls.Add(Me.PnlThreads)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmThreadSymbols"
        Me.Text = "ProjectThread Symbols"
        Me.PnlThreads.ResumeLayout(False)
        Me.PnlThreads.PerformLayout()
        CType(Me.DgvThreads, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicSingle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicDouble, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpSelectedColour.ResumeLayout(False)
        Me.GrpSelectedColour.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents FlpSymbols As FlowLayoutPanel
    Friend WithEvents BtnAuto As Button
    Friend WithEvents BtnUpdate As Button
    Friend WithEvents BtnClose As Button
    Friend WithEvents PnlThreads As Panel
    Friend WithEvents DgvThreads As DataGridView
    Friend WithEvents Label7 As Label
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents LblProjectName As Label
    Friend WithEvents BtnClearSymbol As Button
    Friend WithEvents threadId As DataGridViewTextBoxColumn
    Friend WithEvents threadName As DataGridViewTextBoxColumn
    Friend WithEvents threadNo As DataGridViewTextBoxColumn
    Friend WithEvents threadSortNumber As DataGridViewTextBoxColumn
    Friend WithEvents threadColour As DataGridViewImageColumn
    Friend WithEvents threadsymbol1 As DataGridViewImageColumn
    Friend WithEvents threadsymbol As DataGridViewImageColumn
    Friend WithEvents threadsymbolid As DataGridViewTextBoxColumn
    Friend WithEvents threadsymbol1id As DataGridViewTextBoxColumn
    Friend WithEvents PicSingle As PictureBox
    Friend WithEvents PicDouble As PictureBox
    Friend WithEvents RbSingle As RadioButton
    Friend WithEvents RbDouble As RadioButton
    Friend WithEvents LblColourName As Label
    Friend WithEvents GrpSelectedColour As GroupBox
End Class
