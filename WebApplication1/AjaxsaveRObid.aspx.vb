Imports System.Data
Imports System.Data.SqlClient
Imports DAL

Partial Class AjaxsaveRObid
    Inherits System.Web.UI.Page


    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim EmployeeID As String = ""
    Dim LocationID As String = ""
    Dim ItemID As String = ""
    Public result As String = ""
    Dim ShipDate As String = ""

    '  CREATE TABLE [dbo].[InvitationToBid](
    '[CompanyID] [nvarchar](36) Not NULL,
    '[DivisionID] [nvarchar](36) Not NULL,
    '[DepartmentID] [nvarchar](36) Not NULL,
    '[ItemID] [nvarchar](36) Not NULL,
    'ShipDate [datetime] NULL,
    '[LocationID] [nvarchar](50) Not NULL,
    '[BidQty] [int] NULL,
    'Price money
    ')

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")


        'Response.Clear()
        'Response.Write(Request.QueryString("ItemID"))
        'Response.End()

        'Label1.Text = Request.QueryString("ItemID")

        If Not Request.QueryString("ItemID") = Nothing Then

            VendorID = Request.QueryString("VendorID")
            ItemID = Request.QueryString("ItemID")
            LocationID = Request.QueryString("LocationID")
            ShipDate = Request.QueryString("ShipDate")

            'Dim BidQty As String = ""
            'BidQty = Request.QueryString("BidQty")
            'Dim Price As String = ""
            'Price = Request.QueryString("Price")

            Dim name As String = ""
            name = Request.QueryString("name")

            Dim value As String = ""
            value = Request.QueryString("value")


            Dim dt As New DataTable
            dt = SelectProductData()
            If dt.Rows.Count = 0 Then
                InsertDetails()
                Update(name, value)
            Else
                Update(name, value)
            End If

            Response.Clear()
            Response.Write(ItemID)
            Response.End()

        End If

    End Sub


    Public Function Update(ByVal name As String, ByVal value As String) As Boolean

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "Update InvitationToBid SET   " & name & "= @value   Where VendorID=@VendorID AND ShipDate=@ShipDate AND  LocationID = @LocationID and   ItemID = @ItemID and CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2  "
        'Values (@f00,@f0,@f1,@f2,@f3,@f31,@f32,@f311,@f322)
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)

        com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@ItemID", SqlDbType.NVarChar, 36)).Value = ItemID
            com.Parameters.Add(New SqlParameter("@LocationID", SqlDbType.NVarChar, 36)).Value = Me.LocationID
            com.Parameters.Add(New SqlParameter("@VendorID", SqlDbType.NVarChar, 36)).Value = Me.VendorID
            com.Parameters.Add(New SqlParameter("@ShipDate", SqlDbType.Date)).Value = Me.ShipDate

            com.Parameters.Add(New SqlParameter("@value", SqlDbType.NVarChar)).Value = value

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

        Return True
        Try
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function


    '   CREATE TABLE [dbo].[InventoryByGrowerAvailability](
    '[CompanyID] [nvarchar](36) Not NULL,
    '[DivisionID] [nvarchar](36) Not NULL,
    '[DepartmentID] [nvarchar](36) Not NULL,
    '[ItemID] [nvarchar](36) Not NULL,
    '[LocationID] [nvarchar](50) Not NULL,
    '[QtyOnHand] [int] NULL,
    'Price money,
    '   [ArrivalDate] [datetime] NULL,
    'startavailabledate  [datetime] NULL,
    'endavailabledate  [datetime] NULL,
    '[EmployeeID] [nvarchar](50) Not NULL,
    Dim VendorID As String = ""
    Public Function SelectProductData() As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT * FROM [InvitationToBid] where VendorID=@VendorID AND ShipDate=@ShipDate AND  LocationID =@LocationID  AND ItemID = @ItemID and CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2  "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
        com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
        com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
        com.Parameters.Add(New SqlParameter("@ItemID", SqlDbType.NVarChar, 36)).Value = ItemID
        com.Parameters.Add(New SqlParameter("@LocationID", SqlDbType.NVarChar, 36)).Value = Me.LocationID
        com.Parameters.Add(New SqlParameter("@VendorID", SqlDbType.NVarChar, 36)).Value = Me.VendorID
        com.Parameters.Add(New SqlParameter("@ShipDate", SqlDbType.Date)).Value = Me.ShipDate
        da.SelectCommand = com
        da.Fill(dt)

        Try
        Catch ex As Exception

        End Try

        Return dt
    End Function

    'INSERT INTO [Enterprise].[dbo].[InvitationToBid]
    '       ([CompanyID]
    '       ,[DivisionID]
    '       ,[DepartmentID]
    '       ,[ItemID]
    '       ,[ShipDate]
    '       ,[LocationID]
    '       ,[BidQty]
    '       ,[Price]
    '       ,[VendorID])

    Public Function InsertDetails() As Integer

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String

        If Me.ItemID <> "" Then
            qry = " insert into InvitationToBid ([CompanyID] ,[DivisionID] ,[DepartmentID] ,[ItemID] ,[LocationID],ShipDate,VendorID,BidQty,Price ) " _
             & " values(@CompanyID ,@DivisionID ,@DepartmentID ,@ItemID ,@LocationID,@ShipDate,@VendorID,0,0) "

            Dim com As SqlCommand
            com = New SqlCommand(qry, connec)

            com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@ItemID", SqlDbType.NVarChar, 36)).Value = ItemID
            com.Parameters.Add(New SqlParameter("@LocationID", SqlDbType.NVarChar, 36)).Value = Me.LocationID
            com.Parameters.Add(New SqlParameter("@VendorID", SqlDbType.NVarChar, 36)).Value = Me.VendorID
            com.Parameters.Add(New SqlParameter("@ShipDate", SqlDbType.Date)).Value = Me.ShipDate


            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()



        End If

        Dim InLineNumber As Integer = 0


        Return InLineNumber
    End Function






End Class
