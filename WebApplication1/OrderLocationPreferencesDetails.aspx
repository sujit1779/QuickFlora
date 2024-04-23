<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="OrderLocationPreferencesDetails.aspx.vb" Inherits="OrderLocationPreferencesDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

 
<script language="JavaScript">

    function WebForm_OnSubmit() {


        obj101 = window.document.getElementById("ctl00_ContentPlaceHolder_txtLocationID");
        if (obj101.value.length == 0) {
            alert("Please enter a value for LocationID.");
            obj101.focus();
            return false;
        }

        obj100 = window.document.getElementById("ctl00_ContentPlaceHolder_txtLocationName");
        if (obj100.value.length == 0) {
            alert("Please enter a value for txtLocationName.");
            obj100.focus();
            return false;
        }

    }


    function IsNumeric(strString)
    //  check for valid numeric strings	
    {

        var strValidChars = "0123456789.-";
        var strChar;
        var blnResult = true;

        if (strString.length == 0) return false;

        //  test strString consists of valid characters listed above
        for (i = 0; i < strString.length && blnResult == true; i++) {
            strChar = strString.charAt(i);
            if (strValidChars.indexOf(strChar) == -1) {
                blnResult = false;
            }
        }
        return blnResult;
    }
</script>

         

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">

<div class="row">
                                    <div class="col-md-6">
                                        Multi-Location
                                    </div> 
                                     <div class="col-md-6">
                                         <asp:Table runat="server" ID="tblMain" SkinID="groupHeaderSkin">
                                            <asp:TableRow>
                                                <asp:TableCell HorizontalAlign="Left">
                                                    <asp:LinkButton ID="btnNew" runat="server" SkinID="" CssClass="btn btn-success btn-xs" Text="Back to Location List" PostBackUrl="OrderLocationPreferences.aspx"></asp:LinkButton>
                                                     
                                                </asp:TableCell>
                                                 
                                            </asp:TableRow>
                                        </asp:Table>
                                    </div> 
</div> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">

		<div class="portlet box green">
						<div class="portlet-title">
							<div class="caption">Search Options</div>
							<div class="tools"><a href="javascript:;" class="collapse"></a></div>
						</div>
						<div class="portlet-body" >

                        <div class="portlet-body form">


                        <div class="row">
                                            <div class="col-md-12" style=" padding-left:50px;">
                        
  <table class="table table-bordered table-striped table-condensed flip-content"   border="0" width="100%" id="table1">
	<tr>
		
		<td width="217">&nbsp;</td>
		<td width="11">&nbsp;</td>
		<td>&nbsp;</td>
		
	</tr>
	<tr>
		
		<td width="217" align="right"  style=" font-weight:bold;padding-right:10px;  ">Location ID</td>
		<td width="11">&nbsp;</td>
		<td> 
            <asp:TextBox    ID="txtLocationID" runat="server" Width="300px"></asp:TextBox></td>
		
	</tr>
	<tr>
		
		<td width="217" align="right" style=" font-weight:bold;padding-right:10px;">Location Name</td>
		<td width="11">&nbsp;</td>
		<td>
            <asp:TextBox ID="txtLocationName" runat="server" Width="300px"></asp:TextBox> </td>
		
	</tr>
	
	
	<tr>
		
		<td width="217">&nbsp;</td>
		<td width="11">&nbsp;</td>
		<td>&nbsp;</td>
		
	</tr>

    	<tr>
		
		<td width="217" align="right" style=" font-weight:bold; padding-right:10px;">Address</td>
		<td width="11">&nbsp;</td>
		<td> 
            <asp:TextBox ID="txtadddress" runat="server" Width="300px"></asp:TextBox>
             </td>    
		
	</tr>
	
	
		<tr>
		
		<td width="217" align="right" style=" font-weight:bold;padding-right:10px;">City</td>
		<td width="11">&nbsp;</td>
		<td>
            <asp:TextBox ID="txtCity" runat="server" Width="300px"></asp:TextBox> </td>
		
	</tr>
	<tr>
		
		<td width="217" align="right" style=" font-weight:bold;padding-right:10px;">State</td>
		<td width="11">&nbsp;</td>
		<td> <asp:TextBox ID="txtState" runat="server"   Width="300px"></asp:TextBox></td>
		
	</tr>
	<tr>
		
		<td width="217" align="right" style=" font-weight:bold;padding-right:10px;">ZipCode</td>
		<td width="11">&nbsp;</td>
		<td> <asp:TextBox ID="txtZipCode" runat="server"  Width="300px"></asp:TextBox> </td>
		
	</tr>

	<tr>
		
		<td width="217" align="right" style=" font-weight:bold; padding-right:10px;">Country</td>
		<td width="11">&nbsp;</td>
		<td> 
            <asp:TextBox ID="txtCountry" runat="server" Width="300px"></asp:TextBox>
             </td>    
		
	</tr>
<tr>
		
		<td width="217">&nbsp;</td>
		<td width="11">&nbsp;</td>
		<td>&nbsp;</td>
		
	</tr>

    	<tr>
		
		<td width="217" align="right" style=" font-weight:bold; padding-right:10px;">Phone</td>
		<td width="11">&nbsp;</td>
		<td> 
            <asp:TextBox ID="txtphone" runat="server" Width="300px"></asp:TextBox>
             </td>    
		
	</tr>


    <tr>
		
		<td width="217" align="right" style=" font-weight:bold; padding-right:10px;">Fax</td>
		<td width="11">&nbsp;</td>
		<td> 
            <asp:TextBox ID="txtFax" runat="server" Width="300px"></asp:TextBox>
             </td>    
		
	</tr>

    <tr>
		
		<td width="217" align="right" style=" font-weight:bold; padding-right:10px;">Email</td>
		<td width="11">&nbsp;</td>
		<td> 
            <asp:TextBox ID="txtEmail" runat="server" Width="300px"></asp:TextBox>
             </td>    
		
	</tr>

	<tr>
		
		<td width="217">&nbsp;</td>
		<td width="11">&nbsp;</td>
		<td>&nbsp;</td>
		
	</tr>
      <tr>
		
		<td width="217" align="right" style=" font-weight:bold; padding-right:10px;">Notes</td>
		<td width="11">&nbsp;</td>
		<td> 
            <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Rows="2" Width="500px"></asp:TextBox>
             </td>    
		
	</tr>

	<tr>
		
		<td width="217">&nbsp;</td>
		<td width="11">&nbsp;</td>
		<td>&nbsp;</td>
		
	</tr>

    	<tr>
		
		<td width="217" align="right" style=" font-weight:bold; padding-right:10px;">Enable Adress display by location</td>
		<td width="11">&nbsp;</td>
		<td> 
            <asp:CheckBox ID="chkenable" runat="server" />
             </td>    
		
	</tr>

      
	<tr>
		
		<td width="217" align="right" style=" font-weight:bold; padding-right:10px;">&nbsp;Allowed All Items </td>
		<td width="11">&nbsp;   </td>
		<td><asp:CheckBox ID="chkAllowedAllItems" runat="server" /></td>
		
	</tr>
	<tr>
		
		<td width="217">&nbsp;</td>
		<td width="11">&nbsp;</td>
		<td>&nbsp;</td>
		
	</tr>
	<tr>
		
		<td width="217">&nbsp;</td>
		<td width="11">&nbsp;</td>
		<td>&nbsp;</td>
		
	</tr>
	<tr>
		
		<td width="217">&nbsp;</td>
		<td width="11">&nbsp;</td>
		<td>
            <asp:Button ID="BtnSubmit" CssClass="btn btn-success btn-xs" OnClientClick="javascript:return WebForm_OnSubmit()" runat="server" Text="Save" /> 
            <asp:Button ID="BtnCancel"  CssClass="btn btn-success btn-xs" runat="server" Text="Cancel" />
            <br />
            <br />
            <asp:Label ID="lblerror" runat="server" Text=""></asp:Label>
          </td>
		
	</tr>
</table>

                                </div> 
                        </div> 



                        </div> 

                        </div>
                 </div>  

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

