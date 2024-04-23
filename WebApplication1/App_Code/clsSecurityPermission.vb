Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class clsSecurityPermission

    'Table Name AccessPermissions 
    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    'Public variable

    Public CompanyID As String = ""
    Public DivisionID As String = ""
    Public DepartmentID As String = ""
    Public EmployeeID As String = ""
    Public DefaultPageToDisplay As String = ""
    Public IPAddress As String = ""
    Public MachineName As String = ""
    Public RestrictMultipleLogins As String = ""
    Public RestrictSecurityIP As String = ""
    Public SecurityLevel As String = ""
    Public OEView As Boolean = False
    Public OEAdd As Boolean = False
    Public OEEdit As Boolean = False
    Public OEDelete As Boolean = False
    Public OEReports As Boolean = False
    Public ApproveOrders As Boolean = False
    Public ARView As Boolean = False
    Public ARAdd As Boolean = False
    Public AREdit As Boolean = False
    Public ARDelete As Boolean = False
    Public ARReports As Boolean = False
    Public ApproveReceipt As Boolean = False
    Public ApproveContract As Boolean = False
    Public ApproveCustomer As Boolean = False
    Public ApproveRMA As Boolean = False
    Public POView As Boolean = False
    Public POAdd As Boolean = False
    Public POEdit As Boolean = False
    Public PODelete As Boolean = False
    Public ApprovePurchase As Boolean = False
    Public POReports As Boolean = False
    Public APView As Boolean = False
    Public APAdd As Boolean = False
    Public APEdit As Boolean = False
    Public APDelete As Boolean = False
    Public APChecks As Boolean = False
    Public APReports As Boolean = False
    Public ApproveVendor As Boolean = False
    Public ApproveAPChedks As Boolean = False
    Public ApprovePayment As Boolean = False
    Public ApproveReturns As Boolean = False
    Public GLView As Boolean = False
    Public GLAdd As Boolean = False
    Public GLEdit As Boolean = False
    Public GLDelete As Boolean = False
    Public GLReports As Boolean = False
    Public GLFinancials As Boolean = False
    Public GLMonthEnd As Boolean = False
    Public GLYearEnd As Boolean = False
    Public WHAdjust As Boolean = False
    Public WHTransfer As Boolean = False
    Public WHPick As Boolean = False
    Public WHPack As Boolean = False
    Public WHShip As Boolean = False
    Public WHReceive As Boolean = False
    Public WHPrint As Boolean = False
    Public WOView As Boolean = False
    Public WOAdd As Boolean = False
    Public WOEdit As Boolean = False
    Public WODelete As Boolean = False
    Public WOReports As Boolean = False
    Public WOForecast As Boolean = False
    Public ApproveAdjustment As Boolean = False
    Public ApproveTransfer As Boolean = False
    Public ApproveItems As Boolean = False
    Public EMView As Boolean = False
    Public EMAdd As Boolean = False
    Public EMEdit As Boolean = False
    Public EMDelete As Boolean = False
    Public EMReports As Boolean = False
    Public ApproveEmployees As Boolean = False
    Public PRView As Boolean = False
    Public PRAdd As Boolean = False
    Public PREdit As Boolean = False
    Public PRDelete As Boolean = False
    Public PRReports As Boolean = False
    Public PRChecks As Boolean = False
    Public ApprovePayroll As Boolean = False
    Public ADView As Boolean = False
    Public ADSecurity As Boolean = False
    Public ADSetup As Boolean = False
    Public APSetup As Boolean = False
    Public ARSetup As Boolean = False
    Public AuditSetup As Boolean = False
    Public ChallangeSetup As Boolean = False
    Public EMSetup As Boolean = False
    Public GLSetup As Boolean = False
    Public OESetup As Boolean = False
    Public POSetup As Boolean = False
    Public PRSetup As Boolean = False
    Public WHSetup As Boolean = False
    Public WOSetup As Boolean = False
    Public AuditView As Boolean = False
    Public AuditAdd As Boolean = False
    Public AuditEdit As Boolean = False
    Public AuditDelete As Boolean = False
    Public AuditReports As Boolean = False


    Public Function FillAccessPermissionsValuesFromDB()

        Dim dt As New DataTable

        dt = GetValueFromDB()

        If dt.Rows.Count <> 0 Then
            Try
                DefaultPageToDisplay = dt.Rows(0)("DefaultPageToDisplay")
            Catch ex As Exception

            End Try
            Try
                IPAddress = dt.Rows(0)("IPAddress")
            Catch ex As Exception

            End Try
            Try
                MachineName = dt.Rows(0)("MachineName")
            Catch ex As Exception

            End Try
            Try
                RestrictMultipleLogins = dt.Rows(0)("RestrictMultipleLogins")
            Catch ex As Exception

            End Try

            Try
                RestrictSecurityIP = dt.Rows(0)("RestrictSecurityIP")
            Catch ex As Exception

            End Try
            Try
                SecurityLevel = dt.Rows(0)("SecurityLevel ")
            Catch ex As Exception

            End Try
            Try
                OEView = dt.Rows(0)("OEView")
            Catch ex As Exception

            End Try

            Try
                OEAdd = dt.Rows(0)("OEAdd")
            Catch ex As Exception

            End Try

            Try
                OEEdit = dt.Rows(0)("OEEdit")
            Catch ex As Exception

            End Try

            Try
                OEDelete = dt.Rows(0)("OEDelete")
            Catch ex As Exception

            End Try

            Try
                OEReports = dt.Rows(0)("OEReports")
            Catch ex As Exception

            End Try

            Try
                ApproveOrders = dt.Rows(0)("ApproveOrders")
            Catch ex As Exception

            End Try

            Try
                ARView = dt.Rows(0)("ARView")
            Catch ex As Exception

            End Try

            Try
                ARAdd = dt.Rows(0)("ARAdd")
            Catch ex As Exception

            End Try
            Try
                AREdit = dt.Rows(0)("AREdit")
            Catch ex As Exception

            End Try

            Try
                ARDelete = dt.Rows(0)("ARDelete")
            Catch ex As Exception

            End Try
            Try
                ARReports = dt.Rows(0)("ARReports")
            Catch ex As Exception

            End Try
            Try
                ApproveReceipt = dt.Rows(0)("ApproveReceipt")
            Catch ex As Exception

            End Try
            Try
                ApproveContract = dt.Rows(0)("ApproveContract")
            Catch ex As Exception

            End Try
            Try
                ApproveCustomer = dt.Rows(0)("ApproveCustomer")
            Catch ex As Exception

            End Try
            Try
                ApproveRMA = dt.Rows(0)("ApproveRMA")
            Catch ex As Exception

            End Try
            Try
                POView = dt.Rows(0)("POView")
            Catch ex As Exception

            End Try
            Try
                POAdd = dt.Rows(0)("POAdd")
            Catch ex As Exception

            End Try
            Try
                POEdit = dt.Rows(0)("POEdit")
            Catch ex As Exception

            End Try
            Try
                PODelete = dt.Rows(0)("PODelete")
            Catch ex As Exception

            End Try
            Try
                ApprovePurchase = dt.Rows(0)("ApprovePurchase")
            Catch ex As Exception

            End Try
            Try
                POReports = dt.Rows(0)("POReports")
            Catch ex As Exception

            End Try
            Try
                APView = dt.Rows(0)("APView")
            Catch ex As Exception

            End Try
            Try

                APAdd = dt.Rows(0)("APAdd")
            Catch ex As Exception

            End Try
            Try
                APEdit = dt.Rows(0)("APEdit")
            Catch ex As Exception

            End Try
            Try
                APDelete = dt.Rows(0)("APDelete")
            Catch ex As Exception

            End Try
            Try
                APChecks = dt.Rows(0)("APChecks")
            Catch ex As Exception

            End Try
            Try
                APReports = dt.Rows(0)("APReports")
            Catch ex As Exception

            End Try
            Try
                ApproveVendor = dt.Rows(0)("ApproveVendor")
            Catch ex As Exception

            End Try
            Try
                ApproveAPChedks = dt.Rows(0)("ApproveAPChedks")
            Catch ex As Exception

            End Try
            Try

                ApprovePayment = dt.Rows(0)("ApprovePayment")
            Catch ex As Exception

            End Try
            Try
                ApproveReturns = dt.Rows(0)("ApproveReturns")
            Catch ex As Exception

            End Try
            Try
                GLView = dt.Rows(0)("GLView")
            Catch ex As Exception

            End Try
            Try
                GLAdd = dt.Rows(0)("GLAdd")
            Catch ex As Exception

            End Try
            Try
                GLEdit = dt.Rows(0)("GLEdit")
            Catch ex As Exception

            End Try
            Try
                GLDelete = dt.Rows(0)("GLDelete")
            Catch ex As Exception

            End Try
            Try
                GLReports = dt.Rows(0)("GLReports")
            Catch ex As Exception

            End Try
            Try
                GLFinancials = dt.Rows(0)("GLFinancials")
            Catch ex As Exception

            End Try
            Try
                GLMonthEnd = dt.Rows(0)("GLMonthEnd")
            Catch ex As Exception

            End Try
            Try
                GLYearEnd = dt.Rows(0)("GLYearEnd")
            Catch ex As Exception

            End Try
            Try
                WHAdjust = dt.Rows(0)("WHAdjust")
            Catch ex As Exception

            End Try
            Try
                WHTransfer = dt.Rows(0)("WHTransfer")
            Catch ex As Exception

            End Try
            Try
                WHPick = dt.Rows(0)("WHPick")
            Catch ex As Exception

            End Try
            Try
                WHPack = dt.Rows(0)("WHPack")
            Catch ex As Exception

            End Try
            Try
                WHShip = dt.Rows(0)("WHShip")
            Catch ex As Exception

            End Try
            Try
                WHReceive = dt.Rows(0)("WHReceive")
            Catch ex As Exception

            End Try
            Try
                WHPrint = dt.Rows(0)("WHPrint")
            Catch ex As Exception

            End Try
            Try
                WOView = dt.Rows(0)("WOView")
            Catch ex As Exception

            End Try
            Try
                WOAdd = dt.Rows(0)("WOAdd")
            Catch ex As Exception

            End Try
            Try
                WOEdit = dt.Rows(0)("WOEdit")
            Catch ex As Exception

            End Try
            Try
                WODelete = dt.Rows(0)("WODelete")
            Catch ex As Exception

            End Try
            Try
                WOReports = dt.Rows(0)("WOReports")
            Catch ex As Exception

            End Try
            Try
                WOForecast = dt.Rows(0)("WOForecast")
            Catch ex As Exception

            End Try
            Try
                ApproveAdjustment = dt.Rows(0)("ApproveAdjustment")
            Catch ex As Exception

            End Try
            Try
                ApproveTransfer = dt.Rows(0)("ApproveTransfer")
            Catch ex As Exception

            End Try
            Try
                ApproveItems = dt.Rows(0)("ApproveItems")
            Catch ex As Exception

            End Try
            Try
                EMView = dt.Rows(0)("EMView")
            Catch ex As Exception

            End Try
            Try
                EMAdd = dt.Rows(0)("EMAdd")
            Catch ex As Exception

            End Try
            Try
                EMEdit = dt.Rows(0)("EMEdit")
            Catch ex As Exception

            End Try
            Try
                EMDelete = dt.Rows(0)("EMDelete")
            Catch ex As Exception

            End Try
            Try
                EMReports = dt.Rows(0)("EMReports")
            Catch ex As Exception

            End Try
            Try
                ApproveEmployees = dt.Rows(0)("ApproveEmployees")
            Catch ex As Exception

            End Try
            Try
                PRView = dt.Rows(0)("PRView")
            Catch ex As Exception

            End Try
            Try
                PRAdd = dt.Rows(0)("PRAdd")
            Catch ex As Exception

            End Try
            Try
                PREdit = dt.Rows(0)("PREdit")
            Catch ex As Exception

            End Try
            Try
                PRDelete = dt.Rows(0)("PRDelete")
            Catch ex As Exception

            End Try
            Try
                PRReports = dt.Rows(0)("PRReports")
            Catch ex As Exception

            End Try
            Try
                PRChecks = dt.Rows(0)("PRChecks")
            Catch ex As Exception

            End Try
            Try
                ApprovePayroll = dt.Rows(0)("ApprovePayroll")
            Catch ex As Exception

            End Try
            Try
                ADView = dt.Rows(0)("ADView")
            Catch ex As Exception

            End Try
            Try
                ADSecurity = dt.Rows(0)("ADSecurity")
            Catch ex As Exception

            End Try
            Try
                ADSetup = dt.Rows(0)("ADSetup")
            Catch ex As Exception

            End Try
            Try
                APSetup = dt.Rows(0)("APSetup")
            Catch ex As Exception

            End Try
            Try
                ARSetup = dt.Rows(0)("ARSetup")
            Catch ex As Exception

            End Try
            Try
                AuditSetup = dt.Rows(0)("AuditSetup")
            Catch ex As Exception

            End Try
            Try
                ChallangeSetup = dt.Rows(0)("ChallangeSetup")
            Catch ex As Exception

            End Try
            Try
                EMSetup = dt.Rows(0)("EMSetup")
            Catch ex As Exception

            End Try
            Try
                GLSetup = dt.Rows(0)("GLSetup")
            Catch ex As Exception

            End Try
            Try
                OESetup = dt.Rows(0)("OESetup")
            Catch ex As Exception

            End Try
            Try
                POSetup = dt.Rows(0)("POSetup")
            Catch ex As Exception

            End Try
            Try
                PRSetup = dt.Rows(0)("PRSetup")
            Catch ex As Exception

            End Try
            Try
                WHSetup = dt.Rows(0)("WHSetup")
            Catch ex As Exception

            End Try
            Try
                WOSetup = dt.Rows(0)("WOSetup")
            Catch ex As Exception

            End Try
            Try
                AuditView = dt.Rows(0)("AuditView")
            Catch ex As Exception

            End Try
            Try
                AuditAdd = dt.Rows(0)("AuditAdd")
            Catch ex As Exception

            End Try
            Try
                AuditEdit = dt.Rows(0)("AuditEdit")
            Catch ex As Exception

            End Try
            Try
                AuditDelete = dt.Rows(0)("AuditDelete")
            Catch ex As Exception

            End Try
            Try
                AuditReports = dt.Rows(0)("AuditReports")
            Catch ex As Exception

            End Try
            '

        End If

        Return ""
    End Function


    Public Function GetValueFromDB() As DataTable

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "Select * from AccessPermissions WHERE CompanyID=@CompanyID AND DivisionID=@DivisionID AND DepartmentID=@DepartmentID AND EmployeeID=@EmployeeID	"
        Dim dt As New DataTable
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@EmployeeID", SqlDbType.NVarChar, 36)).Value = Me.EmployeeID


            Dim da As New SqlDataAdapter(com)

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
