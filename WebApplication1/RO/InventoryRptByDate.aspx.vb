Imports System.Data
Imports System.Data.SqlClient

Partial Class Report_InventoryByLocation
    Inherits System.Web.UI.Page


    Public CompanyID As String = ""
    Public DivisionID As String = ""
    Public DepartmentID As String = ""
    Public parameter As String = ""
    Public EmployeeID As String = ""


    'Public Function PopulateImage(ByVal ob As String) As String
    '    Dim DocumentDir As String = ""
    '    Dim img1 As String = ""
    '    Dim ImgName As String = ob.ToLower()
    '    DocumentDir = ConfigurationManager.AppSettings("DocPath")
    '    If (ImgName.Trim() = "") Then

    '        Return "../../images/no_image.gif"

    '    Else

    '        If System.IO.File.Exists(DocumentDir & ImgName.Trim()) Then

    '            Return "../../images/" & ImgName.Trim()

    '        Else
    '            Return "../../images/no_image.gif"
    '        End If




    '    End If


    'End Function


    'Sub BindGrid()
    '    ' Load the name of the stored procedure where our data comes from here into commandtext
    '    Dim CommandText As String = "enterprise.spCompanyInformation"
    '    Dim ConnectionString As String = ""
    '    ConnectionString = EnterpriseCommon.Configuration.ConfigSettings.ConnectionString
    '    ' get the connection ready
    '    Dim myConnection As New SqlConnection(ConnectionString)
    '    Dim myCommand As New SqlCommand(CommandText, myConnection)
    '    Dim workParam As New SqlParameter()

    '    myCommand.CommandType = CommandType.StoredProcedure

    '    ' Set the input parameter, companyid, divisionid, departmentid
    '    ' these parameters are set in the sub page_load
    '    myCommand.Parameters.Add("@CompanyID", SqlDbType.NVarChar).Value = CompanyID
    '    myCommand.Parameters.Add("@DivisionID", SqlDbType.NVarChar).Value = DivisionID
    '    myCommand.Parameters.Add("@DepartmentID", SqlDbType.NVarChar).Value = DepartmentID


    '    ' open the connection
    '    myConnection.Open()

    '    'bind the datasource
    '    DataGrid1.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
    '    DataGrid1.DataBind()


    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            'txtDeliveryDate.Text = Date.Now.Date
            'txtDeliveryDateTO.Text = Date.Now.Date

        End If


        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        Dim company As String = CType(SessionKey("CompanyID"), String)
        Dim UserName As String = CType(SessionKey("EmployeeID"), String)
        Dim DivID As String = CType(SessionKey("DivisionID"), String)
        Dim DeptID As String = CType(SessionKey("DepartmentID"), String)
        CompanyID = company
        DivisionID = DivID
        DepartmentID = DeptID
        EmployeeID = UserName
        ' BindGrid()

        If Not IsPostBack Then

            'SetLocationIDdropdown()
            'BindPaymentandDeliveryList()
        End If


    End Sub


    'Public Sub SetLocationIDdropdown()
    '    '''''''''''''''''
    '    Dim obj As New clsOrder_Location
    '    Dim dt As New Data.DataTable
    '    obj.CompanyID = CompanyID
    '    obj.DivisionID = DivisionID
    '    obj.DepartmentID = DepartmentID
    '    dt = obj.FillLocation
    '    If dt.Rows.Count <> 0 Then
    '        cmblocationid.DataSource = dt
    '        cmblocationid.DataTextField = "LocationName"
    '        cmblocationid.DataValueField = "LocationID"
    '        cmblocationid.DataBind()
    '        'Setdropdown()
    '    Else
    '        cmblocationid.Items.Clear()
    '        Dim item As New ListItem
    '        item.Text = "DEFAULT"
    '        item.Value = "DEFAULT"
    '        cmblocationid.Items.Add(item)
    '    End If
    '    ''''''''''''''''''''




    '    ' Session("OrderLocationid") = cmblocationid.SelectedValue
    'End Sub



    'Public Sub BindPaymentandDeliveryList()
    '    Dim objUser As New DAL.CustomOrder()
    '    Dim dt As New DataTable
    '    dt = objUser.PaymentMethodsList(CompanyID, DepartmentID, DivisionID)

    '    Payment.DataTextField = "PaymentMethodDescription"
    '    Payment.DataValueField = "PaymentMethodID"
    '    Payment.DataSource = dt
    '    Payment.DataBind()



    '    Dim rs As SqlDataReader
    '    rs = objUser.PopulateTransactionTypes(CompanyID, DepartmentID, DivisionID)

    '    drpTransaction.DataTextField = "TransactionDescription"
    '    drpTransaction.DataValueField = "TransactionTypeID"
    '    drpTransaction.DataSource = rs
    '    drpTransaction.DataBind()


    'End Sub




End Class
