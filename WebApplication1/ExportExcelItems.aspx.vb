Imports System.Data
Imports System.Data.SqlClient
Imports DAL

Partial Class ExportExcelItems
    Inherits System.Web.UI.Page

    Public CompanyID As String = ""
    Public DivisionID As String = ""
    Public DepartmentID As String = ""
    Public EmployeeID As String = ""

    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")


    Public Function SetOrderProductData(ByVal OrderNo As String) As DataTable
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT * FROM [PO_Requisition_Details] where  OrderNo = @OrderNo and CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 order by InLineNumber DESC "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
        com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
        com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
        com.Parameters.Add(New SqlParameter("@OrderNo", SqlDbType.NVarChar, 36)).Value = OrderNo
        da.SelectCommand = com
        da.Fill(dt)

        Return dt
    End Function


    Public Function GetInventoryItemsList(Optional ByVal ItemID As String = "", Optional ByVal ItemName As String = "", Optional ByVal ItemType As String = "", _
                                          Optional ByVal Location As String = "", Optional ByVal VendorID As String = "") As DataTable

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetInventoryItemsListExport]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)

                Command.Parameters.AddWithValue("@ItemID", ItemID)
                Command.Parameters.AddWithValue("@ItemName", ItemName)
                Command.Parameters.AddWithValue("@ItemType", ItemType)

                Command.Parameters.AddWithValue("@Location", Location)
                Command.Parameters.AddWithValue("@VendorID", VendorID)

                Try
                    Dim ds As New DataTable
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)

                    If ds.Rows.Count >= 0 Then
                        Return ds
                    Else
                        Return Nothing
                    End If

                Catch ex As Exception
                    'Debug = ex.Message
                    Return Nothing

                End Try

            End Using
        End Using

    End Function



    Private Sub ExportExcelROL_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load


        Dim filters As EnterpriseCommon.Core.FilterSet
        filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
        CompanyID = filters!CompanyID
        DivisionID = filters!DivisionID
        DepartmentID = filters!DepartmentID
        EmployeeID = Session("EmployeeID")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""


        Dim dt As New DataTable

        Dim p1 As String = ""
        Dim p2 As String = ""
        Dim p3 As String = ""
        Dim p4 As String = ""
        Dim p5 As String = ""
        Try
            p1 = Session("1")
        Catch ex As Exception

        End Try

        Try
            p2 = Session("2")
        Catch ex As Exception

        End Try
        Try
            p3 = Session("3")
        Catch ex As Exception

        End Try
        Try
            p4 = Session("4")
        Catch ex As Exception

        End Try
        Try
            p5 = Session("5")
        Catch ex As Exception

        End Try

        dt = GetInventoryItemsList(p1, p2, p3, p4, p5)

        If dt.Rows.Count <> 0 Then

            Response.ClearContent()
            Response.Buffer = True
            Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", "ItemsList.xls"))
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
