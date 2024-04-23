<%@ Page Language="VB" EnableViewState="false"  %>

<%@Import Namespace="System.Data"%>
<%@Import Namespace="System.Data.SqlClient"%>
<%@Import Namespace="System.Configuration"%> 

<script runat="server">

    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""

    Dim InventoryAdjustmentsNumber As String = ""
    Dim ItemID As String = ""
    Dim name As String = ""

    Dim qty As Integer = 1


    Dim cost As Decimal = 0
    Dim total As Decimal = 0

    Public result As String = ""

    Dim inlineno As String = "'"


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)

        Try
            CompanyID = Session("CompanyID")
            DivisionID = Session("DivisionID")
            DepartmentID = Session("DepartmentID")
        Catch ex As Exception
            Response.Clear()
            Response.Write("")
            Response.End()
            Exit Sub
        End Try


        If Not Request.QueryString("InventoryAdjustmentsNumber") = Nothing Then

            InventoryAdjustmentsNumber = Request.QueryString("InventoryAdjustmentsNumber")
            ItemID = Request.QueryString("ItemID")
            name = Request.QueryString("name")
            qty = Request.QueryString("qty")
            cost = Request.QueryString("cost")
            total = Request.QueryString("total")


            inlineno = Request.QueryString("inlineno").Trim()



            If InventoryAdjustmentsNumber <> "" Then

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


    Public Function updateItemDetailsCustomisedGrid() As Integer


        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand("[dbo].[UpdateInventoryAdjustmentDetailsJsGrid]", myCon)
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


        Dim parameterOrderNumber As New SqlParameter("@TransactionNumber", Data.SqlDbType.NVarChar)
        parameterOrderNumber.Value = InventoryAdjustmentsNumber
        myCommand.Parameters.Add(parameterOrderNumber)

        Dim pOrderLineNumber As New SqlParameter("@RowID", Data.SqlDbType.NVarChar)
        pOrderLineNumber.Value = OrderLineNumber
        myCommand.Parameters.Add(pOrderLineNumber)

        Dim parameterItemID As New SqlParameter("@ItemID", Data.SqlDbType.NVarChar)
        parameterItemID.Value = ItemID
        myCommand.Parameters.Add(parameterItemID)

        Dim parameterDescription As New SqlParameter("@ItemName", Data.SqlDbType.NVarChar)
        parameterDescription.Value = name
        myCommand.Parameters.Add(parameterDescription)

        Dim pqty As New SqlParameter("@Qty", Data.SqlDbType.Float)
        pqty.Value = qty
        myCommand.Parameters.Add(pqty)

        'Color
        Dim pcost As New SqlParameter("@AverageCost", Data.SqlDbType.Money)
        pcost.Value = cost
        myCommand.Parameters.Add(pcost)

        Dim ptotal As New SqlParameter("@ItemTotal", Data.SqlDbType.Money)
        ptotal.Value = total
        myCommand.Parameters.Add(ptotal)



        Dim paramReturnValue As New SqlParameter("@ReturnValue", Data.SqlDbType.Int)
        paramReturnValue.Direction = ParameterDirection.Output
        myCommand.Parameters.Add(paramReturnValue)

        myCon.Open()

        myCommand.ExecuteNonQuery()


        Dim OutPutValue As Integer
        OutPutValue = Convert.ToInt32(paramReturnValue.Value)
        myCon.Close()
        Return OutPutValue

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