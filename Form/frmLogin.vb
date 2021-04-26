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
        'txtEmployeeId.Text = "1710-003" 'mam irene
        'txtEmployeeId.Text = "FMB-0451" 'doc omman

        If ApplicationDeployment.IsNetworkDeployed Then
            lblVersion.Text = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString
        Else
            lblVersion.Text = Application.ProductVersion.ToString
        End If

        Application.EnableVisualStyles()
        Me.ActiveControl = txtEmployeeId
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If String.IsNullOrEmpty(txtEmployeeId.Text.Trim) Then
            MessageBox.Show("Please enter your employee ID.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtEmployeeId.Focus()
            Return
        End If

        Dim _count As Integer = 0
        Dim _prmEmpCode1(0) As SqlParameter
        _prmEmpCode1(0) = New SqlParameter("@EmployeeCode", SqlDbType.VarChar)
        _prmEmpCode1(0).Value = txtEmployeeId.Text.Trim

        _count = dbLeaveFiling.ExecuteScalar("RdClinic", CommandType.StoredProcedure, _prmEmpCode1)

        If _count > 0 Then
            Dim _prmEmpCode2(0) As SqlParameter
            _prmEmpCode2(0) = New SqlParameter("@EmployeeCode", SqlDbType.VarChar)
            _prmEmpCode2(0).Value = txtEmployeeId.Text.Trim

            Dim _reader As IDataReader = dbLeaveFiling.ExecuteReader("RdClinic", CommandType.StoredProcedure, _prmEmpCode2)

            While _reader.Read
                employeeId = _reader.Item("Id")
                employeeCode = _reader.Item("EmployeeCode").ToString.Trim
                employeeName = _reader.Item("Name").ToString.Trim
                positionName = _reader.Item("PositionName").ToString.Trim
            End While
            _reader.Close()

            Me.Hide()
            Dim _frmMain As New frmScreenList(employeeId, employeeCode, employeeName, positionName)
            _frmMain.Show()
            txtEmployeeId.Clear()
        Else
            MessageBox.Show("Employee not found or do not have permission.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtEmployeeId.Clear()
            txtEmployeeId.Focus()
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Application.Exit()
    End Sub

End Class
