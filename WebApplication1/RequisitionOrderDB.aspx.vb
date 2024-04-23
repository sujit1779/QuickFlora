Imports System.Data
Imports System.Data.SqlClient
Imports DAL

Partial Class RequisitionOrderDB
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

    Public classgrid As String = ""

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


        If CompanyID = "SouthFloral" Then
            ' Response.Redirect("home.aspx")
        End If

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

        If Me.CompanyID = "SouthFloralsTraining" Or CompanyID = "SouthFloral" Or Me.CompanyID = "_DierbergsMarkets,Inc63017" Then
            Response.Redirect("CreateRequisition.aspx")
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

        If Me.CompanyID = "HBFARM" Then
            btnLoadProductList.Visible = False
        End If

        If Me.CompanyID = "DierbergsMarkets,Inc63017" And False Then

            Dim strJS1 As String = ""
            Dim strJS2 As String = ""
            Dim myCalendar As String = ""

            myCalendar = "myCalendarShipDate"
            strJS1 = strJS1 & " var " & myCalendar & ";  " & vbCrLf

            strJS2 = strJS2 & "" & myCalendar & " = new dhtmlXCalendarObject([""" & txtshipdate.ClientID & """]);" & vbCrLf
            strJS2 = strJS2 & "" & myCalendar & ".setDateFormat('%m/%d/%Y'); " & vbCrLf
            'myCalendar3.setSensitiveRange("3/20/2017", null);

            'strJS2 = strJS2 & "" & myCalendar & ".setSensitiveRange('" & dtnow.Date.ToShortDateString & "',null); " & vbCrLf
            'strJS2 = strJS2 & "" & myCalendar & ".disableDays('week',[" & disableDays & "]); " & vbCrLf
            ' strJS2 = strJS2 & "" & myCalendar & ".attachEvent('onClick', function(){SetStatusColorforblankdate();});"
            'ctl00_ContentPlaceHolder_btnarrivedate



            Dim onloadScript As String = ""
            onloadScript = onloadScript & "<link rel='stylesheet' type='text/css' href='https://secure.quickflora.com/Admin/EnterpriseASPAR/CustomOrder/codebase/dhtmlxcalendar.css'></link>"
            onloadScript = onloadScript & "<link rel='stylesheet' type='text/css' href='https://secure.quickflora.com/Admin/EnterpriseASPAR/CustomOrder/codebase/skins/dhtmlxcalendar_dhx_skyblue.css'></link>"
            onloadScript = onloadScript & "<script type='text/javascript'>" & vbCrLf
            onloadScript = onloadScript & "  " & "setTimeout(doOnLoadShipDate(), 5000);" & " " & vbCrLf
            onloadScript = onloadScript & "  " & strJS1 & " " & vbCrLf
            onloadScript = onloadScript & "  " & "function doOnLoadShipDate() {alert('Ship');" & " " & vbCrLf
            onloadScript = onloadScript & "  " & strJS2 & " " & vbCrLf
            onloadScript = onloadScript & "  " & "}" & " " & vbCrLf
            onloadScript = onloadScript & "<" & "/" & "script>"
            ' Register script with page 
            Me.ClientScript.RegisterStartupScript(Me.GetType(), "onLoadCallShipDate", onloadScript.ToString())
        End If
        If Me.CompanyID = "DierbergsMarkets,Inc63017" And False Then
            Dim strJS1 As String = ""
            Dim strJS2 As String = ""
            Dim myCalendar As String = ""

            myCalendar = "myCalendararrivedate"
            strJS1 = strJS1 & " var " & myCalendar & ";  " & vbCrLf

            strJS2 = strJS2 & "" & myCalendar & " = new dhtmlXCalendarObject([""" & txtarrivedate.ClientID & """]);" & vbCrLf
            strJS2 = strJS2 & "" & myCalendar & ".setDateFormat('%m/%d/%Y'); " & vbCrLf
            'myCalendar3.setSensitiveRange("3/20/2017", null);

            'strJS2 = strJS2 & "" & myCalendar & ".setSensitiveRange('" & dtnow.Date.ToShortDateString & "',null); " & vbCrLf
            ' strJS2 = strJS2 & "" & myCalendar & ".disableDays('week',[" & disableDays & "]); " & vbCrLf
            ' strJS2 = strJS2 & "" & myCalendar & ".attachEvent('onClick', function(){document.getElementById('ctl00_ContentPlaceHolder_btnarrivedate').focus();document.getElementById('ctl00_ContentPlaceHolder_btnarrivedate').click();});"
            'ctl00_ContentPlaceHolder_btnarrivedate



            Dim onloadScript As String = ""
            onloadScript = onloadScript & "<script type='text/javascript'>" & vbCrLf
            onloadScript = onloadScript & "  " & "setTimeout(doOnLoadarrivedate(), 5000);" & " " & vbCrLf
            onloadScript = onloadScript & "  " & strJS1 & " " & vbCrLf
            onloadScript = onloadScript & "  " & "function doOnLoadarrivedate() {alert('Arrival');" & " " & vbCrLf
            onloadScript = onloadScript & "  " & strJS2 & " " & vbCrLf
            onloadScript = onloadScript & "  " & "}" & " " & vbCrLf
            onloadScript = onloadScript & "<" & "/" & "script>"
            ' Register script with page 
            Me.ClientScript.RegisterStartupScript(Me.GetType(), "doOnLoadarrivedate", onloadScript.ToString())
        End If


        If CompanyID = "DierbergsMarkets,Inc63017" Or Me.CompanyID = "QuickfloraDemo" Then
            onloadcalnder = "1"
            classgrid = "class='_tableFixHead'"
        End If
        If Me.CompanyID = "QuickfloraDemo" Or Me.CompanyID = "DierbergsMarkets,Inc63017" Then


            cmblocationid.Attributes.Add("onchange", "SetStatusColorforblank(this);")
            If cmblocationid.SelectedValue = "" Then
                cmblocationid.Attributes.Add("style", "background-color:#FFA8A8 !important;")
            End If

            'style='background-color:#FFA8A8 !important;'
            drpshipemthod.Attributes.Add("onchange", "SetStatusColorforblank(this);")
            'drpshipemthod.BackColor = Drawing.Color.Yellow
            If drpshipemthod.SelectedValue = "" Then
                drpshipemthod.Attributes.Add("style", "background-color:#FFA8A8 !important;")
            End If


            drpInventoryOrigin.Attributes.Add("onchange", "SetStatusColorforblank(this);")
            'drpInventoryOrigin.BackColor = Drawing.Color.Yellow
            If drpInventoryOrigin.SelectedValue = "" Then
                drpInventoryOrigin.Attributes.Add("style", "background-color:#FFA8A8 !important;")
            End If


            drpType.Attributes.Add("onchange", "SetStatusColorforblank(this);")
            'drpType.BackColor = Drawing.Color.Yellow
            If drpType.SelectedValue = "" Then
                drpType.Attributes.Add("style", "background-color:#FFA8A8 !important;")
            End If


            txtarrivedate.Attributes.Add("onblur", "SetStatusColorforblank(this);")
            'txtarrivedate.BackColor = Drawing.Color.Yellow
            If txtarrivedate.Text = "" Then
                txtarrivedate.Attributes.Add("style", "background-color:#FFA8A8 !important;")
            End If


            txtshipdate.Attributes.Add("onblur", "SetStatusColorforblank(this);")
            'txtshipdate.BackColor = Drawing.Color.Yellow
            If txtshipdate.Text = "" Then
                txtshipdate.Attributes.Add("style", "background-color:#FFA8A8 !important;")
            End If

        End If
    End Sub

    Public onloadcalnder As String = "0"

    Public Function LoadItemsData(ByVal ItemCategoryID As String, Optional ByVal ItemFamilyID As String = "") As DataTable
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select  ItemID,[InventoryItems].UnitPrice , [InventoryItems].UnitsPerBox ,VendorID from [InventoryItems] where  ItemID <> 'DEFAULT' AND CompanyID=@CompanyID AND DivisionID=@DivisionID AND DepartmentID=@DepartmentID AND  InventoryItems.IsActive=1 and InventoryItems.WireServiceIdAllowed = 1 "
        If Me.CompanyID = "DierbergsMarkets,Inc63017" Or Me.CompanyID = "QuickfloraDemo" Then
            ssql = "select  ItemID,ISNULL([InventoryItems].wholesalePrice,0) AS 'UnitPrice' , [InventoryItems].UnitsPerBox ,VendorID from [InventoryItems] where  ItemID <> 'DEFAULT' AND CompanyID=@CompanyID AND DivisionID=@DivisionID AND DepartmentID=@DepartmentID AND  InventoryItems.IsActive=1 and InventoryItems.WireServiceIdAllowed = 1 "
        End If

        If ItemCategoryID.Trim <> "" Then
            ssql = ssql & " and ItemCategoryID='" & ItemCategoryID & "' "
        End If

        If Me.CompanyID = "DierbergsMarkets,Inc63017" Or Me.CompanyID = "QuickfloraDemo" Then

            Try
                If ItemFamilyID.Trim <> "" Then
                    ssql = ssql & " and ItemFamilyID='" & ItemFamilyID & "' "
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


    Public Function LoadItemsDataDierbergsMarkets(ByVal ItemCategoryID As String, Optional ByVal ItemFamilyID As String = "") As DataTable
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select  ItemID,[InventoryItems].UnitPrice , [InventoryItems].UnitsPerBox ,VendorID from [InventoryItems] where  ItemID <> 'DEFAULT' AND CompanyID=@CompanyID AND DivisionID=@DivisionID AND DepartmentID=@DepartmentID AND  InventoryItems.IsActive=1 and InventoryItems.WireServiceIdAllowed = 1 "
        If Me.CompanyID = "DierbergsMarkets,Inc63017" Or Me.CompanyID = "QuickfloraDemo" Then
            ssql = "select  ItemID,ISNULL([InventoryItems].wholesalePrice,0) AS 'UnitPrice' , [InventoryItems].UnitsPerBox ,VendorID from [InventoryItems] where  ItemID <> 'DEFAULT' AND CompanyID=@CompanyID AND DivisionID=@DivisionID AND DepartmentID=@DepartmentID   and InventoryItems.WireServiceIdAllowed = 1 "
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

            'ssql = ssql & " Order by (Select SUM(OrderDetail.OrderQty) from OrderDetail Where   CompanyID=@CompanyID AND DivisionID=@DivisionID AND DepartmentID=@DepartmentID  AND OrderDetail.ItemID = [InventoryItems].ItemID  ) Desc "
            ssql = ssql & " Order by InventoryItems.ItemName  "
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
        dt.Columns.Add(New DataColumn("FromReq"))
        dt.Columns.Add(New DataColumn("ToReq"))
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
        dr("FromReq") = String.Empty
        dr("ToReq") = String.Empty

        dt.Rows.Add(dr)




        '//dr = dt.NewRow();

        OrderHeaderGrid.DataSource = dt
        OrderHeaderGrid.DataBind()

    End Sub

    Public Sub SetOrderProductData()
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT * FROM [PO_Requisition_Details] where  OrderNo = @OrderNo and CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 order by Product ASC "
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
        ssql = "SELECT OrderNo FROM [PO_Requisition_Header] where  OrderNo = @OrderNo and CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 "
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
        ssql = "SELECT * FROM [PO_Requisition_Header] where  OrderNo = @OrderNo and CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 "

        Dim CommandText As String = " "
        CommandText = CommandText & "SELECT  [CompanyID] ,[DivisionID] ,[DepartmentID] ,[OrderNo] ,[Location] ,[Remarks] ,[Status] ,[Type] ,[LastChangeDateTime] ,[LastChangeBy] "
        CommandText = CommandText & " ,  "
        CommandText = CommandText & " ( Select SUM(round(convert(money,  CASE WHEN  Isnumeric(Q_ORD)  = 1  THEN  Q_ORD ELSE '0' END) * convert(money,  CASE WHEN  Isnumeric(COST)  = 1  THEN  COST ELSE '0' END) * convert(money,  CASE WHEN  Isnumeric(PACK)  = 1  THEN  PACK ELSE '0' END),2)   ) "
        CommandText = CommandText & "  From PO_Requisition_Details Where PO_Requisition_Details.CompanyID=@f0 and PO_Requisition_Details.DivisionID=@f1 and PO_Requisition_Details.DepartmentID=@f2   AND PO_Requisition_Details.OrderNo = [PO_Requisition_Header].OrderNo    ) AS 'TotalAmount' "
        CommandText = CommandText & " ,[ShipDate] ,[ArriveDate] ,[OrderPlacedDate] ,[ReceivedOnDate] ,[OrderBy] ,[ReceivedBy] ,[PONO] ,[Standing] ,[InventoryOrigin] ,[ShipMethodID] ,[canceled] ,[canceledDate] ,[canceledBy] ,[Received] ,[ReceivedDate] ,[ReceivedByNew] "
        CommandText = CommandText & " FROM [PO_Requisition_Header] where  OrderNo = @OrderNo and CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2  "
        CommandText = CommandText & " "
        ssql = CommandText

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
                SetShipMethoddropdown()
                Try
                    drpshipemthod.SelectedValue = dt.Rows(0)("ShipMethodID")
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

        If dt.Rows.Count = 1 Then
            drpInventoryOrigin.SelectedIndex = 1
            Dim o As New Object
            Dim e As New EventArgs
            drpInventoryOrigin_SelectedIndexChanged(o, e)
        End If

    End Sub

    Public Sub SetdrpType()
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT * FROM [PO_Requisition_ProductTypes] where IsActive = 1 AND  CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2  Order by ProductTypesID "
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

        '              ,[ProductTypesID]
        '      ,[IsActive]
        '  From [Enterprise].[dbo].[PO_Requisition_ProductTypes]
        'Where [CompanyID] = 'McCarthyg'


        If dt.Rows.Count <> 0 Then
            drpType.DataSource = dt
            drpType.DataTextField = "ProductTypesID"
            drpType.DataValueField = "ProductTypesID"
            drpType.DataBind()
        End If

    End Sub


    Public Sub SetLocationIDdropdown()
        SetOriginDdropdown()
        SetdrpType()
        If Me.CompanyID = "DierbergsMarkets,Inc63017" Or Me.CompanyID = "QuickfloraDemo" Then
            SetShipMethoddropdown()
        End If
        ''SetShipMethoddropdown()
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




        ''------------------''
        Dim locationid_chk As String = ""
        Dim locationid_true As Boolean = True

        Try
            Dim dt_new As New Data.DataTable
            dt_new = obj.FillLocationIsmaster()

            locationid_chk = Session("Locationid")

            Dim n As Integer
            For n = 0 To dt_new.Rows.Count - 1
                If locationid_chk = dt_new.Rows(n)("LocationID") Then
                    locationid_true = False
                    lblmasterlocation.Text = locationid_chk
                    Exit For
                End If
            Next


        Catch ex As Exception

        End Try

        If locationid_true Then
            lblmasterlocation.Text = ""
            cmblocationid.SelectedIndex = cmblocationid.Items.IndexOf(cmblocationid.Items.FindByValue(locationid))
            cmblocationid.Enabled = False
        End If
        lblmasterlocation.Visible = False
    End Sub
    'CREATE TABLE [dbo].[PO_Requisition_Header](
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

    Public Function DetailsOrder_Location(ByVal LocationID As String) As DataTable
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from Order_Location where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and LocationID=@f3"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = LocationID

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






    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As EventArgs)
        txtlastchanged.Text = Date.Now
        txtlastchangedby.Text = EmployeeID

        Dim chk As String = ""
        Try
            chk = Session("LoadProductList") ' = "True"
        Catch ex As Exception

        End Try

        If chk = "True" Then
            Checkonsubmit()
        End If


        AllVendor = True

        If AllVendor Then
            drpStatus.SelectedValue = "Entry Completed"
            txtorderplaced.Text = Date.Now
            txtorderby.Text = EmployeeID
            savechages()
            saveall()
            If Me.CompanyID = "DierbergsMarkets,Inc63017" Then
                If lblmasterlocation.Text <> "" And lblmasterlocation.Text <> cmblocationid.SelectedValue Then

                    Dim dt_lc As New DataTable
                    dt_lc = DetailsOrder_Location(cmblocationid.SelectedValue)
                    If dt_lc.Rows.Count > 0 Then
                        Dim emaillc As String = ""
                        Try
                            emaillc = dt_lc.Rows(0)("Email")
                        Catch ex As Exception

                        End Try

                        Dim locationame As String = ""
                        Try
                            locationame = dt_lc.Rows(0)("LocationName")
                        Catch ex As Exception

                        End Try
                        Dim QFmail As New com.quickflora.qfscheduler.QFPrintService
                        Dim emailconent As String = ""
                        ' emailconent = emailconent & "InLineNumber : " & InLineNumber & "<br>"
                        emailconent = emailconent & " Hello, " & "" & "<br><br>"
                        emailconent = emailconent & " Admin has created requisition # " & txtoderno.Text & "  for you. Please have a look and make the edits if required. " & "" & "<br><br>"
                        emailconent = emailconent & " Thanks, " & "" & "<br>"
                        emailconent = emailconent & " Dierbergs Floral Buying Team " & "" & "<br>"
                        lblmail.Text = "Attention: New Requisition # " & txtoderno.Text & " created for your location:" & locationame
                        Dim emm As String = ""
                        If Me.CompanyID = "DierbergsMarkets,Inc63017" Then
                            emm = "floralorders@dierbergs.com"
                        Else
                            emm = "info@quickflora.com"
                        End If
                        Try
                            QFmail.newmailsending(emm, emaillc, "", "", lblmail.Text, emailconent, CompanyID, DivisionID, DepartmentID)
                            'QFmail.newmailsending(emm, "gaurav@quickflora.com", "imy@quickflora.com", "", lblmail.Text, emailconent, CompanyID, DivisionID, DepartmentID)
                            'mailto:
                        Catch ex As Exception

                        End Try

                        lblmail.Text = lblmail.Text & "<br>" & emaillc
                    End If
                    'Exit Sub
                    lblmail.Visible = False
                Else

                End If
            End If


            Dim MVC As String = ""
            Try
                MVC = Request.QueryString("MVC")
            Catch ex As Exception

            End Try

            If MVC = "1" Then
                Response.Redirect("http://bpom2.quickflora.com/Web/BPO.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&TerminalID=" & "DEFAULT" & "&locationid=" & Session("Locationid") & "&EmployeeID=" & EmployeeID & "&Session_SessionID=" & Session.SessionID)

            Else
                Response.Redirect("RequisitionOrderList.aspx")
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
            rs = PopOrderNo.GetNextOrdNumber(CompanyID, DepartmentID, DivisionID, "NextRequisitionNumber")
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
            qry = "insert into PO_Requisition_Header(ShipMethodID,InventoryOrigin, CompanyID, DivisionID, DepartmentID, OrderNo,Location,Remarks,Status,Type,LastChangeDateTime,LastChangeBy,TotalAmount,ShipDate,ArriveDate,OrderPlacedDate,ReceivedOnDate,OrderBy,ReceivedBy) " _
             & " values(@ShipMethodID,@InventoryOrigin,@CompanyID, @DivisionID, @DepartmentID, @OrderNo,@Location,@Remarks,@Status,@Type,@LastChangeDateTime,@LastChangeBy,@TotalAmount,@ShipDate,@ArriveDate,@OrderPlacedDate,@ReceivedOnDate,@OrderBy,@ReceivedBy)"
        Else
            qry = "Update PO_Requisition_Header SET ShipMethodID =@ShipMethodID, InventoryOrigin=@InventoryOrigin, Location=@Location,Remarks=@Remarks,Status=@Status,Type=@Type,LastChangeDateTime=@LastChangeDateTime,LastChangeBy=@LastChangeBy,TotalAmount=@TotalAmount,ShipDate=@ShipDate,ArriveDate=@ArriveDate,OrderPlacedDate=@OrderPlacedDate,ReceivedOnDate=@ReceivedOnDate,OrderBy=@OrderBy,ReceivedBy=@ReceivedBy   " _
             & "  Where CompanyID=@CompanyID AND DivisionID=@DivisionID AND DepartmentID=@DepartmentID AND OrderNo=@OrderNo"

            Logchangehistory("", EmployeeID, "PO_Requisition_Header", "Location", cmblocationid.SelectedValue, txtoderno.Text, "", "Text")
            Logchangehistory("", EmployeeID, "PO_Requisition_Header", "Remarks", txtRemarks.Text, txtoderno.Text, "", "Text")
            Logchangehistory("", EmployeeID, "PO_Requisition_Header", "Status", drpStatus.SelectedValue, txtoderno.Text, "", "Text")
            Logchangehistory("", EmployeeID, "PO_Requisition_Header", "Type", drpType.SelectedValue, txtoderno.Text, "", "Text")
            Logchangehistory("", EmployeeID, "PO_Requisition_Header", "LastChangeDateTime", txtlastchanged.Text, txtoderno.Text, "", "Date")
            Logchangehistory("", EmployeeID, "PO_Requisition_Header", "LastChangeBy", txtlastchangedby.Text, txtoderno.Text, "", "Text")
            Logchangehistory("", EmployeeID, "PO_Requisition_Header", "TotalAmount", txttotal.Text, txtoderno.Text, "", "Money")
            Logchangehistory("", EmployeeID, "PO_Requisition_Header", "ShipDate", txtshipdate.Text, txtoderno.Text, "", "Date")
            Logchangehistory("", EmployeeID, "PO_Requisition_Header", "ArriveDate", txtarrivedate.Text, txtoderno.Text, "", "Date")
            Logchangehistory("", EmployeeID, "PO_Requisition_Header", "OrderPlacedDate", txtorderplaced.Text, txtoderno.Text, "", "Date")
            Logchangehistory("", EmployeeID, "PO_Requisition_Header", "ReceivedOnDate", txtrecvon.Text, txtoderno.Text, "", "Date")
            Logchangehistory("", EmployeeID, "PO_Requisition_Header", "OrderBy", txtorderby.Text, txtoderno.Text, "", "Text")
            Logchangehistory("", EmployeeID, "PO_Requisition_Header", "ReceivedBy", txtrecvby.Text, txtoderno.Text, "", "Text")
        End If

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)

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

        Dim ttl As Decimal = 0
        Try
            ttl = txttotal.Text
        Catch ex As Exception

        End Try

        lblsave.Text = "Requisition <strong>#" & txtoderno.Text & "</strong> saved Successfully. " & "</b>"
        dvsave.Visible = False

        Return True
    End Function

    '    CREATE TABLE [dbo].[PO_Requisition_Details](
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

        dt.Columns.Add(New DataColumn("FromReq"))
        dt.Columns.Add(New DataColumn("ToReq"))

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
            ' txtStatus.Text = drpPOStatus.SelectedValue

            Dim drpBuyer As New DropDownList
            drpBuyer = row.FindControl("drpBuyer")
            Try
                ' txtBuyer.Text = drpBuyer.SelectedValue
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
            qry = "insert into PO_Requisition_Details ([CompanyID] ,[DivisionID] ,[DepartmentID] ,[OrderNo] ,[Product] ,[QOH] ,[DUMP] ,[Q_REQ] ,[PRESOLD] ,[COLOR_VARIETY] ,[REMARKS] ,[Q_ORD] ,[PACK] ,[COST] ,[Ext_COSt] ,[Vendor_Code] ,[Buyer] ,[Status] ,[Q_Recv] ,[ISSUE],[PONO])" _
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

    Public Function getitemimg(ByVal id As String) As String
        Dim itemname As String = ""
        Dim sql As String = "select  ISNULL(ThumbnailImage ,'noimagenew.png') AS ThumbnailImage from [InventoryItems] where  ItemID=@id AND CompanyID=@CompanyID AND DivisionID=@DivisionID AND DepartmentID=@DepartmentID  "
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
                itemname = returl(dt.Rows(0)("ThumbnailImage").ToString())
            Catch ex As Exception
                itemname = returl("noimagenew.png")
            End Try

        End If

        itemname = "<img width='75' height='75' src='" & itemname & "'>"
        Return itemname
    End Function

    Public Function returl(ByVal ob As Object) As String
        '''NasCheck(ob)


        Dim DocumentDir As String = ""
        Dim img1 As String = ""
        Dim ImgName As String = ""
        Try
            ImgName = ob.ToString()
        Catch ex As Exception

        End Try
        DocumentDir = "D:\WebApps\QuickFloraFrontEnd\itemimages\" ' ConfigurationManager.AppSettings("InvPath")

        If (ImgName.Trim() = "") Then

            Return "https://secure.quickflora.com/itemimages/noimagenew.png"

        Else

            If System.IO.File.Exists(DocumentDir & ImgName.Trim()) Then
                Return "https://secure.quickflora.com/itemimages/" & ImgName.Trim()
                ''Return "../../images/products/" & ImgName.Trim()

            Else
                Return "https://secure.quickflora.com/itemimages/noimagenew.png"
            End If




        End If


    End Function


    Private Sub OrderHeaderGrid_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles OrderHeaderGrid.RowDataBound
        'Exit Sub
        If Me.CompanyID = "QuickfloraDemo" Or Me.CompanyID = "SouthFloralsTraining" Or Me.CompanyID = "SouthFloral" Then


            If e.Row.RowType = DataControlRowType.Header Then
                e.Row.Cells(3).Visible = False
                e.Row.Cells(4).Visible = False
                e.Row.Cells(6).Visible = False
            End If
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Cells(3).Visible = False
                e.Row.Cells(4).Visible = False
                e.Row.Cells(6).Visible = False
            End If

        End If


        Dim txtInLineNumber As New TextBox
        'Response.Write(e.Row.RowType)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(6).BackColor = Drawing.Color.SandyBrown
            txtInLineNumber = e.Row.FindControl("txtInLineNumber")
            If txtInLineNumber.Text = "0" Then
                e.Row.Visible = False
            End If

            e.Row.Attributes("id") = txtInLineNumber.Text

            Dim txtProductname As New Label
            txtProductname = e.Row.FindControl("txtProductname")
            Dim txtProductimg As New Label
            txtProductimg = e.Row.FindControl("txtProductimg")

            Dim txtProduct As TextBox = e.Row.FindControl("txtProduct")
            txtProductname.Text = getitemname(txtProduct.Text.Trim)
            ' txtProductname.Text = " <a class=""link"" href=""javascript:void();"">" + txtProductname.Text + " <img class=""preview"" src=""https://secure.quickflora.com/POM/img.aspx?CompanyID=" & Me.CompanyID & "&itemname=" + txtProductname.Text + " ""></a>"

            txtProductimg.Text = getitemimg(txtProduct.Text.Trim)

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

            Dim chkseleect As CheckBox = e.Row.FindControl("chkseleect")

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

            txtQ_REQ.ForeColor = Drawing.Color.Red

            If txtStatus.Text <> "No Action" Then
                chkseleect.Enabled = False
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
            If Me.CompanyID = "DierbergsMarkets,Inc63017" Or Me.CompanyID = "QuickfloraDemo" Then
                txtProduct.Attributes.Add("onblur", "ItemNamesearch('" & txtProduct.ClientID & "','" & txtInLineNumber.Text & "','" & txtPACK.ClientID & "','" & txtCOST.ClientID & "','" & txtVendor_Code.ClientID & "')")
            Else
                txtProduct.Attributes.Add("onblur", "Saveitem(this,'" & txtInLineNumber.Text & "','Product','" & txtProduct.Text & "')")
            End If




            txtProduct.Attributes.Add("placeholder", "SEARCH Item")
            txtProduct.Attributes.Add("onKeyUp", "SendQuery2(this.value,this,'" & txtInLineNumber.Text & "','" & txtPACK.ClientID & "','" & txtCOST.ClientID & "','" & txtVendor_Code.ClientID & "')")

            txtQOH.Attributes.Add("onfocus", "myFocusFunction(this)")
            txtQOH.Attributes.Add("onblur", "Saveitem(this,'" & txtInLineNumber.Text & "','QOH','" & txtQOH.Text & "')")

            txtDUMP.Attributes.Add("onfocus", "myFocusFunction(this)")
            txtDUMP.Attributes.Add("onblur", "Saveitem(this,'" & txtInLineNumber.Text & "','DUMP','" & txtDUMP.Text & "')")

            txtQ_REQ.Attributes.Add("onfocus", "myFocusFunction(this)")
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


            'txtVendor_Code.Attributes.Add("onfocus", "myFocusFunction(this)")
            'txtVendor_Code.Attributes.Add("onblur", "Saveitem(this,'" & txtInLineNumber.Text & "','Vendor_Code','" & txtVendor_Code.Text & "')")


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
        dt.Columns.Add(New DataColumn("FromReq"))
        dt.Columns.Add(New DataColumn("ToReq"))


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
                'txtStatus.Text = drpPOStatus.SelectedValue

                Dim drpBuyer As New DropDownList
                drpBuyer = row.FindControl("drpBuyer")
                Try
                    ' txtBuyer.Text = drpBuyer.SelectedValue
                Catch ex As Exception

                End Try
                Dim txtQ_Recv As TextBox = row.FindControl("txtQ_Recv")
                Dim txtISSUE As TextBox = row.FindControl("txtISSUE")

                'Response.Write(InLineNumber)
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
                        Dim comnew As SqlCommand
                        Try
                            qry = "DeletePO_RequisitionItemDetailsJsGrid"

                            comnew = New SqlCommand(qry, connec)
                            comnew.CommandType = CommandType.StoredProcedure
                            comnew.Parameters.Add(New SqlParameter("@InLineNumber", SqlDbType.NVarChar)).Value = InLineNumber
                            comnew.Parameters.Add(New SqlParameter("@EmployeeID", SqlDbType.NVarChar)).Value = Me.EmployeeID
                            comnew.Connection.Open()
                            comnew.ExecuteNonQuery()
                        Catch ex As Exception
                            lblsavechanges.Text = lblsavechanges.Text & " ex:" & ex.Message
                            lblsavechanges.Visible = True
                        Finally
                            comnew.Connection.Close()
                        End Try


                        qry = "Delete From PO_Requisition_Details  Where InLineNumber = @InLineNumber"
                        'lblsavechanges.Text = lblsavechanges.Text & "  QRY:" & qry
                        'lblsavechanges.Text = lblsavechanges.Text & "  InLineNumber:" & InLineNumber
                        'lblsavechanges.Visible = True

                        Dim com As SqlCommand
                        com = New SqlCommand(qry, connec)
                        com.Parameters.Add(New SqlParameter("@InLineNumber", SqlDbType.NVarChar)).Value = InLineNumber
                        Try
                            com.Connection.Open()
                            com.ExecuteNonQuery()
                        Catch ex As Exception
                            lblsavechanges.Text = lblsavechanges.Text & " ex:" & ex.Message
                            lblsavechanges.Visible = True
                        Finally
                            com.Connection.Close()
                        End Try



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


    Private Sub btnsavechanges_Click(ByVal sender As Object, ByVal e As EventArgs)
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
                Dim txtQ_REQ As TextBox = row.FindControl("txtQ_REQ")
                Dim req As Integer = 0
                Try
                    req = txtQ_REQ.Text
                Catch ex As Exception

                End Try

                If req > 0 Then
                    Continue For
                    'Dim dtchg As New DataTable
                    'dtchg = SelectChangeHistory(InLineNumber)
                    'If dtchg.Rows.Count > 0 Then
                    '    If InLineNumber <> "0" And txtProduct.Text.Trim = "" Then
                    '        Dim QFmail As New com.quickflora.qfscheduler.QFPrintService
                    '        Dim emailconent As String = ""
                    '        emailconent = emailconent & "InLineNumber : " & InLineNumber & "<br>"
                    '        emailconent = emailconent & "Employee : " & EmployeeID & "<br>"
                    '        QFmail.newmailsending("info@quickflora.com", "gaurav@quickflora.com", "imy@quickflora.com", "", "RO Delete Check on submit From:" & CompanyID, emailconent, CompanyID, DivisionID, DepartmentID)
                    '    End If
                    '    Return False
                    'End If

                End If


                Dim connec As New SqlConnection(constr)
                Dim qry As String

                If InLineNumber <> "0" And txtProduct.Text.Trim = "" Then
                    qry = "Delete From PO_Requisition_Details  Where InLineNumber = @InLineNumber"
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

    Public Function SelectChangeHistory(ByVal RowNumber As String) As DataTable
        'Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT * FROM [ChangeHistoryRequisition] where  ChangeHistoryRequisition.CustomerID  = @RowNumber   and CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 AND  [ChangeHistoryRequisition].TableName = 'PO_Requisition_Details'  "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@RowNumber", SqlDbType.NVarChar)).Value = RowNumber
            da.SelectCommand = com
            da.Fill(dt)


        Catch ex As Exception

        End Try

        Return dt
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


    Public Function PO_Requisition_Details_Status(ByVal RowNumber As Integer) As String
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim LocationName As String = ""
        Dim dt As New DataTable

        ssql = ssql & " SELECT  Status  "
        ssql = ssql & " FROM PO_Requisition_Details Where CompanyID ='" & Me.CompanyID & "' AND DivisionID ='" & Me.DivisionID & "'  AND DepartmentID ='" & Me.DepartmentID & "' "
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



    Public Function SetOrderProductData(ByVal PurchaseNumber As String) As Decimal
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT * FROM [PO_Requisition_Details] where  OrderNo = @OrderNo and CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2  "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
        com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
        com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
        com.Parameters.Add(New SqlParameter("@OrderNo", SqlDbType.NVarChar, 36)).Value = PurchaseNumber
        da.SelectCommand = com
        da.Fill(dt)

        '        Response.Write(dt.Rows.Count)
        Dim total As Decimal = 0
        If dt.Rows.Count <> 0 Then
            Dim n As Integer = 0
            For n = 0 To dt.Rows.Count - 1
                Dim txtQ_ORD As Integer = 0
                Dim txtPACK As Decimal = 0
                Dim txtCOST As Decimal = 0
                Dim txtExt_COSt As Decimal = 0

                Try
                    txtQ_ORD = dt.Rows(n)("Q_ORD")
                Catch ex As Exception

                End Try
                Try
                    txtPACK = dt.Rows(n)("PACK")
                Catch ex As Exception

                End Try
                Try
                    txtCOST = dt.Rows(n)("COST")
                Catch ex As Exception

                End Try
                txtExt_COSt = (txtQ_ORD * txtPACK * txtCOST)
                total = total + txtExt_COSt
            Next
            Return total
        Else
            Return 0
        End If
        Try
        Catch ex As Exception

        End Try

        Return 0
    End Function

    Public Function savechages() As Boolean
        Dim total As Decimal = 0
        Try
            total = SetOrderProductData(txtoderno.Text)
        Catch ex As Exception

        End Try



        ' Return True
        Dim rs As SqlDataReader
        Dim OrderNo As String = ""


        Dim PopOrderNo As New CustomOrder()
        If IsNumeric(txtoderno.Text.Trim) = False Then
            rs = PopOrderNo.GetNextOrdNumber(CompanyID, DepartmentID, DivisionID, "NextRequisitionNumber")
            While rs.Read()
                OrderNo = rs("NextNumberValue")
            End While
            rs.Close()
            txtoderno.Text = OrderNo
        Else
            OrderNo = txtoderno.Text
        End If



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
                ' txtStatus.Text = drpPOStatus.SelectedValue

                Dim drpBuyer As New DropDownList
                drpBuyer = row.FindControl("drpBuyer")
                Try
                    ' txtBuyer.Text = drpBuyer.SelectedValue
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

                        If PO_Requisition_Details_Status(InLineNumber) <> "No Action" Then
                            Continue For
                        End If

                        Try
                            ' total = total + txtExt_COSt.Text
                        Catch ex As Exception

                        End Try

                        ',[Product]=@Product ,[QOH]=@QOH ,[DUMP]=@DUMP ,[Q_REQ]=@Q_REQ ,[PRESOLD]=@PRESOLD ,[COLOR_VARIETY]=@COLOR_VARIETY ,[REMARKS]=@REMARKS ,[Q_ORD]=@Q_ORD ,[PACK]=@PACK ,[COST]=@COST ,[Ext_COSt]=@Ext_COSt ,[Vendor_Code]=@Vendor_Code,[Q_Recv]=@Q_Recv ,[ISSUE]=@ISSUE 

                        qry = "Update PO_Requisition_Details SET  [ShipDate]=@ShipDate ,[Location]=@Location ,[Type]=@Type ,[ProductName]=@ProductName ,[HDStatus]=@HDStatus " _
                        & "  Where InLineNumber = @InLineNumber "

                        'Logchangehistory("", EmployeeID, "PO_Requisition_Details", "Product", txtProduct.Text, txtoderno.Text, InLineNumber, "Text")
                        'Logchangehistory("", EmployeeID, "PO_Requisition_Details", "QOH", txtQOH.Text, txtoderno.Text, InLineNumber, "Money")
                        'Logchangehistory("", EmployeeID, "PO_Requisition_Details", "DUMP", txtDUMP.Text, txtoderno.Text, InLineNumber, "Money")
                        'Logchangehistory("", EmployeeID, "PO_Requisition_Details", "Q_REQ", txtQ_REQ.Text, txtoderno.Text, InLineNumber, "Money")
                        'Logchangehistory("", EmployeeID, "PO_Requisition_Details", "PRESOLD", txtPRESOLD.Text, txtoderno.Text, InLineNumber, "Money")
                        'Logchangehistory("", EmployeeID, "PO_Requisition_Details", "COLOR_VARIETY", txtCOLOR_VARIETY.Text, txtoderno.Text, InLineNumber, "Text")
                        'Logchangehistory("", EmployeeID, "PO_Requisition_Details", "REMARKS", txtREMARKS.Text, txtoderno.Text, InLineNumber, "Text")
                        'Logchangehistory("", EmployeeID, "PO_Requisition_Details", "Q_ORD", txtQ_ORD.Text, txtoderno.Text, InLineNumber, "Money")
                        'Logchangehistory("", EmployeeID, "PO_Requisition_Details", "PACK", txtPACK.Text, txtoderno.Text, InLineNumber, "Money")
                        'Logchangehistory("", EmployeeID, "PO_Requisition_Details", "COST", txtCOST.Text, txtoderno.Text, InLineNumber, "Money")
                        'Logchangehistory("", EmployeeID, "PO_Requisition_Details", "Ext_COSt", txtExt_COSt.Text, txtoderno.Text, InLineNumber, "Money")
                        'Logchangehistory("", EmployeeID, "PO_Requisition_Details", "Vendor_Code", txtVendor_Code.Text, txtoderno.Text, InLineNumber, "Text")
                        ''''Logchangehistory("", EmployeeID, "PO_Requisition_Details", "Buyer", txtREMARKS.Text, txtBuyer.Text, InLineNumber, "Text")
                        ''''Logchangehistory("", EmployeeID, "PO_Requisition_Details", "Status", txtREMARKS.Text, txtStatus.Text, InLineNumber, "Text")
                        'Logchangehistory("", EmployeeID, "PO_Requisition_Details", "Q_Recv", txtQ_Recv.Text, txtoderno.Text, InLineNumber, "Money")
                        'Logchangehistory("", EmployeeID, "PO_Requisition_Details", "ISSUE", txtISSUE.Text, txtoderno.Text, InLineNumber, "Text")

                        Dim com As SqlCommand
                        com = New SqlCommand(qry, connec)

                        com.Parameters.Add(New SqlParameter("@InLineNumber", SqlDbType.BigInt)).Value = InLineNumber

                        'com.Parameters.Add(New SqlParameter("@Product", SqlDbType.NVarChar)).Value = txtProduct.Text
                        'com.Parameters.Add(New SqlParameter("@QOH", SqlDbType.NVarChar)).Value = txtQOH.Text
                        'com.Parameters.Add(New SqlParameter("@DUMP", SqlDbType.NVarChar)).Value = txtDUMP.Text
                        'com.Parameters.Add(New SqlParameter("@Q_REQ", SqlDbType.NVarChar)).Value = txtQ_REQ.Text
                        'com.Parameters.Add(New SqlParameter("@PRESOLD", SqlDbType.NVarChar)).Value = txtPRESOLD.Text
                        'com.Parameters.Add(New SqlParameter("@COLOR_VARIETY", SqlDbType.NVarChar)).Value = txtCOLOR_VARIETY.Text
                        'com.Parameters.Add(New SqlParameter("@REMARKS", SqlDbType.NVarChar)).Value = txtREMARKS.Text
                        'com.Parameters.Add(New SqlParameter("@Q_ORD", SqlDbType.NVarChar)).Value = txtQ_ORD.Text
                        'com.Parameters.Add(New SqlParameter("@PACK", SqlDbType.NVarChar)).Value = txtPACK.Text
                        'com.Parameters.Add(New SqlParameter("@COST", SqlDbType.NVarChar)).Value = txtCOST.Text
                        'com.Parameters.Add(New SqlParameter("@Ext_COSt", SqlDbType.NVarChar)).Value = txtExt_COSt.Text
                        'com.Parameters.Add(New SqlParameter("@Vendor_Code", SqlDbType.NVarChar)).Value = txtVendor_Code.Text
                        ''''com.Parameters.Add(New SqlParameter("@Buyer", SqlDbType.NVarChar)).Value = txtBuyer.Text
                        ''''com.Parameters.Add(New SqlParameter("@Status", SqlDbType.NVarChar)).Value = txtStatus.Text
                        'com.Parameters.Add(New SqlParameter("@Q_Recv", SqlDbType.NVarChar)).Value = txtQ_Recv.Text
                        'com.Parameters.Add(New SqlParameter("@ISSUE", SqlDbType.NVarChar)).Value = txtISSUE.Text

                        '[ShipDate]=@ShipDate ,[Location]=@Location ,[Type]=@Type ,[ProductName]=@ProductName ,[HDStatus]=@HDStatus

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
        'savechages()

        If True Then
            Dim connec As New SqlConnection(constr)
            Dim qry As String

            If True Then

                qry = " Update PO_Requisition_Details SET  [ShipDate]=@ShipDate ,[Location]=@Location ,[Type]=@Type ,[ProductName]= (Select (Case When LEN(ItemName) > 50 Then SUBSTRING(ItemName,0,49) ELSE ItemName End) from InventoryItems Where InventoryItems.CompanyID = PO_Requisition_Details.CompanyID AND InventoryItems.DivisionID  = PO_Requisition_Details.DivisionID AND InventoryItems.DepartmentID  = PO_Requisition_Details.DepartmentID AND InventoryItems.ItemID   = PO_Requisition_Details.Product  ) ,[HDStatus]=@HDStatus " _
                        & "  Where  CompanyID=@CompanyID AND DivisionID=@DivisionID AND DepartmentID=@DepartmentID AND  OrderNo = @OrderNo "

                Dim com As SqlCommand
                com = New SqlCommand(qry, connec)

                com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
                com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar)).Value = Me.DivisionID
                com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar)).Value = Me.DepartmentID
                com.Parameters.Add(New SqlParameter("@OrderNo", SqlDbType.NVarChar)).Value = txtoderno.Text
                com.Parameters.Add(New SqlParameter("@ShipDate", SqlDbType.NVarChar)).Value = txtshipdate.Text
                com.Parameters.Add(New SqlParameter("@Type", SqlDbType.NVarChar)).Value = drpType.SelectedValue
                'com.Parameters.Add(New SqlParameter("@ProductName", SqlDbType.NVarChar, 50)).Value = getitemname(txtProduct.Text)
                com.Parameters.Add(New SqlParameter("@HDStatus", SqlDbType.NVarChar)).Value = drpStatus.SelectedValue
                com.Parameters.Add(New SqlParameter("@Location", SqlDbType.NVarChar)).Value = cmblocationid.SelectedValue

                com.Connection.Open()
                com.ExecuteNonQuery()
                com.Connection.Close()


            End If

        End If
        Dim MVC As String = ""
        Try
            MVC = Request.QueryString("MVC")
        Catch ex As Exception

        End Try

        If MVC = "1" Then
            Response.Redirect("http://bpom.quickflora.com/Web/BPO.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&TerminalID=" & "DEFAULT" & "&locationid=" & Session("Locationid") & "&EmployeeID=" & EmployeeID & "&Session_SessionID=" & Session.SessionID)

        Else
            Response.Redirect("RequisitionOrderList.aspx")
        End If
    End Sub


    Public Function Logchangehistory(ByVal CustomerID As String, ByVal EmployeeID As String, ByVal tableName As String, ByVal fieldName As String, ByVal fieldChangeValue As String, ByVal OrderNumber As String, ByVal OrderLine As String, ByVal type As String) As Boolean
        'Return True
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
        dt.Columns.Add(New DataColumn("FromReq"))
        dt.Columns.Add(New DataColumn("ToReq"))

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
            'txtStatus.Text = drpPOStatus.SelectedValue

            Dim drpBuyer As New DropDownList
            drpBuyer = row.FindControl("drpBuyer")
            Try
                ' txtBuyer.Text = drpBuyer.SelectedValue
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
            dtLoadItemsData = LoadItemsData(ItemCategoryID, ItemFamilyID)
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
            dr("FromReq") = String.Empty
            dr("ToReq") = String.Empty
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

                Try
                    dr("Vendor_Code") = dtLoadItemsData.Rows(n)(3)
                Catch ex As Exception
                    dr("Vendor_Code") = String.Empty
                End Try


                dr("Buyer") = String.Empty
                dr("Status") = "No Action"
                dr("Q_Recv") = String.Empty
                dr("ISSUE") = String.Empty
                dr("FromReq") = String.Empty
                dr("ToReq") = String.Empty

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
        dt.Columns.Add(New DataColumn("FromReq"))
        dt.Columns.Add(New DataColumn("ToReq"))

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
            dr("FromReq") = String.Empty
            dr("ToReq") = String.Empty
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
                dr("FromReq") = String.Empty
                dr("ToReq") = String.Empty
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

        If dt.Rows.Count = 1 Then
            Dim o As New Object
            Dim e As New EventArgs
            drpshipemthod_SelectedIndexChanged(o, e)
        Else

        End If



        'Dim lst As New ListItem
        'lst.Value = ""
        'lst.Text = "-- Select Ship Method --"
        'drpshipemthod.Items.Insert(0, lst)

    End Sub


    Private Sub drpInventoryOrigin_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles drpInventoryOrigin.SelectedIndexChanged, cmblocationid.SelectedIndexChanged

        If Me.CompanyID <> "QuickfloraDemo" And Me.CompanyID <> "DierbergsMarkets,Inc63017" Then
            txtshipdate.Text = ""
            txtarrivedate.Text = ""
            SetShipMethoddropdown()
        End If

    End Sub

    Private Sub drpshipemthod_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles drpshipemthod.SelectedIndexChanged
        If Me.CompanyID <> "QuickfloraDemo" And Me.CompanyID <> "DierbergsMarkets,Inc63017" Then
            txtshipdate.Text = ""
            txtarrivedate.Text = ""
            ' Exit Sub
        End If

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
        If Me.CompanyID <> "DierbergsMarkets,Inc63017" Then
            Me.ClientScript.RegisterStartupScript(Me.GetType(), "onLoadCallShipDate", onloadScript.ToString())
        End If



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

        If Me.CompanyID <> "DierbergsMarkets,Inc63017" Then
            txtarrivedate.Text = dtnow.Date.ToShortDateString
        End If


        saveall()

    End Sub

    Private Sub RequisitionOrder_PreRenderComplete(ByVal sender As Object, ByVal e As EventArgs) Handles Me.PreRender

    End Sub


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

    Private Sub dvsave_Load(ByVal sender As Object, ByVal e As EventArgs) Handles dvsave.Load

    End Sub

    Private Sub btnsaveUP_Click(sender As Object, e As EventArgs) Handles btnsaveUP.Click, btnsave.Click
        txtlastchanged.Text = Date.Now
        txtlastchangedby.Text = EmployeeID

        Dim chk As String = ""
        Try
            chk = Session("LoadProductList") ' = "True"
        Catch ex As Exception

        End Try

        If chk = "True" Then
            '  Checkonsubmit()
        End If


        AllVendor = True

        If AllVendor Then
            drpStatus.SelectedValue = "Entry Completed"
            txtorderplaced.Text = Date.Now
            txtorderby.Text = EmployeeID
            ' savechages()
            saveall()


            If True Then
                Dim connec As New SqlConnection(constr)
                Dim qry As String

                If True Then

                    qry = " Update PO_Requisition_Details SET  [ShipDate]=@ShipDate ,[Location]=@Location ,[Type]=@Type ,[ProductName]= (Select (Case When LEN(ItemName) > 50 Then SUBSTRING(ItemName,0,49) ELSE ItemName End) from InventoryItems Where InventoryItems.CompanyID = PO_Requisition_Details.CompanyID AND InventoryItems.DivisionID  = PO_Requisition_Details.DivisionID AND InventoryItems.DepartmentID  = PO_Requisition_Details.DepartmentID AND InventoryItems.ItemID   = PO_Requisition_Details.Product  ) ,[HDStatus]=@HDStatus " _
                        & "  Where  CompanyID=@CompanyID AND DivisionID=@DivisionID AND DepartmentID=@DepartmentID AND  OrderNo = @OrderNo "

                    Dim com As SqlCommand
                    com = New SqlCommand(qry, connec)

                    com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
                    com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar)).Value = Me.DivisionID
                    com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar)).Value = Me.DepartmentID
                    com.Parameters.Add(New SqlParameter("@OrderNo", SqlDbType.NVarChar)).Value = txtoderno.Text
                    com.Parameters.Add(New SqlParameter("@ShipDate", SqlDbType.NVarChar)).Value = txtshipdate.Text
                    com.Parameters.Add(New SqlParameter("@Type", SqlDbType.NVarChar)).Value = drpType.SelectedValue
                    'com.Parameters.Add(New SqlParameter("@ProductName", SqlDbType.NVarChar, 50)).Value = getitemname(txtProduct.Text)
                    com.Parameters.Add(New SqlParameter("@HDStatus", SqlDbType.NVarChar)).Value = drpStatus.SelectedValue
                    com.Parameters.Add(New SqlParameter("@Location", SqlDbType.NVarChar)).Value = cmblocationid.SelectedValue

                    com.Connection.Open()
                    com.ExecuteNonQuery()
                    com.Connection.Close()


                End If

            End If


            If Me.CompanyID = "DierbergsMarkets,Inc63017" Then
                If lblmasterlocation.Text <> "" And lblmasterlocation.Text <> cmblocationid.SelectedValue Then

                    Dim dt_lc As New DataTable
                    dt_lc = DetailsOrder_Location(cmblocationid.SelectedValue)
                    If dt_lc.Rows.Count > 0 Then
                        Dim emaillc As String = ""
                        Try
                            emaillc = dt_lc.Rows(0)("Email")
                        Catch ex As Exception

                        End Try

                        Dim locationame As String = ""
                        Try
                            locationame = dt_lc.Rows(0)("LocationName")
                        Catch ex As Exception

                        End Try
                        Dim QFmail As New com.quickflora.qfscheduler.QFPrintService
                        Dim emailconent As String = ""
                        ' emailconent = emailconent & "InLineNumber : " & InLineNumber & "<br>"
                        emailconent = emailconent & " Hello, " & "" & "<br><br>"
                        emailconent = emailconent & " Admin has created requisition # " & txtoderno.Text & "  for you. Please have a look and make the edits if required. " & "" & "<br><br>"
                        emailconent = emailconent & " Thanks, " & "" & "<br>"
                        emailconent = emailconent & " Dierbergs Floral Buying Team " & "" & "<br>"
                        lblmail.Text = "Attention: New Requisition # " & txtoderno.Text & " created for your location:" & locationame
                        Dim emm As String = ""
                        If Me.CompanyID = "DierbergsMarkets,Inc63017" Then
                            emm = "floralorders@dierbergs.com"
                        Else
                            emm = "info@quickflora.com"
                        End If
                        Try
                            QFmail.newmailsending(emm, emaillc, "", "", lblmail.Text, emailconent, CompanyID, DivisionID, DepartmentID)
                            'QFmail.newmailsending(emm, "gaurav@quickflora.com", "imy@quickflora.com", "", lblmail.Text, emailconent, CompanyID, DivisionID, DepartmentID)
                            'mailto:
                        Catch ex As Exception

                        End Try

                        lblmail.Text = lblmail.Text & "<br>" & emaillc
                    End If
                    'Exit Sub
                    lblmail.Visible = False
                Else

                End If
            End If


            Dim MVC As String = ""
            Try
                MVC = Request.QueryString("MVC")
            Catch ex As Exception

            End Try

            If MVC = "1" Then
                Response.Redirect("http://bpom2.quickflora.com/Web/BPO.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&TerminalID=" & "DEFAULT" & "&locationid=" & Session("Locationid") & "&EmployeeID=" & EmployeeID & "&Session_SessionID=" & Session.SessionID)

            Else
                Response.Redirect("RequisitionOrderList.aspx")
            End If
        Else
            dvsavealert.Visible = True
            lblsavealert.Text = "Please select Vendor for each items to submit complete Request."

            saveall()

            dvsave.Visible = False
            lblsave.Text = ""
        End If


    End Sub

    Private Sub btnsavechangesUP_Click(sender As Object, e As EventArgs) Handles btnsavechangesUP.Click, btnsavechanges.Click
        Dim total As Decimal = 0
        Try
            total = SetOrderProductData(txtoderno.Text)
            txttotal.Text = total
        Catch ex As Exception

        End Try

        txtlastchanged.Text = Date.Now
        txtlastchangedby.Text = EmployeeID

        saveall()

        If True Then
            Dim connec As New SqlConnection(constr)
            Dim qry As String

            If True Then

                qry = " Update PO_Requisition_Details SET  [ShipDate]=@ShipDate ,[Location]=@Location ,[Type]=@Type ,[ProductName]= (Select (Case When LEN(ItemName) > 50 Then SUBSTRING(ItemName,0,49) ELSE ItemName End) from InventoryItems Where InventoryItems.CompanyID = PO_Requisition_Details.CompanyID AND InventoryItems.DivisionID  = PO_Requisition_Details.DivisionID AND InventoryItems.DepartmentID  = PO_Requisition_Details.DepartmentID AND InventoryItems.ItemID   = PO_Requisition_Details.Product  ) ,[HDStatus]=@HDStatus " _
                        & "  Where  CompanyID=@CompanyID AND DivisionID=@DivisionID AND DepartmentID=@DepartmentID AND  OrderNo = @OrderNo "

                Dim com As SqlCommand
                com = New SqlCommand(qry, connec)

                com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
                com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar)).Value = Me.DivisionID
                com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar)).Value = Me.DepartmentID
                com.Parameters.Add(New SqlParameter("@OrderNo", SqlDbType.NVarChar)).Value = txtoderno.Text
                com.Parameters.Add(New SqlParameter("@ShipDate", SqlDbType.NVarChar)).Value = txtshipdate.Text
                com.Parameters.Add(New SqlParameter("@Type", SqlDbType.NVarChar)).Value = drpType.SelectedValue
                'com.Parameters.Add(New SqlParameter("@ProductName", SqlDbType.NVarChar, 50)).Value = getitemname(txtProduct.Text)
                com.Parameters.Add(New SqlParameter("@HDStatus", SqlDbType.NVarChar)).Value = drpStatus.SelectedValue
                com.Parameters.Add(New SqlParameter("@Location", SqlDbType.NVarChar)).Value = cmblocationid.SelectedValue

                com.Connection.Open()
                com.ExecuteNonQuery()
                com.Connection.Close()


            End If

        End If

        lblsavechanges.Text = "Data Saved Successfully"
        lblsavechanges.Visible = True
    End Sub
End Class

