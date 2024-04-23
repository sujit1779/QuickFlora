<%@ Control %>
<%@ import Namespace="System.Data.SqlClient" %>
<%@ import Namespace="System.Data" %>
<script runat="server">

' This is a control that displays the current company information in the header of the report

' To use this control in a report, simple take the register line and put that in the "Import" section of the report
' then put the control tag which is that line that begins with "sp1" in the report where you want the header to display.


    Dim ConnectionString As String = ""
    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim EmployeeID As String = ""
    Dim Allowed As Boolean = False


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    'Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)

    'If Session("SessionKey") Is Nothing Then
    ' This is a problem, if this string is  null then report was called
    ' from outside an Enterprise session and we redirect to error page
    'Response.Redirect("reporterror.htm")
    '    End If
    ' Change this ling to be the security setting from the access permissions table
    ' for example, if you want the conditions to test against ARReports in Access permissions,
    ' then put that value here as is the following line
    'Allowed = Session("GLReports")
    ' Ok, now do security check. If it fails, then exit to security error page
    'If Not Allowed Then
    'Response.Redirect("securityerror.htm")
    'End If
    ' Ok so everything is fine! Let's run our report now!
    
    'ConnectionString = EnterpriseCommon.Configuration.ConfigSettings.ConnectionString
    'CompanyID = SessionKey("CompanyID")
    'DivisionID = SessionKey("DivisionID")
    'DepartmentID = SessionKey("DepartmentID")
    'EmployeeID = SessionKey("EmployeeID")
    
    ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
        If Not Session("CompanyID") Is Nothing Then
            CompanyID = Session("CompanyID")
            DivisionID = Session("DivisionID")
            DepartmentID = Session("DepartmentID")
            EmployeeID = Session("EmployeeID")
            'PopulateSecurityPermissionsByEmpID(EmployeeID)
        Else
            Response.Redirect("loginform.aspx")
        End If
    
    BindGrid()

updateJSvariable()

    End Sub


    Sub BindGrid()
        ' Load the name of the stored procedure where our data comes from here into commandtext
      Dim CommandText As String = "enterprise.spCompanyInformation"

       ' get the connection ready
        Dim myConnection As New SqlConnection(ConnectionString)
        Dim myCommand As New SqlCommand(CommandText, myConnection)
        Dim workParam As New SqlParameter()

        myCommand.CommandType = CommandType.StoredProcedure

        ' Set the input parameter, companyid, divisionid, departmentid
        ' these parameters are set in the sub page_load
        myCommand.Parameters.Add("@CompanyID", SqlDbType.NVarChar).Value = CompanyID
        myCommand.Parameters.Add("@DivisionID", SqlDbType.NVarChar).Value = DivisionID
        myCommand.Parameters.Add("@DepartmentID", SqlDbType.NVarChar).Value = DepartmentID


        ' open the connection
        myConnection.Open()

        'bind the datasource
        DataGrid1.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
        DataGrid1.DataBind()


    End Sub


    Public Function PopulateImage(ByVal ob As String) As String
        Dim DocumentDir As String = ""
        Dim img1 As String = ""
        Dim ImgName As String = ob.ToLower()
        DocumentDir = ConfigurationManager.AppSettings("DocPath")
        If (ImgName.Trim() = "") Then

            Return "/images/no_image.gif"

        Else

            If System.IO.File.Exists(DocumentDir & ImgName.Trim()) Then

                Return "/images/" & ImgName.Trim()

            Else
                Return "/images/no_image.gif"
            End If




        End If


    End Function



    Public CompanyName As String = ""
    Public Add1 As String = ""
    Public State As String = ""
    Public Zip As String = ""
    Public City As String = ""
    Public Fax As String = ""
    Public Phone As String = ""
    Public email As String = ""
   
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
        ' execute command

        reader = Command.ExecuteReader(behavior)

        ' save new reader
        context.Items("SqlDataReader") = reader
        Return reader
    End Function


    Public Sub updateJSvariable()

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim MyCommand As New SqlCommand( _
       "enterprise.spCompanyInformation", myCon)
        MyCommand.Parameters.AddWithValue("@CompanyID", CompanyID)
        MyCommand.Parameters.AddWithValue("@DivisionID", DivisionID)
        MyCommand.Parameters.AddWithValue("@DepartmentID", DepartmentID)

        MyCommand.CommandType = CommandType.StoredProcedure

        MyCommand.Connection.Open()
        Dim reader As SqlDataReader
        reader = ExecuteReader(MyCommand)
        While reader.Read()
            CompanyName = reader("CompanyName").ToString()
            Add1 = reader("CompanyAddress1").ToString()
            State = reader("CompanyState").ToString()
            City = reader("CompanyCity").ToString()
            Zip = reader("CompanyZip").ToString()
            Phone = reader("CompanyPhone").ToString()
            Fax = reader("CompanyFax").ToString()
            email = reader("CompanyEmail").ToString()
        End While
        MyCommand.Connection.Close()
        reader.Close()
  
        ''new code for update address by location id
               
        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        Dim locationid As String = CType(SessionKey("Locationid"), String)
        If locationid <> "" Then
            'Response.Write("Location = " & locationid  ) 
            Dim objloc As New clsOrder_Location
            objloc.CompanyID = CompanyID
            objloc.DivisionID = DivisionID
            objloc.DepartmentID = DepartmentID
            objloc.LocationID = locationid
                   
            Dim dtobjloc As New DataTable
            dtobjloc = objloc.DetailsOrder_Location()
                   
            If dtobjloc.Rows.Count <> 0 Then
                       
                Dim chk As Boolean = False
                Try
                    chk = dtobjloc.Rows(0)("AllowLocationAddress").ToString()
                Catch ex As Exception

                End Try
                       
                If chk Then
                    Add1 = dtobjloc.Rows(0)("Add1").ToString()
                    State = dtobjloc.Rows(0)("State").ToString()
                    City = dtobjloc.Rows(0)("City").ToString()
                    Zip = dtobjloc.Rows(0)("ZipCode").ToString()
                    Phone = dtobjloc.Rows(0)("Phone").ToString()
                    Fax = dtobjloc.Rows(0)("Fax").ToString()
                    email = dtobjloc.Rows(0)("Email").ToString()
                    
                    DataGrid1.Visible = False
                    pnllocation.Visible = True
                End If
                       
            End If
                   
                   
        End If
        ''new code for update address by location id
         
    End Sub

</script>
<html>
<head>
</head>
<body style="FONT-FAMILY: arial">
        <asp:Repeater id="DataGrid1" runat="server">
            <HeaderTemplate></HeaderTemplate>
            <ItemTemplate>
                    <table border="0" rules="none" width="99%" bgcolor="#ffffff" cellpadding="1" cellspacing="1">
                        <tr>
                            <td width="10%" rowspan="3" align="center" valign="middle">
                                 <img src="/images/foflogo.jpg" style="border-width:0px;">
                            </td>
                            <td width="90%" height="10%" valign="bottom">
                                    <b><font face="Arial" size="4"> <%# DataBinder.Eval(Container, "DataItem.CompanyName") %></font></b>
                            </td>
                        </tr>
                        <tr>
                            <td width="90%" height="49%" valign="middle">
                                <font face="Arial" size="2"> <%# DataBinder.Eval(Container, "DataItem.CompanyAddress1") & ", " & DataBinder.Eval(Container, "DataItem.CompanyCity") & ", " & DataBinder.Eval(Container, "DataItem.CompanyState") & ", " & DataBinder.Eval(Container, "DataItem.CompanyZip") %> </font>
                            </td>
                        </tr>
                        <tr>
                            <td width="90%" height="40%" valign="middle">
                                <font face="Arial" size="1"> <%# DataBinder.Eval(Container, "DataItem.CompanyPhone") & ", " & DataBinder.Eval(Container, "DataItem.CompanyEmail") & ", " & DataBinder.Eval(Container, "DataItem.CompanyWebAddress") %> </font>
                            </td>
                        </tr>                    
                     </table>
            </ItemTemplate>
            <AlternatingItemTemplate></AlternatingItemTemplate>
            <FooterTemplate></FooterTemplate>
        </asp:Repeater>
        
        
            <asp:Panel ID="pnllocation" Visible="false"  runat="server">
    
         
                    <table border="0" rules="none" width="99%" bgcolor="#ffffff" cellpadding="1" cellspacing="1">
                        <tr>
                            <td width="10%" rowspan="3" align="center" valign="middle">
                             <img src="/images/foflogo.jpg" style="border-width:0px;">
                            </td>
                            <td width="90%" height="10%" valign="bottom">
                                    <b><font face="Arial" size="4"> <% =CompanyName %></font></b>
                            </td>
                        </tr>
                        <tr>
                            <td width="90%" height="49%" valign="middle">
                                <font face="Arial" size="2"> <% = Add1 & ", " & City & ", " & State & ", " & Zip  %> </font>
                            </td>
                        </tr>
                        <tr>
                            <td width="90%" height="40%" valign="middle">
                                <font face="Arial" size="1"> <% = Phone & ", " & email  %> </font>
                            </td>
                        </tr>                    
                     </table>
          
</asp:Panel>
</body>
</html>













