Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports BlackCoffeeLibrary.BlackCoffee
Imports LeaveFilingSystem
Imports LeaveFilingSystem.dsLeaveFiling
Imports LeaveFilingSystem.dsLeaveFilingTableAdapters

Public Class frmScreenEntry
    Private connection As New clsConnection
    Private dbLeaveFiling As New SqlDbMethod(connection.LeaveFiling)
    Private dbJeonsoft As New SqlDbMethod(connection.Jeonsoft)
    Private main As New Main

    'server datetime
    Private serverDate As DateTime = dbLeaveFiling.GetServerDate

    'dataset
    Private dsLeaveFiling As New dsLeaveFiling

    Private adpScreening As New ScreeningTableAdapter
    Private dtScreening As New ScreeningDataTable
    Private bsScreening As New BindingSource

    'custom bindings
    Private WithEvents screenDate As Binding

    Private WithEvents absentFrom As Binding
    Private WithEvents absentTo As Binding

    'constructor
    Private screenBy As String = String.Empty 'doctor/nurse
    Private screenId As Integer = 0

    'others
    Private employeeId As Integer = 0 'employee to be screen
    Private arrSplitted() As String

    Public Sub New(ByVal _screenBy As String, Optional ByVal _screenId As Integer = 0)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        screenBy = _screenBy
        screenId = _screenId
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not screenId = 0 Then
            Me.adpScreening.FillByScreenId(Me.dsLeaveFiling.Screening, screenId)
            Me.bsScreening.DataSource = Me.dsLeaveFiling
            Me.bsScreening.DataMember = dtScreening.TableName
            Me.bsScreening.Position = Me.bsScreening.Find("ScreenId", screenId)

            txtEmployeeScanId.Enabled = False
            If CType(Me.bsScreening.Current, DataRowView).Item("EmployeeId") Is DBNull.Value Then
                employeeId = 0
            Else
                employeeId = CType(Me.bsScreening.Current, DataRowView).Item("EmployeeId")
            End If
            txtEmployeeCode.DataBindings.Add(New Binding("Text", Me.bsScreening.Current, "EmployeeCode"))
            txtEmployeeName.DataBindings.Add(New Binding("Text", Me.bsScreening.Current, "EmployeeName"))
            screenDate = New Binding("Text", Me.bsScreening.Current, "ScreenDate")
            txtDate.DataBindings.Add(screenDate)
            absentFrom = New Binding("Text", Me.bsScreening.Current, "AbsentFrom")
            txtAbsentFrom.DataBindings.Add(absentFrom)
            absentTo = New Binding("Text", Me.bsScreening.Current, "AbsentTo")
            txtAbsentTo.DataBindings.Add(absentTo)
            txtReason.DataBindings.Add(New Binding("Text", Me.bsScreening.Current, "Reason"))
            txtDiagnosis.DataBindings.Add(New Binding("Text", Me.bsScreening.Current, "Diagnosis"))
            If CType(Me.bsScreening.Current, DataRowView).Item("IsFitToWork") = True Then
                chkNotFtw.Checked = False
            Else
                chkNotFtw.Checked = True
            End If
            If CType(Me.bsScreening.Current, DataRowView).Item("LeaveTypeId") = 1 Then
                chkSetToVl.Checked = False
            Else
                chkSetToVl.Checked = True
            End If
            If txtEmployeeCode.Text.Trim.Substring(0, 3).ToUpper.Trim.Equals("FMB") Then
                txtEmployeeName.ReadOnly = False
            Else
                txtEmployeeName.ReadOnly = True
            End If
            Me.ActiveControl = txtDiagnosis
            txtDiagnosis.Select(txtDiagnosis.Text.Trim.Length, 0)
        Else
            ResetForm()
        End If
    End Sub

    Private Sub frmMain_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                e.Handled = True
                btnClear.PerformClick()

                screenId = 0
                employeeId = 0
                txtEmployeeCode.Text = ""
                txtEmployeeName.Clear()
                txtDate.Text = ""
                txtAbsentFrom.Text = String.Format("{0:MM/dd/yyyy}", CheckDate(serverDate))
                txtAbsentFrom.ValidatingType = GetType(System.DateTime)
                txtAbsentTo.ReadOnly = True
                txtAbsentTo.Text = String.Format("{0:MM/dd/yyyy}", CheckDate(serverDate))
                txtAbsentTo.ValidatingType = GetType(System.DateTime)
                txtReason.Clear()
            Case Keys.F10
                e.Handled = True
                btnSave.PerformClick()
            Case Keys.F11
                e.Handled = True
                NotFitToWork()
            Case Keys.F12
                e.Handled = True
                SetToVl()
            Case Keys.Enter
                Me.SelectNextControl(Me.ActiveControl, True, True, True, True)
        End Select
    End Sub

    Private Sub txtEmployeeScanId_KeyDown(sender As Object, e As KeyEventArgs) Handles txtEmployeeScanId.KeyDown
        If e.KeyCode.Equals(Keys.Enter) Then
            e.Handled = True
            If String.IsNullOrEmpty(txtEmployeeScanId.Text.Trim) Then
                Me.ActiveControl = txtEmployeeScanId
                MessageBox.Show("Please scan your ID.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
            End If

            arrSplitted = Split(txtEmployeeScanId.Text.Trim, " ", 2)
            ValidateId(arrSplitted(0).ToString)
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If String.IsNullOrEmpty(txtEmployeeScanId.Text.Trim) AndAlso String.IsNullOrEmpty(txtEmployeeCode.Text.Trim) Then
                Me.ActiveControl = txtEmployeeScanId
                MessageBox.Show("Please scan your ID.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            If String.IsNullOrEmpty(txtReason.Text.Trim) Then
                MessageBox.Show("Remarks cannot be empty.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.ActiveControl = txtReason
                Return
            End If

            If String.IsNullOrEmpty(txtDiagnosis.Text.Trim) Then
                MessageBox.Show("Diagnosis cannot be empty.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.ActiveControl = txtDiagnosis
                Return
            End If

            If CDate(txtAbsentFrom.Text).Date > CDate(txtAbsentTo.Text).Date Then
                MessageBox.Show("Start date is later than end date.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.ActiveControl = txtAbsentFrom
                Return
            End If

            If screenId = 0 Then
                Dim _newScreeningRow As ScreeningRow = Me.dsLeaveFiling.Screening.NewScreeningRow

                With _newScreeningRow
                    .ScreenDate = serverDate
                    .ScreenBy = screenBy
                    If employeeId = 0 Then
                        .SetEmployeeIdNull()
                    Else
                        .EmployeeId = employeeId
                    End If
                    .EmployeeCode = txtEmployeeCode.Text.Trim
                    .EmployeeName = txtEmployeeName.Text.Trim
                    .AbsentFrom = CDate(txtAbsentFrom.Text)
                    .AbsentTo = CDate(txtAbsentTo.Text)
                    .Quantity = GetTotalDays(CDate(txtAbsentFrom.Text), CDate(txtAbsentTo.Text))
                    If chkSetToVl.CheckState = CheckState.Checked Then
                        .LeaveTypeId = 2
                    Else
                        .LeaveTypeId = 1
                    End If
                    .Reason = txtReason.Text.Trim
                    .Diagnosis = txtDiagnosis.Text.Trim
                    If chkNotFtw.CheckState = CheckState.Checked Then
                        .IsFitToWork = False
                    Else
                        .IsFitToWork = True
                    End If
                    .IsUsed = False
                    .ModifiedBy = screenBy
                    .ModifiedDate = serverDate
                End With
                Me.dsLeaveFiling.Screening.AddScreeningRow(_newScreeningRow)

                If Me.dsLeaveFiling.HasChanges Then
                    Me.adpScreening.Update(Me.dsLeaveFiling.Screening)
                    Me.dsLeaveFiling.AcceptChanges()
                    Dim _frmScreenList As frmScreenList = TryCast(Me.Owner, frmScreenList)
                    _frmScreenList.RefreshValues()
                    ResetForm()
                End If
            Else
                Dim _rowScreening As ScreeningRow = Me.dsLeaveFiling.Screening.FindByScreenId(screenId)

                With _rowScreening
                    .ScreenBy = screenBy
                    If employeeId = 0 Then
                        .SetEmployeeIdNull()
                    Else
                        .EmployeeId = employeeId
                    End If
                    .EmployeeCode = txtEmployeeCode.Text.Trim
                    .EmployeeName = txtEmployeeName.Text.Trim
                    .AbsentFrom = CDate(txtAbsentFrom.Text)
                    .AbsentTo = CDate(txtAbsentTo.Text)
                    .Quantity = GetTotalDays(CDate(txtAbsentFrom.Text), CDate(txtAbsentTo.Text))
                    If chkSetToVl.CheckState = CheckState.Checked Then
                        .LeaveTypeId = 2
                    Else
                        .LeaveTypeId = 1
                    End If
                    .Reason = txtReason.Text.Trim
                    .Diagnosis = txtDiagnosis.Text.Trim
                    If chkNotFtw.CheckState = CheckState.Checked Then
                        .IsFitToWork = False
                    Else
                        .IsFitToWork = True
                    End If
                    .ModifiedBy = screenBy
                    .ModifiedDate = serverDate
                End With
                Me.Validate()
                Me.bsScreening.EndEdit()

                If Me.dsLeaveFiling.HasChanges Then
                    Me.adpScreening.Update(Me.dsLeaveFiling.Screening)
                    Me.dsLeaveFiling.AcceptChanges()
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If Not screenId = 0 Then
                If MessageBox.Show("Delete this record?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                    Me.bsScreening.RemoveCurrent()
                End If
                If Me.dsLeaveFiling.HasChanges Then
                    Me.adpScreening.Update(Me.dsLeaveFiling.Screening)
                    Me.dsLeaveFiling.AcceptChanges()
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ResetForm()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub dateBinding_Format(sender As Object, e As ConvertEventArgs) Handles screenDate.Format
        If Not e.Value Is DBNull.Value Then
            e.Value = Format(e.Value, "MMMM dd, yyyy  HH:mm")
        Else
            e.Value = DateTime.Now.ToString("MMMM dd, yyyy  HH:mm")
        End If
    End Sub

    Private Sub maskedFrom_Format(sender As Object, e As ConvertEventArgs) Handles absentFrom.Format
        e.Value = Format(e.Value, "MM/dd/yyyy")
    End Sub

    Private Sub maskedTo_Format(sender As Object, e As ConvertEventArgs) Handles absentTo.Format
        e.Value = Format(e.Value, "MM/dd/yyyy")
    End Sub

    Private Sub txtEmployeeName_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtEmployeeName.Validating
        If String.IsNullOrEmpty(txtEmployeeName.Text.Trim) Then
            MessageBox.Show("Employee name is required.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            e.Cancel = True
        End If
    End Sub

    'validate input from scanner or manual type
    Private Sub ValidateId(ByVal _employeeCode As String)
        Try
            Dim _count As Integer = 0
            Dim _prmCount(0) As SqlParameter
            _prmCount(0) = New SqlParameter("@EmployeeCode", SqlDbType.VarChar)
            _prmCount(0).Value = _employeeCode
            _count = dbJeonsoft.ExecuteScalar("SELECT Count(Id) FROM viwGroupEmployees WHERE EmployeeCode = @EmployeeCode AND Active = 1", CommandType.Text, _prmCount)

            If _count > 0 Then
                Dim _prmReader(0) As SqlParameter
                _prmReader(0) = New SqlParameter("@EmployeeCode", SqlDbType.VarChar)
                _prmReader(0).Value = _employeeCode
                Dim _reader As IDataReader = dbLeaveFiling.ExecuteReader("RdEmployee", CommandType.StoredProcedure, _prmReader)

                While _reader.Read
                    employeeId = _reader.Item("Id").ToString
                    txtEmployeeCode.Text = _reader.Item("EmployeeCode").ToString.Trim
                    txtEmployeeName.Text = _reader("EmployeeName").ToString.Trim
                End While
                _reader.Close()

                txtEmployeeScanId.Clear()
                txtEmployeeScanId.Enabled = False
                txtEmployeeName.Enabled = True
                txtEmployeeName.ReadOnly = True
                txtDate.Text = Format(serverDate, "MMMM dd, yyyy HH:mm")
                txtAbsentFrom.Enabled = True
                txtAbsentFrom.ReadOnly = False
                txtAbsentTo.Enabled = True
                txtAbsentTo.ReadOnly = False
                txtReason.Enabled = True
                txtReason.ReadOnly = False
                txtDiagnosis.Enabled = True
                txtDiagnosis.ReadOnly = False
                chkNotFtw.Enabled = True
                chkSetToVl.Enabled = True
                txtReason.Focus()
            Else
                If _employeeCode.Substring(0, 3).ToUpper.Trim.Equals("FMB") Then
                    employeeId = 0
                    txtEmployeeScanId.Clear()
                    txtEmployeeScanId.Enabled = False
                    txtEmployeeCode.Text = _employeeCode
                    txtEmployeeCode.Text = StrConv(txtEmployeeCode.Text.Trim, VbStrConv.Uppercase)
                    txtEmployeeName.Enabled = True
                    txtEmployeeName.ReadOnly = False
                    txtDate.Text = Format(serverDate, "MMMM dd, yyyy HH:mm")
                    txtAbsentFrom.Enabled = True
                    txtAbsentFrom.ReadOnly = False
                    txtAbsentTo.Enabled = True
                    txtAbsentTo.ReadOnly = False
                    txtReason.Enabled = True
                    txtReason.ReadOnly = False
                    txtDiagnosis.Enabled = True
                    txtDiagnosis.ReadOnly = False
                    chkNotFtw.Enabled = True
                    chkSetToVl.Enabled = True
                    txtEmployeeName.Focus()
                Else
                    MessageBox.Show("Employee not found.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    txtEmployeeScanId.Focus()
                    Return
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ResetForm()
        screenId = 0
        employeeId = 0
        txtEmployeeScanId.Enabled = True
        txtEmployeeScanId.Clear()
        txtEmployeeCode.Text = ""
        txtEmployeeName.Clear()
        txtEmployeeName.Enabled = False
        txtDate.Text = ""
        txtAbsentFrom.Enabled = False
        txtAbsentFrom.Text = String.Format("{0:MM/dd/yyyy}", CheckDate(serverDate))
        txtAbsentFrom.ValidatingType = GetType(System.DateTime)
        txtAbsentTo.Enabled = False
        txtAbsentTo.Text = String.Format("{0:MM/dd/yyyy}", CheckDate(serverDate))
        txtAbsentTo.ValidatingType = GetType(System.DateTime)
        txtReason.Clear()
        txtReason.Enabled = False
        txtDiagnosis.Clear()
        txtDiagnosis.Enabled = False
        chkNotFtw.Enabled = False
        chkNotFtw.CheckState = CheckState.Unchecked
        chkSetToVl.Enabled = False
        chkSetToVl.CheckState = CheckState.Unchecked
        txtEmployeeScanId.Focus()
    End Sub

    'set the default value of absent date to last working day - excluding sunday, company holidays and legal holidays
    Private Function CheckDate(ByVal _date As DateTime) As Date
        Try
            _date = _date.AddDays(-1)
            While IsHoliday(_date) Or IsWeekend(_date)
                _date = _date.AddDays(-1)
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return _date
    End Function

    Private Function IsWeekend(ByVal _date As Date) As Boolean
        If _date.DayOfWeek.Equals(DayOfWeek.Sunday) Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function IsHoliday(ByVal _date As Date) As Boolean
        Dim _count As Integer

        Try
            Dim _prmHoliday(0) As SqlParameter
            _prmHoliday(0) = New SqlParameter("@HolidayDate", SqlDbType.Date)
            _prmHoliday(0).Value = _date.ToShortDateString
            _count = 0
            _count = dbLeaveFiling.ExecuteScalar("SELECT COUNT(HolidayId) FROM dbo.Holiday WHERE HolidayDate = @HolidayDate", CommandType.Text, _prmHoliday)
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        If _count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    'get total number of days from start date up to end date - excluding holidays and sundays
    Private Function GetTotalDays(ByVal _startDate As Date, ByVal _endDate As Date) As Integer
        Dim _count As Integer = 0

        Try
            If _startDate.Date.Equals(_endDate.Date) Then
                _count = 1
            Else
                For _i As Integer = 0 To (_endDate - _startDate).Days
                    If Not IsHoliday(_startDate) Then
                        If Not IsWeekend(_startDate) Then
                            _count += 1
                        End If
                    End If
                    _startDate = _startDate.AddDays(1)
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return _count
    End Function

    'save and close data entry form with not fit to work as true
    Private Sub NotFitToWork()
        If String.IsNullOrEmpty(txtEmployeeScanId.Text.Trim) AndAlso String.IsNullOrEmpty(txtEmployeeCode.Text.Trim) Then
            txtEmployeeScanId.Select()
            MessageBox.Show("Please scan your ID.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        If String.IsNullOrEmpty(txtReason.Text.Trim) Then
            MessageBox.Show("Remarks cannot be empty.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        If CDate(txtAbsentFrom.Text).Date > CDate(txtAbsentTo.Text).Date Then
            MessageBox.Show("Start date is later than end date.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If screenId = 0 Then
            Dim _newScreeningRow As ScreeningRow = Me.dsLeaveFiling.Screening.NewScreeningRow

            With _newScreeningRow
                .ScreenDate = serverDate
                .ScreenBy = screenBy
                If employeeId = 0 Then
                    .SetEmployeeIdNull()
                Else
                    .EmployeeId = employeeId
                End If
                .EmployeeCode = txtEmployeeCode.Text.Trim
                .EmployeeName = txtEmployeeName.Text.Trim
                .AbsentFrom = CDate(txtAbsentFrom.Text)
                .AbsentTo = CDate(txtAbsentTo.Text)
                .Quantity = GetTotalDays(CDate(txtAbsentFrom.Text), CDate(txtAbsentTo.Text))
                If chkSetToVl.CheckState = CheckState.Checked Then
                    .LeaveTypeId = 2
                Else
                    .LeaveTypeId = 1
                End If
                .Reason = txtReason.Text.Trim
                .Diagnosis = txtDiagnosis.Text.Trim
                .IsFitToWork = False
                .IsUsed = False
                .ModifiedBy = screenBy
                .ModifiedDate = serverDate
            End With
            Me.dsLeaveFiling.Screening.AddScreeningRow(_newScreeningRow)

            If Me.dsLeaveFiling.HasChanges Then
                Me.adpScreening.Update(Me.dsLeaveFiling.Screening)
                Me.dsLeaveFiling.AcceptChanges()
                Dim _frmScreenList As frmScreenList = TryCast(Me.Owner, frmScreenList)
                _frmScreenList.RefreshValues()
                ResetForm()
            End If
        Else
            Dim _rowScreening As ScreeningRow = Me.dsLeaveFiling.Screening.FindByScreenId(screenId)

            With _rowScreening
                .ScreenBy = screenBy
                If employeeId = 0 Then
                    .SetEmployeeIdNull()
                Else
                    .EmployeeId = employeeId
                End If
                .EmployeeCode = txtEmployeeCode.Text.Trim
                .EmployeeName = txtEmployeeName.Text.Trim
                .AbsentFrom = CDate(txtAbsentFrom.Text)
                .AbsentTo = CDate(txtAbsentTo.Text)
                .Quantity = GetTotalDays(CDate(txtAbsentFrom.Text), CDate(txtAbsentTo.Text))
                If chkSetToVl.CheckState = CheckState.Checked Then
                    .LeaveTypeId = 2
                Else
                    .LeaveTypeId = 1
                End If
                .Reason = txtReason.Text.Trim
                .Diagnosis = txtDiagnosis.Text.Trim
                .IsFitToWork = False
                .ModifiedBy = screenBy
                .ModifiedDate = serverDate
            End With
            Me.Validate()
            Me.bsScreening.EndEdit()

            If Me.dsLeaveFiling.HasChanges Then
                Me.adpScreening.Update(Me.dsLeaveFiling.Screening)
                Me.dsLeaveFiling.AcceptChanges()
                Me.DialogResult = Windows.Forms.DialogResult.OK
            End If
        End If
    End Sub

    'save and close data entry form with leave type as vacation leave
    Private Sub SetToVl()
        If String.IsNullOrEmpty(txtEmployeeScanId.Text.Trim) AndAlso String.IsNullOrEmpty(txtEmployeeCode.Text.Trim) Then
            txtEmployeeScanId.Select()
            MessageBox.Show("Please scan your ID.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        If String.IsNullOrEmpty(txtReason.Text.Trim) Then
            MessageBox.Show("Remarks cannot be empty.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        If CDate(txtAbsentFrom.Text).Date > CDate(txtAbsentTo.Text).Date Then
            MessageBox.Show("Start date is later than end date.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If screenId = 0 Then
            Dim _newScreeningRow As ScreeningRow = Me.dsLeaveFiling.Screening.NewScreeningRow

            With _newScreeningRow
                .ScreenDate = serverDate
                .ScreenBy = screenBy
                If employeeId = 0 Then
                    .SetEmployeeIdNull()
                Else
                    .EmployeeId = employeeId
                End If
                .EmployeeCode = txtEmployeeCode.Text.Trim
                .EmployeeName = txtEmployeeName.Text.Trim
                .AbsentFrom = CDate(txtAbsentFrom.Text)
                .AbsentTo = CDate(txtAbsentTo.Text)
                .Quantity = GetTotalDays(CDate(txtAbsentFrom.Text), CDate(txtAbsentTo.Text))
                .LeaveTypeId = 2
                .Reason = txtReason.Text.Trim
                .Diagnosis = txtDiagnosis.Text.Trim
                If chkNotFtw.CheckState = CheckState.Checked Then
                    .IsFitToWork = False
                Else
                    .IsFitToWork = True
                End If
                .IsUsed = False
                .ModifiedBy = screenBy
                .ModifiedDate = serverDate
            End With
            Me.dsLeaveFiling.Screening.AddScreeningRow(_newScreeningRow)

            If Me.dsLeaveFiling.HasChanges Then
                Me.adpScreening.Update(Me.dsLeaveFiling.Screening)
                Me.dsLeaveFiling.AcceptChanges()
                Dim _frmScreenList As frmScreenList = TryCast(Me.Owner, frmScreenList)
                _frmScreenList.RefreshValues()
                ResetForm()
            End If
        Else
            Dim _rowScreening As ScreeningRow = Me.dsLeaveFiling.Screening.FindByScreenId(screenId)

            With _rowScreening
                .ScreenBy = screenBy
                If employeeId = 0 Then
                    .SetEmployeeIdNull()
                Else
                    .EmployeeId = employeeId
                End If
                .EmployeeCode = txtEmployeeCode.Text.Trim
                .EmployeeName = txtEmployeeName.Text.Trim
                .AbsentFrom = CDate(txtAbsentFrom.Text)
                .AbsentTo = CDate(txtAbsentTo.Text)
                .Quantity = GetTotalDays(CDate(txtAbsentFrom.Text), CDate(txtAbsentTo.Text))
                .LeaveTypeId = 2
                .Reason = txtReason.Text.Trim
                .Diagnosis = txtDiagnosis.Text.Trim
                If chkNotFtw.CheckState = CheckState.Checked Then
                    .IsFitToWork = False
                Else
                    .IsFitToWork = True
                End If
                .ModifiedBy = screenBy
                .ModifiedDate = serverDate
            End With
            Me.Validate()
            Me.bsScreening.EndEdit()

            If Me.dsLeaveFiling.HasChanges Then
                Me.adpScreening.Update(Me.dsLeaveFiling.Screening)
                Me.dsLeaveFiling.AcceptChanges()
                Me.DialogResult = Windows.Forms.DialogResult.OK
            End If
        End If
    End Sub

    'validates input from masked textbox - it should be in MM/dd/yyyy format
    Private Sub txtAbsentFrom_TypeValidationCompleted(sender As Object, e As TypeValidationEventArgs) Handles txtAbsentFrom.TypeValidationCompleted
        If (Not e.IsValidInput) Then
            SendKeys.Send("{End}")
            MessageBox.Show("Please input date in Month/Day/Year format.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            e.Cancel = True
        End If
    End Sub

    Private Sub txtAbsentTo_TypeValidationCompleted(sender As Object, e As TypeValidationEventArgs) Handles txtAbsentTo.TypeValidationCompleted
        If (Not e.IsValidInput) Then
            SendKeys.Send("{End}")
            MessageBox.Show("Please input date in Month/Day/Year format.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            e.Cancel = True
        End If
    End Sub

    Private Sub lblNotFtw_Click(sender As Object, e As EventArgs) Handles lblNotFtw.Click
        If chkNotFtw.Enabled = True Then
            If chkNotFtw.CheckState = CheckState.Checked Then
                chkNotFtw.Checked = False
            Else
                chkNotFtw.Checked = True
            End If
        End If
    End Sub

    Private Sub lblSetToVl_Click(sender As Object, e As EventArgs) Handles lblSetToVl.Click
        If chkSetToVl.Enabled = True Then
            If chkSetToVl.CheckState = CheckState.Checked Then
                chkSetToVl.Checked = False
            Else
                chkSetToVl.Checked = True
            End If
        End If
    End Sub

    Private Sub txtEmployeeScanId_Enter(sender As Object, e As EventArgs) Handles txtEmployeeScanId.Enter
        lblEmployeeScanId.ForeColor = Color.White
        lblEmployeeScanId.BackColor = Color.DarkSlateGray
    End Sub

    Private Sub txtEmployeeScanId_Leave(sender As Object, e As EventArgs) Handles txtEmployeeScanId.Leave
        lblEmployeeScanId.ForeColor = Color.Black
        lblEmployeeScanId.BackColor = SystemColors.Control
    End Sub

    Private Sub txtEmployeeName_Enter(sender As Object, e As EventArgs) Handles txtEmployeeName.Enter
        lblEmployeeName.ForeColor = Color.White
        lblEmployeeName.BackColor = Color.DarkSlateGray
    End Sub

    Private Sub txtEmployeeName_Leave(sender As Object, e As EventArgs) Handles txtEmployeeName.Leave
        lblEmployeeName.ForeColor = Color.Black
        lblEmployeeName.BackColor = SystemColors.Control
    End Sub

    Private Sub txtAbsentFrom_Enter(sender As Object, e As EventArgs) Handles txtAbsentFrom.Enter
        lblAbsentFrom.ForeColor = Color.White
        lblAbsentFrom.BackColor = Color.DarkSlateGray
    End Sub

    Private Sub txtAbsentFrom_Leave(sender As Object, e As EventArgs) Handles txtAbsentFrom.Leave
        lblAbsentFrom.ForeColor = Color.Black
        lblAbsentFrom.BackColor = SystemColors.Control
    End Sub

    Private Sub txtAbsentTo_Enter(sender As Object, e As EventArgs) Handles txtAbsentTo.Enter
        lblAbsentTo.ForeColor = Color.White
        lblAbsentTo.BackColor = Color.DarkSlateGray
    End Sub

    Private Sub txtAbsentTo_Leave(sender As Object, e As EventArgs) Handles txtAbsentTo.Leave
        lblAbsentTo.ForeColor = Color.Black
        lblAbsentTo.BackColor = SystemColors.Control
    End Sub

    Private Sub txtReason_Enter(sender As Object, e As EventArgs) Handles txtReason.Enter
        lblReason.ForeColor = Color.White
        lblReason.BackColor = Color.DarkSlateGray
    End Sub

    Private Sub txtReason_Leave(sender As Object, e As EventArgs) Handles txtReason.Leave
        lblReason.ForeColor = Color.Black
        lblReason.BackColor = SystemColors.Control
    End Sub

    Private Sub txtDiagnosis_Enter(sender As Object, e As EventArgs) Handles txtDiagnosis.Enter
        lblDiagnosis.ForeColor = Color.White
        lblDiagnosis.BackColor = Color.DarkSlateGray
    End Sub

    Private Sub txtDiagnosis_Leave(sender As Object, e As EventArgs) Handles txtDiagnosis.Leave
        lblDiagnosis.ForeColor = Color.Black
        lblDiagnosis.BackColor = SystemColors.Control
    End Sub

    Private Sub chkNotFtw_Enter(sender As Object, e As EventArgs) Handles chkNotFtw.Enter
        lblNotFtw.ForeColor = Color.White
        lblNotFtw.BackColor = Color.DarkSlateGray
    End Sub

    Private Sub chkNotFtw_Leave(sender As Object, e As EventArgs) Handles chkNotFtw.Leave
        lblNotFtw.ForeColor = Color.Black
        lblNotFtw.BackColor = SystemColors.Control
    End Sub

    Private Sub lblNotFtw_Enter(sender As Object, e As EventArgs) Handles lblNotFtw.Enter
        lblNotFtw.ForeColor = Color.White
        lblNotFtw.BackColor = Color.DarkSlateGray
    End Sub

    Private Sub lblNotFtw_Leave(sender As Object, e As EventArgs) Handles lblNotFtw.Leave
        lblNotFtw.ForeColor = Color.Black
        lblNotFtw.BackColor = SystemColors.Control
    End Sub

    Private Sub lblNotFtw_MouseEnter(sender As Object, e As EventArgs) Handles lblNotFtw.MouseEnter
        lblNotFtw.ForeColor = Color.White
        lblNotFtw.BackColor = Color.DarkSlateGray
    End Sub

    Private Sub lblNotFtw_MouseLeave(sender As Object, e As EventArgs) Handles lblNotFtw.MouseLeave
        lblNotFtw.ForeColor = Color.Black
        lblNotFtw.BackColor = SystemColors.Control
    End Sub

    Private Sub chkNotFtw_MouseEnter(sender As Object, e As EventArgs) Handles chkNotFtw.MouseEnter
        lblNotFtw.ForeColor = Color.White
        lblNotFtw.BackColor = Color.DarkSlateGray
    End Sub

    Private Sub chkNotFtw_MouseLeave(sender As Object, e As EventArgs) Handles chkNotFtw.MouseLeave
        lblNotFtw.ForeColor = Color.Black
        lblNotFtw.BackColor = SystemColors.Control
    End Sub

    Private Sub chkSetToVl_Enter(sender As Object, e As EventArgs) Handles chkSetToVl.Enter
        lblSetToVl.ForeColor = Color.White
        lblSetToVl.BackColor = Color.DarkSlateGray
    End Sub

    Private Sub chkSetToVl_Leave(sender As Object, e As EventArgs) Handles chkSetToVl.Leave
        lblSetToVl.ForeColor = Color.Black
        lblSetToVl.BackColor = SystemColors.Control
    End Sub

    Private Sub lblSetToVl_Enter(sender As Object, e As EventArgs) Handles lblSetToVl.Enter
        lblSetToVl.ForeColor = Color.White
        lblSetToVl.BackColor = Color.DarkSlateGray
    End Sub

    Private Sub lblSetToVl_Leave(sender As Object, e As EventArgs) Handles lblSetToVl.Leave
        lblSetToVl.ForeColor = Color.Black
        lblSetToVl.BackColor = SystemColors.Control
    End Sub

    Private Sub lblSetToVl_MouseEnter(sender As Object, e As EventArgs) Handles lblSetToVl.MouseEnter
        lblSetToVl.ForeColor = Color.White
        lblSetToVl.BackColor = Color.DarkSlateGray
    End Sub

    Private Sub lblSetToVl_MouseLeave(sender As Object, e As EventArgs) Handles lblSetToVl.MouseLeave
        lblSetToVl.ForeColor = Color.Black
        lblSetToVl.BackColor = SystemColors.Control
    End Sub

    Private Sub chkSetToVl_MouseEnter(sender As Object, e As EventArgs) Handles chkSetToVl.MouseEnter
        lblSetToVl.ForeColor = Color.White
        lblSetToVl.BackColor = Color.DarkSlateGray
    End Sub

    Private Sub chkSetToVl_MouseLeave(sender As Object, e As EventArgs) Handles chkSetToVl.MouseLeave
        lblSetToVl.ForeColor = Color.Black
        lblSetToVl.BackColor = SystemColors.Control
    End Sub

End Class