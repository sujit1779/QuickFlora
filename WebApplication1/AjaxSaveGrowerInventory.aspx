<%@ Page Language="VB" EnableViewState="false"  %>

<%@Import Namespace="System.Data"%>
<%@Import Namespace="System.Data.SqlClient"%>
<%@Import Namespace="System.Configuration"%> 

<script runat="server">


    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim EmployeeID As String = ""
    Dim LocationID As String = ""
    Dim ItemID As String = ""
    Public result As String = ""

    Dim txtDateTo As String = ""
    Dim txtstart As String = ""
    Dim txtend As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")


        'txtDateTo 
        'txtstart 
        'txtend 

        'Response.Clear()
        'Response.Write(Request.QueryString("ItemID"))
        'Response.Write(Request.QueryString("CompanyID"))
        'Response.End()

        If Not Request.QueryString("ItemID") = Nothing Then


            Try
                txtDateTo = Request.QueryString("txtDateTo")
            Catch ex As Exception

            End Try
            Try
                txtstart = Request.QueryString("txtstart")
            Catch ex As Exception

            End Try
            Try
                txtend = Request.QueryString("txtend")
            Catch ex As Exception

            End Try

            ItemID = Request.QueryString("ItemID")
            LocationID = Request.QueryString("LocationID")

            'Dim ArrivalDate As String = ""
            'ArrivalDate = Request.QueryString("ArrivalDate")

            'Dim Startdate As String = ""
            'Startdate = Request.QueryString("Startdate")

            'Dim Endate As String = ""
            'Endate = Request.QueryString("Endate")

            Dim name As String = ""
            name = Request.QueryString("name")

            Dim value As String = ""
            value = Request.QueryString("value")


            Dim dt As New DataTable
            dt = SelectProductData()
            Response.Write(dt.Rows.Count)
            If dt.Rows.Count = 0 Then
                InsertDetails()
                Update(name, value)
                'Update("ArrivalDate", txtDateTo)
                'Update("endavailabledate", txtend)
                'Update("startavailabledate", txtstart)
            Else
                Update(name, value)
            End If
            '   [ArrivalDate] [datetime] NULL,
            'startavailabledate  [datetime] NULL,
            'endavailabledate  [datetime] NULL,
            


            Response.Clear()
            Response.Write(ItemID)
            Response.End()

        End If

    End Sub


    Public Function Update(ByVal name As String, ByVal value As String) As Boolean

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "Update InventoryByGrowerAvailability SET   " & name & "= @value   Where   startavailabledate =@startavailabledate  AND   endavailabledate =@endavailabledate  AND   ArrivalDate =@ArrivalDate  AND  LocationID = @LocationID and  ItemID = @ItemID and CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2  "
        'Values (@f00,@f0,@f1,@f2,@f3,@f31,@f32,@f311,@f322)
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)

        com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
        com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
        com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
        com.Parameters.Add(New SqlParameter("@ItemID", SqlDbType.NVarChar, 36)).Value = ItemID
        com.Parameters.Add(New SqlParameter("@LocationID", SqlDbType.NVarChar, 36)).Value = Me.LocationID
        com.Parameters.Add(New SqlParameter("@value", SqlDbType.NVarChar)).Value = value
        com.Parameters.Add(New SqlParameter("@ArrivalDate", SqlDbType.NVarChar)).Value = Me.txtDateTo
        com.Parameters.Add(New SqlParameter("@endavailabledate", SqlDbType.NVarChar)).Value = Me.txtend
        com.Parameters.Add(New SqlParameter("@startavailabledate", SqlDbType.NVarChar)).Value = Me.txtstart


        Try
            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()
            Return True
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

    Public Function SelectProductData() As DataTable
        'Update("ArrivalDate", txtDateTo)
        'Update("endavailabledate", txtend)
        'Update("startavailabledate", txtstart)
        
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT * FROM [InventoryByGrowerAvailability] where   startavailabledate =@startavailabledate  AND   endavailabledate =@endavailabledate  AND   ArrivalDate =@ArrivalDate  AND LocationID =@LocationID  AND ItemID = @ItemID and CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2  "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
        com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
        com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
        com.Parameters.Add(New SqlParameter("@ItemID", SqlDbType.NVarChar, 36)).Value = ItemID
        com.Parameters.Add(New SqlParameter("@LocationID", SqlDbType.NVarChar, 36)).Value = Me.LocationID
        com.Parameters.Add(New SqlParameter("@ArrivalDate", SqlDbType.NVarChar)).Value = Me.txtDateTo
        com.Parameters.Add(New SqlParameter("@endavailabledate", SqlDbType.NVarChar)).Value = Me.txtend
        com.Parameters.Add(New SqlParameter("@startavailabledate", SqlDbType.NVarChar)).Value = Me.txtstart
        
        
        da.SelectCommand = com
        da.Fill(dt)

        Try
        Catch ex As Exception

        End Try

        Return dt
    End Function


    Public Function InsertDetails() As Integer
        'Update("ArrivalDate", txtDateTo)
        'Update("endavailabledate", txtend)
        'Update("startavailabledate", txtstart)
        
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String

        If Me.ItemID <> "" Then
            qry = " insert into InventoryByGrowerAvailability ([CompanyID] ,[DivisionID] ,[DepartmentID]  ,[ItemID],[LocationID],EmployeeID ,startavailabledate, endavailabledate ,ArrivalDate  ) " _
             & " values(@CompanyID ,@DivisionID ,@DepartmentID ,@ItemID ,@LocationID,@EmployeeID,@startavailabledate,@endavailabledate,@ArrivalDate ) "

            Dim com As SqlCommand
            com = New SqlCommand(qry, connec)

            com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@ItemID", SqlDbType.NVarChar, 36)).Value = ItemID
            com.Parameters.Add(New SqlParameter("@LocationID", SqlDbType.NVarChar, 36)).Value = Me.LocationID
            com.Parameters.Add(New SqlParameter("@EmployeeID", SqlDbType.NVarChar, 36)).Value = Me.EmployeeID
            com.Parameters.Add(New SqlParameter("@ArrivalDate", SqlDbType.NVarChar)).Value = Me.txtDateTo
            com.Parameters.Add(New SqlParameter("@endavailabledate", SqlDbType.NVarChar)).Value = Me.txtend
            com.Parameters.Add(New SqlParameter("@startavailabledate", SqlDbType.NVarChar)).Value = Me.txtstart
            
            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()



        End If

        Dim InLineNumber As Integer = 0


        Return InLineNumber
    End Function



</script>