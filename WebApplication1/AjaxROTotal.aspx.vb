Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Partial Class AjaxROTotal
    Inherits System.Web.UI.Page

    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim EmployeeID As String = ""
    Dim PurchaseNumber As String = ""

    Private Sub AjaxROTotal_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim total As Decimal = 0
        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

        If Not Request.QueryString("PurchaseNumber") = Nothing Then
            PurchaseNumber = Request.QueryString("PurchaseNumber")
            total = SetOrderProductData(PurchaseNumber)
            Response.Clear()
            Response.Write(Format(total, "0.00"))
            Response.End()

        End If
    End Sub



    Public Function SetOrderProductData(ByVal PurchaseNumber As String) As Decimal
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT * FROM [PO_Requisition_Details] where  OrderNo = @OrderNo and CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2  "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
        com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
        com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
        com.Parameters.Add(New SqlParameter("@OrderNo", SqlDbType.NVarChar, 36)).Value = PurchaseNumber
        da.SelectCommand = com
        da.Fill(dt)

        '        Response.Write(dt.Rows.Count)
        Dim total As Decimal = 0
        If dt.Rows.Count <> 0 Then
            Dim n As Integer = 0
            For n = 0 To dt.Rows.Count - 1
                Dim txtQ_ORD As Integer = 0
                Dim txtPACK As Decimal = 0
                Dim txtCOST As Decimal = 0
                Dim txtExt_COSt As Decimal = 0

                Try
                    txtQ_ORD = dt.Rows(n)("Q_ORD")
                Catch ex As Exception

                End Try
                Try
                    txtPACK = dt.Rows(n)("PACK")
                Catch ex As Exception

                End Try
                Try
                    txtCOST = dt.Rows(n)("COST")
                Catch ex As Exception

                End Try
                txtExt_COSt = (txtQ_ORD * txtPACK * txtCOST)
                total = total + txtExt_COSt
            Next
            Return total
        Else
            Return 0
        End If
        Try
        Catch ex As Exception

        End Try

        Return 0
    End Function


    Public Sub updatetotal(ByVal total As Decimal)



    End Sub


End Class
