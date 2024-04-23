Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class clsAddressZone


Public Function UpdateLatLongOnOrder(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, _
                                         ByVal OrderNumber As String, ByVal Latitude As String, ByVal Longitude As String) As String

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[UpdateLatLongOnOrder]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)

                Command.Parameters.AddWithValue("@OrderNumber", OrderNumber)
                Command.Parameters.AddWithValue("@ShipppingLatitude", Latitude)
                Command.Parameters.AddWithValue("@ShipppingLongitude", Longitude)

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



	Public Sub UpdateAddressVerificationStatus(CompanyID As String, DivisionID As String, DepartmentID As String, OrderNumber As String, Message As String)

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[UpdateAddressVerificationStatus]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("@OrderNumber", OrderNumber)
                Command.Parameters.AddWithValue("@Message", Message)

                Try
                    Command.Connection.Open()
                    Command.ExecuteNonQuery()

                Catch ex As Exception

                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Sub


    Public Function GetCountry(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As DataSet

        Dim ds As New DataSet

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetCountry]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)


                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)
                Catch ex As Exception

                End Try

            End Using
        End Using

        Return ds

    End Function

    Public Function GetState(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal CountryID As String) As DataSet

        Dim ds As New DataSet

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetState]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("@CountryID", CountryID)


                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)
                Catch ex As Exception

                End Try

            End Using
        End Using

        Return ds

    End Function

    Public Function GetCity(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal StateID As String) As DataSet

        Dim ds As New DataSet

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetCity]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("@StateID", StateID)

                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)
                Catch ex As Exception

                End Try

            End Using
        End Using

        Return ds

    End Function

    Public Function GetZipCode(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal City As String) As DataSet

        Dim ds As New DataSet

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetZipCode]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("@City", City)

                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)
                Catch ex As Exception

                End Try

            End Using
        End Using

        Return ds

    End Function

    Public Function GetDetails(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal CountryID As String, ByVal ZoneID As String, ByVal LocationID As String) As DataSet

        Dim ds As New DataSet

        If CompanyID.ToLower() = "fieldofflowers" Then
            LocationID = ""
        End If


        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetAddressZoneDetails]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)
                If Not CountryID = String.Empty Then Command.Parameters.AddWithValue("@CountryID", CountryID)
                Command.Parameters.AddWithValue("@ZoneID", ZoneID)
                Command.Parameters.AddWithValue("@LocationID", LocationID)

                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)
                Catch ex As Exception

                End Try

            End Using
        End Using

        Return ds

    End Function

    Public Function GetAddressZoneList(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, Optional ByVal CountryID As String = "") As DataSet

        Dim ds As New DataSet

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetAddressZoneList]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)

                If CountryID <> String.Empty Then Command.Parameters.AddWithValue("@CountryID", CountryID)


                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)

                Catch ex As Exception

                End Try

            End Using
        End Using

        Return ds

    End Function


    Public Function InsertAddressZone(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal CountryID As String, ByVal LocationID As String, _
                                      ByVal ZoneID As String, ByVal ZoneName As String, ByVal Active As Boolean, ByVal DeliveryCharge As String, ByVal WireCharge As String, _
                                      ByVal HomeStore As String, ByVal StoreZOne As String) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[InsertAddressZone]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("@CountryID", CountryID)
                Command.Parameters.AddWithValue("@LocationID", LocationID)
                Command.Parameters.AddWithValue("@ZoneID", ZoneID)
                Command.Parameters.AddWithValue("@ZoneName", ZoneName)
                Command.Parameters.AddWithValue("@Active", Active)
                Command.Parameters.AddWithValue("@DeliveryCharge", DeliveryCharge)
                Command.Parameters.AddWithValue("@WireCharge", WireCharge)

                Command.Parameters.AddWithValue("@HomeStore", HomeStore)
                Command.Parameters.AddWithValue("@StoreZOne", StoreZOne)

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

    Public Function UpdateAddressZone(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal CountryID As String, ByVal LocationID As String, _
                                      ByVal ZoneID As String, ByVal ZoneName As String, ByVal Active As Boolean, ByVal DeliveryCharge As String, ByVal WireCharge As String, _
                                      ByVal HomeStore As String, ByVal StoreZone As String) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[UpdateAddressZone]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("@CountryID", CountryID)
                Command.Parameters.AddWithValue("@LocationID", LocationID)
                Command.Parameters.AddWithValue("@ZoneID", ZoneID)
                Command.Parameters.AddWithValue("@ZoneName", ZoneName)
                Command.Parameters.AddWithValue("@Active", Active)
                Command.Parameters.AddWithValue("@DeliveryCharge", DeliveryCharge)
                Command.Parameters.AddWithValue("@WireCharge", WireCharge)

                Command.Parameters.AddWithValue("@HomeStore", HomeStore)
                Command.Parameters.AddWithValue("@StoreZOne", StoreZone)

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

    Public Function DeleteAddressZone(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal CountryID As String, ByVal ZoneID As String, ByVal LocationID As String) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[DeleteAddressZone]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("@CountryID", CountryID)
                Command.Parameters.AddWithValue("@ZoneID", ZoneID)
                Command.Parameters.AddWithValue("@LocationID", LocationID)

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

    Public Function SearchAddressList(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal Type As String, ByVal City As String, ByVal Province As String, ByVal CountryID As String) As DataSet

        Dim ds As New DataSet

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[SearchAddressList]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)

                Command.Parameters.AddWithValue("@Type", Type)

                Command.Parameters.AddWithValue("@City", City)
                Command.Parameters.AddWithValue("@Province", Province)
                Command.Parameters.AddWithValue("@CountryID", CountryID)

                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)
                Catch ex As Exception

                End Try

            End Using
        End Using

        Return ds

    End Function

    Public Function CheckAddressZoneVerification(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[CheckAddressZoneVerification]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)

                Try
                    Command.Connection.Open()
                    Return Convert.ToBoolean(Command.ExecuteScalar)
                Catch ex As Exception
                    Return False
                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    Public Function CheckPostalCodeDropDownFeature(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[CheckPostalCodeDropDownFeature]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)

                Try
                    Command.Connection.Open()
                    Return Convert.ToBoolean(Command.ExecuteScalar)
                Catch ex As Exception
                    Return False
                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    Public Function GetAddressListByZoneID(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal ZoneID As String) As DataSet

        Dim ds As New DataSet

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetAddressListByZoneID]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)

                Command.Parameters.AddWithValue("@ZoneID", ZoneID)

                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)
                Catch ex As Exception

                End Try

            End Using
        End Using

        Return ds

    End Function

    Public Function GetAddressListByTypedSearch(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal Add1 As String, ByVal Add2 As String, ByVal Add3 As String, ByVal City As String, ByVal Province As String, ByVal PostalCode As String, Optional ByVal ZoneID As String = "") As DataSet

        Dim ds As New DataSet

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetAddressListByTypedSearch]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)

                Command.Parameters.AddWithValue("@Add1", Add1)
                Command.Parameters.AddWithValue("@Add2", Add2)
                Command.Parameters.AddWithValue("@Add3", Add3)

                Command.Parameters.AddWithValue("@City", City)
                Command.Parameters.AddWithValue("@Province", Province)
                Command.Parameters.AddWithValue("@PostalCode", PostalCode)

                Command.Parameters.AddWithValue("@ZoneID", ZoneID)

                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)
                Catch ex As Exception

                End Try

            End Using
        End Using

        Return ds

    End Function

    Public Function GetPostalCode(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal CountryID As String, ByVal PostalCode As String) As DataSet

        Dim ds As New DataSet

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetPostalCode]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)

                Command.Parameters.AddWithValue("@CountryID", CountryID)
                Command.Parameters.AddWithValue("@PostalCode", PostalCode)

                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)
                Catch ex As Exception

                End Try

            End Using
        End Using

        Return ds

    End Function

    'Public Function GetPostalCodeByTypedSearch(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal Add1 As String, ByVal Add2 As String, ByVal Add3 As String, ByVal City As String, ByVal Province As String, Optional ByVal ZoneID As String = "") As DataSet

    '    Dim ds As New DataSet

    '    Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
    '        Using Command As New SqlCommand("[enterprise].[GetPostalCodeByTypedSearch]", Connection)
    '            Command.CommandType = CommandType.StoredProcedure

    '            Command.Parameters.AddWithValue("@CompanyID", CompanyID)
    '            Command.Parameters.AddWithValue("@DivisionID", DivisionID)
    '            Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)

    '            Command.Parameters.AddWithValue("@Add1", Add1)
    '            Command.Parameters.AddWithValue("@Add2", Add2)
    '            Command.Parameters.AddWithValue("@Add3", Add3)

    '            Command.Parameters.AddWithValue("@City", City)
    '            Command.Parameters.AddWithValue("@Province", Province)

    '            Command.Parameters.AddWithValue("@ZoneID", ZoneID)

    '            Try
    '                Dim da As New SqlDataAdapter(Command)
    '                da.Fill(ds)
    '            Catch ex As Exception

    '            End Try

    '        End Using
    '    End Using

    '    Return ds

    'End Function

    Public Function UpdateAddressZoneAssociation(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal CountryID As String, ByVal Type As String, ByVal ZoneID As String, ByVal UpdateFor As String) As Boolean

        Dim ReturnCall As Boolean = False

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[UpdateAddressZoneAssociation]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("@CountryID", CountryID)
                Command.Parameters.AddWithValue("@Type", Type)

                Command.Parameters.AddWithValue("@ZoneID", ZoneID)
                Command.Parameters.AddWithValue("@Values", UpdateFor)


                Try
                    Command.Connection.Open()
                    Command.ExecuteNonQuery()

                    ReturnCall = True

                Catch ex As Exception

                    ReturnCall = False

                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

        Return ReturnCall

    End Function

    Public Function VerifyAddressByAddress1AndZipCode(ByVal Address1 As String, ByVal ZipCode As String) As DataSet

        Dim ds As New DataSet

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[VerifyAddressByAddress1AndZipCode]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@Address1", Address1)
                Command.Parameters.AddWithValue("@ZipCode", ZipCode)

                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)
                Catch ex As Exception

                End Try

            End Using
        End Using

        Return ds

    End Function

    Public Function VerifyAddressWithAllValues(ByVal Address As String, ByVal City As String, ByVal Province As String, ByVal ZipCode As String, ByVal ZoneID As String) As DataSet

        Dim ds As New DataSet

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[VerifyAddressWithAllValues]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@Address", Address.Replace("'", ""))
                Command.Parameters.AddWithValue("@City", City.Replace("'", ""))
                Command.Parameters.AddWithValue("@Province", Province.Replace("'", ""))
                Command.Parameters.AddWithValue("@ZipCode", ZipCode.Replace("'", ""))
                Command.Parameters.AddWithValue("@ZoneID", ZoneID.Replace("'", ""))

                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)
                Catch ex As Exception

                End Try

            End Using
        End Using

        Return ds

    End Function

    Public Function VerifyAddressWithAllValues2(ByVal Address As String, ByVal City As String, ByVal Province As String, ByVal ZipCode As String, ByVal ZoneID As String) As DataSet

        Dim ds As New DataSet

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[VerifyAddressWithAllValues2]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@Address", Address)
                Command.Parameters.AddWithValue("@City", City)
                Command.Parameters.AddWithValue("@Province", Province)
                Command.Parameters.AddWithValue("@ZipCode", ZipCode)
                Command.Parameters.AddWithValue("@ZoneID", ZoneID)

                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)
                Catch ex As Exception

                End Try

            End Using
        End Using

        Return ds

    End Function

    Public Function InsertAddressWithLatitudeLongitude(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, _
                                                       ByVal Add1 As String, ByVal Add2 As String, ByVal Add3 As String, ByVal City As String, ByVal State As String, _
                                                       ByVal Country As String, ByVal ZipCode As String, ByVal Latitude As String, ByVal Longitude As String, _
                                                       ByVal ZoneID As String) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[InsertAddressWithLatitudeLongitude]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)

                Command.Parameters.AddWithValue("@Add1", Add1)
                Command.Parameters.AddWithValue("@Add2", Add2)
                Command.Parameters.AddWithValue("@Add3", Add3)
                Command.Parameters.AddWithValue("@City", City)
                Command.Parameters.AddWithValue("@State", State)
                Command.Parameters.AddWithValue("@Country", Country)
                Command.Parameters.AddWithValue("@ZipCode", ZipCode)

                Command.Parameters.AddWithValue("@Latitude", Latitude)
                Command.Parameters.AddWithValue("@Longitude", Longitude)
                Command.Parameters.AddWithValue("@ZoneID", ZoneID)

                Try

                    Command.Connection.Open()
                    Command.ExecuteNonQuery()
                    Command.Connection.Close()

                Catch ex As Exception

                End Try

            End Using
        End Using

    End Function

    Public Function InsertVerifiedAddress(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal ZoneID As String, _
                                             ByVal Number As String, ByVal Street As String, ByVal City As String, ByVal StateCode As String, ByVal State As String, ByVal Zip As String, ByVal CountryCode As String, _
                                             ByVal Country As String, ByVal Latitude As String, ByVal Longitude As String) As String

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[InsertVerifiedAddres]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("@ZoneID", ZoneID)

                Command.Parameters.AddWithValue("@Number", Number)
                Command.Parameters.AddWithValue("@Street", Street)
                Command.Parameters.AddWithValue("@City", City)
                Command.Parameters.AddWithValue("@StateCode", StateCode)
                Command.Parameters.AddWithValue("@State", State)

                Command.Parameters.AddWithValue("@Zip", Zip)
                Command.Parameters.AddWithValue("@CountryCode", CountryCode)
                Command.Parameters.AddWithValue("@Country", Country)
                Command.Parameters.AddWithValue("@Latitude", Latitude)
                Command.Parameters.AddWithValue("@Longitude", Longitude)

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

    Public Function GetStateCodeByName(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal CountryID As String, ByVal StateDescription As String) As DataSet

        Dim ds As New DataSet

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetStateCodeByName]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("@CountryID", CountryID)
                Command.Parameters.AddWithValue("@StateDescription", StateDescription)

                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)
                Catch ex As Exception

                End Try

            End Using
        End Using

        Return ds

    End Function

    Public Function FillZoneList(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal LocationID As String) As DataSet

        Dim ds As New DataSet

        If CompanyID.ToLower() = "fieldofflowers" Then
            LocationID = ""
        End If

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[FillZoneList]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)

                'Command.Parameters.AddWithValue("@LocationID", LocationID)

                If CompanyID.ToLower() <> "fieldofflowers" Then
                    Command.Parameters.AddWithValue("@LocationID", LocationID)
                End If


                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)

                Catch ex As Exception

                End Try

            End Using
        End Using

        Return ds

    End Function

    Public Function GetVerifiedAddres(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal RowID As String) As DataSet

        Dim ds As New DataSet

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetVerifiedAddres]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("@RowID", RowID)

                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)
                Catch ex As Exception

                End Try

            End Using
        End Using

        Return ds

    End Function

    Public Function CheckAddressZoneSetting(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[CheckAddressZoneSetting]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)

                Try
                    Command.Connection.Open()
                    Return Convert.ToBoolean(Command.ExecuteScalar)
                Catch ex As Exception
                    Return False
                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    Public Function GetAddressVerificationURL(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As String

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetAddressVerificationURL]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)

                Try
                    Command.Connection.Open()
                    Return Command.ExecuteScalar.ToString()
                Catch ex As Exception
                    Return False
                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    Public Function CheckZoneChargeSetting(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[CheckZoneChargeSetting]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)

                Try
                    Command.Connection.Open()
                    Return Convert.ToBoolean(Command.ExecuteScalar)
                Catch ex As Exception
                    Return False
                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function



End Class

Public Class AddressVerifyClass

    Private _status As String
    Private _zone As String
    Private _lat As String
    Private _lon As String
    Private _address As String
    Private _number As String
    Private _street As String
    Private _zip As String
    Private _city As String
    Private _state As String
    Private _country As String
    Private _errorType As String

    Public Property Status() As String
        Get
            Return _status
        End Get
        Set(ByVal value As String)
            _status = value
        End Set
    End Property

    Public Property Zone() As String
        Get
            Return _zone
        End Get
        Set(ByVal value As String)
            _zone = value
        End Set
    End Property

    Public Property Lat() As String
        Get
            Return _lat
        End Get
        Set(ByVal value As String)
            _lat = value
        End Set
    End Property

    Public Property Lon() As String
        Get
            Return _lon
        End Get
        Set(ByVal value As String)
            _lon = value
        End Set
    End Property

    Public Property Address() As String
        Get
            Return _address
        End Get
        Set(ByVal value As String)
            _address = value
        End Set
    End Property

    Public Property Number() As String
        Get
            Return _number
        End Get
        Set(ByVal value As String)
            _number = value
        End Set
    End Property

    Public Property Street() As String
        Get
            Return _street
        End Get
        Set(ByVal value As String)
            _street = value
        End Set
    End Property

    Public Property Zip() As String
        Get
            Return _zip
        End Get
        Set(ByVal value As String)
            _zip = value
        End Set
    End Property

    Public Property City() As String
        Get
            Return _city
        End Get
        Set(ByVal value As String)
            _city = value
        End Set
    End Property

    Public Property State() As String
        Get
            Return _state
        End Get
        Set(ByVal value As String)
            _state = value
        End Set
    End Property

    Public Property Country() As String
        Get
            Return _country
        End Get
        Set(ByVal value As String)
            _country = value
        End Set
    End Property

    Public Property ErrorType() As String
        Get
            Return _errorType
        End Get
        Set(ByVal value As String)
            _errorType = value
        End Set
    End Property

End Class

Public Class AddressRemoveStrings

    Function AddressRemoveStrings() As ArrayList

        Dim array As New ArrayList

        array.Add("Alachua County")
        array.Add("Baker County")
        array.Add("Bay County")
        array.Add("Bradford County")
        array.Add("Brevard County")
        array.Add("Broward County")
	array.Add("Capital")
        array.Add("Calhoun County")
        array.Add("Charlotte County")
        array.Add("Citrus County")
        array.Add("Clay County")
        array.Add("Collier County")
        array.Add("Columbia County")
        array.Add("DeSoto County")
        array.Add("Dixie County")
        array.Add("Duval County")
        array.Add("Escambia County")
        array.Add("Flagler County")
        array.Add("Franklin County")
        array.Add("Gadsden County")
        array.Add("Gilchrist County")
        array.Add("Glades County")
        array.Add("Gulf County")
        array.Add("Hamilton County")
        array.Add("Hardee County")
        array.Add("Hendry County")
        array.Add("Hernando County")
        array.Add("Highlands County")
        array.Add("Hillsborough County")
        array.Add("Holmes County")
        array.Add("Indian River County")
        array.Add("Jackson County")
        array.Add("Jefferson County")
        array.Add("Lafayette County")
        array.Add("Lake County")
        array.Add("Lee County")
        array.Add("Leon County")
        array.Add("Levy County")
        array.Add("Liberty County")
        array.Add("Madison County")
        array.Add("Manatee County")
        array.Add("Marion County")
        array.Add("Martin County")
        array.Add("Miami-Dade County")
        array.Add("Monroe County")
        array.Add("Nassau County")
        array.Add("Okaloosa County")
        array.Add("Okeechobee County")
        array.Add("Orange County")
        array.Add("Osceola County")
        array.Add("Palm Beach County")
        array.Add("Pasco County")
        array.Add("Pinellas County")
        array.Add("Polk County")
        array.Add("Putnam County")
        array.Add("St.Johns County")
        array.Add("St.Lucie County")
        array.Add("Santa Rosa County")
        array.Add("Sarasota County")
        array.Add("Seminole County")
        array.Add("Sumter County")
        array.Add("Suwannee County")
        array.Add("Taylor County")
        array.Add("Union County")
        array.Add("Volusia County")
        array.Add("Wakulla County")
        array.Add("Walton County")
        array.Add("Washington County")

        Return array

    End Function

End Class
