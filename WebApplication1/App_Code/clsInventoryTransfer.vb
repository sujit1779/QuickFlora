Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class clsInventoryTransfer

    Public Function GetEmployees(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As DataTable

        Dim dt As New DataTable

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("PopulateEmployees", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                Try

                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(dt)

                Catch ex As Exception

                End Try

            End Using
        End Using

        Return dt

    End Function

    Public Function GetNextInventoryTransferNumber(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As String

        Dim NextTransferNumber As String = ""

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetNextInventoryTransferNumber]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                Try
                    Command.Connection.Open()
                    NextTransferNumber = Convert.ToString(Command.ExecuteScalar())

                Catch ex As Exception

                End Try

            End Using
        End Using

        Return NextTransferNumber

    End Function

    Public Function GetInventoryTransferList(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, _
                                             Optional ByVal TransferNumber As String = "") As DataTable

        Dim dt As New DataTable

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetInventoryTransferList]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("TransferNumber", TransferNumber)

                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(dt)

                Catch ex As Exception

                End Try

            End Using
        End Using

        Return dt

    End Function

    Public Function GetInventoryTransferItemsList(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, _
                                                    ByVal TransferNumber As String) As DataTable

        Dim dt As New DataTable

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetInventoryTransferItemsList]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("TransferNumber", TransferNumber)

                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(dt)

                Catch ex As Exception

                End Try

            End Using
        End Using

        Return dt

    End Function

    Public Function UpdateInventoryTransferHeader(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, _
                                                    ByVal TransferNumber As String, TransferDate As String, TansferFromLocation As String, TransferToLocation As String, _
                                                    TransferByEmployee As String, ApprovedByEmployee As String, ApprovedTime As String, TotalItemsTransfer As String) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[UpdateInventoryTransferHeader]", Connection)

                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("TransferNumber", TransferNumber)

                Command.Parameters.AddWithValue("TransferDate", TransferDate)
                Command.Parameters.AddWithValue("TansferFromLocation", TansferFromLocation)
                Command.Parameters.AddWithValue("TransferToLocation", TransferToLocation)
                Command.Parameters.AddWithValue("TransferByEmployee", TransferByEmployee)

                Command.Parameters.AddWithValue("ApprovedByEmployee", ApprovedByEmployee)
                Command.Parameters.AddWithValue("ApprovedTime", ApprovedTime)
                Command.Parameters.AddWithValue("TotalItemsTransfer", TotalItemsTransfer)

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

    Public Function InsertInventoryTransferDetail(ByVal CompanyID As String, DivisionID As String, DepartmentID As String, _
                                                    TransferNumber As String, ItemId As String, TransferQty As String, AddionalItemNotes As String) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[InsertInventoryTransferDetail]", Connection)

                Command.CommandType = CommandType.StoredProcedure
                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                Command.Parameters.AddWithValue("TransferNumber", TransferNumber)
                Command.Parameters.AddWithValue("ItemId", ItemId)
                Command.Parameters.AddWithValue("TransferQty", TransferQty)
                Command.Parameters.AddWithValue("AddionalItemNotes", AddionalItemNotes)

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

    Public Function UpdateInventoryTransferDetail(ByVal RowID As String, TransferQty As String, AddionalItemNotes As String) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[UpdateInventoryTransferDetail]", Connection)

                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("RowID", RowID)
                Command.Parameters.AddWithValue("TransferQty", TransferQty)
                Command.Parameters.AddWithValue("AddionalItemNotes", AddionalItemNotes)

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

    Public Function DeleteInventoryTransferDetail(ByVal RowID As String) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[DeleteInventoryTransferDetail]", Connection)

                Command.CommandType = CommandType.StoredProcedure
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

    Public Function UpdateInventoryReceiveHeader(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, _
                                                    ByVal TransferNumber As String, ReceivedDate As String, TransferToLocation As String, _
                                                    ReceivedByEmployee As String) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[UpdateInventoryReceiveHeader]", Connection)

                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("TransferNumber", TransferNumber)

                Command.Parameters.AddWithValue("ReceivedDate", ReceivedDate)
                Command.Parameters.AddWithValue("ReceivedByEmployee", ReceivedByEmployee)
                Command.Parameters.AddWithValue("TransferToLocation", TransferToLocation)
                
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

    Public Function UpdateInventoryReceiveDetail(ByVal RowID As String, ReceivedQty As String) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[UpdateInventoryReceiveDetail]", Connection)

                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("RowID", RowID)
                Command.Parameters.AddWithValue("ReceivedQty", ReceivedQty)

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

End Class
