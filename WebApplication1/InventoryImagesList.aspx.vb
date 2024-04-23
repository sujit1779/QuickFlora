Option Strict Off
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration
Imports AddressClass
Imports System.Drawing
Imports System.IO

Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D


Imports EnterpriseASPClient.Core
Imports EnterpriseClient.Core
Partial Class InventoryImagesList
    Inherits System.Web.UI.Page

    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            PopulateInventoryItemImages()
            Session("flag") = ""
        End If
    End Sub
    Sub PopulateInventoryItemImages()
        Dim filters As EnterpriseCommon.Core.FilterSet
        filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
        CompanyID = filters!CompanyID
        DivisionID = filters!DivisionID
        DepartmentID = filters!DepartmentID

        Dim objUser As New DAL.CustomOrder()

        Dim ds As DataTable

        ds = GetBH()
        grdInventory.DataSource = ds
        grdInventory.DataBind()

    End Sub


    Public Function GetBH() As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim DT As New DataTable

        Dim sql As String = ""
        sql = " SELECT Top 500  [ItemID],ISNULL([ItemName],'') AS ItemName,ISNULL([Price],0) AS Price,ISNULL(PictureURL,'noimagenew.png') AS PictureURL,ISNULL(MediumPictureURL,'noimagenew.png') AS MediumPictureURL,ISNULL(LargePictureURL,'noimagenew.png') AS LargePictureURL,ISNULL(ThumbnailImage ,'noimagenew.png') AS ThumbnailImage FROM  [InventoryItems] Where   [CompanyID] =@CompanyID AND [DivisionID] = @DivisionID AND [DepartmentID] =@DepartmentID "
        If txtSearchExpression.Text.Trim <> "" Then
            If drpCondition.SelectedValue = "=" Then
                sql = sql & " AND  " & drpFieldName.SelectedValue & "= '" & txtSearchExpression.Text.Trim & "'"
            End If
            If drpCondition.SelectedValue = "Like" Then
                sql = sql & " AND  " & drpFieldName.SelectedValue & " Like '%" & txtSearchExpression.Text.Trim & "%'"
            End If
        End If

        Using Connection As New SqlConnection(constr)
            Using Command As New SqlCommand(sql, Connection)
                Command.CommandType = CommandType.Text

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)


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






    Public Sub NasCheck(ByVal ob As String)
        Dim DocumentDir As String = ""
        Dim ImageDir As String = ""
        Dim ImgName As String = ob.ToLower()
        DocumentDir = ConfigurationManager.AppSettings("ImgPath")
        ImageDir = ConfigurationManager.AppSettings("InvPath")
        If (ImgName.Trim() = "") Then
        Else
            If System.IO.File.Exists(DocumentDir & ImgName.Trim()) Then
            Else
                If FileIO.FileSystem.FileExists("\\Nas\QF_Shared\" & ImgName.Trim()) Then
                    Dim imgPhoto As Image = Image.FromFile("\\Nas\QF_Shared\" & ImgName.Trim())
                    Try
                        imgPhoto.Save(DocumentDir & ImgName.Trim(), ImageFormat.Jpeg)
                        imgPhoto.Save(ImageDir & ImgName.Trim(), ImageFormat.Jpeg)
                    Catch Exc As Exception
                        'Response.Write("Error: " & Exc.Message)
                    End Try
                    imgPhoto.Dispose()
                End If
            End If
        End If
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

            Return "https://secure.quickflora.com/itemimages/noimagenew.png"

        Else

            If System.IO.File.Exists(DocumentDir & ImgName.Trim()) Then
                Return "https://secure.quickflora.com/itemimages/" & ImgName.Trim()
                ''Return "../../images/products/" & ImgName.Trim()

            Else
                Return "https://secure.quickflora.com/itemimages/noimagenew.png"
            End If




        End If


    End Function

    Public Function returl1(ByVal ob As String) As String
        ''NasCheck(ob)
        Dim DocumentDir As String = ""
        Dim img1 As String = ""
        Dim ImgName As String = ob.ToLower()
        DocumentDir = ConfigurationManager.AppSettings("InvPath")
        If (ImgName.Trim() = "") Then

            Return "../../images/products/no_image.gif"

        Else

            If System.IO.File.Exists(DocumentDir & ImgName.Trim()) Then

                Return "../../images/products/" & ImgName.Trim()

            Else
                Return "../../images/products/no_image.gif"
            End If




        End If


    End Function
    Public Function returl2(ByVal ob As String) As String
        ''NasCheck(ob)
        Dim DocumentDir As String = ""
        Dim img1 As String = ""
        Dim ImgName As String = ob.ToLower()
        DocumentDir = ConfigurationManager.AppSettings("InvPath")
        If (ImgName.Trim() = "") Then

            Return "../../images/products/no_image.gif"

        Else

            If System.IO.File.Exists(DocumentDir & ImgName.Trim()) Then

                Return "../../images/products/" & ImgName.Trim()

            Else
                Return "../../images/products/no_image.gif"
            End If




        End If


    End Function

    Protected Sub grdInventory_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdInventory.PageIndexChanging
        grdInventory.PageIndex = e.NewPageIndex
        PopulateInventoryItemImages()

    End Sub

    Protected Sub grdInventory_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grdInventory.RowEditing
        Dim ItemID As String = grdInventory.DataKeys(e.NewEditIndex).Value.ToString()
        Response.Redirect("InventoryItemImages.aspx?ItemID=" & ItemID)

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        PopulateInventoryItemImages()

    End Sub


End Class
