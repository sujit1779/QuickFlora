Imports System.Data
Imports System.Data.SqlClient
Partial Class AjaxInventorybylocation
    Inherits System.Web.UI.Page


    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
    'UpdateInventoryByWarehouse

    Public Function UpdateInventoryByWarehouse(ByVal ItemID As String, ByVal LocationID As String) As Boolean
        ' Select InventoryByWarehouse.ItemID,InventoryByWarehouse.LocationID,InventoryByWarehouse.QtyOnHand,InventoryByWarehouse.QtyCommitted
        ',InventoryByWarehouse.QtyOnOrder,InventoryByWarehouse.QtyOnBackorder,InventoryByWarehouse.ReOrderQty        from InventoryByWarehouse
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = " Update [dbo].[InventoryByWarehouse] SET LastCountDate=GETDATE() , [QtyOnHand]=@QtyOnHand,QtyCommitted=@QtyCommitted,QtyOnOrder=@QtyOnOrder,QtyOnBackorder=@QtyOnBackorder,ReOrderQty =@ReOrderQty,AverageCost=@avg   "
        qry = qry & "  where [CompanyID] =@f0 AND [DivisionID] =@f1 AND [DepartmentID] =@f2 AND  [ItemID] =@ItemID AND [LocationID] =@LocationID   "

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        com.CommandType = CommandType.Text
        com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
        com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
        com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
        com.Parameters.Add(New SqlParameter("@ItemID", SqlDbType.NVarChar, 36)).Value = ItemID
        com.Parameters.Add(New SqlParameter("@LocationID", SqlDbType.NVarChar)).Value = LocationID
        com.Parameters.Add(New SqlParameter("@QtyOnHand", SqlDbType.Int)).Value = txtQtyOnHand
        com.Parameters.Add(New SqlParameter("@QtyCommitted", SqlDbType.Int)).Value = txtQtyCommitted
        com.Parameters.Add(New SqlParameter("@QtyOnOrder", SqlDbType.Int)).Value = txtQtyOnOrder
        com.Parameters.Add(New SqlParameter("@QtyOnBackorder", SqlDbType.Int)).Value = txtQtyBackordered
        com.Parameters.Add(New SqlParameter("@ReOrderQty", SqlDbType.Int)).Value = txtQtyReordered
        com.Parameters.Add(New SqlParameter("@avg", SqlDbType.Decimal)).Value = avg
        com.Connection.Open()
        com.ExecuteNonQuery()
        com.Connection.Close()
        Try

            Return True
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False
        End Try
    End Function

    Public Function InsertInventoryByWarehouse(ByVal ItemID As String, ByVal LocationID As String) As Boolean
        ' Select InventoryByWarehouse.ItemID,InventoryByWarehouse.LocationID,InventoryByWarehouse.QtyOnHand,InventoryByWarehouse.QtyCommitted
        ',InventoryByWarehouse.QtyOnOrder,InventoryByWarehouse.QtyOnBackorder,InventoryByWarehouse.ReOrderQty        from InventoryByWarehouse
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = " INSERT INTO [dbo].[InventoryByWarehouse]([CompanyID],[DivisionID],[DepartmentID],WarehouseID,WarehouseBinID,LastCountDate,[ItemID],[LocationID],[QtyOnHand],QtyCommitted,QtyOnOrder,QtyOnBackorder,ReOrderQty)   "
        qry = qry & "  VALUES(@f0,@f1,@f2,'DEFAULT','DEFAULT',GETDATE(),@ItemID,@LocationID,@QtyOnHand,@QtyCommitted,@QtyOnOrder,@QtyOnBackorder,@ReOrderQty) "

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        com.CommandType = CommandType.Text
        com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
        com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
        com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
        com.Parameters.Add(New SqlParameter("@ItemID", SqlDbType.NVarChar, 36)).Value = ItemID
        com.Parameters.Add(New SqlParameter("@LocationID", SqlDbType.NVarChar)).Value = LocationID
        com.Parameters.Add(New SqlParameter("@QtyOnHand", SqlDbType.Int)).Value = txtQtyOnHand
        com.Parameters.Add(New SqlParameter("@QtyCommitted", SqlDbType.Int)).Value = txtQtyCommitted
        com.Parameters.Add(New SqlParameter("@QtyOnOrder", SqlDbType.Int)).Value = txtQtyOnOrder
        com.Parameters.Add(New SqlParameter("@QtyOnBackorder", SqlDbType.Int)).Value = txtQtyBackordered
        com.Parameters.Add(New SqlParameter("@ReOrderQty", SqlDbType.Int)).Value = txtQtyReordered
        com.Connection.Open()
        com.ExecuteNonQuery()
        com.Connection.Close()
        Try

            Return True
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False
        End Try
    End Function

    Public Function DeleteCustomerEmailList(ByVal EmailListID As Integer) As String
        ' Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "Delete From  [CustomerEmailList]  where [EmailListID] = @EmailListID "
        'Values (@f00,@f0,@f1,@f2,@f3,@f31,@f32,@f311,@f322)
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        com.CommandType = CommandType.Text
        Try
            com.Parameters.Add(New SqlParameter("@EmailListID", SqlDbType.Int)).Value = EmailListID
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

    Public Function Getinv() As DataTable

        Dim DT As New DataTable

        Using Connection As New SqlConnection(constr)
            Using Command As New SqlCommand(" Select    InventoryByWarehouse.ItemID ,InventoryItems.ItemName,InventoryByWarehouse.LocationID,InventoryByWarehouse.QtyOnHand,InventoryByWarehouse.QtyCommitted,InventoryByWarehouse.QtyOnOrder,InventoryByWarehouse.QtyOnBackorder,InventoryByWarehouse.ReOrderQty,InventoryByWarehouse.AverageCost        from InventoryByWarehouse Left Outer Join InventoryItems on InventoryByWarehouse.CompanyID = InventoryItems.CompanyID AND InventoryByWarehouse.DivisionID  = InventoryItems.DivisionID AND InventoryByWarehouse.DepartmentID  = InventoryItems.DepartmentID    AND InventoryByWarehouse.ItemID  = InventoryItems.ItemID     Where  InventoryByWarehouse.[CompanyID] =@CompanyID AND InventoryByWarehouse.[DivisionID] = @DivisionID AND InventoryByWarehouse.[DepartmentID] =@DepartmentID    ", Connection)
                Command.CommandType = CommandType.Text

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(DT)
                    Return DT

                Catch ex As Exception

                    Return DT

                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    Public Function Getbyitemidandlocinv(ByVal ItemID As String, ByVal LocationID As String) As DataTable

        Dim DT As New DataTable

        Using Connection As New SqlConnection(constr)
            Using Command As New SqlCommand(" Select  InventoryByWarehouse.ItemID ,InventoryItems.ItemName,InventoryByWarehouse.LocationID,InventoryByWarehouse.QtyOnHand,InventoryByWarehouse.QtyCommitted,InventoryByWarehouse.QtyOnOrder,InventoryByWarehouse.QtyOnBackorder,InventoryByWarehouse.ReOrderQty,InventoryByWarehouse.AverageCost,InventoryByWarehouse.AverageCost        from InventoryByWarehouse Left Outer Join InventoryItems on InventoryByWarehouse.CompanyID = InventoryItems.CompanyID AND InventoryByWarehouse.DivisionID  = InventoryItems.DivisionID AND InventoryByWarehouse.DepartmentID  = InventoryItems.DepartmentID    AND InventoryByWarehouse.ItemID  = InventoryItems.ItemID     Where  InventoryByWarehouse.[CompanyID] =@CompanyID AND InventoryByWarehouse.[DivisionID] = @DivisionID AND InventoryByWarehouse.[DepartmentID] =@DepartmentID    AND InventoryByWarehouse.[ItemID] = @ItemID AND InventoryByWarehouse.[LocationID] =@LocationID    ", Connection)
                Command.CommandType = CommandType.Text

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.Add(New SqlParameter("@ItemID", SqlDbType.NVarChar, 36)).Value = ItemID
                Command.Parameters.Add(New SqlParameter("@LocationID", SqlDbType.NVarChar)).Value = LocationID

                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(DT)
                    Return DT

                Catch ex As Exception

                    Return DT

                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function



    Public Function Getbyitemid(ByVal ItemID As String) As DataTable

        Dim DT As New DataTable

        Using Connection As New SqlConnection(constr)
            Using Command As New SqlCommand(" Select  InventoryByWarehouse.ItemID ,InventoryItems.ItemName,InventoryByWarehouse.LocationID,InventoryByWarehouse.QtyOnHand,InventoryByWarehouse.QtyCommitted,InventoryByWarehouse.QtyOnOrder,InventoryByWarehouse.QtyOnBackorder,InventoryByWarehouse.ReOrderQty,InventoryByWarehouse.AverageCost        from InventoryByWarehouse Left Outer Join InventoryItems on InventoryByWarehouse.CompanyID = InventoryItems.CompanyID AND InventoryByWarehouse.DivisionID  = InventoryItems.DivisionID AND InventoryByWarehouse.DepartmentID  = InventoryItems.DepartmentID    AND InventoryByWarehouse.ItemID  = InventoryItems.ItemID     Where  InventoryByWarehouse.[CompanyID] =@CompanyID AND InventoryByWarehouse.[DivisionID] = @DivisionID AND InventoryByWarehouse.[DepartmentID] =@DepartmentID    AND InventoryByWarehouse.[ItemID] = @ItemID   ", Connection)
                Command.CommandType = CommandType.Text

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.Add(New SqlParameter("@ItemID", SqlDbType.NVarChar, 36)).Value = ItemID
                ' Command.Parameters.Add(New SqlParameter("@LocationID", SqlDbType.NVarChar)).Value = LocationID

                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(DT)
                    Return DT

                Catch ex As Exception

                    Return DT

                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function


    Public Function Getbylocinv(ByVal LocationID As String) As DataTable

        Dim DT As New DataTable

        Using Connection As New SqlConnection(constr)
            Using Command As New SqlCommand(" Select  InventoryByWarehouse.ItemID ,InventoryItems.ItemName,InventoryByWarehouse.LocationID,InventoryByWarehouse.QtyOnHand,InventoryByWarehouse.QtyCommitted,InventoryByWarehouse.QtyOnOrder,InventoryByWarehouse.QtyOnBackorder,InventoryByWarehouse.ReOrderQty,InventoryByWarehouse.AverageCost        from InventoryByWarehouse Left Outer Join InventoryItems on InventoryByWarehouse.CompanyID = InventoryItems.CompanyID AND InventoryByWarehouse.DivisionID  = InventoryItems.DivisionID AND InventoryByWarehouse.DepartmentID  = InventoryItems.DepartmentID   AND InventoryByWarehouse.ItemID  = InventoryItems.ItemID      Where  InventoryByWarehouse.[CompanyID] =@CompanyID AND InventoryByWarehouse.[DivisionID] = @DivisionID AND InventoryByWarehouse.[DepartmentID] =@DepartmentID  AND InventoryByWarehouse.[LocationID] =@LocationID    ", Connection)
                Command.CommandType = CommandType.Text

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                ' Command.Parameters.Add(New SqlParameter("@ItemID", SqlDbType.NVarChar, 36)).Value = ItemID
                Command.Parameters.Add(New SqlParameter("@LocationID", SqlDbType.NVarChar)).Value = LocationID

                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(DT)
                    Return DT

                Catch ex As Exception

                    Return DT

                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    '[CompanyID] [nvarchar](36) Not NULL,
    '[DivisionID] [nvarchar](36) Not NULL,
    '[DepartmentID] [nvarchar](36) Not NULL,
    '[ItemID] [nvarchar](36) Not NULL,
    '[QtyOnHandAdded] [int] NULL,
    '[AddDate] [datetime] NULL,
    '[LocationID] [nvarchar](50) Not NULL,
    '[VendorID] [nvarchar](50) Not NULL
    '[EmployeeID]
    '[InventoryByWarehouseStockAddLog]

    Dim txtQtyOnHand As Integer = 0
    Dim txtQtyCommitted As Integer = 0
    Dim txtQtyOnOrder As Integer = 0
    Dim txtQtyBackordered As Integer = 0
    Dim txtQtyReordered As Integer = 0
    Dim avg As Decimal = 0

    Public Function InsertInventoryByWarehouselogstoaddstock(ByVal ItemID As String, ByVal LocationID As String, ByVal QtyOnHand As String, ByVal VendorID As String) As Boolean
        ' Select InventoryByWarehouse.ItemID,InventoryByWarehouse.LocationID,InventoryByWarehouse.QtyOnHand,InventoryByWarehouse.QtyCommitted
        ',InventoryByWarehouse.QtyOnOrder,InventoryByWarehouse.QtyOnBackorder,InventoryByWarehouse.ReOrderQty        from InventoryByWarehouse
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = " INSERT INTO [dbo].[InventoryByWarehouseStockAddLog]([CompanyID],[DivisionID],[DepartmentID],AddDate,[ItemID],[LocationID],[QtyOnHandAdded],EmployeeID,VendorID)   "
        qry = qry & "  VALUES(@f0,@f1,@f2,GETDATE(),@ItemID,@LocationID,@QtyOnHand,@EmployeeID,@VendorID) "

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        com.CommandType = CommandType.Text
        com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
        com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
        com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
        com.Parameters.Add(New SqlParameter("@ItemID", SqlDbType.NVarChar, 36)).Value = ItemID
        com.Parameters.Add(New SqlParameter("@LocationID", SqlDbType.NVarChar)).Value = LocationID
        com.Parameters.Add(New SqlParameter("@QtyOnHand", SqlDbType.Int)).Value = QtyOnHand
        Dim EmployeeID As String = ""
        Try
            EmployeeID = Session("EmployeeID")
        Catch ex As Exception

        End Try
        com.Parameters.Add(New SqlParameter("@EmployeeID", SqlDbType.NVarChar)).Value = EmployeeID
        com.Parameters.Add(New SqlParameter("@VendorID", SqlDbType.NVarChar)).Value = VendorID
        com.Connection.Open()
        com.ExecuteNonQuery()
        com.Connection.Close()
        Try

            Return True
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False
        End Try
    End Function


    Public Function AddInventoryByWarehouse(ByVal ItemID As String, ByVal LocationID As String, ByVal QtyOnHand As String, ByVal Vendor As String) As Boolean
        ' Select InventoryByWarehouse.ItemID,InventoryByWarehouse.LocationID,InventoryByWarehouse.QtyOnHand,InventoryByWarehouse.QtyCommitted
        ',InventoryByWarehouse.QtyOnOrder,InventoryByWarehouse.QtyOnBackorder,InventoryByWarehouse.ReOrderQty        from InventoryByWarehouse
        Try
            InsertInventoryByWarehouselogstoaddstock(ItemID, LocationID, QtyOnHand, Vendor)
        Catch ex As Exception

        End Try
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = " Update [dbo].[InventoryByWarehouse] SET LastCountDate=GETDATE() , [QtyOnHand]= QtyOnHand + @QtyOnHand   "
        qry = qry & "  where [CompanyID] =@f0 AND [DivisionID] =@f1 AND [DepartmentID] =@f2 AND  [ItemID] =@ItemID AND [LocationID] =@LocationID   "

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        com.CommandType = CommandType.Text
        com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
        com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
        com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
        com.Parameters.Add(New SqlParameter("@ItemID", SqlDbType.NVarChar, 36)).Value = ItemID
        com.Parameters.Add(New SqlParameter("@LocationID", SqlDbType.NVarChar)).Value = LocationID
        com.Parameters.Add(New SqlParameter("@QtyOnHand", SqlDbType.Int)).Value = QtyOnHand

        com.Connection.Open()
        com.ExecuteNonQuery()
        com.Connection.Close()
        Try

            Return True
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False
        End Try
    End Function



    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")


        Dim txtItemID As String = ""
        Dim location As String = ""

        Dim txtItemID2 As String = ""
        Dim txtQtyOnHand2 As String = ""
        Dim txtlocation As String = ""
        Dim txtvendor As String = ""
        Try
            txtItemID2 = Request.Form("txtItemID2")
            txtlocation = Request.Form("txtlocation")
            txtQtyOnHand2 = Request.Form("txtQtyOnHand2")
            txtvendor = Request.Form("txtvendor")
            If txtItemID2.Trim <> "" And txtlocation.Trim <> "" And txtQtyOnHand2.Trim <> "" Then
                AddInventoryByWarehouse(txtItemID2, txtlocation, txtQtyOnHand2, txtvendor)
            End If
        Catch ex As Exception

        End Try

        Try
            txtItemID = Request.Form("txtItemID")
            location = Request.Form("location")
            txtQtyOnHand = Request.Form("txtQtyOnHand")
            txtQtyCommitted = Request.Form("txtQtyCommitted")
            txtQtyOnOrder = Request.Form("txtQtyOnOrder")
            txtQtyBackordered = Request.Form("txtQtyBackordered")
            txtQtyReordered = Request.Form("txtQtyReordered")
            avg = Request.Form("avg")

            If txtItemID.Trim <> "" And location.Trim <> "" Then
                Dim dtm As New DataTable
                dtm = Getbyitemidandlocinv(txtItemID, location)
                If dtm.Rows.Count > 0 Then
                    UpdateInventoryByWarehouse(txtItemID, location)
                Else
                    InsertInventoryByWarehouse(txtItemID, location)
                End If

            End If

        Catch ex As Exception

        End Try


        'Try
        '    EmailListID = Request.QueryString("EmailListID")
        '    If EmailListID <> 0 And IsNumeric(EmailListID) Then
        '        DeleteCustomerEmailList(EmailListID)
        '    End If
        'Catch ex As Exception

        'End Try


        ''   .ItemID, .LocationID, .QtyOnHand, .QtyCommitted,
        ' .QtyOnOrder,  .QtyOnBackorder,  .ReOrderQty     
        Response.ContentType = "application/json"
        Response.Clear()
        Dim JSON As String = ""
        JSON = JSON + "["
        Dim dt As New DataTable

        Dim ItemID As String = ""
        Dim LocationID As String = ""
        Try
            LocationID = Request.QueryString("LocationID")
            ItemID = Request.QueryString("ItemID")
            Try
                ItemID = ItemID.Trim
            Catch ex As Exception

            End Try
            Try
                LocationID = LocationID.Trim
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
        If ItemID = "" And LocationID = "" Then
            dt = Getinv()
        Else
            If ItemID <> "" And LocationID <> "" Then
                dt = Getbyitemidandlocinv(ItemID, LocationID)
            Else

                If ItemID <> "" Then
                    dt = Getbyitemid(ItemID)
                Else
                    If LocationID <> "" Then
                        dt = Getbylocinv(LocationID)
                    Else
                        dt = Getbyitemidandlocinv(ItemID, LocationID)
                    End If
                End If

            End If


        End If


        Dim n As Integer
        For n = 0 To dt.Rows.Count - 1
            If n = 0 Then
            Else
                JSON = JSON + "  , "
            End If
            JSON = JSON + "  {"
            JSON = JSON + " ""ItemID"": """ + dt.Rows(n)("ItemID").ToString().Replace(",", "").Replace(":", "").Replace("}", "").Replace("{", "").Replace("""", "").Replace("'", "") + """ "
            JSON = JSON + ", ""ItemName"": """ + dt.Rows(n)("ItemName").ToString().Replace(",", "").Replace(":", "").Replace("}", "").Replace("""", "").Replace("'", "") + """ "
            JSON = JSON + ", ""LocationID"": """ + dt.Rows(n)("LocationID").ToString() + """ "
            JSON = JSON + ", ""QtyOnHand"": """ + dt.Rows(n)("QtyOnHand").ToString() + """ "
            JSON = JSON + ", ""QtyCommitted"": """ + dt.Rows(n)("QtyCommitted").ToString() + """ "
            JSON = JSON + ", ""QtyOnOrder"": """ + dt.Rows(n)("QtyOnOrder").ToString() + """ "
            JSON = JSON + ", ""QtyOnBackorder"": """ + dt.Rows(n)("QtyOnBackorder").ToString() + """ "
            JSON = JSON + ", ""ReOrderQty"": """ + dt.Rows(n)("ReOrderQty").ToString() + """ "
            JSON = JSON + ", ""AverageCost"": """ + dt.Rows(n)("AverageCost").ToString() + """ "
            JSON = JSON + "  }"
        Next


        JSON = JSON + "  ]"
        Response.Write(JSON)
        Response.End()

    End Sub

End Class
