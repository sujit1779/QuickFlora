Imports Microsoft.VisualBasic
Imports Paygateway

Public Class PPIPayMover

    Public ACCOUNT_TOKEN As String = "195325FCC230184964CAB3A8D93EEB31888C42C714E39CBBB2E541884485D04B"

    ' Request variables
    ' Order details
    Public orderID As String = ""
    Public purchaseOrderNumber As String = ""
    Public orderUserID As String = ""
    Public invoiceNumber As String = ""
    Public referenceID As String = ""
    ' Credit card details
    Public creditCardNumber As String = ""
    Public expireMonth As String = ""
    Public expireYear As String = ""
    Public creditCardVerificationNumber As String = ""
    ' Purchase details 
    Public chargeTotal As String = ""
    Public chargeType As String = ""
    Public taxAmount As String = ""
    Public stateTax As String = ""
    Public taxExempt As String = ""
    Public shippingCharge As String = ""
    Public transactionConditionCode As String = ""
    Public buyerCode As String = ""
    Public orderDescription As String = ""
    ' Service details
    Public folioNumber As String = ""
    Public industry As String = ""
    Public chargeTotalIncludesRestaurant As String = ""
    Public chargeTotalIncludesGiftshop As String = ""
    Public chargeTotalIncludesMinibar As String = ""
    Public chargeTotalIncludesPhone As String = ""
    Public chargeTotalIncludesLaundry As String = ""
    Public chargeTotalIncludesOther As String = ""
    Public serviceRate As String = ""
    Public serviceStartYear As String = ""
    Public serviceStartMonth As String = ""
    Public serviceStartDay As String = ""
    Public serviceEndYear As String = ""
    Public serviceEndMonth As String = ""
    Public serviceEndDay As String = ""
    Public serviceNoShow As String = ""
    ' Billing info
    Public billCustomerTitle As String = ""
    Public billFirstName As String = ""
    Public billMiddleName As String = ""
    Public billLastName As String = ""
    Public billCompany As String = ""
    Public billAddressOne As String = ""
    Public billAddressTwo As String = ""
    Public billCity As String = ""
    Public billStateOrProvince As String = ""
    Public billCountry As String = ""
    Public billZipOrPostalCode As String = ""
    Public billPhone As String = ""
    Public billFax As String = ""
    Public billEmail As String = ""
    Public billNote As String = ""
    'Shipping info
    Public shipCustomerTitle As String = ""
    Public shipFirstName As String = ""
    Public shipMiddleName As String = ""
    Public shipLastName As String = ""
    Public shipCompany As String = ""
    Public shipAddressOne As String = ""
    Public shipAddressTwo As String = ""
    Public shipCity As String = ""
    Public shipStateOrProvince As String = ""
    Public shipCountry As String = ""
    Public shipZipOrPostalCode As String = ""
    Public shipPhone As String = ""
    Public shipFax As String = ""
    Public shipEmail As String = ""
    Public shipNote As String = ""
    Public bankApprovalCode As String = ""
    Public duplicateCheck As String = ""
    Public deviceTerminalID As String = ""
    Public preferredServicesCustomer As String = ""

    ' Set proxy if specified
    Public proxyUsed = False
    Public proxyHost As String = ""
    Public proxyPort As String = ""

    ' Response variables
    Public resOrderID As String = ""
    Public resResponseCode As String = ""
    Public resSecondaryResponseCode As String = ""
    Public resResponseCodeText As String = ""
    Public resBankApprovalCode As String = ""
    Public resBankTransactionID As String = ""
    Public resCreditCardVerificationResponse As String = ""
    Public resAVSCode As String = ""
    Public resISOCode As String = ""
    Public resBatchID As String = ""
    Public resReferenceID As String = ""
    Public resTimeStamp As String = ""
    Public resRetryRecommended As String = ""

    Public resState As String = ""
    Public resAuthorizedAmount As String = ""
    Public resOriginalAuthorizedAmount As String = ""
    Public resCapturedAmount As String = ""
    Public resCreditedAmount As String = ""
    Public resTimeStampCreated As String = ""

    Public CompanyID As String
    Public DivisionID As String
    Public DepartmentID As String

    Public Track1 As String = ""
    Public Track2 As String = ""

    Public Function Process_Transaction() As Boolean






        Dim ccreq As New CreditCardRequest()
        Dim ccresp As CreditCardResponse

        If orderID <> "" Then
            ccreq.setOrderId(orderID)
        End If

        If creditCardNumber <> "" Then
            ccreq.setCreditCardNumber(creditCardNumber)
        End If
        If expireMonth <> "" Then
            ccreq.setExpireMonth(expireMonth)
        End If
        If expireYear <> "" Then
            ccreq.setExpireYear(expireYear)
        End If
        ccreq.setCreditCardVerificationNumber(creditCardVerificationNumber)

        If chargeTotal <> "" Then
            ccreq.setChargeTotal(chargeTotal)
        End If
        If chargeType <> "" Then
            ccreq.setChargeType(chargeType)
        End If

        If transactionConditionCode <> "" Then
            ccreq.setTransactionConditionCode(transactionConditionCode)
        End If

        If Me.Track1 <> "" Then
            ccreq.setTrack1(Me.Track1)
        End If

        If Me.Track2 <> "" Then
            ccreq.setTrack2(Me.Track2)
        End If
        If Me.industry <> "" Then
            ccreq.setIndustry(Me.industry)
        End If

        '
        'ccreq.setBillCustomerTitle(billCustomerTitle)
        ccreq.setBillFirstName(billFirstName)
        'ccreq.setBillMiddleName(billMiddleName)
        ccreq.setBillLastName(billLastName)
        'ccreq.setBillCompany(billCompany)
        ccreq.setBillAddressOne(billAddressOne)
        ccreq.setBillAddressTwo(billAddressTwo)
        ccreq.setBillCity(billCity)
        ccreq.setBillStateOrProvince(billStateOrProvince)
        ccreq.setBillCountryCode(billCountry)
        ''
        ccreq.setBillPostalCode(billZipOrPostalCode)

        Try
            ccresp = TransactionClient.doTransaction(ccreq, ACCOUNT_TOKEN)

            ' Retrieve Response Variables
            resOrderID = ccresp.getOrderId()
            resResponseCode = ccresp.getResponseCode()
            resSecondaryResponseCode = ccresp.getSecondaryResponseCode()
            resResponseCodeText = ccresp.getResponseCodeText()
            resBankApprovalCode = ccresp.getBankApprovalCode()
            resBankTransactionID = ccresp.getBankTransactionId()
            resCreditCardVerificationResponse = ccresp.getCreditCardVerificationResponse()
            resAVSCode = ccresp.getAvsCode()
            resISOCode = ccresp.getIsoCode()
            resBatchID = ccresp.getBatchId()
            resReferenceID = ccresp.getCaptureReferenceId()
            resTimeStamp = ccresp.getTimeStamp()
            resRetryRecommended = ccresp.getRetryRecommended()

            resState = ccresp.getState()
            resAuthorizedAmount = ccresp.getAuthorizedAmount()
            resOriginalAuthorizedAmount = ccresp.getOriginalAuthorizedAmount()
            resCapturedAmount = ccresp.getCapturedAmount()
            resCreditedAmount = ccresp.getCreditedAmount()
            resTimeStampCreated = ccresp.getTimeStampCreated()
        Catch ex As Exception

            Return False

        End Try

        Return True

    End Function

End Class
