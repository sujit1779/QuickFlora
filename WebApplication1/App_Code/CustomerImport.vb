Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient


Public Class CustomerImport

    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    Public CompanyID As String
    Public DivisionID As String
    Public DepartmentID As String
    Public CustomerID As String
    Public CustomerFirstName As String
    Public CustomerLastName As String
    Public Attention As String
    Public CustomerAddress1 As String
    Public CustomerAddress2 As String
    Public CustomerAddress3 As String
    Public CustomerCity As String
    Public CustomerState As String
    Public CustomerCountry As String
    Public CustomerFax As String
    Public CustomerEmail As String
    Public CreditLimit As String
    Public AccountStatus As String
    Public CustomerSince As String
    Public CreditComments As String
    Public CustomerSalutation As String
    Public CustomerZip As String
    Public CustomerCell As String
    Public CustomerPhoneExt As String
    Public CustomerCompany As String
    Public Newsletter As String

    Public Login As String
    Public Password As String
    Public CustomerPhone As String


    Public Function InsertCustomerInformationDetail() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "INSERT INTO  CustomerInformation (CompanyID, DivisionID, DepartmentID, CustomerID, AccountStatus" _
        & ",CustomerAddress1, CustomerAddress2, CustomerAddress3,CustomerCity, CustomerState, CustomerZip, CustomerCountry" _
        & ",CustomerPhone, CustomerFax, CustomerEmail,CustomerLogin,CustomerPassword,CustomerFirstName" _
        & ",CustomerLastName, CustomerSalutation, Attention,CustomerSince,CreditLimit,CreditComments" _
        & ",CustomerCell, CustomerPhoneExt,CustomerCompany, Newsletter) values(@f1,@f2,@f3,@f4,@f5,@f6,@f7,@f8,@f9,@f10,@f11,@f12,@f13,@f14,@f15,@f16,@f17,@f18,@f19,@f20,@f21,@f22,@f23,@f24,@f25,@f26,@f27,@f28)"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID.Trim()
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID.Trim()
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID.Trim()
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 50)).Value = Me.CustomerID.Trim()
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 36)).Value = Me.AccountStatus.Trim()
            ''second line'''
            'CustomerAddress1, CustomerAddress2, CustomerAddress3,CustomerCity, CustomerState, CustomerZip, CustomerCountry
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 50)).Value = Me.CustomerAddress1.Trim()
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.NVarChar, 50)).Value = Me.CustomerAddress2.Trim()
            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.NVarChar, 50)).Value = Me.CustomerAddress3.Trim()
            com.Parameters.Add(New SqlParameter("@f9", SqlDbType.NVarChar, 50)).Value = Me.CustomerCity.Trim()
            com.Parameters.Add(New SqlParameter("@f10", SqlDbType.NVarChar, 50)).Value = Me.CustomerState.Trim()
            com.Parameters.Add(New SqlParameter("@f11", SqlDbType.NVarChar, 10)).Value = Me.CustomerZip.Trim()
            com.Parameters.Add(New SqlParameter("@f12", SqlDbType.NVarChar, 50)).Value = Me.CustomerCountry.Trim()
            ''third line
            'CustomerPhone, CustomerFax, CustomerEmail,CustomerLogin,CustomerPassword,CustomerFirstName
            com.Parameters.Add(New SqlParameter("@f13", SqlDbType.NVarChar, 50)).Value = Me.CustomerPhone.Trim()
            com.Parameters.Add(New SqlParameter("@f14", SqlDbType.NVarChar, 50)).Value = Me.CustomerFax.Trim()
            com.Parameters.Add(New SqlParameter("@f15", SqlDbType.NVarChar, 60)).Value = Me.CustomerEmail.Trim()
            com.Parameters.Add(New SqlParameter("@f16", SqlDbType.NVarChar, 60)).Value = Me.Login.Trim()
            com.Parameters.Add(New SqlParameter("@f17", SqlDbType.NVarChar, 20)).Value = Me.Password.Trim()
            com.Parameters.Add(New SqlParameter("@f18", SqlDbType.NVarChar, 50)).Value = Me.CustomerFirstName.Trim()

            ''fourth line
            'CustomerLastName, CustomerSalutation, Attention,CustomerSince,CreditLimit,CreditComments
            com.Parameters.Add(New SqlParameter("@f19", SqlDbType.NVarChar, 50)).Value = Me.CustomerLastName.Trim()
            com.Parameters.Add(New SqlParameter("@f20", SqlDbType.NVarChar, 10)).Value = Me.CustomerSalutation.Trim()
            com.Parameters.Add(New SqlParameter("@f21", SqlDbType.NVarChar, 36)).Value = Me.Attention.Trim()
            com.Parameters.Add(New SqlParameter("@f22", SqlDbType.DateTime)).Value = Me.CustomerSince
            com.Parameters.Add(New SqlParameter("@f23", SqlDbType.Float)).Value = Me.CreditLimit
            com.Parameters.Add(New SqlParameter("@f24", SqlDbType.NVarChar, 250)).Value = Me.CreditComments.Trim()

            ''fifth line 
            'CustomerCell, CustomerPhoneExt,CustomerCompany, Newsletter
            com.Parameters.Add(New SqlParameter("@f25", SqlDbType.NVarChar, 50)).Value = Me.CustomerCell.Trim()
            com.Parameters.Add(New SqlParameter("@f26", SqlDbType.NVarChar, 50)).Value = Me.CustomerPhoneExt.Trim()
            com.Parameters.Add(New SqlParameter("@f27", SqlDbType.NVarChar, 50)).Value = Me.CustomerCompany.Trim()
            com.Parameters.Add(New SqlParameter("@f28", SqlDbType.NVarChar, 20)).Value = Me.Newsletter.Trim()

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function


    Public Function GetNextCustomerID() As String

        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT Count(CustomerID)as CustomerID from customerinformation where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID


            da.SelectCommand = com
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0)("CustomerID")
            End If
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return "0"
        End Try
        Return "0"
    End Function

    Public Function GetCustomerID() As Integer
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT  *  from customerinformation where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 AND CustomerID=@f3"
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
            If dt.Rows.Count > 0 Then
                Return 1
            Else
                Return 0
            End If
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return "0"
        End Try
        Return "0"
    End Function

    Public Function GetListOfCustomerID() As Integer
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT  *  from customerinformation where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 AND CustomerID=@f3"
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
            If dt.Rows.Count > 0 Then
                Return 1
            Else
                Return 0
            End If
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return "0"
        End Try
        Return "0"
    End Function


End Class
