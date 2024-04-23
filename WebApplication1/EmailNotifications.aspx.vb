Option Strict Off
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Collections.Generic
Imports EnterpriseASPClient.Core
Imports EnterpriseClient.Core
Partial Class EnterpriseASPSystem_CustomCompanySetup_EmailNotifications
    Inherits System.Web.UI.Page

    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim EmployeeID As String = ""

    Dim WebSitePackage As String = ""
    Dim EmailType As String = ""
    Dim EmailSubject As String = ""
    Dim EmailContent As String = ""

    Dim EmailAllowed As Boolean = True


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)

        CompanyID = SessionKey("CompanyID")
        DivisionID = SessionKey("DivisionID")
        DepartmentID = SessionKey("DepartmentID")
        EmployeeID = SessionKey("EmployeeID")
        If Not Page.IsPostBack Then

            EmailType = drpEmailTypes.SelectedValue.Trim()
            '  PopulateWebSitePackages()
            PopulateEmailContent(EmailType)

        End If
    End Sub


    Public Sub PopulateEmailContent(ByVal EmailType As String)



        If EmailType <> "" Then


            Dim objUser As New DAL.CustomOrder()

            Dim rs As SqlDataReader

            rs = objUser.PopulateEmailContentByEmailType(CompanyID, DepartmentID, DivisionID, EmailType)

            If rs.HasRows = True Then
                While rs.Read()

                    '      drpEmailTypes.SelectedIndex = drpEmailTypes.Items.IndexOf(drpEmailTypes.Items.FindByValue("EmailType"))
                    '   drpWebSitePackages.SelectedIndex = drpWebSitePackages.Items.IndexOf(drpWebSitePackages.Items.FindByValue("WebSitePackageID"))
                    txtEmailSubject.Text = rs("EmailSubject").ToString()
                    FCKeditor1.Value = rs("EmailContent").ToString()

                    Try
                        chkEmailActive.Checked = rs("EmailAllowed")
                    Catch ex As Exception
                        chkEmailActive.Checked = False
                    End Try


                End While
            Else
               
                txtEmailSubject.Text = ""
                FCKeditor1.Value = ""
            End If



        End If


    End Sub

    'Public Sub PopulateWebSitePackages()


    '    Dim objUser As New DAL.CustomOrder()

    '    Dim rs As SqlDataReader

    '    rs = objUser.PopulateWebSitePackages(CompanyID, DivisionID, DepartmentID, EmployeeID)

    '    drpWebSitePackages.DataTextField = "WebSitePlan"
    '    drpWebSitePackages.DataValueField = "WebSitePackageID"
    '    drpWebSitePackages.DataSource = rs
    '    drpWebSitePackages.DataBind()



    'End Sub

    Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click

        EmailContent = FCKeditor1.Value
        EmailSubject = txtEmailSubject.Text
        EmailType = drpEmailTypes.SelectedValue
        EmailAllowed = chkEmailActive.Checked

        AddEditEmailContent(CompanyID, DivisionID, DepartmentID, EmailContent, EmailSubject, EmailType, EmailAllowed)


    End Sub

    Public Function AddEditEmailContent(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal EmailContent As String, ByVal EmailSubject As String, ByVal EmailType As String, ByVal EmailAllowed As Boolean) As Integer

        Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        ConString.Open()

        Dim myCommand As New SqlCommand("AddEditEmailContent", ConString)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
        parameterCompanyID.Value = CompanyID
        myCommand.Parameters.Add(parameterCompanyID)

        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
        parameterDepartmentID.Value = DepartmentID
        myCommand.Parameters.Add(parameterDepartmentID)

        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
        parameterDivisionID.Value = DivisionID
        myCommand.Parameters.Add(parameterDivisionID)

        'Dim parameterWebSitePackage As New SqlParameter("@WebSitePackage", Data.SqlDbType.NVarChar)
        'parameterWebSitePackage.Value = WebSitePackage
        'myCommand.Parameters.Add(parameterWebSitePackage)

        Dim parameterEmailContent As New SqlParameter("@EmailContent", Data.SqlDbType.NText)
        parameterEmailContent.Value = EmailContent
        myCommand.Parameters.Add(parameterEmailContent)

        Dim parameterEmailSubject As New SqlParameter("@EmailSubject", Data.SqlDbType.NVarChar)
        parameterEmailSubject.Value = EmailSubject
        myCommand.Parameters.Add(parameterEmailSubject)


        Dim parameterEmailType As New SqlParameter("@EmailType", Data.SqlDbType.NVarChar)
        parameterEmailType.Value = EmailType
        myCommand.Parameters.Add(parameterEmailType)



        Dim parameterEmailAllowed As New SqlParameter("@EmailAllowed", Data.SqlDbType.Bit)
        parameterEmailAllowed.Value = EmailAllowed
        myCommand.Parameters.Add(parameterEmailAllowed)



        Dim paramReturnValue As New SqlParameter("@ReturnValue", Data.SqlDbType.Int)
        paramReturnValue.Direction = ParameterDirection.Output
        myCommand.Parameters.Add(paramReturnValue)






        myCommand.ExecuteNonQuery()



        Dim res As Integer



        ConString.Close()
        If paramReturnValue.Value.ToString() <> "" Then
            res = Convert.ToDecimal(paramReturnValue.Value)
        End If


        Return res



    End Function


    Protected Sub drpEmailTypes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpEmailTypes.SelectedIndexChanged


        EmailType = drpEmailTypes.SelectedValue.Trim()
        '  PopulateWebSitePackages()
        PopulateEmailContent(EmailType)


    End Sub


End Class
