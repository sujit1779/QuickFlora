Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient


Public Class clsEmployee

    'Protected CompanyID As String, DivisionID As String, DepartmentID As String

    Public Function GetEmployeeTypeList(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As DataTable

        Dim dt As New DataTable

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using command As New SqlCommand("[enterprise].[GetEmployeeTypeList]", connection)
                command.CommandType = CommandType.StoredProcedure

                command.Parameters.AddWithValue("CompanyID", CompanyID)
                command.Parameters.AddWithValue("DivisionID", DivisionID)
                command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                Try
                    Dim da As New SqlDataAdapter(command)
                    da.Fill(dt)
                Catch ex As Exception

                End Try

            End Using
        End Using

        Return dt

    End Function

    Public Function GetEmployeeDepartmentList(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As DataTable

        Dim dt As New DataTable

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using command As New SqlCommand("[enterprise].[GetEmployeeDepartmentList]", connection)
                command.CommandType = CommandType.StoredProcedure

                command.Parameters.AddWithValue("CompanyID", CompanyID)
                command.Parameters.AddWithValue("DivisionID", DivisionID)
                command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                Try
                    Dim da As New SqlDataAdapter(command)
                    da.Fill(dt)
                Catch ex As Exception

                End Try

            End Using
        End Using

        Return dt

    End Function

    Public Function GetLocation(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As DataTable

        Dim dt As New DataTable

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using command As New SqlCommand("[enterprise].[FillLocation]", connection)
                command.CommandType = CommandType.StoredProcedure

                command.Parameters.AddWithValue("CompanyID", CompanyID)
                command.Parameters.AddWithValue("DivisionID", DivisionID)
                command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                Try
                    Dim da As New SqlDataAdapter(command)
                    da.Fill(dt)
                Catch ex As Exception

                End Try

            End Using
        End Using

        Return dt

    End Function

    Public Function GetEmployeeDetail(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal EmployeeID As String) As DataTable

        Dim dt As New DataTable

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using command As New SqlCommand("[enterprise].[GetEmployeeDetail]", connection)
                command.CommandType = CommandType.StoredProcedure

                command.Parameters.AddWithValue("CompanyID", CompanyID)
                command.Parameters.AddWithValue("DivisionID", DivisionID)
                command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                command.Parameters.AddWithValue("EmployeeID", EmployeeID)

                Try
                    Dim da As New SqlDataAdapter(command)
                    da.Fill(dt)
                Catch ex As Exception

                End Try

            End Using
        End Using

        Return dt

    End Function

    Public Function CheckIsEmployeeExists(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal EmployeeID As String) As Boolean

        Dim dt As New DataTable

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using command As New SqlCommand("[enterprise].[CheckIsEmployeeExists]", connection)
                command.CommandType = CommandType.StoredProcedure

                command.Parameters.AddWithValue("CompanyID", CompanyID)
                command.Parameters.AddWithValue("DivisionID", DivisionID)
                command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                command.Parameters.AddWithValue("EmployeeID", EmployeeID)

                Try
                    command.Connection.Open()
                    Dim count As Integer = Convert.ToInt32(command.ExecuteScalar)

                    If count = 0 Then
                        Return False
                    End If

                    Return True

                Catch ex As Exception
                    Return True
                Finally
                    command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    Public Function InsertEmployeeDetail(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal EmployeeID As String, ByVal EmployeeTypeID As String, _
    ByVal EmployeeUserName As String, ByVal EmployeePassword As String, ByVal EmployeeName As String, _
    ByVal ActiveYN As Boolean, ByVal EmployeeAddress1 As String, ByVal EmployeeAddress2 As String, ByVal EmployeeCity As String, _
    ByVal EmployeeState As String, ByVal EmployeeZip As String, ByVal EmployeeCountry As String, ByVal EmployeePhone As String, _
    ByVal EmployeeFax As String, ByVal EmployeeSSNumber As String, ByVal EmployeeEmailAddress As String, ByVal EmployeeDepartmentID As String, _
    ByVal PictureURL As String, ByVal HireDate As String, ByVal Birthday As String, _
    ByVal Commissionable As Boolean, ByVal CommissionPerc As String, ByVal CommissionCalcMethod As String, ByVal Notes As String, _
    ByVal LocationID As String, ByVal IsAdmin As String, ByVal IsMasterEmployee As String) As Boolean

        Dim dt As New DataTable

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using command As New SqlCommand("[enterprise].[InsertEmployeeDetail]", connection)
                command.CommandType = CommandType.StoredProcedure

                command.Parameters.AddWithValue("CompanyID", CompanyID)
                command.Parameters.AddWithValue("DivisionID", DivisionID)
                command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                command.Parameters.AddWithValue("EmployeeID", EmployeeID)

                command.Parameters.AddWithValue("EmployeeTypeID", EmployeeTypeID)
                command.Parameters.AddWithValue("EmployeeUserName", EmployeeUserName)

                command.Parameters.AddWithValue("EmployeePassword", EmployeePassword)
                command.Parameters.AddWithValue("EmployeeName", EmployeeName)
                command.Parameters.AddWithValue("ActiveYN", ActiveYN)
                command.Parameters.AddWithValue("EmployeeAddress1", EmployeeAddress1)

                command.Parameters.AddWithValue("EmployeeAddress2", EmployeeAddress2)
                command.Parameters.AddWithValue("EmployeeCity", EmployeeCity)
                command.Parameters.AddWithValue("EmployeeState", EmployeeState)
                command.Parameters.AddWithValue("EmployeeZip", EmployeeZip)

                command.Parameters.AddWithValue("EmployeeCountry", EmployeeCountry)
                command.Parameters.AddWithValue("EmployeePhone", EmployeePhone)
                command.Parameters.AddWithValue("EmployeeFax", EmployeeFax)
                command.Parameters.AddWithValue("EmployeeSSNumber", EmployeeSSNumber)

                command.Parameters.AddWithValue("EmployeeEmailAddress", EmployeeEmailAddress)
                command.Parameters.AddWithValue("EmployeeDepartmentID", EmployeeDepartmentID)
                command.Parameters.AddWithValue("PictureURL", PictureURL)
                command.Parameters.AddWithValue("HireDate", HireDate)

                command.Parameters.AddWithValue("Birthday", Birthday)
                command.Parameters.AddWithValue("Commissionable", Commissionable)
                command.Parameters.AddWithValue("CommissionPerc", CommissionPerc)
                command.Parameters.AddWithValue("CommissionCalcMethod", CommissionCalcMethod)

                command.Parameters.AddWithValue("Notes", Notes)
                command.Parameters.AddWithValue("LocationID", LocationID)
                command.Parameters.AddWithValue("IsAdmin", IsAdmin)
                command.Parameters.AddWithValue("IsMasterEmployee", IsMasterEmployee)

                Try
                    command.Connection.Open()
                    command.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    Return False
                Finally
                    command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    Public Function UpdateEmployeeDetail(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal EmployeeID As String, ByVal EmployeeTypeID As String, _
    ByVal EmployeeUserName As String, ByVal EmployeePassword As String, ByVal EmployeeName As String, _
    ByVal ActiveYN As Boolean, ByVal EmployeeAddress1 As String, ByVal EmployeeAddress2 As String, ByVal EmployeeCity As String, _
    ByVal EmployeeState As String, ByVal EmployeeZip As String, ByVal EmployeeCountry As String, ByVal EmployeePhone As String, _
    ByVal EmployeeFax As String, ByVal EmployeeSSNumber As String, ByVal EmployeeEmailAddress As String, ByVal EmployeeDepartmentID As String, _
    ByVal PictureURL As String, ByVal HireDate As String, ByVal Birthday As String, _
    ByVal Commissionable As Boolean, ByVal CommissionPerc As String, ByVal CommissionCalcMethod As String, ByVal Notes As String, _
    ByVal LocationID As String, ByVal IsAdmin As String, ByVal IsMasterEmployee As String) As Boolean

        Dim dt As New DataTable

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using command As New SqlCommand("[enterprise].[UpdateEmployeeDetail]", connection)
                command.CommandType = CommandType.StoredProcedure

                command.Parameters.AddWithValue("CompanyID", CompanyID)
                command.Parameters.AddWithValue("DivisionID", DivisionID)
                command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                command.Parameters.AddWithValue("EmployeeID", EmployeeID)

                command.Parameters.AddWithValue("EmployeeTypeID", EmployeeTypeID)
                command.Parameters.AddWithValue("EmployeeUserName", EmployeeUserName)

                command.Parameters.AddWithValue("EmployeePassword", EmployeePassword)
                command.Parameters.AddWithValue("EmployeeName", EmployeeName)
                command.Parameters.AddWithValue("ActiveYN", ActiveYN)
                command.Parameters.AddWithValue("EmployeeAddress1", EmployeeAddress1)

                command.Parameters.AddWithValue("EmployeeAddress2", EmployeeAddress2)
                command.Parameters.AddWithValue("EmployeeCity", EmployeeCity)
                command.Parameters.AddWithValue("EmployeeState", EmployeeState)
                command.Parameters.AddWithValue("EmployeeZip", EmployeeZip)

                command.Parameters.AddWithValue("EmployeeCountry", EmployeeCountry)
                command.Parameters.AddWithValue("EmployeePhone", EmployeePhone)
                command.Parameters.AddWithValue("EmployeeFax", EmployeeFax)
                command.Parameters.AddWithValue("EmployeeSSNumber", EmployeeSSNumber)

                command.Parameters.AddWithValue("EmployeeEmailAddress", EmployeeEmailAddress)
                command.Parameters.AddWithValue("EmployeeDepartmentID", EmployeeDepartmentID)
                command.Parameters.AddWithValue("PictureURL", PictureURL)
                command.Parameters.AddWithValue("HireDate", HireDate)

                command.Parameters.AddWithValue("Birthday", Birthday)
                command.Parameters.AddWithValue("Commissionable", Commissionable)
                command.Parameters.AddWithValue("CommissionPerc", CommissionPerc)
                command.Parameters.AddWithValue("CommissionCalcMethod", CommissionCalcMethod)

                command.Parameters.AddWithValue("Notes", Notes)
                command.Parameters.AddWithValue("LocationID", LocationID)
                command.Parameters.AddWithValue("IsAdmin", IsAdmin)
                command.Parameters.AddWithValue("IsMasterEmployee", IsMasterEmployee)

                Try
                    command.Connection.Open()
                    command.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    Return False
                Finally
                    command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    Public Function GetEmployeesModuleAccess(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal EmployeeID As String) As DataTable

        Dim dt As New DataTable

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using command As New SqlCommand("[enterprise].[GetEmployeesModuleAccess]", connection)
                command.CommandType = CommandType.StoredProcedure

                command.Parameters.AddWithValue("CompanyID", CompanyID)
                command.Parameters.AddWithValue("DivisionID", DivisionID)
                command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                command.Parameters.AddWithValue("EmployeeID", EmployeeID)

                Try
                    Dim da As New SqlDataAdapter(command)
                    da.Fill(dt)
                Catch ex As Exception

                End Try

            End Using
        End Using

        Return dt

    End Function

    Public Function AddUpdateEmployeeModuleAccess(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal EmployeeID As String, _
    ByVal ModuleID As String, ByVal IsAccess As Boolean) As Boolean

        Dim dt As New DataTable

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using command As New SqlCommand("[enterprise].[AddUpdateEmployeeModuleAccess]", connection)
                command.CommandType = CommandType.StoredProcedure

                command.Parameters.AddWithValue("CompanyID", CompanyID)
                command.Parameters.AddWithValue("DivisionID", DivisionID)
                command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                command.Parameters.AddWithValue("EmployeeID", EmployeeID)

                command.Parameters.AddWithValue("ModuleID", ModuleID)
                command.Parameters.AddWithValue("IsAccess", IsAccess)

                Try
                    command.Connection.Open()
                    command.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    Return False
                Finally
                    command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    Public Function GetEmployeeList(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, Optional ByVal EmployeeID As String = "") As DataTable

        Dim dt As New DataTable

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using command As New SqlCommand("[enterprise].[GetEmployeeList]", connection)
                command.CommandType = CommandType.StoredProcedure

                command.Parameters.AddWithValue("CompanyID", CompanyID)
                command.Parameters.AddWithValue("DivisionID", DivisionID)
                command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                command.Parameters.AddWithValue("EmployeeID", EmployeeID)

                Try
                    Dim da As New SqlDataAdapter(command)
                    da.Fill(dt)
                Catch ex As Exception

                End Try

            End Using
        End Using

        Return dt

    End Function

    Public Function CheckIsEmployeeAdmin(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal EmployeeID As String) As Boolean

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using command As New SqlCommand("[enterprise].[CheckIsEmployeeAdmin]", connection)
                command.CommandType = CommandType.StoredProcedure

                command.Parameters.AddWithValue("CompanyID", CompanyID)
                command.Parameters.AddWithValue("DivisionID", DivisionID)
                command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                command.Parameters.AddWithValue("EmployeeID", EmployeeID)

                Try
                    command.Connection.Open()
                    Return Convert.ToBoolean(command.ExecuteScalar)
                Catch ex As Exception
                    Return False
                Finally
                    command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    Public Function CheckIsMasterEmployee(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal EmployeeID As String) As Boolean

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using command As New SqlCommand("[enterprise].[CheckIsMasterEmployee]", connection)
                command.CommandType = CommandType.StoredProcedure

                command.Parameters.AddWithValue("CompanyID", CompanyID)
                command.Parameters.AddWithValue("DivisionID", DivisionID)
                command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                command.Parameters.AddWithValue("EmployeeID", EmployeeID)

                Try
                    command.Connection.Open()
                    Return Convert.ToBoolean(command.ExecuteScalar)
                Catch ex As Exception
                    Return False
                Finally
                    command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    Public Function DeleteEmployeeDetail(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal EmployeeID As String) As Boolean

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using command As New SqlCommand("[enterprise].[DeleteEmployeeDetail]", connection)
                command.CommandType = CommandType.StoredProcedure

                command.Parameters.AddWithValue("CompanyID", CompanyID)
                command.Parameters.AddWithValue("DivisionID", DivisionID)
                command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                command.Parameters.AddWithValue("EmployeeID", EmployeeID)

                Try
                    command.Connection.Open()
                    command.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    Return False
                Finally
                    command.Connection.Close()
                End Try

            End Using
        End Using

    End Function


End Class
