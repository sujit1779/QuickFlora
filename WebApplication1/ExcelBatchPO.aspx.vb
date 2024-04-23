Imports System.Data
Imports System.Data.SqlClient
Imports DAL

Partial Class ExcelBatchPO
    Inherits System.Web.UI.Page


    Public CompanyID As String = ""
    Public DivisionID As String = ""
    Public DepartmentID As String = ""
    Public EmployeeID As String = ""

    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'startDate&1/1/2018&endDate&9/14/2018CompanyID&QuickfloraDemoProductTypes&BuyStatus&Pending%20Emaillocationid&AssociatedBuyerlist&DefaultBuyStatus&EmployeeID&AdminDefaultVendor&

        Dim startDate As String = ""
        Dim endDate As String = ""

        startDate = Request.QueryString("startDate")
        endDate = Request.QueryString("endDate")


        CompanyID = Request.QueryString("CompanyID")
        DivisionID = "DEFAULT"
        DepartmentID = "DEFAULT"
        EmployeeID = Request.QueryString("EmployeeID")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""

        'ssql = Session("BatchPO")

        ' ssql = ssql & "      Select ItemName,RowRank,[Location],[Product],[Type],[OrderNo],[PONO],[QOH],[PRESOLD],[Q_REQ],[COLOR_VARIETY],[REMARKS],[ShipDate],[Q_ORD],[PACK],[COST],[Ext_COSt],[Vendor_Code],Vendor_Remarks,[Status],Buyer,[InLineNumber],[OrderBy]  FROM ( "
        ssql = ssql & "      Select [Enterprise].[dbo].InventoryItems.ItemName   , [PO_Requisition_Details].Vendor_Remarks,[PO_Requisition_Details].[PONO],[PO_Requisition_Details].[Product],[PO_Requisition_Details].[QOH],[PO_Requisition_Details].[PRESOLD] ,[PO_Requisition_Details].[Q_REQ] ,[PO_Requisition_Details].[COLOR_VARIETY] ,[PO_Requisition_Details].[REMARKS] ,[PO_Requisition_Details].[Q_ORD] "
        ssql = ssql & "      ,[PO_Requisition_Details].[PACK] ,[PO_Requisition_Details].[COST] ,[PO_Requisition_Details].[Ext_COSt] ,[PO_Requisition_Details].[Vendor_Code] ,[PO_Requisition_Details].[Status] ,[PO_Requisition_Details].[Buyer] "
        ssql = ssql & "      , [PO_Requisition_Header].[OrderNo] ,[PO_Requisition_Header].[Location] ,[PO_Requisition_Header].[Type] ,[PO_Requisition_Header].[ShipDate]  "

        ssql = ssql & "      FROM [Enterprise].[dbo].[PO_Requisition_Details] Left Outer Join [Enterprise].[dbo].[PO_Requisition_Header] "
        ssql = ssql & "      ON [Enterprise].[dbo].[PO_Requisition_Details].[CompanyID] = [Enterprise].[dbo].[PO_Requisition_Header].[CompanyID] AND "
        ssql = ssql & "      [Enterprise].[dbo].[PO_Requisition_Details].[DivisionID] =[Enterprise].[dbo].[PO_Requisition_Header].[DivisionID] And "
        ssql = ssql & "      [Enterprise].[dbo].[PO_Requisition_Details].[DepartmentID] = [Enterprise].[dbo].[PO_Requisition_Header].[DepartmentID] AND "
        ssql = ssql & "      [Enterprise].[dbo].[PO_Requisition_Details].[OrderNo] = [Enterprise].[dbo].[PO_Requisition_Header].[OrderNo] "

        ssql = ssql & " Left Outer Join [Enterprise].[dbo].InventoryItems  "
        ssql = ssql & " ON [Enterprise].[dbo].[PO_Requisition_Details].[CompanyID] = [Enterprise].[dbo].InventoryItems.[CompanyID] "
        ssql = ssql & " AND [Enterprise].[dbo].[PO_Requisition_Details].[DivisionID] =[Enterprise].[dbo].InventoryItems.[DivisionID] "
        ssql = ssql & " And [Enterprise].[dbo].[PO_Requisition_Details].[DepartmentID] = [Enterprise].[dbo].InventoryItems.[DepartmentID]  "
        ssql = ssql & " AND [Enterprise].[dbo].[PO_Requisition_Details].[Product] = [Enterprise].[dbo].InventoryItems.ItemID   "

        ssql = ssql & "  Where    [PO_Requisition_Details].[Status]  <> 'DateChanged' AND [PO_Requisition_Details].[Q_REQ]  <> '0' AND HDStatus <> 'Entry In Process'  AND    [PO_Requisition_Details].[Q_REQ]  <> ''   AND  [Enterprise].[dbo].[PO_Requisition_Details].CompanyID ='" & Me.CompanyID & "' AND [Enterprise].[dbo].[PO_Requisition_Details].DivisionID ='" & Me.DivisionID & "'  AND [Enterprise].[dbo].[PO_Requisition_Details].DepartmentID ='" & Me.DepartmentID & "' "
        ''  ssql = ssql & " AND  [PO_Requisition_Header].[Status]  <> 'Entry In Process' "

        ssql = ssql & " AND  [PO_Requisition_Details].[Product]  <> '' "

        ''  convert(int, CASE WHEN  ISNUMERIC([PO_Requisition_Details].[Q_REQ])  = 1  THEN  [PO_Requisition_Details].[Q_REQ] ELSE '0' END)  <> 0 AND 

        ssql = ssql & " AND [PO_Requisition_Details].[ShipDate] >= '" & startDate & "'"
        ssql = ssql & " AND [PO_Requisition_Details].[ShipDate] <= '" & endDate & "'"

        'startDate&1/1/2018&endDate&9/14/2018CompanyID&QuickfloraDemoProductTypes&BuyStatus&Pending%20Emaillocationid&AssociatedBuyerlist&DefaultBuyStatus&EmployeeID&AdminDefaultVendor&

        'https://secure.quickflora.com/POMBeta/ExcelBatchPO.aspx?startDate=1/1/2018&endDate=9/14/2018&CompanyID=QuickfloraDemo&
        'ProductTypes=Plants,Flowers&BuyStatus=No%20Action,Pending%20Email&locationid=Associated,Corporate&Buyerlist=Admin,Claire&DefaultBuyStatus=&EmployeeID=Admin&DefaultVendor=

        '  ISNULL([PO_Requisition_Header].[canceled],0) <> 1 AND

        Dim Buyerlist As String()
        Dim QBuyerlist As String = ""

        QBuyerlist = Request.QueryString("Buyerlist")

        If QBuyerlist.IndexOf(",") > -1 Then
            Buyerlist = QBuyerlist.Split(",")


            ssql = ssql & " AND [PO_Requisition_Details].[Buyer] in ( "

            Dim n As Integer = 0


            For n = 0 To Buyerlist.Length - 1
                If n <> 0 Then
                    ssql = ssql & ","
                End If
                ssql = ssql & "'" & Buyerlist(n) & "'"
            Next

            ssql = ssql & " ) "

        Else
            If QBuyerlist <> "" Then
                ssql = ssql & " AND [PO_Requisition_Details].[Buyer] = '" & QBuyerlist & "' "
            End If

        End If

        Dim ProductTypes As String()
        Dim QProductTypes As String = ""

        QProductTypes = Request.QueryString("ProductTypes")

        If QProductTypes.IndexOf(",") > -1 Then
            ProductTypes = QProductTypes.Split(",")


            ssql = ssql & " AND [PO_Requisition_Details].[Type] in ( "

            Dim n As Integer = 0


            For n = 0 To ProductTypes.Length - 1
                If n <> 0 Then
                    ssql = ssql & ","
                End If
                ssql = ssql & "'" & ProductTypes(n) & "'"
            Next

            ssql = ssql & " ) "

        Else
            If QProductTypes <> "" Then
                ssql = ssql & " AND [PO_Requisition_Details].[Type] = '" & QProductTypes & "' "
            End If

        End If

        Dim Status As String()
        Dim QrStatus As String = ""

        QrStatus = Request.QueryString("BuyStatus")

        If QrStatus.IndexOf(",") > -1 Then
            Status = QrStatus.Split(",")


            ssql = ssql & " AND [PO_Requisition_Details].[Status] in ( "

            Dim n As Integer = 0


            For n = 0 To Status.Length - 1
                If n <> 0 Then
                    ssql = ssql & ","
                End If
                ssql = ssql & "'" & Status(n) & "'"
            Next

            ssql = ssql & " ) "

        Else
            If QrStatus <> "" Then
                ssql = ssql & " AND [PO_Requisition_Details].[Status] = '" & QrStatus & "' "
            End If

        End If

        Dim Location As String()
        Dim QLocation As String = ""

        QLocation = Request.QueryString("locationid")

        If QLocation.IndexOf(",") > -1 Then
            Location = QLocation.Split(",")


            ssql = ssql & " AND [PO_Requisition_Header].[Location] in ( "

            Dim n As Integer = 0


            For n = 0 To Location.Length - 1
                If n <> 0 Then
                    ssql = ssql & ","
                End If
                ssql = ssql & "'" & Location(n) & "'"
            Next

            ssql = ssql & " ) "

        Else
            If QLocation <> "" Then
                ssql = ssql & " AND [PO_Requisition_Header].[Location] = '" & QLocation & "'"
            End If

        End If




        Dim dt As New DataTable
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        'Session("BatchPO") = ssql
        'com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
        'com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
        'com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID

        da.SelectCommand = com
        Try
            da.Fill(dt)
        Catch ex As Exception

        End Try


        'Response.Write(ssql)
        'Exit Sub

        If dt.Rows.Count <> 0 Then

            Response.ClearContent()
            Response.Buffer = True
            Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", "BatchPO.xls"))
            Response.ContentType = "application/ms-excel"

            Dim str As String = String.Empty
            For Each dtcol As DataColumn In dt.Columns
                Response.Write(str + dtcol.ColumnName)
                str = vbTab
            Next
            Response.Write(vbLf)
            For Each dr As DataRow In dt.Rows
                str = ""
                For j As Integer = 0 To dt.Columns.Count - 1
                    Response.Write(str & Convert.ToString(dr(j)))
                    str = vbTab
                Next
                Response.Write(vbLf)
            Next
            Response.[End]()

        Else
            'lblmsg.Text = "No Result Found <br><br>"

        End If

    End Sub
End Class
