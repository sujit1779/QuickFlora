Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class clsAlertSystem

#Region "Alert section"

    Public Function GetAlertList(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, Optional ByVal AlertID As String = "") As DataSet

        Dim ds As New DataSet

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetAlertInformation]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                If AlertID <> "" Then Command.Parameters.AddWithValue("AlertID", AlertID)

                Try
                    Dim daAlertList As New SqlDataAdapter(Command)
                    daAlertList.Fill(ds)
                Catch ex As Exception

                End Try

            End Using
        End Using

        Return ds

    End Function

    Public Function DeleteAlertInformation(ByVal AlertID As String) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[DeleteAlertInformation]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("AlertID", AlertID)

                Try
                    Command.Connection.Open()
                    Command.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    Return False
                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    Public Function InsertAlertInformation(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal LocationID As String, _
                                           ByVal AlertName As String, ByVal AlertType As String, ByVal Active As Boolean, ByVal FrequencyHour As Int16, ByVal FrequencyMinutes As Int16, _
                                           ByVal OrderStatus As String, ByVal AlertByHours As Int16, ByVal AlertByMinutes As Int16, ByVal AlertPriority As String, ByVal CreatedBy As String) As String

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[InsertAlertInformation]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                Command.Parameters.AddWithValue("LocationID", LocationID)
                Command.Parameters.AddWithValue("AlertName", AlertName)
                Command.Parameters.AddWithValue("AlertType", AlertType)

                Command.Parameters.AddWithValue("Active", Active)
                Command.Parameters.AddWithValue("FrequencyHour", FrequencyHour)
                Command.Parameters.AddWithValue("FrequencyMinutes", FrequencyMinutes)

                Command.Parameters.AddWithValue("OrderStatus", OrderStatus)
                Command.Parameters.AddWithValue("AlertByHours", AlertByHours)
                Command.Parameters.AddWithValue("AlertByMinutes", AlertByMinutes)

                Command.Parameters.AddWithValue("AlertPriority", AlertPriority)
                Command.Parameters.AddWithValue("CreatedBy", CreatedBy)

                Try

                    Command.Connection.Open()
                    Return Command.ExecuteScalar()

                Catch ex As Exception
                    Return ""
                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    Public Function UpdateAlertInformation(ByVal AlertID As String, ByVal AlertName As String, ByVal AlertType As String, ByVal Active As Boolean, ByVal FrequencyHour As Int16, ByVal FrequencyMinutes As Int16, _
                                           ByVal OrderStatus As String, ByVal AlertByHours As Int16, ByVal AlertByMinutes As Int16, ByVal AlertPriority As String) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[UpdateAlertInformation]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("AlertID", AlertID)
                Command.Parameters.AddWithValue("AlertName", AlertName)
                Command.Parameters.AddWithValue("AlertType", AlertType)

                Command.Parameters.AddWithValue("Active", Active)
                Command.Parameters.AddWithValue("FrequencyHour", FrequencyHour)
                Command.Parameters.AddWithValue("FrequencyMinutes", FrequencyMinutes)

                Command.Parameters.AddWithValue("OrderStatus", OrderStatus)
                Command.Parameters.AddWithValue("AlertByHours", AlertByHours)
                Command.Parameters.AddWithValue("AlertByMinutes", AlertByMinutes)

                Command.Parameters.AddWithValue("AlertPriority", AlertPriority)

                Try

                    Command.Connection.Open()
                    Command.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    Return False
                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    Public Function GetEmployeeInformation(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal EmployeeID As String) As DataSet

        Dim ds As New DataSet

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetEmployeeInformation]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                If EmployeeID <> "" Then Command.Parameters.AddWithValue("EmployeeID", EmployeeID)
                ''Command.Parameters.AddWithValue("EmployeeID", EmployeeID)

                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)
                Catch ex As Exception

                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

        Return ds

    End Function


#End Region

#Region "Emp section"

    Public Function GetAlertNotificationList(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal AlertID As String) As DataSet

        Dim ds As New DataSet

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetAlertNotificationList]", Connection)
                Command.CommandType = CommandType.StoredProcedure
                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("AlertID", AlertID)

                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)
                Catch ex As Exception

                End Try

            End Using
        End Using

        Return ds

    End Function

    Public Function InsertAlertNotificationList(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal LocationID As String, ByVal AlertID As String, ByVal EmployeeID As String, ByVal SendSMS As Boolean, ByVal SendEmail As Boolean) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[InsertAlertNotificationList]", Connection)
                Command.CommandType = CommandType.StoredProcedure
                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("LocationID", LocationID)
                Command.Parameters.AddWithValue("AlertID", AlertID)
                Command.Parameters.AddWithValue("EmployeeID", EmployeeID)
                Command.Parameters.AddWithValue("SendEmail", SendEmail)
                Command.Parameters.AddWithValue("SendSMS", SendSMS)

                Try
                    Command.Connection.Open()
                    Command.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    Return False
                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    Public Function UpdateAlertNotificationList(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal TempAlertID As String, ByVal AlertID As String) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[UpdateAlertNotificationList]", Connection)
                Command.CommandType = CommandType.StoredProcedure
                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("TempAlertID", TempAlertID)
                Command.Parameters.AddWithValue("AlertID", AlertID)

                Try
                    Command.Connection.Open()
                    Command.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    Return False
                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    Public Function DeleteAlertNotificationList(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal RowID As String) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[DeleteAlertNotificationList]", Connection)
                Command.CommandType = CommandType.StoredProcedure
                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("RowID", RowID)

                Try
                    Command.Connection.Open()
                    Command.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    Return False
                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

#End Region

    Public Function GetAlertInformationLogs(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal LocationID As String, _
                                                    ByVal AlertType As String, ByVal OrderStatus As String, ByVal SeachField As String, ByVal SearchValue As String, ByVal FromDate As String, ByVal ToDate As String) As DataSet

        Dim ds As New DataSet

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetAlertInformationLogs]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                Command.Parameters.AddWithValue("LocationID", LocationID)
                Command.Parameters.AddWithValue("AlertType", AlertType)
                Command.Parameters.AddWithValue("OrderStatus", OrderStatus)

                Command.Parameters.AddWithValue("FromDate", FromDate)
                Command.Parameters.AddWithValue("ToDate", ToDate)

                If SeachField = "AlertID" Then
                    Command.Parameters.AddWithValue("AlertID", SearchValue)
                ElseIf SeachField = "AlertName" Then
                    Command.Parameters.AddWithValue("AlertName", SearchValue)
                ElseIf SeachField = "OrderNumber" Then
                    Command.Parameters.AddWithValue("OrderNumber", SearchValue)
                End If

                Try
                    Dim daAlertList As New SqlDataAdapter(Command)
                    daAlertList.Fill(ds)
                Catch ex As Exception

                End Try

            End Using
        End Using

        Return ds

    End Function

    Public Function GetEmployeeAlertLogs(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal InlineNumber As String) As DataSet

        Dim ds As New DataSet

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetEmployeeAlertLogs]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("InlineNumber", InlineNumber)

                Try
                    Dim daAlertList As New SqlDataAdapter(Command)
                    daAlertList.Fill(ds)
                Catch ex As Exception

                End Try

            End Using
        End Using

        Return ds

    End Function


#Region "New Alerts"

    Public Function GetActiveAlerts(Optional ByVal CompanyID As String = "", Optional ByVal DivisionID As String = "", Optional ByVal DepartmentID As String = "", Optional ByVal LocationID As String = "") As DataTable

        Using connection As New SqlConnection((ConfigurationManager.AppSettings("ConnectionString")))
            Using command As New SqlCommand("[enterprise].[GetActiveAlerts]", connection)
                command.CommandType = CommandType.StoredProcedure

                command.Parameters.AddWithValue("CompanyID", CompanyID)
                command.Parameters.AddWithValue("DivisionID", DivisionID)
                command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                command.Parameters.AddWithValue("LocationID", LocationID)

                Try
                    Dim dt As New DataTable
                    Dim da As New SqlDataAdapter(command)
                    da.Fill(dt)

                    Return dt
                Catch ex As Exception

                    Return Nothing

                End Try

            End Using
        End Using

    End Function

    Public Function GetOrderListActiveCompanies(Optional ByVal CompanyID As String = "", Optional ByVal DivisionID As String = "", Optional ByVal DepartmentId As String = "", Optional ByVal OrderNumber As String = "") As DataTable

        Using connection As New SqlConnection((ConfigurationManager.AppSettings("ConnectionString")))
            Using command As New SqlCommand("[enterprise].[GetOrderListActiveCompanies]", connection)

                command.CommandType = CommandType.StoredProcedure
                command.Parameters.AddWithValue("CompanyID", CompanyID)
                command.Parameters.AddWithValue("DivisionID", DivisionID)
                command.Parameters.AddWithValue("DepartmentId", DepartmentId)
                command.Parameters.AddWithValue("OrderNumber", OrderNumber)

                Try
                    Dim dt As New DataTable
                    Dim da As New SqlDataAdapter(command)
                    da.Fill(dt)

                    Return dt
                Catch ex As Exception

                    Return Nothing

                End Try

            End Using
        End Using

    End Function

    Public Function GetOrderListActiveCompaniesForItemTypeAlert(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal LocationID As String, _
    ByVal ItemID As String, Optional ByVal OrderNumber As String = "") As DataTable

        Using connection As New SqlConnection((ConfigurationManager.AppSettings("ConnectionString")))
            Using command As New SqlCommand("[enterprise].[GetOrderListActiveCompaniesForItemTypeAlert]", connection)

                command.CommandType = CommandType.StoredProcedure
                command.Parameters.AddWithValue("CompanyID", CompanyID)
                command.Parameters.AddWithValue("DivisionID", DivisionID)
                command.Parameters.AddWithValue("DepartmentId", DepartmentID)
                command.Parameters.AddWithValue("LocationID", LocationID)
                command.Parameters.AddWithValue("ItemID", ItemID)

                command.Parameters.AddWithValue("OrderNumber", OrderNumber)

                Try
                    Dim dt As New DataTable
                    Dim da As New SqlDataAdapter(command)
                    da.Fill(dt)

                    Return dt
                Catch ex As Exception

                    Return Nothing

                End Try

            End Using
        End Using

    End Function

    Public Function filterOrders(ByVal dtOrders As DataTable, ByVal rowOrder As DataRow, Optional ByVal OrderNumber As String = "") As DataTable

        Dim dv As DataView = dtOrders.DefaultView

        Dim filterExpression As String = String.Format("CompanyID = '{0}' AND DivisionID = '{1}' AND DepartmentID = '{2}' AND LocationID = '{3}' AND ", _
                                                 rowOrder("CompanyID").ToString(), rowOrder("DivisionID").ToString(), rowOrder("DepartmentID").ToString(), _
                                                 rowOrder("LocationID").ToString())

        Select Case rowOrder("AlertType")
            Case "DestinationType"

                dv.RowFilter = filterExpression + String.Format("DestinationType = '{0}'", rowOrder("OrderStatus").ToString())


            Case "OccassionType"

                dv.RowFilter = filterExpression + String.Format("OccassionCode = '{0}'", rowOrder("OrderStatus").ToString())

            Case "DeliveryType"

                dv.RowFilter = filterExpression + String.Format("ShipMethod = '{0}'", rowOrder("OrderStatus").ToString())

            Case "PaymentType"

                dv.RowFilter = filterExpression + String.Format("PaymentMethod = '{0}'", rowOrder("OrderStatus").ToString())

            Case "OrderType"

                dv.RowFilter = filterExpression + String.Format("OrderType = '{0}'", rowOrder("OrderStatus").ToString())

            Case "OrderAmount"

                dv.RowFilter = filterExpression + String.Format("OrderTotal < {0}", rowOrder("OrderStatus").ToString())

            Case "ItemType"

                'dv.RowFilter = filterExpression + String.Format("ItemType = '{0}'", rowOrder("OrderStatus").ToString())
                Return GetOrderListActiveCompaniesForItemTypeAlert(rowOrder("CompanyID").ToString(), rowOrder("DivisionID").ToString(), _
                                                                            rowOrder("DepartmentID").ToString(), rowOrder("LocationID").ToString(), _
                                                                            rowOrder("OrderStatus").ToString(), OrderNumber)

        End Select

        Return dv.ToTable()


    End Function

    Public Function processAlert(ByVal rowOrder As DataRow, ByVal rowAlert As DataRow) As String

        Dim CompanyID As String = rowAlert("CompanyID")
        Dim DivisionID As String = rowAlert("DivisionID")
        Dim DepartmentID As String = rowAlert("DepartmentID")
        Dim OrderStatus As String = rowAlert("OrderStatus")
        'Dim DelayedByHours As Integer = rowAlert("AlertByHours")
        'Dim DelayedByMinutes As Integer = rowAlert("AlertByMinutes")
        Dim FrequencyHour As Integer = rowAlert("FrequencyHour")
        Dim FrequencyMinutes As Integer = rowAlert("FrequencyMinutes")
        Dim AlertPriority As String = rowAlert("AlertPriority")
        Dim LocationID As String = rowAlert("LocationID")
        Dim AlertID As Integer = rowAlert("AlertID")
        Dim AlertName As String = rowAlert("AlertName")
        Dim AlertType As String = rowAlert("AlertType")
        Dim CreatedBy As String = rowAlert("CreatedBy")
        Dim CreatedDate As String = rowAlert("CreatedDate")

        If OrderStatus <> "" Then

            Dim m As Integer = 0
            'Loop for send alerd for all order for selected Alert 

            'Dim dt_order_details As New DataTable
            Dim ordernumber As String = rowOrder("OrderNumber")
            Dim assignedto As String = rowOrder("AssignedTo")
            Dim salesperson As String = rowOrder("SalesPerson")
            Dim billingname As String = rowOrder("BillingName")
            Dim shippingname As String = rowOrder("shippingName")
            Dim shippingaddress As String = rowOrder("ShippingAddress")
            Dim OrderLocationID As String = rowOrder("LocationID")

            Dim destinationType As String = rowOrder("DestinationType")
            Dim occassionType As String = rowOrder("OccassionCode")
            Dim deliveryType As String = rowOrder("ShipMethod")
            Dim paymentType As String = rowOrder("PaymentMethod")
            Dim orderType As String = rowOrder("OrderType")
            Dim orderAmount As String = rowOrder("OrderTotal")

            'Dim dt_order_details As DataTable = dtOrder.Clone()
            If OrderLocationID <> LocationID Then
                Return ""
                'Continue For
            End If

            Dim objlogs As New clsAlertSystemLogs
            objlogs.CompanyID = CompanyID
            objlogs.DivisionID = DivisionID
            objlogs.DepartmentID = DepartmentID
            objlogs.LocationID = LocationID
            objlogs.AlertID = AlertID
            objlogs.AlertName = AlertName
            objlogs.AlertType = AlertType
            objlogs.OrderStatus = OrderStatus
            objlogs.OrderNumber = ordernumber

            Dim ResponseMessge As String = ""

            If CheckAlertAcknowledgementAndLimit(AlertID, ordernumber, FrequencyMinutes, FrequencyHour, objlogs, ResponseMessge) Then
                Return ResponseMessge
                'Response.Write(ResponseMessge)
                'Continue For
            End If

            CheckAlertTypeAndSendAlert(AlertID, AlertPriority, AlertName, ordernumber, assignedto, billingname, shippingname, shippingaddress, _
                                        "", CreatedBy, CreatedDate, objlogs, AlertType, OrderStatus, ResponseMessge, FrequencyMinutes, FrequencyHour)

            Return ResponseMessge

            'If AlertType = "DestinationType" And OrderStatus = destinationType Then

            '    CheckAlertTypeAndSendAlert(AlertID, AlertPriority, AlertName, ordernumber, assignedto, billingname, shippingname, shippingaddress, _
            '                                "", CreatedBy, CreatedDate, objlogs, AlertType, OrderStatus, ResponseMessge)
            '    Return ResponseMessge
            '    'Continue For

            'End If

            'If AlertType = "OccassionType" And OrderStatus = occassionType Then

            '    CheckAlertTypeAndSendAlert(AlertID, AlertPriority, AlertName, ordernumber, assignedto, billingname, shippingname, shippingaddress, _
            '                                "", CreatedBy, CreatedDate, objlogs, AlertType, OrderStatus, ResponseMessge)
            '    Return ResponseMessge
            '    'Continue For

            'End If

            'If AlertType = "DeliveryType" And OrderStatus = deliveryType Then

            '    CheckAlertTypeAndSendAlert(AlertID, AlertPriority, AlertName, ordernumber, assignedto, billingname, shippingname, shippingaddress, _
            '                                "", CreatedBy, CreatedDate, objlogs, AlertType, OrderStatus, ResponseMessge)
            '    Return ResponseMessge
            '    'Continue For

            'End If

            'If AlertType = "PaymentType" And OrderStatus = paymentType Then

            '    CheckAlertTypeAndSendAlert(AlertID, AlertPriority, AlertName, ordernumber, assignedto, billingname, shippingname, shippingaddress, _
            '                                "", CreatedBy, CreatedDate, objlogs, AlertType, OrderStatus, ResponseMessge)
            '    Return ResponseMessge
            '    'Continue For

            'End If

            'If AlertType = "OrderType" And OrderStatus = orderType Then

            '    CheckAlertTypeAndSendAlert(AlertID, AlertPriority, AlertName, ordernumber, assignedto, billingname, shippingname, shippingaddress, _
            '                                "", CreatedBy, CreatedDate, objlogs, AlertType, OrderStatus, ResponseMessge)
            '    Return ResponseMessge
            '    'Continue For

            'End If

            'If AlertType = "OrderAmount" Then
            '    If Convert.ToDouble(OrderStatus) <= Convert.ToDouble(orderAmount) Then
            '        CheckAlertTypeAndSendAlert(AlertID, AlertPriority, AlertName, ordernumber, assignedto, billingname, shippingname, shippingaddress, _
            '                                    "", CreatedBy, CreatedDate, objlogs, AlertType, OrderStatus, ResponseMessge)
            '        Return ResponseMessge
            '        'Continue For
            '    End If
            'End If

            'If AlertType = "ItemType" Then  'And OrderStatus = orderAmount Then

            '    Dim ItemID As String = OrderStatus
            '    If CheckOrderForItemID(CompanyID, DivisionID, DepartmentID, ordernumber, ItemID) Then
            '        CheckAlertTypeAndSendAlert(AlertID, AlertPriority, AlertName, ordernumber, assignedto, billingname, shippingname, shippingaddress, _
            '                                "", CreatedBy, CreatedDate, objlogs, AlertType, OrderStatus, ResponseMessge)
            '        Return ResponseMessge
            '    End If

            'End If

        End If

    End Function

    Public Function CheckAlertAcknowledgementAndLimit(ByVal AlertID As String, ByVal ordernumber As String, ByVal FrequencyMinutes As Integer, ByVal FrequencyHour As Integer, _
    ByVal objlogs As clsAlertSystemLogs, ByRef ResponseMessage As String) As Boolean

        Dim dt_CheckAlertAlertAcknowledg As New DataTable
        dt_CheckAlertAlertAcknowledg = objlogs.CheckAlertAlertAcknowledg()

        If dt_CheckAlertAlertAcknowledg.Rows.Count <> 0 Then
            Return True     'Continue For
        End If

        Dim dt_logs As New DataTable
        dt_logs = objlogs.AlertInformationLogscount()

        If dt_logs.Rows.Count >= 10 Then
            'Response.Write("<br>Already 10 alert send so checcking next..ordernumber " & ordernumber & "<br>")
            ResponseMessage = "<br>Already 10 alert send so checcking next..ordernumber " & ordernumber & "<br>"
            Return True     'Continue For
        Else
            If dt_logs.Rows.Count <> 0 Then
                Dim date_logs As Date
                Try
                    date_logs = dt_logs.Rows(0)("SendAt")
                Catch ex As Exception
                End Try

                date_logs = date_logs.AddMinutes(FrequencyMinutes)
                date_logs = date_logs.AddHours(FrequencyHour)

                If Date.Now < date_logs Then
                    'Response.Write("<br>Alert send next frequency count down. for ordernumber " & ordernumber & "...next alert will send at " & date_logs.ToString & " AlertID = " & AlertID & "<br>")
                    ResponseMessage = "<br>Alert send next frequency count down. for ordernumber " & ordernumber & "...next alert will send at " & date_logs.ToString & " AlertID = " & AlertID & "<br>"
                    Return True     ''Continue For
                End If
            End If
        End If


    End Function

    Public Sub CheckAlertTypeAndSendAlert(ByVal AlertID As String, ByVal AlertPriority As String, ByVal AlertName As String, ByVal OrderNumber As String, ByVal AssignedTo As String, _
    ByVal BillingName As String, ByVal ShippingName As String, ByVal ShippingAddress As String, ByVal Priority As String, _
    ByVal CreatedBy As String, ByVal CreatedDate As String, _
    ByVal objlogs As clsAlertSystemLogs, ByVal AlertType As String, ByVal AlertValue As String, ByRef ResponseMessage As String, _
    ByVal FrequencyMinutes As String, ByVal FrequencyHours As String)

        Dim alert_str As String = ""
        alert_str = "QuickFlora System Alert! (" & AlertPriority & " Priority)"
        alert_str += "<br><br>Order # " & OrderNumber & " for " + AlertType + ": " + AlertValue + " for delivery " + Date.Now.Date & " needs attention"
        alert_str += "<br><br>Customer Name: " & BillingName & " ; <br> Recipient Name: " & ShippingName & " ; <br> Delivery Address: " & ShippingAddress
        alert_str += "<br><br>Order is booked for " + AlertType + ": " + AlertValue ' Picked yet and its Priority Time is :" & Priority
        alert_str += "<br><br>This alert will keep firing for every " + FrequencyHours + " hours and " + FrequencyMinutes + " minutes until you acknowledge the alert in this email notice."
        alert_str += "<br>@"

        alert_str += "<br><br>This alert was created by " & CreatedBy & " on " & CreatedDate & ". If you would like to edit these alerts, please login to your Administrator back office for Quickflora and on the left side menu, click on 'Alerts'. <br /><br />"
        objlogs.AlertMessage = alert_str.Replace("<br>", " ")

        objlogs.InsertAlertLogs()

        Dim dt_logs As New DataTable
        dt_logs = objlogs.AlertInformationLogscount()
        Dim Inlinenumber As Integer
        If dt_logs.Rows.Count <> 0 Then
            Try
                Inlinenumber = dt_logs.Rows(0)("Inlinenumber")
            Catch ex As Exception
            End Try
        End If

        objlogs.Inlinenumber = Inlinenumber
        'objlogs.emailsubject = "(" & AlertPriority & ") Alert for Order # " & ordernumber & " regarding not Picked yet."
        objlogs.emailsubject = "Order #" & OrderNumber & " " & AlertName
        objlogs.smsmessage = "Alert! (" & AlertPriority & ") Order # " & OrderNumber & " for " + AlertType + " : " + AlertValue + " at " + Date.Now.Date & " needs attention as Order is just booked."
        objlogs.sendalert(alert_str, AssignedTo)

        'Response.Write("Alerts sent for " + AlertType + " : " + AlertValue + " for ordernumber = " & OrderNumber & " Alert ID =" & AlertID)
        ResponseMessage = "Alerts sent for " + AlertType + " : " + AlertValue + " for ordernumber = " & OrderNumber & " Alert ID =" & AlertID

    End Sub

#End Region

End Class
