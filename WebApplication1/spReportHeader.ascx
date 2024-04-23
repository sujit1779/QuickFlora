<%@ Control %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Data" %>

<script runat="server">


    ' This is a control that displays the current company information in the header of the report
    ' we could do this just with session variables if performance is a concene, but we use this 
    ' fancy routine instead that calls the companies table to get the company name and the company
    ' logo url to display in the header of the report. Since this control opens the companies table anyway,
    ' you can set other global reporting options there and reference them here

    ' To use this control in a report, simple take the register line and put that in the "Import" section of the report
    ' then put the control tag which is that line that begins with "sp1" in the report where you want the header to display.




    Dim ConnectionString As String = ""
    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim EmployeeID As String = ""
    Dim Allowed As Boolean = False


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' Ok so everything is fine! Let's run our report now!
        ConnectionString = ConfigurationManager.AppSettings("ConnectionString")
        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

        BindGrid()

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

            Return "../images/no_image.gif"

        Else

            If System.IO.File.Exists(DocumentDir & ImgName.Trim()) Then

                Return "../images/" & ImgName.Trim()

            Else
                Return "../images/no_image.gif"
            End If




        End If


    End Function
</script>


        <asp:Repeater ID="DataGrid1" runat="server">
        <HeaderTemplate>
        </HeaderTemplate>
        <ItemTemplate>
            <table border="0" rules="none" width="99%" bgcolor="#ffffff" cellpadding="1" cellspacing="1">
                <tr>
                    <td width="10%" rowspan="3" align="center" valign="middle">
                        <asp:Image ID="Image1" runat="server" border="0" ImageUrl='<%# PopulateImage(DataBinder.Eval(Container, "DataItem.CompanyLogoUrl")) %> '>
                        </asp:Image>
                    </td>
                    <td width="90%" height="80%" valign="bottom">
                        <br>
                        <b><font face="Arial" size="5">
                           
                        </font></b>
                    </td>
                </tr>
                <tr>
                    <td width="90%" height="10%" valign="bottom">
                        <font face="Arial" size="2">
                               </font>
                    </td>
                </tr>
               
                 
                
              
           
            </table>
            
            <table border="0" width="25%" id="table1">
            	<tr>
			<td colspan="3"> <%# DataBinder.Eval(Container, "DataItem.CompanyName") %></td>
		</tr>
		<tr>
			<td colspan="3"><%#DataBinder.Eval(Container, "DataItem.CompanyAddress1")%></td>
		</tr>
		 
		<tr><td colspan="3">
			  <%#DataBinder.Eval(Container, "DataItem.CompanyCity")%> 
			 <%#DataBinder.Eval(Container, "DataItem.CompanyState")%> 
			 <%#DataBinder.Eval(Container, "DataItem.CompanyZip")%></td>
		</tr>
		<tr>
			<td colspan="3">Executed By <%# EmployeeID & " on " & Today %></td>
		</tr>
	</table>
    

                    
    
        </ItemTemplate>
        <AlternatingItemTemplate>
        </AlternatingItemTemplate>
        <FooterTemplate>
            <hr>
        </FooterTemplate>
    </asp:Repeater>

