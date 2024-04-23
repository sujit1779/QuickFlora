Imports System.Data.SqlClient

Imports System.Data
Imports DAL
Imports EnterpriseCommon.Configuration
Imports EnterpriseASPClient.Core
Imports EnterpriseClient.Core


Partial Class QFPOSLogin
    Inherits System.Web.UI.Page

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
    Dim EmployeeID As String = ""
    Public TerminalID As String = ""
    Public locationid As String = ""
    Public FLORICA As String = "FLORICA Sign In"
    Public CSScls As String = "alert alert-danger display-hide"


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CompanyID = Request.QueryString("CompanyID")
        DivisionID = Request.QueryString("DivisionID")
        DepartmentID = Request.QueryString("DepartmentID")
        TerminalID = Request.QueryString("TerminalID")
        locationid = Request.QueryString("locationid")

        lbllocationid.Text = locationid
        lblTerminalID.Text = TerminalID

        Try
            If Request.QueryString("ITB") = "True" Then
                FLORICA = "ITB Sign In"
            End If
        Catch ex As Exception

        End Try

        JsCompanyID = CompanyID
        JSDivisionID = DivisionID
        JSDepartmentID = DepartmentID
        JSUserName = "Login"

        If Not IsPostBack Then
            Session("ShiftID") = Nothing
            Session("EmployeeID") = Nothing
        End If
        

        Session("CompanyID") = CompanyID
 
        Session("DivisionID") = DivisionID
 
        Session("DepartmentID") = DepartmentID

        TBLogin.Attributes.Add("autocomplete", "off")
        TBLogin.Attributes.Add("placeholder", "Username")

        TBPassword.Attributes.Add("autocomplete", "off")
        TBPassword.Attributes.Add("placeholder", "Password")

        Dim GMT As Integer
        ConnectionString = ConfigurationManager.AppSettings("ConnectionString")
        Dim commandText As String = "enterprise.GetRetailerCompanyGMT"
        Dim myConnection As New SqlConnection(ConnectionString)
        Dim myCommand As New SqlCommand(commandText, myConnection)
        Dim workParam As New SqlParameter()
        myCommand.CommandType = CommandType.StoredProcedure

        myCommand.Parameters.Add("@CompanyID", SqlDbType.NVarChar).Value = CompanyID
        myCommand.Parameters.Add("@DepartmentID", SqlDbType.NVarChar).Value = DepartmentID
        myCommand.Parameters.Add("@DivisionID", SqlDbType.NVarChar).Value = DivisionID

        myConnection.Open()
        reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
        If reader.HasRows() = True Then
            While reader.Read()
                Try
                    GMT = Convert.ToInt16(reader("GMTOffset").ToString())
                Catch ex As Exception
                    GMT = 0
                End Try

            End While

            ' lblTime.Text = DateTime.UtcNow.AddHours(GMT).ToShortTimeString()
            'lblDate.Text = DateTime.UtcNow.AddHours(GMT).ToShortDateString()
        Else
            ' lblTime.Text = DateTime.Now.ToShortTimeString()
            ' lblDate.Text = DateTime.Now.ToShortDateString()
        End If
        myConnection.Close()

        populateCompany()


        If IsPostBack Then

            If Login() = True Then
                Dim filters As New EnterpriseCommon.Core.FilterSet()
                filters!CompanyID = CompanyID
                filters!DivisionID = DivisionID
                filters!DepartmentID = DepartmentID
                filters!EmployeeID = TBLogin.Text

                Session!SessionFilters = filters
                Dim SessionKey As Hashtable = New Hashtable
                SessionKey("CompanyID") = CompanyID
                SessionKey("DivisionID") = DivisionID
                SessionKey("DepartmentID") = DepartmentID
                SessionKey("EmployeeID") = TBLogin.Text

                SessionKey("Locationid") = locationid


                Session("EmployeeID") = TBLogin.Text
                Session("Locationid") = locationid



                If Me.CompanyID = "DierbergsMarkets,Inc63017" Or Me.CompanyID = "QuickfloraDemo" Then
                    Dim _EmployeeID As String = ""
                    Try
                        _EmployeeID = Session("EmployeeID")
                    Catch ex As Exception

                    End Try
                    If _EmployeeID <> "" Then
                        Dim newlocation As String = ""
                        Try
                            newlocation = SelectLocationIDbYeMPLOYEE(_EmployeeID)
                        Catch ex As Exception

                        End Try
                        If newlocation <> "" Then
                            Session("Locationid") = newlocation
                            SessionKey("Locationid") = newlocation
                        End If
                    End If

                End If


                Session("SessionKey") = SessionKey

                'Response.Redirect("Default.aspx")
                If EmployeeTypeID = "Grower" Then
                    Session("Grower") = "True"
                    OldCommon.Redirect("InventoryForm.aspx")
                End If
                If EmployeeTypeID = "Vendor" Then
                    Session("Vendor") = "True"
                    OldCommon.Redirect("RequisitionBid.aspx")
                End If
                OldCommon.Redirect("home.aspx")

            Else

                CSScls = "alert alert-danger display-box"

            End If

            



        End If


    End Sub


    Public Function SelectLocationIDbYeMPLOYEE(ByVal EmployeeID As String) As String
        Dim dt As New DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "SELECT  Isnull(PayrollEmployees.LocationID,'')  FROM PayrollEmployees Where PayrollEmployees.CompanyID= @CompanyID  and PayrollEmployees.DivisionID=@DivisionID and PayrollEmployees.DepartmentID= @DepartmentID and PayrollEmployees.EmployeeID=@EmployeeID "

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID.Trim()
            com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = Me.DivisionID.Trim()
            com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID.Trim()
            com.Parameters.Add(New SqlParameter("@EmployeeID", SqlDbType.NVarChar, 36)).Value = EmployeeID

            com.CommandType = CommandType.Text

            Dim da As New SqlDataAdapter(com)

            da.Fill(dt)

            Return dt.Rows(0)(0)

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return ""
        End Try
        Return ""
    End Function




    Dim EmployeeTypeID As String = ""

    Function Login() As Boolean

 
        ' get login and password
        Dim LoginName As String = TBLogin.Text
        Dim Password As String = TBPassword.Text
         
        Session("TerminalID") = TerminalID
        Password = CryptographyRijndael.EncryptionRijndael.RijndaelEncode(TBPassword.Text, LoginName.ToLower() & CompanyID.ToLower())

        ConnectionString = ConfigurationManager.AppSettings("ConnectionString")
        Dim myConnection As New SqlConnection(ConnectionString)
        ' check credentials
        Dim myCommand As New SqlCommand( _
          "SELECT EmployeeID,LocationID,EmployeeTypeID FROM PayrollEmployees WHERE EmployeeID=@EmployeeLogin " & _
          "AND CompanyID = @CompanyID AND DivisionID = @DivisionID AND DepartmentID = @DepartmentID " & _
          "AND EmployeePassword = @EmployeePassword", myConnection)

        myCommand.Parameters.Add("@CompanyID", SqlDbType.NVarChar).Value = CompanyID
        myCommand.Parameters.Add("@DivisionID", SqlDbType.NVarChar).Value = DivisionID
        myCommand.Parameters.Add("@DepartmentID", SqlDbType.NVarChar).Value = DepartmentID
        myCommand.Parameters.Add("@EmployeeLogin", SqlDbType.NVarChar).Value = LoginName
        myCommand.Parameters.Add("@EmployeePassword", SqlDbType.NVarChar).Value = Password

        Try
            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(myCommand)
            da.Fill(dt)

 
            If dt.Rows.Count <> 0 Then

                EmployeeID = dt.Rows(0)(0)
                Session("EmployeeID") = EmployeeID
                TBLogin.Text = EmployeeID

                If CompanyID.ToLower = "mccarthyg" Then
                    locationid = dt.Rows(0)(1)
                End If

                Try
                    EmployeeTypeID = dt.Rows(0)("EmployeeTypeID")
                Catch ex As Exception

                End Try

                Return True

            End If
        Catch


        End Try
        Session("EmployeeID") = Nothing
        Return False
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
