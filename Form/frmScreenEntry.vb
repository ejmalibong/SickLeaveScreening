Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports BlackCoffeeLibrary.BlackCoffee
Imports LeaveFilingSystem
Imports LeaveFilingSystem.dsLeaveFiling
Imports LeaveFilingSystem.dsLeaveFilingTableAdapters

Public Class frmScreenEntry
    Private connection As New clsConnection
    Private dbMethodLocal As New SqlDbMethod(connection.LocalConnection)
    Private dbMethodJeonsoft As New SqlDbMethod(connection.JeonsoftConnection)
    Private main As New Main

    Private screenId As Integer = 0
    Private employeeId As Integer = 0
    Private arrSplitted() As String

    Private indexScroll As Integer = 0
    Private indexPosition As Integer = 0

    Private dsLeaveFiling As New dsLeaveFiling
    Private dsJeonsoft As New dsJeonsoft

    Private myDataset As New dsLeaveFiling
    Private adpScreening As New ScreeningTableAdapter
    Private dtScreening As New ScreeningDataTable
    Private bsScreening As New BindingSource

    Private WithEvents dateBinding As Binding

    Dim keyIsDown As Boolean

    Public Sub New(Optional ByVal _screenId As Integer = 0)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        screenId = _screenId
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not screenId = 0 Then
            Me.adpScreening.FillByScreenId(Me.myDataset.Screening, screenId)
            Me.bsScreening.DataSource = Me.myDataset
            Me.bsScreening.DataMember = dtScreening.TableName
            Me.bsScreening.Position = Me.bsScreening.Find("ScreenId", screenId)

            txtEmployeeScanId.Enabled = False
            txtEmployeeId.DataBindings.Add(New Binding("Text", Me.bsScreening.Current, "EmployeeCode"))
            txtEmployeeName.DataBindings.Add(New Binding("Text", Me.bsScreening.Current, "EmployeeName"))
            dateBinding = New Binding("Text", Me.bsScreening.Current, "ScreenDate")
            txtDate.DataBindings.Add(dateBinding)
            txtRemarks.DataBindings.Add(New Binding("Text", Me.bsScreening.Current, "Remarks"))
            txtRemarks.Enabled = True
            txtRemarks.Select(txtRemarks.Text.Trim.Length, 0)
        Else
            txtRemarks.Enabled = False
        End If
    End Sub

    Private Sub frmMain_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode.Equals(Keys.F10) Then
            e.Handled = True
            btnSave.PerformClick()
        ElseIf e.KeyCode.Equals(Keys.Escape) Then
            e.Handled = True
            btnClear.PerformClick()
        End If
    End Sub

    Private Sub txtEmployeeScanId_KeyDown(sender As Object, e As KeyEventArgs) Handles txtEmployeeScanId.KeyDown
        If e.KeyCode.Equals(Keys.Enter) Then
            If String.IsNullOrEmpty(txtEmployeeScanId.Text.Trim) = True Then
                MessageBox.Show("Please tap your ID.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
            End If

            arrSplitted = Split(txtEmployeeScanId.Text.Trim, " ", 2)

            If Not arrSplitted.Length = 2 Then
                MessageBox.Show("Please tap your ID.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
            End If

            ValidateUser(arrSplitted(0).ToString, arrSplitted(1).ToString)
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If String.IsNullOrEmpty(txtEmployeeScanId.Text.Trim) AndAlso String.IsNullOrEmpty(txtEmployeeId.Text.Trim) Then
                MessageBox.Show("Please tap your ID.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
            End If

            If String.IsNullOrEmpty(txtRemarks.Text.Trim) Then
                MessageBox.Show("Remarks cannot be empty.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
            End If

            If screenId = 0 Then
                Dim _newRowScreening As ScreeningRow = Me.myDataset.Screening.NewScreeningRow

                With _newRowScreening
                    .ScreenDate = DateTime.Now
                    .EmployeeId = employeeId
                    .EmployeeCode = txtEmployeeId.Text.Trim
                    .EmployeeName = txtEmployeeName.Text.Trim
                    .Remarks = txtRemarks.Text.Trim
                    .ModifiedDate = DateTime.Now
                    .IsEncoded = False
                End With
                Me.myDataset.Screening.AddScreeningRow(_newRowScreening)
                Me.adpScreening.Update(Me.myDataset.Screening)
                Me.myDataset.AcceptChanges()

                frmScreenList.RefreshValues()
                ResetForm()
            Else
                Dim _rowScreening As ScreeningRow = Me.myDataset.Screening.FindByScreenId(screenId)

                With _rowScreening
                    .ScreenDate = txtDate.Text.Trim
                    .EmployeeCode = txtEmployeeId.Text.Trim
                    .EmployeeName = txtEmployeeName.Text.Trim
                    .Remarks = txtRemarks.Text.Trim
                    .ModifiedDate = DateTime.Now
                End With
                Me.bsScreening.EndEdit()
                Me.adpScreening.Update(Me.myDataset.Screening)
                Me.myDataset.AcceptChanges()

                txtEmployeeScanId.Enabled = True
                txtEmployeeScanId.Focus()
                screenId = 0
                txtEmployeeId.Text = String.Empty
                txtEmployeeName.Text = String.Empty
                txtDate.Text = String.Empty
                txtRemarks.Text = String.Empty
                txtRemarks.Enabled = False

                frmScreenList.RefreshValues()
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

    Private Sub dateBinding_Format(sender As Object, e As ConvertEventArgs) Handles dateBinding.Format
        If Not e.Value Is DBNull.Value Then
            e.Value = Format(e.Value, "MMMM dd, yyyy  HH:mm")
        Else
            e.Value = DateTime.Now.ToString("MMMM dd, yyyy  HH:mm")
        End If
    End Sub

    'Private Sub txtRemarks_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRemarks.KeyDown
    '    If e.KeyCode.Equals(Keys.F10) Then
    '        e.Handled = True
    '        btnSave.PerformClick()
    '    End If
    'End Sub

#Region "Subs"
    Private Sub ValidateUser(ByVal _employeeId As String, ByVal _employeeName As String)
        Try
            Dim _countUser As Integer = 0
            Dim _paramCount(0) As SqlParameter
            _paramCount(0) = New SqlParameter("@EmployeeId", SqlDbType.VarChar)
            _paramCount(0).Value = _employeeId

            _countUser = dbMethodJeonsoft.ExecuteScalar("SELECT COUNT(Id) FROM dbo.viwEmployeeInfo WHERE (EmployeeCode = @EmployeeId)", CommandType.Text, _paramCount)

            If _countUser > 0 Then
                Dim _paramReader(0) As SqlParameter
                _paramReader(0) = New SqlParameter("@EmployeeId", SqlDbType.VarChar)
                _paramReader(0).Value = _employeeId

                Dim _reader As IDataReader = dbMethodJeonsoft.ExecuteReader("SELECT Id, EmployeeCode, TRIM(EmployeeName) AS EmployeeName FROM dbo.viwEmployeeInfo WHERE (EmployeeCode = @EmployeeId)", CommandType.Text, _paramReader)

                While _reader.Read
                    employeeId = _reader.Item("Id").ToString
                    txtEmployeeId.Text = _reader.Item("EmployeeCode").ToString.Trim
                    txtEmployeeName.Text = _reader("EmployeeName").ToString.Trim
                End While
                _reader.Close()

                txtEmployeeScanId.Clear()
                txtEmployeeScanId.Enabled = False

                txtDate.Text = DateTime.Now.ToString("MMMM dd, yyyy HH:mm")
                txtRemarks.Enabled = True
                txtRemarks.Focus()
            Else
                MessageBox.Show("Employee ID not found.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                btnClear.PerformClick()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ResetForm()
        txtEmployeeScanId.Enabled = True
        txtEmployeeScanId.Text = String.Empty
        txtEmployeeId.Text = String.Empty
        txtEmployeeName.Text = String.Empty
        txtDate.Text = String.Empty
        txtRemarks.Clear()
        txtRemarks.Enabled = False
        txtEmployeeScanId.Select()
    End Sub
#End Region

#Region "UI"
    Private Sub txtEmployeeScanId_Enter(sender As Object, e As EventArgs)
        lblEmployeeScanId.ForeColor = Color.White
        lblEmployeeScanId.BackColor = Color.DarkSlateGray
    End Sub

    Private Sub txtEmployeeScanId_Leave(sender As Object, e As EventArgs)
        lblEmployeeScanId.ForeColor = Color.Black
        lblEmployeeScanId.BackColor = SystemColors.Control
    End Sub

    Private Sub txtRemarks_Enter(sender As Object, e As EventArgs)
        lblRemarks.ForeColor = Color.White
        lblRemarks.BackColor = Color.DarkSlateGray
    End Sub

    Private Sub txtRemarks_Leave(sender As Object, e As EventArgs)
        lblRemarks.ForeColor = Color.Black
        lblRemarks.BackColor = SystemColors.Control
    End Sub
#End Region

End Class
