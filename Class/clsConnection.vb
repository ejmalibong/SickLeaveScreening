Imports System.Configuration
Imports System.Data.SqlClient

Public Class clsConnection
    Private con As SqlConnection

    Public Function LeaveFiling() As String
        Return ConfigurationManager.ConnectionStrings("SickLeaveScreening.My.MySettings.LeaveFilingConnectionString").ConnectionString
    End Function

    Public Function Jeonsoft() As String
        Return ConfigurationManager.ConnectionStrings("SickLeaveScreening.My.MySettings.NBCTECHDBConnectionString").ConnectionString
    End Function

End Class