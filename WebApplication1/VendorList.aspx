<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="VendorList.aspx.vb" Inherits="VendorList" %>

<%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>

<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Core"
    TagPrefix="core" %>
<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Controls"
    TagPrefix="ctls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
Vendor List
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
        var url = "AjaxVendorSearch.aspx?k=" + key;
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



    <ajax:ajaxpanel id="AjaxPanel101" runat="server">


   
					<div class="portlet box green">
						<div class="portlet-title">
							<div class="caption">Search Options</div>
							<div class="tools"><a href="javascript:;" class="collapse"></a></div>
						</div>
						<div class="portlet-body" >

    
    <div class="vsep">
    </div>
    <asp:Table runat="server" ID="tblMain" SkinID="groupHeaderSkin">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">
               
                  <a href="VendorDetails.aspx" Class="btn btn-success btn-xs" >Create New Vendor</a>
            </asp:TableCell>
             
        </asp:TableRow>
    </asp:Table>
     


      
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
                            



     </div>
				
                
                
                
                
                	</div>
				<!-- END PORTLET-->
     
    <asp:HiddenField ID="hdSortDirection" runat="server" />
    <asp:GridView ID="InventoryTransferGrid" runat="server" AutoGenerateColumns="False"
        DataKeyNames="VendorID" AllowSorting="true" PageSize="25" AllowPaging="true"  CssClass="table table-bordered table-striped table-condensed flip-content"   >
        <Columns> 
            <asp:TemplateField HeaderText="Edit"  >
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" CausesValidation="false" ToolTip="Edit"  ImageUrl="~/Images/edit.gif"
                        runat="server" CommandName="Edit" />
                </ItemTemplate>
                <ItemStyle Width="3px" HorizontalAlign="Center" />
            </asp:TemplateField>
           
         

            <asp:BoundField HeaderText="Vendor ID" SortExpression="VendorID" ItemStyle-HorizontalAlign="Right" DataField="VendorID"  />
            <asp:BoundField HeaderText="Vendor Name" SortExpression="VendorName" ItemStyle-HorizontalAlign="Left" DataField="VendorName"  />
            <asp:BoundField HeaderText="Vendor Address1" SortExpression="VendorAddress1" ItemStyle-HorizontalAlign="Left" DataField="VendorAddress1"   />
            <asp:BoundField HeaderText="Vendor City" SortExpression="VendorCity" ItemStyle-HorizontalAlign="Right" DataField="VendorCity"   />
            <asp:BoundField HeaderText="Vendor Zip" SortExpression="VendorZip" ItemStyle-HorizontalAlign="Right" DataField="VendorZip"   />
            <asp:BoundField HeaderText="Vendor Phone" SortExpression="VendorPhone" ItemStyle-HorizontalAlign="Right" DataField="VendorPhone"   />
            <asp:TemplateField HeaderText="Vendor Email" ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>
                            <a class="btn btn-success-light"  href='VendorEmailList.aspx?VendorID=<%# Eval("VendorID")%>&CustomerName=<%# Eval("VendorName")%>'>
                               Add +
                            </a>
                            &nbsp;<%# Eval("VendorEmail")%>


                        </ItemTemplate>
             </asp:TemplateField>
            <asp:BoundField HeaderText="Vendor Email" SortExpression="VendorEmail" ItemStyle-HorizontalAlign="Right" DataField="VendorEmail"   />
            <asp:BoundField HeaderText="Vendor Type" SortExpression="VendorTypeID" ItemStyle-HorizontalAlign="Right" DataField="VendorTypeID"   />
           <%-- <asp:TemplateField HeaderText="Transfer Date"  SortExpression="TransferDate" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="25px" ItemStyle-Width="25px">
                    <ItemTemplate>
                        <%# Convert.ToDateTime(IIf(IsDBNull(Eval("TransferDate")), "1900-01-01", Eval("TransferDate")).ToShortDateString)%>
                    </ItemTemplate>
            </asp:TemplateField>
--%>
          
        </Columns>
    </asp:GridView>

     </ajax:ajaxpanel> 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

