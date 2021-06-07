Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms
Imports BlackCoffeeLibrary.BlackCoffee
Imports SickLeaveScreening
Imports SickLeaveScreening.dsScreeningReport
Imports SickLeaveScreening.dsScreeningReportTableAdapters

Public Class frmScreenReport
    Private connection As New clsConnection
    Private dbLeaveFiling As New SqlDbMethod(connection.LocalConnection)
    Private main As New Main
    'server datetime
    Private serverDate As DateTime = dbLeaveFiling.GetServerDate
    'dataset
    Private dsScreeningReport As New dsScreeningReport
    Private adpScreeningReport As New VwScreeningTableAdapter
    Private dtScreeningReport As New VwScreeningDataTable
    Private bsScreeningReport As New BindingSource
    'report params
    Private query As String = String.Empty
    Private periodCovered As String = String.Empty
    Private leaveType As String = String.Empty
    Private employeeType As String = String.Empty
    'dictionary for employment type
    Private dictionary As New Dictionary(Of String, Integer)

    Private Sub frmHealthScreeningReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            dbLeaveFiling.FillCmbWithCaption("RdLeaveType", CommandType.StoredProcedure, _
                                             "LeaveTypeId", "LeaveTypeName", cmbLeaveType, _
                                             "< Select Leave Type >")

            dictionary.Add("< Select Employment > ", 0)
            dictionary.Add("Direct", 1)
            dictionary.Add("Agency", 2)
            cmbEmployment.DisplayMember = "Key"
            cmbEmployment.ValueMember = "Value"
            cmbEmployment.DataSource = New BindingSource(dictionary, Nothing)

            Me.adpScreeningReport.Fill(Me.dsScreeningReport.VwScreening)
            Me.bsScreeningReport.DataSource = Me.dsScreeningReport
            Me.bsScreeningReport.DataMember = dtScreeningReport.TableName

            Me.ActiveControl = btnGenerate
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmHealthScreeningReport_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode.Equals(Keys.Enter) Then
            Me.SelectNextControl(Me.ActiveControl, True, True, True, True)
        ElseIf e.KeyCode.Equals(Keys.F10) Then
            e.Handled = True
            btnGenerate.PerformClick()
        End If
    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        Try
            If dtpStartDate.Value.Date > dtpEndDate.Value.Date Then
                MessageBox.Show("Start date is later than end date.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ElseIf dtpStartDate.Value.Date = dtpEndDate.Value.Date Then
                GoTo GenerateReport
            Else
GenerateReport:
                query = "ScreenDate >= '" + main.FormatDate(dtpStartDate.Value.Date, True) + "' AND ScreenDate < '" + main.FormatDate(dtpEndDate.Value.Date, False) + "'"

                If dtpStartDate.Value.Date.Equals(dtpEndDate.Value.Date) Then
                    periodCovered = dtpStartDate.Value.ToString("MMMM dd, yyyy")
                Else
                    periodCovered = dtpStartDate.Value.ToString("MMMM dd, yyyy") & " to " & dtpEndDate.Value.ToString("MMMM dd, yyyy")
                End If

                If Not cmbLeaveType.SelectedValue = 0 Then
                    query += " AND LeaveTypeId = '" & cmbLeaveType.SelectedValue & "'"
                    leaveType = cmbLeaveType.Text
                Else
                    leaveType = " "
                End If

                If Not cmbEmployment.SelectedValue = 0 Then
                    If cmbEmployment.SelectedValue = 1 Then
                        query += " AND EmployeeId IS NOT NULL"
                        employeeType = "Direct"
                    ElseIf cmbEmployment.SelectedValue = 2 Then
                        query += " AND EmployeeId IS NULL"
                        employeeType = "Agency"
                    End If
                Else
                    employeeType = " "
                End If

                If chkNotFtw.CheckState = CheckState.Checked Then
                    query += " AND IsFitToWork = 0"
                End If

                Me.bsScreeningReport.Filter = String.Format(query)
                Me.bsScreeningReport.Sort = "ScreenDate ASC"

                If Me.bsScreeningReport.Count > 0 Then
                    rptViewer.LocalReport.ReportPath = "ReportFile\Screening.rdlc"
                    rptViewer.LocalReport.DataSources.Clear()
                    rptViewer.LocalReport.DataSources.Add(New ReportDataSource(dtScreeningReport.TableName, Me.bsScreeningReport))

                    Dim _rptParam As New ReportParameterCollection
                    _rptParam.Add(New Microsoft.Reporting.WinForms.ReportParameter("PeriodCovered", periodCovered))
                    _rptParam.Add(New Microsoft.Reporting.WinForms.ReportParameter("LeaveType", leaveType))
                    _rptParam.Add(New Microsoft.Reporting.WinForms.ReportParameter("EmploymentType", employeeType))
                    rptViewer.LocalReport.SetParameters(_rptParam)

                    rptViewer.SetDisplayMode(DisplayMode.PrintLayout)
                    rptViewer.ZoomMode = ZoomMode.PageWidth
                    rptViewer.LocalReport.DisplayName = "Monitoring Report"
                    rptViewer.RefreshReport()
                Else
                    MessageBox.Show("No records found.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnRemoveFilters.Click
        main.ClearForm(Me)
        chkNotFtw.CheckState = CheckState.Unchecked
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class