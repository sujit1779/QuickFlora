Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class clsOrderEntryFormWireIn

    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    Public CompanyID As String
    Public DivisionID As String
    Public DepartmentID As String
    
    Public FloristName As String
    Public FloristPhone As String
    Public City As String
    Public State As String
    Public Representativetalkedto As String
    Public Receivedamount As String


    'new feild added
    Public CustomerID As String
    Public OrderTypeID As String
    Public TransactionTypeID As String
    Public WireService As String
    'new feild added
    'Public VendorID As String

    Public OrderNumber As String

    Public Function DetailsOrderHeader() As DataTable
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from OrderWireServiceDetails where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and OrderNumber=@f3"
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
            HttpContext.Current.Response.Write(msg)
            Return dt

        End Try

    End Function


    Public Function UpdateOrderWireIn() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE OrderWireServiceDetails set RepresentativeTalkedTo=@f4 ,FloristName=@f5 ,ReceivedAmount=@f6 ,FloristPhone=@f7,FloristCity=@f8,FloristState=@f9   Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And OrderNumber=@f05"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.Representativetalkedto
            com.Parameters.Add(New SqlParameter("@f05", SqlDbType.NVarChar, 100)).Value = Me.OrderNumber
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 100)).Value = Me.FloristName
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.Money)).Value = Me.Receivedamount
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.NVarChar, 50)).Value = Me.FloristPhone
            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.NVarChar, 50)).Value = Me.City
            com.Parameters.Add(New SqlParameter("@f9", SqlDbType.NVarChar, 50)).Value = Me.State

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

    Public Function DetailsTransactionTypeOrderHeader() As DataTable
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
            HttpContext.Current.Response.Write(msg)
            Return dt

        End Try

    End Function

    Public Function InsertWireDetailsOrder() As String
        'Dim dt As New DataTable
        'dt = DetailsTransactionTypeOrderHeader()

        'If dt.Rows.Count <> 0 Then
        '    Try
        '        CustomerID = dt.Rows(0)("CustomerID")
        '        OrderTypeID = dt.Rows(0)("OrderTypeID")
        '        TransactionTypeID = dt.Rows(0)("TransactionTypeID")
        '        WireService = dt.Rows(0)("WireOutService")
        '    Catch ex As Exception

        '    End Try
        'End If

        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand("[enterprise].[Order_WireSerivceDetailsUpdate]", myCon)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
        parameterCompanyID.Value = Me.CompanyID
        myCommand.Parameters.Add(parameterCompanyID)

        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
        parameterDepartmentID.Value = Me.DepartmentID
        myCommand.Parameters.Add(parameterDepartmentID)

        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
        parameterDivisionID.Value = Me.DivisionID
        myCommand.Parameters.Add(parameterDivisionID)

        Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
        parameterOrderNumber.Value = Me.OrderNumber
        myCommand.Parameters.Add(parameterOrderNumber)

        Dim parameterCustomerID As New SqlParameter("@CustomerID", Data.SqlDbType.NVarChar)
        parameterCustomerID.Value = Me.CustomerID
        myCommand.Parameters.Add(parameterCustomerID)

        Dim parameterOrderTypeID As New SqlParameter("@OrderTypeID", Data.SqlDbType.NVarChar)
        parameterOrderTypeID.Value = Me.OrderTypeID
        myCommand.Parameters.Add(parameterOrderTypeID)

        Dim parameterTransactionTypeID As New SqlParameter("@TransactionTypeID", Data.SqlDbType.NVarChar)
        parameterTransactionTypeID.Value = Me.TransactionTypeID
        myCommand.Parameters.Add(parameterTransactionTypeID)

        Dim parameterWireService As New SqlParameter("@WireService", Data.SqlDbType.NVarChar)
        parameterWireService.Value = Me.WireService
        myCommand.Parameters.Add(parameterWireService)

        Dim parameterPostingResult As New SqlParameter("@PostingResult", Data.SqlDbType.NVarChar, 200)
        parameterPostingResult.Direction = ParameterDirection.Output
        myCommand.Parameters.Add(parameterPostingResult)
        Dim OutputValue As String = ""
        myCon.Open()

        myCommand.ExecuteNonQuery()

        OutputValue = parameterPostingResult.Value.ToString()

        myCon.Close()
        Return OutputValue

    End Function

    Public DeliveryAmount As Decimal

    Public Function UpdateDiscountFromDeliveryItemWise() As String
        'Dim dt As New DataTable
        'dt = DetailsTransactionTypeOrderHeader()

        'If dt.Rows.Count <> 0 Then
        '    Try
        '        CustomerID = dt.Rows(0)("CustomerID")
        '        OrderTypeID = dt.Rows(0)("OrderTypeID")
        '        TransactionTypeID = dt.Rows(0)("TransactionTypeID")
        '        WireService = dt.Rows(0)("WireOutService")
        '    Catch ex As Exception

        '    End Try
        'End If

        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand("[UpdateDiscountFromDeliveryItemWise]", myCon)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
        parameterCompanyID.Value = Me.CompanyID
        myCommand.Parameters.Add(parameterCompanyID)

        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
        parameterDepartmentID.Value = Me.DepartmentID
        myCommand.Parameters.Add(parameterDepartmentID)

        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
        parameterDivisionID.Value = Me.DivisionID
        myCommand.Parameters.Add(parameterDivisionID)

        Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
        parameterOrderNumber.Value = Me.OrderNumber
        myCommand.Parameters.Add(parameterOrderNumber)

        Dim parameterDeliveryAmount As New SqlParameter("@DeliveryAmount", Data.SqlDbType.Money)
        parameterDeliveryAmount.Value = Me.DeliveryAmount
        myCommand.Parameters.Add(parameterDeliveryAmount)


        myCon.Open()
        myCommand.ExecuteNonQuery()
        myCon.Close()

        'OutputValue = OutputValue & "CompanyID=" & CompanyID & " "
        'OutputValue = OutputValue & "OrderNumber=" & OrderNumber & " "
        'OutputValue = OutputValue & "TransactionTypeID=" & CustomerID & " "
        'OutputValue = OutputValue & "WireService=" & WireService & " "


        Return ""

    End Function


  Public Function DeleteOrderWires() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "DELETE FROM [Enterprise].[dbo].[OrderWireServiceDetails]   Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And OrderNumber=@f05"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f05", SqlDbType.NVarChar, 100)).Value = Me.OrderNumber
            

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

End Class
