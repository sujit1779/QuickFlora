Imports System.Data
Imports System.Data.SqlClient

Partial Class TransferReceivingReport
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


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not IsPostBack Then

            txtDeliveryDate.Text = Date.Now.Date
            txtDeliveryDateTO.Text = Date.Now.Date

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

        If Not IsPostBack Then
            FillOrderLocation(CompanyID, DivisionID, DepartmentID)
        End If

        Dim dt As New DataTable

        If Not IsPostBack Then
            ' SetShipMethoddropdown()
            ' SetLocationIDdropdown()
            ' SetOriginDdropdown()
            '  GetProductVendor()
        End If

        dt = GetReportData()
        AutoGrid.DataSource = dt
        AutoGrid.DataBind()


    End Sub

    Private Sub SearchButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SearchButton.Click
        Dim dt As New DataTable
        dt = GetReportData()

        AutoGrid.DataSource = dt
        AutoGrid.DataBind()
    End Sub


    Public Sub GetProductVendor()
        Dim constr As String = ""
        constr = EnterpriseCommon.Configuration.ConfigSettings.ConnectionString
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT  distinct InventoryItems.ProductVendor FROM [InventoryItems] where   CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 AND ISNULL(InventoryItems.VendorID,'') <> '' "
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
            drpProductVendor.DataSource = dt
            drpProductVendor.DataTextField = "ProductVendor"
            drpProductVendor.DataValueField = "ProductVendor"
            drpProductVendor.DataBind()
        End If

    End Sub


    Public Sub SetShipMethoddropdown()
        Dim constr As String = ""
        constr = EnterpriseCommon.Configuration.ConfigSettings.ConnectionString
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = " SElect DISTINCT TruckingSchedule.ShipMethodID,TruckingSchedule.ShipMethodDescription     FROM TruckingSchedule "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try
            ''com.Parameters.Add(New SqlParameter("@Location", SqlDbType.NVarChar)).Value = drpInventoryOrigin.SelectedValue
            ''com.Parameters.Add(New SqlParameter("@ShippingToLocation", SqlDbType.NVarChar, 36)).Value = cmblocationid.SelectedValue
            da.SelectCommand = com
            da.Fill(dt)

        Catch ex As Exception

        End Try


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

    Public Function FillVendor() As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT * FROM [VendorInformation] where   CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 order by VendorID ASC"
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


    Public Sub SetLocationIDdropdown()

        '''''''''''''''''

        Dim dt As New Data.DataTable

        dt = FillVendor()

        If dt.Rows.Count <> 0 Then
            cmblocationid.DataSource = dt
            cmblocationid.DataTextField = "VendorName"
            cmblocationid.DataValueField = "VendorID"
            cmblocationid.DataBind()
            'Setdropdown()
        Else
            'cmblocationid.Items.Clear()
            Dim item As New ListItem
            item.Text = "DEFAULT"
            item.Value = "DEFAULT"
            cmblocationid.Items.Add(item)
        End If
        ''''''''''''''''''''

    End Sub



    Public Function FillLocation() As DataTable

        Dim connec As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT * FROM [Order_Location] where  LocationID <> 'Wholesale' and CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 order by LocationName ASC"
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
    Private Sub FillOrderLocation(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String)

        Dim dt As New DataTable
        Dim obj As New clsOrder_Location

        obj.CompanyID = CompanyID
        obj.DivisionID = DivisionID
        obj.DepartmentID = DepartmentID

        dt = FillLocation()

        If dt.Rows.Count > 0 Then
            With drpTansferFromLocaton
                .DataSource = dt
                .DataTextField = "LocationName"
                .DataValueField = "LocationID"
                .DataBind()
                .Items.Remove("")
                .Items.Insert(0, (New ListItem("--Please Select--", "")))
            End With

            With drpTransferToLocaton
                .DataSource = dt
                .DataTextField = "LocationName"
                .DataValueField = "LocationID"
                .DataBind()
                .Items.Remove("")
                .Items.Insert(0, (New ListItem("--Please Select--", "")))
            End With

            Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
            Dim locationid As String = ""
            If locationid <> "" Then
                'drpTansferFromLocaton.SelectedIndex = drpTansferFromLocaton.Items.IndexOf(drpTansferFromLocaton.Items.FindByValue(locationid))
                'drpTransferToLocaton.SelectedIndex = drpTransferToLocaton.Items.IndexOf(drpTransferToLocaton.Items.FindByValue(locationid))
            End If

            'drpTansferFromLocaton.Items.Remove("Wholesale")
            '`  drpTransferToLocaton.Items.Remove("Wholesale")

            ' Dim locationid As String = ""
            Try
                locationid = Session("Locationid")
            Catch ex As Exception

            End Try
            ''------------------''
            Dim locationid_chk As String = ""
            Dim locationid_true As Boolean = True

            Try
                Dim obj_new As New clsOrder_Location
                ' Dim dt As New Data.DataTable
                obj_new.CompanyID = CompanyID
                obj_new.DivisionID = DivisionID
                obj_new.DepartmentID = DepartmentID
                Dim dt_new As New Data.DataTable
                dt_new = obj_new.FillLocationIsmaster()

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
                drpTransferToLocaton.SelectedIndex = drpTransferToLocaton.Items.IndexOf(drpTransferToLocaton.Items.FindByValue(locationid))
                drpTransferToLocaton.Enabled = False
            End If


        End If

    End Sub


    Public Function GetReportData() As DataTable
        Dim dt As New DataTable

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = "[dbo].[TransferReceivingreport]"


        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        com.CommandType = CommandType.StoredProcedure
        com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
        com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
        com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
        com.Parameters.Add(New SqlParameter("@fromDate", SqlDbType.DateTime)).Value = txtDeliveryDate.Text
        com.Parameters.Add(New SqlParameter("@todate", SqlDbType.DateTime)).Value = txtDeliveryDateTO.Text
        com.Parameters.Add(New SqlParameter("@TransferFrom", SqlDbType.NVarChar, 36)).Value = drpTansferFromLocaton.SelectedValue
        com.Parameters.Add(New SqlParameter("@Transferto", SqlDbType.NVarChar, 36)).Value = drpTransferToLocaton.SelectedValue

        If optbyShipDate.Checked Then
            com.Parameters.Add(New SqlParameter("@bydate", SqlDbType.NVarChar, 36)).Value = "1"
            Session("bydate") = "1"
        End If

        If optbyArriveDate.Checked Then
            com.Parameters.Add(New SqlParameter("@bydate", SqlDbType.NVarChar, 36)).Value = "2"
            Session("bydate") = "2"
        End If

        If drpstatus.SelectedValue = "Received" Then
            com.Parameters.Add(New SqlParameter("@status", SqlDbType.NVarChar, 36)).Value = "1"
            Session("status") = "1"
        End If
        If drpstatus.SelectedValue = "Not Received" Then
            com.Parameters.Add(New SqlParameter("@status", SqlDbType.NVarChar, 36)).Value = "2"
            Session("status") = "2"
        End If
        If drpstatus.SelectedValue = "" Then
            com.Parameters.Add(New SqlParameter("@status", SqlDbType.NVarChar, 36)).Value = "1"
            Session("status") = "3"
        End If

        Session("txtDeliveryDate") = txtDeliveryDate.Text
        Session("txtDeliveryDateTO") = txtDeliveryDateTO.Text
        Session("TransferFrom") = drpTansferFromLocaton.SelectedValue
        Session("Transferto") = drpTransferToLocaton.SelectedValue


        da.SelectCommand = com

        Try
            da.Fill(dt)
        Catch ex As Exception
            Response.Write(ex.Message)
            Response.Write("<br>")
            Response.Write(ssql)
        End Try
        'Response.Write(dt.Rows.Count)
        'Response.Write(ssql)
        Return dt

    End Function

    Private Sub btnsendemail_Click(sender As Object, e As ImageClickEventArgs) Handles btnsendemail.Click
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "insert into [POExportoExcelemaill]( [CompanyID] ,[DivisionID] ,[DepartmentID] ,[PONumber] ,[bydate] ,[txtDeliveryDate] ,[txtDeliveryDateTO] ,[cmblocationid] ,[ProductVendorCode] ,[Done] ) " _
         & " values(@CompanyID ,@DivisionID ,@DepartmentID ,@PONumber ,@bydate ,@txtDeliveryDate ,@txtDeliveryDateTO ,@cmblocationid ,@ProductVendorCode ,1)"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@PONumber", SqlDbType.NVarChar, 255)).Value = ""
            com.Parameters.Add(New SqlParameter("@bydate", SqlDbType.NVarChar, 500)).Value = Session("bydate")
            com.Parameters.Add(New SqlParameter("@txtDeliveryDate", SqlDbType.NVarChar, 500)).Value = Session("txtDeliveryDate")
            com.Parameters.Add(New SqlParameter("@txtDeliveryDateTO", SqlDbType.NVarChar, 500)).Value = Session("txtDeliveryDateTO")
            com.Parameters.Add(New SqlParameter("@cmblocationid", SqlDbType.NVarChar, 500)).Value = Session("cmblocationid")
            com.Parameters.Add(New SqlParameter("@ProductVendorCode", SqlDbType.NVarChar, 500)).Value = Session("ProductVendorCode")

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            ' Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            '' HttpContext.Current.Response.Write(msg)
            ' Return False

        End Try
    End Sub



    'Protected Sub rptorderlist_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles rptorderlist.PageIndexChanging

    '    rptorderlist.PageIndex = e.NewPageIndex
    '    Dim dt As New DataTable
    '    dt = GetReportData()

    '    rptorderlist.DataSource = dt
    '    rptorderlist.DataBind()
    'End Sub

End Class



