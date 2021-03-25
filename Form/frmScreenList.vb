Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports BlackCoffeeLibrary.BlackCoffee
Imports LeaveFilingSystem
Imports LeaveFilingSystem.dsLeaveFiling
Imports LeaveFilingSystem.dsLeaveFilingTableAdapters

Public Class frmScreenList
    Private connection As New clsConnection
    Private dbMethodLocal As New SqlDbMethod(connection.LocalConnection)
    Private dbMethodJeonsoft As New SqlDbMethod(connection.JeonsoftConnection)
    Private main As New Main

    Private pageSize As Integer 'display the number of records per page
    Private pageIndex As Integer 'page sequence number
    Private totalCount As Integer 'total records
    Private pageCount As Integer 'total pages

    Private table As New DataTable
    Private isFiltered As Boolean = False
    Private indexScroll As Integer = 0
    Private indexPosition As Integer = 0

    Private Sub frmScreenList_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ActiveControl = dgvList
        pageSize = 100
        pageIndex = 0
        SetPage()
        Application.EnableVisualStyles()
        main.EnableDoubleBuffered(dgvList)

        'disable the resize/maximize button of the form if maximize, enable if the form is minimize
        AddHandler Me.SizeChanged, AddressOf ThisForm_SizeEventHandler

        'disable resize/maximize button of the form
        Me.MaximizeBox = False
    End Sub

    Private Sub frmScreenList_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode.Equals(Keys.F2) Then
            btnAdd.PerformClick()
        ElseIf e.KeyCode.Equals(Keys.F4) Then
            btnDelete.PerformClick()
        ElseIf e.KeyCode.Equals(Keys.F3) Then
            btnEdit.PerformClick()
        ElseIf e.KeyCode.Equals(Keys.F5) Then
            RefreshValues()
        End If
    End Sub

    Private Sub frmScreenList_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        dgvList.Dispose()
    End Sub

    Private Sub BindingNavigatorMoveFirstItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorMoveFirstItem.Click
        pageIndex = 0
        If isFiltered = True Then
            SetPage(dtpFrom.Value.Date, dtpTo.Value.Date)
        Else
            SetPage()
        End If
    End Sub

    Private Sub BindingNavigatorMovePreviousItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorMovePreviousItem.Click
        pageIndex -= 1
        If pageIndex < 0 Then
            pageIndex = 0
        End If

        If isFiltered = True Then
            SetPage(dtpFrom.Value.Date, dtpTo.Value.Date)
        Else
            SetPage()
        End If
    End Sub

    Private Sub BindingNavigatorMoveNextItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorMoveNextItem.Click
        pageIndex += 1
        If pageIndex > pageCount - 1 Then
            pageIndex = pageCount - 1
        End If

        If isFiltered = True Then
            SetPage(dtpFrom.Value.Date, dtpTo.Value.Date)
        Else
            SetPage()
        End If
    End Sub

    Private Sub BindingNavigatorMoveLastItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorMoveLastItem.Click
        pageIndex = pageCount - 1

        If isFiltered = True Then
            SetPage(dtpFrom.Value.Date, dtpTo.Value.Date)
        Else
            SetPage()
        End If
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

    Private Sub dgvList_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellDoubleClick
        btnEdit.PerformClick()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Go()
    End Sub

    Private Sub btnSearchDate_Click(sender As Object, e As EventArgs) Handles btnSearchDate.Click
        Try
            SetPage(dtpFrom.Value.Date, dtpTo.Value.Date)
            isFiltered = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnResetDate_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        dtpFrom.Value = Date.Now
        dtpTo.Value = Date.Now
        isFiltered = False
        RefreshValues()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            Using frmScreenEntry As New frmScreenEntry()
                frmScreenEntry.ShowDialog(Me)
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            Dim _screenId As Integer = dgvList.CurrentRow.Cells("ColScreenId").Value

            Using frmScreenEntry As New frmScreenEntry(_screenId)
                frmScreenEntry.ShowDialog(Me)
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If dgvList.Rows.Count > 0 Then
                If MessageBox.Show("Delete this row?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                    Dim _param(0) As SqlParameter
                    _param(0) = New SqlParameter("@ScreenId", SqlDbType.Int)
                    _param(0).Value = dgvList.CurrentRow.Cells("ColScreenId").Value
                    dbMethodLocal.ExecuteNonQuery("DELETE FROM Screening WHERE ScreenId = @ScreenId", CommandType.Text, _param)
                End If
            End If
            RefreshValues()
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        RefreshValues()
    End Sub

    Private Sub ThisForm_SizeEventHandler(ByVal sender As Object, ByVal e As EventArgs)
        If Me.WindowState = FormWindowState.Minimized Then
            Me.MaximizeBox = False

        ElseIf Me.WindowState = FormWindowState.Maximized Then
            Me.MaximizeBox = False
        End If
    End Sub

#Region "Sub"
    Private Sub SetPage(Optional ByVal _startDate As Date = Nothing, Optional ByVal _endDate As Date = Nothing)
        totalCount = 0

        If Not _startDate = Nothing AndAlso Not _endDate = Nothing Then
            BindPage(pageSize, pageIndex, totalCount, dtpFrom.Value.Date, dtpTo.Value.Date)
            isFiltered = True
        Else
            BindPage(pageSize, pageIndex, totalCount)
        End If

        If totalCount Mod pageSize = 0 Then
            pageCount = totalCount / pageSize
        Else
            pageCount = Math.Truncate(totalCount / pageSize) + 1
        End If

        'current and total pages
        txtPageNumber.Text = pageIndex + 1
        txtTotalPageNumber.Text = "of " & CInt(pageCount) & " Page(s)"

        'enables pager
        txtPageNumber.Enabled = True
        txtTotalPageNumber.Enabled = True
        BindingNavigatorMoveFirstItem.Enabled = True
        BindingNavigatorMovePreviousItem.Enabled = True
        BindingNavigatorMoveNextItem.Enabled = True
        BindingNavigatorMoveLastItem.Enabled = True
    End Sub

    Private Sub BindPage(ByVal _pageSize As Integer, ByVal _pageIndex As Integer, ByVal _totalCount As Integer, Optional ByVal _startDate As Date = Nothing, Optional ByVal _endDate As Date = Nothing)
        Try
            totalCount = 0

            If Not _startDate = Nothing AndAlso Not _endDate = Nothing Then
                Dim _param(4) As SqlParameter
                _param(0) = New SqlParameter("@PageSize", SqlDbType.Int)
                _param(0).Value = _pageSize
                _param(1) = New SqlParameter("@PageIndex", SqlDbType.Int)
                _param(1).Value = _pageIndex
                _param(2) = New SqlParameter("@TotalCount", SqlDbType.Int)
                _param(2).Direction = ParameterDirection.Output
                _param(3) = New SqlParameter("@StartDate", SqlDbType.Date)
                _param(3).Value = dtpFrom.Value.Date
                _param(4) = New SqlParameter("@EndDate", SqlDbType.Date)
                _param(4).Value = dtpTo.Value.Date

                table = dbMethodLocal.FillDataTableSp("RdScreeningPage", _param)
                totalCount = Convert.ToInt32(_param(2).Value)
            Else

                Dim _param(2) As SqlParameter
                _param(0) = New SqlParameter("@PageSize", SqlDbType.Int)
                _param(0).Value = _pageSize
                _param(1) = New SqlParameter("@PageIndex", SqlDbType.Int)
                _param(1).Value = _pageIndex
                _param(2) = New SqlParameter("@TotalCount", SqlDbType.Int)
                _param(2).Direction = ParameterDirection.Output

                table = dbMethodLocal.FillDataTableSp("RdScreeningPage", _param)
                totalCount = Convert.ToInt32(_param(2).Value)
            End If

            dgvList.AutoGenerateColumns = False
            dgvList.DataSource = table
        Catch ex As Exception
            MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            If isFiltered = True Then
                SetPage(dtpFrom.Value.Date, dtpTo.Value.Date)
            Else
                SetPage()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub RefreshValues(Optional _fromOtherForm As Boolean = False)
        If dgvList IsNot Nothing AndAlso dgvList.CurrentRow IsNot Nothing Then Me.Invoke(New Action(AddressOf GetScrollingIndex))
        pageSize = 100
        SetPage()
        If dgvList IsNot Nothing AndAlso dgvList.CurrentRow IsNot Nothing Then Me.Invoke(New Action(AddressOf SetScrollingIndex))
    End Sub

    Private Sub GetScrollingIndex()
        indexScroll = dgvList.FirstDisplayedCell.RowIndex
        indexPosition = dgvList.CurrentRow.Index
    End Sub

    Private Sub SetScrollingIndex()
        dgvList.FirstDisplayedScrollingRowIndex = indexScroll
        dgvList.Rows(indexPosition).Selected = True
    End Sub
#End Region

End Class