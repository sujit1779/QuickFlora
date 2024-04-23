Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class clsInventoryAssembly

    Public Function GetInventoryAssmeblyList(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, _
                                             Optional ByVal AssemblyID As String = "", Optional ByVal PriceType As String = "") As DataTable

        Dim dt As New DataTable  

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetInventoryAssemblyList]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("AssemblyID", AssemblyID)
                Command.Parameters.AddWithValue("PriceType", PriceType)

                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(dt)

                Catch ex As Exception

                End Try

            End Using
        End Using

        Return dt

    End Function

    Public Function GetInventoryAssemblyListByItem(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal ItemID As String) As DataTable

        Dim dt As New DataTable

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetInventoryAssemblyListByItem]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("ItemID", ItemID)


                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(dt)

                Catch ex As Exception

                End Try

            End Using
        End Using

        Return dt

    End Function
    '[GetInventoryAssemblyListByItem]


    Public Function GetInventoryAssmeblyDetailList(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, _
                                                   ByVal AssemblyID As String, Optional ByVal PriceType As String = "") As DataTable

        Dim dt As New DataTable

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetInventoryAssmeblyDetailList]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("AssemblyID", AssemblyID)

                Command.Parameters.AddWithValue("PriceType", PriceType)

                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(dt)

                Catch ex As Exception

                End Try

            End Using
        End Using

        Return dt

    End Function

    Public Function InsertInventoryAssembly(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal AssemblyID As String, _
                                            ByVal ItemID As String, ByVal NumberOfItemsInAssmebly As String, ByVal CurrencyID As String, ByVal CurrencyExchangeRate As String, _
                                            ByVal TotalCost As String, ByVal TotalRetail As String, ByVal Margin As String, ByVal AssemblyTime As String, ByVal LaborRate As String, ByVal LaborCost As String, _
                                            ByVal Instruction As String, ByVal Description As String, ByVal GLAccount As String, ByVal PriceType As String, _
                                            Optional ByRef ErrorMessage As String = "") As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[InsertInventoryAssembly]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("AssemblyID", AssemblyID)

                Command.Parameters.AddWithValue("ItemID", ItemID)
                Command.Parameters.AddWithValue("NumberOfItemsInAssembly", NumberOfItemsInAssmebly)
                Command.Parameters.AddWithValue("CurrencyID", CurrencyID)
                Command.Parameters.AddWithValue("CurrencyExchangeRate", CurrencyExchangeRate)

                Command.Parameters.AddWithValue("TotalCost", TotalCost)
                Command.Parameters.AddWithValue("TotalRetail", TotalRetail)
                Command.Parameters.AddWithValue("Margin", Margin)
                Command.Parameters.AddWithValue("AssemblyTime", AssemblyTime)

                Command.Parameters.AddWithValue("LaborCost", LaborCost)
                Command.Parameters.AddWithValue("LaborRate", LaborRate)

                Command.Parameters.AddWithValue("Instruction", Instruction)
                Command.Parameters.AddWithValue("Description", Description)

                Command.Parameters.AddWithValue("GLAccount", GLAccount)
                Command.Parameters.AddWithValue("PriceType", PriceType)

                Try
                    Command.Connection.Open()
                    Command.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    ErrorMessage = ex.Message
                    Return False
                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    Public Function UpdateInventoryAssembly(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal AssemblyID As String, _
                                            ByVal ItemID As String, ByVal NumberOfItemsInAssmebly As String, ByVal CurrencyID As String, ByVal CurrencyExchangeRate As String, _
                                            ByVal TotalCost As String, ByVal TotalRetail As String, ByVal Margin As String, ByVal AssemblyTime As String, ByVal LaborRate As String, ByVal LaborCost As String, _
                                            ByVal Instruction As String, ByVal Description As String, ByVal GLAccount As String, ByVal PriceType As String, _
                                            Optional ByRef ErrorMessage As String = "") As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[UpdateInventoryAssemlbly]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("AssemblyID", AssemblyID)

                Command.Parameters.AddWithValue("ItemID", ItemID)
                Command.Parameters.AddWithValue("NumberOfItemsInAssembly", NumberOfItemsInAssmebly)
                Command.Parameters.AddWithValue("CurrencyID", CurrencyID)
                Command.Parameters.AddWithValue("CurrencyExchangeRate", CurrencyExchangeRate)

                Command.Parameters.AddWithValue("TotalCost", TotalCost)
                Command.Parameters.AddWithValue("TotalRetail", TotalRetail)
                Command.Parameters.AddWithValue("Margin", Margin)
                Command.Parameters.AddWithValue("AssemblyTime", AssemblyTime)

                Command.Parameters.AddWithValue("LaborCost", LaborCost)
                Command.Parameters.AddWithValue("LaborRate", LaborRate)

                Command.Parameters.AddWithValue("Instruction", Instruction)
                Command.Parameters.AddWithValue("Description", Description)

                Command.Parameters.AddWithValue("GLAccount", GLAccount)
                Command.Parameters.AddWithValue("PriceType", PriceType)

                Try
                    Command.Connection.Open()
                    Command.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    ErrorMessage = ex.Message
                    Return False
                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    Public Function DeleteInventoryAssembly(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal AssemblyID As String, _
                                            Optional ByVal ItemID As String = "", Optional ByVal PriceType As String = "", Optional ByRef ErrorMessage As String = "") As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[DeleteInventoryAssemlbly]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("AssemblyID", AssemblyID)

                Command.Parameters.AddWithValue("ItemID", ItemID)
                Command.Parameters.AddWithValue("PriceType", PriceType)

                Try
                    Command.Connection.Open()
                    Command.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    ErrorMessage = ex.Message
                    Return False
                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    Public Function InsertInventoryAssmeblyItems(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal AssemblyID As String, _
                                                 ByVal ItemID As String, ByVal Qty As Decimal, ByVal UnitPrice As Decimal, ByVal TotalPrice As Decimal, _
                                                 ByVal PriceType As String, Optional ByRef ErrorMessage As String = "") As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[InsertInventoryAssemblyDetail]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("AssemblyID", AssemblyID)

                Command.Parameters.AddWithValue("ItemID", ItemID)
                Command.Parameters.AddWithValue("Qty", Qty)
                Command.Parameters.AddWithValue("UnitPrice", UnitPrice)
                Command.Parameters.AddWithValue("TotalPrice", TotalPrice)

                Command.Parameters.AddWithValue("PriceType", PriceType)

                Try
                    Command.Connection.Open()
                    Command.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    ErrorMessage = ex.Message
                    Return False
                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    Public Function UpdateInventoryAssmeblyItems(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal AssemblyID As String, _
                                                 ByVal ItemID As String, ByVal Qty As Decimal, ByVal UnitPrice As Decimal, ByVal TotalPrice As Decimal, _
                                                 ByVal PriceType As String, Optional ByRef ErrorMessage As String = "") As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[UpdateInventoryAssemblyDetail]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("AssemblyID", AssemblyID)

                Command.Parameters.AddWithValue("ItemID", ItemID)
                Command.Parameters.AddWithValue("Qty", Qty)
                Command.Parameters.AddWithValue("UnitPrice", UnitPrice)
                Command.Parameters.AddWithValue("TotalPrice", TotalPrice)
                Command.Parameters.AddWithValue("PriceType", PriceType)

                Try
                    Command.Connection.Open()
                    Command.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    ErrorMessage = ex.Message
                    Return False
                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    Public Sub DeleteInventoryAssmeblyItems(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal AssemblyID As String, _
                                                 ByVal ItemID As String, ByVal PriceType As String, Optional ByRef ErrorMessage As String = "")

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[DeleteInventoryAssemblyDetail]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("AssemblyID", AssemblyID)

                Command.Parameters.AddWithValue("ItemID", ItemID)
                Command.Parameters.AddWithValue("PriceType", PriceType)

                Try
                    Command.Connection.Open()
                    Command.ExecuteNonQuery()
                Catch ex As Exception
                    ErrorMessage = ex.Message
                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Sub

    Public Function GetCountFutureDeliveryOrdersForAssemblyItem(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, _
                                                                   ByVal AssemblyID As String, Optional ByVal PriceType As String = "") As Integer

        Dim Count As Integer = 0

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetCountFutureDeliveryOrdersForAssemblyItem]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("AssemblyID", AssemblyID)
                Command.Parameters.AddWithValue("PriceType", PriceType)

                Try
                    Command.Connection.Open()
                    Count = Command.ExecuteScalar
                    Return Count

                Catch ex As Exception
                    Return -1
                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

End Class