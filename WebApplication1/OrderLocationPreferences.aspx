<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="OrderLocationPreferences.aspx.vb" Inherits="OrderLocationPreferences" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
                                                    <asp:LinkButton ID="btnNew" runat="server" SkinID="" CssClass="btn btn-success btn-xs" Text="Create New Location" PostBackUrl="OrderLocationPreferencesDetails.aspx"></asp:LinkButton>
                                                     
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


                        <asp:Label ID="lblmsgOrderLocationDelete" runat="server" Font-Bold="True" ForeColor="Green"></asp:Label>
                        <br /><br />
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="true" PageSize="100" AllowSorting="true" CssClass="table table-bordered table-striped table-condensed flip-content" 
                                AutoGenerateColumns="False" DataKeyNames="LocationID" Width="90%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Edit" ItemStyle-VerticalAlign="Middle"  ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Edit" ImageUrl="~/Images/edit.gif"
                                                TabIndex="57" ToolTip="edit item" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Delete" ImageUrl="~/Images/Delete.gif"
                                                OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                                TabIndex="58" ToolTip="delete item" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="LocationID" HeaderText="Location ID" ItemStyle-Width="10%"
                                        SortExpression="LocationID" />
                                    <asp:BoundField DataField="LocationName" HeaderText="Location Name" ItemStyle-Width="15%"
                                        SortExpression="LocationName" />
                                    <asp:BoundField DataField="City" HeaderText="City" ItemStyle-Width="10%" SortExpression="City" />
                                    <asp:BoundField DataField="State" HeaderText="State/Province" ItemStyle-Width="10%"
                                        SortExpression="State" />
                                    <asp:BoundField DataField="ZipCode" HeaderText="Zip/Postal Code" SortExpression="ZipCode" ItemStyle-Width="15%"/>
                                    <asp:BoundField DataField="Country" HeaderText="Country" SortExpression="Country" ItemStyle-Width="10%"/>
                                    <asp:BoundField DataField="Fax" HeaderText="Fax" SortExpression="Fax" ItemStyle-Width="10%"/>
                                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" ItemStyle-Width="10%"/>
                                </Columns>
                            </asp:GridView>





                        </div> 

                        </div>
                 </div>  

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

