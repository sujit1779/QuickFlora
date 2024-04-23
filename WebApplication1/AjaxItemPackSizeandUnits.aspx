<%@ Page Language="VB" %>

<%@Import Namespace="System.Data"%>
<%@Import Namespace="System.Data.SqlClient"%>
<%@Import Namespace="System.Configuration"%> 

<script runat="server">

    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim ItemID As String = ""
    Dim OrderQty As Integer = 1
    Dim ItemUOM As String = ""
     
 

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
       
        Dim finalsend As String = ""
        
        If Not Request.QueryString("ItemID") = Nothing Then

          
            ItemID = Request.QueryString("ItemID")
            OrderQty = Request.QueryString("OrderQty")
            ItemUOM = Request.QueryString("ItemUOM")
            
           

            If ItemID <> "" Then

                Dim sql As String = "[enterprise].[GetItemPackSizeAndUnits]"
                Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Dim myCommand As New SqlCommand(sql, myCon)
                  myCommand.CommandType = CommandType.StoredProcedure
                myCommand.Parameters.AddWithValue("@CompanyID", CompanyID)
                myCommand.Parameters.AddWithValue("@DivisionID", DivisionID)
                myCommand.Parameters.AddWithValue("@DepartmentID", DepartmentID)
                
               myCommand.Parameters.AddWithValue("@ItemID", ItemID)
                myCommand.Parameters.AddWithValue("@ItemUOM", ItemUOM)
                myCommand.Parameters.AddWithValue("@ItemOrderedQty", OrderQty)
                
                Dim da As New SqlDataAdapter(myCommand)
                Dim dt As New DataTable
                da.Fill(dt)
               
               
                
                If dt.Rows.Count <> 0 Then
                    Try
                        finalsend = finalsend & dt.Rows(0)("ItemID").ToString()
                        finalsend = finalsend & "~!"
                        finalsend = finalsend & dt.Rows(0)("PackSize").ToString()
                        finalsend = finalsend & "~!"
                        finalsend = finalsend & dt.Rows(0)("Units").ToString()
                    Catch ex As Exception

                    End Try
                End If
                 

               

            End If

        End If

        
        Response.Clear()
        Response.Write(finalsend)
        Response.End()
        
    End Sub

     

 

    
</script>
 
  <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    
</head>
<body>
    <form id="form1" runat="server">
   
    </form>
</body>
</html>