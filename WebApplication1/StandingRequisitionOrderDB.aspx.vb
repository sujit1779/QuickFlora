Imports System.Data
Imports System.Data.SqlClient
Imports DAL

Partial Class StandingRequisitionOrderDB
    Inherits System.Web.UI.Page


    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim EmployeeID As String = ""

    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    Private Sub OrderHeaderGrid_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles OrderHeaderGrid.PageIndexChanging

    End Sub

    Private Sub OrderHeaderGrid_PageIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles OrderHeaderGrid.PageIndexChanged

    End Sub

    Dim rs As SqlDataReader


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


    Public Function GetCategorylist(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As DataTable

        Dim dt As New DataTable
        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand(" Select * from [AddProductCategorylist] Where CompanyID = @CompanyID AND DivisionID=@DivisionID AND DepartmentID=@DepartmentID  ", Connection)
                Command.CommandType = CommandType.Text
                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(dt)
                Catch ex As Exception
                    ' lblerror.Text = ex.Message
                End Try
            End Using
        End Using
        Return dt

    End Function


    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click

        Dim dt As New DataTable
        dt = GetCategorylist(Me.CompanyID, Me.DivisionID, Me.DepartmentID)
        If dt.Rows.Count > 0 Then
            Dim n As Integer = 0
            Dim ItemCategoryID As String = 0
            Dim ItemFamilyID As String = 0

            Try
                ItemFamilyID = dt.Rows(0)("ItemFamilyID")
                ItemCategoryID = dt.Rows(0)("ItemCategoryID")
                LoadProductList(ItemCategoryID, ItemFamilyID)
            Catch ex As Exception

            End Try

        End If

    End Sub

    Private Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        Dim dt As New DataTable
        dt = GetCategorylist(Me.CompanyID, Me.DivisionID, Me.DepartmentID)
        If dt.Rows.Count > 0 Then
            Dim n As Integer = 0
            Dim ItemCategoryID As String = 0
            Dim ItemFamilyID As String = 0

            Try
                ItemFamilyID = dt.Rows(1)("ItemFamilyID")
                ItemCategoryID = dt.Rows(1)("ItemCategoryID")
                LoadProductList(ItemCategoryID, ItemFamilyID)
            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button3.Click
        Dim dt As New DataTable
        dt = GetCategorylist(Me.CompanyID, Me.DivisionID, Me.DepartmentID)
        If dt.Rows.Count > 0 Then
            Dim n As Integer = 0
            Dim ItemCategoryID As String = 0
            Dim ItemFamilyID As String = 0

            Try
                ItemFamilyID = dt.Rows(2)("ItemFamilyID")
                ItemCategoryID = dt.Rows(2)("ItemCategoryID")
                LoadProductList(ItemCategoryID, ItemFamilyID)
            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button4.Click
        Dim dt As New DataTable
        dt = GetCategorylist(Me.CompanyID, Me.DivisionID, Me.DepartmentID)
        If dt.Rows.Count > 0 Then
            Dim n As Integer = 0
            Dim ItemCategoryID As String = 0
            Dim ItemFamilyID As String = 0

            Try
                ItemFamilyID = dt.Rows(3)("ItemFamilyID")
                ItemCategoryID = dt.Rows(3)("ItemCategoryID")
                LoadProductList(ItemCategoryID, ItemFamilyID)
            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button5.Click
        Dim dt As New DataTable
        dt = GetCategorylist(Me.CompanyID, Me.DivisionID, Me.DepartmentID)
        If dt.Rows.Count > 0 Then
            Dim n As Integer = 0
            Dim ItemCategoryID As String = 0
            Dim ItemFamilyID As String = 0

            Try
                ItemFamilyID = dt.Rows(4)("ItemFamilyID")
                ItemCategoryID = dt.Rows(4)("ItemCategoryID")
                LoadProductList(ItemCategoryID, ItemFamilyID)
            Catch ex As Exception

            End Try
            'lblsavechanges.Text = lblsavechanges.Text & " " & ItemCategoryID
            'lblsavechanges.Visible = True
        End If
    End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblsave.Text = ""
        dvsave.Visible = False

        lblsavealert.Text = ""
        dvsavealert.Visible = False

        txtshipdate.Attributes.Add("onkeypress", "return AvoidWrite(event);")
        txtarrivedate.Attributes.Add("onkeypress", "return AvoidWrite(event);")

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

        rs = PopulateEmployees(CompanyID, DepartmentID, DivisionID, "PurchaseRequest")

        Dim securitycheck As Boolean = False

        While (rs.Read())

            If rs("EmployeeID").ToString() = EmployeeID Then
                securitycheck = True
                Exit While
            End If

        End While
        rs.Close()

        If securitycheck = False Then
            Response.Redirect("SecurityAcessPermission.aspx?MOD=PurchaseRequest")
        End If



        If Me.CompanyID = "QuickfloraDemo" Or Me.CompanyID = "DierbergsMarkets,Inc63017" Then



            btnLoadProductList.Visible = False
            btnFlowers.Visible = False
            btnGreens.Visible = False
            btnHardgoods.Visible = False
            btnPlants.Visible = False

            Dim dt As New DataTable
            dt = GetCategorylist(Me.CompanyID, Me.DivisionID, Me.DepartmentID)
            If dt.Rows.Count > 0 Then
                Dim n As Integer = 0
                Try
                    Button1.Text = dt.Rows(0)("ButtonName")
                    Button1.ToolTip = dt.Rows(0)("ItemCategoryID")
                    If Button1.Text.Trim = "" Then
                        Button1.Visible = False
                    End If
                Catch ex As Exception

                End Try
                Try
                    Button2.Text = dt.Rows(1)("ButtonName")
                    Button2.ToolTip = dt.Rows(1)("ItemCategoryID")
                    If Button2.Text.Trim = "" Then
                        Button2.Visible = False
                    End If
                Catch ex As Exception

                End Try
                Try
                    Button3.Text = dt.Rows(2)("ButtonName")
                    Button3.ToolTip = dt.Rows(2)("ItemCategoryID")
                    If Button3.Text.Trim = "" Then
                        Button3.Visible = False
                    End If
                Catch ex As Exception

                End Try
                Try
                    Button4.Text = dt.Rows(3)("ButtonName")
                    Button4.ToolTip = dt.Rows(3)("ItemCategoryID")
                    If Button4.Text.Trim = "" Then
                        Button4.Visible = False
                    End If
                Catch ex As Exception

                End Try
                Try
                    Button5.Text = dt.Rows(4)("ButtonName")
                    Button5.ToolTip = dt.Rows(4)("ItemCategoryID")
                    If Button5.Text.Trim = "" Then
                        Button5.Visible = False
                    End If
                Catch ex As Exception

                End Try
            End If

        Else
            Button1.Visible = False
            Button2.Visible = False
            Button3.Visible = False
            Button4.Visible = False
            Button5.Visible = False
        End If

        If IsPostBack = False Then
            'txtshipdate.Attributes.Add("onblur", "Javascript:FillArrivaldate2();")
            SetLocationIDdropdown()
            ''txtrecvon.Text = Date.Now.Date.ToShortDateString
            txtlastchanged.Text = Date.Now
            txtlastchangedby.Text = EmployeeID
            ''txtshipdate.Text = Date.Now.Date.ToShortDateString
            ''txtorderplaced.Text = Date.Now.Date.ToShortDateString
            ''txtarrivedate.Text = Date.Now.Date.ToShortDateString

            txttotal.Text = "0.00"

            Dim OrderNo As String = ""
            Try
                OrderNo = Request.QueryString("OrderNo")
            Catch ex As Exception

            End Try

            If OrderNo <> "" Then
                txtoderno.Text = OrderNo
                SetOrderData()
                SetOrderProductData()
            Else
                SetOrderProductEmptyData()
            End If



        Else

        End If


    End Sub



    Public Function LoadItemsDataDierbergsMarkets(ByVal ItemCategoryID As String, Optional ByVal ItemFamilyID As String = "") As DataTable
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select  ItemID,[InventoryItems].UnitPrice , [InventoryItems].UnitsPerBox ,VendorID from [InventoryItems] where  ItemID <> 'DEFAULT' AND CompanyID=@CompanyID AND DivisionID=@DivisionID AND DepartmentID=@DepartmentID AND  InventoryItems.IsActive=1 and InventoryItems.WireServiceIdAllowed = 1 "
        If Me.CompanyID = "DierbergsMarkets,Inc63017" Or Me.CompanyID = "QuickfloraDemo" Then
            ssql = "select  ItemID,ISNULL([InventoryItems].wholesalePrice,0) AS 'UnitPrice' , [InventoryItems].UnitsPerBox ,VendorID from [InventoryItems] where  ItemID <> 'DEFAULT' AND CompanyID=@CompanyID AND DivisionID=@DivisionID AND DepartmentID=@DepartmentID AND  InventoryItems.IsActive=1 and InventoryItems.WireServiceIdAllowed = 1 "
        End If

        If ItemCategoryID.Trim <> "" Then
            ssql = ssql & " and ItemCategoryID2='" & ItemCategoryID & "' "
        End If

        If Me.CompanyID = "DierbergsMarkets,Inc63017" Or Me.CompanyID = "QuickfloraDemo" Then

            Try
                If ItemFamilyID.Trim <> "" Then
                    ssql = ssql & " and ItemFamilyID2='" & ItemFamilyID & "' "
                End If
            Catch ex As Exception

            End Try

            Dim locationid As String = ""
            Try
                locationid = Session("Locationid")
            Catch ex As Exception

            End Try


            Dim obj As New clsOrder_Location
            Dim dtnew As New System.Data.DataTable()
            obj.CompanyID = CompanyID
            obj.DivisionID = DivisionID
            obj.DepartmentID = DepartmentID
            obj.LocationID = locationid
            dtnew = obj.DetailsOrder_Location()
            Dim AllowedAllItems As Boolean = True
            If dtnew.Rows.Count <> 0 Then
                Try
                    AllowedAllItems = dtnew.Rows(0)("AllowedAllItems")
                Catch ex As Exception
                End Try
            End If
            If AllowedAllItems = False Then
                ssql = ssql & "   AND  ISNULL([ActiveForPOM],1) = 1 AND  ISNULL([ActiveForStore],1) = 1   "
            Else
                ssql = ssql & "   AND  ISNULL([ActiveForPOM],1) = 1   "
            End If

            ssql = ssql & " Order by (Select SUM(OrderDetail.OrderQty) from OrderDetail Where   CompanyID=@CompanyID AND DivisionID=@DivisionID AND DepartmentID=@DepartmentID  AND OrderDetail.ItemID = [InventoryItems].ItemID  ) Desc "

        End If


        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try
            com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            ' com.Parameters.Add(New SqlParameter("@OrderNo", SqlDbType.NVarChar, 36)).Value = txtoderno.Text
            da.SelectCommand = com
            da.Fill(dt)


        Catch ex As Exception

        End Try

        Return dt
    End Function



    Public Function LoadItemsData(ByVal ItemCategoryID As String) As DataTable
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select  ItemID,[InventoryItems].UnitPrice , [InventoryItems].UnitsPerBox from [InventoryItems] where  ItemID <> 'DEFAULT' AND CompanyID=@CompanyID AND DivisionID=@DivisionID AND DepartmentID=@DepartmentID AND  InventoryItems.IsActive=1 and InventoryItems.WireServiceIdAllowed = 1 "
        If ItemCategoryID.Trim <> "" Then
            ssql = ssql & " and ItemCategoryID='" & ItemCategoryID & "'"
        End If


        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try
            com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@OrderNo", SqlDbType.NVarChar, 36)).Value = txtoderno.Text
            da.SelectCommand = com
            da.Fill(dt)


        Catch ex As Exception

        End Try

        Return dt
    End Function


    Public Sub SetOrderProductEmptyData()
        Dim dt As New DataTable()
        dt.Columns.Add(New DataColumn("InLineNumber"))
        dt.Columns.Add(New DataColumn("OrderNo"))
        dt.Columns.Add(New DataColumn("Product"))
        dt.Columns.Add(New DataColumn("QOH"))
        dt.Columns.Add(New DataColumn("DUMP"))
        dt.Columns.Add(New DataColumn("Q_REQ"))
        dt.Columns.Add(New DataColumn("PRESOLD"))
        dt.Columns.Add(New DataColumn("COLOR_VARIETY"))
        dt.Columns.Add(New DataColumn("REMARKS"))
        dt.Columns.Add(New DataColumn("Q_ORD"))
        dt.Columns.Add(New DataColumn("PACK"))
        dt.Columns.Add(New DataColumn("COST"))
        dt.Columns.Add(New DataColumn("Ext_COSt"))
        dt.Columns.Add(New DataColumn("Vendor_Code"))
        dt.Columns.Add(New DataColumn("Buyer"))
        dt.Columns.Add(New DataColumn("Status"))
        dt.Columns.Add(New DataColumn("Q_Recv"))
        dt.Columns.Add(New DataColumn("ISSUE"))
        Dim dr As DataRow
        dr = dt.NewRow()

        dr("InLineNumber") = 0
        dr("OrderNo") = 1
        dr("Product") = String.Empty
        dr("QOH") = String.Empty
        dr("DUMP") = String.Empty
        dr("Q_REQ") = String.Empty
        dr("PRESOLD") = String.Empty
        dr("COLOR_VARIETY") = String.Empty
        dr("REMARKS") = String.Empty
        dr("Q_ORD") = String.Empty
        dr("PACK") = String.Empty
        dr("COST") = String.Empty
        dr("Ext_COSt") = String.Empty
        dr("Vendor_Code") = String.Empty
        dr("Buyer") = String.Empty
        dr("Status") = "No Action"
        dr("Q_Recv") = String.Empty
        dr("ISSUE") = String.Empty
        dt.Rows.Add(dr)




        '//dr = dt.NewRow();

        OrderHeaderGrid.DataSource = dt
        OrderHeaderGrid.DataBind()

    End Sub

    Public Sub SetOrderProductData()
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT * FROM [PO_Standing_Requisition_Details] where  OrderNo = @OrderNo and CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 order by Product ASC "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
        com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
        com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
        com.Parameters.Add(New SqlParameter("@OrderNo", SqlDbType.NVarChar, 36)).Value = txtoderno.Text
        da.SelectCommand = com
        da.Fill(dt)

        '        Response.Write(dt.Rows.Count)

        If dt.Rows.Count <> 0 Then
            OrderHeaderGrid.PageSize = 500
            OrderHeaderGrid.DataSource = dt
            OrderHeaderGrid.DataBind()
        Else
            SetOrderProductEmptyData()
        End If
        Try
        Catch ex As Exception

        End Try


    End Sub

    Public Function CheckOrderData() As Boolean
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT OrderNo FROM [PO_Standing_Requisition_Header] where  OrderNo = @OrderNo and CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@OrderNo", SqlDbType.NVarChar, 36)).Value = txtoderno.Text
            da.SelectCommand = com
            da.Fill(dt)


            If dt.Rows.Count = 0 Then
                Return True
            End If

        Catch ex As Exception

        End Try

        Return False
    End Function

    Public Sub SetOrderData()
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT * FROM [PO_Standing_Requisition_Header] where  OrderNo = @OrderNo and CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@OrderNo", SqlDbType.NVarChar, 36)).Value = txtoderno.Text
            da.SelectCommand = com
            da.Fill(dt)


            If dt.Rows.Count <> 0 Then
                Try
                    drpCreateDay.SelectedValue = dt.Rows(0)("CreateDay")
                Catch ex As Exception

                End Try

                Try
                    cmblocationid.SelectedValue = dt.Rows(0)("Location")
                Catch ex As Exception

                End Try

                Try
                    txtRemarks.Text = dt.Rows(0)("Remarks")
                Catch ex As Exception

                End Try

                Try
                    drpStatus.SelectedValue = dt.Rows(0)("Status")
                Catch ex As Exception

                End Try

                Try
                    drpType.SelectedValue = dt.Rows(0)("Type")
                Catch ex As Exception

                End Try

                Try
                    txtlastchanged.Text = dt.Rows(0)("LastChangeDateTime")
                Catch ex As Exception

                End Try

                Try
                    txtlastchangedby.Text = dt.Rows(0)("LastChangeBy")
                Catch ex As Exception

                End Try


                Try
                    txttotal.Text = Format(dt.Rows(0)("TotalAmount"), "0.00")
                Catch ex As Exception

                End Try

                Try
                    txtshipdate.Text = dt.Rows(0)("ShipDate")
                Catch ex As Exception

                End Try

                Try
                    txtarrivedate.Text = dt.Rows(0)("ArriveDate")
                Catch ex As Exception

                End Try

                Try
                    txtorderplaced.Text = dt.Rows(0)("OrderPlacedDate")
                Catch ex As Exception

                End Try

                Try
                    txtrecvon.Text = dt.Rows(0)("ReceivedOnDate")
                Catch ex As Exception

                End Try

                Try
                    txtorderby.Text = dt.Rows(0)("OrderBy")
                Catch ex As Exception

                End Try

                Try
                    txtrecvby.Text = dt.Rows(0)("ReceivedBy")
                Catch ex As Exception

                End Try


                Try
                    drpInventoryOrigin.SelectedValue = dt.Rows(0)("InventoryOrigin")
                Catch ex As Exception

                End Try
                'SetShipMethoddropdown(drpInventoryOrigin.SelectedValue)
                Try
                    drpshipemthod.SelectedValue = dt.Rows(0)("ShipMethodID")
                Catch ex As Exception

                End Try
                Dim ss As New Object
                Dim ee As New EventArgs
                Try
                    'drpshipemthod_SelectedIndexChanged(ss, ee)
                Catch ex As Exception

                End Try


            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub SetOriginDdropdown()
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT * FROM [InventoryOrigin] where   CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2  "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            da.SelectCommand = com
            da.Fill(dt)

        Catch ex As Exception

        End Try

        If dt.Rows.Count <> 0 Then
            drpInventoryOrigin.DataSource = dt
            drpInventoryOrigin.DataTextField = "InventoryOriginName"
            drpInventoryOrigin.DataValueField = "InventoryOriginID"
            drpInventoryOrigin.DataBind()
        End If

    End Sub


    Public Sub SetLocationIDdropdown()
        SetOriginDdropdown()
        SetShipMethoddropdown()
        '''''''''''''''''
        Dim obj As New clsOrder_Location
        Dim dt As New Data.DataTable
        obj.CompanyID = CompanyID
        obj.DivisionID = DivisionID
        obj.DepartmentID = DepartmentID
        dt = obj.FillLocation
        If dt.Rows.Count <> 0 Then
            cmblocationid.DataSource = dt
            cmblocationid.DataTextField = "LocationName"
            cmblocationid.DataValueField = "LocationID"
            cmblocationid.DataBind()
            'Setdropdown()
        Else
            cmblocationid.Items.Clear()
            Dim item As New ListItem
            item.Text = "DEFAULT"
            item.Value = "DEFAULT"
            cmblocationid.Items.Add(item)
        End If
        ''''''''''''''''''''
        Dim locationid As String = ""
        Try
            locationid = Session("Locationid")
        Catch ex As Exception

        End Try
        If locationid <> "Corporate" Then
            '  cmblocationid.SelectedIndex = cmblocationid.Items.IndexOf(cmblocationid.Items.FindByValue(locationid))
            ' cmblocationid.Enabled = False
        End If

    End Sub
    'CREATE TABLE [dbo].[PO_Standing_Requisition_Header](
    '	[CompanyID] [nvarchar](36) Not NULL,
    '	[DivisionID] [nvarchar](36) Not NULL,
    '	[DepartmentID] [nvarchar](36) Not NULL,
    '	[OrderNo] [nvarchar](50) Not NULL,
    '	[Location] [nvarchar](50) Not NULL,
    '	[Remarks] [nvarchar](2000) Not NULL,
    '	[Status] [nvarchar](50) Not NULL,
    '	[Type] [nvarchar](50) Not NULL,
    '	[LastChangeDateTime] [datetime] NULL,
    '	[LastChangeBy] [nvarchar](50) Not NULL,
    '	[TotalAmount] [money] Not NULL,
    '	[ShipDate] [datetime] NULL,
    '	[ArriveDate] [datetime] NULL,
    '	[OrderPlacedDate] [datetime] NULL,
    '	[ReceivedOnDate] [datetime] NULL,
    '	[OrderBy] [nvarchar](50) Not NULL,
    '	[ReceivedBy] [nvarchar](50) Not NULL
    ') ON [PRIMARY]

    Dim AllVendor As Boolean = True

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnsave.Click, btnsaveUP.Click
        txtlastchanged.Text = Date.Now
        txtlastchangedby.Text = EmployeeID


        Checkonsubmit()

        AllVendor = True

        If AllVendor Then
            drpStatus.SelectedValue = "Entry Completed"
            txtorderplaced.Text = Date.Now
            txtorderby.Text = EmployeeID
            savechages()
            saveall()
            Dim MVC As String = ""
            Try
                MVC = Request.QueryString("MVC")
            Catch ex As Exception

            End Try

            If MVC = "1" Then
                Response.Redirect("http://bpom.quickflora.com/Web/BPO.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&TerminalID=" & "DEFAULT" & "&locationid=" & Session("Locationid") & "&EmployeeID=" & EmployeeID & "&Session_SessionID=" & Session.SessionID)

            Else
                Response.Redirect("StandingRequisitionOrderList.aspx")
            End If
        Else
            dvsavealert.Visible = True
            lblsavealert.Text = "Please select Vendor for each items to submit complete Request."

            saveall()

            dvsave.Visible = False
            lblsave.Text = ""
        End If



    End Sub

    Public Function saveall() As Boolean

        Dim rs As SqlDataReader
        Dim OrderNo As String = ""

        Dim PopOrderNo As New CustomOrder()
        If IsNumeric(txtoderno.Text.Trim) = False Then
            rs = PopOrderNo.GetNextOrdNumber(CompanyID, DepartmentID, DivisionID, "NextSTRequisitionNumber")
            While rs.Read()
                OrderNo = rs("NextNumberValue")
            End While
            rs.Close()
            txtoderno.Text = OrderNo
        Else
            OrderNo = txtoderno.Text
        End If

        txtlastchanged.Text = Date.Now
        txtlastchangedby.Text = EmployeeID


        Dim connec As New SqlConnection(constr)
        Dim qry As String

        If CheckOrderData() Then
            qry = "insert into PO_Standing_Requisition_Header(CreateDay,ShipMethodID,InventoryOrigin, CompanyID, DivisionID, DepartmentID, OrderNo,Location,Remarks,Status,Type,LastChangeDateTime,LastChangeBy,TotalAmount,ShipDate,ArriveDate,OrderPlacedDate,ReceivedOnDate,OrderBy,ReceivedBy) " _
             & " values(@CreateDay,@ShipMethodID,@InventoryOrigin,@CompanyID, @DivisionID, @DepartmentID, @OrderNo,@Location,@Remarks,@Status,@Type,@LastChangeDateTime,@LastChangeBy,@TotalAmount,@ShipDate,@ArriveDate,@OrderPlacedDate,@ReceivedOnDate,@OrderBy,@ReceivedBy)"
        Else
            qry = "Update PO_Standing_Requisition_Header SET CreateDay=@CreateDay, ShipMethodID =@ShipMethodID, InventoryOrigin=@InventoryOrigin, Location=@Location,Remarks=@Remarks,Status=@Status,Type=@Type,LastChangeDateTime=@LastChangeDateTime,LastChangeBy=@LastChangeBy,TotalAmount=@TotalAmount,ShipDate=@ShipDate,ArriveDate=@ArriveDate,OrderPlacedDate=@OrderPlacedDate,ReceivedOnDate=@ReceivedOnDate,OrderBy=@OrderBy,ReceivedBy=@ReceivedBy   " _
             & "  Where CompanyID=@CompanyID AND DivisionID=@DivisionID AND DepartmentID=@DepartmentID AND OrderNo=@OrderNo"

            Logchangehistory("", EmployeeID, "PO_Standing_Requisition_Header", "Location", cmblocationid.SelectedValue, txtoderno.Text, "", "Text")
            Logchangehistory("", EmployeeID, "PO_Standing_Requisition_Header", "Remarks", txtRemarks.Text, txtoderno.Text, "", "Text")
            Logchangehistory("", EmployeeID, "PO_Standing_Requisition_Header", "Status", drpStatus.SelectedValue, txtoderno.Text, "", "Text")
            Logchangehistory("", EmployeeID, "PO_Standing_Requisition_Header", "Type", drpType.SelectedValue, txtoderno.Text, "", "Text")
            Logchangehistory("", EmployeeID, "PO_Standing_Requisition_Header", "LastChangeDateTime", txtlastchanged.Text, txtoderno.Text, "", "Date")
            Logchangehistory("", EmployeeID, "PO_Standing_Requisition_Header", "LastChangeBy", txtlastchangedby.Text, txtoderno.Text, "", "Text")
            Logchangehistory("", EmployeeID, "PO_Standing_Requisition_Header", "TotalAmount", txttotal.Text, txtoderno.Text, "", "Money")
            Logchangehistory("", EmployeeID, "PO_Standing_Requisition_Header", "ShipDate", txtshipdate.Text, txtoderno.Text, "", "Date")
            Logchangehistory("", EmployeeID, "PO_Standing_Requisition_Header", "ArriveDate", txtarrivedate.Text, txtoderno.Text, "", "Date")
            Logchangehistory("", EmployeeID, "PO_Standing_Requisition_Header", "OrderPlacedDate", txtorderplaced.Text, txtoderno.Text, "", "Date")
            Logchangehistory("", EmployeeID, "PO_Standing_Requisition_Header", "ReceivedOnDate", txtrecvon.Text, txtoderno.Text, "", "Date")
            Logchangehistory("", EmployeeID, "PO_Standing_Requisition_Header", "OrderBy", txtorderby.Text, txtoderno.Text, "", "Text")
            Logchangehistory("", EmployeeID, "PO_Standing_Requisition_Header", "ReceivedBy", txtrecvby.Text, txtoderno.Text, "", "Text")
        End If

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)

        com.Parameters.Add(New SqlParameter("@CreateDay", SqlDbType.NVarChar)).Value = drpCreateDay.SelectedValue
        com.Parameters.Add(New SqlParameter("@ShipMethodID", SqlDbType.NVarChar)).Value = drpshipemthod.SelectedValue

        com.Parameters.Add(New SqlParameter("@InventoryOrigin", SqlDbType.NVarChar)).Value = drpInventoryOrigin.SelectedValue
        com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
        com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar)).Value = Me.DivisionID
        com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar)).Value = Me.DepartmentID
        com.Parameters.Add(New SqlParameter("@OrderNo", SqlDbType.NVarChar)).Value = txtoderno.Text
        com.Parameters.Add(New SqlParameter("@Location", SqlDbType.NVarChar)).Value = cmblocationid.SelectedValue
        com.Parameters.Add(New SqlParameter("@Remarks", SqlDbType.NVarChar)).Value = txtRemarks.Text
        com.Parameters.Add(New SqlParameter("@Status", SqlDbType.NVarChar)).Value = drpStatus.SelectedValue
        com.Parameters.Add(New SqlParameter("@Type", SqlDbType.NVarChar)).Value = drpType.SelectedValue
        com.Parameters.Add(New SqlParameter("@LastChangeDateTime", SqlDbType.NVarChar)).Value = txtlastchanged.Text
        com.Parameters.Add(New SqlParameter("@LastChangeBy", SqlDbType.NVarChar)).Value = txtlastchangedby.Text
        com.Parameters.Add(New SqlParameter("@TotalAmount", SqlDbType.NVarChar)).Value = txttotal.Text
        com.Parameters.Add(New SqlParameter("@ShipDate", SqlDbType.NVarChar)).Value = txtshipdate.Text
        com.Parameters.Add(New SqlParameter("@ArriveDate", SqlDbType.NVarChar)).Value = txtarrivedate.Text
        com.Parameters.Add(New SqlParameter("@OrderPlacedDate", SqlDbType.NVarChar)).Value = txtorderplaced.Text
        com.Parameters.Add(New SqlParameter("@ReceivedOnDate", SqlDbType.NVarChar)).Value = txtrecvon.Text
        com.Parameters.Add(New SqlParameter("@OrderBy", SqlDbType.NVarChar)).Value = txtorderby.Text
        com.Parameters.Add(New SqlParameter("@ReceivedBy", SqlDbType.NVarChar)).Value = txtrecvby.Text

        com.Connection.Open()
        com.ExecuteNonQuery()
        com.Connection.Close()

        lblsave.Text = "Requisition <strong>#" & txtoderno.Text & "</strong> saved Successfully "
        dvsave.Visible = True

        Return True
    End Function

    '    CREATE TABLE [dbo].[PO_Standing_Requisition_Details](
    '	[CompanyID] [nvarchar](36) Not NULL,
    '	[DivisionID] [nvarchar](36) Not NULL,
    '	[DepartmentID] [nvarchar](36) Not NULL,
    '	[OrderNo] [nvarchar](50) Not NULL,
    '	[Product] [nvarchar](50) NULL,
    '	[QOH] [nvarchar](50) NULL,
    '	[DUMP] [nvarchar](50) NULL,
    '	[Q_REQ] [nvarchar](50) NULL,
    '	[PRESOLD] [nvarchar](50) NULL,
    '	[COLOR_VARIETY] [nvarchar](50) NULL,
    '	[REMARKS] [nvarchar](50) NULL,
    '	[Q_ORD] [nvarchar](50) NULL,
    '	[PACK] [nvarchar](50) NULL,
    '	[COST] [nvarchar](50) NULL,
    '	[Ext_COSt] [nvarchar](50) NULL,
    '	[Vendor_Code] [nvarchar](50) NULL,
    '	[Buyer] [nvarchar](50) NULL,
    '	[Status] [nvarchar](50) NULL,
    '	[Q_Recv] [nvarchar](50) NULL,
    '	[ISSUE] [nvarchar](50) NULL
    ') ON [PRIMARY]

    'GO

    Private Sub btnaddnew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnaddnew.Click
        savechages()
        saveall()

        Dim dt As New DataTable()
        dt.Columns.Add(New DataColumn("InLineNumber"))
        dt.Columns.Add(New DataColumn("OrderNo"))
        dt.Columns.Add(New DataColumn("Product"))
        dt.Columns.Add(New DataColumn("QOH"))
        dt.Columns.Add(New DataColumn("DUMP"))
        dt.Columns.Add(New DataColumn("Q_REQ"))
        dt.Columns.Add(New DataColumn("PRESOLD"))
        dt.Columns.Add(New DataColumn("COLOR_VARIETY"))
        dt.Columns.Add(New DataColumn("REMARKS"))
        dt.Columns.Add(New DataColumn("Q_ORD"))
        dt.Columns.Add(New DataColumn("PACK"))
        dt.Columns.Add(New DataColumn("COST"))
        dt.Columns.Add(New DataColumn("Ext_COSt"))
        dt.Columns.Add(New DataColumn("Vendor_Code"))
        dt.Columns.Add(New DataColumn("Buyer"))
        dt.Columns.Add(New DataColumn("Status"))
        dt.Columns.Add(New DataColumn("Q_Recv"))
        dt.Columns.Add(New DataColumn("ISSUE"))

        If True Then
            Dim dr As DataRow
            '[InLineNumber]
            Dim n As String = ""
            n = DateTime.Now.Day.ToString() & DateTime.Now.Month.ToString() & DateTime.Now.Year.ToString() & DateTime.Now.Hour.ToString() & DateTime.Now.Minute.ToString() & DateTime.Now.Second.ToString() & DateTime.Now.Millisecond.ToString()
            dr = dt.NewRow()
            dr("InLineNumber") = n & "@NewItem@" & txtoderno.Text
            dr("OrderNo") = 1
            dr("Product") = ""
            dr("QOH") = "0"
            dr("DUMP") = "0"
            dr("Q_REQ") = "0"
            dr("PRESOLD") = "0"
            dr("COLOR_VARIETY") = ""
            dr("REMARKS") = ""
            dr("Q_ORD") = "0"
            dr("PACK") = "1"
            dr("COST") = "0"
            dr("Ext_COSt") = "0"
            dr("Vendor_Code") = ""
            dr("Buyer") = ""
            dr("Status") = "No Action"
            dr("Q_Recv") = "0"
            dr("ISSUE") = "0"
            dt.Rows.Add(dr)
        End If

        For Each row As GridViewRow In OrderHeaderGrid.Rows

            Dim InLineNumber As String = (OrderHeaderGrid.DataKeys(row.RowIndex).Value)
            Dim chk As CheckBox = row.FindControl("chkseleect")
            Dim txtProduct As TextBox = row.FindControl("txtProduct")
            Dim txtQOH As TextBox = row.FindControl("txtQOH")
            Dim txtDUMP As TextBox = row.FindControl("txtDUMP")
            Dim txtQ_REQ As TextBox = row.FindControl("txtQ_REQ")
            Dim txtPRESOLD As TextBox = row.FindControl("txtPRESOLD")
            Dim txtCOLOR_VARIETY As TextBox = row.FindControl("txtCOLOR_VARIETY")
            Dim txtREMARKS As TextBox = row.FindControl("txtREMARKS")
            Dim txtQ_ORD As TextBox = row.FindControl("txtQ_ORD")
            Dim txtPACK As TextBox = row.FindControl("txtPACK")
            Dim txtCOST As TextBox = row.FindControl("txtCOST")
            Dim txtExt_COSt As TextBox = row.FindControl("txtExt_COSt")
            Dim txtVendor_Code As TextBox = row.FindControl("txtVendor_Code")
            Dim txtBuyer As Label = row.FindControl("txtBuyer")

            Dim drpPOStatus As New DropDownList
            drpPOStatus = row.FindControl("drpPOStatus")

            Dim txtStatus As Label = row.FindControl("txtStatus")
            txtStatus.Text = drpPOStatus.SelectedValue

            Dim drpBuyer As New DropDownList
            drpBuyer = row.FindControl("drpBuyer")
            Try
                txtBuyer.Text = drpBuyer.SelectedValue
            Catch ex As Exception

            End Try
            Dim txtQ_Recv As TextBox = row.FindControl("txtQ_Recv")
            Dim txtISSUE As TextBox = row.FindControl("txtISSUE")

            Dim dr As DataRow
            '[InLineNumber]
            dr = dt.NewRow()
            dr("InLineNumber") = InLineNumber
            dr("OrderNo") = txtoderno.Text
            dr("Product") = txtProduct.Text
            dr("QOH") = txtQOH.Text
            dr("DUMP") = txtDUMP.Text
            dr("Q_REQ") = txtQ_REQ.Text
            dr("PRESOLD") = txtPRESOLD.Text
            dr("COLOR_VARIETY") = txtCOLOR_VARIETY.Text
            dr("REMARKS") = txtREMARKS.Text
            dr("Q_ORD") = txtQ_ORD.Text
            dr("PACK") = txtPACK.Text
            dr("COST") = txtCOST.Text
            dr("Ext_COSt") = txtExt_COSt.Text
            dr("Vendor_Code") = txtVendor_Code.Text
            dr("Buyer") = txtBuyer.Text
            dr("Status") = txtStatus.Text
            dr("Q_Recv") = txtQ_Recv.Text
            dr("ISSUE") = txtISSUE.Text
            dt.Rows.Add(dr)
        Next



        OrderHeaderGrid.PageSize = 500
        OrderHeaderGrid.DataSource = dt
        OrderHeaderGrid.DataBind()

        Exit Sub
        btnLoadProductList.Enabled = False
        btnLoadProductList.Enabled = False
        btnFlowers.Enabled = False
        btnGreens.Enabled = False
        btnHardgoods.Enabled = False
        btnPlants.Enabled = False

        lblsave.Text = ""
        dvsave.Visible = False

        Dim OrderNo As String = ""
        OrderNo = txtoderno.Text


        Dim connec As New SqlConnection(constr)
        Dim qry As String

        If OrderNo <> "" Then
            qry = "insert into PO_Standing_Requisition_Details ([CompanyID] ,[DivisionID] ,[DepartmentID] ,[OrderNo] ,[Product] ,[QOH] ,[DUMP] ,[Q_REQ] ,[PRESOLD] ,[COLOR_VARIETY] ,[REMARKS] ,[Q_ORD] ,[PACK] ,[COST] ,[Ext_COSt] ,[Vendor_Code] ,[Buyer] ,[Status] ,[Q_Recv] ,[ISSUE],[PONO])" _
             & " values(@CompanyID ,@DivisionID ,@DepartmentID ,@OrderNo ,@Product ,@QOH ,@DUMP ,@Q_REQ ,@PRESOLD ,@COLOR_VARIETY ,@REMARKS ,@Q_ORD ,@PACK ,@COST ,@Ext_COSt ,@Vendor_Code ,@Buyer ,@Status ,@Q_Recv ,@ISSUE,'') "

            Dim com As SqlCommand
            com = New SqlCommand(qry, connec)

            com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@OrderNo", SqlDbType.NVarChar)).Value = OrderNo
            com.Parameters.Add(New SqlParameter("@Product", SqlDbType.NVarChar)).Value = ""
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


        SetOrderProductData()

    End Sub

    Public Function getitemname(ByVal id As String) As String
        Dim itemname As String = ""
        Dim sql As String = "select ItemName from [InventoryItems] where  ItemID=@id AND CompanyID=@CompanyID AND DivisionID=@DivisionID AND DepartmentID=@DepartmentID  "
        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand(sql, myCon)
        myCommand.Parameters.AddWithValue("@CompanyID", CompanyID)
        myCommand.Parameters.AddWithValue("@DivisionID", DivisionID)
        myCommand.Parameters.AddWithValue("@DepartmentID", DepartmentID)
        myCommand.Parameters.AddWithValue("@id", id)

        Dim da As New SqlDataAdapter(myCommand)
        Dim dt As New DataTable
        da.Fill(dt)
        If dt.Rows.Count <> 0 Then
            Try
                itemname = dt.Rows(0)("ItemName").ToString()
            Catch ex As Exception

            End Try

        End If


        Return itemname
    End Function


    Private Sub OrderHeaderGrid_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles OrderHeaderGrid.RowDataBound
        'Exit Sub
        Dim txtInLineNumber As New TextBox
        'Response.Write(e.Row.RowType)
        If e.Row.RowType = DataControlRowType.DataRow Then
            txtInLineNumber = e.Row.FindControl("txtInLineNumber")
            If txtInLineNumber.Text = "0" Then
                e.Row.Visible = False
            End If

            e.Row.Attributes("id") = txtInLineNumber.Text

            Dim txtProductname As New Label
            txtProductname = e.Row.FindControl("txtProductname")


            Dim txtProduct As TextBox = e.Row.FindControl("txtProduct")

            txtProductname.Text = getitemname(txtProduct.Text.Trim)


            Dim txtQOH As TextBox = e.Row.FindControl("txtQOH")
            Dim txtDUMP As TextBox = e.Row.FindControl("txtDUMP")
            Dim txtQ_REQ As TextBox = e.Row.FindControl("txtQ_REQ")
            Dim txtPRESOLD As TextBox = e.Row.FindControl("txtPRESOLD")
            Dim txtCOLOR_VARIETY As TextBox = e.Row.FindControl("txtCOLOR_VARIETY")
            Dim txtREMARKS As TextBox = e.Row.FindControl("txtREMARKS")
            Dim txtQ_ORD As TextBox = e.Row.FindControl("txtQ_ORD")
            Dim txtPACK As TextBox = e.Row.FindControl("txtPACK")
            Dim txtCOST As TextBox = e.Row.FindControl("txtCOST")
            Dim txtExt_COSt As TextBox = e.Row.FindControl("txtExt_COSt")
            Dim txtVendor_Code As TextBox = e.Row.FindControl("txtVendor_Code")
            Dim txtBuyer As Label = e.Row.FindControl("txtBuyer")
            Dim txtStatus As Label = e.Row.FindControl("txtStatus")
            Dim txtQ_Recv As TextBox = e.Row.FindControl("txtQ_Recv")
            Dim txtISSUE As TextBox = e.Row.FindControl("txtISSUE")

            txtProduct.Attributes.Add("autocomplete", "off")
            txtQOH.Attributes.Add("autocomplete", "off")
            txtDUMP.Attributes.Add("autocomplete", "off")
            txtQ_REQ.Attributes.Add("autocomplete", "off")
            txtPRESOLD.Attributes.Add("autocomplete", "off")
            txtCOLOR_VARIETY.Attributes.Add("autocomplete", "off")
            txtREMARKS.Attributes.Add("autocomplete", "off")
            txtQ_ORD.Attributes.Add("autocomplete", "off")
            txtPACK.Attributes.Add("autocomplete", "off")
            txtCOST.Attributes.Add("autocomplete", "off")
            txtExt_COSt.Attributes.Add("autocomplete", "off")
            txtVendor_Code.Attributes.Add("autocomplete", "off")
            txtBuyer.Attributes.Add("autocomplete", "off")
            txtStatus.Attributes.Add("autocomplete", "off")
            txtQ_Recv.Attributes.Add("autocomplete", "off")
            txtISSUE.Attributes.Add("autocomplete", "off")

            If False Then
                txtProduct.Enabled = False
                txtQOH.Enabled = False
                txtDUMP.Enabled = False
                txtQ_REQ.Enabled = False
                txtPRESOLD.Enabled = False
                txtCOLOR_VARIETY.Enabled = False
                ''txtREMARKS.Enabled = False
                txtQ_ORD.Enabled = False
                txtPACK.Enabled = False
                txtCOST.Enabled = False
                txtExt_COSt.Enabled = False
                txtVendor_Code.Enabled = False
                txtBuyer.Enabled = False
                txtStatus.Enabled = False
                txtQ_Recv.Enabled = False
                txtISSUE.Enabled = False
            End If
            If txtStatus.Text = "Bought" Then
                txtQ_Recv.Enabled = True
                txtISSUE.Enabled = True
            End If
            txtVendor_Code.Attributes.Add("placeholder", "SEARCH VENDOR")
            txtVendor_Code.Attributes.Add("onKeyUp", "SendQuery(this.value,this,'" & txtInLineNumber.Text & "')")

            txtProduct.Attributes.Add("onfocus", "myFocusFunction(this)")
            ' txtProduct.Attributes.Add("onblur", "Saveitem(this,'" & txtInLineNumber.Text & "','Product','" & txtProduct.Text & "')")
            If Me.CompanyID = "DierbergsMarkets,Inc63017" Or Me.CompanyID = "QuickfloraDemo" Then
                txtProduct.Attributes.Add("onblur", "ItemNamesearch('" & txtProduct.ClientID & "','" & txtInLineNumber.Text & "','" & txtPACK.ClientID & "','" & txtCOST.ClientID & "','" & txtVendor_Code.ClientID & "')")
            Else
                txtProduct.Attributes.Add("onblur", "Saveitem(this,'" & txtInLineNumber.Text & "','Product','" & txtProduct.Text & "')")
            End If



            txtProduct.Attributes.Add("placeholder", "SEARCH Item")
            txtProduct.Attributes.Add("onKeyUp", "SendQuery2(this.value,this,'" & txtInLineNumber.Text & "','" & txtPACK.ClientID & "','" & txtCOST.ClientID & "')")

            txtQOH.Attributes.Add("onfocus", "myFocusFunction(this)")
            txtQOH.Attributes.Add("onblur", "Saveitem(this,'" & txtInLineNumber.Text & "','QOH','" & txtQOH.Text & "')")

            txtDUMP.Attributes.Add("onfocus", "myFocusFunction(this)")
            txtDUMP.Attributes.Add("onblur", "Saveitem(this,'" & txtInLineNumber.Text & "','DUMP','" & txtDUMP.Text & "')")

            txtQ_REQ.Attributes.Add("onfocus", "myFocusFunction(this)")
            ' txtQ_REQ.Attributes.Add("onblur", "Saveitem(this,'" & txtInLineNumber.Text & "','Q_REQ','" & txtQ_REQ.Text & "')")

            ' txtQ_REQ.Attributes.Add("onfocus", "myFocusFunction(this)")
            If Me.CompanyID = "DierbergsMarkets,Inc63017" Or Me.CompanyID = "QuickfloraDemo" Then
                ' txtQ_REQ.Attributes.Add("onblur", "Saveitemandcopy(this,'" & txtInLineNumber.Text & "','Q_REQ','" & txtQ_REQ.Text & "','" & txtQ_ORD.ClientID & "')")
                txtQ_REQ.Attributes.Add("onblur", "myFocusFunctiontotalnewqreq('" & txtQ_REQ.ClientID & "','" & txtExt_COSt.ClientID & "','" & txtInLineNumber.Text & "','" & txtQ_ORD.ClientID & "','" & txtPACK.ClientID & "','" & txtCOST.ClientID & "','" & txtVendor_Code.ClientID & "')")
            Else
                txtQ_REQ.Attributes.Add("onblur", "Saveitem(this,'" & txtInLineNumber.Text & "','Q_REQ','" & txtQ_REQ.Text & "')")
            End If



            txtPRESOLD.Attributes.Add("onfocus", "myFocusFunction(this)")
            txtPRESOLD.Attributes.Add("onblur", "Saveitem(this,'" & txtInLineNumber.Text & "','PRESOLD','" & txtPRESOLD.Text & "')")

            txtCOLOR_VARIETY.Attributes.Add("onfocus", "myFocusFunction(this)")
            txtCOLOR_VARIETY.Attributes.Add("onblur", "Saveitem(this,'" & txtInLineNumber.Text & "','COLOR_VARIETY','" & txtCOLOR_VARIETY.Text & "')")

            txtREMARKS.Attributes.Add("onfocus", "myFocusFunction(this)")
            txtREMARKS.Attributes.Add("onblur", "Saveitem(this,'" & txtInLineNumber.Text & "','REMARKS','" & txtREMARKS.Text & "')")



            txtQ_ORD.Attributes.Add("onfocus", "myFocusFunction(this)")
            txtQ_ORD.Attributes.Add("onblur", "myFocusFunctiontotal('" & txtExt_COSt.ClientID & "','" & txtInLineNumber.Text & "','" & txtQ_ORD.ClientID & "','" & txtPACK.ClientID & "','" & txtCOST.ClientID & "')")
            'txtQ_ORD.Attributes.Add("onblur", "Saveitem(this,'" & txtInLineNumber.Text & "','Q_ORD','" & txtQ_ORD.Text & "')")

            txtPACK.Attributes.Add("onfocus", "myFocusFunction(this)")
            txtPACK.Attributes.Add("onblur", "myFocusFunctiontotal('" & txtExt_COSt.ClientID & "','" & txtInLineNumber.Text & "','" & txtQ_ORD.ClientID & "','" & txtPACK.ClientID & "','" & txtCOST.ClientID & "')")
            'txtPACK.Attributes.Add("onblur", "Saveitem(this,'" & txtInLineNumber.Text & "','PACK','" & txtPACK.Text & "')")

            txtCOST.Attributes.Add("onfocus", "myFocusFunction(this)")
            txtCOST.Attributes.Add("onblur", "myFocusFunctiontotal('" & txtExt_COSt.ClientID & "','" & txtInLineNumber.Text & "','" & txtQ_ORD.ClientID & "','" & txtPACK.ClientID & "','" & txtCOST.ClientID & "')")
            'txtCOST.Attributes.Add("onblur", "Saveitem(this,'" & txtInLineNumber.Text & "','COST','" & txtCOST.Text & "')")

            'txtExt_COSt.Attributes.Add("onfocus", "myFocusFunctiontotal(this,'" & txtQ_ORD.ClientID & "','" & txtPACK.ClientID & "','" & txtCOST.ClientID & "')")
            'txtExt_COSt.Attributes.Add("onblur", "Saveitem(this,'" & txtInLineNumber.Text & "','Ext_COSt','" & txtExt_COSt.Text & "')")

            txtExt_COSt.Enabled = False



            txtVendor_Code.Attributes.Add("onfocus", "myFocusFunction(this)")
            txtVendor_Code.Attributes.Add("onblur", "Saveitem(this,'" & txtInLineNumber.Text & "','Vendor_Code','" & txtVendor_Code.Text & "')")


            txtVendor_Code.Attributes.Add("onfocus", "myFocusFunction(this)")
            txtVendor_Code.Attributes.Add("onblur", "Saveitem(this,'" & txtInLineNumber.Text & "','Vendor_Code','" & txtVendor_Code.Text & "')")


            ''            txtBuyer.Attributes.Add("onfocus", "myFocusFunction(this)")
            ''txtBuyer.Attributes.Add("onblur", "Saveitem(this,'" & txtInLineNumber.Text & "','Buyer','" & txtBuyer.Text & "')")

            'Dim drpBuyer As New DropDownList
            'drpBuyer = e.Row.FindControl("drpBuyer")

            'drpBuyer.Attributes.Add("onfocus", "myFocusFunction(this)")
            'drpBuyer.Attributes.Add("onchange", "Saveitem(this,'" & txtInLineNumber.Text & "','Buyer','" & txtBuyer.Text & "')")
            ''myonblurFunction
            'drpBuyer.Attributes.Add("onblur", "myonblurFunction(this)")

            'If True Then 'Me.CompanyID <> "mccarthyg" Then
            '    drpBuyer.Items.Clear()
            '    Dim PopOrderType As New CustomOrder()
            '    Dim rs As SqlDataReader

            '    Try
            '        rs = PopOrderType.PopulateEmployees(CompanyID, DepartmentID, DivisionID)
            '        drpBuyer.DataTextField = "EmployeeName"
            '        drpBuyer.DataValueField = "EmployeeID"
            '        drpBuyer.DataSource = rs
            '        drpBuyer.DataBind()
            '        drpBuyer.SelectedValue = EmployeeID ' Session("EmployeeUserName")
            '        rs.Close()
            '    Catch ex As Exception

            '    End Try


            '    drpBuyer.Items.Insert(0, (New ListItem("-Select-", "0")))

            'End If

            'If txtBuyer.Text.Trim <> "" Then
            '    Try
            '        drpBuyer.SelectedValue = txtBuyer.Text
            '    Catch ex As Exception

            '    End Try
            'End If

            '' txtStatus.Attributes.Add("onfocus", "myFocusFunction(this)")
            ''txtStatus.Attributes.Add("onblur", "Saveitem(this,'" & txtInLineNumber.Text & "','Status','" & txtStatus.Text & "')")
            '',[Status] ,[Q_Recv] ,[ISSUE]

            If txtStatus.Text.Trim = "Pending Email" Then
                e.Row.BackColor = Drawing.Color.White
            End If

            If txtStatus.Text.Trim = "Pending-Stg Email" Then
                e.Row.BackColor = Drawing.Color.White
            End If

            If txtStatus.Text.Trim = "Do Not Touch" Then
                e.Row.BackColor = Drawing.Color.FromArgb(204, 204, 255)
            End If

            If txtStatus.Text.Trim = "Wed" Then
                e.Row.BackColor = Drawing.Color.FromArgb(255, 204, 204)
            End If

            If txtStatus.Text.Trim = "Not Avail" Then
                e.Row.BackColor = Drawing.Color.FromArgb(204, 204, 204)
            End If

            If txtStatus.Text.Trim = "Pending" Then
                e.Row.BackColor = Drawing.Color.FromArgb(153, 255, 153)
            End If

            If txtStatus.Text.Trim = "Bought Wed" Then
                e.Row.BackColor = Drawing.Color.FromArgb(255, 204, 204)
            End If

            If txtStatus.Text.Trim = "In Process" Then
                e.Row.BackColor = Drawing.Color.FromArgb(204, 255, 255)
            End If

            If txtStatus.Text.Trim = "Bought" Then
                e.Row.BackColor = Drawing.Color.FromArgb(153, 255, 255)
            End If

            If txtStatus.Text.Trim = "No Action" Then
                e.Row.BackColor = Drawing.Color.FromArgb(255, 255, 153)
            End If

            If txtStatus.Text.Trim = "Pending Auction" Then
                e.Row.BackColor = Drawing.Color.FromArgb(153, 255, 153)
            End If

            If txtStatus.Text.Trim = "Wed In Process" Then
                e.Row.BackColor = Drawing.Color.FromArgb(255, 204, 204)
            End If



            'Dim drpPOStatus As New DropDownList
            'drpPOStatus = e.Row.FindControl("drpPOStatus")

            'If txtStatus.Text.Trim <> "" Then
            '    Try
            '        drpPOStatus.SelectedValue = txtStatus.Text
            '    Catch ex As Exception

            '    End Try
            'End If

            'drpPOStatus.Attributes.Add("onfocus", "myFocusFunction(this)")
            'drpPOStatus.Attributes.Add("onchange", "Saveitem(this,'" & txtInLineNumber.Text & "','Status','" & txtStatus.Text & "')")
            ''myonblurFunction
            'drpPOStatus.Attributes.Add("onblur", "myonblurFunction(this)")



            txtQ_Recv.Attributes.Add("onfocus", "myFocusFunction(this)")
            txtQ_Recv.Attributes.Add("onblur", "Saveitem(this,'" & txtInLineNumber.Text & "','Q_Recv','" & txtQ_Recv.Text & "')")

            txtISSUE.Attributes.Add("onfocus", "myFocusFunction(this)")
            txtISSUE.Attributes.Add("onblur", "Saveitem(this,'" & txtInLineNumber.Text & "','ISSUE','" & txtISSUE.Text & "')")



        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        'Dim InLineNumber As String = ""
        Dim dt As New DataTable()
        dt.Columns.Add(New DataColumn("InLineNumber"))
        dt.Columns.Add(New DataColumn("OrderNo"))
        dt.Columns.Add(New DataColumn("Product"))
        dt.Columns.Add(New DataColumn("QOH"))
        dt.Columns.Add(New DataColumn("DUMP"))
        dt.Columns.Add(New DataColumn("Q_REQ"))
        dt.Columns.Add(New DataColumn("PRESOLD"))
        dt.Columns.Add(New DataColumn("COLOR_VARIETY"))
        dt.Columns.Add(New DataColumn("REMARKS"))
        dt.Columns.Add(New DataColumn("Q_ORD"))
        dt.Columns.Add(New DataColumn("PACK"))
        dt.Columns.Add(New DataColumn("COST"))
        dt.Columns.Add(New DataColumn("Ext_COSt"))
        dt.Columns.Add(New DataColumn("Vendor_Code"))
        dt.Columns.Add(New DataColumn("Buyer"))
        dt.Columns.Add(New DataColumn("Status"))
        dt.Columns.Add(New DataColumn("Q_Recv"))
        dt.Columns.Add(New DataColumn("ISSUE"))



        For Each row As GridViewRow In OrderHeaderGrid.Rows

            Try

                'Dim InLineNumber As String = (OrderHeaderGrid.DataKeys(row.RowIndex).Value)
                'Dim chk As CheckBox = row.FindControl("chkseleect")

                Dim InLineNumber As String = (OrderHeaderGrid.DataKeys(row.RowIndex).Value)
                Dim chk As CheckBox = row.FindControl("chkseleect")
                Dim txtProduct As TextBox = row.FindControl("txtProduct")
                Dim txtQOH As TextBox = row.FindControl("txtQOH")
                Dim txtDUMP As TextBox = row.FindControl("txtDUMP")
                Dim txtQ_REQ As TextBox = row.FindControl("txtQ_REQ")
                Dim txtPRESOLD As TextBox = row.FindControl("txtPRESOLD")
                Dim txtCOLOR_VARIETY As TextBox = row.FindControl("txtCOLOR_VARIETY")
                Dim txtREMARKS As TextBox = row.FindControl("txtREMARKS")
                Dim txtQ_ORD As TextBox = row.FindControl("txtQ_ORD")
                Dim txtPACK As TextBox = row.FindControl("txtPACK")
                Dim txtCOST As TextBox = row.FindControl("txtCOST")
                Dim txtExt_COSt As TextBox = row.FindControl("txtExt_COSt")
                Dim txtVendor_Code As TextBox = row.FindControl("txtVendor_Code")
                Dim txtBuyer As Label = row.FindControl("txtBuyer")

                Dim drpPOStatus As New DropDownList
                drpPOStatus = row.FindControl("drpPOStatus")

                Dim txtStatus As Label = row.FindControl("txtStatus")
                txtStatus.Text = drpPOStatus.SelectedValue

                Dim drpBuyer As New DropDownList
                drpBuyer = row.FindControl("drpBuyer")
                Try
                    txtBuyer.Text = drpBuyer.SelectedValue
                Catch ex As Exception

                End Try
                Dim txtQ_Recv As TextBox = row.FindControl("txtQ_Recv")
                Dim txtISSUE As TextBox = row.FindControl("txtISSUE")


                If chk.Checked Then
                    Dim connec As New SqlConnection(constr)
                    Dim qry As String
                    If InLineNumber <> "0" Then
                        ' lblsavealert.Text = " Delete InLineNumber1 = " & InLineNumber

                        If IsNumeric(InLineNumber) = False Then
                            Dim dt2 As New DataTable
                            dt2 = SelectInsertPO_AjaxBinding(InLineNumber)
                            If dt2.Rows.Count = 0 Then

                            Else
                                InLineNumber = dt2.Rows(0)("InLineNumber")
                            End If
                        End If

                        If IsNumeric(InLineNumber) = False Then
                            Continue For
                        End If
                        'lblsavealert.Text = lblsavechanges.Text & " Delete InLineNumber2 = " & InLineNumber
                        'lblsavealert.Visible = True
                        'dvsavealert.Visible = True


                        qry = "Delete From PO_Standing_Requisition_Details  Where InLineNumber = @InLineNumber"
                        Dim com As SqlCommand
                        com = New SqlCommand(qry, connec)
                        com.Parameters.Add(New SqlParameter("@InLineNumber", SqlDbType.NVarChar)).Value = InLineNumber
                        com.Connection.Open()
                        com.ExecuteNonQuery()
                        com.Connection.Close()
                    End If
                Else
                    Dim dr As DataRow
                    '[InLineNumber]
                    dr = dt.NewRow()
                    dr("InLineNumber") = InLineNumber
                    dr("OrderNo") = txtoderno.Text
                    dr("Product") = txtProduct.Text
                    dr("QOH") = txtQOH.Text
                    dr("DUMP") = txtDUMP.Text
                    dr("Q_REQ") = txtQ_REQ.Text
                    dr("PRESOLD") = txtPRESOLD.Text
                    dr("COLOR_VARIETY") = txtCOLOR_VARIETY.Text
                    dr("REMARKS") = txtREMARKS.Text
                    dr("Q_ORD") = txtQ_ORD.Text
                    dr("PACK") = txtPACK.Text
                    dr("COST") = txtCOST.Text
                    dr("Ext_COSt") = txtExt_COSt.Text
                    dr("Vendor_Code") = txtVendor_Code.Text
                    dr("Buyer") = txtBuyer.Text
                    dr("Status") = txtStatus.Text
                    dr("Q_Recv") = txtQ_Recv.Text
                    dr("ISSUE") = txtISSUE.Text
                    dt.Rows.Add(dr)

                End If


            Catch ex As Exception

            End Try


        Next

        OrderHeaderGrid.PageSize = 500
        OrderHeaderGrid.DataSource = dt
        OrderHeaderGrid.DataBind()

        'SetOrderProductData()

    End Sub


    Private Sub btnsavechanges_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnsavechanges.Click, btnsavechangesUP.Click
        txtlastchanged.Text = Date.Now
        txtlastchangedby.Text = EmployeeID

        saveall()
        savechages()

        lblsavechanges.Text = "Data Saved Successfully"

    End Sub
    Public Function Checkonsubmit() As Boolean
        For Each row As GridViewRow In OrderHeaderGrid.Rows

            Try

                Dim InLineNumber As String = (OrderHeaderGrid.DataKeys(row.RowIndex).Value)
                Dim chk As CheckBox = row.FindControl("chkseleect")
                Dim txtProduct As TextBox = row.FindControl("txtProduct")

                Dim connec As New SqlConnection(constr)
                Dim qry As String

                If InLineNumber <> "0" And txtProduct.Text.Trim = "" Then

                    qry = "Delete From PO_Standing_Requisition_Details  Where InLineNumber = @InLineNumber"
                    Dim com As SqlCommand
                    com = New SqlCommand(qry, connec)
                    com.Parameters.Add(New SqlParameter("@InLineNumber", SqlDbType.BigInt)).Value = InLineNumber
                    com.Connection.Open()
                    com.ExecuteNonQuery()
                    com.Connection.Close()

                End If
            Catch ex As Exception

            End Try



        Next


        Return True
    End Function


    Public Function SelectInsertPO_AjaxBinding(ByVal RowNumber As String) As DataTable
        'Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
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


    Public Function PO_Standing_Requisition_Details_Status(ByVal RowNumber As Integer) As String
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim LocationName As String = ""
        Dim dt As New DataTable

        ssql = ssql & " SELECT  Status  "
        ssql = ssql & " FROM PO_Standing_Requisition_Details Where CompanyID ='" & Me.CompanyID & "' AND DivisionID ='" & Me.DivisionID & "'  AND DepartmentID ='" & Me.DepartmentID & "' "
        ssql = ssql & " AND InLineNumber =" & RowNumber
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        da.SelectCommand = com
        da.Fill(dt)
        Dim Status As String = ""

        If dt.Rows.Count > 0 Then
            Try
                Status = dt.Rows(0)("Status")
            Catch ex As Exception

            End Try
        End If

        Return Status
    End Function


    Public Function savechages() As Boolean

        Dim rs As SqlDataReader
        Dim OrderNo As String = ""


        Dim PopOrderNo As New CustomOrder()
        If IsNumeric(txtoderno.Text.Trim) = False Then
            rs = PopOrderNo.GetNextOrdNumber(CompanyID, DepartmentID, DivisionID, "NextSTRequisitionNumber")
            While rs.Read()
                OrderNo = rs("NextNumberValue")
            End While
            rs.Close()
            txtoderno.Text = OrderNo
        Else
            OrderNo = txtoderno.Text
        End If

        Dim total As Decimal = 0

        For Each row As GridViewRow In OrderHeaderGrid.Rows
            Try
                Dim InLineNumber As String = (OrderHeaderGrid.DataKeys(row.RowIndex).Value)
                Dim chk As CheckBox = row.FindControl("chkseleect")
                Dim txtProduct As TextBox = row.FindControl("txtProduct")
                Dim txtQOH As TextBox = row.FindControl("txtQOH")
                Dim txtDUMP As TextBox = row.FindControl("txtDUMP")
                Dim txtQ_REQ As TextBox = row.FindControl("txtQ_REQ")
                Dim txtPRESOLD As TextBox = row.FindControl("txtPRESOLD")
                Dim txtCOLOR_VARIETY As TextBox = row.FindControl("txtCOLOR_VARIETY")
                Dim txtREMARKS As TextBox = row.FindControl("txtREMARKS")
                Dim txtQ_ORD As TextBox = row.FindControl("txtQ_ORD")
                Dim txtPACK As TextBox = row.FindControl("txtPACK")
                Dim txtCOST As TextBox = row.FindControl("txtCOST")
                Dim txtExt_COSt As TextBox = row.FindControl("txtExt_COSt")
                Dim txtVendor_Code As TextBox = row.FindControl("txtVendor_Code")
                Dim txtBuyer As Label = row.FindControl("txtBuyer")

                Dim drpPOStatus As New DropDownList
                drpPOStatus = row.FindControl("drpPOStatus")

                Dim txtStatus As Label = row.FindControl("txtStatus")
                txtStatus.Text = drpPOStatus.SelectedValue

                Dim drpBuyer As New DropDownList
                drpBuyer = row.FindControl("drpBuyer")
                Try
                    txtBuyer.Text = drpBuyer.SelectedValue
                Catch ex As Exception

                End Try



                Dim txtQ_Recv As TextBox = row.FindControl("txtQ_Recv")
                Dim txtISSUE As TextBox = row.FindControl("txtISSUE")


                If True Then
                    Dim connec As New SqlConnection(constr)
                    Dim qry As String

                    If InLineNumber <> "0" Then

                        If IsNumeric(InLineNumber) = False Then
                            Dim dt2 As New DataTable
                            dt2 = SelectInsertPO_AjaxBinding(InLineNumber)
                            If dt2.Rows.Count = 0 Then

                            Else
                                InLineNumber = dt2.Rows(0)("InLineNumber")
                            End If
                        End If

                        If IsNumeric(InLineNumber) = False Then
                            Continue For
                        End If

                        If PO_Standing_Requisition_Details_Status(InLineNumber) <> "No Action" Then
                            Continue For
                        End If

                        Try
                            total = total + txtExt_COSt.Text
                        Catch ex As Exception

                        End Try


                        qry = "Update PO_Standing_Requisition_Details SET  [ShipDate]=@ShipDate ,[Location]=@Location ,[Type]=@Type ,[ProductName]=@ProductName ,[HDStatus]=@HDStatus ,[Product]=@Product ,[QOH]=@QOH ,[DUMP]=@DUMP ,[Q_REQ]=@Q_REQ ,[PRESOLD]=@PRESOLD ,[COLOR_VARIETY]=@COLOR_VARIETY ,[REMARKS]=@REMARKS ,[Q_ORD]=@Q_ORD ,[PACK]=@PACK ,[COST]=@COST ,[Ext_COSt]=@Ext_COSt ,[Vendor_Code]=@Vendor_Code,[Q_Recv]=@Q_Recv ,[ISSUE]=@ISSUE " _
                        & "  Where InLineNumber = @InLineNumber "

                        Logchangehistory("", EmployeeID, "PO_Standing_Requisition_Details", "Product", txtProduct.Text, txtoderno.Text, InLineNumber, "Text")
                        Logchangehistory("", EmployeeID, "PO_Standing_Requisition_Details", "QOH", txtQOH.Text, txtoderno.Text, InLineNumber, "Money")
                        Logchangehistory("", EmployeeID, "PO_Standing_Requisition_Details", "DUMP", txtDUMP.Text, txtoderno.Text, InLineNumber, "Money")
                        Logchangehistory("", EmployeeID, "PO_Standing_Requisition_Details", "Q_REQ", txtQ_REQ.Text, txtoderno.Text, InLineNumber, "Money")
                        Logchangehistory("", EmployeeID, "PO_Standing_Requisition_Details", "PRESOLD", txtPRESOLD.Text, txtoderno.Text, InLineNumber, "Money")
                        Logchangehistory("", EmployeeID, "PO_Standing_Requisition_Details", "COLOR_VARIETY", txtCOLOR_VARIETY.Text, txtoderno.Text, InLineNumber, "Text")
                        Logchangehistory("", EmployeeID, "PO_Standing_Requisition_Details", "REMARKS", txtREMARKS.Text, txtoderno.Text, InLineNumber, "Text")
                        Logchangehistory("", EmployeeID, "PO_Standing_Requisition_Details", "Q_ORD", txtQ_ORD.Text, txtoderno.Text, InLineNumber, "Money")
                        Logchangehistory("", EmployeeID, "PO_Standing_Requisition_Details", "PACK", txtPACK.Text, txtoderno.Text, InLineNumber, "Money")
                        Logchangehistory("", EmployeeID, "PO_Standing_Requisition_Details", "COST", txtCOST.Text, txtoderno.Text, InLineNumber, "Money")
                        Logchangehistory("", EmployeeID, "PO_Standing_Requisition_Details", "Ext_COSt", txtExt_COSt.Text, txtoderno.Text, InLineNumber, "Money")
                        Logchangehistory("", EmployeeID, "PO_Standing_Requisition_Details", "Vendor_Code", txtVendor_Code.Text, txtoderno.Text, InLineNumber, "Text")
                        ''Logchangehistory("", EmployeeID, "PO_Standing_Requisition_Details", "Buyer", txtREMARKS.Text, txtBuyer.Text, InLineNumber, "Text")
                        ''Logchangehistory("", EmployeeID, "PO_Standing_Requisition_Details", "Status", txtREMARKS.Text, txtStatus.Text, InLineNumber, "Text")
                        Logchangehistory("", EmployeeID, "PO_Standing_Requisition_Details", "Q_Recv", txtQ_Recv.Text, txtoderno.Text, InLineNumber, "Money")
                        Logchangehistory("", EmployeeID, "PO_Standing_Requisition_Details", "ISSUE", txtISSUE.Text, txtoderno.Text, InLineNumber, "Text")

                        Dim com As SqlCommand
                        com = New SqlCommand(qry, connec)

                        com.Parameters.Add(New SqlParameter("@InLineNumber", SqlDbType.BigInt)).Value = InLineNumber
                        com.Parameters.Add(New SqlParameter("@Product", SqlDbType.NVarChar)).Value = txtProduct.Text
                        com.Parameters.Add(New SqlParameter("@QOH", SqlDbType.NVarChar)).Value = txtQOH.Text
                        com.Parameters.Add(New SqlParameter("@DUMP", SqlDbType.NVarChar)).Value = txtDUMP.Text
                        com.Parameters.Add(New SqlParameter("@Q_REQ", SqlDbType.NVarChar)).Value = txtQ_REQ.Text
                        com.Parameters.Add(New SqlParameter("@PRESOLD", SqlDbType.NVarChar)).Value = txtPRESOLD.Text
                        com.Parameters.Add(New SqlParameter("@COLOR_VARIETY", SqlDbType.NVarChar)).Value = txtCOLOR_VARIETY.Text
                        com.Parameters.Add(New SqlParameter("@REMARKS", SqlDbType.NVarChar)).Value = txtREMARKS.Text
                        com.Parameters.Add(New SqlParameter("@Q_ORD", SqlDbType.NVarChar)).Value = txtQ_ORD.Text
                        com.Parameters.Add(New SqlParameter("@PACK", SqlDbType.NVarChar)).Value = txtPACK.Text
                        com.Parameters.Add(New SqlParameter("@COST", SqlDbType.NVarChar)).Value = txtCOST.Text
                        com.Parameters.Add(New SqlParameter("@Ext_COSt", SqlDbType.NVarChar)).Value = txtExt_COSt.Text
                        com.Parameters.Add(New SqlParameter("@Vendor_Code", SqlDbType.NVarChar)).Value = txtVendor_Code.Text
                        ''com.Parameters.Add(New SqlParameter("@Buyer", SqlDbType.NVarChar)).Value = txtBuyer.Text
                        ''com.Parameters.Add(New SqlParameter("@Status", SqlDbType.NVarChar)).Value = txtStatus.Text
                        com.Parameters.Add(New SqlParameter("@Q_Recv", SqlDbType.NVarChar)).Value = txtQ_Recv.Text
                        com.Parameters.Add(New SqlParameter("@ISSUE", SqlDbType.NVarChar)).Value = txtISSUE.Text

                        com.Parameters.Add(New SqlParameter("@ShipDate", SqlDbType.NVarChar)).Value = txtshipdate.Text
                        com.Parameters.Add(New SqlParameter("@Type", SqlDbType.NVarChar)).Value = drpType.SelectedValue
                        com.Parameters.Add(New SqlParameter("@ProductName", SqlDbType.NVarChar, 50)).Value = getitemname(txtProduct.Text)
                        com.Parameters.Add(New SqlParameter("@HDStatus", SqlDbType.NVarChar)).Value = drpStatus.SelectedValue
                        com.Parameters.Add(New SqlParameter("@Location", SqlDbType.NVarChar)).Value = cmblocationid.SelectedValue

                        com.Connection.Open()
                        com.ExecuteNonQuery()
                        com.Connection.Close()

                        If txtVendor_Code.Text.Trim = "" Then
                            AllVendor = False
                        End If

                    End If

                End If
            Catch ex As Exception

            End Try


        Next

        txttotal.Text = total

        Return True
    End Function

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnclose.Click, btncloseUP.Click
        saveall()
        savechages()
        Dim MVC As String = ""
        Try
            MVC = Request.QueryString("MVC")
        Catch ex As Exception

        End Try

        If MVC = "1" Then
            Response.Redirect("http://bpom.quickflora.com/Web/BPO.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&TerminalID=" & "DEFAULT" & "&locationid=" & Session("Locationid") & "&EmployeeID=" & EmployeeID & "&Session_SessionID=" & Session.SessionID)

        Else
            Response.Redirect("StandingRequisitionOrderList.aspx")
        End If
    End Sub


    Public Function Logchangehistory(ByVal CustomerID As String, ByVal EmployeeID As String, ByVal tableName As String, ByVal fieldName As String, ByVal fieldChangeValue As String, ByVal OrderNumber As String, ByVal OrderLine As String, ByVal type As String) As Boolean
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

    Private Sub btnLoadProductList_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLoadProductList.Click
        LoadProductList("")
    End Sub



    Private Sub LoadProductList(ByVal ItemCategoryID As String, Optional ByVal ItemFamilyID As String = "")
        Session("LoadProductList") = "True"
        savechages()
        saveall()

        Dim dt As New DataTable()
        dt.Columns.Add(New DataColumn("InLineNumber"))
        dt.Columns.Add(New DataColumn("OrderNo"))
        dt.Columns.Add(New DataColumn("Product"))
        dt.Columns.Add(New DataColumn("QOH"))
        dt.Columns.Add(New DataColumn("DUMP"))
        dt.Columns.Add(New DataColumn("Q_REQ"))
        dt.Columns.Add(New DataColumn("PRESOLD"))
        dt.Columns.Add(New DataColumn("COLOR_VARIETY"))
        dt.Columns.Add(New DataColumn("REMARKS"))
        dt.Columns.Add(New DataColumn("Q_ORD"))
        dt.Columns.Add(New DataColumn("PACK"))
        dt.Columns.Add(New DataColumn("COST"))
        dt.Columns.Add(New DataColumn("Ext_COSt"))
        dt.Columns.Add(New DataColumn("Vendor_Code"))
        dt.Columns.Add(New DataColumn("Buyer"))
        dt.Columns.Add(New DataColumn("Status"))
        dt.Columns.Add(New DataColumn("Q_Recv"))
        dt.Columns.Add(New DataColumn("ISSUE"))


        For Each row As GridViewRow In OrderHeaderGrid.Rows

            Dim InLineNumber As String = (OrderHeaderGrid.DataKeys(row.RowIndex).Value)
            Dim chk As CheckBox = row.FindControl("chkseleect")
            Dim txtProduct As TextBox = row.FindControl("txtProduct")
            Dim txtQOH As TextBox = row.FindControl("txtQOH")
            Dim txtDUMP As TextBox = row.FindControl("txtDUMP")
            Dim txtQ_REQ As TextBox = row.FindControl("txtQ_REQ")
            Dim txtPRESOLD As TextBox = row.FindControl("txtPRESOLD")
            Dim txtCOLOR_VARIETY As TextBox = row.FindControl("txtCOLOR_VARIETY")
            Dim txtREMARKS As TextBox = row.FindControl("txtREMARKS")
            Dim txtQ_ORD As TextBox = row.FindControl("txtQ_ORD")
            Dim txtPACK As TextBox = row.FindControl("txtPACK")
            Dim txtCOST As TextBox = row.FindControl("txtCOST")
            Dim txtExt_COSt As TextBox = row.FindControl("txtExt_COSt")
            Dim txtVendor_Code As TextBox = row.FindControl("txtVendor_Code")
            Dim txtBuyer As Label = row.FindControl("txtBuyer")

            Dim drpPOStatus As New DropDownList
            drpPOStatus = row.FindControl("drpPOStatus")

            Dim txtStatus As Label = row.FindControl("txtStatus")
            txtStatus.Text = drpPOStatus.SelectedValue

            Dim drpBuyer As New DropDownList
            drpBuyer = row.FindControl("drpBuyer")
            Try
                txtBuyer.Text = drpBuyer.SelectedValue
            Catch ex As Exception

            End Try
            Dim txtQ_Recv As TextBox = row.FindControl("txtQ_Recv")
            Dim txtISSUE As TextBox = row.FindControl("txtISSUE")

            Dim dr As DataRow
            '[InLineNumber]
            dr = dt.NewRow()
            dr("InLineNumber") = InLineNumber
            dr("OrderNo") = txtoderno.Text
            dr("Product") = txtProduct.Text
            dr("QOH") = txtQOH.Text
            dr("DUMP") = txtDUMP.Text
            dr("Q_REQ") = txtQ_REQ.Text
            dr("PRESOLD") = txtPRESOLD.Text
            dr("COLOR_VARIETY") = txtCOLOR_VARIETY.Text
            dr("REMARKS") = txtREMARKS.Text
            dr("Q_ORD") = txtQ_ORD.Text
            dr("PACK") = txtPACK.Text
            dr("COST") = txtCOST.Text
            dr("Ext_COSt") = txtExt_COSt.Text
            dr("Vendor_Code") = txtVendor_Code.Text
            dr("Buyer") = txtBuyer.Text
            dr("Status") = txtStatus.Text
            dr("Q_Recv") = txtQ_Recv.Text
            dr("ISSUE") = txtISSUE.Text
            dt.Rows.Add(dr)
        Next

        Dim dtLoadItemsData As New DataTable
        If Me.CompanyID = "DierbergsMarkets,Inc63017" Or Me.CompanyID = "QuickfloraDemo" Then
            dtLoadItemsData = LoadItemsDataDierbergsMarkets(ItemCategoryID, ItemFamilyID)
        Else
            dtLoadItemsData = LoadItemsData(ItemCategoryID)
        End If
        If dtLoadItemsData.Rows.Count = 0 Then
            Dim dr As DataRow
            '[InLineNumber]
            dr = dt.NewRow()
            dr("InLineNumber") = 0
            dr("OrderNo") = 1
            dr("Product") = String.Empty
            dr("QOH") = String.Empty
            dr("DUMP") = String.Empty
            dr("Q_REQ") = String.Empty
            dr("PRESOLD") = String.Empty
            dr("COLOR_VARIETY") = String.Empty
            dr("REMARKS") = String.Empty
            dr("Q_ORD") = String.Empty
            dr("PACK") = String.Empty
            dr("COST") = String.Empty
            dr("Ext_COSt") = String.Empty
            dr("Vendor_Code") = String.Empty
            dr("Buyer") = String.Empty
            dr("Status") = "No Action"
            dr("Q_Recv") = String.Empty
            dr("ISSUE") = String.Empty
            dt.Rows.Add(dr)

        Else
            Dim n As Integer
            For n = 0 To dtLoadItemsData.Rows.Count - 1
                Dim dr As DataRow
                dr = dt.NewRow()
                '[InventoryItems].UnitPrice , [InventoryItems].UnitsPerBox
                dr("InLineNumber") = (n + 1) & "@" & dtLoadItemsData.Rows(n)(0) & "@" & txtoderno.Text
                dr("OrderNo") = txtoderno.Text
                dr("Product") = dtLoadItemsData.Rows(n)(0)
                dr("QOH") = String.Empty
                dr("DUMP") = String.Empty
                dr("Q_REQ") = String.Empty
                dr("PRESOLD") = String.Empty
                dr("COLOR_VARIETY") = String.Empty
                dr("REMARKS") = String.Empty
                dr("Q_ORD") = String.Empty

                Try
                    dr("PACK") = dtLoadItemsData.Rows(n)(2)
                Catch ex As Exception
                    dr("PACK") = 1
                End Try

                Try
                    dr("COST") = dtLoadItemsData.Rows(n)(1)
                Catch ex As Exception
                    dr("COST") = 0
                End Try


                'dr("COST") = String.Empty

                dr("Ext_COSt") = String.Empty
                dr("Vendor_Code") = String.Empty
                dr("Buyer") = String.Empty
                dr("Status") = "No Action"
                dr("Q_Recv") = String.Empty
                dr("ISSUE") = String.Empty
                dt.Rows.Add(dr)
            Next


        End If

        OrderHeaderGrid.PageSize = 500
        OrderHeaderGrid.DataSource = dt
        OrderHeaderGrid.DataBind()

        Exit Sub
        btnaddnew.Enabled = False

        btnLoadProductList.Enabled = False
        btnFlowers.Enabled = False
        btnGreens.Enabled = False
        btnHardgoods.Enabled = False
        btnPlants.Enabled = False

    End Sub


    Private Sub LoadProductListOld(ByVal ItemCategoryID As String)
        savechages()
        saveall()

        Dim dt As New DataTable()
        dt.Columns.Add(New DataColumn("InLineNumber"))
        dt.Columns.Add(New DataColumn("OrderNo"))
        dt.Columns.Add(New DataColumn("Product"))
        dt.Columns.Add(New DataColumn("QOH"))
        dt.Columns.Add(New DataColumn("DUMP"))
        dt.Columns.Add(New DataColumn("Q_REQ"))
        dt.Columns.Add(New DataColumn("PRESOLD"))
        dt.Columns.Add(New DataColumn("COLOR_VARIETY"))
        dt.Columns.Add(New DataColumn("REMARKS"))
        dt.Columns.Add(New DataColumn("Q_ORD"))
        dt.Columns.Add(New DataColumn("PACK"))
        dt.Columns.Add(New DataColumn("COST"))
        dt.Columns.Add(New DataColumn("Ext_COSt"))
        dt.Columns.Add(New DataColumn("Vendor_Code"))
        dt.Columns.Add(New DataColumn("Buyer"))
        dt.Columns.Add(New DataColumn("Status"))
        dt.Columns.Add(New DataColumn("Q_Recv"))
        dt.Columns.Add(New DataColumn("ISSUE"))

        Dim dtLoadItemsData As New DataTable
        dtLoadItemsData = LoadItemsData(ItemCategoryID)
        If dtLoadItemsData.Rows.Count = 0 Then
            Dim dr As DataRow
            '[InLineNumber]
            dr = dt.NewRow()
            dr("InLineNumber") = 0
            dr("OrderNo") = 1
            dr("Product") = String.Empty
            dr("QOH") = String.Empty
            dr("DUMP") = String.Empty
            dr("Q_REQ") = String.Empty
            dr("PRESOLD") = String.Empty
            dr("COLOR_VARIETY") = String.Empty
            dr("REMARKS") = String.Empty
            dr("Q_ORD") = String.Empty
            dr("PACK") = String.Empty
            dr("COST") = String.Empty
            dr("Ext_COSt") = String.Empty
            dr("Vendor_Code") = String.Empty
            dr("Buyer") = String.Empty
            dr("Status") = "No Action"
            dr("Q_Recv") = String.Empty
            dr("ISSUE") = String.Empty
            dt.Rows.Add(dr)

        Else
            Dim n As Integer
            For n = 0 To dtLoadItemsData.Rows.Count - 1
                Dim dr As DataRow
                dr = dt.NewRow()
                '[InventoryItems].UnitPrice , [InventoryItems].UnitsPerBox
                dr("InLineNumber") = (n + 1) & "@" & dtLoadItemsData.Rows(n)(0) & "@" & txtoderno.Text
                dr("OrderNo") = txtoderno.Text
                dr("Product") = dtLoadItemsData.Rows(n)(0)
                dr("QOH") = String.Empty
                dr("DUMP") = String.Empty
                dr("Q_REQ") = String.Empty
                dr("PRESOLD") = String.Empty
                dr("COLOR_VARIETY") = String.Empty
                dr("REMARKS") = String.Empty
                dr("Q_ORD") = String.Empty

                Try
                    dr("PACK") = dtLoadItemsData.Rows(n)(2)
                Catch ex As Exception
                    dr("PACK") = 1
                End Try

                Try
                    dr("COST") = dtLoadItemsData.Rows(n)(1)
                Catch ex As Exception
                    dr("COST") = 0
                End Try


                'dr("COST") = String.Empty

                dr("Ext_COSt") = String.Empty
                dr("Vendor_Code") = String.Empty
                dr("Buyer") = String.Empty
                dr("Status") = "No Action"
                dr("Q_Recv") = String.Empty
                dr("ISSUE") = String.Empty
                dt.Rows.Add(dr)
            Next


        End If

        OrderHeaderGrid.PageSize = 500
        OrderHeaderGrid.DataSource = dt
        OrderHeaderGrid.DataBind()

        btnaddnew.Enabled = False

        btnLoadProductList.Enabled = False
        btnFlowers.Enabled = False
        btnGreens.Enabled = False
        btnHardgoods.Enabled = False
        btnPlants.Enabled = False

    End Sub

    'Flowers
    'Greens
    'Hardgoods
    'Holland
    'Plants

    Private Sub btnFlowers_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFlowers.Click
        LoadProductList("Flowers")
    End Sub

    Private Sub btnGreens_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGreens.Click
        LoadProductList("Greens")
    End Sub

    Private Sub btnHardgoods_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnHardgoods.Click
        LoadProductList("Hardgoods")
    End Sub

    Private Sub btnPlants_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPlants.Click
        LoadProductList("Plants")
    End Sub

    Private Sub RequisitionOrder_LoadComplete(ByVal sender As Object, ByVal e As EventArgs) Handles Me.LoadComplete

    End Sub


    Public Function GetVendorShipMethods(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As DataTable

        Dim dt As New DataTable

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetVendorShipMethods]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(dt)

                Catch ex As Exception

                End Try

            End Using
        End Using

        Return dt

    End Function



    Public Sub SetShipMethoddropdown()
        'Dim connec As New SqlConnection(constr)
        'Dim ssql As String = ""
        'Dim dt As New DataTable()
        'ssql = " SElect DISTINCT TruckingSchedule.ShipMethodID,TruckingSchedule.ShipMethodDescription     FROM TruckingSchedule  Where TruckingSchedule.Location =@Location and TruckingSchedule.[ShippingToLocation] =@ShippingToLocation "
        'Dim da As New SqlDataAdapter
        'Dim com As SqlCommand
        'com = New SqlCommand(ssql, connec)
        'Try
        '    com.Parameters.Add(New SqlParameter("@Location", SqlDbType.NVarChar)).Value = drpInventoryOrigin.SelectedValue
        '    com.Parameters.Add(New SqlParameter("@ShippingToLocation", SqlDbType.NVarChar, 36)).Value = cmblocationid.SelectedValue
        '    da.SelectCommand = com
        '    da.Fill(dt)

        'Catch ex As Exception

        'End Try
        Dim dt As New DataTable()
        dt = GetVendorShipMethods(Me.CompanyID, Me.DivisionID, Me.DepartmentID)


        drpshipemthod.Items.Clear()
        If dt.Rows.Count <> 0 Then
            drpshipemthod.DataSource = dt
            drpshipemthod.DataTextField = "ShipMethodDescription"
            drpshipemthod.DataValueField = "ShipMethodID"
            drpshipemthod.DataBind()
        End If
        Dim lst As New ListItem
        lst.Value = ""
        lst.Text = "-- Select Ship Method --"
        drpshipemthod.Items.Insert(0, lst)

    End Sub


    Private Sub drpInventoryOrigin_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles drpInventoryOrigin.SelectedIndexChanged, cmblocationid.SelectedIndexChanged
        SetShipMethoddropdown()
        txtshipdate.Text = ""
        txtarrivedate.Text = ""
    End Sub

    Private Sub drpshipemthod_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles drpshipemthod.SelectedIndexChanged
        txtshipdate.Text = ""
        txtarrivedate.Text = ""
        Dim dt1 As New DataTable
        dt1 = GetTruckingScheduleDays(Me.CompanyID, Me.DivisionID, Me.DepartmentID, drpInventoryOrigin.SelectedValue, drpshipemthod.SelectedValue, cmblocationid.SelectedValue)

        Dim disableDays As String = ""
        If dt1.Rows.Count <> 0 Then
            Dim n As Integer = 0
            For n = 1 To 7
                Dim chk As Boolean = True
                Dim m As Integer
                For m = 0 To dt1.Rows.Count - 1
                    Dim chkn As Integer = 0
                    Try
                        chkn = dt1.Rows(m)(0)
                    Catch ex As Exception

                    End Try
                    If chkn = n Then
                        chk = False
                    End If
                Next
                If chk Then
                    If disableDays.Trim = "" Then
                        disableDays = n
                    Else
                        disableDays = disableDays & "," & n.ToString
                    End If
                End If
            Next
        End If

        Dim dt2 As New DataTable
        dt2 = GetTruckingScheduleMinShipDate(Me.CompanyID, Me.DivisionID, Me.DepartmentID, drpInventoryOrigin.SelectedValue, drpshipemthod.SelectedValue, cmblocationid.SelectedValue)
        Dim dtnow As New DateTime
        dtnow = Date.Now.Date

        If dt2.Rows.Count <> 0 Then

            Try
                dtnow = dt2.Rows(0)(0)
            Catch ex As Exception

            End Try

        End If

        Dim strJS1 As String = ""
        Dim strJS2 As String = ""
        Dim myCalendar As String = ""

        myCalendar = "myCalendarShipDate"
        strJS1 = strJS1 & " var " & myCalendar & ";  " & vbCrLf

        strJS2 = strJS2 & "" & myCalendar & " = new dhtmlXCalendarObject([""" & txtshipdate.ClientID & """]);" & vbCrLf
        strJS2 = strJS2 & "" & myCalendar & ".setDateFormat('%m/%d/%Y'); " & vbCrLf
        'myCalendar3.setSensitiveRange("3/20/2017", null);

        strJS2 = strJS2 & "" & myCalendar & ".setSensitiveRange('" & dtnow.Date.ToShortDateString & "',null); " & vbCrLf
        strJS2 = strJS2 & "" & myCalendar & ".disableDays('week',[" & disableDays & "]); " & vbCrLf
        strJS2 = strJS2 & "" & myCalendar & ".attachEvent('onClick', function(){document.getElementById('ctl00_ContentPlaceHolder_btnarrivedate').focus();document.getElementById('ctl00_ContentPlaceHolder_btnarrivedate').click();});"
        'ctl00_ContentPlaceHolder_btnarrivedate



        Dim onloadScript As String = ""
        onloadScript = onloadScript & "<script type='text/javascript'>" & vbCrLf
        onloadScript = onloadScript & "  " & "doOnLoadShipDate();" & " " & vbCrLf
        onloadScript = onloadScript & "  " & strJS1 & " " & vbCrLf
        onloadScript = onloadScript & "  " & "function doOnLoadShipDate() {" & " " & vbCrLf
        onloadScript = onloadScript & "  " & strJS2 & " " & vbCrLf
        onloadScript = onloadScript & "  " & "}" & " " & vbCrLf
        onloadScript = onloadScript & "<" & "/" & "script>"
        ' Register script with page 
        Me.ClientScript.RegisterStartupScript(Me.GetType(), "onLoadCallShipDate", onloadScript.ToString())


    End Sub

    Public Function GetTruckingScheduleDays(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal ShipFromLocation As String, ByVal ShipMethodID As String, ByVal ShipToLocation As String) As DataTable

        Dim ds As New DataTable

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetTruckingScheduleDays]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("@ShipFromLocation", ShipFromLocation)
                Command.Parameters.AddWithValue("@ShipMethodID", ShipMethodID)
                Command.Parameters.AddWithValue("@ShipToLocation", ShipToLocation)


                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)
                Catch ex As Exception

                End Try

            End Using
        End Using

        Return ds

    End Function


    Public Function GetTruckingScheduleMinShipDate(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal ShipFromLocation As String, ByVal ShipMethodID As String, ByVal ShipToLocation As String) As DataTable

        Dim ds As New DataTable

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetTruckingScheduleMinShipDate]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("@ShipFromLocation", ShipFromLocation)
                Command.Parameters.AddWithValue("@ShipMethodID", ShipMethodID)
                Command.Parameters.AddWithValue("@ShipToLocation", ShipToLocation)


                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)
                Catch ex As Exception

                End Try

            End Using
        End Using

        Return ds

    End Function


    Public Function GetArrivalDayForTruckingDay(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal ShipFromLocation As String, ByVal ShipMethodID As String, ByVal ShipToLocation As String, ByVal TruckingDate As String) As DataTable

        Dim ds As New DataTable

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetArrivalDayForTruckingDay]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("@ShipFromLocation", ShipFromLocation)
                Command.Parameters.AddWithValue("@ShipMethodID", ShipMethodID)
                Command.Parameters.AddWithValue("@ShipToLocation", ShipToLocation)
                Command.Parameters.AddWithValue("@TruckingDate", TruckingDate)

                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)
                Catch ex As Exception

                End Try

            End Using
        End Using

        Return ds

    End Function

    Private Sub txtshipdate_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtshipdate.TextChanged, btnarrivedate.Click
        Dim dt2 As New DataTable
        dt2 = GetArrivalDayForTruckingDay(Me.CompanyID, Me.DivisionID, Me.DepartmentID, drpInventoryOrigin.SelectedValue, drpshipemthod.SelectedValue, cmblocationid.SelectedValue, txtshipdate.Text)
        Dim dtnow As New DateTime
        Try
            dtnow = txtshipdate.Text
        Catch ex As Exception

        End Try


        If dt2.Rows.Count <> 0 Then

            Try
                dtnow = dt2.Rows(0)(0)
            Catch ex As Exception

            End Try

        End If

        txtarrivedate.Text = dtnow.Date.ToShortDateString
        saveall()

    End Sub

End Class

