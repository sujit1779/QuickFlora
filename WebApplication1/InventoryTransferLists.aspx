<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="InventoryTransferLists.aspx.vb" Inherits="InventoryTransferLists" %>

 <%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>

<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Core"
    TagPrefix="core" %>
<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Controls"
    TagPrefix="ctls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">

<div class="row">
                                    <div class="col-md-6">
                                        Merchandise Transfer List
                                    </div> 
                                     <div class="col-md-6">
                                         <asp:Table runat="server" ID="tblMain" SkinID="groupHeaderSkin">
                                            <asp:TableRow>
                                                <asp:TableCell HorizontalAlign="Left">
                                                    <asp:LinkButton ID="btnNew" runat="server" SkinID="" CssClass="btn btn-success btn-xs" Text="Create New Merchandise Transfer" PostBackUrl="InventoryTransferDetaila.aspx"></asp:LinkButton>
                                                     
                                                </asp:TableCell>
                                                 
                                            </asp:TableRow>
                                        </asp:Table>
                                    </div> 
</div>                                     

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">




  
<script type="text/javascript">


    var req;

    function Initialize() {
        try {
            req = new ActiveXObject("Msxml2.XMLHTTP");
        }
        catch (e) {
            try {
                req = new ActiveXObject("Microsoft.XMLHTTP");
            }
            catch (oc) {
                req = null;
            }
        }

        if (!req && typeof XMLHttpRequest != "undefined") {
            req = new XMLHttpRequest();
        }

    }
    var global_item_add = 0;

    function Saveitem(key, saveid) {
        Initialize();
        var url = "AjaxItemsSave.aspx?" + key;

        global_item_add = 0;

        //         alert(saveid);
        //          alert(document.getElementById(saveid));

        document.getElementById("ctl00_ContentPlaceHolder_reqsaveid").value = saveid;

        if (req != null) {
            req.onreadystatechange = ProcessSave;
            req.open("GET", url, true);
            req.send(null);


        }

    }


    function SendQuery(key) {
        Initialize();
        var url = "AjaxMTSearch.aspx?k=" + key;
        //alert(url);
        if (req != null) {
            req.onreadystatechange = Process;
            req.open("GET", url, true);
            req.send(null);

        }

    }

    function Process() {

        //alert(document.getElementById("<%=txtcustomersearch.ClientID%>"));

        document.getElementById("<%=txtcustomersearch.ClientID%>").style.backgroundImage = 'url(ajax-loader-text.gif)';
        document.getElementById("<%=txtcustomersearch.ClientID%>").style.backgroundRepeat = 'no-repeat';
        document.getElementById("<%=txtcustomersearch.ClientID%>").style.backgroundPositionX = 'right';



        if (req.readyState == 4) {
            // only if "OK"

            if (req.status == 200) {
                document.getElementById("<%=txtcustomersearch.ClientID%>").style.backgroundImage = 'url()';
                // alert(req.status);
                if (trim(req.responseText) == "") {
                    //alert("in blank");  
                    //HideDiv("autocomplete");
                    document.getElementById("<%=txtcustomersearch.ClientID%>").style.backgroundImage = 'url(searchbackground.jpg)';
                    document.getElementById("<%=txtcustomersearch.ClientID%>").style.backgroundRepeat = 'no-repeat';
                    document.getElementById("<%=txtcustomersearch.ClientID%>").style.backgroundPositionX = 'left';
                    document.getElementById("<%=txtcustomersearch.ClientID%>").value = "";
                    var newdiv = document.createElement("div");
                    newdiv.innerHTML = "<br><br><div style='text-align:center' >No result found, Please try with some other keyword.</div>";
                    var container = document.getElementById("autocomplete");
                    container.innerHTML = "<div style='text-align:center' ><a class='btn btn-danger btn-xs'  href='javascript:CcustomersearchcloseProcess();' >Close Search result</a></div>";
                    container.appendChild(newdiv)
                }
                else {
                    if (document.getElementById("<%=txtcustomersearch.ClientID%>").value != "") {

                        ShowDiv("autocomplete");

                        var newdiv = document.createElement("div");
                        newdiv.innerHTML = req.responseText;
                        var container = document.getElementById("autocomplete");
                        container.innerHTML = "<div style='text-align:center' ><a class='btn btn-danger btn-xs' href='javascript:CcustomersearchcloseProcess();' >Close Search result</a></div>";
                        container.appendChild(newdiv)
                    }
                }
            }
            else {
                document.getElementById("autocomplete").innerHTML = "There was a problem retrieving data:<br>" + req.statusText;

            }
        }
    }

    function ShowDiv(divid) {


        document.getElementById(divid).style.height = "200px";
        if (document.layers) document.layers[divid].visibility = "show";
        else document.getElementById(divid).style.visibility = "visible";
    }


    function HideDiv(divid) {
        document.getElementById(divid).style.height = "0px";
        if (document.layers) document.layers[divid].visibility = "hide";
        else document.getElementById(divid).style.visibility = "hidden";
    }

    // Removes leading whitespaces
    function LTrim(value) {

        var re = /\s*((\S+\s*)*)/;
        return value.replace(re, "$1");

    }

    // Removes ending whitespaces
    function RTrim(value) {

        var re = /((\s*\S+)*)\s*/;
        return value.replace(re, "$1");

    }

    // Removes leading and ending whitespaces
    function trim(value) {

        return LTrim(RTrim(value));

    }

    function CcustomersearchcloseProcess() {




        document.getElementById("<%=txtcustomersearch.ClientID%>").style.backgroundImage = 'url(searchbackground.jpg)';
        document.getElementById("<%=txtcustomersearch.ClientID%>").style.backgroundRepeat = 'no-repeat';
        document.getElementById("<%=txtcustomersearch.ClientID%>").style.backgroundPositionX = 'left';
        document.getElementById("<%=txtcustomersearch.ClientID%>").value = "";
        document.getElementById("autocomplete").innerHTML = "";
        HideDiv("autocomplete");


    }

    function CcustomersearchBlurProcess() {

        if (document.getElementById("<%=txtcustomersearch.ClientID%>").value == "") {
            HideDiv("autocomplete");
            document.getElementById("<%=txtcustomersearch.ClientID%>").style.backgroundImage = 'url(searchbackground.jpg)';
        }

    }

    function FillSearchtextBox(val) {
        // alert(val);
        document.getElementById("<%=txtcustomersearch.ClientID%>").value = val;
        document.getElementById("autocomplete").innerHTML = "";
        HideDiv("autocomplete");
        document.getElementById("<%=txtcustomersearch.ClientID%>").style.backgroundImage = 'url()';
        document.getElementById("<%=btncustsearch.ClientID%>").focus();
        document.getElementById("<%=btncustsearch.ClientID%>").click();
    }


 </script>
 



    <script type="text/javascript">

        function deleteConfirmForTransfer() {
            if (confirm("Are you sure to delete this Transfer completely ?")) {
                return true;
            } else {
                return false;
            }
        }

    </script>

					<div class="portlet box green">
						<div class="portlet-title">
							<div class="caption">Search Options</div>
							<div class="tools"><a href="javascript:;" class="collapse"></a></div>
						</div>
						<div class="portlet-body" >

    

     
   <div class="row">
                                    <div class="col-md-5">
                                    	<div class="row form-group">
                                        	<label class="control-label col-md-3">Date Range</label>
                                                        <div class="col-md-9">
                                                            <div class="input-group input-sm date-picker input-daterange" data-date="07/03/2015" data-date-format="mm/dd/yyyy">
                                                                <div class="input-icon">
                                                               <table>
                                                                    <tr>
                                                                        <td> <i class="fa fa-calendar"></i></td>
                                                                        <td><asp:TextBox  CssClass="form-control"      runat="server" ID="txtDateFrom"> </asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                                    
                                                                     
                                                                 
                                                                
                                                                </div>
                                                                <span class="input-group-addon">
                                                                to </span>
                                                                <div class="input-icon">
                                                                <table>
                                                                    <tr>
                                                                        <td>&nbsp;&nbsp;</td>
                                                                        <td> <i class="fa fa-calendar"></i></td>
                                                                        <td><asp:TextBox    CssClass="form-control"      runat="server" ID="txtDateTo"> </asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                               
                                                                 
                                                                    

                                                                </div>
                                                            </div>
                                                            <!-- /input-group -->
                                                            
                                                        </div>
                                       	</div>
                                          
                                    </div>
                                    <div class="col-md-5">
                                        <div class="row form-group">
                                            <label class="col-md-3 control-label">Search By</label>
                                            <div class="col-md-9">
                                            <div class="radio-list">
                                                     <table>
                                                        <tr>
                                                            <td><asp:RadioButton ID="rdAllDates" Checked="true" GroupName="Dates" Text="All Dates"    runat="server" /></td>
                                                            <td>&nbsp;</td>
                                                            <td><asp:RadioButton ID="rdOrderDates"  GroupName="Dates" Text=" Transfer Date"   runat="server" /></td>
                                                            <td>&nbsp;</td>
                                                            <td><asp:RadioButton ID="rdDeliveryDates"  GroupName="Dates" Text="Receive Date"   runat="server" /></td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;
                                                                <asp:Button ID="btnsearch" CssClass="btn btn-success btn-xs" runat="server" Text="Search" />
                                                            </td>
                                                        </tr>
                                                     
                                                     </table>
                                                     
                                                    
                                                    

                                                </div>
                                                
                                            </div>
                                    	</div>
                                    </div>
                               
                               <div class="col-md-2">
                                   
                                     <a href="#"  onClick="window.open('InventoryTransferListsExcel.aspx','gotFusion','toolbar=1,location=1,directories=0,status=0,menubar=1,scrollbars=1,resizable=1,copyhistory=0,width=800,height=600,left=0,top=0')" ><img  alt="excel" src="https://secure.quickflora.com/images/excel.png" width="50" height="50" border="0" /></a>
                               </div>
       </div>

      
                             <div class="portlet-body form">
                                        <div class="form-group-search-block">
                                            <div class="input-group">
                                                <span class="input-group-addon input-circle-left"><i class="fa fa-search"></i></span>
                                                <ajax:ajaxpanel id="AjaxPanel6" runat="server">
                                                                    <asp:TextBox ID="txtcustomersearch" runat="server" CssClass="form-control input-circle-right"    ></asp:TextBox>
                                                                </ajax:ajaxpanel>
                                                <br />
                                                <div align="left" class="box autocomplete" style="visibility: hidden;" id="autocomplete">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12" style="padding-top: 10px;">
                                                <ajax:ajaxpanel id="AjaxPanel7" runat="server">
                                                                        &nbsp;<asp:ImageButton ID="btncustsearch"  ToolTip="Update Customer" ImageUrl="~/images/2-sh-stock-in.gif" Width="0" runat="server" /> 
                                                                        <asp:Label ID="lblsearchcustomermsg" runat="server" Text=""></asp:Label>
                                                                </ajax:ajaxpanel>
                                            </div>
                                        </div>

                                    </div>
                            


                                                     

<div class="portlet-body">
            <div class="row">
                
                <div class="col-md-4">
                    <!-- BEGIN FORM-->
                    <div class="form-body">
                        <table id="table1" class="table table-striped table-hover table-bordered">
                            
                            
                            <tr>
                                <td width="110" height="12" valign="left">
                                       <span style="display: inline-block;">Transfer From</span> 
                                
                                </td>
                                <td width="151" height="12">
                                     <span>
                                    <asp:DropDownList ID="drpTansferFromLocaton" CssClass="form-control" AutoPostBack="true"  runat="server"></asp:DropDownList></span>
                                </td>
                                 
                            </tr>
                           
                                    
                        </table>
                    </div>
                    <!-- END FORM-->
                </div>
                <div class="col-md-4">
                    
                      <table id="table2" class="table table-striped table-hover table-bordered">
                            
                            
                            <tr>
                                <td width="110" height="12" valign="left">
                                     <span>Transfer To</span> 
                                
                                </td>
                                <td width="151" height="12">
                                     <span>
                                    <asp:DropDownList ID="drpTransferToLocaton" CssClass="form-control" AutoPostBack="true"  runat="server"></asp:DropDownList></span>
                                </td>
                                
                            </tr>
                            
                           
                                    
                        </table>


                </div>
                 <div class="col-md-4">
                    
                      <table id="table2" class="table table-striped table-hover table-bordered">
                            
                            
                            <tr>
                                <td width="110" height="12" valign="left">
                                     <span>Transfer Status</span> 
                                
                                </td>
                                <td width="151" height="12">
                                     <span>
                                    <asp:DropDownList ID="drpstatus" CssClass="form-control" AutoPostBack="true"  runat="server">
                                        <asp:ListItem Selected="True"  Text="--All--" Value =""></asp:ListItem>
                                        <asp:ListItem  Text="Received" Value ="Received"></asp:ListItem>
                                        <asp:ListItem  Text="Canceled" Value ="Canceled"></asp:ListItem>
                                        <asp:ListItem  Text="Not Received" Value ="Not Received"></asp:ListItem>
                                    </asp:DropDownList></span>
                                </td>
                               
                            </tr>
                            
                           
                                    
                        </table>


                </div>
            </div>
        </div>



     </div>
				
                
                
                
                
 </div>
				<!-- END PORTLET-->
     <ajax:ajaxpanel id="AjaxPanel077" runat="server">
    <asp:HiddenField ID="hdSortDirection" runat="server" />
    <asp:GridView ID="InventoryTransferGrid" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="50"
        DataKeyNames="TransferNumber" AllowSorting="true" CssClass="table table-bordered table-striped table-condensed flip-content"   >
        <Columns> 
            <asp:TemplateField HeaderText="Edit"    >
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" Enabled='<%#getEditVisible()%>' CausesValidation="false" ToolTip="Edit"  ImageUrl="~/Images/edit.gif"
                        runat="server" CommandName="Edit" />
                </ItemTemplate>
                <ItemStyle Width="3px" HorizontalAlign="Center" />
            </asp:TemplateField>
              
                    <asp:TemplateField HeaderText="View"  ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>
                            <a target="_blank" class="btn btn-warning btn-xs tooltips" href='https://reports.quickflora.com/reports/scripts/MTReport.aspx?CompanyID=<%=CompanyID %>&DivisionID=<%=DivisionID %>&DepartmentID=<%=DepartmentID %>&TransferNumber=<%# Eval("TransferNumber")%>'>
                                <i class="fa fa-search"></i>
                                
                                </a>
                        </ItemTemplate>
                        
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText="Receive"  ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>

                            <asp:HyperLink ID="hprReceive" NavigateUrl="" runat="server"> <i class="fa fa-check"></i></asp:HyperLink>
                        </ItemTemplate>
                        
                    </asp:TemplateField> 
                    
                      
                        <asp:TemplateField Visible="false" HeaderText="PRN">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgWorkTicketPrint" 
                            CommandName="MTPrint" CommandArgument='<%# Eval("TransferNumber") %>' ToolTip="Print MT"
                            ImageUrl="~/images/print.gif" runat="server" />
                    </ItemTemplate>
                    
                </asp:TemplateField>
                      
                     <asp:TemplateField HeaderText="Email"  ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>
                            <a  class="btn btn-warning btn-xs tooltips" href='emailMT.aspx?TransferNumber=<%# Eval("TransferNumber")%>'>
                                <img border=0 src="https://secure.quickflora.com/EnterpriseASP/images/menu_icons/imp.png" />
                                
                                </a>
                        </ItemTemplate>
                        
                    </asp:TemplateField> 
                    
                     <asp:TemplateField HeaderText="Dup"  ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>
                            <a  class="btn btn-warning btn-xs tooltips" href='MTDuplicate.aspx?TransferNumber=<%# Eval("TransferNumber")%>'>
                                 
                                 <img border=0 src="https://secure.quickflora.com/EnterpriseASP/images/withdrawl.gif" />
                                </a>
                        </ItemTemplate>
                        
                    </asp:TemplateField> 
                    
            
                 <asp:TemplateField HeaderText="Transfer Number"  >
                    <ItemTemplate>
                       <asp:Label ID="lblstrik" Text='*' ForeColor="Orange" Visible="false" runat="server">

                       </asp:Label> <asp:Label ID="lblOrderNumber" Text='<%#Eval("TransferNumber")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                     
                </asp:TemplateField>
             
            <asp:TemplateField HeaderText="Tansfer From Location" >
                    <ItemTemplate>
                       
                        <%# FillLocationName(Eval("TansferFromLocation"))%> 
                    </ItemTemplate>
                    
                </asp:TemplateField>
                
            <asp:BoundField HeaderText="Tansfer From Location"  Visible="false"   ItemStyle-HorizontalAlign="Left" DataField="TansferFromLocation"  />
            <asp:BoundField HeaderText="Transfer To Location"  Visible="false"  ItemStyle-HorizontalAlign="Left" DataField="TransferToLocation"   />
            <asp:TemplateField HeaderText="Transfer To Location" >
                    <ItemTemplate>
                       
                        <%# FillLocationName(Eval("TransferToLocation"))%> 
                    </ItemTemplate>
                    
                </asp:TemplateField>
            <asp:BoundField HeaderText="Total Items Transfer"   ItemStyle-HorizontalAlign="Right" DataField="TotalItemsTransfer"   />
              <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right" Visible="true" >
                    <ItemTemplate>
                        <asp:Label ID="lblTotalQty" Text='<%# totalprice(Eval("TransferNumber"))%>' runat="server"></asp:Label>
                        
                    </ItemTemplate>
                </asp:TemplateField>    
           
             <asp:TemplateField HeaderText="Transfer Date"     >
                    <ItemTemplate>
                        <asp:Label ID="lblTransfer" runat="server" Text='<%# Eval("TransferDate","{0:d}") %>'></asp:Label>
                    </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Received Date"     >
                    <ItemTemplate>
                        <asp:Label ID="lblReceived" runat="server" Text='<%# Eval("ReceivedDate","{0:d}") %>'></asp:Label>
                    </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:BoundField HeaderText="Total Items Received"   ItemStyle-HorizontalAlign="Right" DataField="Qty"   />--%>
             <asp:TemplateField HeaderText="Total Items Received" Visible="false"    >
                    <ItemTemplate>
                        <asp:Label ID="lblTotalReceived" Text='' runat="server"></asp:Label>
                       
				    </ItemTemplate>
				     
                </asp:TemplateField>
              <asp:TemplateField HeaderText="Status"  >
                    <ItemTemplate>
                        <asp:Label ID="lblstatus" Text='' runat="server"></asp:Label>
                       
				    </ItemTemplate>
				     
                </asp:TemplateField>
          <asp:TemplateField HeaderText="Cancel Transfer">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgCancel" CommandArgument='<%# Eval("TransferNumber") %>'  
                            OnClientClick="return confirm('Alert! You are about to cancel Transfer Number. \n If you wish to continue, press OK.');"
                            CommandName="CancelOrder" ToolTip="Cancel" ImageUrl="~/Images/un-post.gif"
                            runat="server" />
                    </ItemTemplate>
                    <ItemStyle Width="25px" />
                </asp:TemplateField>
        </Columns>
    </asp:GridView>
</ajax:ajaxpanel>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
<script type="text/javascript" src="assets/global/plugins/select2/select2.min.js"></script>
<script type="text/javascript" src="assets/global/plugins/datatables/media/js/jquery.dataTables.min.js"></script>
<script type="text/javascript" src="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js"></script>
<script type="text/javascript" src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
<script type="text/javascript" src="assets/global/plugins/keyboard/keyboard.js"></script>
<script type="text/javascript"  src="assets/admin/pages/scripts/search.js"></script>

<script>
    jQuery(document).ready(function() {

        Search.init();
    });
</script>

</asp:Content>

