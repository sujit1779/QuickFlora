Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports
Imports System.IO
Imports System.Configuration
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine

Public Class clsPrintPDFCreation



    Public OrderNumber As String = ""

    Public ConnectionString As String = ""
    Public CompanyID As String = ""
    Public DivisionID As String = ""
    Public DepartmentID As String = ""
    Public EmployeeID As String = ""
    Public pdffilename As String


    Dim OrderLineNumber As String = ""
    Dim i As Integer
    Dim AddOnItems As String

    Public Sub EnclosurecardPDF()
        Dim rptDoc As New CrystalDecisions.CrystalReports.Engine.ReportDocument

        'OrderNumber = Request.QueryString("OrderNumber")

        Dim ImgPath As String = ConfigurationManager.AppSettings("DocPath")

        If OrderNumber = "" Then
            OrderNumber = "*"
        End If


        ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")

        'CompanyID = Session("CompanyID")
        'DivisionID = Session("DivisionID")
        'DepartmentID = Session("DepartmentID")


        Dim myConnection As New SqlConnection(ConnectionString)

        Dim CommandText As String = "enterprise.RptDocOrderHeaderSingleSection"
        Dim myCommand As New SqlCommand(CommandText, myConnection)
        Dim workParam As New SqlParameter()

        myCommand.CommandType = CommandType.StoredProcedure

        ' Set the input parameter, companyid, divisionid, departmentid
        ' these parameters are set in the sub page_load
        myCommand.Parameters.Add("@CompanyID", SqlDbType.NVarChar).Value = CompanyID
        myCommand.Parameters.Add("@DivisionID", SqlDbType.NVarChar).Value = DivisionID
        myCommand.Parameters.Add("@DepartmentID", SqlDbType.NVarChar).Value = DepartmentID
        myCommand.Parameters.Add("@OrderNumber", SqlDbType.NVarChar).Value = OrderNumber

        Dim EnclosureCardDataSet As New DataSet

        Dim ImageName As String = ""



        Dim daAdapter As New SqlDataAdapter()
        daAdapter.SelectCommand = myCommand
        daAdapter.Fill(EnclosureCardDataSet, "EnclosurecardDetails")


        Dim CommandText2 As String = "enterprise.spCompanyInformation"
        Dim myCommand2 As New SqlCommand(CommandText2, myConnection)



        myCommand2.CommandType = CommandType.StoredProcedure


        myCommand2.Parameters.Add("@CompanyID", SqlDbType.NVarChar).Value = CompanyID
        myCommand2.Parameters.Add("@DivisionID", SqlDbType.NVarChar).Value = DivisionID
        myCommand2.Parameters.Add("@DepartmentID", SqlDbType.NVarChar).Value = DepartmentID

        Dim daAdapter2 As New SqlDataAdapter()
        daAdapter2.SelectCommand = myCommand2
        daAdapter2.Fill(EnclosureCardDataSet, "CompanyDetails")
        ImageName = EnclosureCardDataSet.Tables("CompanyDetails").Rows(0)("CompanyLogoUrl").ToString()

        ImgPath = ImgPath & ImageName.ToString

        Dim fs As System.IO.FileStream = New System.IO.FileStream(ImgPath, System.IO.FileMode.Open, System.IO.FileAccess.Read)

        Dim Image() As Byte = New Byte(fs.Length - 1) {}


        ' fs.Read(Image, 0, Image.Length)

        fs.Read(Image, 0, CType(fs.Length, Integer))

        fs.Close()


        Dim Images As System.Data.DataTable = New DataTable()
        Images.TableName = "Images"
        'Images.Columns.Add(New DataColumn("imagedata", Image.GetType()))

        'Dim objRow() As Object = New Object(1) {}
        'objRow(0) = Image

        Dim objDataColumn As DataColumn = New DataColumn("imagedata", Image.GetType())
        Images.Columns.Add(objDataColumn)
        Dim row As DataRow
        row = Images.NewRow()
        row(0) = Image


        Images.Rows.Add(row)

        EnclosureCardDataSet.Tables.Add(Images)

        rptDoc.Load("D:\WebApps\EnterprisePOS\EnclosureCardCrystalReport.rpt")

        If pdffilename <> "" Then
            Dim PdfPath As String = ConfigurationManager.AppSettings("DocPath")
            pdffilename = "Card_" & pdffilename ' CompanyID & Date.Now.Month & Date.Now.Year & Date.Now.Hour & Date.Now.Minute & Date.Now.Second & Date.Now.Millisecond & ".pdf"
            pdffilename = pdffilename.Replace(" ", "_")
            rptDoc.ExportToDisk(ExportFormatType.PortableDocFormat, PdfPath & pdffilename)
            'Response.Redirect("Print_status.aspx?OrderNumber=" & OrderNumber & "&pdffilename=" & Request.QueryString("pdffilename") & "&print=" & Request.QueryString("print"))
        End If


        rptDoc.Dispose()

    End Sub

    Public Sub WorkRoomTicketPDF()

        Dim rptDoc As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim objUser As New DAL.CustomOrder()

        ' OrderNumber = Request.QueryString("OrderNumber")
        Dim ImgPath As String = ConfigurationManager.AppSettings("DocPath")


        If OrderNumber = "" Then
            OrderNumber = "*"
        End If


        ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")

        Dim myConnection As New SqlConnection(ConnectionString)

        Dim CommandText As String = "enterprise.RptWorkTicketReport"
        Dim myCommand As New SqlCommand(CommandText, myConnection)
        Dim workParam As New SqlParameter()

        myCommand.CommandType = CommandType.StoredProcedure

        ' Set the input parameter, companyid, divisionid, departmentid
        ' these parameters are set in the sub page_load
        myCommand.Parameters.Add("@CompanyID", SqlDbType.NVarChar).Value = CompanyID
        myCommand.Parameters.Add("@DivisionID", SqlDbType.NVarChar).Value = DivisionID
        myCommand.Parameters.Add("@DepartmentID", SqlDbType.NVarChar).Value = DepartmentID
        myCommand.Parameters.Add("@OrderNumber", SqlDbType.NVarChar).Value = OrderNumber

        Dim WorkTicketDS As New DataSet

        Dim ImageName As String = ""



        Dim daAdapter As New SqlDataAdapter()
        daAdapter.SelectCommand = myCommand
        daAdapter.Fill(WorkTicketDS, "RptWorkRoomTicket")
        '    Dim AddOnItems As String = WorkTicketDS.Tables("OrderDetailsGrid").Rows(0)("AddOnsIDsQty").ToString()


        If WorkTicketDS.Tables("RptWorkRoomTicket").Rows.Count > 0 Then

            'Decrypting Credit Card Number
            Dim CrdNum As String = ""
            CrdNum = WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CreditCardNumber").ToString()
            If CrdNum <> "" Then


                Dim DeCryptValue As New Encryption

                'CrdNum = DeCryptValue.DecryptData(WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CustomerID").ToString(), CrdNum)
                CrdNum = DeCryptValue.TripleDESDecode(CrdNum, WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CustomerID").ToString())
            End If

            'Last number display X in Credit cards
            Dim cardNo As String = ""
            Dim cLen As Integer = 0
            Dim subLen As Integer = 0
            Dim SubcardNo As String = ""

            cardNo = CrdNum
            cLen = cardNo.Length()
            Dim slen As Integer = 0
            If cLen > 0 Then
                If cLen > 8 Then
                    subLen = cLen - 8
                    SubcardNo = cardNo.Substring(0, subLen)
                    slen = SubcardNo.Length()
                    If slen > 4 Then
                        SubcardNo = SubcardNo.Substring(0, slen - 4) & "-" & SubcardNo.Substring((slen - 4))
                    End If
                    cardNo = SubcardNo & "-" & RepeatChar("X", 8)
                Else
                    cardNo = RepeatChar("X", cLen)
                End If
            End If
            WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CreditCardNumber") = cardNo

            Dim CreditCardExpDate As DateTime
            If WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CreditCardExpDate").ToString() <> "" Then

                CreditCardExpDate = Convert.ToDateTime(WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CreditCardExpDate").ToString())
                WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CreditCardExpDate") = CreditCardExpDate.ToString("MM/dd/yyyy")
            End If


        End If
        ' get the connection ready

        Dim CommandText1 As String = "enterprise.RptDocOrderDetailSingle"
        Dim myCommand1 As New SqlCommand(CommandText1, myConnection)



        myCommand1.CommandType = CommandType.StoredProcedure


        myCommand1.Parameters.Add("@CompanyID", SqlDbType.NVarChar).Value = CompanyID
        myCommand1.Parameters.Add("@DivisionID", SqlDbType.NVarChar).Value = DivisionID
        myCommand1.Parameters.Add("@DepartmentID", SqlDbType.NVarChar).Value = DepartmentID
        myCommand1.Parameters.Add("@OrderNumber", SqlDbType.NVarChar).Value = OrderNumber
        Dim daAdapter1 As New SqlDataAdapter()
        daAdapter1.SelectCommand = myCommand1
        daAdapter1.Fill(WorkTicketDS, "OrderDetailsGrid")



        Dim CommandText2 As String = "enterprise.spCompanyInformation"
        Dim myCommand2 As New SqlCommand(CommandText2, myConnection)



        myCommand2.CommandType = CommandType.StoredProcedure


        myCommand2.Parameters.Add("@CompanyID", SqlDbType.NVarChar).Value = CompanyID
        myCommand2.Parameters.Add("@DivisionID", SqlDbType.NVarChar).Value = DivisionID
        myCommand2.Parameters.Add("@DepartmentID", SqlDbType.NVarChar).Value = DepartmentID

        Dim daAdapter2 As New SqlDataAdapter()
        daAdapter2.SelectCommand = myCommand2
        daAdapter2.Fill(WorkTicketDS, "CompanyDetails")



        ImageName = WorkTicketDS.Tables("CompanyDetails").Rows(0)("CompanyLogoUrl").ToString()

        ImgPath = ImgPath & ImageName.ToString

        Dim fs As System.IO.FileStream = New System.IO.FileStream(ImgPath, System.IO.FileMode.Open, System.IO.FileAccess.Read)

        Dim Image() As Byte = New Byte(fs.Length - 1) {}




        fs.Read(Image, 0, CType(fs.Length, Integer))

        fs.Close()


        Dim Images As System.Data.DataTable = New DataTable()
        Images.TableName = "Images"


        Dim objDataColumn As DataColumn = New DataColumn("imagedata", Image.GetType())
        Images.Columns.Add(objDataColumn)
        Dim row As DataRow
        row = Images.NewRow()
        row(0) = Image


        Images.Rows.Add(row)

        WorkTicketDS.Tables.Add(Images)
        Dim PaymentMethod As String = ""

        If WorkTicketDS.Tables("RptWorkRoomTicket").Rows.Count > 0 Then
            PaymentMethod = WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("PaymentMethodID").ToString()


        End If

        If PaymentMethod = "Wire In" Then

            rptDoc.Load("D:\WebApps\EnterprisePOS\WireOutWorkTicketCrystalReport.rpt")
        ElseIf PaymentMethod = "COD" Then
            rptDoc.Load("D:\WebApps\EnterprisePOS\PaymentWorkTicketCrystalReport.rpt")
        ElseIf PaymentMethod = "Check" Then
            rptDoc.Load("D:\WebApps\EnterprisePOS\PaymentWorkTicketCrystalReport.rpt")
        ElseIf PaymentMethod = "E Check" Then
            rptDoc.Load("D:\WebApps\EnterprisePOS\PaymentWorkTicketCrystalReport.rpt")
        ElseIf PaymentMethod = "Credit Card" Then
            'rptDoc.Load(Server.MapPath("WorkTicketCrystalReport.rpt"))
            rptDoc.Load("D:\WebApps\EnterprisePOS\CreditPaymentWorkTicketCrystalReport.rpt")
        ElseIf PaymentMethod = "House Account" Then
            rptDoc.Load("D:\WebApps\EnterprisePOS\HouseAccountWorkTicketCrystalReport.rpt")
        Else
            rptDoc.Load("D:\WebApps\EnterprisePOS\HouseAccountWorkTicketCrystalReport.rpt")

        End If

        rptDoc.SetDataSource(WorkTicketDS)

        If pdffilename = "" Then

            Dim pdffilename As String
            Dim PdfPath As String = ConfigurationManager.AppSettings("DocPath")
            pdffilename = "Workticket_" & pdffilename
            pdffilename = pdffilename.Replace(" ", "_")
            rptDoc.ExportToDisk(ExportFormatType.PortableDocFormat, PdfPath & pdffilename)
        End If
        rptDoc.Dispose()
    End Sub


    Function RepeatChar(ByVal c As Char, ByVal len As Integer) As String
        Dim retValue As String = ""
        Dim i As Integer
        If len > 0 Then
            For i = 1 To len
                If (i = 5) Then
                    retValue = retValue + "-"
                    retValue = retValue + c
                Else
                    retValue = retValue + c
                End If
            Next i
        End If
        Return retValue
    End Function


End Class
