Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data.SqlClient

Imports System.Data


Public Class AddressClass
    Public Function CompanyDetails(ByVal CmpID As String) As Data.SqlClient.SqlDataReader
        Dim conString As New SqlConnection
        conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")
        conString.Open()
        Dim sqlCommand As New Data.SqlClient.SqlCommand
        Dim sqlStr As String = "SELECT CompanyName,CompanyAddress1,CompanyCity,CompanyState,CompanyZip,CompanyCountry,CompanyPhone,CompanyFax,CompanyWebAddress,CompanyEmail,CompanyLogoUrl FROM companies where companyid='" & CmpID & "'"
        sqlCommand.CommandText = sqlStr
        sqlCommand.Connection = conString
        Dim rs1 As Data.SqlClient.SqlDataReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection)
        CompanyDetails = rs1
    End Function

#Region "Picking sessionTimeOut Value from DB"
    Public Function PicksessionTimeOut(ByVal compID As String, ByVal DivID As String, ByVal DeptID As String) As Data.SqlClient.SqlDataReader
        Dim conString As New SqlConnection
        conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")
        conString.Open()
        Dim sqlCommand As New Data.SqlClient.SqlCommand
        Dim sqlStr As String = "SELECT SessionTimeOut FROM HomePageManagement WHERE companyid='" & compID & "' " _
                            & " AND DivisionID='" & DivID & "' AND DepartmentID='" & DeptID & "'"

        sqlCommand.CommandText = sqlStr
        sqlCommand.Connection = conString
        Dim rs1 As Data.SqlClient.SqlDataReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection)
        PicksessionTimeOut = rs1
    End Function
#End Region

#Region "Exact URL of the Company"
    Public Function GetCompURL(ByVal ExactURL As String) As String
        Dim myurl As String = ""
        myurl = ExactURL
        myurl = "http://quickflorafrontend.quickflora.com/admin/enterpriseASP/Default.aspx"
        If myurl.IndexOf("/Admin/") > 0 Or myurl.IndexOf("/admin/") > 0 Then
            Dim aspxPosition = myurl.IndexOf("/Admin")
            myurl = myurl.Substring(0, aspxPosition)

        End If
        If myurl.IndexOf("quickflora.com") <= 0 And myurl.Substring(0, 11) = ("http://www.") Then
            myurl = myurl.Replace("http://www.", "http://")
        End If
        Return myurl
    End Function

    Public Function GetCompanyDivisionDepartment(ByVal CompURL As String) As String
        Dim conString As New SqlConnection
        Dim CompanyIDDivisionIDDepartmentID As String = ""
        conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")
        conString.Open()
        Dim sqlCommand As New Data.SqlClient.SqlCommand
        Dim sqlStr = "SELECT CompanyID, DivisionID, DepartmentID FROM HomePageManagement " _
                    & "WHERE FrontEndURL='" & CompURL & "'"
        Dim rs As SqlDataReader
        sqlCommand.CommandText = sqlStr
        sqlCommand.Connection = conString
        rs = sqlCommand.ExecuteReader()

        While rs.Read()
            CompanyIDDivisionIDDepartmentID = rs("CompanyID").ToString() & "~" & rs("DivisionID").ToString() & "~" & rs("DepartmentID").ToString()
        End While
        rs.Close()
        conString.Close()
        Return CompanyIDDivisionIDDepartmentID
    End Function
#End Region

#Region "Populating Category and products for Add ons"
    Public Function PopCate(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As DataSet
        Dim conString As New SqlConnection
        conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")
        conString.Open()
        Dim sqlCommand As New Data.SqlClient.SqlCommand
        Dim sqlStr As String = "SELECT * FROM InventoryCategories where " _
                                & "CompanyID='" & CompanyID & "' AND " _
                                & "DivisionID='" & DivisionID & "' AND " _
                                & "DepartmentID='" & DepartmentID & "'"
        sqlCommand.CommandText = sqlStr
        sqlCommand.Connection = conString

        Dim adapter As New SqlDataAdapter(sqlCommand)
        Dim ds As New DataSet
        adapter.Fill(ds)
        conString.Close()

        Return ds

    End Function
    Public Function PopProd(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal CategoryID As String) As DataSet
        Dim conString As New SqlConnection
        conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

        Dim sqlCommand As New Data.SqlClient.SqlCommand
        Dim sqlStr As String = "SELECT ItemID,ItemName FROM InventoryItems where " _
                                & "CompanyID='" & CompanyID & "' AND " _
                                & "DivisionID='" & DivisionID & "' AND " _
                                & "DepartmentID='" & DepartmentID & "' AND " _
                                & "ItemCategoryID='" & CategoryID & "'"
        sqlCommand.CommandText = sqlStr
        sqlCommand.Connection = conString
        conString.Open()
        Dim adapter As New SqlDataAdapter(sqlCommand)
        Dim ds As New DataSet
        adapter.Fill(ds)
        conString.Close()
        Return ds
    End Function
#End Region

    Sub SaveRetailerLogo(ByVal filename As String, ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String)


        Dim conString As New SqlConnection
        conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

        Dim myCommand As New SqlCommand("SaveRetailerLogo", conString)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        Dim parameterFileName As New SqlParameter("@filename", Data.SqlDbType.NVarChar)
        parameterFileName.Value = filename
        myCommand.Parameters.Add(parameterFileName)

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
        parameterCompanyID.Value = CompanyID
        myCommand.Parameters.Add(parameterCompanyID)


        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
        parameterDivisionID.Value = DivisionID
        myCommand.Parameters.Add(parameterDivisionID)


        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
        parameterDepartmentID.Value = DepartmentID
        myCommand.Parameters.Add(parameterDepartmentID)

        conString.Open()
        myCommand.ExecuteNonQuery()
        conString.Close()



    End Sub


    Sub PriorityDetails(ByVal PriorirtyID As String, ByVal PriorirtyDesc As String, ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String)


        Dim conString As New SqlConnection
        conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

        Dim myCommand As New SqlCommand("PriorityDetails", conString)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        Dim parameterPriorirtyID As New SqlParameter("@PriorirtyID", Data.SqlDbType.NVarChar)
        parameterPriorirtyID.Value = PriorirtyID
        myCommand.Parameters.Add(parameterPriorirtyID)

        Dim parameterPriorirtyDesc As New SqlParameter("@PriorirtyDesc", Data.SqlDbType.NVarChar)
        parameterPriorirtyDesc.Value = PriorirtyDesc
        myCommand.Parameters.Add(parameterPriorirtyDesc)

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
        parameterCompanyID.Value = CompanyID
        myCommand.Parameters.Add(parameterCompanyID)


        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
        parameterDivisionID.Value = DivisionID
        myCommand.Parameters.Add(parameterDivisionID)


        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
        parameterDepartmentID.Value = DepartmentID
        myCommand.Parameters.Add(parameterDepartmentID)

        conString.Open()
        myCommand.ExecuteNonQuery()
        conString.Close()



    End Sub


    Public Function AddOccasionDesc(ByVal Occasion As String, ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As Integer



        Dim conString As New SqlConnection
        conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

        Dim myCommand As New SqlCommand("AddOccasionDesc", conString)
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


        Dim parameterOccasionDesc As New SqlParameter("@OccasionDesc", Data.SqlDbType.NVarChar)
        parameterOccasionDesc.Value = Occasion
        myCommand.Parameters.Add(parameterOccasionDesc)

        Dim paramReturnValue As New SqlParameter("@ReturnValue", Data.SqlDbType.Int)
        paramReturnValue.Direction = ParameterDirection.Output

        myCommand.Parameters.Add(paramReturnValue)

        Dim OutPutValue As Integer


        conString.Open()
        myCommand.ExecuteNonQuery()
        conString.Close()
        OutPutValue = Convert.ToInt32(paramReturnValue.Value)

        Return OutPutValue



    End Function
    Public Function UpdateOccasionDesc(ByVal Occasion As String, ByVal OccasionId As Integer, ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As Integer



        Dim conString As New SqlConnection
        conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

        Dim myCommand As New SqlCommand("UpdateOccasionDesc", conString)
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


        Dim parameterOccasionId As New SqlParameter("@OccasionId", Data.SqlDbType.Int)
        parameterOccasionId.Value = OccasionId
        myCommand.Parameters.Add(parameterOccasionId)

        Dim parameterOccasionDesc As New SqlParameter("@OccasionDesc", Data.SqlDbType.NVarChar)
        parameterOccasionDesc.Value = Occasion
        myCommand.Parameters.Add(parameterOccasionDesc)



        Dim paramReturnValue As New SqlParameter("@ReturnValue", Data.SqlDbType.Int)
        paramReturnValue.Direction = ParameterDirection.Output

        myCommand.Parameters.Add(paramReturnValue)

        Dim OutPutValue As Integer


        conString.Open()
        myCommand.ExecuteNonQuery()
        conString.Close()
        OutPutValue = Convert.ToInt32(paramReturnValue.Value)

        Return OutPutValue



    End Function

    Sub DeleteOccasionDesc(ByVal OccasionId As Integer)


        Dim conString As New SqlConnection
        conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

        Dim myCommand As New SqlCommand("DeleteOccasionDesc", conString)
        myCommand.CommandType = Data.CommandType.StoredProcedure


        Dim parameterOccasionId As New SqlParameter("@OccasionId", Data.SqlDbType.Int)
        parameterOccasionId.Value = OccasionId
        myCommand.Parameters.Add(parameterOccasionId)


        conString.Open()
        myCommand.ExecuteNonQuery()
        conString.Close()



    End Sub
    Public Function PopulateOccasionDesc(ByVal Occasion As Integer, ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As SqlDataReader



        Dim conString As New SqlConnection
        conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

        Dim myCommand As New SqlCommand("PopulateOccasionDesc", conString)
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


        Dim parameterOccasionDesc As New SqlParameter("@OccasionID", Data.SqlDbType.Int)
        parameterOccasionDesc.Value = Occasion
        myCommand.Parameters.Add(parameterOccasionDesc)

        conString.Open()
        Dim rs As SqlDataReader
        rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
        Return rs






    End Function
    Sub AddPriorityDesc(ByVal PriorirtyDesc As String, ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String)



        Dim conString As New SqlConnection
        conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

        Dim myCommand As New SqlCommand("AddPriorityDesc", conString)
        myCommand.CommandType = Data.CommandType.StoredProcedure


        Dim parameterPriorirtyDesc As New SqlParameter("@PriorityDesc", Data.SqlDbType.NVarChar)
        parameterPriorirtyDesc.Value = PriorirtyDesc
        myCommand.Parameters.Add(parameterPriorirtyDesc)

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
        parameterCompanyID.Value = CompanyID
        myCommand.Parameters.Add(parameterCompanyID)


        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
        parameterDivisionID.Value = DivisionID
        myCommand.Parameters.Add(parameterDivisionID)


        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
        parameterDepartmentID.Value = DepartmentID
        myCommand.Parameters.Add(parameterDepartmentID)

        conString.Open()
        myCommand.ExecuteNonQuery()
        conString.Close()



    End Sub

    Sub UpdatePriorityDesc(ByVal PriorirtyDesc As String, ByVal PriorirtyID As Integer, ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String)


        Dim conString As New SqlConnection
        conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

        Dim myCommand As New SqlCommand("UpdatePriorityDesc", conString)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        Dim parameterPriorirtyID As New SqlParameter("@PriorityID", Data.SqlDbType.Int)
        parameterPriorirtyID.Value = PriorirtyID
        myCommand.Parameters.Add(parameterPriorirtyID)

        Dim parameterPriorirtyDesc As New SqlParameter("@PriorityDesc", Data.SqlDbType.NVarChar)
        parameterPriorirtyDesc.Value = PriorirtyDesc
        myCommand.Parameters.Add(parameterPriorirtyDesc)

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
        parameterCompanyID.Value = CompanyID
        myCommand.Parameters.Add(parameterCompanyID)


        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
        parameterDivisionID.Value = DivisionID
        myCommand.Parameters.Add(parameterDivisionID)


        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
        parameterDepartmentID.Value = DepartmentID
        myCommand.Parameters.Add(parameterDepartmentID)

        conString.Open()
        myCommand.ExecuteNonQuery()
        conString.Close()



    End Sub
    Sub DeletePriorityDesc(ByVal PriorityId As Integer)


        Dim conString As New SqlConnection
        conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

        Dim myCommand As New SqlCommand("DeletePriorityDesc", conString)
        myCommand.CommandType = Data.CommandType.StoredProcedure


        Dim parameterPriorityId As New SqlParameter("@PriorityId", Data.SqlDbType.Int)
        parameterPriorityId.Value = PriorityId
        myCommand.Parameters.Add(parameterPriorityId)


        conString.Open()
        myCommand.ExecuteNonQuery()
        conString.Close()



    End Sub

    Public Function PopulatePriorityDesc(ByVal Priority As Integer, ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As SqlDataReader



        Dim conString As New SqlConnection
        conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

        Dim myCommand As New SqlCommand("PopulatePriorityDesc", conString)
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


        Dim parameterPriorityID As New SqlParameter("@PriorityID", Data.SqlDbType.Int)
        parameterPriorityID.Value = Priority
        myCommand.Parameters.Add(parameterPriorityID)

        conString.Open()
        Dim rs As SqlDataReader
        rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
        Return rs






    End Function

    Public Function BindOccasionCodes(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As DataSet


        Dim conString As New SqlConnection
        conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

        Dim myCommand As New SqlCommand("BindOccasionCodes", conString)
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

        Dim adapter As New SqlDataAdapter(myCommand)
        Dim ds As New DataSet
        adapter.Fill(ds)
        conString.Close()

        Return ds



    End Function

End Class


