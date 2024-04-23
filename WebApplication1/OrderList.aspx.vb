Option Strict Off

Imports System.Data.SqlClient
Imports Microsoft.Office
Imports System.Data
Imports DAL
Imports EnterpriseCommon.Configuration
Imports System.Net.Mail

Partial Class OrderList
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


            If OrderStatus = "Not Booked" Or OrderStatus = "Booked" Or OrderStatus = "Routed" Or OrderStatus = "Shipped" Or OrderStatus = "Picked" Or OrderStatus = "Delivered" Then



                Return True
            Else
                Return False
            End If




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

        If Me.CompanyID = "CamelbackFlowershop85018" Then
            disp = "block"
        End If

        rs = PopulateEmployees(CompanyID, DepartmentID, DivisionID, "PO")

        Dim securitycheck As Boolean = False

        While (rs.Read())

            If rs("EmployeeID").ToString() = EmployeeID Then
                securitycheck = True
                Exit While
            End If

        End While
        rs.Close()

        If securitycheck = False And CompanyID <> "PhillipsFlowersGifts60559" Then
            Response.Redirect("SecurityAcessPermission.aspx?MOD=PO")
        End If

        If CompanyID.ToUpper = "JWF" Or CompanyID.ToUpper = "NEWWF" Then
            Response.Redirect("OrderListWH.aspx")
        End If

        Dim AllowRefundFunction As Boolean
            AllowRefundFunction = CheckAllowRefnudFunction(CompanyID, DivisionID, DepartmentID)
            If Not AllowRefundFunction Then
                OrderHeaderGrid.Columns(1).Visible = False
            End If

            If Not Page.IsPostBack Then
            txtDateFrom.Text = DateTime.Now.ToString("MM/dd/yyyy")

            If CompanyID.ToLower() = "FarmDirect".ToLower() Or CompanyID.ToLower() = "FarmDirectTS".ToLower() Then
                drpPurchaseTransaction.SelectedValue = "POF"
            End If

            Dim dtfamily As New DataTable
            dtfamily = Fillfamily()

            drpItemFamilyID1.DataSource = dtfamily
            drpItemFamilyID1.DataTextField = "FamilyName"
            drpItemFamilyID1.DataValueField = "ItemFamilyID"
            drpItemFamilyID1.DataBind()

            Dim obj As New clsItems
            obj.CompanyID = CompanyID
            obj.DivisionID = DivisionID
            obj.DepartmentID = DepartmentID
            Dim ds As New DataSet
            ds = obj.GetItemCategories(drpItemFamilyID1.SelectedValue)

            drpItemCategoryID1.DataSource = ds
            drpItemCategoryID1.DataTextField = "CategoryName"
            drpItemCategoryID1.DataValueField = "ItemCategoryID"
            drpItemCategoryID1.DataBind()
            Dim lst As New ListItem
            lst.Text = "--Select--"
            lst.Value = ""
            drpItemCategoryID1.Items.Insert(0, lst)

            Dim PopOrderType As New CustomOrder()
            rs = PopulateEmployees(CompanyID, DepartmentID, DivisionID, "PO")

            emp.DataTextField = "EmployeeName"
            emp.DataValueField = "EmployeeID"
            emp.DataSource = rs
            emp.DataBind()
            Dim lst1 As New ListItem
            lst1.Text = "--Select--"
            lst1.Value = ""
            emp.Items.Insert(0, lst1)
            ' emp.SelectedValue = EmployeeID ' Session("EmployeeUserName")
            rs.Close()


            txtDateTo.Text = DateTime.Now.ToString("MM/dd/yyyy")
            SetLocationIDdropdown()
            BindPaymentandDeliveryList()
            SetFillVendorn()
            BindOrderHeaderList()
            End If

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

    Protected Sub cmblocationid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmblocationid.SelectedIndexChanged, drpstatus.SelectedIndexChanged
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

    Public disp As String = "none"
    'block
    Public Function POSOrderSearchList(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal Condition As String, ByVal fieldName As String, ByVal fieldexpression As String, ByVal FromDate As String, ByVal ToDate As String, ByVal AllDate As Integer, ByVal SortField As String, ByVal SortDirection As String, ByVal Payment As String, ByVal Delivery As String) As DataSet

        Dim conString As New SqlConnection
        conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

        Dim SQL As String = "POSPurchaseOrderSearchList"
        If Me.CompanyID = "QuickfloraDemo" Or Me.CompanyID = "DierbergsMarkets,Inc63017" Then
            SQL = "POSPurchaseOrderSearchListDB"
            If rdShipDates.Checked Then
                AllDate = 4
            End If
        Else
            rdShipDates.Visible = False
        End If

        If Me.CompanyID = "CamelbackFlowershop85018" Or True Then
            SQL = "POSPurchaseOrderSearchListWithStatus"
            disp = "block"
        End If

        If Me.CompanyID = "FarmDirect" Or True Then
            SQL = "POSPurchaseOrderSearchListWithStatus"

        End If
        Dim myCommand As New SqlCommand(SQL, conString)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        Dim prPayment As New SqlParameter("@Payment", Data.SqlDbType.NVarChar)
        prPayment.Value = Payment
        myCommand.Parameters.Add(prPayment)

        '@TransType

        Dim prItemFamily As New SqlParameter("@ItemFamily", Data.SqlDbType.NVarChar)
        prItemFamily.Value = drpItemFamilyID1.SelectedValue
        myCommand.Parameters.Add(prItemFamily)

        Dim prItemCategory As New SqlParameter("@ItemCategory", Data.SqlDbType.NVarChar)
        prItemCategory.Value = drpItemCategoryID1.SelectedValue
        myCommand.Parameters.Add(prItemCategory)

        Dim premp As New SqlParameter("@employeeID", Data.SqlDbType.NVarChar)
        premp.Value = emp.SelectedValue
        myCommand.Parameters.Add(premp)

        Dim prTransType As New SqlParameter("@TransType", Data.SqlDbType.NVarChar)
        prTransType.Value = drpPurchaseTransaction.SelectedValue
        myCommand.Parameters.Add(prTransType)

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


        If Me.CompanyID = "CamelbackFlowershop85018" Or True Then
            Dim pOrderStatus As New SqlParameter("@OrderStatus", Data.SqlDbType.NVarChar)
            pOrderStatus.Value = drpstatus.SelectedValue
            myCommand.Parameters.Add(pOrderStatus)
            '@VendorID
            Dim pVendorID As New SqlParameter("@VendorID", Data.SqlDbType.NVarChar)
            pVendorID.Value = drpvendor.SelectedValue
            myCommand.Parameters.Add(pVendorID)
        End If


        If cmblocationid.SelectedValue <> "" Then myCommand.Parameters.AddWithValue("@LocationID", cmblocationid.SelectedValue)


        Dim adapter As New SqlDataAdapter(myCommand)
        Dim ds As New DataSet()
        Try

        Catch ex As Exception

        End Try
        adapter.Fill(ds)
        conString.Close()

        Return ds


    End Function


        Protected Sub OrderHeaderGrid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles OrderHeaderGrid.PageIndexChanging
            OrderHeaderGrid.PageIndex = e.NewPageIndex
            BindOrderHeaderList()
        End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click, btnsearch1.Click
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

         

         

        Response.Redirect("~/PO.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&EmployeeID=" & EmployeeID & "&PurchaseOrderNumber=" & PurchaseOrderNumber)
            '?CompanyID=Greene+And+Greene&DivisionID=DEFAULT&DepartmentID=DEFAULT&EmployeeID=Admin

        End Sub

      
    Protected Sub OrderHeaderGrid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles OrderHeaderGrid.RowCommand

        If e.CommandName = "POPrint" Then
            Dim ordernumber As String
            ordernumber = e.CommandArgument

            'Response.Redirect("~/PO.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&EmployeeID=" & EmployeeID & "&PurchaseOrderNumber=" & ordernumber)

            Dim term As String = "" '= Session("TerminalID")

            Try
                term = Session("TerminalID")
            Catch ex As Exception

            End Try
            Dim constr11 As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim connec As New SqlConnection(constr11)
            Dim qry As String
            qry = "insert into [POPrintRequest]( CompanyID, DivisionID, DepartmentID, [TerminalName]" _
            & " , [PrintText], [Active],[TimeStamp]) values(@f0,@f1,@f2,@f3,@f4,@f5,GetDate())"
            Dim com As SqlCommand
            com = New SqlCommand(qry, connec)


            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = term
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = ordernumber
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.Bit)).Value = True
            Try
                com.Connection.Open()
                com.ExecuteNonQuery()
                com.Connection.Close()

            Catch ex As Exception
                Dim msg As String
                msg = ex.Message
                HttpContext.Current.Response.Write(msg)

            End Try

        End If
 



        If e.CommandName = "CancelOrder" Then
            Dim ordernumber As String
            ordernumber = e.CommandArgument


            EmailNotifications(ordernumber)
            Dim StrBody As New StringBuilder()

            StrBody.Append("<table border='0' cellspacing='0' cellpadding='0'  width='80%'   id='table1'>")
            StrBody.Append("<tr    align='left'>")
            StrBody.Append("<td  align='left' >&nbsp;<b>This PO#" & ordernumber & "</b>&nbsp; is canceled now.</td>")
            StrBody.Append("<td  align='left' >&nbsp;<b>" & txtEmailContent.Text & "</b>&nbsp;</td>")

            StrBody.Append("</tr>")
            StrBody.Append("</table>")
            StrBody.Append("<hr>")

            EmailSendingWithhCC("[Canceled] " & txtemailsubject.Text, StrBody.ToString() & divEmailContent.InnerHtml, txtfrom.Text, txtto.Text, txtcc.Text)




            UpdateCancelOrderNumber(ordernumber)
            CancelPurchaseItemDetailsJsGrid(ordernumber)
            BindOrderHeaderList()
        End If


    End Sub



    Dim txtemailsubject As New TextBox
    Dim txtfrom As New TextBox
    Dim txtto As New TextBox
    Dim txtEmailContent As New TextBox
    Dim txtcc As New TextBox
    Dim txtcc2 As New TextBox
    Dim txtvendorid As New TextBox




    Public Function PopulateEmailContent(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As SqlDataReader



        Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        ConString.Open()

        Dim myCommand As New SqlCommand("PopulateEmailContent2", ConString)
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



        Dim rs As SqlDataReader
        rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
        Return rs


    End Function




    Public Sub EmailNotifications(ByVal OrdNumber As String)



        Dim PostOrder As New DAL.CustomOrder()
        Dim EmailType As String = "Order Placed"
        Dim EmailContent As String = ""
        Dim EmailSubject As String = ""
        Dim EmailAllowed As Boolean = True

        Dim rs As SqlDataReader
        rs = PopulateEmailContent(CompanyID, DivisionID, DepartmentID)
        If rs.HasRows = True Then

            While rs.Read

                EmailType = rs("EmailType").ToString()
                EmailContent = rs("EmailContent").ToString()
                EmailSubject = rs("EmailSubject").ToString()
                'EmailContent = EmailContent & "<br><br><p class='MsoNormal' style='margin: 0in 0in 0pt' align='justify'><font face='Verdana' size='2'>Powered by Sunflower Technologies, Inc 323.735.7272</font></p>"
                EmailAllowed = rs("EmailAllowed").ToString()
                If EmailType <> "Order Cancel Notification to Vendor" Then
                    'txtemailsubject.Text = txtemailsubject.Text & EmailType & "--"
                    Continue While
                End If

                If EmailAllowed Then
                    PopulateEmailForVendorsForItems("Order Cancel Notification to Vendor", EmailContent, EmailSubject, OrdNumber)

                End If



            End While

        End If

        rs.Close()

    End Sub


    Public Function PurchaseOrderNumber_Details(ByVal PO As String) As DataTable



        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        Dim dt As New DataTable

        qry = "select  *  from [PurchaseHeader]   where [PurchaseHeader].PurchaseNumber =@f31 and CompanyID = @f0 AND DivisionID = @f1 AND DepartmentID = @f2 "
        'Values (@f00,@f0,@f1,@f2,@f3,@f31,@f32,@f311,@f322)
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)

        com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
        com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
        com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
        com.Parameters.Add(New SqlParameter("@f31", SqlDbType.NVarChar, 36)).Value = PO.Trim()


        Dim da As New SqlDataAdapter(com)

        da.Fill(dt)



        Return dt
        Try
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)

        End Try

        Return dt

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

    Public Function GetPurchaseDetail_ItemID_name(ByVal ItemID As String) As String

        Dim name As String = 0

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "select  ItemName  from [InventoryItems]   where [InventoryItems].ItemID =@f31 and CompanyID = @f0 AND DivisionID = @f1 AND DepartmentID = @f2 "
        'Values (@f00,@f0,@f1,@f2,@f3,@f31,@f32,@f311,@f322)
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f31", SqlDbType.NVarChar, 36)).Value = ItemID.Trim()

            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(com)

            da.Fill(dt)


            If (dt.Rows.Count <> 0) Then

                Try
                    name = dt.Rows(0)(0)
                Catch ex As Exception
                    Dim msg As String
                    msg = ex.Message
                    'HttpContext.Current.Response.Write(msg)
                End Try

            End If

            Return name

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)

        End Try

        Return name

    End Function


    '--New Function For--'

    Public Sub PopulateEmailForVendorsForItems(ByVal EmailType As String, ByVal EmailContent As String, ByVal EmailSubject As String, ByVal PurchaseOrderNumber As String)

        Dim dt As New DataTable

        dt = PurchaseOrderNumber_Details(PurchaseOrderNumber)

        txtemailsubject.Text = "In PO count " & dt.Rows.Count

        If (dt.Rows.Count <> 0) Then


            Dim strvendor As String = ""

            If dt.Rows.Count <> 0 Then
                Try
                    strvendor = dt.Rows(0)("VendorID")
                Catch ex As Exception
                    strvendor = ""
                End Try
            End If




            If strvendor <> "" Then

                Dim dtvendor As DataTable
                dtvendor = FillDetailsVendor(strvendor)
                Dim vendoremail As String
                vendoremail = ""

                txtemailsubject.Text = "In PO dtvendor.Rows.Count " & dtvendor.Rows.Count

                If dtvendor.Rows.Count <> 0 Then



                    Try
                        vendoremail = dtvendor.Rows(0)("VendorEmail")
                    Catch ex As Exception
                        'vendoremail = "imtiyazsir@gmail.com"
                    End Try

                    If vendoremail = "" Then
                        'vendoremail = "imtiyazsir@gmail.com"
                    End If

                    txtemailsubject.Text = "In PO vendoremail " & vendoremail


                    PopulateEmailForVendors(EmailType, EmailContent, EmailSubject, PurchaseOrderNumber, vendoremail)


                End If


            End If




        End If


    End Sub


    Public Sub PopulateEmailForVendors(ByVal EmailType As String, ByVal EmailContent As String, ByVal EmailSubject As String, ByVal PurchaseOrderNumber As String, ByVal vendoremail As String)



        Dim podate As String = ""

        Dim itemID As String = ""


        Dim ShippingName As String = ""
        Dim ShippingAddress1 As String = ""

        Dim ShipCity As String = ""
        Dim ShipState As String = ""
        Dim ShipZip As String = ""
        Dim ShippingPhone As String = ""
        Dim ShippingCell As String = ""
        Dim DeliveryMethod As String = ""

        Dim Vendorid As String = ""
        Dim PurchaseDateRequested As New DateTime

        Dim dtPO As New DataTable

        dtPO = PurchaseOrderNumber_Details(PurchaseOrderNumber)

        If (dtPO.Rows.Count <> 0) Then

            Try
                ShippingName = dtPO.Rows(0)("ShippingName")
            Catch ex As Exception

            End Try

            Try
                ShippingAddress1 = dtPO.Rows(0)("ShippingAddress1")
                ShippingAddress1 = ShippingAddress1.Replace("Flower Market ", "")
            Catch ex As Exception

            End Try

            Try
                ShipCity = dtPO.Rows(0)("ShippingCity")
            Catch ex As Exception

            End Try


            Try
                ShipZip = dtPO.Rows(0)("ShippingZip")
            Catch ex As Exception

            End Try

            Try
                ShipState = dtPO.Rows(0)("ShippingState")
            Catch ex As Exception

            End Try

            ShippingPhone = ""
            ShippingCell = ""

            Try
                podate = dtPO.Rows(0)("PurchaseDate")
            Catch ex As Exception

            End Try

            Try
                DeliveryMethod = dtPO.Rows(0)("ShipMethodID")
            Catch ex As Exception

            End Try


            Vendorid = dtPO.Rows(0)("VendorID").ToString()
            txtvendorid.Text = Vendorid

            Try
                PurchaseDateRequested = dtPO.Rows(0)("PurchaseDateRequested").ToString()
            Catch ex As Exception

            End Try
        End If

        '------------------------------------------------------------------------------------------------------------'


        Dim StrBody As New StringBuilder()
        StrBody.Append("<table border='1' cellspacing='0' cellpadding='0' width='100%' id='table1'>")

        StrBody.Append("<tr    align='center'>")
        StrBody.Append("<td align='center' > <b>Item ID</b> </td>")

        StrBody.Append("<td> <b>Item Details</b> </td>")


        '' StrBody.Append("<td align='center' ><b> Color </b></td>")

        StrBody.Append("<td align='center' ><b> Qty </b></td>")
        StrBody.Append("<td align='center' ><b> UOM </b></td>")
        If CompanyID.ToLower <> "adarawholesale" Then
            StrBody.Append("<td align='center' ><b> Pack </b></td>")
            StrBody.Append("<td align='center' ><b> Units </b></td>")
        End If

        StrBody.Append("<td align='center' ><b> Price </b></td>")
        StrBody.Append("<td align='center' ><b> Total </b></td>")

        If CompanyID.ToLower <> "adarawholesale" Then
            StrBody.Append("<td align='center' ><b> Buyer </b></td>")
        End If


        StrBody.Append("</tr>")

        Dim FillItemDetailGrid As New CustomOrder()
        Dim ds As New Data.DataSet
        ds = GetPurchaseDetail_list(PurchaseOrderNumber)



        Dim tr As String = ""
        Dim n As Integer = 0
        For n = 0 To ds.Tables(0).Rows.Count - 1


            Dim inline As String = "0"
            Try
                inline = ds.Tables(0).Rows(n)("Request_InLineNumber")
            Catch ex As Exception

            End Try


            Dim stradd As String = ""
            stradd = GetPO_Requisition_Details(inline)

            tr = tr & "<tr><td align='center'>" & ds.Tables(0).Rows(n)("ItemID") & "</td>"
            If CompanyID.ToLower = "adarawholesale" Then
                tr = tr & "<td align='left'> "
                tr = tr & "" & ds.Tables(0).Rows(n)("Description") & "<br>"
                tr = tr & "</td>"
            Else
                tr = tr & "<td align='left'>&nbsp;<b>Name</b>: " & GetPurchaseDetail_ItemID_name(ds.Tables(0).Rows(n)("ItemID")) & "<br>"
                tr = tr & "&nbsp;<b>Vendor Remarks</b>: " & ds.Tables(0).Rows(n)("Description") & "<br>"
                tr = tr & "&nbsp;<b> Color/Variety</b>: " & ds.Tables(0).Rows(n)("Color") & "<br>"
                '' tr = tr & "&nbsp;<b>Comments</b>: " & ds.Tables(0).Rows(n)("DetailMemo1") & "<br>"
                tr = tr & "&nbsp;<b>Location</b>: " & ds.Tables(0).Rows(n)("LocationID") & "<br>"
                tr = tr & "&nbsp;" & stradd & "<br>"
                tr = tr & "</td>"
            End If


            '' tr = tr & "<td align='center' > " & ds.Tables(0).Rows(n)("Color") & "</td>"
            tr = tr & "<td align='center'> " & ds.Tables(0).Rows(n)("VendorQTY") & "</td>"
            tr = tr & "<td align='center'> " & ds.Tables(0).Rows(n)("ItemUOM") & "</td>"
            If CompanyID.ToLower <> "adarawholesale" Then
                tr = tr & "<td align='center'> " & ds.Tables(0).Rows(n)("VendorPacksize") & "</td>"
                tr = tr & "<td align='center'> " & ds.Tables(0).Rows(n)("OrderQty") & "</td>"
            End If

            tr = tr & "<td align='center'> $" & Format(ds.Tables(0).Rows(n)("ItemUnitPrice"), "0.00") & "</td>"
            tr = tr & "<td align='center'> $" & Format(ds.Tables(0).Rows(n)("Total"), "0.00") & "</td>"
            If CompanyID.ToLower <> "adarawholesale" Then
                tr = tr & "<td align='center'> " & ds.Tables(0).Rows(n)("Buyer") & "</td>"
            End If

            tr = tr & "<tr>"

        Next

        StrBody.Append(tr)

        StrBody.Append("</table>")




        Dim ItemDetails As String = StrBody.ToString()



        '--------------------------------------------------------------------------------------------------------------'
        EmailSubject = EmailSubject.Replace("~Vendorid~", Vendorid)
        EmailSubject = EmailSubject.Replace("~ponumber~", PurchaseOrderNumber)


        ''EmailContent = EmailContent.Replace("~RetailerDate~", RetailerDate)
        ''EmailContent = EmailContent.Replace("~RetailerTime~", RetailerTime)

        ''EmailContent = EmailContent.Replace("~ship date~", ShipDate)
        ''EmailContent = EmailContent.Replace("~Occasion code~", OccasionCode)
        'EmailContent = EmailContent.Replace("~Special instructions~", SpecialInstructions)
        EmailContent = EmailContent.Replace("~city~", ShipCity)
        EmailContent = EmailContent.Replace("~shippingstate~", ShipState)
        EmailContent = EmailContent.Replace("~zip~", ShipZip)
        'EmailContent = EmailContent.Replace("~shippingcountry~", ShipCountry)
        EmailContent = EmailContent.Replace("~ponumber~", PurchaseOrderNumber)
        EmailContent = EmailContent.Replace("~ship to address 1~", ShippingAddress1)
        'EmailContent = EmailContent.Replace("~ship to address 2~", ShippingAddress2)
        'EmailContent = EmailContent.Replace("~ship to address 3~", ShippingAddress3)
        EmailContent = EmailContent.Replace("~Req.Date~", PurchaseDateRequested.ToShortDateString())

        EmailContent = EmailContent.Replace("~podate~", podate)
        'EmailContent = EmailContent.Replace("~company phone~", CompanyPhone)

        'EmailContent = EmailContent.Replace("~salutation~", Salutation)
        'EmailContent = EmailContent.Replace("~customername~", name)
        'EmailContent = EmailContent.Replace("~payment method~", Paymentmethod)
        'EmailContent = EmailContent.Replace("~Total~", Total)


        EmailContent = EmailContent.Replace("~Delivery method~", DeliveryMethod)
        'EmailContent = EmailContent.Replace("~Destination type~", DestinationType)
        'EmailContent = EmailContent.Replace("~payment method~", Paymentmethod)
        EmailContent = EmailContent.Replace("~item details~", ItemDetails)
        'EmailContent = EmailContent.Replace("~ship to salutation~", shipsalutation)
        ' EmailContent = EmailContent.Replace("~shippingcustomername~", "McCarthy Group Florists")

        'New Changes Starts here
        'EmailContent = EmailContent.Replace("~CompanyFax~", CompanyFax)
        'EmailContent = EmailContent.Replace("~CustomerAddress1~", CustomerAddress1)
        'EmailContent = EmailContent.Replace("~CustomerAddress2~", CustomerAddress2)
        'EmailContent = EmailContent.Replace("~CustomerAddress3~", CustomerAddress3)
        'EmailContent = EmailContent.Replace("~CustomerCity~", CustomerCity)
        'EmailContent = EmailContent.Replace("~CustomerState~", CustomerState)
        'EmailContent = EmailContent.Replace("~CustomerZip~", CustomerZip)
        'EmailContent = EmailContent.Replace("~CustomerCountry~", CustomerCountry)
        'EmailContent = EmailContent.Replace("~CustomerPhone~", CustomerPhone)
        'EmailContent = EmailContent.Replace("~CustomerCell~", CustomerCell)
        'EmailContent = EmailContent.Replace("~CustomerEmail~", CustomerEmail)
        'EmailContent = EmailContent.Replace("~CardMessage~", CardMessage)

        EmailContent = EmailContent.Replace("~ShippingPhone~", ShippingPhone)
        EmailContent = EmailContent.Replace("~ShippingCell~", ShippingCell)

        'EmailContent = EmailContent.Replace("~link~", "<a  target='_blank'  href='https://reports.quickflora.com/reports/scripts/POReport.aspx?CompanyID=FieldOfFlowersTraining&DivisionID=DEFAULT&DepartmentID=DEFAULT&PurchaseNumber=" & PurchaseOrderNumber & "' >Click to Open</a>")
        ' EmailContent = EmailContent.Replace("~link~", "<a  target='_blank'  href='https://reports.quickflora.com/reports/scripts/POReport.aspx?CompanyID=" & Me.CompanyID & "&DivisionID=" & Me.DivisionID & "&DepartmentID=" & Me.DepartmentID & "&PurchaseNumber=" & PurchaseOrderNumber & "' >Click to Open</a>")


        'IPAddress = Request.ServerVariables("REMOTE_ADDR")
        'EmailContent = EmailContent.Replace("~IpAddress~", IPAddress)
        'EmailContent = EmailContent.Replace("~WebsiteAddress~", "<a href='" & WebsiteAddress & "'>" & WebsiteAddress & "</a>")
        'EmailContent = EmailContent.Replace("~Ship to Attention~", ShipAttention)
        'EmailContent = EmailContent.Replace("~Ship to Company~", ShipCompany)

        Dim OrderPlacedSubject As String
        Dim OrderPlacedContent As String




        OrderPlacedSubject = EmailSubject
        OrderPlacedContent = EmailContent


        Dim CompanyEmail As String = "support@quickflora.com"

        Dim ToAddress As String = vendoremail
        Dim FromAddress As String = CompanyEmail


        OrderPlacedSubject = EmailSubject
        OrderPlacedContent = EmailContent

        Dim str_PurchaseOrderNumber As String = ""
        str_PurchaseOrderNumber = GetPO_PurchaseOrderNumber_Details(PurchaseOrderNumber)

        If str_PurchaseOrderNumber <> "" Then
            OrderPlacedSubject = OrderPlacedSubject & str_PurchaseOrderNumber
        End If

        txtfrom.Text = FromAddress
        txtto.Text = ToAddress
        txtcc.Text = ""
        txtemailsubject.Text = OrderPlacedSubject
        divEmailContent.InnerHtml = OrderPlacedContent
        'FCKeditor1.Value = OrderPlacedContent


        If vendoremail <> "" And CompanyEmail <> "" And OrderPlacedContent <> "" Then

            ' EmailSending(OrderPlacedSubject, OrderPlacedContent, FromAddress, ToAddress)

        End If


    End Sub



    Public Function GetPO_PurchaseOrderNumber_Details(ByVal PO As String) As String

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()

        ssql = ssql & "  Select [PurchaseNumber] ,[PO_Requisition_Details].[ShipDate] ,[PO_Requisition_Details].[Location] "
        ssql = ssql & " From [Enterprise].[dbo].[PurchaseDetail] Inner Join [PO_Requisition_Details] On [PurchaseDetail].Request_InLineNumber = [PO_Requisition_Details].[InLineNumber] "
        ssql = ssql & " Where [PurchaseNumber] = @PurchaseNumber  AND [PurchaseDetail].CompanyID = '" & Me.CompanyID & "' AND [PurchaseDetail].DivisionID  = '" & Me.DivisionID & "' AND [PurchaseDetail].DepartmentID  = '" & Me.DepartmentID & "'  "
        ssql = ssql & " Group by [PurchaseNumber] ,[PO_Requisition_Details].[ShipDate] ,[PO_Requisition_Details].[Location] "



        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        com.Parameters.Add(New SqlParameter("@PurchaseNumber", SqlDbType.NVarChar)).Value = PO
        da.SelectCommand = com
        da.Fill(dt)

        ' Response.Write(dt.Rows.Count)

        Dim rtstr As String = ""

        If dt.Rows.Count = 1 Then
            rtstr = rtstr & " Shipping on: " & dt.Rows(0)("ShipDate") & "  Ship to: " & dt.Rows(0)("Location")
        End If

        Try
        Catch ex As Exception

        End Try

        Return rtstr

    End Function


    Public Function GetPO_Requisition_Details(ByVal InLineNumber As String) As String
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ''ssql = "SELECT * FROM [PO_Requisition_Details] where  InLineNumber = @InLineNumber and CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 order by InLineNumber DESC "

        ssql = ssql & " SELECT  PO_Requisition_Details.TransmissionBy,PO_Requisition_Details.OrderNo,PO_Requisition_Header.ShipDate, PO_Requisition_Header.ArriveDate, PO_Requisition_Header.ShipMethodID  "
        ssql = ssql & "      FROM [Enterprise].[dbo].[PO_Requisition_Details] Left Outer Join [Enterprise].[dbo].[PO_Requisition_Header] "
        ssql = ssql & "      ON [Enterprise].[dbo].[PO_Requisition_Details].[CompanyID] = [Enterprise].[dbo].[PO_Requisition_Header].[CompanyID] AND "
        ssql = ssql & "      [Enterprise].[dbo].[PO_Requisition_Details].[DivisionID] =[Enterprise].[dbo].[PO_Requisition_Header].[DivisionID] And "
        ssql = ssql & "      [Enterprise].[dbo].[PO_Requisition_Details].[DepartmentID] = [Enterprise].[dbo].[PO_Requisition_Header].[DepartmentID] AND "
        ssql = ssql & "      [Enterprise].[dbo].[PO_Requisition_Details].[OrderNo] = [Enterprise].[dbo].[PO_Requisition_Header].[OrderNo] "
        ssql = ssql & "  Where   [Enterprise].[dbo].[PO_Requisition_Details].InLineNumber=@InLineNumber AND  [Enterprise].[dbo].[PO_Requisition_Details].CompanyID ='" & Me.CompanyID & "' AND [Enterprise].[dbo].[PO_Requisition_Details].DivisionID ='" & Me.DivisionID & "'  AND [Enterprise].[dbo].[PO_Requisition_Details].DepartmentID ='" & Me.DepartmentID & "' "


        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        com.Parameters.Add(New SqlParameter("@InLineNumber", SqlDbType.BigInt)).Value = InLineNumber
        da.SelectCommand = com
        da.Fill(dt)

        '        Response.Write(dt.Rows.Count)

        Dim rtstr As String = ""

        If dt.Rows.Count <> 0 Then

            rtstr = rtstr & "<b>Ship Date</b>:" & dt.Rows(0)("ShipDate")
            Dim ShipMethodID As String = ""
            Dim ShipMethodDesc As String = ""
            Try
                ShipMethodID = dt.Rows(0)("ShipMethodID")
                If ShipMethodID <> "" Then
                    ShipMethodDesc = SetShipMethodDescription(ShipMethodID)
                End If

            Catch ex As Exception

            End Try
            If ShipMethodDesc <> "" Then
                rtstr = rtstr & "<br><b>Ship Method</b>:" & ShipMethodDesc
            End If

            Try
                If dt.Rows(0)("TransmissionBy") <> "" Then
                    rtstr = rtstr & "<br><b>Transmission Method</b>:" & dt.Rows(0)("TransmissionBy")
                End If
            Catch ex As Exception

            End Try


        End If
        Try
        Catch ex As Exception

        End Try

        Return rtstr

    End Function




    Public Function SetShipMethodDescription(ByVal ShipMethodID As String) As String
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = " SElect   TruckingSchedule.ShipMethodID,TruckingSchedule.ShipMethodDescription     FROM TruckingSchedule  Where TruckingSchedule.ShipMethodID = @ShipMethodID  "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try
            com.Parameters.Add(New SqlParameter("@ShipMethodID", SqlDbType.NVarChar)).Value = ShipMethodID

            da.SelectCommand = com
            da.Fill(dt)

        Catch ex As Exception

        End Try


        Dim ShipMethodDescription As String = ""
        If dt.Rows.Count <> 0 Then
            ShipMethodDescription = dt.Rows(0)("ShipMethodDescription")
        End If

        Return ShipMethodDescription
    End Function



    Public Sub EmailSendingWithhCC(ByVal OrderPlacedSubject As String, ByVal OrderPlacedContent As String, ByVal FromAddress As String, ByVal ToAddress As String, ByVal CCAddress As String)
        'Exit Sub
        Dim mMailMessage As New MailMessage()
        Try

            ' Set the sender address of the mail message
            mMailMessage.From = New MailAddress(FromAddress)
            ' Set the recepient address of the mail message
            mMailMessage.To.Add(New MailAddress(ToAddress))

            If CCAddress.Trim <> "" Then
                mMailMessage.CC.Add(New MailAddress(CCAddress))
            End If


            If txtcc2.Text.Trim <> "" Then
                mMailMessage.CC.Add(New MailAddress(txtcc2.Text.Trim))
            End If

            'mMailMessage.Bcc.Add(New MailAddress("qfclientorders@sunflowertechnologies.com"))
            'Set the subject of the mail message
            mMailMessage.Subject = OrderPlacedSubject.ToString()
            ' Set the body of the mail message
            mMailMessage.Body = OrderPlacedContent.ToString()

            ' Set the format of the mail message body as HTML
            mMailMessage.IsBodyHtml = True


            ' Set the priority of the mail message to normal
            mMailMessage.Priority = MailPriority.Normal

            ' Instantiate a new instance of SmtpClient
            Dim smtp As New System.Net.Mail.SmtpClient()
            smtp.Host = ConfigurationManager.AppSettings("SystemSMTPServer")

            Try
                'smtp.Send(mMailMessage)
                newmailsending(mMailMessage)

            Catch ex As Exception

            End Try

        Catch ex As Exception

        End Try

    End Sub


    Public Sub newmailsending(ByVal Email As MailMessage)

        Dim QFmail As New com.quickflora.qfscheduler.QFPrintService
        QFmail.newmailsending(Email.From.ToString, Email.To.ToString, Email.CC.ToString, "", Email.Subject.ToString, Email.Body.ToString, CompanyID, DivisionID, DepartmentID)

        Exit Sub

    End Sub


    ''[CancelPurchaseItemDetailsJsGrid]

    Public Function CancelPurchaseItemDetailsJsGrid(ByVal PurchaseNumber As String) As Integer


        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand("[dbo].[CancelPurchaseItemDetailsJsGrid]", myCon)
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

        Dim parameterOrderLineNumber As New SqlParameter("@PurchaseNumber", Data.SqlDbType.NVarChar)
        parameterOrderLineNumber.Value = PurchaseNumber
        myCommand.Parameters.Add(parameterOrderLineNumber)




        myCon.Open()

        myCommand.ExecuteNonQuery()

        myCon.Close()

        Return 0

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

    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    Public Function UpdateCancelOrderNumber(ByVal PurchaseNumber As String) As Boolean
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE PurchaseHeader set Canceled=1,Posted=0,CancelDate=@f6 Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And PurchaseNumber=@f5"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 36)).Value = PurchaseNumber
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.DateTime)).Value = Date.Now

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
        txtSearchExpression.Text = ""
        BindOrderHeaderList()
    End Sub

    Protected Sub Delivery_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Delivery.SelectedIndexChanged, drpvendor.SelectedIndexChanged

        txtSearchExpression.Text = ""
        BindOrderHeaderList()
    End Sub

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


    Private Sub OrderHeaderGrid_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles OrderHeaderGrid.RowDataBound

        Dim lblBuyer As New Label
        Dim lblOrderNumber As New Label

        lblBuyer.Text = ""
        If e.Row.RowType = DataControlRowType.Header Then
            If Me.CompanyID = "DierbergsMarkets,Inc63017" Or Me.CompanyID = "QuickfloraDemo" Then
                e.Row.Cells(8).Visible = False
                e.Row.Cells(9).Visible = True
                e.Row.Cells(10).Visible = True
            Else
                e.Row.Cells(8).Visible = True
                e.Row.Cells(9).Visible = False
                e.Row.Cells(10).Visible = False
            End If

            If Me.CompanyID = "FarmDirect" Or Me.CompanyID = "QuickfloraDemo" Or Me.CompanyID = "FarmDirectTS" Then
                e.Row.Cells(8).Text = "Departure Date"
            Else
                e.Row.Cells(19).Visible = False
                e.Row.Cells(8).Text = "Arrival Date"
            End If
        End If


        If e.Row.RowType = DataControlRowType.DataRow Then
            lblBuyer = e.Row.FindControl("lblBuyer")
            lblOrderNumber = e.Row.FindControl("lblOrderNumber")
            Dim dt2 As New DataTable
            dt2 = PODETAILSBYInLineNumber(lblOrderNumber.Text)
            If dt2.Rows.Count <> 0 Then

                Dim fn As Integer = 0

                For fn = 0 To dt2.Rows.Count - 1
                    Dim Buyer As String = ""
                    Try
                        Buyer = dt2.Rows(fn)(0)
                        If lblBuyer.Text = "" Then
                            lblBuyer.Text = Buyer
                        Else
                            '' lblBuyer.Text = lblBuyer.Text & "<br>" & Buyer
                        End If

                    Catch ex As Exception

                    End Try

                Next



            End If

            If Me.CompanyID = "DierbergsMarkets,Inc63017" Or Me.CompanyID = "QuickfloraDemo" Then
                e.Row.Cells(8).Visible = False
                e.Row.Cells(9).Visible = True
                e.Row.Cells(10).Visible = True
            Else
                e.Row.Cells(8).Visible = True
                e.Row.Cells(9).Visible = False
                e.Row.Cells(10).Visible = False
            End If

            If Me.CompanyID = "DierbergsMarkets,Inc63017" Or Me.CompanyID = "QuickfloraDemo" Then

                Dim lblShip As New Label
                lblShip = e.Row.FindControl("lblShip")
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
                    Try
                        lblShip.Text = dtPODETAILS.Rows(0)("ShipDate")
                    Catch ex As Exception
                        ' lblShip.Text = ex.Message
                    End Try
                End If
                lblArrival.Text = lblArrival.Text.Replace("12:00:00 AM", "")
                lblShip.Text = lblShip.Text.Replace("12:00:00 AM", "")
                lblShip.Text = lblShip.Text.Replace("Jan 1 1900 12:00AM", "")
                '
            End If

            If Me.CompanyID <> "FarmDirect" Then
                e.Row.Cells(19).Visible = False

            End If

            If Me.CompanyID <> "FarmDirectTS" Then
                e.Row.Cells(19).Visible = False

            End If

            Dim lblOrderStatus As New Label
            lblOrderStatus = e.Row.FindControl("lblOrderStatus")

            If lblOrderStatus.Text = "Canceled" Or lblOrderStatus.Text = "Not Booked" Then
                lblOrderStatus.ForeColor = Drawing.Color.Red
            End If


            Dim lblstatus As New Label
            lblstatus = e.Row.FindControl("lblstatus")

            Dim imgEdit As New ImageButton
            imgEdit = e.Row.FindControl("imgEdit")
            Dim imgCancel As New ImageButton
            imgCancel = e.Row.FindControl("imgCancel")

            Dim PartialReceived As Boolean = False
            Dim dt3 As New DataTable
            dt3 = POHeaderDETAILS(lblOrderNumber.Text)
            If dt3.Rows.Count <> 0 Then
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
                    PartialReceived = dt3.Rows(0)("PartialReceived")
                Catch ex As Exception
                End Try
                If canceled Then
                    lblstatus.Text = "Canceled"
                    lblstatus.ForeColor = Drawing.Color.Red
                    imgEdit.Visible = False
                    imgCancel.Visible = False
                End If


                If CompanyID = "DierbergsMarkets,Inc63017" Or CompanyID = "QuickfloraDemo" Then
                    If Received Or PartialReceived Then
                        lblstatus.Text = "Received"
                        lblstatus.ForeColor = Drawing.Color.Green
                        imgEdit.Visible = False
                        imgCancel.Visible = False
                    End If
                End If

            End If


        End If

    End Sub


    Public Function PODETAILSBYInLineNumber(ByVal PurchaseNumber As String) As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim LocationName As String = ""
        Dim dt As New DataTable

        'From [Enterprise].[dbo].[PurchaseDetail]
        'Order By [PurchaseLineNumber] DESC

        ssql = ssql & " SELECT  Buyer  "
        ssql = ssql & " FROM Enterprise.dbo.PurchaseDetail Where CompanyID ='" & Me.CompanyID & "' AND DivisionID ='" & Me.DivisionID & "'  AND DepartmentID ='" & Me.DepartmentID & "' "
        ssql = ssql & " AND [PurchaseNumber] ='" & PurchaseNumber & "'   "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        da.SelectCommand = com
        da.Fill(dt)


        Return dt
    End Function

    Private Sub drpPurchaseTransaction_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drpPurchaseTransaction.SelectedIndexChanged
        BindOrderHeaderList()
    End Sub

    Private Sub drpItemFamilyID1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drpItemFamilyID1.SelectedIndexChanged
        Dim obj As New clsItems
        obj.CompanyID = CompanyID
        obj.DivisionID = DivisionID
        obj.DepartmentID = DepartmentID
        Dim ds As New DataSet
        ds = obj.GetItemCategories(drpItemFamilyID1.SelectedValue)

        drpItemCategoryID1.DataSource = ds
        drpItemCategoryID1.DataTextField = "CategoryName"
        drpItemCategoryID1.DataValueField = "ItemCategoryID"
        drpItemCategoryID1.DataBind()
        Dim lst As New ListItem
        lst.Text = "--Select--"
        lst.Value = ""
        drpItemCategoryID1.Items.Insert(0, lst)
        BindOrderHeaderList()
    End Sub

    Private Sub drpItemCategoryID1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drpItemCategoryID1.SelectedIndexChanged
        BindOrderHeaderList()
    End Sub

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

    Private Sub emp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles emp.SelectedIndexChanged
        BindOrderHeaderList()

    End Sub
End Class
