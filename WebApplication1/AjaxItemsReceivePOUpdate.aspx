<%@ Page Language="VB" EnableViewState="false"  %>

<%@Import Namespace="System.Data"%>
<%@Import Namespace="System.Data.SqlClient"%>
<%@Import Namespace="System.Configuration"%> 

<script runat="server">

    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim EmployeeID As String = ""
    Dim PurchaseNumber As String = ""
    Dim ItemID As String = ""
    Dim Description As String = ""
    Dim Comments As String = ""
    Dim Color As String = ""

    Dim OrderQty As Integer = 1
    Dim ItemUOM As String = ""
    Dim Pack As Integer = 1
    Dim Units As Integer = 1

    Dim ItemUnitPrice As Decimal = 0
    Dim SubTotal As Decimal = 0
    Dim Total As Decimal = 0

    Dim inlineno As String = "'"

    Public result As String = ""

    Dim RPack As String = "'"

    Public RQty As String = ""

    '"RPack=" + RPack + "&RQty=" + RQty +

    Public LocationID As String = ""
    Dim POReceivedDate As String = ""

    '@PurchaseNumber, @ItemID, @Description,@Color, @OrderQty, @ItemUOM,@Pack,
    '@Units,@ItemUnitPrice ,  @SubTotal, @Total

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")


        If Not Request.QueryString("PurchaseNumber") = Nothing Then

            PurchaseNumber = Request.QueryString("PurchaseNumber")
            ItemID = Request.QueryString("ItemID")
            RPack = Request.QueryString("RPack")
            RQty = Request.QueryString("RQty")
            inlineno = Request.QueryString("inlineno").Trim()
            RUnits = Request.QueryString("RUnits")

            LocationID = Request.QueryString("LocationID")


            'Comments = Request.QueryString("Comments")
            'Color = Request.QueryString("Color")
            'OrderQty = Request.QueryString("OrderQty")
            'ItemUOM = Request.QueryString("ItemUOM")
            'Pack = Request.QueryString("Pack")
            'Units = Request.QueryString("Units")
            'ItemUnitPrice = Request.QueryString("ItemUnitPrice")
            'Total = Request.QueryString("Total")
            'SubTotal = Total
            ReceivedBy = Request.QueryString("drpReceiveby")
            VendorInvoiceNumber = Request.QueryString("txtVendorInvoiceNumber")


            inlineno = Request.QueryString("inlineno").Trim()
            POReceivedDate = Request.QueryString("POReceivedDate").Trim
            InsertLog(RQty, PurchaseNumber, ItemID, RPack, RUnits, LocationID, ReceivedBy, POReceivedDate, VendorInvoiceNumber, inlineno)

            If PurchaseNumber <> "" Then

                '= AddItemDetailsCustomisedGrid()

                If CheckOrder_AjaxBinding(inlineno) = 0 Then
                    OrderLineNumber = GetOrder_AjaxBinding(inlineno, ItemID)
                Else
                    OrderLineNumber = inlineno
                End If

                updateItemDetailsCustomisedGrid()
                UpdatePurcahseHeaderReceivedStatus()
                updatePOReceiveby()

                'InsertOrder_AjaxBinding(OrderLineNumber, inlineno, ItemID)
                Dim rowID As Int64 = 0
                Try
                    If CompanyID.ToUpper() = "JWF" Or CompanyID.ToLower() = "QuickfloraDemo".ToLower() Then
                        rowID = InsertRawData(RQty, PurchaseNumber, ItemID, RPack, RUnits, LocationID, ReceivedBy, POReceivedDate, VendorInvoiceNumber, inlineno)
                    End If
                Catch ex As Exception

                End Try
                TempTest(RUnits, inlineno)
                UpdateInventoryForItem(LocationID, ItemID, RUnits, inlineno, POReceivedDate)
                'TempTest(RUnits, rowID)
                Try
                    UpdateRawData(rowID)
                    'If rowID <> 0 Then
                    'End If
                Catch ex As Exception

                End Try
                Response.Clear()
                Response.Write(OrderLineNumber)
                Response.End()

            End If

        End If

    End Sub

    Private Function TempTest(Qty As String, PurchaseLineMumber As String) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("INSERT INTO tempInvetnory (purchaselinenumber, qty ) VALUES (" + PurchaseLineMumber + "," + Qty + ")", Connection)
                Command.CommandType = CommandType.Text

                Try

                    Command.Connection.Open()
                    Command.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    Response.Write(ex.Message)
                    Return False
                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    Private Function InsertRawData(Qty As String, PurchaseNo As String, itemid As String, rPack As String, rUnits As Int32, location As String, emp As String, poDate As String, vIno As String, PurchaseLineMumber As String) As Int64
        Dim OutPutValue As Int64 = 0

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[dbo].[InsertInventoryReceiveRawData]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("PurchaseNumber", PurchaseNo)
                Command.Parameters.AddWithValue("ItemID", itemid)
                Command.Parameters.AddWithValue("RPack", rPack)
                Command.Parameters.AddWithValue("RQty", Qty)
                Command.Parameters.AddWithValue("inlineno", PurchaseLineMumber)
                Command.Parameters.AddWithValue("RUnits", rUnits)
                Command.Parameters.AddWithValue("LocationID", location)
                Command.Parameters.AddWithValue("emp", emp)
                Command.Parameters.AddWithValue("POReceivedDate", poDate)
                Command.Parameters.AddWithValue("VInvoice", vIno)
                Command.Parameters.AddWithValue("processtype", "POR")


                Dim paramReturnValue As New SqlParameter("@rowID", Data.SqlDbType.BigInt)
                paramReturnValue.Direction = ParameterDirection.Output
                Command.Parameters.Add(paramReturnValue)
                Try


                    Command.Connection.Open()
                    Command.ExecuteNonQuery()


                    OutPutValue = Convert.ToInt64(paramReturnValue.Value)
                    ' myCon.Close()
                Catch ex As Exception
                    Response.Write(ex.Message)
                    Return 0
                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

        Return OutPutValue

    End Function

    Private Function UpdateRawData(rowid As Int64) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[dbo].UpdateReceiveInevntoryRawData", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Try
                    Command.Parameters.AddWithValue("CompanyID", CompanyID)
                    Command.Parameters.AddWithValue("DivisionID", DivisionID)
                    Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                    Command.Parameters.AddWithValue("RowID", rowid)


                    Command.Connection.Open()
                    Command.ExecuteNonQuery()


                    Return True
                Catch ex As Exception
                    Response.Write(ex.Message)
                    Return False
                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    Private Function InsertLog(Qty As String, PurchaseNo As String, itemid As String, rPack As String, rUnits As Int32, location As String, emp As String, poDate As String, vIno As String, PurchaseLineMumber As String) As Boolean
        Dim qu As String = ""

        Dim param As String = "Qty:"
        param = param & IIf(String.IsNullOrEmpty(Qty), "", Qty)
        param = param & " PurchaseNo:"
        param = param & IIf(String.IsNullOrEmpty(PurchaseNo), "", PurchaseNo)
        param = param & " itemid:"
        param = param & IIf(String.IsNullOrEmpty(itemid), "", itemid)
        param = param & " rPack:"
        param = param & IIf(String.IsNullOrEmpty(rPack), "", rPack)
        param = param & " rUnits:"
        param = param & IIf(String.IsNullOrEmpty(rUnits.ToString()), "", rUnits)
        param = param & " location:"
        param = param & IIf(String.IsNullOrEmpty(location), "", location)
        param = param & " emp:"
        param = param & IIf(String.IsNullOrEmpty(emp), "", emp)
        param = param & " poDate:"
        param = param & IIf(String.IsNullOrEmpty(poDate), "", poDate)
        param = param & " vIno:"
        param = param & IIf(String.IsNullOrEmpty(vIno), "", vIno)
        'param = param & " PurchaseLineMumber:"
        'param = param & IIf(String.IsNullOrEmpty(PurchaseLineMumber), "", PurchaseLineMumber)



        qu = "INSERT INTO [Enterprise].[dbo].[LogParmsCommon] ([Comp],[divi],[depa],[CalledFrom],[params],[lineno]) VALUES(@f0,@f1,@f2,@f3,@f4,@f5)"
        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand(qu, Connection)
                Command.CommandType = CommandType.Text

                Try
                    Command.Parameters.AddWithValue("@f0", CompanyID)
                    Command.Parameters.AddWithValue("@f1", DivisionID)
                    Command.Parameters.AddWithValue("@f2", DepartmentID)
                    Command.Parameters.AddWithValue("@f3", "AjaxItemsReceivePOUpdate.aspx")
                    Command.Parameters.AddWithValue("@f4", param)
                    Command.Parameters.AddWithValue("@f5", PurchaseLineMumber)


                    Command.Connection.Open()
                    Command.ExecuteNonQuery()


                    Return True
                Catch ex As Exception
                    Response.Write(ex.Message)
                    Return False
                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function


#Region "Inventory"

    Private Sub UpdateInventoryForItem(LocationID As String, ItemID As String, ItemQty As Integer, PurchaseLineNumber As String, PurchaseReceivedDate As String)

        UpdateItemInventoryByLocationForReceivedPO(LocationID, ItemID, ItemQty, PurchaseLineNumber, PurchaseReceivedDate)

        Dim dtOrdersList As New DataTable
        dtOrdersList = GetOrderItemsForReceivedPO(LocationID, ItemID, PurchaseReceivedDate)

        If dtOrdersList.Rows.Count > 0 Then
            For Each rowItem As DataRow In dtOrdersList.Rows
                UpateItemInventoryFutureDeliveryForBackOrderItems(LocationID, rowItem("OrderLineNumber"), ItemID, rowItem("BackOrderQty"), rowItem("OrderShipDate"))
            Next
        End If

    End Sub

    Private Function UpdateItemInventoryByLocationForReceivedPO(LocationID As String, ItemID As String, Qty As Integer, PurchaseLineMumber As String, PoDate As String) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[UpdateItemInventoryByLocationForReceivedPO]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("LocationID", LocationID)
                Command.Parameters.AddWithValue("ItemID", ItemID)
                Command.Parameters.AddWithValue("QtyOnHand", Qty)
                Command.Parameters.AddWithValue("POReceivedDate", PoDate)
                Command.Parameters.AddWithValue("PurchaseLineNumber", PurchaseLineMumber)

                Try

                    Command.Connection.Open()
                    Command.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    Return False
                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    Private Function GetOrderItemsForReceivedPO(LocationID As String, ItemID As String, POReceivedDate As String) As DataTable

        Dim dt As New DataTable

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetOrderItemsForReceivedPO]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("LocationID", LocationID)
                Command.Parameters.AddWithValue("ItemID", ItemID)
                Command.Parameters.AddWithValue("POReceivedDate", POReceivedDate)

                Try

                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(dt)

                Catch ex As Exception

                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

        Return dt

    End Function

    Public Function UpateItemInventoryFutureDeliveryForBackOrderItems(ByVal LocationID As String, OrderLineNumber As String, _
                                                                      ByVal ItemID As String, ByVal Qty As Int16, OrderShipDate As String) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[UpateItemInventoryFutureDeliveryForBackOrderItems]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                Command.Parameters.AddWithValue("LocationID", LocationID)
                Command.Parameters.AddWithValue("ItemID", ItemID)
                Command.Parameters.AddWithValue("Qty", Qty)

                Command.Parameters.AddWithValue("OrderLineNumber", OrderLineNumber)
                Command.Parameters.AddWithValue("OrderShipDate", OrderShipDate)

                Try
                    Command.Connection.Open()
                    Command.ExecuteNonQuery()

                    Return True

                Catch ex As Exception

                    Return False

                Finally

                    Command.Connection.Close()

                End Try

            End Using
        End Using

    End Function

#End Region

    Dim ReceivedBy As String = ""
    Dim VendorInvoiceNumber As String = ""
    Dim RUnits As String = ""

    Public Function updatePOReceiveby() As Boolean

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "update [PurchaseHeader] set [ReceivedBy] = @ReceivedBy, VendorInvoiceNumber =@VendorInvoiceNumber where CompanyID=@f0 and DivisionID =@f1 and DepartmentID=@f2 and [PurchaseNumber] =@PurchaseNumber "
        'Values (@f00,@f0,@f1,@f2,@f3,@f31,@f32,@f311,@f322)
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@PurchaseNumber", SqlDbType.NVarChar, 36)).Value = PurchaseNumber
            com.Parameters.Add(New SqlParameter("@ReceivedBy", SqlDbType.NVarChar, 36)).Value = ReceivedBy
            com.Parameters.Add(New SqlParameter("@VendorInvoiceNumber", SqlDbType.NVarChar, 36)).Value = VendorInvoiceNumber

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

    Dim OrderLineNumber As Integer

    '([CompanyID]
    '      ,[DivisionID]
    '      ,[DepartmentID]
    '      ,[Inlinenumber]
    '      ,[SystemDate]
    '      ,[QUID]
    '      ,[ItemID])

    Public Function InsertOrder_AjaxBinding(ByVal Inlinenumber As Integer, ByVal QUID As String, ByVal ItemID As String) As Boolean

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "INSERT INTO [Order_AjaxBinding] (CompanyID,DivisionID,DepartmentID,Inlinenumber,QUID,ItemID) Values(@f0,@f1,@f2,@f3,@f31,@f32)"
        'Values (@f00,@f0,@f1,@f2,@f3,@f31,@f32,@f311,@f322)
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.BigInt)).Value = Inlinenumber
            com.Parameters.Add(New SqlParameter("@f31", SqlDbType.NVarChar, 100)).Value = QUID
            com.Parameters.Add(New SqlParameter("@f32", SqlDbType.NVarChar, 36)).Value = ItemID

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


    Public Function updateItemDetailsCustomisedGrid() As Integer


        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand("[enterprise].[UpdatePurchaseDetailRecievedQty]", myCon)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
        parameterCompanyID.Value = CompanyID
        myCommand.Parameters.Add(parameterCompanyID)

        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
        parameterDepartmentID.Value = DepartmentID
        myCommand.Parameters.Add(parameterDepartmentID)

        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
        parameterDivisionID.Value = DivisionID
        myCommand.Parameters.Add(parameterDivisionID)

        Dim parameterOrderLineNumber As New SqlParameter("@PurchaseLineNumber", Data.SqlDbType.NVarChar)
        parameterOrderLineNumber.Value = OrderLineNumber
        myCommand.Parameters.Add(parameterOrderLineNumber)

        Dim parameterOrderNumber As New SqlParameter("@PurchaseNumber", Data.SqlDbType.NVarChar)
        parameterOrderNumber.Value = PurchaseNumber
        myCommand.Parameters.Add(parameterOrderNumber)

        Dim ReceivedQty As New SqlParameter("@ReceivedQty", Data.SqlDbType.NVarChar)
        ReceivedQty.Value = RQty
        myCommand.Parameters.Add(ReceivedQty)

        Dim ReceivedPackSize As New SqlParameter("@ReceivedPackSize", Data.SqlDbType.NVarChar)
        ReceivedPackSize.Value = RPack
        myCommand.Parameters.Add(ReceivedPackSize)

        Dim ReceivedQtyByPackSize As New SqlParameter("@ReceivedQtyByPackSize", Data.SqlDbType.NVarChar)
        ReceivedQtyByPackSize.Value = RUnits
        myCommand.Parameters.Add(ReceivedQtyByPackSize)


        myCon.Open()

        myCommand.ExecuteNonQuery()



        myCon.Close()

        Return 1

    End Function

    Public ConnectionString As String = ConfigurationManager.AppSettings("ConnectionString")
    Public Function UpdatePurcahseHeaderReceivedStatus() As Boolean

        Using connection As New SqlConnection(ConnectionString)
            Using Command As New SqlCommand("[enterprise].[UpdatePurcahseHeaderReceivedStatus]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", Me.CompanyID)
                Command.Parameters.AddWithValue("DivisionID", Me.DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", Me.DepartmentID)
                Command.Parameters.AddWithValue("PurchaseNumber", PurchaseNumber)
                Command.Parameters.AddWithValue("ReceivedDate", Date.Now)
                Command.Parameters.AddWithValue("ReceivingNumber", VendorInvoiceNumber)
                Command.Parameters.AddWithValue("PurchaseLineNumber", OrderLineNumber)
                Command.Parameters.AddWithValue("LocationID", LocationID)
                Command.Parameters.AddWithValue("Result", True)
                Command.Connection.Open()
                Command.ExecuteNonQuery()
                Try

                    Return True
                Catch ex As Exception
                    Return False
                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function


    Public Function GetOrder_AjaxBinding(ByVal QUID As String, ByVal ItemID As String) As Integer

        Dim Inlinenumber As Integer = 0

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "Select * from  [Order_AjaxBinding]  Where CompanyID = @f0 AND DivisionID = @f1 AND DepartmentID = @f2 and QUID = @f31 "
        'Values (@f00,@f0,@f1,@f2,@f3,@f31,@f32,@f311,@f322)
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f31", SqlDbType.NVarChar, 100)).Value = QUID.Trim()

            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(com)

            da.Fill(dt)


            If (dt.Rows.Count <> 0) Then

                Try
                    Inlinenumber = dt.Rows(0)("Inlinenumber")
                Catch ex As Exception
                    Dim msg As String
                    msg = ex.Message
                    'HttpContext.Current.Response.Write(msg)
                End Try

            End If

            Return Inlinenumber

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)

        End Try

        Return Inlinenumber

    End Function

    Public Function CheckOrder_AjaxBinding(ByVal QUID As String) As Integer

        Dim Inlinenumber As Integer = 0

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "Select PurchaseLineNumber from  [PurchaseDetail]  Where CompanyID = @f0 AND DivisionID = @f1 AND DepartmentID = @f2 and PurchaseLineNumber = @f31 "
        'Values (@f00,@f0,@f1,@f2,@f3,@f31,@f32,@f311,@f322)
        ' HttpContext.Current.Response.Write(qry)

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f31", SqlDbType.BigInt)).Value = QUID.Trim()

            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(com)

            da.Fill(dt)




            If (dt.Rows.Count <> 0) Then

                Try
                    Inlinenumber = dt.Rows(0)("PurchaseLineNumber")
                Catch ex As Exception
                    Dim msg As String
                    msg = ex.Message
                    HttpContext.Current.Response.Write(msg)
                End Try

            End If

            Return Inlinenumber

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)

        End Try

        Return Inlinenumber

    End Function


</script>
 
  <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    
</head>
<body>
    <form id="form1" runat="server">
   
    </form>
</body>
</html>