Option Strict Off

Imports System.Data.SqlClient
Imports System.Data
Imports DAL
Imports EnterpriseCommon.Configuration
Imports EnterpriseASPClient.Core
Imports EnterpriseClient.Core
Imports System.Diagnostics
Imports PayPal.Payments.DataObjects
Imports PayPal.Payments.Common
Imports PayPal.Payments.Common.Utility
Imports PayPal.Payments.Transactions
Imports System.Net.Mail
Imports System.Text.RegularExpressions
Imports System.IO
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.Shared
Imports AuthorizeNet

Imports System.Web.Script.Serialization
Imports System.Web.Script.Services
Imports System.Collections.Generic

Partial Class TruckingScheduleDetail
    Inherits System.Web.UI.Page



    Public ConnectionString As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim EmployeeID As String = ""
    Dim ScheduleID As String = ""
    Dim obj As New clsTruckingSchedule

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

        ' btnsave.Enabled = False


        If Not IsPostBack Then

            GetInventoryOrigin()

            GetCompanyLocations()

            GetVendorShipMethods()

            ScheduleID = ""
            Try
                ScheduleID = Request.QueryString("ScheduleID")
            Catch ex As Exception

            End Try

            Dim dt As New DataTable
            If ScheduleID <> "" Then
                dt = obj.GetTruckingSchedules(CompanyID, DepartmentID, DivisionID, ScheduleID)
                lbldebug.Text = lbldebug.Text & "<br>" & " Level 1"

                Try
                    If dt.Rows.Count <> 0 Then
                        lbldebug.Text = lbldebug.Text & "<br>" & "dt.Rows.Count:" & dt.Rows.Count

                        txtScheduleID.Text = dt.Rows(0)("ScheduleID").ToString()

                        ''drpInventoryOrigin.Items.FindByValue("Origin").Selected = True
                        Try
                            drpInventoryOrigin.SelectedValue = dt.Rows(0)("Origin").ToString()
                        Catch ex As Exception

                        End Try
                        ''drpInventoryOrigin.Items.FindByValue("Origin").Selected = True
                        Try
                            drpShipMethod.SelectedValue = dt.Rows(0)("ShipMethodID").ToString()
                        Catch ex As Exception

                        End Try
                        ''drpShipMethod.Items.FindByValue("ShipMethodID").Selected = True
                        Try
                            drpLocation.SelectedValue = dt.Rows(0)("LocationID").ToString()
                        Catch ex As Exception

                        End Try
                        ''drpLocation.Items.FindByValue("LocationID").Selected = True
                        Try
                            drpTruckingDay.SelectedValue = dt.Rows(0)("TruckingDay").ToString()
                        Catch ex As Exception

                        End Try
                        ''drpTruckingDay.Items.FindByValue("TruckingDay").Selected = True
                        Try
                            drpArrivalDay.SelectedValue = dt.Rows(0)("ArrivalDay").ToString()
                        Catch ex As Exception

                        End Try
                        ''drpLocation.Items.FindByValue("ArrivalDay").Selected = True
                        Try
                            drpDayCutOff.SelectedValue = dt.Rows(0)("DayCutoff").ToString()
                        Catch ex As Exception

                        End Try
                        ''drpDayCutOff.Items.FindByValue("DayCutoff").Selected = True
                        Try
                            drpHours.SelectedValue = dt.Rows(0)("Hours").ToString()
                        Catch ex As Exception

                        End Try
                        ''drpHours.Items.FindByValue("Hours").Selected = True
                        Try
                            drpMinutes.SelectedValue = dt.Rows(0)("Minutes").ToString()
                        Catch ex As Exception

                        End Try
                        ''drpMinutes.Items.FindByValue("Minutes").Selected = True
                        Try
                            drpAMPM.SelectedValue = dt.Rows(0)("AMPM").ToString()
                        Catch ex As Exception

                        End Try
                        ''drpAMPM.Items.FindByValue("AMPM").Selected = True
                        Try
                            drpTimeZone.SelectedValue = dt.Rows(0)("TimeZone").ToString()
                        Catch ex As Exception

                        End Try
                        ''drpTimeZone.Items.FindByValue("TimeZone").Selected = True
                    End If

                Catch ex As Exception
                    lbldebug.Text = lbldebug.Text & "<br>" & " ex:" & ex.Message
                End Try

            Else
                'dt = obj.GetTruckingSchedules(CompanyID, DepartmentID, DivisionID)
            End If




        End If

        lbldebug.Visible = False

    End Sub

    Public Sub GetInventoryOrigin()

        Dim dtInventoryOrigin As New DataTable
        dtInventoryOrigin = obj.GetInventoryOrigin(CompanyID, DivisionID, DepartmentID)
        If dtInventoryOrigin.Rows.Count <> 0 Then
            drpInventoryOrigin.DataSource = dtInventoryOrigin
            drpInventoryOrigin.DataTextField = "InventoryOriginName"
            drpInventoryOrigin.DataValueField = "InventoryOriginID"
            drpInventoryOrigin.DataBind()
        End If
    End Sub

    Public Sub GetVendorShipMethods()
        Dim dtShipMethod As New DataTable
        dtShipMethod = obj.GetVendorShipMethods(CompanyID, DivisionID, DepartmentID)
        If dtShipMethod.Rows.Count <> 0 Then
            drpShipMethod.DataSource = dtShipMethod
            drpShipMethod.DataTextField = "ShipMethodDescription"
            drpShipMethod.DataValueField = "ShipMethodID"
            drpShipMethod.DataBind()
        End If
    End Sub

    Public Sub GetCompanyLocations()

        Dim dt As New Data.DataTable
        dt = obj.GetCompanyLocations(CompanyID, DivisionID, DepartmentID)
        If dt.Rows.Count <> 0 Then
            drpLocation.DataSource = dt
            drpLocation.DataTextField = "LocationName"
            drpLocation.DataValueField = "LocationID"
            drpLocation.DataBind()

        Else
            drpLocation.Items.Clear()
            Dim item As New ListItem
            item.Text = "DEFAULT"
            item.Value = "DEFAULT"
            drpLocation.Items.Add(item)
        End If

    End Sub

    Protected Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Try
            ScheduleID = Request.QueryString("ScheduleID")
        Catch ex As Exception

        End Try

        If ScheduleID = "" Then
            obj.InsertTruckingSchedule(CompanyID, DivisionID, DepartmentID, drpInventoryOrigin.SelectedValue, drpShipMethod.SelectedValue, drpShipMethod.SelectedItem.Text, drpLocation.SelectedValue, drpTruckingDay.SelectedValue, drpArrivalDay.SelectedValue, drpDayCutOff.SelectedValue, drpHours.SelectedValue, drpMinutes.SelectedValue, drpAMPM.SelectedValue, drpTimeZone.SelectedValue)
            Response.Redirect("TruckingScheduleList.aspx")
        Else
            obj.UpdateTruckingSchedule(CompanyID, DivisionID, DepartmentID, ScheduleID, drpInventoryOrigin.SelectedValue, drpShipMethod.SelectedValue, drpShipMethod.SelectedItem.Text, drpLocation.SelectedValue, drpTruckingDay.SelectedValue, drpArrivalDay.SelectedValue, drpDayCutOff.SelectedValue, drpHours.SelectedValue, drpMinutes.SelectedValue, drpAMPM.SelectedValue, drpTimeZone.SelectedValue)
            Response.Redirect("TruckingScheduleList.aspx")
        End If


    End Sub

    Protected Sub btnback_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnback.Click
        Response.Redirect("TruckingScheduleList.aspx")
    End Sub
End Class
