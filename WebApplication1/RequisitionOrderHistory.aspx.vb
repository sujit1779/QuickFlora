Imports System.Data
Imports System.Data.SqlClient
Imports DAL

Partial Class RequisitionOrderHistory
    Inherits System.Web.UI.Page


    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim EmployeeID As String = ""

    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    Public Function localDatetime(ByVal datePST As DateTime) As DateTime

        Dim datePST_Local As New DateTime

        datePST_Local = datePST.AddHours(3)

        Return datePST_Local
    End Function

    Public Function BatchPOSampleRow(ByVal RowNumber As Integer) As String
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim LocationName As String = ""
        Dim dt As New DataTable

        ssql = ssql & " SELECT  *  "
        ssql = ssql & " FROM PO_Requisition_Details Where CompanyID ='" & Me.CompanyID & "' AND DivisionID ='" & Me.DivisionID & "'  AND DepartmentID ='" & Me.DepartmentID & "' "
        ssql = ssql & " AND InLineNumber =" & RowNumber
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        da.SelectCommand = com
        da.Fill(dt)

        Dim _Product_ As String = ""

        If dt.Rows.Count <> 0 Then
            Try
                _Product_ = dt.Rows(0)("Product")
            Catch ex As Exception

            End Try
        End If


        Return "--" & _Product_
    End Function



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Dim filters As EnterpriseCommon.Core.FilterSet
        filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
        CompanyID = filters!CompanyID
        DivisionID = filters!DivisionID
        DepartmentID = filters!DepartmentID
        EmployeeID = Session("EmployeeID")

        If IsPostBack = False Then
            SetLocationIDdropdown()

            Dim OrderNumber As String = ""
            Try
                OrderNumber = Request.QueryString("OrderNumber")
            Catch ex As Exception

            End Try

            SetOrderDataHeader(OrderNumber)
            SetOrderDataDetails(OrderNumber)


        End If


    End Sub

    Public Sub SetOrderDataHeader(ByVal OrderNumber As String)

        ''[ChangeHistoryRequisition](OrderNumber,CustomerID, EmployeeID, tableName, fieldName, FieldInitValue, fieldChangeValue,CompanyID,DivisionID,DepartmentID) 

        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT * FROM [ChangeHistoryRequisition] where tableName='PO_Requisition_Header' AND CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and OrderNumber=@OrderNumber"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@OrderNumber", SqlDbType.NVarChar, 36)).Value = OrderNumber
            da.SelectCommand = com
            da.Fill(dt)


            If dt.Rows.Count <> 0 Then
                OrderHeaderGrid.DataSource = dt
                OrderHeaderGrid.DataBind()

                'cmblocationid.SelectedValue = dt.Rows(0)("Location")
                'txtRemarks.Text = dt.Rows(0)("Remarks")
                'drpStatus.SelectedValue = dt.Rows(0)("Status")
                'drpType.SelectedValue = dt.Rows(0)("Type")
                'txtlastchanged.Text = dt.Rows(0)("LastChangeDateTime")
                'txtlastchangedby.Text = dt.Rows(0)("LastChangeBy")
                'txttotal.Text = dt.Rows(0)("TotalAmount")
                'txtshipdate.Text = dt.Rows(0)("ShipDate")
                'txtarrivedate.Text = dt.Rows(0)("ArriveDate")
                'txtorderplaced.Text = dt.Rows(0)("OrderPlacedDate")
                'txtrecvon.Text = dt.Rows(0)("ReceivedOnDate")
                'txtorderby.Text = dt.Rows(0)("OrderBy")
                'txtrecvby.Text = dt.Rows(0)("ReceivedBy")
            End If
        Catch ex As Exception
        End Try
    End Sub


    Public Sub SetOrderDataDetails(ByVal OrderNumber As String)

        ''[ChangeHistoryRequisition](OrderNumber,CustomerID, EmployeeID, tableName, fieldName, FieldInitValue, fieldChangeValue,CompanyID,DivisionID,DepartmentID) 

        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT * FROM [ChangeHistoryRequisition] where  tableName='PO_Requisition_Details' AND CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and OrderNumber=@OrderNumber"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@OrderNumber", SqlDbType.NVarChar, 36)).Value = OrderNumber
            da.SelectCommand = com
            da.Fill(dt)


            If dt.Rows.Count <> 0 Then
                OrderHeaderGrid2.DataSource = dt
                OrderHeaderGrid2.DataBind()

                'cmblocationid.SelectedValue = dt.Rows(0)("Location")
                'txtRemarks.Text = dt.Rows(0)("Remarks")
                'drpStatus.SelectedValue = dt.Rows(0)("Status")
                'drpType.SelectedValue = dt.Rows(0)("Type")
                'txtlastchanged.Text = dt.Rows(0)("LastChangeDateTime")
                'txtlastchangedby.Text = dt.Rows(0)("LastChangeBy")
                'txttotal.Text = dt.Rows(0)("TotalAmount")
                'txtshipdate.Text = dt.Rows(0)("ShipDate")
                'txtarrivedate.Text = dt.Rows(0)("ArriveDate")
                'txtorderplaced.Text = dt.Rows(0)("OrderPlacedDate")
                'txtrecvon.Text = dt.Rows(0)("ReceivedOnDate")
                'txtorderby.Text = dt.Rows(0)("OrderBy")
                'txtrecvby.Text = dt.Rows(0)("ReceivedBy")
            End If
        Catch ex As Exception
        End Try
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
            cmblocationid.Items.Clear()
            Dim item As New ListItem
            item.Text = "DEFAULT"
            item.Value = "DEFAULT"
            cmblocationid.Items.Add(item)
        End If
        ''''''''''''''''''''


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


    Private Sub OrderHeaderGrid2_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles OrderHeaderGrid2.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lblfieldName As New Label
            lblfieldName = e.Row.FindControl("lblfieldName")

            If txtSearchExpression.Text <> "" Then
                If txtSearchExpression.Text.Trim <> lblfieldName.Text.Trim Then
                    e.Row.Visible = False
                End If
            End If

        End If
    End Sub

    Private Sub btnSearchExpression_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearchExpression.Click
        Dim OrderNumber As String = ""
        Try
            OrderNumber = Request.QueryString("OrderNumber")
        Catch ex As Exception

        End Try
        SetOrderDataHeader(OrderNumber)
        SetOrderDataDetails(OrderNumber)
    End Sub

    Private Sub btnreset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnreset.Click
        Dim OrderNumber As String = ""
        Try
            OrderNumber = Request.QueryString("OrderNumber")
        Catch ex As Exception

        End Try
        txtSearchExpression.Text = ""
        SetOrderDataHeader(OrderNumber)
        SetOrderDataDetails(OrderNumber)
    End Sub

End Class

