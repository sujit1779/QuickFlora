Imports System.Data
Imports System.Data.SqlClient

Partial Class AjaxItemsSearchName
    Inherits System.Web.UI.Page

    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim EmployeeID As String = ""
    Dim CustomerID As String = ""

    Public result As String = ""
    Public locationid As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim CustomerTypeID As String = ""
        Try
            ' CustomerTypeID = Session("CustomerTypeID")
        Catch ex As Exception
        End Try

        Try
            ' locationid = Session("OrderLocationid")
        Catch ex As Exception
        End Try

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

         
        If Not Request.QueryString("k") = Nothing Then
            Dim keyword As String = ""
            keyword = Request.QueryString("k")

            If keyword <> "" Then

                Dim str As String = ""
                Dim dt As New DataTable
                dt = ExactInventoryItemSearch(keyword)
                If dt.Rows.Count = 1 Then
                    Try
                        str = dt.Rows(0)("ItemName")
                    Catch ex As Exception
                        str = ex.Message
                    End Try
                    'wholesalePrice,UnitsPerBox
                    Try
                        str = str & "~!" & dt.Rows(0)("UnitsPerBox")
                    Catch ex As Exception
                        str = str & "~!" & "1"
                    End Try
                    Try
                        Dim wp As Decimal = 0
                        wp = dt.Rows(0)("wholesalePrice")
                        str = str & "~!" & Format(wp, "0.00")
                    Catch ex As Exception
                        str = str & "~!" & "0"
                    End Try

                    'VendorID
                    Try
                        str = str & "~!" & dt.Rows(0)("VendorID")
                    Catch ex As Exception
                        str = str & "~!" & ""
                    End Try
                Else
                    str = "Not Found" '& dt.Rows.Count
                End If

                If str.Trim = "" Then
                    str = "No Name"
                End If
                '  Response.Clear()
                    Response.Write(str)
                    Response.End()
                Else
                    Response.End()
                    Response.Clear()
                    Response.Write("")
                End If

            End If



    End Sub
 



    Private Function ExactInventoryItemSearch(ByVal keyword As String) As DataTable

        Dim sql As String = "" 'select top 35 ItemID,ItemDescription,ItemName,Price,PictureURL,wholesalePrice,[ItemTypeID], Isnull(IsAssembly,0) as IsAssembly "
        sql = sql & " select  InventoryItems.ItemName,InventoryItems.Price,wholesalePrice,UnitsPerBox ,VendorID  "
        sql = sql & " from [InventoryItems] "
        sql = sql & " where  [InventoryItems].ItemID <> 'DEFAULT' AND [InventoryItems].CompanyID=@CompanyID AND [InventoryItems].DivisionID=@DivisionID AND [InventoryItems].DepartmentID=@DepartmentID AND   [InventoryItems].[ItemID]  = '" & keyword & "' "


        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand(sql, myCon)
        myCommand.Parameters.AddWithValue("@CompanyID", CompanyID)
        myCommand.Parameters.AddWithValue("@DivisionID", DivisionID)
        myCommand.Parameters.AddWithValue("@DepartmentID", DepartmentID)

        Dim da As New SqlDataAdapter(myCommand)
        Dim dt As New DataTable

        'populate the data control and bind the data source

        da.Fill(dt)

        Return dt

    End Function



    Private Function WithoutInventoryItemSearch(ByVal keyword As String) As DataTable

        Dim sql As String = "" 'select top 35 ItemID,ItemDescription,ItemName,Price,PictureURL,wholesalePrice,[ItemTypeID], Isnull(IsAssembly,0) as IsAssembly "

        'sql = sql & " select top 35 InventoryItems.ItemID,InventoryItems.ItemDescription,InventoryItems.ItemName,InventoryItems.Price,InventoryItems.PictureURL,InventoryItems.wholesalePrice,InventoryItems.[ItemTypeID], Isnull(InventoryItems.IsAssembly,0) as IsAssembly "
        'sql = sql & " ,InventoryItems.Variety, InventoryItems.StartDateAvailable ,InventoryItems.EndDateAvailable ,InventoryItems.Grower ,[InventoryByWarehouse].QtyOnHand "
        'sql = sql & " from [InventoryItems] LEFT OUTER JOIN [InventoryByWarehouse] ON "
        'sql = sql & " [InventoryItems].CompanyID = [InventoryByWarehouse].CompanyID AND "
        'sql = sql & " [InventoryItems].DivisionID = [InventoryByWarehouse].DivisionID AND "
        'sql = sql & " [InventoryItems].DepartmentID = [InventoryByWarehouse].DepartmentID AND "
        'sql = sql & " [InventoryItems].ItemID = [InventoryByWarehouse].ItemID "
        'sql = sql & "   where  [InventoryItems].ItemID <> 'DEFAULT' AND [InventoryItems].CompanyID=@CompanyID AND [InventoryItems].DivisionID=@DivisionID AND [InventoryItems].DepartmentID=@DepartmentID AND  InventoryItems.IsActive=1 and InventoryItems.WireServiceIdAllowed = 1 and ([InventoryItems].[ItemID]  like '%" + keyword.Trim().Replace("'", "''") + "%' Or [InventoryItems].[ItemName] like '%" + keyword.Trim().Replace("'", "''") + "%'  Or [InventoryItems].[ItemUPCCode] like '%" + keyword.Trim().Replace("'", "''") + "%' ) "

        sql = sql & " select top 35 InventoryItems.ItemID,InventoryItems.ItemDescription,InventoryItems.ItemName,InventoryItems.Price,InventoryItems.PictureURL,InventoryItems.wholesalePrice,InventoryItems.[ItemTypeID], Isnull(InventoryItems.IsAssembly,0) as IsAssembly "
        sql = sql & " ,InventoryItems.Variety, InventoryItems.StartDateAvailable ,InventoryItems.EndDateAvailable ,InventoryItems.Grower "
        sql = sql & " from [InventoryItems] "
        sql = sql & " where  [InventoryItems].ItemID <> 'DEFAULT' AND [InventoryItems].CompanyID=@CompanyID AND [InventoryItems].DivisionID=@DivisionID AND [InventoryItems].DepartmentID=@DepartmentID AND  InventoryItems.IsActive=1 and InventoryItems.WireServiceIdAllowed = 1 and ([InventoryItems].[ItemID]  like '%"
        sql = sql & keyword.Trim().Replace("'", "''") + "%' Or [InventoryItems].[ItemName] like '%" + keyword.Trim().Replace("'", "''") + "%'  Or [InventoryItems].[ItemUPCCode] like '%" + keyword.Trim().Replace("'", "''") + "%' ) "

        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand(sql, myCon)
        myCommand.Parameters.AddWithValue("@CompanyID", CompanyID)
        myCommand.Parameters.AddWithValue("@DivisionID", DivisionID)
        myCommand.Parameters.AddWithValue("@DepartmentID", DepartmentID)

        Dim da As New SqlDataAdapter(myCommand)
        Dim dt As New DataTable

        'populate the data control and bind the data source

        da.Fill(dt)

        Return dt

    End Function

    Private Function WithInventoryItemSearch(ByVal KeyWord As String, ByVal OrderDate As String, ByVal DeliveryDate As String) As DataTable

        Dim dt As New DataTable

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using command As New SqlCommand("[enterprise].[NewItemSearchWithInventoryDetails]", connection)
                command.CommandType = CommandType.StoredProcedure

                command.Parameters.AddWithValue("CompanyID", CompanyID)
                command.Parameters.AddWithValue("DivisionID", DivisionID)
                command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                command.Parameters.AddWithValue("KeyWord", KeyWord.Trim().Replace("'", "''").Trim().Replace("'", "''").Trim().Replace("'", "''"))
                command.Parameters.AddWithValue("OrderDate", OrderDate)
                command.Parameters.AddWithValue("DeliveryDate", DeliveryDate)

                Try
                    Dim da As New SqlDataAdapter(command)
                    da.Fill(dt)
                Catch ex As Exception

                End Try

            End Using

        End Using

        Return dt

    End Function


End Class
