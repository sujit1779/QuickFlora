Imports System.Data
Imports System.Data.SqlClient

Partial Class RequestReport
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


        Dim dt As New DataTable

        If Not IsPostBack Then
            SetShipMethoddropdown()
            SetLocationIDdropdown()
            SetOriginDdropdown()
        End If

        dt = GetReportData()
        rptorderlist.DataSource = dt
        rptorderlist.DataBind()


    End Sub

    Private Sub SearchButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SearchButton.Click
        Dim dt As New DataTable
        dt = GetReportData()

        rptorderlist.DataSource = dt
        rptorderlist.DataBind()
    End Sub


    Public Sub SetOriginDdropdown()
        Dim constr As String = ""
        constr = EnterpriseCommon.Configuration.ConfigSettings.ConnectionString
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
            'cmblocationid.Items.Clear()
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

    Public Function GetReportData() As DataTable
        Dim dt As New DataTable

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        'ssql = "SELECT 	convert(datetime, Convert(nvarchar(36),OrderHeader.OrderDate,101)) as ReportDay  FROM   OrderHeader  "

        ssql = ssql & "Select [PO_Requisition_Details].[OrderNo] , [PO_Requisition_Details].[PONO]  ,PO_Requisition_Header.ShipDate ,PO_Requisition_Header.ArriveDate ,VendorInformation.VendorName ,[Product] , InventoryItems.ItemName  "
        ssql = ssql & " , [Q_REQ] , [COLOR_VARIETY] "
        ssql = ssql & " ,[Q_ORD] ,[PACK] , Convert(Money, CASE WHEN  ISNUMERIC([PO_Requisition_Details].[COST])  = 1  THEN  [PO_Requisition_Details].[COST] ELSE '0.00' END) As 'COST' ,[Ext_COSt] ,[Buyer] ,[PO_Requisition_Details].[Status], PO_Requisition_Header.Location"
        ssql = ssql & "  "
        ssql = ssql & "  From [Enterprise].[dbo].[PO_Requisition_Details]  Inner Join PO_Requisition_Header   On "
        ssql = ssql & " [PO_Requisition_Details].[CompanyID] = PO_Requisition_Header.[CompanyID] "
        ssql = ssql & " And [PO_Requisition_Details].[DivisionID] = PO_Requisition_Header.[DivisionID] "
        ssql = ssql & " And [PO_Requisition_Details].[DepartmentID] = PO_Requisition_Header.[DepartmentID]  "
        ssql = ssql & " And [PO_Requisition_Details].[OrderNo] = PO_Requisition_Header.[OrderNo] "
        ssql = ssql & " Inner Join InventoryItems ON "
        ssql = ssql & " [PO_Requisition_Details].[CompanyID] = InventoryItems.[CompanyID]  "
        ssql = ssql & " And [PO_Requisition_Details].[DivisionID] = InventoryItems.[DivisionID]  "
        ssql = ssql & " And [PO_Requisition_Details].[DepartmentID] = InventoryItems.[DepartmentID]  "
        ssql = ssql & " And [PO_Requisition_Details].Product  = InventoryItems.ItemID    "
        ssql = ssql & " Left Outer Join VendorInformation  ON  "
        ssql = ssql & " [PO_Requisition_Details].[CompanyID] = VendorInformation.[CompanyID]  "
        ssql = ssql & " And [PO_Requisition_Details].[DivisionID] = VendorInformation.[DivisionID]  "
        ssql = ssql & " And [PO_Requisition_Details].[DepartmentID] = VendorInformation.[DepartmentID]  "
        ssql = ssql & " And [PO_Requisition_Details].Vendor_Code   = VendorInformation.VendorID   "
        ssql = ssql & " where   PO_Requisition_Header.CompanyID=@f0 And PO_Requisition_Header.DivisionID=@f1 And PO_Requisition_Header.DepartmentID=@f2   "
        '' ssql = ssql & " And ISNULL([PO_Requisition_Details].PONO,'') <> '' "

        If chkWithOther.Checked And optBought.Checked = False Then
            ssql = ssql & " And (ISNULL([PO_Requisition_Details].PONO,'') <> '' OR PO_Requisition_Details.Status = 'With-Other') "
        Else
            '  ssql = ssql & " And (ISNULL([PO_Requisition_Details].PONO,'') <> '') "
        End If

        If optallstatus.Checked Then

        End If

        If optBought.Checked And chkWithOther.Checked = False Then
            ssql = ssql & " And (ISNULL([PO_Requisition_Details].PONO,'') <> '' OR  PO_Requisition_Details.Status = 'Bought' )"
        End If

        If optBought.Checked And chkWithOther.Checked Then
            ssql = ssql & " And (ISNULL([PO_Requisition_Details].PONO,'') <> '' OR  PO_Requisition_Details.Status = 'Bought'  OR PO_Requisition_Details.Status = 'With-Other' )"
        End If


        If optShipDate.Checked Then
            ' ssql = ssql & " And IsDate(PO_Requisition_Header.ShipDate) = 1 "
            ssql = ssql & " and [PO_Requisition_Details].ShipDate >= '" & txtDeliveryDate.Text & "'"
            ssql = ssql & " and [PO_Requisition_Details].ShipDate <='" & txtDeliveryDateTO.Text & "'"
        End If

        If OptArriveDate.Checked Then
            ssql = ssql & " And IsDate(PO_Requisition_Header.ArriveDate) = 1 "
            ssql = ssql & " and convert(datetime, Convert(nvarchar(36), CASE WHEN  ISDATE([PO_Requisition_Header].[ArriveDate])  = 1  THEN  [PO_Requisition_Header].[ArriveDate] ELSE '1/1/1900' END,101))  >= '" & txtDeliveryDate.Text & "'"
            ssql = ssql & " and convert(datetime, Convert(nvarchar(36), CASE WHEN  ISDATE([PO_Requisition_Header].[ArriveDate])  = 1  THEN  [PO_Requisition_Header].[ArriveDate] ELSE '1/1/1900' END,101))  <='" & txtDeliveryDateTO.Text & "'"
        End If

        If drpInventoryOrigin.SelectedValue <> "" Then
            ssql = ssql & " And ISNULL(PO_Requisition_Header.InventoryOrigin,'') = '" & drpInventoryOrigin.SelectedValue & "'"
        End If
        'PO_Requisition_Header.InventoryOrigin,PO_Requisition_Header.Location,PO_Requisition_Header.ShipMethodID
        If cmblocationid.SelectedValue <> "" Then
            ssql = ssql & " And ISNULL(PO_Requisition_Header.Location,'') = '" & cmblocationid.SelectedValue & "'"
        End If

        If drpshipemthod.SelectedValue <> "" Then
            ssql = ssql & " And ISNULL(PO_Requisition_Header.ShipMethodID,'') = '" & drpshipemthod.SelectedValue & "'"
        End If

        ssql = ssql & " Order by VendorInformation.VendorName , InventoryItems.ItemName  "
        'If optShipDate.Checked Then
        '    ssql = ssql & " Order by convert(datetime, Convert(nvarchar(36), CASE WHEN  ISDATE([PO_Requisition_Header].[ShipDate])  = 1  THEN  [PO_Requisition_Header].[ShipDate] ELSE '1/1/1900' END,101))  DESC "
        'End If

        'If OptArriveDate.Checked Then
        '    ssql = ssql & " Order by convert(datetime, Convert(nvarchar(36), CASE WHEN  ISDATE([PO_Requisition_Header].[ArriveDate])  = 1  THEN  [PO_Requisition_Header].[ArriveDate] ELSE '1/1/1900' END,101))  DESC "
        'End If

        Session("RequestReport") = ssql
        'Response.Write(ssql)

        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
        com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
        com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
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

    'Protected Sub rptorderlist_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles rptorderlist.PageIndexChanging

    '    rptorderlist.PageIndex = e.NewPageIndex
    '    Dim dt As New DataTable
    '    dt = GetReportData()

    '    rptorderlist.DataSource = dt
    '    rptorderlist.DataBind()
    'End Sub

End Class
