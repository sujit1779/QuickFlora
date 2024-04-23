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
    Dim Color As String = ""

    Dim OrderQty As Integer = 1
    Dim ItemUOM As String = ""
    Dim Comments As String = ""
    Dim Pack As Integer = 1
    Dim Units As Integer = 1

    Dim ItemUnitPrice As Decimal = 0
    Dim SubTotal As Decimal = 0
    Dim Total As Decimal = 0

    Public result As String = ""

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
            Comments = Request.QueryString("Comments")
            Description = Request.QueryString("Description")
            Color = Request.QueryString("Color")
            OrderQty = Request.QueryString("OrderQty")
            ItemUOM = Request.QueryString("ItemUOM")
            Pack = Request.QueryString("Pack")
            Units = Request.QueryString("Units")
            ItemUnitPrice = Request.QueryString("ItemUnitPrice")
            Total = Request.QueryString("Total")
            SubTotal = Total


            Dim inlineno As String = "'"
            inlineno = Request.QueryString("inlineno")

            If PurchaseNumber <> "" Then

                Dim OrderLineNumber As Integer = AddItemDetailsCustomisedGrid()


                InsertOrder_AjaxBinding(OrderLineNumber, inlineno, ItemID)


                Response.Clear()
                Response.Write(OrderLineNumber)
                Response.End()

            End If

        End If

    End Sub


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


    Public Function AddItemDetailsCustomisedGrid() As Integer


        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand("[dbo].[AddPurchaseItemDetailsJsGrid]", myCon)
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

        'Dim parameterOrderLineNumber As New SqlParameter("@OrderLineNumber ", Data.SqlDbType.Int)
        'parameterOrderLineNumber.Value = OLineNumber
        'myCommand.Parameters.Add(parameterOrderLineNumber)

        Dim parameterOrderNumber As New SqlParameter("@PurchaseNumber", Data.SqlDbType.NVarChar)
        parameterOrderNumber.Value = PurchaseNumber
        myCommand.Parameters.Add(parameterOrderNumber)

        Dim parameterItemID As New SqlParameter("@ItemID", Data.SqlDbType.NVarChar)
        parameterItemID.Value = ItemID
        myCommand.Parameters.Add(parameterItemID)

        Dim parameterDescription As New SqlParameter("@Description", Data.SqlDbType.NVarChar)
        parameterDescription.Value = Description
        myCommand.Parameters.Add(parameterDescription)
        
         Dim pComments As New SqlParameter("@Comments", Data.SqlDbType.NVarChar)
        pComments.Value = Comments
        myCommand.Parameters.Add(pComments)

        'Color
        Dim pColor As New SqlParameter("@Color", Data.SqlDbType.NVarChar)
        pColor.Value = Color
        myCommand.Parameters.Add(pColor)

        Dim parameterOrderQty As New SqlParameter("@OrderQty", Data.SqlDbType.Float)
        parameterOrderQty.Value = OrderQty
        myCommand.Parameters.Add(parameterOrderQty)

        Dim parameterItemUOM As New SqlParameter("@ItemUOM", Data.SqlDbType.NVarChar)
        parameterItemUOM.Value = ItemUOM
        myCommand.Parameters.Add(parameterItemUOM)


     

        Dim pPack As New SqlParameter("@Pack", Data.SqlDbType.Int)
        pPack.Value = Pack
        myCommand.Parameters.Add(pPack)

        Dim pUnits As New SqlParameter("@Units", Data.SqlDbType.Int)
        pUnits.Value = Units
        myCommand.Parameters.Add(pUnits)

        Dim parameterItemUnitPrice As New SqlParameter("@ItemUnitPrice", Data.SqlDbType.Money)
        parameterItemUnitPrice.Value = ItemUnitPrice
        myCommand.Parameters.Add(parameterItemUnitPrice)

        

        Dim pSubTotal As New SqlParameter("@SubTotal", Data.SqlDbType.Money)
        pSubTotal.Value = SubTotal
        myCommand.Parameters.Add(pSubTotal)

        Dim parameterSubTotal As New SqlParameter("@Total", Data.SqlDbType.Money)
        parameterSubTotal.Value = Total
        myCommand.Parameters.Add(parameterSubTotal)

        

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