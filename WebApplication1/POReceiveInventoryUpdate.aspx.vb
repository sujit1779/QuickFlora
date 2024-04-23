Imports System.Data
Imports System.Data.SqlClient

Partial Class POReceiveInventoryUpdate
    Inherits System.Web.UI.Page

    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim PONumber As String = ""
    Dim PODate As String = ""

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        CompanyID = "QuickfloraDemo"    'Session("CompanyID")
        DivisionID = "Default" 'Session("DivisionID")
        DepartmentID = "Default" 'Session("DepartmentID")

        If Not Page.IsPostBack Then
        End If

    End Sub

    Private Sub UpdateInventoryReceivedTodayPO()

        'Find Today POs list
        'Add inventory into in hand
        'find delivery orders list (having backorder items) - order by delivery date, order number, itemid
        'if quantity is enough againast order then remove backorder for that item 

        UpdatePOItemsForReceiving(PODate, PONumber)

        Dim dtPOList As New DataTable
        dtPOList = GetTodaysPOItemsList(PODate, PONumber)

        If dtPOList.Rows.Count > 0 Then

            For Each row As DataRow In dtPOList.Rows

                UpdateItemInventoryByLocationForReceivedPO(row("LocationID"), row("ItemID"), row("ItemQty"), row("PurchaseLineNumber"), PODate)

                'Dim dtOrdersList As New DataTable
                'dtOrdersList = GetOrderItemsForReceivedPO(row("LocationID"), row("ItemID"))

                'If dtOrdersList.Rows.Count > 0 Then

                '    For Each rowItem As DataRow In dtOrdersList.Rows

                '        UpateItemInventoryFutureDeliveryForBackOrderItems(row("LocationID"), rowItem("OrderLineNumber"), row("ItemID"), rowItem("OrderQty"))

                '    Next

                'End If


            Next

        End If

    End Sub


    Private Function UpdatePOItemsForReceiving(PODate As String, ByVal PONumber As String) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[UpdatePOItemsForReceiving]", Connection)
                Command.CommandType = CommandType.StoredProcedure


                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("PONumber", PONumber)
                Command.Parameters.AddWithValue("PODate", PODate)

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

    Private Function GetTodaysPOItemsList(PODate As String, PONumber As String) As DataTable

        Dim dt As New DataTable

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetTodaysPOItemsList]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("PONumber", PONumber)
                Command.Parameters.AddWithValue("PODate", PODate)

                Try

                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(dt)

                Catch ex As Exception

                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

        Return dt

    End Function

    Private Function GetOrderItemsForReceivedPO(LocationID As String, ItemID As String) As DataTable

        Dim dt As New DataTable

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetOrderItemsForReceivedPO]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("LocationID", LocationID)
                Command.Parameters.AddWithValue("ItemID", ItemID)

                Try

                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(dt)

                Catch ex As Exception

                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

        Return dt

    End Function

    Private Function UpdateItemInventoryByLocationForReceivedPO(ByVal LocationID As String, ByVal ItemID As String, ByVal Qty As Integer, ByVal PurchaseLineMumber As String, ByVal PoDate As String) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[UpdateItemInventoryByLocationForReceivedPO]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("LocationID", LocationID)
                Command.Parameters.AddWithValue("ItemID", ItemID)
                Command.Parameters.AddWithValue("QtyOnHand", Qty)
                Command.Parameters.AddWithValue("PoDate", PoDate)

                Command.Parameters.AddWithValue("PurchaseLineNumber", PurchaseLineMumber)

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

    Public Function UpateItemInventoryFutureDeliveryForBackOrderItems(ByVal LocationID As String, OrderLineNumber As String, ByVal ItemID As String, ByVal Qty As Int16) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[UpateItemInventoryFutureDeliveryForBackOrderItems]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                Command.Parameters.AddWithValue("LocationID", LocationID)
                Command.Parameters.AddWithValue("ItemID", ItemID)
                Command.Parameters.AddWithValue("Qty", Qty)

                Command.Parameters.AddWithValue("OrderLineNumber", OrderLineNumber)


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

    Protected Sub btnPO_Click(sender As Object, e As EventArgs) Handles btnPO.Click

        PONumber = txtPO.Text.Trim
        PODate = Convert.ToDateTime(txtReceiveDate.Text.Trim)

        UpdateInventoryReceivedTodayPO()

    End Sub

End Class
