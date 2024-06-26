<%@ Page Language="VB" EnableViewState="false"  %>

<%@Import Namespace="System.Data"%>
<%@Import Namespace="System.Data.SqlClient"%>
<%@Import Namespace="System.Configuration"%> 

<script runat="server">

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
            locationid = Session("Locationid")
        Catch ex As Exception

        End Try




        CompanyID = Session("CompanyID")
        DivisionID =  Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID =   Session("EmployeeID")


        Dim obj As New clsOrder_Location
        Dim dtnew As New System.Data.DataTable()
        obj.CompanyID = CompanyID
        obj.DivisionID = DivisionID
        obj.DepartmentID = DepartmentID
        obj.LocationID = locationid
        dtnew = obj.DetailsOrder_Location()
        Dim AllowedAllItems As Boolean = True
        If dtnew.Rows.Count <> 0 Then
            Try
                AllowedAllItems = dtnew.Rows(0)("AllowedAllItems")
            Catch ex As Exception
            End Try
        End If

        If Not Request.QueryString("lc") = Nothing Then
            CustomerTypeID = Request.QueryString("lc")
        END IF
        If Not Request.QueryString("k") = Nothing Then
            Dim keyword As String = ""
            keyword = Request.QueryString("k")

            If keyword <> "" Then
                Dim sql As String = "select top 100 ItemID,ItemDescription,ItemName, AverageCost as 'Price',PictureURL,wholesalePrice,InventoryItems.ItemUOM,InventoryItems.ItemColor from [InventoryItems] where  ItemID <> 'DEFAULT' AND CompanyID=@CompanyID AND DivisionID=@DivisionID AND DepartmentID=@DepartmentID  and InventoryItems.WireServiceIdAllowed = 1 and ([ItemID]  like '%" + keyword.Trim().Replace("'", "''") + "%' Or [ItemName] like '%" + keyword.Trim().Replace("'", "''") + "%'  Or [ItemUPCCode] like '%" + keyword.Trim().Replace("'", "''") + "%' ) "

                If Me.CompanyID = "DierbergsMarkets,Inc63017" Or Me.CompanyID = "QuickfloraDemo" Then
                    ' sql = " select top 35 [InventoryItems].UnitPrice , [InventoryItems].VendorID,[InventoryItems].UnitsPerBox , ItemID,ItemDescription,ItemName, AverageCost as 'Price',PictureURL,wholesalePrice,InventoryItems.ItemUOM,InventoryItems.ItemColor from [InventoryItems] "
                    If AllowedAllItems = False Then
                        sql = sql & "   AND ISNULL([ActiveForPOM],1) = 1 AND  ISNULL([ActiveForStore],1) = 1    "
                    Else
                        sql = sql & "   AND  ISNULL([ActiveForPOM],1) = 1   "
                    End If
                Else
                    sql = sql & "    And  InventoryItems.IsActive=1 "
                End If


                Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Dim myCommand As New SqlCommand(sql, myCon)
                myCommand.Parameters.AddWithValue("@CompanyID", CompanyID)
                myCommand.Parameters.AddWithValue("@DivisionID", DivisionID)
                myCommand.Parameters.AddWithValue("@DepartmentID", DepartmentID)

                Dim da As New SqlDataAdapter(myCommand)
                Dim dt As New DataTable

                'populate the data control and bind the data source

                da.Fill(dt)
                Dim str As String = "<table width='100%'   class='fixed_headers' >"
                str = str & "<thead><tr  > "
                str = str & "<th  >Item ID</th> <th  >Item Name</th> <th  >Price </th>  <th  >Description </th> "
                str = str & "</tr></thead><tbody>"

                If dt.Rows.Count <> 0 Then
                    Dim n As Integer = 0
                    Dim gridrow As Boolean = True
                    For Each row As DataRow In dt.Rows
                        n = n + 1
                        keyword = keyword.ToLower
                        Dim ItemDescription As String = row("ItemDescription").ToString()
                        ItemDescription = ItemDescription.ToLower
                        ItemDescription = ItemDescription.Replace(keyword, "<font color='red'>" + keyword + "</font>")

                        Dim ItemName As String = row("ItemName").ToString()
                        ItemName = ItemName.ToLower
                        ItemName = ItemName.Replace(keyword, "<font color='red'>" + keyword + "</font>")

                        Dim Itemprice As Double = 0
                        Try
                            Itemprice = row("Price").ToString()
                        Catch ex As Exception

                        End Try

                        If CustomerTypeID = "WHO" Or CustomerTypeID = "WHOC" Then
                            Try
                                Itemprice = row("wholesalePrice").ToString()
                            Catch ex As Exception

                            End Try
                        End If

                        If Me.CompanyID = "CamelbackFlowershop85018" Then
                            Try
                                Itemprice = row("wholesalePrice").ToString()
                            Catch ex As Exception
                                Itemprice = 0
                            End Try
                        End If

                        If ItemDescription.Trim = "" Then
                            ItemDescription = ItemName
                        End If

                        Dim ItemID As String = row("ItemID").ToString().Replace("'","")
                        ItemID = ItemID.ToUpper
                        ItemID = ItemID.Replace(keyword.ToUpper, "<font color='red'>" + keyword.ToUpper + "</font>")

                        Dim ItemUOM As String = ""
                        Try
                            ItemUOM = row("ItemUOM").ToString().Replace("'","")
                        Catch ex As Exception

                        End Try

                        Dim ItemColor As String = ""
                        Try
                            ItemColor = row("ItemColor").ToString().Replace("'","")
                        Catch ex As Exception

                        End Try

                        Dim finalsend As String = ""
                        Dim pr As Decimal = 0

                        Try
                            pr = Itemprice
                        Catch ex As Exception

                        End Try

                        finalsend = finalsend & row("ItemID").ToString().Replace("'", "").Replace(";", "").Replace("~!", "").Replace("""", "").Replace("(", "").Replace(")", "")
                        finalsend = finalsend & "~!"
                        Try
                            finalsend = finalsend & Format(pr, "0.00").ToString().Replace("'", "").Replace(";", "").Replace("~!", "").Replace("""", "").Replace("(", "").Replace(")", "")
                        Catch ex As Exception

                        End Try
                        finalsend = finalsend & "~!"
                        finalsend = finalsend & row("ItemName").ToString().Replace("'", "").Replace(";", "").Replace("~!", "").Replace("""", "").Replace("(", "").Replace(")", "")

                        finalsend = finalsend & "~!"
                        finalsend = finalsend & ItemColor.ToString().Replace("'", "").Replace(";", "").Replace("~!", "").Replace("""", "").Replace("(", "").Replace(")", "")

                        finalsend = finalsend & "~!"
                        finalsend = finalsend & ItemUOM.ToString().Replace("'", "").Replace(";", "").Replace("~!", "").Replace("""", "").Replace("(", "").Replace(")", "")


                        'grid-alternative-row
                        If gridrow Then
                            str = str & "<tr align='left'    id='InventoryInfoGrid_tr" & n & "'  >"
                            gridrow = False
                        Else
                            str = str & "<tr align='left'     id='InventoryInfoGrid_tr" & n & "'  >"
                            gridrow = True
                        End If


                        str = str & "<td align='left'><a  href=" & """" & "javascript:FillSearchtextBox2('" + finalsend + "')" + """" + " >" + ItemID + "</a></td>"
                        str = str & "<td >" & ItemName & "</td>"
                        str = str & "<td align='left'>$"  & Format(Itemprice, "0.00") & ""
                        str = str & "<div id='InventoryInfoGrid_tr" & n & "_gridPopup' style='display: none; z-index: 101; position: absolute;'><br />"
                        str = str & "<img border='0' src='" & returl(row("PictureURL").ToString()) & "'></div>"
                        str = str & "</td>"
                        str = str & "<td >" & ItemDescription & "</td>"
                        str = str & "</tr>"
                        'Response.Write("<div style='text-decoration:none; border:dotted; border-width:1px; border-color:White; width:660px;'><a  href=" & """" & "javascript:FillSearchtextBox2('[" + row("ItemID").ToString() + "]')" + """" + " > <img  width='25' height='25' src='" + returl(row("PictureURL").ToString()) + "' /> <strong>" + ItemID + "</strong>  <i>" + ItemName + "</i> : " + ItemDescription + "</a></div>")

                    Next
                    str = str & "</tbody></table>"
                    Response.Write(str)
                    result = str
                    Response.End()
                Else
                    Response.End()
                    Response.Clear()
                    Response.Write("")
                End If

            End If

        End If

    End Sub

    Public Function returl(ByVal ob As String) As String

        Dim DocumentDir As String = ""
        Dim img1 As String = ""
        Dim ImgName As String = ob.ToLower()
        DocumentDir = ConfigurationManager.AppSettings("InvPath")
        If (ImgName.Trim() = "") Then

            Return "itemimages/no_image.gif"

        Else

            If System.IO.File.Exists(DocumentDir & ImgName.Trim()) Then

                Return "itemimages/" & ImgName.Trim()

            Else
                Return "itemimages/no_image.gif"
            End If




        End If


    End Function

</script>
 
  <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    
</head>
<body>
    <form id="form1" runat="server">
   
    </form>
</body>
</html>