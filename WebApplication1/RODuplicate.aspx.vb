Imports System.Data
Imports System.Data.SqlClient
Imports DAL
Imports EnterpriseCommon.Configuration
Imports EnterpriseASPClient.Core
Imports EnterpriseClient.Core
Imports System.Diagnostics
Imports System.Net.Mail
Imports System.Text.RegularExpressions
Imports System.IO

Partial Class RODuplicate
    Inherits System.Web.UI.Page

    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim OrderNumber As String = ""

    Private Sub RODuplicate_Load(sender As Object, e As EventArgs) Handles Me.Load

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")


        If Not IsNothing(Request.QueryString("OrderNumber")) Then
            OrderNumber = Request.QueryString("OrderNumber")
            If Not IsPostBack Then
                Dim rs As SqlDataReader
                Dim OrderNo As String = ""


                Dim PopOrderNo As New CustomOrder()
                If (OrderNumber) <> "" Then
                    rs = PopOrderNo.GetNextOrdNumber(CompanyID, DepartmentID, DivisionID, "NextRequisitionNumber")
                    While rs.Read()
                        OrderNo = rs("NextNumberValue")
                    End While
                    rs.Close()

                    GetDuplicateOrdNumber(CompanyID, DepartmentID, DivisionID, OrderNumber, OrderNo)


                    If Me.CompanyID = "DierbergsMarkets,Inc63017" Or Me.CompanyID = "QuickfloraDemo" Then
                        'newmenu = "RequisitionOrderDB.aspx"
                        HyperLink1.NavigateUrl = "RequisitionOrderDB.aspx?OrderNo=" & OrderNo & "&type=dup"
                    Else
                        HyperLink1.NavigateUrl = "RequisitionOrder.aspx?OrderNo=" & OrderNo & "&type=dup"
                    End If

                    HyperLink1.Text = "Click here to edit new Requisition #" & OrderNo

                    HyperLink2.NavigateUrl = "RequisitionOrderList.aspx"
                Else

                End If
            End If
        End If

    End Sub


#Region "GetDuplicateOrdNumber"

    Public Function GetDuplicateOrdNumber(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal OrderNumber As String, ByVal OrderNo As String) As String
        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand("[enterprise].GetDuplicateROrdNumber", myCon)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
        parameterCompanyID.Value = CompanyID
        myCommand.Parameters.Add(parameterCompanyID)

        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
        parameterDepartmentID.Value = DepartmentID
        myCommand.Parameters.Add(parameterDepartmentID)

        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
        parameterDivisionID.Value = DivisionID
        myCommand.Parameters.Add(parameterDivisionID)


        Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
        parameterOrderNumber.Value = OrderNumber
        myCommand.Parameters.Add(parameterOrderNumber)

        Dim parameterEntity As New SqlParameter("@OrderNo", Data.SqlDbType.NVarChar)
        parameterEntity.Value = OrderNo
        myCommand.Parameters.Add(parameterEntity)

        myCon.Open()
        Try
            myCommand.ExecuteNonQuery()
        Catch ex As Exception
            lblGtotal.Text = ex.Message
        End Try
        myCon.Close()

        Return ""
    End Function
#End Region

End Class
