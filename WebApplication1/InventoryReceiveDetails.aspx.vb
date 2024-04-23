Imports System.Data
Imports System.Data.SqlClient


Partial Class InventoryReceiveDetails
    Inherits System.Web.UI.Page


    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim EmployeeID As String = ""
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


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim filters As EnterpriseCommon.Core.FilterSet
        filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
        CompanyID = filters!CompanyID
        DivisionID = filters!DivisionID
        DepartmentID = filters!DepartmentID
        EmployeeID = Session("EmployeeID")
        Dim TransferNumber As String = ""

        'txtItemID.Attributes.Add("onKeyUp", "SendQueryForTransfer(this.value)")


        'Populating RetailerLogo
        Dim ImageTemp As String = ""

        Dim rs As SqlDataReader
        rs = PopulateCompanyLogo(CompanyID, DepartmentID, DivisionID)
        While (rs.Read())

            'Dim objNascheck As New clsNasImageCheck
            'ImgRetailerLogo.ImageUrl = objNascheck.retLogourl(rs("CompanyLogoUrl").ToString()) ' "~/images/" & rs("CompanyLogoUrl").ToString()

            ImgRetailerLogo.ImageUrl = "~" & returl(rs("CompanyLogoUrl").ToString())

        End While

        rs.Close()


        If Not Page.IsPostBack Then
            txtReceivedDate.Text = DateTime.Now.Date.ToShortDateString
            FillOrderLocation(CompanyID, DivisionID, DepartmentID)
            FillEmployee()
            drpReceivedByEmployee.SelectedValue = EmployeeID
            If Request.QueryString("TransferNumber") <> String.Empty Then
                TransferNumber = Request.QueryString("TransferNumber")
                GetInventoryTransferInformation(TransferNumber)
            End If

        End If

        lblErrorMessage.Visible = False

    End Sub

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
            Try
                txtReceivedDate.Text = Convert.ToDateTime(row("ReceivedDate")).ToShortDateString
            Catch ex As Exception

            End Try

            FillInvenntoryTransferItems(TransferNumber)

        End If

    End Sub

    Private Sub FillInvenntoryTransferItems(ByVal TransferNumber As String)

        Dim dt As New DataTable
        dt = obj.GetInventoryTransferItemsList(CompanyID, DivisionID, DepartmentID, TransferNumber)

        If dt.Rows.Count > 0 Then

            InventoryTransferItemGrid.DataSource = dt
            InventoryTransferItemGrid.DataBind()
            InventoryTransferItemGrid.Visible = True

            'lblTotalNumberOfItems.Text = dt.Rows.Count

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

        'Dim dt As New DataTable
        'dt = obj.GetEmployees(CompanyID, DepartmentID, DivisionID)
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

        '    With drpReceivedByEmployee
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



        Dim rs2 As SqlDataReader
        rs2 = PopulateEmployees(CompanyID, DepartmentID, DivisionID, "MT")

        drpReceivedByEmployee.DataTextField = "EmployeeName"
        drpReceivedByEmployee.DataValueField = "EmployeeID"
        drpReceivedByEmployee.DataSource = rs2
        drpReceivedByEmployee.DataBind()
        drpReceivedByEmployee.Items.Insert(0, (New ListItem("-Select-", "0")))
        rs2.Close()

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

    'Protected Sub InventoryTransferItemGrid_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles InventoryTransferItemGrid.RowDeleting

    '    Dim RowID As String = InventoryTransferItemGrid.DataKeys(e.RowIndex).Values(0).ToString()

    '    obj.DeleteInventoryTransferDetail(RowID)

    '    FillInvenntoryTransferItems(txtTransferNumber.Text)

    'End Sub

    'Protected Sub InventoryTransferItemGrid_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles InventoryTransferItemGrid.RowEditing

    '    InventoryTransferItemGrid.EditIndex = e.NewEditIndex
    '    FillInvenntoryTransferItems(txtTransferNumber.Text)
    '    InventoryTransferItemGrid.Rows(e.NewEditIndex).BackColor = Drawing.Color.YellowGreen

    'End Sub

    'Protected Sub InventoryTransferItemGrid_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles InventoryTransferItemGrid.RowUpdating

    '    Dim RowID As Label = CType(InventoryTransferItemGrid.Rows(e.RowIndex).FindControl("lblUpdateRowID"), Label)
    '    Dim TransferQty As Label = CType(InventoryTransferItemGrid.Rows(e.RowIndex).FindControl("lblUpdateQty"), Label)

    '    Dim ReceivedQty As TextBox = CType(InventoryTransferItemGrid.Rows(e.RowIndex).FindControl("txtUpdateReceivedQty"), TextBox)

    '    If ReceivedQty.Text.Trim <> "" Then

    '        If Not IsNumeric(ReceivedQty.Text) Then

    '            Dim objRandom As New Random
    '            Page.ClientScript.RegisterStartupScript(Me.GetType(), "OpenWindow" + _
    '                                                    Convert.ToString(objRandom.Next(1000)), _
    '                                                    "alert('Received Qty is not numeric.');", True)

    '            Exit Sub

    '        ElseIf Convert.ToDouble(ReceivedQty.Text) > Convert.ToDouble(TransferQty.Text) Then

    '            Dim objRandom As New Random
    '            Page.ClientScript.RegisterStartupScript(Me.GetType(), "OpenWindow" + _
    '                                                    Convert.ToString(objRandom.Next(1000)), _
    '                                                    "alert('Received Qty can not be more than Transferred Qty.');", True)

    '            Exit Sub
    '        Else
    '            obj.UpdateInventoryReceiveDetail(RowID.Text, ReceivedQty.Text)
    '        End If

    '    End If

    '        InventoryTransferItemGrid.EditIndex = -1
    '        FillInvenntoryTransferItems(txtTransferNumber.Text)

    'End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        'Update Inventory from Grid 
        If Not CheckReceivedInventory() Then
            Dim objRandom As New Random
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "OpenWindow" + _
                                                    Convert.ToString(objRandom.Next(1000)), _
                                                    "alert('Received Qty is not numeric or more than transffered qty.');", True)
            Exit Sub
        End If

        UpdateReceivedInventory()

        Dim Output As Boolean = False

        Dim TransferNumber As String = txtTransferNumber.Text
        Dim TransferToLocation As String = drpTransferToLocaton.SelectedValue
        Dim ReceivedByEmployee As String = drpReceivedByEmployee.SelectedValue
        Dim ReceivedDate As String = txtReceivedDate.Text

        If txtReceivedDate.Text.Trim = "" Then
            ReceivedDate = DateTime.Now.Date

        End If

        Dim ErrorMessage As String = ""

        Output = obj.UpdateInventoryReceiveHeader(CompanyID, DivisionID, DepartmentID, TransferNumber, ReceivedDate, TransferToLocation, ReceivedByEmployee)

        If Output Then
            Response.Redirect("InventoryTransferLists.aspx")
        Else
            lblErrorMessage.Visible = True
            lblErrorMessage.Text = ErrorMessage
        End If

    End Sub

    Private Function CheckReceivedInventory() As Boolean

        Dim checkQty As Boolean = True

        For Each row As GridViewRow In InventoryTransferItemGrid.Rows
            Dim RowID As String = (InventoryTransferItemGrid.DataKeys(row.RowIndex).Value)
            Dim ReceivedQuantity As TextBox = row.FindControl("txtUpdateReceivedQty")
            Dim TransferQuantity As Label = row.FindControl("lblQty")

            If Not IsNumeric(ReceivedQuantity.Text) Then

                'Dim objRandom As New Random
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "OpenWindow" + _
                '                                        Convert.ToString(objRandom.Next(1000)), _
                '                                        "alert('Received Qty is not numeric.');", True)

                ReceivedQuantity.BackColor = Drawing.Color.OrangeRed
                checkQty = False

                'Exit Sub

            ElseIf Convert.ToDouble(ReceivedQuantity.Text) > Convert.ToDouble(TransferQuantity.Text) Or Convert.ToDouble(ReceivedQuantity.Text) < 0 Then

                'Dim objRandom As New Random
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "OpenWindow" + _
                '                                        Convert.ToString(objRandom.Next(1000)), _
                '                                        "alert('Received Qty can not be more than Transferred Qty.');", True)

                ReceivedQuantity.BackColor = Drawing.Color.OrangeRed
                checkQty = False
                'Exit Sub

            Else
                ReceivedQuantity.BackColor = Drawing.Color.White
            End If

        Next

        Return checkQty

    End Function

    Private Sub UpdateReceivedInventory()

        For Each row As GridViewRow In InventoryTransferItemGrid.Rows
            Dim RowID As String = (InventoryTransferItemGrid.DataKeys(row.RowIndex).Value)
            Dim ReceivedQuantity As TextBox = row.FindControl("txtUpdateReceivedQty")
            Dim TransferQuantity As Label = row.FindControl("lblQty")
            Dim lblItemID As Label = row.FindControl("lblItemID")
            Try
                UpdateInventoryReceiveLogs(RowID, ReceivedQuantity.Text, lblItemID.Text)
            Catch ex As Exception

            End Try
            obj.UpdateInventoryReceiveDetail(RowID, ReceivedQuantity.Text)

        Next

    End Sub



    Public Function UpdateInventoryReceiveLogs(ByVal RowID As String, ReceivedQty As String, itemid As String) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[UpdateInventoryTransferReceivedQtyLogs]", Connection)

                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("RowID", RowID)
                Command.Parameters.AddWithValue("ReceivedQty", ReceivedQty)
                Command.Parameters.AddWithValue("ItemID", itemid)
                Command.Parameters.AddWithValue("TransferNumber", txtTransferNumber.Text)
                Command.Parameters.AddWithValue("CompanyID", Me.CompanyID)
                Command.Parameters.AddWithValue("DivisionID", Me.DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", Me.DepartmentID)


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




    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        Response.Redirect("InventoryTransferLists.aspx")

    End Sub

    Protected Sub btnGetTransferDetail_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetTransferDetail.Click

        GetInventoryTransferInformation(txtTransferNumber.Text)

    End Sub

End Class

