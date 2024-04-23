Option Strict Off

Imports System.Data.SqlClient
Imports System.Data
Imports DAL
Imports EnterpriseCommon.Configuration
Imports EnterpriseASPClient.Core
Imports EnterpriseClient.Core
Imports System.Diagnostics
Imports PayPal.Payments.DataObjects
Imports PayPal.Payments.Common
Imports PayPal.Payments.Common.Utility
Imports PayPal.Payments.Transactions
Imports System.Net.Mail
Imports System.Text.RegularExpressions
Imports System.IO
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.Shared
Imports AuthorizeNet

Imports System.Web.Script.Serialization
Imports System.Web.Script.Services
Imports System.Collections.Generic


Partial Class InventoryAdjustments
    Inherits System.Web.UI.Page

    Public ConnectionString As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim EmployeeID As String = ""
    Dim PurchaseOrderNumber As String = ""

    Dim Allowed As Boolean = False
    Dim rs As SqlDataReader

    Public Bill_to_Customer_ID As String = "collapse"
    Public Ship_To As String = "collapse"

    Dim InventoryAdjustmentsNumber As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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


        txtitemsearch.Attributes.Add("placeholder", "SEARCH")
        txtitemsearch.Attributes.Add("onKeyUp", "SendQuery2(this.value)")

        ' txtQty.Attributes.Add("onKeyUp", "onblurtxtQty()")
        ' txtuom.Attributes.Add("onchange", "onblurtxtQty()")

        ' txtitemsearchprice.Attributes.Add("onKeyUp", "Processuomtotal()")
        ' txtunits.Attributes.Add("onKeyUp", "Processuomtotal()")
        ' txtitemsearchprice.Attributes.Add("onKeyUp", "Processuomtotal()")


        'Populating RetailerLogo
        Dim ImageTemp As String = ""

        Dim PopOrderType As New CustomOrder()
        rs = PopOrderType.PopulateCompanyLogo(CompanyID, DepartmentID, DivisionID)
        While (rs.Read())

            'Dim objNascheck As New clsNasImageCheck
            'ImgRetailerLogo.ImageUrl = objNascheck.retLogourl(rs("CompanyLogoUrl").ToString()) ' "~/images/" & rs("CompanyLogoUrl").ToString()

            ImgRetailerLogo.ImageUrl = "~" & returl(rs("CompanyLogoUrl").ToString())

        End While

        rs.Close()
        If Not IsPostBack Then

            PopulateDrops()
            lblTransactionDate.Text = DateTime.Now.ToShortDateString()
           

            txtOrderNumber.Text = lblTransactionNumberData.Text



            PurchaseOrderNumber = ""
            Try
                InventoryAdjustmentsNumber = Request.QueryString("InventoryAdjustmentsNumber")
            Catch ex As Exception

            End Try


            If InventoryAdjustmentsNumber <> "" Then
                BindCustomerDetails(InventoryAdjustmentsNumber)
                BindGrid()
                txtOrderNumber.Text = lblTransactionNumberData.Text
            End If




        End If


    End Sub


    Sub BindGrid()


        If lblTransactionNumberData.Text.Trim() = "(New)" Then

            Exit Sub
        End If

        Dim FillItemDetailGrid As New CustomOrder()
        Dim ds As New Data.DataSet
        ds = GetInventoryAdjustmentDetail_list(lblTransactionNumberData.Text)



        Dim tr As String = ""
        Dim n As Integer = 0
        For n = 0 To ds.Tables(0).Rows.Count - 1

          

            tr = tr & "<tr><td>" & ds.Tables(0).Rows(n)("ItemID") & "</td>"
            tr = tr & "<td>" & GetPurchaseDetail_ItemID_name(ds.Tables(0).Rows(n)("ItemID")) & "</td>"
            tr = tr & "<td>" & ds.Tables(0).Rows(n)("Qty") & "</td>"
            tr = tr & "<td>" & Format(ds.Tables(0).Rows(n)("AverageCost"), "0.00") & "</td>"
            tr = tr & "<td>" & Format(ds.Tables(0).Rows(n)("ItemTotal"), "0.00") & "</td>"
            'tr = tr & "<td><a class='edit btn btn-info btn-block btn-xs' href=''>Edit</a></td><td><a class='delete btn btn-danger btn-block btn-xs' href=''>Delete</a></td>"
            tr = tr & "<td></td><td></td>"
            tr = tr & "<td><input type=""hidden"" value=""" & ds.Tables(0).Rows(n)("rowid") & """></td></tr>"

            txtfirst.Text = 0

        Next
        tbody.InnerHtml = tr


    End Sub


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


    Public Function GetInventoryAdjustmentDetail_list(ByVal TransactionNumber As String) As Data.DataSet

        Dim Total As Decimal = 0
        Dim dt As New Data.DataSet

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "select  *  from [InventoryAdjustmentDetail]   where [InventoryAdjustmentDetail].TransactionNumber =@f31 and CompanyID = @f0 AND DivisionID = @f1 AND DepartmentID = @f2 "
        'Values (@f00,@f0,@f1,@f2,@f3,@f31,@f32,@f311,@f322)
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f31", SqlDbType.NVarChar, 36)).Value = TransactionNumber.Trim()


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



    Public Sub BindCustomerDetails(ByVal InventoryAdjustmentsNumber As String)


        Dim dt As New DataTable

        dt = InventoryAdjustmentsNumber_Details(InventoryAdjustmentsNumber)

        If (dt.Rows.Count <> 0) Then

            lblTransactionNumberData.Text = "in bind"

            
            lblTransactionNumberData.Text = InventoryAdjustmentsNumber

            drpTransaction.SelectedValue = dt.Rows(0)("TransactionType").ToString()
            drpTransaction.Enabled = False
            txtitemsearch.Enabled = False

            Dim TransactionDate As Date
            Try
                TransactionDate = dt.Rows(0)("TransactionDate")
            Catch ex As Exception

            End Try
            lblTransactionDate.Text = TransactionDate.Date

            


            drpEmployeeID.SelectedValue = dt.Rows(0)("EmployeeID").ToString()
            drpEmployeeID.Enabled = False

           CalculationPart()



            cmblocationid.SelectedValue = dt.Rows(0)("LocationID").ToString()
            cmblocationid.Enabled = False

            txtInternalNotes.Text = dt.Rows(0)("InternalNotes")

            txtInternalNotes.Enabled = False

            btnsave.Enabled = False

        End If

    End Sub


    Public Function InventoryAdjustmentsNumber_Details(ByVal InventoryAdjustmentsNumber As String) As DataTable



        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        Dim dt As New DataTable

        qry = "select  *  from  [InventoryAdjustmentHeader]   where [InventoryAdjustmentHeader].TransactionNumber =@f31 and CompanyID = @f0 AND DivisionID = @f1 AND DepartmentID = @f2 "
        'Values (@f00,@f0,@f1,@f2,@f3,@f31,@f32,@f311,@f322)
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)

        com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
        com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
        com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
        com.Parameters.Add(New SqlParameter("@f31", SqlDbType.NVarChar, 36)).Value = InventoryAdjustmentsNumber.Trim()


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


    Protected Sub ImgUpdateSearchitems_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgUpdateSearchitems.Click

        txtInternalNotes.Text = "In ord process"

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")


        If IsNumeric(lblTransactionNumberData.Text.Trim) = False Then
            Dim PopOrderNo As New CustomOrder()
            Dim rs As SqlDataReader
            rs = PopOrderNo.GetNextOrdNumber(CompanyID, DepartmentID, DivisionID, "InventoryAdjustmentsNumber")
            While rs.Read()
                PurchaseOrderNumber = rs("NextNumberValue")

            End While
            rs.Close()
            lblTransactionNumberData.Text = PurchaseOrderNumber
            Session("PurchaseOrderNumbe") = PurchaseOrderNumber
            txtOrderNumber.Text = lblTransactionNumberData.Text
            ' EmailSendingWithoutBcc(CompanyID & "- POS new PurchaseOrderNumbe" & PurchaseOrderNumbe & " - line number customer search 14224", "Existing order - " & lblOrderNumberData.Text & " - " & Date.Now, "support@quickflora.com", "imtiyazsir@gmail.com")
        End If


    End Sub


     

    Public Sub Setdropdown()

        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        Dim locationid As String = CType(SessionKey("Locationid"), String)
        If locationid <> "" Then
            cmblocationid.SelectedIndex = cmblocationid.Items.IndexOf(cmblocationid.Items.FindByValue(locationid))
        End If

        cmblocationid.Items.Remove("Wholesale")
        'Session("OrderLocationid") = cmblocationid.SelectedValue
    End Sub


    Public Sub PopulateDrops()
        Dim CompanyID As String = ""
        Dim DivisionID As String = ""
        Dim DepartmentID As String = ""
        Dim EmpID As String = ""



        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")


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
            Setdropdown()
        Else
            cmblocationid.Items.Clear()
            Dim item As New ListItem
            item.Text = "DEFAULT"
            item.Value = "DEFAULT"
            cmblocationid.Items.Add(item)
        End If
        ''''''''''''''''''''

        ''''''''''''''''''''

        Dim PopOrderType As New CustomOrder()
        rs = PopulateEmployees(CompanyID, DepartmentID, DivisionID, "IA")

        drpEmployeeID.DataTextField = "EmployeeName"
        drpEmployeeID.DataValueField = "EmployeeID"
        drpEmployeeID.DataSource = rs
        drpEmployeeID.DataBind()
        drpEmployeeID.SelectedValue = EmployeeID ' Session("EmployeeUserName")
        rs.Close()

        

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



    Protected Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click

        If IsNumeric(lblTransactionNumberData.Text.Trim) Then

            Dim _TransactionNumber As String = ""
            Try
                _TransactionNumber = Check_InventoryAdjustmentHeader_TransactionNumber(lblTransactionNumberData.Text.Trim)
            Catch ex As Exception

            End Try
            CalculationPart()

            If _TransactionNumber = "" Then
                If InsertInventoryAdjustmentHeader() Then
                    If UpdateInventoryAdjustmentStatus() Then
                        Response.Redirect("InventoryAdjustments.aspx")
                    End If

                End If
            Else
                If UpdateInventoryAdjustmentHeader() Then
                    If UpdateInventoryAdjustmentStatus() Then
                        Response.Redirect("InventoryAdjustments.aspx")
                    End If
                End If
            End If
        End If




    End Sub




    Public Function UpdateInventoryAdjustmentStatus() As Boolean

        Using connection As New SqlConnection(ConnectionString)
            Using Command As New SqlCommand("[enterprise].[UpdateInventoryAdjustmentStatus]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", Me.CompanyID)
                Command.Parameters.AddWithValue("DivisionID", Me.DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", Me.DepartmentID)
                Command.Parameters.AddWithValue("TransactionNumber", lblTransactionNumberData.Text.Trim)
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


    Public Function InsertInventoryAdjustmentHeader() As Boolean

        Using connection As New SqlConnection(ConnectionString)
            Using Command As New SqlCommand("[enterprise].[InsertInventoryAdjustmentHeader]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", Me.CompanyID)
                Command.Parameters.AddWithValue("DivisionID", Me.DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", Me.DepartmentID)

                Command.Parameters.AddWithValue("TransactionNumber", lblTransactionNumberData.Text.Trim)
                Command.Parameters.AddWithValue("TransactionDate", lblTransactionDate.Text)
                Command.Parameters.AddWithValue("TransactionType", drpTransaction.SelectedValue)

                Command.Parameters.AddWithValue("LocationID", cmblocationid.SelectedValue)
                Command.Parameters.AddWithValue("EmployeeID", drpEmployeeID.SelectedValue)

                Command.Parameters.AddWithValue("InternalNotes", txtInternalNotes.Text)

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


    Public Function UpdateInventoryAdjustmentHeader() As Boolean

        Using connection As New SqlConnection(ConnectionString)
            Using Command As New SqlCommand("[enterprise].[UpdateInventoryAdjustmentHeader]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", Me.CompanyID)
                Command.Parameters.AddWithValue("DivisionID", Me.DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", Me.DepartmentID)

                Command.Parameters.AddWithValue("TransactionNumber", lblTransactionNumberData.Text.Trim)
                Command.Parameters.AddWithValue("TransactionDate", lblTransactionDate.Text)
                Command.Parameters.AddWithValue("TransactionType", drpTransaction.SelectedValue)

                Command.Parameters.AddWithValue("LocationID", cmblocationid.SelectedValue)
                Command.Parameters.AddWithValue("EmployeeID", drpEmployeeID.SelectedValue)

                Command.Parameters.AddWithValue("InternalNotes", txtInternalNotes.Text)

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


    Public Function Check_InventoryAdjustmentHeader_TransactionNumber(ByVal TransactionNumber As String) As String

        Dim PurchaseNumber As String = ""

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "select  [TransactionNumber]  from [InventoryAdjustmentHeader]   where [InventoryAdjustmentHeader].TransactionNumber =@f31 and CompanyID = @f0 AND DivisionID = @f1 AND DepartmentID = @f2 "
        'Values (@f00,@f0,@f1,@f2,@f3,@f31,@f32,@f311,@f322)
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f31", SqlDbType.NVarChar, 36)).Value = TransactionNumber.Trim()

            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(com)

            da.Fill(dt)


            If (dt.Rows.Count <> 0) Then

                Try
                    PurchaseNumber = dt.Rows(0)(0)
                Catch ex As Exception
                    Dim msg As String
                    msg = ex.Message
                    'HttpContext.Current.Response.Write(msg)
                End Try

            End If

            Return PurchaseNumber

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)

        End Try

        Return PurchaseNumber

    End Function




    Protected Sub CalculationPart()
        Dim SubTotalToDisplay As Decimal = 0

        SubTotalToDisplay = InventoryAdjustmentDetail_Total(lblTransactionNumberData.Text)

        txtSubtotal.Text = Format(SubTotalToDisplay, "0.00")



    End Sub

    Public Function InventoryAdjustmentDetail_Total(ByVal TransactionNumber As String) As Decimal

        Dim Total As Decimal = 0

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "select  SUM([ItemTotal])  from [InventoryAdjustmentDetail]   where [InventoryAdjustmentDetail].TransactionNumber =@f31 and CompanyID = @f0 AND DivisionID = @f1 AND DepartmentID = @f2 "
        'Values (@f00,@f0,@f1,@f2,@f3,@f31,@f32,@f311,@f322)
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f31", SqlDbType.NVarChar, 36)).Value = TransactionNumber.Trim()

            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(com)

            da.Fill(dt)


            If (dt.Rows.Count <> 0) Then

                Try
                    Total = dt.Rows(0)(0)
                Catch ex As Exception
                    Dim msg As String
                    msg = ex.Message
                    'HttpContext.Current.Response.Write(msg)
                End Try

            End If

            Return Total

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)

        End Try

        Return Total

    End Function


    
 
End Class
