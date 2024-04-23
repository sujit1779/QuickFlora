
Imports System.Data
Imports System.Data.SqlClient

Partial Class AjaxFamily
    Inherits System.Web.UI.Page


    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""

    Private Sub AjaxFamily_Load(sender As Object, e As EventArgs) Handles Me.Load

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")


        'ItemFamilyID,FamilyName
        Response.ContentType = "application/json"
        Response.Clear()
        Dim JSON As String = ""
        JSON = JSON + "["
        Dim dt As New DataTable
        dt = GetFamily()
        Dim n As Integer
        For n = 0 To dt.Rows.Count - 1
            If n = 0 Then
                JSON = JSON + "  {""ItemFamilyID"": """ + dt.Rows(n)("ItemFamilyID").ToString() + """, ""FamilyName"": """ + dt.Rows(n)("FamilyName").ToString() + """}"
            Else
                JSON = JSON + "  ,{""ItemFamilyID"": """ + dt.Rows(n)("ItemFamilyID").ToString() + """, ""FamilyName"": """ + dt.Rows(n)("FamilyName").ToString() + """}"
            End If
        Next


        JSON = JSON + "  ]"
        Response.Write(JSON)
        Response.End()

    End Sub

    Public Function GetFamily() As DataTable

        Dim DT As New DataTable

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand(" SELECT ItemFamilyID,FamilyName  FROM InventoryFamilies Where  [CompanyID] =@CompanyID AND [DivisionID] = @DivisionID AND [DepartmentID] =@DepartmentID ", Connection)
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

End Class
