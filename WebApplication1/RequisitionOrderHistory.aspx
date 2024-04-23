<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="RequisitionOrderHistory.aspx.vb" Inherits="RequisitionOrderHistory" %>

<%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>
<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Core" TagPrefix="core" %>
<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Controls" TagPrefix="ctls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css" />

    <link rel="stylesheet" type="text/css" href="assets/global/plugins/keyboard/keyboard.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    Requisition Orders History
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">


    <ajax:AjaxPanel ID="AjaxPanel101" runat="server">

        <!-- BEGIN PORTLET 1st Block-->
        <div class="portlet box green">
            <div class="portlet-title">
                <div class="caption">
                    &nbsp;Search
                </div>
                <div class="tools">
                    <a href="javascript:;" class="collapse"></a>
                </div>
            </div>

            <div class="portlet-body">


                <div class="note note-success margin-bottom-0">

                    <div class="row">
                        <div class="col-xs-4">
                            Type:
                             <asp:DropDownList ID="drpType" CssClass="form-control input-sm select2me" runat="server" AppendDataBoundItems="true" AutoPostBack="true">
                                 <asp:ListItem Text="--Select Status--" Value=""></asp:ListItem>
                                 <asp:ListItem Text="Flowers" Value="Flowers"></asp:ListItem>
                                 <asp:ListItem Text="Plants" Value="Plants"></asp:ListItem>
                             </asp:DropDownList>
                        </div>
                        <div class="col-xs-4">
                            Location 
                            <asp:DropDownList ID="cmblocationid" CssClass="form-control input-sm select2me" runat="server" AppendDataBoundItems="true" AutoPostBack="true">
                                <asp:ListItem Text="--Select Location--" Value=""></asp:ListItem>
                            </asp:DropDownList>

                        </div>
                         
                        <div class="col-xs-4">
                            Status
                            <asp:DropDownList ID="drpStatus" CssClass="form-control input-sm select2me" runat="server" AppendDataBoundItems="true" AutoPostBack="true">
                                <asp:ListItem Text="--Select Status--" Value=""></asp:ListItem>
                                <asp:ListItem Text="New Request" Value="New"></asp:ListItem>
                                <asp:ListItem Text="In Process" Value="InProcess"></asp:ListItem>
                            </asp:DropDownList>
                        </div>

                    </div>
                     
                </div>


            </div>
        </div>
        <!-- END PORTLET-->

        <div class="portlet box green">
            <div class="portlet-title">
                <div class="caption">
                    &nbsp;Order Header Change History 
                </div>
                <div class="tools">
                    <a href="javascript:;" class="collapse"></a>
                </div>
            </div>

            <div class="portlet-body">
                 

                <div  style='z-index:90;height:100%' class="table-responsive">

                <asp:GridView ID="OrderHeaderGrid"     runat="server" DataKeyNames="HID" CssClass="table table-bordered table-striped table-condensed flip-content" AutoGenerateColumns="false"  AllowPaging="True" PageSize="500">
                    <Columns >
                         <asp:TemplateField HeaderText="OrderNumber">
                            <ItemTemplate> <%#Eval("OrderNumber")%>  </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Field Name">
                            <ItemTemplate> <%#Eval("fieldName")%>  </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Field Old Value">
                            <ItemTemplate> <%#Eval("FieldInitValue")%>  </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Field New Value">
                            <ItemTemplate> <%#Eval("fieldChangeValue")%>  </ItemTemplate>
                        </asp:TemplateField>
                    </Columns> 

                </asp:GridView>

                </div> 
                 
            </div>
        </div>


        <div class="portlet box green">
            <div class="portlet-title">
                <div class="caption">
                    &nbsp;Order Product Change History 
                </div>
                <div class="tools">
                    <a href="javascript:;" class="collapse"></a>
                </div>
            </div>

            <div class="portlet-body">
                  <div class="note note-success margin-bottom-0">
                 <div class="row">
                                    <div class="col-xs-4 col-md-2">
                                    		<div class="form-group">
                                                <asp:DropDownList ID="drpFieldName"  CssClass="form-control input-sm select2me" runat="server">
                                                        <asp:ListItem Selected="True" Value="fieldName">Field Name</asp:ListItem>                                                
                                                 </asp:DropDownList>	
                                            </div>
                                     </div>
                                     <div class="col-xs-3 col-md-1">
                                     		<div class="form-group">
                                             
                                            <asp:DropDownList ID="drpCondition" CssClass="form-control input-sm select2me" runat="server">
                                                <asp:ListItem Selected="True" Value="=">=</asp:ListItem>
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
                                         
                                            <asp:Button ID="btnSearchExpression" CssClass="btn btn-success btn-xs" runat="server" Text="SEARCH" />
                                            
                                            &nbsp;&nbsp;<asp:Button ID="btnreset" CssClass="btn btn-success btn-xs" runat="server" Text="RESET SEARCH" />
                                        </div>
                                        <asp:Label ID="lblErr" runat="server" Text=" "></asp:Label>
                                </div>
                  </div>
             </div> 


                <div  style='z-index:90;height:100%' class="table-responsive">

                <asp:GridView ID="OrderHeaderGrid2"     runat="server" DataKeyNames="HID" CssClass="table table-bordered table-striped table-condensed flip-content" AutoGenerateColumns="false"   AllowPaging="True" PageSize="5000">

                    <Columns>
                        <asp:TemplateField HeaderText="OrderNumber">
                            <ItemTemplate> <%#Eval("OrderNumber")%> <%#BatchPOSampleRow(Eval("CustomerID")) %> </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Field Name">
                             <ItemTemplate>  
                                <asp:Label ID="lblfieldName" Text='<%#Eval("fieldName")%>'   runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Field Old Value">
                            <ItemTemplate> <%#Eval("FieldInitValue")%>  </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Field New Value">
                            <ItemTemplate> <%#Eval("fieldChangeValue")%>  </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="EmployeeID">
                            <ItemTemplate> <%#Eval("EmployeeID")%>  </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date time">
                            <ItemTemplate> <%#localDatetime(Eval("HDate"))%>  </ItemTemplate>
                        </asp:TemplateField>


                    </Columns>
                     
                </asp:GridView>

                </div> 
                 
            </div>
        </div>


    </ajax:AjaxPanel>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script type="text/javascript" src="assets/global/plugins/select2/select2.min.js"></script>
    <script type="text/javascript" src="assets/global/plugins/datatables/media/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js"></script>
    <script type="text/javascript" src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript" src="assets/global/plugins/keyboard/keyboard.js"></script>
    <script type="text/javascript" src="assets/admin/pages/scripts/search.js"></script>

    <script>
        jQuery(document).ready(function() {

            Search.init();
        });
    </script>

</asp:Content>

