Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient

Partial Class AjaxTransferitemSave
    Inherits System.Web.UI.Page



    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim EmployeeID As String = ""
    Dim CustomerID As String = ""
    Dim Loc As String = ""
    Public result As String = ""

    '    Select   QOH, PreSold, Requested, ColorVerity, RowNumber  FROM     BatchPOSampleDemo


    Dim BrowserSessionId As String = ""
    Dim SessionRefresh As Date


    Public Function SessionRefreshTableInsert() As Boolean

        BrowserSessionId = Session.SessionID
        Me.SessionRefresh = Date.Now
        Dim url As String
        'url = Request.Url.ToString()
        url = Request.ServerVariables("QUERY_STRING")

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "insert into SessionRefreshTable( CompanyID, DivisionID, DepartmentID, EmployeeID,BrowserSessionId,SessionRefresh,Using,IP,Url) " _
             & " values(@CompanyID,@DivisionID,@DepartmentID,@EmployeeID,@BrowserSessionId,@SessionRefresh,@Using,@IP,@Url)"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@EmployeeID", SqlDbType.NVarChar, 36)).Value = Me.EmployeeID
            com.Parameters.Add(New SqlParameter("@BrowserSessionId", SqlDbType.NVarChar, 500)).Value = Me.BrowserSessionId
            com.Parameters.Add(New SqlParameter("@SessionRefresh", SqlDbType.DateTime)).Value = Me.SessionRefresh
            com.Parameters.Add(New SqlParameter("@Using", SqlDbType.NVarChar)).Value = "AjaxPOSave"
            com.Parameters.Add(New SqlParameter("@IP", SqlDbType.NVarChar)).Value = Request.ServerVariables("REMOTE_ADDR")
            com.Parameters.Add(New SqlParameter("@Url", SqlDbType.NVarChar)).Value = url

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



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

        SessionRefreshTableInsert()

        If Not Request.QueryString("RowNumber") = Nothing Then

            Dim RowNumber As String = ""
            RowNumber = Request.QueryString("RowNumber")

            Dim name As String = ""
            name = Request.QueryString("name")

            Dim value As String = ""
            value = Request.QueryString("value")


            Dim dt As New DataTable
            ' dt = SelectOrderProductData(RowNumber)
            If dt.Rows.Count = 0 And False Then
                Dim dt2 As New DataTable
                dt2 = SelectInsertPO_AjaxBinding(RowNumber)
                If dt2.Rows.Count = 0 Then
                    RowNumber = InsertPO_OrderDetails(RowNumber)
                Else
                    RowNumber = dt2.Rows(0)("InLineNumber")
                End If

            End If

            If RowNumber <> "" Then

                'If name = "Vendor_Code" Then
                '    Dim dt3 As New DataTable
                '    dt3 = FillDetailsVendor(value.Trim())
                '    If dt3.Rows.Count = 0 Then
                '        Response.Clear()
                '        Response.Write(RowNumber)
                '        Response.End()
                '        Exit Sub
                '    End If
                'End If



                InsertOrder_AjaxBinding(RowNumber, name, value)
                'If name = "Ext_COSt" Then
                '    UpdateOrderTotal(RowNumber)
                'End If
                Response.Clear()
                Response.Write(RowNumber)
                Response.End()

            End If

        End If

    End Sub
    '  	UPDATE InventoryTransferDetail
    '  Set TransferQty = @TransferQty, AddtionalItemNotes = @AddionalItemNotes ,MTPrice= @MTPrice
    'WHERE RowID = @RowID

    Public Function InsertOrder_AjaxBinding(ByVal RowNumber As Integer, ByVal name As String, ByVal value As String) As Boolean
        If name = "MTPrice" Then
            value = value.Replace("$", "")
        End If
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        ' addchangehistory(RowNumber, name, value)
        qry = "Update InventoryTransferDetail SET  " & name & " =@value    Where RowID = @RowNumber"
        'Values (@f00,@f0,@f1,@f2,@f3,@f31,@f32,@f311,@f322)
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@value", SqlDbType.NVarChar)).Value = value
            com.Parameters.Add(New SqlParameter("@RowNumber", SqlDbType.NVarChar)).Value = RowNumber

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()


            'Try
            '    Dim DT_LoadROData As New DataTable
            '    DT_LoadROData = LoadROData(RowNumber)

            '    If DT_LoadROData.Rows.Count > 0 Then
            '        Dim dt_LoadItemsData As New DataTable
            '        dt_LoadItemsData = LoadItemsData(DT_LoadROData.Rows(0)(0))

            '        Dim UOM As String = ""
            '        Dim WHOPrice As String = ""
            '        If dt_LoadItemsData.Rows.Count > 0 Then
            '            UOM = dt_LoadItemsData.Rows(0)(0)
            '            WHOPrice = dt_LoadItemsData.Rows(0)(1)
            '            qry = "Update PO_Requisition_Details SET  UOM =@value  Where InLineNumber = @RowNumber"
            '            com = New SqlCommand(qry, connec)
            '            com.Parameters.Add(New SqlParameter("@value", SqlDbType.NVarChar)).Value = UOM
            '            com.Parameters.Add(New SqlParameter("@WHOPrice", SqlDbType.NVarChar)).Value = WHOPrice
            '            com.Parameters.Add(New SqlParameter("@RowNumber", SqlDbType.NVarChar)).Value = RowNumber
            '            com.Connection.Open()
            '            com.ExecuteNonQuery()
            '            com.Connection.Close()
            '        End If

            '    End If

            'Catch ex As Exception

            'End Try

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function



    Public Function FillDetailsVendor(ByVal VendorID As String) As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from VendorInformation where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and VendorID=@f3"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 50)).Value = VendorID

            da.SelectCommand = com
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return dt

        End Try

    End Function




    Public Function UpdateOrderTotal(ByVal RowNumber As String) As Boolean
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT OrderNo FROM [PO_Requisition_Details] where  InLineNumber = @InLineNumber and CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2  "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@InLineNumber", SqlDbType.NVarChar, 36)).Value = RowNumber
            da.SelectCommand = com
            da.Fill(dt)

            Dim OrderNo As String = ""
            If dt.Rows.Count <> 0 Then
                Try
                    OrderNo = dt.Rows(0)("OrderNo")
                Catch ex As Exception

                End Try

                Dim total As Decimal = 0
                total = GetOrderTotal(OrderNo)
                Updatetotal(OrderNo, total)

            End If

        Catch ex As Exception

        End Try

        Return True
    End Function

    Public Function Updatetotal(ByVal OrderNo As Integer, ByVal Total As Decimal) As Boolean

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "Update PO_Requisition_Header SET   TotalAmount=@TotalAmount   Where  OrderNo = @OrderNo and CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2  "
        'Values (@f00,@f0,@f1,@f2,@f3,@f31,@f32,@f311,@f322)
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@TotalAmount", SqlDbType.Money)).Value = Total
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@OrderNo", SqlDbType.NVarChar, 36)).Value = OrderNo

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



    Public Function GetOrderTotal(ByVal OrderNo As String) As Decimal
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT SUM(CONVERT(Money, Ext_COSt)) FROM [PO_Requisition_Details] where  ISNUMERIC(Ext_COSt) = 1 and  OrderNo = @OrderNo and CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2  "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Dim Total As Decimal = 0
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@OrderNo", SqlDbType.NVarChar, 36)).Value = OrderNo
            da.SelectCommand = com
            da.Fill(dt)


            If dt.Rows.Count <> 0 Then
                Try
                    Total = dt.Rows(0)(0)
                Catch ex As Exception

                End Try


            End If

        Catch ex As Exception

        End Try

        Return Total
    End Function



    'CREATE TABLE [dbo].[InsertPO_AjaxBinding](
    '[CompanyID] [nvarchar](36) Not NULL,
    '[DivisionID] [nvarchar](36) Not NULL,
    '[DepartmentID] [nvarchar](36) Not NULL,
    'RowNumber [nvarchar](500) Not NULL,
    'InLineNumber bigInt NULL,

    Public Function SelectInsertPO_AjaxBinding(ByVal RowNumber As String) As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT * FROM [InsertPO_AjaxBinding] where  RowNumber = @RowNumber and CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2  "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@RowNumber", SqlDbType.NVarChar, 36)).Value = RowNumber
            da.SelectCommand = com
            da.Fill(dt)


        Catch ex As Exception

        End Try

        Return dt
    End Function



    Public Function SelectOrderProductData(ByVal RowNumber As String) As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT * FROM [PO_Requisition_Details] where  InLineNumber = @InLineNumber and CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2  "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@InLineNumber", SqlDbType.NVarChar, 36)).Value = RowNumber
            da.SelectCommand = com
            da.Fill(dt)


        Catch ex As Exception

        End Try

        Return dt
    End Function


    Public Function InsertPO_OrderDetails(ByVal RowNumber As String) As Integer

        Dim OrderNo As String = ""
        Dim Product As String = ""

        Dim words As String() = RowNumber.Split("@")
        Product = words(1)
        OrderNo = words(2)

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String

        If OrderNo <> "" Then
            qry = "insert into PO_Requisition_Details ([CompanyID] ,[DivisionID] ,[DepartmentID] ,[OrderNo] ,[Product] ,[QOH] ,[DUMP] ,[Q_REQ] ,[PRESOLD] ,[COLOR_VARIETY] ,[REMARKS] ,[Q_ORD] ,[PACK] ,[COST] ,[Ext_COSt] ,[Vendor_Code] ,[Buyer] ,[Status] ,[Q_Recv] ,[ISSUE])" _
             & " values(@CompanyID ,@DivisionID ,@DepartmentID ,@OrderNo ,@Product ,@QOH ,@DUMP ,@Q_REQ ,@PRESOLD ,@COLOR_VARIETY ,@REMARKS ,@Q_ORD ,@PACK ,@COST ,@Ext_COSt ,@Vendor_Code ,@Buyer ,@Status ,@Q_Recv ,@ISSUE) "

            Dim com As SqlCommand
            com = New SqlCommand(qry, connec)

            com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@OrderNo", SqlDbType.NVarChar)).Value = OrderNo
            com.Parameters.Add(New SqlParameter("@Product", SqlDbType.NVarChar)).Value = Product
            com.Parameters.Add(New SqlParameter("@QOH", SqlDbType.NVarChar)).Value = "0"
            com.Parameters.Add(New SqlParameter("@DUMP", SqlDbType.NVarChar)).Value = "0"
            com.Parameters.Add(New SqlParameter("@Q_REQ", SqlDbType.NVarChar)).Value = "0"
            com.Parameters.Add(New SqlParameter("@PRESOLD", SqlDbType.NVarChar)).Value = ""
            com.Parameters.Add(New SqlParameter("@COLOR_VARIETY", SqlDbType.NVarChar)).Value = ""
            com.Parameters.Add(New SqlParameter("@REMARKS", SqlDbType.NVarChar)).Value = ""
            com.Parameters.Add(New SqlParameter("@Q_ORD", SqlDbType.NVarChar)).Value = "0"
            com.Parameters.Add(New SqlParameter("@PACK", SqlDbType.NVarChar)).Value = "1"
            com.Parameters.Add(New SqlParameter("@COST", SqlDbType.NVarChar)).Value = "0"
            com.Parameters.Add(New SqlParameter("@Ext_COSt", SqlDbType.NVarChar)).Value = "0"
            com.Parameters.Add(New SqlParameter("@Vendor_Code", SqlDbType.NVarChar)).Value = ""
            com.Parameters.Add(New SqlParameter("@Buyer", SqlDbType.NVarChar)).Value = ""
            com.Parameters.Add(New SqlParameter("@Status", SqlDbType.NVarChar)).Value = "No Action"
            com.Parameters.Add(New SqlParameter("@Q_Recv", SqlDbType.NVarChar)).Value = "0"
            com.Parameters.Add(New SqlParameter("@ISSUE", SqlDbType.NVarChar)).Value = ""

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()



        End If

        Dim InLineNumber As Integer = 0

        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT MAX(InLineNumber) FROM [PO_Requisition_Details] where  Product=@Product and  OrderNo = @OrderNo and CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2  "
        Dim da As New SqlDataAdapter
        Dim com2 As SqlCommand
        com2 = New SqlCommand(ssql, connec)
        Try
            com2.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com2.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com2.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com2.Parameters.Add(New SqlParameter("@OrderNo", SqlDbType.NVarChar, 36)).Value = OrderNo
            com2.Parameters.Add(New SqlParameter("@Product", SqlDbType.NVarChar)).Value = Product
            da.SelectCommand = com2
            da.Fill(dt)


        Catch ex As Exception

        End Try

        If dt.Rows.Count <> 0 Then

            InLineNumber = dt.Rows(0)(0)
            InsertPO_AjaxBinding(RowNumber, InLineNumber)
            Try
            Catch ex As Exception

            End Try
        End If

        Return InLineNumber
    End Function



    Public Function InsertPO_AjaxBinding(ByVal RowNumber As String, ByVal InLineNumber As Integer) As Boolean

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "Insert Into InsertPO_AjaxBinding (CompanyID,DivisionID,DepartmentID,RowNumber,InLineNumber) Values(@f0,@f1,@f2,@RowNumber,@InLineNumber) "
        'Values (@f00,@f0,@f1,@f2,@f3,@f31,@f32,@f311,@f322)
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)

        com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
        com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
        com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID

        com.Parameters.Add(New SqlParameter("@InLineNumber", SqlDbType.BigInt)).Value = InLineNumber
        com.Parameters.Add(New SqlParameter("@RowNumber", SqlDbType.NVarChar)).Value = RowNumber

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


    'Select   QOH, PreSold, Requested, ColorVerity, RowNumber  FROM     BatchPOSampleDemo



    Public Function LoadROData(ByVal RowNumber As Integer) As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select  Product from [PO_Requisition_Details] where    CompanyID=@CompanyID AND DivisionID=@DivisionID AND DepartmentID=@DepartmentID   "
        ssql = ssql & " and InLineNumber = @RowNumber "


        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try
            com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@RowNumber", SqlDbType.NVarChar)).Value = RowNumber

            da.SelectCommand = com
            da.Fill(dt)


        Catch ex As Exception

        End Try

        Return dt
    End Function


    Public Function LoadItemsData(ByVal ItemID As String) As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select  ISNULL(ItemUOM,''),ISNULL(wholesalePrice,0) from [InventoryItems] where    CompanyID=@CompanyID AND DivisionID=@DivisionID AND DepartmentID=@DepartmentID  "
        ssql = ssql & " and ItemID='" & ItemID & "'"


        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try
            com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = Me.DivisionID

            da.SelectCommand = com
            da.Fill(dt)


        Catch ex As Exception

        End Try

        Return dt
    End Function



    Public Sub addchangehistory(ByVal RowNumber As Integer, ByVal name As String, ByVal value As String)

        Dim type As String = "Text"


        Select Case name
            Case "Product"
                type = "Text"
            Case "QOH"
                type = "Money"
            Case "DUMP"
                type = "Money"
            Case "Q_REQ"
                type = "Money"
            Case "PRESOLD"
                type = "Money"
            Case "COLOR_VARIETY"
                type = "Text"
            Case "REMARKS"
                type = "Text"
            Case "Q_ORD"
                type = "Money"
            Case "PACK"
                type = "Money"
            Case "COST"
                type = "Money"
            Case "Ext_COSt"
                type = "Money"
            Case "Vendor_Code"
                type = "Text"
            Case "Buyer"
                type = "Text"
            Case "Status"
                type = "Text"
            Case "Q_Recv"
                type = "Text"
            Case "ISSUE"
                type = "Text"
        End Select

        Dim OrderNo As String = ""
        Dim dt As New DataTable
        dt = SelectOrderProductData(RowNumber)

        If dt.Rows.Count <> 0 Then

            Try
                OrderNo = dt.Rows(0)("OrderNo")
            Catch ex As Exception
            End Try

        End If
        Response.Write("OrderNo:" & OrderNo)
        Logchangehistory("", EmployeeID, "PO_Requisition_Details", name, value, OrderNo, RowNumber, type)


    End Sub

    Public Function Logchangehistory(ByVal CustomerID As String, ByVal EmployeeID As String, ByVal tableName As String, ByVal fieldName As String, ByVal fieldChangeValue As String, ByVal OrderNumber As String, ByVal OrderLine As String, ByVal type As String) As Boolean

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "[AddChangeHistoryRequisition]"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        com.CommandType = CommandType.StoredProcedure
        '@CustomerID varchar(36)=null,
        '@EmployeeID VARCHAR(36)=null,  
        '@tableName VARCHAR(50),
        '@fieldName VARCHAR(50),
        '@fieldChangeValue varchar(4000),
        '@CompanyID varchar(36),
        '@DivisionID varchar(36),
        '@DepartmentID varchar(36),
        '@OrderNumber  varchar(36),
        '@OrderLine  nvarchar(36)=null   
        com.Parameters.Add(New SqlParameter("@CustomerID", SqlDbType.NVarChar)).Value = CustomerID
        com.Parameters.Add(New SqlParameter("@EmployeeID", SqlDbType.NVarChar)).Value = EmployeeID
        com.Parameters.Add(New SqlParameter("@tableName", SqlDbType.NVarChar)).Value = tableName
        com.Parameters.Add(New SqlParameter("@fieldName", SqlDbType.NVarChar)).Value = fieldName
        com.Parameters.Add(New SqlParameter("@fieldChangeValue", SqlDbType.NVarChar)).Value = fieldChangeValue
        com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar)).Value = Me.CompanyID
        com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar)).Value = Me.DivisionID
        com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar)).Value = Me.DepartmentID
        com.Parameters.Add(New SqlParameter("@OrderNumber", SqlDbType.NVarChar)).Value = OrderNumber
        Response.Write(":OrderNumber:" & OrderNumber)
        com.Parameters.Add(New SqlParameter("@OrderLine", SqlDbType.NVarChar)).Value = OrderLine
        com.Parameters.Add(New SqlParameter("@type", SqlDbType.NVarChar)).Value = type

        Try
            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()
        Catch ex As Exception

        End Try


        Return True
    End Function



End Class
