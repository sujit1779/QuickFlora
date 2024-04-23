Imports System.Data
Imports System.Data.SqlClient
Imports DAL

Partial Class ReceivingByScan
    Inherits System.Web.UI.Page

    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""

    Public parameter As String = ""

    Public EmployeeID As String = ""

    Public Function PopulateImage(ByVal ob As String) As String
        Dim DocumentDir As String = ""
        Dim img1 As String = ""
        Dim ImgName As String = ob.ToLower()
        DocumentDir = ConfigurationManager.AppSettings("DocPath")
        If (ImgName.Trim() = "") Then

            Return "https://secure.quickflora.com/Admin/images/no_image.gif"

        Else

            If System.IO.File.Exists(DocumentDir & ImgName.Trim()) Then

                Return "https://secure.quickflora.com/Admin/images/" & ImgName.Trim()

            Else
                Return "https://secure.quickflora.com/Admin/images/no_image.gif"
            End If




        End If


    End Function

    Sub BindGrid()
        ' Load the name of the stored procedure where our data comes from here into commandtext
        Dim CommandText As String = "enterprise.spCompanyInformation"
        Dim ConnectionString As String = ""
        ConnectionString = EnterpriseCommon.Configuration.ConfigSettings.ConnectionString
        ' get the connection ready
        Dim myConnection As New SqlConnection(ConnectionString)
        Dim myCommand As New SqlCommand(CommandText, myConnection)
        Dim workParam As New SqlParameter()

        myCommand.CommandType = CommandType.StoredProcedure

        ' Set the input parameter, companyid, divisionid, departmentid
        ' these parameters are set in the sub page_load
        myCommand.Parameters.Add("@CompanyID", SqlDbType.NVarChar).Value = CompanyID
        myCommand.Parameters.Add("@DivisionID", SqlDbType.NVarChar).Value = DivisionID
        myCommand.Parameters.Add("@DepartmentID", SqlDbType.NVarChar).Value = DepartmentID


        ' open the connection
        myConnection.Open()

        'bind the datasource
        DataGrid1.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
        DataGrid1.DataBind()


    End Sub


    Public Function PopulateEmployees(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal MO As String) As SqlDataReader

        Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        ConString.Open()
        Dim myCommand As New SqlCommand("PopulateEmployeesByAccess", ConString)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
        parameterCompanyID.Value = CompanyID
        myCommand.Parameters.Add(parameterCompanyID)

        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
        parameterDepartmentID.Value = DepartmentID
        myCommand.Parameters.Add(parameterDepartmentID)

        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
        parameterDivisionID.Value = DivisionID
        myCommand.Parameters.Add(parameterDivisionID)

        Dim pModule As New SqlParameter("@Module", Data.SqlDbType.NVarChar, 36)
        pModule.Value = MO
        myCommand.Parameters.Add(pModule)

        Dim rs As SqlDataReader
        rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
        Return rs



    End Function


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        'Me.Form.DefaultButton = "SearchButton"
        ClientScript.RegisterStartupScript(Me.GetType(), "Startup", String.Format("reswipe()"), True)

        If Not IsPostBack Then
            txtArrivalDate.Text = DateTime.Now.ToShortDateString()
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
        BindGrid()

        Dim rs As SqlDataReader
        rs = PopulateEmployees(CompanyID, DepartmentID, DivisionID, "RS")

        Dim securitycheck As Boolean = False

        While (rs.Read())

            If rs("EmployeeID").ToString() = EmployeeID Then
                ' securitycheck = True
                Exit While
            End If

        End While
        rs.Close()
        If CompanyID.ToUpper = "QuickfloraDemo".ToUpper Then
            securitycheck = True
        End If
        If CompanyID.ToUpper = "FMW" Then
            securitycheck = True
        End If
        If securitycheck = False Then
            ' Response.Redirect("SecurityAcessPermission.aspx?MOD=PO")
        End If


        Dim dt As New DataTable

        If Not IsPostBack Then
            'SetShipMethoddropdown()
            'SetLocationIDdropdown()
            'SetOriginDdropdown()
        End If

        'dt = GetReportData()
        'rptorderlist.DataSource = dt
        'rptorderlist.DataBind()


    End Sub

    Private Sub SearchButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SearchButton.Click
        'Dim dt As New DataTable
        'dt = GetReportData()
        'rptorderlist.DataSource = dt
        'rptorderlist.DataBind()
        btnrecevied_Click(sender, e)
    End Sub


    Public Function GetReportData() As DataTable
        Dim dt As New DataTable

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        'ssql = "SELECT 	convert(datetime, Convert(nvarchar(36),OrderHeader.OrderDate,101)) as ReportDay  FROM   OrderHeader  "

        ssql = ssql & " select   PurchaseHeader.PurchaseNumber,PurchaseHeader.VendorID,PurchaseDetail.ItemID,(Select  ItemName   from InventoryItems Where InventoryItems.CompanyID = PurchaseHeader.CompanyID AND InventoryItems.ItemID  = PurchaseDetail.ItemID) AS 'ItemName',  "
        ssql = ssql & " PurchaseDetail.Description ,PurchaseDetail.VendorQTY,PurchaseDetail.ItemUOM,PurchaseDetail.VendorPacksize,PurchaseDetail.OrderQty,ISNULL(PurchaseDetail.ReceivedVenorQTY,0) 'ReceivedVenorQTY',ISNULL(PurchaseDetail.ReceivedPackSize,'0') AS 'ReceivedPackSize',PurchaseDetail.ReceivedQty    from PurchaseHeader   Inner Join PurchaseDetail  On  PurchaseHeader.CompanyID = PurchaseDetail.CompanyID AND  PurchaseHeader.DepartmentID  = PurchaseDetail.DepartmentID AND   "
        ssql = ssql & " PurchaseHeader.DivisionID  = PurchaseDetail.DivisionID AND  PurchaseHeader.PurchaseNumber   = PurchaseDetail.PurchaseNumber   "
        ssql = ssql & " where   PurchaseHeader.CompanyID=@f0 And PurchaseHeader.DivisionID=@f1 And PurchaseHeader.DepartmentID=@f2   AND PurchaseHeader.PurchaseNumber  = @f3"
        '' ssql = ssql & " And ISNULL([PO_Requisition_Details].PONO,'') <> '' "


        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
        com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
        com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
        com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = txtscan.Text.Trim
        da.SelectCommand = com
        Try
            da.Fill(dt)
        Catch ex As Exception
            Response.Write(ex.Message)
            Response.Write("<br>")
            Response.Write(ssql)
        End Try


        Return dt

    End Function

    Private Sub btnrecevied_Click(sender As Object, e As EventArgs) Handles btnrecevied.Click
        If IsNumeric(txtscan.Text.Trim) Then

            Dim FillItemDetailGrid As New CustomOrder()
            Dim ds As New Data.DataSet
            ds = GetPurchaseDetail_list(txtscan.Text)

            Dim tr As String = ""
            Dim n As Integer = 0
            For n = 0 To ds.Tables(0).Rows.Count - 1



                OrderLineNumber = ds.Tables(0).Rows(n)("PurchaseLineNumber")
                VRPack = ds.Tables(0).Rows(n)("VendorPacksize")
                VRQty = ds.Tables(0).Rows(n)("VendorQTY")
                VRTotalQty = ds.Tables(0).Rows(n)("OrderQty")

                updateItemDetailsCustomisedGrid()
                UpdatePurcahseHeaderReceivedStatus()
                updatePOReceiveby()

            Next

            UpdateInventoryReceivedTodayPO()

            ' Response.Redirect("ReceivePOList.aspx")
            Dim dt As New DataTable
            dt = GetReportData()

            rptorderlist.DataSource = dt
            rptorderlist.DataBind()

        End If

    End Sub




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
            com.Parameters.Add(New SqlParameter("@PurchaseNumber", SqlDbType.NVarChar, 36)).Value = txtscan.Text
            com.Parameters.Add(New SqlParameter("@ReceivedBy", SqlDbType.NVarChar, 36)).Value = EmployeeID
            com.Parameters.Add(New SqlParameter("@VendorInvoiceNumber", SqlDbType.NVarChar, 36)).Value = "00000"

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
    Public ConnectionString As String = ConfigurationManager.AppSettings("ConnectionString")
    Public Function UpdatePurcahseHeaderReceivedStatus() As Boolean

        Using connection As New SqlConnection(ConnectionString)
            Using Command As New SqlCommand("[enterprise].[UpdatePurcahseHeaderReceivedStatus]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", Me.CompanyID)
                Command.Parameters.AddWithValue("DivisionID", Me.DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", Me.DepartmentID)
                Command.Parameters.AddWithValue("PurchaseNumber", txtscan.Text.Trim)
                Command.Parameters.AddWithValue("ReceivedDate", Date.Now)
                Command.Parameters.AddWithValue("ReceivingNumber", "00000")
                Command.Parameters.AddWithValue("PurchaseLineNumber", OrderLineNumber)
                Command.Parameters.AddWithValue("LocationID", "DEFAULT")
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


    Public Function GetPurchaseDetail_list(ByVal PO As String) As Data.DataSet

        Dim Total As Decimal = 0
        Dim dt As New Data.DataSet

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "select  *  from [PurchaseDetail]   where [PurchaseDetail].PurchaseNumber =@f31 and CompanyID = @f0 AND DivisionID = @f1 AND DepartmentID = @f2 "
        'Values (@f00,@f0,@f1,@f2,@f3,@f31,@f32,@f311,@f322)
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f31", SqlDbType.NVarChar, 36)).Value = PO.Trim()


            Dim da As New SqlDataAdapter(com)

            da.Fill(dt)


            Return dt

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)

        End Try

        Return dt

    End Function


#Region "Inventory"

    Dim txtArrivalDate As New TextBox

    Private Sub UpdateInventoryReceivedTodayPO()

        'Find Today POs list
        'Add inventory into in hand
        'find delivery orders list (having backorder items) - order by delivery date, order number, itemid
        'if quantity is enough againast order then remove backorder for that item 

        UpdatePOItemsForReceiving(txtArrivalDate.Text.Trim, txtscan.Text)

        Dim dtPOList As New DataTable
        dtPOList = GetTodaysPOItemsList(txtArrivalDate.Text.Trim, txtscan.Text)

        If dtPOList.Rows.Count > 0 Then

            For Each row As DataRow In dtPOList.Rows

                UpdateItemInventoryByLocationForReceivedPO(row("LocationID"), row("ItemID"), row("ItemQty"), row("PurchaseLineNumber"), txtArrivalDate.Text.Trim)

                Dim dtOrdersList As New DataTable
                dtOrdersList = GetOrderItemsForReceivedPO(row("LocationID"), row("ItemID"), txtArrivalDate.Text.Trim)

                If dtOrdersList.Rows.Count > 0 Then

                    For Each rowItem As DataRow In dtOrdersList.Rows

                        UpateItemInventoryFutureDeliveryForBackOrderItems(row("LocationID"), rowItem("OrderLineNumber"), row("ItemID"), rowItem("BackOrderQty"), rowItem("OrderShipDate"))

                    Next

                End If


            Next

        End If

    End Sub

    Private Function UpdatePOItemsForReceiving(ByVal PODate As String, ByVal PONumber As String) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[UpdatePOItemsForReceiving]", Connection)
                Command.CommandType = CommandType.StoredProcedure


                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("PONumber", PONumber)
                Command.Parameters.AddWithValue("PODate", PODate)

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

    Private Function GetTodaysPOItemsList(ByVal PODate As String, ByVal PONumber As String) As DataTable

        Dim dt As New DataTable

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetTodaysPOItemsList]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("PONumber", PONumber)
                Command.Parameters.AddWithValue("PODate", PODate)

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

    Private Function GetOrderItemsForReceivedPO(ByVal LocationID As String, ByVal ItemID As String, ByVal POReceivedDate As String) As DataTable

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

    Private Function UpdateItemInventoryByLocationForReceivedPO(ByVal LocationID As String, ByVal ItemID As String, ByVal Qty As Integer, ByVal PurchaseLineMumber As String, ByVal PoDate As String) As Boolean

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

    Public Function UpateItemInventoryFutureDeliveryForBackOrderItems(ByVal LocationID As String, ByVal OrderLineNumber As String, _
                                                                      ByVal ItemID As String, ByVal Qty As Int16, ByVal OrderShipDate As String) As Boolean

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
    Dim OrderLineNumber As String = "'"

    Dim VRPack As String = "'"
    Public VRQty As String = ""
    Public VRTotalQty As String = ""


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

        Dim parameterOrderLineNumber As New SqlParameter("@PurchaseLineNumber", Data.SqlDbType.Int)
        parameterOrderLineNumber.Value = OrderLineNumber
        myCommand.Parameters.Add(parameterOrderLineNumber)

        Dim parameterOrderNumber As New SqlParameter("@PurchaseNumber", Data.SqlDbType.NVarChar)
        parameterOrderNumber.Value = txtscan.Text
        myCommand.Parameters.Add(parameterOrderNumber)

        Dim ReceivedQty As New SqlParameter("@ReceivedQty", Data.SqlDbType.NVarChar)
        ReceivedQty.Value = VRQty
        myCommand.Parameters.Add(ReceivedQty)

        Dim ReceivedPackSize As New SqlParameter("@ReceivedPackSize", Data.SqlDbType.NVarChar)
        ReceivedPackSize.Value = VRPack
        myCommand.Parameters.Add(ReceivedPackSize)

        Dim ReceivedQtyByPackSize As New SqlParameter("@ReceivedQtyByPackSize", Data.SqlDbType.NVarChar)
        ReceivedQtyByPackSize.Value = VRTotalQty
        myCommand.Parameters.Add(ReceivedQtyByPackSize)


        myCon.Open()

        myCommand.ExecuteNonQuery()



        myCon.Close()

        Return 1

    End Function


    Public Function PurchaseNumberPost(ByVal PurchaseNumber As String) As String
        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand("[enterprise].[Purchase_Post]", myCon)
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

        Dim parameterOrderNumber As New SqlParameter("@PurchaseNumber", Data.SqlDbType.NVarChar)
        parameterOrderNumber.Value = PurchaseNumber
        myCommand.Parameters.Add(parameterOrderNumber)


        Dim parameterPostingResult As New SqlParameter("@PostingResult", Data.SqlDbType.NVarChar, 200)
        parameterPostingResult.Direction = ParameterDirection.Output
        myCommand.Parameters.Add(parameterPostingResult)
        Dim OutputValue As String = ""
        myCon.Open()

        myCommand.ExecuteNonQuery()

        OutputValue = parameterPostingResult.Value.ToString()

        myCon.Close()
        Return OutputValue

    End Function


    'Protected Sub rptorderlist_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles rptorderlist.PageIndexChanging

    '    rptorderlist.PageIndex = e.NewPageIndex
    '    Dim dt As New DataTable
    '    dt = GetReportData()

    '    rptorderlist.DataSource = dt
    '    rptorderlist.DataBind()
    'End Sub

End Class
