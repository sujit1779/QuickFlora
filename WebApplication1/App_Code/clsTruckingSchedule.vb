Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class clsTruckingSchedule


    Public Function GetTruckingSchedules(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, _
                                        Optional ByVal ScheduleID As String = "") As DataTable

        Dim dt As New DataTable

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetTruckingSchedules]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("ScheduleID", ScheduleID)

                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(dt)

                Catch ex As Exception

                End Try

            End Using
        End Using

        Return dt

    End Function




    Public Function DeleteTruckingSchedule(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal ScheduleID As String) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[DeleteTruckingSchedule]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("@ScheduleID", ScheduleID)

                Try
                    Command.Connection.Open()
                    Command.ExecuteNonQuery()
                    Command.Connection.Close()

                    Return True
                Catch ex As Exception
                    Return False
                End Try

            End Using
        End Using

    End Function


    Public Function GetInventoryOrigin(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As DataTable

        Dim dt As New DataTable

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetInventoryOrigin]", Connection)
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

    Public Function GetCompanyLocations(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As DataTable

        Dim dt As New DataTable

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetCompanyLocations]", Connection)
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


    Public Function GetVendorShipMethods(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As DataTable

        Dim dt As New DataTable

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetVendorShipMethods]", Connection)
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




    Public Function InsertTruckingSchedule(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal InventoryOriginID As String, ByVal ShipMethodID As String, _
                                      ByVal ShipMethodDescription As String, ByVal ShippingToLocation As String, ByVal TruckingDay As String, ByVal ArrivalDay As String, ByVal DayCutoff As String, _
                                      ByVal Hours As String, ByVal Minutes As String, ByVal AMPM As String, ByVal TimeZone As String) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[InsertTruckingSchedule]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("@InventoryOriginID", InventoryOriginID)
                Command.Parameters.AddWithValue("@ShipMethodID", ShipMethodID)
                Command.Parameters.AddWithValue("@ShipMethodDescription", ShipMethodDescription)
                Command.Parameters.AddWithValue("@ShippingToLocation", ShippingToLocation)
                Command.Parameters.AddWithValue("@TruckingDay", TruckingDay)
                Command.Parameters.AddWithValue("@ArrivalDay", ArrivalDay)
                Command.Parameters.AddWithValue("@DayCutoff", DayCutoff)
                Command.Parameters.AddWithValue("@Hours", Hours)
                Command.Parameters.AddWithValue("@Minutes", Minutes)
                Command.Parameters.AddWithValue("@AMPM", AMPM)
                Command.Parameters.AddWithValue("@TimeZone", TimeZone)

                Try
                    Command.Connection.Open()
                    Command.ExecuteNonQuery()
                    Command.Connection.Close()

                    Return True
                Catch ex As Exception
                    Return False
                End Try

            End Using
        End Using

    End Function

    Public Function UpdateTruckingSchedule(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal ScheduleID As String, ByVal InventoryOriginID As String, ByVal ShipMethodID As String, _
                                      ByVal ShipMethodDescription As String, ByVal ShippingToLocation As String, ByVal TruckingDay As String, ByVal ArrivalDay As String, ByVal DayCutoff As String, _
                                      ByVal Hours As String, ByVal Minutes As String, ByVal AMPM As String, ByVal TimeZone As String) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[UpdateTruckingSchedule]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("@ScheduleID", ScheduleID)
                Command.Parameters.AddWithValue("@InventoryOriginID", InventoryOriginID)
                Command.Parameters.AddWithValue("@ShipMethodID", ShipMethodID)
                Command.Parameters.AddWithValue("@ShipMethodDescription", ShipMethodDescription)
                Command.Parameters.AddWithValue("@ShippingToLocation", ShippingToLocation)
                Command.Parameters.AddWithValue("@TruckingDay", TruckingDay)
                Command.Parameters.AddWithValue("@ArrivalDay", ArrivalDay)
                Command.Parameters.AddWithValue("@DayCutoff", DayCutoff)
                Command.Parameters.AddWithValue("@Hours", Hours)
                Command.Parameters.AddWithValue("@Minutes", Minutes)
                Command.Parameters.AddWithValue("@AMPM", AMPM)
                Command.Parameters.AddWithValue("@TimeZone", TimeZone)

                Try
                    Command.Connection.Open()
                    Command.ExecuteNonQuery()
                    Command.Connection.Close()

                    Return True

                Catch ex As Exception
                    Return False
                End Try

            End Using
        End Using

    End Function







End Class

