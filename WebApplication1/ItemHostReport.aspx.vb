Imports System.Data
Imports System.Data.SqlClient

Partial Class ItemHostReport
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


    Private Sub btnsendemail_Click(sender As Object, e As ImageClickEventArgs) Handles btnsendemail.Click
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "insert into [HostReportExportoExcelemaill] ( [CompanyID] ,[DivisionID] ,[DepartmentID] ,[itmhostsql] ,[Done],[Hostdate] ) " _
         & " values(@CompanyID ,@DivisionID ,@DepartmentID ,@itmhostsql ,1,@Hostdate)"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@itmhostsql", SqlDbType.NVarChar)).Value = Session("itmhostsql")
            com.Parameters.Add(New SqlParameter("@Hostdate", SqlDbType.NVarChar)).Value = " From " & txtDeliveryDate.Text & " To " & "" & txtDeliveryDateTO.Text

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
            'SetShipMethoddropdown()
            'SetLocationIDdropdown()
            'SetOriginDdropdown()
            FillItemFamilies()
        End If

        dt = GetReportData()
        AutoGrid.DataSource = dt
        AutoGrid.DataBind()


    End Sub

    Public Function Fillfamily() As DataTable
        Dim connec As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from InventoryFamilies where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 "
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


    Public Function FillDetailscategory(ByVal familyid As String) As DataTable
        Dim connec As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from InventoryCategories where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and ItemFamilyID=@f3 "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = familyid

            da.SelectCommand = com
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return dt

        End Try

    End Function



    Private Sub FillItemFamilies()

        Dim dtfamily As New DataTable
        dtfamily = Fillfamily()

        drpItemFamilyID1.DataSource = dtfamily
        drpItemFamilyID1.DataTextField = "FamilyName"
        drpItemFamilyID1.DataValueField = "ItemFamilyID"
        drpItemFamilyID1.DataBind()

        drpItemFamilyID1.SelectedIndex = drpItemFamilyID1.Items.IndexOf(drpItemFamilyID1.Items.FindByValue("Shop By Products"))

        Dim s As New Object
        Dim e As New EventArgs

        drpItemFamilyID1_SelectedIndexChanged(s, e)

    End Sub

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
                    ' Debug = ex.Message
                    Return Nothing

                End Try

            End Using
        End Using

    End Function

    Protected Sub drpItemFamilyID1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles drpItemFamilyID1.SelectedIndexChanged
        drpItemCategoryID1.Items.Clear()

        Dim ds As New DataSet
        ds = GetItemCategories(drpItemFamilyID1.SelectedValue)

        drpItemCategoryID1.DataSource = ds
        drpItemCategoryID1.DataTextField = "CategoryName"
        drpItemCategoryID1.DataValueField = "ItemCategoryID"
        drpItemCategoryID1.DataBind()
        Dim lst As New ListItem
        lst.Value = ""
        lst.Text = "--Select--"
        drpItemCategoryID1.Items.Insert(0, lst)

    End Sub



    Private Sub SearchButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SearchButton.Click
        Dim dt As New DataTable
        dt = GetReportData()

        AutoGrid.DataSource = dt
        AutoGrid.DataBind()
    End Sub



    Public Function GetReportData() As DataTable
        Dim dt As New DataTable

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        'ssql = "SELECT 	convert(datetime, Convert(nvarchar(36),OrderHeader.OrderDate,101)) as ReportDay  FROM   OrderHeader  "

        ssql = ssql & " select ItemID AS 'Item ID',CreatedAT AS 'Created On',InventoryItems.VendorID AS 'Vendor', "
        ssql = ssql & " (Select VendorInformation.VendorName  from VendorInformation where VendorInformation.CompanyID = InventoryItems.CompanyID AND VendorInformation.DivisionID = InventoryItems.DivisionID AND VendorInformation.DepartmentID = InventoryItems.DepartmentID and VendorInformation.VendorID = InventoryItems.VendorID ) AS 'Vendor Name', "
        ssql = ssql & " InventoryItems.ItemUPCCode AS 'UPC Code' ,InventoryItems.ItemDescription AS 'Description',cast(ISNULL(InventoryItems.AverageCost,0)  as decimal(12,2)) AS 'Store Cost',cast(ISNULL(InventoryItems.Price,0)  as decimal(12,2)) AS 'Store Retail',InventoryItems.GroupCode AS 'Group',InventoryItems.ItemCategoryID AS 'Category',[Class] AS 'Class'       from InventoryItems   "
        ssql = ssql & " where   InventoryItems.CompanyID=@f0 And InventoryItems.DivisionID=@f1 And InventoryItems.DepartmentID=@f2   "
        ssql = ssql & " and [InventoryItems].CreatedAT >= '" & txtDeliveryDate.Text & "'"
        ssql = ssql & " and [InventoryItems].CreatedAT <='" & txtDeliveryDateTO.Text & "'"
        If drpItemCategoryID1.SelectedValue <> "" Then
            ssql = ssql & " and InventoryItems.ItemCategoryID = '" & drpItemCategoryID1.SelectedValue & "'"
        End If

        ssql = ssql & " Order by   InventoryItems.ItemName  "
        'If optShipDate.Checked Then
        '    ssql = ssql & " Order by convert(datetime, Convert(nvarchar(36), CASE WHEN  ISDATE([PO_Requisition_Header].[ShipDate])  = 1  THEN  [PO_Requisition_Header].[ShipDate] ELSE '1/1/1900' END,101))  DESC "
        'End If

        'If OptArriveDate.Checked Then
        '    ssql = ssql & " Order by convert(datetime, Convert(nvarchar(36), CASE WHEN  ISDATE([PO_Requisition_Header].[ArriveDate])  = 1  THEN  [PO_Requisition_Header].[ArriveDate] ELSE '1/1/1900' END,101))  DESC "
        'End If

        Session("itmhostsql") = ssql
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

    Private Sub drpItemCategoryID1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drpItemCategoryID1.SelectedIndexChanged
        Dim dt As New DataTable
        dt = GetReportData()

        AutoGrid.DataSource = dt
        AutoGrid.DataBind()
    End Sub

    'Protected Sub rptorderlist_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles rptorderlist.PageIndexChanging

    '    rptorderlist.PageIndex = e.NewPageIndex
    '    Dim dt As New DataTable
    '    dt = GetReportData()

    '    rptorderlist.DataSource = dt
    '    rptorderlist.DataBind()
    'End Sub

End Class


