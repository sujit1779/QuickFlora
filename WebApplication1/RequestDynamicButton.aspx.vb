Option Strict Off

Imports System.Data.SqlClient
Imports System.Data


Partial Class RequestDynamicButton
    Inherits System.Web.UI.Page

    Public ConnectionString As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim EmployeeID As String = ""
    Dim inlinenumber As String = ""
    Dim obj As New clsTruckingSchedule


    Public Function GetCategorylist(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, _
                                        Optional ByVal inlinenumber As String = "") As DataTable

        Dim dt As New DataTable

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("Select * from [AddProductCategorylist] Where CompanyID = @CompanyID AND DivisionID=@DivisionID AND DepartmentID=@DepartmentID AND inlinenumber=@inlinenumber ", Connection)
                Command.CommandType = CommandType.Text
                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("inlinenumber", inlinenumber)
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

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")

        If Not IsPostBack Then

            FillItemFamilies()

            inlinenumber = ""
            Try
                inlinenumber = Request.QueryString("inlinenumber")
            Catch ex As Exception
            End Try

            Dim dt As New DataTable
            If inlinenumber <> "" Then
                dt = GetCategorylist(CompanyID, DepartmentID, DivisionID, inlinenumber)

                Try
                    If dt.Rows.Count <> 0 Then
                        lblerror.Text = lblerror.Text & "<br>" & "dt.Rows.Count:" & dt.Rows.Count
                        txtbutton1.Text = dt.Rows(0)("ButtonName").ToString()

                        Try
                            drpfamily1.SelectedValue = dt.Rows(0)("ItemFamilyID").ToString()
                        Catch ex As Exception
                        End Try

                        drpcategory1.Items.Clear()

                        Dim ds As New DataSet
                        ds = GetItemCategories(drpfamily1.SelectedValue)
                        drpcategory1.DataSource = ds
                        drpcategory1.DataTextField = "CategoryName"
                        drpcategory1.DataValueField = "ItemCategoryID"
                        drpcategory1.DataBind()
                        drpcategory1.Items.Insert(0, "")

                        Try
                            drpcategory1.SelectedValue = dt.Rows(0)("ItemCategoryID").ToString()
                        Catch ex As Exception
                        End Try

                    End If

                Catch ex As Exception
                    ' lbldebug.Text = lbldebug.Text & "<br>" & " ex:" & ex.Message
                End Try
            Else
                'dt = obj.GetTruckingSchedules(CompanyID, DepartmentID, DivisionID)
            End If




        End If

        'lbldebug.Visible = False

    End Sub

    Protected Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Try
            inlinenumber = Request.QueryString("inlinenumber")
        Catch ex As Exception

        End Try

        If inlinenumber = "" Then

        Else

            'obj.UpdateTruckingSchedule(CompanyID, DivisionID, DepartmentID, ScheduleID, drpInventoryOrigin.SelectedValue, drpShipMethod.SelectedValue, drpShipMethod.SelectedItem.Text, drpLocation.SelectedValue, drpTruckingDay.SelectedValue, drpArrivalDay.SelectedValue, drpDayCutOff.SelectedValue, drpHours.SelectedValue, drpMinutes.SelectedValue, drpAMPM.SelectedValue, drpTimeZone.SelectedValue)
            UpdateAddProductCategorylist(CompanyID, DivisionID, DepartmentID, inlinenumber, txtbutton1.Text, drpfamily1.SelectedValue, drpcategory1.SelectedValue)
            Response.Redirect("RequestDynamicButtonList.aspx")

        End If


    End Sub
    ' ButtonName, ItemFamilyID, ItemCategoryID

    Public Function UpdateAddProductCategorylist(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal inlinenumber As String, ByVal ButtonName As String, ByVal ItemFamilyID As String, ByVal ItemCategoryID As String) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("  Update AddProductCategorylist SET  ButtonName = @ButtonName , ItemFamilyID = @ItemFamilyID , ItemCategoryID =@ItemCategoryID  Where CompanyID = @CompanyID AND DivisionID =@DivisionID AND DepartmentID = @DepartmentID AND inlinenumber = @inlinenumber   ", Connection)
                Command.CommandType = CommandType.Text

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("@inlinenumber", inlinenumber)
                Command.Parameters.AddWithValue("@ButtonName", ButtonName)
                Command.Parameters.AddWithValue("@ItemFamilyID", ItemFamilyID)
                Command.Parameters.AddWithValue("@ItemCategoryID", ItemCategoryID)


                Try
                    Command.Connection.Open()
                    Command.ExecuteNonQuery()
                    Command.Connection.Close()

                    Return True

                Catch ex As Exception
                    Return False
                End Try

            End Using
        End Using

    End Function


    Public Function Fillfamily() As DataTable
        Dim connec As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from InventoryFamilies where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID

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


    Public Function FillDetailscategory(ByVal familyid As String) As DataTable
        Dim connec As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from InventoryCategories where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and ItemFamilyID=@f3 "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = familyid

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



    Private Sub FillItemFamilies()

        Dim dtfamily As New DataTable
        dtfamily = Fillfamily()

        drpfamily1.DataSource = dtfamily
        drpfamily1.DataTextField = "FamilyName"
        drpfamily1.DataValueField = "ItemFamilyID"
        drpfamily1.DataBind()

    End Sub



    Public Function GetItemCategories(ByVal ItemFamilyID As String) As DataSet

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetItemCategoriesByItemFamily]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                Command.Parameters.AddWithValue("ItemFamilyID", ItemFamilyID)

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
                    ' Debug = ex.Message
                    Return Nothing

                End Try

            End Using
        End Using

    End Function

    Private Sub btnback_Click(sender As Object, e As EventArgs) Handles btnback.Click
        Response.Redirect("RequestDynamicButtonList.aspx")
    End Sub

    Private Sub drpfamily1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drpfamily1.SelectedIndexChanged
        Dim ds As New DataSet
        ds = GetItemCategories(drpfamily1.SelectedValue)
        drpcategory1.Items.Clear()
        drpcategory1.DataSource = ds
        drpcategory1.DataTextField = "CategoryName"
        drpcategory1.DataValueField = "ItemCategoryID"
        drpcategory1.DataBind()
        drpcategory1.Items.Insert(0, "")
    End Sub
End Class

