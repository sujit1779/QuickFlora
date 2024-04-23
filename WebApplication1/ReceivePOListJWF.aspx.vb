Option Strict Off

Imports System.Data.SqlClient
Imports Microsoft.Office
Imports System.Data
Imports DAL
Imports EnterpriseCommon.Configuration


Partial Class ReceivePOList
    Inherits System.Web.UI.Page


    Dim ConnectionString As String = ""
    Public CompanyID As String = ""
    Public DivisionID As String = ""
    Public DepartmentID As String = ""
    Dim EmployeeID As String = ""
    Dim ShipMethod As String = ""
    Dim FromDate As String = ""
    Dim ToDate As String = ""
    Dim fieldName As String = ""
    Dim Condition As String = ""
    Dim fieldexpression As String = ""
    Dim AllDate As Integer
    Public SortField As String = ""
    Public SortDirection As String = ""
    Public itemsearchcss As String = ""


    Public Function GetStatus(ByVal ob As Object) As String

        Dim OrderStatus As String = ob.ToString()


        If OrderStatus = "True" Then
            Return "Yes"
        Else
            Return "No"
        End If


        Return "No"

    End Function




    Public Function FillLocationName(ByVal LocationID As String) As String
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim LocationName As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT LocationName FROM [Order_Location] where  LocationID <> 'Wholesale' and CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and LocationID=@LocationID"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@LocationID", SqlDbType.NVarChar, 36)).Value = LocationID

            da.SelectCommand = com
            da.Fill(dt)


            If dt.Rows.Count <> 0 Then
                Try
                    LocationName = dt.Rows(0)(0)
                Catch ex As Exception

                End Try
            End If

            Return LocationName
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return LocationName
        End Try
        Return LocationName
    End Function



    Protected Sub btncustsearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btncustsearch.Click
        Dim TempCustomerID As String = txtcustomersearch.Text
        lblsearchcustomermsg.ForeColor = Drawing.Color.Black

        If txtcustomersearch.Text.Trim = "" Then
            Exit Sub

        End If

        If TempCustomerID.IndexOf("[") > -1 Then
            Dim st, ed As Integer
            st = TempCustomerID.IndexOf("[")
            ed = TempCustomerID.IndexOf("]")

            If st = -1 Then
                Exit Sub
            End If

            If (ed - st) - 1 = -1 Then
                Exit Sub
            End If

            TempCustomerID = TempCustomerID.Substring(st + 1, (ed - st) - 1)
        End If


        txtSearchExpression.Text = TempCustomerID

        BindOrderHeaderList()

        txtcustomersearch.Text = ""

    End Sub


    Public Function PopulateCompaniesNewOrderEntryformDetails(ByVal companyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As Boolean

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim neworderentryform As Boolean = False
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT  * FROM   Companies   where  CompanyID='" & companyID & "' AND  [DivisionID]='" & DivisionID & "' AND  [DepartmentID]='" & DepartmentID & "'"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            da.SelectCommand = com
            da.Fill(dt)

            If dt.Rows.Count <> 0 Then


                Try
                    neworderentryform = dt.Rows(0)("neworderentryform")
                Catch ex As Exception

                End Try


            End If

            Return neworderentryform
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return neworderentryform
        End Try

    End Function




    Public Property FieldSortDirection() As String
        Get
            Return ViewState("FieldSortDirection")
        End Get
        Set(ByVal value As String)
            ViewState("FieldSortDirection") = value
        End Set
    End Property

    Public Property SortFieldName() As String
        Get
            Return ViewState("SortFieldName")
        End Get
        Set(ByVal value As String)
            ViewState("SortFieldName") = value
        End Set
    End Property


    Public Function getWirOut(ByVal ob As Object) As Boolean

        ShipMethod = ob.ToString()


        If ShipMethod = "Wire_Out" Then



            Return False
        Else
            Return True
        End If




    End Function

    Public Function getEditVisible(ByVal ob As Object) As Boolean

        Dim OrderStatus As String = ob.ToString()


        If OrderStatus = "Received" Then
            Return False

        Else
            Return True
        End If


        Return True

    End Function


    Public Function getDeleteVisible(ByVal ob As Object) As Boolean

        Dim OrderStatus As String = ob.ToString()


        If OrderStatus = "Not Booked" Or OrderStatus = "Booked" Or OrderStatus = "Routed" Or OrderStatus = "Shipped" Or OrderStatus = "Picked" Or OrderStatus = "Delivered" Or OrderStatus = "Returned" Then



            Return True
        Else
            Return False
        End If




    End Function

    Public Function myIsDate(ByVal pExpression As Object) As Boolean

        Try





            myIsDate = False



            ' Check we have an object

            If Not pExpression Is Nothing Then

                Dim expressionString As String = ""



                ' Check it's a string (and isn't empty)

                If pExpression.GetType Is expressionString.GetType AndAlso Not pExpression = String.Empty Then

                    Dim dateRegEx As Regex = New Regex("(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d")



                    ' Check it's a date

                    If Not pExpression Is Nothing Then

                        If dateRegEx.IsMatch(pExpression) Then



                            myIsDate = True

                        Else

                            myIsDate = False

                        End If

                    End If



                End If



            End If

        Catch

            myIsDate = False



        End Try

    End Function

    Public Function myIsDecimal(ByVal pExpression As Object) As Boolean

        Try



            ' Default

            myIsDecimal = False



            ' Check we have an object

            If Not pExpression Is Nothing Then

                Dim expressionString As String = ""



                ' Check it's a string (and isn't empty)

                If pExpression.GetType Is expressionString.GetType AndAlso Not pExpression = String.Empty Then

                    Dim dateRegEx As Regex = New Regex("[-+]?([0-9]*\.[0-9]+|[0-9]+)")



                    ' Check it's a Decimal

                    If Not pExpression Is Nothing Then

                        If dateRegEx.IsMatch(pExpression) Then



                            myIsDecimal = True

                        Else

                            myIsDecimal = False

                        End If

                    End If



                End If



            End If

        Catch

            myIsDecimal = False



        End Try

    End Function


    Public Function ReportPage(ByVal OrderNumber As String) As String


        Return "javascript:window.open('../../EnterpriseASPAR/CustomOrder/WTReport.aspx?OrderNumber=" & OrderNumber & " ' );"

    End Function

    Public Function GetEnclosureCard(ByVal OrderNumber As String) As String

        Return "javascript:window.open('../../EnterpriseASPAR/CustomOrder/EnclosureCardReport.aspx?OrderNumber=" & OrderNumber & " ' );"

    End Function


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

    Public Function Fillfamily() As DataTable
        Dim connec As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from InventoryFamilies where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 Order by InventoryFamilies.FamilyName  "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID

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


    Public Function GetItemCategories(ByVal ItemFamilyID As String) As DataSet

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetItemCategoriesByItemFamily]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                Command.Parameters.AddWithValue("ItemFamilyID", ItemFamilyID)

                Try
                    Dim ds As New DataSet
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)

                    If ds.Tables(0).Rows.Count >= 0 Then
                        Return ds
                    Else
                        Return Nothing
                    End If

                Catch ex As Exception
                    'Debug = ex.Message
                    Return Nothing

                End Try

            End Using
        End Using

    End Function


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Session("CompanyID") Is Nothing Then
            Response.Redirect("loginform.aspx")
        End If

        txtcustomersearch.Attributes.Add("placeholder", "SEARCH")
        txtcustomersearch.Attributes.Add("onKeyUp", "SendQuery(this.value)")

        'Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        '' get the connection ready
        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

        rs = PopulateEmployees(CompanyID, DepartmentID, DivisionID, "REC")

        Dim securitycheck As Boolean = False

        While (rs.Read())

            If rs("EmployeeID").ToString() = EmployeeID Then
                securitycheck = True
                Exit While
            End If

        End While
        rs.Close()

        If securitycheck = False Then
            Response.Redirect("SecurityAcessPermission.aspx?MOD=REC")
        End If

        If CompanyID.ToUpper() = "JWF" Then
            'Response.Redirect("Home.aspx")
        End If

        Dim AllowRefundFunction As Boolean
        AllowRefundFunction = CheckAllowRefnudFunction(CompanyID, DivisionID, DepartmentID)
        If Not AllowRefundFunction Then
            OrderHeaderGrid.Columns(1).Visible = False
        End If

        If Not Page.IsPostBack Then
            txtDateFrom.Text = DateTime.Now.ToString("MM/dd/yyyy")
            txtDateTo.Text = DateTime.Now.ToString("MM/dd/yyyy")
            SetLocationIDdropdown()
            SetFillVendorn()
            BindPaymentandDeliveryList()

            Dim dtfamily As New DataTable
            dtfamily = Fillfamily()

            drpProductFamily.DataSource = dtfamily
            drpProductFamily.DataTextField = "FamilyName"
            drpProductFamily.DataValueField = "ItemFamilyID"
            drpProductFamily.DataBind()
            Dim lst As New ListItem
            lst.Text = "--Select--"
            lst.Value = ""
            drpProductFamily.Items.Insert(0, lst)

            Dim ds As New DataSet
            ds = GetItemCategories(drpProductFamily.SelectedValue)

            ctl00_ContentPlaceHolder_drpProductCategory.DataSource = ds
            ctl00_ContentPlaceHolder_drpProductCategory.DataTextField = "CategoryName"
            ctl00_ContentPlaceHolder_drpProductCategory.DataValueField = "ItemCategoryID"
            ctl00_ContentPlaceHolder_drpProductCategory.DataBind()
            Dim lst1 As New ListItem
            lst1.Text = "--Select--"
            lst1.Value = ""
            ctl00_ContentPlaceHolder_drpProductCategory.Items.Insert(0, lst1)

            Dim dtItemGroup As New DataTable
            dtItemGroup = FillItemGroup(drpProductFamily.SelectedValue, ctl00_ContentPlaceHolder_drpProductCategory.SelectedValue)
            'drpItemGroup
            ' ,[ItemGroupID]
            ' ,[GroupName]
            drpGrp.DataSource = dtItemGroup
            drpGrp.DataTextField = "GroupName"
            drpGrp.DataValueField = "ItemGroupID"
            drpGrp.DataBind()
            Dim lst2 As New ListItem
            lst2.Text = "--Select--"
            lst2.Value = ""
            drpGrp.Items.Insert(0, lst2)

            BindOrderHeaderList()
        End If

        If CompanyID = "metroflowermarket" Or CompanyID = "SouthFloral" Or CompanyID = "SouthFloralsTraining" Or CompanyID = "JoseDiazWH" Or CompanyID = "FLORICA90004" Or CompanyID = "QuickfloraDemo" Or CompanyID = "wholesaledemo" Or CompanyID = "Florica90004" Or CompanyID = "CowardandGlisson33955" Or CompanyID = "FloristSoftwareDemo" Or CompanyID = "McCarthyg" Or CompanyID = "FloraVina33301" Or CompanyID = "MarshallsWholesaleFlowers" Or CompanyID = "FloricaWholesale" Or CompanyID = "ICON" Or CompanyID = "HBFARM_" Or CompanyID = "MatrangaFloral" Or CompanyID = "DierbergsMarkets,Inc63017" Or CompanyID = "FreytagsFlorist" Or Me.CompanyID = "DierbergsMarketsJune2020" Or Me.CompanyID.ToUpper = "SAFC".ToUpper Then
            itemsearchcss = itemsearchcss & ".fixed_headers td:nth-child(5), .fixed_headers th:nth-child(5) { width: 50px; }"
            itemsearchcss = itemsearchcss & ".fixed_headers td:nth-child(6), .fixed_headers th:nth-child(6) { width: 50px; }"
            itemsearchcss = itemsearchcss & ".fixed_headers td:nth-child(7), .fixed_headers th:nth-child(7) { width: 50px; }"
            itemsearchcss = itemsearchcss & ".fixed_headers td:nth-child(8), .fixed_headers th:nth-child(8) { width: 120px; }"
            itemsearchcss = itemsearchcss & ".fixed_headers td:nth-child(9), .fixed_headers th:nth-child(9) { width: 100px; }"
            itemsearchcss = itemsearchcss & ".fixed_headers td:nth-child(10), .fixed_headers th:nth-child(10) { width: 100px; }"
            itemsearchcss = itemsearchcss & ".fixed_headers td:nth-child(11), .fixed_headers th:nth-child(11) { width: 100px; }"
        Else
            itemsearchcss = itemsearchcss & ""
        End If

    End Sub


    Public Function FillItemGroup(ByVal ItemFamilyID As String, ByVal ItemCategoryID As String) As DataTable
        Dim connec As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from [InventoryGroups] where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and ItemCategoryID=@ItemCategoryID and ItemFamilyID=@ItemFamilyID Order by GroupName  "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@ItemCategoryID", SqlDbType.NVarChar, 36)).Value = ItemCategoryID
            com.Parameters.Add(New SqlParameter("@ItemFamilyID", SqlDbType.NVarChar, 36)).Value = ItemFamilyID

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



    Public Function FillVendor() As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT * FROM [VendorInformation] where   CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 AND ISNULL(VendorID,'') <> '' order by VendorName ASC"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
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
    Public Sub SetFillVendorn()

        '''''''''''''''''

        Dim dt As New Data.DataTable

        dt = FillVendor()

        If dt.Rows.Count <> 0 Then
            drpvendor.DataSource = dt
            drpvendor.DataTextField = "VendorName"
            drpvendor.DataValueField = "VendorID"
            drpvendor.DataBind()
            'Setdropdown()
        Else
            'cmblocationid.Items.Clear()
            Dim item As New ListItem
            item.Text = "DEFAULT"
            item.Value = "DEFAULT"
            drpvendor.Items.Add(item)
        End If
        ''''''''''''''''''''

    End Sub



    Public Sub SetLocationIDdropdown()
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
                    Exit For
                End If
            Next


        Catch ex As Exception

        End Try

        If locationid_true Then
            cmblocationid.SelectedIndex = cmblocationid.Items.IndexOf(cmblocationid.Items.FindByValue(locationid))
            cmblocationid.Enabled = False
        End If
        ' Session("OrderLocationid") = cmblocationid.SelectedValue
    End Sub


    Protected Sub chkhold_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkhold.CheckedChanged, checkreturn.CheckedChanged, chkcartorders.CheckedChanged, chkall.CheckedChanged
        BindOrderHeaderList()
    End Sub

    Protected Sub cmblocationid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmblocationid.SelectedIndexChanged, drpstatus.SelectedIndexChanged, drpvendor.SelectedIndexChanged, drpOccasionCode.SelectedIndexChanged
        BindOrderHeaderList()
    End Sub


    Private Function CheckAllowRefnudFunction(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("SELECT isnull(bAllowedRefundFunction,0) AS AllowedRefundFunction FROM HomePageManagement WHERE CompanyID = @CompanyID AND DivisionID = @DivisionID AND DepartmentID = @DepartmentID", Connection)
                Command.CommandType = CommandType.Text

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)

                Try
                    Command.Connection.Open()
                    If Command.ExecuteScalar() Then
                        Return True
                    Else
                        Return False
                    End If

                Catch ex As Exception
                    Return False
                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    Public Function checkRefundProcess(ByVal ob As Object, ByVal ob2 As Object, ByVal ob3 As Object) As Boolean
        Dim OrderStatus As String = ob.ToString()
        Dim PaymentMethod As String = ob2.ToString()
        Dim OShipMethod As String = ob2.ToString()

        If OrderStatus = "Invoiced" And (PaymentMethod.ToLower = "credit card" Or PaymentMethod.ToLower = "cash" Or PaymentMethod.ToLower = "check" Or PaymentMethod.ToLower = "house account" Or PaymentMethod.ToLower().Contains("emv")) Then
            'If OrderStatus = "Invoiced" And (PaymentMethod.ToLower = "cash") Then
            Return True
        Else
            Return False
        End If

    End Function


    Public Sub BindPaymentandDeliveryList()
        ' Dim objUser As New DAL.CustomOrder()
        Dim dt As New DataTable
        dt = PopulatePaymentTypes(CompanyID, DepartmentID, DivisionID)

        Payment.DataTextField = "PaymentMethodID"
        Payment.DataValueField = "PaymentMethodID"
        Payment.DataSource = dt
        Payment.DataBind()

        Dim Paymentlist As ListItem = Payment.Items.FindByValue("Will Call")
        If Not Paymentlist Is Nothing Then
            'Payment.Items.FindByValue("Will Call").Selected = True
        End If
        Dim PopOrderType As New CustomOrder()
        rs = PopOrderType.PopulateOccasionCodes(CompanyID, DepartmentID, DivisionID)
        drpOccasionCode.DataTextField = "OccasionDesc"
        drpOccasionCode.DataValueField = "OccasionDesc"
        drpOccasionCode.DataSource = rs
        drpOccasionCode.DataBind()
        drpOccasionCode.Items.Insert(0, (New ListItem("-Select-", "")))
        drpOccasionCode.SelectedIndex = drpOccasionCode.Items.IndexOf(drpOccasionCode.Items.FindByValue("0"))
        rs.Close()

    End Sub




    Public Function PopulatePaymentTypes(ByVal CompanyID As String, ByVal DeptID As String, ByVal DivID As String) As DataTable
        Dim ConnectionString As String = ""

        ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
        Dim ConString As New SqlConnection
        ConString.ConnectionString = ConnectionString
        Dim sqlStr As String = "SELECT * FROM VendorPaymentMethods WHERE CompanyID='" & CompanyID & "' AND DepartmentID='" & DeptID & "' AND DivisionID='" & DivID & "' AND Active=1  "
        Dim Cmd As New SqlCommand
        Cmd.CommandText = sqlStr
        Cmd.Connection = ConString
        Dim rs As New DataTable
        ConString.Open()
        Dim da As New SqlDataAdapter

        da.SelectCommand = Cmd
        da.Fill(rs)

        Return rs
    End Function


    Public Sub BindOrderHeaderList()
        Dim objUser As New DAL.CustomOrder()
        fieldName = drpFieldName.SelectedValue
        Condition = drpCondition.SelectedValue
        fieldexpression = txtSearchExpression.Text.Trim().Replace("'", "''")
        FromDate = txtDateFrom.Text
        ToDate = txtDateTo.Text
        If rdAllDates.Checked Then
            AllDate = 1

        ElseIf rdOrderDates.Checked Then
            AllDate = 2

        ElseIf rdDeliveryDates.Checked Then

            AllDate = 3

        End If

        If drpFieldName.SelectedValue = "OrderShipDate" Then

            If myIsDate(fieldexpression) = True Then

                lblErr.Text = " "
            Else
                lblErr.ForeColor = Drawing.Color.Red


                lblErr.Text = " Please Enter Date in mm/dd/yyyy"
                Exit Sub
            End If

        End If


        If drpFieldName.SelectedValue = "Total" Then

            If myIsDecimal(fieldexpression) = True Then

                lblErr.Text = " "


            Else
                lblErr.ForeColor = Drawing.Color.Red

                lblErr.Text = "Please Enter  Numeric only"

                Exit Sub




            End If
        End If


        Dim strpayment As String = Payment.SelectedValue
        Dim strDelivery As String = Delivery.SelectedValue


        Dim ds As New DataSet()

        ds = POSOrderSearchList(CompanyID, DepartmentID, DivisionID, Condition, fieldName, fieldexpression, FromDate, ToDate, AllDate, Me.SortFieldName, Me.FieldSortDirection, strpayment, strDelivery)

        OrderHeaderGrid.DataSource = ds

        OrderHeaderGrid.DataBind()


    End Sub


    Public Function POSOrderSearchList(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal Condition As String, ByVal fieldName As String, ByVal fieldexpression As String, ByVal FromDate As String, ByVal ToDate As String, ByVal AllDate As Integer, ByVal SortField As String, ByVal SortDirection As String, ByVal Payment As String, ByVal Delivery As String) As DataSet

        Dim conString As New SqlConnection
        conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

        Dim myCommand As New SqlCommand("POSPurchaseReceivingOrderSearchList", conString)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        Dim prPayment As New SqlParameter("@Payment", Data.SqlDbType.NVarChar)
        prPayment.Value = Payment
        myCommand.Parameters.Add(prPayment)

        Dim prInvoiveNo As New SqlParameter("@invoiceNo", Data.SqlDbType.NVarChar)
        prInvoiveNo.Value = InvoiceNo.Text
        'prItemFamily.Value = drpItemFamilyID1.SelectedValue
        myCommand.Parameters.Add(prInvoiveNo)

        Dim prMabwNo As New SqlParameter("@MabwNo", Data.SqlDbType.NVarChar)
        prMabwNo.Value = mawbNo.Text
        'prItemFamily.Value = drpItemFamilyID1.SelectedValue
        myCommand.Parameters.Add(prMabwNo)

        Dim parameterOccasion As New SqlParameter("@occasion", Data.SqlDbType.NVarChar)
        parameterOccasion.Value = drpOccasionCode.SelectedValue
        myCommand.Parameters.Add(parameterOccasion)


        Dim prDelivery As New SqlParameter("@Delivery", Data.SqlDbType.NVarChar)
        prDelivery.Value = Delivery
        myCommand.Parameters.Add(prDelivery)

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
        parameterCompanyID.Value = CompanyID
        myCommand.Parameters.Add(parameterCompanyID)

        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
        parameterDivisionID.Value = DivisionID
        myCommand.Parameters.Add(parameterDivisionID)

        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
        parameterDepartmentID.Value = DepartmentID
        myCommand.Parameters.Add(parameterDepartmentID)

        Dim parameterCondition As New SqlParameter("@Condition", Data.SqlDbType.NVarChar)
        parameterCondition.Value = Condition
        myCommand.Parameters.Add(parameterCondition)


        Dim parameterfieldName As New SqlParameter("@fieldName", Data.SqlDbType.NVarChar)
        parameterfieldName.Value = fieldName
        myCommand.Parameters.Add(parameterfieldName)

        Dim parameterfieldexpression As New SqlParameter("@fieldexpression", Data.SqlDbType.NVarChar)
        parameterfieldexpression.Value = fieldexpression
        myCommand.Parameters.Add(parameterfieldexpression)


        Dim parameterFromDate As New SqlParameter("@FromDate", Data.SqlDbType.NVarChar)
        parameterFromDate.Value = FromDate
        myCommand.Parameters.Add(parameterFromDate)

        Dim parameterToDate As New SqlParameter("@ToDate", Data.SqlDbType.NVarChar)
        parameterToDate.Value = ToDate
        myCommand.Parameters.Add(parameterToDate)


        Dim parameterAllDate As New SqlParameter("@AllDate", Data.SqlDbType.Int)
        parameterAllDate.Value = AllDate
        myCommand.Parameters.Add(parameterAllDate)


        Dim parameterSortField As New SqlParameter("@SortField", Data.SqlDbType.NVarChar)
        parameterSortField.Value = SortField
        myCommand.Parameters.Add(parameterSortField)

        Dim parameterSortDirection As New SqlParameter("@SortDirection", Data.SqlDbType.NVarChar)
        parameterSortDirection.Value = SortDirection
        myCommand.Parameters.Add(parameterSortDirection)

        Dim pOrderStatus As New SqlParameter("@OrderStatus", Data.SqlDbType.NVarChar)
        pOrderStatus.Value = drpstatus.SelectedValue
        myCommand.Parameters.Add(pOrderStatus)
        '@VendorID
        Dim pVendorID As New SqlParameter("@VendorID", Data.SqlDbType.NVarChar)
        pVendorID.Value = drpvendor.SelectedValue
        myCommand.Parameters.Add(pVendorID)

        If cmblocationid.SelectedValue <> "" Then
            myCommand.Parameters.AddWithValue("@LocationID", cmblocationid.SelectedValue)
        Else
            myCommand.Parameters.AddWithValue("@LocationID", "")
        End If



        Dim adapter As New SqlDataAdapter(myCommand)
        Dim ds As New DataSet()
        Try
            adapter.Fill(ds)
        Catch ex As Exception

        End Try

        conString.Close()

        Return ds


    End Function


    Protected Sub OrderHeaderGrid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles OrderHeaderGrid.PageIndexChanging
        OrderHeaderGrid.PageIndex = e.NewPageIndex
        BindOrderHeaderList()
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindOrderHeaderList()

    End Sub

    Public Sub SessionClearDataInStart()


    End Sub

    Protected Sub OrderHeaderGrid_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles OrderHeaderGrid.RowEditing
        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

        Dim PurchaseOrderNumber As String = OrderHeaderGrid.DataKeys(e.NewEditIndex).Value.ToString()

        Response.Redirect("~/ReceivePO.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&EmployeeID=" & EmployeeID & "&PurchaseOrderNumber=" & PurchaseOrderNumber)
        '?CompanyID=Greene+And+Greene&DivisionID=DEFAULT&DepartmentID=DEFAULT&EmployeeID=Admin

    End Sub




    Protected Sub OrderHeaderGrid_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles OrderHeaderGrid.Sorting

        If e.SortExpression = "OrderTypeID" Then
            If Me.FieldSortDirection = "ASC" Then
                e.SortExpression = "OrderTypeID"
                Me.FieldSortDirection = "DESC"
                SortField = "OrderTypeID"
                SortDirection = "ASC"
                Me.SortFieldName = "OrderTypeID"
            Else
                e.SortExpression = "OrderTypeID"
                Me.FieldSortDirection = "ASC"

                SortField = "OrderTypeID"
                Me.SortFieldName = "OrderTypeID"
                SortDirection = "DESC"
            End If
        ElseIf e.SortExpression = "OrderStatus" Then
            If Me.FieldSortDirection = "ASC" Then
                e.SortExpression = "OrderStatus"
                SortField = "OrderStatus"
                Me.SortFieldName = "OrderStatus"
                Me.FieldSortDirection = "DESC"
            Else
                e.SortExpression = "OrderStatus"
                Me.FieldSortDirection = "ASC"

                SortField = "OrderStatus"
                Me.SortFieldName = "OrderStatus"
                SortDirection = "DESC"
            End If
        ElseIf e.SortExpression = "OrderNumber" Then
            If Me.FieldSortDirection = "ASC" Then
                e.SortExpression = "OrderNumber"
                SortField = "OrderNumber"
                Me.SortFieldName = "OrderNumber"
                SortDirection = "ASC"
                Me.FieldSortDirection = "DESC"
            Else
                e.SortExpression = "OrderNumber"
                Me.FieldSortDirection = "ASC"
                Me.SortFieldName = "OrderNumber"
                SortDirection = "DESC"
            End If
        ElseIf e.SortExpression = "CustomerID" Then
            If Me.FieldSortDirection = "ASC" Then
                e.SortExpression = "CustomerID"
                SortField = "CustomerID"
                Me.SortFieldName = "CustomerID"
                SortDirection = "ASC"
                Me.FieldSortDirection = "DESC"
            Else
                e.SortExpression = "CustomerID"
                Me.FieldSortDirection = "ASC"
                Me.SortFieldName = "CustomerID"
                SortDirection = "DESC"
            End If
        ElseIf e.SortExpression = "CustomerFirstName" Then
            If Me.FieldSortDirection = "ASC" Then
                e.SortExpression = "CustomerFirstName"
                SortField = "CustomerID"
                Me.SortFieldName = "CustomerFirstName"
                SortDirection = "ASC"
                Me.FieldSortDirection = "DESC"
            Else
                e.SortExpression = "CustomerFirstName"
                Me.FieldSortDirection = "ASC"
                Me.SortFieldName = "CustomerFirstName"
                SortDirection = "DESC"
            End If
        ElseIf e.SortExpression = "CustomerLastName" Then
            If Me.FieldSortDirection = "ASC" Then
                e.SortExpression = "CustomerLastName"
                SortField = "CustomerLastName"
                Me.SortFieldName = "CustomerLastName"
                SortDirection = "ASC"
                Me.FieldSortDirection = "DESC"
            Else
                e.SortExpression = "CustomerLastName"
                Me.FieldSortDirection = "ASC"
                Me.SortFieldName = "CustomerLastName"
                SortDirection = "DESC"
            End If
        ElseIf e.SortExpression = "OrderShipDate" Then
            If Me.FieldSortDirection = "ASC" Then
                e.SortExpression = "OrderShipDate"
                SortField = "OrderShipDate"
                Me.SortFieldName = "OrderShipDate"
                SortDirection = "ASC"
                Me.FieldSortDirection = "DESC"
            Else
                e.SortExpression = "OrderShipDate"
                Me.FieldSortDirection = "ASC"
                Me.SortFieldName = "OrderShipDate"
                SortDirection = "DESC"
            End If
        ElseIf e.SortExpression = "Total" Then
            If Me.FieldSortDirection = "ASC" Then
                e.SortExpression = "Total"
                SortField = "Total"
                Me.SortFieldName = "Total"
                SortDirection = "ASC"
                Me.FieldSortDirection = "DESC"
            Else
                e.SortExpression = "Total"
                Me.FieldSortDirection = "ASC"
                Me.SortFieldName = "Total"
                SortDirection = "DESC"
            End If
        ElseIf e.SortExpression = "PaymentMethodID" Then
            If Me.FieldSortDirection = "ASC" Then
                e.SortExpression = "PaymentMethodID"
                SortField = "PaymentMethodID"
                Me.SortFieldName = "PaymentMethodID"
                SortDirection = "ASC"
                Me.FieldSortDirection = "DESC"
            Else
                e.SortExpression = "PaymentMethodID"
                Me.FieldSortDirection = "ASC"
                Me.SortFieldName = "PaymentMethodID"
                SortDirection = "DESC"
            End If
        ElseIf e.SortExpression = "ShipMethodID" Then
            If Me.FieldSortDirection = "ASC" Then
                e.SortExpression = "ShipMethodID"
                SortField = "ShipMethodID"
                Me.SortFieldName = "ShipMethodID"
                SortDirection = "ASC"
                Me.FieldSortDirection = "DESC"
            Else
                e.SortExpression = "ShipMethodID"
                Me.FieldSortDirection = "ASC"
                Me.SortFieldName = "ShipMethodID"
                SortDirection = "DESC"
            End If
        End If

        BindOrderHeaderList()
    End Sub

    Protected Sub Payment_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Payment.SelectedIndexChanged
        BindOrderHeaderList()
    End Sub

    Protected Sub Delivery_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Delivery.SelectedIndexChanged
        BindOrderHeaderList()
    End Sub

    Private Sub OrderHeaderGrid_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles OrderHeaderGrid.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim imgEdit As New ImageButton
            imgEdit = e.Row.FindControl("imgEdit")
            Dim lblOrderNumber As New Label
            lblOrderNumber = e.Row.FindControl("lblOrderNumber")

            If Me.CompanyID = "DierbergsMarkets,Inc63017" Or Me.CompanyID = "QuickfloraDemo" Then
                Dim lblArrival As New Label
                lblArrival = e.Row.FindControl("lblArrival")
                Dim dtPODETAILS As New DataTable
                dtPODETAILS = PODETAILS(lblOrderNumber.Text)
                If dtPODETAILS.Rows.Count <> 0 Then
                    Try
                        lblArrival.Text = dtPODETAILS.Rows(0)("ArriveDate")
                    Catch ex As Exception
                        'lblArrival.Text = ex.Message
                    End Try
                End If
                lblArrival.Text = lblArrival.Text.Replace("12:00:00 AM", "")
            End If
            Dim Consolidatelocation As String = ""

            Dim imgTransfer As New ImageButton
            imgTransfer = e.Row.FindControl("imgTransfer")

            Dim dt3 As New DataTable
            dt3 = POHeaderDETAILS(lblordernumber.Text)
            If dt3.Rows.Count <> 0 Then
                Dim Done As Boolean = False
                Try
                    Done = dt3.Rows(0)("Done")
                Catch ex As Exception

                End Try
                Dim canceled As Boolean = False
                Try
                    canceled = dt3.Rows(0)("Canceled")
                Catch ex As Exception

                End Try
                Dim Received As Boolean = False
                Try
                    Received = dt3.Rows(0)("Received")
                Catch ex As Exception

                End Try

                Try
                    Consolidatelocation = dt3.Rows(0)("Consolidatelocation")
                Catch ex As Exception

                End Try
                If Consolidatelocation.ToString().Trim = "" Then
                    imgTransfer.Visible = False
                Else
                    If Done Then
                        ' imgEdit.Visible = False
                    Else
                        imgTransfer.Visible = False
                    End If
                End If
                If CompanyID = "DierbergsMarkets,Inc63017" Or CompanyID = "QuickfloraDemo" Then
                    If Received Then
                        imgEdit.Visible = False
                    End If
                End If

            End If

            Dim dt4 As New DataTable
            dt4 = CheckPO(lblOrderNumber.Text)
            If dt4.Rows.Count = 0 Then
                e.Row.Visible = False
            End If
        End If
    End Sub


    Public Function PODETAILS(ByVal PurchaseNumber As String) As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim LocationName As String = ""
        Dim dt As New DataTable

        'From [Enterprise].[dbo].[PurchaseDetail]
        'Order By [PurchaseLineNumber] DESC

        ssql = ssql & " SELECT  *  "
        ssql = ssql & " FROM [PurchaseDetail] Where CompanyID ='" & Me.CompanyID & "' AND DivisionID ='" & Me.DivisionID & "'  AND DepartmentID ='" & Me.DepartmentID & "' "
        ssql = ssql & " AND [PurchaseNumber] ='" & PurchaseNumber & "'   "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        da.SelectCommand = com
        da.Fill(dt)


        Return dt
    End Function

    Public Function CheckPO(ByVal PurchaseNumber As String) As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim LocationName As String = ""
        Dim dt As New DataTable

        'From [Enterprise].[dbo].[PurchaseDetail]
        'Order By [PurchaseLineNumber] DESC

        ssql = "GetPOByItemFilter"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand


        com = New SqlCommand(ssql, connec)
        com.CommandType = CommandType.StoredProcedure
        com.Parameters.AddWithValue("CompanyID", Me.CompanyID)
        com.Parameters.AddWithValue("DivisionID", Me.DivisionID)
        com.Parameters.AddWithValue("DepartmentID", Me.DepartmentID)
        com.Parameters.AddWithValue("PO", PurchaseNumber)
        com.Parameters.AddWithValue("ItemID", txtitemsearch.Text)
        com.Parameters.AddWithValue("family", drpProductFamily.SelectedValue)
        com.Parameters.AddWithValue("category", ctl00_ContentPlaceHolder_drpProductCategory.SelectedValue)
        com.Parameters.AddWithValue("grp", drpGrp.SelectedValue)
        com.Parameters.AddWithValue("color", itemColor.Value)
        com.Parameters.AddWithValue("size", itemSize.Value)
        da.SelectCommand = com
        da.Fill(dt)


        Return dt
    End Function


    Public Function POHeaderDETAILS(ByVal PurchaseNumber As String) As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim LocationName As String = ""
        Dim dt As New DataTable

        'From [Enterprise].[dbo].[PurchaseDetail]
        'Order By [PurchaseLineNumber] DESC

        ssql = ssql & " SELECT  *  "
        ssql = ssql & " FROM PurchaseHeader Where CompanyID ='" & Me.CompanyID & "' AND DivisionID ='" & Me.DivisionID & "'  AND DepartmentID ='" & Me.DepartmentID & "' "
        ssql = ssql & " AND [PurchaseNumber] ='" & PurchaseNumber & "'   "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        da.SelectCommand = com
        da.Fill(dt)


        Return dt
    End Function

    Private Sub OrderHeaderGrid_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles OrderHeaderGrid.RowUpdating

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")
        Dim PurchaseOrderNumber As String = OrderHeaderGrid.DataKeys(e.RowIndex).Value.ToString()
        'Response.Redirect("~/ReceivePO.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&EmployeeID=" & EmployeeID & "&PurchaseOrderNumber=" & PurchaseOrderNumber)
        '?CompanyID=Greene+And+Greene&DivisionID=DEFAULT&DepartmentID=DEFAULT&EmployeeID=Admin
        UpdatePurcahseHeaderAllReceivedStatus(PurchaseOrderNumber, "123456")
        Response.Redirect("~/InventoryTransferLists.aspx")
    End Sub



    Public Function UpdatePurcahseHeaderAllReceivedStatus(ByVal PurchaseNumber As String, ByVal ReceivingNumber As String) As Boolean

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[UpdatePurcahseHeaderAllReceivedToTransfer]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", Me.CompanyID)
                Command.Parameters.AddWithValue("DivisionID", Me.DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", Me.DepartmentID)
                Command.Parameters.AddWithValue("PurchaseNumber", PurchaseNumber)
                Command.Parameters.AddWithValue("ReceivedDate", Date.Now)
                Command.Parameters.AddWithValue("ReceivingNumber", ReceivingNumber)
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

    Private Sub search_Click(sender As Object, e As EventArgs) Handles search.Click
        BindOrderHeaderList()
    End Sub



    Private Sub drpProductFamily_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drpProductFamily.SelectedIndexChanged
        'Debug.Text = Debug.Text & "Family-Change "

        Dim ds As New DataSet
        ds = GetItemCategories(drpProductFamily.SelectedValue)
        'Debug.Text = Debug.Text & "Cat-Fill "

        ctl00_ContentPlaceHolder_drpProductCategory.DataSource = ds
        ctl00_ContentPlaceHolder_drpProductCategory.DataTextField = "CategoryName"
        ctl00_ContentPlaceHolder_drpProductCategory.DataValueField = "ItemCategoryID"
        ctl00_ContentPlaceHolder_drpProductCategory.DataBind()
        Dim lst As New ListItem
        lst.Text = "--Select--"
        lst.Value = ""
        ctl00_ContentPlaceHolder_drpProductCategory.Items.Insert(0, lst)

        txtitemsearch.Text = ""
        BindOrderHeaderList()
    End Sub

    Private Sub ctl00_ContentPlaceHolder_drpProductCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ctl00_ContentPlaceHolder_drpProductCategory.SelectedIndexChanged
        Dim dtItemGroup As New DataTable
        'Debug.Text = Debug.Text & "GRp-Fill "
        dtItemGroup = FillItemGroup(drpProductFamily.SelectedValue, ctl00_ContentPlaceHolder_drpProductCategory.SelectedValue)
        'drpItemGroup
        ' ,[ItemGroupID]
        ' ,[GroupName]
        drpGrp.DataSource = dtItemGroup
        drpGrp.DataTextField = "GroupName"
        drpGrp.DataValueField = "ItemGroupID"
        drpGrp.DataBind()
        Dim lst1 As New ListItem
        lst1.Text = "--Select--"
        lst1.Value = ""
        drpGrp.Items.Insert(0, lst1)
        'Debug.Text = Debug.Text & "Cat-callBind"
        txtitemsearch.Text = ""

        BindOrderHeaderList()
    End Sub

    Private Sub drpGrp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drpGrp.SelectedIndexChanged
        txtitemsearch.Text = ""
        BindOrderHeaderList()

    End Sub

    Private Sub Search2_Load(sender As Object, e As EventArgs) Handles Search2.Load

    End Sub

    Private Sub Search2_Click(sender As Object, e As EventArgs) Handles Search2.Click
        BindOrderHeaderList()
    End Sub
End Class
