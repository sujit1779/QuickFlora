Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Data

Public Class BackOrder


    Public Function CheckOrderQty(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal ItemID As String, ByVal Quantity As Decimal) As Integer
        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand("CheckOrderQty", myCon)
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

        Dim parameterItemID As New SqlParameter("@ItemID", Data.SqlDbType.NVarChar)
        parameterItemID.Value = ItemID
        myCommand.Parameters.Add(parameterItemID)

        Dim parameterQuatity As New SqlParameter("@Quantity", Data.SqlDbType.Float)
        parameterQuatity.Value = Quantity
        myCommand.Parameters.Add(parameterQuatity)


        Dim parameterPostingResult As New SqlParameter("@BackOrderStatus", Data.SqlDbType.Int)
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
