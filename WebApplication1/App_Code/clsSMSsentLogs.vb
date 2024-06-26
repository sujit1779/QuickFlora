Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Data


Public Class clsSMSsentLogs


    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    Public CompanyID As String
    Public DivisionID As String
    Public DepartmentID As String

    Public CustomerID As String = ""
    Public InLineNumber As String
    Public PaymentGateway As String = ""

    Public ReferenceNumber As String = ""
    Public RefrenaceType As String = ""
    Public ProcessDate As Date = Date.Now

    Public ProcessAmount As Double = 0
    Public ProcessType As String = ""
    Public ProcessDetails As String = ""


    Public ResponseMessage As String = ""
    Public ResponseNumber As String = ""
    Public SecondryResponseMessage As String = ""



    '   CREATE TABLE [dbo].[SMSSentLogs](
    '	[CompanyID] [nvarchar](36) NULL,
    '	[DivisionID] [nvarchar](36) NULL,
    '	[DepartmentID] [nvarchar](36) NULL,
    '	[CustomerID] [nvarchar](36) NULL,
    '	[InLineNumber] [bigint] NOT NULL,
    '	[ReferenceNumber] [nvarchar](36) NULL,
    '	[RefrenaceType] [nvarchar](36) NULL,
    '	[ProcessDate] [datetime] NULL,
    '	[ProcessAmount] [money] NULL,
    '	[ProcessType] [nvarchar](100) NULL,
    '	[ProcessDetails] [nvarchar](100) NULL, 
    '	[ResponseMessage] [nvarchar](1000) NULL,
    '	[ResponseNumber] [nchar](10) NULL,
    '	[SecondryResponseMessage] [nvarchar](1000) NULL
    ') ON [PRIMARY]

    Public Function InsertSMSLogs() As Boolean

        Me.InLineNumber = Date.Now.Year & Date.Now.Month & Date.Now.Hour & Date.Now.Minute & Date.Now.Second & Date.Now.Millisecond

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "INSERT INTO SMSSentLogs ( CompanyID, DivisionID, DepartmentID, CustomerID, InLineNumber, ReferenceNumber, RefrenaceType, ProcessDate, "
        qry = qry & " ProcessAmount, ProcessType, ProcessDetails, ResponseMessage, ResponseNumber, SecondryResponseMessage )"
        qry = qry & " VALUES(@CompanyID,@DivisionID,@DepartmentID,@CustomerID,@InLineNumber,@ReferenceNumber,@RefrenaceType,@ProcessDate,@ProcessAmount,@ProcessType,@ProcessDetails,@ResponseMessage,@ResponseNumber,@SecondryResponseMessage)"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@CustomerID", SqlDbType.NVarChar, 36)).Value = Me.CustomerID
            com.Parameters.Add(New SqlParameter("@InLineNumber", SqlDbType.BigInt)).Value = Me.InLineNumber

            com.Parameters.Add(New SqlParameter("@ReferenceNumber", SqlDbType.NVarChar, 36)).Value = Me.ReferenceNumber
            com.Parameters.Add(New SqlParameter("@RefrenaceType", SqlDbType.NVarChar, 36)).Value = Me.RefrenaceType
            com.Parameters.Add(New SqlParameter("@ProcessDate", SqlDbType.DateTime)).Value = Me.ProcessDate
            com.Parameters.Add(New SqlParameter("@ProcessAmount", SqlDbType.Money)).Value = Me.ProcessAmount
            com.Parameters.Add(New SqlParameter("@ProcessType", SqlDbType.NVarChar, 100)).Value = Me.ProcessType
            com.Parameters.Add(New SqlParameter("@ProcessDetails", SqlDbType.NVarChar, 100)).Value = Me.ProcessDetails

            com.Parameters.Add(New SqlParameter("@ResponseMessage", SqlDbType.NVarChar, 1000)).Value = Me.ResponseMessage
            com.Parameters.Add(New SqlParameter("@ResponseNumber", SqlDbType.NVarChar, 10)).Value = Me.ResponseNumber
            com.Parameters.Add(New SqlParameter("@SecondryResponseMessage", SqlDbType.NVarChar, 1000)).Value = Me.SecondryResponseMessage


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


    Public Function UpdateSMSLogsResponse() As Boolean
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE SMSSentLogs set ResponseMessage=@ResponseMessage,SecondryResponseMessage=@SecondryResponseMessage,ResponseNumber=@ResponseNumber  Where CompanyID=@CompanyID And DivisionID=@DivisionID And DepartmentID=@DepartmentID And InLineNumber=@InLineNumber  "
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@InLineNumber", SqlDbType.BigInt)).Value = Me.InLineNumber
            com.Parameters.Add(New SqlParameter("@ResponseMessage", SqlDbType.NVarChar, 1000)).Value = Me.ResponseMessage
            com.Parameters.Add(New SqlParameter("@ResponseNumber", SqlDbType.NVarChar, 10)).Value = Me.ResponseNumber
            com.Parameters.Add(New SqlParameter("@SecondryResponseMessage", SqlDbType.NVarChar, 1000)).Value = Me.SecondryResponseMessage

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



    Public Function GetSMSSentLogs() As DataTable


        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from SMSSentLogs where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID

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



    Public Function getSMSSentLogsByinlinenumber() As DataTable


        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from SMSSentLogs where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and InLineNumber=@InLineNumber"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@InLineNumber", SqlDbType.BigInt)).Value = Me.InLineNumber

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




    Public Function PaymentGatewayTransactionLogsSearchList(ByVal Condition As String, ByVal fieldName As String, ByVal fieldexpression As String, ByVal FromDate As String, ByVal ToDate As String, ByVal AllDate As Integer, ByVal SortField As String, ByVal SortDirection As String) As DataSet


        Dim conString As New SqlConnection
        conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

        Dim myCommand As New SqlCommand("PaymentGatewayTransactionLogsSearchList", conString)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
        parameterCompanyID.Value = Me.CompanyID
        myCommand.Parameters.Add(parameterCompanyID)

        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
        parameterDivisionID.Value = Me.DivisionID
        myCommand.Parameters.Add(parameterDivisionID)

        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
        parameterDepartmentID.Value = Me.DepartmentID
        myCommand.Parameters.Add(parameterDepartmentID)

        Dim parameterCondition As New SqlParameter("@Condition", Data.SqlDbType.NVarChar)
        parameterCondition.Value = Condition
        myCommand.Parameters.Add(parameterCondition)


        Dim parameterfieldName As New SqlParameter("@fieldName", Data.SqlDbType.NVarChar)
        parameterfieldName.Value = fieldName
        myCommand.Parameters.Add(parameterfieldName)

        Dim parameterfieldexpression As New SqlParameter("@fieldexpression", Data.SqlDbType.NVarChar)
        parameterfieldexpression.Value = fieldexpression
        myCommand.Parameters.Add(parameterfieldexpression)


        Dim parameterFromDate As New SqlParameter("@FromDate", Data.SqlDbType.NVarChar)
        parameterFromDate.Value = FromDate
        myCommand.Parameters.Add(parameterFromDate)

        Dim parameterToDate As New SqlParameter("@ToDate", Data.SqlDbType.NVarChar)
        parameterToDate.Value = ToDate
        myCommand.Parameters.Add(parameterToDate)


        Dim parameterAllDate As New SqlParameter("@AllDate", Data.SqlDbType.Int)
        parameterAllDate.Value = AllDate
        myCommand.Parameters.Add(parameterAllDate)


        Dim parameterSortField As New SqlParameter("@SortField", Data.SqlDbType.NVarChar)
        parameterSortField.Value = SortField
        myCommand.Parameters.Add(parameterSortField)

        Dim parameterSortDirection As New SqlParameter("@SortDirection", Data.SqlDbType.NVarChar)
        parameterSortDirection.Value = SortDirection
        myCommand.Parameters.Add(parameterSortDirection)


        Dim adapter As New SqlDataAdapter(myCommand)
        Dim ds As New DataSet()
        adapter.Fill(ds)
        conString.Close()

        Return ds


    End Function





End Class

