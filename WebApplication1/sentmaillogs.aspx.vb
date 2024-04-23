Option Strict Off
Imports System.Data.SqlClient
Imports Microsoft.Office
Imports System.Data
Imports DAL
Imports EnterpriseCommon.Configuration
Imports EnterpriseASPClient.Core
Imports EnterpriseClient.Core


Partial Class EnterpriseASPSystem_CustomCompanySetup_sentmaillogs
    Inherits System.Web.UI.Page

    Dim ConnectionString As String = ""

    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""

    Dim FromDate As String = ""
    Dim ToDate As String = ""
    Dim fieldName As String = ""
    Dim Condition As String = ""
    Dim fieldexpression As String = ""
    Dim AllDate As Integer
    Public SortField As String = ""
    Public SortDirection As String = ""

    Public Property FieldSortDirection() As String
        Get
            Return ViewState("FieldSortDirection")
        End Get
        Set(ByVal value As String)
            ViewState("FieldSortDirection") = value
        End Set
    End Property

    Public Property SortFieldName() As String
        Get
            Return ViewState("SortFieldName")
        End Get
        Set(ByVal value As String)
            ViewState("SortFieldName") = value
        End Set
    End Property


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        '    ' get the connection ready
        CompanyID = SessionKey("CompanyID")
        DivisionID = SessionKey("DivisionID")
        DepartmentID = SessionKey("DepartmentID")


        If Not Page.IsPostBack Then

            Dim dtm As Date
            dtm = Date.Now
            txtDateFrom.Text = dtm.ToString("MM/dd/yyyy")
            txtDateTo.Text = dtm.ToString("MM/dd/yyyy")

            Dim frmdate As Date = Date.Now.Date
            frmdate = frmdate.AddMonths(-1)
            rdOrderDates.Checked = True
            rdAllDates.Checked = False

            txtDateFrom.Text = frmdate.ToString("MM/dd/yyyy")

            BindPaymentGatewayTransactionLogs()

        End If
    End Sub

    Public Sub BindPaymentGatewayTransactionLogs()

        fieldName = drpFieldName.SelectedValue
        Condition = drpCondition.SelectedValue
        fieldexpression = txtSearchExpression.Text.Trim().Replace("'", "''")
        FromDate = txtDateFrom.Text
        ToDate = txtDateTo.Text

        AllDate = 1

        If rdAllDates.Checked Then
            AllDate = 1

        ElseIf rdOrderDates.Checked Then
            AllDate = 2
        End If

        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        '    ' get the connection ready
        CompanyID = SessionKey("CompanyID")
        DivisionID = SessionKey("DivisionID")
        DepartmentID = SessionKey("DepartmentID")

        Dim ds As New DataSet()


        ds = GenratedCustomerStatementDataLogsList(Condition, fieldName, fieldexpression, FromDate, ToDate, AllDate, Me.SortFieldName, Me.FieldSortDirection)
        gridPaymentGatewayTransactionLogs.DataSource = ds
        gridPaymentGatewayTransactionLogs.DataBind()


    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindPaymentGatewayTransactionLogs()
    End Sub



    Public Function GenratedCustomerStatementDataLogsList(ByVal Condition As String, ByVal fieldName As String, ByVal fieldexpression As String, ByVal FromDate As String, ByVal ToDate As String, ByVal AllDate As Integer, ByVal SortField As String, ByVal SortDirection As String) As DataSet


        Dim conString As New SqlConnection
        conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

        Dim myCommand As New SqlCommand("CompaniesEmailTraceLogsList", conString)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
        parameterCompanyID.Value = Me.CompanyID
        myCommand.Parameters.Add(parameterCompanyID)

        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
        parameterDivisionID.Value = Me.DivisionID
        myCommand.Parameters.Add(parameterDivisionID)

        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
        parameterDepartmentID.Value = Me.DepartmentID
        myCommand.Parameters.Add(parameterDepartmentID)

        Dim parameterCondition As New SqlParameter("@Condition", Data.SqlDbType.NVarChar)
        parameterCondition.Value = Condition
        myCommand.Parameters.Add(parameterCondition)


        Dim parameterfieldName As New SqlParameter("@fieldName", Data.SqlDbType.NVarChar)
        parameterfieldName.Value = fieldName
        myCommand.Parameters.Add(parameterfieldName)

        Dim parameterfieldexpression As New SqlParameter("@fieldexpression", Data.SqlDbType.NVarChar)
        parameterfieldexpression.Value = fieldexpression
        myCommand.Parameters.Add(parameterfieldexpression)


        Dim parameterFromDate As New SqlParameter("@FromDate", Data.SqlDbType.NVarChar)
        parameterFromDate.Value = FromDate
        myCommand.Parameters.Add(parameterFromDate)

        Dim parameterToDate As New SqlParameter("@ToDate", Data.SqlDbType.NVarChar)
        parameterToDate.Value = ToDate
        myCommand.Parameters.Add(parameterToDate)


        Dim parameterAllDate As New SqlParameter("@AllDate", Data.SqlDbType.Int)
        parameterAllDate.Value = AllDate
        myCommand.Parameters.Add(parameterAllDate)


        Dim parameterSortField As New SqlParameter("@SortField", Data.SqlDbType.NVarChar)
        parameterSortField.Value = SortField
        myCommand.Parameters.Add(parameterSortField)

        Dim parameterSortDirection As New SqlParameter("@SortDirection", Data.SqlDbType.NVarChar)
        parameterSortDirection.Value = SortDirection
        myCommand.Parameters.Add(parameterSortDirection)


        Dim adapter As New SqlDataAdapter(myCommand)
        Dim ds As New DataSet()
        adapter.Fill(ds)
        conString.Close()

        Return ds


    End Function


    Protected Sub gridPaymentGatewayTransactionLogs_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridPaymentGatewayTransactionLogs.PageIndexChanging
        gridPaymentGatewayTransactionLogs.PageIndex = e.NewPageIndex
        BindPaymentGatewayTransactionLogs()
    End Sub


End Class

