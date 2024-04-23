<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Company.ascx.vb" Inherits="masterpages_Company" %>
<%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>

                                        <table width="100%" border="0" style="vertical-align:top;">
                     <tr >
                     <td width="0%" align="right" valign="bottom" >
                     
                         <asp:Label ID="lblDate" Visible="false"   runat="server" Text=""></asp:Label>
                    
                     </td>
                         <td width="40%" align="right" valign="bottom">

                             <asp:Label ID="lblTime"  Visible="false"    runat="server" Text="Label"></asp:Label>
                            <div   id="dvtime"></div> 
                     </td>
                         <td width="60%" align="Center"  valign="bottom">
                             <asp:Label ID="lblCompany" runat="server" Text=""></asp:Label>
                     </td>
                        
                     </tr>
                     
                     </table>
                  
                     