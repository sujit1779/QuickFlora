Imports System.Data
Imports System.Data.SqlClient
Imports DAL

Partial Class InventoryTransferDetaila
    Inherits System.Web.UI.Page

    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""

    Public imgW As Integer = 49
    Public imgH As Integer = 34

    Dim obj As New clsInventoryTransfer



    Public Function PopulateCompanyLogo(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As SqlDataReader


        Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        ConString.Open()
        Dim myCommand As New SqlCommand("enterprise.spCompanyInformation", ConString)
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

    Public EmployeeID As String = ""



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        Dim filters As EnterpriseCommon.Core.FilterSet
        filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
        CompanyID = filters!CompanyID
        DivisionID = filters!DivisionID
        DepartmentID = filters!DepartmentID
        EmployeeID = Session("EmployeeID")

        Dim rs1 As SqlDataReader
        rs1 = PopulateEmployees(CompanyID, DepartmentID, DivisionID, "MTEDIT")

        Dim securitycheck As Boolean = False

        While (rs1.Read())

            If rs1("EmployeeID").ToString() = EmployeeID Then
                securitycheck = True
                Exit While
            End If

        End While
        rs1.Close()

        If securitycheck = False Then
            Response.Redirect("SecurityAcessPermission.aspx?MOD=MT")
        End If


        Dim TransferNumber As String = ""

        'txtItemID.Attributes.Add("onKeyUp", "SendQueryForTransfer(this.value)")


        'Populating RetailerLogo
        Dim ImageTemp As String = ""

        Dim rs As SqlDataReader
        rs = PopulateCompanyLogo(CompanyID, DepartmentID, DivisionID)
        While (rs.Read())

            'Dim objNascheck As New clsNasImageCheck
            'ImgRetailerLogo.ImageUrl = objNascheck.retLogourl(rs("CompanyLogoUrl").ToString()) ' "~/images/" & rs("CompanyLogoUrl").ToString()

            ImgRetailerLogo.ImageUrl = "https://secure.quickflora.com/Admin/" & returl(rs("CompanyLogoUrl").ToString())

        End While

        rs.Close()

        If Not Page.IsPostBack Then

            FillOrderLocation(CompanyID, DivisionID, DepartmentID)
            FillEmployee()
            drpTansferByEmployee.SelectedValue = EmployeeID
            drpApprovedByEmployee.SelectedValue = EmployeeID
            Dim objtm As New clsCompanyLocalTime
            Dim gmtdt As New DateTime

 

            gmtdt = Date.Now
            gmtdt = objtm.populateCMPTime(CompanyID, DivisionID, DepartmentID, gmtdt)


            'Try
            '    txtPostedDate.Text = gmtdt.ToShortDateString
            '    txtPostedTime.Text = gmtdt.ToShortTimeString
            'Catch ex As Exception

            'End Try
            txtTransferDate.Text = gmtdt.ToShortDateString

            If Request.QueryString("TransferNumber") <> String.Empty Then
                TransferNumber = Request.QueryString("TransferNumber")
                GetInventoryTransferInformation(TransferNumber)
            Else
                txtApprovedTime.Text = gmtdt.ToShortTimeString
                btnGenerateTransferNumber.Visible = True
            End If



            'DateTime.Now.ToString("MM/dd/yyyy")

        End If

        lblErrorMessage.Visible = False

    End Sub

    Public Function returl(ByVal ob As String) As String

        Dim DocumentDir As String = ""
        Dim img1 As String = ""
        Dim ImgName As String = ob.ToLower()
        DocumentDir = ConfigurationManager.AppSettings("DocPath")
        If (ImgName.Trim() = "") Then

            Return "/images/no_image.gif"

        Else

            If System.IO.File.Exists(DocumentDir & ImgName.Trim()) Then

                Return "/images/" & ImgName.Trim()

            Else
                Return "/images/no_image.gif"
            End If




        End If


    End Function

    Private Sub FillOrderLocation(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String)

        Dim dt As New DataTable
        Dim obj As New clsOrder_Location

        obj.CompanyID = CompanyID
        obj.DivisionID = DivisionID
        obj.DepartmentID = DepartmentID

        dt = obj.FillLocation

        If dt.Rows.Count > 0 Then
            With drpTansferFromLocaton
                .DataSource = dt
                .DataTextField = "LocationName"
                .DataValueField = "LocationID"
                .DataBind()
                .Items.Remove("")
                .Items.Insert(0, (New ListItem("Please Select", "")))
            End With

            With drpTransferToLocaton
                .DataSource = dt
                .DataTextField = "LocationName"
                .DataValueField = "LocationID"
                .DataBind()
                .Items.Remove("")
                .Items.Insert(0, (New ListItem("Please Select", "")))
            End With

            Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
            Dim locationid As String = "Davie"
            If locationid <> "" Then
                drpTansferFromLocaton.SelectedIndex = drpTansferFromLocaton.Items.IndexOf(drpTansferFromLocaton.Items.FindByValue(locationid))
                drpTransferToLocaton.SelectedIndex = drpTransferToLocaton.Items.IndexOf(drpTransferToLocaton.Items.FindByValue(locationid))
            End If

            drpTansferFromLocaton.Items.Remove("Wholesale")
            drpTransferToLocaton.Items.Remove("Wholesale")

            'Try

            '    Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
            '    Dim locationid As String = CType(SessionKey("Locationid"), String)
            '    If locationid <> "" Then
            '        drpTansferFromLocaton.SelectedIndex = drpTansferFromLocaton.Items.IndexOf(drpTansferFromLocaton.Items.FindByValue(locationid))
            '        drpTransferToLocaton.SelectedIndex = drpTransferToLocaton.Items.IndexOf(drpTransferToLocaton.Items.FindByValue(locationid))
            '    End If
            'Catch ex As Exception

            'End Try


        End If

    End Sub

    Private Sub GetInventoryTransferInformation(ByVal TransferNumber As String)

        FillInventoryTransferHeader(TransferNumber)

    End Sub

    Private Sub FillInventoryTransferHeader(ByVal TransferNumber As String)

        Dim dt As New DataTable
        dt = obj.GetInventoryTransferList(CompanyID, DivisionID, DepartmentID, TransferNumber)

        If dt.Rows.Count > 0 Then

            Dim row As DataRow
            row = dt.Rows(0)

            txtTransferNumber.Text = TransferNumber

            Try
                txtTransferDate.Text = row("TransferDate")
            Catch ex As Exception

            End Try

            Try
                drpTransferToLocaton.SelectedValue = row("TransferToLocation")
            Catch ex As Exception

            End Try


            Try
                drpTansferFromLocaton.SelectedValue = row("TansferFromLocation")
            Catch ex As Exception

            End Try



            Try
                drpTansferByEmployee.SelectedValue = row("TransferByEmployee")
            Catch ex As Exception

            End Try

            Try
                'Dim dtm As Date

                'dtm = row("TransferDate")
                '
                txtTransferDate.Text = row("TransferDate")
            Catch ex As Exception

            End Try



            Try
                drpApprovedByEmployee.SelectedValue = row("ApprovedByEmployee")
            Catch ex As Exception

            End Try


            Try
                lblTotalNumberOfItems.Text = row("TotalItemsTransfer")
            Catch ex As Exception

            End Try


            Try
                txtApprovedTime.Text = Convert.ToDateTime(row("ApprovedTime")).ToShortTimeString
            Catch ex As Exception

            End Try


            FillInvenntoryTransferItems(TransferNumber)

        End If

    End Sub

    Public Function returnenable(ByVal TransferQty As String, ByVal ReceivedQty As String) As Boolean

        Dim vTransferQty As Integer = 0
        Dim vReceivedQty As Integer = -1

        Try
            vTransferQty = TransferQty
        Catch ex As Exception

        End Try

        Try
            vReceivedQty = ReceivedQty
        Catch ex As Exception

        End Try

        If vTransferQty = vReceivedQty Then
            Return False
        End If

        Return True
    End Function



    Public Function totalprice(ByVal TransferNumber As String) As String
        Dim dtInventoryTransferDetail As New DataTable
        dtInventoryTransferDetail = InventoryTransferDetail(TransferNumber)


        If dtInventoryTransferDetail.Rows.Count > 0 Then

            Dim TransferQty As Integer = 0
            Dim MTPrice As Decimal = 0

            Try
                TransferQty = dtInventoryTransferDetail.Rows(0)(0)
            Catch ex As Exception

            End Try


            Try
                MTPrice = dtInventoryTransferDetail.Rows(0)(1)
            Catch ex As Exception

            End Try

            Return "$" & Format(MTPrice, "0.00")
        End If

        Return ""
    End Function



    Public Function InventoryTransferDetail(ByVal TransferNumber As String) As DataTable
        Dim dt As New DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "select sum(InventoryTransferDetail.TransferQty) , sum(InventoryTransferDetail.TransferQty*InventoryTransferDetail.MTPrice)  from InventoryTransferDetail Where CompanyID=@f1  AND   DivisionID =@f2  AND  DepartmentID =@f3  AND [TransferNumber]=@f4"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID.Trim()
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID.Trim()
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID.Trim()
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = TransferNumber

            com.CommandType = CommandType.Text

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


    Private Sub FillInvenntoryTransferItems(ByVal TransferNumber As String)

        lblprice.Text = totalprice(TransferNumber)

        Dim dt As New DataTable
        dt = obj.GetInventoryTransferItemsList(CompanyID, DivisionID, DepartmentID, TransferNumber)

        If dt.Rows.Count > 0 Then

            InventoryTransferItemGrid.DataSource = dt
            InventoryTransferItemGrid.DataBind()
            InventoryTransferItemGrid.Visible = True

            '    lblTotalNumberOfItems.Text = dt.Rows.Count

            Dim n As Integer = 0
            'TransferQty
            Dim TransferQty As Integer = 0

            For n = 0 To dt.Rows.Count - 1

                Try
                    TransferQty = TransferQty + dt.Rows(n)("TransferQty")
                Catch ex As Exception

                End Try

            Next
            lblTotalNumberOfItems.Text = TransferQty


        Else
            InventoryTransferItemGrid.Visible = False
            lblTotalNumberOfItems.Text = 0
        End If

    End Sub

    Private Function GenerateNextTransferNumber() As String

        txtTransferNumber.Text = obj.GetNextInventoryTransferNumber(CompanyID, DivisionID, DepartmentID)

    End Function

    Private Sub FillEmployee()

        Dim rs As SqlDataReader
        rs = PopulateEmployees(CompanyID, DepartmentID, DivisionID, "MT")

        'If dt.Rows.Count > 0 Then

        '    With drpApprovedByEmployee
        '        .DataSource = dt
        '        .DataTextField = "EmployeeID"
        '        .DataValueField = "EmployeeID"
        '        .DataBind()
        '        .Items.Remove("")
        '        .Items.Insert(0, (New ListItem("Please Select", "")))
        '    End With

        '    With drpTansferByEmployee
        '        .DataSource = dt
        '        .DataTextField = "EmployeeID"
        '        .DataValueField = "EmployeeID"
        '        .DataBind()
        '        .Items.Remove("")
        '        .Items.Insert(0, (New ListItem("Please Select", "")))
        '    End With

        'End If



        drpApprovedByEmployee.DataTextField = "EmployeeName"
        drpApprovedByEmployee.DataValueField = "EmployeeID"
        drpApprovedByEmployee.DataSource = rs
        drpApprovedByEmployee.DataBind()
        ' drpApprovedByEmployee.SelectedValue = EmployeeID ' Session("EmployeeUserName")
        drpApprovedByEmployee.Items.Insert(0, (New ListItem("-Select-", "0")))
        rs.Close()

        Dim rs1 As SqlDataReader
        rs1 = PopulateEmployees(CompanyID, DepartmentID, DivisionID, "MT")

        drpTansferByEmployee.DataTextField = "EmployeeName"
        drpTansferByEmployee.DataValueField = "EmployeeID"
        drpTansferByEmployee.DataSource = rs1
        drpTansferByEmployee.DataBind()
        drpTansferByEmployee.Items.Insert(0, (New ListItem("-Select-", "0")))
        rs1.Close()

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

    Protected Sub btnGenerateTransferNumber_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGenerateTransferNumber.Click

        Dim TransferNumber As String = ""



        Dim PopOrderNo As New CustomOrder()
        Dim rs As SqlDataReader
        rs = PopOrderNo.GetNextOrdNumber(CompanyID, DepartmentID, DivisionID, "NextInventoryTransferNumber")
        While rs.Read()
            TransferNumber = rs("NextNumberValue")

        End While
        rs.Close()
        txtTransferNumber.Text = TransferNumber
        UpdateNextInventoryTransferNumber(CompanyID, DivisionID, DepartmentID, TransferNumber)

        btnGenerateTransferNumber.Enabled = False

    End Sub

    Public Function UpdateNextInventoryTransferNumber(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal TransferNumber As String) As String

        Dim NextTransferNumber As String = ""

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[UpdateNextInventoryTransferNumber]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("TransferNumber", TransferNumber)

                Try
                    Command.Connection.Open()
                    NextTransferNumber = Convert.ToString(Command.ExecuteScalar())

                Catch ex As Exception

                End Try

            End Using
        End Using

        Return NextTransferNumber

    End Function



    Public Function CheckPicture(ByVal FileNameString As String) As String
        Dim DocumentDir As String = ""
        Dim PicFileName As String = ""
        DocumentDir = ConfigurationManager.AppSettings("ItemPath")
        If System.IO.File.Exists(DocumentDir & FileNameString.Trim()) Then
            PicFileName = FileNameString.Trim()
        Else

            PicFileName = "no_image.gif"
        End If
        Return PicFileName

    End Function

    Protected Sub ImgUpdateSearchitems_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgUpdateSearchitems.Click


        If txtTransferNumber.Text.Trim = "" Then
            Dim TransferNumber As String = ""

            Dim PopOrderNo As New CustomOrder()
            Dim rs As SqlDataReader
            rs = PopOrderNo.GetNextOrdNumber(CompanyID, DepartmentID, DivisionID, "NextInventoryTransferNumber")
            While rs.Read()
                TransferNumber = rs("NextNumberValue")

            End While
            rs.Close()
            txtTransferNumber.Text = TransferNumber
            UpdateNextInventoryTransferNumber(CompanyID, DivisionID, DepartmentID, TransferNumber)

            btnGenerateTransferNumber.Enabled = False
        End If

        'Session("RunCount") += 1
        'If Session("RunCount") <> 1 Then
        '    Exit Sub
        'End If

        If txtitemsearch.Text.Trim = "" Then
            Exit Sub
        End If

        Dim TempID As String = txtitemsearch.Text
        txtitemsearch.Text = ""

        If TempID.IndexOf("[") > -1 Then
            Dim st, ed As Integer
            st = TempID.IndexOf("[")
            ed = TempID.IndexOf("]")

            If st = -1 Then
                Exit Sub
            End If

            If (ed - st) - 1 = -1 Then
                Exit Sub
            End If

            TempID = TempID.Substring(st + 1, (ed - st) - 1)
        End If

        Dim arrItem() As String = TempID.Split(",")
        Dim ErrorMessage As String = ""

        Try

            Dim rowCount As Integer = 0
            Dim chk As Boolean = False
            If obj.InsertInventoryTransferDetail(CompanyID, DivisionID, DepartmentID, txtTransferNumber.Text, arrItem(0), 1, "") Then
                'InventoryTransferItemGrid.EditIndex = rowCount
                chk = True
            Else
                For Each row As GridViewRow In InventoryTransferItemGrid.Rows
                    If CType(row.Cells(3).FindControl("lblItemID"), Label).Text.Trim = arrItem(0) Then
                        'InventoryTransferItemGrid.EditIndex = rowCount
                        Exit For
                    End If
                    rowCount += 1
                Next
            End If
            If chk Then
                ' InventoryTransferItemGrid.EditIndex = InventoryTransferItemGrid.Rows.Count
            End If


            FillInvenntoryTransferItems(txtTransferNumber.Text)
            If chk Then
                '  InventoryTransferItemGrid.Rows(InventoryTransferItemGrid.EditIndex).BackColor = Drawing.Color.YellowGreen
                '  InventoryTransferItemGrid.Rows(InventoryTransferItemGrid.EditIndex).FindControl("txtUpdateQty").Focus()

            End If
            'txtitemsearch.Enabled = False
            'ItemSearch.Enabled = False

        Catch ex As Exception

            lblErrorMessage.Text = ErrorMessage
            lblErrorMessage.Visible = True

        End Try

    End Sub

    Protected Sub InventoryTransferItemGrid_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles InventoryTransferItemGrid.RowDataBound

        'if enter key is pressed (keycode==13) call __doPostBack on grid and with 
        ' 1st param = gvChild.UniqueID (Gridviews UniqueID)
        ' 2nd param = CommandName=Update$  +  CommandArgument=RowIndex

        If e.Row.RowState = DataControlRowState.Edit Then

            ' e.Row.FindControl("txtUpdateQty").Focus()
            '  e.Row.Attributes.Add("onkeypress", "javascript:if (event.keyCode == 13) { __doPostBack('" + _
            '  InventoryTransferItemGrid.UniqueID + "', 'Update$" + e.Row.RowIndex.ToString() + "'); return false; }")





        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim txtInLineNumber As New Label
            txtInLineNumber = e.Row.FindControl("lblRowID")
            Dim lblUnitPrice As TextBox = e.Row.FindControl("lblUnitPrice")
            Dim lblQty As TextBox = e.Row.FindControl("lblQty")
            Dim lblAdditionalNotes As TextBox = e.Row.FindControl("lblAdditionalNotes")

            lblUnitPrice.Attributes.Add("onfocus", "myFocusFunction(this)")
            lblUnitPrice.Attributes.Add("onblur", "Saveitem(this,'" & txtInLineNumber.Text & "','MTPrice','" & lblUnitPrice.Text & "')")

            lblQty.Attributes.Add("onfocus", "myFocusFunction(this)")
            lblQty.Attributes.Add("onblur", "Saveitem(this,'" & txtInLineNumber.Text & "','TransferQty','" & lblQty.Text & "')")

            lblAdditionalNotes.Attributes.Add("onfocus", "myFocusFunction(this)")
            lblAdditionalNotes.Attributes.Add("onblur", "Saveitem(this,'" & txtInLineNumber.Text & "','AddtionalItemNotes','" & lblAdditionalNotes.Text & "')")
        End If




    End Sub



    Public Function UpdateInventoryTransferDetail(ByVal RowID As String, ByVal TransferQty As String, ByVal AddionalItemNotes As String, ByVal UnitPrice As Decimal) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[UpdateInventoryTransferDetail]", Connection)

                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("RowID", RowID)
                Command.Parameters.AddWithValue("TransferQty", TransferQty)
                Command.Parameters.AddWithValue("AddionalItemNotes", AddionalItemNotes)
                Command.Parameters.AddWithValue("MTPrice", UnitPrice)

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


    Protected Sub InventoryTransferItemGrid_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles InventoryTransferItemGrid.RowCommand


        If e.CommandName = "Update" Then

            Dim row As GridViewRow = InventoryTransferItemGrid.Rows(Convert.ToInt32(e.CommandArgument))

            Dim RowID As Label = CType(row.FindControl("lblUpdateRowID"), Label)
            Dim Qty As TextBox = CType(row.FindControl("txtUpdateQty"), TextBox)

            Dim lblUpdateUnitPrice As TextBox = CType(row.FindControl("lblUpdateUnitPrice"), TextBox)

            Dim AdditionalNotes As TextBox = CType(row.FindControl("txtUpdateAddtionalNotes"), TextBox)

            Dim ErrorMessage As String = ""

            If IsNumeric(lblUpdateUnitPrice.Text) Then

            Else
                lblUpdateUnitPrice.Text = 0
            End If

            If UpdateInventoryTransferDetail(RowID.Text, Qty.Text, AdditionalNotes.Text, lblUpdateUnitPrice.Text) Then
                InventoryTransferItemGrid.EditIndex = -1
                FillInvenntoryTransferItems(txtTransferNumber.Text)
            Else
                lblErrorMessage.Text = ErrorMessage
                lblErrorMessage.Visible = True
            End If

            txtitemsearch.Enabled = True
            ItemSearch.Enabled = True
            Session("RunCount") = 0

        End If

    End Sub

    Protected Sub InventoryTransferItemGrid_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles InventoryTransferItemGrid.RowDeleting

        Dim RowID As String = InventoryTransferItemGrid.DataKeys(e.RowIndex).Values(0).ToString()

        obj.DeleteInventoryTransferDetail(RowID)

        FillInvenntoryTransferItems(txtTransferNumber.Text)

        txtitemsearch.Enabled = True
        ItemSearch.Enabled = True
        Session("RunCount") = 0

    End Sub

    Protected Sub InventoryTransferItemGrid_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles InventoryTransferItemGrid.RowEditing

        InventoryTransferItemGrid.EditIndex = e.NewEditIndex
        FillInvenntoryTransferItems(txtTransferNumber.Text)
        InventoryTransferItemGrid.Rows(e.NewEditIndex).BackColor = Drawing.Color.YellowGreen

        InventoryTransferItemGrid.Rows(e.NewEditIndex).FindControl("txtUpdateQty").Focus()

        txtitemsearch.Enabled = False
        ItemSearch.Enabled = False
        Session("RunCount") = 0

    End Sub

    Protected Sub InventoryTransferItemGrid_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles InventoryTransferItemGrid.RowUpdating

        'Dim RowID As Label = CType(InventoryTransferItemGrid.Rows(e.RowIndex).FindControl("lblUpdateRowID"), Label)
        'Dim Qty As TextBox = CType(InventoryTransferItemGrid.Rows(e.RowIndex).FindControl("txtUpdateQty"), TextBox)
        'Dim AdditionalNotes As TextBox = CType(InventoryTransferItemGrid.Rows(e.RowIndex).FindControl("txtUpdateAddtionalNotes"), TextBox)

        'Dim ErrorMessage As String = ""

        'If obj.UpdateInventoryTransferDetail(RowID.Text, Qty.Text, AdditionalNotes.Text) Then
        '    InventoryTransferItemGrid.EditIndex = -1
        '    FillInvenntoryTransferItems(txtTransferNumber.Text)
        'Else
        '    lblErrorMessage.Text = ErrorMessage
        '    lblErrorMessage.Visible = True
        'End If

        'txtitemsearch.Enabled = True
        'ItemSearch.Enabled = True
        'Session("RunCount") = 0

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Dim Output As Boolean = False

        Dim TransferNumber As String = txtTransferNumber.Text
        Dim TransferDate As String = txtTransferDate.Text
        Dim TransferFromLocation As String = drpTansferFromLocaton.SelectedValue
        Dim TransferToLocation As String = drpTransferToLocaton.SelectedValue
        Dim TransferByEmployee As String = drpTansferByEmployee.SelectedValue
        Dim TotalItems As String = lblTotalNumberOfItems.Text
        Dim ApprovedByEmployee As String = drpApprovedByEmployee.SelectedValue
        Dim ApprovedTime As String = txtApprovedTime.Text

        Dim ErrorMessage As String = ""

        Output = UpdateInventoryTransferHeader(CompanyID, DivisionID, DepartmentID, TransferNumber, TransferDate, TransferFromLocation, TransferToLocation, TransferByEmployee, ApprovedByEmployee, ApprovedTime, TotalItems)

        If Output Then
            Response.Redirect("InventoryTransferLists.aspx")
        Else
            lblErrorMessage.Visible = True
            ' lblErrorMessage.Text = ErrorMessage
        End If

    End Sub


    Public Function UpdateInventoryTransferHeader(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, _
                                                    ByVal TransferNumber As String, TransferDate As String, TansferFromLocation As String, TransferToLocation As String, _
                                                    TransferByEmployee As String, ApprovedByEmployee As String, ApprovedTime As String, TotalItemsTransfer As String) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[UpdateInventoryTransferHeader]", Connection)

                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("TransferNumber", TransferNumber)

                Command.Parameters.AddWithValue("TransferDate", TransferDate)
                Command.Parameters.AddWithValue("TansferFromLocation", TansferFromLocation)
                Command.Parameters.AddWithValue("TransferToLocation", TransferToLocation)
                Command.Parameters.AddWithValue("TransferByEmployee", TransferByEmployee)

                Command.Parameters.AddWithValue("ApprovedByEmployee", ApprovedByEmployee)
                Command.Parameters.AddWithValue("ApprovedTime", ApprovedTime)
                Command.Parameters.AddWithValue("TotalItemsTransfer", TotalItemsTransfer)

                Try
                    Command.Connection.Open()
                    Command.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    lblErrorMessage.Text = ex.Message
                    Return False
                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function




    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        Response.Redirect("InventoryTransferLists.aspx")

    End Sub

End Class

