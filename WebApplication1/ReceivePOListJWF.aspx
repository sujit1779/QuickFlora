<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="ReceivePOListJWF.aspx.vb" Inherits="ReceivePOList" %>

<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Core" TagPrefix="core" %>
<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Controls" TagPrefix="ctls" %>
 <%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css"/>
<link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css"/>

<link rel="stylesheet" type="text/css" href="assets/global/plugins/keyboard/keyboard.css"/>
    
<style>
    /**================================== @ JAY 13 JULY 2020*/ 
    ._tableFixHead {overflow-y: auto;height: 700px;}
        ._tableFixHead thead th, ._tableFixHead tbody th { position: -webkit-sticky; /* Safari */ position: sticky; top:-1px;background-color: #C6DBAD; z-index: 2;}
    <%=itemsearchcss%>
.autocomplete_item {
  overflow-x: hidden;
  overflow-y: hidden;
  height: 400px;
  position: absolute;
  z-index: 3;
  width: 80%;
  background-color: #fff;
  box-shadow: 5px 5px rgba(102, 102, 102, 0.1);
  border: 2px solid #40BD24;
  top: 33px;
  visibility: hidden;
}
    /*body{
             font-size:16px !important;
             color:#000000;
        }
        label{
            font-size:16px !important;
            color:#000000;
        }
        .input-sm
        {
            font-size:16px !important;
            color:#000000;
        }
        .input-xs
        {
            font-size:16px !important;
            color:#000000;
        }
        .select.input-xs
        {
            font-size:16px !important;
            color:#000000;
        }*/

		.fixed_headers {
			width: 100%;
			table-layout: fixed;
			border-collapse: collapse;
		}
		.fixed_headers th {
			text-decoration: underline;
		}
		.fixed_headers th,
		.fixed_headers td {
			padding: 5px;
			text-align: left;
		}

		.fixed_headers td:nth-child(2),
		.fixed_headers th:nth-child(2) {
			min-width: 120px;
		}
		.fixed_headers td:nth-child(3),
		.fixed_headers th:nth-child(3) {
			min-width: 250px;
		}
		.fixed_headers td:nth-child(4),
		.fixed_headers th:nth-child(4) {
			width: 100px;
		}
		
		.fixed_headers thead {
			background-color: #678B38;
			color: #fdfdfd;
			font-size:12px;
		}
		.fixed_headers thead tr {
			display: block;
			position: relative;
		}
		.fixed_headers tbody {
			display: block;
			overflow: auto;
			width: 100%;
			height: 400px;
		}
		.fixed_headers tbody tr:nth-child(even) {
			/*background-color: #FFFFF0;*/
		}
		.old_ie_wrapper {
			height: 400px;
			width: 100%;
			overflow-x: hidden;
			overflow-y: auto;
		}
		.old_ie_wrapper tbody {
			height: auto;
		}

        .autocomplete_item
        {
            overflow-x: auto !important;
			overflow-y: auto !important;
        }
        div.dataTables_length label{
            float:right;
        }
        div.dataTables_paginate{
            float:left;
            display:block!important;
        }
        .preview_desc{
              display: none;
              position: absolute;
              background: #eaeaea;
              Max-height: 130px;
              width: 250px;
              font-size: 12px;
              padding: 3px 7px 3px;
              text-align: justify;
              overflow: hidden;
              /*white-space: nowrap;*/
              overflow: hidden;
              text-overflow: ellipsis;

        }
        #autocomplete2{
            height:400px!important;
        }
        .autocomplete{
            z-index:3!important;
        }
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
	<h3 class="page-title">
			Receiving 
			</h3>
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
        var url = "AjaxPOSearch.aspx?k=" + key;
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
  
 
<script type="text/javascript" language="javascript">
    function checkDate() {


        var criteria = document.getElementById("ctl00__mainContent_drpFieldName").value;
        if (criteria == "OrderShipDate") {
            var dtvalue = document.getElementById("ctl00__mainContent_txtSearchExpression").value;
            //  alert(dtvalue);
            if (!isDate(dtvalue)) {
                alert("Invalid Date");
            }
        }
    }
</script>

 

  
  <ajax:AjaxPanel id="AjaxPanel3" runat="server">
      
      <asp:Panel ID="pnlgrid" runat="server"  Visible="true"  >    
      
      
      <!-- BEGIN PORTLET-->
					<div class="portlet box green">
						<div class="portlet-title">
							<div class="caption">Search Options</div>
							<div class="tools"><a href="javascript:;" class="collapse"></a></div>
						</div>
						<div class="portlet-body" >
                                <div class="row">
                                    <div class="col-md-6">
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
                                           <!-- <div class="row form-group">
                                                <label class="col-md-3 control-label">Date Range :</label>
                                                <div class="col-md-9">
                                                    <div class="row form-group">
                                                        

                                                        <div class="col-xs-6 col-md-6">
                                                            <label class="control-label"><small>FROM</small></label>
                                                            <div class="input-icon">
                                                                <i class="fa fa-calendar"></i>
                                                                <input class="form-control input-sm date-picker" size="16" type="text" value="12-02-2012" data-date="12-02-2012" data-date-format="dd-mm-yyyy" data-date-viewmode="years"/>
                                                            </div>
                                                        </div>
                                                        <div class="col-xs-6 col-md-6">
                                                            <label class="control-label"><small>TO</small></label>
                                                            <div class="input-icon">
                                                                <i class="fa fa-calendar"></i>
                                                                <input class="form-control input-sm date-picker" size="16" type="text" value="12-02-2012" data-date="12-02-2012" data-date-format="dd-mm-yyyy" data-date-viewmode="years"/>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>-->
                                    </div>
                                    <div class="col-md-6">
                                        <div class="row form-group">
                                            <label class="col-md-3 control-label">Search By</label>
                                            <div class="col-md-9">
                                            <div class="radio-list">
                                                     <table>
                                                        <tr>
                                                            <td><asp:RadioButton ID="rdAllDates" Checked="true" GroupName="Dates" Text="All Dates"    runat="server" /></td>
                                                            <td>&nbsp;</td>
                                                            <td><asp:RadioButton ID="rdOrderDates"  GroupName="Dates" Text=" PO Date"   runat="server" /></td>
                                                            <td>&nbsp;</td>
                                                            <td><asp:RadioButton ID="rdDeliveryDates"  GroupName="Dates" Text="Arrival Date"   runat="server" /></td>
                                                        </tr>
                                                     
                                                     </table>
                                                     
                                                    
                                                    

                                                </div>
                                                
                                            </div>
                                    	</div>
                                    </div>
                                </div>
                            <div style="text-align:right">
                                <asp:Button ID="search" runat="server" Text="Search" CssClass="btn btn-primary" />
                            </div>
                                <div id="dvse"  runat="server" visible="false" class="row">
                                    <div class="col-xs-4 col-md-2">
                                    		<div class="form-group">

                                                

                                                <asp:DropDownList ID="drpFieldName"  CssClass="form-control input-sm select2me" runat="server">
                                                        <asp:ListItem Value="VendorName">Vendor Name</asp:ListItem>
                                                         <asp:ListItem Value="TransactionTypeID">Transaction Type</asp:ListItem>                                     
                                                        <asp:ListItem Selected="True" Value="PurchaseNumber">PO Number</asp:ListItem>                    
                                                                      
                                                        
                                                        <asp:ListItem Value="Total">Total</asp:ListItem>
                                                                         
                                                 </asp:DropDownList>	

                                            </div>
                                     </div>
                                     <div class="col-xs-3 col-md-1">
                                     		<div class="form-group">
                                             
                                            <asp:DropDownList ID="drpCondition" CssClass="form-control input-sm select2me" runat="server">
                                                <asp:ListItem Value="Like">Like</asp:ListItem>
                                                <asp:ListItem Value=">">&gt;</asp:ListItem>
                                                <asp:ListItem Value="<">&lt;</asp:ListItem>
                                                <asp:ListItem Value="=">=</asp:ListItem>
                                            </asp:DropDownList>	

                                            </div>
                                     </div>
                                     <div class="col-xs-5 col-md-3">
                                     		<div class="form-group">
                                             
                                                 <asp:TextBox ID="txtSearchExpression"  CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                          	</div>
                                    </div>
                                    <div class="col-xs-12 col-md-6">
                                    	<div class="form-group">
                                         
                                            <asp:Button ID="btnSearch" CssClass="btn blue btn-block" runat="server" Text="SEARCH" />

                                        </div>
                                        <asp:Label ID="lblErr" runat="server" Text=" "></asp:Label>
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
                           
                              <div class="note note-success margin-bottom-0">




                                  <div class="row">
                                      <div class="col-xs-2">
                                          Payment Method 
                                             

                                            <asp:DropDownList AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control input-sm" ID="Payment" runat="server">
                                                <asp:ListItem Text="--Please Select Payment--" Value=""></asp:ListItem>
                                            </asp:DropDownList>



                                      </div>
                                      <div class="col-xs-2">
                                          Delivery Method 
                                             

                                             <asp:DropDownList ID="Delivery" AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control input-sm" runat="server">
                                                 <asp:ListItem Text="--Please Select Delivery--" Value=""></asp:ListItem>
                                                 <asp:ListItem Text="Delivery" Value="Delivery"></asp:ListItem>
                                                 <asp:ListItem Text="Ship" Value="Ship"></asp:ListItem>
                                             </asp:DropDownList>


                                      </div>


                                      <div class="col-xs-3">
                                          Location 
                                            

                                             <asp:DropDownList ID="cmblocationid" CssClass="form-control input-sm" runat="server" AppendDataBoundItems="true" AutoPostBack="true">
                                                 <asp:ListItem Text="--All Locations--" Value=""></asp:ListItem>
                                             </asp:DropDownList>



                                      </div>
                                      <div class="col-xs-3">
                                          Trans. Type 
                                         <asp:DropDownList CssClass="form-control input-sm" ID="drpPurchaseTransaction" AutoPostBack="true" runat="server" TabIndex="2">
                                             <asp:ListItem Text="--All Type--" Value=""></asp:ListItem>
                                             <asp:ListItem Value="POF" Text="POF- Fresh"></asp:ListItem>
                                             <asp:ListItem Value="POF-Prebook" Text="POF-Prebook"></asp:ListItem>
                                             <asp:ListItem Value="POF-Standing" Text="POF-Standing"></asp:ListItem>
                                             <asp:ListItem Value="POH" Text="POH- Hard goods"></asp:ListItem>
                                             <asp:ListItem Value="POH-Prebook" Text="POH-Prebook"></asp:ListItem>
                                             <asp:ListItem Value="POH-Standing" Text="POH-Standing"></asp:ListItem>

                                             <asp:ListItem Value="POFS" Text="POFS-PO Fresh standing"></asp:ListItem>

                                         </asp:DropDownList>
                                      </div>
                                      <div class="col-xs-2">
                                          <%--<table>
                                                <tr>
                                                    <td>--%> Status 
                                            <asp:DropDownList ID="drpstatus" CssClass="form-control input-sm" runat="server" AppendDataBoundItems="true" AutoPostBack="true">
                                                <asp:ListItem Text="--All Status--" Value=""></asp:ListItem>
                                                <asp:ListItem Text="Booked" Value="Booked"></asp:ListItem>
                                                <asp:ListItem Text="Bought" Value="Bought"></asp:ListItem>
                                                <asp:ListItem Text="Received" Value="Received"></asp:ListItem>
                                                <asp:ListItem Text="Partial Received" Value="Partial Received"></asp:ListItem>
                                                <asp:ListItem Text="Not Booked" Value="Not Booked"></asp:ListItem>
                                                <asp:ListItem Text="Canceled" Value="Canceled"></asp:ListItem>
                                            </asp:DropDownList>
                                      </div>
                                   </div>
                                   <div class="row" style="margin-top:10px;">
                                      <div class="col-xs-3">
                                          Occasion Code 
                                        <asp:DropDownList ID="drpOccasionCode" runat="server" CssClass="form-control input-sm"  AppendDataBoundItems="true" AutoPostBack="true">
                                                            </asp:DropDownList>
                                      </div>
                                      <div class="col-xs-3">
                                          <%--</td>
                                               <td>&nbsp;&nbsp;</td>
                                                    <td>--%>
                                                          PO  Vendor
                                                <asp:DropDownList ID="drpvendor" CssClass="form-control input-sm" AppendDataBoundItems="true" AutoPostBack="true" runat="server">
                                                    <asp:ListItem Text="--All Vendor--" Value=""> </asp:ListItem>
                                                </asp:DropDownList>
                                          <%--</td>
                                                </tr>
                                    		</table> --%>
                                      </div>
                                      <div class="col-xs-3">
                                          Vendor Invoice #
                                          <asp:TextBox ID="InvoiceNo"  CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                        </div>
                                      <div class="col-xs-3">
                                          MAWB Number
                                          <asp:TextBox ID="mawbNo"  CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                        </div>

                                  </div>

                                  <%--<div class="row">
							
                                      	<div class="col-xs-2">
                                    	 
                                             Product Family
                                               <asp:DropDownList AutoPostBack="true" AppendDataBoundItems="true" ID="drpItemFamilyID1"   CssClass="form-control input-sm"    runat="server">
                                                    <asp:ListItem Text="--Please Select--" Value=""></asp:ListItem>
                                                </asp:DropDownList> 

                                    	 
                                      	 
                                    </div>
                                      	<div class="col-xs-2">
                                              Product Category 
                                                <asp:DropDownList ID="drpItemCategoryID1" AutoPostBack="true"    CssClass="form-control input-sm"  runat="server"  >
                                                            <asp:ListItem Text="--Please Select--" Value=""></asp:ListItem>
                                                 </asp:DropDownList> 
                                                </div>

							        </div>--%>
							   </div> 
                            
                           
                                
                            
                            <asp:Label ID="Debug" runat="server" Text=""></asp:Label>
                           
                            
                           
                            <div style="display:none;" class="note note-success margin-bottom-0">
								 
								<div class="row">
                                	<div class="col-xs-3">
                                    	<div class="row">
                                            <label class="col-md-4 control-label text-right">All Orders</label>
                                            <div class="col-md-8 text-left">
                                           
                                            <asp:RadioButton ID="chkall" GroupName="ordertype"   Checked="true" AutoPostBack="true" runat="server" />

                                    	</div>
                                      	</div>
                                    </div>
                                    <div class="col-xs-3">
                                    		<div class="row">
                                            <label class="col-md-4 control-label text-right">Hold Orders</label>
                                            <div class="col-md-8 text-left">
                                              <asp:RadioButton ID="chkhold" GroupName="ordertype"   CssClass="radio-inline"  runat="server"    AutoPostBack="true"  />
                                                                    
                                            </div>
                                        	</div>
                                    </div>
                                    <div class="col-xs-3">
                                    		<div class="row">
                                            <label class="col-md-4 control-label text-right">Return Orders</label>
                                            <div class="col-md-8 text-left">
                                                 <asp:RadioButton ID="checkreturn" GroupName="ordertype"  CssClass="radio-inline" runat="server"   AutoPostBack="true"  />
                                            </div>
                                        	</div>
                                    </div>
                                     <div class="col-xs-3">
                                    		<div class="row">
                                            <label class="col-md-4 control-label text-right">Website/Mobile</label>
                                            <div class="col-md-8 text-left">
                                                <asp:RadioButton ID="chkcartorders"  GroupName="ordertype"  CssClass="radio-inline"    runat="server"     AutoPostBack="true"  />                                   
                                            </div>
                                        	</div>
                                    </div>
                                </div>
							</div>
                            
						</div>
					</div>
				<!-- END PORTLET-->
           <div class="portlet box green">
              <div class="portlet-title">
                  <div class="caption">Item Search</div>
                  <div class="tools"><a href="javascript:;" class="collapse"></a></div>
              </div>
              <div class="portlet-body">
                  <%-- Item Search --%>

                  <div class="note note-success margin-bottom-0">
                      <div class="input-group">
                          <table id="tbpc" runat="server">

                              <tr>
                                  <%--<td>Product Family </td>--%>
                                  <td>&nbsp; </td>
                                  <td>
                                      <label>Product Family</label>
                                      <asp:DropDownList  AutoPostBack="true" ID="drpProductFamily" runat="server" CssClass="form-control"></asp:DropDownList>
                                  </td>
                                  <td>&nbsp; </td>
                                  <td>&nbsp; </td>
                                  <%--<td> </td>--%>
                                  <td>&nbsp; </td>
                                  <td>
                                      <label>Product Category</label>
                                      <asp:DropDownList  AutoPostBack="true"  ID="ctl00_ContentPlaceHolder_drpProductCategory" runat="server" CssClass="form-control"></asp:DropDownList>

                                  </td>
                                  <td>&nbsp; </td>
                                  <td>&nbsp; </td>
                                  <%--<td>Product Size </td>--%>
                                  <td>&nbsp; </td>
                                  <td>
                                      <label>Product Group</label>
                                      <asp:DropDownList  AutoPostBack="true" ID="drpGrp" runat="server" CssClass="form-control"></asp:DropDownList>

                                  </td>
                                  <td>&nbsp; </td>
                                  <td>&nbsp; </td>
                                  <%--<td>Product Color </td>--%>
                                  <td>&nbsp; </td>
                                  <td>
                                      <label>Product Color</label>
                                      <input type="text" class="form-control" id="itemColor" runat="server" />

                                  </td>
                                  <td>&nbsp; </td>
                                  <td>&nbsp; </td>
                                  <%--<td>Product Size </td>--%>
                                  <td>&nbsp; </td>
                                  <td>
                                      <label>Product Size</label>
                                      <input type="text" class="form-control" id="itemSize" runat="server" />

                                  </td>
                              </tr>
                          </table>
                          <br />
                      </div>

                      <div class="input-group">
                          <span class="input-group-addon input-circle-left"><i class="fa fa-search"></i></span>
                          <ajax:AjaxPanel ID="AjaxPanel12" runat="server">
                              <asp:TextBox ID="txtitemsearch" CssClass="form-control input-circle-right" runat="server" data-tab-priority="28" placeholder="Search Product" autocomplete="off"></asp:TextBox>
                          </ajax:AjaxPanel>
                          <br />
                          <div align="left" class="box autocomplete_item" style="visibility: hidden; overflow-y: hidden !important; overflow-x: auto !important; overflow-x: auto  !important; height: 400px!important;" id="autocomplete2">
                          </div>
                      </div>
                  </div>

                  <div style="text-align: right">
                      <asp:Button ID="Search2" runat="server" Text="Search" CssClass="btn btn-primary" />
                  </div>
              </div>
          </div>
        
     
           <div class="_tableFixHead">
 
 <asp:GridView ID="OrderHeaderGrid" AllowSorting="True" runat="server" DataKeyNames="PurchaseNumber"  CssClass="table table-bordered table-striped table-condensed flip-content"     AutoGenerateColumns="false" AllowPaging="True" PageSize="200">
  
            <Columns>
                <asp:TemplateField HeaderText="Receive" >
                <ItemTemplate>
                   <asp:ImageButton ID="imgEdit" Visible='<%# getEditVisible(Eval("OrderStatus")) %> '  ToolTip="Edit" ImageUrl="~/Images/edit.gif" runat="server" CommandName="Edit" />
                </ItemTemplate>
                    
                </asp:TemplateField>
                    <asp:TemplateField HeaderText="Issue Transfer" >
                        <ItemTemplate>
                           <asp:ImageButton ID="imgTransfer"    ToolTip="Transfer" ImageUrl="~/Images/transfer_right_left.png" runat="server" CommandName="Update" />
                        </ItemTemplate>
                     </asp:TemplateField>
                  
                
                 <asp:TemplateField HeaderText="PO #" SortExpression="PurchaseNumber">
                    <ItemTemplate>
                       <asp:Label ID="lblstrik" Text='*' ForeColor="Orange" Visible="false" runat="server"></asp:Label>
                        <asp:Label ID="lblOrderNumber" Text='<%#Eval("PurchaseNumber")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                     
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Vendor ID"  SortExpression="VendorID">
                    <ItemTemplate>
                        <asp:Label ID="lblCustomerID" Text='<%#Eval("VendorID")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                     
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Vendor Name"  SortExpression="VendorName">
                    <ItemTemplate>
                        <asp:Label ID="lblCustomerFirstName" Text='<%#Eval("VendorName")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                    
                </asp:TemplateField>
                
                
                <asp:TemplateField HeaderText="Arrival . Date"  >
                    <ItemTemplate>
					   <asp:Label ID="lblArrival" Text='<%#Eval("OrderShipDate") %>' runat="server"></asp:Label>
				    </ItemTemplate>
				     
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Total" SortExpression="Total">
                    <ItemTemplate>
					    <%#String.Format("{0:N2}", Eval("Total"))%>
				    </ItemTemplate>
				     
                </asp:TemplateField>
                
                
                
                <asp:TemplateField HeaderText="Payment"  SortExpression="PaymentMethodID">
                    <ItemTemplate>
					    <%#Eval("PaymentMethodID")%>
				    </ItemTemplate>
				     
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Delivery Method"  SortExpression="ShipMethodID">
                    <ItemTemplate>
                        <asp:Label ID="lblShipMethodID" Text='<%#Eval("ShipMethodID")%>' runat="server"></asp:Label>

				    </ItemTemplate>
				    
                </asp:TemplateField>
                
                
                  
                
                   <asp:TemplateField HeaderText="Posted" SortExpression="Posted">
                    <ItemTemplate>
                       <asp:Label ID="lblOrderStatus" Text='<%#GetStatus(Eval("Posted"))%>' runat="server"></asp:Label>
                       <%-- <%#Eval("OrderStatus")%>--%>
				    </ItemTemplate>
				     
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Status" SortExpression="OrderStatus">
                    <ItemTemplate>
                        <asp:Label ID="lblOrderStatus1" Text='<%#Eval("OrderStatus")%>' runat="server"></asp:Label>
                       <%-- <%#Eval("OrderStatus")%>--%>
				    </ItemTemplate>
				     
                </asp:TemplateField>
                
                
                <asp:TemplateField HeaderText="Received By" >
                    <ItemTemplate>
                       
                        <%# (Eval("ReceivedBy"))%> 
                    </ItemTemplate>
                    
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Location" >
                    <ItemTemplate>
                       
                        <%# FillLocationName(Eval("LocationID"))%> 
                    </ItemTemplate>
                    
                </asp:TemplateField>                
                
                <asp:TemplateField HeaderText="Vendor Invoice #" >
                    <ItemTemplate>
                       
                         <%#Eval("VendorInvoiceNumber")%>
                    </ItemTemplate>
                    
                </asp:TemplateField>                
                
                
                <asp:TemplateField HeaderText="MAWB No" >
                    <ItemTemplate>
                       
                         <%#Eval("MABWNumber")%>
                    </ItemTemplate>
                    
                </asp:TemplateField>    
                
            </Columns>
        </asp:GridView>
  
    </div> 


           </asp:Panel>

   <asp:Panel ID="pnlconfirm" runat="server" Visible="false"   >
           <div style="text-align:center;"> <b>Order Number : <asp:Label ID="lblordernumber" runat="server" Text="Label"></asp:Label> </b> 
           <input type="hidden" name="HD_Ordernumber" id="HD_Ordernumber"   runat="server" value="" />
           </div>
            <table border="0" width="775" id="table8" align=center cellspacing="0" cellpadding="0">
                                              <tr bgcolor="#456037">
                                                    <td colspan="5" width="775" height="4">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="176" align="center">
                                                        &nbsp;Reason for cancel
                                                    </td>
                                                    <td width="1" bgcolor="#808080">
                                                    </td>
                                                    <td width="450" align="center" valign="top">
                                                        <asp:Label ID="lblmessage" runat="server" ForeColor=red Text=""></asp:Label>
                                                    <br />
                                                        <asp:TextBox ID="txtcancelDesc" TextMode="MultiLine" Width="400" Height="100"
                                                            runat="server"></asp:TextBox>
                                                    <br />
                                                    </td>
                                                    <td width="1" bgcolor="#808080">
                                                    </td>
                                                    <td width="147" valign="top">
                                                        &nbsp;&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                                <tr bgcolor="#456037">
                                                    <td colspan="5" width="775" height="4">
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td width="176" align="center">
                                                        &nbsp;Employee Name
                                                    </td>
                                                    <td width="1" bgcolor="#808080">
                                                    </td>
                                                    <td width="450" align="center" valign="top">
                                                    <asp:TextBox ID="txtemployee"  Width="400" 
                                                            runat="server"></asp:TextBox>
                                                    <br />
                                                    </td>
                                                    <td width="1" bgcolor="#808080">
                                                    </td>
                                                    <td width="147" valign="top">
                                                        &nbsp;&nbsp;&nbsp;
                                                    </td>
                                                </tr>   
                                              <tr bgcolor="#456037">
                                                    <td colspan="5" width="775" height="4">
                                              
                                                    </td>
                                                </tr> 
                                              <tr  >
                                                    <td colspan="5" width="775" height="4">
                                                     <asp:Panel ID="pnlemv"  Visible=false  runat="server">
                                                     <center>
                                                         <asp:Label ID="lblCCMessage" runat="server" Text=""></asp:Label>
                                                         <br />
                                                         Aprroval Status : <asp:TextBox ID="txtCheck" runat="server"></asp:TextBox>
                                                         Aprroval Number : <asp:TextBox ID="txtID" runat="server"></asp:TextBox>
                                                     </center>
                                                     <br />
                                                     <br />
                                                     <div id="autocompletePayment" ></div>
                                                     <br />
                                                        <center >
                                                         <asp:Button ID="btnemv" runat="server"  Text="EMV Payment Request" />
                                                                                    &nbsp;<br />
                                                         <asp:Button ID="btnemvres" runat="server"  Text="EMV Payment Respose" />
                                                         </center>
                                                        </asp:Panel>  
                                                    </td>
                                                </tr> 
                                                                                                                                            
                                                <tr>
                                                    <td width="176" align="center">
                                                        &nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td width="1" bgcolor="#808080">
                                                    </td>
                                                    <td width="450" valign="top">
                                                        <table border="0" width="450" id="table9" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td width="20" height="8">
                                                                </td>
                                                                <td width="210" height="8">
                                                                </td>
                                                                <td width="201" height="8">
                                                                </td>
                                                                <td width="19" height="8">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="20" height="12">
                                                                </td>
                                                                <td width="411" align="center" colspan="3" height="12">
                                                                    <asp:Button CssClass="actionbutton" ID="btnProcess" runat="server" Text="Confirm for order Cancel" />
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;
 <asp:Button ID="btnwindoprint" CssClass="actionbutton" Visible="false"  runat="server" Text="Print To Receipt Printer" />
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;                                                                    
                                                                    <asp:Button CssClass="actionbutton" ID="btncancel" runat="server" Text="Back" />
                                                                </td>
                                                                <td width="19" height="12">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="20" height="8">
                                                                </td>
                                                                <td width="210" height="8">
                                                                </td>
                                                                <td width="201" height="8">
                                                                </td>
                                                                <td width="19" height="8">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="1" bgcolor="#808080">
                                                    </td>
                                                    <td width="147" valign="top">
                                                        &nbsp;&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                                <tr bgcolor="#456037">
                                                    <td colspan="5" width="775" height="4">
                                                    </td>
                                                </tr>
                                            </table>
        
        
        </asp:Panel>
 
  </ajax:AjaxPanel> 

   




  <script type="text/javascript">


      var reqPayment;

      function InitializePayment() {
          try {
              reqPayment = new ActiveXObject("Msxml2.XMLHTTP");
          }
          catch (e) {
              try {
                  reqPayment = new ActiveXObject("Microsoft.XMLHTTP");
              }
              catch (oc) {
                  reqPayment = null;
              }
          }

          if (!reqPayment && typeof XMLHttpRequest != "undefined") {
              reqPayment = new XMLHttpRequest();
          }

      }

      function SendQueryPayment(key) {
          InitializePayment();
          var start = 0;
          start = new Date();
          start = start.getTime();
          var url = "AjaxOrderPaymentStatus.aspx?k=" + key + '&start=' + start;
          //alert(url);
          if (reqPayment != null) {
              reqPayment.onreadystatechange = ProcessPayment;
              reqPayment.open("GET", url, true);
              reqPayment.send(null);

          }

      }



      function ProcessPayment() {

          //alert('In Payment process Main');
          if (reqPayment.readyState == 4) {

              //alert('In Payment process Main 4');
              // only if "OK"
              if (reqPayment.status == 200) {

                  //alert('In Payment process Main 200');

                  //alert(reqPayment.responseText);
                  //alert(document.getElementById("ctl00_ContentPlaceHolder_txtCheck"));  
                  if (trim(reqPayment.responseText) == "") {
                      // alert("in blank");  
                      //HideDiv("autocomplete");
                      document.getElementById("<%=txtCheck.ClientID%>").value = "";

                      var newdiv = document.createElement("div");
                      newdiv.innerHTML = "<br><div style='text-align:center;font-size:18px; color:Red;' >your Payment request processing.......</div>";
                      var container = document.getElementById("autocompletePayment");

                      container.appendChild(newdiv)
                      //calldoPollTimePayment();
                      var pollHand = setTimeout(calldoPollTimePayment, 10000);
                  }
                  else {

                      //alert("in not blank");  

                      document.getElementById("<%=txtCheck.ClientID%>").value = reqPayment.responseText;
                      var container = document.getElementById("autocompletePayment");



                      if (window.document.getElementById("<%=txtCheck.ClientID%>").value == "Approved") {
                          //alert("Approved");
                          container.innerHTML = "<div style='text-align:center;font-size:18px; color:Green;' >Payment Process Result " + reqPayment.responseText + " </div>";
                          window.document.getElementById("<%=btnemvres.ClientID%>").focus();
                          window.document.getElementById("<%=btnemvres.ClientID%>").click();
                          //BtnBookOrder

                      }
                      else {
                          container.innerHTML = "<div style='text-align:center;font-size:18px; color:Red;' >Payment Process Result " + reqPayment.responseText + " </div>";

                      }

                  }
              }
              else {
                  document.getElementById("autocompletePayment").innerHTML = "There was a problem retrieving data:<br>" + req.statusText;

              }
          }
      }



      function calldoPollTimePayment() {
          //alert('In Payment process');

          if (window.document.getElementById("<%=txtCheck.ClientID%>") != null) {
              if (window.document.getElementById("<%=txtCheck.ClientID%>").value == "") {
                  //alert(window.document.getElementById("<%=HD_Ordernumber.ClientID%>").value);
                  SendQueryPayment(window.document.getElementById("<%=HD_Ordernumber.ClientID%>").value);
              }


          }

      }
       

</script>



 

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
<script type="text/javascript" src="assets/global/plugins/select2/select2.min.js"></script>
<script type="text/javascript" src="assets/global/plugins/datatables/media/js/jquery.dataTables.min.js"></script>
<script type="text/javascript" src="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js"></script>
<script type="text/javascript" src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
<script type="text/javascript" src="assets/global/plugins/keyboard/keyboard.js"></script>
<script type="text/javascript"  src="assets/admin/pages/scripts/search.js"></script>

<script>
    var CompanyID = "<%= CompanyID %>";
        var DivisionID = "<%= DivisionID %>";
    var DepartmentID = "<%= DepartmentID %>";

    jQuery(document).ready(function () {



        <%--$.getJSON("AjaxFamily.aspx", function (data) {
            binddataFamily(data);
            $.getJSON("AjaxCategories.aspx?id=" + $('#<%=drpProductFamily.ClientID%>').val(), function (data) {
                binddata(data);
            });
        });--%>



        $('#<%=drpProductFamily.ClientID%>').change(function () {
            //alert($('#drpProductFamily').val());
            <%--$.getJSON("AjaxCategories.aspx?id=" + $('#<%=drpProductFamily.ClientID%>').val(), function (data) {
                binddata(data);
            });--%>
            SendQuery2callfromcategory();

        });

        $('#<%=ctl00_ContentPlaceHolder_drpProductCategory.ClientID%>').change(function () {
            SendQuery2callfromcategory();
            //loadGroups();
        });

    });


    function loadGroups() {
        var family = $("#<%=drpProductFamily.ClientID%>").val();
        var cate = $("#<%=ctl00_ContentPlaceHolder_drpProductCategory.ClientID%>").val();
        $.ajax({
            method: 'GET',
            url: `https://secureapps.quickflora.com/V2/api/ItemGroup?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&family=${family}&category=${cate}`,
            dataType: 'json',
            success: function (response) {
                var options_html = `<option value="">-- Select Group --</option>`;
                if (response) {
                    response.forEach(emp => {
                        if (emp.GroupID) {
                            options_html += `<option value="${emp.GroupID}">${emp.GroupName}</option>`;
                        }
                    });
                }
                $('#<%=drpGrp.ClientID%>').html(options_html);
            }
        });
    }


    function binddata(data) {

        $('#<%=ctl00_ContentPlaceHolder_drpProductCategory.ClientID%>').empty();
        $('#<%=ctl00_ContentPlaceHolder_drpProductCategory.ClientID%>').append($('<option></option>').val('').text('---- Select Category ----'));
        $.each(data, function (key, value) {
            //txtBloomhouseName
            $('#<%=ctl00_ContentPlaceHolder_drpProductCategory.ClientID%>').append($('<option></option>').val(value.ItemCategoryID).text(value.CategoryName));
        });
        loadGroups();
    }

    function binddataFamily(data) {
        $('#<%=drpProductFamily.ClientID%>').append($('<option></option>').val('').text('---- Select Family ----'));
        $.each(data, function (key, value) {
            //txtBloomhouseName
            $('#<%=drpProductFamily.ClientID%>').append($('<option></option>').val(value.ItemFamilyID).text(value.FamilyName));
        });

    }
    function SendQuery2callfromcategory() {
        var key = "";
        key = document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").value;
        SendQuery2(key);
    }

    function SendQuery2(key) {

        if (global_item_add == 1) { alert('Please make sure you click on the SAVE button to add this item : "' + global_item + '" to your order.'); return false; }

        // alert(document.getElementById("ctl00_ContentPlaceHolder_txtOrderNumber").value);
        var start = 0;
        start = new Date();
        start = start.getTime();

        Initialize();
        //var url = "AjaxItemsSearch.aspx?k=" + key + "&start=" + start + "&lc=" + document.getElementById("ctl00_ContentPlaceHolder_txtCustomerTypeID").value;
        var url = 'AjaxItemsSearchPOS';
            url = url + ".aspx?k=" + key + "&start=" + start
                + "&lc=" 
                + "&locationid=" + (document.getElementById("ctl00_ContentPlaceHolder_cmblocationid").value ? document.getElementById("ctl00_ContentPlaceHolder_cmblocationid").value : 'DEFAULT')
                //+ "&od=" + document.getElementById("ctl00_ContentPlaceHolder_lblOrderDate").value
                + "&sd=" + document.getElementById("ctl00_ContentPlaceHolder_txtDateFrom").value
                + "&ProductCategory=" + document.getElementById("<%=ctl00_ContentPlaceHolder_drpProductCategory.ClientID%>").value
                + "&drpProductFamily=" + document.getElementById("<%=drpProductFamily.ClientID%>").value
                + "&ProductColor=" + document.getElementById("<%=itemColor.ClientID%>").value
                + "&ProductGroup=" + document.getElementById("<%=drpGrp.ClientID%>").value
                + "&ProductSize=" + document.getElementById("<%=itemSize.ClientID%>").value;



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
                    //console.log(req.responseText);
                    if (req.responseText == "") {
                        //alert("in blank");  
                        //HideDiv("autocomplete");
                       // document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundImage = 'url(searchanimation.gif)';
                        document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundRepeat = 'no-repeat';
                        document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundPositionX = 'left';
                       // document.getElementById("<%=txtitemsearch.ClientID%>").value = "";
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

                             var table = document.getElementById('display-table');
                            var cells = table.getElementsByTagName('td');

                            for (var i = 0; i < cells.length; i++) {
                                // Take each cell
                                var cell = cells[i];
                                // do something on onclick event for cell
                                cell.onclick = function () {
                                    // Get the row id where the cell exists
                                    var rowId = this.parentNode.rowIndex;

                                    var rowsNotSelected = table.getElementsByTagName('tr');
                                    for (var row = 0; row < rowsNotSelected.length; row++) {
                                        rowsNotSelected[row].style.backgroundColor = "";
                                       // rowsNotSelected[row].classList.remove('selected');
                                    }
                                    var rowSelected = table.getElementsByTagName('tr')[rowId];
                                    rowSelected.style.backgroundColor = "yellow";
                                   // rowSelected.className += " selected";

                                    msg = rowSelected.cells[0].innerHTML;
                                    //alert(msg);
                                    FillSearchtextBox2(msg);
                                }
                            }

                        }
                    }
                }
                else {
                    document.getElementById("autocomplete2").innerHTML = "There was a problem retrieving data:<br>" + req.statusText;
                }
            }
    }

    
        function FillSearchtextBox2(val) {
            // alert(val);




            if (document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").value == "") {

            }
            else {
                var str = val;
                var str1;
                str1 = str.split("~!");

                document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").value = str1[0];
            //    document.getElementById("ctl00_ContentPlaceHolder_txtitemid").value = str1[0];
            //    document.getElementById("ctl00_ContentPlaceHolder_txtitemsearchprice").value = str1[1];
            //    document.getElementById("ctl00_ContentPlaceHolder_txtitemsearchDesc").value = str1[2];


            //    document.getElementById("ctl00_ContentPlaceHolder_txtitemidQty").value = str1[3];


            //    document.getElementById("autocomplete2").innerHTML = "";
            //    HideDiv("autocomplete2");
            //    document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").style.backgroundImage = 'url()';
            //    //alert(val);
            //    if (val.length > 3) {
            //        document.getElementById("sample_editable_1_new").focus();
            //        document.getElementById("sample_editable_1_new").click();
            //        document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").value = "";
            //        global_item_add = 1;
            //        global_item = str1[0];
            //    }
            //    // document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").focus();

            //}

            //var ordernumber = "";
            //ordernumber = document.getElementById("ctl00_ContentPlaceHolder_txtOrderNumber").value;


            //// alert(ordernumber);

            //if (ordernumber == "(New)") {
            //    // alert(document.getElementById("ctl00_ContentPlaceHolder_ImgUpdateSearchitems"));
            //    document.getElementById("ctl00_ContentPlaceHolder_ImgUpdateSearchitems").focus();
            //    document.getElementById("ctl00_ContentPlaceHolder_ImgUpdateSearchitems").click();
            //    //document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").focus();
            //    // return;
            }


            itemrsearchcloseProcess();

        }

    function itemrsearchcloseProcess() {

           // document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").style.backgroundImage = 'url(searchanimation.gif)';
            document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").style.backgroundRepeat = 'no-repeat';
            document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").style.backgroundPositionX = 'left';
            //document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").value = "";
            document.getElementById("autocomplete2").innerHTML = "";
            HideDiv("autocomplete2");


        }

    jQuery(document).ready(function () {
        $("#<%=txtitemsearch.ClientID%>").on("keyup",function () {
            SendQuery2(this.value)
        });
        Search.init();
    });
</script>
</asp:Content>

