<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="Home.aspx.vb" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
PO Dashboard  
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
 <!-- BEGIN Dashbord-->
                <div class="__home-dashboard">
                    <ul class="dashboard-links">
                        <li><a href="BPO.aspx?page=1">
                            <p>
                                <img src="assets/admin/layout/img/dashboard/pos.png" alt="" /></p>
                            <h3>
                                Batch PO</h3>
                        </a></li>
                        <li><a href="RequisitionOrderList.aspx">
                            <p>
                                <img src="assets/admin/layout/img/dashboard/transactions.png" alt="" /></p>
                            <h3>
                                Requisition</h3>
                        </a></li>
                        <li class="visible-xs clear"></li>
                        <li><a href="InventoryStatus.aspx">
                            <p>
                                <img src="assets/admin/layout/img/dashboard/functions.png" alt="" /></p>
                            <h3>
                                Functions</h3>
                        </a></li>
                        <li class="visible-sm visible-md visible-lg clear"></li>
                        <li><a href="RequestReport.aspx">
                            <p>
                                <img src="assets/admin/layout/img/dashboard/reports.png" alt="" /></p>
                            <h3>
                                Reports</h3>
                        </a></li>
                        <li class="visible-xs clear"></li>
                        <li><a href="OrderLocationPreferences.aspx">
                            <p>
                                <img src="assets/admin/layout/img/dashboard/system-setup.png" alt="" /></p>
                            <h3>
                                System Setup</h3>
                        </a></li>
                        <li><a style="text-decoration:none;" href="#" onclick="overlay()">
                            <p>
                                <img src="assets/admin/layout/img/dashboard/support.png" alt="" /></p>
                            <h3>
                                Support</h3>
                        </a></li>
                    </ul>
                </div>
                <!-- END Dashbord-->

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">



</asp:Content>

