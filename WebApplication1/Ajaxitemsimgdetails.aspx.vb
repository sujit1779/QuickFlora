
Imports System.Data
Imports System.Data.SqlClient


Partial Class Ajaxitemsimgdetails
    Inherits System.Web.UI.Page

    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""

    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")


    Public Function GetBH(ByVal ItemID As String) As DataTable

        Dim DT As New DataTable

        Using Connection As New SqlConnection(constr)
            Using Command As New SqlCommand(" SELECT  [ItemID],ISNULL([ItemName],'') AS ItemName,ISNULL([Price],0) AS Price,ISNULL(PictureURL,'noimagenew.png') AS PictureURL,ISNULL(MediumPictureURL,'noimagenew.png') AS MediumPictureURL,ISNULL(LargePictureURL,'noimagenew.png') AS LargePictureURL,ISNULL(ThumbnailImage ,'noimagenew.png') AS ThumbnailImage  FROM  [InventoryItems] Where ItemID=@ItemID AND  [CompanyID] =@CompanyID AND [DivisionID] = @DivisionID AND [DepartmentID] =@DepartmentID ", Connection)
                Command.CommandType = CommandType.Text

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("ItemID", ItemID)

                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(DT)
                    Return DT

                Catch ex As Exception

                    Return DT

                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")


        '[ItemID],[ItemName],[Price],PictureURL,[BHID],BHITMID
        Dim ItemID As String = ""
        ItemID = Request.QueryString("ItemID")

        '[ItemID],[ItemName],[Price],PictureURL,[BHID],BHITMID
        Response.ContentType = "application/json"
        Response.Clear()
        Dim JSON As String = ""
        JSON = JSON + "["
        Dim dt As New DataTable
        dt = GetBH(ItemID)
        Dim n As Integer
        For n = 0 To dt.Rows.Count - 1
            If n = 0 Then
                JSON = JSON + "  {""ItemID"": """ + dt.Rows(n)("ItemID").ToString() + """, ""ItemName"": """ + dt.Rows(n)("ItemName").ToString() + """, ""Price"": """ + dt.Rows(n)("Price").ToString() + """, ""PictureURL"": """ + returl(dt.Rows(n)("PictureURL").ToString()) + """, ""MediumPictureURL"": """ + returl(dt.Rows(n)("MediumPictureURL").ToString()) + """, ""LargePictureURL"": """ + returl(dt.Rows(n)("LargePictureURL").ToString()) + """, ""ThumbnailImage"": """ + returl(dt.Rows(n)("ThumbnailImage").ToString()) + """}"
            Else
                JSON = JSON + "  {""ItemID"": """ + dt.Rows(n)("ItemID").ToString() + """, ""ItemName"": """ + dt.Rows(n)("ItemName").ToString() + """, ""Price"": """ + dt.Rows(n)("Price").ToString() + """, ""PictureURL"": """ + returl(dt.Rows(n)("PictureURL").ToString()) + """, ""MediumPictureURL"": """ + returl(dt.Rows(n)("MediumPictureURL").ToString()) + """, ""LargePictureURL"": """ + returl(dt.Rows(n)("LargePictureURL").ToString()) + """, ""ThumbnailImage"": """ + returl(dt.Rows(n)("ThumbnailImage").ToString()) + """}"
            End If
        Next


        JSON = JSON + "  ]"
        Response.Write(JSON)
        Response.End()

    End Sub



    Public Function returl(ByVal ob As Object) As String
        '''NasCheck(ob)


        Dim DocumentDir As String = ""
        Dim img1 As String = ""
        Dim ImgName As String = ""
        Try
            ImgName = ob.ToString()
        Catch ex As Exception

        End Try
        DocumentDir = "D:\WebApps\QuickFloraFrontEnd\itemimages\" ' ConfigurationManager.AppSettings("InvPath")

        If (ImgName.Trim() = "") Then

            Return "noimagenew.png"

        Else

            If System.IO.File.Exists(DocumentDir & ImgName.Trim()) Then
                Return "" & ImgName.Trim()
                ''Return "../../images/products/" & ImgName.Trim()

            Else
                Return "noimagenew.png"
            End If




        End If


    End Function


End Class
