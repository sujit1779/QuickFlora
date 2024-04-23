Option Strict Off

Imports System.Data.SqlClient
Imports System.Data
Imports DAL
Imports EnterpriseCommon.Configuration
Imports EnterpriseASPClient.Core
Imports EnterpriseClient.Core
Imports System.Diagnostics
Imports PayPal.Payments.DataObjects
Imports PayPal.Payments.Common
Imports PayPal.Payments.Common.Utility
Imports PayPal.Payments.Transactions
Imports System.Net.Mail
Imports System.Text.RegularExpressions
Imports System.IO
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.Shared
Imports AuthorizeNet

Imports System.Web.Script.Serialization
Imports System.Web.Script.Services
Imports System.Collections.Generic
Partial Class PayrollEmployeesModuleAccessList
    Inherits System.Web.UI.Page

    Public ConnectionString As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim EmployeeID As String = ""
    Dim rs As SqlDataReader
    Public Function returl(ByVal ob As String) As String

        Dim DocumentDir As String = ""
        Dim img1 As String = ""
        Dim ImgName As String = ob.ToLower()
        DocumentDir = ConfigurationManager.AppSettings("DocPath")
        If (ImgName.Trim() = "") Then

            Return "/images/no_image.gif"

        Else

            If System.IO.File.Exists(DocumentDir & ImgName.Trim()) Then

                Return "/images/" & ImgName.Trim()

            Else
                Return "/images/no_image.gif"
            End If




        End If


    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

        'Populating RetailerLogo
        Dim ImageTemp As String = ""

        Dim PopOrderType As New CustomOrder()
        rs = PopOrderType.PopulateCompanyLogo(CompanyID, DepartmentID, DivisionID)
        While (rs.Read())


            ImgRetailerLogo.ImageUrl = "~" & returl(rs("CompanyLogoUrl").ToString())

        End While


        Dim securitycheck As Boolean = False

        Dim dt As New DataTable

        dt = bindgridAccessModulebyemployeeid(EmployeeID)

        Dim n As Integer = 0

        For n = 0 To dt.Rows.Count - 1

            Dim isadmin As Boolean = False

            Try
                isadmin = dt.Rows(n)("IsAdmin")
            Catch ex As Exception

            End Try

            If isadmin = True Then
                securitycheck = True
            End If

        Next

        If securitycheck = False Then
            Response.Redirect("SecurityAcessPermission.aspx?MOD=MA")
        End If

        If Not IsPostBack Then
            PopulateDrops()
            bindgridAccessModule()
        End If




    End Sub


    Public Function bindgridAccessModulebyemployeeid(ByVal emp As String) As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT * FROM [PayrollEmployeesModuleAccess] where   CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2  and EmployeeID=@EmployeeID"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
        com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
        com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
        com.Parameters.Add(New SqlParameter("@EmployeeID", SqlDbType.NVarChar, 36)).Value = EmployeeID

        da.SelectCommand = com
        da.Fill(dt)

        Return dt

    End Function

    Public Sub bindgridAccessModule()
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT * FROM [PayrollEmployeesModuleAccess] where   CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2  "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
        com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
        com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
        da.SelectCommand = com
        da.Fill(dt)

        lblErrorText.Text = dt.Rows.Count
        lblErrorText.Visible = True

        gridAccessModule.DataSource = dt
        gridAccessModule.DataBind()
        gridAccessModule.Visible = True

        Try
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)

        End Try

    End Sub

    Public Function filldrpQuickfloraModules() As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT * FROM [QuickfloraModules] where   CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2  "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            da.SelectCommand = com
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return dt
        End Try

    End Function



    Public Sub PopulateDrops()
        Dim CompanyID As String = ""
        Dim DivisionID As String = ""
        Dim DepartmentID As String = ""



        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")


        '''''''''''''''''

        Dim dt As New Data.DataTable

        dt = filldrpQuickfloraModules()
        If dt.Rows.Count <> 0 Then
            drpQuickfloraModules.DataSource = dt
            drpQuickfloraModules.DataTextField = "ModuleName"
            drpQuickfloraModules.DataValueField = "ModuleID"
            drpQuickfloraModules.DataBind()

            grdModule.DataSource = dt
            grdModule.DataBind()
        End If
        ''''''''''''''''''''

        ''''''''''''''''''''

        Dim PopOrderType As New CustomOrder()
        rs = PopulateEmployees(CompanyID, DepartmentID, DivisionID)

        drpEmployeeID.DataTextField = "EmployeeName"
        drpEmployeeID.DataValueField = "EmployeeID"
        drpEmployeeID.DataSource = rs
        drpEmployeeID.DataBind()

        rs.Close()



    End Sub


    Public Function PopulateEmployees(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As SqlDataReader

        Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        ConString.Open()
        Dim myCommand As New SqlCommand("PopulateEmployees", ConString)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
        parameterCompanyID.Value = CompanyID
        myCommand.Parameters.Add(parameterCompanyID)

        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
        parameterDepartmentID.Value = DepartmentID
        myCommand.Parameters.Add(parameterDepartmentID)

        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
        parameterDivisionID.Value = DivisionID
        myCommand.Parameters.Add(parameterDivisionID)



        Dim rs As SqlDataReader
        rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
        Return rs



    End Function



    Protected Sub btnadd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnadd.Click
        Dim connec As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))

        Dim qry As String
        Dim com As SqlCommand

        For Each row As GridViewRow In grdModule.Rows


            Dim lblAccessModule As Label = row.FindControl("lblAccessModule")

            Dim chk As CheckBox = row.FindControl("chkselect")

            If chk.Checked Then
                qry = "insert into  PayrollEmployeesModuleAccess  (CompanyID,DivisionID,DepartmentID,EmployeeID,AccessModule) values(@CompanyID,@DivisionID,@DepartmentID,@EmployeeID,@AccessModule)"

                com = New SqlCommand(qry, connec)


                com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
                com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
                com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
                com.Parameters.Add(New SqlParameter("@EmployeeID", SqlDbType.NVarChar, 100)).Value = drpEmployeeID.SelectedValue
                com.Parameters.Add(New SqlParameter("@AccessModule", SqlDbType.NVarChar, 100)).Value = lblAccessModule.Text
                Try
                    com.Connection.Open()
                    com.ExecuteNonQuery()
                    com.Connection.Close()
                    chk.Checked = False
                Catch ex As Exception

                End Try

            End If

        Next



        drpEmployeeID.SelectedIndex = -1
        drpQuickfloraModules.SelectedIndex = -1


        bindgridAccessModule()
    End Sub

    Protected Sub gridAccessModule_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gridAccessModule.RowUpdating
        Dim lblEmployeeID As New Label
        lblEmployeeID = CType(gridAccessModule.Rows(e.RowIndex).FindControl("lblEmployeeID"), Label)

        Dim lblAccessModule As New Label
        lblAccessModule = CType(gridAccessModule.Rows(e.RowIndex).FindControl("lblAccessModule"), Label)

        Dim connec As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))

        Dim qry As String
        qry = "delete from  PayrollEmployeesModuleAccess   Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And EmployeeID=@EmployeeID And AccessModule=@AccessModule"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@EmployeeID", SqlDbType.NVarChar, 100)).Value = lblEmployeeID.Text
            com.Parameters.Add(New SqlParameter("@AccessModule", SqlDbType.NVarChar, 100)).Value = lblAccessModule.Text

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()



        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)

        End Try

        bindgridAccessModule()
    End Sub



    Public Function Employeename(ByVal EmployeeID As String) As String
        Dim dt As New DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "Select [EmployeeName] from   [PayrollEmployees] Where CompanyID=@f1  AND   DivisionID =@f2  AND  DepartmentID =@f3  AND [EmployeeID]=@f4"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID.Trim()
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID.Trim()
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID.Trim()
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = EmployeeID

            com.CommandType = CommandType.Text

            Dim da As New SqlDataAdapter(com)

            da.Fill(dt)

            If dt.Rows.Count <> 0 Then
                Dim Ename As String = ""

                Try
                    Ename = dt.Rows(0)(0)
                Catch ex As Exception

                End Try

                Return Ename

            Else
                Return EmployeeID
            End If



        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return EmployeeID
        End Try
        Return EmployeeID
    End Function



    Public Function QuickfloraModules(ByVal ModuleID As String) As String
        Dim dt As New DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "Select [ModuleName] from   [QuickfloraModules] Where CompanyID=@f1  AND   DivisionID =@f2  AND  DepartmentID =@f3  AND [ModuleID]=@f4"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID.Trim()
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID.Trim()
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID.Trim()
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = ModuleID

            com.CommandType = CommandType.Text

            Dim da As New SqlDataAdapter(com)

            da.Fill(dt)

            If dt.Rows.Count <> 0 Then
                Dim Ename As String = ""

                Try
                    Ename = dt.Rows(0)(0)
                Catch ex As Exception

                End Try

                Return Ename

            Else
                Return ModuleID
            End If



        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return ModuleID
        End Try
        Return ModuleID
    End Function


End Class
