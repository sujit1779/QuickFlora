Option Strict Off
Imports System.Data.SqlClient
Imports Microsoft.Office
Imports System.Data
Imports DAL
Imports EnterpriseCommon.Configuration
Imports EnterpriseASPClient.Core
Imports EnterpriseClient.Core

Partial Class EnterpriseASPSystem_CustomCompanySetup_SentMailsDetails
    Inherits System.Web.UI.Page

    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim EmailID As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim filters As EnterpriseCommon.Core.FilterSet
        filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
        CompanyID = filters!CompanyID
        DivisionID = filters!DivisionID
        DepartmentID = filters!DepartmentID


        If Not Page.IsPostBack Then
            EmailID = Request.QueryString("EmailID")
            Dim dt As New Data.DataTable
            dt = SentMailsDetails()

            If dt.Rows.Count <> 0 Then
                txtfrom.Text = dt.Rows(0)("From_Email")
                txtto.Text = dt.Rows(0)("To_Email")
                txtcc.Text = dt.Rows(0)("CC_Email")
                txtSubject.Text = dt.Rows(0)("Email_Subject")
                txtMessage.InnerHtml = dt.Rows(0)("Email_Body")
            End If

        End If
    End Sub


    Public Function SentMailsDetails() As DataTable


        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from [CompaniesEmailTrace] where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and EmailID=@EmailID"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@EmailID", SqlDbType.BigInt)).Value = Me.EmailID

            da.SelectCommand = com
            da.Fill(dt)

            Return dt
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return dt
        End Try


    End Function




End Class
