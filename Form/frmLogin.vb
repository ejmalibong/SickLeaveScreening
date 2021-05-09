Imports System.Data.SqlClient
Imports BlackCoffeeLibrary.BlackCoffee
Imports System.Deployment.Application

Public Class frmLogin
    Private connection As New clsConnection
    Private dbLeaveFiling As New SqlDbMethod(connection.LeaveFiling)
    Private main As New Main

    Private employeeId As Integer = 0
    Private employeeCode As String = String.Empty
    Private employeeName As String = String.Empty
    Private positionName As String = String.Empty

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'txtEmployeeId.Text = "1710-003" 'mam faye
        'txtEmployeeId.Text = "1001" 'doc omman
        'txtEmployeeId.Text = "1805-003" 'mam irene

        If ApplicationDeployment.IsNetworkDeployed Then
            lblVersion.Text = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString
        Else
            lblVersion.Text = Application.ProductVersion.ToString
        End If

        Me.ActiveControl = txtEmployeeId
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If String.IsNullOrEmpty(txtEmployeeId.Text.Trim) Then
            MessageBox.Show("Please enter employee ID.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtEmployeeId.Focus()
            Return
        End If

        Dim _count1 As Integer = 0
        Dim _prm1(0) As SqlParameter
        _prm1(0) = New SqlParameter("@Password", SqlDbType.NVarChar)
        _prm1(0).Value = txtEmployeeId.Text.Trim

        _count1 = dbLeaveFiling.ExecuteScalar("RdClinic", CommandType.StoredProcedure, _prm1)

        If _count1 > 0 Then
            Dim _prm2(0) As SqlParameter
            _prm2(0) = New SqlParameter("@Password", SqlDbType.VarChar)
            _prm2(0).Value = txtEmployeeId.Text.Trim

            Dim _reader As IDataReader = dbLeaveFiling.ExecuteReader("RdClinic", CommandType.StoredProcedure, _prm2)

            While _reader.Read
                employeeId = _reader.Item("EmployeeId")
                employeeCode = _reader.Item("EmployeeCode").ToString.Trim
                employeeName = _reader.Item("EmployeeName").ToString.Trim
                positionName = _reader.Item("PositionName").ToString.Trim
            End While
            _reader.Close()

            Me.Hide()
            Dim _frmScreenList As New frmScreenList(employeeId, employeeCode, employeeName, positionName)
            _frmScreenList.Show()
            txtEmployeeId.Clear()
        Else
            Dim _count3 As Integer = 0
            Dim _prm3(0) As SqlParameter
            _prm3(0) = New SqlParameter("@EmployeeCode", SqlDbType.VarChar)
            _prm3(0).Value = txtEmployeeId.Text.Trim

            _count3 = dbLeaveFiling.ExecuteScalar("RdClinic", CommandType.StoredProcedure, _prm3)

            If _count3 > 0 Then
                Dim _prm4(0) As SqlParameter
                _prm4(0) = New SqlParameter("@EmployeeCode", SqlDbType.VarChar)
                _prm4(0).Value = txtEmployeeId.Text.Trim

                Dim _reader As IDataReader = dbLeaveFiling.ExecuteReader("RdClinic", CommandType.StoredProcedure, _prm4)

                While _reader.Read
                    employeeId = _reader.Item("EmployeeId")
                    employeeCode = _reader.Item("EmployeeCode").ToString.Trim
                    employeeName = _reader.Item("EmployeeName").ToString.Trim
                    positionName = _reader.Item("PositionName").ToString.Trim
                End While
                _reader.Close()

                Me.Hide()
                Dim _frmScreenList As New frmScreenList(employeeId, employeeCode, employeeName, positionName)
                _frmScreenList.Show()
                txtEmployeeId.Clear()
            Else
                MessageBox.Show("Employee not found or do not have permission.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtEmployeeId.Clear()
                txtEmployeeId.Focus()
            End If
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Application.Exit()
    End Sub

End Class
