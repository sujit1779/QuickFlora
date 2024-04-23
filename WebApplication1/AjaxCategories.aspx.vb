
Imports System.Data
Imports System.Data.SqlClient

Partial Class AjaxCategories
    Inherits System.Web.UI.Page


    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""

    Public Function GetCategories(ByVal ItemFamilyID As String) As DataTable

        Dim DT As New DataTable

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand(" SELECT ItemCategoryID,CategoryName  FROM InventoryCategories Where  [CompanyID] =@CompanyID AND [DivisionID] = @DivisionID AND [DepartmentID] =@DepartmentID AND ItemFamilyID=@ItemFamilyID ", Connection)
                Command.CommandType = CommandType.Text

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("ItemFamilyID", ItemFamilyID)

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
        Dim id As String = ""
        id = Request.QueryString("id")

        'ItemFamilyID,FamilyName
        Response.ContentType = "application/json"
        Response.Clear()
        Dim JSON As String = ""
        JSON = JSON + "["
        Dim dt As New DataTable
        dt = GetCategories(id)
        Dim n As Integer
        For n = 0 To dt.Rows.Count - 1
            If n = 0 Then
                JSON = JSON + "  {""ItemCategoryID"": """ + dt.Rows(n)("ItemCategoryID").ToString() + """, ""CategoryName"": """ + dt.Rows(n)("CategoryName").ToString() + """}"
            Else
                JSON = JSON + "  ,{""ItemCategoryID"": """ + dt.Rows(n)("ItemCategoryID").ToString() + """, ""CategoryName"": """ + dt.Rows(n)("CategoryName").ToString() + """}"
            End If
        Next


        JSON = JSON + "  ]"
        Response.Write(JSON)
        Response.End()

    End Sub

End Class
