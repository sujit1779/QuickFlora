Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class clsOrderAdjustments

    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    '  SELECT [CompanyID]
    '    ,[DivisionID]
    '    ,[DepartmentID]
    '    ,[OrderNumber]
    '    ,[AdjustmentsType]
    '    ,[AdjustmentValue]
    '    ,[AdjustmentDate]
    'FROM [Enterprise].[dbo].[OrderAdjustments]



    '   [MercuryData_InvoiceNo] [nvarchar](100) NULL,
    '[MercuryData_RefNo] [nvarchar](100) NULL,
    '[MercuryData_ACQRefData] [nvarchar](100) NULL,
    '[MercuryData_AuthCode] [nvarchar](100) NULL,
    '[MercuryData_ProcessData] [nvarchar](100) NULL,
    '[MercuryData_RecordNo] [nvarchar](100) NULL

    Public MercuryData_InvoiceNo As String = ""
    Public MercuryData_RefNo As String = ""
    Public MercuryData_ACQRefData As String = ""
    Public MercuryData_AuthCode As String = ""
    Public MercuryData_ProcessData As String = ""
    Public MercuryData_RecordNo As String = ""

    Public Function UpdateCreditCardPaymentMercury() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE  OrderHeaderCreditCardProcessingDetails SET  [MercuryData_RecordNo]=@MercuryData_RecordNo,[MercuryData_ProcessData]=@MercuryData_ProcessData,[MercuryData_AuthCode]=@MercuryData_AuthCode,[MercuryData_InvoiceNo]=@MercuryData_InvoiceNo,[MercuryData_RefNo]=@MercuryData_RefNo,[MercuryData_ACQRefData]=@MercuryData_ACQRefData where CompanyID=@f1 and DivisionID=@f2 and DepartmentID=@f3 and OrderNumber=@f4"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.OrderNumber
            com.Parameters.Add(New SqlParameter("@MercuryData_InvoiceNo", SqlDbType.NVarChar, 100)).Value = MercuryData_InvoiceNo
            com.Parameters.Add(New SqlParameter("@MercuryData_RefNo", SqlDbType.NVarChar, 100)).Value = MercuryData_RefNo
            com.Parameters.Add(New SqlParameter("@MercuryData_ACQRefData", SqlDbType.NVarChar, 100)).Value = MercuryData_ACQRefData
            com.Parameters.Add(New SqlParameter("@MercuryData_AuthCode", SqlDbType.NVarChar, 100)).Value = MercuryData_AuthCode
            com.Parameters.Add(New SqlParameter("@MercuryData_ProcessData", SqlDbType.NVarChar, 100)).Value = MercuryData_ProcessData
            com.Parameters.Add(New SqlParameter("@MercuryData_RecordNo", SqlDbType.NVarChar, 100)).Value = MercuryData_RecordNo

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function




    Public CompanyID As String
    Public DivisionID As String
    Public DepartmentID As String
    Public OrderNumber As String
    Public AdjustmentsType As String
    Public AdjustmentValue As Double

    Public AdjustmentValueWithOutTax As Double

    Public AdjustmentDate As Date

    Public OrderID As String = ""
    Public ReferenceID As String = ""
    Public ApprovalNumber As String = ""
    Public Address As String = ""
    Public CreditCardNumber As String = ""
    Public CreditCardCSVNumber As String = ""
    Public ExpirationDate As String = ""

    Public OrderHeaderTotal As Double

    Public MemoType As String = ""
    Public MemoNumber As String = ""


    Public Function Insert() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "INSERT INTO  OrderAdjustments (CompanyID,DivisionID,DepartmentID,OrderNumber,AdjustmentsType,AdjustmentValue,AdjustmentDate,OrderID,ReferenceID,ApprovalNumber,Address,CreditCardNumber,ExpirationDate,CreditCardCSVNumber,MemoType,MemoNumber) values(@f1,@f2,@f3,@f4,@f5,@f6,@f7,@f8,@f9,@f10,@f11,@f12,@f13,@f14,@f15,@f16)"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.OrderNumber
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 36)).Value = Me.AdjustmentsType
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.Money)).Value = Me.AdjustmentValue
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.DateTime)).Value = Me.AdjustmentDate

            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.NVarChar, 50)).Value = Me.OrderID
            com.Parameters.Add(New SqlParameter("@f9", SqlDbType.NVarChar, 50)).Value = Me.ReferenceID
            com.Parameters.Add(New SqlParameter("@f10", SqlDbType.NVarChar, 50)).Value = Me.ApprovalNumber
            com.Parameters.Add(New SqlParameter("@f11", SqlDbType.NVarChar, 50)).Value = Me.Address
            com.Parameters.Add(New SqlParameter("@f12", SqlDbType.NVarChar, 50)).Value = Me.CreditCardNumber
            com.Parameters.Add(New SqlParameter("@f13", SqlDbType.NVarChar, 50)).Value = Me.ExpirationDate
            com.Parameters.Add(New SqlParameter("@f14", SqlDbType.NVarChar, 50)).Value = Me.CreditCardCSVNumber

            com.Parameters.Add(New SqlParameter("@f15", SqlDbType.NVarChar, 36)).Value = Me.MemoType
            com.Parameters.Add(New SqlParameter("@f16", SqlDbType.NVarChar, 36)).Value = Me.MemoNumber

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()



            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function

    Public Function UpdateOrderAdjustmentsApprovalNumber() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE [Enterprise].[dbo].[OrderHeaderCreditCardProcessingDetails] set PPIOrderID=@f5,PPIReferenceID=@f6,PayPalPNREF=@f7,ProcessDate=@f8,PaymentAmount=@f9 Where CompanyID=@f1 and DivisionID=@f2 and DepartmentID=@f3 and OrderNumber=@f4 "
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.OrderNumber
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 100)).Value = Me.OrderID
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 100)).Value = Me.ReferenceID
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.NVarChar, 100)).Value = Me.PayPalPNREF
            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.DateTime)).Value = Date.Now.Date
            com.Parameters.Add(New SqlParameter("@f9", SqlDbType.Money)).Value = Me.PaymentAmount

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function

    Public Function DeleteOrderAdjustmentsApprovalNumber() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "Delete from OrderHeaderCreditCardProcessingDetails Where CompanyID=@f1 and DivisionID=@f2 and DepartmentID=@f3 and OrderNumber=@f4 "
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.OrderNumber

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function

    Public Function DetailsOrderAdjustments() As DataTable
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from OrderAdjustments where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and OrderNumber=@f3 Order BY InLIneNUmber"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.OrderNumber

            da.SelectCommand = com
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return dt
        End Try

    End Function

    Public Function InvoiceNumberOFOrderHeader() As String
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select InvoiceNumber from OrderHeader where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and OrderNumber=@f3"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.OrderNumber

            da.SelectCommand = com
            da.Fill(dt)

            If dt.Rows.Count <> 0 Then
                Dim invno As String = ""

                Try
                    invno = dt.Rows(0)("InvoiceNumber")
                Catch ex As Exception

                End Try
                Return invno
            Else
                Return ""
            End If

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return ""
        End Try
        Return ""
    End Function

    Public Function UpdateInvoiceNumberOrderHeader() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE OrderHeader set Invoice_Recalc=0,InvoiceNumber='', InvoiceDate=null,Invoiced=0 Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And OrderNumber=@f5"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 36)).Value = Me.OrderNumber

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function


    Public Function InsertItems() As Boolean


        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "INSERT INTO  InventoryItems (CompanyID,DivisionID,DepartmentID,ItemID,IsActive,ItemTypeID,ItemName,ItemDescription,ItemLongDescription,ItemCategoryID,ItemFamilyID,PictureURL,LargePictureURL,MediumPictureURL,Price,WireServiceProducts,WireServiceID,ItemCategoryID2,ItemFamilyID2,ItemCategoryID3,ItemFamilyID3,[ItemFamilyID2IsActive],[ItemFamilyID3IsActive],[PremiumPrice],[DeluxePrice],[EnableItemPrice],[EnableAddtoCart],[EnabledFrontEndItem]) values(@f1,@f2,@f3,@f4,@f5,@f6,@f7,@f8,@f9,@f10,@f11,@f12,@f13,@f14,@f15,@f16,@f17,@f18,@f19,@f20,@f21,@f22,@f23,@f24,@f25,@f26,@f27,@f28)"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = "Adjustment Item"
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.Bit)).Value = False
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 36)).Value = "Non Stock"
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.NVarChar, 50)).Value = "Adjustment Item"
            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.NVarChar, 4000)).Value = "Adjustment Item"
            com.Parameters.Add(New SqlParameter("@f9", SqlDbType.NVarChar, 4000)).Value = "Adjustment Item"
            com.Parameters.Add(New SqlParameter("@f10", SqlDbType.NVarChar, 36)).Value = ""
            com.Parameters.Add(New SqlParameter("@f11", SqlDbType.NVarChar, 36)).Value = ""
            com.Parameters.Add(New SqlParameter("@f12", SqlDbType.NVarChar, 80)).Value = ""
            com.Parameters.Add(New SqlParameter("@f13", SqlDbType.NVarChar, 80)).Value = ""
            com.Parameters.Add(New SqlParameter("@f14", SqlDbType.NVarChar, 80)).Value = ""
            com.Parameters.Add(New SqlParameter("@f15", SqlDbType.Money)).Value = 0
            com.Parameters.Add(New SqlParameter("@f16", SqlDbType.Bit)).Value = False
            com.Parameters.Add(New SqlParameter("@f17", SqlDbType.NVarChar, 36)).Value = ""
            com.Parameters.Add(New SqlParameter("@f18", SqlDbType.NVarChar, 36)).Value = ""
            com.Parameters.Add(New SqlParameter("@f19", SqlDbType.NVarChar, 36)).Value = ""
            com.Parameters.Add(New SqlParameter("@f20", SqlDbType.NVarChar, 36)).Value = ""
            com.Parameters.Add(New SqlParameter("@f21", SqlDbType.NVarChar, 36)).Value = ""
            '[ItemFamilyID2IsActive]=@f22,[ItemFamilyID3IsActive]=@f23,[PremiumPrice]=@f24,[DeluxePrice]=@f25,[EnableItemPrice]=@f26,[EnableAddtoCart]=@f27,[EnabledFrontEndItem]=@f28
            com.Parameters.Add(New SqlParameter("@f22", SqlDbType.Bit)).Value = False
            com.Parameters.Add(New SqlParameter("@f23", SqlDbType.Bit)).Value = False
            com.Parameters.Add(New SqlParameter("@f24", SqlDbType.Money)).Value = 0
            com.Parameters.Add(New SqlParameter("@f25", SqlDbType.Money)).Value = 0
            com.Parameters.Add(New SqlParameter("@f26", SqlDbType.Bit)).Value = False
            com.Parameters.Add(New SqlParameter("@f27", SqlDbType.Bit)).Value = False
            com.Parameters.Add(New SqlParameter("@f28", SqlDbType.Bit)).Value = False

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function


    'Public TaxGroupID As String
    'Public TaxPercent As String

    Public Function UpdateOrderTotal() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select ItemID from InventoryItems where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and ItemID=@f3"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = "Adjustment Item"

            da.SelectCommand = com
            da.Fill(dt)

            If dt.Rows.Count = 0 Then
                InsertItems()
            End If

        Catch ex As Exception
        
        End Try

      

        Dim grdTaxGroupID = "DEFAULT"
        Dim grdTaxAmount = 0.0

        Dim dtorder As New DataTable
        dtorder = DataOFOrderHeader()
        If dtorder.Rows.Count <> 0 Then
            grdTaxGroupID = dtorder.Rows(0)("TaxGroupID")
            grdTaxAmount = dtorder.Rows(0)("TaxPercent")
        End If


        Dim ItemsDetailSubmit As New DAL.CustomOrder()
        ItemsDetailSubmit.CompanyID = CompanyID
        ItemsDetailSubmit.DivisionID = DivisionID
        ItemsDetailSubmit.DepartmentID = DepartmentID
        ItemsDetailSubmit.OrderNumber = Me.OrderNumber
        ItemsDetailSubmit.Description = "Adjustment Item"
        ItemsDetailSubmit.ItemID = "Adjustment Item"
        ItemsDetailSubmit.OrderQty = 1
        ItemsDetailSubmit.ItemUOM = "Each"
        ItemsDetailSubmit.ItemUnitPrice = Me.AdjustmentValueWithOutTax
        ItemsDetailSubmit.ItemDiscountPerc = Convert.ToDecimal(0)
        ItemsDetailSubmit.ItemDiscountFlatOrPercent = "Flat"
        ItemsDetailSubmit.TaxGroupID = grdTaxGroupID
        ItemsDetailSubmit.TaxPercent = grdTaxAmount
        ItemsDetailSubmit.Total = Me.AdjustmentValueWithOutTax
        ItemsDetailSubmit.SubTotal = Me.AdjustmentValueWithOutTax
        Dim OrderLineNumber As Integer = ItemsDetailSubmit.AddItemDetailsCustomisedGrid()
        CalculationPart(OrderLineNumber, Me.AdjustmentValueWithOutTax)


    End Function

    Public Function DataOFOrderHeader() As DataTable
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from OrderHeader where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and OrderNumber=@f3"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.OrderNumber

            da.SelectCommand = com
            da.Fill(dt)

            
            Return dt


        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
        End Try
        Return dt
    End Function



    Public fullrefund As Boolean = False



    Protected Sub CalculationPart(ByVal OrderLineNumber As Integer, ByVal ItemSubTotal As Decimal)
        Dim FillItemDetailGrid As New DAL.CustomOrder
        Dim dsTotal As SqlDataReader

        dsTotal = FillItemDetailGrid.PopulateSubTotal(CompanyID, DepartmentID, DivisionID, Me.OrderNumber)
        Dim SubTotal As Decimal

        Dim Total As Decimal
        Dim Handling As Decimal
        Dim Freight As Decimal
        Dim Discount As Decimal
        Dim TaxAmount As Decimal
        Dim TaxPercent As Decimal
        Dim SubTotalToDisplay As Decimal


        While dsTotal.Read()
            'SubTotalToDisplay = dsTotal("SubTotal")
            SubTotal = dsTotal("SubTotal")
            Freight = dsTotal("Freight")
            Handling = dsTotal("Handling")
            Discount = dsTotal("DiscountAmount")
            TaxAmount = dsTotal("TaxAmount")
            TaxPercent = dsTotal("TaxPercent")
        End While
        dsTotal.Close()



        Dim ServiceCharge As Decimal = 0
        Dim RelayCharge As Decimal = 0
        Dim DeliveryCharge As Decimal = 0
        Dim SalesTax As Decimal = 0
        Dim ardjustment As Decimal = 0

        Dim dt As New DataTable

        dt = DataOFOrderHeader()

        If dt.Rows.Count <> 0 Then
            Try
                ServiceCharge = dt.Rows(0)("Service")
            Catch ex As Exception

            End Try

            Try
                RelayCharge = dt.Rows(0)("Relay")
            Catch ex As Exception

            End Try

            Try
                DeliveryCharge = dt.Rows(0)("Delivery")
            Catch ex As Exception

            End Try

            Try
                ardjustment = dt.Rows(0)("AdjustmentsAmount")
            Catch ex As Exception

            End Try

        End If


        Dim objCustomer As New DAL.CustomOrder()
        Dim rs1 As SqlDataReader
        rs1 = objCustomer.CheckTaxable(CompanyID, DivisionID, DepartmentID)
        While rs1.Read()
            If rs1("DeliveryTaxable").ToString() <> "" Then
                If rs1("DeliveryTaxable").ToString() = True Then
                    SalesTax += DeliveryCharge
                End If
            End If
            If rs1("ServiceTaxable").ToString() <> "" Then


                If rs1("ServiceTaxable").ToString() = True Then
                    SalesTax += ServiceCharge
                End If
            End If
            If rs1("Internationaltaxable").ToString() <> "" Then

                If rs1("Internationaltaxable").ToString() = True Then
                    SalesTax += RelayCharge
                End If
            End If

        End While

        TaxAmount = (TaxPercent * (Convert.ToDecimal(SubTotal) + SalesTax - Discount) / 100)
        Total = Convert.ToDecimal(SubTotal) + TaxAmount + ServiceCharge + RelayCharge + DeliveryCharge - Discount

        If fullrefund Then
            If Total <> 0 Then
                Total = 0
            End If
        End If

        UpdateOrderALlDetails(SubTotal, TaxAmount, Total)

        Dim ItemTaxAmount As Decimal = 0
        Dim ItemTaxPercent As Decimal = 0
        Dim ItemTotal As Decimal = 0

        ItemTaxPercent = TaxPercent
        ItemTaxAmount = (ItemTaxPercent * (Convert.ToDecimal(ItemSubTotal)) / 100)
        ItemTotal = Convert.ToDecimal(ItemSubTotal) + ItemTaxAmount

        UpdateOrderDetails(ItemTaxPercent, ItemTaxAmount, ItemTotal, OrderLineNumber)


    End Sub


    Public paymenthod As String = ""


    Public Function UpdateOrderDetails(ByVal ItemTaxPercent As Decimal, ByVal ItemTaxAmount As Decimal, ByVal ItemTotal As Decimal, ByVal OrderLineNumber As Integer) As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String

        If paymenthod = "House Account" Then
            qry = "UPDATE OrderDetail set TaxPercent=@f5 ,TaxAmount=@f6,Total=@f7 Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And OrderLineNumber=@f4"
        Else
            qry = "UPDATE OrderDetail set TaxPercent=@f5 ,TaxAmount=@f6,Total=@f7 ,AmountPaid=@f7 , BalanceDue=0 Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And OrderLineNumber=@f4"
        End If

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.Int)).Value = OrderLineNumber
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.Float)).Value = ItemTaxPercent
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.Money)).Value = ItemTaxAmount
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.Money)).Value = ItemTotal
          


            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function

    Public Function UpdateOrderALlDetails(ByVal SubTotal As Decimal, ByVal TaxAmount As Decimal, ByVal Total As Decimal) As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE OrderHeader set AdjustmentsAmount=(Isnull(AdjustmentsAmount,0) + @f4),Subtotal=@f6,Total=@f8,BalanceDue=@f8,TaxAmount=@f7 Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And OrderNumber=@f5"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.Money)).Value = Me.AdjustmentValue
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 36)).Value = Me.OrderNumber
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.Money)).Value = SubTotal
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.Money)).Value = TaxAmount
            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.Money)).Value = Total


            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function

    Public Function CreditAdjustments_CustomerAccount() As String
        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand("[enterprise].[Order_CreditAdjustments_CustomerAccount]", myCon)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
        parameterCompanyID.Value = Me.CompanyID
        myCommand.Parameters.Add(parameterCompanyID)

        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
        parameterDepartmentID.Value = Me.DepartmentID
        myCommand.Parameters.Add(parameterDepartmentID)

        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
        parameterDivisionID.Value = Me.DivisionID
        myCommand.Parameters.Add(parameterDivisionID)

        Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
        parameterOrderNumber.Value = Me.OrderNumber
        myCommand.Parameters.Add(parameterOrderNumber)

        Dim CreditAdjustments As New SqlParameter("@CreditAdjustments", Data.SqlDbType.Money)
        CreditAdjustments.Value = Me.AdjustmentValue
        myCommand.Parameters.Add(CreditAdjustments)


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


    Public Function Refund_CreditAdjustments_CustomerAccount() As String
        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand("[enterprise].[OrderRefund_CreditAdjustments_CustomerAccount]", myCon)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
        parameterCompanyID.Value = Me.CompanyID
        myCommand.Parameters.Add(parameterCompanyID)

        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
        parameterDepartmentID.Value = Me.DepartmentID
        myCommand.Parameters.Add(parameterDepartmentID)

        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
        parameterDivisionID.Value = Me.DivisionID
        myCommand.Parameters.Add(parameterDivisionID)

        Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
        parameterOrderNumber.Value = Me.OrderNumber
        myCommand.Parameters.Add(parameterOrderNumber)

        Dim CreditAdjustments As New SqlParameter("@CreditAdjustments", Data.SqlDbType.Money)
        CreditAdjustments.Value = Me.AdjustmentValue
        myCommand.Parameters.Add(CreditAdjustments)


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


    Public PaymentGateway As String
    Public PPIOrderID As String
    Public PPIReferenceID As String
    Public PayPalPNREF As String
    Public PaymentAmount As Decimal 

    Public NewCreditCardNumber As String = ""
    Public NewCreditCardExpDate As DateTime
    Public NewCreditCardCSVNumber As String = ""



    Public Function InsertCreditCardPaymentDetails() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "INSERT INTO  OrderHeaderCreditCardProcessingDetails (CompanyID,DivisionID,DepartmentID,OrderNumber,PaymentGateway,PPIOrderID,PPIReferenceID,PayPalPNREF,PaymentAmount,CreditCardNumber,CreditCardExpDate,CreditCardCSVNumber) values(@f1,@f2,@f3,@f4,@f5,@f6,@f7,@f8,@f9,@f10,@f11,@f12)"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.OrderNumber
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 50)).Value = Me.PaymentGateway
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 50)).Value = Me.PPIOrderID
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.NVarChar, 50)).Value = Me.PPIReferenceID
            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.NVarChar, 50)).Value = Me.PayPalPNREF
            com.Parameters.Add(New SqlParameter("@f9", SqlDbType.Money)).Value = Me.PaymentAmount

            'com.Parameters.Add(New SqlParameter("@f10", SqlDbType.NVarChar, 50)).Value = Me.NewCreditCardNumber


            ''New code updated for encryption
            Dim encrypted_card As String = ""
            If Me.NewCreditCardNumber <> "" Then
                encrypted_card = CryptographyRijndael.EncryptionRijndael.RijndaelEncode(Me.NewCreditCardNumber, Me.OrderNumber)
            Else
                encrypted_card = ""
            End If
            com.Parameters.Add(New SqlParameter("@f10", SqlDbType.NVarChar, 50)).Value = encrypted_card
            ''New code updated for encryption



            com.Parameters.Add(New SqlParameter("@f11", SqlDbType.DateTime)).Value = Me.NewCreditCardExpDate
            com.Parameters.Add(New SqlParameter("@f12", SqlDbType.NVarChar, 5)).Value = Me.NewCreditCardCSVNumber


            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function


    Public Function UpdateCreditCardPaymentNewCardDetails() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE  OrderHeaderCreditCardProcessingDetails SET  CreditCardNumber=@f10,CreditCardExpDate=@f11,CreditCardCSVNumber=@f12 where CompanyID=@f1 and DivisionID=@f2 and DepartmentID=@f3 and OrderNumber=@f4"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.OrderNumber


            'com.Parameters.Add(New SqlParameter("@f10", SqlDbType.NVarChar, 50)).Value = Me.NewCreditCardNumber
            ''New code updated for encryption
            Dim encrypted_card As String = ""
            If Me.NewCreditCardNumber <> "" Then
                encrypted_card = CryptographyRijndael.EncryptionRijndael.RijndaelEncode(Me.NewCreditCardNumber, Me.OrderNumber)
            Else
                encrypted_card = ""
            End If
            com.Parameters.Add(New SqlParameter("@f10", SqlDbType.NVarChar, 50)).Value = encrypted_card
            ''New code updated for encryption


            com.Parameters.Add(New SqlParameter("@f11", SqlDbType.DateTime)).Value = Me.NewCreditCardExpDate
            com.Parameters.Add(New SqlParameter("@f12", SqlDbType.NVarChar, 5)).Value = Me.NewCreditCardCSVNumber


            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function



    Public Function UpdateCreditCardPaymentChargeType(ByVal ChargeTypes As String, ByVal ChargeStatus As String) As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE  OrderHeaderCreditCardProcessingDetails SET  [ChargeTypes]=@f5,[ChargeStatus]=@f6 where CompanyID=@f1 and DivisionID=@f2 and DepartmentID=@f3 and OrderNumber=@f4"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.OrderNumber
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 36)).Value = ChargeTypes
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 36)).Value = ChargeStatus


            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function


    Public Function DetailsCreditCardPaymentDetails() As DataTable
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from OrderHeaderCreditCardProcessingDetails where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and OrderNumber=@f3"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.OrderNumber

            da.SelectCommand = com
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return dt
        End Try

    End Function

    Public Function DebitMemo_CreateFromOrderAdjustments() As String
        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand("[enterprise].[DebitMemo_CreateFromOrderAdjustments]", myCon)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
        parameterCompanyID.Value = Me.CompanyID
        myCommand.Parameters.Add(parameterCompanyID)

        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
        parameterDepartmentID.Value = Me.DepartmentID
        myCommand.Parameters.Add(parameterDepartmentID)

        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
        parameterDivisionID.Value = Me.DivisionID
        myCommand.Parameters.Add(parameterDivisionID)

        Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
        parameterOrderNumber.Value = Me.OrderNumber
        myCommand.Parameters.Add(parameterOrderNumber)

        Dim CreditAdjustments As New SqlParameter("@Amount", Data.SqlDbType.Money)
        CreditAdjustments.Value = Me.AdjustmentValue
        myCommand.Parameters.Add(CreditAdjustments)


        Dim parameterPostingResult As New SqlParameter("@DebitMemoNumber", Data.SqlDbType.NVarChar, 36)
        parameterPostingResult.Direction = ParameterDirection.Output
        myCommand.Parameters.Add(parameterPostingResult)
        Dim OutputValue As String = ""
        myCon.Open()

        myCommand.ExecuteNonQuery()

        OutputValue = parameterPostingResult.Value.ToString()

        myCon.Close()
        Return OutputValue

    End Function


    Public Function CreditMemo_CreateFromOrderAdjustments() As String
        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand("[enterprise].[CreditMemo_CreateFromOrderAdjustments]", myCon)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
        parameterCompanyID.Value = Me.CompanyID
        myCommand.Parameters.Add(parameterCompanyID)

        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
        parameterDepartmentID.Value = Me.DepartmentID
        myCommand.Parameters.Add(parameterDepartmentID)

        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
        parameterDivisionID.Value = Me.DivisionID
        myCommand.Parameters.Add(parameterDivisionID)

        Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
        parameterOrderNumber.Value = Me.OrderNumber
        myCommand.Parameters.Add(parameterOrderNumber)

        Dim CreditAdjustments As New SqlParameter("@Amount", Data.SqlDbType.Money)
        CreditAdjustments.Value = Me.AdjustmentValue
        myCommand.Parameters.Add(CreditAdjustments)


        Dim parameterPostingResult As New SqlParameter("@CreditMemoNumber", Data.SqlDbType.NVarChar, 36)
        parameterPostingResult.Direction = ParameterDirection.Output
        myCommand.Parameters.Add(parameterPostingResult)
        Dim OutputValue As String = ""
        myCon.Open()

        myCommand.ExecuteNonQuery()

        OutputValue = parameterPostingResult.Value.ToString()

        myCon.Close()
        Return OutputValue

    End Function


    Public Function Invoice_CreateFromOrderAdjustments() As String
        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand("[enterprise].[Invoice_CreateFromOrder]", myCon)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
        parameterCompanyID.Value = Me.CompanyID
        myCommand.Parameters.Add(parameterCompanyID)

        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
        parameterDepartmentID.Value = Me.DepartmentID
        myCommand.Parameters.Add(parameterDepartmentID)

        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
        parameterDivisionID.Value = Me.DivisionID
        myCommand.Parameters.Add(parameterDivisionID)

        Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
        parameterOrderNumber.Value = Me.OrderNumber
        myCommand.Parameters.Add(parameterOrderNumber)


        Dim parameterPostingResult As New SqlParameter("@InvoiceNumber", Data.SqlDbType.NVarChar, 36)
        parameterPostingResult.Direction = ParameterDirection.Output
        myCommand.Parameters.Add(parameterPostingResult)
        Dim OutputValue As String = ""
        myCon.Open()

        myCommand.ExecuteNonQuery()

        OutputValue = parameterPostingResult.Value.ToString()

        myCon.Close()
        Return OutputValue

    End Function

End Class
