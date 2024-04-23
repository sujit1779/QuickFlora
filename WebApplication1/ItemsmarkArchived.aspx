<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="ItemsmarkArchived.aspx.vb" Inherits="ItemsmarkArchived" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    Archive Item
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    
            <!-- Item Main Tab -->
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box green ">
                        <div class="portlet-title">
                            <div class="caption">
                                Item Details
                            </div>
                            <div class="tools">
                                <a href="#" class="collapse"></a>
                                <!--<a href="#portlet-config" data-toggle="modal" class="config"></a>-->
                                <!--<a href="" class="reload"></a>-->
                                <!--<a href="#" class="remove"></a>-->
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-group">


                                <div class="col-md-4">
                                    <div class="form-horizontal" role="form">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">ID</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtItemID" runat="server" CssClass="form-control input-md" placeholder="Item ID"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Item Type</label>
                                                <div class="col-md-6">
                                                    <asp:DropDownList Enabled="false"  ID="drpItemType" runat="server" class="form-control input-md">
                                                        <asp:ListItem Text="--Please Select--" Value=""></asp:ListItem>
                                                        <asp:ListItem Text="Stock" Value="Stock"></asp:ListItem>
                                                        <asp:ListItem Text="Non Stock" Value="Non Stock"></asp:ListItem>
                                                        <asp:ListItem Text="Class" Value="Flower Class"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Name</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtItemName" runat="server" class="form-control input-md" placeholder="Item Name"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Short Description</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtItemShortDescription" runat="server" class="form-control input-md" placeholder="Short Desciption" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Long Description</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtItemLongDescription" runat="server" class="form-control input-md" placeholder="Long Desciption" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                             

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-horizontal" role="form">
                                        <div class="form-body">
                                            
                                            <div class="form-group">
                                                <label class="col-md-2 control-label"></label>
                                                <div class="col-md-10">
                                                   <b>  On Mark Archived </b>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-2 control-label"> </label>
                                                <div class="col-md-10">
                                                    Please confirm that on Archive action, this item will be be hidden for following sections: 
                                                    <ul>
                                                        <li>POS Module Order Entry Form</li>
                                                        <li>Admin Module Order Entry form</li>
                                                        <li>Website</li>
                                                        <li>Events Module</li>
                                                        <li>Recipe</li>
                                                        <li>Purchase Module</li>
                                                    </ul>

                                                </div>
                                            </div>
                                             <div class="form-group">
                                                <label class="col-md-2 control-label"></label>
                                                <div class="col-md-10">
                                                  <asp:Button ID="btnTab1Save" runat="server" class="btn green" Text="Archive" />
                                    &nbsp;&nbsp;
                                        <asp:Button ID="btnTab1SaveClose" runat="server" class="btn green" Text="Cancel" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                

                            </div>
                           
                             

                            <div class="form-group">
                                <div style="text-align: center; padding: 10px;" class="col-md-12">
                                    
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <!-- END PAGE CONTENT-->
            </div>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

