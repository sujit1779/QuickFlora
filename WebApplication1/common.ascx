<%@ Control Language="VB" ClassName="Common" Debug="True" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Web.Mail" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="System.Text.RegularExpressions" %>

<script runat="server">

    'Helper properties for reading configuration properties    
    '----------------------------
    'returns string configuration property
    Public Shared ReadOnly Property StringSetting(ByVal name As String, Optional ByVal defValue As String = Nothing) As String
        Get
            If Not System.Configuration.ConfigurationManager.AppSettings.GetValues(name) Is Nothing Then
                Return System.Configuration.ConfigurationManager.AppSettings(name)
            ElseIf Not defValue Is Nothing Then
                Return defValue
            Else
                Return ""
            End If
        End Get
    End Property

    'returns boolean configuration property
    Public Shared ReadOnly Property BoolSetting(ByVal name As String, Optional ByVal defValue As Boolean = False) As Boolean
        Get
            If Not System.Configuration.ConfigurationManager.AppSettings.GetValues(name) Is Nothing Then
                If System.Configuration.ConfigurationManager.AppSettings(name).ToLower = "true" Then
                    Return True
                End If
            Else
                Return defValue
            End If
            Return False
        End Get
    End Property

    'returns string configuration property
    Public Shared ReadOnly Property IntSetting(ByVal name As String, Optional ByVal defValue As Integer = 0) As Integer
        Get
            If Not System.Configuration.ConfigurationManager.AppSettings.GetValues(name) Is Nothing Then
                Try
                    Return CInt(System.Configuration.ConfigurationManager.AppSettings(name))
                Catch ex As Exception
                    Return defValue
                End Try
            End If
            Return defValue
        End Get
    End Property
    
    '-----------------------------------------------------------
    'Global application settings properies
    '-----------------------------------------------------------

    ' database connection
    Shared m_ConnectionString As String = Nothing
    Shared m_DefConnectionString As String = "data source=(local);initial catalog=enterprise;user id=enterprise;password=entx!2003n;persist security info=False;packet size=4096"
    Public Shared ReadOnly Property ConnectionString() As String
        Get
            If m_ConnectionString Is Nothing Then
                m_ConnectionString = StringSetting("ConnectionString", m_DefConnectionString)
            End If
            Return m_ConnectionString
        End Get
    End Property

    
    Private Shared m_CompanyID As String = Nothing
    Public Shared ReadOnly Property DefaultCompany() As String
        Get
            If m_CompanyID Is Nothing Then
                m_CompanyID = StringSetting("Company", "DEFAULT")
            End If
            Return m_CompanyID
        End Get
    End Property
    
    Private Shared m_DivisionID As String = Nothing
    Public Shared ReadOnly Property DefaultDivision() As String
        Get
            If m_DivisionID Is Nothing Then
                m_DivisionID = StringSetting("Division", "DEFAULT")
            End If
            Return m_DivisionID
        End Get
    End Property

    Private Shared m_DepartmentID As String = Nothing
    Public Shared ReadOnly Property DefaultDepartment() As String
        Get
            If m_DepartmentID Is Nothing Then
                m_DepartmentID = StringSetting("Department", "DEFAULT")
            End If
            Return m_DepartmentID
        End Get
    End Property
    Private Shared m_CurrencyID As String = Nothing
    Public Shared ReadOnly Property DefaultCurrency() As String
        Get
            If m_CurrencyID Is Nothing Then
                m_CurrencyID = StringSetting("Currency", "USD")
            End If
            Return m_CurrencyID
        End Get
    End Property
    Private Shared m_Skin As String = Nothing
    Public Shared ReadOnly Property DefaultSkin() As String
        Get
            If m_Skin Is Nothing Then
                m_Skin = StringSetting("Skin", "DEFAULT")
            End If
            Return m_Skin
        End Get
    End Property
    Private Shared m_Language As String = Nothing
    Public Shared ReadOnly Property DefaultLanguage() As String
        Get
            If m_Language Is Nothing Then
                m_Language = StringSetting("Language", "English")
            End If
            Return m_Language
        End Get
    End Property
    ' smtp server
    Shared m_SmtpServer As String = Nothing
    Public Shared ReadOnly Property SmtpServer() As String
        Get
            If m_SmtpServer Is Nothing Then
                m_SmtpServer = StringSetting("SmtpServer")
            End If
            Return m_SmtpServer
        End Get
    End Property

    ' set to True to enable e-mail sending
    Public Shared ReadOnly Property EnableEmail() As Boolean
        Get
            Return BoolSetting("EnableEmail")
        End Get
    End Property
    
    ' admin email
    Private Shared m_AdminEmail As String = Nothing
    Public Shared ReadOnly Property AdminEmail() As String
        Get
            If m_AdminEmail Is Nothing Then
                m_AdminEmail = StringSetting("AdminEmail", "support@sunflowertechnologies.com")
            End If
            Return m_AdminEmail
        End Get
    End Property
    
   
    ' ecart email (appears in From: field of error notification emails)
    Private Shared m_EcartEmail As String = Nothing
    Public Shared ReadOnly Property EcartEmail() As String
        Get
            If m_EcartEmail Is Nothing Then
                m_EcartEmail = StringSetting("EcartEmail", "support@sunflowertechnologies.com")
            End If
            Return m_EcartEmail
        End Get
    End Property
    
       
    ' sales dept. email
    Public Shared m_SalesEmail As String = Nothing
    Public Shared ReadOnly Property SalesEmail() As String
        Get
            If m_SalesEmail Is Nothing Then
                m_SalesEmail = StringSetting("SalesEmail", "support@sunflowertechnologies.com")
            End If
            Return m_SalesEmail
        End Get
    End Property
    
    'Error log file path
    Private Shared m_logPath As String = Nothing
    Public Shared ReadOnly Property LogPath() As String
        Get
            If m_logPath Is Nothing Then
                m_logPath = StringSetting("LogPath", "C:\inetpub\wwwroot\EnterpriseCart\StfbCart.log")
            End If
            Return m_logPath
        End Get
    End Property
    '---------------------------------------------  
    'Activity or Trace log path   
    Private Shared m_ActivityLogPath As String = Nothing
    Public Shared ReadOnly Property ActivityLogPath() As String
        Get
            If m_ActivityLogPath Is Nothing Then
                m_ActivityLogPath = StringSetting("ActivityLogPath", "C:\inetpub\wwwroot\EnterpriseCart\StfbCartTrace.log")
            End If
            Return m_ActivityLogPath
        End Get
    End Property

    Public Shared ReadOnly Property WriteActivityLog() As Boolean
        Get
            Return BoolSetting("WriteActivityLog")
        End Get
    End Property
    '----------------------------------------------
    ' session timeout in minutes
    Public Shared ReadOnly Property SessionLife() As Integer
        Get
            Return IntSetting("SessionLife", 999)
        End Get
    End Property
 
    ' cart table cookie format version - change when modifying CartTable structure
    Private Shared cartTableVersion As String = "CART20030924-1"
    
    ' random number generator
    Private Shared rnd As New Random()

    '---------------------------------------------    
    'Trace log path 
    'use to write login information in specific path
    Public Shared blnLogInfo As Boolean = True
    '----------------------------------------------
    'Use For Terminal Validation 
    'To use Random TerminalID or TerminalID
    
    Public Shared blnTerminalInfo As Boolean = False
    '----------------------------------------------

    '///////////////////////////////////
     
    'Use To POST AN ORDER Automatically    
    Public Shared blnAutomaticPosting As Boolean = True

    '/////////////////////////////////////////

    
    'Call this method to use single connection instance
    Shared Function Connection() As SqlConnection
        Dim context As HttpContext = HttpContext.Current
        Dim myConnection As SqlConnection = context.Items("SqlConnection")
        If Not myConnection Is Nothing Then
            If myConnection.State = ConnectionState.Open Then
                Return myConnection
            End If
        End If
        myConnection = New SqlConnection(ConnectionString)
        myConnection.Open()
        context.Items("SqlConnection") = myConnection
        Return myConnection
    End Function
    
    Shared Sub ResetConnection()
        Dim context As HttpContext = HttpContext.Current
        context.Items("SqlConnection") = Nothing
    End Sub
    
    ' This method closes DataReader
    Shared Sub CloseDataReader(ByVal reader As SqlDataReader)
        If reader Is Nothing Then Return
        If reader.IsClosed Then Return
        reader.Close()
        
    End Sub
    
    ' get a new datareader for sql command, closing previous reader
    Shared Function ExecuteReader(ByVal Command As SqlCommand, _
    Optional ByVal behavior As CommandBehavior = CommandBehavior.Default) As SqlDataReader
        ' close active reader
        Dim context As HttpContext = HttpContext.Current
        Dim reader As SqlDataReader = context.Items("SqlDataReader")
        If Not (reader Is Nothing) Then
            If Not reader.IsClosed Then
                reader.Close()
            End If
            ' reset SqlDataReader (necessary due to possible exceptions)
            context.Items("SqlDataReader") = Nothing
        End If

        If Command.Connection.State = ConnectionState.Closed Then
            Command.Connection.Open()
        End If

        ' execute command
        reader = Command.ExecuteReader(behavior)
        ' save new reader
        context.Items("SqlDataReader") = reader
        Return reader
    End Function
     
    Shared Function ExecuteScalar(ByVal Command As SqlCommand) As Object
        ' close active reader
        Dim context As HttpContext = HttpContext.Current
        Dim reader As SqlDataReader = context.Items("SqlDataReader")
        If Not (reader Is Nothing) Then
            If Not reader.IsClosed Then
                reader.Close()

            End If
            ' reset SqlDataReader
            context.Items("SqlDataReader") = Nothing
        End If
        ' execute command
        If Command.Connection.State = ConnectionState.Closed Then
            Command.Connection.Open()
        End If
        Return Command.ExecuteScalar()
    End Function
    
    Public Shared CompanyCond As String = _
        "CompanyID = @CompanyID AND DivisionID = @DivisionID AND DepartmentID = @DepartmentID"
    
    Shared Function getValue(ByVal reader As SqlDataReader, ByVal field As String) As Object
        ' return reader field value or empty string if it is null
        Dim v As Object = reader(field)
        If IsDBNull(v) Then
            Return ""
        Else
            Return v
        End If
    End Function
    
    Shared Function getBoolValue(ByVal reader As SqlDataReader, ByVal field As String) As Boolean
        ' return reader field value or empty string if it is null
        Dim v As Object = reader(field)
        If IsDBNull(v) Then
            Return False
        Else
            Return CType(v, Boolean)
        End If
    End Function

    Shared Sub SetSelectedValue(ByVal dd As DropDownList, ByVal Value As Object)
        Dim li As ListItem = dd.Items.FindByValue(Value)
        If Not li Is Nothing Then
            dd.SelectedIndex = -1
            li.Selected = True
        End If
    End Sub
    
    Shared Sub SetSelectedText(ByVal dd As DropDownList, ByVal Text As Object)
        Dim li As ListItem = dd.Items.FindByText(Text)
        If Not li Is Nothing Then
            dd.SelectedIndex = -1
            li.Selected = True
        End If
    End Sub
    
    Shared Sub SetCountryList(ByVal dd As DropDownList)
        dd.Items.Clear()
        Dim CountryTable As Hashtable = GetCountries()
        Dim CountryNames(CountryTable.Count - 1) As String
        CountryTable.Keys.CopyTo(CountryNames, 0)
        Array.Sort(CountryNames)
        Dim k As String
        For Each k In CountryNames
            dd.Items.Add(New ListItem(k, CountryTable(k)))
        Next
        SetSelectedValue(dd, "US")
    End Sub
    
    Shared Sub Debug(ByVal format As String, ByVal ParamArray args() As Object)
        System.Diagnostics.Debug.WriteLine(String.Format(format, args))
    End Sub
    
    Shared Function GetCountryCode(ByVal Country As String) As String
        If Country Is Nothing OrElse Country = "" Then Return ""
        Dim CountryTable As Hashtable = GetCountries()
        Dim result As String = CountryTable(Country)
        If result Is Nothing Then
            result = Country
        End If
        Debug("returning '{0}' for '{1}'", result, Country)
        Return result
    End Function
    
    Private Shared Countries As Hashtable
    
    ' retrieve Countries hashtable (Country name -> 2-letter code)
    Shared Function GetCountries() As Hashtable
        If Countries Is Nothing Then
            Countries = New Hashtable()
            Countries("Albania") = "AL"
            Countries("American Samoa") = "AS"
            Countries("Andorra") = "AD"
            Countries("Angola") = "AO"
            Countries("Anguilla") = "AI"
            Countries("Antigua/Barbuda") = "AG"
            Countries("Argentina") = "AR"
            Countries("Armenia") = "AM"
            Countries("Aruba") = "AW"
            Countries("Australia") = "AU"
            Countries("Austria") = "AT"
            Countries("Azerbaijan") = "AZ"
            Countries("Bahamas") = "BS"
            Countries("Bahrain") = "BH"
            Countries("Bangladesh") = "BD"
            Countries("Barbados") = "BB"
            Countries("Belarus") = "BY"
            Countries("Belgium") = "BE"
            Countries("Belize") = "BZ"
            Countries("Benin") = "BJ"
            Countries("Bermuda") = "BM"
            Countries("Bhutan") = "BT"
            Countries("Bolivia") = "BO"
            Countries("Bosnia-Herzegovina") = "BA"
            Countries("Botswana") = "BW"
            Countries("Brazil") = "BR"
            Countries("British Virgin Islands") = "VG"
            Countries("Brunei") = "BN"
            Countries("Bulgaria") = "BG"
            Countries("Burkina Faso") = "BF"
            Countries("Burundi") = "BI"
            Countries("Cambodia") = "KH"
            Countries("Cameroon") = "CM"
            Countries("Canada") = "CA"
            Countries("Cape Verde") = "CV"
            Countries("Cayman Islands") = "KY"
            Countries("Chad") = "TD"
            Countries("Chile") = "CL"
            Countries("China") = "CN"
            Countries("Colombia") = "CO"
            Countries("Congo Brazzaville") = "CG"
            Countries("Congo Democratic Rep. of") = "CD"
            Countries("Cook Islands") = "CK"
            Countries("Costa Rica") = "CR"
            Countries("Croatia") = "HR"
            Countries("Cyprus") = "CY"
            Countries("Czech Republic") = "CZ"
            Countries("Denmark") = "DK"
            Countries("Djibouti") = "DJ"
            Countries("Dominica") = "DM"
            Countries("Dominican Republic") = "DO"
            Countries("Ecuador") = "EC"
            Countries("Egypt") = "EG"
            Countries("El Salvador") = "SV"
            Countries("Equatorial Guinea") = "GQ"
            Countries("Eritrea") = "ER"
            Countries("Estonia") = "EE"
            Countries("Ethiopia") = "ET"
            Countries("Faeroe Islands") = "FO"
            Countries("Fiji") = "FJ"
            Countries("Finland") = "FI"
            Countries("France") = "FR"
            Countries("French Guiana") = "GF"
            Countries("French Polynesia") = "PF"
            Countries("Gabon") = "GA"
            Countries("Gambia") = "GM"
            Countries("Georgia") = "GE"
            Countries("Germany") = "DE"
            Countries("Ghana") = "GH"
            Countries("Gibraltar") = "GI"
            Countries("Greece") = "GR"
            Countries("Greenland") = "GL"
            Countries("Grenada") = "GD"
            Countries("Guadeloupe") = "GP"
            Countries("Guam") = "GU"
            Countries("Guatemala") = "GT"
            Countries("Guinea") = "GN"
            Countries("Guyana") = "GY"
            Countries("Haiti") = "HT"
            Countries("Honduras") = "HN"
            Countries("Hong Kong") = "HK"
            Countries("Hungary") = "HU"
            Countries("Iceland") = "IS"
            Countries("India") = "IN"
            Countries("Indonesia") = "ID"
            Countries("Ireland") = "IE"
            Countries("Israel") = "IL"
            Countries("Italy/Vatican City") = "IT"
            Countries("Ivory Coast") = "CI"
            Countries("Jamaica") = "JM"
            Countries("Japan") = "JP"
            Countries("Jordan") = "JO"
            Countries("Kazakhstan") = "KZ"
            Countries("Kenya") = "KE"
            Countries("Kuwait") = "KW"
            Countries("Kyrgyzstan") = "KG"
            Countries("Laos") = "LA"
            Countries("Latvia") = "LV"
            Countries("Lebanon") = "LB"
            Countries("Lesotho") = "LS"
            Countries("Liberia") = "LR"
            Countries("Liechtenstein") = "LI"
            Countries("Lithuania") = "LT"
            Countries("Luxembourg") = "LU"
            Countries("Macau") = "MO"
            Countries("Macedonia") = "MK"
            Countries("Malawi") = "MW"
            Countries("Malaysia") = "MY"
            Countries("Maldives") = "MV"
            Countries("Mali") = "ML"
            Countries("Malta") = "MT"
            Countries("Marshall Islands") = "MH"
            Countries("Martinique") = "MQ"
            Countries("Mauritania") = "MR"
            Countries("Mauritius") = "MU"
            Countries("Mexico") = "MX"
            Countries("Micronesia") = "FM"
            Countries("Moldova") = "MD"
            Countries("Monaco") = "MC"
            Countries("Mongolia") = "MN"
            Countries("Montserrat") = "MS"
            Countries("Morocco") = "MA"
            Countries("Mozambique") = "MZ"
            Countries("Namibia") = "NA"
            Countries("Nepal") = "NP"
            Countries("Netherlands") = "NL"
            Countries("Netherlands Antilles") = "AN"
            Countries("New Caledonia") = "NC"
            Countries("New Zealand") = "NZ"
            Countries("Nicaragua") = "NI"
            Countries("Niger") = "NE"
            Countries("Nigeria") = "NG"
            Countries("Norway") = "NO"
            Countries("Oman") = "OM"
            Countries("Pakistan") = "PK"
            Countries("Palau") = "PW"
            Countries("Palestine Autonomous") = "PS"
            Countries("Panama") = "PA"
            Countries("Papua New Guinea") = "PG"
            Countries("Paraguay") = "PY"
            Countries("Peru") = "PE"
            Countries("Philippines") = "PH"
            Countries("Poland") = "PL"
            Countries("Portugal") = "PT"
            Countries("Puerto Rico") = "PR"
            Countries("Qatar") = "QA"
            Countries("Reunion") = "RE"
            Countries("Romania") = "RO"
            Countries("Russian Federation") = "RU"
            Countries("Rwanda") = "RW"
            Countries("Saipan") = "MP"
            Countries("Saudi Arabia") = "SA"
            Countries("Senegal") = "SN"
            Countries("Seychelles") = "SC"
            Countries("Singapore") = "SG"
            Countries("Slovak Republic") = "SK"
            Countries("Slovenia") = "SI"
            Countries("South Africa") = "ZA"
            Countries("South Korea") = "KR"
            Countries("Spain") = "ES"
            Countries("Sri Lanka") = "LK"
            Countries("St. Kitts/Nevis") = "KN"
            Countries("St. Lucia") = "LC"
            Countries("St. Vincent") = "VC"
            Countries("Suriname") = "SR"
            Countries("Swaziland") = "SZ"
            Countries("Sweden") = "SE"
            Countries("Switzerland") = "CH"
            Countries("Syria") = "SY"
            Countries("Taiwan") = "TW"
            Countries("Tanzania") = "TZ"
            Countries("Thailand") = "TH"
            Countries("Togo") = "TG"
            Countries("Trinidad/Tobago") = "TT"
            Countries("Tunisia") = "TN"
            Countries("Turkey") = "TR"
            Countries("Turkmenistan") = "TM"
            Countries("Turks & Caicos Islands") = "TC"
            Countries("U.S. Virgin Islands") = "VI"
            Countries("Uganda") = "UG"
            Countries("Ukraine") = "UA"
            Countries("United Arab Emirates") = "AE"
            Countries("United Kingdom") = "GB"
            Countries("United States") = "US"
            Countries("Uruguay") = "UY"
            Countries("Uzbekistan") = "UZ"
            Countries("Vanuatu") = "VU"
            Countries("Venezuela") = "VE"
            Countries("Vietnam") = "VN"
            Countries("Wallis & Futuna") = "WF"
            Countries("Yemen") = "YE"
            Countries("Yugoslavia") = "YU"
            Countries("Zambia") = "ZM"
            Countries("Zimbabwe") = "ZW"
        End If
        Return Countries
    End Function
    
    'Send e-mail message
    Shared Sub SendEmail(ByVal MailFrom As String, ByVal MailTo As String, ByVal Subject As String, ByVal Body As String, Optional ByVal Html As Boolean = True)
        If Not EnableEmail OrElse NothingOrEmpty(SmtpServer) OrElse NothingOrEmpty(MailTo) Then
            Return
        End If
        Try
            Dim mailMessage As System.Web.Mail.MailMessage = New System.Web.Mail.MailMessage()
            mailMessage.From = MailFrom
            mailMessage.To = MailTo
            mailMessage.Subject = Subject
            If Html Then
                mailMessage.BodyFormat = System.Web.Mail.MailFormat.Html
            Else
                mailMessage.BodyFormat = System.Web.Mail.MailFormat.Text
            End If
            mailMessage.Body = Body
            System.Web.Mail.SmtpMail.SmtpServer = Common.SmtpServer
            System.Web.Mail.SmtpMail.Send(mailMessage)
        Catch ex As Exception
            HandleError("Error sending mail", ex, False)
        End Try
    End Sub
    
    ' make a nice looking message from an exception
    Public Shared Function GetErrorText(ByVal ex As Exception, Optional ByVal HtmlFormat As Boolean = False)
        'Shared method that built error message text from exception.
        'Override this method if you need display error in some other manner
        Dim errorMsg As StringWriter = New StringWriter()
        errorMsg.WriteLine("An error occurred please contact the adminstrator with the following information:")
        If HtmlFormat Then
            errorMsg.WriteLine("<br />")
        Else
            errorMsg.WriteLine("")
        End If
        errorMsg.WriteLine(ex.Message)
        If HtmlFormat Then
            errorMsg.WriteLine("<br />")
        Else
            errorMsg.WriteLine("")
        End If
        errorMsg.WriteLine("Stack Trace:")
        If HtmlFormat Then
            errorMsg.WriteLine("<br />")
        End If
        If HtmlFormat Then
            errorMsg.WriteLine(ex.StackTrace.Replace(vbCrLf, "<br />"))
        Else
            errorMsg.WriteLine(ex.StackTrace)
        End If
        'This is especially for email sending errors
        'If error message was not send via email, the reason will be writen to the error lof file
        If Not ex.InnerException Is Nothing Then
            If HtmlFormat Then
                errorMsg.WriteLine("<br />")
            End If
            errorMsg.WriteLine("Inner Exception:")
            errorMsg.WriteLine(ex.InnerException.InnerException.Message)
            If HtmlFormat Then
                errorMsg.WriteLine("<br />")
            End If
            If Not ex.InnerException.InnerException Is Nothing Then
                If HtmlFormat Then
                    errorMsg.WriteLine("<br />")
                End If
                errorMsg.WriteLine("Inner Exception:")
                If HtmlFormat Then
                    errorMsg.WriteLine("<br />")
                End If
                errorMsg.WriteLine(ex.InnerException.InnerException.Message)
            End If
        End If
        Return errorMsg.ToString()
    End Function
    
    Public Shared Sub WriteErrorLog(ByVal errMsg As String)
        'Writes error message to the error log file

        Dim w As StreamWriter = Nothing
        Try
            If NothingOrEmpty(LogPath) Then
                Return
            End If
            Dim logFile As New FileInfo(LogPath)
            If (logFile.Exists) Then
                'Rotate log
                If (logFile.Length >= 100000) Then
                    Dim I As Integer = 1
                    While File.Exists(LogPath & "." & I)
                        I += 1
                    End While
                    While I > 1
                        Dim J As Integer = I - 1
                        Try
                            File.Move(LogPath & "." & J, LogPath & "." & I)
                        Catch
                            '...
                        End Try
                        I = J
                    End While
                    File.Move(LogPath, LogPath & ".1")
                End If
            End If
            Dim fs As FileStream = New FileStream(LogPath, _
                    FileMode.OpenOrCreate, FileAccess.ReadWrite)
            w = New StreamWriter(fs)  '  create a Char writer
            w.BaseStream.Seek(0, SeekOrigin.End)          '  set the file pointer to the end
            w.Write(Chr(13) & "Log Entry : ")
            w.Write("{0} {1} " & Chr(13) & Chr(13), DateTime.Now.ToLongTimeString(), _
                    DateTime.Now.ToLongDateString())
            w.Write(errMsg & Chr(13))
            w.Write("------------------------------------" & Chr(13))
            w.Flush()                              '  update underlying file
           
        Finally
            If Not w Is Nothing Then
                w.Close()                              '  close the writer and underlying file               
            End If
        End Try
    End Sub
    
    ' handle error:
    ' logMessage    - message to be written to log and sent to admin
    ' errorPage     - page to redirect user to
    ' ex            - exception that was caused by the error
    ' notifyByEmail - [optional, default=True] True to notify admin by e-mail
    Shared Sub HandleError(ByVal logMessage As String, ByVal errorPage As String, Optional ByVal ex As Exception = Nothing, Optional ByVal notifyByEmail As Boolean = True)
        ' we do not want to 'handle' redirect
        If TypeOf (ex) Is Threading.ThreadAbortException Then
            Throw ex
        End If
        Try
            If Not logMessage Is Nothing Or Not ex Is Nothing Then
                Dim msg As String = ""
                If Not logMessage Is Nothing Then
                    msg = logMessage & vbCrLf
                End If
                If Not ex Is Nothing Then
                    msg &= GetErrorText(ex)
                End If
                WriteErrorLog(msg)
                If notifyByEmail Then
                    SendEmail(EcartEmail, AdminEmail, "Error information", msg, False)
                End If
                Debug("*** ERROR ***")
                Debug(msg)
                Debug("*************")
            End If
            If Not errorPage Is Nothing Then
                HttpContext.Current.Session("LastError") = GetErrorText(ex, True)
                Redirect(errorPage & ".aspx")
            End If
        Catch InternalEx As Exception
            If TypeOf (InternalEx) Is Threading.ThreadAbortException Then
                Throw InternalEx
            Else
                Debug("Error in HandleError: {0}", InternalEx)
            End If
        End Try
    End Sub
    
    ' a convenience overload
    Public Shared Sub HandleError(ByVal logMessage As String, Optional ByVal ex As Exception = Nothing, Optional ByVal notifyByEmail As Boolean = True)
        HandleError(logMessage, Nothing, ex, notifyByEmail)
    End Sub
    
    ' check session validity
    Private Shared Function IsSessionValid(ByVal MandatoryRequestVars As Boolean)
        Dim Context As HttpContext = HttpContext.Current
        Dim Request As HttpRequest = Context.Request
        Dim Session As HttpSessionState = Context.Session
    
        If Session("EcartSessionStarted") Is Nothing Then
            Return False
        End If
    
        Dim checked() As String = {"CompanyID", "DepartmentID", "DivisionID"}
        Dim k As String
        For Each k In checked
            Dim s = Session(k)
            If s Is Nothing Then
                Debug("need to start new session: {0} is nothing", k)
                Return False
            End If
            Dim r As String = Request.QueryString(k)
            If r Is Nothing And MandatoryRequestVars Then
                r = "DEFAULT"
            End If
            If Not r Is Nothing Then
                If r <> s Then
                    Debug("need to start new session: {0}: [req]{1} <> [session]{2}", k, r, s)
                    Return False
                End If
            End If
        Next
    
        Return True
    End Function
    
    ' create a cart table
    Public Shared Function CreateCartTable() As DataTable
        Dim CartTable As New DataTable()
        ' add columns to our table - very easy to expand in the future if need be
        ' column 0
        CartTable.Columns.Add(New DataColumn("ItemID", GetType(String)))
        ' column 1
        CartTable.Columns.Add(New DataColumn("ItemName", GetType(String)))
        ' column 2
        CartTable.Columns.Add(New DataColumn("ItemDescription", GetType(String)))
        ' column 3
        CartTable.Columns.Add(New DataColumn("Quantity", GetType(Double)))
        ' column 4
        CartTable.Columns.Add(New DataColumn("Price", GetType(Double)))
        ' column 5
        CartTable.Columns.Add(New DataColumn("CurrencyID", GetType(String)))
        ' column 6
        CartTable.Columns.Add(New DataColumn("WarehouseID", GetType(String)))
        ' column 7
        CartTable.Columns.Add(New DataColumn("ItemWeight", GetType(Integer)))
        ' column 8
        CartTable.Columns.Add(New DataColumn("Taxable", GetType(Boolean)))
        ' column 9
        CartTable.Columns.Add(New DataColumn("Remove", GetType(Boolean)))
        'Column 10
        CartTable.Columns.Add(New DataColumn("TaxGroupID", GetType(String)))
        'Column 11
        CartTable.Columns.Add(New DataColumn("PictureURL", GetType(String)))
        'Column 12 
        CartTable.Columns.Add(New DataColumn("ItemTaxPercent", GetType(Double)))
        'Column 13
        CartTable.Columns.Add(New DataColumn("TaxAmount", GetType(Double)))
        'Column 14
        CartTable.Columns.Add(New DataColumn("ItemTotalAmount", GetType(Double)))
        'Column 15
        CartTable.Columns.Add(New DataColumn("ItemCost", GetType(Double)))
        'Column 16
        CartTable.Columns.Add(New DataColumn("SalesTaxGroupID", GetType(String)))
        'Column 17
        CartTable.Columns.Add(New DataColumn("SalesTaxPercent", GetType(Double)))

        Return CartTable
    End Function
    
	
    ' convert char to its code
    Private Shared Function EscapeChar(ByVal m As Match) As String
        Return "-" & Asc(m.ToString()).ToString("X2")
    End Function
    
    ' convert escape code to char
    Private Shared Function UnescapeChar(ByVal m As Match) As String
        Return Chr(Convert.ToInt32(m.ToString().Substring(1), 16))
    End Function
    
    ' escape string
    Public Shared Function Escape(ByVal s As String) As String
        Return Regex.Replace(s, "[^A-Za-z0-9]", AddressOf EscapeChar)
    End Function
    
    ' unescape string
    Public Shared Function Unescape(ByVal s As String) As String
        Return Regex.Replace(s, "-[0-9A-Za-z]{2}", AddressOf UnescapeChar)
    End Function
    
    ' join func for ArrayList
    Public Shared Function Join(ByVal sep As String, ByVal l As ArrayList)
        Dim a() As String = l.ToArray(GetType(String))
        Return String.Join(sep, a)
    End Function
    
    ' convert cart table to string
    Public Shared Function CartTableToString(ByVal CartTable As DataTable) As String
        Dim row As DataRow
        Dim rowText As New ArrayList()
        rowText.Add(cartTableVersion)
        For Each row In CartTable.Rows
            Dim v As Object
            Dim itemText As New ArrayList()
    
            For Each v In row.ItemArray
                Dim s = "-"
                If IsDBNull(v) Then
                    s = "0"
                ElseIf Not v Is Nothing Then
                    s = "*" & Escape(v.ToString())
                End If
                itemText.Add(s)
            Next
            rowText.Add(Join(".", itemText))
        Next
        Dim CartTableStr As String = Join("|", rowText)
        Debug("CartTableToString: returning {0}", CartTableStr)
        Return CartTableStr
    End Function
    
    ' get cart table from string representation
    Public Shared Function CartTableFromString(ByVal CartTableStr As String) As DataTable
        Debug("CartTableFromString: {0}", CartTableStr)
        Dim rowText() As String = CartTableStr.Split("|")
        If rowText.Length < 2 Then
            Debug("CartTableFromString: no data")
            Return Nothing
        End If
        If rowText(0) <> cartTableVersion Then
            Debug("CartTableFromString: bad cart table version")
            Return Nothing
        End If
        Dim CartTable As DataTable = CreateCartTable()
        Dim I As Integer, J As Integer
        For I = 1 To rowText.Length - 1
            Dim row As DataRow = CartTable.NewRow()
            Dim items() As String = rowText(I).Split(".")
            For J = 0 To items.Length - 1
                Dim item = items(J)
                Try
                    Dim c As String = item.SubString(0, 1)
                    If c = "-" Then
                        row(J) = Nothing
                    ElseIf c = "0" Then
                        row(J) = DBNull.Value
                    ElseIf c = "*" Then
                        row(J) = Unescape(item.SubString(1))
                    Else
                        Debug("CartTableFromString: invalid value type")
                        Return Nothing
                    End If
                Catch ex As Exception
                    Debug("CartTableFromString: invalid cart table data [{0}, {1}]: '{2}'", I, J, item)
                    Return Nothing
                End Try
            Next
            CartTable.Rows.Add(row)
        Next
        Return CartTable
    End Function
    

    Private Shared Function DoCheckSession(ByVal MandatoryRequestVars As Boolean) As Boolean
        ' check/restore session; return True if session is OK, False if session was restarted
        Dim Context As HttpContext = HttpContext.Current
        Dim Request As HttpRequest = Context.Request
        Dim Response As HttpResponse = Context.Response
        Dim Session As HttpSessionState = Context.Session
        If Not IsSessionValid(MandatoryRequestVars) Then
            ' get company/department/division/employee info from Request
            Debug("starting new session")
            Session.Timeout = SessionLife
            Session("EcartSessionStarted") = 1
    
            ' check for a company id, if so then display the cart for that company, if not then
            ' display cart for default company
           
            
            Dim CompanyID As String = Request.QueryString("CompanyID")
            
            
            If NothingOrEmpty(CompanyID) Then
                CompanyID = DefaultCompany
            End If
            Session("CompanyID") = CompanyID
    
            ' check for a division id, if so then display the cart for that company, if not then
            ' display cart for default company
            Dim DivisionID As String = Request.QueryString("DivisionID")
            If NothingOrEmpty(DivisionID) Then
                DivisionID = DefaultDivision
            End If
            Session("DivisionID") = DivisionID
    
            ' check for a department id, if so then display the cart for that company, if not then
            ' display cart for default company
            Dim DepartmentID As String = Request.QueryString("DepartmentID")
            If NothingOrEmpty(DepartmentID) Then
                DepartmentID = DefaultDepartment
            End If
            Session("DepartmentID") = DepartmentID
    
            ' check for a referal to this page, if so, then store their ID so
            ' that if a customer places an order, then will get proper credit
            Dim EmployeeID As String = Session("EmployeeID")
            If NothingOrEmpty(EmployeeID) Then
                EmployeeID = ""
            End If
            Session("EmployeeID") = EmployeeID
            Session("CartTable") = CartTable
            Return False
        Else
            ' get company info to generate cookie name
            Session("CartTable") = CartTable
            Return True
        End If
    End Function
    
    ' check/restore current session; return True if session is OK, False if session was restarted
    Public Shared Function CheckSession(Optional ByVal MandatoryRequestVars As Boolean = False)
        Try
            Return DoCheckSession(MandatoryRequestVars)
        Catch ex As Exception
            HandleError("Database error", "DatabaseError", ex)
            Return False ' never get here
        End Try
    End Function
    
    ' retrieves company/div/dept/employee info from session
    Public Shared Sub GetCompanyInfo(ByRef CompanyID As String, ByRef DivisionID As String, _
        ByRef DepartmentID As String, ByRef EmployeeID As String, _
    Optional ByVal NeedToCheckSession As Boolean = True)
        If NeedToCheckSession Then
            CheckSession()
        End If
        Dim Session As HttpSessionState = HttpContext.Current.Session
        
        CompanyID = Session("CompanyID")
        If NothingOrEmpty(CompanyID) Then
            CompanyID = DefaultCompany
            Session("CompanyID") = CompanyID
        End If
    
        DivisionID = Session("DivisionID")
        If NothingOrEmpty(DivisionID) Then
            DivisionID = DefaultDivision
            Session("DivisionID") = DivisionID
        End If
    
        DepartmentID = Session("DepartmentID")
        If NothingOrEmpty(DepartmentID) Then
            DepartmentID = DefaultDepartment
            Session("DepartmentID") = DepartmentID
        End If
    
        EmployeeID = Session("EmployeeID")
        If NothingOrEmpty(EmployeeID) Then
            EmployeeID = ""
            Session("EmployeeID") = EmployeeID
        End If
        If NothingOrEmpty(Session("CurrencyID")) Then
            ' Get Currencies
            Dim cn As SqlConnection = Connection()
            Try
                Dim myCommand As New SqlCommand("SELECT CurrencyID FROM Companies " & _
                 "WHERE CompanyID = @CompanyID AND DepartmentID = @DepartmentID AND DivisionID = @DivisionID", _
                cn)
                myCommand.Parameters.AddWithValue("@CompanyID", CompanyID)
                myCommand.Parameters.AddWithValue("@DivisionID", DivisionID)
                myCommand.Parameters.AddWithValue("@DepartmentID", DepartmentID)
                Dim r As Object = Common.ExecuteScalar(myCommand)
                If Not IsDBNull(r) Then
                    Session("CurrencyID") = CType(r, String)
                Else
                    Session("CurrencyID") = Common.DefaultCurrency
                End If
                myCommand = New SqlCommand("SELECT CurrencyID, CurrenycySymbol, CurrencyExchangeRate FROM CurrencyTypes " & _
                 "WHERE CompanyID = @CompanyID AND DepartmentID = @DepartmentID AND DivisionID = @DivisionID  and CurrencyID = @CurrencyID", _
                cn)
                myCommand.Parameters.AddWithValue("@CompanyID", CompanyID)
                myCommand.Parameters.AddWithValue("@DivisionID", DivisionID)
                myCommand.Parameters.AddWithValue("@DepartmentID", DepartmentID)
                myCommand.Parameters.AddWithValue("@CurrencyID", Session("CurrencyID"))
                Dim reader As SqlDataReader = Common.ExecuteReader(myCommand, CommandBehavior.CloseConnection)
                If reader.Read() Then
                    Session("CurrencySymbol") = Common.getValue(reader, "CurrenycySymbol")
                    Session("CurrencyExchangeRate") = Common.getValue(reader, "CurrencyExchangeRate")
                Else
                    Session("CurrencySymbol") = "$"
                    Session("CurrencyExchangeRate") = 1
                End If
                CloseDataReader(reader)
                cn.Close()
                
            Catch ex As Exception
                
            Finally
                cn.Close()
            End Try
        End If
        
    End Sub
    
    ' Add Company/Division/Department/Employee info to URL
    Public Shared Function ExtendURL(ByVal url As String, Optional ByVal needToCheckSession As Boolean = True)
    
        Dim CompanyID As String
        Dim DivisionID As String
        Dim DepartmentID As String
        Dim EmployeeID As String
        GetCompanyInfo(CompanyID, DivisionID, DepartmentID, EmployeeID)
    
        Dim glueChar = "?"
        If url.IndexOf("?") >= 0 Then
            glueChar = "&"
        End If
    
        Return url & glueChar & String.Format( _
            "CompanyID={0}&DivisionID={1}&DepartmentID={2}&EmployeeID={3}", _
            HttpUtility.UrlEncode(CompanyID), HttpUtility.UrlEncode(DivisionID), _
            HttpUtility.UrlEncode(DepartmentID), HttpUtility.UrlEncode(EmployeeID))
    End Function
    
    ' redirect user to specified page. Company/Div/Dept/Employee info is added to URL
    Public Shared Sub Redirect(ByVal url As String, Optional ByVal needToCheckSession As Boolean = True)
        HttpContext.Current.Response.Redirect(ExtendURL(url, needToCheckSession))
    End Sub
    
    ' returns True if s is Nothing or empty
    Public Shared Function NothingOrEmpty(ByVal s As String) As Boolean
        If s Is Nothing Then
            Return True
        End If
        Return s = ""
    End Function
    
    ' returns a random value
    Public Shared Function Random(ByVal minValue As Integer, ByVal maxValue As Integer)
        Return rnd.Next(minValue, maxValue)
    End Function

    Public Shared ReadOnly Property CartTable() As DataTable
        Get
            Dim Session As HttpSessionState = HttpContext.Current.Session
            Dim Request As HttpRequest = HttpContext.Current.Request
            Dim Response As HttpResponse = HttpContext.Current.Response
            Dim m_CartTable As DataTable = Session("CartTable")
            Dim CookieName As String = String.Format("CartTable-{0}-{1}-{2}", Session("CompanyID"), Session("DivisionID"), Session("DepartmentID"))
            If Not m_CartTable Is Nothing Then
                Dim resetCookie As Boolean = True
                If m_CartTable.Rows.Count > 0 Then
                    ' store cart table into the cookies
                    Response.Cookies(CookieName).Value = CartTableToString(m_CartTable)
                    Response.Cookies(CookieName).Expires = DateTime.Now.AddDays(100)
                    resetCookie = False
                    Debug("stored cart table in cookies")
                End If
                If resetCookie Then
                    Debug("resetting CartTable cookie")
                    Response.Cookies(CookieName).Value = ""
                    Response.Cookies(CookieName).Expires = DateTime.Now
                End If
            Else
                Dim CartTableCookie As HttpCookie = Request.Cookies(CookieName)
                If Not CartTableCookie Is Nothing Then
                    Debug("cookie is not nothing: {0}", CartTableCookie.Value)
                    Dim CartTableStr = CartTableCookie.Value
                    If Not NothingOrEmpty(CartTableStr) Then
                        Try
                            ' try to get the cart table from cookies
                            Debug("loading cart table from cookies")
                            m_CartTable = CartTableFromString(CartTableStr)
                        Catch ex As Exception
                            Debug("error loading cart table from cookies: " & ex.Message)
                        End Try
                    End If
                End If               
            End If
            If m_CartTable Is Nothing Then
                m_CartTable = CreateCartTable()               
            End If
            Session("CartTable") = m_CartTable
            Return m_CartTable
        End Get
    End Property

    Public Shared Sub WriteLog(ByVal LogText() As String)
        If WriteActivityLog = True Then
            Dim SWriter As StreamWriter = Nothing
            Try
                SWriter = File.AppendText(ActivityLogPath)
                For Each line As String In LogText
                    SWriter.WriteLine(line)
                Next
            Catch ex As Exception

            Finally
                If Not SWriter Is Nothing Then
                    SWriter.Close()
                End If
            End Try
        End If
    End Sub

    Public Shared Function JSEscape(ByVal text As String) As String
        Return text.Replace("\", "\\"). _
            Replace("'", "\'").Replace("<br>", "\n"). _
            Replace(vbCrLf, "\n"). _
            Replace(Chr(13), "\n"). _
            Replace(Chr(10), "\n")
    End Function

    Public Shared Sub AddOnLoadJSAction(ByVal aPage As System.Web.UI.Page, ByVal jsAction As String)
        Static id As Integer = 0
        aPage.ClientScript.RegisterClientScriptBlock(aPage.GetType(), "AlertScript", _
            "<script>var onloadActions = new Array(); " & _
            "window.onload = function ()" & _
            "{ for(var i = 0; i < onloadActions.length; ++i)" & _
            "(onloadActions[i])(); }" & _
            "<" & "/script>")
        aPage.ClientScript.RegisterClientScriptBlock(aPage.GetType(), id, _
            "<script>onloadActions[onloadActions.length] = function() {" & jsAction & "}<" & "/script>")
        id += 1
    End Sub

    Public Shared Sub Alert(ByVal aPage As System.Web.UI.Page, ByVal msg As String)
        AddOnLoadJSAction(aPage, String.Format("alert('{0}')", JSEscape(msg)))
    End Sub
    Public Shared Sub PrintPage(ByVal aPage As System.Web.UI.Page)
        AddOnLoadJSAction(aPage, "window.print();")
    End Sub

    Public Shared Sub AddParam(ByVal cmd As SqlCommand, ByVal paramName As String, ByVal paramValue As Object, Optional ByVal paramType As SqlDbType = SqlDbType.NVarChar, Optional ByVal paramSize As Integer = 0)
        If Not paramValue Is Nothing AndAlso Not paramValue Is DBNull.Value Then
            cmd.Parameters.AddWithValue(paramName, paramValue)
        Else
            If paramType = SqlDbType.NVarChar Then
                If paramSize = 0 Then
                    cmd.Parameters.Add(paramName, paramType, 1000).Value = DBNull.Value
                Else
                    cmd.Parameters.Add(paramName, paramType, paramSize).Value = DBNull.Value
                End If
            Else
                cmd.Parameters.Add(paramName, paramType).Value = DBNull.Value
            End If
        End If
    End Sub
    
    Public Shared ReadOnly Property DefaultOrderWarehouse() As String
        Get
            Dim Session As HttpSessionState = HttpContext.Current.Session
            If Session("DefaultOrderWarehouse") Is Nothing Then
                Dim myCommand As New SqlCommand("Select WarehouseID from OrderHeader " & _
                "WHERE  " & _
                "CompanyID = @CompanyID " & _
                "AND DivisionID = @DivisionID " & _
                "AND DepartmentID = @DepartmentID " & _
                "AND OrderNumber = 'DEFAULT' ", Common.Connection())
                myCommand.Parameters.AddWithValue("@CompanyID", Session("CompanyID"))
                myCommand.Parameters.AddWithValue("@DivisionID", Session("DivisionID"))
                myCommand.Parameters.AddWithValue("@DepartmentID", Session("DepartmentID"))
                Dim w As Object = myCommand.ExecuteScalar()
                If Not w Is Nothing Then
                    Session("DefaultOrderWarehouse") = w
                Else
                    Session("DefaultOrderWarehouse") = ""
                End If
            End If
            Return Session("DefaultOrderWarehouse")
        End Get
    End Property

    Private Shared Sub GetOrderDetailWarehouse()
        Dim Session As HttpSessionState = HttpContext.Current.Session
        Dim myCommand As New SqlCommand("Select Top 1 WarehouseID, WarehouseBinID from OrderDetail " & _
        "WHERE  " & _
        "CompanyID = @CompanyID " & _
        "AND DivisionID = @DivisionID " & _
        "AND DepartmentID = @DepartmentID " & _
        "AND OrderNumber = 'DEFAULT' ", Common.Connection())
        myCommand.Parameters.AddWithValue("@CompanyID", Session("CompanyID"))
        myCommand.Parameters.AddWithValue("@DivisionID", Session("DivisionID"))
        myCommand.Parameters.AddWithValue("@DepartmentID", Session("DepartmentID"))
        Dim reader As SqlDataReader = Nothing
        Try
            reader = Common.ExecuteReader(myCommand, CommandBehavior.CloseConnection)
            If reader.Read() Then
                Session("DefaultOrderDetailWarehouse") = Common.getValue(reader, "WarehouseID")
                Session("DefaultOrderDetailWarehouseBin") = Common.getValue(reader, "WarehouseBinID")
            Else
                Session("DefaultOrderDetailWarehouse") = ""
                Session("DefaultOrderDetailWarehouseBin") = ""
            End If
        Finally
            CloseDataReader(reader)
        End Try
                
    End Sub

    Public Shared ReadOnly Property DefaultOrderDetailWarehouse() As String
        Get
            Dim Session As HttpSessionState = HttpContext.Current.Session
            If Session("DefaultOrderDetailWarehouse") Is Nothing Then
                GetOrderDetailWarehouse()
            End If
            Return Session("DefaultOrderDetailWarehouse")
        End Get
    End Property
    Public Shared ReadOnly Property DefaultOrderDetailWarehouseBin() As String
        Get
            Dim Session As HttpSessionState = HttpContext.Current.Session
            If Session("DefaultOrderDetailWarehouseBin") Is Nothing Then
                GetOrderDetailWarehouse()
            End If
            Return Session("DefaultOrderDetailWarehouseBin")
        End Get
    End Property
    Public Shared ReadOnly Property DefaultCompanyWarehouse() As String
        Get
            Dim Session As HttpSessionState = HttpContext.Current.Session
            If Session("DefaultCompanyWarehouse") Is Nothing Then
                Dim myCommand As New SqlCommand("Select WarehouseID from Company " & _
                "WHERE  " & _
                "CompanyID = @CompanyID " & _
                "AND DivisionID = @DivisionID " & _
                "AND DepartmentID = @DepartmentID ", Common.Connection())
                myCommand.Parameters.AddWithValue("@CompanyID", Session("CompanyID"))
                myCommand.Parameters.AddWithValue("@DivisionID", Session("DivisionID"))
                myCommand.Parameters.AddWithValue("@DepartmentID", Session("DepartmentID"))
                Dim w As Object = myCommand.ExecuteScalar()
                If Not w Is Nothing Then
                    Session("DefaultCompanyWarehouse") = w
                Else
                    Session("DefaultCompanyWarehouse") = ""
                End If
            End If
            Return Session("DefaultCompanyWarehouse")
        End Get
    End Property
    
    Public Shared Function IsEmpty(ByVal o As Object) As Boolean
        Return o Is Nothing OrElse o Is DBNull.Value OrElse o.ToString().Trim = ""
    End Function
    
    Public Shared Function PopulateItemInfo(ByVal ItemID As String) As SqlDataReader
        Dim Session As HttpSessionState = HttpContext.Current.Session
        'Dim myCommand As New SqlCommand("enterprise.Inventory_PopulatePOSPrice", Common.Connection)
        'myCommand.CommandType = CommandType.StoredProcedure
        'myCommand.Parameters.AddWithValue("@CompanyID", Session("CompanyID"))
        'myCommand.Parameters.AddWithValue("@DivisionID", Session("DivisionID"))
        'myCommand.Parameters.AddWithValue("@DepartmentID", Session("DepartmentID"))
        'myCommand.Parameters.AddWithValue("@ItemID", ItemID)
        'if Session("CustomerID") is nothing then
        '   myCommand.Parameters.Add("@CustomerID", SqlDbType.NVarChar, 36)
        'else
        '   myCommand.Parameters.AddWithValue("@CustomerID", Session("CustomerID"))
        'end if
        'myCommand.Parameters.Add("@Price",SqlDbType.Money).Direction=ParameterDirection.Output
        'mycommand.ExecuteNonQuery()
        'Dim ItemPrice as Double = myCommand.Parameters("@Price").Value
        Dim myCommand As New SqlCommand( _
        "DECLARE @Price money; " & _
        "EXECUTE enterprise.Inventory_PopulatePOSPrice " & _
            "@CompanyID,@DivisionID,@DepartmentID,@ItemID,@CustomerID,@Price OUTPUT; " & _
        "SELECT " & _
        "@Price AS Price, " & _
        "InventoryItems.PictureURL, InventoryItems.ItemName, " & _
        "InventoryItems.ItemDescription, " & _
        "InventoryItems.ItemLongDescription, " & _
        "InventoryItems.ItemWeight, " & _
        "InventoryItems.ItemID, " & _
        "InventoryItems.ItemFamilyID, " & _
        "InventoryItems.ItemCategoryID, " & _
        "InventoryItems.Taxable, " & _
        "InventoryItems.TaxGroupId, " & _
        "(CASE Companies.DefaultInventoryCostingMethod " & _
        "WHEN N'F' THEN ISNULL(InventoryItems.FIFOCost,0) " & _
        "WHEN N'L' THEN ISNULL(InventoryItems.LIFOCost,0) " & _
        "WHEN N'A' THEN ISNULL(InventoryItems.AverageCost,0) " & _
        "ELSE 0 END) AS ItemCost " & _
        "FROM InventoryItems " & _
        "LEFT OUTER JOIN Companies ON " & _
        "        Companies.CompanyID = InventoryItems.CompanyID " & _
        "       AND Companies.DivisionID = InventoryItems.DivisionID " & _
        "       AND Companies.DepartmentID = InventoryItems.DepartmentID " & _
        "WHERE " & _
        "InventoryItems.CompanyID = @CompanyID " & _
        "and InventoryItems.DivisionID = @DivisionID " & _
        "and InventoryItems.DepartmentID = @DepartmentID  " & _
        "and InventoryItems.ItemID = @ItemID and IsActive = 1", Connection)

        myCommand.Parameters.AddWithValue("@CompanyID", Session("CompanyID"))
        myCommand.Parameters.AddWithValue("@DivisionID", Session("DivisionID"))
        myCommand.Parameters.AddWithValue("@DepartmentID", Session("DepartmentID"))
        myCommand.Parameters.AddWithValue("@ItemID", ItemID)
        If Session("CustomerID") Is Nothing Then
            myCommand.Parameters.Add("@CustomerID", SqlDbType.NVarChar, 36).Value = DBNull.Value
        Else
            myCommand.Parameters.AddWithValue("@CustomerID", Session("CustomerID"))
        End If
        myCommand.CommandTimeout = 200
        Dim reader As SqlDataReader = Common.ExecuteReader(myCommand, CommandBehavior.CloseConnection)
        Return reader
    End Function
    
    Public Shared Function GetTaxPercent(ByVal TaxGroupID As Object) As Double
        If TaxGroupID Is DBNull.Value OrElse TaxGroupID Is Nothing Then
            Return 0
        End If
        Dim Session As HttpSessionState = HttpContext.Current.Session
        Dim myCommand As New SqlCommand("enterprise.TaxGroup_GetTotalPercent", Connection)
        myCommand.CommandType = CommandType.StoredProcedure
        myCommand.Parameters.AddWithValue("@CompanyID", Session("CompanyID"))
        myCommand.Parameters.AddWithValue("@DivisionID", Session("DivisionID"))
        myCommand.Parameters.AddWithValue("@DepartmentID", Session("DepartmentID"))
        myCommand.Parameters.AddWithValue("@TaxGroupID", TaxGroupID)
        myCommand.Parameters.AddWithValue("@TotalPercent", 0.0).Direction = ParameterDirection.Output
        myCommand.ExecuteNonQuery()
        Dim TaxPercent As Object = myCommand.Parameters("@TotalPercent").Value
        If TaxPercent Is DBNull.Value OrElse TaxPercent Is Nothing Then
            Return 0
        Else
            Return CDbl(TaxPercent)
        End If
    End Function

    Public Shared Function GetCustomerSalesTaxPercent(Optional ByVal ForceQuery As Boolean = False) As Double
        Dim Session As HttpSessionState = HttpContext.Current.Session
        If IsEmpty(Session("CustomerID")) ORELSE Session("CustomerID")="CASH" ORELSE Session("CustomerID")="DEFAULT" Then
            Session("SalesTaxGroup") = Nothing
            Session("SalesTaxPercent") = 0
            Return 0
        ElseIf ForceQuery = False Then
            Return ToDouble(Session("SalesTaxPercent"))
        End If
        Dim myCommand As New SqlCommand("SELECT tg.TaxGroupID, tg.TotalPercent " & _
            "FROM TaxGroups tg " & _
            "Inner JOIN CustomerInformation c ON " & _
                "tg.CompanyID = c.CompanyID " & _
                "AND tg.DivisionID=c.DivisionID " & _
                "AND tg.DepartmentID=c.DepartmentID " & _
            "LEFT OUTER JOIN CustomerShipToLocations csl ON " & _
                 "c.CompanyID = csl.CompanyID " & _
                 "AND c.DivisionID=csl.DivisionID " & _
                 "AND c.DepartmentID=csl.DepartmentID " & _
                 "AND c.CustomerID=csl.CustomerID " & _
                 "AND c.CustomerShipToId=csl.ShipToID  " & _
            "WHERE " & _
                 "tg.TaxGroupID=CASE c.CustomerShipToId WHEN 'SAME' THEN c.CustomerState + ' Sales Tax' ELSE csl.ShipToState + ' Sales Tax' END " & _
                 "AND tg.CompanyID = @CompanyID " & _
                 "AND tg.DivisionID = @DivisionID " & _
                 "AND tg.DepartmentID = @DepartmentID " & _
                 "AND c.CustomerID = @CustomerID", Common.Connection())
        myCommand.Parameters.AddWithValue("@CompanyID", Session("CompanyID"))
        myCommand.Parameters.AddWithValue("@DivisionID", Session("DivisionID"))
        myCommand.Parameters.AddWithValue("@DepartmentID", Session("DepartmentID"))
        myCommand.Parameters.AddWithValue("@CustomerID", Session("CustomerID"))
        Dim r As SqlDataReader = Common.ExecuteReader(myCommand, CommandBehavior.SingleRow)
        If r.Read() Then
            Session("SalesTaxGroup") = Common.getValue(r, "TaxGroupID")
            Session("SalesTaxPercent") = Common.getValue(r, "TotalPercent")
        Else
            Session("SalesTaxGroup") = Nothing
            Session("SalesTaxPercent") = 0
        End If
        CloseDataReader(r)
        Return CDbl(Session("SalesTaxPercent"))
    End Function

    Public Shared Sub RecalcCart()
        Dim Session As HttpSessionState = HttpContext.Current.Session
        If Session("CartTable") Is Nothing Then Return
        
        For Each row As DataRow In Session("CartTable").Rows
            RecalcCartRow(row, row("Quantity"))
        Next
    End Sub

    Public Shared Function RecalcCartRow(ByVal row As DataRow, ByVal Qty As Double) As Boolean
        row.BeginEdit()
        If Qty < 0 Then
            Qty = 0
        End If
        If Qty > 0 Then
            Dim RowChanged As Boolean = row("Quantity") <> Qty
            row("Quantity") = Qty
            '----------------------------------------------------------------------------
            'Changed Code
            Dim ItemQuantity
            Dim ItemPrice
            Dim FinalItemPrice
            Dim TaxPercent = 0
            Dim TaxAmount = 0
            'Customer Sales tax group/percent is saved in the Session variables
            'if it is not defined we use item tax,
            'in other case the sales tax is used to calculate tax amount
            If Not row("Taxable") Is DBNull.Value AndAlso CBool(row("Taxable")) = True Then
                TaxPercent = GetCustomerSalesTaxPercent()
                If TaxPercent = 0 Then
                    TaxPercent = ToDouble(row("ItemTaxPercent"))
                End If
            End If
            ItemQuantity = Qty
            ItemPrice = CDbl(FormatNumber(IIf(row("Price") Is DBNull.Value, 0, row("Price"))))
            TaxAmount = CDbl(FormatNumber((ItemPrice * Qty) * TaxPercent / 100))
            FinalItemPrice = CDbl(FormatNumber((ItemPrice * Qty) + TaxAmount))
            If row("ItemTotalAmount") Is DBNull.Value OrElse row("ItemTotalAmount") <> FormatNumber(FinalItemPrice) Then
                RowChanged = True
            End If
            row("Price") = ItemPrice
            row("ItemTotalAmount") = FinalItemPrice
            row("TaxAmount") = TaxAmount
            '---------------------------------------------------------------------------
            row.EndEdit()
            Return RowChanged
        Else
            row.Table.Rows.Remove(row)
            Return True
        End If
    End Function

    Public Shared Function AddItemToCard(ByVal CartTable As DataTable, ByVal ItemID As String) As Boolean
        Dim reader As SqlDataReader = Common.PopulateItemInfo(ItemID)
        If Not reader.Read() Then
            reader.Close()
            Return False
        End If
        Dim I As Integer
        ' check whether the item is already in the cart
        Dim alreadyInCart As Boolean = False
        Dim CartRow As DataRow = Nothing
        Dim Qty As Integer = 0
        For I = 0 To CartTable.Rows.Count - 1
            Dim tmpItemID As String = reader.Item("ItemID")
            CartRow = CartTable.Rows(I)
            If CartRow("ItemID") = tmpItemID Then
                ' found it, increment quantity
                alreadyInCart = True
                Qty = CartRow("Quantity") + 1
                Exit For
            End If
        Next
        If Not alreadyInCart Then
            ' copy values from database
            CartRow = CartTable.NewRow()
            CartRow.BeginEdit()
            For I = 0 To reader.FieldCount - 1
                Dim col As String = reader.GetName(I)
                If CartTable.Columns.Contains(col) Then
                    CartRow(col) = reader.GetValue(I)
                End If
            Next
            Common.CloseDataReader(reader)
            Qty = 1
            CartRow("Quantity") = 0
            If Not CartRow("Taxable") Is DBNull.Value AndAlso CartRow("Taxable") Then
                CartRow("ItemTaxPercent") = Common.GetTaxPercent(CartRow("TaxGroupID"))
            Else
                CartRow("ItemTaxPercent") = 0
            End If
            CartRow.EndEdit()
            CartTable.Rows.Add(CartRow)
        Else
            Common.CloseDataReader(reader)
        End If
        RecalcCartRow(CartRow, Qty)
        Return True
    End Function
    Public Shared Function ToDouble(ByVal num As Object) As Double
        If num Is DBNull.Value OrElse num Is Nothing Then Return 0
        Return CDbl(num)
    End Function
    Public Shared Function GetPictureUrl(ByVal Container As Object, ByVal fieldName As String) As String
        Dim url As Object = DataBinder.Eval(Container, fieldName)
        If url Is DBNull.Value OrElse url.ToString.Trim() = "" Then Return ".\itemimages\empty.gif"
        Return url.ToString
    End Function
    
    Public Shared Sub ClearCart()
        Dim Session As HttpSessionState = HttpContext.Current.Session
        Session("CartTable") = Nothing
        Session("Order") = Nothing
        Session("CustomerID") = "CASH"
        Session("CustomerType") = "CASH"
        Dim Response As HttpResponse = HttpContext.Current.Response
        Dim CookieName As String = String.Format("CartTable-{0}-{1}-{2}", Session("CompanyID"), Session("DivisionID"), Session("DepartmentID"))
        Response.Cookies(CookieName).Value = Nothing
        Response.Cookies(CookieName).Expires = DateTime.Now

    End Sub

</script>

