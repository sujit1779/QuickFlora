Option Strict Off
Imports System.Data
Imports System.Data.SqlClient

Partial Class RequestDynamicButtonList
    Inherits System.Web.UI.Page

    Public CompanyID As String = ""
    Public DivisionID As String = ""
    Public DepartmentID As String = ""


    Public Function GetCategorylist(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As DataTable

        Dim dt As New DataTable
        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand(" Select * from [AddProductCategorylist] Where CompanyID = @CompanyID AND DivisionID=@DivisionID AND DepartmentID=@DepartmentID  ", Connection)
                Command.CommandType = CommandType.Text
                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(dt)
                Catch ex As Exception
                    lblerror.Text = ex.Message
                End Try
            End Using
        End Using
        Return dt

    End Function


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        CompanyID = CType(SessionKey("CompanyID"), String)
        DivisionID = CType(SessionKey("DivisionID"), String)
        DepartmentID = CType(SessionKey("DepartmentID"), String)

        Dim dt As New DataTable

        If Not Page.IsPostBack Then

            dt = GetCategorylist(CompanyID, DivisionID, DepartmentID)

            If dt.Rows.Count > 0 Then
                categorylist.DataSource = dt
                categorylist.DataBind()
            End If
        End If
    End Sub


    Private Sub categorylist_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles categorylist.RowEditing
        Dim inlinenumber As String = categorylist.DataKeys(e.NewEditIndex).Values(0).ToString()
        If inlinenumber <> "" Then
            Response.Redirect(String.Format("RequestDynamicButton.aspx?inlinenumber={0}", inlinenumber))
        End If
    End Sub


End Class
