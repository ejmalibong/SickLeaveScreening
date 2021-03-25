<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmScreenEntry
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
        Me.txtEmployeeScanId = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.lblEmployeeScanId = New System.Windows.Forms.Label()
        Me.lblRemarks = New System.Windows.Forms.Label()
        Me.btnSave = New PinkieControls.ButtonXP()
        Me.btnClose = New PinkieControls.ButtonXP()
        Me.btnClear = New PinkieControls.ButtonXP()
        Me.lblEmployeeName = New System.Windows.Forms.Label()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.txtEmployeeName = New System.Windows.Forms.Label()
        Me.txtDate = New System.Windows.Forms.Label()
        Me.lblClinicClearance = New System.Windows.Forms.Label()
        Me.txtEmployeeId = New System.Windows.Forms.Label()
        Me.lblEmployeeId = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txtEmployeeScanId
        '
        Me.txtEmployeeScanId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmployeeScanId.Font = New System.Drawing.Font("Verdana", 20.0!)
        Me.txtEmployeeScanId.Location = New System.Drawing.Point(50, 97)
        Me.txtEmployeeScanId.Name = "txtEmployeeScanId"
        Me.txtEmployeeScanId.ShortcutsEnabled = False
        Me.txtEmployeeScanId.Size = New System.Drawing.Size(380, 40)
        Me.txtEmployeeScanId.TabIndex = 0
        Me.txtEmployeeScanId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtRemarks
        '
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.txtRemarks.Location = New System.Drawing.Point(50, 256)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(380, 85)
        Me.txtRemarks.TabIndex = 1
        '
        'lblEmployeeScanId
        '
        Me.lblEmployeeScanId.BackColor = System.Drawing.SystemColors.Control
        Me.lblEmployeeScanId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblEmployeeScanId.ForeColor = System.Drawing.Color.Black
        Me.lblEmployeeScanId.Location = New System.Drawing.Point(50, 74)
        Me.lblEmployeeScanId.Name = "lblEmployeeScanId"
        Me.lblEmployeeScanId.Size = New System.Drawing.Size(380, 24)
        Me.lblEmployeeScanId.TabIndex = 504
        Me.lblEmployeeScanId.Text = " Employee ID"
        Me.lblEmployeeScanId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRemarks
        '
        Me.lblRemarks.BackColor = System.Drawing.SystemColors.Control
        Me.lblRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRemarks.ForeColor = System.Drawing.Color.Black
        Me.lblRemarks.Location = New System.Drawing.Point(50, 233)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(380, 24)
        Me.lblRemarks.TabIndex = 505
        Me.lblRemarks.Text = " Remarks"
        Me.lblRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnSave.DefaultScheme = False
        Me.btnSave.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnSave.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnSave.Hint = ""
        Me.btnSave.Location = New System.Drawing.Point(118, 352)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnSave.Size = New System.Drawing.Size(100, 35)
        Me.btnSave.TabIndex = 2
        Me.btnSave.TabStop = False
        Me.btnSave.Text = "Save"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnClose.DefaultScheme = False
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnClose.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnClose.Hint = ""
        Me.btnClose.Location = New System.Drawing.Point(330, 352)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnClose.Size = New System.Drawing.Size(100, 35)
        Me.btnClose.TabIndex = 4
        Me.btnClose.TabStop = False
        Me.btnClose.Text = "Close"
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnClear.DefaultScheme = False
        Me.btnClear.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnClear.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnClear.Hint = ""
        Me.btnClear.Location = New System.Drawing.Point(224, 352)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnClear.Size = New System.Drawing.Size(100, 35)
        Me.btnClear.TabIndex = 3
        Me.btnClear.TabStop = False
        Me.btnClear.Text = "Clear"
        '
        'lblEmployeeName
        '
        Me.lblEmployeeName.BackColor = System.Drawing.SystemColors.Control
        Me.lblEmployeeName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblEmployeeName.ForeColor = System.Drawing.Color.Black
        Me.lblEmployeeName.Location = New System.Drawing.Point(50, 181)
        Me.lblEmployeeName.Name = "lblEmployeeName"
        Me.lblEmployeeName.Size = New System.Drawing.Size(68, 24)
        Me.lblEmployeeName.TabIndex = 509
        Me.lblEmployeeName.Text = " Name"
        Me.lblEmployeeName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDate
        '
        Me.lblDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDate.ForeColor = System.Drawing.Color.Black
        Me.lblDate.Location = New System.Drawing.Point(50, 207)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(68, 24)
        Me.lblDate.TabIndex = 510
        Me.lblDate.Text = " Date"
        Me.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtEmployeeName
        '
        Me.txtEmployeeName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtEmployeeName.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtEmployeeName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmployeeName.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.txtEmployeeName.ForeColor = System.Drawing.Color.Black
        Me.txtEmployeeName.Location = New System.Drawing.Point(117, 181)
        Me.txtEmployeeName.Name = "txtEmployeeName"
        Me.txtEmployeeName.Size = New System.Drawing.Size(313, 24)
        Me.txtEmployeeName.TabIndex = 511
        Me.txtEmployeeName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtEmployeeName.UseCompatibleTextRendering = True
        '
        'txtDate
        '
        Me.txtDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDate.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDate.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.txtDate.ForeColor = System.Drawing.Color.Black
        Me.txtDate.Location = New System.Drawing.Point(117, 207)
        Me.txtDate.Name = "txtDate"
        Me.txtDate.Size = New System.Drawing.Size(313, 24)
        Me.txtDate.TabIndex = 512
        Me.txtDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtDate.UseCompatibleTextRendering = True
        '
        'lblClinicClearance
        '
        Me.lblClinicClearance.AutoSize = True
        Me.lblClinicClearance.Font = New System.Drawing.Font("Verdana", 20.0!, System.Drawing.FontStyle.Bold)
        Me.lblClinicClearance.Location = New System.Drawing.Point(93, 31)
        Me.lblClinicClearance.Name = "lblClinicClearance"
        Me.lblClinicClearance.Size = New System.Drawing.Size(299, 32)
        Me.lblClinicClearance.TabIndex = 501
        Me.lblClinicClearance.Text = "Doctor's Clearance"
        '
        'txtEmployeeId
        '
        Me.txtEmployeeId.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtEmployeeId.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtEmployeeId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmployeeId.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.txtEmployeeId.ForeColor = System.Drawing.Color.Black
        Me.txtEmployeeId.Location = New System.Drawing.Point(117, 155)
        Me.txtEmployeeId.Name = "txtEmployeeId"
        Me.txtEmployeeId.Size = New System.Drawing.Size(313, 24)
        Me.txtEmployeeId.TabIndex = 514
        Me.txtEmployeeId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtEmployeeId.UseCompatibleTextRendering = True
        '
        'lblEmployeeId
        '
        Me.lblEmployeeId.BackColor = System.Drawing.SystemColors.Control
        Me.lblEmployeeId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblEmployeeId.ForeColor = System.Drawing.Color.Black
        Me.lblEmployeeId.Location = New System.Drawing.Point(50, 155)
        Me.lblEmployeeId.Name = "lblEmployeeId"
        Me.lblEmployeeId.Size = New System.Drawing.Size(68, 24)
        Me.lblEmployeeId.TabIndex = 513
        Me.lblEmployeeId.Text = " ID No"
        Me.lblEmployeeId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmScreenEntry
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(480, 396)
        Me.Controls.Add(Me.txtEmployeeScanId)
        Me.Controls.Add(Me.txtEmployeeId)
        Me.Controls.Add(Me.lblEmployeeId)
        Me.Controls.Add(Me.txtDate)
        Me.Controls.Add(Me.txtEmployeeName)
        Me.Controls.Add(Me.lblDate)
        Me.Controls.Add(Me.lblEmployeeName)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.lblRemarks)
        Me.Controls.Add(Me.lblEmployeeScanId)
        Me.Controls.Add(Me.txtRemarks)
        Me.Controls.Add(Me.lblClinicClearance)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Verdana", 8.5!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmScreenEntry"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Data Entry"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtEmployeeScanId As System.Windows.Forms.TextBox
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents lblEmployeeScanId As System.Windows.Forms.Label
    Friend WithEvents lblRemarks As System.Windows.Forms.Label
    Friend WithEvents btnSave As PinkieControls.ButtonXP
    Friend WithEvents btnClose As PinkieControls.ButtonXP
    Friend WithEvents btnClear As PinkieControls.ButtonXP
    Friend WithEvents lblEmployeeName As System.Windows.Forms.Label
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents txtEmployeeName As System.Windows.Forms.Label
    Friend WithEvents txtDate As System.Windows.Forms.Label
    Friend WithEvents lblClinicClearance As System.Windows.Forms.Label
    Friend WithEvents txtEmployeeId As System.Windows.Forms.Label
    Friend WithEvents lblEmployeeId As System.Windows.Forms.Label

End Class
