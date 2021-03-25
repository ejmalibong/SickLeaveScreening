<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmScreenList
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmScreenList))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.lblStartFrom = New System.Windows.Forms.Label()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.btnSearchDate = New System.Windows.Forms.Button()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.lblStartTo = New System.Windows.Forms.Label()
        Me.bindingNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.txtTotalPageNumber = New System.Windows.Forms.ToolStripLabel()
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.txtPageNumber = New System.Windows.Forms.ToolStripTextBox()
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnGo = New System.Windows.Forms.ToolStripButton()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.btnRefresh = New PinkieControls.ButtonXP()
        Me.btnClose = New PinkieControls.ButtonXP()
        Me.btnDelete = New PinkieControls.ButtonXP()
        Me.btnEdit = New PinkieControls.ButtonXP()
        Me.btnAdd = New PinkieControls.ButtonXP()
        Me.dgvList = New System.Windows.Forms.DataGridView()
        Me.ColScreenId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColScreenDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColEmployeeCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColEmployeeName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColRemarks = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColModifiedDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pnlTop.SuspendLayout()
        CType(Me.bindingNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.bindingNavigator.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTop.Controls.Add(Me.lblStartFrom)
        Me.pnlTop.Controls.Add(Me.btnReset)
        Me.pnlTop.Controls.Add(Me.btnSearchDate)
        Me.pnlTop.Controls.Add(Me.dtpFrom)
        Me.pnlTop.Controls.Add(Me.dtpTo)
        Me.pnlTop.Controls.Add(Me.lblStartTo)
        Me.pnlTop.Controls.Add(Me.bindingNavigator)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(1039, 36)
        Me.pnlTop.TabIndex = 0
        '
        'lblStartFrom
        '
        Me.lblStartFrom.AutoSize = True
        Me.lblStartFrom.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.lblStartFrom.Location = New System.Drawing.Point(6, 10)
        Me.lblStartFrom.Name = "lblStartFrom"
        Me.lblStartFrom.Size = New System.Drawing.Size(38, 14)
        Me.lblStartFrom.TabIndex = 24
        Me.lblStartFrom.Text = "From"
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnReset.Location = New System.Drawing.Point(436, 4)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(85, 26)
        Me.btnReset.TabIndex = 23
        Me.btnReset.Text = "Reset"
        Me.btnReset.UseVisualStyleBackColor = True
        '
        'btnSearchDate
        '
        Me.btnSearchDate.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnSearchDate.Location = New System.Drawing.Point(348, 4)
        Me.btnSearchDate.Name = "btnSearchDate"
        Me.btnSearchDate.Size = New System.Drawing.Size(85, 26)
        Me.btnSearchDate.TabIndex = 22
        Me.btnSearchDate.Text = "Search"
        Me.btnSearchDate.UseVisualStyleBackColor = True
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "MMM dd, yyyy"
        Me.dtpFrom.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(50, 6)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(130, 22)
        Me.dtpFrom.TabIndex = 20
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "MMM dd, yyyy"
        Me.dtpTo.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(213, 6)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(130, 22)
        Me.dtpTo.TabIndex = 21
        '
        'lblStartTo
        '
        Me.lblStartTo.AutoSize = True
        Me.lblStartTo.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.lblStartTo.Location = New System.Drawing.Point(186, 10)
        Me.lblStartTo.Name = "lblStartTo"
        Me.lblStartTo.Size = New System.Drawing.Size(21, 14)
        Me.lblStartTo.TabIndex = 25
        Me.lblStartTo.Text = "To"
        '
        'bindingNavigator
        '
        Me.bindingNavigator.AddNewItem = Nothing
        Me.bindingNavigator.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bindingNavigator.BackColor = System.Drawing.Color.White
        Me.bindingNavigator.CountItem = Me.txtTotalPageNumber
        Me.bindingNavigator.CountItemFormat = "of "
        Me.bindingNavigator.DeleteItem = Nothing
        Me.bindingNavigator.Dock = System.Windows.Forms.DockStyle.None
        Me.bindingNavigator.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.bindingNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.txtPageNumber, Me.txtTotalPageNumber, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.ToolStripSeparator1, Me.btnGo})
        Me.bindingNavigator.Location = New System.Drawing.Point(837, 4)
        Me.bindingNavigator.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.bindingNavigator.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.bindingNavigator.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.bindingNavigator.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.bindingNavigator.Name = "bindingNavigator"
        Me.bindingNavigator.PositionItem = Me.txtPageNumber
        Me.bindingNavigator.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.bindingNavigator.Size = New System.Drawing.Size(201, 25)
        Me.bindingNavigator.TabIndex = 10
        Me.bindingNavigator.Text = "PagerPanel"
        '
        'txtTotalPageNumber
        '
        Me.txtTotalPageNumber.Name = "txtTotalPageNumber"
        Me.txtTotalPageNumber.Size = New System.Drawing.Size(21, 22)
        Me.txtTotalPageNumber.Text = "of "
        Me.txtTotalPageNumber.ToolTipText = "Total number of items"
        '
        'BindingNavigatorMoveFirstItem
        '
        Me.BindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveFirstItem.Image = CType(resources.GetObject("BindingNavigatorMoveFirstItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveFirstItem.Name = "BindingNavigatorMoveFirstItem"
        Me.BindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveFirstItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveFirstItem.Text = "Move first"
        '
        'BindingNavigatorMovePreviousItem
        '
        Me.BindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMovePreviousItem.Image = CType(resources.GetObject("BindingNavigatorMovePreviousItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMovePreviousItem.Name = "BindingNavigatorMovePreviousItem"
        Me.BindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMovePreviousItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMovePreviousItem.Text = "Move previous"
        '
        'BindingNavigatorSeparator
        '
        Me.BindingNavigatorSeparator.Name = "BindingNavigatorSeparator"
        Me.BindingNavigatorSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'txtPageNumber
        '
        Me.txtPageNumber.AccessibleName = "Position"
        Me.txtPageNumber.AutoSize = False
        Me.txtPageNumber.Name = "txtPageNumber"
        Me.txtPageNumber.Size = New System.Drawing.Size(30, 23)
        Me.txtPageNumber.Text = "0"
        Me.txtPageNumber.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtPageNumber.ToolTipText = "Current position"
        '
        'BindingNavigatorSeparator1
        '
        Me.BindingNavigatorSeparator1.Name = "BindingNavigatorSeparator1"
        Me.BindingNavigatorSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorMoveNextItem
        '
        Me.BindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveNextItem.Image = CType(resources.GetObject("BindingNavigatorMoveNextItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveNextItem.Name = "BindingNavigatorMoveNextItem"
        Me.BindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveNextItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveNextItem.Text = "Move next"
        '
        'BindingNavigatorMoveLastItem
        '
        Me.BindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveLastItem.Image = CType(resources.GetObject("BindingNavigatorMoveLastItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveLastItem.Name = "BindingNavigatorMoveLastItem"
        Me.BindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveLastItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveLastItem.Text = "Move last"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnGo
        '
        Me.btnGo.AutoSize = False
        Me.btnGo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnGo.Image = CType(resources.GetObject("btnGo.Image"), System.Drawing.Image)
        Me.btnGo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(35, 22)
        Me.btnGo.Text = "Go"
        '
        'pnlBottom
        '
        Me.pnlBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlBottom.Controls.Add(Me.btnRefresh)
        Me.pnlBottom.Controls.Add(Me.btnClose)
        Me.pnlBottom.Controls.Add(Me.btnDelete)
        Me.pnlBottom.Controls.Add(Me.btnEdit)
        Me.pnlBottom.Controls.Add(Me.btnAdd)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 615)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(1039, 46)
        Me.pnlBottom.TabIndex = 1
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnRefresh.DefaultScheme = False
        Me.btnRefresh.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnRefresh.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnRefresh.Hint = "Refresh List"
        Me.btnRefresh.Image = Global.SickLeaveScreening.My.Resources.Resources.Sync_16_x_16
        Me.btnRefresh.Location = New System.Drawing.Point(4, 4)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnRefresh.Size = New System.Drawing.Size(130, 36)
        Me.btnRefresh.TabIndex = 160
        Me.btnRefresh.TabStop = False
        Me.btnRefresh.Text = "Refresh (F5)"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnClose.DefaultScheme = False
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnClose.Hint = ""
        Me.btnClose.Location = New System.Drawing.Point(922, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnClose.Size = New System.Drawing.Size(110, 36)
        Me.btnClose.TabIndex = 159
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnDelete.DefaultScheme = False
        Me.btnDelete.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnDelete.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnDelete.Hint = ""
        Me.btnDelete.Image = Global.SickLeaveScreening.My.Resources.Resources.Delete_16_x_16
        Me.btnDelete.Location = New System.Drawing.Point(808, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnDelete.Size = New System.Drawing.Size(110, 36)
        Me.btnDelete.TabIndex = 158
        Me.btnDelete.Text = "Delete (F4)"
        '
        'btnEdit
        '
        Me.btnEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEdit.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnEdit.DefaultScheme = False
        Me.btnEdit.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnEdit.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnEdit.Hint = ""
        Me.btnEdit.Image = Global.SickLeaveScreening.My.Resources.Resources.Modify_16_x_16
        Me.btnEdit.Location = New System.Drawing.Point(694, 4)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnEdit.Size = New System.Drawing.Size(110, 36)
        Me.btnEdit.TabIndex = 157
        Me.btnEdit.Text = "Edit [F3]"
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnAdd.DefaultScheme = False
        Me.btnAdd.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnAdd.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnAdd.Hint = "Add New Entry"
        Me.btnAdd.Image = Global.SickLeaveScreening.My.Resources.Resources.Create_16_x_16
        Me.btnAdd.Location = New System.Drawing.Point(580, 4)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnAdd.Size = New System.Drawing.Size(110, 36)
        Me.btnAdd.TabIndex = 156
        Me.btnAdd.Text = "Add (F2)"
        '
        'dgvList
        '
        Me.dgvList.AllowUserToAddRows = False
        Me.dgvList.AllowUserToDeleteRows = False
        Me.dgvList.AllowUserToResizeColumns = False
        Me.dgvList.AllowUserToResizeRows = False
        Me.dgvList.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Verdana", 8.5!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.dgvList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvList.ColumnHeadersHeight = 25
        Me.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColScreenId, Me.ColScreenDate, Me.ColEmployeeCode, Me.ColEmployeeName, Me.ColRemarks, Me.ColModifiedDate})
        Me.dgvList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvList.Location = New System.Drawing.Point(0, 36)
        Me.dgvList.MultiSelect = False
        Me.dgvList.Name = "dgvList"
        Me.dgvList.ReadOnly = True
        Me.dgvList.RowHeadersWidth = 40
        Me.dgvList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvList.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Verdana", 8.5!)
        Me.dgvList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvList.Size = New System.Drawing.Size(1039, 579)
        Me.dgvList.TabIndex = 2
        '
        'ColScreenId
        '
        Me.ColScreenId.DataPropertyName = "ScreenId"
        Me.ColScreenId.HeaderText = "ScreenId"
        Me.ColScreenId.Name = "ColScreenId"
        Me.ColScreenId.ReadOnly = True
        Me.ColScreenId.Visible = False
        '
        'ColScreenDate
        '
        Me.ColScreenDate.DataPropertyName = "ScreenDate"
        Me.ColScreenDate.HeaderText = "Date"
        Me.ColScreenDate.Name = "ColScreenDate"
        Me.ColScreenDate.ReadOnly = True
        Me.ColScreenDate.Width = 140
        '
        'ColEmployeeCode
        '
        Me.ColEmployeeCode.DataPropertyName = "EmployeeCode"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.ColEmployeeCode.DefaultCellStyle = DataGridViewCellStyle2
        Me.ColEmployeeCode.HeaderText = "Employee ID"
        Me.ColEmployeeCode.Name = "ColEmployeeCode"
        Me.ColEmployeeCode.ReadOnly = True
        '
        'ColEmployeeName
        '
        Me.ColEmployeeName.DataPropertyName = "EmployeeName"
        Me.ColEmployeeName.HeaderText = "Employee Name"
        Me.ColEmployeeName.Name = "ColEmployeeName"
        Me.ColEmployeeName.ReadOnly = True
        Me.ColEmployeeName.Width = 275
        '
        'ColRemarks
        '
        Me.ColRemarks.DataPropertyName = "Remarks"
        Me.ColRemarks.HeaderText = "Remarks"
        Me.ColRemarks.Name = "ColRemarks"
        Me.ColRemarks.ReadOnly = True
        Me.ColRemarks.Width = 325
        '
        'ColModifiedDate
        '
        Me.ColModifiedDate.DataPropertyName = "ModifiedDate"
        Me.ColModifiedDate.HeaderText = "Modified Date"
        Me.ColModifiedDate.Name = "ColModifiedDate"
        Me.ColModifiedDate.ReadOnly = True
        Me.ColModifiedDate.Width = 140
        '
        'frmScreenList
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1039, 661)
        Me.Controls.Add(Me.dgvList)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.pnlTop)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Verdana", 8.5!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmScreenList"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Health Screening List"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        CType(Me.bindingNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.bindingNavigator.ResumeLayout(False)
        Me.bindingNavigator.PerformLayout()
        Me.pnlBottom.ResumeLayout(False)
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents dgvList As System.Windows.Forms.DataGridView
    Friend WithEvents bindingNavigator As System.Windows.Forms.BindingNavigator
    Friend WithEvents txtTotalPageNumber As System.Windows.Forms.ToolStripLabel
    Friend WithEvents BindingNavigatorMoveFirstItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents txtPageNumber As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnGo As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblStartFrom As System.Windows.Forms.Label
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents btnSearchDate As System.Windows.Forms.Button
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblStartTo As System.Windows.Forms.Label
    Friend WithEvents btnClose As PinkieControls.ButtonXP
    Friend WithEvents btnDelete As PinkieControls.ButtonXP
    Friend WithEvents btnEdit As PinkieControls.ButtonXP
    Friend WithEvents btnAdd As PinkieControls.ButtonXP
    Friend WithEvents btnRefresh As PinkieControls.ButtonXP
    Friend WithEvents ColScreenId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColScreenDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColEmployeeCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColEmployeeName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColRemarks As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColModifiedDate As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
