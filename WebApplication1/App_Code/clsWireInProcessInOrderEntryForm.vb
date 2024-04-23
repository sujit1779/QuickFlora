Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data


Public Class clsWireInProcessInOrderEntryForm

    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    Public CompanyID As String
    Public DivisionID As String
    Public DepartmentID As String
    Public CustomerID As String
    Public AccountStatus As String
    Public CustomerName As String

    Public CustomerAddress1 As String
    Public CustomerAddress2 As String
    Public CustomerAddress3 As String
    Public CustomerCity As String
    Public CustomerState As String
    Public CustomerZip As String
    Public CustomerCountry As String

    Public CustomerPhone As String
    Public CustomerFax As String
 
    Public CustomerFirstName As String
    Public CustomerLastName As String
    Public Attention As String

    Public CustomerCompany As String
    Public CustomerTypeID As String

 
    Public Function Insert() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "INSERT INTO [Enterprise].[dbo].[CustomerInformation] ([CompanyID],[DivisionID],[DepartmentID],[CustomerID],[AccountStatus],[CustomerName],[CustomerAddress1],[CustomerAddress2],[CustomerAddress3],[CustomerCity],[CustomerState],[CustomerZip],[CustomerCountry],[CustomerPhone],[CustomerFax],[CustomerFirstName],[CustomerLastName],[CustomerSalutation],[Attention],[CustomerTypeID],[CustomerCompany])"
        qry = qry & " values(@f1,@f2,@f3,@f4,@f5,@f6,@f7,@f8,@f9,@f10,@f11,@f12,@f13,@f14,@f15,@f16,@f17,@f18,@f19,@f20,@f21)"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.CustomerID
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 36)).Value = Me.AccountStatus
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 50)).Value = Me.CustomerName
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.NVarChar, 50)).Value = Me.CustomerAddress1
            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.NVarChar, 50)).Value = Me.CustomerAddress2
            com.Parameters.Add(New SqlParameter("@f9", SqlDbType.NVarChar, 50)).Value = Me.CustomerAddress3
            com.Parameters.Add(New SqlParameter("@f10", SqlDbType.NVarChar, 50)).Value = Me.CustomerCity
            '[CustomerState],[CustomerZip],[CustomerCountry],[CustomerPhone],[CustomerFax],[CustomerFirstName],[CustomerLastName],
            com.Parameters.Add(New SqlParameter("@f11", SqlDbType.NVarChar, 50)).Value = Me.CustomerState
            com.Parameters.Add(New SqlParameter("@f12", SqlDbType.NVarChar, 10)).Value = Me.CustomerZip
            com.Parameters.Add(New SqlParameter("@f13", SqlDbType.NVarChar, 50)).Value = Me.CustomerCountry
            com.Parameters.Add(New SqlParameter("@f14", SqlDbType.NVarChar, 50)).Value = Me.CustomerPhone
            com.Parameters.Add(New SqlParameter("@f15", SqlDbType.NVarChar, 50)).Value = Me.CustomerFax
            com.Parameters.Add(New SqlParameter("@f16", SqlDbType.NVarChar, 50)).Value = Me.CustomerFirstName
            com.Parameters.Add(New SqlParameter("@f17", SqlDbType.NVarChar, 50)).Value = Me.CustomerLastName
            '[CustomerSalutation],[Attention],[CustomerTypeID],[CustomerCompany])"
            com.Parameters.Add(New SqlParameter("@f18", SqlDbType.NVarChar, 36)).Value = ""
            com.Parameters.Add(New SqlParameter("@f19", SqlDbType.NVarChar, 36)).Value = ""
            com.Parameters.Add(New SqlParameter("@f20", SqlDbType.NVarChar, 36)).Value = Me.CustomerTypeID
            com.Parameters.Add(New SqlParameter("@f21", SqlDbType.NVarChar, 50)).Value = Me.CustomerCompany

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function


    Public Function UPDATE() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE [Enterprise].[dbo].[CustomerInformation] SET  [AccountStatus]=@f5,[CustomerName]=@f6,[CustomerAddress1]=@f7,[CustomerAddress2]=@f8,[CustomerAddress3]=@f9,[CustomerCity]=@f10,[CustomerState]=@f11,[CustomerZip]=@f12,[CustomerCountry]=@f13,[CustomerPhone]=@f14,[CustomerFax]=@f15,[CustomerFirstName]=@f16,[CustomerLastName]=@f17,[CustomerSalutation]=@f18,[Attention]=@f19,[CustomerTypeID]=@f20,[CustomerCompany]=@f21"
        qry = qry & " Where [CompanyID]=@f1 AND [DivisionID]=@f2 AND [DepartmentID]=@f3 AND [CustomerID]=@f4  "
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.CustomerID
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 36)).Value = Me.AccountStatus
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 50)).Value = Me.CustomerName
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.NVarChar, 50)).Value = Me.CustomerAddress1
            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.NVarChar, 50)).Value = Me.CustomerAddress2
            com.Parameters.Add(New SqlParameter("@f9", SqlDbType.NVarChar, 50)).Value = Me.CustomerAddress3
            com.Parameters.Add(New SqlParameter("@f10", SqlDbType.NVarChar, 50)).Value = Me.CustomerCity
            '[CustomerState],[CustomerZip],[CustomerCountry],[CustomerPhone],[CustomerFax],[CustomerFirstName],[CustomerLastName],
            com.Parameters.Add(New SqlParameter("@f11", SqlDbType.NVarChar, 50)).Value = Me.CustomerState
            com.Parameters.Add(New SqlParameter("@f12", SqlDbType.NVarChar, 10)).Value = Me.CustomerZip
            com.Parameters.Add(New SqlParameter("@f13", SqlDbType.NVarChar, 50)).Value = Me.CustomerCountry
            com.Parameters.Add(New SqlParameter("@f14", SqlDbType.NVarChar, 50)).Value = Me.CustomerPhone
            com.Parameters.Add(New SqlParameter("@f15", SqlDbType.NVarChar, 50)).Value = Me.CustomerFax
            com.Parameters.Add(New SqlParameter("@f16", SqlDbType.NVarChar, 50)).Value = Me.CustomerFirstName
            com.Parameters.Add(New SqlParameter("@f17", SqlDbType.NVarChar, 50)).Value = Me.CustomerLastName
            '[CustomerSalutation],[Attention],[CustomerTypeID],[CustomerCompany])"
            com.Parameters.Add(New SqlParameter("@f18", SqlDbType.NVarChar, 36)).Value = ""
            com.Parameters.Add(New SqlParameter("@f19", SqlDbType.NVarChar, 36)).Value = ""
            com.Parameters.Add(New SqlParameter("@f20", SqlDbType.NVarChar, 36)).Value = Me.CustomerTypeID
            com.Parameters.Add(New SqlParameter("@f21", SqlDbType.NVarChar, 50)).Value = Me.CustomerCompany

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function


    Public Function IsCustomerExist() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from [Enterprise].[dbo].[CustomerInformation] where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and [CustomerID]=@f3"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.CustomerID

            da.SelectCommand = com
            da.Fill(dt)

            If dt.Rows.Count <> 0 Then
                Return True
            Else
                Return False
            End If


        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False

        End Try
    End Function


    Public Function AddEditWireService(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal WireServiceID As String, ByVal WireServiceDescription As String, ByVal AccountNumber As String, ByVal AddEdit As String, ByVal WireActive As Integer, ByVal Address1 As String, ByVal Address2 As String, ByVal City As String, ByVal State As String, ByVal ZipCode As String, ByVal Fax As String, ByVal Phone As String) As Integer


        



        Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        ConString.Open()

        Dim myCommand As New SqlCommand("[AddEditWireServiceWithCustomerID]", ConString)
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


        Dim parameterWireServiceID As New SqlParameter("@WireServiceID", Data.SqlDbType.NVarChar)
        parameterWireServiceID.Value = WireServiceID
        myCommand.Parameters.Add(parameterWireServiceID)

        Dim parameterWireServiceDescription As New SqlParameter("@WireServiceDescription", Data.SqlDbType.NVarChar)
        parameterWireServiceDescription.Value = WireServiceDescription
        myCommand.Parameters.Add(parameterWireServiceDescription)


        Dim parameterAddress1 As New SqlParameter("@Address1", Data.SqlDbType.NVarChar)
        parameterAddress1.Value = Address1
        myCommand.Parameters.Add(parameterAddress1)

        Dim parameterAddress2 As New SqlParameter("@Address2", Data.SqlDbType.NVarChar)
        parameterAddress2.Value = Address2
        myCommand.Parameters.Add(parameterAddress2)

        Dim parameterCity As New SqlParameter("@City", Data.SqlDbType.NVarChar)
        parameterCity.Value = City
        myCommand.Parameters.Add(parameterCity)


        Dim parameterState As New SqlParameter("@State", Data.SqlDbType.NVarChar)
        parameterState.Value = State
        myCommand.Parameters.Add(parameterState)


        Dim parameterPhone As New SqlParameter("@Phone", Data.SqlDbType.NVarChar)
        parameterPhone.Value = Phone
        myCommand.Parameters.Add(parameterPhone)


        Dim parameterFax As New SqlParameter("@Fax", Data.SqlDbType.NVarChar)
        parameterFax.Value = Fax
        myCommand.Parameters.Add(parameterFax)


        Dim parameterZipCode As New SqlParameter("@ZipCode", Data.SqlDbType.NVarChar)
        parameterZipCode.Value = ZipCode
        myCommand.Parameters.Add(parameterZipCode)



        Dim parameterAccountNumber As New SqlParameter("@AccountNumber", Data.SqlDbType.NVarChar)
        parameterAccountNumber.Value = AccountNumber
        myCommand.Parameters.Add(parameterAccountNumber)

        Dim parameterAddEdit As New SqlParameter("@AddEdit", Data.SqlDbType.NVarChar)
        parameterAddEdit.Value = AddEdit
        myCommand.Parameters.Add(parameterAddEdit)


        Dim paramWireActive As New SqlParameter("@WireActive", Data.SqlDbType.Int)
        paramWireActive.Value = WireActive
        myCommand.Parameters.Add(paramWireActive)



        Dim paramReturnValue As New SqlParameter("@ReturnValue", Data.SqlDbType.Int)
        paramReturnValue.Direction = ParameterDirection.Output
        myCommand.Parameters.Add(paramReturnValue)

        myCommand.ExecuteNonQuery()
        Dim res As Integer

        ConString.Close()

        If paramReturnValue.Value.ToString() <> "" Then
            res = Convert.ToDecimal(paramReturnValue.Value)
        End If

        If res <> 8 Then
            Me.CompanyID = CompanyID
            Me.DepartmentID = DepartmentID
            Me.DivisionID = DivisionID
            Me.CustomerID = WireServiceID

            Dim check As Boolean = False
            check = Me.IsCustomerExist()

            Me.AccountStatus = "Open"
            Me.CustomerName = WireServiceID
            Me.CustomerAddress1 = Address1
            Me.CustomerAddress2 = Address2
            Me.CustomerAddress3 = ""
            Me.CustomerCity = City
            Me.CustomerState = State
            Me.CustomerZip = ZipCode
            Me.CustomerCountry = ""
            Me.CustomerPhone = Phone
            Me.CustomerFax = Fax
            Me.CustomerFirstName = WireServiceID
            Me.CustomerLastName = WireServiceID
            Me.CustomerTypeID = "WireService"
            Me.CustomerCompany = WireServiceID

            If check Then
                Me.UPDATE()
            Else
                Me.Insert()
            End If

        End If
        
        Return res


    End Function


    Public Function GetWireSeriveCustomer() As DataTable

        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from [Enterprise].[dbo].[CustomerInformation] where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and [CustomerTypeID]=@f3"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = "WireService"

            da.SelectCommand = com
            da.Fill(dt)

            If dt.Rows.Count <> 0 Then
                Return dt
            Else
                Return dt
            End If


        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return dt

        End Try
    End Function


    Public Function CheckWireSeriveCustomer() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from [Enterprise].[dbo].[CustomerInformation] where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and [CustomerTypeID]=@f3 and [CustomerID]=@f4"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.CustomerID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = "WireService"

            da.SelectCommand = com
            da.Fill(dt)

            If dt.Rows.Count <> 0 Then
                Return True
            Else
                Return False
            End If


        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return True
        End Try

    End Function


    Public Function DeleteWireSeriveCustomer(ByVal WID As Integer) As Boolean

        Dim DTWID As New DataTable
        DTWID = Me.GetWireSeriveID(WID)

        If DTWID.Rows.Count <> 0 Then

            Dim wirserviceid As String
            wirserviceid = DTWID.Rows(0)("WireServiceID")
            Me.CustomerID = wirserviceid

        End If

        If CheckWireSeriveCustomerInOrderheader() = False Then
            Dim connec As New SqlConnection(constr)
            Dim ssql As String = ""
            Dim dt As New DataTable()
            ssql = "DELETE FROM [Enterprise].[dbo].[CustomerInformation] where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and [CustomerID]=@f3"
            Dim da As New SqlDataAdapter
            Dim com As SqlCommand
            com = New SqlCommand(ssql, connec)
            Try

                com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
                com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
                com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
                com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.CustomerID
                da.DeleteCommand = com

                com.Connection.Open()
                com.ExecuteNonQuery()
                com.Connection.Close()

            Catch ex As Exception
                Dim msg As String
                msg = ex.Message
                'HttpContext.Current.Response.Write(msg)
                Return True
            End Try

        End If

    End Function


    Public Function CheckWireSeriveCustomerInOrderheader() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from  [OrderHeader] where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and [CustomerID]=@f4"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.CustomerID


            da.SelectCommand = com
            da.Fill(dt)

            If dt.Rows.Count <> 0 Then
                Return True
            Else
                Return False
            End If


        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return True
        End Try

    End Function


    Public Function GetWireSeriveID(ByVal WID As Integer) As DataTable

        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select WireServiceID from WireServices where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and WID=@f3"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = WID

            da.SelectCommand = com
            da.Fill(dt)

            If dt.Rows.Count <> 0 Then
                Return dt
            Else
                Return dt
            End If


        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return dt

        End Try
    End Function

End Class
