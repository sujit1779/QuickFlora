<%@ Page Language="VB" EnableViewState="false"   %>

<%@Import Namespace="System.Data"%>
<%@Import Namespace="System.Data.SqlClient"%>
<%@Import Namespace="System.Configuration"%> 


 
<script runat="server">

    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim EmployeeID As String = ""
    Dim CustomerID As String = ""
    Public locationid As String = ""
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
     
       
        Dim JSN As String = ""
        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

        If Not Request.QueryString("k") = Nothing Then

            Dim keyword As String = ""
            keyword = Request.QueryString("k")



            If keyword <> "" Then

                Dim sql As String = "select top 50 [VendorID],[VendorName] from [VendorInformation] where VendorID <> 'DEFAULT' AND CompanyID=@CompanyID AND DivisionID=@DivisionID AND DepartmentID=@DepartmentID AND (  [VendorName] like '%" + keyword.Trim().Replace("'", "''") + "%' Or [VendorID] like '%" + keyword.Trim().Replace("'", "''") + "%'  )"

                Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Dim myCommand As New SqlCommand(sql, myCon)
                myCommand.Parameters.AddWithValue("@CompanyID", CompanyID)
                myCommand.Parameters.AddWithValue("@DivisionID", DivisionID)
                myCommand.Parameters.AddWithValue("@DepartmentID", DepartmentID)

                Dim da As New SqlDataAdapter(myCommand)
                Dim dt As New DataTable

                'populate the data control and bind the data source

                da.Fill(dt)



                If dt.Rows.Count <> 0 Then
                    Dim n As Integer = 0
                    Dim gridrow As Boolean = True

                    JSN = JSN & "["

                    For Each row As DataRow In dt.Rows

                        Dim VendorID As String = row("VendorID").ToString()
                        Dim VendorName As String = row("VendorName").ToString()
                        If n <> 0 Then
                            JSN = JSN & ","
                        Else
                            n = 1
                        End If
                        JSN = JSN & "{"

                        JSN = JSN & """VendorID"":" & """" & VendorID & ""","
                        JSN = JSN & """VendorName"":" & """" & VendorName.Replace(vbCrLf, " ") & """"

                        JSN = JSN & "}"


                    Next

                    JSN = JSN & "]"
                End If

                '' Response.Write(sql)
                
            End If
        End If



        Response.Clear()
        Response.ContentType = "application/json; charset=utf-8"
        Response.Write(JSN)
        Response.End()

        
    End Sub
    
     
</script>
 
  
<html  >
<head id="Head1" runat="server">
    
</head>
<body>
    <form id="form1" runat="server">
    
    </form>
</body>
</html>