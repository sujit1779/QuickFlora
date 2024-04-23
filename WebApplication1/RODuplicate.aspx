<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="RODuplicate.aspx.vb" Inherits="RODuplicate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
  Requisition Duplication
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    
    
        <table border="0" width="100%" id="table51" style="margin-top: 0px; margin-bottom: 0px;">
            <tr>
                <td width="10%">
                    <asp:Label ID="lblGtotal" Visible="true" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <table style="border: solid 1px #808080;" cellpadding="0" cellspacing="0">
                          <tr runat="server" id="tr2" visible="true">
                            <td>
                                <table border="0" width="775" id="table10" cellspacing="0" cellpadding="0">
                                    <!-- Ajax Panel -->
                                    <tr>
                                        <td>
                                            <table border="0" width="775" id="table11" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="176" align="center">
                                                        &nbsp; 
                                                    </td>
                                                    <td width="1" bgcolor="#808080">
                                                    </td>
                                                    <td width="450" align="center" valign="top">
                                                    <br /><br />                                                    
                                                   A new requisition has been created. You can now edit this requisition if you wish to make any changes and to submit.
                                                    <br /><br />
                                                        <asp:HyperLink ID="HyperLink1" CssClass="btn btn-success btn-xs" runat="server">Click to Open New Requisition </asp:HyperLink>
                                                        
                                                    </td>
                                                    <td width="1" bgcolor="#808080">
                                                    </td>
                                                    <td width="147" valign="top">
                                                        &nbsp;&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="176" align="center">
                                                        &nbsp; 
                                                    </td>
                                                    <td width="1" bgcolor="#808080">
                                                    </td>
                                                    <td width="450" align="center" valign="top">
                                                    <br /><br />
                                                        <asp:HyperLink ID="HyperLink2" CssClass="btn btn-success btn-xs" runat="server">Click here to open Requisition list screen</asp:HyperLink>
                                                    <br /><br />                                                        
                                                    </td>
                                                    <td width="1" bgcolor="#808080">
                                                    </td>
                                                    <td width="147" valign="top">
                                                        &nbsp;&nbsp;&nbsp;
                                                    </td>
                                                </tr>                                                
                                            </table>
                                            
                                           
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="10%">
                </td>
            </tr>
        </table>
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

