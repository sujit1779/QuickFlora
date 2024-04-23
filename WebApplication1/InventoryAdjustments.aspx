<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="InventoryAdjustments.aspx.vb" Inherits="InventoryAdjustments" %>

<%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Core"
    TagPrefix="core" %>
<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Controls"
    TagPrefix="ctls" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Import Namespace="CrystalDecisions.CrystalReports" %>
<%@ Import Namespace="CrystalDecisions.Shared" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css" />
    <link href="assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet"
        type="text/css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    Adjust Inventory
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">



<style type="text/css" >
	/* Grid */
 
 
  
.searchgrid-row
{
background-color: #FFFFFF;
}

.searchgrid-alternative-row
{
	background-color: #F5F5F5;
}


.boxdiv { 
		
			       visibility : hidden;
	                margin : 0px!important;
	                background-color : inherit;
	                color : windowtext;
	                border : buttonshadow;
	                border-width : 1px;
	                border-style : solid;
	                cursor : 'default';
	                overflow : auto;
	                height : 200px;
                    text-align : left; 
                    list-style-type : none;
                    font-family: Verdana;
                    font-size:8pt;
		 }
		 
		 
		 
 .paging_bootstrap {
    display: none !important
}

        
         div#sample_editable_1_filter
          {
          	display:none;
          }

          div#sample_editable_1_length
           {
          	display:none;
          }
          
          
          div#sample_editable_1_info
           {
          	display:none;
          }
              

</style>


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
        var url = "AjaxInventoryItemsSave.aspx?" + key;
        //alert(url);
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

    function ProcessSave() {

        if (req.readyState == 4) {
            if (req.status == 200) {

                // alert("Items saved Inline Number =" + req.responseText);
                document.getElementById("ctl00_ContentPlaceHolder_reqsaveid").value = "";
                Subtotal();

            }
            else {
                alert("There was a problem saving data:<br>" + req.statusText);
            }
        }

    }


      

    function Subtotal() {
        Initialize();

        var start = 0;
        start = new Date();
        start = start.getTime();

        var url = "AjaxItemsInventoryOrderSubtotal.aspx?InventoryAdjustmentsNumber=" + document.getElementById("ctl00_ContentPlaceHolder_txtOrderNumber").value + "&start=" + start;

        //alert(url);
        if (req != null) {
            req.onreadystatechange = ProcessSubtotal;
            req.open("GET", url, true);
            req.send(null);


        }

    }



    function ProcessSubtotal() {

        if (req.readyState == 4) {
            if (req.status == 200) {

                //alert(req.responseText);
                document.getElementById("ctl00_ContentPlaceHolder_txtSubtotal").value = req.responseText;
                Processtotal();


            }
            else {
                alert("There was a problem updating subtotal.<br>" + req.statusText);
            }
        }

    }


    function Processtotal() {

     

    }

    function Updateitem(key, saveid) {
        Initialize();
        var url = "AjaxInventoryItemsUpdate.aspx?" + key;
        // alert(url);
        //          alert(document.getElementById(saveid));

        document.getElementById("ctl00_ContentPlaceHolder_reqsaveid").value = saveid;

        if (req != null) {
            req.onreadystatechange = ProcessUpdate;
            req.open("GET", url, true);
            req.send(null);


        }

    }

    function ProcessUpdate() {

        if (req.readyState == 4) {
            if (req.status == 200) {

                //alert("Items updated Inline Number =" + req.responseText);
                document.getElementById("ctl00_ContentPlaceHolder_reqsaveid").value = "";
                Subtotal();

            }
            else {
                alert("There was a problem updating data:<br>" + req.statusText);
            }
        }

    }
    function checksubmit() {

        if (document.getElementById("ctl00_ContentPlaceHolder_drpTransaction").value == '') {
            alert("Please select Transaction type!");
            document.getElementById("ctl00_ContentPlaceHolder_drpTransaction").focus();
            return false;
        }

        return true;

    }
         

    function Deleteitem(key, saveid) {
        Initialize();
        var url = "AjaxInventoryItemsDelete.aspx?" + key;

        // alert(url);
        //          alert(document.getElementById(saveid));

        document.getElementById("ctl00_ContentPlaceHolder_reqsaveid").value = saveid;

        if (req != null) {
            req.onreadystatechange = ProcessDelete;
            req.open("GET", url, true);
            req.send(null);


        }

    }

    function ProcessDelete() {

        if (req.readyState == 4) {
            if (req.status == 200) {

                //alert("Items Deleted Inline Number =" + req.responseText);
                document.getElementById("ctl00_ContentPlaceHolder_reqsaveid").value = "";
                Subtotal();

            }
            else {
                alert("There was a problem Delete data:<br>" + req.statusText);
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

    

    function SendQuery2(key) {



        // alert(document.getElementById("ctl00_ContentPlaceHolder_txtOrderNumber").value);
        var start = 0;
        start = new Date();
        start = start.getTime();

        Initialize();
        var url = "AjaxInventoryItemsSearch.aspx?k=" + key + "&start=" + start + "&lc=";  /// + document.getElementById("ctl00_ContentPlaceHolder_txtCustomerTypeID").value;
       // alert(url);
        if (req != null) {
            req.onreadystatechange = Process2;
            req.open("GET", url, true);
            req.send(null);

        }

    }

    function Process2() {
        document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundImage = 'url(ajax-loader-text.gif)';
        document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundRepeat = 'no-repeat';
        document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundPositionX = 'right';


        if (req.readyState == 4) {
            // only if "OK"
            if (req.status == 200) {
                document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundImage = 'url()';
                if (req.responseText == "") {
                    //alert("in blank");  
                    //HideDiv("autocomplete");
                    document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundImage = 'url(searchbackground.jpg)';
                    document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundRepeat = 'no-repeat';
                    document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundPositionX = 'left';
                    document.getElementById("<%=txtitemsearch.ClientID%>").value = "";
                    var newdiv = document.createElement("div");
                    newdiv.innerHTML = "<br><br><div style='text-align:center' >No result found, Please try with some other keyword.</div>";
                    var container = document.getElementById("autocomplete2");
                    container.innerHTML = "<div style='text-align:center' ><a class='btn btn-danger btn-xs'  href='javascript:itemrsearchcloseProcess();' >Close Search result</a></div>";
                    container.appendChild(newdiv)
                }
                else {


                    if (document.getElementById("<%=txtitemsearch.ClientID%>").value != "") {
                        ShowDiv("autocomplete2");
                        //document.getElementById("autocomplete2").innerHTML = req.responseText;

                        var newdiv = document.createElement("div");
                        newdiv.innerHTML = req.responseText;
                        var container = document.getElementById("autocomplete2");
                        container.innerHTML = "<div style='text-align:center' ><a class='btn btn-danger btn-xs'  href='javascript:itemrsearchcloseProcess();' >Close Search result</a></div>";
                        container.appendChild(newdiv)
                    }
                }
            }
            else {
                document.getElementById("autocomplete2").innerHTML = "There was a problem retrieving data:<br>" + req.statusText;
            }
        }
    }


    function FillSearchtextBox22() {
        // alert(val);
        if (document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").value != "") {
            FillSearchtextBox2(document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").value);

        }
    }

    function FillSearchtextBox2_old(val) {
        // alert(val);
        document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").value = val;
        document.getElementById("autocomplete2").innerHTML = "";
        HideDiv("autocomplete2");
        document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").style.backgroundImage = 'url()';
        document.getElementById("ctl00_ContentPlaceHolder_ImgUpdateSearchitems").focus();
        document.getElementById("ctl00_ContentPlaceHolder_ImgUpdateSearchitems").click();
        //ImgUpdateSearchitems
    }



    function FillSearchtextBox2(val) {
       // alert(val);
 
        if (document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").value == "") {

        }
        else {
            var str = val;
            var str1;
            str1 = str.split("~!");

            document.getElementById("ctl00_ContentPlaceHolder_txtitemid").value = str1[0];
            document.getElementById("ctl00_ContentPlaceHolder_txtitemsearchprice").value = str1[1];
            document.getElementById("ctl00_ContentPlaceHolder_txtitemsearchDesc").value = str1[2];
 
            document.getElementById("autocomplete2").innerHTML = "";
            HideDiv("autocomplete2");
            document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").style.backgroundImage = 'url()';
            //alert(val);
            if (val.length > 3) {
                document.getElementById("sample_editable_1_new").focus();
                document.getElementById("sample_editable_1_new").click();
 
                document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").value = "";
                global_item_add = 1;
            }
            // document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").focus();

        }

        var ordernumber = "";
        ordernumber = document.getElementById("ctl00_ContentPlaceHolder_txtOrderNumber").value;


        // alert(ordernumber);

        if (ordernumber == "(New)") {
            // alert(document.getElementById("ctl00_ContentPlaceHolder_ImgUpdateSearchitems"));
            document.getElementById("ctl00_ContentPlaceHolder_ImgUpdateSearchitems").focus();
            document.getElementById("ctl00_ContentPlaceHolder_ImgUpdateSearchitems").click();
            //document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").focus();
            // return;
        }


        itemrsearchcloseProcess();

    }


    function itemrsearchcloseProcess() {

        document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundImage = 'url(searchbackground.jpg)';
        document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundRepeat = 'no-repeat';
        document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundPositionX = 'left';
        document.getElementById("<%=txtitemsearch.ClientID%>").value = "";
        document.getElementById("autocomplete2").innerHTML = "";
        HideDiv("autocomplete2");


    }


    function itemsearchBlurProcess() {

        if (document.getElementById("<%=txtitemsearch.ClientID%>").value == "") {
            HideDiv("autocomplete2");
            document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundImage = 'url(searchbackground.jpg)';
        }

    }

     
</script>     

    <!-- BEGIN PORTLET 1st Block-->
    <div class="portlet box green">
        <div class="portlet-title">
            <div class="caption">
                &nbsp;Inventory Adjustment Details</div>
            <div class="tools">
                <a href="javascript:;" class="collapse"></a>
            </div>
        </div>
        <div class="portlet-body">
            <div class="row">
                <div class="col-md-4">
                    <div class="text-center" style="padding-top: 100px;">
                        <asp:Image ID="ImgRetailerLogo" CssClass="img-rounded" ImageUrl="" runat="server" />
                    </div>
                </div>
                <div class="col-md-4">
                    <!-- BEGIN FORM-->
                    <div class="form-body">
                        <table id="table3" class="table table-striped table-hover table-bordered">
                            <tr id="trtop" runat="server" visible="false">
                                <td width="110" height="12">
                                    Auto Saved At
                                </td>
                                <td width="101" style="background-color: #F6F6F6;" height="12">
                                    <ajax:ajaxpanel id="AjaxPanel1" runat="server">
                                                                    <asp:Label Width="95" BorderWidth="1px" BorderColor="#939393" ID='lblrefersh'    CssClass="txtStyle" Height="18" runat="server" Text=''></asp:Label>
                                                                </ajax:ajaxpanel>
                                </td>
                            </tr>
                            <tr>
                                <td width="110j" height="12">
                                    Adjustment #
                                </td>
                                <td width="101" height="12">
                                    <ajax:ajaxpanel id="AjaxPanel2" runat="server">
                                                                    <asp:Label Width="95"  ID='lblTransactionNumberData'   Height="18" runat="server" Text='(New)'></asp:Label>
                                                                  </ajax:ajaxpanel>
                                </td>
                            </tr>
                            <tr>
                                <td width="110" height="12" valign="middle">
                                    Adjustment Date
                                </td>
                                <td width="101" height="12">
                                    <asp:Label ID="lblTransactionDate" Height="18" Width="95" runat="server" Text=" "></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="110" height="15" valign="middle">
                                    Adjustment Type
                                </td>
                                <td width="101" height="15">
                                    <ajax:ajaxpanel id="AjaxPanel270" runat="server">
                                       <asp:DropDownList CssClass="form-control input-xs" ID="drpTransaction"   runat="server" TabIndex="2">
                                       <asp:ListItem Value="" Text="--Select--" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="ADD" Text="ADD"></asp:ListItem>
                                        <asp:ListItem Value="SUBTRACT" Text="SUBTRACT"></asp:ListItem>
                                        <asp:ListItem Value="DUMP" Text="DUMP"></asp:ListItem>
                                       </asp:DropDownList>
                                     </ajax:ajaxpanel>
                                </td>
                            </tr>
                            <!--location id-->
                            <tr>
                                <td width="110" height="15" valign="middle">
                                    Location
                                </td>
                                <td width="101" height="15">
                                    <ajax:ajaxpanel id="AjaxPanel2710" runat="server">
                                                                <asp:DropDownList ID="cmblocationid" CssClass="form-control input-xs" runat="server"    TabIndex="4">
                                                                </asp:DropDownList>
                                                              </ajax:ajaxpanel>
                                </td>
                            </tr>
                            <!--location id-->
                            <tr>
                                <td width="110" height="15" valign="middle">
                                    Adjust by 
                                </td>
                                <td width="101" height="15">
                                    <asp:DropDownList CssClass="form-control input-xs" ID="drpEmployeeID" runat="server"
                                        TabIndex="3">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                                
                                
                                  
                                    
                        </table>
                    </div>
                    <!-- END FORM-->
                </div>
                <div class="col-md-4">
                    <div class="alert alert-info">
                        System Wide Messages</div>
                    <asp:Label ID="lblSystemWM" Height="160" runat="server" Font-Size="9pt"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <!-- END PORTLET-->
   
    
      <!-- BEGIN PORTLET 3rd Block-->
					<div class="portlet box green">
                    	<div class="portlet-title">
                            <div class="caption" style="width:95%;"> 
                                 <div class="row">
								    <div class="col-md-2">
									    <div class="text-left" >
                                         Items Detail 
                                        </div> 
								    </div>
								    <div class="col-md-5">
                                         <Ajax:AjaxPanel ID="AjaxPanel10" runat="server">
									     <div ID="pnlPricerange" runat="server" class="text-center" >
                                     
                                             <asp:Label ID="lblPricerange" BackColor="#40BD24" ForeColor="white" Font-Bold="true"   runat="server"   Text=""></asp:Label>
                                            <asp:Label ID="lblInventoryStatus" BackColor="#40BD24" ForeColor="red" Font-Bold="true"  runat="server"   Text=""></asp:Label>
                                     
                                         </div> 
                                         </Ajax:AjaxPanel> 
								    </div>
                                    <div class="col-md-5">
									        <div class="text-right" >
                                                 
                                            </div>     
								    </div>
							</div>
                            </div>
							<div class="tools">
                                <a href="javascript:;" class="collapse"></a>
                            </div>
						</div>

                        	<div class="portlet-body form"  >

                         
                   
                                      <div class="form-group-search-block">
                                        <div class="input-group">
                                            <span class="input-group-addon input-circle-left">
                                            <i class="fa fa-search"></i>
                                            </span>
                                               <Ajax:AjaxPanel ID="AjaxPanel12" runat="server">
                                                <asp:TextBox ID="txtitemsearch"  CssClass="form-control input-circle-right"    runat="server"></asp:TextBox>
                                             </Ajax:AjaxPanel> 
                                             <br />
                                             <div align="left" class="box autocomplete" style="visibility: hidden;" id="autocomplete2"   ></div>   
                                           <div id="newedititems"    style="visibility: hidden;" class="box autocomplete" >
                                           ID <asp:TextBox ID="txtitemid"  CssClass="form-control input-sm"   runat="server"></asp:TextBox>
                                           txtitemsearchDesc <asp:TextBox ID="txtitemsearchDesc"  CssClass="form-control input-sm"   runat="server"></asp:TextBox>
                                           txtitemsearchprice <asp:TextBox ID="txtitemsearchprice"  CssClass="form-control input-sm"   runat="server"></asp:TextBox>
                                           <button id="sample_editable_1_new" class="btn green">
                                                Add New <i class="fa fa-plus"></i>
                                            </button>
                                           
                                        </div>
                                            
                                        </div>
                                    </div>


                                      <div class="row">
								        <div class="col-md-12"  >
                                             <Ajax:AjaxPanel ID="AjaxPanel13" runat="server">
                                                <asp:ImageButton ID="ImgUpdateSearchitems" OnClientClick="javascript:FillSearchtextBox22();"  ToolTip="Update Item" ImageUrl="~/images/2-sh-stock-in.gif" Width="10"    runat="server"  />  
                                                <asp:Label ID="lblitemsearchmsg" runat="server" Text=""></asp:Label>
                                            </Ajax:AjaxPanel> 
                                        </div>
								 
							        </div>
  
                                    
                                        
                                       
                                      
                                       
  
                                      
  
                                    <div id="dvitems"  class="row">
								        <div class="col-md-12"    >
                          
                           <table class="table table-striped table-hover table-bordered dataTable tableRecord"   id="sample_editable_1">
							 <thead>
                                <tr>
                                    
                                    <th>
                                        Item ID 
                               
                                    </th>
                                    <th>
                                        Item Name 
                                    </th>
                                    
                                    <th>
                                        Qty 
                                    </th>
                                    
                                     <th>
                                         Average Cost 
                                    </th>
                                    <th>
                                        Total 
                                    </th>
                                    <th>
                                        Edit
                                    </th>
                                    <th>
                                        Delete
                                    </th>
                                    <th>
                                
                                    </th>
                                </tr>
                            </thead>
							<tbody runat="server" id="tbody" >
							    
                            </tbody> 
                            </table> 

                                        </div>
								 
							        </div>
                                    
                                      
                                        <div style="display:none" >
                                            
                                            <Ajax:AjaxPanel ID="AjaxPanel104" runat="server">
                                            
                                            <asp:TextBox ID="txtfirst"   Text="1" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="reqsaveid"   Text="" runat="server"></asp:TextBox>
                                                                       
                                            <asp:TextBox ID="txtOrderNumber" Width="200px"   Font-Size="12"  Text=""  Height="28" runat="server"></asp:TextBox>
                                           </Ajax:AjaxPanel>
                                        </div>
                                        
                
                        
                                        <Ajax:AjaxPanel ID="AjaxPanel14" runat="server">
                                           <asp:Label ID="lblErrorText" TabIndex="63" runat="server" Visible="false"></asp:Label>
                                        </Ajax:AjaxPanel> 

                                        </div>


                         


                                 
                    </div>
      <!-- END PORTLET-->
      
      
      <!-- Begin 4th Block -->
      
      
                  <div class="row">
					<div class="col-md-12">
                    <!-- BEGIN PORTLET-->
					<div class="portlet box green">
                    	<div class="portlet-title">
                    	
							<div class="caption" style="width: 95%;">
                        <div class="row">
                            <div class="col-md-6">
							<div class="text-left">
							    
							</div> 
							</div> 
							<div class="col-md-6">
                                <div class="text-right">
                                    Total
                                </div>
                            </div>
                         </div>
							
							
							 </div> 
							
							
							<div class="tools"><a href="javascript:;" class="collapse"></a></div>
							
						</div>
						<div class="portlet-body">
							<div class="row">
								<div class="col-md-4">
									<!-- BEGIN FORM-->
                                        <div class="form-body">
                                              <table class="table table-striped table-hover table-bordered">
                                               
                                                  <tr>
                                                    <td colspan="2"  >Internal Notes</td>
                                                  </tr>
                                                  <tr>
                                                    <td colspan="2" >
                                                        <asp:TextBox ID="txtInternalNotes"   TextMode="MultiLine"   Rows="3"  CssClass="form-control input-xs"    runat="server"></asp:TextBox>
                                                         
                                                    </td>
                                                  </tr>
                                                 
                                                </table>  
                                        
                                        </div>
                                    <!-- END FORM-->
								</div>
                                <div class="col-md-4">
									<!-- BEGIN FORM-->
                                        <div class="form-body">
                                         <Ajax:AjaxPanel ID="AjaxPanel20" runat="server">
                                         
                                             <table class="table table-striped table-hover table-bordered">
                                                <tr>
                                                    <td colspan="2" class="text-center">
                                                        <br />
                                                        <br />
                                                        <br />
                                                       
                                                      </td>
                                                    
                                                  </tr>
                                                  <tr>
                                                    <td colspan="2" class="text-center">
                                                        <asp:Button ID="btnsave" OnClientClick="return checksubmit();"     CssClass="btn btn-success btn-xs" runat="server"  Text="Adjust" Width="150" ></asp:Button>
                                                    </td>
                                                    
                                                  </tr>
                                                  
                                                 
                                                </table> 

                                        </Ajax:AjaxPanel> 
                                        </div>
                                    <!-- END FORM-->
								</div>
								<div class="col-md-4">
									<!-- BEGIN FORM-->
                                        <div class="form-body">
                                         <Ajax:AjaxPanel ID="AjaxPanel21" runat="server">
                                          <table class="table table-striped table-hover table-bordered">
                                            <tr class="total-foot" >
                                            <td style="width:50%" class="text-right"> Grand Total</td>
                                            <td style="width:50%"><asp:TextBox ID="txtSubtotal" runat="server" Enabled="True" ReadOnly="True" Text="0.00"  CssClass="form-control input-xs"   ></asp:TextBox> </td>
                                            </tr>
 
                                               
                                             
                                            </table>  
                                        </Ajax:AjaxPanel> 
                                        </div>
                                    <!-- END FORM-->
								</div>
							</div>

						</div>
					</div>
                    <!-- END PORTLET-->
					</div>
				</div>
<!-- END PORTLET-->
    
         
      
    
</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

<script type="text/javascript" src="assets/global/plugins/select2/select2.min.js"></script>
<script type="text/javascript" src="assets/global/plugins/datatables/media/js/jquery.dataTables.min.js"></script>
<script type="text/javascript" src="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js"></script>

<script type="text/javascript" src="assets/global/plugins/keyboard/keyboard.js"></script>
<script type="text/javascript" src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
<script type="text/javascript" src="assets/admin/pages/scripts/search.js"></script>


<script type="text/javascript" src="assets/plugins/data-tables/jquery.dataTables.js"></script>
<script type="text/javascript" src="assets/plugins/data-tables/DT_bootstrap.js"></script>


<script type="text/javascript" src="assets/scripts/table-editable3.js"></script>

<script type="text/javascript" >
    jQuery(document).ready(function () {

        TableEditable.init();
        Search.init();
    });
</script>

</asp:Content>


