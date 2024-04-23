<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="InventoryTransferDetaila.aspx.vb" Inherits="InventoryTransferDetaila" %>

<%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>
<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Core" TagPrefix="core" %>
<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Controls" TagPrefix="ctls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css"/>
<link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css"/>

<link rel="stylesheet" type="text/css" href="assets/global/plugins/keyboard/keyboard.css"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
Merchandise Transfer
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
 <script type="text/javascript" language="javascript">

     function ShowPopup(panel, gridviewRow) {
         var row = document.getElementById(gridviewRow);
         row.style.backgroundColor = "#F7F1BE";
         row.style.color = "#456037";
         var pnl = document.getElementById(panel);
         pnl.style.display = "block";
     }

     //Hides DIV popup commands for gridview
     function HidePopup(panel, gridviewRow, alternateRow) {
         var row = document.getElementById(gridviewRow);
         if (alternateRow == "1") {
             row.style.backgroundColor = "#E6E3CB";
             row.style.color = "#000000";
         }
         else {
             row.style.backgroundColor = "#CAC590";
             row.style.color = "#000000";
         }
         var pnl = document.getElementById(panel);
         pnl.style.display = "none";
     }

     function checkItemSearch() {

         if (document.getElementById("ctl00__mainContent_txtTransferNumber") != null) {
             if (document.getElementById("ctl00__mainContent_txtTransferNumber").value == "") {
                 alert("Please fill Transfer #");
                 document.getElementById("ctl00__mainContent_txtTransferNumber").focus();
                 return false;
             }
         }

         return true;
     }

     function checkPopupItemSearch() {
         if (checkItemSearch()) {
             window.open('ItemsSearch.aspx?searchfor=Transfer', 'MyWindow', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=650,height=400,Left=350,top=275,screenX=0,screenY=275,alwaysRaised=yes');
             return false;
         }
     }

     function CheckValues() {

         if (document.getElementById("ctl00__mainContent_drpTansferFromLocaton") != null) {
             if (document.getElementById("ctl00__mainContent_drpTansferFromLocaton").value == "") {
                 alert("Please select transfer from location");
                 document.getElementById("ctl00__mainContent_drpTansferFromLocaton").focus();
                 return false;
             }
         }

         if (document.getElementById("ctl00__mainContent_drpTransferToLocaton") != null) {
             if (document.getElementById("ctl00__mainContent_drpTransferToLocaton").value == "") {
                 alert("Please select transfer to location");
                 document.getElementById("ctl00__mainContent_drpTransferToLocaton").focus();
                 return false;
             }
         }

         if (document.getElementById("ctl00__mainContent_drpTansferByEmployee") != null) {
             if (document.getElementById("ctl00__mainContent_drpTansferByEmployee").value == "") {
                 alert("Please select employee");
                 document.getElementById("ctl00__mainContent_drpTansferByEmployee").focus();
                 return false;
             }
         }

         if (document.getElementById("ctl00__mainContent_txtTransferDate") != null) {
             if (document.getElementById("ctl00__mainContent_txtTransferDate").value == "") {
                 alert("Please select transfer date");
                 document.getElementById("ctl00__mainContent_txtTransferDate").focus();
                 return false;
             }
         }
     }


     
        function Saveitem(This, RowNumber, name, oldvalue) {
            Initialize();
            This.style.backgroundColor = "";
            var url = "AjaxTransferitemSave.aspx?RowNumber=" + RowNumber + "&value=" + This.value + "&name=" + name;
            
            if (This.value == oldvalue) {
                return;
            }
            // alert(url);
            document.getElementById("autosave").style.backgroundImage = 'url(progress_bar.gif)';
            document.getElementById("autosave").style.backgroundRepeat = 'no-repeat';

            $.get(url, function(data, status) {
               // alert("Data: " + data + "\nStatus: " + status);
                document.getElementById("autosave").style.backgroundImage = 'url()';
            });
        }


       var value_on_click = "";
        
        function myFocusFunction(This) {
            This.style.backgroundColor = "yellow";
            if (This.value == "0") {
                This.value = "";
            }
            value_on_click = This.value;
        }

        function myonblurFunction(This) {
            This.style.backgroundColor = "";
        }


    </script>

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Services>
            <%--<asp:ServiceReference Path="~/EnterpriseASPAR/CustomOrder/AutoCompleteAjax.asmx" />--%>
        </Services>
    </asp:ScriptManager>
    <script type="text/javascript">        //This comlete script allows for Ajax Call and search for item only. This will populate a list in div (right beneath of ItemSearch box)

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

        function SendQuery2(key) {

            if (!checkItemSearch()) {
                return false;
            }

            Initialize();
            //var url = "~/EnterpriseASPAR/CustomOrder/AjaxItemsSearch.aspx?k=" + key;
            var url = "AjaxItemsSearch2.aspx?k=" + key + "&SearchFor=Transfer";

            if (req != null) {
                req.onreadystatechange = Process2;
                req.open("GET", url, true);
                req.send(null);
            }
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

        function Process2() {
            document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundImage = 'url(ajax-loader-text.gif)';
            document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundRepeat = 'no-repeat';
            document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundPositionX = 'right';

            if (req.readyState == 4) {              // only if "OK"
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
                        container.innerHTML = "<div style='text-align:center' ><a href='javascript:itemrsearchcloseProcess();' >Close Search result</a></div>";
                        container.appendChild(newdiv)
                    }
                    else {
                        if (document.getElementById("<%=txtitemsearch.ClientID%>").value != "") {
                            ShowDiv("autocomplete2");
                            //document.getElementById("autocomplete2").innerHTML = req.responseText;
                            var newdiv = document.createElement("div");
                            newdiv.innerHTML = req.responseText;
                            var container = document.getElementById("autocomplete2");
                            container.innerHTML = "<div style='text-align:center' ><a href='javascript:itemrsearchcloseProcess();' >Close Search result</a></div>";
                            container.appendChild(newdiv)
                        }
                    }
                }
                else {
                    ShowDiv("autocomplete2");
                    //document.getElementById("autocomplete2").innerHTML = req.responseText;
                    var newdiv = document.createElement("div");
                    newdiv.innerHTML = req.responseText;
                    var container = document.getElementById("autocomplete2");
                    container.innerHTML = "<div style='text-align:center' ><a href='javascript:itemrsearchcloseProcess();' >Close Search result</a></div>";
                    container.appendChild(newdiv)
                }
            }
        }

        function FillSearchtextBox2(val) {
            // alert(val);
            document.getElementById("<%=txtitemsearch.ClientID%>").value = val;
            document.getElementById("autocomplete2").innerHTML = "";
            HideDiv("autocomplete2");
            document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundImage = 'url()';
            document.getElementById("<%=ImgUpdateSearchitems.ClientID%>").focus();
            document.getElementById("<%=ImgUpdateSearchitems.ClientID%>").click();
            //ImgUpdateSearchitems
        }

        function BodyLoad() {
            HideDiv("autocomplete2");
            HideDiv("autocompleteForTransfer");
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

        function Itemsearch() {
            var CustomerIDTemp = " ";
            window.open("../../EnterpriseASPAR/CustomOrder/ItemDetailsSearch.aspx?CustomerID=" + CustomerIDTemp + "", 'MyWindow', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=650,height=400,Left=350,top=275,screenX=0,screenY=275,alwaysRaised=yes');
        }

        function deleteConfirm() {
            if (confirm("Are you sure to delete this item from Transfer ?")) {
                return true;
            } else {
                return false;
            }
        }

        function deleteConfirmForTransfer() {
            if (confirm("Are you sure to delete this Transfer for selected price type ?")) {
                return true;
            } else {
                return false;
            }
        }


    </script>

     <Ajax:AjaxPanel ID="AjaxPanel101" runat="server">

        <!-- BEGIN PORTLET 1st Block-->
    <div class="portlet box green">
        <div class="portlet-title">
            <div class="caption">
                &nbsp;Merchandise Transfer Details</div>
            <div class="tools">
                <a href="javascript:;" class="collapse"></a>
            </div>
        </div>
        <div class="portlet-body">
            <div class="row">
                <div class="col-md-4">
                    <div class="text-center" style="padding-top:50px;">
                        <asp:Image ID="ImgRetailerLogo" CssClass="img-rounded" ImageUrl="" runat="server" />
                    </div>
                </div>
                <div class="col-md-4">
                    <!-- BEGIN FORM-->
                    <div class="form-body">
                        <table id="table3" class="table table-striped table-hover table-bordered">
                            
                            <tr>
                                <td width="110" height="12">
                                    Transfer Number 
                                </td>
                                <td width="101" height="12">
                                    <ajax:ajaxpanel id="AjaxPanel2" runat="server">
                                                <asp:TextBox ID="txtTransferNumber" runat="server" Width="150px" ReadOnly="true"></asp:TextBox>
                                   </ajax:ajaxpanel>
                                </td>
                            </tr>
                            <tr>
                                <td width="110" height="12" valign="middle">
                                     
                                </td>
                                <td width="101" height="12">
                                    <asp:Button ID="btnGenerateTransferNumber" Visible="false"  CssClass="btn green" runat="server" Text="Generate" />
                                </td>
                            </tr>
                            
                            
                            <tr>
                                <td width="110" height="15" valign="middle">
                                    Employee
                                </td>
                                <td width="101" height="15">
                                   <asp:DropDownList ID="drpTansferByEmployee" CssClass="form-control"  runat="server"></asp:DropDownList>
                                </td>
                            </tr>

                              <tr>
                                <td width="110" height="15" valign="middle">
                                    
                                </td>
                                <td width="101" height="15">
                                    
                                </td>
                            </tr>


                             <tr>
                                <td width="110" height="15" valign="middle">
                                    Transfer Date
                                </td>
                                <td width="101" height="15">
                                    <div class="input-group input-sm date-picker input-daterange" data-date="07/03/2015" data-date-format="mm/dd/yyyy">
                                    
                                     <div class="input-icon">
                                       <table>
                                            <tr>
                                                <td> <i class="fa fa-calendar"></i></td>
                                                <td>
                                                    <asp:TextBox  ID="txtTransferDate" CssClass="form-control"   runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                       </div>
                                </div> 

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

        <!-- BEGIN PORTLET 2nd Block-->
    <div class="portlet box green">
        <div class="portlet-title">
            <div class="caption">
                &nbsp;Transfer Details</div>
            <div class="tools">
                <a href="javascript:;" class="collapse"></a>
            </div>
        </div>
        <div class="portlet-body">
            <div class="row">
                
                <div class="col-md-6">
                    <!-- BEGIN FORM-->
                    <div class="form-body">
                        <table id="table1" class="table table-striped table-hover table-bordered">
                            
                            
                            <tr>
                                <td width="110" height="12" valign="middle">
                                       <span style="display: inline-block;">Transfer From</span> 
                                
                                </td>
                                <td width="101" height="12">
                                     <span>
                                    <asp:DropDownList ID="drpTansferFromLocaton" CssClass="form-control"  runat="server"></asp:DropDownList></span>
                                </td>
                            </tr>
                            <tr>
                                <td width="110" height="15" valign="middle">
                                   <span style="display: inline-block;">Approved By </span>
                                </td>
                                <td width="101" height="15">
                                    
                                          <span>  <asp:DropDownList ID="drpApprovedByEmployee" CssClass="form-control"  runat="server"></asp:DropDownList></span>
                                    
                                </td>
                            </tr>
                            
                         
                           

                                    
                        </table>
                    </div>
                    <!-- END FORM-->
                </div>
                <div class="col-md-6">
                    
                      <table id="table2" class="table table-striped table-hover table-bordered">
                            
                            
                            <tr>
                                <td width="110" height="12" valign="middle">
                                     <span>Transfer To</span> 
                                
                                </td>
                                <td width="101" height="12">
                                     <span>
                                    <asp:DropDownList ID="drpTransferToLocaton" CssClass="form-control"  runat="server"></asp:DropDownList></span>
                                </td>
                            </tr>
                            <tr>
                                <td width="110" height="15" valign="middle">
                                     
                                <span>Approved At</span> 
                              
                               
                                </td>
                                <td width="101" height="15">
                                    
                                         <span>  <asp:TextBox ID="txtApprovedTime" CssClass="form-control"  runat="server"></asp:TextBox></span>
                                     
                                </td>
                            </tr>
                           
                                    
                        </table>


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
                                          
								    </div>
                                    <div class="col-md-5">
									        <div class="text-right" >
                                                 <asp:LinkButton ID="ItemSearch" Font-Underline="true" Visible="false" runat="server" Text="Advanced Item Search" OnClientClick="javascript:return checkPopupItemSearch();"></asp:LinkButton>
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
                                                    <asp:TextBox ID="txtitemsearch" CssClass="form-control input-circle-right" runat="server" onKeyUp="SendQuery2(this.value);"></asp:TextBox>
                                             </Ajax:AjaxPanel> 
                                             <br />
                                             <div align="left" class="box autocomplete" style="visibility: hidden;" id="autocomplete2"   ></div>   

 
                                            
                                        </div>
                                    </div>


                                      <div style="display:none;" class="row">
								        <div class="col-md-12"  >
                                             <Ajax:AjaxPanel ID="AjaxPanel13" runat="server">
                                                <asp:ImageButton ID="ImgUpdateSearchitems" OnClientClick="javascript:FillSearchtextBox22();"  ToolTip="Update Item" ImageUrl="~/images/2-sh-stock-in.gif" Width="0"    runat="server"  />  
                                                <asp:Label ID="lblitemsearchmsg" runat="server" Text=""></asp:Label>
                                            </Ajax:AjaxPanel> 
                                        </div>
								 
							        </div>
  
              <div   id="autosave">
                   <br />
                     <br />
                 </div>
                         <br />            

                              <asp:HiddenField ID="hdSortDirection" runat="server" />
                                <asp:GridView ID="InventoryTransferItemGrid" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-condensed flip-content"
                                    DataKeyNames="RowID" AllowSorting="true">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="5" HeaderText="Edit" Visible="false"  ItemStyle-HorizontalAlign="Center"
                                            FooterStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgEdit" TabIndex="57" ToolTip="Edit Item" ImageUrl="~/Images/edit.gif"  Enabled='<%# returnenable(Eval("TransferQty"),Eval("ReceivedQty")) %> ' 
                                                    runat="server" CommandName="Edit" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:ImageButton ID="ImgUpdate" TabIndex="57" ToolTip="Update Item" ImageUrl="~/images/Add-Button.gif"
                                                    CommandName="Update" CommandArgument="<%# Container.DataItemIndex %>"
                                                    runat="server" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:ImageButton CausesValidation="true" ID="imgPostItemDetails" TabIndex="59" CommandName="footerPostDetails"
                                                    ToolTip="Add Item" ImageUrl="~/images/Add-Button.gif" runat="server" />
                                            </FooterTemplate>
                                            <ItemStyle Width="5px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="5px">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton11" CausesValidation="false" ToolTip="Delete" ImageUrl="~/images/Delete.gif"  Enabled='<%# returnenable(Eval("TransferQty"),Eval("ReceivedQty")) %> ' 
                                                    runat="server" CommandName="Delete" OnClientClick="javascript:return deleteConfirm();" />
                                            </ItemTemplate>
                                            <ItemStyle Width="5px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                       

                                        <asp:TemplateField HeaderText="Row ID" HeaderStyle-Width="100px" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowID" Text='<%# Bind("RowID")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblUpdateRowID" Text='<%# Bind("RowID")%>' runat="server"></asp:Label>
                                            </EditItemTemplate>
                                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Item ID" HeaderStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemID" Text='<%# Bind("ItemID") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblUpdateItemID" Text='<%# Bind("ItemID") %>' runat="server"></asp:Label>
                                            </EditItemTemplate>
                                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Item Name" HeaderStyle-Width="150px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemName" Text='<%# Bind("ItemName") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblUpdateItemName" Text='<%# Bind("ItemName") %>' runat="server"></asp:Label>
                                            </EditItemTemplate>
                                            <ItemStyle Width="150px" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Qty" ItemStyle-Width="50">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lblQty" Text='<%# Eval("TransferQty")%>' runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtUpdateQty" Text='<%# Eval("TransferQty") %>' runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit Price" HeaderStyle-Width="25px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lblUnitPrice" Text='<%#String.Format("${0:N2}", Eval("UnitPrice")) %>'
                                                    Width="100" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="lblUpdateUnitPrice" Text='<%# Bind("UnitPrice") %>' Width="100" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle Width="25px" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Addional Notes" ItemStyle-Width="50">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lblAdditionalNotes" Text='<%# Eval("AddtionalItemNotes")%>' runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtUpdateAddtionalNotes" Text='<%# Eval("AddtionalItemNotes")%>' runat="server" TextMode="MultiLine"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>


                                 
                            </div>
 
                     </div> 

     <!-- END PORTLET-->

         <!-- BEGIN PORTLET 5th Block-->
					<div class="portlet box green">
                    	<div class="portlet-title">
                            <div class="caption" style="width:95%;"> 
                                 <div class="row">
								    <div class="col-md-2">
									    <div class="text-left" >
                                          
                                        </div> 
								    </div>
								    <div class="col-md-5">
                                          
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
                        	  <div class="row">
								    <div class="col-md-4">
									       <asp:Label ID="lblErrorMessage" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
								    </div>
								    <div class="col-md-4">
                                              
                                      <table  class="table table-bordered table-striped table-condensed flip-content">
                                        <tr>
                                            <td> <asp:Button ID="btnSave" runat="server" Text="Save Transfer" CssClass="btn btn-success btn-xs" Width="150" OnClientClick="javascript:return CheckValues();" /></td>
                                            <td> &nbsp;&nbsp; </td>
                                            <td> <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="150" CssClass="btn btn-success btn-xs"/></td>
                    
                                        </tr>
                                    </table>
								    </div>
                                    <div class="col-md-4"> 
									      
                                    <table  class="table table-bordered table-striped table-condensed flip-content">
                    
                                           <tr>
                                                    <td width="110" height="15" valign="middle">
                                                       Total # of Items
                                                    </td>
                                                    <td width="101" height="15">
                                                       <asp:Label ID="lblTotalNumberOfItems" CssClass="form-control"  runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td width="110" height="15" valign="middle">
                                                       Total
                                                    </td>
                                                    <td width="101" height="15">
                                                       <asp:Label ID="lblprice" CssClass="form-control"  runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                   
                                    </table>
                                   
                                    </div> 
							</div>
                        	    
                        	</div> 
                      </div> 
    
            <!-- END PORTLET-->
</Ajax:AjaxPanel> 

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

