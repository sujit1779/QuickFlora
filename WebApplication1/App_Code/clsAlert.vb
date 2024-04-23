Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class clsAlert


    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    Public CompanyID As String
    Public DivisionID As String
    Public DepartmentID As String
    ' Public OrderNumber As String

    Public Function OrderHeaderDetails() As DataTable

        Dim today As String

        today = Date.Now.Date.Month & "/" & Date.Now.Date.Day & "/" & Date.Now.Date.Year

        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select OrderNumber,  '' AS AssignedTo, EmployeeID, DriverID from [OrderHeader] where  CompanyID='" & CompanyID & "' And DivisionID='" & DivisionID & "' And DepartmentID='" & DepartmentID & "' And  isnull([Invoiced],0) <> 1 and isnull([Canceled],0) <> 1 and isnull([Backordered],0) <> 1   AND [Posted] = 1 and convert(datetime, Convert(nvarchar(36),OrderHeader.OrderShipDate,101)) >='" & today & "'  and convert(datetime, Convert(nvarchar(36),OrderHeader.OrderShipDate,101)) <='" & today & "'"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            da.SelectCommand = com
            da.Fill(dt)
            Return dt

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return dt

        End Try

    End Function

    Public Function AlertDetailsPrecourtionary() As DataTable

        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from [AlertInformationList] where  CompanyID='" & CompanyID & "' And DivisionID='" & DivisionID & "' And DepartmentID='" & DepartmentID & "' And  ( AlertType ='Status' ) and  Active = 1 "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            da.SelectCommand = com
            da.Fill(dt)
            Return dt

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return dt

        End Try

    End Function

    Public Function CheckIfAlert() As DataTable

        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select AlertID from [AlertInformationList] where  CompanyID='" & CompanyID & "' And DivisionID='" & DivisionID & "' And DepartmentID='" & DepartmentID & "' and  Active = 1 And   [AlertType] ='Status' "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            da.SelectCommand = com
            da.Fill(dt)
            Return dt

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return dt

        End Try

    End Function

    Public Function sendstatusAlert(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal OrderNumber As String, ByVal Status As String) As Boolean

        Me.CompanyID = CompanyID
        Me.DivisionID = DivisionID
        Me.DepartmentID = DepartmentID

        Dim dt_checkAlert As New DataTable
        dt_checkAlert = CheckIfAlert()

        If dt_checkAlert.Rows.Count = 0 Then
            Return False
        End If

        Dim objmail As New clsCreditCardChargeOnRebook
        objmail.CompanyID = Me.CompanyID
        objmail.DivisionID = Me.DivisionID
        objmail.DepartmentID = Me.DepartmentID
        ' objmail.EmailSending("Status Alert reach to event", "Satus alert for order number =" & OrderNumber, "imy@quickflora.com", "imy@quickflora.com")

        alertPrecourtionary(OrderNumber, Status)

        Return False

    End Function

    Public Sub alertPrecourtionary(ByVal OrderNumber As String, ByVal Status As String)
        Dim dt_Alert_Precourtionary As New DataTable
        dt_Alert_Precourtionary = AlertDetailsPrecourtionary()

        If dt_Alert_Precourtionary.Rows.Count <> 0 Then
            Dim n As Integer = 0
            Dim objmail As New clsCreditCardChargeOnRebook
            objmail.CompanyID = Me.CompanyID
            objmail.DivisionID = Me.DivisionID
            objmail.DepartmentID = Me.DepartmentID


            'Loop for all Alert set as Precourtionary
            For n = 0 To dt_Alert_Precourtionary.Rows.Count - 1
                Dim OrderStatus As String = ""
                Dim DelayedByHours As Integer = 0
                Dim DelayedByMinutes As Integer = 0

                Dim FrequencyHour As Integer = 0
                Dim FrequencyMinutes As Integer = 0

                Dim AlertPriority As String = ""

                Dim LocationID As String = ""
                Dim AlertID As Integer = 0
                Dim AlertName As String = ""
                Dim AlertType As String = ""

                Dim CreatedBy As String = ""
                Dim CreatedDate As String = ""



                Try
                    CreatedBy = dt_Alert_Precourtionary.Rows(n)("CreatedBy")
                Catch ex As Exception
                End Try
                Try
                    CreatedDate = dt_Alert_Precourtionary.Rows(n)("CreatedDate")
                Catch ex As Exception
                End Try


                Try
                    FrequencyHour = dt_Alert_Precourtionary.Rows(n)("FrequencyHour")
                Catch ex As Exception
                End Try

                Try
                    FrequencyMinutes = dt_Alert_Precourtionary.Rows(n)("FrequencyMinutes")
                Catch ex As Exception
                End Try

                Try
                    LocationID = dt_Alert_Precourtionary.Rows(n)("LocationID")
                Catch ex As Exception
                End Try

                Try
                    AlertID = dt_Alert_Precourtionary.Rows(n)("AlertID")
                Catch ex As Exception
                End Try

                Try
                    AlertName = dt_Alert_Precourtionary.Rows(n)("AlertName")
                Catch ex As Exception
                End Try


                Try
                    AlertType = dt_Alert_Precourtionary.Rows(n)("AlertType")
                Catch ex As Exception
                End Try


                Try
                    OrderStatus = dt_Alert_Precourtionary.Rows(n)("OrderStatus")
                Catch ex As Exception
                End Try

                Try
                    DelayedByHours = dt_Alert_Precourtionary.Rows(n)("AlertByHours")
                Catch ex As Exception
                End Try

                Try
                    DelayedByMinutes = dt_Alert_Precourtionary.Rows(n)("AlertByMinutes")
                Catch ex As Exception
                End Try

                Try
                    AlertPriority = dt_Alert_Precourtionary.Rows(n)("AlertPriority")
                Catch ex As Exception
                End Try
                Dim objlogs As New clsAlertSystemLogs
                objlogs.CompanyID = Me.CompanyID
                objlogs.DivisionID = Me.DivisionID
                objlogs.DepartmentID = Me.DepartmentID
                objlogs.LocationID = LocationID
                objlogs.AlertID = AlertID
                objlogs.AlertName = AlertName
                objlogs.AlertType = AlertType
                objlogs.OrderStatus = OrderStatus
                objlogs.OrderNumber = OrderNumber

                Dim dt_order_details As New DataTable
                Dim dt_logs As New DataTable
                Dim assignedto As String = ""
                Dim salesperson As String = ""
                Dim Driver As String = ""

                'dt_order_details = OrderHeaderDetailsOrderNumber(dt_Order)
                dt_order_details = OrderHeaderDetailsOrderNumber(OrderNumber, LocationID)
                Dim billingname As String = ""
                Dim shippingname As String = ""
                Dim shippingaddress As String = ""

                If dt_order_details.Rows.Count = 0 Then
                    Continue For
                Else
                    Try
                        billingname = dt_order_details.Rows(0)(1)
                    Catch ex As Exception

                    End Try

                    Try
                        shippingname = dt_order_details.Rows(0)(2)
                    Catch ex As Exception

                    End Try

                    Try
                        shippingaddress = dt_order_details.Rows(0)(3)
                    Catch ex As Exception

                    End Try

                End If

                Dim Priority As String = ""
                Try
                    Priority = dt_order_details.Rows(0)("Priority")
                Catch ex As Exception
                End Try

                Try
                    assignedto = dt_order_details.Rows(0)("AssignedTo")
                Catch ex As Exception

                End Try

                Try
                    salesperson = dt_order_details.Rows(0)("EmployeeID")
                Catch ex As Exception

                End Try


                Try
                    Driver = dt_order_details.Rows(0)("DriverID")
                Catch ex As Exception

                End Try

                ' first case when Alert is for "Picked"


                If OrderStatus = Status Then
                    ' objmail.EmailSending("Status Alert reach to send aler main function 2 stage", "Satus alert for order Status =" & Status & "-" & Priority, "imy@quickflora.com", "imy@quickflora.com")
                    Dim Picked As Boolean = False
                    Dim alert_str As String = ""
                    alert_str = "<br>Alert! (" & AlertPriority & ")"
                    alert_str += "<br>Order # " & OrderNumber & " for delivery " & Date.Now.Date & " needs attention"
                    alert_str += "<br>Customer Name: " & billingname & " ; Recipient Name: " & shippingname & " ; Delivery Adress: " & shippingaddress
                    alert_str += "<br>Order " & Status & " and its Priority Time is " & Priority
                    'alert_str += "Click here to acknowledge receiving this alert"
                    alert_str += "<br>This alert was created by " & CreatedBy & " on " & CreatedDate & ". If you would like to edit these alerts, please login to your Administrator back office for Quickflora and on the left side menu, click on 'Alerts'. "
                    objlogs.AlertMessage = alert_str.Replace("<br>", " ")
                    objlogs.InsertAlertLogs()
                    dt_logs = objlogs.AlertInformationLogscount()
                    Dim Inlinenumber As Integer
                    If dt_logs.Rows.Count <> 0 Then
                        Try
                            Inlinenumber = dt_logs.Rows(0)("Inlinenumber")
                        Catch ex As Exception
                        End Try
                    End If
                    objlogs.Inlinenumber = Inlinenumber
                    'objlogs.emailsubject = "(" & AlertPriority & ") Alert for Order # " & OrderNumber & " Status regarding " & Status & "."
                    objlogs.emailsubject = "Order #" & OrderNumber & " " & Status & "."
                    objlogs.smsmessage = "Alert! (" & AlertPriority & ") Order # " & OrderNumber & " for delivery " & Date.Now.Date & " is " & Status & " and its Priority Time is :" & Priority

                    If Status = "Delivered" Then
                        objlogs.sendalert(alert_str, "", salesperson)
                    ElseIf Status = "Routed" Then
                        objlogs.sendalert(alert_str, "", Driver)
                    Else
                        objlogs.sendalert(alert_str)
                    End If

                End If
                'oh.Picked, oh.Shipped, oh.Routed, oh.Delivered, oh.Canceled,
            Next
        End If
    End Sub




    Public Function OrderHeaderDetailsOrderNumber(ByVal OrderNumber As String, ByVal locationid As String) As DataTable

        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select H.OrderNumber, ltrim(rtrim(ltrim(rtrim(C.CustomerFirstName)) + ' ' + ltrim(rtrim(C.CustomerLastName)))) AS BillingName, ltrim(rtrim(ltrim(rtrim(H.ShippingFirstName)) + ' ' + ltrim(rtrim(H.ShippingLastName)))) AS ShippingName, ltrim(rtrim(ltrim(rtrim(H.ShippingAddress1)) + ' ' + ltrim(rtrim(H.ShippingAddress2)) + ' ' + ltrim(rtrim(H.ShippingAddress3)) + ' ' + ltrim(rtrim(H.ShippingCity)) + ' ' + ltrim(rtrim(H.ShippingState)) + ' ' + ltrim(rtrim(H.ShippingZip)) + ' ' + ltrim(rtrim(H.ShippingCountry)))) AS ShippingAddress from [OrderHeader] H INNER JOIN CustomerInformation C ON C.CompanyID = H.CompanyID AND C.DivisionID = H.DivisionID  AND C.DepartmentID = H.DepartmentID AND C.CustomerID = H.CustomerID where  H.CompanyID='" & CompanyID & "' And H.DivisionID='" & DivisionID & "' And H.DepartmentID='" & DepartmentID & "'  and H.OrderNumber='" & OrderNumber & "' and  H.locationid ='" & locationid & "'"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            da.SelectCommand = com
            da.Fill(dt)
            Return dt

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return dt

        End Try

    End Function


End Class
