Imports System.Data.SqlClient
Imports BlackCoffeeLibrary.BlackCoffee
Imports System.Deployment.Application

Public Class frmLogin
    Private connection As New clsConnection
    Private dbLeaveFiling As New SqlDbMethod(connection.LocalConnection)
    Private dbJeonsoft As New SqlDbMethod(connection.JeonsoftConnection)
    Private main As New Main

    Private employeeId As Integer = 0
    Private employeeCode As String = String.Empty
    Private employeeName As String = String.Empty
    Private positionName As String = String.Empty

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'txtEmployeeId.Text = "1710-003" 'mam faye
        'txtEmployeeId.Text = "1001" 'doc omman

        'mam irene
        'txtEmployeeId.Text = "1805-003"
        'txtPassword.Text = "torejas"

        If ApplicationDeployment.IsNetworkDeployed Then
            lblVersion.Text = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString
        Else
            lblVersion.Text = Application.ProductVersion.ToString
        End If

        Me.ActiveControl = txtEmployeeId
    End Sub

    Private Sub frmLogin_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        Me.ActiveControl = txtEmployeeId
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Try
            If String.IsNullOrEmpty(txtEmployeeId.Text.Trim) Then
                MessageBox.Show("Please enter your employee ID.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtEmployeeId.Focus()
                Return
            End If

            If String.IsNullOrEmpty(txtPassword.Text.Trim) Then
                MessageBox.Show("Please enter your password.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtPassword.Focus()
                Return
            End If

            Dim _count1 As Integer = 0
            Dim _prm1(1) As SqlParameter
            _prm1(0) = New SqlParameter("@EmployeeCode", SqlDbType.NVarChar)
            _prm1(0).Value = txtEmployeeId.Text.Trim
            _prm1(1) = New SqlParameter("@Password", SqlDbType.NVarChar)
            _prm1(1).Value = txtPassword.Text.Trim

            'check if nbc nurses
            'use latin1 general collation for case-sensitive password
            _count1 = dbLeaveFiling.ExecuteScalar("SELECT COUNT(EmployeeId) FROM VwClinicNbc WHERE EmployeeCode = @EmployeeCode AND " & _
                                                  "(TRIM(Password) COLLATE Latin1_General_CS_AS = @Password)", CommandType.Text, _prm1)

            If _count1 > 0 Then 'nbc nurses
                Dim _prm2(1) As SqlParameter
                _prm2(0) = New SqlParameter("@EmployeeCode", SqlDbType.VarChar)
                _prm2(0).Value = txtEmployeeId.Text.Trim
                _prm2(1) = New SqlParameter("@Password", SqlDbType.NVarChar)
                _prm2(1).Value = txtPassword.Text.Trim

                Dim _reader As IDataReader = dbLeaveFiling.ExecuteReader("RdClinicNbc", CommandType.StoredProcedure, _prm2)
                GetInfo(_reader)

            Else 'non-nbc doctors
                Dim _count2 As Integer = 0
                Dim _prm3(1) As SqlParameter
                _prm3(0) = New SqlParameter("@EmployeeCode", SqlDbType.VarChar)
                _prm3(0).Value = txtEmployeeId.Text.Trim
                _prm3(1) = New SqlParameter("@Password", SqlDbType.NVarChar)
                _prm3(1).Value = txtPassword.Text.Trim

                _count2 = dbLeaveFiling.ExecuteScalar("SELECT COUNT(EmployeeId) FROM dbo.Clinic WHERE TRIM(EmployeeCode) = @EmployeeCode AND " & _
                                                      "(TRIM(Password) COLLATE Latin1_General_CS_AS = @Password) AND IsActive = 1", CommandType.Text, _prm3)

                If _count2 > 0 Then 'non-nbc doctors
                    Dim _prm4(1) As SqlParameter
                    _prm4(0) = New SqlParameter("@EmployeeCode", SqlDbType.VarChar)
                    _prm4(0).Value = txtEmployeeId.Text.Trim
                    _prm4(1) = New SqlParameter("@Password", SqlDbType.NVarChar)
                    _prm4(1).Value = txtPassword.Text.Trim

                    Dim _reader As IDataReader = dbLeaveFiling.ExecuteReader("RdClinic", CommandType.StoredProcedure, _prm4)
                    GetInfo(_reader)

                Else 'unauthorized login
                    MessageBox.Show("Incorrect employeee ID or password.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    txtEmployeeId.Focus()
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Application.Exit()
    End Sub

#Region "Sub"
    Private Sub GetInfo(ByVal _reader As IDataReader)
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
        txtPassword.Clear()
    End Sub
#End Region

End Class
