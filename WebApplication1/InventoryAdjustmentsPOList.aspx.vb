Option Strict Off

Imports System.Data.SqlClient
Imports Microsoft.Office
Imports System.Data
Imports DAL
Imports EnterpriseCommon.Configuration


Partial Class InventoryAdjustmentsPOList
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




    Protected Sub OrderHeaderGrid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles OrderHeaderGrid.RowCommand

        If e.CommandName = "AIPrint" Then
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
            qry = "insert into [AdjustInventoryPrintRequest]( CompanyID, DivisionID, DepartmentID, [TerminalName]" _
            & " , [PrintText], [Active]) values(@f0,@f1,@f2,@f3,@f4,@f5)"
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Session("CompanyID") Is Nothing Then
            Response.Redirect("loginform.aspx")
        End If



        'Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        '' get the connection ready
        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")


        rs = PopulateEmployees(CompanyID, DepartmentID, DivisionID, "IA")

        Dim securitycheck As Boolean = False

        While (rs.Read())

            If rs("EmployeeID").ToString() = EmployeeID Then
                securitycheck = True
                Exit While
            End If

        End While
        rs.Close()

        If securitycheck = False Then
            Response.Redirect("SecurityAcessPermission.aspx?MOD=IA")
        End If

        If CompanyID.ToUpper() = "JWF" Then
            Response.Redirect("RO/MarketingAdjustDetailedLog.aspx")
        End If

        If Not Page.IsPostBack Then
            txtDateFrom.Text = DateTime.Now.ToString("MM/dd/yyyy")
            txtDateTo.Text = DateTime.Now.ToString("MM/dd/yyyy")
            SetLocationIDdropdown()
            BindPaymentandDeliveryList()
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
        If locationid <> "" Then
            cmblocationid.SelectedIndex = cmblocationid.Items.IndexOf(cmblocationid.Items.FindByValue(locationid))
        End If
        ' Session("OrderLocationid") = cmblocationid.SelectedValue
    End Sub


    Protected Sub chkhold_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkhold.CheckedChanged, checkreturn.CheckedChanged, chkcartorders.CheckedChanged, chkall.CheckedChanged
        BindOrderHeaderList()
    End Sub

    Protected Sub cmblocationid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmblocationid.SelectedIndexChanged
        BindOrderHeaderList()
    End Sub

 

    Public Sub BindPaymentandDeliveryList()
        Dim objUser As New DAL.CustomOrder()
        Dim dt As New DataTable
        dt = objUser.PaymentMethodsList(CompanyID, DepartmentID, DivisionID)

        Payment.DataTextField = "PaymentMethodID"
        Payment.DataValueField = "PaymentMethodID"
        Payment.DataSource = dt
        Payment.DataBind()

        Dim Paymentlist As ListItem = Payment.Items.FindByValue("Will Call")
        If Not Paymentlist Is Nothing Then
            'Payment.Items.FindByValue("Will Call").Selected = True
        End If



    End Sub

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

        Dim myCommand As New SqlCommand("InventoryAdjustmentHeaderSearchList", conString)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        Dim prPayment As New SqlParameter("@Payment", Data.SqlDbType.NVarChar)
        prPayment.Value = Payment
        myCommand.Parameters.Add(prPayment)

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




        If cmblocationid.SelectedValue <> "" Then myCommand.Parameters.AddWithValue("@LocationID", cmblocationid.SelectedValue)


        Dim adapter As New SqlDataAdapter(myCommand)
        Dim ds As New DataSet()
        adapter.Fill(ds)
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



        Dim InventoryAdjustmentsNumber As String = OrderHeaderGrid.DataKeys(e.NewEditIndex).Value.ToString()





        Response.Redirect("~/InventoryAdjustments.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&EmployeeID=" & EmployeeID & "&InventoryAdjustmentsNumber=" & InventoryAdjustmentsNumber)
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




End Class
