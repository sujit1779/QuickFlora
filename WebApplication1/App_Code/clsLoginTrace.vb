Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class clsLoginTrace

    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    'CREATE TABLE [dbo].[POSEmployeeLoginTrace](
    '	[CompanyID] [nvarchar](36) NOT NULL,
    '	[DivisionID] [nvarchar](36) NULL,
    '	[DepartmentID] [nvarchar](36) NULL,
    '	[ShiftID] [nvarchar](36) NULL,
    '	[EmployeeID] [nvarchar](36) NULL,
    '	[LogOpenDate] [nvarchar](12) NULL,
    '	[LogOpenTime] [nvarchar](10) NULL,
    '	[LogCloseDate] [nvarchar](12) NULL,
    '	[LogCloseTime] [nvarchar](10) NULL,
    '	[TerminalID] [nvarchar](36) NULL,
    '	[Logstatus] [bit] NULL
    ') ON [PRIMARY]

    Public CompanyID As String
    Public DivisionID As String
    Public DepartmentID As String
    Public ShiftID As String
    Public EmployeeID As String
    Public LogOpenDate As String
    Public LogOpenTime As String
    Public LogCloseDate As String
    Public LogCloseTime As String
    Public TerminalID As String
    Public Logstatus As String

    Public ShiftOpenDate As String
    Public ShiftOpenTime As String
    Public ShiftCloseDate As String
    Public ShiftCloseTime As String


    Public Function GetOrderFortodayShiftclose() As DataTable


        Dim connec As New SqlConnection(constr)
        Dim qry As String
        Dim da As New SqlDataAdapter
        Dim dt As New DataTable
        qry = "Select * from  POSShiftMaster  where CompanyID=@f0 ANd DivisionID=@f1 ANd DepartmentID=@f2 ANd Invoiced=0 And ShiftOpen=0"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.ShiftCloseDate
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


    Public Function GetShifttrace() As DataTable


        Dim connec As New SqlConnection(constr)
        Dim qry As String
        Dim da As New SqlDataAdapter
        Dim dt As New DataTable
        qry = "Select * from  POSShiftMaster  where CompanyID=@f0 ANd DivisionID=@f1 ANd DepartmentID=@f2 And ShiftOpen=1"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
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


    Public Function UpdateShiftForInvoiced() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE POSShiftMaster set Invoiced=1  where  CompanyID=@f0 And DivisionID=@f1 And DepartmentID=@f2 And ShiftID=@f3"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.ShiftID

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



    Public Function GetLogintrace() As DataTable


        Dim connec As New SqlConnection(constr)
        Dim qry As String
        Dim da As New SqlDataAdapter
        Dim dt As New DataTable
        qry = "Select * from  POSEmployeeLoginTrace  where CompanyID=@f0 ANd DivisionID=@f1 ANd DepartmentID=@f2 And Logstatus=1"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
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

    Public Function InsertLogintrace() As Boolean


        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "insert into POSEmployeeLoginTrace( CompanyID, DivisionID, DepartmentID, ShiftID" _
        & " , EmployeeID, LogOpenDate, LogOpenTime, LogCloseDate,TerminalID,Logstatus,LogCloseTime) values(@f0,@f1,@f2,@f3,@f4,@f5,@f6,@f7,@f8,@f9,@f10)"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.ShiftID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.EmployeeID
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 12)).Value = Me.LogOpenDate
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 10)).Value = Me.LogOpenTime
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.NVarChar, 12)).Value = Me.LogCloseDate
            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.NVarChar, 36)).Value = Me.TerminalID
            com.Parameters.Add(New SqlParameter("@f9", SqlDbType.Bit)).Value = Me.Logstatus
            com.Parameters.Add(New SqlParameter("@f10", SqlDbType.NVarChar, 10)).Value = Me.LogCloseTime

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

    Public Function UpdateLogintraceforterminal() As Boolean


        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "update POSEmployeeLoginTrace set LogCloseDate=@f5 ,LogCloseTime=@f7 ,Logstatus=@f6 where  CompanyID=@f0 And DivisionID=@f1 And DepartmentID=@f2 And   TerminalID=@f8 And Logstatus=1"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 12)).Value = ""
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.Bit)).Value = False
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.NVarChar, 10)).Value = ""
            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.NVarChar, 36)).Value = Me.TerminalID

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


    Public Function checkLogintrace() As String


        Dim connec As New SqlConnection(constr)
        Dim qry As String
        Dim da As New SqlDataAdapter
        Dim dt As New DataTable
        qry = "Select * from  POSEmployeeLoginTrace  where CompanyID=@f0 ANd DivisionID=@f1 ANd DepartmentID=@f2 ANd EmployeeID=@f3 And Logstatus=1"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.EmployeeID

            da.SelectCommand = com
            da.Fill(dt)

            If dt.Rows.Count <> 0 Then
                Dim TerminalID1 As String = ""
                Try
                    TerminalID1 = dt.Rows(0)("TerminalID")
                Catch ex As Exception

                End Try

                If TerminalID1 = TerminalID Then
                    ShiftID = dt.Rows(0)("ShiftID")
                    TerminalID = TerminalID
                    LogCloseDate = DateTime.Now().ToString("d")
                    LogCloseTime = DateTime.Now().ToString("hh:mm")
                    Logstatus = False
                    UpdateLogintrace()
                End If

                'Return "You are already logged in on Terminal =" & dt.Rows(0)("TerminalID") & " at " & dt.Rows(0)("LogOpenTime") & " of " & dt.Rows(0)("LogOpenDate")
            Else
                'Return ""
            End If


            Return ""

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return msg
        End Try

    End Function

    Public Function UpdateLogintrace() As Boolean


        Dim connec As New SqlConnection(constr)
        Dim qry As String
        If getmaxLogintraceshift() <> "" Then
            qry = "update POSEmployeeLoginTrace set LogCloseDate=@f5 ,LogCloseTime=@f7 ,Logstatus=@f6 where  CompanyID=@f0 And DivisionID=@f1 And DepartmentID=@f2 And ShiftID=@f3 And  EmployeeID=@f4 And POSlogintraceID=@f8"
            Dim com As SqlCommand
            com = New SqlCommand(qry, connec)
            Try

                com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
                com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
                com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
                com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.ShiftID
                com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.EmployeeID
                com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 12)).Value = Me.LogCloseDate
                com.Parameters.Add(New SqlParameter("@f6", SqlDbType.Bit)).Value = Me.Logstatus
                com.Parameters.Add(New SqlParameter("@f7", SqlDbType.NVarChar, 10)).Value = Me.LogCloseTime
                com.Parameters.Add(New SqlParameter("@f8", SqlDbType.NVarChar, 10)).Value = getmaxLogintraceshift()

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
        Else
            qry = "update POSEmployeeLoginTrace set LogCloseDate=@f5 ,LogCloseTime=@f7 ,Logstatus=@f6 where  CompanyID=@f0 And DivisionID=@f1 And DepartmentID=@f2 And   EmployeeID=@f4 And POSlogintraceID=@f8 And Logstatus=1"
            Dim com As SqlCommand
            com = New SqlCommand(qry, connec)
            Try

                com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
                com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
                com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID

                com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.EmployeeID
                com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 12)).Value = Me.LogCloseDate
                com.Parameters.Add(New SqlParameter("@f6", SqlDbType.Bit)).Value = Me.Logstatus
                com.Parameters.Add(New SqlParameter("@f7", SqlDbType.NVarChar, 10)).Value = Me.LogCloseTime
                com.Parameters.Add(New SqlParameter("@f8", SqlDbType.NVarChar, 10)).Value = getmaxLogintraceshiftforterminal()

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
        End If


    End Function

    Public POSlogintraceID As String

    Public Function UpdateLogintracebyAdmin() As Boolean


        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "update POSEmployeeLoginTrace set LogCloseDate=@f5 ,LogCloseTime=@f7 ,Logstatus=@f6 where  CompanyID=@f0 And DivisionID=@f1 And DepartmentID=@f2 And   EmployeeID=@f4 And POSlogintraceID=@f8 And Logstatus=1"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID

            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.EmployeeID
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 12)).Value = Me.LogCloseDate
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.Bit)).Value = Me.Logstatus
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.NVarChar, 10)).Value = Me.LogCloseTime
            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.NVarChar, 10)).Value = Me.POSlogintraceID

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

    Public Function UpdateLogintraceForshift() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
         
        qry = "update POSEmployeeLoginTrace set  ShiftID=@f3,ShiftOpenDate=@f31,ShiftOpenTime=@f32,ShiftCloseDate=@f311,ShiftCloseTime=@f322 where  CompanyID=@f0 And DivisionID=@f1 And DepartmentID=@f2 And   EmployeeID=@f4 And POSlogintraceID=@f8 And TerminalID=@f9 And Logstatus=1"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.ShiftID
            com.Parameters.Add(New SqlParameter("@f31", SqlDbType.NVarChar, 12)).Value = Me.ShiftOpenDate
            com.Parameters.Add(New SqlParameter("@f32", SqlDbType.NVarChar, 10)).Value = Me.ShiftOpenTime
            com.Parameters.Add(New SqlParameter("@f311", SqlDbType.NVarChar, 12)).Value = ""
            com.Parameters.Add(New SqlParameter("@f322", SqlDbType.NVarChar, 10)).Value = ""
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.EmployeeID
            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.NVarChar, 10)).Value = getmaxLogintraceshiftforterminal()
            com.Parameters.Add(New SqlParameter("@f9", SqlDbType.NVarChar, 36)).Value = Me.TerminalID

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            InsertLogintraceForLoginsshift()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function


    Public Function UpdateLogintraceForshiftClose() As Boolean


        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "update POSEmployeeLoginTrace set  ShiftID=@f3,ShiftCloseDate=@f31,ShiftCloseTime=@f32 where  CompanyID=@f0 And DivisionID=@f1 And DepartmentID=@f2 And   EmployeeID=@f4 And POSlogintraceID=@f8 And TerminalID=@f9 And Logstatus=1"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.ShiftID
            com.Parameters.Add(New SqlParameter("@f31", SqlDbType.NVarChar, 12)).Value = Me.ShiftCloseDate
            com.Parameters.Add(New SqlParameter("@f32", SqlDbType.NVarChar, 10)).Value = Me.ShiftCloseTime
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.EmployeeID
            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.NVarChar, 10)).Value = getmaxLogintraceshiftforterminal()
            com.Parameters.Add(New SqlParameter("@f9", SqlDbType.NVarChar, 36)).Value = Me.TerminalID

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            UpdateLogintraceForLoginshiftClose()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function

    '    CREATE TABLE [dbo].[POSEmployeeLoginShiftTrace](
    '	[POSlogintraceID] [bigint] NULL,
    '	[CompanyID] [nvarchar](36) NULL,
    '	[DivisionID] [nvarchar](36) NULL,
    '	[DepartmentID] [nvarchar](36) NULL,
    '	[ShiftID] [nvarchar](36) NULL,
    '	[ShiftOpenDate] [nvarchar](12) NULL,
    '	[ShiftOpenTime] [nvarchar](10) NULL,
    '	[ShiftCloseDate] [nvarchar](12) NULL,
    '	[ShiftCloseTime] [nvarchar](50) NULL
    '   ) ON [PRIMARY]

    Public Function InsertLogintraceForLoginsshift() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "INSERT INTO POSEmployeeLoginShiftTrace (POSlogintraceID,CompanyID,DivisionID,DepartmentID,ShiftID,ShiftOpenDate,ShiftOpenTime,ShiftCloseDate,ShiftCloseTime) Values(@f00,@f0,@f1,@f2,@f3,@f31,@f32,@f311,@f322)"
        'Values (@f00,@f0,@f1,@f2,@f3,@f31,@f32,@f311,@f322)
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f00", SqlDbType.NVarChar, 10)).Value = getmaxLogintraceshiftforterminal()
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.ShiftID
            com.Parameters.Add(New SqlParameter("@f31", SqlDbType.NVarChar, 12)).Value = Me.ShiftOpenDate
            com.Parameters.Add(New SqlParameter("@f32", SqlDbType.NVarChar, 10)).Value = Me.ShiftOpenTime
            com.Parameters.Add(New SqlParameter("@f311", SqlDbType.NVarChar, 12)).Value = "" 'Me.ShiftCloseDate
            com.Parameters.Add(New SqlParameter("@f322", SqlDbType.NVarChar, 10)).Value = "" 'Me.ShiftCloseTime

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

    Public Function UpdateLogintraceForLoginshiftClose() As Boolean


        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "update POSEmployeeLoginShiftTrace set  ShiftCloseDate=@f31,ShiftCloseTime=@f32 where  CompanyID=@f0 And DivisionID=@f1 And DepartmentID=@f2  And POSlogintraceID=@f8 And ShiftID=@f3 "
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.ShiftID
            com.Parameters.Add(New SqlParameter("@f31", SqlDbType.NVarChar, 12)).Value = Me.ShiftCloseDate
            com.Parameters.Add(New SqlParameter("@f32", SqlDbType.NVarChar, 10)).Value = Me.ShiftCloseTime
            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.NVarChar, 10)).Value = getmaxLogintraceshiftforterminal()

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

    Public Function getmaxLogintraceshiftforterminal() As String


        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "Select max(POSlogintraceID) from  POSEmployeeLoginTrace where EmployeeID=@f4  And CompanyID=@f0 And DivisionID=@f1 And DepartmentID=@f2 And TerminalID=@f3 And Logstatus=1"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.TerminalID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.EmployeeID


            Dim da As New SqlDataAdapter(com)
            Dim dt As New DataTable
            da.Fill(dt)


            If dt.Rows.Count <> 0 Then
                Return dt.Rows(0)(0)
            Else
                Return ""
            End If


        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)

        End Try
        Return ""
    End Function

    Public Function UpdateLogintraceAllold() As Boolean


        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "update POSEmployeeLoginTrace set LogCloseDate=@f5 ,LogCloseTime=@f7 ,Logstatus=@f6 where  CompanyID=@f0 And DivisionID=@f1 And DepartmentID=@f2 And  TerminalID=@f3 And  EmployeeID=@f4 ANd LogCloseDate='' And LogCloseTime=''"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.TerminalID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.EmployeeID
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 12)).Value = Me.LogCloseDate
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.Bit)).Value = Me.Logstatus
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.NVarChar, 10)).Value = Me.LogCloseTime

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

    Public Function getEmployeeidLogintraceshiftforterminal() As String


        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "Select EmployeeID from  POSEmployeeLoginTrace where ShiftID=@f4  And CompanyID=@f0 And DivisionID=@f1 And DepartmentID=@f2 And TerminalID=@f3 And Logstatus=1"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.TerminalID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.ShiftID

            'com.Connection.Open()
            'com.ExecuteNonQuery()
            'com.Connection.Close()
            Dim da As New SqlDataAdapter(com)
            Dim dt As New DataTable
            da.Fill(dt)


            If dt.Rows.Count <> 0 Then
                Return dt.Rows(0)(0)
            Else
                Return ""
            End If


        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)

        End Try
        Return ""
    End Function

    Public Function getmaxLogintraceshift() As String


        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "Select max(POSlogintraceID) from  POSEmployeeLoginTrace where EmployeeID=@f4  And CompanyID=@f0 And DivisionID=@f1 And DepartmentID=@f2 And ShiftID=@f3"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.ShiftID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.EmployeeID

            'com.Connection.Open()
            'com.ExecuteNonQuery()
            'com.Connection.Close()
            Dim da As New SqlDataAdapter(com)
            Dim dt As New DataTable
            da.Fill(dt)


            If dt.Rows.Count <> 0 Then
                Return dt.Rows(0)(0)
            Else
                Return ""
            End If


        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)

        End Try
        Return ""
    End Function

    Public Function getLastEmployeeshiftID() As String


        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "Select EmployeeID from  POSSHIFTMASTER where   CompanyID=@f0 And DivisionID=@f1 And DepartmentID=@f2 And ShiftID=@f3"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = getmaxshiftID()

            Dim da As New SqlDataAdapter(com)
            Dim dt As New DataTable
            da.Fill(dt)


            If dt.Rows.Count <> 0 Then
                Return dt.Rows(0)(0)
            Else
                Return ""
            End If


        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return ""
        End Try
        Return ""
    End Function

    Public Function getmaxshiftID() As String


        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "Select max(ShiftID) from  POSSHIFTMASTER where   CompanyID=@f0 And DivisionID=@f1 And DepartmentID=@f2 And TERMINALID=@f3"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.TerminalID
 
            Dim da As New SqlDataAdapter(com)
            Dim dt As New DataTable
            da.Fill(dt)


            If dt.Rows.Count <> 0 Then
                Return dt.Rows(0)(0)
            Else
                Return ""
            End If


        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return ""
        End Try
        Return ""
    End Function

    Public Function UpdateShift() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE POSShiftMaster set EmployeeID=@f4  where  CompanyID=@f0 And DivisionID=@f1 And DepartmentID=@f2 And ShiftID=@f3"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.ShiftID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.EmployeeID
            
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

    Public OrderNumber As String
    Public CheckID As String
    Public CheckNumber As String
    Public Coupon As String
    Public GiftCertificate As String

    Public Function UpdateOrderCheckDetails() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE OrderHeader set CheckID=@f4,CheckNumber=@f5,Coupon=@f6,GiftCertificate=@f7  where  CompanyID=@f0 And DivisionID=@f1 And DepartmentID=@f2 And OrderNumber=@f3"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.OrderNumber
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 50)).Value = Me.CheckID
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 20)).Value = Me.CheckNumber
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 1000)).Value = Me.Coupon
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.NVarChar, 50)).Value = Me.GiftCertificate

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


    Public Function checkoutforday() As Boolean
        'obj.CompanyID = CompanyID
        'obj.DepartmentID = DepartmentID
        'obj.DivisionID = DivisionID
        'obj.ShiftID = ShiftID

        Dim dt As New DataTable
        dt = GetListOfOrder()

        If dt.Rows.Count <> 0 Then
            Dim n As Integer
            For n = 0 To dt.Rows.Count - 1
                Dim ordernumber As String

                ordernumber = dt.Rows(n)("OrderNumber")
                PostOrder(ordernumber)

            Next

        End If
        Return True
    End Function

    Public ipadd As String = ""
    Public Function PostOrder(ByVal ordnumber As String) As String
        Dim dt As New DataTable
        dt = GetOrderHeaderDetails(ordnumber)

        'Check that Order number details is exist in Order header details tables
        If dt.Rows.Count <> 0 Then


            Try
                Dim Canceled As Boolean

                Try
                    Canceled = dt.Rows(0)("Canceled")
                Catch ex As Exception
                    Canceled = 0
                End Try

                If Canceled Then
                    Exit Function
                End If

                Dim Posted As Boolean

                Try
                    Posted = dt.Rows(0)("Posted")
                Catch ex As Exception
                    Posted = 0
                End Try

                If Posted Then
                Else
                    Exit Function
                End If

            Catch ex As Exception

            End Try

            '' New codes added
            Dim PaymentMethodID As String = ""
            Dim ShipMethodID As String = ""

            Dim Invoiced As Boolean = False

            Try
                PaymentMethodID = dt.Rows(0)("PaymentMethodID")
            Catch ex As Exception
                PaymentMethodID = ""
            End Try
            Try
                Invoiced = dt.Rows(0)("Invoiced")
            Catch ex As Exception
                Invoiced = False
            End Try


            If Invoiced Then
                Exit Function
            End If

            Try
                ShipMethodID = dt.Rows(0)("ShipMethodID")
            Catch ex As Exception
                ShipMethodID = ""
            End Try


            If ShipMethodID.ToLower() <> ("Taken").ToLower() Then
                Exit Function
            End If



            'Else
            Dim nboolchk As Boolean = True

            If PaymentMethodID.ToLower() = ("Cash").ToLower() Then
                nboolchk = False
            End If

            If PaymentMethodID.ToLower() = ("check").ToLower() Then
                nboolchk = False
            End If

            If PaymentMethodID.ToLower() = ("Credit Card").ToLower() Then
                nboolchk = False
            End If

            If PaymentMethodID.ToLower() = ("House Account").ToLower() Then
                nboolchk = False
            End If

            'EMV-Debit 


            If PaymentMethodID.ToLower() = ("EMV-Debit").ToLower() Then
                nboolchk = False
            End If



            If nboolchk Then
                Exit Function
            End If

            '' New codes added

            Dim cardflag As Boolean = True

            Me.OrderNumber = ordnumber

            Dim st As String = ""
            'st = st & "OrderNumber=" & OrderNumber & "<br> and its InvoiceNumber=" & Invoice_CreateFromOrder(OrderNumber) & "<br>"

            'new Code For Caputre The Amount
            Dim obj1 As New clsOrderAdjustments
            obj1.CompanyID = Me.CompanyID
            obj1.DivisionID = Me.DivisionID
            obj1.DepartmentID = Me.DepartmentID
            obj1.OrderNumber = Me.OrderNumber
            Dim dtCardPayment As New Data.DataTable
            dtCardPayment = obj1.DetailsCreditCardPaymentDetails
            If dtCardPayment.Rows.Count <> 0 Then


                Dim ChargeTypes As String = ""
                Try
                    ChargeTypes = dtCardPayment.Rows(0)("ChargeTypes")
                Catch ex As Exception

                End Try


                Dim ChargeStatus As String = ""

                Try
                    ChargeStatus = dtCardPayment.Rows(0)("ChargeStatus")
                Catch ex As Exception

                End Try


                If ChargeTypes = "AUTH" Or ChargeTypes = "InvCancel" Then

                    If ChargeStatus = "AUTH" Or ChargeStatus = "InvCancel" Then

                        cardflag = False

                        Dim objCapture As New clsCaptureCreditCardAmount
                        objCapture.AmttotalCharge = dtCardPayment.Rows(0)("PaymentAmount")
                        objCapture.CompanyID = Me.CompanyID
                        objCapture.DepartmentID = Me.DepartmentID
                        objCapture.DivisionID = Me.DivisionID
                        objCapture.OrderNumber = Me.OrderNumber

                        Dim ApprovalNumber As String
                        ApprovalNumber = objCapture.CaptureCreditCardCHarge()

                        If ApprovalNumber = "ERROR" Then
                            'Return objcheck.lblerrormessag.Text
                            st = st & "OrderNumber=" & OrderNumber & "<br> and its fail during capturing Amount process error is=" & objCapture.lblerrormessag.Text & "<br>"
                        End If

                        If ApprovalNumber.Trim = "" Then
                            ApprovalNumber = "No Approval Number"
                        End If

                        'txtApproval.Text = ApprovalNumber
                        'txtIpAddress.Text = Request.ServerVariables("REMOTE_ADDR")
                        Dim objUser As New DAL.CustomOrder()
                        objUser.UpdateCreditCardTransactions(CompanyID, DepartmentID, DivisionID, OrderNumber, ApprovalNumber, ipadd)
                        If ChargeTypes = "InvCancel" Then
                            obj1.UpdateCreditCardPaymentChargeType("SALE", "SALE")
                        Else
                            obj1.UpdateCreditCardPaymentChargeType("AUTH", "CAPTURE")
                        End If

                        st = st & "OrderNumber=" & OrderNumber & "<br> and its InvoiceNumber=" & Invoice_CreateFromOrder(OrderNumber) & "<br>"
                        Me.OrderNumber = OrderNumber
                        UpdateOrderPostDetails(OrderNumber)
                        st = st & "OrderNumber=" & OrderNumber & "<br> and its Approval Number=" & ApprovalNumber & " after Capture Credit Card Charge.<br>"


                    End If

                End If

            End If

            If cardflag Then
                st = st & "OrderNumber=" & OrderNumber & "<br> and its InvoiceNumber=" & Invoice_CreateFromOrder(OrderNumber) & "<br>"
                Me.OrderNumber = OrderNumber
                UpdateOrderPostDetails(OrderNumber)
            End If


            Return st
            Exit Function


            Dim obj As New ImportInvoiceDetail
            Dim inv As String

            obj.CompanyID = Me.CompanyID
            obj.DepartmentID = Me.DepartmentID
            obj.DivisionID = Me.DivisionID

            obj.CustomerID = dt.Rows(0)("CustomerID")
            inv = obj.GetNextInvoiceNumber
            obj.InvoiceNumber = inv
            While obj.CheckNextInvoiceNumber = "0"
                inv = obj.GetNextInvoiceNumber
                obj.InvoiceNumber = inv
            End While

            If obj.CheckNextInvoiceNumber = "no" Then
                Exit Function
            End If

            'Response.Write(inv)
            '-'''''''''''''''''''''''''''''''''''''''''''''''

            obj.OrderNumber = ordnumber

            obj.TransactionTypeID = "Invoice"
            obj.TransOpen = "True"
            obj.InvoiceDate = Date.Now
            obj.InvoiceDueDate = Date.Now
            obj.InvoiceShipDate = Date.Now
            obj.InvoiceCancelDate = "12/31/2025 12:00:00 AM"

            obj.SystemDate = Date.Now
            obj.Memorize = False
            obj.PurchaseOrderNumber = "None"
            obj.TaxExemptID = ""
            obj.TaxGroupID = ""
            'obj.CustomerID = "Test_05"
            obj.TermsID = "Net Due"

            obj.CurrencyID = "USD"
            obj.CurrencyExchangeRate = "1"
            obj.Subtotal = dt.Rows(0)("Subtotal")
            obj.DiscountPers = dt.Rows(0)("DiscountPers")
            obj.DiscountAmount = dt.Rows(0)("DiscountAmount")
            obj.TaxPercent = dt.Rows(0)("TaxPercent")
            obj.TaxAmount = dt.Rows(0)("TaxAmount")

            obj.TaxableSubTotal = dt.Rows(0)("TaxableSubTotal")
            obj.Freight = dt.Rows(0)("Freight")
            obj.TaxFreight = dt.Rows(0)("TaxFreight")
            obj.Handling = dt.Rows(0)("Handling")
            obj.Advertising = dt.Rows(0)("Advertising")
            obj.Total = dt.Rows(0)("Total")
            Try
                obj.EmployeeID = dt.Rows(0)("EmployeeID")
            Catch ex As Exception
                obj.EmployeeID = ""
            End Try


            obj.CommissionPaid = False

            obj.CommissionSelectToPay = False
            obj.Commission = "0.0000"
            obj.CommissionableSales = "0.0000"
            obj.ComissionalbleCost = "0.0000"
            obj.CustomerDropShipment = False

            obj.ShipMethodID = ""
            obj.WarehouseID = "DEFAULT"
            obj.ShipToID = "SAME"
            obj.ShipForID = "SAME"
            obj.ShippingName = ""
            obj.ShippingAddress1 = ""
            obj.ShippingAddress2 = ""

            obj.ShippingAddress3 = ""
            obj.ShippingCity = ""
            obj.ShippingState = ""
            obj.ShippingZip = ""
            obj.ShippingCountry = ""
            obj.GLSalesAccount = "410000"

            obj.PaymentMethodID = ""
            obj.AmountPaid = "0.0000"
            obj.UndistributedAmount = "0.0000"
            obj.BalanceDue = dt.Rows(0)("Total")
            obj.Picked = False
            obj.PickedDate = Date.Now
            obj.Printed = False

            obj.PrintedDate = Date.Now
            obj.Shipped = True
            obj.ShipDate = Date.Now
            obj.TrackingNumber = ""
            obj.Billed = False
            obj.BilledDate = Date.Now
            obj.Backordered = False
            obj.Posted = False
            obj.PostedDate = Date.Now
            obj.InsertInvoiceHeader()


            Dim dt1 As New DataTable
            dt1 = GetOrderDetails(ordnumber)

            'Check that Order number details is exist in Order details tables for the items 

            If dt1.Rows.Count <> 0 Then

                Dim n As Integer
                For n = 0 To dt1.Rows.Count - 1

                    obj.InvoiceLineNumber = "0"
                    obj.ItemID = dt1.Rows(n)("ItemID")
                    obj.WarehouseID = dt1.Rows(n)("WarehouseID")
                    Try
                        obj.WarehouseBinID = dt1.Rows(n)("WarehouseBinID")
                    Catch ex As Exception
                        obj.WarehouseBinID = "DEFAULT"
                    End Try


                    obj.OrderQty = dt1.Rows(n)("OrderQty")
                    obj.Backordered = dt1.Rows(n)("BackOrdered")
                    obj.BackOrderQty = dt1.Rows(n)("BackOrderQyyty")
                    obj.ItemUOM = dt1.Rows(n)("ItemUOM")
                    Try
                        obj.ItemWeight = dt1.Rows(n)("ItemWeight")
                    Catch ex As Exception
                        obj.ItemWeight = "0"
                    End Try

                    obj.Description = dt1.Rows(n)("Description")
                    obj.DiscountPerc = dt1.Rows(n)("DiscountPerc")
                    obj.Taxable = dt1.Rows(n)("Taxable")
                    Try
                        obj.CurrencyID = dt1.Rows(n)("CurrencyID")
                    Catch ex As Exception
                        obj.CurrencyID = "USD"
                    End Try
                    Try
                        obj.CurrencyExchangeRate = dt1.Rows(n)("CurrencyExchangeRate")
                    Catch ex As Exception
                        obj.CurrencyExchangeRate = "1"
                    End Try

                    Try
                        obj.ItemCost = dt1.Rows(n)("ItemCost")
                    Catch ex As Exception
                        obj.ItemCost = "0.000"
                    End Try

                    obj.ItemUnitPrice = dt1.Rows(n)("ItemUnitPrice")

                    Try
                        obj.TaxGroupID = dt1.Rows(n)("TaxGroupID")
                    Catch ex As Exception
                        obj.TaxGroupID = "DEFAULT"
                    End Try

                    Try
                        obj.TaxAmount = dt1.Rows(n)("TaxAmount")
                    Catch ex As Exception
                        obj.TaxAmount = "0.000"
                    End Try

                    Try
                        obj.TaxPercent = dt1.Rows(n)("TaxPercent")
                    Catch ex As Exception
                        obj.TaxPercent = "0.000"
                    End Try

                    Try
                        obj.Subtotal = dt1.Rows(n)("SubTotal")
                    Catch ex As Exception
                        obj.Subtotal = "0.000"
                    End Try

                    obj.Total = dt1.Rows(n)("Total")
                    obj.TotalWeight = dt1.Rows(n)("TotalWeight")

                    Try
                        obj.GLSalesAccount = dt1.Rows(n)("GLSalesAccount")
                    Catch ex As Exception
                        obj.GLSalesAccount = ""
                    End Try

                    Try
                        obj.ProjectID = dt1.Rows(n)("ProjectID")
                    Catch ex As Exception
                        obj.ProjectID = "DEFAULT"
                    End Try


                    obj.InsertInvoiceDetail()

                Next

            End If

            Dim rs As Integer
            rs = obj.PostInvoice()

            Me.OrderNumber = ordnumber
            UpdateOrderPostDetails(obj.InvoiceNumber)

        End If

        Return True

    End Function

    Public Function GetListOfOrder() As DataTable
        Dim ssql As String = ""
        Dim connec As New SqlConnection(constr)
        Dim dt As New DataTable()
        ssql = "SELECT * FROM POSShiftTransaction WHERE ShiftID=@ShiftID AND CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 "

        'HttpContext.Current.Response.Write(ssql)

        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@ShiftID", SqlDbType.NVarChar, 36)).Value = Me.ShiftID

            da.SelectCommand = com
            da.Fill(dt)


        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)

        End Try
        Return dt
    End Function

    Public Function GetOrderHeaderDetails(ByVal ordnumber As String) As DataTable
        Dim ssql As String = ""
        Dim connec As New SqlConnection(constr)
        Dim dt As New DataTable()
        ssql = "SELECT * FROM OrderHeader WHERE OrderNumber=@OrderNumber AND CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 "

        'HttpContext.Current.Response.Write(ssql)

        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@OrderNumber", SqlDbType.NVarChar, 36)).Value = ordnumber

            da.SelectCommand = com
            da.Fill(dt)


        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)

        End Try
        Return dt
    End Function

    Public Function GetOrderDetails(ByVal ordnumber As String) As DataTable
        Dim ssql As String = ""
        Dim connec As New SqlConnection(constr)
        Dim dt As New DataTable()
        ssql = "SELECT * FROM OrderDetail WHERE OrderNumber=@OrderNumber AND CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 "

        'HttpContext.Current.Response.Write(ssql)

        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@OrderNumber", SqlDbType.NVarChar, 36)).Value = ordnumber

            da.SelectCommand = com
            da.Fill(dt)

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
        End Try
        Return dt
    End Function

    Public Function UpdateOrderPostDetails(ByVal InvoiceNumber As String) As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE OrderHeader set Picked=@f4,PickedDate=@f5,Shipped=@f6,ShipDate=@f7,Printed=@f8,PrintedDate=@f9,Invoiced=@f10,InvoiceDate=@f11,InvoiceNumber=@f12  where  CompanyID=@f0 And DivisionID=@f1 And DepartmentID=@f2 And OrderNumber=@f3"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            'Picked=@f4,PickedDate=@f5,Shipped=@f6,ShipDate=@f7,Printed=@f8,PrintedDate=@f9,
            'Invoiced=@f10,InvoiceDate=@f11,InvoiceNumber=@f12
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.OrderNumber
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.Bit, 50)).Value = True
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.DateTime)).Value = Date.Now

            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.Bit, 50)).Value = True
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.DateTime)).Value = Date.Now

            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.Bit, 50)).Value = True
            com.Parameters.Add(New SqlParameter("@f9", SqlDbType.DateTime)).Value = Date.Now

            com.Parameters.Add(New SqlParameter("@f10", SqlDbType.Bit, 50)).Value = True
            com.Parameters.Add(New SqlParameter("@f11", SqlDbType.DateTime)).Value = Date.Now

            com.Parameters.Add(New SqlParameter("@f12", SqlDbType.NVarChar, 20)).Value = InvoiceNumber

            com.CommandTimeout = 1000

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

    Public Function Invoice_CreateFromOrder(ByVal OrderNumber As String) As String
        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand("[enterprise].[Invoice_CreateFromOrder]", myCon)
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
        parameterOrderNumber.Value = OrderNumber
        myCommand.Parameters.Add(parameterOrderNumber)


        Dim parameterPostingResult As New SqlParameter("@InvoiceNumber", Data.SqlDbType.NVarChar, 36)
        parameterPostingResult.Direction = ParameterDirection.Output
        myCommand.Parameters.Add(parameterPostingResult)
        Dim OutputValue As String = ""
        myCon.Open()

        myCommand.CommandTimeout = 6000

        myCommand.ExecuteNonQuery()

        OutputValue = parameterPostingResult.Value.ToString()

        myCon.Close()
        Return OutputValue

    End Function

End Class
