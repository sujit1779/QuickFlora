<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MainMaster.master" CodeFile="ItemsCategories.aspx.vb" Inherits="ItemsCategories" %>
 
   <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css" />
    <link href="assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet"
        type="text/css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
      <h3 class="page-title">Items Categories List
    </h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
  
<script>
function getconfirm() 
{ 
if (confirm("Do you want to delete record?")==true) 
return true; 
else 
return false; 
}


</script>
 
   <div class="table-toolbar">
                <div class="btn-group">
                    <asp:LinkButton ID="lnkAdd" runat="server"  CssClass="btn green">Add New <i class='fa fa-plus'></i></asp:LinkButton>
                </div>
                <div class="btn-group pull-right">
                    <button class="btn dropdown-toggle" data-toggle="dropdown">
                        Tools <i class="fa fa-angle-down"></i>
                    </button>
                    <ul class="dropdown-menu pull-right">
                        <li><a href="#">Print</a> </li>
                        <li><a href="#">Save as PDF</a> </li>
                        <li><a href="#">Export to Excel</a> </li>
                    </ul>
                </div>
            </div>

            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                        Search Options
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse"></a>
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="table-toolbar text-center">
                        <div class="btn-group">
                            <div class="col-md-4">

                                <asp:DropDownList ID="drpSearchFor" runat="server" CssClass="form-control input-medium select2me select2-offscreen" data-placeholder="Select..." tabindex="-1">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="CategoryName" Text="Category Name"></asp:ListItem>
                                    <asp:ListItem Value="ItemFamilyID" Text="Family ID"></asp:ListItem>
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="btn-group">
                            <div class="col-md-1">
                                <asp:DropDownList ID="drpSearchCondition" runat="server" CssClass="form-control input-medium select2me select2-offscreen" data-placeholder="Select..." tabindex="-1">
                                    <asp:ListItem Value="Like" Text="Like"></asp:ListItem>                                    
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="btn-group">
                            <asp:TextBox ID="txtSearchValue" runat="server" CssClass="form-control input-large"></asp:TextBox>
                        </div>
                        <div class="btn-group">
                             
                            <asp:Button ID="btnSearch" runat="server" Text="SEARCH" CssClass="btn green"/>
                        </div>
                        <div class="btn-group">
                             
                            <asp:Button ID="btnClear" runat="server" Text="CLEAR" CssClass="btn grey"/>
                        </div>
                    </div>

                      

                </div>
            </div>

            



 <asp:GridView ID="grdContentList" DataKeyNames="ItemCategoryID,ItemFamilyID"  AutoGenerateColumns="false"  AllowPaging="true" runat="server" 
 CssClass="table table-striped table-hover table-bordered"  PageSize="10">
                <Columns>
                <asp:TemplateField  HeaderText="Edit" >
                <ItemTemplate>
                
                <asp:ImageButton ID="ImageButton1" ToolTip="Edit" ImageUrl="~/images/edit.gif"  runat="server" CommandName="Edit" />
                
                </ItemTemplate>
                     
                </asp:TemplateField>
                 
                 <asp:TemplateField  HeaderText="Delete" >
                        <ItemTemplate >
                        <asp:ImageButton  ID="ImageButton2"  OnClientClick="return confirm('Are you sure you want to delete this record?');"
                        CommandName="Delete" ToolTip="Delete"  ImageUrl="~/images/Delete.gif"  runat="server" />
                        </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField HeaderText="CategoryID"  DataField="ItemCategoryID" > </asp:BoundField>
                <asp:BoundField HeaderText="FamilyID"  DataField="ItemFamilyID" > </asp:BoundField>
                <asp:BoundField HeaderText="Category Name"  DataField="CategoryName" > </asp:BoundField>
                <asp:BoundField HeaderText="Description"  DataField="CategoryDescription" > </asp:BoundField>
                <asp:BoundField HeaderText="SortOrder"  DataField="SortOrder" > </asp:BoundField>
                <asp:BoundField HeaderText="Is Active"  DataField="EnableItemCategoryID" > </asp:BoundField>
                <asp:BoundField HeaderText="Container Type"  DataField="ContainerType" > </asp:BoundField>

                   </Columns>
                </asp:GridView>
                
                
   
                
 

 
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
 
<script type="text/javascript" >
    jQuery(document).ready(function() {

        Search.init();
    });
</script>

</asp:Content>

