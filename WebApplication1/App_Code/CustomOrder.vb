Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Data

Namespace DAL
    Public Class CustomOrder
#Region "Private Variables"

        Dim _ItemDiscountPerc As Decimal


        Dim _CompanyID As String
        Dim _DivisionID As String
        Dim _DepartmentID As String
        Dim _OrderNumber As String

        Dim _OrderLineNumber As String
        Dim _ItemID As String
        Dim _WarehouseID As String
        Dim _WarehouseBinID As String
        Dim _SerialNumber As String
        Dim _Description As String
        Dim _OrderQty As Double
        Dim _BackOrdered As Byte
        Dim _BackOrderQyyty As Double
        Dim _ItemUOM As String
        Dim _ItemWeight As Double
        Dim _DiscountPerc As Double
        Dim _ItemDiscountFlatOrPercent As String 'JMT added on 11th August 2008
        Dim _Taxable As Byte
        Dim _ItemCost As Decimal
        Dim _ItemUnitPrice As Decimal
        Dim _TaxGroupID As String
        Dim _TaxAmount As Decimal
        Dim _TaxPercent As Double
        Dim _SubTotal As Decimal
        Dim _Total As Decimal
        Dim _TotalWeight As Double
        Dim _GLSalesAccount As String
        Dim _ProjectID As String
        Dim _TrackingNumber As String

        Dim _TransactionTypeID As String
        Dim _OrderTypeID As String
        Dim _OrderDate As Date
        Dim _OrderShipDate As String
        Dim _CustomerID As String
        Dim _EmployeeID As String
        Dim _ShipMethodID As String
        Dim _ShippingName As String
        Dim _ShippingAddress1 As String
        Dim _ShippingAddress2 As String
        Dim _ShippingAddress3 As String
        Dim _ShippingCity As String
        Dim _ShippingState As String
        Dim _ShippingZip As String
        Dim _ShippingCountry As String
        Dim _CreditCardName As String
        Dim _AddEdit As Integer
        Dim _Quantity As Decimal
        Dim _PaymentMethodID As String
        Dim _PaymentMethodDescription As String
        Dim _PaymentMethodActive As Boolean

        Dim _LocationID As String

        Dim _CreditCardType As String
        Dim _CreditCardNumber As String
        Dim _CreditCardExpDate As String
        Dim _CreditCardCSVNumber As String
        Dim _CreditCardValidationCode As String
        Dim _CreditCardApprovalNumber As String
        Dim _CreditCardBillToZip As String
        Dim _CreditCardDescription As String
        Dim _CrediCardAccept As Boolean
        Dim _CreditCardActive As Boolean
        Dim _CreditCardVoicePhone As String
        Dim _CreditCardMerchantID As String
        Dim _CreditCardFraudNo As String

        Dim _ShippingPhone As String
        Dim _ShippingFax As String
        Dim _ShippingCell As String
        Dim _ShippingExt As String
        Dim _ShippingAttention As String
        Dim _DestinationType As String
        Dim _Priority As String
        Dim _OccasionCode As String
        Dim _ShippingFirstName As String
        Dim _ShippingLastName As String
        Dim _ShippingCompany As String
        Dim _IpAddress As String
        Dim _FraudRating As String
        Dim _ShippingSalutation As String
        Dim _CustomerFirstName As String
        Dim _CustomerLastName As String
        Dim _Attention As String
        Dim _CustomerAddress1 As String
        Dim _CustomerAddress2 As String
        Dim _CustomerAddress3 As String
        Dim _CustomerCity As String

        Dim _CustomerState As String
        Dim _CustomerCountry As String

        Dim _CustomerFax As String
        Dim _CustomerPhone As String
        Dim _CustomerEmail As String
        Dim _CustomerPhoneExt As String
        Dim _CustomerZip As String
        Dim _CustomerCell As String

        Dim _CreditLimit As String
        Dim _AccountStatus As String
        Dim _CustomerSince As String
        Dim _Discounts As String
        Dim _CreditComments As String
        Dim _CustomerCompany As String



        Dim _Ytdorders As String
        Dim _MemberPoints As String
        Dim _CustomerRank As String
        Dim _SalesLifeTime As String

        Dim _CustomerComments As String

        Dim _CustomerSalutation As String
        Dim _PO As String


        Dim _CheckNumber As String
        Dim _CheckID As String
        Dim _Coupon As String
        Dim _GiftCertificate As String

        Dim _WireService As String
        Dim _WireCode As String
        Dim _WireRefernceID As String
        Dim _WireTransmitMethod As String

        Dim _InternalNotes As String
        Dim _DriverRouteInfo As String

        Dim _AddEdit2 As String

        Dim _Service As Decimal
        Dim _Delivery As Decimal
        Dim _Relay As Decimal

        Dim _OrderSourceCode As String
        Dim _Newsletter As String
        Dim _OLineNumber As Integer

#End Region

#Region "Properties"

        Public Property ItemDiscountPerc() As Decimal
            Get
                Return _ItemDiscountPerc
            End Get
            Set(ByVal value As Decimal)
                _ItemDiscountPerc = value
            End Set
        End Property

        'JMT Code on 11th August 2008 Starts here
        Public Property ItemDiscountFlatOrPercent() As String
            Get
                Return _ItemDiscountFlatOrPercent
            End Get
            Set(ByVal value As String)
                _ItemDiscountFlatOrPercent = value
            End Set
        End Property
        'JMT Code on 11th August 2008 Ends here

        Public Property Newsletter() As String
            Get
                Return _Newsletter
            End Get
            Set(ByVal value As String)
                _Newsletter = value
            End Set
        End Property
        Public Property OrderSourceCode() As String
            Get
                Return _OrderSourceCode
            End Get
            Set(ByVal value As String)
                _OrderSourceCode = value
            End Set
        End Property
        Public Property AddEdit() As Int32
            Get
                Return _AddEdit
            End Get
            Set(ByVal value As Int32)
                _AddEdit = value
            End Set
        End Property
        Public Property AddEdit2() As String
            Get
                Return _AddEdit2
            End Get
            Set(ByVal value As String)
                _AddEdit2 = value
            End Set
        End Property

        Public Property CompanyID() As String
            Get
                Return _CompanyID
            End Get
            Set(ByVal value As String)
                _CompanyID = value
            End Set
        End Property
        Public Property DivisionID() As String
            Get
                Return _DivisionID
            End Get
            Set(ByVal value As String)
                _DivisionID = value
            End Set
        End Property
        Public Property DepartmentID() As String
            Get
                Return _DepartmentID
            End Get
            Set(ByVal value As String)
                _DepartmentID = value
            End Set
        End Property
        Public Property OrderLineNumber() As String
            Get
                Return _OrderLineNumber
            End Get
            Set(ByVal value As String)
                _OrderLineNumber = value
            End Set
        End Property

        Public Property OLineNumber() As Integer
            Get
                Return _OLineNumber
            End Get
            Set(ByVal value As Integer)
                _OLineNumber = value
            End Set
        End Property
        Public Property OrderNumber() As String
            Get
                Return _OrderNumber
            End Get
            Set(ByVal value As String)
                _OrderNumber = value
            End Set
        End Property


        Public Property ItemID() As String
            Get
                Return _ItemID
            End Get
            Set(ByVal value As String)
                _ItemID = value
            End Set
        End Property
        Public Property WarehouseID() As String
            Get
                Return _WarehouseID
            End Get
            Set(ByVal value As String)
                _WarehouseID = value
            End Set
        End Property
        Public Property WarehouseBinID() As String
            Get
                Return _WarehouseBinID
            End Get
            Set(ByVal value As String)
                _WarehouseBinID = value
            End Set
        End Property


        Public Property SerialNumber() As String
            Get
                Return _SerialNumber
            End Get
            Set(ByVal value As String)
                _SerialNumber = value
            End Set
        End Property
        Public Property Description() As String
            Get
                Return _Description
            End Get
            Set(ByVal value As String)
                _Description = value
            End Set
        End Property


        Public Property ItemUOM() As String
            Get
                Return _ItemUOM
            End Get
            Set(ByVal value As String)
                _ItemUOM = value
            End Set
        End Property

        Public Property OrderQty() As Double
            Get
                Return _OrderQty
            End Get
            Set(ByVal value As Double)
                _OrderQty = value
            End Set
        End Property
        Public Property BackOrdered() As Byte
            Get
                Return _BackOrdered
            End Get
            Set(ByVal value As Byte)
                _BackOrdered = value
            End Set
        End Property
        Public Property BackOrderQyyty() As Double
            Get
                Return _BackOrderQyyty
            End Get
            Set(ByVal value As Double)
                _BackOrderQyyty = value
            End Set
        End Property
        Public Property ItemWeight() As Double
            Get
                Return _ItemWeight
            End Get
            Set(ByVal value As Double)
                _ItemWeight = value
            End Set
        End Property
        Public Property DiscountPerc() As Double
            Get
                Return _DiscountPerc
            End Get
            Set(ByVal value As Double)
                _DiscountPerc = value
            End Set
        End Property
        Public Property Taxable() As Byte
            Get
                Return _Taxable
            End Get
            Set(ByVal value As Byte)
                _Taxable = value
            End Set
        End Property
        Public Property ItemCost() As Decimal
            Get
                Return _ItemCost
            End Get
            Set(ByVal value As Decimal)
                _ItemCost = value
            End Set
        End Property
        Public Property ItemUnitPrice() As Decimal
            Get
                Return _ItemUnitPrice
            End Get
            Set(ByVal value As Decimal)
                _ItemUnitPrice = value
            End Set
        End Property
        Public Property TaxGroupID() As String
            Get
                Return _TaxGroupID
            End Get
            Set(ByVal value As String)
                _TaxGroupID = value
            End Set
        End Property

        Public Property TaxAmount() As Decimal
            Get
                Return _TaxAmount
            End Get
            Set(ByVal value As Decimal)
                _TaxAmount = value
            End Set
        End Property
        Public Property TaxPercent() As Double
            Get
                Return _TaxPercent
            End Get
            Set(ByVal value As Double)
                _TaxPercent = value
            End Set
        End Property
        Public Property SubTotal() As Decimal
            Get
                Return _SubTotal
            End Get
            Set(ByVal value As Decimal)
                _SubTotal = value
            End Set
        End Property
        Public Property Total() As Decimal
            Get
                Return _Total
            End Get
            Set(ByVal value As Decimal)
                _Total = value
            End Set
        End Property
        Public Property TotalWeight() As Double
            Get
                Return _TotalWeight
            End Get
            Set(ByVal value As Double)
                _TotalWeight = value
            End Set
        End Property

        Public Property GLSalesAccount() As String
            Get
                Return _GLSalesAccount
            End Get
            Set(ByVal value As String)
                _GLSalesAccount = value
            End Set
        End Property
        Public Property ProjectID() As String
            Get
                Return _ProjectID
            End Get
            Set(ByVal value As String)
                _ProjectID = value
            End Set
        End Property
        Public Property TrackingNumber() As String
            Get
                Return _TrackingNumber
            End Get
            Set(ByVal value As String)
                _TrackingNumber = value
            End Set
        End Property


        Public Property TransactionTypeID() As String
            Get
                Return _TransactionTypeID
            End Get
            Set(ByVal value As String)
                _TransactionTypeID = value
            End Set
        End Property

        Public Property OrderTypeID() As String
            Get
                Return _OrderTypeID
            End Get
            Set(ByVal value As String)
                _OrderTypeID = value
            End Set
        End Property

        Public Property CustomerID() As String
            Get
                Return _CustomerID
            End Get
            Set(ByVal value As String)
                _CustomerID = value
            End Set
        End Property

        Public Property EmployeeID() As String
            Get
                Return _EmployeeID
            End Get
            Set(ByVal value As String)
                _EmployeeID = value
            End Set
        End Property




        Public Property OrderDate() As Date
            Get
                Return _OrderDate
            End Get
            Set(ByVal value As Date)
                _OrderDate = value
            End Set
        End Property

        Public Property OrderShipDate() As String
            Get
                Return _OrderShipDate
            End Get
            Set(ByVal value As String)
                _OrderShipDate = value
            End Set
        End Property

        Public Property ShipMethodID() As String
            Get
                Return _ShipMethodID
            End Get
            Set(ByVal value As String)
                _ShipMethodID = value
            End Set
        End Property

        Public Property ShippingName() As String
            Get
                Return _ShippingName
            End Get
            Set(ByVal value As String)
                _ShippingName = value
            End Set
        End Property
        Public Property ShippingAddress1() As String
            Get
                Return _ShippingAddress1
            End Get
            Set(ByVal value As String)
                _ShippingAddress1 = value
            End Set
        End Property

        Public Property ShippingAddress2() As String
            Get
                Return _ShippingAddress2
            End Get
            Set(ByVal value As String)
                _ShippingAddress2 = value
            End Set
        End Property
        Public Property ShippingAddress3() As String
            Get
                Return _ShippingAddress3
            End Get
            Set(ByVal value As String)
                _ShippingAddress3 = value
            End Set
        End Property

        Public Property ShippingCity() As String
            Get
                Return _ShippingCity
            End Get
            Set(ByVal value As String)
                _ShippingCity = value
            End Set
        End Property
        Public Property ShippingState() As String
            Get
                Return _ShippingState
            End Get
            Set(ByVal value As String)
                _ShippingState = value
            End Set
        End Property
        Public Property ShippingZip() As String
            Get
                Return _ShippingZip
            End Get
            Set(ByVal value As String)
                _ShippingZip = value
            End Set
        End Property
        Public Property ShippingCountry() As String
            Get
                Return _ShippingCountry
            End Get
            Set(ByVal value As String)
                _ShippingCountry = value
            End Set
        End Property
        Public Property CreditCardName() As String
            Get
                Return _CreditCardName
            End Get
            Set(ByVal value As String)
                _CreditCardName = value
            End Set
        End Property

        Public Property Quantity() As Decimal
            Get
                Return _Quantity
            End Get
            Set(ByVal value As Decimal)
                _Quantity = value
            End Set
        End Property

        Public Property PaymentMethodID() As String
            Get
                Return _PaymentMethodID
            End Get
            Set(ByVal value As String)
                _PaymentMethodID = value
            End Set
        End Property

        Public Property PaymentMethodDescription() As String
            Get
                Return _PaymentMethodDescription
            End Get
            Set(ByVal value As String)
                _PaymentMethodDescription = value
            End Set
        End Property


        Public Property PaymentMethodActive() As Boolean
            Get
                Return _PaymentMethodActive
            End Get
            Set(ByVal value As Boolean)
                _PaymentMethodActive = value
            End Set
        End Property

        Public Property CreditCardType() As String
            Get
                Return _CreditCardType
            End Get
            Set(ByVal value As String)
                _CreditCardType = value
            End Set
        End Property

        Public Property CreditCardNumber() As String
            Get
                Return _CreditCardNumber
            End Get
            Set(ByVal value As String)
                _CreditCardNumber = value
            End Set
        End Property

        Public Property CreditCardExpDate() As String
            Get
                Return _CreditCardExpDate
            End Get
            Set(ByVal value As String)
                _CreditCardExpDate = value
            End Set
        End Property

        Public Property CreditCardCSVNumber() As String
            Get
                Return _CreditCardCSVNumber
            End Get
            Set(ByVal value As String)
                _CreditCardCSVNumber = value
            End Set
        End Property

        Public Property CreditCardValidationCode() As String
            Get
                Return _CreditCardValidationCode
            End Get
            Set(ByVal value As String)
                _CreditCardValidationCode = value
            End Set
        End Property

        Public Property CreditCardBillToZip() As String
            Get
                Return _CreditCardBillToZip
            End Get
            Set(ByVal value As String)
                _CreditCardBillToZip = value
            End Set
        End Property

         
        Public Property CreditCardDescription() As String
            Get
                Return _CreditCardDescription
            End Get
            Set(ByVal value As String)
                _CreditCardDescription = value
            End Set
        End Property


        Public Property CrediCardAccept() As Boolean
            Get
                Return _CrediCardAccept
            End Get
            Set(ByVal value As Boolean)
                _CrediCardAccept = value
            End Set
        End Property
        Public Property CreditCardActive() As Boolean
            Get
                Return _CreditCardActive
            End Get
            Set(ByVal value As Boolean)
                _CreditCardActive = value
            End Set
        End Property
        Public Property CreditCardVoicePhone() As String
            Get
                Return _CreditCardVoicePhone
            End Get
            Set(ByVal value As String)
                _CreditCardVoicePhone = value
            End Set
        End Property
        Public Property CreditCardMerchantID() As String
            Get
                Return _CreditCardMerchantID
            End Get
            Set(ByVal value As String)
                _CreditCardMerchantID = value
            End Set
        End Property
        Public Property CreditCardFraudNo() As String
            Get
                Return _CreditCardFraudNo
            End Get
            Set(ByVal value As String)
                _CreditCardFraudNo = value
            End Set
        End Property
       
        Public Property CreditCardApprovalNumber() As String
            Get
                Return _CreditCardApprovalNumber
            End Get
            Set(ByVal value As String)
                _CreditCardApprovalNumber = value
            End Set
        End Property



        Public Property ShippingPhone() As String
            Get
                Return _ShippingPhone
            End Get
            Set(ByVal value As String)
                _ShippingPhone = value
            End Set
        End Property
        Public Property ShippingFax() As String
            Get
                Return _ShippingFax
            End Get
            Set(ByVal value As String)
                _ShippingFax = value
            End Set
        End Property
        Public Property ShippingCell() As String
            Get
                Return _ShippingCell
            End Get
            Set(ByVal value As String)
                _ShippingCell = value
            End Set
        End Property
        Public Property ShippingExt() As String
            Get
                Return _ShippingExt
            End Get
            Set(ByVal value As String)
                _ShippingExt = value
            End Set
        End Property
        Public Property ShippingAttention() As String
            Get
                Return _ShippingAttention
            End Get
            Set(ByVal value As String)
                _ShippingAttention = value
            End Set
        End Property
        Public Property DestinationType() As String
            Get
                Return _DestinationType
            End Get
            Set(ByVal value As String)
                _DestinationType = value
            End Set
        End Property
        Public Property Priority() As String
            Get
                Return _Priority
            End Get
            Set(ByVal value As String)
                _Priority = value
            End Set
        End Property
        Public Property OccasionCode() As String
            Get
                Return _OccasionCode
            End Get
            Set(ByVal value As String)
                _OccasionCode = value
            End Set
        End Property

        Public Property ShippingFirstName() As String
            Get
                Return _ShippingFirstName
            End Get
            Set(ByVal value As String)
                _ShippingFirstName = value
            End Set
        End Property
        Public Property ShippingLastName() As String
            Get
                Return _ShippingLastName
            End Get
            Set(ByVal value As String)
                _ShippingLastName = value
            End Set
        End Property
        Public Property ShippingCompany() As String
            Get
                Return _ShippingCompany
            End Get
            Set(ByVal value As String)
                _ShippingCompany = value
            End Set
        End Property
        Public Property ShippingSalutation() As String
            Get
                Return _ShippingSalutation
            End Get
            Set(ByVal value As String)
                _ShippingSalutation = value
            End Set
        End Property
        Public Property IpAddress() As String
            Get
                Return _IpAddress
            End Get
            Set(ByVal value As String)
                _IpAddress = value
            End Set
        End Property
        Public Property FraudRating() As String
            Get
                Return _FraudRating
            End Get
            Set(ByVal value As String)
                _FraudRating = value
            End Set
        End Property

        Public Property CustomerFirstName() As String
            Get
                Return _CustomerFirstName
            End Get
            Set(ByVal value As String)
                _CustomerFirstName = value
            End Set
        End Property

        Public Property CustomerLastName() As String
            Get
                Return _CustomerLastName
            End Get
            Set(ByVal value As String)
                _CustomerLastName = value
            End Set
        End Property

        Public Property Attention() As String
            Get
                Return _Attention
            End Get
            Set(ByVal value As String)
                _Attention = value
            End Set
        End Property

        Public Property CustomerAddress1() As String
            Get
                Return _CustomerAddress1
            End Get
            Set(ByVal value As String)
                _CustomerAddress1 = value
            End Set
        End Property

        Public Property CustomerAddress2() As String
            Get
                Return _CustomerAddress2
            End Get
            Set(ByVal value As String)
                _CustomerAddress2 = value
            End Set
        End Property

        Public Property CustomerAddress3() As String
            Get
                Return _CustomerAddress3
            End Get
            Set(ByVal value As String)
                _CustomerAddress3 = value
            End Set
        End Property

        Public Property CustomerCity() As String
            Get
                Return _CustomerCity
            End Get
            Set(ByVal value As String)
                _CustomerCity = value
            End Set
        End Property

        Public Property CustomerState() As String
            Get
                Return _CustomerState
            End Get
            Set(ByVal value As String)
                _CustomerState = value
            End Set
        End Property

        Public Property CustomerCountry() As String
            Get
                Return _CustomerCountry
            End Get
            Set(ByVal value As String)
                _CustomerCountry = value
            End Set
        End Property

        Public Property CustomerFax() As String
            Get
                Return _CustomerFax
            End Get
            Set(ByVal value As String)
                _CustomerFax = value
            End Set
        End Property

        Public Property CustomerPhone() As String
            Get
                Return _CustomerPhone
            End Get
            Set(ByVal value As String)
                _CustomerPhone = value
            End Set
        End Property

        Public Property CustomerEmail() As String
            Get
                Return _CustomerEmail
            End Get
            Set(ByVal value As String)
                _CustomerEmail = value
            End Set
        End Property

        Public Property CreditLimit() As String
            Get
                Return _CreditLimit
            End Get
            Set(ByVal value As String)
                _CreditLimit = value
            End Set
        End Property

        Public Property AccountStatus() As String
            Get
                Return _AccountStatus
            End Get
            Set(ByVal value As String)
                _AccountStatus = value
            End Set
        End Property

        Public Property CustomerSince() As String
            Get
                Return _CustomerSince
            End Get
            Set(ByVal value As String)
                _CustomerSince = value
            End Set
        End Property

        Public Property Discounts() As String
            Get
                Return _Discounts
            End Get
            Set(ByVal value As String)
                _Discounts = value
            End Set
        End Property

        Public Property CustomerRank() As String
            Get
                Return _CustomerRank
            End Get
            Set(ByVal value As String)
                _CustomerRank = value
            End Set
        End Property

        Public Property SalesLifeTime() As String
            Get
                Return _SalesLifeTime
            End Get
            Set(ByVal value As String)
                _SalesLifeTime = value
            End Set
        End Property

        Public Property MemberPoints() As String
            Get
                Return _MemberPoints
            End Get
            Set(ByVal value As String)
                _MemberPoints = value
            End Set
        End Property

        Public Property Ytdorders() As String
            Get
                Return _Ytdorders
            End Get
            Set(ByVal value As String)
                _Ytdorders = value
            End Set
        End Property


        Public Property CreditComments() As String
            Get
                Return _CreditComments
            End Get
            Set(ByVal value As String)
                _CreditComments = value
            End Set
        End Property

        Public Property CustomerSalutation() As String
            Get
                Return _CustomerSalutation
            End Get
            Set(ByVal value As String)
                _CustomerSalutation = value
            End Set
        End Property

        Public Property CustomerComments() As String
            Get
                Return _CustomerComments
            End Get
            Set(ByVal value As String)
                _CustomerComments = value
            End Set
        End Property



        Public Property CheckNumber() As String
            Get
                Return _CheckNumber
            End Get
            Set(ByVal value As String)
                _CheckNumber = value
            End Set
        End Property


        Public Property CheckID() As String
            Get
                Return _CheckID
            End Get
            Set(ByVal value As String)
                _CheckID = value
            End Set
        End Property

        Public Property Coupon() As String
            Get
                Return _Coupon
            End Get
            Set(ByVal value As String)
                _Coupon = value
            End Set
        End Property

        Public Property GiftCertificate() As String
            Get
                Return _GiftCertificate
            End Get
            Set(ByVal value As String)
                _GiftCertificate = value
            End Set
        End Property

        Public Property WireService() As String
            Get
                Return _WireService
            End Get
            Set(ByVal value As String)
                _WireService = value
            End Set
        End Property

        Public Property WireCode() As String
            Get
                Return _WireCode
            End Get
            Set(ByVal value As String)
                _WireCode = value
            End Set
        End Property

        Public Property WireRefernceID() As String
            Get
                Return _WireRefernceID
            End Get
            Set(ByVal value As String)
                _WireRefernceID = value
            End Set
        End Property
        Public Property WireTransmitMethod() As String
            Get
                Return _WireTransmitMethod
            End Get
            Set(ByVal value As String)
                _WireTransmitMethod = value
            End Set
        End Property
        Public Property PO() As String
            Get
                Return _PO
            End Get
            Set(ByVal value As String)
                _PO = value
            End Set
        End Property
        Public Property CustomerCompany() As String
            Get
                Return _CustomerCompany
            End Get
            Set(ByVal value As String)
                _CustomerCompany = value
            End Set
        End Property

        Public Property CustomerPhoneExt() As String
            Get
                Return _CustomerPhoneExt
            End Get
            Set(ByVal value As String)
                _CustomerPhoneExt = value
            End Set
        End Property


        Public Property CustomerCell() As String
            Get
                Return _CustomerCell
            End Get
            Set(ByVal value As String)
                _CustomerCell = value
            End Set
        End Property

        Public Property CustomerZip() As String
            Get
                Return _CustomerZip
            End Get
            Set(ByVal value As String)
                _CustomerZip = value
            End Set
        End Property



        Public Property LocationID() As String
            Get
                Return _LocationID
            End Get
            Set(ByVal value As String)
                _LocationID = value
            End Set
        End Property

        Public Property InternalNotes() As String
            Get
                Return _InternalNotes
            End Get
            Set(ByVal value As String)
                _InternalNotes = value
            End Set
        End Property


        Public Property DriverRouteInfo() As String
            Get
                Return _DriverRouteInfo
            End Get
            Set(ByVal value As String)
                _DriverRouteInfo = value
            End Set
        End Property
        Public Property Delivery() As Decimal
            Get
                Return _Delivery
            End Get
            Set(ByVal value As Decimal)
                _Delivery = value
            End Set
        End Property

        Public Property Service() As Decimal
            Get
                Return _Service
            End Get
            Set(ByVal value As Decimal)
                _Service = value
            End Set
        End Property

        Public Property Relay() As Decimal
            Get
                Return _Relay
            End Get
            Set(ByVal value As Decimal)
                _Relay = value
            End Set
        End Property
#End Region

#Region "PopulateOrder"
        Public Function PopulateOrder(ByVal CompanyID As String, ByVal DeptID As String, ByVal DivID As String, ByVal OrderNo As String) As SqlDataReader
            Dim ConnectionString As String = ""

            ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            ' Dim SQLStr As String = "SELECT * FROM OrderHeader WHERE CompanyID='" & CompanyID & "' AND DepartmentID='" & DeptID & "' AND DivisionID='" & DivID & "' AND OrderNumber='" & OrderNo & "'"

            Dim SQLStr As String = "SELECT     TOP (1) OrderHeader.*, OrderDetail.CardMessage " _
                                    & "FROM OrderHeader Left outer JOIN " _
                                    & "OrderDetail ON OrderHeader.CompanyID = OrderDetail.CompanyID " _
                                    & "AND OrderHeader.DivisionID = OrderDetail.DivisionID AND " _
                                    & "OrderHeader.DepartmentID = OrderDetail.DepartmentID And " _
                                    & "OrderHeader.OrderNumber = OrderDetail.OrderNumber " _
                                    & "WHERE (OrderHeader.CompanyID = '" & CompanyID & "') AND " _
                                    & "(OrderHeader.DivisionID = '" & DivID & "') AND " _
                                    & "(OrderHeader.DepartmentID = '" & DeptID & "') AND " _
                                    & "(OrderHeader.OrderNumber = '" & OrderNo & "')"
            Dim Cmd As New SqlCommand
            Cmd.CommandText = SQLStr
            Cmd.Connection = ConString
            Dim rs As SqlDataReader
            ConString.Open()
            rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)

            Return rs
        End Function
#End Region

#Region "GetOrderNumberThruInvoiceNumber"
        Public Function GetOrderNumberThruInvoiceNumber(ByVal CompanyID As String, ByVal DeptID As String, ByVal DivID As String, ByVal InvNo As String) As SqlDataReader
            Dim ConnectionString As String = ""

            ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim SQLStr As String = "SELECT OrderNumber FROM InvoiceHeader WHERE CompanyID='" & CompanyID & "' AND DepartmentID='" & DeptID & "' AND DivisionID='" & DivID & "' AND InvoiceNumber='" & InvNo & "'"
            Dim Cmd As New SqlCommand
            Cmd.CommandText = SQLStr
            Cmd.Connection = ConString
            Dim rs As SqlDataReader
            ConString.Open()
            rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)

            Return rs
            rs.Close()
        End Function
#End Region

#Region "PopulateTopLevelControls"
        Public Function PopulateTopLevelControls(ByVal CompanyID As String, ByVal DeptID As String, ByVal DivID As String, ByVal OrderNo As String) As SqlDataReader
            Dim ConnectionString As String = ""

            ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim SQLStr As String = "SELECT PaymentMethodID,EmployeeID,OrderTypeID,TransactionTypeID,OrderDate FROM OrderHeader WHERE CompanyID='" & CompanyID & "' AND DepartmentID='" & DeptID & "' AND DivisionID='" & DivID & "' AND OrderNumber='" & OrderNo & "'"
            Dim Cmd As New SqlCommand
            Cmd.CommandText = SQLStr
            Cmd.Connection = ConString
            Dim rs As SqlDataReader
            ConString.Open()
            rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)

            Return rs
            rs.Close()
        End Function
#End Region

#Region "PopulateItemDefaultWarehouse"
        Public Function PopulateItemDefaultWarehouse(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As SqlDataReader
            Dim ConnectionString As String = ""

            ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim sqlStr As String = "SELECT Distinct ItemDefaultWarehouse FROM InventoryItems WHERE CompanyID='" & CompanyID & "'"
            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            Dim rs As SqlDataReader
            ConString.Open()
            rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)

            Return rs
        End Function
#End Region

#Region "PopulateItemDefaultWarehouseBin"
        Public Function PopulateItemDefaultWarehouseBin(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As SqlDataReader
            Dim ConnectionString As String = ""

            ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim sqlStr As String = "SELECT Distinct ItemDefaultWarehouseBin FROM InventoryItems WHERE CompanyID='" & CompanyID & "'"
            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            Dim rs As SqlDataReader
            ConString.Open()
            rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)

            Return rs
        End Function
#End Region

#Region "PopulateGLAccountNumber"
        Public Function PopulateGLAccountNumber(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As SqlDataReader
            Dim ConnectionString As String = ""

            ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim sqlStr As String = "SELECT GLAccountNumber+ ', ' + GLAccountName as  test, GLAccountNumber FROM LedgerChartOfAccounts WHERE CompanyID='" & CompanyID & "'"
            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            Dim rs As SqlDataReader
            ConString.Open()
            rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)

            Return rs
        End Function
#End Region

#Region "PopulateProjectID"
        Public Function PopulateProjectID(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As SqlDataReader
            Dim ConnectionString As String = ""

            ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim sqlStr As String = "SELECT Distinct ProjectID FROM OrderDetail WHERE CompanyID='" & CompanyID & "'"
            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            Dim rs As SqlDataReader
            ConString.Open()
            rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)

            Return rs
        End Function
#End Region

#Region "PopulateTaxDetails"
        Public Function PopulateTaxDetails(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As SqlDataReader
            Dim ConnectionString As String = ""

            ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim sqlStr As String = "SELECT Distinct TaxGroupID FROM TaxGroups WHERE CompanyID='" & CompanyID & "'"
            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            Dim rs As SqlDataReader
            ConString.Open()
            rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)

            Return rs
        End Function
#End Region

#Region "PopulateTaxPercentage"
        Public Function PopulateTaxPercentage(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal TaxGroupID As String) As SqlDataReader
            Dim ConnectionString As String = ""

            ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim sqlStr As String = "SELECT TotalPercent FROM TaxGroups WHERE CompanyID='" & CompanyID & "' and TaxGroupID= '" & TaxGroupID & "'"
            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            Dim rs As SqlDataReader
            ConString.Open()
            rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)

            Return rs
        End Function
#End Region

#Region "PopulateItemID"
        Public Function PopulateItemID(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As SqlDataReader

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("enterprise.PopulateItemID", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            myCon.Open()

            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(Data.CommandBehavior.CloseConnection)
            Return rs

        End Function
#End Region

#Region "PopulateItemDetails"
        Public Function PopulateItemDetails(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal ItemID As String) As SqlDataReader
            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("enterprise.Inventory_PopulateItemInfoSimple", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterItemID As New SqlParameter("@ItemID", Data.SqlDbType.NVarChar)
            parameterItemID.Value = ItemID
            myCommand.Parameters.Add(parameterItemID)
            myCon.Open()

            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(Data.CommandBehavior.CloseConnection)
            Return rs
        End Function
#End Region

#Region "GetNextOrdNumber"
        Public Function GetNextOrdNumber(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal Entity As String) As SqlDataReader
            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("enterprise.[GetNextOrderNumber]", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterEntity As New SqlParameter("@Entity", Data.SqlDbType.NVarChar)
            parameterEntity.Value = Entity
            myCommand.Parameters.Add(parameterEntity)


            myCon.Open()

            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(Data.CommandBehavior.CloseConnection)
            Return rs
        End Function
#End Region

#Region "AddEditedItemDetails()"
        Public Sub AddEditedItemDetails()

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("dbo.[AddEditedItemDetails]", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterOrderLineNumber As New SqlParameter("@OrderLineNumber ", Data.SqlDbType.NVarChar)
            parameterOrderLineNumber.Value = OrderLineNumber
            myCommand.Parameters.Add(parameterOrderLineNumber)

            Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
            parameterOrderNumber.Value = OrderNumber
            myCommand.Parameters.Add(parameterOrderNumber)

            Dim parameterItemID As New SqlParameter("@ItemID  ", Data.SqlDbType.NVarChar)
            parameterItemID.Value = ItemID
            myCommand.Parameters.Add(parameterItemID)

            Dim parameterDescription As New SqlParameter("@Description   ", Data.SqlDbType.NVarChar)
            parameterDescription.Value = Description
            myCommand.Parameters.Add(parameterDescription)

            Dim parameterOrderQty As New SqlParameter("@OrderQty ", Data.SqlDbType.Float)
            parameterOrderQty.Value = Quantity
            myCommand.Parameters.Add(parameterOrderQty)

            Dim parameterItemUnitPrice As New SqlParameter("@ItemUnitPrice  ", Data.SqlDbType.Money)
            parameterItemUnitPrice.Value = ItemUnitPrice
            myCommand.Parameters.Add(parameterItemUnitPrice)

            Dim parameterSubTotal As New SqlParameter("@SubTotal    ", Data.SqlDbType.Money)
            parameterSubTotal.Value = SubTotal
            myCommand.Parameters.Add(parameterSubTotal)

            myCon.Open()

            myCommand.ExecuteNonQuery()
            myCon.Close()
            'Dim rs As SqlDataReader
            'rs = myCommand.ExecuteReader(Data.CommandBehavior.CloseConnection)

        End Sub
#End Region

#Region "AddEditItemDetailsDetails"
        Public Sub AddEditItemDetailsDetails()



            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("dbo.[AddEditItemDetailsDetails1]", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterOrderLineNumber As New SqlParameter("@OrderLineNumber ", Data.SqlDbType.NVarChar)
            parameterOrderLineNumber.Value = OrderLineNumber
            myCommand.Parameters.Add(parameterOrderLineNumber)

            Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
            parameterOrderNumber.Value = OrderNumber
            myCommand.Parameters.Add(parameterOrderNumber)


            Dim parameterItemID As New SqlParameter("@ItemID  ", Data.SqlDbType.NVarChar)
            parameterItemID.Value = ItemID
            myCommand.Parameters.Add(parameterItemID)

            Dim parameterWarehouseID As New SqlParameter("@WarehouseID  ", Data.SqlDbType.NVarChar)
            parameterWarehouseID.Value = WarehouseID
            myCommand.Parameters.Add(parameterWarehouseID)

            Dim parameterWarehouseBinID As New SqlParameter("@WarehouseBinID  ", Data.SqlDbType.NVarChar)
            parameterWarehouseBinID.Value = WarehouseBinID
            myCommand.Parameters.Add(parameterWarehouseBinID)


            Dim parameterSerialNumber As New SqlParameter("@SerialNumber  ", Data.SqlDbType.NVarChar)
            parameterSerialNumber.Value = SerialNumber
            myCommand.Parameters.Add(parameterSerialNumber)

            Dim parameterDescription As New SqlParameter("@Description   ", Data.SqlDbType.NVarChar)
            parameterDescription.Value = Description
            myCommand.Parameters.Add(parameterDescription)

            Dim parameterOrderQty As New SqlParameter("@OrderQty ", Data.SqlDbType.Float)
            parameterOrderQty.Value = OrderQty
            myCommand.Parameters.Add(parameterOrderQty)

            Dim parameterBackOrdered As New SqlParameter("@BackOrdered ", Data.SqlDbType.Bit)
            parameterBackOrdered.Value = BackOrdered
            myCommand.Parameters.Add(parameterBackOrdered)

            Dim parameterBackOrderQyyty As New SqlParameter("@BackOrderQyyty ", Data.SqlDbType.Float)
            parameterBackOrderQyyty.Value = BackOrderQyyty
            myCommand.Parameters.Add(parameterBackOrderQyyty)

            Dim parameterItemUOM As New SqlParameter("@ItemUOM    ", Data.SqlDbType.NVarChar)
            parameterItemUOM.Value = ItemUOM
            myCommand.Parameters.Add(parameterItemUOM)

            Dim parameterItemWeight As New SqlParameter("@ItemWeight  ", Data.SqlDbType.Float)
            parameterItemWeight.Value = ItemWeight
            myCommand.Parameters.Add(parameterItemWeight)

            Dim parameterDiscountPerc As New SqlParameter("@DiscountPerc ", Data.SqlDbType.Float)
            parameterDiscountPerc.Value = DiscountPerc
            myCommand.Parameters.Add(parameterDiscountPerc)

            Dim parameterTaxable As New SqlParameter("@Taxable ", Data.SqlDbType.Bit)
            parameterTaxable.Value = Taxable
            myCommand.Parameters.Add(parameterTaxable)

            Dim parameterItemCost As New SqlParameter("@ItemCost  ", Data.SqlDbType.Money)
            parameterItemCost.Value = ItemCost
            myCommand.Parameters.Add(parameterItemCost)

            Dim parameterItemUnitPrice As New SqlParameter("@ItemUnitPrice  ", Data.SqlDbType.Money)
            parameterItemUnitPrice.Value = ItemUnitPrice
            myCommand.Parameters.Add(parameterItemUnitPrice)

            Dim parameterTaxGroupID As New SqlParameter("@TaxGroupID     ", Data.SqlDbType.NVarChar)
            parameterTaxGroupID.Value = TaxGroupID
            myCommand.Parameters.Add(parameterTaxGroupID)

            Dim parameterTaxAmount As New SqlParameter("@TaxAmount   ", Data.SqlDbType.Money)
            parameterTaxAmount.Value = TaxAmount
            myCommand.Parameters.Add(parameterTaxAmount)

            Dim parameterTaxPercent As New SqlParameter("@TaxPercent  ", Data.SqlDbType.Float)
            parameterTaxPercent.Value = TaxPercent
            myCommand.Parameters.Add(parameterTaxPercent)

            Dim parameterSubTotal As New SqlParameter("@SubTotal    ", Data.SqlDbType.Money)
            parameterSubTotal.Value = SubTotal
            myCommand.Parameters.Add(parameterSubTotal)

            Dim parameterTotal As New SqlParameter("@Total    ", Data.SqlDbType.Money)
            parameterTotal.Value = Total
            myCommand.Parameters.Add(parameterTotal)

            Dim parameterTotalWeight As New SqlParameter("@TotalWeight  ", Data.SqlDbType.Float)
            parameterTotalWeight.Value = TotalWeight
            myCommand.Parameters.Add(parameterTotalWeight)

            Dim parameterGLSalesAccount As New SqlParameter("@GLSalesAccount      ", Data.SqlDbType.NVarChar)
            parameterGLSalesAccount.Value = GLSalesAccount
            myCommand.Parameters.Add(parameterGLSalesAccount)

            Dim parameterProjectID As New SqlParameter("@ProjectID      ", Data.SqlDbType.NVarChar)
            parameterProjectID.Value = ProjectID
            myCommand.Parameters.Add(parameterProjectID)

            Dim parameterTrackingNumber As New SqlParameter("@TrackingNumber      ", Data.SqlDbType.NVarChar)
            parameterTrackingNumber.Value = TrackingNumber
            myCommand.Parameters.Add(parameterTrackingNumber)

            Dim parameterAddEdit As New SqlParameter("@AddEdit", Data.SqlDbType.Int)
            parameterAddEdit.Value = AddEdit
            myCommand.Parameters.Add(parameterAddEdit)

            Dim parameterEmployeeID As New SqlParameter("@EmployeeID", Data.SqlDbType.NVarChar)
            parameterEmployeeID.Value = EmployeeID
            myCommand.Parameters.Add(parameterEmployeeID)


            myCon.Open()

            myCommand.ExecuteNonQuery()
            myCon.Close()
            'Dim rs As SqlDataReader
            'rs = myCommand.ExecuteReader(Data.CommandBehavior.CloseConnection)

        End Sub
#End Region
     
#Region "AddEditHeaderDetails"

        Public DiscountCouponAmount As Double = 0
        Public Assignedto As String = ""


        Public Sub AddEditHeaderDetails()

            If IsNumeric(OrderNumber.Trim) Then
            Else
                Exit Sub
            End If

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("[dbo].[POSAddEditHeaderDetailSection]", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)


            Dim parameterDiscountCouponAmount As New SqlParameter("@DiscountCouponAmount", Data.SqlDbType.Money)
            parameterDiscountCouponAmount.Value = DiscountCouponAmount
            myCommand.Parameters.Add(parameterDiscountCouponAmount)

            Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
            parameterOrderNumber.Value = OrderNumber
            myCommand.Parameters.Add(parameterOrderNumber)


            Dim parameterTransactionTypeID As New SqlParameter("@TransactionTypeID   ", Data.SqlDbType.NVarChar)
            parameterTransactionTypeID.Value = TransactionTypeID
            myCommand.Parameters.Add(parameterTransactionTypeID)

            Dim parameterOrderTypeID As New SqlParameter("@OrderTypeID", Data.SqlDbType.NVarChar)
            parameterOrderTypeID.Value = OrderTypeID
            myCommand.Parameters.Add(parameterOrderTypeID)

            'Dim parameterOrderDate As New SqlParameter("@OrderDate    ", Data.SqlDbType.DateTime)
            'parameterOrderDate.Value = OrderDate
            'myCommand.Parameters.Add(parameterOrderDate)

            If OrderShipDate = "" Then
                OrderShipDate = DateTime.Now.ToShortDateString()
            End If
            Dim parameterOrderShipDate As New SqlParameter("@OrderShipDate", Data.SqlDbType.DateTime)
            parameterOrderShipDate.Value = Convert.ToDateTime(OrderShipDate)
            myCommand.Parameters.Add(parameterOrderShipDate)

            Dim parameterCustomerID As New SqlParameter("@CustomerID", Data.SqlDbType.NVarChar)
            parameterCustomerID.Value = CustomerID
            myCommand.Parameters.Add(parameterCustomerID)

            Dim parameterEmployeeID As New SqlParameter("@EmployeeID", Data.SqlDbType.NVarChar)
            parameterEmployeeID.Value = EmployeeID
            myCommand.Parameters.Add(parameterEmployeeID)

            Dim parameterShipMethodID As New SqlParameter("@ShipMethodID", Data.SqlDbType.NVarChar)
            parameterShipMethodID.Value = ShipMethodID
            myCommand.Parameters.Add(parameterShipMethodID)


            Dim parameterAssignedto As New SqlParameter("@Assignedto", Data.SqlDbType.NVarChar)
            parameterAssignedto.Value = Assignedto
            myCommand.Parameters.Add(parameterAssignedto)


            Dim parameterShippingFirstName As New SqlParameter("@ShippingFirstName", Data.SqlDbType.NVarChar)
            parameterShippingFirstName.Value = ShippingFirstName
            myCommand.Parameters.Add(parameterShippingFirstName)

            Dim parameterShippingLastName As New SqlParameter("@ShippingLastName", Data.SqlDbType.NVarChar)
            parameterShippingLastName.Value = ShippingLastName
            myCommand.Parameters.Add(parameterShippingLastName)

            Dim parameterShippingAddress1 As New SqlParameter("@ShippingAddress1", Data.SqlDbType.NVarChar)
            parameterShippingAddress1.Value = ShippingAddress1
            myCommand.Parameters.Add(parameterShippingAddress1)

            Dim parameterShippingAddress2 As New SqlParameter("@ShippingAddress2", Data.SqlDbType.NVarChar)
            parameterShippingAddress2.Value = ShippingAddress2
            myCommand.Parameters.Add(parameterShippingAddress2)

            Dim parameterShippingAddress3 As New SqlParameter("@ShippingAddress3", Data.SqlDbType.NVarChar)
            parameterShippingAddress3.Value = ShippingAddress3
            myCommand.Parameters.Add(parameterShippingAddress3)

            Dim parameterShippingCity As New SqlParameter("@ShippingCity", Data.SqlDbType.NVarChar)
            parameterShippingCity.Value = ShippingCity
            myCommand.Parameters.Add(parameterShippingCity)

            Dim parameterShippingState As New SqlParameter("@ShippingState", Data.SqlDbType.NVarChar)
            parameterShippingState.Value = ShippingState
            myCommand.Parameters.Add(parameterShippingState)

            Dim parameterShippingZip As New SqlParameter("@ShippingZip", Data.SqlDbType.NVarChar)
            parameterShippingZip.Value = ShippingZip
            myCommand.Parameters.Add(parameterShippingZip)

            Dim parameterShippingCountry As New SqlParameter("@ShippingCountry", Data.SqlDbType.NVarChar)
            parameterShippingCountry.Value = ShippingCountry
            myCommand.Parameters.Add(parameterShippingCountry)

            Dim parameterPaymentMethodID As New SqlParameter("@PaymentMethodID", Data.SqlDbType.NVarChar)
            parameterPaymentMethodID.Value = PaymentMethodID
            myCommand.Parameters.Add(parameterPaymentMethodID)


            Dim parameterCreditCardType As New SqlParameter("@CreditCardType", Data.SqlDbType.NVarChar)
            parameterCreditCardType.Value = CreditCardType
            myCommand.Parameters.Add(parameterCreditCardType)

            Dim EncryptCreditCard As New Encryption

            Dim parameterCreditCardNumber As New SqlParameter("@CreditCardNumber", Data.SqlDbType.NVarChar)
            'parameterCreditCardNumber.Value = EncryptCreditCard.EncryptData(CustomerID, CreditCardNumber)
            If CreditCardNumber.Trim() <> "" Then
                parameterCreditCardNumber.Value = EncryptCreditCard.TripleDESEncode(CreditCardNumber, CustomerID)
            Else
                parameterCreditCardNumber.Value = ""
            End If

            myCommand.Parameters.Add(parameterCreditCardNumber)

            Dim parameterCreditCardExpDate As New SqlParameter("@CreditCardExpDate", Data.SqlDbType.DateTime)
            parameterCreditCardExpDate.Value = CreditCardExpDate
            myCommand.Parameters.Add(parameterCreditCardExpDate)

            Dim parameterCreditCardCSVNumber As New SqlParameter("@CreditCardCSVNumber", Data.SqlDbType.NVarChar)
            parameterCreditCardCSVNumber.Value = CreditCardCSVNumber
            myCommand.Parameters.Add(parameterCreditCardCSVNumber)

            Dim parameterCreditCardValidationCode As New SqlParameter("@CreditCardValidationCode", Data.SqlDbType.NVarChar)
            parameterCreditCardValidationCode.Value = CreditCardValidationCode
            myCommand.Parameters.Add(parameterCreditCardValidationCode)

            Dim parameterCreditCardApprovalNumber As New SqlParameter("@CreditCardApprovalNumber", Data.SqlDbType.NVarChar)
            parameterCreditCardApprovalNumber.Value = CreditCardApprovalNumber
            myCommand.Parameters.Add(parameterCreditCardApprovalNumber)

            Dim parameterCreditCardBillToZip As New SqlParameter("@CreditCardBillToZip", Data.SqlDbType.NVarChar)
            parameterCreditCardBillToZip.Value = CreditCardBillToZip
            myCommand.Parameters.Add(parameterCreditCardBillToZip)


            Dim parameterShippingAttention As New SqlParameter("@ShippingAttention", Data.SqlDbType.NVarChar)
            parameterShippingAttention.Value = ShippingAttention
            myCommand.Parameters.Add(parameterShippingAttention)


            Dim parameterShippingPhone As New SqlParameter("@ShippingPhone", Data.SqlDbType.NVarChar)
            parameterShippingPhone.Value = ShippingPhone
            myCommand.Parameters.Add(parameterShippingPhone)


            Dim parameterShippingFax As New SqlParameter("@ShippingFax", Data.SqlDbType.NVarChar)
            parameterShippingFax.Value = ShippingFax
            myCommand.Parameters.Add(parameterShippingFax)


            Dim parameterShippingCell As New SqlParameter("@ShippingCell", Data.SqlDbType.NVarChar)
            parameterShippingCell.Value = ShippingCell
            myCommand.Parameters.Add(parameterShippingCell)


            Dim parameterShippingExt As New SqlParameter("@ShippingExt", Data.SqlDbType.NVarChar)
            parameterShippingExt.Value = ShippingExt
            myCommand.Parameters.Add(parameterShippingExt)



            Dim parameterDestinationType As New SqlParameter("@DestinationType", Data.SqlDbType.NVarChar)
            parameterDestinationType.Value = DestinationType
            myCommand.Parameters.Add(parameterDestinationType)

            Dim parameterPriority As New SqlParameter("@Priority", Data.SqlDbType.NVarChar)
            parameterPriority.Value = Priority
            myCommand.Parameters.Add(parameterPriority)

            Dim parameterOccasionCode As New SqlParameter("@OccasionCode", Data.SqlDbType.NVarChar)
            parameterOccasionCode.Value = OccasionCode
            myCommand.Parameters.Add(parameterOccasionCode)


            Dim parameterShippingCompany As New SqlParameter("@ShippingCompany", Data.SqlDbType.NVarChar)
            parameterShippingCompany.Value = ShippingCompany
            myCommand.Parameters.Add(parameterShippingCompany)

            Dim parameterIpAddress As New SqlParameter("@IpAddress", Data.SqlDbType.NVarChar)
            parameterIpAddress.Value = IpAddress
            myCommand.Parameters.Add(parameterIpAddress)

            Dim parameterFraudRating As New SqlParameter("@FraudRating", Data.SqlDbType.NVarChar)
            parameterFraudRating.Value = FraudRating
            myCommand.Parameters.Add(parameterFraudRating)

            Dim parameterShippingSalutation As New SqlParameter("@ShippingSalutation", Data.SqlDbType.NVarChar)
            parameterShippingSalutation.Value = ShippingSalutation
            myCommand.Parameters.Add(parameterShippingSalutation)

            Dim parameterProjectID As New SqlParameter("@ProjectID", Data.SqlDbType.NVarChar)
            parameterProjectID.Value = ProjectID
            myCommand.Parameters.Add(parameterProjectID)

            Dim parameterWarehouseID As New SqlParameter("@WarehouseID", Data.SqlDbType.NVarChar)
            parameterWarehouseID.Value = WarehouseID
            myCommand.Parameters.Add(parameterWarehouseID)


            Dim parameterCheckID As New SqlParameter("@CheckID", Data.SqlDbType.NVarChar)
            parameterCheckID.Value = CheckID
            myCommand.Parameters.Add(parameterCheckID)

            Dim parameterCheckNumber As New SqlParameter("@CheckNumber", Data.SqlDbType.NVarChar)
            parameterCheckNumber.Value = CheckNumber
            myCommand.Parameters.Add(parameterCheckNumber)

            Dim parameterGiftCertificate As New SqlParameter("@GiftCertificate", Data.SqlDbType.NVarChar)
            parameterGiftCertificate.Value = GiftCertificate
            myCommand.Parameters.Add(parameterGiftCertificate)


            Dim parameterCoupon As New SqlParameter("@Coupon", Data.SqlDbType.NVarChar)
            parameterCoupon.Value = Coupon
            myCommand.Parameters.Add(parameterCoupon)

            Dim parameterWireService As New SqlParameter("@WireService", Data.SqlDbType.NVarChar)
            parameterWireService.Value = WireService
            myCommand.Parameters.Add(parameterWireService)


            Dim parameterWireCode As New SqlParameter("@WireCode", Data.SqlDbType.NVarChar)
            parameterWireCode.Value = WireCode
            myCommand.Parameters.Add(parameterWireCode)


            Dim parameterWireRefernceID As New SqlParameter("@WireRefernceID", Data.SqlDbType.NVarChar)
            parameterWireRefernceID.Value = WireRefernceID
            myCommand.Parameters.Add(parameterWireRefernceID)

            Dim parameterWireTransmitMethod As New SqlParameter("@WireTransmitMethod", Data.SqlDbType.NVarChar)
            parameterWireTransmitMethod.Value = WireTransmitMethod
            myCommand.Parameters.Add(parameterWireTransmitMethod)

            Dim parameterInternalNotes As New SqlParameter("@InternalNotes", Data.SqlDbType.NVarChar)
            parameterInternalNotes.Value = InternalNotes
            myCommand.Parameters.Add(parameterInternalNotes)

            Dim parameterDriverRouteInfo As New SqlParameter("@DriverRouteInfo", Data.SqlDbType.NVarChar)
            parameterDriverRouteInfo.Value = DriverRouteInfo
            myCommand.Parameters.Add(parameterDriverRouteInfo)

            '' New Code

            Dim parameterSubTotal As New SqlParameter("@Subtotal", Data.SqlDbType.Money)
            parameterSubTotal.Value = SubTotal
            myCommand.Parameters.Add(parameterSubTotal)

            Dim parameterTaxGroupID As New SqlParameter("@TaxGroupID", Data.SqlDbType.NVarChar)
            parameterTaxGroupID.Value = TaxGroupID
            myCommand.Parameters.Add(parameterTaxGroupID)

            Dim parameterTaxPercent As New SqlParameter("@TaxPercent", Data.SqlDbType.Float)
            parameterTaxPercent.Value = TaxPercent
            myCommand.Parameters.Add(parameterTaxPercent)

            Dim parameterTaxAmount As New SqlParameter("@TaxAmount", Data.SqlDbType.Money)
            parameterTaxAmount.Value = TaxAmount
            myCommand.Parameters.Add(parameterTaxAmount)

            Dim parameterDiscountAmount As New SqlParameter("@DiscountAmount", Data.SqlDbType.Money)
            parameterDiscountAmount.Value = Me.Discounts
            myCommand.Parameters.Add(parameterDiscountAmount)

            Dim parameterTotal As New SqlParameter("@Total", Data.SqlDbType.Money)
            parameterTotal.Value = Total
            myCommand.Parameters.Add(parameterTotal)

            Dim parameterRelay As New SqlParameter("@Relay", Data.SqlDbType.Money)
            parameterRelay.Value = Relay
            myCommand.Parameters.Add(parameterRelay)

            Dim parameterDelivery As New SqlParameter("@Delivery", Data.SqlDbType.Money)
            parameterDelivery.Value = Delivery
            myCommand.Parameters.Add(parameterDelivery)

            Dim parameterService As New SqlParameter("@Service", Data.SqlDbType.Money)
            parameterService.Value = Service
            myCommand.Parameters.Add(parameterService)


            Dim parameterOrderSourceCode As New SqlParameter("@OrderSourceCode", Data.SqlDbType.NVarChar)
            parameterOrderSourceCode.Value = OrderSourceCode
            myCommand.Parameters.Add(parameterOrderSourceCode)



            Dim parameterCustomerComments As New SqlParameter("@CustomerComments", Data.SqlDbType.NText)
            parameterCustomerComments.Value = CustomerComments
            myCommand.Parameters.Add(parameterCustomerComments)

            Dim parameterAddEdit2 As New SqlParameter("@AddEdit2", Data.SqlDbType.NVarChar)
            parameterAddEdit2.Value = AddEdit2
            myCommand.Parameters.Add(parameterAddEdit2)


            Dim parameterPO As New SqlParameter("@PO", Data.SqlDbType.NVarChar)
            parameterPO.Value = PO
            myCommand.Parameters.Add(parameterPO)

            myCon.Open()

            myCommand.ExecuteNonQuery()
            myCon.Close()

            'Dim rs As SqlDataReader
            'rs = myCommand.ExecuteReader(Data.CommandBehavior.CloseConnection)
            'Return rs

        End Sub
#End Region

#Region "UpdateCustomerDetails"

        Public Sub UpdateCustomerDetails()

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("UpdateCustomerInformationFromOrderForm", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterCustomerID As New SqlParameter("@CustomerID", Data.SqlDbType.NVarChar)
            parameterCustomerID.Value = CustomerID
            myCommand.Parameters.Add(parameterCustomerID)

            Dim parameterCustomerFirstName As New SqlParameter("@CustomerFirstName", Data.SqlDbType.NVarChar)
            parameterCustomerFirstName.Value = CustomerFirstName
            myCommand.Parameters.Add(parameterCustomerFirstName)

            Dim parameterCustomerLastName As New SqlParameter("@CustomerLastName", Data.SqlDbType.NVarChar)
            parameterCustomerLastName.Value = CustomerLastName
            myCommand.Parameters.Add(parameterCustomerLastName)

            Dim parameterAttention As New SqlParameter("@Attention", Data.SqlDbType.NVarChar)
            parameterAttention.Value = Attention
            myCommand.Parameters.Add(parameterAttention)

            Dim parameterCustomerAddress1 As New SqlParameter("@CustomerAddress1", Data.SqlDbType.NVarChar)
            parameterCustomerAddress1.Value = CustomerAddress1
            myCommand.Parameters.Add(parameterCustomerAddress1)

            Dim parameterCustomerAddress2 As New SqlParameter("@CustomerAddress2", Data.SqlDbType.NVarChar)
            parameterCustomerAddress2.Value = CustomerAddress2
            myCommand.Parameters.Add(parameterCustomerAddress2)

            Dim parameterCustomerAddress3 As New SqlParameter("@CustomerAddress3", Data.SqlDbType.NVarChar)
            parameterCustomerAddress3.Value = CustomerAddress3
            myCommand.Parameters.Add(parameterCustomerAddress3)

            Dim parameterCustomerCity As New SqlParameter("@CustomerCity", Data.SqlDbType.NVarChar)
            parameterCustomerCity.Value = CustomerCity
            myCommand.Parameters.Add(parameterCustomerCity)

            Dim parameterCustomerState As New SqlParameter("@CustomerState", Data.SqlDbType.NVarChar)
            parameterCustomerState.Value = CustomerState
            myCommand.Parameters.Add(parameterCustomerState)

            Dim parameterCustomerCountry As New SqlParameter("@CustomerCountry", Data.SqlDbType.NVarChar)
            parameterCustomerCountry.Value = CustomerCountry
            myCommand.Parameters.Add(parameterCustomerCountry)


            Dim parameterCustomerFax As New SqlParameter("@CustomerFax", Data.SqlDbType.NVarChar)
            parameterCustomerFax.Value = CustomerFax
            myCommand.Parameters.Add(parameterCustomerFax)

            Dim parameterCustomerPhone As New SqlParameter("@CustomerPhone", Data.SqlDbType.NVarChar)
            parameterCustomerPhone.Value = CustomerPhone
            myCommand.Parameters.Add(parameterCustomerPhone)

            Dim parameterCustomerPhoneExt As New SqlParameter("@CustomerPhoneExt", Data.SqlDbType.NVarChar)
            parameterCustomerPhoneExt.Value = CustomerPhoneExt
            myCommand.Parameters.Add(parameterCustomerPhoneExt)

            Dim parameterCustomerEmail As New SqlParameter("@CustomerEmail", Data.SqlDbType.NVarChar)
            parameterCustomerEmail.Value = CustomerEmail
            myCommand.Parameters.Add(parameterCustomerEmail)

            If CreditLimit = "" Then
                CreditLimit = "0"
            End If

            Dim parameterCreditLimit As New SqlParameter("@CreditLimit", Data.SqlDbType.Money)
            parameterCreditLimit.Value = Double.Parse(CreditLimit)
            myCommand.Parameters.Add(parameterCreditLimit)

            Dim parameterAccountStatus As New SqlParameter("@AccountStatus", Data.SqlDbType.NVarChar)
            parameterAccountStatus.Value = AccountStatus
            myCommand.Parameters.Add(parameterAccountStatus)
            If CustomerSince = "" Then
                CustomerSince = DateTime.Now.ToShortDateString()
            End If
            Dim parameterCustomerSince As New SqlParameter("@CustomerSince", Data.SqlDbType.DateTime)
            parameterCustomerSince.Value = DateTime.Parse(CustomerSince)
            myCommand.Parameters.Add(parameterCustomerSince)

            Dim parameterCreditComments As New SqlParameter("@CreditComments", Data.SqlDbType.NVarChar)
            parameterCreditComments.Value = CreditComments
            myCommand.Parameters.Add(parameterCreditComments)


            Dim parameterCustomerComments As New SqlParameter("@CustomerComments", Data.SqlDbType.NVarChar)
            parameterCustomerComments.Value = CustomerComments
            myCommand.Parameters.Add(parameterCustomerComments)





            Dim parameterCustomerZip As New SqlParameter("@CustomerZip", Data.SqlDbType.NVarChar)
            parameterCustomerZip.Value = CustomerZip
            myCommand.Parameters.Add(parameterCustomerZip)


            Dim parameterCustomerCompany As New SqlParameter("@CustomerCompany", Data.SqlDbType.NVarChar)
            parameterCustomerCompany.Value = CustomerCompany
            myCommand.Parameters.Add(parameterCustomerCompany)



            Dim parameterCustomerCell As New SqlParameter("@CustomerCell", Data.SqlDbType.NVarChar)
            parameterCustomerCell.Value = CustomerCell
            myCommand.Parameters.Add(parameterCustomerCell)


            'Dim parameterPO As New SqlParameter("@PO", Data.SqlDbType.NVarChar)
            'parameterPO.Value = PO
            'myCommand.Parameters.Add(parameterPO)


            Dim parameterNewsletter As New SqlParameter("@Newsletter", Data.SqlDbType.NVarChar)
            parameterNewsletter.Value = Newsletter
            myCommand.Parameters.Add(parameterNewsletter)

            Dim parameterCustomerSalutation As New SqlParameter("@CustomerSalutation", Data.SqlDbType.NVarChar)
            parameterCustomerSalutation.Value = CustomerSalutation
            myCommand.Parameters.Add(parameterCustomerSalutation)
 




            myCon.Open()
            myCommand.ExecuteNonQuery()
            myCon.Close()

            'Dim rs As SqlDataReader
            'rs = myCommand.ExecuteReader(Data.CommandBehavior.CloseConnection)
            'Return rs

        End Sub
#End Region

#Region "AddCustomerInformationFromOrderForm"

        Public Sub AddCustomerInformationFromOrderForm()

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("AddCustomerInformationFromOrderForm", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterCustomerID As New SqlParameter("@CustomerID", Data.SqlDbType.NVarChar)
            parameterCustomerID.Value = CustomerID
            myCommand.Parameters.Add(parameterCustomerID)

            Dim parameterCustomerFirstName As New SqlParameter("@CustomerFirstName", Data.SqlDbType.NVarChar)
            parameterCustomerFirstName.Value = CustomerFirstName
            myCommand.Parameters.Add(parameterCustomerFirstName)

            Dim parameterCustomerLastName As New SqlParameter("@CustomerLastName", Data.SqlDbType.NVarChar)
            parameterCustomerLastName.Value = CustomerLastName
            myCommand.Parameters.Add(parameterCustomerLastName)

            Dim parameterAttention As New SqlParameter("@Attention", Data.SqlDbType.NVarChar)
            parameterAttention.Value = Attention
            myCommand.Parameters.Add(parameterAttention)

            Dim parameterCustomerAddress1 As New SqlParameter("@CustomerAddress1", Data.SqlDbType.NVarChar)
            parameterCustomerAddress1.Value = CustomerAddress1
            myCommand.Parameters.Add(parameterCustomerAddress1)

            Dim parameterCustomerAddress2 As New SqlParameter("@CustomerAddress2", Data.SqlDbType.NVarChar)
            parameterCustomerAddress2.Value = CustomerAddress2
            myCommand.Parameters.Add(parameterCustomerAddress2)

            Dim parameterCustomerAddress3 As New SqlParameter("@CustomerAddress3", Data.SqlDbType.NVarChar)
            parameterCustomerAddress3.Value = CustomerAddress3
            myCommand.Parameters.Add(parameterCustomerAddress3)

            Dim parameterCustomerCity As New SqlParameter("@CustomerCity", Data.SqlDbType.NVarChar)
            parameterCustomerCity.Value = CustomerCity
            myCommand.Parameters.Add(parameterCustomerCity)

            Dim parameterCustomerState As New SqlParameter("@CustomerState", Data.SqlDbType.NVarChar)
            parameterCustomerState.Value = CustomerState
            myCommand.Parameters.Add(parameterCustomerState)

            Dim parameterCustomerCountry As New SqlParameter("@CustomerCountry", Data.SqlDbType.NVarChar)
            parameterCustomerCountry.Value = CustomerCountry
            myCommand.Parameters.Add(parameterCustomerCountry)


            Dim parameterCustomerFax As New SqlParameter("@CustomerFax", Data.SqlDbType.NVarChar)
            parameterCustomerFax.Value = CustomerFax
            myCommand.Parameters.Add(parameterCustomerFax)

            Dim parameterCustomerPhone As New SqlParameter("@CustomerPhone", Data.SqlDbType.NVarChar)
            parameterCustomerPhone.Value = CustomerPhone
            myCommand.Parameters.Add(parameterCustomerPhone)

            Dim parameterCustomerPhoneExt As New SqlParameter("@CustomerPhoneExt", Data.SqlDbType.NVarChar)
            parameterCustomerPhoneExt.Value = CustomerPhoneExt
            myCommand.Parameters.Add(parameterCustomerPhoneExt)

            Dim parameterCustomerEmail As New SqlParameter("@CustomerEmail", Data.SqlDbType.NVarChar)
            parameterCustomerEmail.Value = CustomerEmail
            myCommand.Parameters.Add(parameterCustomerEmail)

            If CreditLimit = "" Then
                CreditLimit = "0"
            End If

            Dim parameterCreditLimit As New SqlParameter("@CreditLimit", Data.SqlDbType.Money)
            parameterCreditLimit.Value = Double.Parse(CreditLimit)
            myCommand.Parameters.Add(parameterCreditLimit)

            Dim parameterAccountStatus As New SqlParameter("@AccountStatus", Data.SqlDbType.NVarChar)
            parameterAccountStatus.Value = AccountStatus
            myCommand.Parameters.Add(parameterAccountStatus)
            If CustomerSince = "" Then
                CustomerSince = DateTime.Now.ToShortDateString()
            End If
            Dim parameterCustomerSince As New SqlParameter("@CustomerSince", Data.SqlDbType.DateTime)
            parameterCustomerSince.Value = DateTime.Parse(CustomerSince)
            myCommand.Parameters.Add(parameterCustomerSince)

            Dim parameterCreditComments As New SqlParameter("@CreditComments", Data.SqlDbType.NVarChar)
            parameterCreditComments.Value = CreditComments
            myCommand.Parameters.Add(parameterCreditComments)


            'Dim parameterCustomerComments As New SqlParameter("@CustomerComments", Data.SqlDbType.NVarChar)
            'parameterCustomerComments.Value = CustomerComments
            'myCommand.Parameters.Add(parameterCustomerComments)


            Dim parameterCustomerZip As New SqlParameter("@CustomerZip", Data.SqlDbType.NVarChar)
            parameterCustomerZip.Value = CustomerZip
            myCommand.Parameters.Add(parameterCustomerZip)


            Dim parameterCustomerCompany As New SqlParameter("@CustomerCompany", Data.SqlDbType.NVarChar)
            parameterCustomerCompany.Value = CustomerCompany
            myCommand.Parameters.Add(parameterCustomerCompany)



            Dim parameterCustomerCell As New SqlParameter("@CustomerCell", Data.SqlDbType.NVarChar)
            parameterCustomerCell.Value = CustomerCell
            myCommand.Parameters.Add(parameterCustomerCell)


            'Dim parameterPO As New SqlParameter("@PO", Data.SqlDbType.NVarChar)
            'parameterPO.Value = PO
            'myCommand.Parameters.Add(parameterPO)

            Dim parameterCustomerSalutation As New SqlParameter("@CustomerSalutation", Data.SqlDbType.NVarChar)
            parameterCustomerSalutation.Value = CustomerSalutation
            myCommand.Parameters.Add(parameterCustomerSalutation)

            Dim parameterNewsletter As New SqlParameter("@Newsletter", Data.SqlDbType.NVarChar)
            parameterNewsletter.Value = Newsletter
            myCommand.Parameters.Add(parameterNewsletter)

            Dim paramReturnValue As New SqlParameter("@ReturnValue", Data.SqlDbType.Int)
            paramReturnValue.Direction = ParameterDirection.Output
            myCommand.Parameters.Add(paramReturnValue)

            myCon.Open()
            myCommand.ExecuteNonQuery()
            myCon.Close()


            Dim OutPutValue As Integer


            OutPutValue = Convert.ToInt32(paramReturnValue.Value)

            ' Return OutPutValue

            'Dim rs As SqlDataReader
            'rs = myCommand.ExecuteReader(Data.CommandBehavior.CloseConnection)
            'Return rs

        End Sub
#End Region

#Region "PopulateItemDetailsGrid"
        Public Function PopulateItemDetailsGrid(ByVal CompanyID As String, ByVal DeptID As String, ByVal DivID As String, ByVal OrderNumber As String) As Data.DataSet

            Dim ConnectionString As String = ""
            ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            'Dim sqlStr As String = "SELECT OrderQty,ItemID,Description,OrderLineNumber,ItemUOM,ItemUnitPrice,ItemCost,Total FROM OrderDetail WHERE CompanyID='" & CompanyID & "' AND DepartmentID='" & DeptID & "' AND DivisionID='" & DivID & "'AND OrderNumber='" & OrderNumber & "'"
            '   Dim sqlStr As String = "SELECT Distinct InventoryItems.ItemName,OrderQty,OrderDetail.ItemID,Description,OrderLineNumber,OrderDetail.ItemUOM,ItemUnitPrice,ISNULL(ItemCost,0) as ItemCost,ISNULL(SubTotal,0) as SubTotal,ISNULL(Total,0) as Total  FROM OrderDetail Left Outer Join InventoryItems on OrderDetail.ItemID=InventoryItems.ItemID WHERE OrderDetail.CompanyID='" & CompanyID & "' AND OrderDetail.DepartmentID='" & DeptID & "'AND OrderDetail.DivisionID='" & DivID & "'AND OrderNumber='" & OrderNumber & "' order by ItemID asc "
            ' Commented Query'
            ' Dim sqlStr As String = "SELECT Distinct InventoryItems.ItemName,OrderQty,OrderDetail.ItemID,Description,OrderLineNumber,AddOnsIDsQty,UpSellPrice,OrderDetail.ItemUOM,ItemUnitPrice,ItemCost,ISNULL(SubTotal,0) as SubTotal,ISNULL(Total,0) as Total FROM OrderDetail Left Outer Join InventoryItems on OrderDetail.ItemID=InventoryItems.ItemID WHERE OrderDetail.CompanyID='" & CompanyID & "' AND OrderDetail.DepartmentID='" & DeptID & "'AND OrderDetail.DivisionID='" & DivID & "'AND OrderNumber='" & OrderNumber & "' order by ItemID asc "
            'Dim sqlStr As String = "SELECT Distinct OrderDetail.OrderQty,OrderDetail.ItemID,OrderDetail.Description,OrderDetail.OrderLineNumber,OrderDetail.ItemUOM,OrderDetail.ItemUnitPrice,OrderDetail.ItemCost,OrderDetail.Total,InventoryItems.ItemName FROM OrderDetail Left outer join InventoryItems on OrderDetail.ItemID=InventoryItems.ItemID WHERE	OrderDetail.CompanyID='" & CompanyID & "' AND OrderDetail.DepartmentID='" & DeptID & "'  AND OrderDetail.DivisionID='" & DivID & "'	AND OrderDetail.OrderNumber='" & OrderNumber & "' AND OrderDetail.ItemID='" & OrderNumber & "'"
            'Dim sqlStr As String = "SELECT Distinct OrderDetail.OrderQty,OrderDetail.ItemID,OrderDetail.Description,OrderDetail.OrderLineNumber,OrderDetail.ItemUOM,OrderDetail.ItemUnitPrice,OrderDetail.ItemCost,OrderDetail.Total,InventoryItems.ItemName FROM OrderDetail Left outer join InventoryItems on OrderDetail.ItemID=InventoryItems.ItemID WHERE	OrderDetail.CompanyID='" & CompanyID & "' AND OrderDetail.DepartmentID='" & DeptID & "'  AND OrderDetail.DivisionID='" & DivID & "'	AND OrderDetail.OrderNumber='" & OrderNumber & "' AND OrderDetail.ItemID='" & OrderNumber & "'"
            Dim sqlStr As String = "SELECT InventoryItems.ItemName, OrderDetail.OrderQty, OrderDetail.ItemID, OrderDetail.Description, OrderDetail.OrderLineNumber,OrderDetail.AddOnsIDsQty, OrderDetail.UpSellPrice, OrderDetail.ItemUOM, OrderDetail.ItemUnitPrice, OrderDetail.ItemCost,ISNULL(OrderDetail.SubTotal, 0) AS SubTotal, ISNULL(OrderDetail.Total, 0) AS Total, ISNULL(OrderDetail.DiscountPerc,0) AS DiscountPerc, ISNULL(OrderDetail.DiscountFlatOrPercent,'%') AS DiscountFlatOrPercent FROM OrderDetail INNER JOIN InventoryItems ON OrderDetail.CompanyID = InventoryItems.CompanyID AND OrderDetail.DivisionID = InventoryItems.DivisionID AND        OrderDetail.DepartmentID = InventoryItems.DepartmentID And OrderDetail.ItemID = InventoryItems.ItemID  WHERE OrderDetail.CompanyID='" & CompanyID & "' AND OrderDetail.DepartmentID='" & DeptID & "'AND OrderDetail.DivisionID='" & DivID & "'AND OrderNumber='" & OrderNumber & "'AND OrderNumber<>'' order by OrderLineNumber asc "
            Dim Cmd As SqlCommand = New SqlCommand()
            Cmd.Connection = ConString
            Cmd.CommandText = sqlStr
            ConString.Open()
            Dim Adapter As New SqlDataAdapter(Cmd)
            Dim ds As New Data.DataSet
            Adapter.Fill(ds)
            ConString.Close()
            Return ds
        End Function
#End Region


        Public Function PopulateItemDetails_SubtotalbyTaxable(ByVal CompanyID As String, ByVal DeptID As String, ByVal DivID As String, ByVal OrderNumber As String) As Data.DataTable

            Dim ConnectionString As String = ""
            ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim sqlStr As String = "SELECT  Sum(ISNULL(OrderDetail.SubTotal, 0)) AS SubTotal  FROM OrderDetail INNER JOIN InventoryItems ON OrderDetail.CompanyID = InventoryItems.CompanyID AND OrderDetail.DivisionID = InventoryItems.DivisionID AND        OrderDetail.DepartmentID = InventoryItems.DepartmentID And OrderDetail.ItemID = InventoryItems.ItemID  WHERE OrderDetail.CompanyID='" & CompanyID & "' AND OrderDetail.DepartmentID='" & DeptID & "'AND OrderDetail.DivisionID='" & DivID & "'   AND isnull(InventoryItems.[Taxable],0)=1       AND OrderNumber='" & OrderNumber & "' "

            Dim Cmd As SqlCommand = New SqlCommand()
            Cmd.Connection = ConString
            Cmd.CommandText = sqlStr
            ConString.Open()
            Dim Adapter As New SqlDataAdapter(Cmd)
            Dim ds As New Data.DataTable
            Adapter.Fill(ds)
            ConString.Close()
            Return ds
        End Function


        Public Function Inventory_GetTotalWithTax(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal ItemID As String, ByVal Quantity As Decimal, ByVal CustomerDiscount As Decimal) As Data.DataSet
            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("[dbo].[Inventory_GetTotalWithTax1]", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterItemID As New SqlParameter("@ItemID", Data.SqlDbType.NVarChar)
            parameterItemID.Value = ItemID
            myCommand.Parameters.Add(parameterItemID)

            Dim parameterQuatity As New SqlParameter("@Quantity", Data.SqlDbType.Float)
            parameterQuatity.Value = Quantity
            myCommand.Parameters.Add(parameterQuatity)

            Dim parameterDiscount As New SqlParameter("@CustomerDiscount", Data.SqlDbType.Float)
            parameterDiscount.Value = CustomerDiscount
            myCommand.Parameters.Add(parameterDiscount)


            myCon.Open()

            Dim Adapter As New SqlDataAdapter(myCommand)
            Dim ds As New Data.DataSet
            Adapter.Fill(ds)
            myCon.Close()
            Return ds

        End Function

        Public Function PopulateSubTotal(ByVal CompanyID As String, ByVal DeptID As String, ByVal DivID As String, ByVal OrderNumber As String) As SqlDataReader


            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("enterprise.GetTotalValues")
            myCommand.CommandType = Data.CommandType.StoredProcedure
            myCommand.Connection = ConString

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DeptID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
            parameterOrderNumber.Value = OrderNumber
            myCommand.Parameters.Add(parameterOrderNumber)

            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs

        End Function


        Public Function PopulatePaymentTypes(ByVal CompanyID As String, ByVal DeptID As String, ByVal DivID As String) As SqlDataReader
            Dim ConnectionString As String = ""

            ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim sqlStr As String = "SELECT * FROM PaymentMethods WHERE CompanyID='" & CompanyID & "' AND DepartmentID='" & DeptID & "' AND DivisionID='" & DivID & "' AND Active=1 AND POSDisplay=1 "
            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            Dim rs As SqlDataReader
            ConString.Open()
            rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)

            Return rs
        End Function

        Public Function PopulateOrderType(ByVal CompanyID As String, ByVal DeptID As String, ByVal DivID As String) As SqlDataReader
            Dim ConnectionString As String = ""

            ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim sqlStr As String = "SELECT * FROM OrderTypes WHERE CompanyID='" & CompanyID & "' AND DepartmentID='" & DeptID & "' AND DivisionID='" & DivID & "'"
            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            Dim rs As SqlDataReader
            ConString.Open()
            rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)

            Return rs
        End Function

        Public Function PopulateEmployee(ByVal CompanyID As String, ByVal DeptID As String, ByVal DivID As String) As SqlDataReader
            Dim ConnectionString As String = ""

            ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim sqlStr As String = "SELECT EmployeeID FROM PayrollEmployees WHERE CompanyID='" & CompanyID & "' AND DepartmentID='" & DeptID & "' AND DivisionID='" & DivID & "'"
            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            Dim rs As SqlDataReader
            ConString.Open()
            rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)

            Return rs
        End Function

        Public Function PopulateCustomers(ByVal CompanyID As String, ByVal DeptID As String, ByVal DivID As String) As SqlDataReader
            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateCustomerNameWithID")
            myCommand.CommandType = Data.CommandType.StoredProcedure
            myCommand.Connection = ConString

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DeptID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim rs As SqlDataReader


            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs
        End Function

        Public Function PopulateCustomerDetails(ByVal CompanyID As String, ByVal DeptID As String, ByVal DivID As String, ByVal CustID As String) As SqlDataReader
            'Dim ConnectionString As String = ""

            'ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
            'Dim ConString As New SqlConnection
            'ConString.ConnectionString = ConnectionString
            'Dim sqlStr As String = "SELECT * FROM CustomerInformation WHERE CompanyID='" & CompanyID & "' AND DepartmentID='" & DeptID & "' AND DivisionID='" & DivID & "' And CustomerID='" & CustID & "'"
            'Dim Cmd As New SqlCommand
            'Cmd.CommandText = sqlStr
            'Cmd.Connection = ConString
            'Dim rs As SqlDataReader
            'ConString.Open()
            'rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)

            'Return rs
            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateCustomerInfo")
            myCommand.CommandType = Data.CommandType.StoredProcedure
            myCommand.Connection = ConString

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DeptID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivID
            myCommand.Parameters.Add(parameterDivisionID)


            Dim parameterCustomerID As New SqlParameter("@CustomerID", Data.SqlDbType.NVarChar, 36)
            parameterCustomerID.Value = CustID
            myCommand.Parameters.Add(parameterCustomerID)
            Dim rs As SqlDataReader


            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs


        End Function

        Public Function PopulateDestinationTypes(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As SqlDataReader


            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateDestinationTypes")
            myCommand.CommandType = Data.CommandType.StoredProcedure
            myCommand.Connection = ConString

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim rs As SqlDataReader


            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs

        End Function
        Public Function PopulateEmployees(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As SqlDataReader

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()
            Dim myCommand As New SqlCommand("PopulateEmployees", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)


            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs



        End Function


        Public Function PopulateCompanyLogo(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As SqlDataReader


            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()
            Dim myCommand As New SqlCommand("enterprise.spCompanyInformation", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs

        End Function

        Public Function PopulateCountries(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As SqlDataReader

            Dim rs As SqlDataReader

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()
            Dim myCommand As New SqlCommand("PopulateCountries", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            Return rs

        End Function

        Public Function GettingFloristCountryName(ByVal CompanyID As String, ByVal DeptID As String, ByVal DivID As String, ByVal EmpID As String) As SqlDataReader
            Dim ConnectionString As String = ""
            Dim RtnCountry = ""
            ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim sqlStr As String = "SELECT EmployeeCountry, EmployeeState FROM PayrollEmployees " _
                                 & "WHERE CompanyID='" & CompanyID & "' AND " _
                                 & " DepartmentID='" & DeptID & "' AND " _
                                 & " DivisionID='" & DivID & "' AND EmployeeId='" & EmpID & "'"
            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            Dim rs As SqlDataReader
            ConString.Open()
            rs = Cmd.ExecuteReader(CommandBehavior.CloseConnection)

            Return rs

        End Function
        Public Function GettingRetailerCountryName(ByVal CompanyID As String, ByVal DeptID As String, ByVal DivID As String) As SqlDataReader
            Dim ConnectionString As String = ""
            Dim RtnCountry = ""
            ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim sqlStr As String = "SELECT CompanyCountry, CompanyState,CompanyCity,Companyzip FROM Companies " _
                                 & "WHERE CompanyID='" & CompanyID & "' AND " _
                                 & " DepartmentID='" & DeptID & "' AND " _
                                 & " DivisionID='" & DivID & "' "
            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            Dim rs As SqlDataReader
            ConString.Open()
            rs = Cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs

        End Function




        Public Function GettingRetailerCountryNameByLocation(ByVal CompanyID As String, ByVal DeptID As String, ByVal DivID As String, ByVal Loc As String) As SqlDataReader
            Dim ConnectionString As String = ""
            Dim RtnCountry = ""
            ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim sqlStr As String = "SELECT Country as 'CompanyCountry', State as 'CompanyState',City as 'CompanyCity',ZipCode As 'Companyzip'  FROM [Order_Location] " _
                                 & "WHERE CompanyID='" & CompanyID & "' AND " _
                                 & " DepartmentID='" & DeptID & "' AND " _
                                 & " DivisionID='" & DivID & "' AND " _
                                 & " locationID='" & Loc & "' "
            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            Dim rs As SqlDataReader
            ConString.Open()
            rs = Cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs

        End Function

        Public Function PopulateStates(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As SqlDataReader

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()
            Dim myCommand As New SqlCommand("PopulateStates", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure
            Dim rs As SqlDataReader
            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID

            myCommand.Parameters.Add(parameterDivisionID)

            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            Return rs

        End Function

        Public Function PopulateOccasionCodes(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As SqlDataReader

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("BindOccasionCodes", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs

        End Function

        Public Function PopulatePriority(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As SqlDataReader


            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()
            Dim myCommand As New SqlCommand("PopulatePriority", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs

        End Function

        'Public Function BindCardMessages(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal OccasionCode As Integer) As DataSet



        '    Dim conString As New SqlConnection
        '    conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")


        '    Dim myCommand As New SqlCommand("BindCardMessages", conString)

        '    myCommand.CommandType = Data.CommandType.StoredProcedure





        '    Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
        '    parameterCompanyID.Value = CompanyID
        '    myCommand.Parameters.Add(parameterCompanyID)


        '    Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
        '    parameterDivisionID.Value = DivisionID
        '    myCommand.Parameters.Add(parameterDivisionID)


        '    Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
        '    parameterDepartmentID.Value = DepartmentID
        '    myCommand.Parameters.Add(parameterDepartmentID)


        '    Dim parameterOccasionCode As New SqlParameter("@OccasionCode", Data.SqlDbType.Int)
        '    parameterOccasionCode.Value = OccasionCode
        '    myCommand.Parameters.Add(parameterOccasionCode)

        '    Dim adapter As New SqlDataAdapter(myCommand)

        '    Dim ds As New DataSet
        '    adapter.Fill(ds)

        '    Return ds


        'End Function

        Public Sub AddCardMessages(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal OccasionCode As Integer, ByVal CardMessageDesc As String)



            Dim conString As New SqlConnection
            conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

            Dim myCommand As New SqlCommand("AddCardMessages", conString)
            myCommand.CommandType = Data.CommandType.StoredProcedure



            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)


            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)


            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)


            Dim parameterOccasionCode As New SqlParameter("@OccasionCode", Data.SqlDbType.Int)
            parameterOccasionCode.Value = OccasionCode
            myCommand.Parameters.Add(parameterOccasionCode)

            Dim parameterCardMessageDesc As New SqlParameter("@CardMessageDesc", Data.SqlDbType.NText)
            parameterCardMessageDesc.Value = CardMessageDesc
            myCommand.Parameters.Add(parameterCardMessageDesc)

            conString.Open()
            myCommand.ExecuteNonQuery()
            conString.Close()


        End Sub
        Public Function PopulateDeliveryMethods(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As SqlDataReader

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()
            Dim myCommand As New SqlCommand("PopulateDeliveryMethods", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs



        End Function

        Public Sub DeleteOrderDetails(ByVal CompanyID As String, ByVal DeptID As String, ByVal DivID As String, ByVal OrderNo As String, ByVal OrderLineNumber As String)
            Dim ConnectionString As String = ""
            ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim SQLStr As String = "Delete FROM OrderDetail WHERE CompanyID='" & CompanyID & "' AND DepartmentID='" & DeptID & "' AND DivisionID='" & DivID & "' AND OrderNumber='" & OrderNo & "' AND OrderLineNumber='" & OrderLineNumber & "'"
            Dim Cmd As New SqlCommand
            Cmd.CommandText = SQLStr
            Cmd.Connection = ConString
            ' Dim rs As SqlDataReader
            ConString.Open()
            Cmd.ExecuteNonQuery()
            ConString.Close()
            'rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)

            ' Return rs
        End Sub
        Public Sub DeleteOrderDetailsFromHeaderList(ByVal CompanyID As String, ByVal DeptID As String, ByVal DivID As String, ByVal OrderNo As String)
            Dim ConnectionString As String = ""
            ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim SQLStr As String = "Delete FROM OrderDetail WHERE CompanyID='" & CompanyID & "' AND DepartmentID='" & DeptID & "' AND DivisionID='" & DivID & "' AND OrderNumber='" & OrderNo & "'"
            Dim Cmd As New SqlCommand
            Cmd.CommandText = SQLStr
            Cmd.Connection = ConString
            ' Dim rs As SqlDataReader
            ConString.Open()
            Cmd.ExecuteNonQuery()
            ConString.Close()
            'rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)

            ' Return rs
        End Sub


        Public Function EditOrderDetails(ByVal CompanyID As String, ByVal DeptID As String, ByVal DivID As String, ByVal OrderNo As String, ByVal LineNumber As String) As SqlDataReader
            Dim ConnectionString As String = ""

            ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim SQLStr As String = "SELECT * FROM OrderDetail WHERE CompanyID='" & CompanyID & "' AND DepartmentID='" & DeptID & "' AND DivisionID='" & DivID & "' AND OrderNumber='" & OrderNo & "' AND OrderLineNumber='" & LineNumber & "'"
            Dim Cmd As New SqlCommand
            Cmd.CommandText = SQLStr
            Cmd.Connection = ConString
            Dim rs As SqlDataReader
            ConString.Open()
            rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)

            Return rs
        End Function


        Public Function BindCardMessages(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal OccasionCode As Integer) As DataSet



            Dim conString As New SqlConnection
            conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")


            Dim myCommand As New SqlCommand("BindCardMessages", conString)

            myCommand.CommandType = Data.CommandType.StoredProcedure





            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)


            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)


            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)


            Dim parameterOccasionCode As New SqlParameter("@OccasionCode", Data.SqlDbType.Int)
            parameterOccasionCode.Value = OccasionCode
            myCommand.Parameters.Add(parameterOccasionCode)

            Dim adapter As New SqlDataAdapter(myCommand)

            Dim ds As New DataSet
            adapter.Fill(ds)
            conString.Close()
            Return ds


        End Function


        Public Function AddCardMessages(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal OccasionCode As Integer, ByVal CardMessageDesc As String, ByVal MacroCode As String) As Integer



            Dim conString As New SqlConnection
            conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

            Dim myCommand As New SqlCommand("AddCardMessages", conString)
            myCommand.CommandType = Data.CommandType.StoredProcedure



            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)


            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)


            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)


            Dim parameterOccasionCode As New SqlParameter("@OccasionCode", Data.SqlDbType.Int)
            parameterOccasionCode.Value = OccasionCode
            myCommand.Parameters.Add(parameterOccasionCode)

            Dim parameterCardMessageDesc As New SqlParameter("@CardMessageDesc", Data.SqlDbType.NText)
            parameterCardMessageDesc.Value = CardMessageDesc
            myCommand.Parameters.Add(parameterCardMessageDesc)

            Dim parameterMacroCode As New SqlParameter("@MacroCode", Data.SqlDbType.NVarChar)
            parameterMacroCode.Value = MacroCode
            myCommand.Parameters.Add(parameterMacroCode)

            Dim paramReturnValue As New SqlParameter("@ReturnValue", Data.SqlDbType.Int)
            paramReturnValue.Direction = ParameterDirection.Output

            myCommand.Parameters.Add(paramReturnValue)

            Dim OutPutValue As Integer


            conString.Open()
            myCommand.ExecuteNonQuery()
            conString.Close()
            OutPutValue = Convert.ToInt32(paramReturnValue.Value)

            Return OutPutValue




        End Function
        Public Function PopulateMessageDesc(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal MessageID As Integer) As SqlDataReader

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateMessageDesc", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterMessageID As New SqlParameter("@MessageID", Data.SqlDbType.Int, 4)
            parameterMessageID.Value = MessageID
            myCommand.Parameters.Add(parameterMessageID)

            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs

        End Function


        Public Function UpdateCardMessages(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal OccasionCode As Integer, ByVal CardMessageDesc As String, ByVal MessageID As Integer, ByVal MacroCode As String) As Integer




            Dim conString As New SqlConnection
            conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

            Dim myCommand As New SqlCommand("UpdateCardMessages", conString)
            myCommand.CommandType = Data.CommandType.StoredProcedure



            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)


            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)


            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)


            Dim parameterOccasionCode As New SqlParameter("@OccasionCode", Data.SqlDbType.Int)
            parameterOccasionCode.Value = OccasionCode
            myCommand.Parameters.Add(parameterOccasionCode)

            Dim parameterCardMessageDesc As New SqlParameter("@CardMessageDesc", Data.SqlDbType.NText)
            parameterCardMessageDesc.Value = CardMessageDesc
            myCommand.Parameters.Add(parameterCardMessageDesc)

            Dim parameterMessageID As New SqlParameter("@MessageID", Data.SqlDbType.Int, 4)
            parameterMessageID.Value = MessageID
            myCommand.Parameters.Add(parameterMessageID)

            Dim parameterMacroCode As New SqlParameter("@MacroCode", Data.SqlDbType.NVarChar)
            parameterMacroCode.Value = MacroCode
            myCommand.Parameters.Add(parameterMacroCode)

            Dim paramReturnValue As New SqlParameter("@ReturnValue", Data.SqlDbType.Int)
            paramReturnValue.Direction = ParameterDirection.Output

            myCommand.Parameters.Add(paramReturnValue)

            Dim OutPutValue As Integer


            conString.Open()
            myCommand.ExecuteNonQuery()
            conString.Close()
            OutPutValue = Convert.ToInt32(paramReturnValue.Value)

            Return OutPutValue




        End Function

        Public Function PopulateMacroCodeList(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As DataSet


            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateMacroCodeList", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)


            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet

            adapter.Fill(ds)
            ConString.Close()
            Return ds


            'Dim rs As SqlDataReader
            'rs = myCommand.ExecuteReader()
            'Return rs

        End Function

        Public Function PopulateCardMessagesList(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal OccasionCode As Integer) As DataSet


            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateCardMessagesList", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterOccasionCode As New SqlParameter("@OccasionCode", Data.SqlDbType.Int, 4)
            parameterOccasionCode.Value = OccasionCode
            myCommand.Parameters.Add(parameterOccasionCode)

            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet

            adapter.Fill(ds)
            ConString.Close()
            Return ds

            'Dim rs As SqlDataReader
            'rs = myCommand.ExecuteReader()
            'Return rs

        End Function


        Public Function PopulateCardMessagetxt(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal MessageID As Integer) As SqlDataReader

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateCardMessagetxt", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterMessageID As New SqlParameter("@MessageID", Data.SqlDbType.Int, 4)
            parameterMessageID.Value = MessageID
            myCommand.Parameters.Add(parameterMessageID)



            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs

        End Function
        Sub DeleteMessageDesc(ByVal MessageId As Integer)


            Dim conString As New SqlConnection
            conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

            Dim myCommand As New SqlCommand("DeleteMessageDesc", conString)
            myCommand.CommandType = Data.CommandType.StoredProcedure


            Dim parameterMessageId As New SqlParameter("@MessageId", Data.SqlDbType.Int)
            parameterMessageId.Value = MessageId
            myCommand.Parameters.Add(parameterMessageId)


            conString.Open()
            myCommand.ExecuteNonQuery()
            conString.Close()



        End Sub

        Public Function PostingOrder(ByVal CompID As String, ByVal DeptID As String, ByVal DivID As String, ByVal OrderID As String) As String
            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("[enterprise].[Order_Post]", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DeptID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
            parameterOrderNumber.Value = OrderID
            myCommand.Parameters.Add(parameterOrderNumber)


            Dim parameterPostingResult As New SqlParameter("@PostingResult", Data.SqlDbType.NVarChar, 200)
            parameterPostingResult.Direction = ParameterDirection.Output
            myCommand.Parameters.Add(parameterPostingResult)
            Dim OutputValue As String = ""
            myCon.Open()

            myCommand.ExecuteNonQuery()

            OutputValue = parameterPostingResult.Value.ToString()

            myCon.Close()
            Return OutputValue

        End Function

        Public Function OrderDelete(ByVal CompID As String, ByVal DeptID As String, ByVal DivID As String, ByVal OrderID As String) As String

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("[enterprise].[Order_DeleteNew]", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DeptID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
            parameterOrderNumber.Value = OrderID
            myCommand.Parameters.Add(parameterOrderNumber)


            Dim parameterPostingResult As New SqlParameter("@ErrorMessage", Data.SqlDbType.NVarChar, 200)
            parameterPostingResult.Direction = ParameterDirection.Output
            myCommand.Parameters.Add(parameterPostingResult)
            Dim OutputValue As String = ""
            myCon.Open()

            myCommand.ExecuteNonQuery()

            OutputValue = parameterPostingResult.Value.ToString()

            myCon.Close()
            Return OutputValue

        End Function

#Region "AddItemDetailsCustomisedGrid"

        Public Function AddItemDetailsCustomisedGrid() As Integer


            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("[dbo].[AddItemDetailsCustomisedGridTemp]", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            'Dim parameterOrderLineNumber As New SqlParameter("@OrderLineNumber ", Data.SqlDbType.Int)
            'parameterOrderLineNumber.Value = OLineNumber
            'myCommand.Parameters.Add(parameterOrderLineNumber)

            Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
            parameterOrderNumber.Value = OrderNumber
            myCommand.Parameters.Add(parameterOrderNumber)

            Dim parameterItemID As New SqlParameter("@ItemID  ", Data.SqlDbType.NVarChar)
            parameterItemID.Value = ItemID
            myCommand.Parameters.Add(parameterItemID)

            Dim parameterDescription As New SqlParameter("@Description   ", Data.SqlDbType.NVarChar)
            parameterDescription.Value = Description
            myCommand.Parameters.Add(parameterDescription)

            Dim parameterOrderQty As New SqlParameter("@OrderQty ", Data.SqlDbType.Float)
            parameterOrderQty.Value = OrderQty
            myCommand.Parameters.Add(parameterOrderQty)

            Dim parameterItemUOM As New SqlParameter("@ItemUOM    ", Data.SqlDbType.NVarChar)
            parameterItemUOM.Value = ItemUOM
            myCommand.Parameters.Add(parameterItemUOM)

            Dim parameterItemUnitPrice As New SqlParameter("@ItemUnitPrice  ", Data.SqlDbType.Money)
            parameterItemUnitPrice.Value = ItemUnitPrice
            myCommand.Parameters.Add(parameterItemUnitPrice)

            Dim parameterTaxGroupID As New SqlParameter("@TaxGroupID     ", Data.SqlDbType.NVarChar)
            parameterTaxGroupID.Value = TaxGroupID
            myCommand.Parameters.Add(parameterTaxGroupID)

            Dim parameterTaxAmount As New SqlParameter("@TaxAmount   ", Data.SqlDbType.Money)
            parameterTaxAmount.Value = TaxAmount
            myCommand.Parameters.Add(parameterTaxAmount)

            Dim parameterSubTotal As New SqlParameter("@Total    ", Data.SqlDbType.Money)
            parameterSubTotal.Value = Total
            myCommand.Parameters.Add(parameterSubTotal)

            Dim parameterTotal As New SqlParameter("@SubTotal    ", Data.SqlDbType.Money)
            parameterTotal.Value = SubTotal
            myCommand.Parameters.Add(parameterTotal)

            Dim parameterItemDiscountPerc As New SqlParameter("@ItemDiscountPerc", Data.SqlDbType.Float)
            parameterItemDiscountPerc.Value = ItemDiscountPerc
            myCommand.Parameters.Add(parameterItemDiscountPerc)


            'JMT code on 11th August 2008 Starts here
            Dim parameterItemDiscountFlatOrPercent As New SqlParameter("@DiscountFlatOrPercentage", Data.SqlDbType.NVarChar)
            parameterItemDiscountFlatOrPercent.Value = ItemDiscountFlatOrPercent
            myCommand.Parameters.Add(parameterItemDiscountFlatOrPercent)
            'JMT code on 11th August 2008 Ends here



            Dim paramReturnValue As New SqlParameter("@ReturnValue", Data.SqlDbType.Int)
            paramReturnValue.Direction = ParameterDirection.Output
            myCommand.Parameters.Add(paramReturnValue)

            myCon.Open()

            myCommand.ExecuteNonQuery()


            Dim OutPutValue As Integer
            OutPutValue = Convert.ToInt32(paramReturnValue.Value)
            myCon.Close()
            Return OutPutValue

        End Function

#End Region

        Public Sub AddSystemMessages(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal Message As String, ByVal EmployeeID As String, ByVal MessageID As Integer)

            Dim conString As New SqlConnection
            conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

            Dim myCommand As New SqlCommand("AddSystemMessages", conString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterEndDate As New SqlParameter("@EndDate", Data.SqlDbType.DateTime)
            parameterEndDate.Value = EndDate
            myCommand.Parameters.Add(parameterEndDate)

            Dim parameterStartDate As New SqlParameter("@StartDate", Data.SqlDbType.DateTime)
            parameterStartDate.Value = StartDate
            myCommand.Parameters.Add(parameterStartDate)

            Dim parameterMessage As New SqlParameter("@Message", Data.SqlDbType.NVarChar)
            parameterMessage.Value = Message
            myCommand.Parameters.Add(parameterMessage)


            Dim parameterEmployeeID As New SqlParameter("@EmployeeID", Data.SqlDbType.NVarChar)
            parameterEmployeeID.Value = EmployeeID
            myCommand.Parameters.Add(parameterEmployeeID)

            Dim parameterMessageID As New SqlParameter("@MessageID", Data.SqlDbType.Int)
            parameterMessageID.Value = MessageID
            myCommand.Parameters.Add(parameterMessageID)

            conString.Open()
            myCommand.ExecuteNonQuery()
            conString.Close()


        End Sub


        Public Function PopulateTransactionTypes(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As SqlDataReader

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()
            Dim myCommand As New SqlCommand("PopulateTransactionTypes", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs



        End Function

        Public Function PopulateMacroValues(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As SqlDataReader
            Dim ConnectionString As String = ""

            ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim sqlStr As String = "SELECT * FROM CardMessages WHERE CompanyID='" & CompanyID & "' AND DepartmentID='" & DepartmentID & "' AND DivisionID='" & DivisionID & "'"
            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            Dim rs As SqlDataReader
            ConString.Open()
            rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)

            Return rs
        End Function




        Public Function BindingSalutations(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As SqlDataReader
            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()
            Dim myCommand As New SqlCommand("PopulateSalutation", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs
        End Function
        Public Function PopulateProjectsName(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String)
            Dim ConnectionString As String = ""

            ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim sqlStr As String = "SELECT ProjectID,ProjectName FROM Projects WHERE CompanyID='" & CompanyID & "' AND DepartmentID='" & DepartmentID & "' AND DivisionID='" & DivisionID & "'"
            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            Dim rs As SqlDataReader
            ConString.Open()
            rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)

            Return rs
        End Function
        Public Function PopulateWarehouseLocations(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionId As String)
            Dim ConnectionString As String = ""

            ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim sqlStr As String = "SELECT WarehouseID,WarehouseName FROM Warehouses WHERE CompanyID='" & CompanyID & "' AND DepartmentID='" & DepartmentID & "' AND DivisionID='" & DivisionId & "'"
            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            Dim rs As SqlDataReader
            ConString.Open()
            rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)

            Return rs
        End Function

#Region "BindOrderHeaderList Function"
        Public Function BindOrderHeaderList(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal Condition As String, ByVal CustomerID As String, ByVal OrderNumber As String, ByVal Total As Decimal, ByVal PaymentMethod As String, ByVal FirstName As String, ByVal LastName As String, ByVal DeliveryMethod As String, ByVal DeliveryDate As String, ByVal FromDate As String, ByVal ToDate As String) As DataSet


            Dim conString As New SqlConnection
            conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

            Dim myCommand As New SqlCommand("BindOrderHeaderList", conString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterCondition As New SqlParameter("@Condition", Data.SqlDbType.NVarChar)
            parameterCondition.Value = Condition
            myCommand.Parameters.Add(parameterCondition)
            Dim parameterFromDate As New SqlParameter("@FromDate", Data.SqlDbType.NVarChar)
            parameterFromDate.Value = FromDate
            myCommand.Parameters.Add(parameterFromDate)

            Dim parameterToDate As New SqlParameter("@ToDate", Data.SqlDbType.NVarChar)
            parameterToDate.Value = ToDate
            myCommand.Parameters.Add(parameterToDate)

            Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
            If OrderNumber = "" Then

                parameterOrderNumber.Value = DBNull.Value


            Else
                parameterOrderNumber.Value = OrderNumber
            End If

            myCommand.Parameters.Add(parameterOrderNumber)


            Dim parameterTotal As New SqlParameter("@Total", Data.SqlDbType.Decimal)
            If Total = 0 Then
                parameterTotal.Value = DBNull.Value

            Else

                parameterTotal.Value = Total
            End If
            'parameterTotal.Value = Total
            myCommand.Parameters.Add(parameterTotal)

            Dim parameterPaymentMethodID As New SqlParameter("@PaymentMethodID", Data.SqlDbType.NVarChar)
            If PaymentMethod = "" Then
                parameterPaymentMethodID.Value = DBNull.Value
            Else
                parameterPaymentMethodID.Value = PaymentMethod
            End If

            myCommand.Parameters.Add(parameterPaymentMethodID)

            Dim parameterCustomerID As New SqlParameter("@CustomerID", Data.SqlDbType.NVarChar)
            If CustomerID = "" Then
                parameterCustomerID.Value = DBNull.Value
            Else
                parameterCustomerID.Value = CustomerID
            End If

            myCommand.Parameters.Add(parameterCustomerID)

            Dim parameterFirstName As New SqlParameter("@CustomerFirstName", Data.SqlDbType.NVarChar)
            If FirstName = "" Then
                parameterFirstName.Value = DBNull.Value
            Else
                parameterFirstName.Value = FirstName
            End If

            myCommand.Parameters.Add(parameterFirstName)

            Dim parameterLastName As New SqlParameter("@CustomerlastName", Data.SqlDbType.NVarChar)
            If LastName = "" Then
                parameterLastName.Value = DBNull.Value
            Else
                parameterLastName.Value = LastName
            End If

            myCommand.Parameters.Add(parameterLastName)

            Dim parameterDeliveryMethod As New SqlParameter("@ShipMethodID", Data.SqlDbType.NVarChar)
            If DeliveryMethod = "" Then
                parameterDeliveryMethod.Value = DBNull.Value
            Else
                parameterDeliveryMethod.Value = DeliveryMethod
            End If

            myCommand.Parameters.Add(parameterDeliveryMethod)


            Dim parameterDeliveryDate As New SqlParameter("@OrderShipDate", Data.SqlDbType.NVarChar)
            If DeliveryDate = "" Then
                parameterDeliveryDate.Value = DBNull.Value
            Else
                parameterDeliveryDate.Value = DeliveryDate
            End If

            myCommand.Parameters.Add(parameterDeliveryDate)

            Dim ds As New DataSet
            Try
                Dim adapter As New SqlDataAdapter(myCommand)


                adapter.Fill(ds)

                Return ds
            Catch ex As Exception
                Return ds
            End Try
            conString.Close()

            'Dim adapter As New SqlDataAdapter(myCommand)

            'Dim ds As New DataSet
            'adapter.Fill(ds)

            'Return ds


        End Function
#End Region

#Region "testDynamicSql"
        Public Function OrderSearchList(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal Condition As String, ByVal fieldName As String, ByVal fieldexpression As String, ByVal FromDate As String, ByVal ToDate As String, ByVal AllDate As Integer) As DataSet


            Dim conString As New SqlConnection
            conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

            Dim myCommand As New SqlCommand("OrderSearchList", conString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterCondition As New SqlParameter("@Condition", Data.SqlDbType.NVarChar)
            parameterCondition.Value = Condition
            myCommand.Parameters.Add(parameterCondition)


            Dim parameterfieldName As New SqlParameter("@fieldName", Data.SqlDbType.NVarChar)
            parameterfieldName.Value = fieldName
            myCommand.Parameters.Add(parameterfieldName)

            Dim parameterfieldexpression As New SqlParameter("@fieldexpression", Data.SqlDbType.NVarChar)
            parameterfieldexpression.Value = fieldexpression
            myCommand.Parameters.Add(parameterfieldexpression)


            Dim parameterFromDate As New SqlParameter("@FromDate", Data.SqlDbType.NVarChar)
            parameterFromDate.Value = FromDate
            myCommand.Parameters.Add(parameterFromDate)

            Dim parameterToDate As New SqlParameter("@ToDate", Data.SqlDbType.NVarChar)
            parameterToDate.Value = ToDate
            myCommand.Parameters.Add(parameterToDate)


            Dim parameterAllDate As New SqlParameter("@AllDate", Data.SqlDbType.Int)
            parameterAllDate.Value = AllDate
            myCommand.Parameters.Add(parameterAllDate)

            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet()
            adapter.Fill(ds)
            conString.Close()

            Return ds


        End Function


        Public Function POSOrderSearchList(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal Condition As String, ByVal fieldName As String, ByVal fieldexpression As String, ByVal FromDate As String, ByVal ToDate As String, ByVal AllDate As Integer, ByVal SortField As String, ByVal SortDirection As String, ByVal Payment As String, ByVal Delivery As String) As DataSet

            Dim conString As New SqlConnection
            conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

            Dim myCommand As New SqlCommand("POSOrderSearchList", conString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim prPayment As New SqlParameter("@Payment", Data.SqlDbType.NVarChar)
            prPayment.Value = Payment
            myCommand.Parameters.Add(prPayment)

            Dim prDelivery As New SqlParameter("@Delivery", Data.SqlDbType.NVarChar)
            prDelivery.Value = Delivery
            myCommand.Parameters.Add(prDelivery)

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterCondition As New SqlParameter("@Condition", Data.SqlDbType.NVarChar)
            parameterCondition.Value = Condition
            myCommand.Parameters.Add(parameterCondition)


            Dim parameterfieldName As New SqlParameter("@fieldName", Data.SqlDbType.NVarChar)
            parameterfieldName.Value = fieldName
            myCommand.Parameters.Add(parameterfieldName)

            Dim parameterfieldexpression As New SqlParameter("@fieldexpression", Data.SqlDbType.NVarChar)
            parameterfieldexpression.Value = fieldexpression
            myCommand.Parameters.Add(parameterfieldexpression)


            Dim parameterFromDate As New SqlParameter("@FromDate", Data.SqlDbType.NVarChar)
            parameterFromDate.Value = FromDate
            myCommand.Parameters.Add(parameterFromDate)

            Dim parameterToDate As New SqlParameter("@ToDate", Data.SqlDbType.NVarChar)
            parameterToDate.Value = ToDate
            myCommand.Parameters.Add(parameterToDate)


            Dim parameterAllDate As New SqlParameter("@AllDate", Data.SqlDbType.Int)
            parameterAllDate.Value = AllDate
            myCommand.Parameters.Add(parameterAllDate)


            Dim parameterSortField As New SqlParameter("@SortField", Data.SqlDbType.NVarChar)
            parameterSortField.Value = SortField
            myCommand.Parameters.Add(parameterSortField)

            Dim parameterSortDirection As New SqlParameter("@SortDirection", Data.SqlDbType.NVarChar)
            parameterSortDirection.Value = SortDirection
            myCommand.Parameters.Add(parameterSortDirection)


            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet()
            adapter.Fill(ds)
            conString.Close()

            Return ds


        End Function


        Public Function PaymentMethodsList(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As DataTable
            Dim connec As New SqlConnection
            connec.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

            Dim ssql As String = ""
            Dim dt As New DataTable()
            ssql = "select * from [PaymentMethods] where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and [Active]=1"
            Dim da As New SqlDataAdapter
            Dim com As SqlCommand
            com = New SqlCommand(ssql, connec)
            Try
                com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = CompanyID
                com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = DepartmentID
                com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = DivisionID
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

        Public Function ShipmentMethodsList(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As DataTable
            Dim connec As New SqlConnection
            connec.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

            Dim ssql As String = ""
            Dim dt As New DataTable()
            ssql = "select * from [ShipmentMethods] where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2"
            Dim da As New SqlDataAdapter
            Dim com As SqlCommand
            com = New SqlCommand(ssql, connec)
            Try
                com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = CompanyID
                com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = DepartmentID
                com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = DivisionID
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


#End Region
#Region "UpdateCardMessagesItemDetails"

        Public Sub UpdateCardMessagesItemDetails(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal OrderNumber As String, ByVal CardMessage As String)


            Dim conString As New SqlConnection
            conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

            Dim myCommand As New SqlCommand("UpdateCardMessagesItemDetails", conString)
            myCommand.CommandType = Data.CommandType.StoredProcedure


            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
            parameterOrderNumber.Value = OrderNumber
            myCommand.Parameters.Add(parameterOrderNumber)

            Dim parameterCardMessage As New SqlParameter("@CardMessage", Data.SqlDbType.NVarChar)
            parameterCardMessage.Value = CardMessage
            myCommand.Parameters.Add(parameterCardMessage)

            conString.Open()
            myCommand.ExecuteNonQuery()
            conString.Close()


        End Sub
#End Region

#Region "AddHeaderDetailsFromItemDetails"
        Public Sub AddHeaderDetailsFromItemDetails()

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("[dbo].[AddEditHeaderDetailSection1]", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
            parameterOrderNumber.Value = OrderNumber
            myCommand.Parameters.Add(parameterOrderNumber)


            Dim parameterTransactionTypeID As New SqlParameter("@TransactionTypeID   ", Data.SqlDbType.NVarChar)
            parameterTransactionTypeID.Value = TransactionTypeID
            myCommand.Parameters.Add(parameterTransactionTypeID)

            Dim parameterOrderTypeID As New SqlParameter("@OrderTypeID", Data.SqlDbType.NVarChar)
            parameterOrderTypeID.Value = OrderTypeID
            myCommand.Parameters.Add(parameterOrderTypeID)

            'Dim parameterOrderDate As New SqlParameter("@OrderDate    ", Data.SqlDbType.DateTime)
            'parameterOrderDate.Value = OrderDate
            'myCommand.Parameters.Add(parameterOrderDate)

            If OrderShipDate = "" Then
                OrderShipDate = DateTime.Now.ToShortDateString()
            End If
            Dim parameterOrderShipDate As New SqlParameter("@OrderShipDate", Data.SqlDbType.DateTime)
            parameterOrderShipDate.Value = Convert.ToDateTime(OrderShipDate)
            myCommand.Parameters.Add(parameterOrderShipDate)

            Dim parameterCustomerID As New SqlParameter("@CustomerID", Data.SqlDbType.NVarChar)
            parameterCustomerID.Value = CustomerID
            myCommand.Parameters.Add(parameterCustomerID)

            Dim parameterEmployeeID As New SqlParameter("@EmployeeID", Data.SqlDbType.NVarChar)
            parameterEmployeeID.Value = EmployeeID
            myCommand.Parameters.Add(parameterEmployeeID)

            Dim parameterShipMethodID As New SqlParameter("@ShipMethodID", Data.SqlDbType.NVarChar)
            parameterShipMethodID.Value = ShipMethodID
            myCommand.Parameters.Add(parameterShipMethodID)

            Dim parameterShippingFirstName As New SqlParameter("@ShippingFirstName", Data.SqlDbType.NVarChar)
            parameterShippingFirstName.Value = ShippingFirstName
            myCommand.Parameters.Add(parameterShippingFirstName)

            Dim parameterShippingLastName As New SqlParameter("@ShippingLastName", Data.SqlDbType.NVarChar)
            parameterShippingLastName.Value = ShippingLastName
            myCommand.Parameters.Add(parameterShippingLastName)

            Dim parameterShippingAddress1 As New SqlParameter("@ShippingAddress1", Data.SqlDbType.NVarChar)
            parameterShippingAddress1.Value = ShippingAddress1
            myCommand.Parameters.Add(parameterShippingAddress1)

            Dim parameterShippingAddress2 As New SqlParameter("@ShippingAddress2", Data.SqlDbType.NVarChar)
            parameterShippingAddress2.Value = ShippingAddress2
            myCommand.Parameters.Add(parameterShippingAddress2)

            Dim parameterShippingAddress3 As New SqlParameter("@ShippingAddress3", Data.SqlDbType.NVarChar)
            parameterShippingAddress3.Value = ShippingAddress3
            myCommand.Parameters.Add(parameterShippingAddress3)

            Dim parameterShippingCity As New SqlParameter("@ShippingCity", Data.SqlDbType.NVarChar)
            parameterShippingCity.Value = ShippingCity
            myCommand.Parameters.Add(parameterShippingCity)

            Dim parameterShippingState As New SqlParameter("@ShippingState", Data.SqlDbType.NVarChar)
            parameterShippingState.Value = ShippingState
            myCommand.Parameters.Add(parameterShippingState)

            Dim parameterShippingZip As New SqlParameter("@ShippingZip", Data.SqlDbType.NVarChar)
            parameterShippingZip.Value = ShippingZip
            myCommand.Parameters.Add(parameterShippingZip)

            Dim parameterShippingCountry As New SqlParameter("@ShippingCountry", Data.SqlDbType.NVarChar)
            parameterShippingCountry.Value = ShippingCountry
            myCommand.Parameters.Add(parameterShippingCountry)

            Dim parameterPaymentMethodID As New SqlParameter("@PaymentMethodID", Data.SqlDbType.NVarChar)
            parameterPaymentMethodID.Value = PaymentMethodID
            myCommand.Parameters.Add(parameterPaymentMethodID)


            Dim parameterCreditCardType As New SqlParameter("@CreditCardType", Data.SqlDbType.NVarChar)
            parameterCreditCardType.Value = CreditCardType
            myCommand.Parameters.Add(parameterCreditCardType)

            Dim parameterCreditCardNumber As New SqlParameter("@CreditCardNumber", Data.SqlDbType.NVarChar)
            parameterCreditCardNumber.Value = CreditCardNumber
            myCommand.Parameters.Add(parameterCreditCardNumber)

            Dim parameterCreditCardExpDate As New SqlParameter("@CreditCardExpDate", Data.SqlDbType.NVarChar)
            parameterCreditCardExpDate.Value = CreditCardExpDate
            myCommand.Parameters.Add(parameterCreditCardExpDate)

            Dim parameterCreditCardCSVNumber As New SqlParameter("@CreditCardCSVNumber", Data.SqlDbType.NVarChar)
            parameterCreditCardCSVNumber.Value = CreditCardCSVNumber
            myCommand.Parameters.Add(parameterCreditCardCSVNumber)

            Dim parameterCreditCardValidationCode As New SqlParameter("@CreditCardValidationCode", Data.SqlDbType.NVarChar)
            parameterCreditCardValidationCode.Value = CreditCardValidationCode
            myCommand.Parameters.Add(parameterCreditCardValidationCode)

            Dim parameterCreditCardApprovalNumber As New SqlParameter("@CreditCardApprovalNumber", Data.SqlDbType.NVarChar)
            parameterCreditCardApprovalNumber.Value = CreditCardApprovalNumber
            myCommand.Parameters.Add(parameterCreditCardApprovalNumber)

            Dim parameterCreditCardBillToZip As New SqlParameter("@CreditCardBillToZip", Data.SqlDbType.NVarChar)
            parameterCreditCardBillToZip.Value = CreditCardBillToZip
            myCommand.Parameters.Add(parameterCreditCardBillToZip)


            Dim parameterShippingAttention As New SqlParameter("@ShippingAttention", Data.SqlDbType.NVarChar)
            parameterShippingAttention.Value = ShippingAttention
            myCommand.Parameters.Add(parameterShippingAttention)


            Dim parameterShippingPhone As New SqlParameter("@ShippingPhone", Data.SqlDbType.NVarChar)
            parameterShippingPhone.Value = ShippingPhone
            myCommand.Parameters.Add(parameterShippingPhone)


            Dim parameterShippingFax As New SqlParameter("@ShippingFax", Data.SqlDbType.NVarChar)
            parameterShippingFax.Value = ShippingFax
            myCommand.Parameters.Add(parameterShippingFax)


            Dim parameterShippingCell As New SqlParameter("@ShippingCell", Data.SqlDbType.NVarChar)
            parameterShippingCell.Value = ShippingCell
            myCommand.Parameters.Add(parameterShippingCell)


            Dim parameterShippingExt As New SqlParameter("@ShippingExt", Data.SqlDbType.NVarChar)
            parameterShippingExt.Value = ShippingExt
            myCommand.Parameters.Add(parameterShippingExt)



            Dim parameterDestinationType As New SqlParameter("@DestinationType", Data.SqlDbType.NVarChar)
            parameterDestinationType.Value = DestinationType
            myCommand.Parameters.Add(parameterDestinationType)

            Dim parameterPriority As New SqlParameter("@Priority", Data.SqlDbType.NVarChar)
            parameterPriority.Value = Priority
            myCommand.Parameters.Add(parameterPriority)

            Dim parameterOccasionCode As New SqlParameter("@OccasionCode", Data.SqlDbType.NVarChar)
            parameterOccasionCode.Value = OccasionCode
            myCommand.Parameters.Add(parameterOccasionCode)


            Dim parameterShippingCompany As New SqlParameter("@ShippingCompany", Data.SqlDbType.NVarChar)
            parameterShippingCompany.Value = ShippingCompany
            myCommand.Parameters.Add(parameterShippingCompany)

            Dim parameterIpAddress As New SqlParameter("@IpAddress", Data.SqlDbType.NVarChar)
            parameterIpAddress.Value = IpAddress
            myCommand.Parameters.Add(parameterIpAddress)

            Dim parameterFraudRating As New SqlParameter("@FraudRating", Data.SqlDbType.NVarChar)
            parameterFraudRating.Value = FraudRating
            myCommand.Parameters.Add(parameterFraudRating)

            Dim parameterShippingSalutation As New SqlParameter("@ShippingSalutation", Data.SqlDbType.NVarChar)
            parameterShippingSalutation.Value = ShippingSalutation
            myCommand.Parameters.Add(parameterShippingSalutation)

            Dim parameterProjectID As New SqlParameter("@ProjectID", Data.SqlDbType.NVarChar)
            parameterProjectID.Value = ProjectID
            myCommand.Parameters.Add(parameterProjectID)

            Dim parameterWarehouseID As New SqlParameter("@WarehouseID", Data.SqlDbType.NVarChar)
            parameterWarehouseID.Value = WarehouseID
            myCommand.Parameters.Add(parameterWarehouseID)


            Dim parameterCheckID As New SqlParameter("@CheckID", Data.SqlDbType.NVarChar)
            parameterCheckID.Value = CheckID
            myCommand.Parameters.Add(parameterCheckID)

            Dim parameterCheckNumber As New SqlParameter("@CheckNumber", Data.SqlDbType.NVarChar)
            parameterCheckNumber.Value = CheckNumber
            myCommand.Parameters.Add(parameterCheckNumber)

            Dim parameterGiftCertificate As New SqlParameter("@GiftCertificate", Data.SqlDbType.NVarChar)
            parameterGiftCertificate.Value = GiftCertificate
            myCommand.Parameters.Add(parameterGiftCertificate)


            Dim parameterCoupon As New SqlParameter("@Coupon", Data.SqlDbType.NVarChar)
            parameterCoupon.Value = Coupon
            myCommand.Parameters.Add(parameterCoupon)

            Dim parameterWireService As New SqlParameter("@WireService", Data.SqlDbType.NVarChar)
            parameterWireService.Value = WireService
            myCommand.Parameters.Add(parameterWireService)


            Dim parameterWireCode As New SqlParameter("@WireCode", Data.SqlDbType.NVarChar)
            parameterWireCode.Value = WireCode
            myCommand.Parameters.Add(parameterWireCode)


            Dim parameterWireRefernceID As New SqlParameter("@WireRefernceID", Data.SqlDbType.NVarChar)
            parameterWireRefernceID.Value = WireRefernceID
            myCommand.Parameters.Add(parameterWireRefernceID)

            Dim parameterWireTransmitMethod As New SqlParameter("@WireTransmitMethod", Data.SqlDbType.NVarChar)
            parameterWireTransmitMethod.Value = WireTransmitMethod
            myCommand.Parameters.Add(parameterWireTransmitMethod)

            Dim parameterInternalNotes As New SqlParameter("@InternalNotes", Data.SqlDbType.NVarChar)
            parameterInternalNotes.Value = InternalNotes
            myCommand.Parameters.Add(parameterInternalNotes)


            Dim parameterDriverRouteInfo As New SqlParameter("@DriverRouteInfo", Data.SqlDbType.NVarChar)
            parameterDriverRouteInfo.Value = DriverRouteInfo
            myCommand.Parameters.Add(parameterDriverRouteInfo)

            myCon.Open()

            myCommand.ExecuteNonQuery()
            myCon.Close()

            'Dim rs As SqlDataReader
            'rs = myCommand.ExecuteReader(Data.CommandBehavior.CloseConnection)
            'Return rs

        End Sub
#End Region

        Public Function BindingTaxes(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As SqlDataReader

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("BindingTaxes", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)


            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs

        End Function


        Public Function PopulateShipAddress(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal CustomerID As String) As DataSet

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateShipAddress", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)


            Dim parameterCustomerID As New SqlParameter("@CustomerID", Data.SqlDbType.NVarChar, 36)
            parameterCustomerID.Value = CustomerID
            myCommand.Parameters.Add(parameterCustomerID)

            Dim adapter As New SqlDataAdapter(myCommand)

            Dim ds As New DataSet

            ConString.Close()
            adapter.Fill(ds)


            Return ds
        
        End Function

#Region "PopulateCommonDeliveryAddress"
        Public Function PopulateCommonDeliveryAddress(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal SearchExpression As String, ByVal FieldName As String, ByVal SortField As String, ByVal SortDirectionSortField As String) As DataSet

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateCommonDeliveryAddress", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterfieldName As New SqlParameter("@fieldName", Data.SqlDbType.NVarChar)
            parameterfieldName.Value = FieldName
            myCommand.Parameters.Add(parameterfieldName)

            Dim parameterfieldexpression As New SqlParameter("@SearchExpression", Data.SqlDbType.NVarChar)
            parameterfieldexpression.Value = SearchExpression
            myCommand.Parameters.Add(parameterfieldexpression)


            ''New code is added for the sorting

            Dim parameterfieldNameSortField As New SqlParameter("@SortField", Data.SqlDbType.NVarChar)
            parameterfieldNameSortField.Value = SortField
            myCommand.Parameters.Add(parameterfieldNameSortField)

            Dim parameterSortDirection As New SqlParameter("@SortDirection", Data.SqlDbType.NVarChar)
            parameterSortDirection.Value = SortDirectionSortField
            myCommand.Parameters.Add(parameterSortDirection)
            ''''''''''''''''''''''''''''''''''''''


            Dim adapter As New SqlDataAdapter(myCommand)

            Dim ds As New DataSet
            adapter.Fill(ds)
            ConString.Close()
            Return ds


        End Function
#End Region

#Region "Authorization"
        Public Function AuthUser(ByVal user As String, ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As String
            Dim _status As String = ""
            Dim _adminID As String = ""

            Dim ConnectionString As String = ""

            ConnectionString = ConfigurationManager.AppSettings("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim sqlStr As String = "SELECT PayrollEmployees.EmployeePassword, PayrollEmployees.ActiveYN, PayrollEmployees.EmployeeID, " _
                                    & "PayrollEmployees.EmployeeName, AccessPermissions.SecurityLevel " _
                                    & "FROM PayrollEmployees INNER JOIN " _
                                    & "AccessPermissions ON PayrollEmployees.CompanyID = AccessPermissions.CompanyID AND " _
                                    & "PayrollEmployees.DivisionID = AccessPermissions.DivisionID AND " _
                                    & "PayrollEmployees.DepartmentID = AccessPermissions.DepartmentID AND " _
                                    & "PayrollEmployees.EmployeeID = AccessPermissions.EmployeeID " _
                                    & "WHERE (PayrollEmployees.EmployeeID = '" & user & "') AND" _
                                    & "(PayrollEmployees.CompanyID = '" & CompanyID & "') AND " _
                                    & "(PayrollEmployees.DivisionID = '" & DivisionID & "') AND " _
                                    & "(PayrollEmployees.DepartmentID = '" & DepartmentID & "') AND (PayrollEmployees.ActiveYN=1)"
            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            ConString.Open()
            Try
                Dim reader As SqlDataReader = Cmd.ExecuteReader()
                If reader.HasRows = True Then
                    _status = "1"
                    While reader.Read()
                        _adminID = reader("EmployeeID").ToString()
                        HttpContext.Current.Session("UserID") = _adminID
                        _status += "," & reader("EmployeeName").ToString() & "," & reader("SecurityLevel").ToString()

                    End While

                End If
            Catch ex As Exception
                _status = "0"
            Finally
                ConString.Close()
                ConString.Dispose()

            End Try

            Return _status

        End Function
#End Region

#Region "SEO"
        Public Function SEOData(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As SqlDataReader
            Dim ConnectionString As String = ""

            ConnectionString = ConfigurationManager.AppSettings("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim sqlStr = "SELECT * FROM SEO WHERE CompanyID='" & CompanyID & "' " _
                                & "AND DivisionID='" & DivisionID & "' " _
                                & "AND DepartmentID='" & DepartmentID & "'"
            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            ConString.Open()
            Return Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)
        End Function

        Public Sub SEOUpdate(ByVal Title As String, ByVal Metatag As String, ByVal Keywords As String, ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String)
            Dim ConnectionString As String = ""

            ConnectionString = ConfigurationManager.AppSettings("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim CCount As Integer = 0
            Dim sqlStr As String = ""
            Dim Cmd As New SqlCommand

            Cmd.Connection = ConString
            ConString.Open()

            sqlStr = "SELECT Count (Title) FROM SEO " _
                                & "WHERE CompanyID='" & CompanyID & "' " _
                                & "AND DivisionID='" & DivisionID & "' " _
                                & "AND DepartmentID='" & DepartmentID & "'"
            Cmd.CommandText = sqlStr
            CCount = Integer.Parse(Cmd.ExecuteScalar().ToString())
            If CCount > 0 Then


                sqlStr = "UPDATE SEO SET Title='" & Title & "', " _
                                    & "Metatag='" & Metatag & "', " _
                                    & "Keywords='" & Keywords & "' " _
                                    & "WHERE CompanyID='" & CompanyID & "' " _
                                    & "AND DivisionID='" & DivisionID & "' " _
                                    & "AND DepartmentID='" & DepartmentID & "'"
            Else
                sqlStr = "INSERT INTO SEO (CompanyID,DivisionID,DepartmentID,Title,Metatag,Keywords) " _
                            & "VALUES ('" & CompanyID & "','" & DivisionID & "','" _
                            & DepartmentID & "','" & Title & "','" & Metatag & "','" & Keywords & "')"

            End If
            Cmd.CommandText = sqlStr
            Cmd.ExecuteNonQuery()
            ConString.Close()
        End Sub
#End Region

#Region "Layout Graphics"
        Public Function StoreLayout(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal OccasionID As String, ByVal GraphicFileName As String, ByVal LayoutName As String) As Integer

            'StoreLayouts

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("[dbo].[StoreLayouts]", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)


            Dim parameterOccasionID As New SqlParameter("@OccasionCode", Data.SqlDbType.Int)
            parameterOccasionID.Value = OccasionID
            myCommand.Parameters.Add(parameterOccasionID)

            Dim parameterGraphicFileName As New SqlParameter("@GraphicFileName  ", Data.SqlDbType.NVarChar)
            parameterGraphicFileName.Value = GraphicFileName
            myCommand.Parameters.Add(parameterGraphicFileName)

            Dim parameterLayoutName As New SqlParameter("@LayoutName   ", Data.SqlDbType.NVarChar)
            parameterLayoutName.Value = LayoutName
            myCommand.Parameters.Add(parameterLayoutName)


            Dim paramReturnValue As New SqlParameter("@ReturnValue", Data.SqlDbType.Int)
            paramReturnValue.Direction = ParameterDirection.Output
            myCommand.Parameters.Add(paramReturnValue)

            myCon.Open()

            myCommand.ExecuteNonQuery()


            Dim OutPutValue As Integer
            OutPutValue = Convert.ToInt32(paramReturnValue.Value)
            myCon.Close()
            Return OutPutValue


        End Function

        Public Sub StoreGraphics(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal LayoutID As Integer, ByVal GraphicsName As String)
            Dim ConnectionString As String = ""

            ConnectionString = ConfigurationManager.AppSettings("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim sqlStr = "INSERT INTO PrimaryGraphics (CompanyID,DivisionID,DepartmentID,ImageName,LayoutID) " _
                         & "VALUES ('" & CompanyID & "','" & DivisionID & "','" & DepartmentID & "','" _
                         & GraphicsName & "'," & LayoutID & ")"


            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            ConString.Open()
            Cmd.ExecuteNonQuery()
            ConString.Close()
        End Sub

        Public Function CheckDesignName(ByVal DesName As String) As Integer
            Dim ConnectionString As String = ""
            Dim status As Integer = 0
            ConnectionString = ConfigurationManager.AppSettings("ConnectionString")
            Dim ConString As New SqlConnection
            Dim dr As SqlDataReader
            ConString.ConnectionString = ConnectionString
            Dim sqlStr = "SELECT * FROM Layouts WHERE DesignName='" & DesName & "'"


            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            ConString.Open()
            dr = Cmd.ExecuteReader()
            If dr.HasRows Then
                status = 0
            Else
                status = 1
            End If
            ConString.Close()
            Return status
        End Function

        Public Function BindMainLayoutGrid(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As DataSet


            Dim conString As New SqlConnection
            conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")
            conString.Open()

            Dim myCommand As New SqlCommand("BindMainLayout", conString)

            myCommand.CommandType = Data.CommandType.StoredProcedure
            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)


            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)


            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)



            Dim adapter As New SqlDataAdapter(myCommand)

            Dim ds As New DataSet
            adapter.Fill(ds)
            conString.Close()
            Return ds


        End Function

        Public Function BindPrimaryLayoutGrid(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal LayoutID As Integer) As DataSet


            Dim conString As New SqlConnection
            conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")
            conString.Open()

            Dim myCommand As New SqlCommand("BindPrimaryImages", conString)

            myCommand.CommandType = Data.CommandType.StoredProcedure
            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)


            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)


            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterLayoutID As New SqlParameter("@LayoutID", Data.SqlDbType.NVarChar)
            parameterLayoutID.Value = LayoutID
            myCommand.Parameters.Add(parameterLayoutID)



            Dim adapter As New SqlDataAdapter(myCommand)

            Dim ds As New DataSet
            adapter.Fill(ds)
            conString.Close()
            Return ds


        End Function

        Public Function GetLayoutName(ByVal LayoutID As Integer) As String
            Dim ConnectionString As String = ""
            Dim status As String = ""
            ConnectionString = ConfigurationManager.AppSettings("ConnectionString")
            Dim ConString As New SqlConnection
            Dim dr As SqlDataReader
            ConString.ConnectionString = ConnectionString
            Dim sqlStr = "SELECT * FROM Layouts WHERE LayoutID=" & LayoutID


            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            ConString.Open()
            dr = Cmd.ExecuteReader()
            If dr.HasRows Then
                While dr.Read()
                    status = dr("DesignName").ToString()
                End While

            Else
                status = ""
            End If
            dr.Close()
            Dim Cmd1 As New SqlCommand
            sqlStr = "DELETE FROM PrimaryGraphics WHERE LayoutId=" & LayoutID
            Cmd1.Connection = ConString
            Cmd1.CommandText = sqlStr
            Cmd1.ExecuteNonQuery()

            Dim Cmd2 As New SqlCommand
            sqlStr = "DELETE FROM Layouts WHERE LayoutId=" & LayoutID
            Cmd2.Connection = ConString
            Cmd2.CommandText = sqlStr
            Cmd2.ExecuteNonQuery()

            ConString.Close()
            Return status
        End Function

        Public Sub DeletePrimaryGraph(ByVal LayoutID As Integer, ByVal PrID As Integer)
            Dim ConnectionString As String = ""
            Dim status As String = ""
            ConnectionString = ConfigurationManager.AppSettings("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim sqlStr = "Delete  FROM PrimaryGraphics WHERE LayoutID=" & LayoutID & " AND GraphicsID=" & PrID

            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            ConString.Open()
            Cmd.ExecuteNonQuery()
            ConString.Close()
        End Sub

#End Region

#Region "HomePageTemplageActivation"
        Public Function TemplatesOccasionName(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As SqlDataReader

            Dim ConnectionString As String = ""
            Dim status As String = ""
            ConnectionString = ConfigurationManager.AppSettings("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim sqlStr = "SELECT TemplateName,CSSID FROM CSSValues WHERE CompanyID='" & CompanyID & "' " _
                            & "AND DivisionID='" & DivisionID & "' " _
                            & "AND DepartmentID='" & DepartmentID & "'"
            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            ConString.Open()
            Return (Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection))

        End Function
#End Region

#Region "Setting Active Template"
        Public Sub SetActiveTemplate(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal TemplateID As Integer)
            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("[dbo].[SetActiveTemplate]", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)


            Dim parameterTemplateID As New SqlParameter("@TemplateID", Data.SqlDbType.Int)
            parameterTemplateID.Value = TemplateID
            myCommand.Parameters.Add(parameterTemplateID)


            myCon.Open()

            myCommand.ExecuteNonQuery()
            myCon.Close()

        End Sub
#End Region

        Public Function PopulateIntenalNotes(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal EmpID As String) As SqlDataReader

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()
            Dim myCommand As New SqlCommand("PopulateInternalNotes", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)


            Dim parameterEmployeeID As New SqlParameter("@EmployeeID", Data.SqlDbType.NVarChar, 36)
            parameterEmployeeID.Value = EmpID
            myCommand.Parameters.Add(parameterEmployeeID)

            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(Data.CommandBehavior.CloseConnection)
            Return rs

        End Function

#Region "PopulateItemsSearch"
        Public Function PopulateItemsSearch(ByVal CompanyID As String, ByVal DeptID As String, ByVal DivID As String) As Data.DataSet

            Dim ConnectionString As String = ""
            ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            'Dim sqlStr As String = "SELECT OrderQty,ItemID,Description,OrderLineNumber,ItemUOM,ItemUnitPrice,ItemCost,Total FROM OrderDetail WHERE CompanyID='" & CompanyID & "' AND DepartmentID='" & DeptID & "' AND DivisionID='" & DivID & "'AND OrderNumber='" & OrderNumber & "'"
            Dim sqlStr As String = "SELECT * FROM InventoryItems WHERE CompanyID='" & CompanyID & "' AND DepartmentID='" & DeptID & "' AND DivisionID='" & DivID & "'  "

            'Dim sqlStr As String = "SELECT Distinct OrderDetail.OrderQty,OrderDetail.ItemID,OrderDetail.Description,OrderDetail.OrderLineNumber,OrderDetail.ItemUOM,OrderDetail.ItemUnitPrice,OrderDetail.ItemCost,OrderDetail.Total,InventoryItems.ItemName FROM OrderDetail Left outer join InventoryItems on OrderDetail.ItemID=InventoryItems.ItemID WHERE	OrderDetail.CompanyID='" & CompanyID & "' AND OrderDetail.DepartmentID='" & DeptID & "'  AND OrderDetail.DivisionID='" & DivID & "'	AND OrderDetail.OrderNumber='" & OrderNumber & "' AND OrderDetail.ItemID='" & OrderNumber & "'"
            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            ConString.Open()
            Dim Adapter As New SqlDataAdapter(Cmd)
            Dim ds As New Data.DataSet
            Adapter.Fill(ds)
            ConString.Close()
            Return ds
        End Function
#End Region

#Region "AddEditItemDetailsDetailsSearch"
        Public Sub AddEditItemDetailsSearch()

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("dbo.[AddEditItemDetailsSearch]", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterOrderLineNumber As New SqlParameter("@OrderLineNumber ", Data.SqlDbType.NVarChar)
            parameterOrderLineNumber.Value = OrderLineNumber
            myCommand.Parameters.Add(parameterOrderLineNumber)

            Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
            parameterOrderNumber.Value = OrderNumber
            myCommand.Parameters.Add(parameterOrderNumber)

            Dim parameterItemID As New SqlParameter("@ItemID  ", Data.SqlDbType.NVarChar)
            parameterItemID.Value = ItemID
            myCommand.Parameters.Add(parameterItemID)

            Dim parameterDescription As New SqlParameter("@Description   ", Data.SqlDbType.NVarChar)
            parameterDescription.Value = Description
            myCommand.Parameters.Add(parameterDescription)

            Dim parameterOrderQty As New SqlParameter("@OrderQty ", Data.SqlDbType.Float)
            parameterOrderQty.Value = OrderQty
            myCommand.Parameters.Add(parameterOrderQty)

            Dim parameterItemUnitPrice As New SqlParameter("@ItemUnitPrice  ", Data.SqlDbType.Money)
            parameterItemUnitPrice.Value = ItemUnitPrice
            myCommand.Parameters.Add(parameterItemUnitPrice)

            Dim parameterSubTotal As New SqlParameter("@SubTotal    ", Data.SqlDbType.Money)
            parameterSubTotal.Value = SubTotal
            myCommand.Parameters.Add(parameterSubTotal)

            Dim parameterTotal As New SqlParameter("@Total    ", Data.SqlDbType.Money)
            parameterTotal.Value = Total
            myCommand.Parameters.Add(parameterTotal)

            myCon.Open()

            myCommand.ExecuteNonQuery()
            myCon.Close()
            'Dim rs As SqlDataReader
            'rs = myCommand.ExecuteReader(Data.CommandBehavior.CloseConnection)

        End Sub
#End Region
        Public Function PopulateChangeHistory(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal OrderNumber As String) As DataSet


            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateChangeHistory", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)


            Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar, 36)
            parameterOrderNumber.Value = OrderNumber
            myCommand.Parameters.Add(parameterOrderNumber)


            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet

            adapter.Fill(ds)
            ConString.Close()

            Return ds

            'Dim rs As SqlDataReader
            'rs = myCommand.ExecuteReader()
            'Return rs

        End Function

#Region "CheckCustomerIDExists"

        Public Function CheckCustomerIDExists(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal CustomerID As String) As Integer

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("[dbo].[CheckCustomerIDExists]", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterCustomerID As New SqlParameter("@CustomerID", Data.SqlDbType.NVarChar)
            parameterCustomerID.Value = CustomerID
            myCommand.Parameters.Add(parameterCustomerID)

            Dim paramReturnValue As New SqlParameter("@ReturnValue", Data.SqlDbType.Int)
            paramReturnValue.Direction = ParameterDirection.Output
            myCommand.Parameters.Add(paramReturnValue)

            myCon.Open()
            myCommand.ExecuteNonQuery()

            Dim OutPutValue As Integer
            OutPutValue = Convert.ToInt32(paramReturnValue.Value)
            myCon.Close()
            Return OutPutValue

        End Function


#End Region

#Region "GetNextCustomerID"
        Public Function GetNextCustomerID(ByVal CompanyID As String, ByVal DeptID As String, ByVal DivID As String) As SqlDataReader
            Dim ConnectionString As String = ""

            ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim sqlStr As String = "SELECT Count(CustomerID)as CustomerID from customerinformation where 	CompanyID='" & CompanyID & "' and 	DivisionID='" & DivID & "' and	DepartmentID='" & DeptID & "' "

            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            Dim rs As SqlDataReader
            ConString.Open()
            rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)
            Return rs
        End Function
#End Region

#Region "GetCustomerID"
        Public Function GetCustomerID(ByVal CompanyID As String, ByVal DeptID As String, ByVal DivID As String, ByVal NewCustID As String) As Integer
            Dim ConnectionString As String = ""
            ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim CustCount As Integer = 0
            Dim sqlStr As String = "SELECT Count(CustomerID) from customerinformation where CustomerID='" & NewCustID & "' AND	CompanyID='" & CompanyID & "' and 	DivisionID='" & DivID & "' and	DepartmentID='" & DeptID & "' "
            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            Dim rs As SqlDataReader
            ConString.Open()
            rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)
            While rs.Read()
                Dim str As String = rs(0).ToString()
                If str = "0" Then
                    CustCount = 0
                Else
                    CustCount = 1
                End If
            End While
            Return CustCount
        End Function
#End Region

        Public Function BindingTaxPercent(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal TaxID As String) As SqlDataReader

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("BindingTaxPercent", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterTaxID As New SqlParameter("@TaxID", Data.SqlDbType.NVarChar, 36)
            parameterTaxID.Value = TaxID
            myCommand.Parameters.Add(parameterTaxID)


            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs

        End Function

        Public Function PopulateWireServiceTransmissionMethod(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As SqlDataReader

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()
            Dim myCommand As New SqlCommand("PopulateWireServiceTransmissionMethod", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs



        End Function

        Public Function PopulateWireServicesStatus(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As SqlDataReader

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()
            Dim myCommand As New SqlCommand("PopulateWireServicesStatus", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs



        End Function

        Public Function PopulateWireServicesPriority(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As SqlDataReader

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()
            Dim myCommand As New SqlCommand("PopulateWireServicesPriority", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs



        End Function

        Public Function PopulateWireServices(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As DataSet

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()
            Dim myCommand As New SqlCommand("PopulateWireServices", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            'Dim rs As SqlDataReader
            'rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            'Return rs

            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet

            adapter.Fill(ds)
            ConString.Close()

            Return ds

        End Function


#Region "Occasion for Rotation Schedule"
        Public Function RotationOccasionName(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As SqlDataReader
            Dim ConnectionString As String = ""
            ConnectionString = ConfigurationManager.AppSettings("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim sqlStr As String = "SELECT * FROM InventoryCategories WHERE " _
                                    & " CompanyID='" & CompanyID & "'" _
                                    & " AND DivisionID='" & DivisionID & "'" _
                                    & " AND DepartmentID='" & DepartmentID & "'"
            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            ConString.Open()
            Return Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)

        End Function
#End Region

#Region "Layout Design Number"
        Public Function RotationLayoutDesignNo(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As SqlDataReader
            Dim ConnectionString As String = ""
            ConnectionString = ConfigurationManager.AppSettings("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim sqlStr As String = "SELECT DISTINCT DesignTypeNo FROM CSSValues WHERE " _
                                    & " CompanyID='" & CompanyID & "'" _
                                    & " AND DivisionID='" & DivisionID & "'" _
                                    & " AND DepartmentID='" & DepartmentID & "'"
            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            ConString.Open()
            Return Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)

        End Function
#End Region

#Region "Template Name according to Design Layout Number"
        Public Function RotationTemplateNames(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal DesignID As String) As SqlDataReader
            Dim ConnectionString As String = ""
            ConnectionString = ConfigurationManager.AppSettings("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim sqlStr As String = "SELECT CSSID,TemplateName FROM CSSValues WHERE " _
                                    & " CompanyID='" & CompanyID & "'" _
                                    & " AND DivisionID='" & DivisionID & "'" _
                                    & " AND DepartmentID='" & DepartmentID & "'" _
                                    & " AND DesignTypeNo='" & DesignID & "'"
            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            ConString.Open()
            Return Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)

        End Function
#End Region

#Region "Rotation Schedule Storing"
        Public Sub RotationScheduleStore(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal OccasionID As String, ByVal CSSID As String, ByVal StartDate As String, ByVal EndDate As String, ByVal AutoRotaiton As Integer)
            Dim ConnectionString As String = ""
            ConnectionString = ConfigurationManager.AppSettings("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim SQLstr As String = ""
            Dim Cmd As New SqlCommand
            Dim StDate As Date
            Dim EdDate As Date
            Cmd.Connection = ConString
            ConString.Open()

            If StartDate <> "" And EndDate <> "" Then
                StDate = Convert.ToDateTime(StartDate)
                EdDate = Convert.ToDateTime(EndDate)
                SQLstr = "INSERT INTO RotationSchedule " _
                            & " (CompanyID,DivisionID,DepartmentID,OccasionID,CSSID,StartDate,EndDate) " _
                            & " VALUES " _
                            & " ('" & CompanyID & "','" & DivisionID & "','" & DepartmentID & "','" _
                            & OccasionID & "','" & CSSID & "','" & StDate & "','" & EdDate & "')"

                Cmd.CommandText = SQLstr


                Cmd.ExecuteNonQuery()
            End If

            If StartDate <> "" And EndDate = "" Then
                EndDate = StartDate
                StDate = Convert.ToDateTime(StartDate)
                EdDate = Convert.ToDateTime(EndDate)
                SQLstr = "INSERT INTO RotationSchedule " _
                            & " (CompanyID,DivisionID,DepartmentID,OccasionID,CSSID,StartDate,EndDate) " _
                            & " VALUES " _
                            & " ('" & CompanyID & "','" & DivisionID & "','" & DepartmentID & "','" _
                            & OccasionID & "','" & CSSID & "','" & StDate & "','" & EdDate & "')"

                Cmd.CommandText = SQLstr


                Cmd.ExecuteNonQuery()
            End If

            If StartDate = "" And EndDate <> "" Then
                StartDate = EndDate
                StDate = Convert.ToDateTime(StartDate)
                EdDate = Convert.ToDateTime(EndDate)
                SQLstr = "INSERT INTO RotationSchedule " _
                            & " (CompanyID,DivisionID,DepartmentID,OccasionID,CSSID,StartDate,EndDate) " _
                            & " VALUES " _
                            & " ('" & CompanyID & "','" & DivisionID & "','" & DepartmentID & "','" _
                            & OccasionID & "','" & CSSID & "','" & StDate & "','" & EdDate & "')"

                Cmd.CommandText = SQLstr


                Cmd.ExecuteNonQuery()
            End If


            SQLstr = "UPDATE HomePageManagement SET AutomaticHomePageRotation=" & AutoRotaiton _
                    & " WHERE CompanyID='" & CompanyID & "' AND " _
                    & " DivisionID='" & DivisionID & "' AND " _
                    & " DepartmentID='" & DepartmentID & "'"

            Cmd.CommandText = SQLstr
            Cmd.ExecuteNonQuery()

            ConString.Close()


        End Sub
#End Region

#Region "Binding Rotation Schedule Grid"
        Public Function BindRotationSchedule(ByVal CompanyID, ByVal DivisionID, ByVal DepartmentID) As DataSet
            Dim ConnectionString As String = ""
            Dim status As String = ""
            ConnectionString = ConfigurationManager.AppSettings("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim sqlStr = "SELECT RotationSchedule.OccasionID, CSSValues.TemplateName,CSSValues.DesignTypeNo," _
                        & "RotationSchedule.StartDate, RotationSchedule.EndDate, RotationSchedule.RotationID " _
                        & "FROM RotationSchedule INNER JOIN " _
                        & "CSSValues ON RotationSchedule.CompanyID = CSSValues.CompanyID AND " _
                        & "RotationSchedule.DivisionID = CSSValues.DivisionID AND " _
                        & "RotationSchedule.DepartmentID = CSSValues.DepartmentID AND " _
                        & "RotationSchedule.CSSID = CSSValues.CSSID " _
                        & "WHERE " _
                        & "RotationSchedule.CompanyID='" & CompanyID & "' AND RotationSchedule.DivisionID='" & DivisionID & "' " _
                        & "AND RotationSchedule.DepartmentID='" & DepartmentID & "' " _
                        & "ORDER BY EndDate"

            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            ConString.Open()
            Dim Adapter As New SqlDataAdapter(Cmd)
            Dim ds As New Data.DataSet
            Adapter.Fill(ds)
            ConString.Close()
            Return ds
        End Function
        
#End Region

#Region "Deleting Rotation Schedule"
        Public Sub DeleteRotationSchedule(ByVal RotationID As Integer)
            Dim ConnectionString As String = ""
            ConnectionString = ConfigurationManager.AppSettings("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim SQLstr As String = "DELETE FROM RotationSchedule WHERE RotationID=" & RotationID
            Dim Cmd As New SqlCommand
            Cmd.CommandText = SQLstr
            Cmd.Connection = ConString
            ConString.Open()
            Cmd.ExecuteNonQuery()
            ConString.Close()
        End Sub

#End Region

#Region "Home Page Management Setup"
        Public Sub SetupHomePage(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal FrontEndURL As String, ByVal TemplateID As Integer, ByVal LogoDisplay As Integer, ByVal MaxProd As Integer, ByVal NewsLetterLinkDisplay As Integer, ByVal ContentShow As Integer, ByVal BestSellerShow As Integer, ByVal LocalWeatherShow As Integer, ByVal TermsShow As Integer, ByVal EmploymentShow As Integer, ByVal WeddingShow As Integer, ByVal CorporateShow As Integer, ByVal NewsletterShow As Integer, ByVal FaxOrderShow As Integer, ByVal SecureURL As String, ByVal AllowDeliveryToday As Integer, ByVal ShowDeliveryMethod As Integer, ByVal ActiveCart As Integer, ByVal ActivePOS As Integer, ByVal ActiveCRM As Integer, ByVal ActiveBudgeting As Integer, ByVal ActiveHelpDesk As Integer, ByVal ZipCode As Integer, ByVal RegistrationOptional As Integer, ByVal FaxUserName As String, ByVal FaxPassword As String, ByVal FaxServer As String, ByVal SessionTime As Integer, ByVal Livechat As Integer, ByVal CurrencyType As String, ByVal PriceMenu1 As Integer, ByVal PriceMenu2 As Integer, ByVal PriceMenu3 As Integer, ByVal PriceMenu4 As Integer, ByVal ActivePriceMenu As Integer)

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("[enterprise].[HomePageSetup]", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterFrontEndURL As New SqlParameter("@FrontEndURL", Data.SqlDbType.NVarChar)
            parameterFrontEndURL.Value = FrontEndURL
            myCommand.Parameters.Add(parameterFrontEndURL)

            Dim parameterSecureURL As New SqlParameter("@SecureURL", Data.SqlDbType.NVarChar)
            parameterSecureURL.Value = SecureURL
            myCommand.Parameters.Add(parameterSecureURL)

            Dim parameterTemplateID As New SqlParameter("@TemplateID", Data.SqlDbType.Int)
            parameterTemplateID.Value = TemplateID
            myCommand.Parameters.Add(parameterTemplateID)

            Dim parameterLogoDisplay As New SqlParameter("@LogoDisplay", Data.SqlDbType.Int)
            parameterLogoDisplay.Value = LogoDisplay
            myCommand.Parameters.Add(parameterLogoDisplay)

            Dim parameterMaxProd As New SqlParameter("@MaxProd", Data.SqlDbType.Int)
            parameterMaxProd.Value = MaxProd
            myCommand.Parameters.Add(parameterMaxProd)

            Dim parameterNewsLetterLinkDisplay As New SqlParameter("@NewsLetterLinkDisplay", Data.SqlDbType.Int)
            parameterNewsLetterLinkDisplay.Value = NewsLetterLinkDisplay
            myCommand.Parameters.Add(parameterNewsLetterLinkDisplay)

            Dim parameterContentShow As New SqlParameter("@ContentShow", Data.SqlDbType.Int)
            parameterContentShow.Value = ContentShow
            myCommand.Parameters.Add(parameterContentShow)

            Dim parameterBestSellerShow As New SqlParameter("@BestSellerShow", Data.SqlDbType.Int)
            parameterBestSellerShow.Value = BestSellerShow
            myCommand.Parameters.Add(parameterBestSellerShow)

            Dim parameterLocalWeatherShow As New SqlParameter("@LocalWeatherShow", Data.SqlDbType.Int)
            parameterLocalWeatherShow.Value = LocalWeatherShow
            myCommand.Parameters.Add(parameterLocalWeatherShow)

            Dim parameterTermsShow As New SqlParameter("@TermsShow", Data.SqlDbType.Int)
            parameterTermsShow.Value = TermsShow
            myCommand.Parameters.Add(parameterTermsShow)

            Dim parameterEmploymentShow As New SqlParameter("@EmploymentShow", Data.SqlDbType.Int)
            parameterEmploymentShow.Value = EmploymentShow
            myCommand.Parameters.Add(parameterEmploymentShow)

            Dim parameterWeddingShow As New SqlParameter("@WeddingShow", Data.SqlDbType.Int)
            parameterWeddingShow.Value = EmploymentShow
            myCommand.Parameters.Add(parameterWeddingShow)

            Dim parameterCorporateShow As New SqlParameter("@CorporateShow", Data.SqlDbType.Int)
            parameterCorporateShow.Value = CorporateShow
            myCommand.Parameters.Add(parameterCorporateShow)

            Dim parameterNewsletterShow As New SqlParameter("@NewsletterShow", Data.SqlDbType.Int)
            parameterNewsletterShow.Value = CorporateShow
            myCommand.Parameters.Add(parameterNewsletterShow)

            Dim parameterFaxOrderShow As New SqlParameter("@FaxOrderShow", Data.SqlDbType.Int)
            parameterFaxOrderShow.Value = CorporateShow
            myCommand.Parameters.Add(parameterFaxOrderShow)


            Dim parameterAllowDeliveryToday As New SqlParameter("@AllowDeliveryToday", Data.SqlDbType.Int)
            parameterAllowDeliveryToday.Value = AllowDeliveryToday
            myCommand.Parameters.Add(parameterAllowDeliveryToday)


            Dim parameterShowDeliveryMethod As New SqlParameter("@ShowDeliveryMethod", Data.SqlDbType.Int)
            parameterShowDeliveryMethod.Value = ShowDeliveryMethod
            myCommand.Parameters.Add(parameterShowDeliveryMethod)




            Dim parameterActiveCart As New SqlParameter("@ActiveCart", Data.SqlDbType.Int)
            parameterActiveCart.Value = ActiveCart
            myCommand.Parameters.Add(parameterActiveCart)



            Dim parameterActivePOS As New SqlParameter("@ActivePOS", Data.SqlDbType.Int)
            parameterActivePOS.Value = ActivePOS
            myCommand.Parameters.Add(parameterActivePOS)



            Dim parameterActiveCRM As New SqlParameter("@ActiveCRM", Data.SqlDbType.Int)
            parameterActiveCRM.Value = ActiveCRM
            myCommand.Parameters.Add(parameterActiveCRM)



            Dim parameterActiveHelpDesk As New SqlParameter("@ActiveHelpDesk", Data.SqlDbType.Int)
            parameterActiveHelpDesk.Value = ActiveHelpDesk
            myCommand.Parameters.Add(parameterActiveHelpDesk)



            Dim parameterActiveBudgeting As New SqlParameter("@ActiveBudgeting", Data.SqlDbType.Int)
            parameterActiveBudgeting.Value = ActiveBudgeting
            myCommand.Parameters.Add(parameterActiveBudgeting)


            Dim parameterZipCode As New SqlParameter("@ZipCode", Data.SqlDbType.Int)
            parameterZipCode.Value = ZipCode
            myCommand.Parameters.Add(parameterZipCode)

            Dim parameterRegistrationOptional As New SqlParameter("@RegistrationOptional", Data.SqlDbType.Int)
            parameterRegistrationOptional.Value = RegistrationOptional
            myCommand.Parameters.Add(parameterRegistrationOptional)

            Dim parameterFaxUserName As New SqlParameter("@FaxUserName", Data.SqlDbType.NVarChar)
            parameterFaxUserName.Value = FaxUserName
            myCommand.Parameters.Add(parameterFaxUserName)


            Dim parameterFaxPassword As New SqlParameter("@FaxPassword", Data.SqlDbType.NVarChar)
            parameterFaxPassword.Value = FaxPassword
            myCommand.Parameters.Add(parameterFaxPassword)


            Dim parameterFaxServer As New SqlParameter("@FaxServer", Data.SqlDbType.NVarChar)
            parameterFaxServer.Value = FaxServer
            myCommand.Parameters.Add(parameterFaxServer)


            Dim parameterLiveChat As New SqlParameter("@Livechat", Data.SqlDbType.Int)
            parameterLiveChat.Value = Livechat
            myCommand.Parameters.Add(parameterLiveChat)

            Dim parameterSessionTime As New SqlParameter("@SessionTime", Data.SqlDbType.Int)
            parameterSessionTime.Value = SessionTime
            myCommand.Parameters.Add(parameterSessionTime)



            Dim parameterCurrecnyType As New SqlParameter("@CurrecnyType", Data.SqlDbType.NVarChar)
            parameterCurrecnyType.Value = CurrencyType
            myCommand.Parameters.Add(parameterCurrecnyType)







            Dim parameterPriceMenu1 As New SqlParameter("@PriceMenu1", Data.SqlDbType.Int)
            parameterPriceMenu1.Value = PriceMenu1
            myCommand.Parameters.Add(parameterPriceMenu1)



            Dim parameterPriceMenu2 As New SqlParameter("@PriceMenu2", Data.SqlDbType.Int)
            parameterPriceMenu2.Value = PriceMenu2
            myCommand.Parameters.Add(parameterPriceMenu2)



            Dim parameterPriceMenu3 As New SqlParameter("@PriceMenu3", Data.SqlDbType.Int)
            parameterPriceMenu3.Value = PriceMenu3
            myCommand.Parameters.Add(parameterPriceMenu3)




            Dim parameterPriceMenu4 As New SqlParameter("@PriceMenu4", Data.SqlDbType.Int)
            parameterPriceMenu4.Value = PriceMenu4
            myCommand.Parameters.Add(parameterPriceMenu4)



            Dim parameterActivePriceMenu As New SqlParameter("@ActivePriceMenu", Data.SqlDbType.Int)
            parameterActivePriceMenu.Value = ActivePriceMenu
            myCommand.Parameters.Add(parameterActivePriceMenu)




            myCon.Open()
            myCommand.ExecuteNonQuery()
            myCon.Close()

        End Sub
#End Region

#Region "Retreiving Home Page Setup Value"
        Public Function GetHomePageSetupValues(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As SqlDataReader
            Dim ConnectionString As String = ""
            ConnectionString = ConfigurationManager.AppSettings("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim SQLstr As String = "SELECT * FROM HomePageManagement WHERE " _
                                    & "CompanyID='" & CompanyID & "' AND " _
                                    & "DivisionID='" & DivisionID & "' AND " _
                                    & "DepartmentID='" & DepartmentID & "'"
            Dim Cmd As New SqlCommand
            Cmd.CommandText = SQLstr
            Cmd.Connection = ConString
            ConString.Open()
            Return Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)
        End Function
#End Region

#Region " Check For Email Address"
        Public Function MailAddressSearch(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal EmployeeID As String) As SqlDataReader
            Dim ConnectionString As String = ""
            ConnectionString = ConfigurationManager.AppSettings("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim sqlStr As String = "select EmployeeEmailAddress, EmployeePassword from payrollemployees WHERE " _
                                    & " CompanyID='" & CompanyID & "'" _
                                    & " AND DivisionID='" & DivisionID & "'" _
                                    & " AND DepartmentID='" & DepartmentID & "'" _
                                    & " AND EmployeeID='" & EmployeeID & "'"
            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            Dim rs As SqlDataReader
            ConString.Open()
            rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)
            Return rs

        End Function
#End Region

#Region "Populate companies"
        Public Function PopulateCompanies() As SqlDataReader
            Dim ConnectionString As String = ""
            ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim sqlStr As String = "SELECT Distinct CompanyID,CompanyName FROM Companies Order by CompanyName  "
            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            Dim rs As SqlDataReader
            ConString.Open()
            rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)
            Return rs
        End Function
#End Region

#Region "Populate Divisions"
        Public Function PopulateDivisions() As SqlDataReader
            Dim ConnectionString As String = ""
            ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim sqlStr As String = "SELECT Distinct DivisionID FROM Divisions"
            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            Dim rs As SqlDataReader
            ConString.Open()
            rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)
            Return rs
        End Function
#End Region

#Region "Populate Departments"
        Public Function PopulateDepartment() As SqlDataReader
            Dim ConnectionString As String = ""
            ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim sqlStr As String = "SELECT Distinct DepartmentID FROM Departments"
            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            Dim rs As SqlDataReader
            ConString.Open()
            rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)
            Return rs
        End Function
#End Region

        Public Function BindDeliveryByZip(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As DataSet


            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("BindDeliveryByZip", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)


            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet

            adapter.Fill(ds)
            ConString.Close()
            Return ds


            'Dim rs As SqlDataReader
            'rs = myCommand.ExecuteReader()
            'Return rs

        End Function
 




        Public Function InsertZipCodeDetails(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal Zip As String, ByVal EveryDayRate As String, ByVal HolidayRate As String, ByVal AfterCutOffRate As String, ByVal CourierRate As String, ByVal CutOffTime As String, ByVal TimeZone As String, ByVal DST As String, ByVal Weekday As String, ByVal Saturday As String, ByVal Sunday As String, ByVal Holiday As String, ByVal Block As String, ByVal PrintPoolTicket As String) As Integer

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("InsertZipCodeDetails", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterZip As New SqlParameter("@Zip", Data.SqlDbType.NVarChar)
            parameterZip.Value = Zip
            myCommand.Parameters.Add(parameterZip)



            Dim parameterEveryDayRate As New SqlParameter("@EveryDayRate", Data.SqlDbType.NVarChar)
            parameterEveryDayRate.Value = EveryDayRate
            myCommand.Parameters.Add(parameterEveryDayRate)

            Dim parameterHolidayRate As New SqlParameter("@HolidayRate", Data.SqlDbType.NVarChar)
            parameterHolidayRate.Value = HolidayRate
            myCommand.Parameters.Add(parameterHolidayRate)

            Dim parameterAfterCutOffRate As New SqlParameter("@AfterCutOffRate ", Data.SqlDbType.NVarChar)
            parameterAfterCutOffRate.Value = AfterCutOffRate
            myCommand.Parameters.Add(parameterAfterCutOffRate)

            Dim parameterCourierRate As New SqlParameter("@CourierRate ", Data.SqlDbType.NVarChar)
            parameterCourierRate.Value = CourierRate
            myCommand.Parameters.Add(parameterCourierRate)

            Dim parameterCutOffTime As New SqlParameter("@CutOffTime ", Data.SqlDbType.NVarChar)
            parameterCutOffTime.Value = CutOffTime
            myCommand.Parameters.Add(parameterCutOffTime)
            Dim parameterTimeZone As New SqlParameter("@TimeZone ", Data.SqlDbType.NVarChar)
            parameterTimeZone.Value = TimeZone
            myCommand.Parameters.Add(parameterTimeZone)

            Dim parameterDST As New SqlParameter("@DST ", Data.SqlDbType.NVarChar)
            parameterDST.Value = DST
            myCommand.Parameters.Add(parameterDST)

            Dim parameterWeekday As New SqlParameter("@Weekday ", Data.SqlDbType.NVarChar)
            parameterWeekday.Value = Weekday
            myCommand.Parameters.Add(parameterWeekday)

            Dim parameterSaturday As New SqlParameter("@Saturday ", Data.SqlDbType.NVarChar)
            parameterSaturday.Value = Saturday
            myCommand.Parameters.Add(parameterSaturday)

            Dim parameterSunday As New SqlParameter("@Sunday ", Data.SqlDbType.NVarChar)
            parameterSunday.Value = Sunday
            myCommand.Parameters.Add(parameterSunday)

            Dim parameterHoliday As New SqlParameter("@Holiday ", Data.SqlDbType.NVarChar)
            parameterHoliday.Value = Holiday
            myCommand.Parameters.Add(parameterHoliday)

            Dim parameterBlock As New SqlParameter("@Block ", Data.SqlDbType.NVarChar)
            parameterBlock.Value = Block
            myCommand.Parameters.Add(parameterBlock)

            Dim parameterPrintPoolTicket As New SqlParameter("@PrintPoolTicket ", Data.SqlDbType.NVarChar)
            parameterPrintPoolTicket.Value = PrintPoolTicket
            myCommand.Parameters.Add(parameterPrintPoolTicket)

            Dim paramReturnValue As New SqlParameter("@ReturnValue", Data.SqlDbType.Int)
            paramReturnValue.Direction = ParameterDirection.Output

            myCommand.Parameters.Add(paramReturnValue)

            Dim OutPutValue As Integer
            myCon.Open()

            myCommand.ExecuteNonQuery()

            myCon.Close()

            OutPutValue = Convert.ToInt32(paramReturnValue.Value)

            Return OutPutValue

        End Function



        Public Function PopulateZip() As DataSet


            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateZip", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

          


            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet

            adapter.Fill(ds)
            ConString.Close()
            Return ds


            'Dim rs As SqlDataReader
            'rs = myCommand.ExecuteReader()
            'Return rs

        End Function

        Public Sub DeleteZipCodeDetails(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal Zip As String)

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("DeleteZipCodeDetails", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterZip As New SqlParameter("@Zip", Data.SqlDbType.NVarChar)
            parameterZip.Value = Zip
            myCommand.Parameters.Add(parameterZip)




            myCon.Open()

            myCommand.ExecuteNonQuery()
            myCon.Close()

        End Sub
 


        Public Function AddDeliveryCharges(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal localDeliveryCharge As String, ByVal ServiceCharge As String, ByVal DefaultIntDelCharge As String, ByVal InternationalCharge As String, ByVal DeliveryTaxable As Integer, ByVal ServiceTaxable As Integer, ByVal DefaultIntlTaxable As Integer, ByVal IntlTaxable As Integer, ByVal WireCharge As String, ByVal WireTaxable As Integer) As Integer

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("AddDeliveryCharges", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterlocalDeliveryCharge As New SqlParameter("@localDeliveryCharge", Data.SqlDbType.NVarChar)
            parameterlocalDeliveryCharge.Value = localDeliveryCharge
            myCommand.Parameters.Add(parameterlocalDeliveryCharge)

            Dim parameterServiceCharge As New SqlParameter("@ServiceCharge ", Data.SqlDbType.NVarChar)
            parameterServiceCharge.Value = ServiceCharge
            myCommand.Parameters.Add(parameterServiceCharge)

            Dim parameterDefaultIntDelCharge As New SqlParameter("@DefaultIntDelCharge", Data.SqlDbType.NVarChar)
            parameterDefaultIntDelCharge.Value = DefaultIntDelCharge
            myCommand.Parameters.Add(parameterDefaultIntDelCharge)

            Dim parameterInternationalCharge As New SqlParameter("@InternationalCharge ", Data.SqlDbType.NVarChar)
            parameterInternationalCharge.Value = InternationalCharge
            myCommand.Parameters.Add(parameterInternationalCharge)


            Dim parameterDeliveryTaxable As New SqlParameter("@DeliveryTaxable ", Data.SqlDbType.Int)
            parameterDeliveryTaxable.Value = DeliveryTaxable
            myCommand.Parameters.Add(parameterDeliveryTaxable)

            Dim parameterServiceTaxable As New SqlParameter("@ServiceTaxable ", Data.SqlDbType.Int)
            parameterServiceTaxable.Value = ServiceTaxable
            myCommand.Parameters.Add(parameterServiceTaxable)

            Dim parameterDefaultIntlTaxable As New SqlParameter("@DefaultIntlTaxable ", Data.SqlDbType.Int)
            parameterDefaultIntlTaxable.Value = DefaultIntlTaxable
            myCommand.Parameters.Add(parameterDefaultIntlTaxable)

            Dim parameterIntlTaxable As New SqlParameter("@IntlTaxable ", Data.SqlDbType.Int)
            parameterIntlTaxable.Value = IntlTaxable
            myCommand.Parameters.Add(parameterIntlTaxable)

            Dim parameterWireTaxable As New SqlParameter("@WireTaxable ", Data.SqlDbType.Int)
            parameterWireTaxable.Value = WireTaxable
            myCommand.Parameters.Add(parameterWireTaxable)


            Dim parameterWireCharge As New SqlParameter("@WireCharge", Data.SqlDbType.NVarChar)
            parameterWireCharge.Value = WireCharge
            myCommand.Parameters.Add(parameterWireCharge)


            Dim paramReturnValue As New SqlParameter("@ReturnValue", Data.SqlDbType.Int)
            paramReturnValue.Direction = ParameterDirection.Output

            myCommand.Parameters.Add(paramReturnValue)

            Dim OutPutValue As Integer

            myCon.Open()

            myCommand.ExecuteNonQuery()

            myCon.Close()

            OutPutValue = Convert.ToInt32(paramReturnValue.Value)

            Return OutPutValue

        End Function


        Public Function PopulateDeliveryCharges(ByVal CompanyID, ByVal DivisionID, ByVal DepartmentID) As SqlDataReader


            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateDeliveryCharge", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)


            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs



        End Function
        Public Function PopulateDeliveryChargeByZip(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal Zip As String) As DataTable

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()
            Dim myCommand As New SqlCommand("POSDeliveryChargebyzip", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterZip As New SqlParameter("@Zip", Data.SqlDbType.NVarChar)
            parameterZip.Value = Zip
            myCommand.Parameters.Add(parameterZip)


            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New Datatable

            adapter.Fill(ds)
            ConString.Close()
            Return ds



        End Function


        Public Function DeliveryChargeByUSZipCode(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal Zip As String) As Decimal

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()
            Dim myCommand As New SqlCommand("DeliveryChargeByUSZipCode", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterZip As New SqlParameter("@Zip", Data.SqlDbType.NVarChar)
            parameterZip.Value = Zip
            myCommand.Parameters.Add(parameterZip)


            Dim paramRelayCharges As New SqlParameter("@RelayCharges", Data.SqlDbType.Money)
            paramRelayCharges.Direction = ParameterDirection.Output
            myCommand.Parameters.Add(paramRelayCharges)



            Dim RelayValue As Decimal



            myCommand.ExecuteNonQuery()

            ConString.Close()
            If paramRelayCharges.Value.ToString() <> "" Then
                RelayValue = Convert.ToDecimal(paramRelayCharges.Value)
            End If


            Return RelayValue



        End Function
        Public Function PopulateServiceRelayCharges(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As SqlDataReader

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()
            Dim myCommand As New SqlCommand("PopulateServiceRelayCharges", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs



        End Function


        Public Function BindDefaultDeliveryCharges(ByVal CompanyID, ByVal DivisionID, ByVal DepartmentID) As SqlDataReader


            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateDeliveryCharge", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)


            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs



        End Function


        

        Public Function PopulateSystemMessage(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal CurrDate As String) As DataSet


            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateSystemMessage", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)



            Dim parameterCurrDate As New SqlParameter("@CurrDate", Data.SqlDbType.NVarChar, 36)
            parameterCurrDate.Value = CurrDate
            myCommand.Parameters.Add(parameterCurrDate)

            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet

            adapter.Fill(ds)
            ConString.Close()
            Return ds


            'Dim rs As SqlDataReader
            'rs = myCommand.ExecuteReader()
            'Return rs

        End Function


        Public Sub DeleteSystemMessage(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal MessageID As Integer)

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("DeleteSystemMessage", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterMessageID As New SqlParameter("@MessageID", Data.SqlDbType.Int)
            parameterMessageID.Value = MessageID
            myCommand.Parameters.Add(parameterMessageID)




            myCon.Open()

            myCommand.ExecuteNonQuery()
            myCon.Close()

        End Sub

        Public Function PopulateSystemWideMessage(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal MessageID As Integer) As SqlDataReader


            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateSystemWideMessage", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterMessageID As New SqlParameter("@MessageID", Data.SqlDbType.Int)
            parameterMessageID.Value = MessageID
            myCommand.Parameters.Add(parameterMessageID)

            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs



        End Function

        Public Function GeTCompanyUrL(ByVal CompanyID, ByVal DivisionID, ByVal DepartmentID) As SqlDataReader


            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("GeTCompanyUrL", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)


            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs



        End Function



        Public Function CheckTaxable(ByVal CompanyID, ByVal DivisionID, ByVal DepartmentID) As SqlDataReader


            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("CheckTaxable", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)


            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs



        End Function

        Public Function AddFkeySetup(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal Fkey As String, ByVal Action As String) As Integer



            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("AddFkeySetup", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)



            Dim parameterFkey As New SqlParameter("@Fkey", Data.SqlDbType.NVarChar, 36)
            parameterFkey.Value = Fkey
            myCommand.Parameters.Add(parameterFkey)


            Dim parameterAction As New SqlParameter("@Action", Data.SqlDbType.NVarChar, 36)
            parameterAction.Value = Action
            myCommand.Parameters.Add(parameterAction)


            Dim paramReturnValue As New SqlParameter("@ReturnValue", Data.SqlDbType.Int)
            paramReturnValue.Direction = ParameterDirection.Output
            myCommand.Parameters.Add(paramReturnValue)







            myCommand.ExecuteNonQuery()



            Dim res As Integer


 
            ConString.Close()
            If paramReturnValue.Value.ToString() <> "" Then
                res = Convert.ToDecimal(paramReturnValue.Value)
            End If


            Return res 




        End Function




        Public Function PopulateFkeys(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As DataSet


            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateFkeys", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)


 

            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet

            adapter.Fill(ds)
            ConString.Close()
            Return ds

 

        End Function



        Public Sub DeleteFkey(ByVal FkeyID As Integer)

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("DeleteFkey", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure


            Dim parameterMessageID As New SqlParameter("@FkeyID", Data.SqlDbType.Int)
            parameterMessageID.Value = FkeyID
            myCommand.Parameters.Add(parameterMessageID)




            myCon.Open()

            myCommand.ExecuteNonQuery()
            myCon.Close()

        End Sub

        Public Function PopulateFunctionalkeys(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As SqlDataReader


            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateFkeys", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)




            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs



        End Function

        Public Sub AddStatementOptions(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal PrintStatement As Integer, ByVal FlatFeePercentage As Integer, ByVal PrintHeader As Integer, ByVal PrintDeliquent As Integer, ByVal ApplyLateCharge As Integer, ByVal PrintStatementMessage As Integer, ByVal Delinquent30dayMessage As String, ByVal Delinquent60dayMessage As String, ByVal Delinquent90dayMessage As String, ByVal Delinquent120dayMessage As String, ByVal StatementCycle As String, ByVal InvoiceCopies As String, ByVal StatementMessage As String, ByVal LateChargeAmount As Decimal, ByVal AnnualChargeAmount As Decimal)

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("AddStatementOptions", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterPrintStatement As New SqlParameter("@PrintStatement ", Data.SqlDbType.Int)
            parameterPrintStatement.Value = PrintStatement
            myCommand.Parameters.Add(parameterPrintStatement)

            Dim parameterFlatFeePercentage As New SqlParameter("@FlatFeePercentage", Data.SqlDbType.Int)
            parameterFlatFeePercentage.Value = FlatFeePercentage
            myCommand.Parameters.Add(parameterFlatFeePercentage)

            Dim parameterPrintHeader As New SqlParameter("@PrintHeader ", Data.SqlDbType.Int)
            parameterPrintHeader.Value = PrintHeader
            myCommand.Parameters.Add(parameterPrintHeader)

            Dim parameterPrintDeliquent As New SqlParameter("@PrintDeliquent ", Data.SqlDbType.Int)
            parameterPrintDeliquent.Value = PrintDeliquent
            myCommand.Parameters.Add(parameterPrintDeliquent)

            Dim parameterApplyLateCharge As New SqlParameter("@ApplyLateCharge ", Data.SqlDbType.Int)
            parameterApplyLateCharge.Value = ApplyLateCharge
            myCommand.Parameters.Add(parameterApplyLateCharge)

            Dim parameterPrintStatementMessage As New SqlParameter("@PrintStatementMessage ", Data.SqlDbType.Int)
            parameterPrintStatementMessage.Value = PrintStatementMessage
            myCommand.Parameters.Add(parameterPrintStatementMessage)


            Dim parameterDelinquent30dayMessage As New SqlParameter("@Delinquent30dayMessage ", Data.SqlDbType.NVarChar)
            parameterDelinquent30dayMessage.Value = Delinquent30dayMessage
            myCommand.Parameters.Add(parameterDelinquent30dayMessage)



            Dim parameterDelinquent60dayMessage As New SqlParameter("@Delinquent60dayMessage ", Data.SqlDbType.NVarChar)
            parameterDelinquent60dayMessage.Value = Delinquent60dayMessage
            myCommand.Parameters.Add(parameterDelinquent60dayMessage)




            Dim parameterDelinquent90dayMessage As New SqlParameter("@Delinquent90dayMessage ", Data.SqlDbType.NVarChar)
            parameterDelinquent90dayMessage.Value = Delinquent90dayMessage
            myCommand.Parameters.Add(parameterDelinquent90dayMessage)



            Dim parameterDelinquent120dayMessage As New SqlParameter("@Delinquent120dayMessage ", Data.SqlDbType.NVarChar)
            parameterDelinquent120dayMessage.Value = Delinquent120dayMessage
            myCommand.Parameters.Add(parameterDelinquent120dayMessage)


            Dim parameterStatementCycle As New SqlParameter("@StatementCycle ", Data.SqlDbType.NVarChar)
            parameterStatementCycle.Value = StatementCycle
            myCommand.Parameters.Add(parameterStatementCycle)

            Dim parameterInvoiceCopies As New SqlParameter("@InvoiceCopies ", Data.SqlDbType.NVarChar)
            parameterInvoiceCopies.Value = InvoiceCopies
            myCommand.Parameters.Add(parameterInvoiceCopies)

            Dim parameterStatementMessage As New SqlParameter("@StatementMessage ", Data.SqlDbType.NVarChar)
            parameterStatementMessage.Value = StatementMessage
            myCommand.Parameters.Add(parameterStatementMessage)



            Dim parameterLateChargeAmount As New SqlParameter("@LateChargeAmount ", Data.SqlDbType.Money)
            parameterLateChargeAmount.Value = LateChargeAmount
            myCommand.Parameters.Add(parameterLateChargeAmount)


            Dim parameterAnnualChargeAmount As New SqlParameter("@AnnualChargeAmount ", Data.SqlDbType.Money)
            parameterAnnualChargeAmount.Value = AnnualChargeAmount
            myCommand.Parameters.Add(parameterAnnualChargeAmount)

            myCommand.ExecuteNonQuery()
            ConString.Close()


        End Sub
 


        Public Function PopulateStatementOptions(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As SqlDataReader


            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateStatementOptions", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)




            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs



        End Function




        Public Function PopulateDefaultWireCharge(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As SqlDataReader

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateDefaultWireCharge", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)




            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs



        End Function




        Public Function AddOrderSourceCode(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal OrderSourceCodeID As String, ByVal OrderSourceCodeDesc As String, ByVal OrderForm As Integer, ByVal WebForm As Integer, ByVal SID As String) As Integer



            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("AddOrderSourceCode", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)



            Dim parameterOrderSourceCodeID As New SqlParameter("@OrderSourceCodeID", Data.SqlDbType.NVarChar, 36)
            parameterOrderSourceCodeID.Value = OrderSourceCodeID
            myCommand.Parameters.Add(parameterOrderSourceCodeID)


            Dim parameterOrderSourceCodeDesc As New SqlParameter("@OrderSourceCodeDesc", Data.SqlDbType.NVarChar, 36)
            parameterOrderSourceCodeDesc.Value = OrderSourceCodeDesc
            myCommand.Parameters.Add(parameterOrderSourceCodeDesc)

            Dim parameterSID As New SqlParameter("@SID", Data.SqlDbType.NVarChar, 36)
            parameterSID.Value = SID
            myCommand.Parameters.Add(parameterSID)

            Dim parameterOrderForm As New SqlParameter("@OrderForm", Data.SqlDbType.Int)
            parameterOrderForm.Value = OrderForm
            myCommand.Parameters.Add(parameterOrderForm)


            Dim parameterWebForm As New SqlParameter("@WebForm", Data.SqlDbType.Int)
            parameterWebForm.Value = WebForm
            myCommand.Parameters.Add(parameterWebForm)

            Dim paramReturnValue As New SqlParameter("@ReturnValue", Data.SqlDbType.Int)
            paramReturnValue.Direction = ParameterDirection.Output
            myCommand.Parameters.Add(paramReturnValue)

            myCommand.ExecuteNonQuery()

            Dim res As Integer

            ConString.Close()
            If paramReturnValue.Value.ToString() <> "" Then
                res = Convert.ToDecimal(paramReturnValue.Value)
            End If

            Return res

        End Function



        Public Function BindOrderSourceCode(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As DataSet


            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("BindOrderSourceCode", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet

            adapter.Fill(ds)
            ConString.Close()
            Return ds

        End Function



        Public Function PopulateOrderSourceCode(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal SourceCodeID As String) As SqlDataReader


            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateOrderSourceCode", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)


            Dim parameterSourceCodeID As New SqlParameter("@SourceCodeID", Data.SqlDbType.NVarChar, 36)
            parameterSourceCodeID.Value = SourceCodeID
            myCommand.Parameters.Add(parameterSourceCodeID)
 
            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs

 
        End Function
 
        Public Sub DeleteOrderSourceCode(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal SourceCodeID As String)

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("DeleteOrderSourceCode", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterSourceCodeID As New SqlParameter("@SourceCodeID", Data.SqlDbType.NVarChar)
            parameterSourceCodeID.Value = SourceCodeID
            myCommand.Parameters.Add(parameterSourceCodeID)

 
            myCon.Open()

            myCommand.ExecuteNonQuery()
            myCon.Close()

        End Sub



        Public Function PopulateOrderSourceCodeID(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As SqlDataReader

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateOrderSourceCodeID", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs



        End Function


        Public Sub InventoryItemImagesUpload(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal ItemID As String, ByVal SmallImageName As String, ByVal MediumImageName As String, ByVal LargeImageName As String)

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("InventoryItemImagesUpload", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterItemID As New SqlParameter("@ItemID", Data.SqlDbType.NVarChar)
            parameterItemID.Value = ItemID
            myCommand.Parameters.Add(parameterItemID)

            Dim parameterSmallImageName As New SqlParameter("@SmallImageName", Data.SqlDbType.NVarChar)
            parameterSmallImageName.Value = SmallImageName
            myCommand.Parameters.Add(parameterSmallImageName)

            Dim parameterMediumImageName As New SqlParameter("@MediumImageName", Data.SqlDbType.NVarChar)
            parameterMediumImageName.Value = MediumImageName
            myCommand.Parameters.Add(parameterMediumImageName)

            Dim parameterLargeImageName As New SqlParameter("@LargeImageName", Data.SqlDbType.NVarChar)
            parameterLargeImageName.Value = LargeImageName
            myCommand.Parameters.Add(parameterLargeImageName)


            myCon.Open()

            myCommand.ExecuteNonQuery()
            myCon.Close()

        End Sub



        Public Function PopulateInventoryItemImages(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As DataSet


            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateInventoryItemImages", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)
 
            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet

            adapter.Fill(ds)
            ConString.Close()
            Return ds

        End Function

#Region "Populate Credit Card Type List"

        Public Function BindCreditCardTypes(ByVal CompanyID As String, ByVal DeptID As String, ByVal DivID As String) As Data.DataSet
            Dim ConnectionString As String = ""
            ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim sqlStr As String = "SELECT CreditCardTypeID,CreditCardDescription,Accept,Active,VoicePhone,MerchantID,FraudNo  FROM CreditCardTypes where CompanyID='" & CompanyID & "' AND DepartmentID='" & DeptID & "'AND DivisionID='" & DivID & "'"
            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            ConString.Open()
            Dim Adapter As New SqlDataAdapter(Cmd)
            Dim ds As New Data.DataSet
            Adapter.Fill(ds)
            ConString.Close()
            Return ds
        End Function
#End Region

#Region "Populate Credit Card Details"

        Public Function GetCreditCardDetails(ByVal CompanyID As String, ByVal DeptID As String, ByVal DivID As String, ByVal CreditCardTypeID As String) As SqlDataReader
            Dim ConnectionString As String = ""
            ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim sqlStr As String = "SELECT CreditCardTypeID,CreditCardDescription,Accept,Active,VoicePhone,MerchantID,FraudNo  FROM CreditCardTypes where CompanyID='" & CompanyID & "' AND DepartmentID='" & DeptID & "'AND DivisionID='" & DivID & "'AND CreditCardTypeID='" & CreditCardTypeID & "'"
            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            ConString.Open()
            Dim rs As SqlDataReader
            rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)
            Return rs

        End Function
#End Region

#Region "ADD/Edit Credit Card Details"

        Public Function AddEditCreditCardDetails(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal CreditCardTypeID As String) As Integer
            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("AddEditCreditCardDetails", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterCreditCardTypeID As New SqlParameter("@CreditCardTypeID", Data.SqlDbType.NVarChar)
            parameterCreditCardTypeID.Value = CreditCardTypeID
            myCommand.Parameters.Add(parameterCreditCardTypeID)

            Dim parameterCreditCardDescription As New SqlParameter("@CreditCardDescription", Data.SqlDbType.NVarChar)
            parameterCreditCardDescription.Value = CreditCardDescription
            myCommand.Parameters.Add(parameterCreditCardDescription)

            Dim parameterAccept As New SqlParameter("@Accept", Data.SqlDbType.Bit)
            parameterAccept.Value = CrediCardAccept
            myCommand.Parameters.Add(parameterAccept)

            Dim parameterActive As New SqlParameter("@Active", Data.SqlDbType.Bit)
            parameterActive.Value = CreditCardActive
            myCommand.Parameters.Add(parameterActive)

            Dim parameterVoicePhone As New SqlParameter("@VoicePhone", Data.SqlDbType.NVarChar)
            parameterVoicePhone.Value = CreditCardVoicePhone
            myCommand.Parameters.Add(parameterVoicePhone)

            Dim parameterMerchantID As New SqlParameter("@MerchantID", Data.SqlDbType.NVarChar)
            parameterMerchantID.Value = CreditCardMerchantID
            myCommand.Parameters.Add(parameterMerchantID)

            Dim parameterFraudNo As New SqlParameter("@FraudNo", Data.SqlDbType.NVarChar)
            parameterFraudNo.Value = CreditCardFraudNo
            myCommand.Parameters.Add(parameterFraudNo)

            Dim parameterAddEdit As New SqlParameter("@AddEdit", Data.SqlDbType.Int)
            parameterAddEdit.Value = AddEdit
            myCommand.Parameters.Add(parameterAddEdit)

            Dim paramReturnValue As New SqlParameter("@ReturnValue", Data.SqlDbType.Int)
            paramReturnValue.Direction = ParameterDirection.Output
            myCommand.Parameters.Add(paramReturnValue)

            myCon.Open()

            myCommand.ExecuteNonQuery()
            myCon.Close()

            Dim OutPutValue As Integer
            OutPutValue = Convert.ToInt32(paramReturnValue.Value)

            Return OutPutValue

        End Function
#End Region

        Public Function PopulateInventoryImageByItemID(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal ItemID As String) As SqlDataReader

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateInventoryImageByItemID", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)


            Dim parameterItemID As New SqlParameter("@ItemID", Data.SqlDbType.NVarChar, 36)
            parameterItemID.Value = ItemID
            myCommand.Parameters.Add(parameterItemID)

            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs



        End Function

        Public Sub AddStepPricing(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal StepActive As Integer, ByVal Premium As Decimal, ByVal Delux As Decimal)


            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("AddStepPricing", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterStepActive As New SqlParameter("@StepActive", Data.SqlDbType.Int)
            parameterStepActive.Value = StepActive
            myCommand.Parameters.Add(parameterStepActive)

            Dim parameterPremium As New SqlParameter("@Premium", Data.SqlDbType.Decimal)
            parameterPremium.Value = Premium
            myCommand.Parameters.Add(parameterPremium)

            Dim parameterDelux As New SqlParameter("@Delux", Data.SqlDbType.Decimal)
            parameterDelux.Value = Delux
            myCommand.Parameters.Add(parameterDelux)
 

            myCommand.ExecuteNonQuery()

            ConString.Close()


        End Sub


        Public Function PopulateStepPricing(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As SqlDataReader

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateStepPricing", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)


            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs



        End Function




        Public Function PopulateCreditCardTypes(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As SqlDataReader

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateCreditCardTypes", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)


            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs



        End Function
        Public Function AddCustomerContent(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal ContentLink As String, ByVal ContentText As String, ByVal ContentActive As Integer, ByVal AddEdit As String, ByVal TopFooter As Integer, ByVal SortOrder As Integer, ByVal TopHeader As Integer)


            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("AddCustomerContent", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterContentLink As New SqlParameter("@ContentLink", Data.SqlDbType.NVarChar)
            parameterContentLink.Value = ContentLink
            myCommand.Parameters.Add(parameterContentLink)

            Dim parameterContentText As New SqlParameter("@ContentText", Data.SqlDbType.NText)
            parameterContentText.Value = ContentText
            myCommand.Parameters.Add(parameterContentText)

            Dim parameterContentActive As New SqlParameter("@ContentActive", Data.SqlDbType.Int)
            parameterContentActive.Value = ContentActive
            myCommand.Parameters.Add(parameterContentActive)


            Dim parameterAddEdit As New SqlParameter("@AddEdit", Data.SqlDbType.NVarChar)
            parameterAddEdit.Value = AddEdit
            myCommand.Parameters.Add(parameterAddEdit)


            Dim parameterTopFooter As New SqlParameter("@TopFooter", Data.SqlDbType.Int)
            parameterTopFooter.Value = TopFooter
            myCommand.Parameters.Add(parameterTopFooter)


            Dim parameterSortOrder As New SqlParameter("@SortOrder", Data.SqlDbType.Int)
            parameterSortOrder.Value = SortOrder
            myCommand.Parameters.Add(parameterSortOrder)


            Dim paramReturnValue As New SqlParameter("@ReturnValue", Data.SqlDbType.Int)
            paramReturnValue.Direction = ParameterDirection.Output
            myCommand.Parameters.Add(paramReturnValue)

            Dim parameterTopHeader As New SqlParameter("@TopHeader", Data.SqlDbType.Int)
            parameterTopHeader.Value = TopHeader
            myCommand.Parameters.Add(parameterTopHeader)






            myCommand.ExecuteNonQuery()



            Dim res As Integer



            ConString.Close()
            If paramReturnValue.Value.ToString() <> "" Then
                res = Convert.ToDecimal(paramReturnValue.Value)
            End If


            Return res



        End Function




        Public Function PopulateContent(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As DataSet


            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateContent", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet

            adapter.Fill(ds)
            ConString.Close()
            Return ds

        End Function



        Public Sub DeleteContentLinks(ByVal ContentID As Integer)

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("DeleteContentLinks", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterContentID As New SqlParameter("@ContentID", Data.SqlDbType.NVarChar)
            parameterContentID.Value = ContentID
            myCommand.Parameters.Add(parameterContentID)


            myCon.Open()

            myCommand.ExecuteNonQuery()

            myCon.Close()

        End Sub


        Public Function PopulateContentsByID(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal ContentID As Integer) As SqlDataReader

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateContentsByID", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterContentID As New SqlParameter("@ContentID", Data.SqlDbType.NVarChar)
            parameterContentID.Value = ContentID
            myCommand.Parameters.Add(parameterContentID)


            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs



        End Function

#Region "Populate Payment Method List"

        Public Function BindPaymentMethodTypes(ByVal CompanyID As String, ByVal DeptID As String, ByVal DivID As String) As Data.DataSet
            Dim ConnectionString As String = ""
            ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim sqlStr As String = "SELECT PaymentMethodID,PaymentMethodDescription,Active FROM PaymentMethods where CompanyID='" & CompanyID & "' AND DepartmentID='" & DeptID & "'AND DivisionID='" & DivID & "'"
            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            ConString.Open()
            Dim Adapter As New SqlDataAdapter(Cmd)
            Dim ds As New Data.DataSet
            Adapter.Fill(ds)
            ConString.Close()
            Return ds
        End Function
#End Region

#Region "Populate Payment Method Details"

        Public Function PopulatePaymentDetails(ByVal CompanyID As String, ByVal DeptID As String, ByVal DivID As String, ByVal PaymentMethodID As String) As SqlDataReader
            Dim ConnectionString As String = ""
            ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim sqlStr As String = "SELECT PaymentMethodID,PaymentMethodDescription,Active  FROM PaymentMethods where CompanyID='" & CompanyID & "' AND DepartmentID='" & DeptID & "'AND DivisionID='" & DivID & "'AND PaymentMethodID='" & PaymentMethodID & "'"
            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            ConString.Open()
            Dim rs As SqlDataReader
            rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)
            Return rs

        End Function
#End Region

#Region "ADD/Edit Payment Method Details"

        Public Function AddEditPaymentMethods(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal PaymentMethodID As String) As Integer
            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("AddEditPaymentMethods", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterPaymentMethodID As New SqlParameter("@PaymentMethodID", Data.SqlDbType.NVarChar)
            parameterPaymentMethodID.Value = PaymentMethodID
            myCommand.Parameters.Add(parameterPaymentMethodID)

            Dim parameterPaymentMethodDescription As New SqlParameter("@PaymentMethodDescription", Data.SqlDbType.NVarChar)
            parameterPaymentMethodDescription.Value = PaymentMethodDescription
            myCommand.Parameters.Add(parameterPaymentMethodDescription)

            Dim parameterActive As New SqlParameter("@Active", Data.SqlDbType.Bit)
            parameterActive.Value = PaymentMethodActive
            myCommand.Parameters.Add(parameterActive)

            Dim parameterAddEdit As New SqlParameter("@AddEdit", Data.SqlDbType.Int)
            parameterAddEdit.Value = AddEdit
            myCommand.Parameters.Add(parameterAddEdit)

            Dim paramReturnValue As New SqlParameter("@ReturnValue", Data.SqlDbType.Int)
            paramReturnValue.Direction = ParameterDirection.Output
            myCommand.Parameters.Add(paramReturnValue)

            myCon.Open()

            myCommand.ExecuteNonQuery()
            myCon.Close()

            Dim OutPutValue As Integer
            OutPutValue = Convert.ToInt32(paramReturnValue.Value)

            Return OutPutValue

        End Function
#End Region

#Region "Delete Payment Method"
        Public Sub DeletePaymentMethod(ByVal CompanyID As String, ByVal DeptID As String, ByVal DivID As String, ByVal PaymentMethodID As String)
            Dim ConnectionString As String = ""
            ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim SQLStr As String = "Delete FROM PaymentMethods WHERE CompanyID='" & CompanyID & "' AND DepartmentID='" & DeptID & "' AND DivisionID='" & DivID & "' AND PaymentMethodID='" & PaymentMethodID & "'"
            Dim Cmd As New SqlCommand
            Cmd.CommandText = SQLStr
            Cmd.Connection = ConString
            ' Dim rs As SqlDataReader
            ConString.Open()
            Cmd.ExecuteNonQuery()
            ConString.Close()
            'rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)

            ' Return rs
        End Sub
#End Region

        Public Sub SaveHeaderTemplateImage(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal sfileName As String, ByVal TemplateID As Integer)


            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("SaveHeaderTemplateImage", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parametersfileName As New SqlParameter("@sfileName", Data.SqlDbType.NVarChar)
            parametersfileName.Value = sfileName
            myCommand.Parameters.Add(parametersfileName)


            Dim parameterTemplateID As New SqlParameter("@TemplateID", Data.SqlDbType.Int)
            parameterTemplateID.Value = TemplateID
            myCommand.Parameters.Add(parameterTemplateID)

            myCommand.ExecuteNonQuery()

            ConString.Close()

        End Sub


        Public Function PopulateHeaderTemplateImage(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal CSSID As Integer) As SqlDataReader

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateHeaderTemplateImage", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterCSSID As New SqlParameter("@CSSID", Data.SqlDbType.Int)
            parameterCSSID.Value = CSSID
            myCommand.Parameters.Add(parameterCSSID)


            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs



        End Function







        Public Function AddCustomerCoupons(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal CouponCode As String, ByVal CouponRate As Decimal, ByVal MinimumOrderRate As Decimal, ByVal StartDate As String, ByVal EndDate As String, ByVal TotalCouponUsed As Integer, ByVal CouponCustomer As Integer, ByVal CouponType As Integer, ByVal AddEdit As String)


            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("AddCustomerCoupons", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterCouponCode As New SqlParameter("@CouponCode", Data.SqlDbType.NVarChar)
            parameterCouponCode.Value = CouponCode
            myCommand.Parameters.Add(parameterCouponCode)

            Dim parameterCouponRate As New SqlParameter("@CouponRate", Data.SqlDbType.Decimal)
            parameterCouponRate.Value = CouponRate
            myCommand.Parameters.Add(parameterCouponRate)

            Dim parameterMinimumOrderRate As New SqlParameter("@MinimumOrderRate", Data.SqlDbType.Decimal)
            parameterMinimumOrderRate.Value = MinimumOrderRate
            myCommand.Parameters.Add(parameterMinimumOrderRate)

            Dim parameterStartDate As New SqlParameter("@StartDate", Data.SqlDbType.DateTime)
            parameterStartDate.Value = StartDate
            myCommand.Parameters.Add(parameterStartDate)

            Dim parameterEndDate As New SqlParameter("@EndDate", Data.SqlDbType.DateTime)
            parameterEndDate.Value = EndDate
            myCommand.Parameters.Add(parameterEndDate)

            Dim parameterTotalCouponUsed As New SqlParameter("@TotalCouponUsed", Data.SqlDbType.Int)
            parameterTotalCouponUsed.Value = TotalCouponUsed
            myCommand.Parameters.Add(parameterTotalCouponUsed)

            Dim parameterCouponCustomer As New SqlParameter("@CouponCustomer", Data.SqlDbType.Int)
            parameterCouponCustomer.Value = CouponCustomer
            myCommand.Parameters.Add(parameterCouponCustomer)


            Dim parameterCouponType As New SqlParameter("@CouponType", Data.SqlDbType.Int)
            parameterCouponType.Value = CouponType
            myCommand.Parameters.Add(parameterCouponType)

            Dim parameterAddEdit As New SqlParameter("@AddEdit", Data.SqlDbType.NVarChar)
            parameterAddEdit.Value = AddEdit
            myCommand.Parameters.Add(parameterAddEdit)

            Dim paramReturnValue As New SqlParameter("@ReturnValue", Data.SqlDbType.Int)
            paramReturnValue.Direction = ParameterDirection.Output
            myCommand.Parameters.Add(paramReturnValue)







            myCommand.ExecuteNonQuery()



            Dim res As Integer



            ConString.Close()
            If paramReturnValue.Value.ToString() <> "" Then
                res = Convert.ToDecimal(paramReturnValue.Value)
            End If


            Return res



        End Function



        Public Function populateCustomerCoupons(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As DataSet

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateCustomerCoupons", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)




            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet

            adapter.Fill(ds)
            ConString.Close()
            Return ds


        End Function


        Public Sub DeleteCustomerCoupons(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal CouponCode As String)


            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("DeleteCustomerCoupons", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterCouponCode As New SqlParameter("@CouponCode", Data.SqlDbType.NVarChar)
            parameterCouponCode.Value = CouponCode
            myCommand.Parameters.Add(parameterCouponCode)

            myCommand.ExecuteNonQuery()

            ConString.Close()

        End Sub



        Public Function PopulateCouponDetailsByCode(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal CouponCode As String) As SqlDataReader



            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateCouponDetailsByCode", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterCouponCode As New SqlParameter("@CouponCode", Data.SqlDbType.NVarChar)
            parameterCouponCode.Value = CouponCode
            myCommand.Parameters.Add(parameterCouponCode)

            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs


        End Function
 

        Public Function PopulateCustomerCouponCodes(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As SqlDataReader



            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateCustomerCouponCodes", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)
 
            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs


        End Function




        Public Function AddBlackOutDates(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal CouponCode As String, ByVal StartDate As String) As Integer




            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("AddBlackOutDates", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterCouponCode As New SqlParameter("@CouponCode", Data.SqlDbType.NVarChar)
            parameterCouponCode.Value = CouponCode
            myCommand.Parameters.Add(parameterCouponCode)

            Dim parameterStartDate As New SqlParameter("@StartDate", Data.SqlDbType.DateTime)
            parameterStartDate.Value = StartDate
            myCommand.Parameters.Add(parameterStartDate)


            Dim parameterReturnValue As New SqlParameter("@ReturnValue", SqlDbType.Int)

            parameterReturnValue.Direction = ParameterDirection.Output
            myCommand.Parameters.Add(parameterReturnValue)








            myCommand.ExecuteNonQuery()

            ConString.Close()

            Dim result As Integer
            If parameterReturnValue.Value.ToString() <> "" Then
                result = Convert.ToInt32(parameterReturnValue.Value)

            End If
             Return result




        End Function


        Public Function PopulateBlackOutDates(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal CouponCode As String) As DataSet


            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("connectionString"))
            ConString.Open()
            Dim myCommand As New SqlCommand("PopulateBlackOutDates", ConString)
            myCommand.CommandType = CommandType.StoredProcedure


            Dim parameterCompanyID As New SqlParameter("@CompanyID", SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)


            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)


            Dim parameterDivisionID As New SqlParameter("@DivisionID", SqlDbType.NVarChar)

            parameterDivisionID.Value = DivisionID

            myCommand.Parameters.Add(parameterDivisionID)


            Dim parameterCouponCode As New SqlParameter("@CouponCode", SqlDbType.NVarChar)

            parameterCouponCode.Value = CouponCode
            myCommand.Parameters.Add(parameterCouponCode)

            Dim ds As New DataSet

            Dim adapter As New SqlDataAdapter(myCommand)

            adapter.Fill(ds)

            ConString.Close()


            Return ds

        End Function

        Public Sub DeleteBlackOutDates(ByVal BlackOutID As Integer)

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("DeleteBlackOutDates", ConString)
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterBlackOutID As New SqlParameter("@BlackOutID", SqlDbType.Int)
            parameterBlackOutID.Value = BlackOutID
            myCommand.Parameters.Add(parameterBlackOutID)

            myCommand.ExecuteNonQuery()

            ConString.Close()






        End Sub


#Region "Populating Item categoryID"
        Public Function PopulateCategoryID(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As DataSet


            Dim mycon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            mycon.Open()
            Dim myCommand As New SqlCommand("PopulateCategoryID", mycon)
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)


            Dim parameterDivisionID As New SqlParameter("@DivisionID", SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            'Dim rs As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            'Return rs

            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet

            adapter.Fill(ds)
            mycon.Close()
            Return ds

        End Function





        Public Function PopulateItemByCategoryID(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal CategoryID As String) As DataSet


            Dim mycon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            mycon.Open()
            Dim myCommand As New SqlCommand("PopulateItemByCategoryID", mycon)
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)


            Dim parameterDivisionID As New SqlParameter("@DivisionID", SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterCategoryID As New SqlParameter("@CategoryID", SqlDbType.NVarChar)
            parameterCategoryID.Value = CategoryID
            myCommand.Parameters.Add(parameterCategoryID)

            'Dim rs As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            'Return rs
            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet

            adapter.Fill(ds)
            mycon.Close()
            Return ds

        End Function
#End Region

        Public Function SaveAddOnItems(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal AddOnItemName As String, ByVal Price As Decimal, ByVal ItemImageName As String, ByVal AddEdit As String, ByVal AddOnActive As Integer)


            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("SaveAddOnItems", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)


            Dim parameterAddOnItemName As New SqlParameter("@AddOnItemName", Data.SqlDbType.NVarChar)
            parameterAddOnItemName.Value = AddOnItemName
            myCommand.Parameters.Add(parameterAddOnItemName)

            Dim parameterPrice As New SqlParameter("@Price", Data.SqlDbType.Money)
            parameterPrice.Value = Price
            myCommand.Parameters.Add(parameterPrice)

            Dim parameterItemImageName As New SqlParameter("@ItemImageName", Data.SqlDbType.NVarChar)
            parameterItemImageName.Value = ItemImageName
            myCommand.Parameters.Add(parameterItemImageName)

            Dim parameterAddEdit As New SqlParameter("@AddEdit", Data.SqlDbType.NVarChar)
            parameterAddEdit.Value = AddEdit
            myCommand.Parameters.Add(parameterAddEdit)


            Dim paramAddOnActive As New SqlParameter("@AddOnActive", Data.SqlDbType.Int)
            paramAddOnActive.Value = AddOnActive
            myCommand.Parameters.Add(paramAddOnActive)



            Dim paramReturnValue As New SqlParameter("@ReturnValue", Data.SqlDbType.Int)
            paramReturnValue.Direction = ParameterDirection.Output
            myCommand.Parameters.Add(paramReturnValue)


       


            myCommand.ExecuteNonQuery()



            Dim res As Integer



            ConString.Close()
            If paramReturnValue.Value.ToString() <> "" Then
                res = Convert.ToDecimal(paramReturnValue.Value)
            End If


            Return res


        End Function

        Public Function PopulateAddOnItemsList(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As DataSet


            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateAddOnItemsList", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet

            adapter.Fill(ds)
            ConString.Close()
            Return ds

        End Function


        Public Sub DeleteAddOnItems(ByVal AddOnItemID As Integer)
            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("DeleteAddOnItems", ConString)
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterAddOnItemID As New SqlParameter("@AddOnItemID", SqlDbType.Int)
            parameterAddOnItemID.Value = AddOnItemID
            myCommand.Parameters.Add(parameterAddOnItemID)

            myCommand.ExecuteNonQuery()

            ConString.Close()

        End Sub



        Public Function PopulateAddOnItemsByID(ByVal AddOnItemID As Integer) As SqlDataReader
            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateAddOnItemsByID", ConString)
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterAddOnItemID As New SqlParameter("@AddOnItemID", SqlDbType.Int)
            parameterAddOnItemID.Value = AddOnItemID
            myCommand.Parameters.Add(parameterAddOnItemID)



            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs





        End Function
 
        Public Function AddTrackingCode(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal PageName As String, ByVal PageScript As String, ByVal IsActive As Integer, ByVal AddEdit As String) As Integer

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("AddTrackingCode", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterPageName As New SqlParameter("@PageName", Data.SqlDbType.NVarChar)
            parameterPageName.Value = PageName
            myCommand.Parameters.Add(parameterPageName)

            Dim parameterPageScript As New SqlParameter("@PageScript", Data.SqlDbType.NText)
            parameterPageScript.Value = PageScript
            myCommand.Parameters.Add(parameterPageScript)

            Dim parameterIsActive As New SqlParameter("@IsActive", Data.SqlDbType.Int)
            parameterIsActive.Value = IsActive
            myCommand.Parameters.Add(parameterIsActive)


            Dim parameterAddEdit As New SqlParameter("@AddEdit", Data.SqlDbType.NVarChar)
            parameterAddEdit.Value = AddEdit
            myCommand.Parameters.Add(parameterAddEdit)



            Dim paramReturnValue As New SqlParameter("@ReturnValue", Data.SqlDbType.Int)
            paramReturnValue.Direction = ParameterDirection.Output
            myCommand.Parameters.Add(paramReturnValue)






            myCommand.ExecuteNonQuery()



            Dim res As Integer



            ConString.Close()
            If paramReturnValue.Value.ToString() <> "" Then
                res = Convert.ToDecimal(paramReturnValue.Value)
            End If


            Return res



        End Function


        Public Function PopulateTrackingCode(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As DataSet


            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("connectionString"))
            ConString.Open()
            Dim myCommand As New SqlCommand("PopulateTrackingCode", ConString)
            myCommand.CommandType = CommandType.StoredProcedure


            Dim parameterCompanyID As New SqlParameter("@CompanyID", SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)


            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)


            Dim parameterDivisionID As New SqlParameter("@DivisionID", SqlDbType.NVarChar)

            parameterDivisionID.Value = DivisionID

            myCommand.Parameters.Add(parameterDivisionID)




            Dim ds As New DataSet

            Dim adapter As New SqlDataAdapter(myCommand)

            adapter.Fill(ds)

            ConString.Close()


            Return ds

        End Function





        Public Sub DeleteTrackCodes(ByVal TrackID As Integer)
            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("DeleteTrackCodes", ConString)
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterTrackID As New SqlParameter("@TrackID", SqlDbType.Int)
            parameterTrackID.Value = TrackID
            myCommand.Parameters.Add(parameterTrackID)

            myCommand.ExecuteNonQuery()
            ConString.Close()

        End Sub



        Public Function PopulateTrackingCodeByID(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal TrackID As Integer) As SqlDataReader



            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateTrackingCodeByID", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterTrackID As New SqlParameter("@TrackID", Data.SqlDbType.Int)
            parameterTrackID.Value = TrackID
            myCommand.Parameters.Add(parameterTrackID)

            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs


        End Function


#Region "Search Inventory item Images"
        Public Function SearchInventoryItemImages(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal Condition As String, ByVal fieldName As String, ByVal fieldexpression As String) As DataSet


            Dim conString As New SqlConnection
            conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

            Dim myCommand As New SqlCommand("SearchInventoryItemImages", conString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterCondition As New SqlParameter("@Condition", Data.SqlDbType.NVarChar)
            parameterCondition.Value = Condition
            myCommand.Parameters.Add(parameterCondition)


            Dim parameterfieldName As New SqlParameter("@fieldName", Data.SqlDbType.NVarChar)
            parameterfieldName.Value = fieldName
            myCommand.Parameters.Add(parameterfieldName)

            Dim parameterfieldexpression As New SqlParameter("@fieldexpression", Data.SqlDbType.NVarChar)
            parameterfieldexpression.Value = fieldexpression
            myCommand.Parameters.Add(parameterfieldexpression)



            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet()
            adapter.Fill(ds)
            conString.Close()

            Return ds


        End Function
#End Region

#Region "Get Details for CreditCardApproval From OrderHeader"

        Public Function GetDetailsForCreditCardApproval(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal OrderNumber As String) As SqlDataReader
            Dim ConnectionString As String = ""
            ConnectionString = ConfigurationManager.AppSettings("ConnectionString")
            Dim ConString As New SqlConnection
            ConString.ConnectionString = ConnectionString
            Dim sqlStr As String = "SELECT CreditCardApprovalNumber, PaymentMethodID FROM OrderHeader WHERE CompanyID='" & CompanyID & "' AND DivisionID='" & DivisionID & "' AND DepartmentID='" & DepartmentID & "' AND OrderNumber='" & OrderNumber & "'"
            Dim Cmd As New SqlCommand
            Cmd.CommandText = sqlStr
            Cmd.Connection = ConString
            ConString.Open()
            Return Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)
        End Function

#End Region

#Region "Associating Add On to Items"
 

        Public Function AssociateAddOntoProducts(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal CategoryID As String, ByVal ItemID As String, ByVal AddOnItemName As Integer, ByVal AddOnForAllItemId As Integer) As Integer

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("AssociateAddOntoProducts", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterCategoryID As New SqlParameter("@CategoryID", Data.SqlDbType.NVarChar)
            parameterCategoryID.Value = CategoryID
            myCommand.Parameters.Add(parameterCategoryID)

            Dim parameterItemID As New SqlParameter("@ItemID", Data.SqlDbType.NVarChar)
            parameterItemID.Value = ItemID
            myCommand.Parameters.Add(parameterItemID)

            Dim parameterAddOnItemName As New SqlParameter("@AddOnItemID", Data.SqlDbType.Int)
            parameterAddOnItemName.Value = AddOnItemName
            myCommand.Parameters.Add(parameterAddOnItemName)

            Dim parameterAddOnForAllItemId As New SqlParameter("@AddOnForAllItemId", Data.SqlDbType.Int)
            parameterAddOnForAllItemId.Value = AddOnForAllItemId
            myCommand.Parameters.Add(parameterAddOnForAllItemId)


            Dim paramReturnValue As New SqlParameter("@ReturnValue", Data.SqlDbType.Int)
            paramReturnValue.Direction = ParameterDirection.Output
            myCommand.Parameters.Add(paramReturnValue)


            myCommand.ExecuteNonQuery()

            Dim res As Integer

            ConString.Close()
            If paramReturnValue.Value.ToString() <> "" Then
                res = Convert.ToDecimal(paramReturnValue.Value)
            End If

            Return res



        End Function

 
        Public Function PopulateItemsByCategoryID(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal Cod1 As String) As DataSet


            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("connectionString"))
            ConString.Open()
            Dim myCommand As New SqlCommand("PopulateItemsByCategoryIDs", ConString)
            myCommand.CommandType = CommandType.StoredProcedure


            Dim parameterCompanyID As New SqlParameter("@CompanyID", SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)


            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)


            Dim parameterDivisionID As New SqlParameter("@DivisionID", SqlDbType.NVarChar)

            parameterDivisionID.Value = DivisionID

            myCommand.Parameters.Add(parameterDivisionID)



            Dim parameterCod1 As New SqlParameter("@CategoryID", SqlDbType.NVarChar)

            parameterCod1.Value = Cod1

            myCommand.Parameters.Add(parameterCod1)


            Dim ds As New DataSet

            Dim adapter As New SqlDataAdapter(myCommand)

            adapter.Fill(ds)

            ConString.Close()


            Return ds

        End Function
#End Region
       

 


        Public Function PopulateAssociateAddOnItems(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal fieldName As String, ByVal fieldexpression As String) As DataSet


            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("connectionString"))
            ConString.Open()
            Dim myCommand As New SqlCommand("PopulateAssociateAddOnItems", ConString)
            myCommand.CommandType = CommandType.StoredProcedure


            Dim parameterCompanyID As New SqlParameter("@CompanyID", SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)


            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)


            Dim parameterDivisionID As New SqlParameter("@DivisionID", SqlDbType.NVarChar)

            parameterDivisionID.Value = DivisionID

            myCommand.Parameters.Add(parameterDivisionID)



            Dim parameterfieldName As New SqlParameter("@fieldName", SqlDbType.NVarChar)

            parameterfieldName.Value = fieldName

            myCommand.Parameters.Add(parameterfieldName)


            Dim parameterfieldexpression As New SqlParameter("@fieldexpression", SqlDbType.NVarChar)

            parameterfieldexpression.Value = fieldexpression

            myCommand.Parameters.Add(parameterfieldexpression)





            Dim ds As New DataSet

            Dim adapter As New SqlDataAdapter(myCommand)

            adapter.Fill(ds)

            ConString.Close()


            Return ds

        End Function



        Public Function SearchAssociateProductCategory(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal fieldName As String, ByVal fieldexpression As String) As DataSet


            Dim conString As New SqlConnection
            conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

            Dim myCommand As New SqlCommand("SearchAssociateProductCategory", conString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

      
            Dim parameterfieldName As New SqlParameter("@fieldName", Data.SqlDbType.NVarChar)
            parameterfieldName.Value = fieldName
            myCommand.Parameters.Add(parameterfieldName)

            Dim parameterfieldexpression As New SqlParameter("@fieldexpression", Data.SqlDbType.NVarChar)
            parameterfieldexpression.Value = fieldexpression
            myCommand.Parameters.Add(parameterfieldexpression)



            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet()
            adapter.Fill(ds)
            conString.Close()

            Return ds


        End Function





        Public Function DeleteProductCategory(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal CategoryID As String, ByVal AddOnItemID As String) As DataSet


            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("DeleteProductCategory", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterAddOnItemID As New SqlParameter("@AddOnItemID", Data.SqlDbType.NVarChar)
            parameterAddOnItemID.Value = AddOnItemID
            myCommand.Parameters.Add(parameterAddOnItemID)

            Dim parameterCategoryID As New SqlParameter("@CategoryID", Data.SqlDbType.NVarChar)
            parameterCategoryID.Value = CategoryID
            myCommand.Parameters.Add(parameterCategoryID)

            myCon.Open()


            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet()
            adapter.Fill(ds)
            myCon.Close()

            Return ds

        End Function



        Public Function DeleteAssociateAddOnItem(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal AssociateID As Integer) As DataSet


            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("DeleteAssociateAddOnItem", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterAssociateID As New SqlParameter("@AssociateID", Data.SqlDbType.Int)
            parameterAssociateID.Value = AssociateID
            myCommand.Parameters.Add(parameterAssociateID)


            myCon.Open()


            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet()
            adapter.Fill(ds)
            myCon.Close()

            Return ds

        End Function



        Public Function PopulateAddOnForItem(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal ItemID As String) As DataSet


            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("PopulateAddOnForItem", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterItemID As New SqlParameter("@ItemID", Data.SqlDbType.NVarChar)
            parameterItemID.Value = ItemID
            myCommand.Parameters.Add(parameterItemID)


            myCon.Open()


            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet()
            adapter.Fill(ds)
            myCon.Close()

            Return ds
        End Function

        Public Function PopulateItemDetailsAddOn(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal ItemID As String) As DataSet

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("enterprise.Inventory_PopulateItemInfoSimple", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterItemID As New SqlParameter("@ItemID", Data.SqlDbType.NVarChar)
            parameterItemID.Value = ItemID
            myCommand.Parameters.Add(parameterItemID)
            myCon.Open()




            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet()
            adapter.Fill(ds)
            myCon.Close()

            Return ds
        End Function




        Public Function AddOnDetailstoOrderDetails(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal OrderNumber As String, ByVal OrderLineNumber As String, ByVal ItemPrice As Decimal, ByVal AddonIDQty As String) As DataSet

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("AddOnDetailstoOrderDetails", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
            parameterOrderNumber.Value = OrderNumber
            myCommand.Parameters.Add(parameterOrderNumber)

            Dim parameterOrderLineNumber As New SqlParameter("@OrderLineNumber", Data.SqlDbType.Int)
            parameterOrderLineNumber.Value = OrderLineNumber
            myCommand.Parameters.Add(parameterOrderLineNumber)



            Dim parameterItemPrice As New SqlParameter("@ItemPrice", Data.SqlDbType.Decimal)
            parameterItemPrice.Value = ItemPrice
            myCommand.Parameters.Add(parameterItemPrice)




            Dim parameterAddonIDQty As New SqlParameter("@AddOnIDQty", Data.SqlDbType.NVarChar)
            parameterAddonIDQty.Value = AddonIDQty
            myCommand.Parameters.Add(parameterAddonIDQty)

            myCon.Open()




            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet()
            adapter.Fill(ds)
            myCon.Close()

            Return ds
        End Function


#Region "Salutations"

        Public Function AddSalutations(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal Salutation As String) As Integer

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("AddSalutations", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterSalutation As New SqlParameter("@Salutation", Data.SqlDbType.NVarChar)
            parameterSalutation.Value = Salutation
            myCommand.Parameters.Add(parameterSalutation)

            Dim paramReturnValue As New SqlParameter("@ReturnValue", Data.SqlDbType.Int)
            paramReturnValue.Direction = ParameterDirection.Output
            myCommand.Parameters.Add(paramReturnValue)


            myCommand.ExecuteNonQuery()



            Dim res As Integer



            ConString.Close()
            If paramReturnValue.Value.ToString() <> "" Then
                res = Convert.ToDecimal(paramReturnValue.Value)
            End If


            Return res



        End Function



        Public Function PopulateSalutations(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As DataSet

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("PopulateSalutation", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)





            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet()
            adapter.Fill(ds)
            myCon.Close()

            Return ds
        End Function


        Public Sub DeleteSalutations(ByVal SalutationID As Integer)

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("DeleteSalutations", ConString)
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterSalutationID As New SqlParameter("@SalutationID", SqlDbType.Int)
            parameterSalutationID.Value = SalutationID
            myCommand.Parameters.Add(parameterSalutationID)

            myCommand.ExecuteNonQuery()


            ConString.Close()


        End Sub

#End Region


#Region "View All products"


        Public Function PopulateInventoryItems(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As DataSet

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("PopulateInventoryItems", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

       
            myCon.Open()




            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet()
            adapter.Fill(ds)
            myCon.Close()

            Return ds
        End Function




        Public Function PopulateItemDetailsByItemID(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal ItemID As String) As SqlDataReader


            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("PopulateInventoryImageByItemID", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterItemID As New SqlParameter("@ItemID", Data.SqlDbType.NVarChar)
            parameterItemID.Value = ItemID
            myCommand.Parameters.Add(parameterItemID)


            myCon.Open()


            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs

        End Function






        Public Function PopulateInventoryFamilies(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As SqlDataReader


            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("PopulateInventoryFamilies", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)


            myCon.Open()


            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs

        End Function



        Public Function PopulateInventoryCategories(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal FamilyID As String) As DataSet



            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("PopulateInventoryCategories", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterFamilyID As New SqlParameter("@ItemFamilyID", Data.SqlDbType.NVarChar)
            parameterFamilyID.Value = FamilyID
            myCommand.Parameters.Add(parameterFamilyID)


            myCon.Open()




            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet()
            adapter.Fill(ds)
            myCon.Close()

            Return ds

        End Function




        Public Function ItemDetailsSearch(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal fieldName As String, ByVal fieldexpression As String, ByVal fieldDisplay As String, ByVal Condition As String) As DataSet

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("ItemDetailsSearch", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterfieldName As New SqlParameter("@fieldName", Data.SqlDbType.NVarChar)
            parameterfieldName.Value = fieldName
            myCommand.Parameters.Add(parameterfieldName)

            Dim parameterfieldexpression As New SqlParameter("@fieldexpression", Data.SqlDbType.NVarChar)
            parameterfieldexpression.Value = fieldexpression
            myCommand.Parameters.Add(parameterfieldexpression)

            Dim parameterfieldDisplay As New SqlParameter("@fieldDisplay", Data.SqlDbType.NVarChar)
            parameterfieldDisplay.Value = fieldDisplay
            myCommand.Parameters.Add(parameterfieldDisplay)

            Dim parameterCondition As New SqlParameter("@Condition", Data.SqlDbType.NVarChar)
            parameterCondition.Value = Condition
            myCommand.Parameters.Add(parameterCondition)



            myCon.Open()




            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet()
            adapter.Fill(ds)
            myCon.Close()

            Return ds
        End Function



        Public Function UpdateInventoryItemDetailsGrid(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal ItemID As String, ByVal IsActive As Integer, ByVal featured As Integer, ByVal Price As Decimal) As Integer

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("UpdateInventoryItemDetailsGrid", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterItemID As New SqlParameter("@ItemID", Data.SqlDbType.NVarChar)
            parameterItemID.Value = ItemID
            myCommand.Parameters.Add(parameterItemID)

            Dim parameterIsActive As New SqlParameter("@IsActive", Data.SqlDbType.Int)
            parameterIsActive.Value = IsActive
            myCommand.Parameters.Add(parameterIsActive)

            Dim parameterfeatured As New SqlParameter("@featured", Data.SqlDbType.Int)
            parameterfeatured.Value = featured
            myCommand.Parameters.Add(parameterfeatured)

            Dim parameterPrice As New SqlParameter("@Price", Data.SqlDbType.Money)
            parameterPrice.Value = Price
            myCommand.Parameters.Add(parameterPrice)


            myCon.Open()
            Dim result As Integer
            result = myCommand.ExecuteNonQuery()
            myCon.Close()

        End Function



        Public Sub updateItemDetails(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal ItemID As String, ByVal FamilyID As String, ByVal ItemName As String, ByVal ItemDescription As String, ByVal ItemLongDescription As String, ByVal ItemWeight As Decimal, ByVal ItemShipWeight As Integer, ByVal ItemUOM As String, ByVal Price As Decimal, ByVal IsActive As Integer, ByVal Featured As Integer, ByVal WireServiceProducts As Integer, ByVal CategoryID As String, ByVal CareInstructions As String)


            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("updateItemDetails", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterItemID As New SqlParameter("@ItemID", Data.SqlDbType.NVarChar)
            parameterItemID.Value = ItemID
            myCommand.Parameters.Add(parameterItemID)

            Dim parameterFamilyID As New SqlParameter("@FamilyID", Data.SqlDbType.NVarChar)
            parameterFamilyID.Value = FamilyID
            myCommand.Parameters.Add(parameterFamilyID)


            Dim parameterItemName As New SqlParameter("@ItemName", Data.SqlDbType.NVarChar)
            parameterItemName.Value = ItemName
            myCommand.Parameters.Add(parameterItemName)



            Dim parameterItemDescription As New SqlParameter("@ItemDescription", Data.SqlDbType.NVarChar)
            parameterItemDescription.Value = ItemDescription
            myCommand.Parameters.Add(parameterItemDescription)



            Dim parameterItemLongDescription As New SqlParameter("@ItemLongDescription", Data.SqlDbType.NVarChar)
            parameterItemLongDescription.Value = ItemLongDescription
            myCommand.Parameters.Add(parameterItemLongDescription)



            Dim parameterItemWeight As New SqlParameter("@ItemWeight", Data.SqlDbType.Decimal)
            parameterItemWeight.Value = ItemWeight
            myCommand.Parameters.Add(parameterItemWeight)



            Dim parameterItemShipWeight As New SqlParameter("@ItemShipWeight", Data.SqlDbType.Int)
            parameterItemShipWeight.Value = ItemShipWeight
            myCommand.Parameters.Add(parameterItemShipWeight)



            Dim parameterItemUOM As New SqlParameter("@ItemUOM", Data.SqlDbType.NVarChar)
            parameterItemUOM.Value = ItemUOM
            myCommand.Parameters.Add(parameterItemUOM)



            Dim parameterCategoryID As New SqlParameter("@CategoryID", Data.SqlDbType.NVarChar)
            parameterCategoryID.Value = CategoryID
            myCommand.Parameters.Add(parameterCategoryID)



            Dim parameterCareInstructions As New SqlParameter("@CareInstructions", Data.SqlDbType.NVarChar)
            parameterCareInstructions.Value = CareInstructions
            myCommand.Parameters.Add(parameterCareInstructions)









            Dim parameterWireServiceProducts As New SqlParameter("@WireServiceProducts", Data.SqlDbType.Int)
            parameterWireServiceProducts.Value = WireServiceProducts
            myCommand.Parameters.Add(parameterWireServiceProducts)










            Dim parameterIsActive As New SqlParameter("@IsActive", Data.SqlDbType.Int)
            parameterIsActive.Value = IsActive
            myCommand.Parameters.Add(parameterIsActive)

            Dim parameterfeatured As New SqlParameter("@featured", Data.SqlDbType.Int)
            parameterfeatured.Value = Featured
            myCommand.Parameters.Add(parameterfeatured)

            Dim parameterPrice As New SqlParameter("@Price", Data.SqlDbType.Money)
            parameterPrice.Value = Price
            myCommand.Parameters.Add(parameterPrice)


            myCon.Open()

            myCommand.ExecuteNonQuery()
            myCon.Close()

        End Sub


        Public Sub DeleteInventoryItems(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal ItemID As String)


            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("DeleteInventoryItems", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterItemID As New SqlParameter("@ItemID", Data.SqlDbType.NVarChar)
            parameterItemID.Value = ItemID
            myCommand.Parameters.Add(parameterItemID)






            myCon.Open()

            myCommand.ExecuteNonQuery()

            myCon.Close()

        End Sub
#End Region

#Region "VendorDetails"


        Public Function PopulateVendorDetails(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal State As String, ByVal City As String) As DataSet

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("PopulateVendorDetails", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterState As New SqlParameter("@State", Data.SqlDbType.NVarChar)
            parameterState.Value = State
            myCommand.Parameters.Add(parameterState)

            Dim parameterCity As New SqlParameter("@City", Data.SqlDbType.NVarChar)
            parameterCity.Value = City
            myCommand.Parameters.Add(parameterCity)


 



        
            myCon.Open()




            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet()
            adapter.Fill(ds)
            myCon.Close()

            Return ds
        End Function

    
        Public Sub UpdateWireOutInformation(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal OrdNumber As String, ByVal vendorID As String, ByVal WireService As String, ByVal WireOutCode As String, ByVal WireOutOwner As String, ByVal WireOutStatus As String, ByVal WireOutNotes As String, ByVal WireOutTransID As String, ByVal WireOutTransMethod As String, ByVal WireOutPriority As String)

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("UpdateWireOutInformation", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)


            Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
            parameterOrderNumber.Value = OrdNumber
            myCommand.Parameters.Add(parameterOrderNumber)

            Dim parametervendorID As New SqlParameter("@vendorID", Data.SqlDbType.NVarChar)
            parametervendorID.Value = vendorID
            myCommand.Parameters.Add(parametervendorID)

            Dim parameterWireService As New SqlParameter("@WireOutService", Data.SqlDbType.NVarChar)
            parameterWireService.Value = WireService
            myCommand.Parameters.Add(parameterWireService)



            Dim parameterWireOutCode As New SqlParameter("@WireOutCode", Data.SqlDbType.NVarChar)
            parameterWireOutCode.Value = WireOutCode
            myCommand.Parameters.Add(parameterWireOutCode)


            Dim parameterWireOutOwner As New SqlParameter("@WireOutOwner", Data.SqlDbType.NVarChar)
            parameterWireOutOwner.Value = WireOutOwner
            myCommand.Parameters.Add(parameterWireOutOwner)



            Dim parameterWireOutStatus As New SqlParameter("@WireOutStatus", Data.SqlDbType.NVarChar)
            parameterWireOutStatus.Value = WireOutStatus
            myCommand.Parameters.Add(parameterWireOutStatus)



            Dim parameterWireOutNotes As New SqlParameter("@WireOutNotes", Data.SqlDbType.NVarChar)
            parameterWireOutNotes.Value = WireOutNotes
            myCommand.Parameters.Add(parameterWireOutNotes)

            Dim parameterWireOutTransID As New SqlParameter("@WireOutTransID", Data.SqlDbType.NVarChar)
            parameterWireOutTransID.Value = WireOutTransID
            myCommand.Parameters.Add(parameterWireOutTransID)

            Dim parameterWireOutTransMethod As New SqlParameter("@WireOutTransMethod", Data.SqlDbType.NVarChar)
            parameterWireOutTransMethod.Value = WireOutTransMethod
            myCommand.Parameters.Add(parameterWireOutTransMethod)




            Dim parameterWireOutPriority As New SqlParameter("@WireOutPriority", Data.SqlDbType.NVarChar)
            parameterWireOutPriority.Value = WireOutPriority
            myCommand.Parameters.Add(parameterWireOutPriority)



            myCommand.ExecuteNonQuery()







            ConString.Close()



        End Sub

        Public Sub UpdateVendorDetails(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal vendorID As String, ByVal VendorName As String, ByVal VendorAddress As String, ByVal VendorCity As String, ByVal VendorCountry As String, ByVal VendorZip As String, ByVal VendorState As String, ByVal VendorPhone As String, ByVal VendorEmail As String, ByVal VendorWebPage As String, ByVal VendorFax As String)
         

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("UpdateVendorDetails", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)




            Dim parametervendorID As New SqlParameter("@vendorID", Data.SqlDbType.NVarChar)
            parametervendorID.Value = vendorID
            myCommand.Parameters.Add(parametervendorID)

            Dim parameterVendorName As New SqlParameter("@VendorName", Data.SqlDbType.NVarChar)
            parameterVendorName.Value = VendorName
            myCommand.Parameters.Add(parameterVendorName)



            Dim parameterVendorAddress As New SqlParameter("@VendorAddress", Data.SqlDbType.NVarChar)
            parameterVendorAddress.Value = VendorAddress
            myCommand.Parameters.Add(parameterVendorAddress)


            Dim parameterVendorCity As New SqlParameter("@VendorCity", Data.SqlDbType.NVarChar)
            parameterVendorCity.Value = VendorCity
            myCommand.Parameters.Add(parameterVendorCity)



            Dim parameterVendorCountry As New SqlParameter("@VendorCountry", Data.SqlDbType.NVarChar)
            parameterVendorCountry.Value = VendorCountry
            myCommand.Parameters.Add(parameterVendorCountry)



            Dim parameterVendorZip As New SqlParameter("@VendorZip", Data.SqlDbType.NVarChar)
            parameterVendorZip.Value = VendorZip
            myCommand.Parameters.Add(parameterVendorZip)

            Dim parameterVendorState As New SqlParameter("@VendorState", Data.SqlDbType.NVarChar)
            parameterVendorState.Value = VendorState
            myCommand.Parameters.Add(parameterVendorState)

            Dim parameterVendorPhone As New SqlParameter("@VendorPhone", Data.SqlDbType.NVarChar)
            parameterVendorPhone.Value = VendorPhone
            myCommand.Parameters.Add(parameterVendorPhone)




            Dim parameterVendorEmail As New SqlParameter("@VendorEmail", Data.SqlDbType.NVarChar)
            parameterVendorEmail.Value = VendorEmail
            myCommand.Parameters.Add(parameterVendorEmail)


            Dim parameterVendorWebPage As New SqlParameter("@VendorWebPage", Data.SqlDbType.NVarChar)
            parameterVendorWebPage.Value = VendorWebPage
            myCommand.Parameters.Add(parameterVendorWebPage)

            Dim parameterVendorFax As New SqlParameter("@VendorFax  ", Data.SqlDbType.NVarChar)
            parameterVendorFax.Value = VendorFax
            myCommand.Parameters.Add(parameterVendorFax)



            myCommand.ExecuteNonQuery()







            ConString.Close()
        End Sub

        Public Function PopulateVendorDetailsOrderForm(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal VendorID As String) As SqlDataReader



            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateVendorDetailsOrderForm", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterVendorID As New SqlParameter("@VendorID", Data.SqlDbType.NVarChar)
            parameterVendorID.Value = vendorID
            myCommand.Parameters.Add(parameterVendorID)

            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs


        End Function


#End Region

#Region "payment Accounts"

        'Added variable for Credit card Offline by jacob on 19/12/2007


        Public Function AddEditPaymentAccounts(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal Partnername As String, ByVal Vendorname As String, ByVal UserName As String, ByVal Password As String, ByVal TestUrl As String, ByVal LiveUrl As String, ByVal TestMode As Integer, ByVal CSCMatch As Integer, ByVal AVSStreetMatch As Integer, ByVal AVSZipMatch As Integer, ByVal InternationalAVSIndicator As Integer, ByVal Toggle As Integer, ByVal CreditCard As String, ByVal CCOffline As Integer, Optional ByVal UsedIn As String = "") As Integer

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("AddEditPaymentAccounts", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterPartnername As New SqlParameter("@Partnername", Data.SqlDbType.NVarChar)
            parameterPartnername.Value = Partnername
            myCommand.Parameters.Add(parameterPartnername)

            Dim parameterVendorname As New SqlParameter("@Vendorname", Data.SqlDbType.NVarChar)
            parameterVendorname.Value = Vendorname
            myCommand.Parameters.Add(parameterVendorname)


            Dim parameterUserName As New SqlParameter("@UserName", Data.SqlDbType.NVarChar)
            parameterUserName.Value = UserName
            myCommand.Parameters.Add(parameterUserName)

            Dim parameterPassword As New SqlParameter("@Password", Data.SqlDbType.NVarChar)
            parameterPassword.Value = Password
            myCommand.Parameters.Add(parameterPassword)


            Dim parameterTestUrl As New SqlParameter("@TestUrl", Data.SqlDbType.NVarChar)
            parameterTestUrl.Value = TestUrl
            myCommand.Parameters.Add(parameterTestUrl)


            Dim parameterLiveUrl As New SqlParameter("@LiveUrl", Data.SqlDbType.NVarChar)
            parameterLiveUrl.Value = LiveUrl
            myCommand.Parameters.Add(parameterLiveUrl)


            Dim parameterTestMode As New SqlParameter("@TestMode", Data.SqlDbType.Int)
            parameterTestMode.Value = TestMode
            myCommand.Parameters.Add(parameterTestMode)

            Dim parameterCSCMatch As New SqlParameter("@CSCMatch", Data.SqlDbType.Int)
            parameterCSCMatch.Value = CSCMatch
            myCommand.Parameters.Add(parameterCSCMatch)

            Dim parameterAVSStreetMatch As New SqlParameter("@AVSStreetMatch", Data.SqlDbType.Int)
            parameterAVSStreetMatch.Value = AVSStreetMatch
            myCommand.Parameters.Add(parameterAVSStreetMatch)

            Dim parameterAVSZipMatch As New SqlParameter("@AVSZipMatch", Data.SqlDbType.Int)
            parameterAVSZipMatch.Value = AVSZipMatch
            myCommand.Parameters.Add(parameterAVSZipMatch)

            Dim parameterInternationalAVSIndicator As New SqlParameter("@InternationalAVSIndicator", Data.SqlDbType.Int)
            parameterInternationalAVSIndicator.Value = InternationalAVSIndicator
            myCommand.Parameters.Add(parameterInternationalAVSIndicator)

            Dim parameterToggle As New SqlParameter("@Toggle ", Data.SqlDbType.Int)
            parameterToggle.Value = Toggle
            myCommand.Parameters.Add(parameterToggle)



            Dim parameterCreditCard As New SqlParameter("@CreditCard ", Data.SqlDbType.NVarChar)
            parameterCreditCard.Value = CreditCard
            myCommand.Parameters.Add(parameterCreditCard)


            Dim parameterCCOffline As New SqlParameter("@CCOffline ", Data.SqlDbType.Int)
            parameterCCOffline.Value = CCOffline
            myCommand.Parameters.Add(parameterCCOffline)

            If UsedIn <> String.Empty Then myCommand.Parameters.AddWithValue("UsedIn", UsedIn)

            Dim result As Integer
            result = myCommand.ExecuteNonQuery()







            ConString.Close()





        End Function
 

        Public Function PopulatePaymentAccounts(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, Optional ByVal UsedIn As String = "") As SqlDataReader



            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulatePaymentAccounts", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs


        End Function



        Public Sub AddEditPaymentAccountsCreditCards(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal CreditCard As String)

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("AddEditPaymentAccountsCreditCards", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterCreditCard As New SqlParameter("@CreditCard", Data.SqlDbType.NVarChar)
            parameterCreditCard.Value = CreditCard
            myCommand.Parameters.Add(parameterCreditCard)

         


            myCommand.ExecuteNonQuery()



            


            ConString.Close()
          





        End Sub



#End Region


#Region "PopulateDeliveryDateandDelivery Method Display"

        Public Function PopulateDeliveryDateandMethodDisplay(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As SqlDataReader



            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateDeliveryDateandMethodDisplay", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs


        End Function

#End Region


#Region " Email Notifications "

        'Public Function PopulateWebSitePackages(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal EmployeeID As String) As SqlDataReader



        '    Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        '    ConString.Open()

        '    Dim myCommand As New SqlCommand("PopulateWebSitePackages", ConString)
        '    myCommand.CommandType = Data.CommandType.StoredProcedure

        '    Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
        '    parameterCompanyID.Value = CompanyID
        '    myCommand.Parameters.Add(parameterCompanyID)

        '    Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
        '    parameterDepartmentID.Value = DepartmentID
        '    myCommand.Parameters.Add(parameterDepartmentID)

        '    Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
        '    parameterDivisionID.Value = DivisionID
        '    myCommand.Parameters.Add(parameterDivisionID)

        '    Dim parameterEmployeeID As New SqlParameter("@EmployeeID", Data.SqlDbType.NVarChar, 36)
        '    parameterEmployeeID.Value = EmployeeID
        '    myCommand.Parameters.Add(parameterEmployeeID)

        '    Dim rs As SqlDataReader
        '    rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
        '    Return rs


        'End Function


        Public Function AddEditEmailContent(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal EmailContent As String, ByVal EmailSubject As String, ByVal EmailType As String) As Integer

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("AddEditEmailContent", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            'Dim parameterWebSitePackage As New SqlParameter("@WebSitePackage", Data.SqlDbType.NVarChar)
            'parameterWebSitePackage.Value = WebSitePackage
            'myCommand.Parameters.Add(parameterWebSitePackage)

            Dim parameterEmailContent As New SqlParameter("@EmailContent", Data.SqlDbType.NText)
            parameterEmailContent.Value = EmailContent
            myCommand.Parameters.Add(parameterEmailContent)

            Dim parameterEmailSubject As New SqlParameter("@EmailSubject", Data.SqlDbType.NVarChar)
            parameterEmailSubject.Value = EmailSubject
            myCommand.Parameters.Add(parameterEmailSubject)


            Dim parameterEmailType As New SqlParameter("@EmailType", Data.SqlDbType.NVarChar)
            parameterEmailType.Value = EmailType
            myCommand.Parameters.Add(parameterEmailType)







            Dim paramReturnValue As New SqlParameter("@ReturnValue", Data.SqlDbType.Int)
            paramReturnValue.Direction = ParameterDirection.Output
            myCommand.Parameters.Add(paramReturnValue)






            myCommand.ExecuteNonQuery()



            Dim res As Integer



            ConString.Close()
            If paramReturnValue.Value.ToString() <> "" Then
                res = Convert.ToDecimal(paramReturnValue.Value)
            End If


            Return res



        End Function





        Public Function PopulateEmailContent(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As SqlDataReader



            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateEmailContent", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)



            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs


        End Function




        Public Function PopulateOrderCustomerDetails(ByVal CompID As String, ByVal DeptID As String, ByVal DivID As String, ByVal OrderID As String) As SqlDataReader
            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("PopulateShippingCustomerDetails", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DeptID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
            parameterOrderNumber.Value = OrderID
            myCommand.Parameters.Add(parameterOrderNumber)
            myCon.Open()

            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs


        End Function



        Public Function PopulateShippingCustomerDetails(ByVal CompID As String, ByVal DeptID As String, ByVal DivID As String, ByVal OrderID As String) As SqlDataReader
            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("PopulateShippingCustomerDetails", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DeptID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
            parameterOrderNumber.Value = OrderID
            myCommand.Parameters.Add(parameterOrderNumber)
            myCon.Open()

            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs


        End Function





        Public Function PopulateOrderItemDetails(ByVal CompID As String, ByVal DeptID As String, ByVal DivID As String, ByVal OrderID As String) As SqlDataReader
            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("PopulateOrderItemDetails", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DeptID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
            parameterOrderNumber.Value = OrderID
            myCommand.Parameters.Add(parameterOrderNumber)
            myCon.Open()

            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs


        End Function

        Public Function PopulateEmailContentByEmailType(ByVal CompID As String, ByVal DeptID As String, ByVal DivID As String, ByVal EmailType As String) As SqlDataReader
            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("PopulateEmailContentByEmailType", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DeptID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterEmailType As New SqlParameter("@EmailType", Data.SqlDbType.NVarChar)
            parameterEmailType.Value = EmailType
            myCommand.Parameters.Add(parameterEmailType)
            myCon.Open()
            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs


        End Function

        Public Function RetailerGetCompanyTime(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As SqlDataReader

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("[enterprise].[GetRetailerCompanyGMT]", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            myCon.Open()
            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs


        End Function
#End Region


#Region "Trip Sheet"

        Public Function PopulateTripID(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As String

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))

            Dim myCommand As New SqlCommand("PopulateTripID", myCon)
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)


            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            myCon.Open()

            Dim TripID As String = myCommand.ExecuteScalar()
            Return TripID

            myCon.Close()

        End Function

        Public Function PopulateDriverID(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As SqlDataReader


            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))

            Dim myCommand As New SqlCommand("PopulateDriverID", myCon)
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)


            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            myCon.Open()

            Dim rs As SqlDataReader

            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            Return rs



        End Function



        Public Function PopulateVehicleID(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As SqlDataReader


            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))

            Dim myCommand As New SqlCommand("PopulateVehicleID", myCon)
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)


            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            myCon.Open()

            Dim rs As SqlDataReader

            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            Return rs



        End Function


        Public Function PopulateVehicleDetails(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal TruckID As String) As SqlDataReader

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))

            Dim myCommand As New SqlCommand("PopulateVehicleDetails", myCon)
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)


            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)



            Dim parameterTruckID As New SqlParameter("@TruckID", SqlDbType.NVarChar)
            parameterTruckID.Value = TruckID
            myCommand.Parameters.Add(parameterTruckID)

            myCon.Open()

            Dim rs As SqlDataReader

            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            Return rs

        End Function


        Public Function PopulateOrderDetails(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal TripID As String) As DataSet


            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))

            Dim myCommand As New SqlCommand("PopulateOrderDetails", myCon)
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)


            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)



            Dim parameterTripID As New SqlParameter("@TripID", SqlDbType.NVarChar)
            parameterTripID.Value = TripID
            myCommand.Parameters.Add(parameterTripID)

            myCon.Open()


            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet()
            adapter.Fill(ds)
            myCon.Close()

            Return ds

        End Function

        Public Function AddTripDetails(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal OrderNumber As String, ByVal DriverID As String, ByVal TruckID As String, ByVal TripID As String, ByVal Mileage As String) As Integer



            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))

            Dim myCommand As New SqlCommand("AddTripDetails", myCon)
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)


            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDriverID As New SqlParameter("@DriverID", SqlDbType.NVarChar)
            parameterDriverID.Value = DriverID
            myCommand.Parameters.Add(parameterDriverID)

            Dim parameterTruckID As New SqlParameter("@TruckID", SqlDbType.NVarChar)
            parameterTruckID.Value = TruckID
            myCommand.Parameters.Add(parameterTruckID)

            Dim parameterOrderNumber As New SqlParameter("@OrderNumber", SqlDbType.NVarChar)
            parameterOrderNumber.Value = OrderNumber
            myCommand.Parameters.Add(parameterOrderNumber)

            Dim parameterTripID As New SqlParameter("@TripID", SqlDbType.NVarChar)
            parameterTripID.Value = TripID
            myCommand.Parameters.Add(parameterTripID)

            Dim parameterMileage As New SqlParameter("@Mileage", SqlDbType.NVarChar)
            parameterMileage.Value = Mileage
            myCommand.Parameters.Add(parameterMileage)


            Dim parameterReturnValue As New SqlParameter("@ReturnValue", Data.SqlDbType.Int)
            parameterReturnValue.Direction = ParameterDirection.Output
            myCommand.Parameters.Add(parameterReturnValue)




            myCon.Open()

            Dim result As Integer

            myCommand.ExecuteNonQuery()

            myCon.Close()

            If parameterReturnValue.Value.ToString() <> "" Then
                result = Convert.ToInt32(parameterReturnValue.Value)

            End If

            Return result



        End Function

        Public Sub DeleteTripSheetDetails(ByVal TID As Integer)


            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("DeleteTripSheetDetails", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterTID As New SqlParameter("@TID", Data.SqlDbType.Int)
            parameterTID.Value = TID
            myCommand.Parameters.Add(parameterTID)

            myCon.Open()

            myCommand.ExecuteNonQuery()

            myCon.Close()

        End Sub


        Public Sub AddTripWireSheetDetails(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal OrderNumber As String, ByVal DriverID As String, ByVal TruckID As String, ByVal TripID As String, ByVal Mileage As String)



            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))

            Dim myCommand As New SqlCommand("AddTripWireSheetDetails", myCon)
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)


            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDriverID As New SqlParameter("@DriverID", SqlDbType.NVarChar)
            parameterDriverID.Value = DriverID
            myCommand.Parameters.Add(parameterDriverID)

            Dim parameterTruckID As New SqlParameter("@TruckID", SqlDbType.NVarChar)
            parameterTruckID.Value = TruckID
            myCommand.Parameters.Add(parameterTruckID)

            Dim parameterOrderNumber As New SqlParameter("@OrderNumber", SqlDbType.NVarChar)
            parameterOrderNumber.Value = OrderNumber
            myCommand.Parameters.Add(parameterOrderNumber)

            Dim parameterTripID As New SqlParameter("@TripID", SqlDbType.NVarChar)
            parameterTripID.Value = TripID
            myCommand.Parameters.Add(parameterTripID)

            Dim parameterMileage As New SqlParameter("@Mileage", SqlDbType.NVarChar)
            parameterMileage.Value = Mileage
            myCommand.Parameters.Add(parameterMileage)






            myCon.Open()



            myCommand.ExecuteNonQuery()

            myCon.Close()




        End Sub


        Public Function AddTripSheetDetails(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal OrderNumber As String, ByVal DriverID As String, ByVal TruckID As String, ByVal TripID As String, ByVal Mileage As String) As Integer




            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))

            Dim myCommand As New SqlCommand("AddTripSheetDetails", myCon)
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)


            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDriverID As New SqlParameter("@DriverID", SqlDbType.NVarChar)
            parameterDriverID.Value = DriverID
            myCommand.Parameters.Add(parameterDriverID)

            Dim parameterTruckID As New SqlParameter("@TruckID", SqlDbType.NVarChar)
            parameterTruckID.Value = TruckID
            myCommand.Parameters.Add(parameterTruckID)

            Dim parameterOrderNumber As New SqlParameter("@OrderNumber", SqlDbType.NVarChar)
            parameterOrderNumber.Value = OrderNumber
            myCommand.Parameters.Add(parameterOrderNumber)

            Dim parameterTripID As New SqlParameter("@TripID", SqlDbType.NVarChar)
            parameterTripID.Value = TripID
            myCommand.Parameters.Add(parameterTripID)

            Dim parameterMileage As New SqlParameter("@Mileage", SqlDbType.NVarChar)
            parameterMileage.Value = Mileage
            myCommand.Parameters.Add(parameterMileage)






            Dim parameterReturnValue As New SqlParameter("@ReturnValue", Data.SqlDbType.Int)
            parameterReturnValue.Direction = ParameterDirection.Output
            myCommand.Parameters.Add(parameterReturnValue)




            myCon.Open()

            Dim result As Integer

            myCommand.ExecuteNonQuery()

            myCon.Close()

            If parameterReturnValue.Value.ToString() <> "" Then
                result = Convert.ToInt32(parameterReturnValue.Value)

            End If

            Return result



        End Function
        Public Function PopulateDeliveryManagerDetails(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal FromDate As String, ByVal ToDate As String) As DataSet

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))


            Dim myCommand As New SqlCommand("PopulateDeliveryManagerDetails", myCon)
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterFromDate As New SqlParameter("@FromDate", SqlDbType.DateTime)

            If FromDate = "" Then
                parameterFromDate.Value = System.DBNull.Value

            Else
                parameterFromDate.Value = FromDate
            End If

            myCommand.Parameters.Add(parameterFromDate)

            Dim parameterToDate As New SqlParameter("@ToDate", SqlDbType.DateTime)

            If ToDate = "" Then
                parameterToDate.Value = System.DBNull.Value

            Else
                parameterToDate.Value = ToDate
            End If

            myCommand.Parameters.Add(parameterToDate)

            myCon.Open()

            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet()
            adapter.Fill(ds)
            myCon.Close()
            Return ds

        End Function

        Public Sub UpdateOrderTripDetails(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal OrderNumber As String, ByVal DriverID As String, ByVal TruckID As String, ByVal TripID As String)




            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))

            Dim myCommand As New SqlCommand("UpdateOrderTripDetails", myCon)
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)


            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDriverID As New SqlParameter("@DriverID", SqlDbType.NVarChar)
            parameterDriverID.Value = DriverID
            myCommand.Parameters.Add(parameterDriverID)

            Dim parameterTruckID As New SqlParameter("@TruckID", SqlDbType.NVarChar)
            parameterTruckID.Value = TruckID
            myCommand.Parameters.Add(parameterTruckID)

            Dim parameterOrderNumber As New SqlParameter("@OrderNumber", SqlDbType.NVarChar)
            parameterOrderNumber.Value = OrderNumber
            myCommand.Parameters.Add(parameterOrderNumber)

            Dim parameterTripID As New SqlParameter("@TripID", SqlDbType.NVarChar)
            parameterTripID.Value = TripID
            myCommand.Parameters.Add(parameterTripID)











            myCon.Open()



            myCommand.ExecuteNonQuery()


            myCon.Close()

        End Sub


        Public Sub UpdateTripSheetDetails(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal OrderNumber As String, ByVal TripID As String, ByVal Active As Integer)





            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))

            Dim myCommand As New SqlCommand("UpdateTripSheetDetails", myCon)
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterOrderNumber As New SqlParameter("@OrderNumber", SqlDbType.NVarChar)
            parameterOrderNumber.Value = OrderNumber
            myCommand.Parameters.Add(parameterOrderNumber)

            Dim parameterTripID As New SqlParameter("@TripID", SqlDbType.NVarChar)
            parameterTripID.Value = TripID
            myCommand.Parameters.Add(parameterTripID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterActive As New SqlParameter("@Active", SqlDbType.Int)
            parameterActive.Value = Active
            myCommand.Parameters.Add(parameterActive)


            myCon.Open()
            myCommand.ExecuteNonQuery()
            myCon.Close()

        End Sub
#End Region

#Region "Delivery Black Out Dates"
        Public Sub AddBlackOutDeliveryDates(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal FromDate As String, ByVal ToDate As String)





            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))

            Dim myCommand As New SqlCommand("AddBlackOutDeliveryDates", myCon)
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)


            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterFromDate As New SqlParameter("@FromDate", SqlDbType.DateTime)
            parameterFromDate.Value = FromDate
            myCommand.Parameters.Add(parameterFromDate)


            Dim parameterToDate As New SqlParameter("@ToDate", SqlDbType.DateTime)
            parameterToDate.Value = ToDate
            myCommand.Parameters.Add(parameterToDate)

            myCon.Open()
            myCommand.ExecuteNonQuery()

            myCon.Close()



        End Sub





        Public Function PopulateBlackOutDeliveryDates(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As DataSet

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))


            Dim myCommand As New SqlCommand("PopulateBlackOutDeliveryDates", myCon)
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

        

            myCon.Open()

            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet()
            adapter.Fill(ds)
            myCon.Close()
            Return ds

        End Function

        Public Sub DeleteBlackOutDeliveryDates(ByVal DeliveryID As Integer)


            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("DeleteBlackOutDeliveryDates", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterDeliveryID As New SqlParameter("@DeliveryID", Data.SqlDbType.Int)
            parameterDeliveryID.Value = DeliveryID
            myCommand.Parameters.Add(parameterDeliveryID)

            myCon.Open()

            myCommand.ExecuteNonQuery()
            myCon.Close()


        End Sub
#End Region

#Region " Populate for Printing TripSheet"


        Public Function PopulateTripIDForPrinting(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal TripID As String) As DataSet

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))


            Dim myCommand As New SqlCommand("PopulateTripReport", myCon)
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterTripID As New SqlParameter("@TripID", SqlDbType.NVarChar)
            parameterTripID.Value = TripID
            myCommand.Parameters.Add(parameterTripID)



            myCon.Open()

            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet()
            adapter.Fill(ds)
            myCon.Close()
            Return ds

        End Function


        Public Function PopulateTripReport(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal TripID As String) As DataSet

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))


            Dim myCommand As New SqlCommand("PopulateTripReport", myCon)
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterTripID As New SqlParameter("@TripID", SqlDbType.NVarChar)
            parameterTripID.Value = TripID
            myCommand.Parameters.Add(parameterTripID)



            myCon.Open()

            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet()
            adapter.Fill(ds)
            myCon.Close()
            Return ds

        End Function
#End Region


#Region "PayPal Settings"
        Public Sub AddEditPayPalSettings(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal PayPal As Integer)





            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))

            Dim myCommand As New SqlCommand("AddEditPayPalSettings", myCon)
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)


            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterPayPal As New SqlParameter("@PayPal", SqlDbType.Int)
            parameterPayPal.Value = PayPal
            myCommand.Parameters.Add(parameterPayPal)


            myCon.Open()
            myCommand.ExecuteNonQuery()

            myCon.Close()



        End Sub




        Public Function PopulatePaymentSettings(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As SqlDataReader

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("PopulatePaymentSettings", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            myCon.Open()
            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs


        End Function
#End Region

#Region "PayPal Express Settings"
        Public Function AddEditPayPalExpressSettings(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal UserName As String, ByVal Password As String, ByVal Signature As String, ByVal TestUrl As String, ByVal LiveUrl As String, ByVal TestMode As Integer, ByVal WebsitetestUrl As String, ByVal WebsiteLiveUrl As String) As Integer

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("AddEditPayPalExpressSettings", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterSignature As New SqlParameter("@Signature", Data.SqlDbType.NVarChar)
            parameterSignature.Value = Signature
            myCommand.Parameters.Add(parameterSignature)

            Dim parameterUserName As New SqlParameter("@UserName", Data.SqlDbType.NVarChar)
            parameterUserName.Value = UserName
            myCommand.Parameters.Add(parameterUserName)

            Dim parameterPassword As New SqlParameter("@Password", Data.SqlDbType.NVarChar)
            parameterPassword.Value = Password
            myCommand.Parameters.Add(parameterPassword)

            Dim parameterTestUrl As New SqlParameter("@TestUrl", Data.SqlDbType.NVarChar)
            parameterTestUrl.Value = TestUrl
            myCommand.Parameters.Add(parameterTestUrl)


            Dim parameterLiveUrl As New SqlParameter("@LiveUrl", Data.SqlDbType.NVarChar)
            parameterLiveUrl.Value = LiveUrl
            myCommand.Parameters.Add(parameterLiveUrl)


            Dim parameterWebsitetestUrl As New SqlParameter("@WebsitetestUrl", Data.SqlDbType.NVarChar)
            parameterWebsitetestUrl.Value = WebsitetestUrl
            myCommand.Parameters.Add(parameterWebsitetestUrl)


            Dim parameterWebsiteLiveUrl As New SqlParameter("@WebsiteLiveUrl", Data.SqlDbType.NVarChar)
            parameterWebsiteLiveUrl.Value = WebsiteLiveUrl
            myCommand.Parameters.Add(parameterWebsiteLiveUrl)

            Dim parameterTestMode As New SqlParameter("@TestMode", Data.SqlDbType.Int)
            parameterTestMode.Value = TestMode
            myCommand.Parameters.Add(parameterTestMode)





            Dim result As Integer
            result = myCommand.ExecuteNonQuery()







            ConString.Close()





        End Function


        Public Function PopulatePayPalExpressSettings(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As SqlDataReader



            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulatePayPalExpressSettings", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs


        End Function
#End Region


#Region " Security Permissions"



        Public Function PopulateSecurityPermissions(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As DataSet

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))


            Dim myCommand As New SqlCommand("PopulateSecurityPermissions", myCon)
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)


            myCon.Open()

            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet()
            adapter.Fill(ds)
            myCon.Close()
            Return ds

        End Function

        Public Sub LockingUser(ByVal UserName As String, ByVal UserPass As String, ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String)
            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("LockLoginUser", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterLoginUser As New SqlParameter("@LoginName", Data.SqlDbType.NVarChar)
            parameterLoginUser.Value = UserName
            myCommand.Parameters.Add(parameterLoginUser)

            Dim parameterLoginUserPassword As New SqlParameter("@LoginUserPassword", Data.SqlDbType.NVarChar)
            parameterLoginUserPassword.Value = UserPass
            myCommand.Parameters.Add(parameterLoginUserPassword)

            myCon.Open()
            myCommand.ExecuteNonQuery()
            myCon.Close()

        End Sub


        Public Function RequestIPAddress(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal EmployeeID As String) As SqlDataReader
            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("RequestIPAddress", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterEmployeeID As New SqlParameter("@EmployeeID", Data.SqlDbType.NVarChar)
            parameterEmployeeID.Value = EmployeeID
            myCommand.Parameters.Add(parameterEmployeeID)
            myCon.Open()

            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs



        End Function


        Public Function PopulateEmployeesByPermission(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal EmployeeID As String) As SqlDataReader

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()
            Dim myCommand As New SqlCommand("PopulateEmployeesByPermission", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterEmployeeID As New SqlParameter("@EmployeeID", Data.SqlDbType.NVarChar, 36)
            parameterEmployeeID.Value = EmployeeID
            myCommand.Parameters.Add(parameterEmployeeID)

            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs



        End Function
#End Region



#Region "Updating ActiveTime for Logged In User"
        Public Function UpdateActiveTime(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal EmployeeID As String, ByVal BrowserID As String) As Integer
            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()
            Dim myCommand As New SqlCommand("UpdateActiveTime", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterEmployeeID As New SqlParameter("@EmployeeID", Data.SqlDbType.NVarChar, 36)
            parameterEmployeeID.Value = EmployeeID
            myCommand.Parameters.Add(parameterEmployeeID)

            Dim parameterBrowserID As New SqlParameter("@BrowserID", Data.SqlDbType.NVarChar, 100)
            parameterBrowserID.Value = BrowserID
            myCommand.Parameters.Add(parameterBrowserID)

            Dim parameterReturnValue As New SqlParameter("@ReturnValue", SqlDbType.Int)
            parameterReturnValue.Direction = ParameterDirection.Output
            myCommand.Parameters.Add(parameterReturnValue)

            myCommand.ExecuteNonQuery()

            Dim Rtnvalue As Integer
            Rtnvalue = Integer.Parse(parameterReturnValue.Value.ToString())


            ConString.Close()
            Return Rtnvalue
        End Function

        Public Function RetriveActiveUserDetails(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal EmployeeID As String, ByVal BrowserID As String) As SqlDataReader
            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))

            Dim myCommand As New SqlCommand("RetriveActiveTime", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterEmployeeID As New SqlParameter("@EmployeeID", Data.SqlDbType.NVarChar, 36)
            parameterEmployeeID.Value = EmployeeID
            myCommand.Parameters.Add(parameterEmployeeID)

            Dim parameterBrowserID As New SqlParameter("@BrowserID", Data.SqlDbType.NVarChar, 100)
            parameterBrowserID.Value = BrowserID
            myCommand.Parameters.Add(parameterBrowserID)

            ConString.Open()
            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs

        End Function

#End Region

#Region "SiteMap"
        Public Function PopulateSiteMap(ByVal ModuleName As String) As SqlDataReader
            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))

            Dim myCommand As New SqlCommand("PopulateSiteMap", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterModuleName As New SqlParameter("@ModuleName", Data.SqlDbType.NVarChar, 36)
            parameterModuleName.Value = ModuleName
            myCommand.Parameters.Add(parameterModuleName)



            ConString.Open()
            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs
          

        End Function


#End Region

#Region "WireService"
        Public Sub DeleteWireService(ByVal WID As Integer)
            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("DeleteWireService", ConString)
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterWID As New SqlParameter("@WID", SqlDbType.Int)
            parameterWID.Value = WID
            myCommand.Parameters.Add(parameterWID)

            myCommand.ExecuteNonQuery()

            ConString.Close()
        End Sub
        Public Function PopulateWireServiceByID(ByVal WID As Integer) As SqlDataReader

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("PopulateWireServiceByID", ConString)
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterWID As New SqlParameter("@WID", SqlDbType.Int)
            parameterWID.Value = WID
            myCommand.Parameters.Add(parameterWID)



            Dim rs As SqlDataReader
            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return rs


        End Function



        Public Function AddEditWireService(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal WireServiceID As String, ByVal WireServiceDescription As String, ByVal AccountNumber As String, ByVal AddEdit As String, ByVal WireActive As Integer)


            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()

            Dim myCommand As New SqlCommand("AddEditWireService", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)


            Dim parameterWireServiceID As New SqlParameter("@WireServiceID", Data.SqlDbType.NVarChar)
            parameterWireServiceID.Value = WireServiceID
            myCommand.Parameters.Add(parameterWireServiceID)

            Dim parameterWireServiceDescription As New SqlParameter("@WireServiceDescription", Data.SqlDbType.NVarChar)
            parameterWireServiceDescription.Value = WireServiceDescription
            myCommand.Parameters.Add(parameterWireServiceDescription)

            Dim parameterAccountNumber As New SqlParameter("@AccountNumber", Data.SqlDbType.NVarChar)
            parameterAccountNumber.Value = AccountNumber
            myCommand.Parameters.Add(parameterAccountNumber)

            Dim parameterAddEdit As New SqlParameter("@AddEdit", Data.SqlDbType.NVarChar)
            parameterAddEdit.Value = AddEdit
            myCommand.Parameters.Add(parameterAddEdit)


            Dim paramWireActive As New SqlParameter("@WireActive", Data.SqlDbType.Int)
            paramWireActive.Value = WireActive
            myCommand.Parameters.Add(paramWireActive)



            Dim paramReturnValue As New SqlParameter("@ReturnValue", Data.SqlDbType.Int)
            paramReturnValue.Direction = ParameterDirection.Output
            myCommand.Parameters.Add(paramReturnValue)





            myCommand.ExecuteNonQuery()



            Dim res As Integer



            ConString.Close()
            If paramReturnValue.Value.ToString() <> "" Then
                res = Convert.ToDecimal(paramReturnValue.Value)
            End If


            Return res


        End Function
#End Region

#Region "Add Ons for work ticket"



        Public Sub AddWorkTicketAddons(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal OrderNumber As String, ByVal OrderLineNumber As String, ByVal addonName As String, ByVal addonPrice As String, ByVal addonQty As String)

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("AddWorkTicketAddons", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
            parameterOrderNumber.Value = OrderNumber
            myCommand.Parameters.Add(parameterOrderNumber)

            Dim parameterOrderLineNumber As New SqlParameter("@OrderLineNumber", Data.SqlDbType.NVarChar)
            parameterOrderLineNumber.Value = OrderLineNumber
            myCommand.Parameters.Add(parameterOrderLineNumber)

            Dim parameteraddonName As New SqlParameter("@addonName", Data.SqlDbType.NVarChar)
            parameteraddonName.Value = addonName
            myCommand.Parameters.Add(parameteraddonName)

            Dim parameteraddonQty As New SqlParameter("@addonQty", Data.SqlDbType.NVarChar)
            parameteraddonQty.Value = addonQty
            myCommand.Parameters.Add(parameteraddonQty)

            Dim parameteraddonPrice As New SqlParameter("@addonPrice", Data.SqlDbType.NVarChar)
            parameteraddonPrice.Value = addonPrice
            myCommand.Parameters.Add(parameteraddonPrice)

            myCon.Open()
            myCommand.ExecuteNonQuery()
            myCon.Close()

        End Sub


        Public Sub DeleteWorkTicketAddOns(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal OrderNumber As String)

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("DeleteWorkTicketAddOns", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
            parameterOrderNumber.Value = OrderNumber
            myCommand.Parameters.Add(parameterOrderNumber)

            myCon.Open()
            myCommand.ExecuteNonQuery()
            myCon.Close()
        End Sub
#End Region


#Region "Price for Updates"

        Public Function PopulatePriceRange(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal ItemID As String) As SqlDataReader

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("PopulatePriceRange", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterItemID As New SqlParameter("@ItemID", Data.SqlDbType.NVarChar)
            parameterItemID.Value = ItemID
            myCommand.Parameters.Add(parameterItemID)



            myCon.Open()

            Dim rs As SqlDataReader

            rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            Return rs


        End Function


#End Region

#Region "SearchInventoryItems"
        Public Function SearchInventoryItems(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal SearchExpression As String, ByVal FieldCondition As String, ByVal FieldName As String) As DataSet

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("SearchInventoryItems", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterfieldName As New SqlParameter("@fieldName", Data.SqlDbType.NVarChar)
            parameterfieldName.Value = FieldName
            myCommand.Parameters.Add(parameterfieldName)

            Dim parameterfieldexpression As New SqlParameter("@SearchExpression", Data.SqlDbType.NVarChar)
            parameterfieldexpression.Value = SearchExpression
            myCommand.Parameters.Add(parameterfieldexpression)



            Dim parameterCondition As New SqlParameter("@FieldCondition", Data.SqlDbType.NVarChar)
            parameterCondition.Value = FieldCondition
            myCommand.Parameters.Add(parameterCondition)



            myCon.Open()

            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet()
            adapter.Fill(ds)
            myCon.Close()
            Return ds

        End Function

#End Region

#Region "Delete Cancelled Orders"

        Public Sub DeleteCancelledOrder(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal OrderNumber As String)

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("[enterprise].[Order_Delete]", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
            parameterOrderNumber.Value = OrderNumber
            myCommand.Parameters.Add(parameterOrderNumber)

            myCon.Open()
            myCommand.ExecuteNonQuery()
            myCon.Close()
        End Sub
#End Region


#Region "Company Images Upload"
        Public Sub CompanyImagesUpload(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal CompanySmallImage As String, ByVal CompanyMediumImage As String, ByVal CompanyLargeImage As String, ByVal CompanyWebsiteImage As String, ByVal CompanyMobilImage As String, ByVal CompanyHRImage As String)
            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("CompanyImagesUpload", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

        

            Dim parameterSmallImageName As New SqlParameter("@CompanySmallImage", Data.SqlDbType.NVarChar)
            parameterSmallImageName.Value = CompanySmallImage
            myCommand.Parameters.Add(parameterSmallImageName)

            Dim parameterMediumImageName As New SqlParameter("@CompanyMediumImage", Data.SqlDbType.NVarChar)
            parameterMediumImageName.Value = CompanyMediumImage
            myCommand.Parameters.Add(parameterMediumImageName)

            Dim parameterLargeImageName As New SqlParameter("@CompanyLargeImage", Data.SqlDbType.NVarChar)
            parameterLargeImageName.Value = CompanyLargeImage
            myCommand.Parameters.Add(parameterLargeImageName)

            Dim parameterCompanyWebsiteImage As New SqlParameter("@CompanyWebsiteImage", Data.SqlDbType.NVarChar)
            parameterCompanyWebsiteImage.Value = CompanyWebsiteImage
            myCommand.Parameters.Add(parameterCompanyWebsiteImage)

            Dim parameterCompanyMobilImage As New SqlParameter("@CompanyMobilImage", Data.SqlDbType.NVarChar)
            parameterCompanyMobilImage.Value = CompanyMobilImage
            myCommand.Parameters.Add(parameterCompanyMobilImage)

            Dim parameterCompanyHRImage As New SqlParameter("@CompanyHRImage", Data.SqlDbType.NVarChar)
            parameterCompanyHRImage.Value = CompanyHRImage
            myCommand.Parameters.Add(parameterCompanyHRImage)

        

            myCon.Open()

            myCommand.ExecuteNonQuery()
            myCon.Close()

        End Sub

#End Region


#Region "EnclosureCard Type"

        'Public Function PopulateEnclosureCardType(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String)  as

        '    Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        '    Dim myCommand As New SqlCommand("[enterprise].[Order_Delete]", myCon)
        '    myCommand.CommandType = Data.CommandType.StoredProcedure

        '    Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
        '    parameterCompanyID.Value = CompanyID
        '    myCommand.Parameters.Add(parameterCompanyID)

        '    Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
        '    parameterDivisionID.Value = DivisionID
        '    myCommand.Parameters.Add(parameterDivisionID)

        '    Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
        '    parameterDepartmentID.Value = DepartmentID
        '    myCommand.Parameters.Add(parameterDepartmentID)

        '    Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
        '    parameterOrderNumber.Value = OrderNumber
        '    myCommand.Parameters.Add(parameterOrderNumber)
        '    Dim EnclosureCardType As String = ""

        '    myCon.Open()
        '    EnclosureCardType = myCommand.ExecuteScalar()

        '    myCon.Close()

        '    Return EnclosureCardType


        'End Function
#End Region


#Region "Duplicate Order Details"


        Public Function DuplicateOrderDetails(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal OrderNumber As String, ByVal Address As String, ByVal ApprovalNumber As String) As String

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("DuplicateOrderDetails", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
            parameterOrderNumber.Value = OrderNumber
            myCommand.Parameters.Add(parameterOrderNumber)

            Dim parameterAddress As New SqlParameter("@Address", Data.SqlDbType.NVarChar)
            parameterAddress.Value = Address
            myCommand.Parameters.Add(parameterAddress)

            Dim parameterApprovalNumber As New SqlParameter("@ApprovalNumber", Data.SqlDbType.NVarChar)
            parameterApprovalNumber.Value = ApprovalNumber
            myCommand.Parameters.Add(parameterApprovalNumber)


            Dim paramReturnValue As New SqlParameter("@ReturnOrderNumber", Data.SqlDbType.NVarChar, 50)
            paramReturnValue.Direction = ParameterDirection.Output
            myCommand.Parameters.Add(paramReturnValue)

            myCon.Open()
            myCommand.ExecuteNonQuery()
            myCon.Close()
            Dim res As String = ""

            If paramReturnValue.Value.ToString() <> "" Then
                res = Convert.ToDecimal(paramReturnValue.Value)
            End If


            Return res

        End Function

#End Region


#Region "Cancel Order"
        Public Sub CancelOrder(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal OrdNumber As String)

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("enterprise.Order_Delete", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
            parameterOrderNumber.Value = OrdNumber
            myCommand.Parameters.Add(parameterOrderNumber)

            myCon.Open()
            myCommand.ExecuteNonQuery()
            myCon.Close()
        End Sub

#End Region


#Region "Populate Currency Types"

        Public Function PopulateCurrencyTypes(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As DataSet

            Dim myConnection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))

            Dim myCommand As New SqlCommand("PopulateCurrencyType", myConnection)
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)


            Dim da As New SqlDataAdapter()
            da.SelectCommand = myCommand

            Dim ds As New DataSet()
            da.Fill(ds)



            Return ds


        End Function
#End Region



        Public Function PopulatePaypalType(ByVal CompanyID, ByVal DivisionID, ByVal DepartmentID, ByVal OrdN) As DataTable
            Dim myConnection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))

            Dim myCommand As New SqlCommand("PopulatePaypalType", myConnection)
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
            parameterOrderNumber.Value = OrdN
            myCommand.Parameters.Add(parameterOrderNumber)

            Dim da As New SqlDataAdapter()
            da.SelectCommand = myCommand

            Dim dt As New DataTable
            da.Fill(dt)



            Return dt
        End Function



        Public Sub UpdateGrandTotal(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal OrdNumber As String, ByVal TaxGroupID As String, ByVal TaxPercent As Decimal, ByVal TaxAmount As Decimal, ByVal Total As Decimal, ByVal Service As Decimal, ByVal Delivery As Decimal, ByVal Relay As Decimal)


            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("UpdateGrandTotal", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
            parameterOrderNumber.Value = OrdNumber
            myCommand.Parameters.Add(parameterOrderNumber)

            Dim parameterTaxGroupID As New SqlParameter("@TaxGroupID", Data.SqlDbType.NVarChar)
            parameterTaxGroupID.Value = TaxGroupID
            myCommand.Parameters.Add(parameterTaxGroupID)

            Dim parameterTaxPercent As New SqlParameter("@TaxPercent", Data.SqlDbType.Float)
            parameterTaxPercent.Value = TaxPercent
            myCommand.Parameters.Add(parameterTaxPercent)

            Dim parameterTaxAmount As New SqlParameter("@TaxAmount", Data.SqlDbType.Money)
            parameterTaxAmount.Value = TaxAmount
            myCommand.Parameters.Add(parameterTaxAmount)

            Dim parameterTotal As New SqlParameter("@Total", Data.SqlDbType.Money)
            parameterTotal.Value = Total
            myCommand.Parameters.Add(parameterTotal)

            Dim parameterRelay As New SqlParameter("@Relay", Data.SqlDbType.Money)
            parameterRelay.Value = Relay
            myCommand.Parameters.Add(parameterRelay)

            Dim parameterDelivery As New SqlParameter("@Delivery", Data.SqlDbType.Money)
            parameterDelivery.Value = Delivery
            myCommand.Parameters.Add(parameterDelivery)

            Dim parameterService As New SqlParameter("@Service", Data.SqlDbType.Money)
            parameterService.Value = Service
            myCommand.Parameters.Add(parameterService)

            myCon.Open()
            myCommand.ExecuteNonQuery()
            myCon.Close()

        End Sub

#Region "Editing ZipCode Details"
        Public Function PopulateZipCodeDetails(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal ZipCode As String) As DataSet

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))


            Dim myCommand As New SqlCommand("PopulateZipCodeDetails", myCon)
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterZipCode As New SqlParameter("@ZipCode", SqlDbType.NVarChar)
            parameterZipCode.Value = ZipCode
            myCommand.Parameters.Add(parameterZipCode)



            myCon.Open()

            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet()
            adapter.Fill(ds)
            myCon.Close()
            Return ds

        End Function

        Public Sub UpdateZipCodeDetails(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal Zip As String, ByVal EveryDayRate As String, ByVal HolidayRate As String, ByVal AfterCutOffRate As String, ByVal CourierRate As String, ByVal CutOffTime As String, ByVal TimeZone As String, ByVal DST As String, ByVal Weekday As String, ByVal Saturday As String, ByVal Sunday As String, ByVal Holiday As String, ByVal Block As String, ByVal PrintPoolTicket As String)


            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("UpdateZipCodeDetails", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterZip As New SqlParameter("@Zip", Data.SqlDbType.NVarChar)
            parameterZip.Value = Zip
            myCommand.Parameters.Add(parameterZip)



            Dim parameterEveryDayRate As New SqlParameter("@EveryDayRate", Data.SqlDbType.NVarChar)
            parameterEveryDayRate.Value = EveryDayRate
            myCommand.Parameters.Add(parameterEveryDayRate)

            Dim parameterHolidayRate As New SqlParameter("@HolidayRate", Data.SqlDbType.NVarChar)
            parameterHolidayRate.Value = HolidayRate
            myCommand.Parameters.Add(parameterHolidayRate)

            Dim parameterAfterCutOffRate As New SqlParameter("@AfterCutOffRate ", Data.SqlDbType.NVarChar)
            parameterAfterCutOffRate.Value = AfterCutOffRate
            myCommand.Parameters.Add(parameterAfterCutOffRate)

            Dim parameterCourierRate As New SqlParameter("@CourierRate ", Data.SqlDbType.NVarChar)
            parameterCourierRate.Value = CourierRate
            myCommand.Parameters.Add(parameterCourierRate)

            Dim parameterCutOffTime As New SqlParameter("@CutOffTime ", Data.SqlDbType.NVarChar)
            parameterCutOffTime.Value = CutOffTime
            myCommand.Parameters.Add(parameterCutOffTime)
            Dim parameterTimeZone As New SqlParameter("@TimeZone ", Data.SqlDbType.NVarChar)
            parameterTimeZone.Value = TimeZone
            myCommand.Parameters.Add(parameterTimeZone)

            Dim parameterDST As New SqlParameter("@DST ", Data.SqlDbType.NVarChar)
            parameterDST.Value = DST
            myCommand.Parameters.Add(parameterDST)

            Dim parameterWeekday As New SqlParameter("@Weekday ", Data.SqlDbType.NVarChar)
            parameterWeekday.Value = Weekday
            myCommand.Parameters.Add(parameterWeekday)

            Dim parameterSaturday As New SqlParameter("@Saturday ", Data.SqlDbType.NVarChar)
            parameterSaturday.Value = Saturday
            myCommand.Parameters.Add(parameterSaturday)

            Dim parameterSunday As New SqlParameter("@Sunday ", Data.SqlDbType.NVarChar)
            parameterSunday.Value = Sunday
            myCommand.Parameters.Add(parameterSunday)

            Dim parameterHoliday As New SqlParameter("@Holiday ", Data.SqlDbType.NVarChar)
            parameterHoliday.Value = Holiday
            myCommand.Parameters.Add(parameterHoliday)

            Dim parameterBlock As New SqlParameter("@Block ", Data.SqlDbType.NVarChar)
            parameterBlock.Value = Block
            myCommand.Parameters.Add(parameterBlock)

            Dim parameterPrintPoolTicket As New SqlParameter("@PrintPoolTicket ", Data.SqlDbType.NVarChar)
            parameterPrintPoolTicket.Value = PrintPoolTicket
            myCommand.Parameters.Add(parameterPrintPoolTicket)



            myCon.Open()

            myCommand.ExecuteNonQuery()

            myCon.Close()



        End Sub
#End Region


        Public Sub UpdateOrderDetails(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal OrdNumber As String, ByVal OrderLineNumber As Integer, ByVal txtIDItem As String, ByVal grdDescription As String, ByVal grdPrice As Decimal, ByVal OrderQty As Decimal, ByVal SubTotal As Decimal, ByVal grdTotal As Decimal, ByVal ItemDiscountPerc As Decimal, ByVal ItemDiscountFlatOrPercent As String)

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("UpdateOrderDetails", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterOrderLineNumber As New SqlParameter("@OrderLineNumber ", Data.SqlDbType.Int)
            parameterOrderLineNumber.Value = OrderLineNumber
            myCommand.Parameters.Add(parameterOrderLineNumber)

            Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
            parameterOrderNumber.Value = OrdNumber
            myCommand.Parameters.Add(parameterOrderNumber)

            Dim parameterItemID As New SqlParameter("@ItemID", Data.SqlDbType.NVarChar)
            parameterItemID.Value = txtIDItem
            myCommand.Parameters.Add(parameterItemID)

            Dim parameterDescription As New SqlParameter("@Description", Data.SqlDbType.NVarChar)
            parameterDescription.Value = grdDescription
            myCommand.Parameters.Add(parameterDescription)

            Dim parameterOrderQty As New SqlParameter("@OrderQty", Data.SqlDbType.Float)
            parameterOrderQty.Value = OrderQty
            myCommand.Parameters.Add(parameterOrderQty)

            Dim parameterItemUnitPrice As New SqlParameter("@ItemUnitPrice", Data.SqlDbType.Money)
            parameterItemUnitPrice.Value = grdPrice
            myCommand.Parameters.Add(parameterItemUnitPrice)



            Dim parameterSubTotal As New SqlParameter("@SubTotal", Data.SqlDbType.Money)
            parameterSubTotal.Value = SubTotal
            myCommand.Parameters.Add(parameterSubTotal)


            Dim parameterTotal As New SqlParameter("@Total", Data.SqlDbType.Money)
            parameterTotal.Value = grdTotal
            myCommand.Parameters.Add(parameterTotal)


            Dim parameterItemDiscountPerc As New SqlParameter("@ItemDiscountPerc", Data.SqlDbType.Float)
            parameterItemDiscountPerc.Value = ItemDiscountPerc
            myCommand.Parameters.Add(parameterItemDiscountPerc)

            'JMT code on 14th August 2008 Starts here
            Dim parameterItemDiscountFlatOrPercent As New SqlParameter("@DiscountFlatOrPercentage", Data.SqlDbType.NVarChar)
            parameterItemDiscountFlatOrPercent.Value = ItemDiscountFlatOrPercent
            myCommand.Parameters.Add(parameterItemDiscountFlatOrPercent)
            'JMT code on 14th August 2008 Ends here

            myCon.Open()

            myCommand.ExecuteNonQuery()

            myCon.Close()

        End Sub



        Public Function PopulateCustomerDetailsSearch(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal SearchExpression As String, ByVal FieldName As String) As DataSet

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("PopulateCustomerDetailsSearch", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterfieldName As New SqlParameter("@fieldName", Data.SqlDbType.NVarChar)
            parameterfieldName.Value = FieldName
            myCommand.Parameters.Add(parameterfieldName)

            Dim parameterfieldexpression As New SqlParameter("@SearchExpression", Data.SqlDbType.NVarChar)
            parameterfieldexpression.Value = SearchExpression
            myCommand.Parameters.Add(parameterfieldexpression)



            myCon.Open()

            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet()
            adapter.Fill(ds)
            myCon.Close()
            Return ds

        End Function


        Public Function DefaultWireOutCharge(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As DataSet

            Dim myConnection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))

            Dim myCommand As New SqlCommand("DefaultWireOutCharge", myConnection)
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)


            Dim da As New SqlDataAdapter()
            da.SelectCommand = myCommand

            Dim ds As New DataSet()
            da.Fill(ds)



            Return ds


        End Function



        'Credit Card Validation number updating to Order Header table


#Region "Updating  Credit card Validation Code"



        Public Function UpdateCreditCardApprovalNumberAndIpAddress(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal OrdNumber As String, ByVal ApprovalNumber As String, ByVal Address As String) As Boolean
            Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim connec As New SqlConnection(constr)
            Dim qry As String
            qry = "Update OrderHeader set  CreditCardApprovalNumber=@ApprovalNumber,IpAddress=@Address,[CreditCardCSVNumber]=''   where CompanyID=@CompanyID and DivisionID= @DivisionID and DepartmentID=@DepartmentID  and OrderNumber=@OrderNumber "
            Dim com As SqlCommand
            com = New SqlCommand(qry, connec)
         
            com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = CompanyID
            com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = DivisionID
            com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = DepartmentID
            com.Parameters.Add(New SqlParameter("@OrderNumber", SqlDbType.NVarChar)).Value = OrdNumber
            com.Parameters.Add(New SqlParameter("@ApprovalNumber", SqlDbType.NVarChar)).Value = ApprovalNumber
            com.Parameters.Add(New SqlParameter("@Address", SqlDbType.NVarChar)).Value = Address


            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True

        End Function



        Public Function InsertCreditCardApprovalNumberAndIpAddress(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal OrdNumber As String, ByVal ApprovalNumber As String, ByVal Address As String) As Boolean
            Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim connec As New SqlConnection(constr)
            Dim qry As String
            qry = "INSERT INTO [Enterprise].[dbo].[TraceCreditCardApprovalNumber] ([CompanyID],[DivisionID],[DepartmentID],[OrderNumber],[CreditCardApprovalNumber],[IpAddress]) VALUES (@CompanyID,@DivisionID,@DepartmentID,@OrderNumber,@ApprovalNumber,@Address) "
            Dim com As SqlCommand
            com = New SqlCommand(qry, connec)

            com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = CompanyID
            com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = DivisionID
            com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = DepartmentID
            com.Parameters.Add(New SqlParameter("@OrderNumber", SqlDbType.NVarChar)).Value = OrdNumber
            com.Parameters.Add(New SqlParameter("@ApprovalNumber", SqlDbType.NVarChar)).Value = ApprovalNumber
            com.Parameters.Add(New SqlParameter("@Address", SqlDbType.NVarChar)).Value = Address

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True

        End Function





        Public Sub UpdateCreditCardTransactions(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal OrdNumber As String, ByVal ApprovalNumber As String, ByVal Address As String)

            InsertCreditCardApprovalNumberAndIpAddress(CompanyID, DepartmentID, DivisionID, OrdNumber, ApprovalNumber, Address)


            UpdateCreditCardApprovalNumberAndIpAddress(CompanyID, DepartmentID, DivisionID, OrdNumber, ApprovalNumber, Address)

            Exit Sub

            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("UpdateCreditCardTransactions", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterApprovalNumber As New SqlParameter("@ApprovalNumber ", Data.SqlDbType.NVarChar)
            parameterApprovalNumber.Value = ApprovalNumber
            myCommand.Parameters.Add(parameterApprovalNumber)

            Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
            parameterOrderNumber.Value = OrdNumber
            myCommand.Parameters.Add(parameterOrderNumber)

            Dim parameterAddress As New SqlParameter("@Address", Data.SqlDbType.NVarChar)
            parameterAddress.Value = Address
            myCommand.Parameters.Add(parameterAddress)

            myCon.Open()

            myCommand.ExecuteNonQuery()

            myCon.Close()

        End Sub

#End Region

     
        ' Credit Card making Offline 20/12/2007

#Region "Credit Card Offline"
        Public Function CheckCreditCardOffline(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, Optional ByVal UsedIn As String = "") As DataSet

            Dim myConnection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))

            Dim myCommand As New SqlCommand("CheckCreditCardOffline", myConnection)
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            If UsedIn <> String.Empty Then myCommand.Parameters.AddWithValue("UsedIn", UsedIn)

            Dim da As New SqlDataAdapter()
            da.SelectCommand = myCommand

            Dim ds As New DataSet()
            da.Fill(ds)



            Return ds


        End Function

 Public Function PopulateAllWireServices(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As DataSet

            Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            ConString.Open()
            Dim myCommand As New SqlCommand("PopulateAllWireServices", ConString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)



            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet

            adapter.Fill(ds)
            ConString.Close()

            Return ds

        End Function

#End Region

        Public Function PopulateQtyRange() As DataTable
            Dim dt As New DataTable

            Dim connec As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))


            'Dim qry As String = "Select QuantityStartFrom,QuantityEndTo from OrderItemQtyRange Where CompanyID=@f1  AND   DivisionID =@f2  AND  DepartmentID =@f3 "
            Dim qry As String = "Select NextNumberValue from CompaniesNextNumbers Where CompanyID=@f1  AND   DivisionID =@f2  AND  DepartmentID =@f3  and NextNumberName=@f4 "
            Dim com As SqlCommand
            com = New SqlCommand(qry, connec)
            Try
                com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID.Trim()
                com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID.Trim()
                com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID.Trim()
                com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = "ItemQtyLimit"


                com.CommandType = CommandType.Text

                Dim da As New SqlDataAdapter(com)

                da.Fill(dt)

                Return dt

            Catch ex As Exception
                Dim msg As String
                msg = ex.Message
                HttpContext.Current.Response.Write(msg)
                Return dt
            End Try
            Return dt
        End Function

	Public Function POSPickupQuickList(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal Condition As String, ByVal fieldName As String, ByVal fieldexpression As String, ByVal FromDate As String, ByVal ToDate As String, ByVal AllDate As Integer, ByVal SortField As String, ByVal SortDirection As String, ByVal Payment As String, ByVal Delivery As String, LocationID As String) As DataSet

            Dim conString As New SqlConnection
            conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

            Dim myCommand As New SqlCommand("[enterprise].[POSPiuckupQuickList]", conString)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim prPayment As New SqlParameter("@Payment", Data.SqlDbType.NVarChar)
            prPayment.Value = Payment
            myCommand.Parameters.Add(prPayment)

            Dim prDelivery As New SqlParameter("@Delivery", Data.SqlDbType.NVarChar)
            prDelivery.Value = Delivery
            myCommand.Parameters.Add(prDelivery)

            Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
            parameterCompanyID.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyID)

            Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
            parameterDivisionID.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionID)

            Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
            parameterDepartmentID.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentID)

            Dim parameterCondition As New SqlParameter("@Condition", Data.SqlDbType.NVarChar)
            parameterCondition.Value = Condition
            myCommand.Parameters.Add(parameterCondition)


            Dim parameterfieldName As New SqlParameter("@fieldName", Data.SqlDbType.NVarChar)
            parameterfieldName.Value = fieldName
            myCommand.Parameters.Add(parameterfieldName)

            Dim parameterfieldexpression As New SqlParameter("@fieldexpression", Data.SqlDbType.NVarChar)
            parameterfieldexpression.Value = fieldexpression
            myCommand.Parameters.Add(parameterfieldexpression)


            Dim parameterFromDate As New SqlParameter("@FromDate", Data.SqlDbType.NVarChar)
            parameterFromDate.Value = FromDate
            myCommand.Parameters.Add(parameterFromDate)

            Dim parameterToDate As New SqlParameter("@ToDate", Data.SqlDbType.NVarChar)
            parameterToDate.Value = ToDate
            myCommand.Parameters.Add(parameterToDate)


            Dim parameterAllDate As New SqlParameter("@AllDate", Data.SqlDbType.Int)
            parameterAllDate.Value = AllDate
            myCommand.Parameters.Add(parameterAllDate)


            Dim parameterSortField As New SqlParameter("@SortField", Data.SqlDbType.NVarChar)
            parameterSortField.Value = SortField
            myCommand.Parameters.Add(parameterSortField)

            Dim parameterSortDirection As New SqlParameter("@SortDirection", Data.SqlDbType.NVarChar)
            parameterSortDirection.Value = SortDirection
            myCommand.Parameters.Add(parameterSortDirection)

	    myCommand.Parameters.AddWithValue("@LocationID", LocationID)

            Dim adapter As New SqlDataAdapter(myCommand)
            Dim ds As New DataSet()
            adapter.Fill(ds)
            conString.Close()

            Return ds


        End Function

         End Class

End Namespace



