<%@ Page Language="VB"  MasterPageFile="~/MainMaster.master" enableEventValidation="false" 
    viewStateEncryptionMode="Never" enableViewStateMac="false"  ValidateRequest="false" 
    AutoEventWireup="false" CodeFile="EmailNotifications.aspx.vb" 
    Inherits="EnterpriseASPSystem_CustomCompanySetup_EmailNotifications" %>

 <%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>
<%@ Register TagPrefix="FCKeditorV2" Namespace="FredCK.FCKeditorV2" Assembly="FredCK.FCKeditorV2" %>
   


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">

    Email Notifications

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">


  <script type="text/javascript" language="javascript">
 function Tagpopup()
 {
   //window.open("TagHelp.aspx",'MyWindow','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=650,height=500,Left=350,top=175,screenX=0,screenY=375,alwaysRaised=yes');
 }

</script>
<script type="text/javascript" language="javascript">
function sendForm()
{
 document.aspnetForm.submit();
 document.aspnetForm.method="POST";
 document.aspnetForm.action="TagPreview.aspx";
   document.aspnetForm.pfck.value = document.getElementById("ctl00__mainContent_FCKeditor1").value;

  if (document.aspnetForm.action=="TagPreview.aspx"){
  msgWindow=window.open("","Preview");
  document.aspnetForm.target = "Preview";
  document.aspnetForm.submit();
  document.aspnetForm.target="_self";
  document.aspnetForm.action="";
    }
 
 
}

</script>
  
 <hr />
  
<table border="0" width="775" id="table2">
	
	<tr>
		<td width="5">&nbsp;</td>
		<td width="770" colspan="3"   > <h3> Use Pre-defined Tags for Creating Email templates</h3>
	 
	
		</td>
		 
	</tr>
	<tr>
		<td width="5">&nbsp;</td>
		<td width="200"></td>
		<td width="10">&nbsp;</td>
		<td width="560">    &nbsp;</td>
	</tr>
 <tr>
  <td width="5">&nbsp;</td>
  <td width="200" style="background-color:#c6dbad;" >Email Type</td>
  <td width="10">&nbsp;</td>
  <td width="560">    
      
      <asp:DropDownList ID="drpEmailTypes" Width="300" runat="server" AutoPostBack="True">
            <asp:ListItem Text="Order Notification to Vendor" Value="Order Notification to Vendor"></asp:ListItem>
            <asp:ListItem Text="Order Cancel Notification to Vendor" Value="Order Cancel Notification to Vendor"></asp:ListItem>
     </asp:DropDownList>

  </td>
 </tr>
    <tr>
		<td width="5">&nbsp;</td>
		<td width="200" style="height: 26px;background-color:#c6dbad;"></td>
		<td width="10">&nbsp;</td>
		<td width="560">    &nbsp;</td>
	</tr>
	<tr>
		<td width="5" style="height: 26px">&nbsp;</td>
		<td width="200" style="height: 26px;background-color:#c6dbad;">Email Subject</td>
		<td width="10" style="height: 26px">&nbsp;</td>
		<td width="560" style="height: 26px">  <asp:TextBox ID="txtEmailSubject" Width="560"   runat="server"  ></asp:TextBox></td>
	</tr>
    <tr>
		<td width="5">&nbsp;</td>
		<td width="200" style="height: 26px;background-color:#c6dbad;"></td>
		<td width="10">&nbsp;</td>
		<td width="560">    &nbsp;</td>
	</tr>
	<tr>
		<td width="5" style="height: 402px">&nbsp;</td>
		<td width="200" style="height: 402px;background-color:#c6dbad;">Email Content</td>
		<td width="10" style="height: 402px">&nbsp;</td>
		<td width="560" style="height: 402px">
		
		    <FCKeditorV2:FCKeditor ID="FCKeditor1" EnableViewState="true"  Height="400" BasePath="~/fckeditor/"  runat="server">
    </FCKeditorV2:FCKeditor> 
		
		
		</td>
	</tr>
    <tr>
		<td width="5">&nbsp;</td>
		<td width="200" style="height: 26px;background-color:#c6dbad;"></td>
		<td width="10">&nbsp;</td>
		<td width="560">    &nbsp;</td>
	</tr>
<tr>
		<td width="5" style="height: 26px">&nbsp;</td>
		<td width="200" style="height: 26px;background-color:#c6dbad;">Email Notifications Active</td>
		<td width="10" style="height: 26px">&nbsp;</td>
		<td width="560" style="height: 26px">
		
            <asp:CheckBox ID="chkEmailActive" runat="server" />
		
		</td>
	</tr>
	
	<tr>
		<td width="5">&nbsp;</td>
		<td width="200">&nbsp;</td>
		<td width="10">&nbsp;</td>
		<td width="560">&nbsp;</td>
	</tr>
	<tr>
		<td width="5">&nbsp;</td>
		<td width="200">&nbsp;</td>
		<td width="10">&nbsp;</td>
		<td width="560">&nbsp;</td>
	</tr>
	<tr>
		<td width="5">&nbsp;</td>
		<td width="200">&nbsp;</td>
		<td width="10">&nbsp;</td>
		<td width="560"><asp:Button ID="BtnSubmit"  CssClass="btn btn-success btn-xs" runat="server" Text="Submit" />
         

		</td>
	</tr>
</table>



</asp:Content>