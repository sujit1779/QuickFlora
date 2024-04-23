Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Data


Public Class clsAffiliate


    Public Function PopulateAffiliateList(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As DataSet

        Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        ConString.Open()

        Dim myCommand As New SqlCommand("[PopulateAffiliateList]", ConString)
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

        Dim adapter As New SqlDataAdapter(myCommand)
        Dim ds As New DataSet

        adapter.Fill(ds)
        ConString.Close()
        Return ds

    End Function

    Public Function PopulateAffiliateByID(ByVal AffiliateID As Integer) As SqlDataReader
        Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        ConString.Open()

        Dim myCommand As New SqlCommand("[PopulateAffiliateByID]", ConString)
        myCommand.CommandType = CommandType.StoredProcedure

        Dim parameterAffiliateID As New SqlParameter("@AffiliateID", SqlDbType.Int)
        parameterAffiliateID.Value = AffiliateID
        myCommand.Parameters.Add(parameterAffiliateID)

        Dim rs As SqlDataReader
        rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
        Return rs

    End Function


    Public Function SaveAffiliate(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal AffiliateName As String, ByVal AffiliateCode As String, ByVal ItemImageName As String, ByVal AddEdit As String, ByVal AffiliateActive As Integer)


        Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        ConString.Open()

        Dim myCommand As New SqlCommand("SaveAffiliate", ConString)
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


        Dim prAffiliateName As New SqlParameter("@AffiliateName", Data.SqlDbType.NVarChar)
        prAffiliateName.Value = AffiliateName
        myCommand.Parameters.Add(prAffiliateName)

        Dim prAffiliateCode As New SqlParameter("@AffiliateCode", Data.SqlDbType.NVarChar)
        prAffiliateCode.Value = AffiliateCode
        myCommand.Parameters.Add(prAffiliateCode)

        Dim parameterItemImageName As New SqlParameter("@ItemImageName", Data.SqlDbType.NVarChar)
        parameterItemImageName.Value = ItemImageName
        myCommand.Parameters.Add(parameterItemImageName)

        Dim parameterAddEdit As New SqlParameter("@AddEdit", Data.SqlDbType.NVarChar)
        parameterAddEdit.Value = AddEdit
        myCommand.Parameters.Add(parameterAddEdit)


        Dim prAffiliateActive As New SqlParameter("@AffiliateActive", Data.SqlDbType.Int)
        prAffiliateActive.Value = AffiliateActive
        myCommand.Parameters.Add(prAffiliateActive)

        Dim paramReturnValue As New SqlParameter("@ReturnValue", Data.SqlDbType.Int)
        paramReturnValue.Direction = ParameterDirection.Output
        myCommand.Parameters.Add(paramReturnValue)

        myCommand.ExecuteNonQuery()

        Dim res As Integer

        ConString.Close()
        If paramReturnValue.Value.ToString() <> "" Then
            res = Convert.ToDecimal(paramReturnValue.Value)
        End If

        Return res

    End Function

    Public Sub DeleteAffiliate(ByVal AddOnItemID As Integer)
        Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        ConString.Open()

        Dim myCommand As New SqlCommand("DeleteAffiliate", ConString)
        myCommand.CommandType = CommandType.StoredProcedure

        Dim parameterAddOnItemID As New SqlParameter("@AffiliateID", SqlDbType.Int)
        parameterAddOnItemID.Value = AddOnItemID
        myCommand.Parameters.Add(parameterAddOnItemID)

        myCommand.ExecuteNonQuery()


    End Sub

End Class
