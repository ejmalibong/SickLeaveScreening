Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports BlackCoffeeLibrary.BlackCoffee
Imports LeaveFilingSystem
Imports LeaveFilingSystem.dsLeaveFiling
Imports LeaveFilingSystem.dsLeaveFilingTableAdapters

Public Class frmScreenList
    Private connection As New clsConnection
    Private dbLeaveFiling As New SqlDbMethod(connection.LocalConnection)
    Private dbJeonsoft As New SqlDbMethod(connection.JeonsoftConnection)
    Private main As New Main
    'server datetime
    Private serverDate As DateTime = dbLeaveFiling.GetServerDate
    'data objects
    Private dsLeaveFiling As New dsLeaveFiling
    Private adpScreening As New ScreeningTableAdapter
    Private adpLeaveFiling As New LeaveFilingTableAdapter
    Private dtScreening As New ScreeningDataTable
    Private bsScreening As New BindingSource
    'pagination
    Private pageSize As Integer
    Private pageIndex As Integer
    Private totalCount As Integer
    Private pageCount As Integer
    Private indexScroll As Integer = 0
    Private indexPosition As Integer = 0
    'search criteria
    Private dictionary As New Dictionary(Of String, Integer)
    'flag filters
    Private isFilterByScreenDate As Boolean = False
    Private isFilterByEmployeeName As Boolean = False
    Private isFilterByAbsentFrom As Boolean = False
    Private isFilterByReason As Boolean = False
    Private isFilterByDiagnosis As Boolean = False
    'doctor, nurse
    Private employeeId As Integer = 0
    Private employeeCode As String = String.Empty
    Private employeeName As String = String.Empty
    Private positionName As String = String.Empty

    Public Sub New(ByVal _employeeId As Integer, ByVal _employeeCode As String, ByVal _employeeName As String, ByVal _positionName As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        employeeId = _employeeId
        employeeCode = _employeeCode
        employeeName = _employeeName
        positionName = _positionName
    End Sub

    Private Sub frmScreenList_Load(sender As Object, e As EventArgs) Handles Me.Load
        main.EnableDoubleBuffered(dgvList)

        pageIndex = 0
        pageSize = 100
        BindPage()

        SearchCriteria()

        'disable the resize/maximize button of the form if maximize, enable if the form is minimize
        AddHandler Me.SizeChanged, AddressOf ThisForm_SizeEventHandler

        'disable resize/maximize button of the form
        Me.MaximizeBox = False

        Me.dgvList.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Me.dgvList.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        txtUsername.Text = StrConv(employeeName, VbStrConv.ProperCase) & " / " & positionName

        Me.ActiveControl = dgvList
    End Sub

    Private Sub frmScreenList_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F2
                e.Handled = True
                btnAdd.PerformClick()
            Case Keys.F3
                e.Handled = True
                btnEdit.PerformClick()
            Case Keys.F4
                e.Handled = True
                btnDelete.PerformClick()
            Case Keys.F5
                e.Handled = True
                RefreshValues()
        End Select
    End Sub

    Private Sub frmScreenList_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        dgvList.Dispose()
    End Sub

    Private Sub dgvList_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellDoubleClick
        btnEdit.PerformClick()
    End Sub

    Private Sub BindingNavigatorMoveFirstItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorMoveFirstItem.Click
        pageIndex = 0
        BindPage()
    End Sub

    Private Sub BindingNavigatorMovePreviousItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorMovePreviousItem.Click
        pageIndex -= 1
        If pageIndex < 0 Then
            pageIndex = 0
        End If
        BindPage()
    End Sub

    Private Sub BindingNavigatorMoveNextItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorMoveNextItem.Click
        pageIndex += 1
        If pageIndex > pageCount - 1 Then
            pageIndex = pageCount - 1
        End If
        BindPage()
    End Sub

    Private Sub BindingNavigatorMoveLastItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorMoveLastItem.Click
        pageIndex = pageCount - 1
        BindPage()
    End Sub

    'can only press 0-9, delete, enter, backspace
    Private Sub BindingNavigatorPositionItem_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPageNumber.KeyPress
        If ((Asc(e.KeyChar) >= 48 AndAlso Asc(e.KeyChar) <= 57) OrElse Asc(e.KeyChar) = 8 OrElse Asc(e.KeyChar) = 13 OrElse Asc(e.KeyChar) = 127) Then
            e.Handled = False
            If Asc(e.KeyChar) = 13 Then
                Go()
            End If
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        RefreshValues()
    End Sub

    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        Try
            Using frmHealthScreeningReport As New frmScreenReport()
                frmHealthScreeningReport.ShowDialog(Me)
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDoctor_Click(sender As Object, e As EventArgs) Handles btnDoctor.Click
        Try
            Using frmDoctor As New frmDoctor()
                frmDoctor.ShowDialog(Me)
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            Using frmScreenEntry As New frmScreenEntry(employeeId)
                frmScreenEntry.ShowDialog(Me)
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            If dgvList.Rows.Count > 0 Then
                Dim _screenId As Integer = CType(Me.bsScreening.Current, DataRowView).Item("ScreenId")
                Using frmScreenEntry As New frmScreenEntry(employeeId, _screenId)
                    frmScreenEntry.ShowDialog(Me)
                    If frmScreenEntry.DialogResult = Windows.Forms.DialogResult.OK Then
                        RefreshValues()
                    End If
                End Using
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If dgvList.Rows.Count > 0 Then
                Dim _screenId As Integer = CType(Me.bsScreening.Current, DataRowView).Item("ScreenId")
                Dim _count As Integer = 0
                Dim _leaveFileId As Integer = 0

                Dim _prmCount(0) As SqlParameter
                _prmCount(0) = New SqlParameter("@ScreenId", SqlDbType.Int)
                _prmCount(0).Value = _screenId

                _count = dbLeaveFiling.ExecuteScalar("SELECT Count(LeaveFileId) FROM dbo.LeaveFiling WHERE ScreenId = @ScreenId", CommandType.Text, _prmCount)

                If _count > 0 Then
                    MessageBox.Show("Screening record already used.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Else
                    If MessageBox.Show("Delete this record?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                        Me.bsScreening.RemoveCurrent()
                    End If
                End If

                Me.adpScreening.Update(Me.dsLeaveFiling.Screening)
                Me.dsLeaveFiling.AcceptChanges()
                RefreshValues()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
       Application.Exit()
    End Sub

    Private Sub btnLogOut_Click(sender As Object, e As EventArgs) Handles btnLogOut.Click
        Me.Hide()
        frmLogin.Show()
    End Sub

    Private Sub cmbSearchCriteria_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbSearchCriteria.SelectedValueChanged
        Try
            If cmbSearchCriteria.SelectedValue = 1 Then
                pnlScreenDate.Visible = True
                pnlEmployeeName.Visible = False
                pnlAbsentDate.Visible = False
                pnlReason.Visible = False
                pnlDiagnosis.Visible = False
                Me.ActiveControl = dtpScreenDateFrom
            ElseIf cmbSearchCriteria.SelectedValue = 2 Then
                pnlScreenDate.Visible = False
                pnlEmployeeName.Visible = True
                pnlAbsentDate.Visible = False
                pnlReason.Visible = False
                pnlDiagnosis.Visible = False
                Me.ActiveControl = txtEmployeeName
            ElseIf cmbSearchCriteria.SelectedValue = 3 Then
                pnlScreenDate.Visible = False
                pnlEmployeeName.Visible = False
                pnlAbsentDate.Visible = True
                pnlReason.Visible = False
                pnlDiagnosis.Visible = False
                Me.ActiveControl = dtpAbsentFrom
            ElseIf cmbSearchCriteria.SelectedValue = 4 Then
                pnlScreenDate.Visible = False
                pnlEmployeeName.Visible = False
                pnlAbsentDate.Visible = False
                pnlReason.Visible = True
                pnlDiagnosis.Visible = False
                Me.ActiveControl = txtReason
            ElseIf cmbSearchCriteria.SelectedValue = 5 Then
                pnlScreenDate.Visible = False
                pnlEmployeeName.Visible = False
                pnlAbsentDate.Visible = False
                pnlReason.Visible = False
                pnlDiagnosis.Visible = True
                Me.ActiveControl = txtDiagnosis
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Go()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            If cmbSearchCriteria.SelectedValue = 1 Then
                If dtpScreenDateFrom.Value.Date > dtpScreenDateTo.Value.Date Then
                    MessageBox.Show("Start date is later than end date.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

                isFilterByScreenDate = True
                isFilterByEmployeeName = False
                isFilterByAbsentFrom = False
                isFilterByReason = False
                isFilterByDiagnosis = False

            ElseIf cmbSearchCriteria.SelectedValue = 2 Then
                isFilterByScreenDate = False
                isFilterByEmployeeName = True
                isFilterByAbsentFrom = False
                isFilterByReason = False
                isFilterByDiagnosis = False

            ElseIf cmbSearchCriteria.SelectedValue = 3 Then
                If dtpAbsentFrom.Value.Date > dtpAbsentTo.Value.Date Then
                    MessageBox.Show("Start date is later than end date.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

                isFilterByScreenDate = False
                isFilterByEmployeeName = False
                isFilterByAbsentFrom = True
                isFilterByReason = False
                isFilterByDiagnosis = False

            ElseIf cmbSearchCriteria.SelectedValue = 4 Then
                isFilterByScreenDate = False
                isFilterByEmployeeName = False
                isFilterByAbsentFrom = False
                isFilterByReason = True
                isFilterByDiagnosis = False

            ElseIf cmbSearchCriteria.SelectedValue = 5 Then
                isFilterByScreenDate = False
                isFilterByEmployeeName = False
                isFilterByAbsentFrom = False
                isFilterByReason = False
                isFilterByDiagnosis = True
            End If

            pageIndex = 0
            BindPage()
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            If cmbSearchCriteria.SelectedValue = 1 Then
                isFilterByScreenDate = False
                isFilterByEmployeeName = False
                isFilterByAbsentFrom = False
                isFilterByReason = False
                isFilterByDiagnosis = False

                dtpScreenDateFrom.Value = Date.Now
                dtpScreenDateTo.Value = Date.Now
                pageIndex = 0
                BindPage()

            ElseIf cmbSearchCriteria.SelectedValue = 2 Then
                txtEmployeeName.Clear()

                isFilterByScreenDate = False
                isFilterByEmployeeName = True
                isFilterByAbsentFrom = False
                isFilterByReason = False
                isFilterByDiagnosis = False

                pageIndex = 0
                BindPage()

            ElseIf cmbSearchCriteria.SelectedValue = 3 Then
                isFilterByScreenDate = False
                isFilterByEmployeeName = False
                isFilterByAbsentFrom = False
                isFilterByReason = False
                isFilterByDiagnosis = False

                dtpAbsentFrom.Value = Date.Now
                dtpAbsentTo.Value = Date.Now
                pageIndex = 0
                BindPage()

            ElseIf cmbSearchCriteria.SelectedValue = 4 Then
                txtReason.Clear()

                isFilterByScreenDate = False
                isFilterByEmployeeName = False
                isFilterByAbsentFrom = False
                isFilterByReason = True
                isFilterByDiagnosis = False

                pageIndex = 0
                BindPage()

            ElseIf cmbSearchCriteria.SelectedValue = 5 Then
                txtDiagnosis.Clear()

                isFilterByScreenDate = False
                isFilterByEmployeeName = False
                isFilterByAbsentFrom = False
                isFilterByReason = False
                isFilterByDiagnosis = True

                pageIndex = 0
                BindPage()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BindPage()
        Try
            totalCount = 0

            If isFilterByScreenDate = True Then
                Me.adpScreening.FillByScreenDate(Me.dsLeaveFiling.Screening, pageIndex, pageSize, totalCount, dtpScreenDateFrom.Value.Date, dtpScreenDateTo.Value.Date)
            ElseIf isFilterByEmployeeName = True Then
                Me.adpScreening.FillByEmployeeName(Me.dsLeaveFiling.Screening, pageIndex, pageSize, totalCount, txtEmployeeName.Text.Trim)
            ElseIf isFilterByAbsentFrom = True Then
                Me.adpScreening.FillByAbsentFrom(Me.dsLeaveFiling.Screening, pageIndex, pageSize, totalCount, dtpAbsentFrom.Value.Date, dtpAbsentTo.Value.Date)
            ElseIf isFilterByReason = True Then
                Me.adpScreening.FillByReason(Me.dsLeaveFiling.Screening, pageIndex, pageSize, totalCount, txtReason.Text.Trim)
            ElseIf isFilterByDiagnosis = True Then
                Me.adpScreening.FillByDiagnosis(Me.dsLeaveFiling.Screening, pageIndex, pageSize, totalCount, txtDiagnosis.Text.Trim)
            Else
                Me.adpScreening.FillScreening(Me.dsLeaveFiling.Screening, pageIndex, pageSize, totalCount)
            End If

            Me.bsScreening.DataSource = Me.dsLeaveFiling
            Me.bsScreening.DataMember = dtScreening.TableName
            Me.bsScreening.ResetBindings(True)
            dgvList.AutoGenerateColumns = False
            Me.dgvList.DataSource = Me.bsScreening

            If totalCount Mod pageSize = 0 Then
                If totalCount = 0 Then
                    pageCount = (totalCount / pageSize) + 1
                Else
                    pageCount = totalCount / pageSize
                End If
            Else
                pageCount = Math.Truncate(totalCount / pageSize) + 1
            End If

            'current and total pages
            txtPageNumber.Text = pageIndex + 1
            txtTotalPageNumber.Text = " of " & CInt(pageCount) & " Page(s)"

            'enables pager
            txtPageNumber.Enabled = True
            txtTotalPageNumber.Enabled = True
            BindingNavigatorMoveFirstItem.Enabled = True
            BindingNavigatorMovePreviousItem.Enabled = True
            BindingNavigatorMoveNextItem.Enabled = True
            BindingNavigatorMoveLastItem.Enabled = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Go()
        Try
            If String.IsNullOrEmpty(txtPageNumber.Text) Then
                MessageBox.Show("Page not found.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtPageNumber.Focus()
                Return
            End If

            If CInt(txtPageNumber.Text) > pageCount Then
                MessageBox.Show("Page not found.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtPageNumber.Focus()
                Return
            End If

            If CInt(txtPageNumber.Text) = 0 Then
                MessageBox.Show("Page not found.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtPageNumber.Focus()
                Return
            End If

            pageIndex = CInt(txtPageNumber.Text) - 1
            BindPage()
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub RefreshValues()
        If dgvList IsNot Nothing AndAlso dgvList.CurrentRow IsNot Nothing Then Me.Invoke(New Action(AddressOf GetScrollingIndex))
        pageSize = 100
        pageIndex = 0
        BindPage()
        If dgvList IsNot Nothing AndAlso dgvList.CurrentRow IsNot Nothing Then Me.Invoke(New Action(AddressOf SetScrollingIndex))
    End Sub

    Private Sub GetScrollingIndex()
        indexScroll = dgvList.FirstDisplayedCell.RowIndex
        indexPosition = dgvList.CurrentRow.Index
    End Sub

    Private Sub SetScrollingIndex()
        dgvList.FirstDisplayedScrollingRowIndex = indexScroll
        If dgvList.Rows.Count > indexPosition Then
            dgvList.Rows(indexPosition).Selected = True
        Else
            dgvList.Rows(indexPosition - 1).Selected = True
        End If
        Me.bsScreening.Position = dgvList.SelectedCells(0).RowIndex
    End Sub

    Private Sub SearchCriteria()
        dictionary.Add(" Screening Date", 1)
        dictionary.Add(" Employee Name", 2)
        dictionary.Add(" Absent Date", 3)
        dictionary.Add(" Reason", 4)
        dictionary.Add(" Diagnosis", 5)
        cmbSearchCriteria.DisplayMember = "Key"
        cmbSearchCriteria.ValueMember = "Value"
        cmbSearchCriteria.DataSource = New BindingSource(dictionary, Nothing)
    End Sub

#Region "Sub"
    'prevent form resizing when double clicked the titlebar or dragged
    Protected Overloads Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
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

    Private Sub ThisForm_SizeEventHandler(ByVal sender As Object, ByVal e As EventArgs)
        If Me.WindowState = FormWindowState.Minimized Then
            Me.MaximizeBox = False

        ElseIf Me.WindowState = FormWindowState.Maximized Then
            Me.MaximizeBox = False
        End If
    End Sub
#End Region

End Class