Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System

Public Class clsItems

    Private _companyID As String
    Public Property CompanyID() As String
        Get
            Return _companyID
        End Get
        Set(ByVal value As String)
            _companyID = value
        End Set
    End Property

    Private _divisionID As String
    Public Property DivisionID() As String
        Get
            Return _divisionID
        End Get
        Set(ByVal value As String)
            _divisionID = value
        End Set
    End Property

    Private _departmentID As String
    Public Property DepartmentID() As String
        Get
            Return _departmentID
        End Get
        Set(ByVal value As String)
            _departmentID = value
        End Set
    End Property

    Public Function GetInventoryItemsList(Optional ByVal ItemID As String = "", Optional ByVal ItemName As String = "", Optional ByVal ItemType As String = "", _
                                          Optional ByVal Location As String = "", Optional ByVal VendorID As String = "") As DataSet

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetInventoryItemsList]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)

                Command.Parameters.AddWithValue("@ItemID", ItemID)
                Command.Parameters.AddWithValue("@ItemName", ItemName)
                Command.Parameters.AddWithValue("@ItemType", ItemType)

                Command.Parameters.AddWithValue("@Location", Location)
                Command.Parameters.AddWithValue("@VendorID", VendorID)


                Try
                    Dim ds As New DataSet
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)

                    If ds.Tables(0).Rows.Count >= 0 Then
                        Return ds
                    Else
                        Return Nothing
                    End If

                Catch ex As Exception
                    debug = ex.Message
                    Return Nothing

                End Try

            End Using
        End Using

    End Function

    Public Function GetEmployeeList() As DataSet

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString").ToString)
            Using Command As New SqlCommand("[PopulateEmployees]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                Try
                    Dim ds As New DataSet
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)

                    If ds.Tables(0).Rows.Count >= 0 Then
                        Return ds
                    Else
                        Return Nothing
                    End If

                Catch ex As Exception
                    debug = ex.Message
                    Return Nothing

                End Try

            End Using
        End Using

    End Function

    Public Function GetLedgerChartOfAccount() As DataSet    'CompanyID As String, DivisionID As String, DepartmentID As String) As DataSet

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetLedgerChartOfAccounts]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                Try
                    Dim ds As New DataSet
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)

                    If ds.Tables(0).Rows.Count >= 0 Then
                        Return ds
                    Else
                        Return Nothing
                    End If

                Catch ex As Exception
                    debug = ex.Message
                    Return Nothing

                End Try

            End Using
        End Using

    End Function

    Public Function GetTaxGroups() As DataSet   'CompanyID As String, DivisionID As String, DepartmentID As String) As DataSet

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetTaxGroups]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                Try
                    Dim ds As New DataSet
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)

                    If ds.Tables(0).Rows.Count >= 0 Then
                        Return ds
                    Else
                        Return Nothing
                    End If

                Catch ex As Exception
                    debug = ex.Message
                    Return Nothing

                End Try

            End Using
        End Using

    End Function

    Public Function GetImageCopyrightholders() As DataSet

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetWireServices]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                Try
                    Dim ds As New DataSet
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)

                    If ds.Tables(0).Rows.Count >= 0 Then
                        Return ds
                    Else
                        Return Nothing
                    End If

                Catch ex As Exception
                    debug = ex.Message
                    Return Nothing

                End Try

            End Using
        End Using

    End Function

    Public Function GetItemFamily() As DataSet

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[PopulateFamily]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                Try
                    Dim ds As New DataSet
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)

                    If ds.Tables(0).Rows.Count >= 0 Then
                        Return ds
                    Else
                        Return Nothing
                    End If

                Catch ex As Exception
                    debug = ex.Message
                    Return Nothing

                End Try

            End Using
        End Using

    End Function

    Public Function GetItemCategories(ByVal ItemFamilyID As String) As DataSet

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetItemCategoriesByItemFamily]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                Command.Parameters.AddWithValue("ItemFamilyID", ItemFamilyID)

                Try
                    Dim ds As New DataSet
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)

                    If ds.Tables(0).Rows.Count >= 0 Then
                        Return ds
                    Else
                        Return Nothing
                    End If

                Catch ex As Exception
                    debug = ex.Message
                    Return Nothing

                End Try

            End Using
        End Using

    End Function

    Public Function GetInventoryAssemblyList() As DataSet    'CompanyID As String, DivisionID As String, DepartmentID As String) As DataSet

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetInventoryAssemblyList]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                Try
                    Dim ds As New DataSet
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)

                    If ds.Tables(0).Rows.Count >= 0 Then
                        Return ds
                    Else
                        Return Nothing
                    End If

                Catch ex As Exception
                    debug = ex.Message
                    Return Nothing

                End Try

            End Using
        End Using

    End Function

    Public Function GetDefaultWarehousesList() As DataSet    'CompanyID As String, DivisionID As String, DepartmentID As String) As DataSet

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetDefaultWarehousesList]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                Try
                    Dim ds As New DataSet
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)

                    If ds.Tables(0).Rows.Count >= 0 Then
                        Return ds
                    Else
                        Return Nothing
                    End If

                Catch ex As Exception
                    debug = ex.Message
                    Return Nothing

                End Try

            End Using
        End Using

    End Function

    Public Function GetDefaultWarehousesBinList(ByVal WarehouseID As String) As DataSet    'CompanyID As String, DivisionID As String, DepartmentID As String) As DataSet

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetDefaultWarehousesBinList]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                Command.Parameters.AddWithValue("WarehouseID", WarehouseID)

                Try
                    Dim ds As New DataSet
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)

                    If ds.Tables(0).Rows.Count >= 0 Then
                        Return ds
                    Else
                        Return Nothing
                    End If

                Catch ex As Exception
                    debug = ex.Message
                    Return Nothing

                End Try

            End Using
        End Using

    End Function

    Public Function GetInventoryItemDetail(ByVal ItemID As String) As DataSet    'CompanyID As String, DivisionID As String, DepartmentID As String) As DataSet

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetInventoryItemDetail]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("ItemID", ItemID)

                Try
                    Dim ds As New DataSet
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)

                    If ds.Tables(0).Rows.Count >= 0 Then
                        Return ds
                    Else
                        Return Nothing
                    End If


                Catch ex As Exception
                    debug = ex.Message
                    Return Nothing

                End Try

            End Using
        End Using

    End Function

    Public Function IsInventoryItemExist(ByVal ItemID As String) As Boolean

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[CheckInventoryItemExist]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("ItemID", ItemID)

                Try
                    Dim rows As Integer = 0

                    Command.Connection.Open()
                    rows = Convert.ToInt32(Command.ExecuteScalar())

                    If rows > 0 Then
                        Return True
                    Else
                        Return False
                    End If

                Catch ex As Exception
                    debug = ex.Message
                    Return True

                End Try

            End Using
        End Using

    End Function

    Public debug As String = ""

    Public Function InsertItemDetail(ByVal ItemID As String, ByVal ItemTypeID As String, _
                    ByVal ItemName As String, ByVal ItemDescription As String, ByVal ItemLongDescription As String, ByVal ItemCareInstruction As String, ByVal ItemUPCCode As String, _
                    ByVal ItemColor As String, ByVal ItemUOM As String, ByVal CurrencyID As String, ByVal CurrencyExchangeRate As String, ByVal EnteredBy As String, _
                    ByVal PricingCode As String, ByVal PricingMethod As String, ByVal IsTaxable As Boolean, ByVal TaxGroupID As String, ByVal IsGSTTax As Boolean, _
                    ByVal IsPSTTax As Boolean, ByVal VendorID As String, ByVal IsTwoItems As Boolean, ByVal IsThreeItems As Boolean, ByVal IsBestSelling As Boolean, _
                    ByVal FlowerClassForSeries As String, ByVal FlowerClassUnitPrice As String, ByVal SortOrder As String, _
                    ByVal IsMarkIfGiftCard As Boolean, _
                    ByVal IsActiveForBackOffice As Boolean, ByVal IsWireServiceProduct As Boolean, ByVal ImageCopyRight As String, ByVal SalesDescription As String, _
                    ByVal PurchaseDescription As String, ByVal GiftCardType As String, _
                    ByVal IsAssembly As Boolean, ByVal ItemAssembly As String, ByVal SKU As String, ByVal LeadTime As String, ByVal ItemSize As String, _
                    ByVal ItemStyle As String, ByVal ItemRFID As String, _
                    ByVal Price As String, ByVal DeluxePrice As String, ByVal PremiumPrice As String, _
                    ByVal HolidayPrice As String, _
                    ByVal MTPrice As String, ByVal CostWOFreightPrice As String, ByVal LocalEverydayPrice As String, _
                    ByVal WireoutEverydayPrice As String, ByVal WireoutHolidayPrice As String, ByVal DropshipEverydayPrice As String, ByVal DropshipHolidayPrice As String, _
                    ByVal AverageCost As String, ByVal AverageValue As String, ByVal IsCommissionable As Boolean, ByVal CommissionType As String, ByVal CommissionPercent As String, _
                    ByVal SmallImageURL As String, ByVal MediumImageURL As String, ByVal LargeImageURL As String, ByVal IconSmallImageURL As String, _
                    ByVal IconMediumImageURL As String, ByVal IconLargeImageURL As String, _
                    ByVal VideoURL As String, ByVal IsMarkIfSale As Boolean, ByVal IsMarkIfNew As Boolean, ByVal IsFeatured As Boolean, _
                    ByVal IsActiveForShoppingCart As Boolean, _
                    ByVal IsEnableItemPrice As Boolean, ByVal IsEnableAddToCart As Boolean, ByVal IsMobileAppSpecial As Boolean, _
                    ByVal ItemFamilyID1 As String, ByVal ItemCategoryID1 As String, ByVal ItemFamilyID2 As String, ByVal ItemCategoryID2 As String, ByVal IsActiveItemFamilyID2 As Boolean, _
                    ByVal ItemFamilyID3 As String, ByVal ItemCategoryID3 As String, ByVal IsActiveItemFamilyID3 As Boolean, _
                    ByVal GLItemSalesAccount As String, ByVal GLItemCOGSAccount As String, ByVal GLItemInventoryAccount As String, _
                    ByVal PageTitle As String, ByVal MetaKeywords As String, ByVal MetaDescription As String, ByVal IsFreeDelivery As Boolean, _
                    ByVal DiscountCouponCode As String, ByVal MSRPPrice As String, ByVal SalePrice As String, ByVal SaleStartDate As String, ByVal SaleEndDate As String, _
                    ByVal LIFOCost As String, ByVal LIFOValue As String, ByVal FIFOCost As String, ByVal FIFOValue As String, ByVal ReOrderLevel As String, ByVal ReOrderQty As String, _
                    ByVal DefaultWarehouse As String, ByVal DefaultWarehouseBin As String, _
                    ByVal ItemCommonName As String, ByVal ItemBotanicalName As String, ByVal ColorGroup As String, ByVal FlowerType As String, _
                    ByVal Variety As String, ByVal Grade As String, ByVal BoxSize As String, _
                    ByVal ActualWeight As String, ByVal DimensionalWeight As String, ByVal Origin As String, ByVal StartDateAvailable As String, ByVal EndDateAvailable As String, _
                    ByVal IsShipMethodAllowed As Boolean, ByVal IsPaymentMethodAllowed As Boolean, ByVal ShipPreparation As String, ByVal UnitsPerBox As String, ByVal BoxPrice As String, _
                    ByVal UnitPrice As String, ByVal UnitsPerBunch As String, ByVal StandingOrderPrice As String, ByVal PreBookPrice As String, ByVal CutOffTime As String, ByVal CutPoint As String, _
                    ByVal StorageTemperature As String, ByVal MisllenaousInformation As String, ByVal VarietyInformation As String, ByVal Grower As String, ByVal Flag As String, _
                    ByVal AvailableNumberOfBoxes As String, ByVal CountryOfOrigin As String, ByVal Location As String, ByVal BoxWidth As String, ByVal BoxLength As String, ByVal BoxHeight As String, _
                    ByVal UOM As String, ByVal OriginalUnitPrice As String, ByVal ImportedFrom As String, ByVal BoxSizeUOM As String, ByVal ImportedAt As String, ByVal ItemPackSize As String, _
                    ByVal ItemUsedAs As String, ByVal VarietyId As String, ByVal NotifyPrice As String, ByVal WholesalePrice As String) As Boolean

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[InsertInventoryItemDetail]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("@ItemID", ItemID)

                Command.Parameters.AddWithValue("@ItemTypeID", ItemTypeID)
                Command.Parameters.AddWithValue("@ItemName", ItemName)
                Command.Parameters.AddWithValue("@ItemDescription", ItemDescription)

                Command.Parameters.AddWithValue("@ItemLongDescription", ItemLongDescription)
                Command.Parameters.AddWithValue("@ItemCareInstruction", ItemCareInstruction)
                Command.Parameters.AddWithValue("@ItemUPCCode", ItemUPCCode)
                Command.Parameters.AddWithValue("@ItemColor", ItemColor)

                Command.Parameters.AddWithValue("@ItemUOM", ItemUOM)
                Command.Parameters.AddWithValue("@CurrencyID", CurrencyID)
                Command.Parameters.AddWithValue("@CurrencyExchangeRate", CurrencyExchangeRate)
                Command.Parameters.AddWithValue("@EnteredBy", EnteredBy)

                Command.Parameters.AddWithValue("@ItemPricingCode", PricingCode)
                Command.Parameters.AddWithValue("@PricingMethods", PricingMethod)
                Command.Parameters.AddWithValue("@Taxable", IsTaxable)
                Command.Parameters.AddWithValue("@TaxGroupID", TaxGroupID)

                Command.Parameters.AddWithValue("@GSTTax", IsGSTTax)
                Command.Parameters.AddWithValue("@PSTTax", IsPSTTax)
                Command.Parameters.AddWithValue("@VendorID", VendorID)
                Command.Parameters.AddWithValue("@Is_TwoItems", IsTwoItems)

                Command.Parameters.AddWithValue("@Is_ThreeItems", IsThreeItems)
                Command.Parameters.AddWithValue("@IsBestSelling", IsBestSelling)
                Command.Parameters.AddWithValue("@FlowerClassIDForSeries", FlowerClassForSeries)
                Command.Parameters.AddWithValue("@FlowerClassUnitPrice", Convert.ToDouble(FlowerClassUnitPrice))

                Command.Parameters.AddWithValue("@SortOrder", SortOrder)
                Command.Parameters.AddWithValue("@IsActive", IsActiveForBackOffice)

                Command.Parameters.AddWithValue("@WireServiceProducts", IsWireServiceProduct)
                Command.Parameters.AddWithValue("@WireServiceID", ImageCopyRight)
                Command.Parameters.AddWithValue("@SalesDescription", SalesDescription)
                Command.Parameters.AddWithValue("@PurchaseDescription", PurchaseDescription)

                Command.Parameters.AddWithValue("@GiftCardType", GiftCardType)
                Command.Parameters.AddWithValue("@IsAssembly", IsAssembly)
                Command.Parameters.AddWithValue("@ItemAssembly", ItemAssembly)
                Command.Parameters.AddWithValue("@SKU", SKU)

                Command.Parameters.AddWithValue("@LeadTime", LeadTime)
                Command.Parameters.AddWithValue("@ItemSize", ItemSize)
                Command.Parameters.AddWithValue("@ItemStyle", ItemStyle)
                Command.Parameters.AddWithValue("@ItemRFID", ItemRFID)

                Command.Parameters.AddWithValue("@Price", Convert.ToDouble(Price))
                Command.Parameters.AddWithValue("@DeluxePrice", Convert.ToDouble(DeluxePrice))
                Command.Parameters.AddWithValue("@PremiumPrice", Convert.ToDouble(PremiumPrice))
                Command.Parameters.AddWithValue("@HolidayEverydayPrice", Convert.ToDouble(HolidayPrice))
                Command.Parameters.AddWithValue("@MTPrice", Convert.ToDouble(MTPrice))

                Command.Parameters.AddWithValue("@CostWOFreight", Convert.ToDouble(CostWOFreightPrice))
                Command.Parameters.AddWithValue("@LocalEverydayPrice", Convert.ToDouble(LocalEverydayPrice))
                Command.Parameters.AddWithValue("@WireoutEverydayPrice", Convert.ToDouble(WireoutEverydayPrice))

                Command.Parameters.AddWithValue("@WireoutHolidayPrice", Convert.ToDouble(WireoutHolidayPrice))
                Command.Parameters.AddWithValue("@DropshipEverydayPrice", Convert.ToDouble(DropshipEverydayPrice))
                Command.Parameters.AddWithValue("@DropshipHolidayPrice", Convert.ToDouble(DropshipHolidayPrice))
                Command.Parameters.AddWithValue("@AverageCost", Convert.ToDouble(AverageCost))

                Command.Parameters.AddWithValue("@AverageValue", Convert.ToDouble(AverageValue))
                Command.Parameters.AddWithValue("@Commissionable", IsCommissionable)
                Command.Parameters.AddWithValue("@CommissionType", CommissionType)
                Command.Parameters.AddWithValue("@CommissionPerc", CommissionPercent)


                Command.Parameters.AddWithValue("@PictureURL", SmallImageURL)
                Command.Parameters.AddWithValue("@MediumPictureURL", MediumImageURL)
                Command.Parameters.AddWithValue("@LargePictureURL", LargeImageURL)
                Command.Parameters.AddWithValue("@IcomImageSmall", IconSmallImageURL)
                Command.Parameters.AddWithValue("@IconImageMedium", IconMediumImageURL)

                Command.Parameters.AddWithValue("@IconImageLarge", IconLargeImageURL)
                Command.Parameters.AddWithValue("@VideoURL", VideoURL)
                Command.Parameters.AddWithValue("@SALE", IsMarkIfSale)
                Command.Parameters.AddWithValue("@NEW", IsMarkIfNew)

                Command.Parameters.AddWithValue("@Featured", IsFeatured)
                Command.Parameters.AddWithValue("@EnabledfrontEndItem", IsActiveForShoppingCart)
                Command.Parameters.AddWithValue("@EnableItemPrice", IsEnableItemPrice)
                Command.Parameters.AddWithValue("@EnableAddToCart", IsEnableAddToCart)

                Command.Parameters.AddWithValue("@bSpecialItem", IsMobileAppSpecial)

                Command.Parameters.AddWithValue("@ItemFamilyID", ItemFamilyID1)
                Command.Parameters.AddWithValue("@ItemCategoryID", ItemCategoryID1)
                Command.Parameters.AddWithValue("@ItemFamilyID2", ItemFamilyID2)
                Command.Parameters.AddWithValue("@ItemCategoryID2", ItemCategoryID2)

                Command.Parameters.AddWithValue("@ItemFamilyID2IsActive", IsActiveItemFamilyID2)
                Command.Parameters.AddWithValue("@ItemFamilyID3", ItemFamilyID3)
                Command.Parameters.AddWithValue("@ItemCategoryID3", ItemCategoryID3)
                Command.Parameters.AddWithValue("@ItemFamilyID3IsActive", IsActiveItemFamilyID3)

                Command.Parameters.AddWithValue("@GLItemSalesAccount", GLItemSalesAccount)
                Command.Parameters.AddWithValue("@GLItemCOGSAccount", GLItemCOGSAccount)
                Command.Parameters.AddWithValue("@GLItemInventoryAccount", GLItemInventoryAccount)

                Command.Parameters.AddWithValue("@SEOTitle", PageTitle)
                Command.Parameters.AddWithValue("@MetaKeywords", MetaKeywords)
                Command.Parameters.AddWithValue("@MetaDescription", MetaDescription)
                Command.Parameters.AddWithValue("@FreeDelivery", IsFreeDelivery)

                'Command.Parameters.AddWithValue("@DeliveryByItem", DeliveryByItem)
                Command.Parameters.AddWithValue("@DiscountCode", DiscountCouponCode)
                Command.Parameters.AddWithValue("@MSRP", Convert.ToDouble(MSRPPrice))
                Command.Parameters.AddWithValue("@SalePrice", Convert.ToDouble(SalePrice))

                Command.Parameters.AddWithValue("@SaleStartDate", SaleStartDate)
                Command.Parameters.AddWithValue("@SaleEndDate", SaleEndDate)
                Command.Parameters.AddWithValue("@LIFOCost", Convert.ToDouble(LIFOCost))
                Command.Parameters.AddWithValue("@LIFOValue", Convert.ToDouble(LIFOValue))

                Command.Parameters.AddWithValue("@FIFOCost", Convert.ToDouble(FIFOCost))
                Command.Parameters.AddWithValue("@FIFOValue", Convert.ToDouble(FIFOValue))
                Command.Parameters.AddWithValue("@ReOrderLevel", ReOrderLevel)
                Command.Parameters.AddWithValue("@ReOrderQty", ReOrderQty)

                Command.Parameters.AddWithValue("@ItemDefaultWarehouse", DefaultWarehouse)
                Command.Parameters.AddWithValue("@ItemDefaultWarehouseBin", DefaultWarehouseBin)


                Command.Parameters.AddWithValue("@ItemCommonName", ItemCommonName)
                Command.Parameters.AddWithValue("@ItemBotanicalName", ItemBotanicalName)

                Command.Parameters.AddWithValue("@ColorGroup", ColorGroup)
                Command.Parameters.AddWithValue("@FlowerType", FlowerType)
                Command.Parameters.AddWithValue("@Variety", Variety)
                Command.Parameters.AddWithValue("@Grade", Grade)

                Command.Parameters.AddWithValue("@BoxSize", BoxSize)
                Command.Parameters.AddWithValue("@ActualWeight", ActualWeight)
                Command.Parameters.AddWithValue("@DimensionalWeight", DimensionalWeight)
                Command.Parameters.AddWithValue("@Origin", Origin)

                Command.Parameters.AddWithValue("@StartDateAvailable", StartDateAvailable)
                Command.Parameters.AddWithValue("@EndDateAvailable", EndDateAvailable)
                Command.Parameters.AddWithValue("@ShipMethodAllowed", IsShipMethodAllowed)
                Command.Parameters.AddWithValue("@PaymentMethodAllowed", IsPaymentMethodAllowed)

                Command.Parameters.AddWithValue("@ShipPreparation", ShipPreparation)
                Command.Parameters.AddWithValue("@UnitsPerBox", UnitsPerBox)
                Command.Parameters.AddWithValue("@BoxPrice", Convert.ToDouble(BoxPrice))
                Command.Parameters.AddWithValue("@UnitPrice", Convert.ToDouble(UnitPrice))

                Command.Parameters.AddWithValue("@UnitsPerBunch", UnitsPerBunch)
                Command.Parameters.AddWithValue("@StandingOrderPrice", Convert.ToDouble(StandingOrderPrice))
                Command.Parameters.AddWithValue("@PreBoookPrice", Convert.ToDouble(PreBookPrice))
                Command.Parameters.AddWithValue("@CutOffTime", CutOffTime)

                Command.Parameters.AddWithValue("@CutPoint", CutPoint)
                Command.Parameters.AddWithValue("@StorageTemperature", StorageTemperature)
                Command.Parameters.AddWithValue("@MiscInformation", MisllenaousInformation)
                Command.Parameters.AddWithValue("@VarietyInformation", VarietyInformation)

                Command.Parameters.AddWithValue("@Grower", Grower)
                Command.Parameters.AddWithValue("@Flag", Flag)
                Command.Parameters.AddWithValue("@AvailableNumberOfBoxes", AvailableNumberOfBoxes)
                Command.Parameters.AddWithValue("@CountryOfOrigin", CountryOfOrigin)

                Command.Parameters.AddWithValue("@Location", Location)
                Command.Parameters.AddWithValue("@BoxWidth", BoxWidth)
                Command.Parameters.AddWithValue("@BoxLength", BoxLength)
                Command.Parameters.AddWithValue("@BoxHeight", BoxHeight)

                Command.Parameters.AddWithValue("@UOM", UOM)
                Command.Parameters.AddWithValue("@OriginalUnitPrice", Convert.ToDouble(OriginalUnitPrice))
                Command.Parameters.AddWithValue("@ImportedFrom", ImportedFrom)
                Command.Parameters.AddWithValue("@BoxSizeUOM", BoxSizeUOM)

                Command.Parameters.AddWithValue("@ImportedAt", ImportedAt)

                Command.Parameters.AddWithValue("@ItemUsedAs", IIf(IsMarkIfGiftCard, "GiftCardSale", ""))
                Command.Parameters.AddWithValue("@ItemPackSize", ItemPackSize)
                Command.Parameters.AddWithValue("@VarietyId", VarietyId)
                Command.Parameters.AddWithValue("@NotifyPrice", Convert.ToDouble(NotifyPrice))
                Command.Parameters.AddWithValue("@WholesalePrice", Convert.ToDouble(WholesalePrice))

                Try

                    Command.Connection.Open()
                    Command.ExecuteNonQuery()

                    Return True
                Catch ex As Exception
                    debug = ex.Message
                    Return False
                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    Public Function UpdateItemDetail(ByVal ItemID As String, ByVal ItemTypeID As String, _
                    ByVal ItemName As String, ByVal ItemDescription As String, ByVal ItemLongDescription As String, ByVal ItemCareInstruction As String, ByVal ItemUPCCode As String, _
                    ByVal ItemColor As String, ByVal ItemUOM As String, ByVal CurrencyID As String, ByVal CurrencyExchangeRate As String, ByVal EnteredBy As String, _
                    ByVal PricingCode As String, ByVal PricingMethod As String, ByVal IsTaxable As Boolean, ByVal TaxGroupID As String, ByVal IsGSTTax As Boolean, _
                    ByVal IsPSTTax As Boolean, ByVal VendorID As String, ByVal IsTwoItems As Boolean, ByVal IsThreeItems As Boolean, ByVal IsBestSelling As Boolean, _
                    ByVal FlowerClassForSeries As String, ByVal FlowerClassUnitPrice As String, ByVal SortOrder As String, _
                    ByVal IsMarkIfGiftCard As Boolean, _
                    ByVal IsActiveForBackOffice As Boolean, ByVal IsWireServiceProduct As Boolean, ByVal ImageCopyRight As String, ByVal SalesDescription As String, _
                    ByVal PurchaseDescription As String, ByVal GiftCardType As String, _
                    ByVal IsAssembly As Boolean, ByVal ItemAssembly As String, ByVal SKU As String, ByVal LeadTime As String, ByVal ItemSize As String, _
                    ByVal ItemStyle As String, ByVal ItemRFID As String, _
                    ByVal Price As String, ByVal DeluxePrice As String, ByVal PremiumPrice As String, _
                    ByVal HolidayPrice As String, _
                    ByVal MTPrice As String, ByVal CostWOFreightPrice As String, ByVal LocalEverydayPrice As String, _
                    ByVal WireoutEverydayPrice As String, ByVal WireoutHolidayPrice As String, ByVal DropshipEverydayPrice As String, ByVal DropshipHolidayPrice As String, _
                    ByVal AverageCost As String, ByVal AverageValue As String, ByVal IsCommissionable As Boolean, ByVal CommissionType As String, ByVal CommissionPercent As String, _
                    ByVal SmallImageURL As String, ByVal MediumImageURL As String, ByVal LargeImageURL As String, ByVal IconSmallImageURL As String, _
                    ByVal IconMediumImageURL As String, ByVal IconLargeImageURL As String, _
                    ByVal VideoURL As String, ByVal IsMarkIfSale As Boolean, ByVal IsMarkIfNew As Boolean, ByVal IsFeatured As Boolean, _
                    ByVal IsActiveForShoppingCart As Boolean, _
                    ByVal IsEnableItemPrice As Boolean, ByVal IsEnableAddToCart As Boolean, ByVal IsMobileAppSpecial As Boolean, _
                    ByVal ItemFamilyID1 As String, ByVal ItemCategoryID1 As String, ByVal ItemFamilyID2 As String, ByVal ItemCategoryID2 As String, ByVal IsActiveItemFamilyID2 As Boolean, _
                    ByVal ItemFamilyID3 As String, ByVal ItemCategoryID3 As String, ByVal IsActiveItemFamilyID3 As Boolean, _
                    ByVal GLItemSalesAccount As String, ByVal GLItemCOGSAccount As String, ByVal GLItemInventoryAccount As String, _
                    ByVal PageTitle As String, ByVal MetaKeywords As String, ByVal MetaDescription As String, ByVal IsFreeDelivery As Boolean, _
                    ByVal DiscountCouponCode As String, ByVal MSRPPrice As String, ByVal SalePrice As String, ByVal SaleStartDate As String, ByVal SaleEndDate As String, _
                    ByVal LIFOCost As String, ByVal LIFOValue As String, ByVal FIFOCost As String, ByVal FIFOValue As String, ByVal ReOrderLevel As String, ByVal ReOrderQty As String, _
                    ByVal DefaultWarehouse As String, ByVal DefaultWarehouseBin As String, _
                    ByVal ItemCommonName As String, ByVal ItemBotanicalName As String, ByVal ColorGroup As String, ByVal FlowerType As String, _
                    ByVal Variety As String, ByVal Grade As String, ByVal BoxSize As String, _
                    ByVal ActualWeight As String, ByVal DimensionalWeight As String, ByVal Origin As String, ByVal StartDateAvailable As String, ByVal EndDateAvailable As String, _
                    ByVal IsShipMethodAllowed As Boolean, ByVal IsPaymentMethodAllowed As Boolean, ByVal ShipPreparation As String, ByVal UnitsPerBox As String, ByVal BoxPrice As String, _
                    ByVal UnitPrice As String, ByVal UnitsPerBunch As String, ByVal StandingOrderPrice As String, ByVal PreBookPrice As String, ByVal CutOffTime As String, ByVal CutPoint As String, _
                    ByVal StorageTemperature As String, ByVal MisllenaousInformation As String, ByVal VarietyInformation As String, ByVal Grower As String, ByVal Flag As String, _
                    ByVal AvailableNumberOfBoxes As String, ByVal CountryOfOrigin As String, ByVal Location As String, ByVal BoxWidth As String, ByVal BoxLength As String, ByVal BoxHeight As String, _
                    ByVal UOM As String, ByVal OriginalUnitPrice As String, ByVal ImportedFrom As String, ByVal BoxSizeUOM As String, ByVal ImportedAt As String, ByVal ItemPackSize As String, _
                    ByVal ItemUsedAs As String, ByVal VarietyId As String, ByVal NotifyPrice As String, ByVal WholesalePrice As String) As Boolean

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[UpdateInventoryItemDetail]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("@ItemID", ItemID)

                Command.Parameters.AddWithValue("@ItemTypeID", ItemTypeID)
                Command.Parameters.AddWithValue("@ItemName", ItemName)
                Command.Parameters.AddWithValue("@ItemDescription", ItemDescription)

                Command.Parameters.AddWithValue("@ItemLongDescription", ItemLongDescription)
                Command.Parameters.AddWithValue("@ItemCareInstruction", ItemCareInstruction)
                Command.Parameters.AddWithValue("@ItemUPCCode", ItemUPCCode)
                Command.Parameters.AddWithValue("@ItemColor", ItemColor)

                Command.Parameters.AddWithValue("@ItemUOM", ItemUOM)
                Command.Parameters.AddWithValue("@CurrencyID", CurrencyID)
                Command.Parameters.AddWithValue("@CurrencyExchangeRate", CurrencyExchangeRate)
                Command.Parameters.AddWithValue("@EnteredBy", EnteredBy)

                Command.Parameters.AddWithValue("@ItemPricingCode", PricingCode)
                Command.Parameters.AddWithValue("@PricingMethods", PricingMethod)
                Command.Parameters.AddWithValue("@Taxable", IsTaxable)
                Command.Parameters.AddWithValue("@TaxGroupID", TaxGroupID)

                Command.Parameters.AddWithValue("@GSTTax", IsGSTTax)
                Command.Parameters.AddWithValue("@PSTTax", IsPSTTax)
                Command.Parameters.AddWithValue("@VendorID", VendorID)
                Command.Parameters.AddWithValue("@Is_TwoItems", IsTwoItems)

                Command.Parameters.AddWithValue("@Is_ThreeItems", IsThreeItems)
                Command.Parameters.AddWithValue("@IsBestSelling", IsBestSelling)
                Command.Parameters.AddWithValue("@FlowerClassIDForSeries", FlowerClassForSeries)
                Command.Parameters.AddWithValue("@FlowerClassUnitPrice", Convert.ToDouble(FlowerClassUnitPrice))

                Command.Parameters.AddWithValue("@SortOrder", SortOrder)
                Command.Parameters.AddWithValue("@IsActive", IsActiveForBackOffice)

                Command.Parameters.AddWithValue("@WireServiceProducts", IsWireServiceProduct)
                Command.Parameters.AddWithValue("@WireServiceID", ImageCopyRight)
                Command.Parameters.AddWithValue("@SalesDescription", SalesDescription)
                Command.Parameters.AddWithValue("@PurchaseDescription", PurchaseDescription)

                Command.Parameters.AddWithValue("@GiftCardType", GiftCardType)
                Command.Parameters.AddWithValue("@IsAssembly", IsAssembly)
                Command.Parameters.AddWithValue("@ItemAssembly", ItemAssembly)
                Command.Parameters.AddWithValue("@SKU", SKU)

                Command.Parameters.AddWithValue("@LeadTime", LeadTime)
                Command.Parameters.AddWithValue("@ItemSize", ItemSize)
                Command.Parameters.AddWithValue("@ItemStyle", ItemStyle)
                Command.Parameters.AddWithValue("@ItemRFID", ItemRFID)

                Command.Parameters.AddWithValue("@Price", Convert.ToDouble(Price))
                Command.Parameters.AddWithValue("@DeluxePrice", Convert.ToDouble(DeluxePrice))
                Command.Parameters.AddWithValue("@PremiumPrice", Convert.ToDouble(PremiumPrice))
                Command.Parameters.AddWithValue("@HolidayEverydayPrice", Convert.ToDouble(HolidayPrice))
                Command.Parameters.AddWithValue("@MTPrice", Convert.ToDouble(MTPrice))

                Command.Parameters.AddWithValue("@CostWOFreight", Convert.ToDouble(CostWOFreightPrice))
                Command.Parameters.AddWithValue("@LocalEverydayPrice", Convert.ToDouble(LocalEverydayPrice))
                Command.Parameters.AddWithValue("@WireoutEverydayPrice", Convert.ToDouble(WireoutEverydayPrice))

                Command.Parameters.AddWithValue("@WireoutHolidayPrice", Convert.ToDouble(WireoutHolidayPrice))
                Command.Parameters.AddWithValue("@DropshipEverydayPrice", Convert.ToDouble(DropshipEverydayPrice))
                Command.Parameters.AddWithValue("@DropshipHolidayPrice", Convert.ToDouble(DropshipHolidayPrice))
                Command.Parameters.AddWithValue("@AverageCost", Convert.ToDouble(AverageCost))

                Command.Parameters.AddWithValue("@AverageValue", Convert.ToDouble(AverageValue))
                Command.Parameters.AddWithValue("@Commissionable", IsCommissionable)
                Command.Parameters.AddWithValue("@CommissionType", CommissionType)
                Command.Parameters.AddWithValue("@CommissionPerc", CommissionPercent)


                Command.Parameters.AddWithValue("@PictureURL", SmallImageURL)
                Command.Parameters.AddWithValue("@MediumPictureURL", MediumImageURL)
                Command.Parameters.AddWithValue("@LargePictureURL", LargeImageURL)
                Command.Parameters.AddWithValue("@IcomImageSmall", IconSmallImageURL)
                Command.Parameters.AddWithValue("@IconImageMedium", IconMediumImageURL)

                Command.Parameters.AddWithValue("@IconImageLarge", IconLargeImageURL)
                Command.Parameters.AddWithValue("@VideoURL", VideoURL)
                Command.Parameters.AddWithValue("@SALE", IsMarkIfSale)
                Command.Parameters.AddWithValue("@NEW", IsMarkIfNew)

                Command.Parameters.AddWithValue("@Featured", IsFeatured)
                Command.Parameters.AddWithValue("@EnabledfrontEndItem", IsActiveForShoppingCart)
                Command.Parameters.AddWithValue("@EnableItemPrice", IsEnableItemPrice)
                Command.Parameters.AddWithValue("@EnableAddToCart", IsEnableAddToCart)

                Command.Parameters.AddWithValue("@bSpecialItem", IsMobileAppSpecial)

                Command.Parameters.AddWithValue("@ItemFamilyID", ItemFamilyID1)
                Command.Parameters.AddWithValue("@ItemCategoryID", ItemCategoryID1)
                Command.Parameters.AddWithValue("@ItemFamilyID2", ItemFamilyID2)
                Command.Parameters.AddWithValue("@ItemCategoryID2", ItemCategoryID2)

                Command.Parameters.AddWithValue("@ItemFamilyID2IsActive", IsActiveItemFamilyID2)
                Command.Parameters.AddWithValue("@ItemFamilyID3", ItemFamilyID3)
                Command.Parameters.AddWithValue("@ItemCategoryID3", ItemCategoryID3)
                Command.Parameters.AddWithValue("@ItemFamilyID3IsActive", IsActiveItemFamilyID3)

                Command.Parameters.AddWithValue("@GLItemSalesAccount", GLItemSalesAccount)
                Command.Parameters.AddWithValue("@GLItemCOGSAccount", GLItemCOGSAccount)
                Command.Parameters.AddWithValue("@GLItemInventoryAccount", GLItemInventoryAccount)

                Command.Parameters.AddWithValue("@SEOTitle", PageTitle)
                Command.Parameters.AddWithValue("@MetaKeywords", MetaKeywords)
                Command.Parameters.AddWithValue("@MetaDescription", MetaDescription)
                Command.Parameters.AddWithValue("@FreeDelivery", IsFreeDelivery)

                'Command.Parameters.AddWithValue("@DeliveryByItem", DeliveryByItem)
                Command.Parameters.AddWithValue("@DiscountCode", DiscountCouponCode)
                Command.Parameters.AddWithValue("@MSRP", Convert.ToDouble(MSRPPrice))
                Command.Parameters.AddWithValue("@SalePrice", Convert.ToDouble(SalePrice))

                Command.Parameters.AddWithValue("@SaleStartDate", SaleStartDate)
                Command.Parameters.AddWithValue("@SaleEndDate", SaleEndDate)
                Command.Parameters.AddWithValue("@LIFOCost", Convert.ToDouble(LIFOCost))
                Command.Parameters.AddWithValue("@LIFOValue", Convert.ToDouble(LIFOValue))

                Command.Parameters.AddWithValue("@FIFOCost", Convert.ToDouble(FIFOCost))
                Command.Parameters.AddWithValue("@FIFOValue", Convert.ToDouble(FIFOValue))
                Command.Parameters.AddWithValue("@ReOrderLevel", ReOrderLevel)
                Command.Parameters.AddWithValue("@ReOrderQty", ReOrderQty)

                Command.Parameters.AddWithValue("@ItemDefaultWarehouse", DefaultWarehouse)
                Command.Parameters.AddWithValue("@ItemDefaultWarehouseBin", DefaultWarehouseBin)


                Command.Parameters.AddWithValue("@ItemCommonName", ItemCommonName)
                Command.Parameters.AddWithValue("@ItemBotanicalName", ItemBotanicalName)

                Command.Parameters.AddWithValue("@ColorGroup", ColorGroup)
                Command.Parameters.AddWithValue("@FlowerType", FlowerType)
                Command.Parameters.AddWithValue("@Variety", Variety)
                Command.Parameters.AddWithValue("@Grade", Grade)

                Command.Parameters.AddWithValue("@BoxSize", BoxSize)
                Command.Parameters.AddWithValue("@ActualWeight", ActualWeight)
                Command.Parameters.AddWithValue("@DimensionalWeight", DimensionalWeight)
                Command.Parameters.AddWithValue("@Origin", Origin)

                Command.Parameters.AddWithValue("@StartDateAvailable", StartDateAvailable)
                Command.Parameters.AddWithValue("@EndDateAvailable", EndDateAvailable)
                Command.Parameters.AddWithValue("@ShipMethodAllowed", IsShipMethodAllowed)
                Command.Parameters.AddWithValue("@PaymentMethodAllowed", IsPaymentMethodAllowed)

                Command.Parameters.AddWithValue("@ShipPreparation", ShipPreparation)
                Command.Parameters.AddWithValue("@UnitsPerBox", UnitsPerBox)
                Command.Parameters.AddWithValue("@BoxPrice", Convert.ToDouble(BoxPrice))
                Command.Parameters.AddWithValue("@UnitPrice", Convert.ToDouble(UnitPrice))

                Command.Parameters.AddWithValue("@UnitsPerBunch", UnitsPerBunch)
                Command.Parameters.AddWithValue("@StandingOrderPrice", Convert.ToDouble(StandingOrderPrice))
                Command.Parameters.AddWithValue("@PreBoookPrice", Convert.ToDouble(PreBookPrice))
                Command.Parameters.AddWithValue("@CutOffTime", CutOffTime)

                Command.Parameters.AddWithValue("@CutPoint", CutPoint)
                Command.Parameters.AddWithValue("@StorageTemperature", StorageTemperature)
                Command.Parameters.AddWithValue("@MiscInformation", MisllenaousInformation)
                Command.Parameters.AddWithValue("@VarietyInformation", VarietyInformation)

                Command.Parameters.AddWithValue("@Grower", Grower)
                Command.Parameters.AddWithValue("@Flag", Flag)
                Command.Parameters.AddWithValue("@AvailableNumberOfBoxes", AvailableNumberOfBoxes)
                Command.Parameters.AddWithValue("@CountryOfOrigin", CountryOfOrigin)

                Command.Parameters.AddWithValue("@Location", Location)
                Command.Parameters.AddWithValue("@BoxWidth", BoxWidth)
                Command.Parameters.AddWithValue("@BoxLength", BoxLength)
                Command.Parameters.AddWithValue("@BoxHeight", BoxHeight)

                Command.Parameters.AddWithValue("@UOM", UOM)
                Command.Parameters.AddWithValue("@OriginalUnitPrice", Convert.ToDouble(OriginalUnitPrice))
                Command.Parameters.AddWithValue("@ImportedFrom", ImportedFrom)
                Command.Parameters.AddWithValue("@BoxSizeUOM", BoxSizeUOM)

                Command.Parameters.AddWithValue("@ImportedAt", ImportedAt)

                Command.Parameters.AddWithValue("@ItemUsedAs", IIf(IsMarkIfGiftCard, "GiftCardSale", ""))
                Command.Parameters.AddWithValue("@ItemPackSize", ItemPackSize)
                Command.Parameters.AddWithValue("@VarietyId", VarietyId)
                Command.Parameters.AddWithValue("@NotifyPrice", Convert.ToDouble(NotifyPrice))
                Command.Parameters.AddWithValue("@WholesalePrice", Convert.ToDouble(WholesalePrice))

                Try

                    Command.Connection.Open()
                    Command.ExecuteNonQuery()

                    Return True
                Catch ex As Exception
                    debug = ex.Message
                    Return False
                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    Public Function GetItemWiseDelivery(ByVal ItemID As String) As String    'CompanyID As String, DivisionID As String, DepartmentID As String) As DataSet

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetItemWiseDelivery]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("ItemID", ItemID)

                Try

                    Command.Connection.Open()
                    Return Convert.ToString(Command.ExecuteScalar)

                Catch ex As Exception
                    debug = ex.Message
                    Return ""
                Finally
                    Command.Connection.Close()

                End Try

            End Using
        End Using

    End Function

    Public Function InsertUpdateItemWiseDelivery(ByVal ItemID As String, ByVal DeliveryCharge As String) As Boolean    'CompanyID As String, DivisionID As String, DepartmentID As String) As DataSet

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[InsertUpdateItemWiseDelivery]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("ItemID", ItemID)
                Command.Parameters.AddWithValue("DeliveryCharge", DeliveryCharge)

                Try
                    Command.Connection.Open()
                    Command.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    debug = ex.Message
                    Return False
                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function


End Class

Public Class Item

    Private newPropertyValue As String
    Public Property NewProperty() As String
        Get
            Return newPropertyValue
        End Get
        Set(ByVal value As String)
            newPropertyValue = value
        End Set
    End Property


End Class


#Region "Params"

#End Region