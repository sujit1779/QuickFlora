Imports System.Data
Imports System.Data.SqlClient

Partial Class Report1
    Inherits System.Web.UI.Page

    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""

    Public parameter As String = ""

    Public EmployeeID As String = ""

    Public Function PopulateImage(ByVal ob As String) As String
        Dim DocumentDir As String = ""
        Dim img1 As String = ""
        Dim ImgName As String = ob.ToLower()
        DocumentDir = ConfigurationManager.AppSettings("DocPath")
        If (ImgName.Trim() = "") Then

            Return "../../images/no_image.gif"

        Else

            If System.IO.File.Exists(DocumentDir & ImgName.Trim()) Then

                Return "../../images/" & ImgName.Trim()

            Else
                Return "../../images/no_image.gif"
            End If




        End If


    End Function


    Private Sub Report1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        Dim company As String = CType(SessionKey("CompanyID"), String)
        Dim UserName As String = CType(SessionKey("EmployeeID"), String)
        Dim DivID As String = CType(SessionKey("DivisionID"), String)
        Dim DeptID As String = CType(SessionKey("DepartmentID"), String)
        CompanyID = company
        DivisionID = DivID
        DepartmentID = DeptID
        EmployeeID = UserName

        If Not Page.IsPostBack Then
            ' SetLocationIDdropdown()



            Dim tm As DateTime
            tm = DateTime.Now
            txtDeliveryDate.Text = tm.Month & "/" & tm.Day & "/" & tm.Year
            ' txtstart.Text = tm.Month & "/" & tm.Day & "/" & tm.Year
            tm = tm.AddDays(7)
            txtDeliveryDateTO.Text = tm.Month & "/" & tm.Day & "/" & tm.Year

            GetInventoryItemsList()

        End If

    End Sub



    Public Function GetInventoryItemsListNew() As DataSet
        Dim locationid As String = ""
        Try
            locationid = Session("Locationid")
        Catch ex As Exception

        End Try
        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetInventoryItemsListAvailbilityListBetaNewPage2]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("@Location", locationid)

                Command.Parameters.AddWithValue("@ArrivalDate", "1/1/1900")
                Command.Parameters.AddWithValue("@startavailabledate", txtDeliveryDate.Text)
                Command.Parameters.AddWithValue("@endavailabledate", txtDeliveryDateTO.Text)

                Command.Parameters.AddWithValue("@ExcludeArrivalDate", chkexculea.Checked)
                ',@Condition NVARCHAR (36)=NULL, @fieldName NVARCHAR (36)=NULL, @fieldexpression NVARCHAR (400)=NULL
                Command.Parameters.AddWithValue("@Condition", "")
                Command.Parameters.AddWithValue("@fieldName", "")
                Command.Parameters.AddWithValue("@fieldexpression", "")
                ', @ArrivalDate datetime = NULL 
                ', @startavailabledate datetime = NULL 
                ', @endavailabledate datetime = NULL 


                Try
                    Dim ds As New DataSet
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)

                    If ds.Tables(0).Rows.Count >= 0 Then
                        Return ds
                    Else
                        Return Nothing
                    End If

                Catch ex As Exception
                    lblInfo.Text = ex.Message
                    Return Nothing

                End Try

            End Using
        End Using

    End Function



    Private Sub GetInventoryItemsList()

        Dim ds As New DataSet
        ds = GetInventoryItemsListNew()
        gvItemsList.DataSource = ds
        gvItemsList.DataBind()
        Try
            If ds.Tables(0).Rows.Count > 0 Then

                lblInfo.Text = ds.Tables(0).Rows.Count.ToString + " records found"
            Else
                lblInfo.Text = "0 records found"
            End If
        Catch ex As Exception

        End Try

        'If ds.Tables.Count > 0 Then

        '    If ds.Tables(0).Rows.Count > 0 Then

        '        lblInfo.Text = ds.Tables(0).Rows.Count.ToString + " records found"
        '    Else
        '        lblInfo.Text = "0 records found"
        '    End If
        'Else
        '    lblInfo.Text = "0 records found"
        'End If


    End Sub

    Protected Sub gvItemsList_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gvItemsList.PageIndexChanging

        gvItemsList.PageIndex = e.NewPageIndex
        GetInventoryItemsList()

        'txtDateTo 
        'txtstart 
        'txtend 


    End Sub

    Private Sub SearchButton_Click(sender As Object, e As EventArgs) Handles SearchButton.Click
        GetInventoryItemsList()
    End Sub
End Class
