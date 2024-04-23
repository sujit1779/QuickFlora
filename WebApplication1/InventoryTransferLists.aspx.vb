Option Strict Off

Imports EnterpriseASPClient.Core
'Imports EnterpriseClient.Core
Imports System.Data
Imports System.Data.SqlClient

Partial Class InventoryTransferLists
    Inherits System.Web.UI.Page

    Public CompanyID As String = ""
    Public DivisionID As String = ""
    Public DepartmentID As String = ""

    Dim SortDirection As String

    Dim obj As New clsInventoryTransfer


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

    Public EmployeeID As String = ""
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



    Public Function getEditVisible() As Boolean
        EmployeeID = Session("EmployeeID")

        rs = PopulateEmployees(CompanyID, DepartmentID, DivisionID, "MTEDIT")

        Dim securitycheck As Boolean = False

        While (rs.Read())

            If rs("EmployeeID").ToString() = EmployeeID Then
                securitycheck = True
                Exit While
            End If

        End While
        rs.Close()

       

        If securitycheck Then
            Return True
        Else
            Return False
        End If
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        CompanyID = CType(SessionKey("CompanyID"), String)
        DivisionID = CType(SessionKey("DivisionID"), String)
        DepartmentID = CType(SessionKey("DepartmentID"), String)

        EmployeeID = Session("EmployeeID")

        rs = PopulateEmployees(CompanyID, DepartmentID, DivisionID, "MT")

        Dim securitycheck As Boolean = False

        While (rs.Read())

            If rs("EmployeeID").ToString() = EmployeeID Then
                securitycheck = True
                Exit While
            End If

        End While
        rs.Close()

        If securitycheck = False Then
            Response.Redirect("SecurityAcessPermission.aspx?MOD=MT")
        End If

        txtcustomersearch.Attributes.Add("placeholder", "SEARCH")
        txtcustomersearch.Attributes.Add("onKeyUp", "SendQuery(this.value)")

        Dim dt As New DataTable

        If Not Page.IsPostBack Then
            FillOrderLocation(CompanyID, DivisionID, DepartmentID)

            Dim locationid_chk As String = ""
            Dim LocationIsmaster As Boolean = True

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
                        LocationIsmaster = True
                        Exit For
                    End If
                Next


            Catch ex As Exception

            End Try

            If LocationIsmaster Then
                '   dt = GetInventoryTransferList1(CompanyID, DivisionID, DepartmentID)
            Else
                '  dt = GetInventoryTransferListbylocation(CompanyID, DivisionID, DepartmentID)
            End If
            dt = GetInventoryTransferListbylocation(CompanyID, DivisionID, DepartmentID)


            If dt.Rows.Count > 0 Then
                Session("InventoryTransferLists") = dt
                InventoryTransferGrid.DataSource = dt
                InventoryTransferGrid.DataBind()
            End If

        End If

        If Not Page.IsPostBack Then
            txtDateFrom.Text = DateTime.Now.ToString("MM/dd/yyyy")
            txtDateTo.Text = DateTime.Now.ToString("MM/dd/yyyy")

        End If

    End Sub

    Public Function GetInventoryTransferList1(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, _
                                             Optional ByVal TransferNumber As String = "") As DataTable

        Dim dt As New DataTable

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetInventoryTransferListpage]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("TransferNumber", TransferNumber)
                Command.Parameters.AddWithValue("status", drpstatus.SelectedValue)

                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(dt)

                Catch ex As Exception

                End Try

            End Using
        End Using

        Return dt

    End Function


    Private Sub drpTansferFromLocaton_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drpTansferFromLocaton.SelectedIndexChanged, drpTransferToLocaton.SelectedIndexChanged, drpstatus.SelectedIndexChanged


        Dim dt As New DataTable
        'dt = GetInventoryTransferListbylocation(CompanyID, DivisionID, DepartmentID)
        'Session("InventoryTransferLists") = dt
        'InventoryTransferGrid.DataSource = dt
        'InventoryTransferGrid.DataBind()


        If rdAllDates.Checked Then
            dt = GetInventoryTransferListbylocation(CompanyID, DivisionID, DepartmentID)
        ElseIf rdOrderDates.Checked Then
            dt = GetInventoryTransferListbyTransferDate(CompanyID, DivisionID, DepartmentID)
        ElseIf rdDeliveryDates.Checked Then
            dt = GetInventoryTransferListbyReceiveDate(CompanyID, DivisionID, DepartmentID)
        End If


        Session("InventoryTransferLists") = dt
        InventoryTransferGrid.DataSource = dt
        InventoryTransferGrid.DataBind()


    End Sub



    Public Function GetInventoryTransferListbylocation(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, _
                                             Optional ByVal TransferNumber As String = "") As DataTable

        Dim dt As New DataTable


        Dim locationid_chk As String = ""
        Dim LocationIsmaster As Boolean = False

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
                    LocationIsmaster = True
                    Exit For
                End If
            Next


        Catch ex As Exception

        End Try

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetInventoryTransferListbylocationpage]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("TransferNumber", TransferNumber)
                Command.Parameters.AddWithValue("TransferFrom", drpTansferFromLocaton.SelectedValue)
                Command.Parameters.AddWithValue("TransferTo", drpTransferToLocaton.SelectedValue)
                Command.Parameters.AddWithValue("ismaster", LocationIsmaster)
                Command.Parameters.AddWithValue("status", drpstatus.SelectedValue)
                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(dt)

                Catch ex As Exception

                End Try

            End Using
        End Using

        Return dt

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
                ' drpTansferFromLocaton.SelectedIndex = drpTansferFromLocaton.Items.IndexOf(drpTansferFromLocaton.Items.FindByValue(locationid))
                ' drpTransferToLocaton.SelectedIndex = drpTransferToLocaton.Items.IndexOf(drpTransferToLocaton.Items.FindByValue(locationid))
            End If

            drpTansferFromLocaton.Items.Remove("Wholesale")
            drpTransferToLocaton.Items.Remove("Wholesale")

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
                drpTansferFromLocaton.SelectedIndex = 0 ' drpTansferFromLocaton.Items.IndexOf(drpTansferFromLocaton.Items.FindByValue(locationid))
            End If


        End If

    End Sub







    Protected Sub InventoryTransferGrid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles InventoryTransferGrid.PageIndexChanging

        InventoryTransferGrid.PageIndex = e.NewPageIndex

        Dim dt As New DataTable

        dt = obj.GetInventoryTransferList(CompanyID, DivisionID, DepartmentID)

        If dt.Rows.Count > 0 Then
            Session("InventoryTransferLists") = dt
            InventoryTransferGrid.DataSource = dt
            InventoryTransferGrid.DataBind()
        End If

    End Sub




    Protected Sub InventoryTransferGrid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles InventoryTransferGrid.RowCommand

        If e.CommandName = "MTPrint" Then
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
            qry = "insert into [MTSPrintRequest]( CompanyID, DivisionID, DepartmentID, [TerminalName]" _
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

        If e.CommandName = "CancelOrder" Then
            Dim ordernumber As String
            ordernumber = e.CommandArgument


            ' EmailNotifications(ordernumber)
            Dim StrBody As New StringBuilder()

            StrBody.Append("<table border='0' cellspacing='0' cellpadding='0'  width='80%'   id='table1'>")
            StrBody.Append("<tr    align='left'>")
            ' StrBody.Append("<td  align='left' >&nbsp;<b>This PO#" & ordernumber & "</b>&nbsp; is canceled now.</td>")
            ' StrBody.Append("<td  align='left' >&nbsp;<b>" & txtEmailContent.Text & "</b>&nbsp;</td>")

            StrBody.Append("</tr>")
            StrBody.Append("</table>")
            StrBody.Append("<hr>")

            ' EmailSendingWithhCC("[Canceled] " & txtemailsubject.Text, StrBody.ToString() & divEmailContent.InnerHtml, txtfrom.Text, txtto.Text, txtcc.Text)




            UpdateCancelOrderNumber(ordernumber)
            ' CancelPurchaseItemDetailsJsGrid(ordernumber)
            ' BindOrderHeaderList()
            Dim dt As New DataTable
            Dim locationid_chk As String = ""
            Dim LocationIsmaster As Boolean = True

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
                        LocationIsmaster = True
                        Exit For
                    End If
                Next


            Catch ex As Exception

            End Try

            If LocationIsmaster Then
                dt = obj.GetInventoryTransferList(CompanyID, DivisionID, DepartmentID)
            Else
                dt = GetInventoryTransferListbylocation(CompanyID, DivisionID, DepartmentID)
            End If



            If dt.Rows.Count > 0 Then
                Session("InventoryTransferLists") = dt
                InventoryTransferGrid.DataSource = dt
                InventoryTransferGrid.DataBind()
            End If

        End If
    End Sub


    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    Public Function UpdateCancelOrderNumber(ByVal TransferNumber As String) As Boolean
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE [InventoryTransferHeader] set [Canceled]=1,[CanceledDate]=@f6 Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And TransferNumber=@f5"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 36)).Value = TransferNumber
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



    'Protected Sub InventoryTransferGrid_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles InventoryTransferGrid.RowDeleting

    '    Dim TransferID As String = InventoryTransferGrid.DataKeys(e.RowIndex).Values(0).ToString()

    '    If obj.GetCountFutureDeliveryOrdersForTransferItem(CompanyID, DivisionID, DepartmentID, TransferID) = 0 Then
    '        obj.DeleteInventoryTransfer(CompanyID, DivisionID, DepartmentID, TransferID)
    '        Response.Redirect("NewInventoryTransferList.aspx")
    '    Else
    '        Dim objR As New Random
    '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "OpenWindow" + Convert.ToString(objR.Next(1000)), _
    '                "alert('Orders for future delivery for this Transfer present. \n you can not delete this Transfer right now');", True)
    '    End If

    'End Sub

    Protected Sub InventoryTransferGrid_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles InventoryTransferGrid.RowEditing

        Dim TransferID As String = InventoryTransferGrid.DataKeys(e.NewEditIndex).Values(0).ToString()

        If TransferID <> "" Then
            Response.Redirect(String.Format("InventoryTransferDetaila.aspx?TransferNumber={0}", TransferID))
        End If

    End Sub

    Protected Sub InventoryTransferGrid_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles InventoryTransferGrid.Sorting

        Dim dt As New DataTable

        dt = obj.GetInventoryTransferList(CompanyID, DivisionID, DepartmentID)

        Dim dv As DataView = dt.DefaultView

        If hdSortDirection.Value = "" Or hdSortDirection.Value = "DESC" Then
            hdSortDirection.Value = "ASC"
        Else
            hdSortDirection.Value = "DESC"
        End If

        dv.Sort = e.SortExpression & " " & hdSortDirection.Value

        If dt.Rows.Count > 0 Then
            InventoryTransferGrid.DataSource = dv
            InventoryTransferGrid.DataBind()
        End If

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


        txtcustomersearch.Text = TempCustomerID

        'BindOrderHeaderList()


        Dim dt As New DataTable

        dt = obj.GetInventoryTransferList(CompanyID, DivisionID, DepartmentID, TempCustomerID)

        If dt.Rows.Count > 0 Then
            Session("InventoryTransferLists") = dt
            InventoryTransferGrid.DataSource = dt
            InventoryTransferGrid.DataBind()
        End If

        txtcustomersearch.Text = "" '"dt.Rows.Count=" & dt.Rows.Count

    End Sub


    Protected Sub btnsearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsearch.Click
        Dim dt As New DataTable

        Dim locationid_chk As String = ""
        Dim LocationIsmaster As Boolean = True

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
                    LocationIsmaster = True
                    Exit For
                End If
            Next


        Catch ex As Exception

        End Try

        If rdAllDates.Checked Then
            dt = GetInventoryTransferListbylocation(CompanyID, DivisionID, DepartmentID)
        ElseIf rdOrderDates.Checked Then
            dt = GetInventoryTransferListbyTransferDate(CompanyID, DivisionID, DepartmentID)
        ElseIf rdDeliveryDates.Checked Then
            dt = GetInventoryTransferListbyReceiveDate(CompanyID, DivisionID, DepartmentID)
        End If


        Session("InventoryTransferLists") = dt
        InventoryTransferGrid.DataSource = dt
        InventoryTransferGrid.DataBind()


    End Sub


    Public Function GetInventoryTransferList(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, _
                                         Optional ByVal TransferNumber As String = "") As DataTable

        Dim dt As New DataTable

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetInventoryTransferList]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("TransferNumber", TransferNumber)

                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(dt)

                Catch ex As Exception

                End Try

            End Using
        End Using

        Return dt

    End Function


    Public Function GetInventoryTransferListbyTransferDate(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, _
                                       Optional ByVal TransferNumber As String = "") As DataTable

        Dim dt As New DataTable


        Dim locationid_chk As String = ""
        Dim LocationIsmaster As Boolean = False

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
                    LocationIsmaster = True
                    Exit For
                End If
            Next


        Catch ex As Exception

        End Try

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetInventoryTransferListbyTransferDate]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("TransferNumber", TransferNumber)
                Command.Parameters.AddWithValue("FromDate", txtDateFrom.Text)
                Command.Parameters.AddWithValue("ToDate", txtDateTo.Text)
                Command.Parameters.AddWithValue("TransferFrom", drpTansferFromLocaton.SelectedValue)
                Command.Parameters.AddWithValue("TransferTo", drpTransferToLocaton.SelectedValue)
                Command.Parameters.AddWithValue("ismaster", LocationIsmaster)
                Command.Parameters.AddWithValue("status", drpstatus.SelectedValue)
                '@ismaster
                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(dt)

                Catch ex As Exception

                End Try

            End Using
        End Using

        Return dt

    End Function

    Public Function GetInventoryTransferListbyReceiveDate(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, _
                                       Optional ByVal TransferNumber As String = "") As DataTable

        Dim dt As New DataTable


        Dim locationid_chk As String = ""
        Dim LocationIsmaster As Boolean = False

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
                    LocationIsmaster = True
                    Exit For
                End If
            Next


        Catch ex As Exception

        End Try

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetInventoryTransferListbyReceiveDate]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("TransferNumber", TransferNumber)
                Command.Parameters.AddWithValue("FromDate", txtDateFrom.Text)
                Command.Parameters.AddWithValue("ToDate", txtDateTo.Text)
                Command.Parameters.AddWithValue("TransferFrom", drpTansferFromLocaton.SelectedValue)
                Command.Parameters.AddWithValue("TransferTo", drpTransferToLocaton.SelectedValue)
                Command.Parameters.AddWithValue("ismaster", LocationIsmaster)
                Command.Parameters.AddWithValue("status", drpstatus.SelectedValue)
                Dim da As New SqlDataAdapter(Command)
                da.Fill(dt)
                Try
                Catch ex As Exception

                End Try

            End Using
        End Using

        Return dt

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

    Private Sub InventoryTransferGrid_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles InventoryTransferGrid.RowDataBound


        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblOrderNumber As New Label
            lblOrderNumber = e.Row.FindControl("lblOrderNumber")

            Dim imgEdit As New ImageButton
            imgEdit = e.Row.FindControl("imgEdit")
            Dim imgCancel As New ImageButton
            imgCancel = e.Row.FindControl("imgCancel")

            Dim lblstatus As New Label
            lblstatus = e.Row.FindControl("lblstatus")
            Dim hprReceive As New HyperLink
            hprReceive = e.Row.FindControl("hprReceive")
            hprReceive.NavigateUrl = "InventoryReceiveDetails.aspx?TransferNumber=" & lblOrderNumber.Text

            Dim lblTotalReceived As New Label
            lblTotalReceived = e.Row.FindControl("lblTotalReceived")

            Dim dt3 As New DataTable
            dt3 = POHeaderDETAILS(lblOrderNumber.Text)
            If dt3.Rows.Count <> 0 Then
                Dim qty As Integer = 0
                Dim Tqty As Integer = 0
                Dim canceled As Boolean = False
                Try
                    canceled = dt3.Rows(0)("Canceled")
                Catch ex As Exception

                End Try
                Try
                    qty = dt3.Rows(0)("Qty")
                    lblTotalReceived.Text = qty
                Catch ex As Exception

                End Try
                Try
                    Tqty = dt3.Rows(0)("TQty")
                    lblTotalReceived.Text = qty & "-" & Tqty
                Catch ex As Exception

                End Try
                Dim Received As Boolean = False
                Try
                    Received = dt3.Rows(0)("Receieved")
                Catch ex As Exception

                End Try
                If qty > 0 Then
                    Received = True
                Else
                    Received = False
                End If

                If canceled Then
                    lblstatus.Text = "Canceled"
                    lblstatus.ForeColor = Drawing.Color.Red
                    imgEdit.Visible = False
                    imgCancel.Visible = False
                    hprReceive.Visible = False
                End If


                If True Then
                    If Received Then
                        If Tqty = qty Then
                            lblstatus.Text = "Received"
                            lblstatus.ForeColor = Drawing.Color.Green
                            imgEdit.Visible = False
                            imgCancel.Visible = False
                            hprReceive.Visible = False
                        Else
                            lblstatus.Text = "Partially Received"
                            lblstatus.ForeColor = Drawing.Color.Green
                            imgEdit.Visible = False
                            imgCancel.Visible = False
                            hprReceive.Visible = True
                        End If

                    End If
                End If

            End If

        End If

    End Sub


    Public Function POHeaderDETAILS(ByVal TransferNumber As String) As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim LocationName As String = ""
        Dim dt As New DataTable

        'From [Enterprise].[dbo].[PurchaseDetail]
        'Order By [PurchaseLineNumber] DESC

        ssql = ssql & " SELECT  (select SUM(ISNULL(InventoryTransferDetail.ReceivedQty,0))  from InventoryTransferDetail Where InventoryTransferDetail.TransferNumber = InventoryTransferHeader.TransferNumber AND  InventoryTransferDetail.CompanyID  = InventoryTransferHeader.CompanyID  AND  InventoryTransferDetail.DivisionID  = InventoryTransferHeader.DivisionID AND  InventoryTransferDetail.DepartmentID  = InventoryTransferHeader.DepartmentID ) AS 'Qty',  "
        ssql = ssql & "  (select SUM(ISNULL(InventoryTransferDetail.TransferQty,0))  from InventoryTransferDetail Where InventoryTransferDetail.TransferNumber = InventoryTransferHeader.TransferNumber AND  InventoryTransferDetail.CompanyID  = InventoryTransferHeader.CompanyID  AND  InventoryTransferDetail.DivisionID  = InventoryTransferHeader.DivisionID AND  InventoryTransferDetail.DepartmentID  = InventoryTransferHeader.DepartmentID ) AS 'TQty', *  "
        ssql = ssql & " FROM [InventoryTransferHeader] Where CompanyID ='" & Me.CompanyID & "' AND DivisionID ='" & Me.DivisionID & "'  AND DepartmentID ='" & Me.DepartmentID & "' "
        ssql = ssql & " AND [TransferNumber] ='" & TransferNumber & "'   "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        da.SelectCommand = com
        da.Fill(dt)


        Return dt
    End Function

End Class
