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
Imports System.Net


Imports System.Text

Imports Amazon
Imports Amazon.EC2
Imports Amazon.EC2.Model
Imports Amazon.SimpleDB
Imports Amazon.SimpleDB.Model
Imports Amazon.S3
Imports Amazon.S3.Model
Imports Amazon.SimpleEmail
Imports Amazon.SimpleEmail.Model


Partial Class emailtovendor
    Inherits System.Web.UI.Page



    Dim OrderNumber As String
    Dim ConnectionString As String = ""

    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim filters As EnterpriseCommon.Core.FilterSet

        filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
        CompanyID = filters!CompanyID
        DivisionID = filters!DivisionID
        DepartmentID = filters!DepartmentID

        If Not IsNothing(Request.QueryString("PurchaseNumber")) Then
            OrderNumber = Request.QueryString("PurchaseNumber")
            lblordernumber.Text = OrderNumber
            If Not IsPostBack Then
                drpEmailTypes_SelectedIndexChanged(sender, e)
            End If
        End If

    End Sub


    Protected Sub drpEmailTypes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpEmailTypes.SelectedIndexChanged
        EmailNotifications(OrderNumber)
    End Sub


    Public Sub EmailNotifications(ByVal OrdNumber As String)



        Dim PostOrder As New DAL.CustomOrder()
        Dim EmailType As String = "Order Placed"
        Dim EmailContent As String = ""
        Dim EmailSubject As String = ""
        Dim EmailAllowed As Boolean = True

        Dim rs As SqlDataReader
        rs = PostOrder.PopulateEmailContent(CompanyID, DivisionID, DepartmentID)
        If rs.HasRows = True Then

            While rs.Read

                EmailType = rs("EmailType").ToString()
                EmailContent = rs("EmailContent").ToString()
                EmailSubject = rs("EmailSubject").ToString()
                'EmailContent = EmailContent & "<br><br><p class='MsoNormal' style='margin: 0in 0in 0pt' align='justify'><font face='Verdana' size='2'>Powered by Sunflower Technologies, Inc 323.735.7272</font></p>"

                If EmailType <> "Order Notification to Vendor" Then
                    'txtemailsubject.Text = txtemailsubject.Text & EmailType & "--"
                    Continue While
                End If


                PopulateEmailForVendorsForItems("Order Notification to Vendor", EmailContent, EmailSubject, OrdNumber)



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

        If Me.CompanyID = "DierbergsMarkets,Inc63017" Then
            qry = "select  ItemName + ' [UPC-' + ISNULL([InventoryItems].ItemUPCCode,'') + ']'  from [InventoryItems]   where [InventoryItems].ItemID =@f31 and CompanyID = @f0 AND DivisionID = @f1 AND DepartmentID = @f2 "
        Else
            qry = "select  ItemName    from [InventoryItems]   where [InventoryItems].ItemID =@f31 and CompanyID = @f0 AND DivisionID = @f1 AND DepartmentID = @f2 "
        End If
        '  qry = "select  ItemName + ' [UPC-' + ISNULL([InventoryItems].ItemUPCCode,'') + ']'  from [InventoryItems]   where [InventoryItems].ItemID =@f31 and CompanyID = @f0 AND DivisionID = @f1 AND DepartmentID = @f2 "
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
        EmailContent = EmailContent.Replace("~shippingcustomername~", "McCarthy Group Florists")

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
        EmailContent = EmailContent.Replace("~link~", "<a  target='_blank'  href='https://reports.quickflora.com/reports/scripts/POReport.aspx?CompanyID=" & Me.CompanyID & "&DivisionID=" & Me.DivisionID & "&DepartmentID=" & Me.DepartmentID & "&PurchaseNumber=" & PurchaseOrderNumber & "' >Click to Open</a>")


        'IPAddress = Request.ServerVariables("REMOTE_ADDR")
        'EmailContent = EmailContent.Replace("~IpAddress~", IPAddress)
        'EmailContent = EmailContent.Replace("~WebsiteAddress~", "<a href='" & WebsiteAddress & "'>" & WebsiteAddress & "</a>")
        'EmailContent = EmailContent.Replace("~Ship to Attention~", ShipAttention)
        'EmailContent = EmailContent.Replace("~Ship to Company~", ShipCompany)

        EmailContent = EmailContent.Replace("Artistic-Flowers", "Jennies of St Pete")
        Dim OrderPlacedSubject As String
        Dim OrderPlacedContent As String




        OrderPlacedSubject = EmailSubject
        OrderPlacedContent = EmailContent


        Dim CompanyEmail As String = ""

        Dim ToAddress As String = vendoremail
        Dim FromAddress As String = CompanyEmail


        OrderPlacedSubject = EmailSubject
        OrderPlacedContent = EmailContent

        Dim str_PurchaseOrderNumber As String = ""
        str_PurchaseOrderNumber = GetPO_PurchaseOrderNumber_Details(PurchaseOrderNumber)

        If str_PurchaseOrderNumber <> "" Then
            OrderPlacedSubject = OrderPlacedSubject & str_PurchaseOrderNumber
        End If
        OrderPlacedSubject = OrderPlacedSubject.Replace("Artistic-Flowers", "Jennies of St Pete")
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



    '--------------------'

    Dim EmployeeID As String = ""




    Public Function UpdateVendor() As String
        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand("[dbo].[UpdateVendorEmail]", myCon)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
        parameterCompanyID.Value = Me.CompanyID
        myCommand.Parameters.Add(parameterCompanyID)

        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
        parameterDepartmentID.Value = Me.DepartmentID
        myCommand.Parameters.Add(parameterDepartmentID)

        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
        parameterDivisionID.Value = Me.DivisionID
        myCommand.Parameters.Add(parameterDivisionID)

        Dim pVendorID As New SqlParameter("@VendorID", Data.SqlDbType.NVarChar)
        pVendorID.Value = txtvendorid.Text
        myCommand.Parameters.Add(pVendorID)


        Dim pVendorEmail As New SqlParameter("@VendorEmail", Data.SqlDbType.NVarChar)
        pVendorEmail.Value = txtto.Text
        myCommand.Parameters.Add(pVendorEmail)


        myCon.Open()

        myCommand.ExecuteNonQuery()

        myCon.Close()

        Return ""

    End Function

    Public Function UpdateSentStatus(ByVal EmployeeID As String, ByVal po As String) As Boolean
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        Dim com As SqlCommand

        If String.IsNullOrEmpty(EmployeeID) Then
            EmployeeID = ""
        End If

        qry = "INSERT INTO [Enterprise].[dbo].[PurchaseOrderEmailStatus] ([CompanyID],[DivisionID],[DepartmentID],[PurchaseNumber],[Sent],[SentBy]) "
        qry = qry + "  VALUES(@f0,@f1,@f2,@po,1,@emp)  "
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.AddWithValue("@po", po)
            com.Parameters.AddWithValue("@emp", EmployeeID)
            com.Parameters.AddWithValue("@f0", Me.CompanyID)
            com.Parameters.AddWithValue("@f1", Me.DivisionID)
            com.Parameters.AddWithValue("@f2", Me.DepartmentID)

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            lblconfirmationerr.Text = msg
            'HttpContext.Current.Response.Write(msg)
            Return False
        End Try
        Return True
    End Function

    Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click

        If txtemailsubject.Text <> "" And txtfrom.Text <> "" And txtto.Text <> "" Then

            'If checkOrdermodified.Checked Then
            '    txtemailsubject.Text = "[Modified] " & txtemailsubject.Text
            'Else
            '    txtemailsubject.Text = "[Duplicate] " & txtemailsubject.Text
            'End If
            Dim emp As String = ""

            Dim StrBody As New StringBuilder()

            StrBody.Append("<table border='0' cellspacing='0' cellpadding='0'  width='80%'   id='table1'>")
            StrBody.Append("<tr    align='left'>")

            StrBody.Append("<td  align='left' >&nbsp;<b>" & txtEmailContent.Text & "</b>&nbsp;</td>")

            StrBody.Append("</tr>")
            StrBody.Append("</table>")
            StrBody.Append("<hr>")

            EmailSendingWithhCC(txtemailsubject.Text, StrBody.ToString() & divEmailContent.InnerHtml, txtfrom.Text, txtto.Text, txtcc.Text)

            Try
                emp = Session("EmployeeID")
            Catch ex As Exception

            End Try

            If CompanyID = "QuickfloraDemo" Or CompanyID = "FarmDirect" Or CompanyID = "FarmDirectTS" Then
                UpdateSentStatus(emp, lblordernumber.Text)

            End If

            If txtto.Text.Trim <> "" Then
                '  UpdateVendor()
            End If

            lblconfirmation.ForeColor = Drawing.Color.Green
            lblconfirmation.Text = "Order mail sent "
            drpEmailTypes.Enabled = False
            BtnSubmit.Visible = False
            Button1.Visible = False
            table1.Visible = False
            table3.Visible = True
        Else
            lblconfirmation.ForeColor = Drawing.Color.Red
            lblconfirmation.Text = "Email subject , Email From and Email To feild can not be blank"
        End If

    End Sub



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

        Dim lblerrortestmail As New TextBox
        Dim filters As EnterpriseCommon.Core.FilterSet
        filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
        CompanyID = filters!CompanyID
        DivisionID = filters!DivisionID
        DepartmentID = filters!DepartmentID

        Dim obj_InsertOutGoingMailDetails As New clsMailServer
        obj_InsertOutGoingMailDetails.CompanyID = CompanyID
        obj_InsertOutGoingMailDetails.DivisionID = DivisionID
        obj_InsertOutGoingMailDetails.DepartmentID = DepartmentID


        Try

            obj_InsertOutGoingMailDetails.From_Email = Email.From.ToString
            obj_InsertOutGoingMailDetails.To_Email = Email.To.ToString
            obj_InsertOutGoingMailDetails.CC_Email = Email.CC.ToString
            obj_InsertOutGoingMailDetails.Email_Subject = Email.Subject.ToString
            obj_InsertOutGoingMailDetails.Email_Body = Email.Body.ToString


            Dim Host As String = ""
            Dim Port As String = ""

            Dim NetworkCredential_username As String = ""
            Dim NetworkCredential_password As String = ""

            Dim Host2 As String = ""
            Dim Port2 As String = ""

            Dim NetworkCredential_username2 As String = ""
            Dim NetworkCredential_password2 As String = ""


            Dim obj As New clsMailServer
            obj.CompanyID = CompanyID
            obj.DivisionID = DivisionID
            obj.DepartmentID = DepartmentID
            Dim dt As New Data.DataTable
            dt = obj.FillDetails

            If dt.Rows.Count <> 0 Then

                Host = dt.Rows(0)("MailServer1")
                Port = dt.Rows(0)("MailServerPort1")
                NetworkCredential_username = dt.Rows(0)("MailServerUserName1")
                NetworkCredential_password = dt.Rows(0)("MailServerPassword1")


                Host2 = dt.Rows(0)("MailServer2")
                Port2 = dt.Rows(0)("MailServerPort2")
                NetworkCredential_username2 = dt.Rows(0)("MailServerUserName2")
                NetworkCredential_password2 = dt.Rows(0)("MailServerPassword2")


                ''New code going to put
                Dim AccessKeyId As String = ""
                Dim SecrectAccesskey As String = ""
                Dim chkAmazonmail As Boolean = False
                Try
                    AccessKeyId = dt.Rows(0)("AccessKeyId")
                    SecrectAccesskey = dt.Rows(0)("SecrectAccesskey")
                    chkAmazonmail = dt.Rows(0)("chkAmazonmail")
                Catch ex As Exception

                End Try

                If chkAmazonmail Then

                    Dim body As String = Email.Body & "*" ' txtMessage.Text
                    Dim subject As String = Email.Subject 'txtSubject.Text


                    Dim client As New AmazonSimpleEmailServiceClient(AccessKeyId, SecrectAccesskey)
                    Dim sesemail As New Amazon.SimpleEmail.Model.SendEmailRequest()


                    sesemail.Message = New Amazon.SimpleEmail.Model.Message()
                    sesemail.Message.Body = New Amazon.SimpleEmail.Model.Body()
                    sesemail.Message.Body.Html = New Amazon.SimpleEmail.Model.Content(body)
                    sesemail.Message.Subject = New Amazon.SimpleEmail.Model.Content(subject)

                    Dim dst As New Destination()
                    Dim ToAddresses() As String = {Email.To(0).ToString}
                    Dim tolst As New System.Collections.Generic.List(Of String)(ToAddresses)

                    Try
                        If Email.CC(0).ToString.Trim <> "" Then
                            Dim CCAddresses() As String = {Email.CC(0).ToString}
                            Dim CClst As New System.Collections.Generic.List(Of String)(CCAddresses)
                            dst.CcAddresses = CClst
                        End If
                    Catch ex As Exception

                    End Try

                    dst.ToAddresses = tolst
                    sesemail.WithDestination(dst)
                    sesemail.WithSource(Email.From.ToString)
                    sesemail.WithReturnPath(Email.From.ToString)
                    Dim resp As New Amazon.SimpleEmail.Model.SendEmailResponse

                    Try
                        resp = client.SendEmail(sesemail)
                        lblerrortestmail.Text = "Mail Sent With Amazon Mail Services Details"
                        Exit Sub
                    Catch ex As Exception
                        lblerrortestmail.Text = "Error occured while send email by Amazon Mail Services :" & ex.Message
                        'Exit Sub
                    End Try

                End If

                '''


                If Host.Trim <> "" Then
                    Dim mailClient As New System.Net.Mail.SmtpClient()

                    'This object stores the authentication values

                    If NetworkCredential_username.Trim <> "" And NetworkCredential_password.Trim <> "" Then
                        Dim basicAuthenticationInfo As New System.Net.NetworkCredential(NetworkCredential_username.Trim, NetworkCredential_password.Trim)
                        'mailClient.UseDefaultCredentials = False
                        mailClient.DeliveryMethod = SmtpDeliveryMethod.Network
                        mailClient.Credentials = basicAuthenticationInfo
                    Else
                        mailClient.UseDefaultCredentials = True
                    End If

                    'Put your own, or your ISPs, mail server name onthis next line


                    mailClient.Host = Host.Trim
                    If Port.Trim <> "" Then
                        mailClient.Port = Port.Trim
                    End If

                    Try
                        obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                        obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port

                        mailClient.Send(Email)

                        '''''Email Details storing''''
                        obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                        obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port
                        obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
                        '''''Email Details storing''''

                        lblerrortestmail.Text = "Mail Sent With Primary SMTP Details"

                    Catch ex As SmtpException
                        Dim Email_Subject1 As String = obj_InsertOutGoingMailDetails.Email_Subject

                        '''''Email Details storing''''
                        obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                        obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port
                        obj_InsertOutGoingMailDetails.Email_Subject = "(Failed To Send Error: " & ex.Message & "  ) " & obj_InsertOutGoingMailDetails.Email_Subject
                        obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
                        '''''Email Details storing''''

                        obj_InsertOutGoingMailDetails.Email_Subject = Email_Subject1

                        lblerrortestmail.Text = "Error From PRIMARY SMTP:" & ex.Message
                        If Host2.Trim <> "" Then
                            mailClient = New System.Net.Mail.SmtpClient()
                            'This object stores the authentication values

                            If NetworkCredential_username2.Trim <> "" And NetworkCredential_password2.Trim <> "" Then
                                Dim basicAuthenticationInfo As New System.Net.NetworkCredential(NetworkCredential_username2.Trim, NetworkCredential_password2.Trim)
                                'mailClient.UseDefaultCredentials = False
                                mailClient.DeliveryMethod = SmtpDeliveryMethod.Network
                                mailClient.Credentials = basicAuthenticationInfo
                            Else
                                mailClient.UseDefaultCredentials = True
                            End If

                            'Put your own, or your ISPs, mail server name onthis next line


                            mailClient.Host = Host2.Trim
                            If Port2.Trim <> "" Then
                                mailClient.Port = Port2.Trim
                            End If

                            obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                            obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port

                            mailClient.Send(Email)
                            '''''Email Details storing''''
                            obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                            obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port
                            obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
                            '''''Email Details storing''''
                            lblerrortestmail.Text = lblerrortestmail.Text & "<br>" & "Mail Sent With Secondry SMTP Details"
                        End If

                    End Try



                Else
                    If Host2.Trim <> "" Then
                        Dim mailClient As New System.Net.Mail.SmtpClient()
                        'This object stores the authentication values

                        If NetworkCredential_username2.Trim <> "" And NetworkCredential_password2.Trim <> "" Then
                            Dim basicAuthenticationInfo As New System.Net.NetworkCredential(NetworkCredential_username2.Trim, NetworkCredential_password2.Trim)
                            mailClient.UseDefaultCredentials = False
                            mailClient.Credentials = basicAuthenticationInfo
                        Else
                            mailClient.UseDefaultCredentials = True
                        End If

                        'Put your own, or your ISPs, mail server name onthis next line


                        mailClient.Host = Host2.Trim
                        If Port2.Trim <> "" Then
                            mailClient.Port = Port2.Trim
                        End If

                        obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                        obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port

                        mailClient.Send(Email)
                        '''''Email Details storing''''
                        obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                        obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port
                        obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
                        '''''Email Details storing''''
                        lblerrortestmail.Text = lblerrortestmail.Text & "<br>" & "Mail Sent With Secondry SMTP Details"

                    Else

                        'Dim mailClient As New System.Net.Mail.SmtpClient()
                        ''This object stores the authentication values
                        ''mailClient.UseDefaultCredentials = True
                        ''Put your own, or your ISPs, mail server name onthis next line
                        ''mailClient.Host = "8.3.16.126"
                        ''mailClient.Port = "25"
                        'mailClient.Send(Email)
                        ''''''Email Details storing''''
                        'obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                        'obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port
                        'obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
                        '''''Email Details storing''''
                        lblerrortestmail.Text = "Mail Not Sent With SMTP Details"

                    End If
                End If



            Else

                Dim mailClient As New System.Net.Mail.SmtpClient()
                'This object stores the authentication values
                'mailClient.UseDefaultCredentials = True
                'Put your own, or your ISPs, mail server name onthis next line
                mailClient.Host = "8.3.16.126"
                mailClient.Port = "25"
                mailClient.Send(Email)
                '''''Email Details storing''''
                obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port
                obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
                ''''Email Details storing''''
                lblerrortestmail.Text = "Mail Not Sent With SMTP Details"

            End If

        Catch ex As FormatException

            '''''Email Details storing''''
            obj_InsertOutGoingMailDetails.Email_Subject = "(Failed To Send Format Exception Error: " & ex.Message & "  ) " & obj_InsertOutGoingMailDetails.Email_Subject
            obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
            '''''Email Details storing''''

            lblerrortestmail.Text = ("Format Exception: " & ex.Message)

        Catch ex As SmtpException

            '''''Email Details storing''''
            obj_InsertOutGoingMailDetails.Email_Subject = "(Failed To Send SMTP Exception Error: " & ex.Message & "  ) " & obj_InsertOutGoingMailDetails.Email_Subject
            obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
            '''''Email Details storing''''
            lblerrortestmail.Text = ("SMTP Exception:  " & ex.Message)

        Catch ex As Exception

            '''''Email Details storing''''
            obj_InsertOutGoingMailDetails.Email_Subject = "(Failed To Send General Exception Error: " & ex.Message & "  ) " & obj_InsertOutGoingMailDetails.Email_Subject
            obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
            '''''Email Details storing''''

            lblerrortestmail.Text = ("General Exception:  " & ex.Message)

        End Try
    End Sub

    Protected Sub btnback_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnback.Click
        Response.Redirect("OrderList.aspx")

    End Sub

    Protected Sub btnmore_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnmore.Click

        Response.Redirect("emailtovendor.aspx?PurchaseNumber=" & OrderNumber)
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Response.Redirect("OrderList.aspx")

    End Sub


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




End Class
