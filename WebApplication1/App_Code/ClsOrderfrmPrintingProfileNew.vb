Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class ClsOrderfrmPrintingProfileNew
    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
    Public CompanyId As String
    Public DivisionId As String
    Public DepartmentId As String
    Public ProfileId As String
    Public ProfileName As String
    Public PrinterName As String
    Public PrinterTray As String
    Public PrinterPaperSize As String


#Region "Functions"

    Public Function Insert() As Boolean
        Try
            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("sp_InsertPrintingProfile", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyId As New SqlParameter("@CompanyId", Data.SqlDbType.NVarChar)
            parameterCompanyId.Value = CompanyId
            myCommand.Parameters.Add(parameterCompanyId)

            Dim parameterDepartmentId As New SqlParameter("@DepartmentId", Data.SqlDbType.NVarChar)
            parameterDepartmentId.Value = DepartmentId
            myCommand.Parameters.Add(parameterDepartmentId)

            Dim parameterDivisionId As New SqlParameter("@DivisionId", Data.SqlDbType.NVarChar)
            parameterDivisionId.Value = DivisionId
            myCommand.Parameters.Add(parameterDivisionId)

            Dim parameterProfileId As New SqlParameter("@ProfileId", Data.SqlDbType.NVarChar)
            parameterProfileId.Value = ProfileId
            myCommand.Parameters.Add(parameterProfileId)

            Dim parameterProfileName As New SqlParameter("@ProfileName", Data.SqlDbType.NVarChar)
            parameterProfileName.Value = ProfileName
            myCommand.Parameters.Add(parameterProfileName)

            Dim parameterPrinterName As New SqlParameter("@PrinterName", Data.SqlDbType.NVarChar)
            parameterPrinterName.Value = PrinterName
            myCommand.Parameters.Add(parameterPrinterName)

            Dim parameterPrinterTray As New SqlParameter("@PrinterTray", Data.SqlDbType.NVarChar)
            parameterPrinterTray.Value = PrinterTray
            myCommand.Parameters.Add(parameterPrinterTray)

            Dim parameterPrinterPaperSize As New SqlParameter("@PrinterPaperSize", Data.SqlDbType.NVarChar)
            parameterPrinterPaperSize.Value = PrinterPaperSize
            myCommand.Parameters.Add(parameterPrinterPaperSize)

            myCon.Open()
            myCommand.ExecuteNonQuery()
            myCon.Close()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False

        End Try
        
    End Function

    Public Function FillDetails() As DataTable  '' Ok
        Dim ssql As String = ""
        Dim dt As New DataTable()
        'ssql = "select * from PrintingProfile where  PrintingProfileName='" & Me.PrintingProfileName & "'"
        ssql = "select * from PrintingProfile where  CompanyID='" & Me.CompanyId & "' and  DivisionID ='" & Me.DivisionId & "' and  DepartmentID='" & Me.DepartmentId & "' and ProfileName='" & Me.ProfileId & "'"
        Dim da As New SqlDataAdapter(ssql, constr)
        da.Fill(dt)
        Return dt
    End Function

    Public Function FillGridDetails() As DataTable  '' Ok
        Dim ConnectionString As String = ""

        ConnectionString = ConfigurationManager.AppSettings("ConnectionString")
        Dim ConString As New SqlConnection
        ConString.ConnectionString = ConnectionString

        Try
            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("sp_PopulatePrintingProfile", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyId As New SqlParameter("@CompanyId", Data.SqlDbType.NVarChar)
            parameterCompanyId.Value = CompanyId
            myCommand.Parameters.Add(parameterCompanyId)

            Dim parameterDepartmentId As New SqlParameter("@DepartmentId", Data.SqlDbType.NVarChar)
            parameterDepartmentId.Value = DepartmentId
            myCommand.Parameters.Add(parameterDepartmentId)

            Dim parameterDivisionId As New SqlParameter("@DivisionId", Data.SqlDbType.NVarChar)
            parameterDivisionId.Value = DivisionId
            myCommand.Parameters.Add(parameterDivisionId)

            'Dim parameterProfileId As New SqlParameter("@ProfileId", Data.SqlDbType.NVarChar)
            'parameterProfileId.Value = ProfileId
            'myCommand.Parameters.Add(parameterProfileId)

            Dim adapter As New SqlDataAdapter(myCommand)

            Dim ds As New DataTable
            adapter.Fill(ds)
            ConString.Close()
            Return ds

        Catch ex As Exception


        End Try


        'Dim ssql As String = ""
        'Dim dt As New DataTable()
        'ssql = "select * from PrintingProfile  where  CompanyID='" & Me.CompanyId & "' and  DivisionID ='" & Me.DivisionId & "' and  DepartmentID='" & Me.DepartmentId & "'"
        'Dim da As New SqlDataAdapter(ssql, constr)
        'da.Fill(dt)
        'Return dt
    End Function

    
    Public Function Returndatatable(ByVal querry As String) As DataTable
        Dim ssql As String = ""
        Dim dt As New DataTable()
        Dim da As New SqlDataAdapter(querry, constr)
        da.Fill(dt)
        Return dt
    End Function

    Public Function UpdatePrinterProfileDetails() As Boolean
        Try
            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("sp_updatePrintingProfile", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyId As New SqlParameter("@CompanyId", Data.SqlDbType.NVarChar)
            parameterCompanyId.Value = CompanyId
            myCommand.Parameters.Add(parameterCompanyId)

            Dim parameterDepartmentId As New SqlParameter("@DepartmentId", Data.SqlDbType.NVarChar)
            parameterDepartmentId.Value = DepartmentId
            myCommand.Parameters.Add(parameterDepartmentId)

            Dim parameterDivisionId As New SqlParameter("@DivisionId", Data.SqlDbType.NVarChar)
            parameterDivisionId.Value = DivisionId
            myCommand.Parameters.Add(parameterDivisionId)

            Dim parameterProfileId As New SqlParameter("@ProfileId", Data.SqlDbType.NVarChar)
            parameterProfileId.Value = ProfileId
            myCommand.Parameters.Add(parameterProfileId)

            Dim parameterProfileName As New SqlParameter("@ProfileName", Data.SqlDbType.NVarChar)
            parameterProfileName.Value = ProfileName
            myCommand.Parameters.Add(parameterProfileName)

            Dim parameterPrinterName As New SqlParameter("@PrinterName", Data.SqlDbType.NVarChar)
            parameterPrinterName.Value = PrinterName
            myCommand.Parameters.Add(parameterPrinterName)

            Dim parameterPrinterTray As New SqlParameter("@PrinterTray", Data.SqlDbType.NVarChar)
            parameterPrinterTray.Value = PrinterTray
            myCommand.Parameters.Add(parameterPrinterTray)

            Dim parameterPrinterPaperSize As New SqlParameter("@PrinterPaperSize", Data.SqlDbType.NVarChar)
            parameterPrinterPaperSize.Value = PrinterPaperSize
            myCommand.Parameters.Add(parameterPrinterPaperSize)

            myCon.Open()
            myCommand.ExecuteNonQuery()
            myCon.Close()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False

        End Try
    End Function
    Public Function DeletePrinterProfile() As Boolean  '' Under Development -- Vikas
        
        
        Try
            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("sp_DeletePrintingProfile", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyId As New SqlParameter("@CompanyId", Data.SqlDbType.NVarChar)
            parameterCompanyId.Value = CompanyId
            myCommand.Parameters.Add(parameterCompanyId)

            Dim parameterDepartmentId As New SqlParameter("@DepartmentId", Data.SqlDbType.NVarChar)
            parameterDepartmentId.Value = DepartmentId
            myCommand.Parameters.Add(parameterDepartmentId)

            Dim parameterDivisionId As New SqlParameter("@DivisionId", Data.SqlDbType.NVarChar)
            parameterDivisionId.Value = DivisionId
            myCommand.Parameters.Add(parameterDivisionId)

            Dim parameterProfileId As New SqlParameter("@ProfileId", Data.SqlDbType.NVarChar)
            parameterProfileId.Value = ProfileId
            myCommand.Parameters.Add(parameterProfileId)


            myCon.Open()
            myCommand.ExecuteNonQuery()
            myCon.Close()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return False

        End Try

    End Function

    Public Function IsPrinterProfileUsed() As Boolean
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select workTicketPrinterProfileName,CardMessagePrinterProfileName from OrderfrmPrintingConfigurations where  CompanyID='" & Me.CompanyId & "' and  DivisionID ='" & Me.DivisionId & "' and  DepartmentID='" & Me.DepartmentId & "'"
        Dim da As New SqlDataAdapter(ssql, constr)
        da.Fill(dt)
        If dt.Rows(0)(0).ToString() = Me.ProfileId Or dt.Rows(0)(1).ToString() = Me.ProfileId Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region







End Class
