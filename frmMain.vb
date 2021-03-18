Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports BlackCoffeeLibrary.BlackCoffee
Imports LeaveFilingSystem.dsLeaveFiling
Imports LeaveFilingSystem.dsLeaveFilingTableAdapters

Public Class frmMain
    Private connection As New clsConnection
    Private dbMethodLocal As New SqlDbMethod(connection.LocalConnection)
    Private dbMethodJeonsoft As New SqlDbMethod(connection.JeonsoftConnection)
    Private main As New Main

    Private countUser As Integer = 0
    Private employeeId As Integer = 0
    Private arrSplitted() As String

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        txtRemarks.Enabled = False
    End Sub

    Private Sub frmMain_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode.Equals(Keys.Escape) Then
            btnClear.PerformClick()
        ElseIf e.KeyCode.Equals(Keys.F10) Then
            btnSave.PerformClick()
        End If
    End Sub

    Private Sub txtEmployeeId_KeyDown(sender As Object, e As KeyEventArgs) Handles txtEmployeeScanId.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If String.IsNullOrEmpty(txtEmployeeScanId.Text.Trim) = True Then
                    MessageBox.Show("Please tap your ID.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return
                End If

                arrSplitted = Split(txtEmployeeScanId.Text.Trim, " ", 2)

                If Not arrSplitted.Length = 2 Then
                    MessageBox.Show("Please tap your ID.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return
                End If

                'ValidateUser(arrSplitted(0).ToString, arrSplitted(1).ToString)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try

        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtEmployeeId.Text = String.Empty
        txtEmployeeName.Text = String.Empty
        txtDate.Text = String.Empty
        txtRemarks.Enabled = False
        txtEmployeeScanId.Enabled = True
        txtEmployeeScanId.Focus()
        txtRemarks.Text = String.Empty
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub ValidateUser(ByVal _employeeId As String, ByVal _employeeName As String)
        Try
            Dim _paramCount(0) As SqlParameter
            _paramCount(0) = New SqlParameter("@EmployeeId", SqlDbType.VarChar)
            _paramCount(0).Value = _employeeId

            countUser = dbMethodJeonsoft.ExecuteScalar("SELECT COUNT(Id) FROM dbo.viwEmployeeInfo WHERE (EmployeeCode = @EmployeeId)", CommandType.Text, _paramCount)

            If countUser > 0 Then
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

                txtDate.Text = DateTime.Now
                txtEmployeeScanId.Enabled = False
                txtRemarks.Enabled = True
                txtRemarks.Focus()

                txtEmployeeScanId.Clear()
            Else
                MessageBox.Show("Employee ID not found.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#Region "UI"
    Private Sub txtEmployeeScanId_Enter(sender As Object, e As EventArgs) Handles txtEmployeeScanId.Enter
        lblEmployeeScanId.ForeColor = Color.White
        lblEmployeeScanId.BackColor = Color.DarkSlateGray
    End Sub

    Private Sub txtEmployeeScanId_Leave(sender As Object, e As EventArgs) Handles txtEmployeeScanId.Leave
        lblEmployeeScanId.ForeColor = Color.Black
        lblEmployeeScanId.BackColor = SystemColors.Control
    End Sub

    Private Sub txtRemarks_Enter(sender As Object, e As EventArgs) Handles txtRemarks.Enter
        lblRemarks.ForeColor = Color.White
        lblRemarks.BackColor = Color.DarkSlateGray
    End Sub

    Private Sub txtRemarks_Leave(sender As Object, e As EventArgs) Handles txtRemarks.Leave
        lblRemarks.ForeColor = Color.Black
        lblRemarks.BackColor = SystemColors.Control
    End Sub
#End Region

End Class
