Imports System.Data
Imports System.Data.SqlClient

Partial Class Report3
    Inherits System.Web.UI.Page

    Public CompanyID As String = ""
    Public DivisionID As String = ""
    Public DepartmentID As String = ""

    Public parameter As String = ""

    Public EmployeeID As String = ""

    Public Function PopulateImage(ByVal ob As String) As String
        Dim DocumentDir As String = ""
        Dim img1 As String = ""
        Dim ImgName As String = ob.ToLower()
        DocumentDir = ConfigurationManager.AppSettings("DocPath")
        If (ImgName.Trim() = "") Then

            Return "../../images/no_image.gif"

        Else

            If System.IO.File.Exists(DocumentDir & ImgName.Trim()) Then

                Return "../../images/" & ImgName.Trim()

            Else
                Return "../../images/no_image.gif"
            End If




        End If


    End Function


    Private Sub Report1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        Dim company As String = CType(SessionKey("CompanyID"), String)
        Dim UserName As String = CType(SessionKey("EmployeeID"), String)
        Dim DivID As String = CType(SessionKey("DivisionID"), String)
        Dim DeptID As String = CType(SessionKey("DepartmentID"), String)
        CompanyID = company
        DivisionID = DivID
        DepartmentID = DeptID
        EmployeeID = UserName

        If Not Page.IsPostBack Then
            ' SetLocationIDdropdown()



            Dim tm As DateTime
            tm = DateTime.Now
            tm = tm.AddDays(-20)
            txtDeliveryDate.Text = tm.Month & "/" & tm.Day & "/" & tm.Year
            ' txtstart.Text = tm.Month & "/" & tm.Day & "/" & tm.Year
            tm = tm.AddDays(20)
            txtDeliveryDateTO.Text = tm.Month & "/" & tm.Day & "/" & tm.Year

            BindOrderHeaderList()

        End If

    End Sub

    Dim AllDate As Integer
    Public Sub BindOrderHeaderList()
        Dim objUser As New DAL.CustomOrder()
        If rdAllDates.Checked Then
            AllDate = 1

        ElseIf rdOrderDates.Checked Then
            AllDate = 2

        ElseIf rdDeliveryDates.Checked Then

            AllDate = 3

        End If
        Dim locationid As String = ""
        Try
            locationid = Session("Locationid")
        Catch ex As Exception

        End Try

        Dim strpayment As String = ""
        Dim strDelivery As String = ""


        Dim ds As New DataSet()

        ds = POSOrderSearchList(CompanyID, DepartmentID, DivisionID, "=", "LocationID", locationid, txtDeliveryDate.Text, txtDeliveryDateTO.Text, AllDate, "OrderShipDate", "ASC", strpayment, strDelivery)

        OrderHeaderGrid.DataSource = ds

        OrderHeaderGrid.DataBind()


    End Sub


    Public Function POSOrderSearchList(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal Condition As String, ByVal fieldName As String, ByVal fieldexpression As String, ByVal FromDate As String, ByVal ToDate As String, ByVal AllDate As Integer, ByVal SortField As String, ByVal SortDirection As String, ByVal Payment As String, ByVal Delivery As String) As DataSet

        Dim conString As New SqlConnection
        conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

        Dim myCommand As New SqlCommand("POSPurchaseOrderSearchListitemWise", conString)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        Dim prPayment As New SqlParameter("@Payment", Data.SqlDbType.NVarChar)
        prPayment.Value = Payment
        myCommand.Parameters.Add(prPayment)

        Dim prDelivery As New SqlParameter("@Delivery", Data.SqlDbType.NVarChar)
        prDelivery.Value = Delivery
        myCommand.Parameters.Add(prDelivery)

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
        parameterCompanyID.Value = CompanyID
        myCommand.Parameters.Add(parameterCompanyID)

        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
        parameterDivisionID.Value = DivisionID
        myCommand.Parameters.Add(parameterDivisionID)

        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
        parameterDepartmentID.Value = DepartmentID
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




        If cmblocationid.SelectedValue <> "" Then myCommand.Parameters.AddWithValue("@LocationID", cmblocationid.SelectedValue)


        Dim adapter As New SqlDataAdapter(myCommand)
        Dim ds As New DataSet()
        Try
            adapter.Fill(ds)
        Catch ex As Exception

        End Try

        conString.Close()

        Return ds


    End Function


    Protected Sub OrderHeaderGrid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles OrderHeaderGrid.PageIndexChanging
        OrderHeaderGrid.PageIndex = e.NewPageIndex
        BindOrderHeaderList()
    End Sub


    Private Sub SearchButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SearchButton.Click
        BindOrderHeaderList()
    End Sub
End Class
