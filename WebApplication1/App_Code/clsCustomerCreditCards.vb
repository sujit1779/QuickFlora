Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class clsCustomerCreditCards

    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    Public CompanyID As String
    Public DivisionID As String
    Public DepartmentID As String
    Public OrderNumber As String
    Public CustomerID As String


    Public Ordercancel As Boolean
    Public Ordercanceldate As Date


    Public BillingCustomerName As String
    Public BillingCustomerAddress1 As String
    Public BillingCustomerAddress2 As String
    Public BillingCustomerAddress3 As String
    Public BillingCustomerCity As String
    Public BillingCustomerState As String
    Public BillingCustomerZip As String
    Public BillingCustomerCountry As String

    ''new files added
    Public BillingCustomerPhone As String = ""
    Public BillingCustomerFax As String = ""
    Public BillingCustomerEmail As String = ""
    Public BillingCustomerCell As String = ""
    Public BillingCustomerPhoneExt As String = ""
    ''new files added


    Public Function UpdateCancelOrderNumber() As Boolean
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE OrderHeader set Canceled=1,Posted=0,CancelDate=@f6 Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And OrderNumber=@f5"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 36)).Value = Me.OrderNumber
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.DateTime)).Value = Me.Ordercanceldate

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

    Public updatebillingCC As Boolean


    Public Function UpdateStoredCreditCardOrderNumber() As Boolean
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE OrderHeader set StoredCreditCard=1,updatebillingCC=@f7,CustomerCardDetailsForlinenumber=@f6 Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And OrderNumber=@f5"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 36)).Value = Me.OrderNumber
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.BigInt)).Value = Me.linenumber
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.Bit)).Value = Me.updatebillingCC

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



    Public Function UpdateCardDetailLineNumberFromOEF() As Boolean
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE CustomerCardDetail set CustomerName=@f6,CustomerAddress1=@f7,CustomerAddress2=@f8,CustomerAddress3=@f9,CustomerCity=@f10,CustomerState=@f11,CustomerZip=@f12,CustomerCountry=@f13  Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And CardDetailLineNumber=@f5"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 36)).Value = Me.linenumber
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 255)).Value = Me.BillingCustomerName
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerAddress1
            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerAddress2
            com.Parameters.Add(New SqlParameter("@f9", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerAddress3
            com.Parameters.Add(New SqlParameter("@f10", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerCity
            com.Parameters.Add(New SqlParameter("@f11", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerState
            com.Parameters.Add(New SqlParameter("@f12", SqlDbType.NVarChar, 10)).Value = Me.BillingCustomerZip
            com.Parameters.Add(New SqlParameter("@f13", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerCountry

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



    Public Function UpdateCardDetailLineNumber() As Boolean
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE CustomerCardDetail set CustomerName=@f6,CustomerAddress1=@f7,CustomerAddress2=@f8,CustomerAddress3=@f9,CustomerCity=@f10,CustomerState=@f11,CustomerZip=@f12,CustomerCountry=@f13,CreditCardTypeID=@f14,CreditCardName=@f15,CreditCardExpDate=@f17,CreditCardCSVNumber=@f18,Priority=@f19 Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And CardDetailLineNumber=@f5"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 36)).Value = Me.linenumber
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 255)).Value = Me.BillingCustomerName
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerAddress1
            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerAddress2
            com.Parameters.Add(New SqlParameter("@f9", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerAddress3
            com.Parameters.Add(New SqlParameter("@f10", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerCity
            com.Parameters.Add(New SqlParameter("@f11", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerState
            com.Parameters.Add(New SqlParameter("@f12", SqlDbType.NVarChar, 10)).Value = Me.BillingCustomerZip
            com.Parameters.Add(New SqlParameter("@f13", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerCountry

            com.Parameters.Add(New SqlParameter("@f14", SqlDbType.NVarChar, 36)).Value = Me.CreditCardTypeID
            com.Parameters.Add(New SqlParameter("@f15", SqlDbType.NVarChar, 50)).Value = Me.CreditCardName
            'com.Parameters.Add(New SqlParameter("@f16", SqlDbType.NVarChar, 50)).Value = Me.CreditCardNumber
            com.Parameters.Add(New SqlParameter("@f17", SqlDbType.NVarChar, 50)).Value = Me.CreditCardExpDate
            com.Parameters.Add(New SqlParameter("@f18", SqlDbType.NVarChar, 5)).Value = Me.CreditCardCSVNumber
            com.Parameters.Add(New SqlParameter("@f19", SqlDbType.NVarChar, 50)).Value = Me.Priority

 
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

    ',<CreditCardTypeID, nvarchar(36),>
    ',<CreditCardName, nvarchar(50),>
    ',<CreditCardNumber, nvarchar(50),>
    ',<CreditCardExpDate, nvarchar(50),>
    ',<CreditCardCSVNumber, nvarchar(5),>
    ',<Priority, nvarchar(50),>)

    Public CreditCardTypeID As String
    Public CreditCardName As String
    Public CreditCardNumber As String
    Public CreditCardExpDate As String
    Public CreditCardCSVNumber As String
    Public Priority As String
 

    Public Function InsertCardDetailLineNumber() As Boolean
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "INSERT INTO [CustomerCardDetail] ([CompanyID],[DivisionID],[DepartmentID],[CustomerID],[CustomerName],[CustomerAddress1],[CustomerAddress2],[CustomerAddress3],[CustomerCity],[CustomerState],[CustomerZip],[CustomerCountry],[CreditCardTypeID],[CreditCardName],[CreditCardNumber],[CreditCardExpDate],[CreditCardCSVNumber],[Priority])"
        qry = qry & " VALUES(@f1,@f2,@f3,@f5,@f6,@f7,@f8,@f9,@f10,@f11,@f12,@f13,@f14,@f15,@f16,@f17,@f18,@f19)"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 36)).Value = Me.CustomerID
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 255)).Value = Me.BillingCustomerName
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerAddress1
            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerAddress2
            com.Parameters.Add(New SqlParameter("@f9", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerAddress3
            com.Parameters.Add(New SqlParameter("@f10", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerCity
            com.Parameters.Add(New SqlParameter("@f11", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerState
            com.Parameters.Add(New SqlParameter("@f12", SqlDbType.NVarChar, 10)).Value = Me.BillingCustomerZip
            com.Parameters.Add(New SqlParameter("@f13", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerCountry
            com.Parameters.Add(New SqlParameter("@f14", SqlDbType.NVarChar, 36)).Value = Me.CreditCardTypeID
            com.Parameters.Add(New SqlParameter("@f15", SqlDbType.NVarChar, 50)).Value = Me.CreditCardName
            com.Parameters.Add(New SqlParameter("@f16", SqlDbType.NVarChar, 50)).Value = Me.CreditCardNumber
            com.Parameters.Add(New SqlParameter("@f17", SqlDbType.NVarChar, 50)).Value = Me.CreditCardExpDate
            com.Parameters.Add(New SqlParameter("@f18", SqlDbType.NVarChar, 5)).Value = Me.CreditCardCSVNumber
            com.Parameters.Add(New SqlParameter("@f19", SqlDbType.NVarChar, 50)).Value = Me.Priority


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


    Public Function UpdateBillingOrderNumber() As Boolean
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE OrderHeader set BillingCustomerName=@f6,BillingCustomerAddress1=@f7,BillingCustomerAddress2=@f8,BillingCustomerAddress3=@f9,BillingCustomerCity=@f10,BillingCustomerState=@f11,BillingCustomerZip=@f12,BillingCustomerCountry=@f13,BillingCustomerPhone=@f14,BillingCustomerFax=@f15,BillingCustomerEmail=@f16,BillingCustomerCell=@f17,BillingCustomerPhoneExt=@f18 Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And OrderNumber=@f5"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 36)).Value = Me.OrderNumber
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 255)).Value = Me.BillingCustomerName
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerAddress1
            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerAddress2
            com.Parameters.Add(New SqlParameter("@f9", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerAddress3
            com.Parameters.Add(New SqlParameter("@f10", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerCity
            com.Parameters.Add(New SqlParameter("@f11", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerState
            com.Parameters.Add(New SqlParameter("@f12", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerZip
            com.Parameters.Add(New SqlParameter("@f13", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerCountry
            com.Parameters.Add(New SqlParameter("@f14", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerPhone
            com.Parameters.Add(New SqlParameter("@f15", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerFax
            com.Parameters.Add(New SqlParameter("@f16", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerEmail
            com.Parameters.Add(New SqlParameter("@f17", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerCell
            com.Parameters.Add(New SqlParameter("@f18", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerPhoneExt


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


    Public Function InsertCustomerCardDetail() As Boolean

        Dim connec As New SqlConnection(constr)

        Dim qry As String
        qry = "INSERT INTO [Enterprise].[dbo].[CustomerCardDetail]([CompanyID],[DivisionID],[DepartmentID],[CustomerID],[CustomerName]" _
        & ",[CustomerAddress1],[CustomerAddress2],[CustomerAddress3],[CustomerCity],[CustomerState],[CustomerZip],[CustomerCountry]" _
        & ",[CreditCardTypeID],[CreditCardName],[CreditCardNumber],[CreditCardExpDate],[CreditCardCSVNumber],[Priority])" _
        & " values(@f1,@f2,@f3,@f4,@f5,@f6,@f7,@f8,@f9,@f10,@f11,@f12,@f13,@f14,@f15,@f16,@f17,@f18)"

        ''

        ''


        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID.Trim()
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID.Trim()
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID.Trim()
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 50)).Value = Me.CustomerID.Trim()
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 255)).Value = Me.BillingCustomerName
            ''second line'''
            'CustomerAddress1, CustomerAddress2, CustomerAddress3,CustomerCity, CustomerState, CustomerZip, CustomerCountry
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerAddress1.Trim()
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerAddress2.Trim()
            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerAddress3.Trim()
            com.Parameters.Add(New SqlParameter("@f9", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerCity.Trim()
            com.Parameters.Add(New SqlParameter("@f10", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerState.Trim()
            com.Parameters.Add(New SqlParameter("@f11", SqlDbType.NVarChar, 10)).Value = Me.BillingCustomerZip.Trim()
            com.Parameters.Add(New SqlParameter("@f12", SqlDbType.NVarChar, 50)).Value = Me.BillingCustomerCountry.Trim()
            ''third line
            'CustomerPhone, CustomerFax, CustomerEmail,CustomerLogin,CustomerPassword,CustomerFirstName
            com.Parameters.Add(New SqlParameter("@f13", SqlDbType.NVarChar, 36)).Value = Me.CreditCardTypeID.Trim
            com.Parameters.Add(New SqlParameter("@f14", SqlDbType.NVarChar, 50)).Value = Me.CreditCardName.Trim
            'com.Parameters.Add(New SqlParameter("@f15", SqlDbType.NVarChar, 60)).Value = Me.CreditCardNumber.Trim()

            ''New COde FOr encrypt Data Card
            Dim encrypted_Card As String = ""
            Try
                If Me.CreditCardNumber <> "" Then
                    Try
                        encrypted_Card = CryptographyRijndael.EncryptionRijndael.RijndaelEncode(Me.CreditCardNumber, Me.CustomerID)
                    Catch ex As Exception
                        encrypted_Card = Me.CreditCardNumber
                    End Try
                Else
                    encrypted_Card = Me.CreditCardNumber
                End If

            Catch ex As Exception

            End Try
            ''New COde FOr encrypt Data Card
            com.Parameters.Add(New SqlParameter("@f15", SqlDbType.NVarChar, 60)).Value = encrypted_Card


            com.Parameters.Add(New SqlParameter("@f16", SqlDbType.NVarChar, 50)).Value = Me.CreditCardExpDate.Trim()
            com.Parameters.Add(New SqlParameter("@f17", SqlDbType.NVarChar, 5)).Value = Me.CreditCardCSVNumber.Trim()
            com.Parameters.Add(New SqlParameter("@f18", SqlDbType.NVarChar, 50)).Value = Me.Priority.Trim()


            com.Connection.Open()
            com.CommandTimeout = 500
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



    Public CustomerName As String

    Public Function UpdateBillingCustomer() As Boolean
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE CustomerInformation set CustomerName=@f6 Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And CustomerID=@f5"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 36)).Value = Me.CustomerID
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 255)).Value = Me.CustomerName
 
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

    Public Function OrderNumberDetails() As DataTable
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from OrderHeader where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and OrderNumber=@f3"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.OrderNumber

            da.SelectCommand = com
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return dt
        End Try
    End Function


    Public Function CustomerCardDetails() As DataTable
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from CustomerCardDetail Where IsNull(CreditCardNumber,'') <> '' AND CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and CustomerID=@f3 Order By Priority"
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

            Return dt

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return dt
        End Try
    End Function


    Public linenumber As String

    Public Function CustomerCardDetailsForlinenumber() As DataTable
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from CustomerCardDetail where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and CardDetailLineNumber=@f3"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.BigInt)).Value = Me.linenumber

            da.SelectCommand = com
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return dt
        End Try
    End Function


    Public Function CustomerCardDetailsForlinenumberForCustomer() As DataTable
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from CustomerCardDetail where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and CustomerID=@f4 and CardDetailLineNumber=@f3"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.BigInt)).Value = Me.linenumber
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.CustomerID

            da.SelectCommand = com
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return dt
        End Try
    End Function

    Public Function CustomerCardDeleteForlinenumber() As Boolean
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "Delete from CustomerCardDetail where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and CardDetailLineNumber=@f3"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.BigInt)).Value = Me.linenumber

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




    Public Function CheckMultiCreditCardBillingDetails() As DataTable
        Dim dt As New DataTable

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "Select BillingCustomerName,BillingCustomerAddress1,BillingCustomerAddress2,BillingCustomerAddress3,BillingCustomerCity,BillingCustomerState,BillingCustomerZip,BillingCustomerCountry,BillingCustomerPhone,BillingCustomerFax,BillingCustomerEmail,BillingCustomerCell,BillingCustomerPhoneExt from   OrderHeader Where isnull(BillingCustomerName,'') <> '' AND CompanyID=@f1  AND   DivisionID =@f2  AND  DepartmentID =@f3  AND OrderNumber=@f4"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID.Trim()
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID.Trim()
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID.Trim()
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.OrderNumber.Trim

            com.CommandType = CommandType.Text

            Dim da As New SqlDataAdapter(com)

            da.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return dt
        End Try
        Return dt
    End Function



End Class
