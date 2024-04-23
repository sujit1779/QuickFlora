Imports System.Data.SqlClient

Imports System.Data
Imports DAL
Imports EnterpriseCommon.Configuration
Imports EnterpriseASPClient.Core
Imports EnterpriseClient.Core


Partial Class MainMaster
    Inherits System.Web.UI.MasterPage
    Public JsCompanyID As String = ""
    Public JSDivisionID As String = ""
    Public JSDepartmentID As String = ""
    Public JSUserName As String = ""

    Dim LastName As String
    Dim CompanyName As String
    Dim Email As String
    Dim Phone As String
    Dim StrSql As String

    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Public EmployeeID As String = ""
    Public TerminalID As String = ""
    Public locationid As String = ""

    Public ShiftID As String = ""

    Public LoggOffURL As String = ""

    Public StandingRequisitionOrderURL As String = "StandingRequisitionOrder.aspx"

    Public POURL As String = ""
    Public POURL2 As String = ""

    Public POURL2Display As String = "block"
    'POURL2Display = "none"

    Public POExcelReport As String = ""
    Public HOstReport As String = ""
    Public TrReport As String = ""

    Public Function checktrialusers() As String
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT [CompanyID] ,[AutoPostID]  FROM [Enterprise].[dbo].[NewCompanyRequestMain] where [CompanyID] ='" & Me.CompanyID & "'"
        ' Response.Write(ssql)
        Dim da As New SqlDataAdapter(ssql, constr)
        da.Fill(dt)
        Dim AutoPostID As Integer = 0
        If dt.Rows.Count <> 0 Then
            Try
                AutoPostID = dt.Rows(0)("AutoPostID")
            Catch ex As Exception

            End Try


            Dim dtcheck As New DataTable
            dtcheck = FillGridDetailsExpiere(AutoPostID)
            If dtcheck.Rows.Count <> 0 Then



                Dim DaysPass As Integer = 0
                Dim UpgradedToRegularuser As Boolean = False
                Dim AllowPOS As Boolean = False
                Dim ExtendedUpto As Integer = 30

                Try
                    Dim ACCEPT_TERMS As Boolean = True
                    Try
                        ACCEPT_TERMS = dtcheck.Rows(0)("ACCEPT_TERMS")
                    Catch ex As Exception

                    End Try

                    '  Response.Write("ACCEPT_TERMS:" & ACCEPT_TERMS)

                    If ACCEPT_TERMS = False Then
                        Response.Redirect("TermsAndConditions.aspx?AutoPostID=" & AutoPostID & "&EmployeeID=" & EmployeeID)
                    End If

                Catch ex As Exception
                    Response.Write(ex.Message)
                End Try

            End If

        End If

        Return ""
    End Function

    Public Function FillGridDetailsExpiere(ByVal RequestID As String) As DataTable
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT  *,DATEDIFF(DAY,[ActivationDate], GETDATE()) as 'DaysPass'  FROM [NCRSmall] where RequestID=" & RequestID
        Dim da As New SqlDataAdapter(ssql, constr)
        da.Fill(dt)
        Return dt
    End Function


    Public newmenu As String = ""
    Public newmenubid As String = "<li id='Li150'><a href='InvitationtoBid.aspx'><i class='fa fa-area-chart'></i><span class='title'>Vendor Bids</span></a></li>"
    Public newmenubutton As String = "<li id='Li20'><a href='RequestDynamicButtonList.aspx'><i class='fa fa-area-chart'></i><span class='title'>Requisition Product hot links </span></a></li>"


    Public VendorLiveAvailability As String = "<li id='Li15'><a href='VendorLiveAvailability.aspx'><i class='fa fa-area-chart'></i><span class='title'>Vendor Live Availability</span></a></li>"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            Session("URL") = Request.Url.AbsolutePath & "?" & Request.QueryString.ToString()
        Catch ex As Exception

        End Try

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

        locationid = Session("Locationid")
        TerminalID = Session("TerminalID")

        JsCompanyID = CompanyID
        JSDivisionID = DivisionID
        JSDepartmentID = DepartmentID
        JSUserName = EmployeeID

        If CompanyID.ToUpper() = "JWF" Or CompanyID.ToUpper = "NEWWF" Then
            Inventory.Visible = False
        End If

        populateCompany()

        checktrialusers()

        LoggOffURL = "QFPOSLogin.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&TerminalID=" & TerminalID & "&locationid=" & locationid & "&logoff=1"

        If Me.CompanyID = "SouthFloralsTraining" Or Me.CompanyID = "QuickfloraDemo" Then
            newmenu = "<li id='CreateRequisiton' class='last'><a href='CreateRequisition.aspx'><i class='icon-settings'></i><span class='title'>Create Requisition</span></a></li> "
        Else
            newmenu = ""
        End If


        Dim dt As New DataTable
        Dim startdate As String = ""
        Dim endtdate As String = ""

        Try
            startdate = Request.QueryString("startdate")
            ' HttpContext.Current.Response.Write(startdate)
        Catch ex As Exception

        End Try
        Try
            endtdate = Request.QueryString("endDate")
            '  HttpContext.Current.Response.Write(endtdate)
        Catch ex As Exception

        End Try
        If startdate <> "" And endtdate <> "" Then
            '' InsertNewCartSession(startdate, endtdate)
        End If

        If Me.CompanyID = "DierbergsMarkets,Inc63017" Or Me.CompanyID = "QuickfloraDemo" Then
            POExcelReport = "<li id='Li130'><a    href='POExporttoExcel.aspx'><i class='fa fa-area-chart'></i><span class='title'>PO to Vendor</span></a></li>	"
            HOstReport = "<li id='Li1301'><a    href='ItemHostReport.aspx'><i class='fa fa-area-chart'></i><span class='title'>Host Report</span></a></li>	"
            TrReport = "<li id='Li1302'><a    href='TransferExcelReport.aspx'><i class='fa fa-area-chart'></i><span class='title'>Transfers Export</span></a></li>"
            POURL2Display = "none"
            StandingRequisitionOrderURL = "StandingRequisitionOrderDB.aspx"
        Else
            newmenubutton = ""
            newmenubid = ""
        End If

        POURL = "BPO.aspx?page=1"
        POURL2 = "BPO.aspx?page=2"
        'POURL = "http://bpom.quickflora.com/Web/BPO.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&TerminalID=" & "DEFAULT" & "&locationid=" & locationid & "&EmployeeID=" & EmployeeID & "&Session_SessionID=" & Session.SessionID
        ' POURL2 = "http://bpom.quickflora.com/Web/BPOR.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&TerminalID=" & "DEFAULT" & "&locationid=" & locationid & "&EmployeeID=" & EmployeeID & "&Session_SessionID=" & Session.SessionID

        If CompanyID = "DierbergsMarkets,Inc63017" Or CompanyID = "QuickfloraDemo" Then
            itemsimagesimport.Visible = True
        End If

        Try
            ShiftID = "Shift ID: " & Session("ShiftID")
        Catch ex As Exception

        End Try

        If Me.CompanyID.ToLower = "FMW".ToLower Or Me.CompanyID = "QuickfloraDemo" Then

        Else
            VendorLiveAvailability = ""
        End If


    End Sub


    Public Function InsertNewCartSession(ByVal startDate As String, ByVal endDate As String) As Boolean
        Dim ConnectionString As String = ""
        ConnectionString = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(ConnectionString)
        Dim qry As String
        qry = "INSERT INTO  [NewPOMSession] ([SessionBrowserID] ,[Employeeid] ,[CompanyID] ,[DivisionID] ,[DepartmentID] ,[startDate] ,[endDate]) "
        qry = qry & " values(@SessionID,@EmployeeID,@f1,@f2,@f3,@startDate,@endDate)"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@EmployeeID", SqlDbType.NVarChar, 36)).Value = Me.EmployeeID
            com.Parameters.Add(New SqlParameter("@SessionID", SqlDbType.NVarChar)).Value = Session.SessionID
            com.Parameters.Add(New SqlParameter("@startDate", SqlDbType.NVarChar)).Value = startDate
            com.Parameters.Add(New SqlParameter("@endDate", SqlDbType.NVarChar)).Value = endDate

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




    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim ConnectionString As String = ""
    Dim reader As SqlDataReader

    Sub populateCompany()


        ConnectionString = ConfigurationManager.AppSettings("ConnectionString")
        Dim CommandText As String = "enterprise.spCompanyInformation"

        ' get the connection ready
        Dim myConnection As New SqlConnection(ConnectionString)
        Dim myCommand As New SqlCommand(CommandText, myConnection)
        Dim workParam As New SqlParameter()

        myCommand.CommandType = CommandType.StoredProcedure

        ' Set the input parameter, companyid, divisionid, departmentid
        ' these parameters are set in the sub page_load
        myCommand.Parameters.Add("@CompanyID", SqlDbType.NVarChar).Value = CompanyID
        myCommand.Parameters.Add("@DivisionID", SqlDbType.NVarChar).Value = DivisionID
        myCommand.Parameters.Add("@DepartmentID", SqlDbType.NVarChar).Value = DepartmentID


        ' open the connection
        myConnection.Open()
        reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

        While reader.Read()
            lblCompany.Text = reader(3).ToString()
            lblCompany.DataBind()
        End While
    End Sub

End Class

