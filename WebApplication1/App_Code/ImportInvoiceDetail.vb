Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class ImportInvoiceDetail

    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    Public CompanyID As String
    Public DivisionID As String
    Public DepartmentID As String
    Public InvoiceNumber As String
    Public OrderNumber As String

    Public TransactionTypeID As String
    Public TransOpen As Boolean
    Public InvoiceDate As Date
    Public InvoiceDueDate As Date
    Public InvoiceShipDate As Date
    Public InvoiceCancelDate As Date

    Public SystemDate As Date
    Public Memorize As Boolean
    Public PurchaseOrderNumber As String
    Public TaxExemptID As String
    Public TaxGroupID As String
    Public CustomerID As String
    Public TermsID As String

    Public CurrencyID As String
    Public CurrencyExchangeRate As Double
    Public Subtotal As Double
    Public DiscountPers As Double
    Public DiscountAmount As Double
    Public TaxPercent As Double
    Public TaxAmount As Double

    Public TaxableSubTotal As Double
    Public Freight As Double
    Public TaxFreight As Boolean
    Public Handling As Double
    Public Advertising As Double
    Public Total As Double
    Public EmployeeID As String
    Public CommissionPaid As Boolean

    Public CommissionSelectToPay As Boolean
    Public Commission As Double
    Public CommissionableSales As Double
    Public ComissionalbleCost As Double
    Public CustomerDropShipment As Double

    Public ShipMethodID As String
    Public WarehouseID As String
    Public ShipToID As String
    Public ShipForID As String
    Public ShippingName As String
    Public ShippingAddress1 As String
    Public ShippingAddress2 As String

    Public ShippingAddress3 As String
    Public ShippingCity As String
    Public ShippingState As String
    Public ShippingZip As String
    Public ShippingCountry As String
    Public GLSalesAccount As String

    Public PaymentMethodID As String
    Public AmountPaid As Double
    Public UndistributedAmount As Double
    Public BalanceDue As Double
    Public Picked As Boolean
    Public PickedDate As Date
    Public Printed As Boolean

    Public PrintedDate As Date
    Public Shipped As Boolean
    Public ShipDate As Date
    Public TrackingNumber As String
    Public Billed As Boolean
    Public BilledDate As Date
    Public Backordered As Boolean
    Public Posted As Boolean
    Public PostedDate As Date


    Public Function CheckCustomerID() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from  CustomerInformation where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and CustomerID=@f3"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.CustomerID

            da.SelectCommand = com
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return False
        End Try
        Return False
    End Function


    Public Function GetNextInvoiceNumber() As String

        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select NextNumberValue from CompaniesNextNumbers where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and NextNumberName=@f3"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = "NextInvoiceNumber"

            da.SelectCommand = com
            da.Fill(dt)

            If dt.Rows.Count >= 0 Then
                UpdateNextInvoiceNumber()
                Return dt.Rows(0)("NextNumberValue")
            End If

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return "0"
        End Try
        Return "0"
    End Function

    Public Function CheckNextInvoiceNumber() As String

        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from InvoiceHeader where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and InvoiceNumber=@f3"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.InvoiceNumber

            da.SelectCommand = com
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                Return "0"
            Else
                Return "1"
            End If

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return "no"
        End Try
        Return "no"
    End Function

    Public Function UpdateNextInvoiceNumber() As String

        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "Update CompaniesNextNumbers set NextNumberValue=NextNumberValue + 1  where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and NextNumberName=@f3"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = "NextInvoiceNumber"

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()


        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return "0"
        End Try
        Return "0"
    End Function

    Public Function InsertInvoiceHeader() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "INSERT INTO  InvoiceHeader (CompanyID, DivisionID, DepartmentID, InvoiceNumber, OrderNumber" _
        & ", TransactionTypeID, TransOpen, InvoiceDate, InvoiceDueDate, InvoiceShipDate,InvoiceCancelDate" _
        & ", SystemDate, Memorize, PurchaseOrderNumber, TaxExemptID, TaxGroupID, CustomerID, TermsID" _
        & ", CurrencyID,CurrencyExchangeRate, Subtotal, DiscountPers, DiscountAmount, TaxPercent, TaxAmount" _
        & ", TaxableSubTotal, Freight, TaxFreight, Handling, Advertising,Total, EmployeeID, CommissionPaid" _
        & ", CommissionSelectToPay, Commission, CommissionableSales, ComissionalbleCost, CustomerDropShipment" _
        & ", ShipMethodID, WarehouseID, ShipToID, ShipForID, ShippingName, ShippingAddress1, ShippingAddress2" _
        & ", ShippingAddress3, ShippingCity,ShippingState, ShippingZip, ShippingCountry,GLSalesAccount" _
        & ", PaymentMethodID, AmountPaid, UndistributedAmount, BalanceDue, Picked, PickedDate, Printed" _
        & ", PrintedDate, Shipped, ShipDate, TrackingNumber, Billed, BilledDate, Backordered, Posted" _
        & ", PostedDate ) values(@f1,@f2,@f3,@f4,@f5,@f6,@f7,@f8,@f9,@f10,@f11,@f12,@f13,@f14,@f15,@f16,@f17,@f18,@f19,@f20,@f21,@f22,@f23,@f24,@f25,@f26,@f27,@f28,@f29,@f30,@f31,@f32,@f33,@f34,@f35,@f36,@f37,@f38,@f39,@f40,@f41,@f42,@f43,@f44,@f45,@f46,@f47,@f48,@f49,@f50,@f51,@f52,@f53,@f54,@f55,@f56,@f57,@f58,@f59,@f60,@f61,@f62,@f63,@f64,@f65,@f66,@f67)"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.InvoiceNumber
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 36)).Value = Me.OrderNumber
            ''second line'''
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 36)).Value = Me.TransactionTypeID
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.Bit)).Value = Me.TransOpen
            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.DateTime)).Value = Me.InvoiceDate
            com.Parameters.Add(New SqlParameter("@f9", SqlDbType.DateTime)).Value = Me.InvoiceDueDate
            com.Parameters.Add(New SqlParameter("@f10", SqlDbType.DateTime)).Value = Me.InvoiceShipDate
            com.Parameters.Add(New SqlParameter("@f11", SqlDbType.DateTime)).Value = Me.InvoiceCancelDate
            ''Third line''
            'SystemDate, Memorize, PurchaseOrderNumber, TaxExemptID, TaxGroupID, CustomerID, TermsID" _
            com.Parameters.Add(New SqlParameter("@f12", SqlDbType.DateTime)).Value = Me.SystemDate
            com.Parameters.Add(New SqlParameter("@f13", SqlDbType.Bit)).Value = Me.Memorize
            com.Parameters.Add(New SqlParameter("@f14", SqlDbType.NVarChar, 36)).Value = Me.PurchaseOrderNumber
            com.Parameters.Add(New SqlParameter("@f15", SqlDbType.NVarChar, 20)).Value = Me.TaxExemptID
            com.Parameters.Add(New SqlParameter("@f16", SqlDbType.NVarChar, 36)).Value = Me.TaxGroupID
            com.Parameters.Add(New SqlParameter("@f17", SqlDbType.NVarChar, 50)).Value = Me.CustomerID
            com.Parameters.Add(New SqlParameter("@f18", SqlDbType.NVarChar, 36)).Value = Me.TermsID
            ''Fourth line''
            'CurrencyID,CurrencyExchangeRate, Subtotal, DiscountPers, DiscountAmount, TaxPercent, TaxAmount" _
            com.Parameters.Add(New SqlParameter("@f19", SqlDbType.NVarChar, 3)).Value = Me.CurrencyID
            com.Parameters.Add(New SqlParameter("@f20", SqlDbType.Float)).Value = Me.CurrencyExchangeRate
            com.Parameters.Add(New SqlParameter("@f21", SqlDbType.Float)).Value = Me.Subtotal
            com.Parameters.Add(New SqlParameter("@f22", SqlDbType.Float)).Value = Me.DiscountPers
            com.Parameters.Add(New SqlParameter("@f23", SqlDbType.Float)).Value = Me.DiscountAmount
            com.Parameters.Add(New SqlParameter("@f24", SqlDbType.Float)).Value = Me.TaxPercent
            com.Parameters.Add(New SqlParameter("@f25", SqlDbType.Float)).Value = Me.TaxAmount
            ''Fifth line 
            'TaxableSubTotal, Freight, TaxFreight, Handling, Advertising,Total, EmployeeID, CommissionPaid" _
            com.Parameters.Add(New SqlParameter("@f26", SqlDbType.NVarChar, 3)).Value = Me.TaxableSubTotal
            com.Parameters.Add(New SqlParameter("@f27", SqlDbType.Float)).Value = Me.Freight
            com.Parameters.Add(New SqlParameter("@f28", SqlDbType.Bit)).Value = Me.TaxFreight
            com.Parameters.Add(New SqlParameter("@f29", SqlDbType.Float)).Value = Me.Handling
            com.Parameters.Add(New SqlParameter("@f30", SqlDbType.Float)).Value = Me.Advertising
            com.Parameters.Add(New SqlParameter("@f31", SqlDbType.Float)).Value = Me.Total
            com.Parameters.Add(New SqlParameter("@f32", SqlDbType.NVarChar, 36)).Value = Me.EmployeeID
            com.Parameters.Add(New SqlParameter("@f33", SqlDbType.Bit)).Value = Me.CommissionPaid
            'Sixeth line
            'CommissionSelectToPay, Commission, CommissionableSales, ComissionalbleCost, CustomerDropShipment
            com.Parameters.Add(New SqlParameter("@f34", SqlDbType.Bit)).Value = Me.CommissionSelectToPay
            com.Parameters.Add(New SqlParameter("@f35", SqlDbType.Float)).Value = Me.Commission
            com.Parameters.Add(New SqlParameter("@f36", SqlDbType.Float)).Value = Me.CommissionableSales
            com.Parameters.Add(New SqlParameter("@f37", SqlDbType.Float)).Value = Me.ComissionalbleCost
            com.Parameters.Add(New SqlParameter("@f38", SqlDbType.Float)).Value = Me.CustomerDropShipment
            ''Seventh line 
            'ShipMethodID, WarehouseID, ShipToID, ShipForID, ShippingName, ShippingAddress1, ShippingAddress2
            com.Parameters.Add(New SqlParameter("@f39", SqlDbType.NVarChar, 36)).Value = Me.ShipMethodID
            com.Parameters.Add(New SqlParameter("@f40", SqlDbType.NVarChar, 36)).Value = Me.WarehouseID
            com.Parameters.Add(New SqlParameter("@f41", SqlDbType.NVarChar, 36)).Value = Me.ShipToID
            com.Parameters.Add(New SqlParameter("@f42", SqlDbType.NVarChar, 36)).Value = Me.ShipForID
            com.Parameters.Add(New SqlParameter("@f43", SqlDbType.NVarChar, 50)).Value = Me.ShippingName
            com.Parameters.Add(New SqlParameter("@f44", SqlDbType.NVarChar, 50)).Value = Me.ShippingAddress1
            com.Parameters.Add(New SqlParameter("@f45", SqlDbType.NVarChar, 50)).Value = Me.ShippingAddress2
            'Eight line
            'ShippingAddress3, ShippingCity,ShippingState, ShippingZip, ShippingCountry,GLSalesAccount
            com.Parameters.Add(New SqlParameter("@f46", SqlDbType.NVarChar, 50)).Value = Me.ShippingAddress3
            com.Parameters.Add(New SqlParameter("@f47", SqlDbType.NVarChar, 50)).Value = Me.ShippingCity
            com.Parameters.Add(New SqlParameter("@f48", SqlDbType.NVarChar, 50)).Value = Me.ShippingState
            com.Parameters.Add(New SqlParameter("@f49", SqlDbType.NVarChar, 10)).Value = Me.ShippingZip
            com.Parameters.Add(New SqlParameter("@f50", SqlDbType.NVarChar, 50)).Value = Me.ShippingCountry
            com.Parameters.Add(New SqlParameter("@f51", SqlDbType.NVarChar, 36)).Value = Me.GLSalesAccount
            'ninth line
            'PaymentMethodID, AmountPaid, UndistributedAmount, BalanceDue, Picked, PickedDate, Printed
            com.Parameters.Add(New SqlParameter("@f52", SqlDbType.NVarChar, 36)).Value = Me.PaymentMethodID
            com.Parameters.Add(New SqlParameter("@f53", SqlDbType.Float)).Value = Me.AmountPaid
            com.Parameters.Add(New SqlParameter("@f54", SqlDbType.Float)).Value = Me.UndistributedAmount
            com.Parameters.Add(New SqlParameter("@f55", SqlDbType.Float)).Value = Me.BalanceDue
            com.Parameters.Add(New SqlParameter("@f56", SqlDbType.Bit)).Value = Me.Picked
            com.Parameters.Add(New SqlParameter("@f57", SqlDbType.DateTime)).Value = Me.PickedDate
            com.Parameters.Add(New SqlParameter("@f58", SqlDbType.Bit)).Value = Me.Printed
            'Tenth line
            'PrintedDate, Shipped, ShipDate, TrackingNumber, Billed, BilledDate, Backordered, Posted,PostedDate
            com.Parameters.Add(New SqlParameter("@f59", SqlDbType.NVarChar, 36)).Value = Me.PrintedDate
            com.Parameters.Add(New SqlParameter("@f60", SqlDbType.Bit)).Value = Me.Shipped
            com.Parameters.Add(New SqlParameter("@f61", SqlDbType.DateTime)).Value = Me.ShipDate
            com.Parameters.Add(New SqlParameter("@f62", SqlDbType.NVarChar, 36)).Value = Me.TrackingNumber
            com.Parameters.Add(New SqlParameter("@f63", SqlDbType.Bit)).Value = Me.Billed
            com.Parameters.Add(New SqlParameter("@f64", SqlDbType.DateTime)).Value = Me.BilledDate
            com.Parameters.Add(New SqlParameter("@f65", SqlDbType.Bit)).Value = Me.Backordered
            com.Parameters.Add(New SqlParameter("@f66", SqlDbType.Bit)).Value = Me.Posted
            com.Parameters.Add(New SqlParameter("@f67", SqlDbType.DateTime)).Value = Me.PostedDate


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


    Public InvoiceLineNumber As Integer
    Public ItemID As String
    'Public WarehouseID As Integer
    Public WarehouseBinID As String
    Public OrderQty As Double
    'Public BackOrdered As Boolean
    Public BackOrderQty As String
    Public ItemUOM As String
    Public ItemWeight As Double
    'Description, DiscountPerc, Taxable, CurrencyID, CurrencyExchangeRate, ItemCost, ItemUnitPrice
    Public Description As String
    Public DiscountPerc As Double
    Public Taxable As Boolean
    'Public CurrencyID As String
    'Public CurrencyExchangeRate As String
    Public ItemCost As Double
    Public ItemUnitPrice As Double
    'TaxGroupID, TaxAmount, TaxPercent, SubTotal, Total, TotalWeight, GLSalesAccount, ProjectID
    'Public TaxGroupID As String
    'Public TaxAmount As Double
    'Public TaxPercent As Double
    'Public SubTotal As Double
    'Public Total As Double
    Public TotalWeight As Double
    'Public GLSalesAccount As String
    Public ProjectID As String

    Public Function InsertInvoiceDetail() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "INSERT INTO  InvoiceDetail (CompanyID, DivisionID, DepartmentID, InvoiceNumber" _
        & ", ItemID, WarehouseID, WarehouseBinID,OrderQty, BackOrdered, BackOrderQty, ItemUOM, ItemWeight" _
        & ", Description, DiscountPerc, Taxable, CurrencyID, CurrencyExchangeRate, ItemCost, ItemUnitPrice" _
        & ", TaxGroupID, TaxAmount, TaxPercent, SubTotal, Total, TotalWeight, GLSalesAccount, ProjectID" _
        & ") values(@f1,@f2,@f3,@f4,@f6,@f7,@f8,@f9,@f10,@f11,@f12,@f13,@f14,@f15,@f16,@f17,@f18,@f19,@f20,@f21,@f22,@f23,@f24,@f25,@f26,@f27,@f28)"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.InvoiceNumber
            'com.Parameters.Add(New SqlParameter("@f5", SqlDbType.Int)).Value = Me.InvoiceLineNumber
            ''second line'''
            'ItemID, WarehouseID, WarehouseBinID,OrderQty, BackOrdered, BackOrderQty, ItemUOM, ItemWeight
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 36)).Value = Me.ItemID
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.NVarChar, 36)).Value = Me.WarehouseID
            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.NVarChar, 36)).Value = Me.WarehouseBinID
            com.Parameters.Add(New SqlParameter("@f9", SqlDbType.Float)).Value = Me.OrderQty
            com.Parameters.Add(New SqlParameter("@f10", SqlDbType.Bit)).Value = Me.Backordered
            com.Parameters.Add(New SqlParameter("@f11", SqlDbType.Float)).Value = Me.BackOrderQty
            com.Parameters.Add(New SqlParameter("@f12", SqlDbType.NVarChar, 15)).Value = Me.ItemUOM
            com.Parameters.Add(New SqlParameter("@f13", SqlDbType.Float)).Value = Me.ItemWeight
            ''third line
            'Description, DiscountPerc, Taxable, CurrencyID, CurrencyExchangeRate, ItemCost, ItemUnitPrice
            com.Parameters.Add(New SqlParameter("@f14", SqlDbType.NVarChar, 36)).Value = Me.Description
            com.Parameters.Add(New SqlParameter("@f15", SqlDbType.NVarChar, 20)).Value = Me.DiscountPerc
            com.Parameters.Add(New SqlParameter("@f16", SqlDbType.NVarChar, 36)).Value = Me.Taxable
            com.Parameters.Add(New SqlParameter("@f17", SqlDbType.NVarChar, 50)).Value = Me.CurrencyID
            com.Parameters.Add(New SqlParameter("@f18", SqlDbType.Float)).Value = Me.CurrencyExchangeRate
            com.Parameters.Add(New SqlParameter("@f19", SqlDbType.Float)).Value = Me.ItemCost
            com.Parameters.Add(New SqlParameter("@f20", SqlDbType.Float)).Value = Me.ItemUnitPrice
            ''fourth line
            'TaxGroupID, TaxAmount, TaxPercent, SubTotal, Total, TotalWeight, GLSalesAccount, ProjectID
            com.Parameters.Add(New SqlParameter("@f21", SqlDbType.NVarChar, 36)).Value = Me.TaxGroupID
            com.Parameters.Add(New SqlParameter("@f22", SqlDbType.Float)).Value = Me.TaxAmount
            com.Parameters.Add(New SqlParameter("@f23", SqlDbType.Float)).Value = Me.TaxPercent
            com.Parameters.Add(New SqlParameter("@f24", SqlDbType.Float)).Value = Me.Subtotal
            com.Parameters.Add(New SqlParameter("@f25", SqlDbType.Float)).Value = Me.Total
            com.Parameters.Add(New SqlParameter("@f26", SqlDbType.Float)).Value = Me.TotalWeight
            com.Parameters.Add(New SqlParameter("@f27", SqlDbType.NVarChar, 36)).Value = Me.GLSalesAccount
            com.Parameters.Add(New SqlParameter("@f28", SqlDbType.NVarChar, 36)).Value = Me.ProjectID

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

    Public Function PostInvoice() As Integer

        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand("[enterprise].[Invoice_Control]", myCon)
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

        Dim parameterCustomerID As New SqlParameter("@InvoiceNumber", Data.SqlDbType.NVarChar)
        parameterCustomerID.Value = Me.InvoiceNumber
        myCommand.Parameters.Add(parameterCustomerID)

        'Dim paramReturnValue As New SqlParameter("@return_value", Data.SqlDbType.Int)
        'paramReturnValue.Direction = ParameterDirection.Output
        'myCommand.Parameters.Add(paramReturnValue)
        Try
            myCon.Open()
            myCommand.ExecuteNonQuery()
            myCon.Close()
        Catch ex As Exception

        End Try
        

        Dim OutPutValue As Integer = 0
        'OutPutValue = Convert.ToInt32(paramReturnValue.Value)
        Return OutPutValue

    End Function


    Public CustomerFirstName As String
    Public CustomerLastName As String
    Public CustomerAddress1 As String
    Public CustomerCity As String
    Public CustomerState As String
    Public CustomerCountry As String
    Public CustomerZip As String

    Public Function GetListOfCustomerID() As DataTable
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim sqlcluase As String = ""
       
        If Me.CustomerFirstName.Trim() <> "" Then
            sqlcluase = sqlcluase & " AND CustomerFirstName='" & Me.CustomerFirstName.Trim() & "'"
        End If

        If Me.CustomerLastName.Trim() <> "" Then
            sqlcluase = sqlcluase & " AND CustomerLastName='" & Me.CustomerLastName.Trim() & "'"
        End If

        If Me.CustomerAddress1.Trim() <> "" Then
            sqlcluase = sqlcluase & " AND dbo.Extract_Numbers_from_a_String(CustomerAddress1) ='" & ExtractNumbers(Me.CustomerAddress1.Trim()) & "'"
        End If

        If Me.CustomerCity.Trim() <> "" Then
            sqlcluase = sqlcluase & " AND CustomerCity='" & Me.CustomerCity.Trim() & "'"
        End If

        If Me.CustomerState.Trim() <> "" Then
            sqlcluase = sqlcluase & " AND CustomerState='" & Me.CustomerState.Trim() & "'"
        End If

        If Me.CustomerCountry.Trim() <> "" Then
            sqlcluase = sqlcluase & " AND CustomerCountry='" & Me.CustomerCountry.Trim() & "'"
        End If

        If Me.CustomerZip.Trim() <> "" Then
            sqlcluase = sqlcluase & " AND CustomerZip='" & Me.CustomerZip.Trim() & "'"
        End If

        Dim dt As New DataTable()
        ssql = "SELECT  *  from customerinformation where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 " & sqlcluase

        'HttpContext.Current.Response.Write(ssql)

        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID

            da.SelectCommand = com
            da.Fill(dt)


        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)

        End Try
        Return dt
    End Function

    Public Function GetListOfCustomerIDByPartialSearch() As DataTable
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim sqlcluase As String = ""
        Dim sqlcluase1 As String = ""
        Dim sqlcluase2 As String = ""
        Dim sqlcluase3 As String = ""
        Dim sqlcluase4 As String = ""
        Dim sqlcluase5 As String = ""
        Dim sqlcluase6 As String = ""

        If Me.CustomerFirstName.Trim() <> "" Then
            sqlcluase = sqlcluase & " AND CustomerFirstName='" & Me.CustomerFirstName.Trim() & "'"
        End If

        If Me.CustomerLastName.Trim() <> "" Then
            sqlcluase = sqlcluase & " AND CustomerLastName='" & Me.CustomerLastName.Trim() & "'"
        End If

        If Me.CustomerAddress1.Trim() <> "" Then
            sqlcluase = sqlcluase & " AND dbo.Extract_Numbers_from_a_String(CustomerAddress1) ='" & ExtractNumbers(Me.CustomerAddress1.Trim()) & "'"
        End If

        If Me.CustomerCity.Trim() <> "" Then
            sqlcluase = sqlcluase & " AND CustomerCity='" & Me.CustomerCity.Trim() & "'"
        End If

        If Me.CustomerState.Trim() <> "" Then
            sqlcluase = sqlcluase & " AND CustomerState='" & Me.CustomerState.Trim() & "'"
        End If

        If Me.CustomerCountry.Trim() <> "" Then
            sqlcluase = sqlcluase & " AND CustomerCountry='" & Me.CustomerCountry.Trim() & "'"
        End If

        If Me.CustomerZip.Trim() <> "" Then
            sqlcluase = sqlcluase & " AND CustomerZip='" & Me.CustomerZip.Trim() & "'"
        End If

        Dim dt As New DataTable()
        ssql = "SELECT  *  from customerinformation where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 " & sqlcluase

        'HttpContext.Current.Response.Write(ssql)

        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID

            da.SelectCommand = com
            da.Fill(dt)

            If dt.Rows.Count = 0 Then
                If Me.CustomerFirstName.Trim() <> "" Then
                    sqlcluase1 = sqlcluase1 & " AND CustomerFirstName='" & Me.CustomerFirstName.Trim() & "'"
                    sqlcluase2 = sqlcluase2 & " AND CustomerFirstName='" & Me.CustomerFirstName.Trim() & "'"
                    sqlcluase3 = sqlcluase3 & " AND CustomerFirstName='" & Me.CustomerFirstName.Trim() & "'"
                    sqlcluase4 = sqlcluase4 & " AND CustomerFirstName='" & Me.CustomerFirstName.Trim() & "'"
                    sqlcluase5 = sqlcluase5 & " AND CustomerFirstName='" & Me.CustomerFirstName.Trim() & "'"
                    sqlcluase6 = sqlcluase6 & " AND CustomerFirstName='" & Me.CustomerFirstName.Trim() & "'"
                End If

                If Me.CustomerLastName.Trim() <> "" Then
                    sqlcluase1 = sqlcluase1 & " AND CustomerLastName='" & Me.CustomerLastName.Trim() & "'"
                    sqlcluase2 = sqlcluase2 & " AND CustomerLastName='" & Me.CustomerLastName.Trim() & "'"
                    sqlcluase3 = sqlcluase3 & " AND CustomerLastName='" & Me.CustomerLastName.Trim() & "'"
                    sqlcluase4 = sqlcluase4 & " AND CustomerLastName='" & Me.CustomerLastName.Trim() & "'"
                    sqlcluase5 = sqlcluase5 & " AND CustomerLastName='" & Me.CustomerLastName.Trim() & "'"
                    sqlcluase6 = sqlcluase6 & " AND CustomerLastName='" & Me.CustomerLastName.Trim() & "'"
                End If

                If Me.CustomerAddress1.Trim() <> "" Then
                    sqlcluase1 = sqlcluase1 & " AND dbo.Extract_Numbers_from_a_String(CustomerAddress1) ='" & ExtractNumbers(Me.CustomerAddress1.Trim()) & "'"
                    ssql = "SELECT  *  from customerinformation where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 " & sqlcluase1
                    com = New SqlCommand(ssql, connec)
                    com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
                    com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
                    com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID

                    da.SelectCommand = com
                    da.Fill(dt)
                End If

                If Me.CustomerCity.Trim() <> "" Then
                    sqlcluase2 = sqlcluase2 & " AND CustomerCity='" & Me.CustomerCity.Trim() & "'"
                    ssql = "SELECT  *  from customerinformation where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 " & sqlcluase2
                    com = New SqlCommand(ssql, connec)
                    com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
                    com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
                    com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID

                    da.SelectCommand = com
                    da.Fill(dt)

                End If

                If Me.CustomerState.Trim() <> "" Then
                    sqlcluase3 = sqlcluase3 & " AND CustomerState='" & Me.CustomerState.Trim() & "'"
                    ssql = "SELECT  *  from customerinformation where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 " & sqlcluase3
                    com = New SqlCommand(ssql, connec)
                    com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
                    com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
                    com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID

                    da.SelectCommand = com
                    da.Fill(dt)

                End If

                If Me.CustomerCountry.Trim() <> "" Then
                    sqlcluase4 = sqlcluase4 & " AND CustomerCountry='" & Me.CustomerCountry.Trim() & "'"
                    ssql = "SELECT  *  from customerinformation where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 " & sqlcluase4
                    com = New SqlCommand(ssql, connec)
                    com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
                    com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
                    com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID

                    da.SelectCommand = com
                    da.Fill(dt)
                End If

                If Me.CustomerZip.Trim() <> "" Then
                    sqlcluase5 = sqlcluase5 & " AND CustomerZip='" & Me.CustomerZip.Trim() & "'"
                    ssql = "SELECT  *  from customerinformation where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 " & sqlcluase5
                    com = New SqlCommand(ssql, connec)
                    com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
                    com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
                    com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID

                    da.SelectCommand = com
                    da.Fill(dt)
                End If

                If Me.CustomerZip.Trim() <> "" And Me.CustomerAddress1.Trim() <> "" Then
                    sqlcluase6 = sqlcluase6 & " AND CustomerZip='" & Me.CustomerZip.Trim() & "'"
                    sqlcluase6 = sqlcluase6 & " AND dbo.Extract_Numbers_from_a_String(CustomerAddress1) ='" & ExtractNumbers(Me.CustomerAddress1.Trim()) & "'"

                    ssql = "SELECT  *  from customerinformation where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 " & sqlcluase6
                    com = New SqlCommand(ssql, connec)
                    com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
                    com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
                    com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID

                    da.SelectCommand = com
                    da.Fill(dt)
                End If


                Dim dtnew As New DataTable

                dtnew = SelectDistinct(dt, "CustomerID")

                Return dtnew

            End If


        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)

        End Try
        Return dt
    End Function

    Public Shared Function SelectDistinct(ByVal SourceTable As DataTable, ByVal ParamArray FieldNames() As String) As DataTable
        Dim lastValues() As Object
        Dim newTable As DataTable

        If FieldNames Is Nothing OrElse FieldNames.Length = 0 Then
            Throw New ArgumentNullException("FieldNames")
        End If

        lastValues = New Object(FieldNames.Length - 1) {}
        newTable = New DataTable

        For Each field As String In FieldNames
            newTable.Columns.Add(field, SourceTable.Columns(field).DataType)
        Next

        For Each Row As DataRow In SourceTable.Select("", String.Join(", ", FieldNames))
            If Not fieldValuesAreEqual(lastValues, Row, FieldNames) Then
                newTable.Rows.Add(createRowClone(Row, newTable.NewRow(), FieldNames))

                setLastValues(lastValues, Row, FieldNames)
            End If
        Next

        Return newTable
    End Function

    Private Shared Function fieldValuesAreEqual(ByVal lastValues() As Object, ByVal currentRow As DataRow, ByVal fieldNames() As String) As Boolean
        Dim areEqual As Boolean = True

        For i As Integer = 0 To fieldNames.Length - 1
            If lastValues(i) Is Nothing OrElse Not lastValues(i).Equals(currentRow(fieldNames(i))) Then
                areEqual = False
                Exit For
            End If
        Next

        Return areEqual
    End Function

    Private Shared Function createRowClone(ByVal sourceRow As DataRow, ByVal newRow As DataRow, ByVal fieldNames() As String) As DataRow
        For Each field As String In fieldNames
            newRow(field) = sourceRow(field)
        Next

        Return newRow
    End Function

    Private Shared Sub setLastValues(ByVal lastValues() As Object, ByVal sourceRow As DataRow, ByVal fieldNames() As String)
        For i As Integer = 0 To fieldNames.Length - 1
            lastValues(i) = sourceRow(fieldNames(i))
        Next
    End Sub



    Shared Function ExtractNumbers(ByVal expr As String) As String
        Return String.Join(Nothing, System.Text.RegularExpressions.Regex.Split(expr, "[^\d]"))

    End Function


    Public Function IsItemIdExist(ByVal Price As Double) As Boolean
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "select * from InventoryItems where  CompanyID=@f1 and DivisionID=@f2 and DepartmentID=@f3 and ItemID=@f4"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Dim dt As New DataTable
        Dim da As New SqlDataAdapter

        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = "Invoiced_Item"

            da.SelectCommand = com
            da.Fill(dt)

            If dt.Rows.Count <> 0 Then
                UpdateItem(Price)
                Return True
            Else
                InsertItem(Price)
                Return False
            End If

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return False
        End Try
    End Function

    Public Function InsertItem(ByVal Price As Double) As Boolean


        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "INSERT INTO  InventoryItems (CompanyID,DivisionID,DepartmentID,ItemID,IsActive,ItemTypeID,ItemName,ItemDescription,ItemLongDescription,Price) values(@f1,@f2,@f3,@f4,@f5,@f6,@f7,@f8,@f9,@f10)"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = "Invoiced_Item"
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.Bit)).Value = False
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 36)).Value = "Non Stock"
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.NVarChar, 50)).Value = "Invoiced Item"
            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.NVarChar, 4000)).Value = "Item For Invoice Import"
            com.Parameters.Add(New SqlParameter("@f9", SqlDbType.NVarChar, 4000)).Value = "Item For Invoice Import"
            com.Parameters.Add(New SqlParameter("@f10", SqlDbType.Money)).Value = Price

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            qry = "INSERT INTO  InventoryByWarehouse (CompanyID,DivisionID,DepartmentID,ItemID,WarehouseID,WarehouseBinID,QtyOnHand,QtyCommitted,QtyOnOrder,QtyOnBackorder) values(@f1,@f2,@f3,@f4,@f5,@f6,@f7,@f8,@f9,@f10)"

            com = New SqlCommand(qry, connec)
            Try
                com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
                com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
                com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
                com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = "Invoiced_Item"
                com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 36)).Value = "DEFAULT"
                com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 36)).Value = "DEFAULT"
                com.Parameters.Add(New SqlParameter("@f7", SqlDbType.Decimal)).Value = 10000
                com.Parameters.Add(New SqlParameter("@f8", SqlDbType.Decimal)).Value = 0
                com.Parameters.Add(New SqlParameter("@f9", SqlDbType.Decimal)).Value = 0
                com.Parameters.Add(New SqlParameter("@f10", SqlDbType.Decimal)).Value = 0

           
                com.Connection.Open()
                com.ExecuteNonQuery()
                com.Connection.Close()

            Catch ex As Exception

            End Try
           

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function

    Public Function UpdateItem(ByVal Price As Double) As Boolean


        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE  InventoryItems  SET  Price=@f5  where  CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And ItemID=@f4"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = "Invoiced_Item"
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.Money)).Value = Price

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            qry = "UPDATE  InventoryByWarehouse SET QtyOnHand=@f5 where  CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And ItemID=@f4"

            com = New SqlCommand(qry, connec)
            Try
                com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
                com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
                com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
                com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = "Invoiced_Item"
                com.Parameters.Add(New SqlParameter("@f5", SqlDbType.Decimal)).Value = 10000

                com.Connection.Open()
                com.ExecuteNonQuery()
                com.Connection.Close()

            Catch ex As Exception

            End Try

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function

End Class
