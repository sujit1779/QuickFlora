Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class VendorModel


    Private _companyID As String
    Public Property CompanyID() As String
        Get
            Return _companyID
        End Get
        Set(ByVal value As String)
            _companyID = value
        End Set
    End Property

    Private _divisionID As String
    Public Property DivisionID() As String
        Get
            Return _divisionID
        End Get
        Set(ByVal value As String)
            _divisionID = value
        End Set
    End Property

    Private _departmentID As String
    Public Property DepartmentID() As String
        Get
            Return _departmentID
        End Get
        Set(ByVal value As String)
            _departmentID = value
        End Set
    End Property



    Private _vendorID As String
    Public Property VendorID() As String
        Get
            Return _vendorID
        End Get
        Set(ByVal value As String)
            _vendorID = value
        End Set
    End Property

    Private _attention As String
    Public Property Attention() As String
        Get
            Return _attention
        End Get
        Set(ByVal value As String)
            _attention = value
        End Set
    End Property

    Private _vendorName As String
    Public Property VendorName() As String
        Get
            Return _vendorName
        End Get
        Set(ByVal value As String)
            _vendorName = value
        End Set
    End Property

    Private _accountNumber As String
    Public Property AccountNumber() As String
        Get
            Return _accountNumber
        End Get
        Set(ByVal value As String)
            _accountNumber = value
        End Set
    End Property

    Private _accountStatus As String
    Public Property AccountStatus() As String
        Get
            Return _accountStatus
        End Get
        Set(ByVal value As String)
            _accountStatus = value
        End Set
    End Property

    Private _vendorLogin As String
    Public Property VendorLogin() As String
        Get
            Return _vendorLogin
        End Get
        Set(ByVal value As String)
            _vendorLogin = value
        End Set
    End Property

    Private _vendorPassword As String
    Public Property VendorPassword() As String
        Get
            Return _vendorPassword
        End Get
        Set(ByVal value As String)
            _vendorPassword = value
        End Set
    End Property

    Private _vendorType As String
    Public Property VendorType() As String
        Get
            Return _vendorType
        End Get
        Set(ByVal value As String)
            _vendorType = value
        End Set
    End Property

    Private _address1 As String
    Public Property Address1() As String
        Get
            Return _address1
        End Get
        Set(ByVal value As String)
            _address1 = value
        End Set
    End Property

    Private _address2 As String
    Public Property Address2() As String
        Get
            Return _address2
        End Get
        Set(ByVal value As String)
            _address2 = value
        End Set
    End Property

    Private _address3 As String
    Public Property Address3() As String
        Get
            Return _address3
        End Get
        Set(ByVal value As String)
            _address3 = value
        End Set
    End Property

    Private _vendorCity As String
    Public Property VendorCity() As String
        Get
            Return _vendorCity
        End Get
        Set(ByVal value As String)
            _vendorCity = value
        End Set
    End Property

    Private _vendorState As String
    Public Property VendorState() As String
        Get
            Return _vendorState
        End Get
        Set(ByVal value As String)
            _vendorState = value
        End Set
    End Property

    Private _vendorZip As String
    Public Property VendorZip() As String
        Get
            Return _vendorZip
        End Get
        Set(ByVal value As String)
            _vendorZip = value
        End Set
    End Property

    Private _country As String
    Public Property VendorCountry() As String
        Get
            Return _country
        End Get
        Set(ByVal value As String)
            _country = value
        End Set
    End Property

    Private _vendorPhone As String
    Public Property VendorPhone() As String
        Get
            Return _vendorPhone
        End Get
        Set(ByVal value As String)
            _vendorPhone = value
        End Set
    End Property

    Private _fax As String
    Public Property VendorFax() As String
        Get
            Return _fax
        End Get
        Set(ByVal value As String)
            _fax = value
        End Set
    End Property

    Private _vendorEmail As String
    Public Property VendorEmail() As String
        Get
            Return _vendorEmail
        End Get
        Set(ByVal value As String)
            _vendorEmail = value
        End Set
    End Property

    Private _webPage As String
    Public Property VendorWebPage() As String
        Get
            Return _webPage
        End Get
        Set(ByVal value As String)
            _webPage = value
        End Set
    End Property

End Class

Public Class clsVendor

    'Protected CompanyID As String, DivisionID As String, DepartmentID As String

    Public Function ConvertDataTable(ByVal dt As DataTable) As List(Of VendorModel)

        Dim list As New List(Of VendorModel)

        For i As Integer = 0 To dt.Rows.Count - 1
            Dim row As DataRow = dt.Rows(i)

            Dim item As New VendorModel
            item.AccountNumber = dt.Rows(i)("AccountNumber")
            item.VendorCity = dt.Rows(i)("VendorCity")
            item.VendorEmail = dt.Rows(i)("VendorEmail")
            item.VendorID = dt.Rows(i)("VendorID")
            item.VendorLogin = dt.Rows(i)("VendorLogin")
            item.VendorName = dt.Rows(i)("VendorName")
            item.VendorPassword = dt.Rows(i)("VendorPassword")
            item.VendorPhone = dt.Rows(i)("VendorPhone")
            item.VendorState = dt.Rows(i)("VendorState")
            'item.VendorTypeID = dt.Rows(i)("VendorTypeID")
            item.VendorZip = dt.Rows(i)("VendorZip")

            list.Add(item)

        Next

        Return list

    End Function

    Public Function GetVendorList(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, Optional ByVal VendorID As String = "") As DataSet

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString").ToString)
            Using Command As New SqlCommand("[enterprise].[GetVendorsList]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                Command.Parameters.AddWithValue("VendorID", VendorID)

                Try
                    Dim ds As New DataSet
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)

                    If ds.Tables(0).Rows.Count >= 0 Then
                        Return ds
                    Else
                        Return Nothing
                    End If

                Catch ex As Exception

                    Return Nothing

                End Try

            End Using
        End Using

    End Function

    Public Function GetVendorAccountStatus(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As DataSet

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString").ToString)
            Using Command As New SqlCommand("[enterprise].[RptListVendorAccountStatuses]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                Try
                    Dim ds As New DataSet
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)

                    If ds.Tables(0).Rows.Count >= 0 Then
                        Return ds
                    Else
                        Return Nothing
                    End If

                Catch ex As Exception

                    Return Nothing

                End Try

            End Using
        End Using

    End Function

    Public Function GetVendorType(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As DataSet

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString").ToString)
            Using Command As New SqlCommand("[enterprise].[RptListVendorTypes]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                Try
                    Dim ds As New DataSet
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)

                    If ds.Tables(0).Rows.Count >= 0 Then
                        Return ds
                    Else
                        Return Nothing
                    End If

                Catch ex As Exception

                    Return Nothing

                End Try

            End Using
        End Using

    End Function

    Public Function IsVendorExists(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal VendorID As String) As Boolean

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString").ToString)
            Using Command As New SqlCommand("[CheckVendorExists]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("VendorID", VendorID)

                Dim ReturnValue As Integer = -1
                Dim param As New SqlParameter("ReturnValue", SqlDbType.Int)
                param.Direction = ParameterDirection.Output
                Command.Parameters.Add(param)

                Try
                    Command.Connection.Open()
                    Command.ExecuteNonQuery()

                    ReturnValue = Convert.ToInt16(param.Value.ToString)

                    If ReturnValue = 0 Then
                        Return False
                    Else
                        Return True
                    End If
                Catch ex As Exception
                    Return True
                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    Public Function CreateVendor(ByVal vendor As VendorModel) As Boolean

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using command As New SqlCommand("[CreateVendor]", connection)
                command.CommandType = CommandType.StoredProcedure

                command.Parameters.AddWithValue("CompanyID", vendor.CompanyID)
                command.Parameters.AddWithValue("DivisionID", vendor.DivisionID)
                command.Parameters.AddWithValue("DepartmentID", vendor.DepartmentID)

                command.Parameters.AddWithValue("VendorID", vendor.VendorID)
                command.Parameters.AddWithValue("VendorName", vendor.VendorName)
                command.Parameters.AddWithValue("VendorAddress1", vendor.Address1)

                command.Parameters.AddWithValue("VendorAddress2", vendor.Address2)
                command.Parameters.AddWithValue("VendorAddress3", vendor.Address3)
                command.Parameters.AddWithValue("VendorCity", vendor.VendorCity)

                command.Parameters.AddWithValue("VendorState", vendor.VendorState)
                command.Parameters.AddWithValue("VendorZip", vendor.VendorZip)
                command.Parameters.AddWithValue("VendorCountry", vendor.VendorCountry)

                command.Parameters.AddWithValue("VendorPhone", vendor.VendorPhone)
                command.Parameters.AddWithValue("VendorFax", vendor.VendorFax)
                command.Parameters.AddWithValue("VendorEmail", vendor.VendorEmail)

                command.Parameters.AddWithValue("VendorWebPage", vendor.VendorWebPage)
                command.Parameters.AddWithValue("VendorAccountNumber", vendor.AccountNumber)
                command.Parameters.AddWithValue("VendorLogin", vendor.VendorLogin)

                command.Parameters.AddWithValue("VendorPassword", vendor.VendorPassword)
                command.Parameters.AddWithValue("VendorType", vendor.VendorType)

                command.Parameters.AddWithValue("Attention", vendor.VendorType)

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

    Public Function UpdateVendor(ByVal vendor As VendorModel) As Boolean

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using command As New SqlCommand("[UpdateVendor]", connection)
                command.CommandType = CommandType.StoredProcedure

                command.Parameters.AddWithValue("CompanyID", vendor.CompanyID)
                command.Parameters.AddWithValue("DivisionID", vendor.DivisionID)
                command.Parameters.AddWithValue("DepartmentID", vendor.DepartmentID)

                command.Parameters.AddWithValue("VendorID", vendor.VendorID)
                command.Parameters.AddWithValue("VendorName", vendor.VendorName)
                command.Parameters.AddWithValue("VendorAddress1", vendor.Address1)

                command.Parameters.AddWithValue("VendorAddress2", vendor.Address2)
                command.Parameters.AddWithValue("VendorAddress3", vendor.Address3)
                command.Parameters.AddWithValue("VendorCity", vendor.VendorCity)

                command.Parameters.AddWithValue("VendorState", vendor.VendorState)
                command.Parameters.AddWithValue("VendorZip", vendor.VendorZip)
                command.Parameters.AddWithValue("VendorCountry", vendor.VendorCountry)

                command.Parameters.AddWithValue("VendorPhone", vendor.VendorPhone)
                command.Parameters.AddWithValue("VendorFax", vendor.VendorFax)
                command.Parameters.AddWithValue("VendorEmail", vendor.VendorEmail)

                command.Parameters.AddWithValue("VendorWebPage", vendor.VendorWebPage)
                command.Parameters.AddWithValue("VendorAccountNumber", vendor.AccountNumber)
                command.Parameters.AddWithValue("VendorLogin", vendor.VendorLogin)

                command.Parameters.AddWithValue("VendorPassword", vendor.VendorPassword)
                command.Parameters.AddWithValue("VendorType", vendor.VendorType)

                command.Parameters.AddWithValue("Attention", vendor.VendorType)

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

    Public Function DeleteVendor(ByVal vendor As VendorModel) As Boolean

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using command As New SqlCommand("[enterprise].[DeleteVendor]", connection)
                command.CommandType = CommandType.StoredProcedure

                command.Parameters.AddWithValue("CompanyID", vendor.CompanyID)
                command.Parameters.AddWithValue("DivisionID", vendor.DivisionID)
                command.Parameters.AddWithValue("DepartmentID", vendor.DepartmentID)

                command.Parameters.AddWithValue("VendorID", vendor.VendorID)

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
