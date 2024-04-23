Option Strict Off

Imports System.Data.SqlClient
Imports System.Data
Imports DAL
Imports EnterpriseCommon.Configuration
Imports EnterpriseASPClient.Core
Imports EnterpriseClient.Core
Imports System.Diagnostics
Imports PayPal.Payments.DataObjects
Imports PayPal.Payments.Common
Imports PayPal.Payments.Common.Utility
Imports PayPal.Payments.Transactions
Imports System.Net.Mail
Imports System.Text.RegularExpressions
Imports System.IO
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.Shared
Imports AuthorizeNet

Imports System.Web.Script.Serialization
Imports System.Web.Script.Services
Imports System.Collections.Generic

Partial Class VendorDetails
    Inherits System.Web.UI.Page



    Public ConnectionString As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim EmployeeID As String = ""
    Dim PurchaseOrderNumber As String = ""




    Public Sub PopulateVendorIDInfo(ByVal VendorID As String)
        Dim dtCST As New DataTable
        If VendorID.Trim <> "" Then
            dtCST = VendorInformation(VendorID)
        End If

        txtVendorTemp.Text = VendorID
        txtVendorTemp.ReadOnly = True

        If dtCST.Rows.Count <> 0 Then

            Dim CustomerTypeID As String = ""
            Try
                chkExportoExcel.Checked = dtCST.Rows(0)("ExportoExcel")
            Catch ex As Exception

            End Try
            Try
                drpVendorTypeID.SelectedValue = dtCST.Rows(0)("VendorTypeID")
                'txtVendorName.Enabled = False
            Catch ex As Exception

            End Try

            Try
                txtVendorName.Text = dtCST.Rows(0)("VendorName")
                'txtVendorName.Enabled = False
            Catch ex As Exception

            End Try
            Try
                txtAttention.Text = dtCST.Rows(0)("Attention").ToString()
                ' txtAttention.Enabled = False
            Catch ex As Exception

            End Try
            Try
                txtVendorAddress1.Text = dtCST.Rows(0)("VendorAddress1").ToString()
                '  txtVendorAddress1.Enabled = False
            Catch ex As Exception

            End Try
            Try
                txtVendorAddress2.Text = dtCST.Rows(0)("VendorAddress2").ToString()
                ' txtVendorAddress2.Enabled = False
            Catch ex As Exception

            End Try

            ' txtVendorAddress3.Text = dtCST.Rows(0)("VendorAddress3").ToString()
            Try
                txtVendorCity.Text = dtCST.Rows(0)("VendorCity").ToString()
                '   txtVendorCity.Enabled = False
            Catch ex As Exception

            End Try
            Try
                txtVendorFax.Text = dtCST.Rows(0)("VendorFax").ToString()
                ' txtVendorFax.Enabled = False
            Catch ex As Exception

            End Try

            Try
                txtVendorPhone.Text = dtCST.Rows(0)("VendorPhone").ToString()
                ' txtVendorPhone.Enabled = False
            Catch ex As Exception

            End Try

            Try
                txtVendorEmail.Text = dtCST.Rows(0)("VendorEmail").ToString()
                txtVendorEmail2.Text = dtCST.Rows(0)("VendorEmail2").ToString()
                txtVendorEmail3.Text = dtCST.Rows(0)("VendorEmail3").ToString()
                ' txtVendorEmail.Enabled = False
            Catch ex As Exception

            End Try

            Try
                txtVendorZip.Text = dtCST.Rows(0)("VendorZip").ToString()
                ' txtVendorZip.Enabled = False
            Catch ex As Exception

            End Try

            Try
                txtVendorCountry.Text = dtCST.Rows(0)("VendorCountry").ToString()
                'txtVendorCountry.Enabled = False
            Catch ex As Exception

            End Try

            Try
                txtVendorState.Text = dtCST.Rows(0)("VendorState").ToString()
                ' txtVendorState.Enabled = False
            Catch ex As Exception

            End Try


        End If

    End Sub


    Public Function VendorInformation(ByVal VendorID As String) As DataTable
        Dim dt As New DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "Select * from   [VendorInformation] Where CompanyID=@f1  AND   DivisionID =@f2  AND  DepartmentID =@f3  AND [VendorID]=@f4"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID.Trim()
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID.Trim()
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID.Trim()
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = VendorID

            com.CommandType = CommandType.Text

            Dim da As New SqlDataAdapter(com)

            da.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return dt
        End Try
        Return dt
    End Function

    Dim VendorID As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

        If Not IsPostBack Then


            BindVendorTyp()

            VendorID = ""
            Try
                VendorID = Request.QueryString("VendorID")
            Catch ex As Exception

            End Try


            If VendorID <> "" Then
                PopulateVendorIDInfo(VendorID)
            Else
                txtVendorTemp.Text = GetNextVendorIDByCount(CompanyID, DepartmentID, DivisionID)

            End If




        End If

    End Sub



    Public Function PopulateVendorTyp(ByVal CompanyID As String, ByVal DivID As String, ByVal DeptID As String) As DataTable
        Dim ConnectionString As String = ""

        ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
        Dim ConString As New SqlConnection
        ConString.ConnectionString = ConnectionString
        Dim sqlStr As String = "SELECT * FROM [VendorTypes] WHERE CompanyID='" & CompanyID & "' AND DepartmentID='" & DeptID & "' AND DivisionID='" & DivID & "'   "
        Dim Cmd As New SqlCommand
        Cmd.CommandText = sqlStr
        Cmd.Connection = ConString
        Dim rs As New DataTable
        ConString.Open()
        Dim da As New SqlDataAdapter

        da.SelectCommand = Cmd
        da.Fill(rs)

        ' lblmessage.Text = sqlStr

        Return rs
    End Function


    Public Sub BindVendorTyp()
        ' Dim objUser As New DAL.CustomOrder()
        Dim dt As New DataTable
        dt = PopulateVendorTyp(CompanyID, DepartmentID, DivisionID)

        drpVendorTypeID.DataTextField = "VendorTypeID"
        drpVendorTypeID.DataValueField = "VendorTypeID"
        drpVendorTypeID.DataSource = dt
        drpVendorTypeID.DataBind()




    End Sub


    Public Function Update_VendorInformation(ByVal VendorID As String, ByVal name As String, ByVal value As String) As Boolean

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        Dim com As SqlCommand

        Dim ds As New DataTable
        ds = VendorInformation(VendorID)
        If ds.Rows.Count > 0 Then
        Else
            qry = "Insert into [VendorInformation] ([CompanyID] ,[DivisionID] ,[DepartmentID] ,[VendorID]) values(@CompanyID,@DivisionID,@DepartmentID,@VendorID)"
            com = New SqlCommand(qry, connec)
            Try

                com.Parameters.AddWithValue("@VendorID", VendorID)
                com.Parameters.AddWithValue("@CompanyID", Me.CompanyID)
                com.Parameters.AddWithValue("@DivisionID", Me.DivisionID)
                com.Parameters.AddWithValue("@DepartmentID", Me.DepartmentID)

                com.Connection.Open()
                com.ExecuteNonQuery()
                com.Connection.Close()
                ' Return True
            Catch ex As Exception
                Dim msg As String
                msg = ex.Message
                'HttpContext.Current.Response.Write(msg)
                'Return False
            End Try

        End If


        qry = "Update VendorInformation SET  " & name & " =@value Where VendorID = @VendorID  AND CompanyID=@CompanyID AND DivisionID=@DivisionID AND DepartmentID=@DepartmentID  "
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.AddWithValue("@value", value)
            com.Parameters.AddWithValue("@VendorID", VendorID)
            com.Parameters.AddWithValue("@CompanyID", Me.CompanyID)
            com.Parameters.AddWithValue("@DivisionID", Me.DivisionID)
            com.Parameters.AddWithValue("@DepartmentID", Me.DepartmentID)

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function



    Protected Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click

        VendorID = ""
        Try
            VendorID = Request.QueryString("VendorID")
        Catch ex As Exception

        End Try


        If VendorID = "" Then

            Dim ReturnValue As Integer = CheckVendorExists(CompanyID, DepartmentID, DivisionID, txtVendorTemp.Text)
            If ReturnValue > 0 Then
                lblmessage.Text = "This Vendor ID already exist please provide other Vendor ID"
                lblmessage.ForeColor = Drawing.Color.Red
            Else
                CreateVendor(CompanyID, DepartmentID, DivisionID)
                UpdateVendortype(CompanyID, DepartmentID, DivisionID)
                Update_VendorInformation(VendorID, "ExportoExcel", chkExportoExcel.Checked)
                Response.Redirect("VendorList.aspx")
            End If
        Else
            UpdateVendor(CompanyID, DepartmentID, DivisionID)
            UpdateVendortype(CompanyID, DepartmentID, DivisionID)
            Update_VendorInformation(VendorID, "ExportoExcel", chkExportoExcel.Checked)
            Response.Redirect("VendorList.aspx")
        End If


    End Sub


    '@CompanyID NVARCHAR(36),
    '@DivisionID NVARCHAR(36),
    '@DepartmentID NVARCHAR(36),
    '@VendorID NVARCHAR(36) ,
    '@VendorName nvarchar(50),
    '@Attention nvarchar(50),
    '@VendorAddress1 nvarchar(50),
    '@VendorAddress2 nvarchar(50),
    '@VendorCity nvarchar(50),
    '@VendorState nvarchar(50),
    '@VendorZip nvarchar(10),
    '@VendorCountry nvarchar(50),
    '@VendorPhone nvarchar(50),
    '@VendorFax nvarchar(50),
    '@VendorEmail nvarchar(60)

    Public Function CreateVendor(ByVal CompID As String, ByVal DeptID As String, ByVal DivID As String) As String
        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand("[dbo].[CreateVendor]", myCon)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
        parameterCompanyID.Value = CompID
        myCommand.Parameters.Add(parameterCompanyID)

        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
        parameterDepartmentID.Value = DeptID
        myCommand.Parameters.Add(parameterDepartmentID)

        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
        parameterDivisionID.Value = DivID
        myCommand.Parameters.Add(parameterDivisionID)

        Dim pVendorID As New SqlParameter("@VendorID", Data.SqlDbType.NVarChar)
        pVendorID.Value = txtVendorTemp.Text
        myCommand.Parameters.Add(pVendorID)

        Dim pVendorName As New SqlParameter("@VendorName", Data.SqlDbType.NVarChar)
        pVendorName.Value = txtVendorName.Text
        myCommand.Parameters.Add(pVendorName)


        Dim pAttention As New SqlParameter("@Attention", Data.SqlDbType.NVarChar)
        pAttention.Value = txtAttention.Text
        myCommand.Parameters.Add(pAttention)


        Dim pVendorAddress1 As New SqlParameter("@VendorAddress1", Data.SqlDbType.NVarChar)
        pVendorAddress1.Value = txtVendorAddress1.Text
        myCommand.Parameters.Add(pVendorAddress1)

        Dim pVendorAddress2 As New SqlParameter("@VendorAddress2", Data.SqlDbType.NVarChar)
        pVendorAddress2.Value = txtVendorAddress2.Text
        myCommand.Parameters.Add(pVendorAddress2)

        Dim pVendorCity As New SqlParameter("@VendorCity", Data.SqlDbType.NVarChar)
        pVendorCity.Value = txtVendorCity.Text
        myCommand.Parameters.Add(pVendorCity)

        Dim pVendorState As New SqlParameter("@VendorState", Data.SqlDbType.NVarChar)
        pVendorState.Value = txtVendorState.Text
        myCommand.Parameters.Add(pVendorState)

        Dim pVendorZip As New SqlParameter("@VendorZip", Data.SqlDbType.NVarChar)
        pVendorZip.Value = txtVendorZip.Text
        myCommand.Parameters.Add(pVendorZip)

        Dim pVendorCountry As New SqlParameter("@VendorCountry", Data.SqlDbType.NVarChar)
        pVendorCountry.Value = txtVendorCountry.Text
        myCommand.Parameters.Add(pVendorCountry)

        Dim pVendorPhone As New SqlParameter("@VendorPhone", Data.SqlDbType.NVarChar)
        pVendorPhone.Value = txtVendorPhone.Text
        myCommand.Parameters.Add(pVendorPhone)


        Dim pVendorFax As New SqlParameter("@VendorFax", Data.SqlDbType.NVarChar)
        pVendorFax.Value = txtVendorFax.Text
        myCommand.Parameters.Add(pVendorFax)

        Dim pVendorEmail As New SqlParameter("@VendorEmail", Data.SqlDbType.NVarChar)
        pVendorEmail.Value = txtVendorEmail.Text
        myCommand.Parameters.Add(pVendorEmail)

        Dim pVendorEmail2 As New SqlParameter("@VendorEmail2", Data.SqlDbType.NVarChar)
        pVendorEmail2.Value = txtVendorEmail2.Text
        myCommand.Parameters.Add(pVendorEmail2)

        Dim pVendorEmail3 As New SqlParameter("@VendorEmail3", Data.SqlDbType.NVarChar)
        pVendorEmail3.Value = txtVendorEmail3.Text
        myCommand.Parameters.Add(pVendorEmail3)


        myCon.Open()

        myCommand.ExecuteNonQuery()

        myCon.Close()

        Return ""

    End Function

    Public Function UpdateVendortype(ByVal CompID As String, ByVal DeptID As String, ByVal DivID As String) As String
        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand("update VendorInformation SET  VendorInformation.VendorTypeID = @VendorTypeID WHERE CompanyID=@CompanyID AND DepartmentID=@DepartmentID AND DivisionID=@DivisionID  and VendorInformation.VendorID = @VendorID ", myCon)
        myCommand.CommandType = Data.CommandType.Text
        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
        parameterCompanyID.Value = CompID
        myCommand.Parameters.Add(parameterCompanyID)

        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
        parameterDepartmentID.Value = DeptID
        myCommand.Parameters.Add(parameterDepartmentID)

        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
        parameterDivisionID.Value = DivID
        myCommand.Parameters.Add(parameterDivisionID)

        Dim pVendorID As New SqlParameter("@VendorID", Data.SqlDbType.NVarChar)
        pVendorID.Value = txtVendorTemp.Text
        myCommand.Parameters.Add(pVendorID)

        Dim pVendorName As New SqlParameter("@VendorTypeID", Data.SqlDbType.NVarChar)
        pVendorName.Value = drpVendorTypeID.SelectedValue
        myCommand.Parameters.Add(pVendorName)
        myCon.Open()

        myCommand.ExecuteNonQuery()

        myCon.Close()

        Return ""
    End Function

    Public Function UpdateVendor(ByVal CompID As String, ByVal DeptID As String, ByVal DivID As String) As String
        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand("[dbo].[UpdateVendor]", myCon)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
        parameterCompanyID.Value = CompID
        myCommand.Parameters.Add(parameterCompanyID)

        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
        parameterDepartmentID.Value = DeptID
        myCommand.Parameters.Add(parameterDepartmentID)

        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
        parameterDivisionID.Value = DivID
        myCommand.Parameters.Add(parameterDivisionID)

        Dim pVendorID As New SqlParameter("@VendorID", Data.SqlDbType.NVarChar)
        pVendorID.Value = txtVendorTemp.Text
        myCommand.Parameters.Add(pVendorID)

        Dim pVendorName As New SqlParameter("@VendorName", Data.SqlDbType.NVarChar)
        pVendorName.Value = txtVendorName.Text
        myCommand.Parameters.Add(pVendorName)


        Dim pAttention As New SqlParameter("@Attention", Data.SqlDbType.NVarChar)
        pAttention.Value = txtAttention.Text
        myCommand.Parameters.Add(pAttention)


        Dim pVendorAddress1 As New SqlParameter("@VendorAddress1", Data.SqlDbType.NVarChar)
        pVendorAddress1.Value = txtVendorAddress1.Text
        myCommand.Parameters.Add(pVendorAddress1)

        Dim pVendorAddress2 As New SqlParameter("@VendorAddress2", Data.SqlDbType.NVarChar)
        pVendorAddress2.Value = txtVendorAddress2.Text
        myCommand.Parameters.Add(pVendorAddress2)

        Dim pVendorCity As New SqlParameter("@VendorCity", Data.SqlDbType.NVarChar)
        pVendorCity.Value = txtVendorCity.Text
        myCommand.Parameters.Add(pVendorCity)

        Dim pVendorState As New SqlParameter("@VendorState", Data.SqlDbType.NVarChar)
        pVendorState.Value = txtVendorState.Text
        myCommand.Parameters.Add(pVendorState)

        Dim pVendorZip As New SqlParameter("@VendorZip", Data.SqlDbType.NVarChar)
        pVendorZip.Value = txtVendorZip.Text
        myCommand.Parameters.Add(pVendorZip)

        Dim pVendorCountry As New SqlParameter("@VendorCountry", Data.SqlDbType.NVarChar)
        pVendorCountry.Value = txtVendorCountry.Text
        myCommand.Parameters.Add(pVendorCountry)

        Dim pVendorPhone As New SqlParameter("@VendorPhone", Data.SqlDbType.NVarChar)
        pVendorPhone.Value = txtVendorPhone.Text
        myCommand.Parameters.Add(pVendorPhone)


        Dim pVendorFax As New SqlParameter("@VendorFax", Data.SqlDbType.NVarChar)
        pVendorFax.Value = txtVendorFax.Text
        myCommand.Parameters.Add(pVendorFax)

        Dim pVendorEmail As New SqlParameter("@VendorEmail", Data.SqlDbType.NVarChar)
        pVendorEmail.Value = txtVendorEmail.Text
        myCommand.Parameters.Add(pVendorEmail)

        Dim pVendorEmail2 As New SqlParameter("@VendorEmail2", Data.SqlDbType.NVarChar)
        pVendorEmail2.Value = txtVendorEmail2.Text
        myCommand.Parameters.Add(pVendorEmail2)

        Dim pVendorEmail3 As New SqlParameter("@VendorEmail3", Data.SqlDbType.NVarChar)
        pVendorEmail3.Value = txtVendorEmail3.Text
        myCommand.Parameters.Add(pVendorEmail3)


        myCon.Open()

        myCommand.ExecuteNonQuery()

        myCon.Close()

        Return ""

    End Function


    Public Function CheckVendorExists(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal VendorID As String) As Integer

        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand("[dbo].[CheckVendorExists]", myCon)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
        parameterCompanyID.Value = CompanyID
        myCommand.Parameters.Add(parameterCompanyID)

        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
        parameterDepartmentID.Value = DepartmentID
        myCommand.Parameters.Add(parameterDepartmentID)

        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
        parameterDivisionID.Value = DivisionID
        myCommand.Parameters.Add(parameterDivisionID)

        Dim parameterCustomerID As New SqlParameter("@VendorID", Data.SqlDbType.NVarChar)
        parameterCustomerID.Value = VendorID
        myCommand.Parameters.Add(parameterCustomerID)

        Dim paramReturnValue As New SqlParameter("@ReturnValue", Data.SqlDbType.Int)
        paramReturnValue.Direction = ParameterDirection.Output
        myCommand.Parameters.Add(paramReturnValue)

        myCon.Open()
        myCommand.ExecuteNonQuery()

        Dim OutPutValue As Integer
        OutPutValue = Convert.ToInt32(paramReturnValue.Value)
        myCon.Close()
        Return OutPutValue

    End Function


    Public Function GetNextVendorIDByCount(ByVal CompID As String, ByVal DeptID As String, ByVal DivID As String) As String
        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand("[dbo].[GetNextVendorIDByCount]", myCon)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
        parameterCompanyID.Value = CompID
        myCommand.Parameters.Add(parameterCompanyID)

        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
        parameterDepartmentID.Value = DeptID
        myCommand.Parameters.Add(parameterDepartmentID)

        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
        parameterDivisionID.Value = DivID
        myCommand.Parameters.Add(parameterDivisionID)

        Dim parameterPostingResult As New SqlParameter("@VendorID", Data.SqlDbType.NVarChar, 36)
        parameterPostingResult.Direction = ParameterDirection.Output
        myCommand.Parameters.Add(parameterPostingResult)
        Dim OutputValue As String = ""
        myCon.Open()

        myCommand.ExecuteNonQuery()

        OutputValue = parameterPostingResult.Value.ToString()

        myCon.Close()
        Return OutputValue

    End Function


    Protected Sub btnback_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnback.Click
        Response.Redirect("VendorList.aspx")
    End Sub
End Class
