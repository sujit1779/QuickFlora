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


Partial Class MTDuplicate
    Inherits System.Web.UI.Page

    Public ConnectionString As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim EmployeeID As String = ""


    Dim TransferNumber As String = ""
    Dim newTransferNumber As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

        If Request.QueryString("TransferNumber") <> String.Empty Then
            TransferNumber = Request.QueryString("TransferNumber")

        End If

        newTransferNumber = DuplicatePurchaseOrderNumberDetails(CompanyID, DivisionID, DepartmentID, TransferNumber)

        If IsNumeric(newTransferNumber.Trim) Then
            'UpdateIsDuplicate(OrdNewNumber)
            Response.Redirect("InventoryTransferDetaila.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&TransferNumber=" & newTransferNumber)
        End If


    End Sub


    Public Function DuplicatePurchaseOrderNumberDetails(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal TransferNumber As String) As String

        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand("DuplicateInventoryTransferDetails", myCon)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
        parameterCompanyID.Value = CompanyID
        myCommand.Parameters.Add(parameterCompanyID)

        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
        parameterDivisionID.Value = DivisionID
        myCommand.Parameters.Add(parameterDivisionID)

        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
        parameterDepartmentID.Value = DepartmentID
        myCommand.Parameters.Add(parameterDepartmentID)

        Dim pTransferNumber As New SqlParameter("@TransferNumber", Data.SqlDbType.NVarChar)
        pTransferNumber.Value = TransferNumber
        myCommand.Parameters.Add(pTransferNumber)



        Dim ReturnTransferNumber As New SqlParameter("@ReturnTransferNumber", Data.SqlDbType.NVarChar, 50)
        ReturnTransferNumber.Direction = ParameterDirection.Output
        myCommand.Parameters.Add(ReturnTransferNumber)

        myCon.Open()
        myCommand.ExecuteNonQuery()
        myCon.Close()
        Dim res As String = ""

        If ReturnTransferNumber.Value.ToString() <> "" Then
            res = ReturnTransferNumber.Value
        End If


        Return res

    End Function

       


End Class
