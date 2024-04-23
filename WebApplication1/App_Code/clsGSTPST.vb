Imports Microsoft.VisualBasic

Public Class clsGSTPST


    Dim taxproduct_pst As Integer = 0
    Dim taxdelivery_pst As Integer = 0
    Dim taxserviceRelya_pst As Integer = 0

    Dim taxproduct_gst As Integer = 0
    Dim taxdelivery_gst As Integer = 0
    Dim taxserviceRelya_gst As Integer = 0


    Sub PopulatingTaxPercentValuesGSTPST()

        If txtShippingState = "BC" Then

            If drpShipMethod = "Wire_Out" Then
                taxproduct_pst = 7
                taxdelivery_pst = 7
                taxserviceRelya_pst = 0

                taxproduct_gst = 5
                taxdelivery_gst = 5
                taxserviceRelya_gst = 5
            Else
                taxproduct_pst = 7
                taxdelivery_pst = 0
                taxserviceRelya_pst = 0

                taxproduct_gst = 5
                taxdelivery_gst = 5
                taxserviceRelya_gst = 5
            End If

        Else

            taxproduct_pst = 0
            taxdelivery_pst = 0
            taxserviceRelya_pst = 0

            taxproduct_gst = 5
            taxdelivery_gst = 5
            taxserviceRelya_gst = 5

        End If


        If txtShippingState = "AB" Or txtShippingState = "MB" Or txtShippingState = "YT" Or txtShippingState = "SK" Or txtShippingState = "NT" Or txtShippingState = "PE" Then

            'txtTaxPercentPST = "0%"
            'txtTaxPercentGST = "5%"

            taxdelivery_pst = 0
            taxproduct_pst = 0
            taxserviceRelya_pst = 0

            taxdelivery_gst = 5
            taxproduct_gst = 5
            taxserviceRelya_gst = 5

        End If

        If txtShippingState = "NB" Or txtShippingState = "NL" Or txtShippingState = "ON" Or txtShippingState = "LB" Then

            'txtTaxPercentPST = "0%"
            'txtTaxPercentGST = "13%"

            taxdelivery_pst = 0
            taxproduct_pst = 0
            taxserviceRelya_pst = 0

            taxdelivery_gst = 13
            taxproduct_gst = 13
            taxserviceRelya_gst = 5

        End If

        'txtShippingState,drpShipCountry,drpPaymentType

        If txtShippingState = "NS" Then

            'txtTaxPercentPST = "0%"
            'txtTaxPercentGST = "15%"

            taxdelivery_pst = 0
            taxproduct_pst = 0
            taxserviceRelya_pst = 0

            taxdelivery_gst = 15
            taxproduct_gst = 15
            taxserviceRelya_gst = 5

        End If

        If drpShipCountry <> "CD" Then

            taxdelivery_pst = 0
            taxproduct_pst = 0
            taxserviceRelya_pst = 0

            taxdelivery_gst = 0
            taxproduct_gst = 0
            taxserviceRelya_gst = 5

        End If


        If drpPaymentType = "Wire In" Then

            taxdelivery_pst = 0
            taxproduct_pst = 0
            taxserviceRelya_pst = 0

            taxdelivery_gst = 0
            taxproduct_gst = 0
            taxserviceRelya_gst = 0

        End If

        If txtCustomerTemp.Trim().ToLower = "b-4683" Or txtCustomerTemp.Trim().ToLower = "b-14544" Or txtCustomerTemp.Trim().ToLower = "b-4685" Or txtCustomerTemp.Trim().ToLower = "b-4687" Or txtCustomerTemp.Trim().ToLower = "b-4894" Or txtCustomerTemp.Trim().ToLower = "b-4692" Or txtCustomerTemp.Trim().ToLower = "b-4686" Or txtCustomerTemp.Trim().ToLower = "b-4891" Then

            taxdelivery_pst = 0
            taxproduct_pst = 0
            taxserviceRelya_pst = 0

        End If


    End Sub

    '      txtShippingState,drpShipCountry,drpPaymentType

    Public txtCustomerTemp As String

    Public txtShippingState As String
    Public drpShipCountry As String
    Public drpPaymentType As String
    Public drpShipMethod As String

    Public CompanyID As String
    Public DivisionID As String
    Public DepartmentID As String

    Public taxgstvalue As Decimal = 0
    Public taxpstvalue As Decimal = 0


    Public Function PopulatingTaxPercent(ByVal ShippingState As String, ByVal ShipCountry As String, ByVal PaymentType As String, ByVal ShipMethod As String, ByVal ServiceCharge As Decimal, ByVal RelayCharge As Decimal, ByVal DeliveryCharge As Decimal, ByVal txtSubtotal As Decimal) As Boolean



        Me.txtShippingState = ShippingState
        Me.drpShipCountry = ShipCountry
        Me.drpPaymentType = PaymentType
        Me.drpShipMethod = ShipMethod

        PopulatingTaxPercentValuesGSTPST()

        Dim SalesTax As Decimal

        SalesTax += ServiceCharge
        SalesTax += DeliveryCharge
        SalesTax += RelayCharge


        Dim DiscountAmount As String = "0"
        Dim SubTotalString As String = txtSubtotal


        Dim SubTotalString1 As Double = 0

        Try
            SubTotalString1 = Convert.ToDecimal(SubTotalString)
        Catch ex As Exception
            SubTotalString1 = 0
        End Try


        taxgstvalue = (((ServiceCharge + RelayCharge) * taxserviceRelya_gst) / 100) + ((DeliveryCharge * taxdelivery_gst) / 100) + ((SubTotalString1 * taxproduct_gst) / 100)
        taxpstvalue = (((ServiceCharge + RelayCharge) * taxserviceRelya_pst) / 100) + ((DeliveryCharge * taxdelivery_pst) / 100) + ((SubTotalString1 * taxproduct_pst) / 100)

        Return True
    End Function

End Class
