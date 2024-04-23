Imports System.Data
Imports System.Data.SqlClient
Imports System
Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Xml

Partial Class masterpages_Defibrillator
    Inherits System.Web.UI.Page

    Dim ConnectionString As String = ""
    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim EmployeeID As String = ""
    Dim Allowed As Boolean = False
    Dim reader As SqlDataReader


    ''New code for trial user check
    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.AddHeader("Refresh", "60")

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

        ' New code for session 
        Me.SessionRefresh = Date.Now
        If CompanyID = "BranchingOutFloralL0E1E0" Then
            ' SessionRefreshTableInsert()

        End If
        SessionRefreshTableInsert()
        ' New code for session 

    End Sub



    Dim BrowserSessionId As String = ""
    Dim SessionRefresh As Date


    Public Function SessionRefreshTableInsert() As Boolean
        SessionRefreshTableInsertDaily()
        Try
            If Me.EmployeeID.Trim = "" Then
                Return True
            End If
        Catch ex As Exception

        End Try


        BrowserSessionId = Session.SessionID


        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "insert into SessionRefreshTable( CompanyID, DivisionID, DepartmentID, EmployeeID,BrowserSessionId,SessionRefresh,Using,IP,url) " _
             & " values(@CompanyID,@DivisionID,@DepartmentID,@EmployeeID,@BrowserSessionId,@SessionRefresh,@Using,@IP,@url)"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@EmployeeID", SqlDbType.NVarChar, 36)).Value = Me.EmployeeID
            com.Parameters.Add(New SqlParameter("@BrowserSessionId", SqlDbType.NVarChar, 500)).Value = Me.BrowserSessionId
            com.Parameters.Add(New SqlParameter("@SessionRefresh", SqlDbType.DateTime)).Value = Me.SessionRefresh
            com.Parameters.Add(New SqlParameter("@Using", SqlDbType.NVarChar)).Value = "POM"
            com.Parameters.Add(New SqlParameter("@IP", SqlDbType.NVarChar)).Value = Request.ServerVariables("REMOTE_ADDR")
            Try
                com.Parameters.Add(New SqlParameter("@url", SqlDbType.NVarChar)).Value = Session("URL")
            Catch ex As Exception
                com.Parameters.Add(New SqlParameter("@url", SqlDbType.NVarChar)).Value = ""
            End Try

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()
            SessionRefreshTableInsertDaily()
            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False

        End Try
    End Function


    Public Function SessionRefreshTableInsertDaily() As Boolean
        SessionRefreshTableInsertDailyClean()
        Try
            If Me.EmployeeID.Trim = "" Then
                Return True
            End If
        Catch ex As Exception

        End Try


        BrowserSessionId = Session.SessionID

        Dim term As String = "DEFAULT" '= Session("TerminalID")

        Try
            term = Session("TerminalID")
            term = term.Trim
        Catch ex As Exception
            term = "DEFAULT"
        End Try

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "insert into SessionRefreshTableDaily( CompanyID, DivisionID, DepartmentID, EmployeeID,BrowserSessionId,SessionRefresh,Using,IP,url,TerminalID) " _
             & " values(@CompanyID,@DivisionID,@DepartmentID,@EmployeeID,@BrowserSessionId,@SessionRefresh,@Using,@IP,@url,@TerminalID)"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@EmployeeID", SqlDbType.NVarChar, 36)).Value = Me.EmployeeID
            com.Parameters.Add(New SqlParameter("@BrowserSessionId", SqlDbType.NVarChar, 500)).Value = Me.BrowserSessionId
            com.Parameters.Add(New SqlParameter("@SessionRefresh", SqlDbType.DateTime)).Value = Me.SessionRefresh
            com.Parameters.Add(New SqlParameter("@Using", SqlDbType.NVarChar)).Value = "POM"
            com.Parameters.Add(New SqlParameter("@IP", SqlDbType.NVarChar)).Value = Request.ServerVariables("REMOTE_ADDR")
            Try
                com.Parameters.Add(New SqlParameter("@url", SqlDbType.NVarChar)).Value = Session("URL")
            Catch ex As Exception
                com.Parameters.Add(New SqlParameter("@url", SqlDbType.NVarChar)).Value = ""
            End Try
            com.Parameters.Add(New SqlParameter("@TerminalID", SqlDbType.NVarChar)).Value = term
            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False

        End Try
    End Function



    Public Function SessionRefreshTableInsertDailyClean() As Boolean

        Try
            If Me.EmployeeID.Trim = "" Then
                Return True
            End If
        Catch ex As Exception

        End Try


        BrowserSessionId = Session.SessionID


        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "Delete From  SessionRefreshTableDaily Where Using = @Using AND CompanyID =@CompanyID AND DivisionID = @DivisionID AND  DepartmentID = @DepartmentID AND  EmployeeID = @EmployeeID   " _
             & "  "

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@EmployeeID", SqlDbType.NVarChar, 36)).Value = Me.EmployeeID
            com.Parameters.Add(New SqlParameter("@Using", SqlDbType.NVarChar)).Value = "POM"
            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False

        End Try
    End Function


End Class
