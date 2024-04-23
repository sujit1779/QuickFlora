Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class clsinventoryitem
    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    Public ComanyID As String
    Public DivisionID As String
    Public DepartmentID As String

    Public ItemID As String
    Public IsActive As Boolean
    Public ItemTypeID As String
    Public ItemName As String
    Public ItemDescription As String
    Public ItemLongDescription As String
    Public ItemCategoryID As String
    Public ItemFamilyID As String
    Public PictureURL As String
    Public LargePictureURL As String
    Public MediumPictureURL As String
    Public Price As String
    Public WireServiceProducts As Boolean
    Public WireServiceID As String
    Public ItemCategoryID2 As String
    Public ItemFamilyID2 As String
    Public ItemCategoryID3 As String
    Public ItemFamilyID3 As String


    Public EnabledFrontEndItem As Boolean
    Public EnableItemPrice As Boolean
    Public EnableAddtoCart As Boolean
    Public IsActiveIIIrd As Boolean
    Public IsActiveIInd As Boolean
    Public DeluxePrice As Double
    Public PremiumPrice As Double

    '
    ',ItemID,IsActive,ItemTypeID,ItemName,ItemDescription,ItemLongDescription,ItemCategoryID,ItemFamilyID,PictureURL,LargePictureURL,MediumPictureURL,Price,WireServiceProducts,WireServiceID

    Public Filename As String
    Public dateup As Date
    Public delimiter As String

    Public Function Insert() As Boolean

        checkforfailycategory()

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "INSERT INTO  InventoryItems (CompanyID,DivisionID,DepartmentID,ItemID,IsActive,ItemTypeID,ItemName,ItemDescription,ItemLongDescription,ItemCategoryID,ItemFamilyID,PictureURL,LargePictureURL,MediumPictureURL,Price,WireServiceProducts,WireServiceID,ItemCategoryID2,ItemFamilyID2,ItemCategoryID3,ItemFamilyID3,[ItemFamilyID2IsActive],[ItemFamilyID3IsActive],[PremiumPrice],[DeluxePrice],[EnableItemPrice],[EnableAddtoCart],[EnabledFrontEndItem]) values(@f1,@f2,@f3,@f4,@f5,@f6,@f7,@f8,@f9,@f10,@f11,@f12,@f13,@f14,@f15,@f16,@f17,@f18,@f19,@f20,@f21,@f22,@f23,@f24,@f25,@f26,@f27,@f28)"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.ComanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.ItemID
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.Bit)).Value = Me.IsActive
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 36)).Value = Me.ItemTypeID
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.NVarChar, 50)).Value = Me.ItemName
            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.NVarChar, 4000)).Value = Me.ItemDescription
            com.Parameters.Add(New SqlParameter("@f9", SqlDbType.NVarChar, 4000)).Value = Me.ItemLongDescription
            com.Parameters.Add(New SqlParameter("@f10", SqlDbType.NVarChar, 36)).Value = Me.ItemCategoryID
            com.Parameters.Add(New SqlParameter("@f11", SqlDbType.NVarChar, 36)).Value = Me.ItemFamilyID
            com.Parameters.Add(New SqlParameter("@f12", SqlDbType.NVarChar, 80)).Value = Me.PictureURL
            com.Parameters.Add(New SqlParameter("@f13", SqlDbType.NVarChar, 80)).Value = Me.LargePictureURL
            com.Parameters.Add(New SqlParameter("@f14", SqlDbType.NVarChar, 80)).Value = Me.MediumPictureURL
            com.Parameters.Add(New SqlParameter("@f15", SqlDbType.Money)).Value = Me.Price
            com.Parameters.Add(New SqlParameter("@f16", SqlDbType.Bit)).Value = Me.WireServiceProducts
            com.Parameters.Add(New SqlParameter("@f17", SqlDbType.NVarChar, 36)).Value = Me.WireServiceID

            com.Parameters.Add(New SqlParameter("@f18", SqlDbType.NVarChar, 36)).Value = Me.ItemCategoryID2
            com.Parameters.Add(New SqlParameter("@f19", SqlDbType.NVarChar, 36)).Value = Me.ItemFamilyID2
            com.Parameters.Add(New SqlParameter("@f20", SqlDbType.NVarChar, 36)).Value = Me.ItemCategoryID3
            com.Parameters.Add(New SqlParameter("@f21", SqlDbType.NVarChar, 36)).Value = Me.ItemFamilyID3
            'drpEnabledFrontEndItem.Items.Clear()
            'drpEnableItemPrice.Items.Clear()
            'drpEnableAddtoCart.Items.Clear()
            'drpIsActiveIIIrd.Items.Clear()
            'drpIsActiveIInd.Items.Clear()
            'drpDeluxePrice.Items.Clear()
            'drpPremiumPrice.Items.Clear()

            ',[ItemFamilyID2IsActive]=@f22,[ItemFamilyID3IsActive]=@f23,[PremiumPrice]=@f24,[DeluxePrice]=@f25,[EnableItemPrice]=@f26,[EnableAddtoCart]=@f27,[EnabledFrontEndItem]=@f28

            com.Parameters.Add(New SqlParameter("@f22", SqlDbType.Bit)).Value = Me.IsActiveIInd
            com.Parameters.Add(New SqlParameter("@f23", SqlDbType.Bit)).Value = Me.IsActiveIIIrd
            com.Parameters.Add(New SqlParameter("@f24", SqlDbType.Money)).Value = Me.PremiumPrice
            com.Parameters.Add(New SqlParameter("@f25", SqlDbType.Money)).Value = Me.DeluxePrice
            com.Parameters.Add(New SqlParameter("@f26", SqlDbType.Bit)).Value = Me.EnableItemPrice
            com.Parameters.Add(New SqlParameter("@f27", SqlDbType.Bit)).Value = Me.EnableAddtoCart
            com.Parameters.Add(New SqlParameter("@f28", SqlDbType.Bit)).Value = Me.EnabledFrontEndItem

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


    Public Function Update() As Boolean

        checkforfailycategory()

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE InventoryItems  set IsActive=@f5,ItemTypeID=@f6,ItemName=@f7,ItemDescription=@f8,ItemLongDescription=@f9,ItemCategoryID=@f10,ItemFamilyID=@f11,PictureURL=@f12,LargePictureURL=@f13,MediumPictureURL=@f14,Price=@f15,WireServiceProducts=@f16,WireServiceID=@f17,ItemCategoryID2=@f18,ItemFamilyID2=@f19,ItemCategoryID3=@f20,ItemFamilyID3=@f21 ,[ItemFamilyID2IsActive]=@f22,[ItemFamilyID3IsActive]=@f23,[PremiumPrice]=@f24,[DeluxePrice]=@f25,[EnableItemPrice]=@f26,[EnableAddtoCart]=@f27,[EnabledFrontEndItem]=@f28  where CompanyID=@f1 and DivisionID=@f2 and DepartmentID=@f3 and ItemID=@f4"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.ComanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.ItemID
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.Bit)).Value = Me.IsActive
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 36)).Value = Me.ItemTypeID
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.NVarChar, 50)).Value = Me.ItemName
            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.NVarChar, 4000)).Value = Me.ItemDescription
            com.Parameters.Add(New SqlParameter("@f9", SqlDbType.NVarChar, 4000)).Value = Me.ItemLongDescription
            com.Parameters.Add(New SqlParameter("@f10", SqlDbType.NVarChar, 36)).Value = Me.ItemCategoryID
            com.Parameters.Add(New SqlParameter("@f11", SqlDbType.NVarChar, 36)).Value = Me.ItemFamilyID
            com.Parameters.Add(New SqlParameter("@f12", SqlDbType.NVarChar, 80)).Value = Me.PictureURL
            com.Parameters.Add(New SqlParameter("@f13", SqlDbType.NVarChar, 80)).Value = Me.LargePictureURL
            com.Parameters.Add(New SqlParameter("@f14", SqlDbType.NVarChar, 80)).Value = Me.MediumPictureURL
            com.Parameters.Add(New SqlParameter("@f15", SqlDbType.Money)).Value = Me.Price
            com.Parameters.Add(New SqlParameter("@f16", SqlDbType.Bit)).Value = Me.WireServiceProducts
            com.Parameters.Add(New SqlParameter("@f17", SqlDbType.NVarChar, 36)).Value = Me.WireServiceID

            com.Parameters.Add(New SqlParameter("@f18", SqlDbType.NVarChar, 36)).Value = Me.ItemCategoryID2
            com.Parameters.Add(New SqlParameter("@f19", SqlDbType.NVarChar, 36)).Value = Me.ItemFamilyID2
            com.Parameters.Add(New SqlParameter("@f20", SqlDbType.NVarChar, 36)).Value = Me.ItemCategoryID3
            com.Parameters.Add(New SqlParameter("@f21", SqlDbType.NVarChar, 36)).Value = Me.ItemFamilyID3


            com.Parameters.Add(New SqlParameter("@f22", SqlDbType.Bit)).Value = Me.IsActiveIInd
            com.Parameters.Add(New SqlParameter("@f23", SqlDbType.Bit)).Value = Me.IsActiveIIIrd
            com.Parameters.Add(New SqlParameter("@f24", SqlDbType.Money)).Value = Me.PremiumPrice
            com.Parameters.Add(New SqlParameter("@f25", SqlDbType.Money)).Value = Me.DeluxePrice
            com.Parameters.Add(New SqlParameter("@f26", SqlDbType.Bit)).Value = Me.EnableItemPrice
            com.Parameters.Add(New SqlParameter("@f27", SqlDbType.Bit)).Value = Me.EnableAddtoCart
            com.Parameters.Add(New SqlParameter("@f28", SqlDbType.Bit)).Value = Me.EnabledFrontEndItem



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


    Public Sub checkforfailycategory()

        Dim dt As New DataTable

        If Me.ItemFamilyID <> "" Then
            dt.Clear()
            dt = Checkfamily(Me.ItemFamilyID)
            If dt.Rows.Count = 0 Then
                Insertfamily(Me.ItemFamilyID)
            End If
        End If
        
        If Me.ItemFamilyID2 <> "" Then
            dt.Clear()
            dt = Checkfamily(Me.ItemFamilyID2)
            If dt.Rows.Count = 0 Then
                Insertfamily(Me.ItemFamilyID2)
            End If
        End If
        
        If Me.ItemFamilyID3 <> "" Then
            dt.Clear()
            dt = Checkfamily(Me.ItemFamilyID3)
            If dt.Rows.Count = 0 Then
                Insertfamily(Me.ItemFamilyID3)
            End If
        End If
        

        If Me.ItemCategoryID <> "" And Me.ItemFamilyID <> "" Then
            dt.Clear()
            dt = FillDetailscategory(Me.ItemFamilyID, Me.ItemCategoryID)
            If dt.Rows.Count = 0 Then
                Insertcategory(Me.ItemFamilyID, Me.ItemCategoryID)
            End If
        End If
        
        If Me.ItemCategoryID2 <> "" And Me.ItemFamilyID2 <> "" Then
            dt.Clear()
            dt = FillDetailscategory(Me.ItemFamilyID2, Me.ItemCategoryID2)
            If dt.Rows.Count = 0 Then
                Insertcategory(Me.ItemFamilyID2, Me.ItemCategoryID2)
            End If
        End If
        

        If Me.ItemCategoryID3 <> "" And Me.ItemFamilyID3 <> "" Then
            dt.Clear()
            dt = FillDetailscategory(Me.ItemFamilyID3, Me.ItemCategoryID3)
            If dt.Rows.Count = 0 Then
                Insertcategory(Me.ItemFamilyID3, Me.ItemCategoryID3)
            End If
        End If
        

    End Sub


    Public Function Checkfamily(ByVal familyid As String) As DataTable
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from InventoryFamilies where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and ItemFamilyID=@f3"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.ComanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = familyid

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

    Public Function Insertfamily(ByVal familyid As String) As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "INSERT INTO  InventoryFamilies (CompanyID,DivisionID,DepartmentID,ItemFamilyID,FamilyName) values(@f1,@f2,@f3,@f4,@f5)"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.ComanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = familyid
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 36)).Value = familyid

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

    Public Function FillDetailscategory(ByVal familyid As String, ByVal categoryid As String) As DataTable
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from InventoryCategories where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and ItemFamilyID=@f3 and ItemCategoryID=@f4"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.ComanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = familyid
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = categoryid

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

    Public Function Insertcategory(ByVal familyid As String, ByVal categoryid As String) As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "INSERT INTO  InventoryCategories (CompanyID,DivisionID,DepartmentID,ItemFamilyID,ItemCategoryID,CategoryName) values(@f1,@f2,@f3,@f4,@f5,@f6)"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.ComanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = familyid
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 36)).Value = categoryid
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 36)).Value = categoryid

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

    Public Function FillDetailsfamily() As DataTable
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from InventoryFamilies where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and ItemFamilyID=@f3"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.ComanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.ItemFamilyID

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



    Public Function IsItemIdExist() As Boolean
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "select * from InventoryItems where  CompanyID=@f1 and DivisionID=@f2 and DepartmentID=@f3 and ItemID=@f4"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Dim dt As New DataTable
        Dim da As New SqlDataAdapter

        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.ComanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.ItemID

            da.SelectCommand = com
            da.Fill(dt)

            If dt.Rows.Count <> 0 Then
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
    End Function


    Public Function Insertfilename() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "INSERT INTO  itemimportsfilename (CompanyID,DivisionID,DepartmentID,filename,dateup,delimiter) values(@f1,@f2,@f3,@f4,@f5,@f6)"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.ComanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 100)).Value = Me.Filename
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.DateTime)).Value = Me.dateup
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 50)).Value = Me.delimiter

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

    Public Function deletefilename() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "delete from itemimportsfilename where  CompanyID=@f1 and DivisionID=@f2 and DepartmentID=@f3 "
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.ComanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID

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

    Public Function IsfileExist() As DataTable
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "select * from itemimportsfilename where  CompanyID=@f1 and DivisionID=@f2 and DepartmentID=@f3 "
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Dim dt As New DataTable
        Dim da As New SqlDataAdapter

        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.ComanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID

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

   



End Class
