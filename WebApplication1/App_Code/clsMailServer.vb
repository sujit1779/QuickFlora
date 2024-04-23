Imports Microsoft.VisualBasic
 Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Collections.Generic
Imports System
Imports System.Net
Imports System.Net.Mail

Public Class clsMailServer

    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    Public CompanyID As String
    Public DivisionID As String
    Public DepartmentID As String

    'INSERT INTO [Enterprise].[dbo].[CompaniesSMTPDetails]
    '([CompanyID]
    ',[DivisionID]
    ',[DepartmentID]
    ',[MailServer1]
    ',[MailServerPort1]
    ',[MailServerUserName1]
    ',[MailServerPassword1]
    ',[MailServer2]
    ',[MailServerPort2]
    ',[MailServerUserName2]
    ',[MailServerPassword2])
    'VALUES
    '(<CompanyID, nvarchar(36),>
    ',<DivisionID, nvarchar(36),>
    ',<DepartmentID, nvarchar(36),>
    ',<MailServer1, nvarchar(255),>
    ',<MailServerPort1, nvarchar(10),>
    ',<MailServerUserName1, nvarchar(50),>
    ',<MailServerPassword1, nvarchar(50),>
    ',<MailServer2, nvarchar(255),>
    ',<MailServerPort2, nvarchar(10),>
    ',<MailServerUserName2, nvarchar(50),>
    ',<MailServerPassword2, nvarchar(50),>)

    Public MailServer1 As String
    Public MailServerPort1 As String
    Public MailServerUserName1 As String
    Public MailServerPassword1 As String

    Public MailServer2 As String
    Public MailServerPort2 As String
    Public MailServerUserName2 As String
    Public MailServerPassword2 As String



    Public Function FillDetails() As DataTable
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from CompaniesSMTPDetails where  CompanyID='" & Me.CompanyID & "' and  DivisionID ='" & Me.DivisionID & "' and  DepartmentID='" & Me.DepartmentID & "'"
        Dim da As New SqlDataAdapter(ssql, constr)
        da.Fill(dt)
        Return dt
    End Function

    Public Function Insert() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "insert into CompaniesSMTPDetails( CompanyID, DivisionID, DepartmentID, MailServer1,MailServerPort1,MailServerUserName1,MailServerPassword1,MailServer2,MailServerPort2,MailServerUserName2,MailServerPassword2) " _
             & " values(@f0,@f1,@f2,@f3,@f4,@f5,@f6,@f7,@f8,@f9,@f10)"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 255)).Value = Me.MailServer1
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 10)).Value = Me.MailServerPort1
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 50)).Value = Me.MailServerUserName1
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 50)).Value = Me.MailServerPassword1
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.NVarChar, 255)).Value = Me.MailServer2
            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.NVarChar, 10)).Value = Me.MailServerPort2
            com.Parameters.Add(New SqlParameter("@f9", SqlDbType.NVarChar, 50)).Value = Me.MailServerUserName2
            com.Parameters.Add(New SqlParameter("@f10", SqlDbType.NVarChar, 50)).Value = Me.MailServerPassword2

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

    Public Function Update() As Boolean
        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "update CompaniesSMTPDetails set  MailServer1=@f3,MailServerPort1=@f4,MailServerUserName1=@f5,MailServerPassword1=@f6,MailServer2=@f7,MailServerPort2=@f8,MailServerUserName2=@f9,MailServerPassword2=@f10 " _
        & " where  CompanyID=@f0 and  DivisionID =@f1 and  DepartmentID=@f2"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 255)).Value = Me.MailServer1
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 10)).Value = Me.MailServerPort1
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 50)).Value = Me.MailServerUserName1
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 50)).Value = Me.MailServerPassword1
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.NVarChar, 255)).Value = Me.MailServer2
            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.NVarChar, 10)).Value = Me.MailServerPort2
            com.Parameters.Add(New SqlParameter("@f9", SqlDbType.NVarChar, 50)).Value = Me.MailServerUserName2
            com.Parameters.Add(New SqlParameter("@f10", SqlDbType.NVarChar, 50)).Value = Me.MailServerPassword2

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


    ' '' UPDATE [Enterprise].[dbo].[CompaniesEmailTrace]
    ' ''SET [CompanyID] = <CompanyID, nvarchar(36),>
    ' '',[DivisionID] = <DivisionID, nvarchar(36),>
    ' '',[DepartmentID] = <DepartmentID, nvarchar(36),>
    ' '',[EmailID] = <EmailID, bigint,>
    ' '',[LocationID] = <LocationID, nvarchar(36),>
    ' '',[From_Email] = <From_Email, nvarchar(255),>
    ' '',[To_Email] = <To_Email, nvarchar(255),>
    ' '',[CC_Email] = <CC_Email, nvarchar(255),>
    ' '',[Email_Subject] = <Email_Subject, ntext,>
    ' '',[Email_Body] = <Email_Body, ntext,>
    ' '',[SMTP_SERVER] = <SMTP_SERVER, nvarchar(255),>
    ' '',[SMTP_PORT] = <SMTP_PORT, nchar(10),>
    ' '',[Email_Date] = <Email_Date, datetime,>
    ' ''WHERE <Search Conditions,,>


    Public LocationID As String = ""
    Public From_Email As String = ""
    Public To_Email As String = ""
    Public CC_Email As String = ""
    Public Email_Subject As String = ""
    Public Email_Body As String = ""
    Public SMTP_SERVER As String = ""
    Public SMTP_PORT As String = ""



    Public Function InsertOutGoingMailDetails() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "insert into CompaniesEmailTrace( CompanyID, DivisionID, DepartmentID, LocationID,From_Email,To_Email,CC_Email,Email_Subject,Email_Body,SMTP_SERVER,SMTP_PORT) " _
             & " values(@f0,@f1,@f2,@f3,@f4,@f5,@f6,@f7,@f8,@f9,@f10)"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.LocationID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 255)).Value = Me.From_Email
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 255)).Value = Me.To_Email
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 255)).Value = Me.CC_Email
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.NText)).Value = Me.Email_Subject
            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.NText)).Value = Me.Email_Body
            com.Parameters.Add(New SqlParameter("@f9", SqlDbType.NVarChar, 255)).Value = Me.SMTP_SERVER
            com.Parameters.Add(New SqlParameter("@f10", SqlDbType.NVarChar, 10)).Value = Me.SMTP_PORT

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
