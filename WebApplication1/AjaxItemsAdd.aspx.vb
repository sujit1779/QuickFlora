
Partial Class AjaxItemsAdd
    Inherits System.Web.UI.Page

 
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


            'Description = Request.QueryString("Description")
            'Comments = Request.QueryString("Comments")
            'Color = Request.QueryString("Color")
            'OrderQty = Request.QueryString("OrderQty")
            'ItemUOM = Request.QueryString("ItemUOM")
            'Pack = Request.QueryString("Pack")
            'Units = Request.QueryString("Units")
            'ItemUnitPrice = Request.QueryString("ItemUnitPrice")
            'Total = Request.QueryString("Total")
            'SubTotal = Total


            inlineno = Request.QueryString("inlineno").Trim()



            If PurchaseNumber <> "" Then

                '= AddItemDetailsCustomisedGrid()

                If CheckOrder_AjaxBinding(inlineno) = 0 Then

                    OrderLineNumber = GetOrder_AjaxBinding(inlineno, ItemID)
                Else

                    OrderLineNumber = inlineno

                End If

                updateItemDetailsCustomisedGrid()

                'InsertOrder_AjaxBinding(OrderLineNumber, inlineno, ItemID)


                Response.Clear()
                Response.Write(OrderLineNumber)
                Response.End()

            End If

        End If

    End Sub
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
            'HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function

    Dim ReceivedBy As String = ""
    Dim VendorInvoiceNumber As String = ""

    Public Function updatePOReceiveby() As Boolean

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "update [PurchaseHeader] set [ReceivedBy] = @ReceivedBy, VendorInvoiceNumber =@VendorInvoiceNumber where CompanyID=@f0 and DivisionID =@f1 and DepartmentID=@f2 [PurchaseNumber] =@PurchaseNumber "
        'Values (@f00,@f0,@f1,@f2,@f3,@f31,@f32,@f311,@f322)
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.BigInt)).Value = Inlinenumber
            com.Parameters.Add(New SqlParameter("@ReceivedBy", SqlDbType.NVarChar, 36)).Value = ReceivedBy
            com.Parameters.Add(New SqlParameter("@VendorInvoiceNumber", SqlDbType.NVarChar, 36)).Value = VendorInvoiceNumber

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

    Public Function updateItemDetailsCustomisedGrid() As Integer


        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand("[dbo].[UpdatePurchaseDetailRecievedQty]", myCon)
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

        Dim parameterOrderLineNumber As New SqlParameter("@PurchaseLineNumber", Data.SqlDbType.Int)
        parameterOrderLineNumber.Value = OrderLineNumber
        myCommand.Parameters.Add(parameterOrderLineNumber)

        Dim parameterOrderNumber As New SqlParameter("@PurchaseNumber", Data.SqlDbType.NVarChar)
        parameterOrderNumber.Value = PurchaseNumber
        myCommand.Parameters.Add(parameterOrderNumber)

        Dim ReceivedQty As New SqlParameter("@ReceivedQty", Data.SqlDbType.NVarChar)
        ReceivedQty.Value = Rqty
        myCommand.Parameters.Add(ReceivedQty)

        Dim ReceivedPackSize As New SqlParameter("@ReceivedPackSize", Data.SqlDbType.NVarChar)
        ReceivedPackSize.Value = RPack
        myCommand.Parameters.Add(ReceivedPackSize)
         
        Dim ReceivedQtyByPackSize As New SqlParameter("@ReceivedQtyByPackSize", Data.SqlDbType.NVarChar)
        ReceivedQtyByPackSize.Value = Rqty
        myCommand.Parameters.Add(ReceivedQtyByPackSize)
         

        myCon.Open()

        myCommand.ExecuteNonQuery()


        
        myCon.Close()

        Return 1

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

 


End Class
