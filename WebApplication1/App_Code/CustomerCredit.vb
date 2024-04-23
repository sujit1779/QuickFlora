Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Data
Public Class CustomerCredit



    Public Function CheckCustomerCredit(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal OrdNumber As String, ByVal Total As Decimal, ByVal CustomerID As String) As Integer


        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))


        Dim myCommand As New SqlCommand("CheckCustomerCredit", myCon)
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

        Dim parameterOrdNumber As New SqlParameter("@OrdNumber", Data.SqlDbType.NVarChar)
        parameterOrdNumber.Value = OrdNumber
        myCommand.Parameters.Add(parameterOrdNumber)

        Dim parameterTotal As New SqlParameter("@Total", Data.SqlDbType.Float)
        parameterTotal.Value = Total
        myCommand.Parameters.Add(parameterTotal)

        Dim parameterCustomerID As New SqlParameter("@CustomerID", Data.SqlDbType.NVarChar, 36)
        parameterCustomerID.Value = CustomerID
        myCommand.Parameters.Add(parameterCustomerID)


        Dim parameterPostingResult As New SqlParameter("@CustomerOrderStatus", Data.SqlDbType.Int)
        parameterPostingResult.Direction = ParameterDirection.Output
        myCommand.Parameters.Add(parameterPostingResult)

        Dim OutputValue As String = ""
        myCon.Open()

        myCommand.ExecuteNonQuery()

        OutputValue = parameterPostingResult.Value.ToString()

        myCon.Close()
        Return OutputValue


    End Function
End Class
