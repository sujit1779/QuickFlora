Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class clsCacelOrder

    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    Public CompanyID As String
    Public DivisionID As String
    Public DepartmentID As String
    Public OrderNumber As String
    Public Ordercancel As Boolean
    Public Ordercanceldate As Date

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

            'Add this New line
            UpdateCancelOrderNumberForPos()
            'Add this New line

            Return True
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False
        End Try
    End Function


    '--From Here need to update

    Public Function UpdateCancelOrderNumberForPos() As Boolean

        Dim dt As New DataTable

        dt = OrderNumberDetails()
        Dim Ordertype As String = ""

        Try
            Ordertype = dt.Rows(0)("OrderTypeID")
        Catch ex As Exception

        End Try
        If Ordertype <> "POS" Then
            Return False
            Exit Function
        End If


        Dim PaymentMethodID As String = ""
        Dim PaymentMethodBit As Boolean = True

        Try
            PaymentMethodID = dt.Rows(0)("PaymentMethodID")
        Catch ex As Exception

        End Try

        If PaymentMethodID = "Credit Card" Then
            PaymentMethodBit = False
        End If


        Dim total As Decimal = 0

        Try
            total = dt.Rows(0)("Total")
        Catch ex As Exception

        End Try

        Dim dtshift As New DataTable
        dtshift = OrderNumberShiftDetails()
        If dtshift.Rows.Count <> 1 Then
            UpdateCancelOrderNumberInternalNotes()
            Return False
            Exit Function

        End If

        Dim shiftid As String = ""
        Try
            shiftid = dtshift.Rows(0)("ShiftID")
        Catch ex As Exception

        End Try


        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE POSShiftMaster set CurrentBalance= (CurrentBalance - @f6) Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And ShiftID=@f7"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.Money)).Value = total
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.NVarChar, 36)).Value = shiftid

            If PaymentMethodBit Then
                com.Connection.Open()
                com.ExecuteNonQuery()
                com.Connection.Close()
            End If

            DeleteOrderNumberFromPOSShiftTransaction()


            Return True
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False
        End Try
    End Function


    Public Function DeleteOrderNumberFromPOSShiftTransaction() As Boolean
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "DELETE FROM POSShiftTransaction Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And OrderNumber=@f5"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 36)).Value = Me.OrderNumber

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

    Public Function OrderNumberShiftDetails() As DataTable
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from POSShiftTransaction where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and OrderNumber=@f3"
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

    Public Function UpdateCancelOrderNumberInternalNotes() As Boolean
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE OrderHeader set InternalNotes=InternalNotes + '   Unable To Update POS ShiftMaster Balance' Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And OrderNumber=@f5"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 36)).Value = Me.OrderNumber

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


    '--Updto Here need to update

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



  
End Class
