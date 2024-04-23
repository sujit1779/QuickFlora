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
            ' locationid = Session("OrderLocationid")

        Catch ex As Exception

        End Try

        CompanyID = Session("CompanyID")
        DivisionID =  Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID =   Session("EmployeeID")


        If Not Request.QueryString("locationid") = Nothing Then
            locationid = Request.QueryString("locationid")
        End If
        If Not Request.QueryString("k") = Nothing Then
            Dim keyword As String = ""
            keyword = Request.QueryString("k")
            'GetInventoryItemsListAvailbilityListAjaxBeta
            If keyword <> "" Then
                'Dim sql As String = "select top 35 ItemID,ItemName,AverageCost from [InventoryItems] where  ItemID <> 'DEFAULT' AND CompanyID=@CompanyID AND DivisionID=@DivisionID AND DepartmentID=@DepartmentID AND  InventoryItems.IsActive=1 and InventoryItems.WireServiceIdAllowed = 1 and ([ItemID]  like '%" + keyword.Trim().Replace("'", "''") + "%' Or [ItemName] like '%" + keyword.Trim().Replace("'", "''") + "%'  Or [ItemUPCCode] like '%" + keyword.Trim().Replace("'", "''") + "%' ) "
                'Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                'Dim myCommand As New SqlCommand(sql, myCon)
                'myCommand.Parameters.AddWithValue("@CompanyID", CompanyID)
                'myCommand.Parameters.AddWithValue("@DivisionID", DivisionID)
                'myCommand.Parameters.AddWithValue("@DepartmentID", DepartmentID)

                'Dim da As New SqlDataAdapter(myCommand)
                Dim dt As New DataTable

                Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                    Using Command As New SqlCommand("[enterprise].[GetInventoryItemsListAvailbilityListAjaxBetaNewpage2]", connection)
                        Command.CommandType = CommandType.StoredProcedure

                        Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                        Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                        Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)
                        Command.Parameters.AddWithValue("@Location", locationid)

                        Command.Parameters.AddWithValue("@ArrivalDate", DateTime.Now)
                        Command.Parameters.AddWithValue("@ExcludeArrivalDate", True)
                        ',@Condition NVARCHAR (36)=NULL, @fieldName NVARCHAR (36)=NULL, @fieldexpression NVARCHAR (400)=NULL
                        Command.Parameters.AddWithValue("@Condition", "")
                        Command.Parameters.AddWithValue("@fieldName", "")
                        Command.Parameters.AddWithValue("@fieldexpression", keyword)


                        Dim da As New SqlDataAdapter(Command)
                        da.Fill(dt)

                    End Using
                End Using


                Dim str As String = "<table width='100%'   class='table table-striped table-hover table-bordered' >"
                If dt.Rows.Count <> 0 Then
                    Dim n As Integer = 0
                    Dim gridrow As Boolean = True
                    For Each row As DataRow In dt.Rows
                        n = n + 1
                        keyword = keyword.ToLower
                        Dim ItemDescription As String = ""
                        'ItemDescription
                        Try
                            ItemDescription = row("ItemDescription").ToString()
                        Catch ex As Exception

                        End Try
                        Dim ItemName As String = ""
                        Try
                            ItemName = row("ItemName").ToString()
                        Catch ex As Exception

                        End Try
                        ItemName = ItemName.ToLower
                        ItemName = ItemName.Replace(keyword, "<font color='red'>" + keyword + "</font>")

                        Dim Location As String = ""
                        Try
                            Location = row("Location").ToString()
                            Location = Location.ToLower
                            Location = Location.Replace(keyword, "<font color='red'>" + keyword + "</font>")
                        Catch ex As Exception

                        End Try

                        'Variety
                        Dim Variety As String = ""
                        Try
                            Variety = row("Variety").ToString()
                            Variety = Variety.ToLower
                            Variety = Variety.Replace(keyword, "<font color='red'>" + keyword + "</font>")
                        Catch ex As Exception

                        End Try
                        Dim ItemColor As String = ""
                        Try
                            ItemColor = row("ItemColor").ToString()
                            ItemColor = ItemColor.ToLower
                            ItemColor = ItemColor.Replace(keyword, "<font color='red'>" + keyword + "</font>")
                        Catch ex As Exception

                        End Try
                        Dim FlowerType As String = ""
                        Try
                            FlowerType = row("FlowerType").ToString()
                            FlowerType = FlowerType.ToLower
                            FlowerType = FlowerType.Replace(keyword, "<font color='red'>" + keyword + "</font>")
                        Catch ex As Exception

                        End Try
                        Dim GroupCode As String = ""
                        Try
                            GroupCode = row("GroupCode").ToString()
                            GroupCode = GroupCode.ToLower
                            GroupCode = GroupCode.Replace(keyword, "<font color='red'>" + keyword + "</font>")
                        Catch ex As Exception

                        End Try
                        Dim Grade As String = ""
                        Try
                            Grade = row("Grade").ToString()
                            Grade = Grade.ToLower
                            Grade = Grade.Replace(keyword, "<font color='red'>" + keyword + "</font>")
                        Catch ex As Exception

                        End Try

                        'ItemColor,FlowerType,GroupCode,Grade


                        ' )


                        If ItemDescription.Trim <> "" Then
                            ItemDescription = ItemDescription.ToLower
                            ItemDescription = ItemDescription.Replace(keyword, "<font color='red'>" + keyword + "</font>")
                        End If

                        Dim ItemID As String = row("ItemID").ToString().Replace("'","")
                        ItemID = ItemID.ToUpper
                        ItemID = ItemID.Replace(keyword.ToUpper, "<font color='red'>" + keyword.ToUpper + "</font>")



                        Dim finalsend As String = ""
                        Dim pr As Decimal = 0



                        finalsend = finalsend & row("ItemID").ToString().Replace("'", "").Replace(";", "").Replace("~!", "").Replace("""", "").Replace("(", "").Replace(")", "")


                        'grid-alternative-row
                        If gridrow Then
                            str = str & "<tr align='left'    id='InventoryInfoGrid_tr" & n & "'  >"
                            gridrow = False
                        Else
                            str = str & "<tr align='left'     id='InventoryInfoGrid_tr" & n & "'  >"
                            gridrow = True
                        End If
                        'ItemColor,FlowerType,GroupCode,Grade

                        str = str & "<td align='left'><a  href=" & """" & "javascript:FillSearchtextBox2('" + finalsend + "')" + """" + " >" + ItemID + "</a></td>"
                        str = str & "<td >" & ItemName & "</td>"
                        str = str & "<td align='left'> " & Location & "</td>"
                        str = str & "<td >" & Variety & "</td>"
                        str = str & "<td >" & ItemColor & "</td>"
                        str = str & "<td >" & FlowerType & "</td>"
                        str = str & "<td >" & GroupCode & "</td>"
                        str = str & "<td >" & Grade & "</td>"
                        str = str & "<td >" & ItemDescription & "</td>"
                        str = str & "</tr>"
                        'Response.Write("<div style='text-decoration:none; border:dotted; border-width:1px; border-color:White; width:660px;'><a  href=" & """" & "javascript:FillSearchtextBox2('[" + row("ItemID").ToString() + "]')" + """" + " > <img  width='25' height='25' src='" + returl(row("PictureURL").ToString()) + "' /> <strong>" + ItemID + "</strong>  <i>" + ItemName + "</i> : " + ItemDescription + "</a></div>")

                    Next
                    str = str & "</table>"
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