Imports System.Data
Imports System.Data.SqlClient
Imports DAL

Partial Class Requisition
    Inherits System.Web.UI.Page

    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""

    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim filters As EnterpriseCommon.Core.FilterSet
        filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
        CompanyID = filters!CompanyID
        DivisionID = filters!DivisionID
        DepartmentID = filters!DepartmentID

        If IsPostBack = False Then
            SetLocationIDdropdown()
        End If


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
        Dim locationid As String = ""
        Try
            locationid = Session("Locationid")
        Catch ex As Exception

        End Try
        If locationid <> "Corporate" Then
            cmblocationid.SelectedIndex = cmblocationid.Items.IndexOf(cmblocationid.Items.FindByValue(locationid))
            cmblocationid.Enabled = False
        End If

    End Sub


    Protected Sub btnRequisition_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRequisition.Click

        Insert()
        Response.Redirect("BatchPO.aspx")

    End Sub

    'CREATE TABLE [dbo].[BatchPOSample](
    '    [CompanyID] [nvarchar](50) NULL,
    ' [DivisionID] [nvarchar](50) NULL,
    ' [DepartmentID] [nvarchar](50) NULL,
    'Store- [LocationID] [nvarchar](50) NULL,
    ' [Product] [nvarchar](250) NULL,
    ' [Type] [nvarchar](50) NULL,
    ' [PONumber] [nvarchar](50) NULL,
    ' [QOH] [nvarchar](50) NULL,
    ' [PreSold] [nvarchar](50) NULL,
    ' [Requested] [nvarchar](50) NULL,
    ' [ColorVerity] [nvarchar](50) NULL,
    ' [PoNotes] [nvarchar](250) NULL,

    Public Function Insert() As Boolean
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "insert into BatchPOSample( CompanyID, DivisionID, DepartmentID, LocationID,Product,Type,PONumber,QOH,PreSold,Requested,ColorVerity,PoNotes) " _
             & " values(@CompanyID,@DivisionID,@DepartmentID,@LocationID,@Product,@Type,@PONumber,@QOH,@PreSold,@Requested,@ColorVerity,@PoNotes)"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)


        com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = "MG"
        com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar)).Value = Me.DivisionID
        com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar)).Value = Me.DepartmentID
        com.Parameters.Add(New SqlParameter("@LocationID", SqlDbType.NVarChar)).Value = cmblocationid.SelectedValue
        com.Parameters.Add(New SqlParameter("@Product", SqlDbType.NVarChar)).Value = txtProduct.Text
        com.Parameters.Add(New SqlParameter("@Type", SqlDbType.NVarChar)).Value = txtType.Text
        com.Parameters.Add(New SqlParameter("@PONumber", SqlDbType.NVarChar)).Value = txtPONumber.Text
        com.Parameters.Add(New SqlParameter("@QOH", SqlDbType.NVarChar)).Value = txtQOH.Text
        com.Parameters.Add(New SqlParameter("@PreSold", SqlDbType.NVarChar)).Value = txtPreSold.Text
        com.Parameters.Add(New SqlParameter("@Requested", SqlDbType.NVarChar)).Value = txtRequested.Text
        com.Parameters.Add(New SqlParameter("@ColorVerity", SqlDbType.NVarChar)).Value = txtColorVerity.Text
        com.Parameters.Add(New SqlParameter("@PoNotes", SqlDbType.NVarChar)).Value = txtPoNotes.Text

        com.Connection.Open()
        com.ExecuteNonQuery()
        com.Connection.Close()

        Return True
        Try
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False

        End Try
    End Function


End Class

