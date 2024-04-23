Imports System.Data.SqlClient
Imports System.Data
Imports DAL
Imports EnterpriseCommon.Configuration


Partial Class EmployeeDetail
    Inherits System.Web.UI.Page

    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim EmployeeID As String = ""
    Dim IsAdmin As Boolean = False
    Dim objEmployee As New clsEmployee

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("CompanyID") Is Nothing Then
            Response.Redirect("loginform.aspx")
        End If

        ' Response.Redirect("Home.aspx")

        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        ' get the connection ready
        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

        If Not Page.IsPostBack Then
            HandleControls()
            FillEmployeeTypeList()
            FillEmployeeDepartmentList()
            FillLocation()
            FillCountry()
            FillState()
            dvEmployeeUserName.Visible = False
            If Not Request.QueryString("EmployeeID") = Nothing Then
                GetEmployeeInformation()
            End If
            GetEmployeeModuleAccessList()
        End If

    End Sub

    Private Sub HandleControls()

        If Request.QueryString("EmployeeID") = Nothing Then
            btnClear.Visible = True
            btnSubmit.Visible = True
            btnUpdate.Visible = False
            btnCancel.Visible = False
        Else
            btnClear.Visible = False
            btnSubmit.Visible = False
            btnUpdate.Visible = True
            btnCancel.Visible = True
            txtEmployeeID.Enabled = False
        End If

    End Sub

    Private Sub FillEmployeeTypeList()

        Dim dt As New DataTable
        dt = objEmployee.GetEmployeeTypeList(CompanyID, DivisionID, DepartmentID)

        If dt.Rows.Count > 0 Then

            drpEmployeeType.DataTextField = "EmployeeTypeDescription"
            drpEmployeeType.DataValueField = "EmployeeTypeID"
            drpEmployeeType.DataSource = dt
            drpEmployeeType.DataBind()
            drpEmployeeDepartment.Items.Insert(0, "")

        End If

    End Sub

    Private Sub FillEmployeeDepartmentList()

        Dim dt As New DataTable
        dt = objEmployee.GetEmployeeDepartmentList(CompanyID, DivisionID, DepartmentID)

        If dt.Rows.Count > 0 Then

            drpEmployeeDepartment.DataTextField = "EmployeeDepartmentDescription"
            drpEmployeeDepartment.DataValueField = "EmployeeDepartmentID"
            drpEmployeeDepartment.DataSource = dt
            drpEmployeeDepartment.DataBind()
            drpEmployeeDepartment.Items.Insert(0, "")

        End If

    End Sub

    Private Sub FillLocation()

        Dim dt As New DataTable
        dt = objEmployee.GetLocation(CompanyID, DivisionID, DepartmentID)

        If dt.Rows.Count > 0 Then

            drpLocation.DataTextField = "LocationName"
            drpLocation.DataValueField = "LocationID"
            drpLocation.DataSource = dt
            drpLocation.DataBind()
            drpLocation.Items.Insert(0, "--Select--")

        End If

    End Sub

    Private Sub FillCountry()

        Dim rs As SqlDataReader
        Dim PopOrderType As New CustomOrder()
        rs = PopOrderType.PopulateCountries(CompanyID, DepartmentID, DivisionID)

        drpEmployeeCountry.DataTextField = "CountryDescription"
        drpEmployeeCountry.DataValueField = "CountryID"

        drpEmployeeCountry.DataSource = rs
        drpEmployeeCountry.DataBind()

        rs.Close()

        drpEmployeeCountry.SelectedValue = "US"

    End Sub

    Private Sub FillState()

        Dim rs As SqlDataReader
        Dim PopOrderType As New CustomOrder()
        rs = PopOrderType.PopulateStates(CompanyID, DepartmentID, DivisionID)

        drpEmployeeState.DataTextField = "StateID"
        drpEmployeeState.DataValueField = "StateID"
        drpEmployeeState.DataSource = rs
        drpEmployeeState.DataBind()

        rs.Close()

    End Sub

    Private Sub GetEmployeeInformation()

        Dim dt As New DataTable
        dt = objEmployee.GetEmployeeDetail(CompanyID, DivisionID, DepartmentID, Request.QueryString("EmployeeID"))

        If dt.Rows.Count > 0 Then
            Dim row As DataRow = dt.Rows(0)
            dvEmployeeUserName.Visible = True
            txtEmployeeUserName.Enabled = False

            txtEmployeeID.Text = row("EmployeeID")
            txtEmployeeName.Text = row("EmployeeName")
            txtEmployeeUserName.Text = row("EmployeeUserName")
            'txtEmployeePassword.Text = row("EmployeePassword")
            Try
                Dim Decr_EmployeePassword As String
                ' Decr_EmployeePassword = CryptographyRijndael.EncryptionRijndael.RijndaelDecode(txtEmployeePassword.Text, txtEmployeeID.Text.ToLower() & CompanyID.ToLower())
                ' txtEmployeePassword.Attributes.Add("Value", Decr_EmployeePassword)
            Catch ex As Exception

            End Try

            drpEmployeeType.SelectedValue = row("EmployeeTypeID")
            drpEmployeeDepartment.SelectedValue = row("EmployeeDepartmentID")
            'drpLocation.SelectedValue = row("LocationID")
            Try
                drpLocation.SelectedIndex = drpLocation.Items.IndexOf(drpLocation.Items.FindByValue(row("LocationID")))
            Catch ex As Exception

            End Try


            txtEmployeeHireDate.Text = row("HireDate")

            txtEmployeeSSN.Text = row("EmployeeSSNumber")
            txtEmployeeEmail.Text = row("EmployeeEmailAddress")
            txtEmployeeBirthday.Text = row("Birthday")
            txtEmployeeAddress1.Text = row("EmployeeAddress1")

            txtEmployeeAddress2.Text = row("EmployeeAddress2")
            txtEmployeeCity.Text = row("EmployeeCity")
            drpEmployeeState.SelectedValue = row("EmployeeState")
            txtEmployeeZip.Text = row("EmployeeZip")

            drpEmployeeCountry.SelectedValue = row("EmployeeCountry")
            txtEmployeePhone.Text = row("EmployeePhone")
            txtEmployeeFax.Text = row("EmployeeFax")
            chkIsAdmin.Checked = row("IsAdmin")

            chkIsMasterEmployee.Checked = row("IsMasterEmployee")
            chkActive.Checked = row("ActiveYN")
            chkCommissionable.Checked = row("Commissionable")
            txtCommissionPercent.Text = row("CommissionPerc")

            txtCommissionCalMethod.Text = row("CommissionCalcMethod")
            txtNotes.Text = row("Notes")

            'GetEmployeeModuleAccessList()

        End If

    End Sub

    Private Sub GetEmployeeModuleAccessList()

        Dim dt As New DataTable
        dt = objEmployee.GetEmployeesModuleAccess(CompanyID, DivisionID, DepartmentID, txtEmployeeID.Text)

        If dt.Rows.Count > 0 Then
            AccessModuleGrid.DataSource = dt
            AccessModuleGrid.DataBind()
        End If

    End Sub

    Private Sub UpdateModuleAccessList()

        For Each row As GridViewRow In AccessModuleGrid.Rows

            Dim ModuleID As String = (AccessModuleGrid.DataKeys(row.RowIndex).Value)
            Dim chk As CheckBox = row.FindControl("chkAccess")

            'If chk.Checked Then
            objEmployee.AddUpdateEmployeeModuleAccess(CompanyID, DivisionID, DepartmentID, txtEmployeeID.Text.Trim, ModuleID, chk.Checked)
            'End If

        Next

    End Sub

    Public Function returl(ByVal ob As String) As String

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

    Private Function UploadImage(ByVal fuImage As FileUpload, ByVal SizeType As String, ByVal Size As Integer) As String

        Return Nothing
        'Dim BackofficeImagePath As String = ConfigurationManager.AppSettings("EmployeeImage")

        'Dim imgPhoto As Drawing.Bitmap
        'Dim imgPhotosave As Drawing.Image
        'Dim keygen As New clsGetUniqueKey
        'Dim sm_fh As Double
        'Dim rt As Double
        'Dim obj As New Tutorial.ImageResize
        'Dim sg As String = ""

        'If fuImage.HasFile Then

        '    '1
        '    fuImage.SaveAs(BackofficeImagePath & fuImage.FileName)
        '    imgPhoto = New Drawing.Bitmap(BackofficeImagePath & fuImage.FileName)
        '    imgPhoto.SetResolution(72, 72)

        '    rt = imgPhoto.Width / imgPhoto.Height
        '    sm_fh = Size / rt
        '    imgPhotosave = obj.FixedSize(imgPhoto, Size, sm_fh)
        '    sg = keygen.GetUniqueKey & "_" + SizeType + ".jpg"
        '    imgPhotosave.Save(BackofficeImagePath + sg, Drawing.Imaging.ImageFormat.Jpeg)
        '    imgPhoto.Dispose()
        '    imgPhotosave.Dispose()

        '    Return sg
        'Else

        '    Return Nothing

        'End If

    End Function

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click

        Response.Redirect("EmployeeList.aspx")

    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click

        Dim r As New Random
        If Not System.Text.RegularExpressions.Regex.IsMatch(txtEmployeeID.Text, "^[A-Za-z0-9]+$") Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loadfn" & r.Next(100001, 200001).ToString(), "alert('Special Chars not allowed employee id');", True)
            Exit Sub
        End If


        If IsNumeric(txtCommissionPercent.Text) Then
        Else
            txtCommissionPercent.Text = "0.0"

        End If

        'If Not System.Text.RegularExpressions.Regex.IsMatch(txtCommissionPercent.Text, "^[0-9.]+$") Then
        '    ClientScript.RegisterStartupScript(Me.GetType(), "loadfn" & r.Next(100001, 200001).ToString(), "alert('Commission percent should be in decimal');", True)
        '    Exit Sub
        'End If

        txtEmployeeUserName.Text = txtEmployeeID.Text

        Dim ImageURL As String = "" 'UploadImage(fuImage, "sm", 300)

        If objEmployee.CheckIsEmployeeExists(CompanyID, DivisionID, DepartmentID, txtEmployeeID.Text) Then
            lblStatus.Text = "Please try again this Employee ID already exist."
            lblStatus.Visible = True
        Else
            Dim Decr_EmployeePassword As String = ""
            If txtEmployeePassword.Text.Trim() <> "" Then
                Decr_EmployeePassword = CryptographyRijndael.EncryptionRijndael.RijndaelEncode(txtEmployeePassword.Text, txtEmployeeID.Text.ToLower() & CompanyID.ToLower())
            End If


            If objEmployee.InsertEmployeeDetail(CompanyID, DivisionID, DepartmentID, txtEmployeeID.Text.Trim, drpEmployeeType.SelectedValue, txtEmployeeUserName.Text.Trim, _
                                    Decr_EmployeePassword, txtEmployeeName.Text.Trim, chkActive.Checked, txtEmployeeAddress1.Text.Trim, _
                                    txtEmployeeAddress2.Text.Trim, txtEmployeeCity.Text.Trim, drpEmployeeState.SelectedValue, txtEmployeeZip.Text.Trim, _
                                    drpEmployeeCountry.SelectedValue, txtEmployeePhone.Text.Trim, txtEmployeeFax.Text.Trim, txtEmployeeSSN.Text.Trim, _
                                    txtEmployeeEmail.Text.Trim, drpEmployeeDepartment.SelectedValue, ImageURL, txtEmployeeHireDate.Text.Trim, _
                                    txtEmployeeBirthday.Text.Trim, chkCommissionable.Checked, txtCommissionPercent.Text.Trim, txtCommissionCalMethod.Text.Trim, _
                                    txtNotes.Text.Trim, drpLocation.SelectedValue, chkIsAdmin.Checked, chkIsMasterEmployee.Checked) Then
                UpdateModuleAccessList()
                Response.Redirect("EmployeeList.aspx")
            End If
        End If

    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdate.Click

        Dim ImageURL As String = "" 'UploadImage(fuImage, "sm", 300)
        Dim Decr_EmployeePassword As String = ""
        If txtEmployeePassword.Text.Trim() <> "" Then
            Decr_EmployeePassword = CryptographyRijndael.EncryptionRijndael.RijndaelEncode(txtEmployeePassword.Text, txtEmployeeID.Text.ToLower() & CompanyID.ToLower())
        End If

        If objEmployee.UpdateEmployeeDetail(CompanyID, DivisionID, DepartmentID, txtEmployeeID.Text.Trim, drpEmployeeType.SelectedValue, txtEmployeeUserName.Text.Trim, _
                                    Decr_EmployeePassword, txtEmployeeName.Text.Trim, chkActive.Checked, txtEmployeeAddress1.Text.Trim, _
                                    txtEmployeeAddress2.Text.Trim, txtEmployeeCity.Text.Trim, drpEmployeeState.SelectedValue, txtEmployeeZip.Text.Trim, _
                                    drpEmployeeCountry.SelectedValue, txtEmployeePhone.Text.Trim, txtEmployeeFax.Text.Trim, txtEmployeeSSN.Text.Trim, _
                                    txtEmployeeEmail.Text.Trim, drpEmployeeDepartment.SelectedValue, ImageURL, txtEmployeeHireDate.Text.Trim, _
                                    txtEmployeeBirthday.Text.Trim, chkCommissionable.Checked, txtCommissionPercent.Text.Trim, txtCommissionCalMethod.Text.Trim, _
                                    txtNotes.Text.Trim, drpLocation.SelectedValue, chkIsAdmin.Checked, chkIsMasterEmployee.Checked) Then
            UpdateModuleAccessList()
            Response.Redirect("EmployeeList.aspx")
        End If

    End Sub

#Region "Class Functions"

    'Public Function GetEmployeeTypeList(CompanyID As String, DivisionID As String, DepartmentID As String) As DataTable

    '    Dim dt As New DataTable

    '    Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
    '        Using command As New SqlCommand("[enterprise].[GetEmployeeTypeList]", connection)
    '            command.CommandType = CommandType.StoredProcedure

    '            command.Parameters.AddWithValue("CompanyID", CompanyID)
    '            command.Parameters.AddWithValue("DivisionID", DivisionID)
    '            command.Parameters.AddWithValue("DepartmentID", DepartmentID)

    '            Try
    '                Dim da As New SqlDataAdapter(command)
    '                da.Fill(dt)
    '            Catch ex As Exception

    '            End Try

    '        End Using
    '    End Using

    '    Return dt

    'End Function

    'Public Function GetEmployeeDepartmentList(CompanyID As String, DivisionID As String, DepartmentID As String) As DataTable

    '    Dim dt As New DataTable

    '    Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
    '        Using command As New SqlCommand("[enterprise].[GetEmployeeDepartmentList]", connection)
    '            command.CommandType = CommandType.StoredProcedure

    '            command.Parameters.AddWithValue("CompanyID", CompanyID)
    '            command.Parameters.AddWithValue("DivisionID", DivisionID)
    '            command.Parameters.AddWithValue("DepartmentID", DepartmentID)

    '            Try
    '                Dim da As New SqlDataAdapter(command)
    '                da.Fill(dt)
    '            Catch ex As Exception

    '            End Try

    '        End Using
    '    End Using

    '    Return dt

    'End Function

    'Public Function GetLocation(CompanyID As String, DivisionID As String, DepartmentID As String) As DataTable

    '    Dim dt As New DataTable

    '    Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
    '        Using command As New SqlCommand("[enterprise].[FillLocation]", connection)
    '            command.CommandType = CommandType.StoredProcedure

    '            command.Parameters.AddWithValue("CompanyID", CompanyID)
    '            command.Parameters.AddWithValue("DivisionID", DivisionID)
    '            command.Parameters.AddWithValue("DepartmentID", DepartmentID)

    '            Try
    '                Dim da As New SqlDataAdapter(command)
    '                da.Fill(dt)
    '            Catch ex As Exception

    '            End Try

    '        End Using
    '    End Using

    '    Return dt

    'End Function

    'Public Function GetEmployeeDetail(CompanyID As String, DivisionID As String, DepartmentID As String, EmployeeID As String) As DataTable

    '    Dim dt As New DataTable

    '    Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
    '        Using command As New SqlCommand("[enterprise].[GetEmployeeDetail]", connection)
    '            command.CommandType = CommandType.StoredProcedure

    '            command.Parameters.AddWithValue("CompanyID", CompanyID)
    '            command.Parameters.AddWithValue("DivisionID", DivisionID)
    '            command.Parameters.AddWithValue("DepartmentID", DepartmentID)
    '            command.Parameters.AddWithValue("EmployeeID", EmployeeID)

    '            Try
    '                Dim da As New SqlDataAdapter(command)
    '                da.Fill(dt)
    '            Catch ex As Exception

    '            End Try

    '        End Using
    '    End Using

    '    Return dt

    'End Function

    'Public Function CheckIsEmployeeExists(CompanyID As String, DivisionID As String, DepartmentID As String, EmployeeID As String) As Boolean

    '    Dim dt As New DataTable

    '    Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
    '        Using command As New SqlCommand("[enterprise].[CheckIsEmployeeExists]", connection)
    '            command.CommandType = CommandType.StoredProcedure

    '            command.Parameters.AddWithValue("CompanyID", CompanyID)
    '            command.Parameters.AddWithValue("DivisionID", DivisionID)
    '            command.Parameters.AddWithValue("DepartmentID", DepartmentID)
    '            command.Parameters.AddWithValue("EmployeeID", EmployeeID)

    '            Try
    '                command.Connection.Open()
    '                Dim count As Integer = Convert.ToInt32(command.ExecuteScalar)

    '                If count = 0 Then
    '                    Return False
    '                End If

    '                Return True

    '            Catch ex As Exception
    '                Return True
    '            Finally
    '                command.Connection.Close()
    '            End Try

    '        End Using
    '    End Using

    'End Function

    'Public Function InsertEmployeeDetail(CompanyID As String, DivisionID As String, DepartmentID As String, EmployeeID As String, EmployeeTypeID As String, _
    '                                     EmployeeUserName As String, EmployeePassword As String, EmployeeName As String, _
    '                                    ActiveYN As Boolean, EmployeeAddress1 As String, EmployeeAddress2 As String, EmployeeCity As String, _
    '                                    EmployeeState As String, EmployeeZip As String, EmployeeCountry As String, EmployeePhone As String, _
    '                                    EmployeeFax As String, EmployeeSSNumber As String, EmployeeEmailAddress As String, EmployeeDepartmentID As String, _
    '                                    PictureURL As String, HireDate As String, Birthday As String, _
    '                                    Commissionable As Boolean, CommissionPerc As String, CommissionCalcMethod As String, Notes As String, _
    '                                    LocationID As String, IsAdmin As String, IsMasterEmployee As String) As Boolean

    '    Dim dt As New DataTable

    '    Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
    '        Using command As New SqlCommand("[enterprise].[InsertEmployeeDetail]", connection)
    '            command.CommandType = CommandType.StoredProcedure

    '            command.Parameters.AddWithValue("CompanyID", CompanyID)
    '            command.Parameters.AddWithValue("DivisionID", DivisionID)
    '            command.Parameters.AddWithValue("DepartmentID", DepartmentID)
    '            command.Parameters.AddWithValue("EmployeeID", EmployeeID)

    '            command.Parameters.AddWithValue("EmployeeTypeID", EmployeeTypeID)
    '            command.Parameters.AddWithValue("EmployeeUserName", EmployeeUserName)

    '            command.Parameters.AddWithValue("EmployeePassword", EmployeePassword)
    '            command.Parameters.AddWithValue("EmployeeName", EmployeeName)
    '            command.Parameters.AddWithValue("ActiveYN", ActiveYN)
    '            command.Parameters.AddWithValue("EmployeeAddress1", EmployeeAddress1)

    '            command.Parameters.AddWithValue("EmployeeAddress2", EmployeeAddress2)
    '            command.Parameters.AddWithValue("EmployeeCity", EmployeeCity)
    '            command.Parameters.AddWithValue("EmployeeState", EmployeeState)
    '            command.Parameters.AddWithValue("EmployeeZip", EmployeeZip)

    '            command.Parameters.AddWithValue("EmployeeCountry", EmployeeCountry)
    '            command.Parameters.AddWithValue("EmployeePhone", EmployeePhone)
    '            command.Parameters.AddWithValue("EmployeeFax", EmployeeFax)
    '            command.Parameters.AddWithValue("EmployeeSSNumber", EmployeeSSNumber)

    '            command.Parameters.AddWithValue("EmployeeEmailAddress", EmployeeEmailAddress)
    '            command.Parameters.AddWithValue("EmployeeDepartmentID", EmployeeDepartmentID)
    '            command.Parameters.AddWithValue("PictureURL", PictureURL)
    '            command.Parameters.AddWithValue("HireDate", HireDate)

    '            command.Parameters.AddWithValue("Birthday", Birthday)
    '            command.Parameters.AddWithValue("Commissionable", Commissionable)
    '            command.Parameters.AddWithValue("CommissionPerc", CommissionPerc)
    '            command.Parameters.AddWithValue("CommissionCalcMethod", CommissionCalcMethod)

    '            command.Parameters.AddWithValue("Notes", Notes)
    '            command.Parameters.AddWithValue("LocationID", LocationID)
    '            command.Parameters.AddWithValue("IsAdmin", IsAdmin)
    '            command.Parameters.AddWithValue("IsMasterEmployee", IsMasterEmployee)

    '            Try
    '                command.Connection.Open()
    '                command.ExecuteNonQuery()
    '                Return True
    '            Catch ex As Exception
    '                Return False
    '            Finally
    '                command.Connection.Close()
    '            End Try

    '        End Using
    '    End Using

    'End Function

    'Public Function UpdateEmployeeDetail(CompanyID As String, DivisionID As String, DepartmentID As String, EmployeeID As String, EmployeeTypeID As String, _
    '                                     EmployeeUserName As String, EmployeePassword As String, EmployeeName As String, _
    '                                    ActiveYN As Boolean, EmployeeAddress1 As String, EmployeeAddress2 As String, EmployeeCity As String, _
    '                                    EmployeeState As String, EmployeeZip As String, EmployeeCountry As String, EmployeePhone As String, _
    '                                    EmployeeFax As String, EmployeeSSNumber As String, EmployeeEmailAddress As String, EmployeeDepartmentID As String, _
    '                                    PictureURL As String, HireDate As String, Birthday As String, _
    '                                    Commissionable As Boolean, CommissionPerc As String, CommissionCalcMethod As String, Notes As String, _
    '                                    LocationID As String, IsAdmin As String, IsMasterEmployee As String) As Boolean

    '    Dim dt As New DataTable

    '    Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
    '        Using command As New SqlCommand("[enterprise].[UpdateEmployeeDetail]", connection)
    '            command.CommandType = CommandType.StoredProcedure

    '            command.Parameters.AddWithValue("CompanyID", CompanyID)
    '            command.Parameters.AddWithValue("DivisionID", DivisionID)
    '            command.Parameters.AddWithValue("DepartmentID", DepartmentID)
    '            command.Parameters.AddWithValue("EmployeeID", EmployeeID)

    '            command.Parameters.AddWithValue("EmployeeTypeID", EmployeeTypeID)
    '            command.Parameters.AddWithValue("EmployeeUserName", EmployeeUserName)

    '            command.Parameters.AddWithValue("EmployeePassword", EmployeePassword)
    '            command.Parameters.AddWithValue("EmployeeName", EmployeeName)
    '            command.Parameters.AddWithValue("ActiveYN", ActiveYN)
    '            command.Parameters.AddWithValue("EmployeeAddress1", EmployeeAddress1)

    '            command.Parameters.AddWithValue("EmployeeAddress2", EmployeeAddress2)
    '            command.Parameters.AddWithValue("EmployeeCity", EmployeeCity)
    '            command.Parameters.AddWithValue("EmployeeState", EmployeeState)
    '            command.Parameters.AddWithValue("EmployeeZip", EmployeeZip)

    '            command.Parameters.AddWithValue("EmployeeCountry", EmployeeCountry)
    '            command.Parameters.AddWithValue("EmployeePhone", EmployeePhone)
    '            command.Parameters.AddWithValue("EmployeeFax", EmployeeFax)
    '            command.Parameters.AddWithValue("EmployeeSSNumber", EmployeeSSNumber)

    '            command.Parameters.AddWithValue("EmployeeEmailAddress", EmployeeEmailAddress)
    '            command.Parameters.AddWithValue("EmployeeDepartmentID", EmployeeDepartmentID)
    '            command.Parameters.AddWithValue("PictureURL", PictureURL)
    '            command.Parameters.AddWithValue("HireDate", HireDate)

    '            command.Parameters.AddWithValue("Birthday", Birthday)
    '            command.Parameters.AddWithValue("Commissionable", Commissionable)
    '            command.Parameters.AddWithValue("CommissionPerc", CommissionPerc)
    '            command.Parameters.AddWithValue("CommissionCalcMethod", CommissionCalcMethod)

    '            command.Parameters.AddWithValue("Notes", Notes)
    '            command.Parameters.AddWithValue("LocationID", LocationID)
    '            command.Parameters.AddWithValue("IsAdmin", IsAdmin)
    '            command.Parameters.AddWithValue("IsMasterEmployee", IsMasterEmployee)

    '            Try
    '                command.Connection.Open()
    '                command.ExecuteNonQuery()
    '                Return True
    '            Catch ex As Exception
    '                Return False
    '            Finally
    '                command.Connection.Close()
    '            End Try

    '        End Using
    '    End Using

    'End Function

    'Public Function GetEmployeesModuleAccess(CompanyID As String, DivisionID As String, DepartmentID As String, EmployeeID As String) As DataTable

    '    Dim dt As New DataTable

    '    Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
    '        Using command As New SqlCommand("[enterprise].[GetEmployeesModuleAccess]", connection)
    '            command.CommandType = CommandType.StoredProcedure

    '            command.Parameters.AddWithValue("CompanyID", CompanyID)
    '            command.Parameters.AddWithValue("DivisionID", DivisionID)
    '            command.Parameters.AddWithValue("DepartmentID", DepartmentID)
    '            command.Parameters.AddWithValue("EmployeeID", EmployeeID)

    '            Try
    '                Dim da As New SqlDataAdapter(command)
    '                da.Fill(dt)
    '            Catch ex As Exception

    '            End Try

    '        End Using
    '    End Using

    '    Return dt

    'End Function

    'Public Function AddUpdateEmployeeModuleAccess(CompanyID As String, DivisionID As String, DepartmentID As String, EmployeeID As String, _
    '                                              ModuleID As String, IsAccess As Boolean) As Boolean

    '    Dim dt As New DataTable

    '    Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
    '        Using command As New SqlCommand("[enterprise].[AddUpdateEmployeeModuleAccess]", connection)
    '            command.CommandType = CommandType.StoredProcedure

    '            command.Parameters.AddWithValue("CompanyID", CompanyID)
    '            command.Parameters.AddWithValue("DivisionID", DivisionID)
    '            command.Parameters.AddWithValue("DepartmentID", DepartmentID)
    '            command.Parameters.AddWithValue("EmployeeID", EmployeeID)

    '            command.Parameters.AddWithValue("ModuleID", ModuleID)
    '            command.Parameters.AddWithValue("IsAccess", IsAccess)

    '            Try
    '                command.Connection.Open()
    '                command.ExecuteNonQuery()
    '                Return True
    '            Catch ex As Exception
    '                Return False
    '            Finally
    '                command.Connection.Close()
    '            End Try

    '        End Using
    '    End Using

    'End Function

#End Region

End Class
