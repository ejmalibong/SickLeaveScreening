Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms
Imports BlackCoffeeLibrary.BlackCoffee
Imports SickLeaveScreening
Imports SickLeaveScreening.dsScreeningReport
Imports SickLeaveScreening.dsScreeningReportTableAdapters

Public Class frmScreenReport
    Private connection As New clsConnection
    Private dbLeaveFiling As New SqlDbMethod(connection.LeaveFiling)
    Private main As New Main
    'server datetime
    Private serverDate As DateTime = dbLeaveFiling.GetServerDate
    'dataset
    Private dsScreeningReport As New dsScreeningReport
    Private adpScreeningReport As New VwScreeningTableAdapter
    Private dtScreeningReport As New VwScreeningDataTable
    Private bsScreeningReport As New BindingSource
    'report
    Private query As String = String.Empty
    Private periodCovered As String = String.Empty
    Private leaveType As String = String.Empty
    Private empType As String = String.Empty
    'employment type
    Private dictionary As New Dictionary(Of String, Integer)

    Private Sub frmHealthScreeningReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'disable the resize/maximize button of the form if maximize, enable if the form is minimize
            AddHandler Me.SizeChanged, AddressOf frmMain_SizeEventHandler

            'disable resize/maximize button of the form
            Me.MaximizeBox = False

            dbLeaveFiling.FillCmbWithCaption("RdLeaveType", CommandType.StoredProcedure, "LeaveTypeId", "LeaveTypeName", cmbLeaveType, "< Select Leave Type >")

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
                MessageBox.Show("Invalid date range.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ElseIf dtpStartDate.Value.Date = dtpEndDate.Value.Date Then
                GoTo ShowReport
            Else
ShowReport:
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
                        empType = "Direct"
                    ElseIf cmbEmployment.SelectedValue = 2 Then
                        query += " AND EmployeeId IS NULL"
                        empType = "Agency"
                    End If
                Else
                    empType = " "
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
                    _rptParam.Add(New Microsoft.Reporting.WinForms.ReportParameter("EmploymentType", empType))
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

#Region "Sub"
    'prevent form resizing when double clicked the titlebar or dragged
    Protected Overloads Overrides Sub WndProc(ByRef m As Message)
        Const WM_NCLBUTTONDBLCLK As Integer = 163 'define doubleclick event
        Const WM_NCLBUTTONDOWN As Integer = 161 'define leftbuttondown event
        Const WM_SYSCOMMAND As Integer = 274 'define move action
        Const HTCAPTION As Integer = 2 'define that the WM_NCLBUTTONDOWN is at titlebar
        Const SC_MOVE As Integer = 61456 'trap move action
        'disable moving titleBar
        If (m.Msg = WM_SYSCOMMAND) AndAlso (m.WParam.ToInt32() = SC_MOVE) Then
            Exit Sub
        End If
        'track whether clicked on title bar
        If (m.Msg = WM_NCLBUTTONDOWN) AndAlso (m.WParam.ToInt32() = HTCAPTION) Then
            Exit Sub
        End If
        'disable double click on title bar
        If (m.Msg = WM_NCLBUTTONDBLCLK) Then
            Exit Sub
        End If

        MyBase.WndProc(m)
    End Sub

    Private Sub frmMain_SizeEventHandler(ByVal sender As Object, ByVal e As EventArgs)
        If Me.WindowState = FormWindowState.Minimized Then
            Me.MaximizeBox = True

        ElseIf Me.WindowState = FormWindowState.Maximized Then
            Me.MaximizeBox = False
        End If
    End Sub
#End Region

End Class