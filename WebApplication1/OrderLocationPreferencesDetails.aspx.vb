Imports System.Data
Imports System.Data.SqlClient

Partial Class OrderLocationPreferencesDetails
    Inherits System.Web.UI.Page
    Public CompanyID As String
    Public DivisionID As String
    Public DepartmentID As String
    Public EmployeeID As String
    Public LocationID As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")


        If Not IsPostBack Then



            Dim obj As New clsOrder_Location

            If (Request.QueryString("ID") <> "") Then
                Dim dtnew As New System.Data.DataTable()
                obj.CompanyID = CompanyID
                obj.DivisionID = DivisionID
                obj.DepartmentID = DepartmentID
                obj.LocationID = Request.QueryString("ID")

                dtnew = obj.DetailsOrder_Location()

                If dtnew.Rows.Count <> 0 Then

                    Try
                        txtLocationID.Text = dtnew.Rows(0)("LocationID")
                        txtLocationID.Enabled = False
                        txtLocationName.Text = dtnew.Rows(0)("LocationName")
                        txtCity.Text = dtnew.Rows(0)("City")
                        txtState.Text = dtnew.Rows(0)("State")
                        txtZipCode.Text = dtnew.Rows(0)("ZipCode")
                        txtCountry.Text = dtnew.Rows(0)("Country")
                        txtFax.Text = dtnew.Rows(0)("Fax")
                        txtEmail.Text = dtnew.Rows(0)("Email")
                    Catch ex As Exception

                    End Try
                    Try
                        txtNotes.Text = dtnew.Rows(0)("Notes")
                    Catch ex As Exception

                    End Try

                    Try
                        txtphone.Text = dtnew.Rows(0)("Phone")
                    Catch ex As Exception

                    End Try


                    Try
                        txtadddress.Text = dtnew.Rows(0)("Add1")
                    Catch ex As Exception

                    End Try


                    Try
                        chkenable.Checked = dtnew.Rows(0)("AllowLocationAddress")
                    Catch ex As Exception

                    End Try


                    Try
                        chkAllowedAllItems.Checked = dtnew.Rows(0)("AllowedAllItems")
                    Catch ex As Exception

                    End Try

                End If
            End If
        End If
    End Sub


    Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click
        Dim obj As New clsOrder_Location
         

        obj.CompanyID = CompanyID
        obj.DivisionID = DivisionID
        obj.DepartmentID = DepartmentID
        LocationID = txtLocationID.Text



        If (Request.QueryString("ID") <> "") Then

            Try
                obj.CompanyID = CompanyID
                obj.DivisionID = DivisionID
                obj.DepartmentID = DepartmentID
                obj.LocationID = txtLocationID.Text
                obj.LocationName = txtLocationName.Text
                obj.City = txtCity.Text
                obj.State = txtState.Text
                obj.ZipCode = txtZipCode.Text
                obj.Country = txtCountry.Text
                obj.Fax = txtFax.Text
                obj.Email = txtEmail.Text

                If obj.Update Then
                    Update()
                    Response.Redirect("OrderLocationPreferences.aspx")
                End If

            Catch ex As Exception
                lblerror.Text = ex.Message
            End Try

        Else

            Try
                obj.CompanyID = CompanyID
                obj.DivisionID = DivisionID
                obj.DepartmentID = DepartmentID
                obj.LocationID = txtLocationID.Text
                obj.LocationName = txtLocationName.Text
                obj.City = txtCity.Text
                obj.State = txtState.Text
                obj.ZipCode = txtZipCode.Text
                obj.Country = txtCountry.Text
                obj.Fax = txtFax.Text
                obj.Email = txtEmail.Text

                If obj.IsLocationIDExist Then
                    lblerror.Text = "Location Id already exist please provide other id"
                    lblerror.ForeColor = Drawing.Color.Red
                Else

                    If obj.Insert Then
                        Update()
                        Response.Redirect("OrderLocationPreferences.aspx")
                    End If

                End If

            Catch ex As Exception
                lblerror.Text = ex.Message
            End Try

        End If

    End Sub



    '   Add1 [nvarchar](100) NULL,
    '[Phone] [nvarchar](50) NULL,
    'AllowLocationAddress bit 

    Public Function Update() As Boolean

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE Order_Location set Notes=@Notes,AllowedAllItems=@AllowedAllItems,Add1=@f5,Phone=@f6,AllowLocationAddress=@f7 Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And LocationID=@f4"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@Notes", SqlDbType.NVarChar, 500)).Value = txtNotes.Text
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.LocationID
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 100)).Value = txtadddress.Text
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 50)).Value = txtphone.Text
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.Bit)).Value = chkenable.Checked
            com.Parameters.Add(New SqlParameter("@AllowedAllItems", SqlDbType.Bit)).Value = chkAllowedAllItems.Checked

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function



    Protected Sub BtnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        Response.Redirect("OrderLocationPreferences.aspx")
    End Sub


End Class
