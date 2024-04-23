Imports System.Data
Imports System.Data.SqlClient
Imports DAL

Partial Class RequisitionOrderList
    Inherits Page


    Public CompanyID As String = ""
    Public DivisionID As String = ""
    Public DepartmentID As String = ""
    Public EmployeeID As String = ""

    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    Private Sub OrderHeaderGrid_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles OrderHeaderGrid.PageIndexChanging

        OrderHeaderGrid.PageIndex = e.NewPageIndex
        ''SetOrderData()

    End Sub

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

    Public newmenu As String = ""
    Public newmenu1 As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Dim filters As EnterpriseCommon.Core.FilterSet
        filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
        CompanyID = filters!CompanyID
        DivisionID = filters!DivisionID
        DepartmentID = filters!DepartmentID
        EmployeeID = Session("EmployeeID")

        If Me.CompanyID = "SouthFloralsTraining" Or Me.CompanyID = "QuickfloraDemo" Or Me.CompanyID = "SouthFloral" Or Me.CompanyID = "_DierbergsMarkets,Inc63017" Then
            newmenu = "CreateRequisition.aspx"
            newmenu1 = "http://rpt.quickflora.com/scripts/ROReport.aspx"
        Else
            newmenu = "RequisitionOrder.aspx"
            newmenu1 = "https://reports.quickflora.com/reports/scripts/ROReport.aspx"
        End If

        If Me.CompanyID = "DierbergsMarkets,Inc63017" Or Me.CompanyID = "QuickfloraDemo" Then
            newmenu = "RequisitionOrderDB.aspx"
            newmenu1 = "https://reports.quickflora.com/reports/scripts/ROReport.aspx"
        End If

        rs = PopulateEmployees(CompanyID, DepartmentID, DivisionID, "PurchaseRequest")

        Dim securitycheck As Boolean = False

        While (rs.Read())

            If rs("EmployeeID").ToString() = EmployeeID Then
                securitycheck = True
                Exit While
            End If

        End While
        rs.Close()

        If securitycheck = False And CompanyID <> "PhillipsFlowersGifts60559" Then
            Response.Redirect("SecurityAcessPermission.aspx?MOD=PurchaseRequest")
        End If

        If CompanyID = "DierbergsMarkets,Inc63017" Then
            SortExpression = "OrderNo"
        End If

        If IsPostBack = False Then
            SetLocationIDdropdown()
            txtDateFrom.Text = DateTime.Now.Date.ToShortDateString
            txtDateTo.Text = DateTime.Now.Date.ToShortDateString
            Session("SortDirection") = "DESC"



            fillitems("1")
            txtpageno.Text = "1"

        End If




    End Sub


    Public Pageno As String = ""
    Public paging As String = ""
    Public pagingleft As String = ""
    Public pagingright As String = ""

    Public Sub fillitems(ByVal pg As String)


        If pg <> "" Then
            Pageno = pg
        Else
            Pageno = "1"
        End If

        Dim dtcount As New DataTable
        dtcount = BindDetailsCount()



        Dim totalrows As Integer = 0

        Try
            totalrows = dtcount.Rows(0)(0)
        Catch ex As Exception

        End Try

        lblpagecount.Text = "totalrows:" & totalrows

        Dim numofpages As Integer = 0
        Dim numofitemsperpage As Integer = 0
        numofitemsperpage = drppagelimit.SelectedValue

        numofpages = totalrows / numofitemsperpage

        If totalrows > numofitemsperpage * numofpages Then
            numofpages = numofpages + 1
        End If



        Dim n As Integer = 0
        paging = ""
        For n = 1 To numofpages

            If n = Pageno Then
                paging = paging & "<li class='c-active'>" & vbCrLf

            Else
                paging = paging & "<li>" & vbCrLf
            End If
            paging = paging & "<a href='Javascript:;'  onclick=""javascript:paging('" & n & "')"">" & n & "</a>" & vbCrLf
            paging = paging & "</li>" & vbCrLf
        Next

        ''txtcheck.Text = paging

        pagingleft = "javascript:paging('" & Pageno - 1 & "')"
        pagingright = "javascript:paging('" & Pageno + 1 & "')"


        BindDetails(Pageno - 1)

    End Sub

    Protected Sub drppagelimit_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drppagelimit.SelectedIndexChanged
        Session("drppagelimit") = drppagelimit.SelectedValue
        ''SortExpression = "DEFAULT"
        fillitems("1")

    End Sub

    Private Sub btnpageno_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnpageno.Click
        fillitems(txtpageno.Text)
    End Sub

    Public Function BindDetailsCount() As DataTable

        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT Count([PO_Requisition_Header].[OrderNo]) FROM [PO_Requisition_Header] where   CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 "

        If rdselected.Checked Then
            ssql = ssql & " AND ISNULL([PO_Requisition_Header].[ShipDate],'') <> '' "
            ssql = ssql & " AND  ISDATE([PO_Requisition_Header].[ShipDate])  = 1 "
            ssql = ssql & " AND convert(datetime, Convert(nvarchar(36),[PO_Requisition_Header].[ShipDate],101)) >= '" & txtDateFrom.Text & "'"
            ssql = ssql & " AND convert(datetime, Convert(nvarchar(36),[PO_Requisition_Header].[ShipDate],101)) <= '" & txtDateTo.Text & "'"
        End If

        If chkincludeStanding.Checked Then
            'ssql = ssql & " AND [PO_Requisition_Header].[Type] = '" & drpProductTypes.SelectedValue & "'"
        Else
            ssql = ssql & " AND [PO_Requisition_Header].[Type] <> 'Standing Auto' "
        End If


        If chkIncludeCanceled.Checked Then
            If rdOnlyCanceled.Checked Then
                ssql = ssql & " AND ISNULL([PO_Requisition_Header].[canceled],0)  = 1 "
            End If
        Else
            If rdOnlyCanceled.Checked Then
                ssql = ssql & " AND ISNULL([PO_Requisition_Header].[canceled],0)  = 1 "
            Else
                ssql = ssql & " AND ISNULL([PO_Requisition_Header].[canceled],0)  <> 1 "
            End If

        End If

        If chkIncludeReceived.Checked Then
            If rdOnlyReceived.Checked Then
                ssql = ssql & " AND ISNULL([PO_Requisition_Header].[Received],0)  = 1 "
            End If
        Else
            If rdOnlyReceived.Checked Then
                ssql = ssql & " AND ISNULL([PO_Requisition_Header].[Received],0)  = 1 "
            Else
                ssql = ssql & " AND ISNULL([PO_Requisition_Header].[Received],0)  <> 1 "
            End If
        End If

        If txtSearchExpression.Text.Trim <> "" Then
            If drpCondition.SelectedValue = "=" Then
                If drpFieldName.SelectedValue = "OrderNo" Then
                    ssql = ssql & " AND  [PO_Requisition_Header].[OrderNo]  = '" & txtSearchExpression.Text & "'"
                End If
                If drpFieldName.SelectedValue = "OrderBy" Then
                    ssql = ssql & " AND  [PO_Requisition_Header].[OrderBy]  = '" & txtSearchExpression.Text & "'"
                End If
                If drpFieldName.SelectedValue = "ReceivedBy" Then
                    ssql = ssql & " AND  [PO_Requisition_Header].[ReceivedBy]  = '" & txtSearchExpression.Text & "'"
                End If

            End If
            If drpCondition.SelectedValue = "Like" Then
                If drpFieldName.SelectedValue = "OrderNo" Then
                    ssql = ssql & " AND  [PO_Requisition_Header].[OrderNo]  Like '%" & txtSearchExpression.Text & "%'"
                End If
                If drpFieldName.SelectedValue = "OrderBy" Then
                    ssql = ssql & " AND  [PO_Requisition_Header].[OrderBy]  Like '%" & txtSearchExpression.Text & "%'"
                End If
                If drpFieldName.SelectedValue = "ReceivedBy" Then
                    ssql = ssql & " AND  [PO_Requisition_Header].[ReceivedBy]  Like '%" & txtSearchExpression.Text & "%'"
                End If

            End If
        End If



        If drpStatus.SelectedValue <> "" Then
            ssql = ssql & " AND  [PO_Requisition_Header].[Status]  = '" & drpStatus.SelectedValue & "'"
        Else

            '"Entry In Process"
        End If
        If drpType.SelectedValue <> "" Then
            ssql = ssql & " AND [PO_Requisition_Header].[Type] = '" & drpType.SelectedValue & "'"
        End If

        If cmblocationid.SelectedValue <> "" Then
            ssql = ssql & " AND [PO_Requisition_Header].[Location] = '" & cmblocationid.SelectedValue & "'"
        End If

        ''ssql = ssql & " Order by convert(datetime, Convert(nvarchar(36), CASE WHEN  ISDATE([PO_Requisition_Header].[ShipDate])  = 1  THEN  [PO_Requisition_Header].[ShipDate] ELSE '1/1/1900' END,101))  DESC "

        'Session("RequisitionOrderList") = ssql

        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
        com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
        com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
        da.SelectCommand = com
        da.Fill(dt)

        Return dt
    End Function


    Public Sub BindDetails(ByVal Pageno As Integer)

        Dim _drppagelimit As Integer = 0
        _drppagelimit = drppagelimit.SelectedValue
        Pageno = Pageno * _drppagelimit + 1

        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        'ssql = " SELECT  [OrderNo] ,[Location] ,[Remarks] ,[Status] ,[Type] ,[LastChangeDateTime] ,[LastChangeBy] ,[TotalAmount] ,[ShipDate] ,[ArriveDate] ,[OrderPlacedDate] "
        'ssql = ssql & "      ,[ReceivedOnDate] ,[OrderBy] ,[ReceivedBy] ,[PONO] ,[Standing] ,[InventoryOrigin] ,[ShipMethodID] ,[canceled] ,[canceledDate] ,[canceledBy],RowRank FROM( "
        'ssql = ssql & "       SELECT  [OrderNo] ,[Location] ,[Remarks] ,[Status] ,[Type] ,[LastChangeDateTime] ,[LastChangeBy] ,[TotalAmount] ,[ShipDate] ,[ArriveDate] ,[OrderPlacedDate] "
        'ssql = ssql & "      ,[ReceivedOnDate] ,[OrderBy] ,[ReceivedBy] ,[PONO] ,[Standing] ,[InventoryOrigin] ,[ShipMethodID] ,[canceled] ,[canceledDate] ,[canceledBy] "

        ssql = " SELECT  [OrderNo] ,[Location] ,[Remarks] ,[Status] ,[Type] ,[LastChangeDateTime] ,[LastChangeBy] ,[TotalAmount] ,[ShipDate] ,[ArriveDate] ,[OrderPlacedDate] "
        ssql = ssql & "      ,[ReceivedOnDate] ,[OrderBy] ,[ReceivedBy] ,[PONO] ,[Standing] ,[InventoryOrigin] ,[ShipMethodID] ,[canceled] ,[canceledDate] ,[canceledBy],RowRank FROM( "
        ssql = ssql & "       SELECT    [OrderNo] ,[Location] ,[Remarks] ,[Status] ,[Type] ,[LastChangeDateTime] ,[LastChangeBy] ,  "
        ''ssql = ssql & " [TotalAmount] "
        ssql = ssql & " ( Select SUM(round(convert(money,  CASE WHEN  Isnumeric(Q_ORD)  = 1  THEN  Q_ORD ELSE '0' END) * convert(money,  CASE WHEN  Isnumeric(COST)  = 1  THEN  COST ELSE '0' END) * convert(money,  CASE WHEN  Isnumeric(PACK)  = 1  THEN  PACK ELSE '0' END),2)   ) "
        ssql = ssql & " From PO_Requisition_Details Where PO_Requisition_Details.CompanyID = @f0 And PO_Requisition_Details.DivisionID=@f1 And PO_Requisition_Details.DepartmentID=@f2   AND PO_Requisition_Details.OrderNo = [PO_Requisition_Header].OrderNo  "
        ssql = ssql & " ) AS 'TotalAmount' "
        ssql = ssql & " ,[ShipDate] ,[ArriveDate] ,[OrderPlacedDate] "
        ssql = ssql & "      ,[ReceivedOnDate] ,[OrderBy] ,[ReceivedBy] ,[PONO] ,[Standing] ,[InventoryOrigin] ,[ShipMethodID] ,[canceled] ,[canceledDate] ,[canceledBy] "



        If Me.CompanyID = "SouthFloralsTraining" Or Me.CompanyID = "QuickfloraDemo" Or CompanyID = "SouthFloral" Then
            ssql = " SELECT  [OrderNo] ,[Location] ,[Remarks] ,[Status] ,[Type] ,[LastChangeDateTime] ,[LastChangeBy] ,[TotalAmount] ,[ShipDate] ,[ArriveDate] ,[OrderPlacedDate] "
            ssql = ssql & "      ,[ReceivedOnDate] ,[OrderBy] ,[ReceivedBy] ,[PONO] ,[Standing] ,[InventoryOrigin] ,[ShipMethodID] ,[canceled] ,[canceledDate] ,[canceledBy],RowRank FROM( "
            ssql = ssql & "       SELECT  [OrderNo] ,[Location] ,[Remarks] ,[Status] ,[Type] ,[LastChangeDateTime] ,[LastChangeBy] ,  "
            ''ssql = ssql & " [TotalAmount] "
            ssql = ssql & " ( Select SUM(round(convert(money,  CASE WHEN  Isnumeric(Q_REQ)  = 1  THEN  Q_REQ ELSE '0' END) * convert(money,  CASE WHEN  Isnumeric(WHOPrice)  = 1  THEN  WHOPrice ELSE '0' END) ,2)   ) "
            ssql = ssql & " From PO_Requisition_Details Where PO_Requisition_Details.CompanyID = @f0 And PO_Requisition_Details.DivisionID=@f1 And PO_Requisition_Details.DepartmentID=@f2   AND PO_Requisition_Details.OrderNo = [PO_Requisition_Header].OrderNo  "
            ssql = ssql & " ) AS 'TotalAmount' "
            ssql = ssql & " ,[ShipDate] ,[ArriveDate] ,[OrderPlacedDate] "
            ssql = ssql & "      ,[ReceivedOnDate] ,[OrderBy] ,[ReceivedBy] ,[PONO] ,[Standing] ,[InventoryOrigin] ,[ShipMethodID] ,[canceled] ,[canceledDate] ,[canceledBy] "


        End If

        Dim SortDirection As String = "DESC"

        Try
            SortDirection = Session("SortDirection")
        Catch ex As Exception

        End Try



        If SortExpression = "ShipDate" Then
            ssql = ssql & "      ,ROW_NUMBER() OVER (ORDER BY   convert(datetime, Convert(nvarchar(36), CASE WHEN  ISDATE([PO_Requisition_Header].[ShipDate])  = 1  THEN  [PO_Requisition_Header].[ShipDate] ELSE '1/1/1900' END,101))    " & SortDirection & "  ) As RowRank  "
        End If

        If SortExpression = "ArriveDate" Then
            ssql = ssql & "      ,ROW_NUMBER() OVER (ORDER BY   convert(datetime, Convert(nvarchar(36), CASE WHEN  ISDATE([PO_Requisition_Header].[ArriveDate])  = 1  THEN  [PO_Requisition_Header].[ArriveDate] ELSE '1/1/1900' END,101))    " & SortDirection & "  ) As RowRank  "
        End If

        If SortExpression = "OrderPlacedDate" Then
            ssql = ssql & "      ,ROW_NUMBER() OVER (ORDER BY   convert(datetime, Convert(nvarchar(36), CASE WHEN  ISDATE([PO_Requisition_Header].[OrderPlacedDate])  = 1  THEN  [PO_Requisition_Header].[OrderPlacedDate] ELSE '1/1/1900' END,101))    " & SortDirection & "  ) As RowRank  "
        End If

        If SortExpression = "ReceivedOnDate" Then
            ssql = ssql & "      ,ROW_NUMBER() OVER (ORDER BY   convert(datetime, Convert(nvarchar(36), CASE WHEN  ISDATE([PO_Requisition_Header].[ReceivedOnDate])  = 1  THEN  [PO_Requisition_Header].[ReceivedOnDate] ELSE '1/1/1900' END,101))    " & SortDirection & "  ) As RowRank  "
        End If

        If SortExpression = "TotalAmount" Then
            ssql = ssql & "      ,ROW_NUMBER() OVER (ORDER BY  [PO_Requisition_Header].[TotalAmount]   " & SortDirection & " ) As RowRank "
        End If


        If SortExpression = "ReceivedBy" Then
            ssql = ssql & "      ,ROW_NUMBER() OVER (ORDER BY  [PO_Requisition_Header].[ReceivedBy]   " & SortDirection & " ) As RowRank "
        End If

        If SortExpression = "OrderBy" Then
            ssql = ssql & "      ,ROW_NUMBER() OVER (ORDER BY  [PO_Requisition_Header].[OrderBy]   " & SortDirection & " ) As RowRank "
        End If

        If SortExpression = "Type" Then
            ssql = ssql & "      ,ROW_NUMBER() OVER (ORDER BY  [PO_Requisition_Header].[Type]   " & SortDirection & " ) As RowRank "
        End If

        If SortExpression = "Status" Then
            ssql = ssql & "      ,ROW_NUMBER() OVER (ORDER BY  [PO_Requisition_Header].[Status]   " & SortDirection & " ) As RowRank "
        End If

        If SortExpression = "Remarks" Then
            ssql = ssql & "      ,ROW_NUMBER() OVER (ORDER BY  [PO_Requisition_Header].[Remarks]   " & SortDirection & " ) As RowRank "
        End If

        If SortExpression = "OrderNo" Then
            ssql = ssql & "      ,ROW_NUMBER() OVER (ORDER BY  [PO_Requisition_Header].[OrderNo]   " & SortDirection & " ) As RowRank "
        End If

        If SortExpression = "Location" Then
            ssql = ssql & "      ,ROW_NUMBER() OVER (ORDER BY  [PO_Requisition_Header].[Location]   " & SortDirection & " ) As RowRank "
        End If


        ssql = ssql & "  FROM [Enterprise].[dbo].[PO_Requisition_Header] "
        ssql = ssql & " where   CompanyID=@f0 And DivisionID=@f1 And DepartmentID=@f2 "
        If rdselected.Checked Then
            ssql = ssql & " And ISNULL([PO_Requisition_Header].[ShipDate],'') <> '' "
            ssql = ssql & " AND  ISDATE([PO_Requisition_Header].[ShipDate])  = 1 "
            ssql = ssql & " AND convert(datetime, Convert(nvarchar(36),[PO_Requisition_Header].[ShipDate],101)) >= '" & txtDateFrom.Text & "'"
            ssql = ssql & " AND convert(datetime, Convert(nvarchar(36),[PO_Requisition_Header].[ShipDate],101)) <= '" & txtDateTo.Text & "'"
        End If

        If chkincludeStanding.Checked Then
            'ssql = ssql & " AND [PO_Requisition_Header].[Type] = '" & drpProductTypes.SelectedValue & "'"
        Else
            ssql = ssql & " AND [PO_Requisition_Header].[Type] <> 'Standing Auto' "
        End If

        If chkIncludeCanceled.Checked Then
            If rdOnlyCanceled.Checked Then
                ssql = ssql & " AND ISNULL([PO_Requisition_Header].[canceled],0)  = 1 "
            End If
        Else
            If rdOnlyCanceled.Checked Then
                ssql = ssql & " AND ISNULL([PO_Requisition_Header].[canceled],0)  = 1 "
            Else
                ssql = ssql & " AND ISNULL([PO_Requisition_Header].[canceled],0)  <> 1 "
            End If

        End If

        If chkIncludeReceived.Checked Then
            If rdOnlyReceived.Checked Then
                ssql = ssql & " AND ISNULL([PO_Requisition_Header].[Received],0)  = 1 "
            End If
        Else
            If rdOnlyReceived.Checked Then
                ssql = ssql & " AND ISNULL([PO_Requisition_Header].[Received],0)  = 1 "
            Else
                ssql = ssql & " AND ISNULL([PO_Requisition_Header].[Received],0)  <> 1 "
            End If
        End If

        If txtSearchExpression.Text.Trim <> "" Then
            If drpCondition.SelectedValue = "=" Then
                If drpFieldName.SelectedValue = "OrderNo" Then
                    ssql = ssql & " AND  [PO_Requisition_Header].[OrderNo]  = '" & txtSearchExpression.Text & "'"
                End If
                If drpFieldName.SelectedValue = "OrderBy" Then
                    ssql = ssql & " AND  [PO_Requisition_Header].[OrderBy]  = '" & txtSearchExpression.Text & "'"
                End If
                If drpFieldName.SelectedValue = "ReceivedBy" Then
                    ssql = ssql & " AND  [PO_Requisition_Header].[ReceivedBy]  = '" & txtSearchExpression.Text & "'"
                End If

            End If
            If drpCondition.SelectedValue = "Like" Then
                If drpFieldName.SelectedValue = "OrderNo" Then
                    ssql = ssql & " AND  [PO_Requisition_Header].[OrderNo]  Like '%" & txtSearchExpression.Text & "%'"
                End If
                If drpFieldName.SelectedValue = "OrderBy" Then
                    ssql = ssql & " AND  [PO_Requisition_Header].[OrderBy]  Like '%" & txtSearchExpression.Text & "%'"
                End If
                If drpFieldName.SelectedValue = "ReceivedBy" Then
                    ssql = ssql & " AND  [PO_Requisition_Header].[ReceivedBy]  Like '%" & txtSearchExpression.Text & "%'"
                End If

            End If
        End If



        If drpStatus.SelectedValue <> "" Then
            ssql = ssql & " AND  [PO_Requisition_Header].[Status]  = '" & drpStatus.SelectedValue & "'"
        Else

            '"Entry In Process"
        End If
        If drpType.SelectedValue <> "" Then
            ssql = ssql & " AND [PO_Requisition_Header].[Type] = '" & drpType.SelectedValue & "'"
        End If

        If cmblocationid.SelectedValue <> "" Then
            ssql = ssql & " AND [PO_Requisition_Header].[Location] = '" & cmblocationid.SelectedValue & "'"
        End If

        ssql = ssql & " ) as Headerwithrownumber "

        Session("RequisitionOrderList") = ssql

        ssql = ssql & "	WHERE RowRank >= " & Pageno.ToString & " And RowRank < (" & (Pageno + _drppagelimit).ToString & ") "
        ssql = ssql & " Order by RowRank ASC "


        '' ssql = ssql & " Order by convert(datetime, Convert(nvarchar(36), CASE WHEN  ISDATE([PO_Requisition_Header].[ShipDate])  = 1  THEN  [PO_Requisition_Header].[ShipDate] ELSE '1/1/1900' END,101))  DESC "

        ''Session("RequisitionOrderList") = ssql

        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
        com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
        com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
        com.CommandTimeout = 5000
        da.SelectCommand = com
        da.Fill(dt)


        If dt.Rows.Count <> 0 Then
            OrderHeaderGrid.DataSource = dt
            OrderHeaderGrid.DataBind()
            lblmsg.Text = ""
        Else
            lblmsg.Text = "No Result Found <br><br>"
            OrderHeaderGrid.DataSource = dt
            OrderHeaderGrid.DataBind()
        End If

    End Sub



    Public Sub SetdrpType()
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ''Select DISTINCT [PO_Requisition_Header].Type from [PO_Requisition_Header]  Where [CompanyID] = 'QuickfloraDemo' and ISNULL([PO_Requisition_Header].Type,'') <> ''
        ssql = "SELECT  DISTINCT [PO_Requisition_Header].Type from [PO_Requisition_Header]  where  ISNULL([PO_Requisition_Header].Type,'') <> '' AND  CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2  Order by [PO_Requisition_Header].Type "
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

        '              ,[ProductTypesID]
        '      ,[IsActive]
        '  From [Enterprise].[dbo].[PO_Requisition_ProductTypes]
        'Where [CompanyID] = 'McCarthyg'


        If dt.Rows.Count <> 0 Then
            drpType.DataSource = dt
            drpType.DataTextField = "Type"
            drpType.DataValueField = "Type"
            drpType.DataBind()
        End If

    End Sub



    Public Sub SetLocationIDdropdown()
        SetdrpType()
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
            cmblocationid.Items.Clear()
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

    End Sub

    Private Sub AjaxPanel101_Load(ByVal sender As Object, ByVal e As EventArgs) Handles AjaxPanel101.Load

    End Sub

    Private Sub OrderHeaderGrid_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles OrderHeaderGrid.RowCommand
        If e.CommandName = "ROPrint" Then
            Dim ordernumber As String
            ordernumber = e.CommandArgument

            ' Response.Redirect("~/PO.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&EmployeeID=" & EmployeeID & "&PurchaseOrderNumber=" & ordernumber)

            Dim term As String = "" '= Session("TerminalID")

            Try
                term = Session("TerminalID")
            Catch ex As Exception

            End Try
            Dim constr11 As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim connec As New SqlConnection(constr11)
            Dim qry As String
            qry = "insert into [ROPrintRequest]( CompanyID, DivisionID, DepartmentID, [TerminalName]" _
            & " , [PrintText], [Active],[TimeStamp]) values(@f0,@f1,@f2,@f3,@f4,@f5,GetDate())"
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
            If CheckOrderProductData(ordernumber) Then
                SETTOCacnelUpdate(ordernumber)
                fillitems("1")
            Else
                Dim onloadScript As String = ""
                onloadScript = onloadScript & "<script type='text/javascript'>" & vbCrLf
                onloadScript = onloadScript & "  " & "doOnLoadShipDate();" & " " & vbCrLf
                onloadScript = onloadScript & "  " & "function doOnLoadShipDate() {" & " " & vbCrLf
                onloadScript = onloadScript & "  " & "alert('Cancel Allowed only if all items of that request are not having any PO generated before. Also all rows status must No Action');" & " " & vbCrLf
                onloadScript = onloadScript & "  " & "}" & " " & vbCrLf
                onloadScript = onloadScript & "<" & "/" & "script>"
                ' Register script with page 
                Me.ClientScript.RegisterStartupScript(Me.GetType(), "onLoadCallShipDate", onloadScript.ToString())
            End If

        End If


        If e.CommandName = "Duplicate" Then
            Dim ordernumber As String
            ordernumber = e.CommandArgument
            Response.Redirect("RODuplicate.aspx?OrderNumber=" & ordernumber)
        End If

    End Sub


    Public Function CheckOrderProductData(ByVal orderno As String) As Boolean
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT * FROM [PO_Requisition_Details] where (ISNULL(PO_Requisition_Details.PONO  ,'') <> '' OR  ISNULL(PO_Requisition_Details.Status ,'') <> 'No Action') AND  OrderNo = @OrderNo and CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 order by InLineNumber DESC "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
        com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
        com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
        com.Parameters.Add(New SqlParameter("@OrderNo", SqlDbType.NVarChar, 36)).Value = orderno
        da.SelectCommand = com
        da.Fill(dt)

        Dim chk As Boolean = False

        'lblErr.Text = lblErr.Text & ssql
        'lblErr.Text = lblErr.Text & "dt.Rows.Count:" & dt.Rows.Count


        If dt.Rows.Count = 0 Then
            chk = True
        Else
            chk = False
        End If

        Return chk

    End Function


    Public Function SETTOCacnelUpdate_PO_Requisition_Details(ByVal RowNumber As String) As Boolean
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE [PO_Requisition_Details] set  [PO_Requisition_Details].[HDStatus]='Entry In Process' Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And OrderNo=@OrderNo  "
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)

        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@OrderNo", SqlDbType.NVarChar, 36)).Value = RowNumber

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return msg
        End Try

        Return True
    End Function



    Public Function SETTOCacnelUpdate(ByVal RowNumber As String) As Boolean

        Try
            SETTOCacnelUpdate_PO_Requisition_Details(RowNumber)
        Catch ex As Exception

        End Try

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE [PO_Requisition_Header] set  [PO_Requisition_Header].[canceled]=1,[canceledDate] = GETDATE(),[canceledBy]=@canceledBy Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And OrderNo=@RowNumber  "
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Dim EmployeeID As String = ""
        Try
            EmployeeID = Session("EmployeeID")
        Catch ex As Exception

        End Try

        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@RowNumber", SqlDbType.NVarChar, 36)).Value = RowNumber
            com.Parameters.Add(New SqlParameter("@canceledBy", SqlDbType.NVarChar)).Value = EmployeeID

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return msg
        End Try

        Return True
    End Function




    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click, btnSearchExpression.Click, drpStatus.SelectedIndexChanged, drpType.SelectedIndexChanged, cmblocationid.SelectedIndexChanged, chkIncludeCanceled.CheckedChanged, chkIncludeReceived.CheckedChanged, rdOnlyCanceled.CheckedChanged, rdOnlyReceived.CheckedChanged
        fillitems("1")
    End Sub


    'CREATE TABLE [dbo].[PO_Requisition_Header](
    '	[CompanyID] [nvarchar](36) Not NULL,
    '	[DivisionID] [nvarchar](36) Not NULL,
    '	[DepartmentID] [nvarchar](36) Not NULL,
    '	[OrderNo] [nvarchar](50) Not NULL,
    '	[Location] [nvarchar](50) Not NULL,
    '	[Remarks] [nvarchar](2000) Not NULL,
    '	[Status] [nvarchar](50) Not NULL,
    '	[Type] [nvarchar](50) Not NULL,
    '	[LastChangeDateTime] [datetime] NULL,
    '	[LastChangeBy] [nvarchar](50) Not NULL,
    '	[TotalAmount] [money] Not NULL,
    '	[ShipDate] [datetime] NULL,
    '	[ArriveDate] [datetime] NULL,
    '	[OrderPlacedDate] [datetime] NULL,
    '	[ReceivedOnDate] [datetime] NULL,
    '	[OrderBy] [nvarchar](50) Not NULL,
    '	[ReceivedBy] [nvarchar](50) Not NULL
    ') ON [PRIMARY]


    Public Function POHeaderDETAILS(ByVal OrderNo As String) As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim LocationName As String = ""
        Dim dt As New DataTable

        'From [Enterprise].[dbo].[PurchaseDetail]
        'Order By [PurchaseLineNumber] DESC

        ssql = ssql & " SELECT  *  "
        ssql = ssql & " FROM PO_Requisition_Header Where CompanyID ='" & Me.CompanyID & "' AND DivisionID ='" & Me.DivisionID & "'  AND DepartmentID ='" & Me.DepartmentID & "' "
        ssql = ssql & " AND [OrderNo] ='" & OrderNo & "'   "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        da.SelectCommand = com
        da.Fill(dt)


        Return dt
    End Function


    Public Function PODETAILS(ByVal OrderNo As String) As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim LocationName As String = ""
        Dim dt As New DataTable

        'From [Enterprise].[dbo].[PurchaseDetail]
        'Order By [PurchaseLineNumber] DESC

        ssql = ssql & " SELECT  *  "
        ssql = ssql & " FROM [PO_Requisition_Details] Where CompanyID ='" & Me.CompanyID & "' AND DivisionID ='" & Me.DivisionID & "'  AND DepartmentID ='" & Me.DepartmentID & "' "
        ssql = ssql & "  AND  (ISNULL([FromReq],'') <> '' OR   ISNULL([ToReq],'') <> '') AND [OrderNo] ='" & OrderNo & "'   "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        da.SelectCommand = com
        da.Fill(dt)


        Return dt
    End Function


    Protected Sub OrderHeaderGrid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles OrderHeaderGrid.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim Panel1 As New Panel
            Panel1 = e.Row.FindControl("Panel1")


            Dim lblfromreq As New Label
            lblfromreq = e.Row.FindControl("lblfromreq")

            Dim lbltoreq As New Label
            lbltoreq = e.Row.FindControl("lbltoreq")

            Dim lblstatus As New Label
            lblstatus = e.Row.FindControl("lblstatus")

            Dim lblOrderNumber As New Label
            lblOrderNumber = e.Row.FindControl("lblOrderNumber")


            Dim lblOrderPlacedDate As New Label
            lblOrderPlacedDate = e.Row.FindControl("lblOrderPlacedDate")

            Dim objtm As New clsCompanyLocalTime

            Try
                If lblOrderPlacedDate.Text <> "1/1/1900 12:00:00 AM" And lblOrderPlacedDate.Text <> "" Then
                    Dim gmtdt As New DateTime
                    gmtdt = lblOrderPlacedDate.Text
                    gmtdt = objtm.populateCMPTime(CompanyID, DivisionID, DepartmentID, gmtdt)
                    lblOrderPlacedDate.Text = gmtdt
                Else
                    'WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("PostedDate") = "NULL"
                End If
            Catch ex As Exception
            End Try

            lblfromreq.Text = ""
            lbltoreq.Text = ""

            Dim dt_ As New DataTable
            dt_ = PODETAILS(lblOrderNumber.Text)
            If dt_.Rows.Count <> 0 Then
                Dim m As Integer = 0
                For m = 0 To dt_.Rows.Count - 1
                    Dim frmrq As String = ""
                    Dim torq As String = ""
                    Try
                        frmrq = dt_.Rows(m)("FromReq")
                    Catch ex As Exception

                    End Try

                    Try
                        torq = dt_.Rows(m)("ToReq")
                    Catch ex As Exception

                    End Try

                    If frmrq <> "" Then
                        If m = 0 Then
                            lblfromreq.Text = lblfromreq.Text & frmrq
                        Else
                            lblfromreq.Text = lblfromreq.Text & "," & frmrq
                        End If

                    End If

                    If torq <> "" Then
                        If m = 0 Then
                            lbltoreq.Text = lbltoreq.Text & torq
                        Else
                            lbltoreq.Text = lbltoreq.Text & "," & torq
                        End If

                    End If

                Next
            End If

            Dim imgCancel As New ImageButton
            imgCancel = e.Row.FindControl("imgCancel")

            Dim dt3 As New DataTable
            dt3 = POHeaderDETAILS(lblOrderNumber.Text)
            If dt3.Rows.Count <> 0 Then
                Dim canceled As Boolean = False
                Try
                    canceled = dt3.Rows(0)("Canceled")
                Catch ex As Exception

                End Try

                If canceled Then
                    Panel1.Visible = False
                    lblOrderNumber.Visible = True
                    lblOrderNumber.ToolTip = "This Request Canceled"
                    Try
                        imgCancel.Visible = False
                    Catch ex As Exception

                    End Try


                    lblstatus.Text = "Canceled"
                    lblstatus.ForeColor = Drawing.Color.Red
                End If

            End If

        End If
    End Sub

    Dim SortExpression As String = "ShipDate"

    Private Sub OrderHeaderGrid_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs) Handles OrderHeaderGrid.Sorting
        Try
            If Session("SortDirection") = "Asc" Then
                Session("SortDirection") = "Desc"
            Else
                Session("SortDirection") = "Asc"
            End If
        Catch ex As Exception
            Session("SortDirection") = "Asc"
        End Try


        SortExpression = e.SortExpression

        fillitems(txtpageno.Text)

        ''e.SortDirection 
        ''e.SortExpression 
    End Sub
End Class

