Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Partial Class Itemiupdate
    Inherits System.Web.UI.Page
    Dim checkglobal As Boolean = True

    Public Function filldatatable() As Data.DataTable

        Dim fn As String = txtgrid.Text
        Dim Path As String = ConfigurationManager.AppSettings("DocPath")
        Dim textdelimiter As String


        If drpdelemitedfiletype.SelectedValue = "CSV" Then

            textdelimiter = ","

        ElseIf drpdelemitedfiletype.SelectedValue = "TAB" Then

            textdelimiter = vbTab

        ElseIf drpdelemitedfiletype.SelectedValue = "Semicolon" Then

            textdelimiter = ";"

        ElseIf drpdelemitedfiletype.SelectedValue = "Pipe" Then

            textdelimiter = "|"

        ElseIf drpdelemitedfiletype.SelectedValue = "Other" Then


        End If




        Dim filetoread As String
        filetoread = Path & fn

        Dim filestream As StreamReader
        filestream = File.OpenText(filetoread)
        Dim sInputLine As String

        Dim readcontents As String
        Dim i As Integer
        Dim objDataTable As New System.Data.DataTable
        objDataTable.Clear()

        sInputLine = filestream.ReadLine()
        readcontents = sInputLine
        Dim splitout() As String
        splitout = Split(readcontents, textdelimiter)

        For i = 0 To UBound(splitout)
            objDataTable.Columns.Add(splitout(i), String.Empty.GetType())
        Next

        sInputLine = filestream.ReadLine()
        Do Until sInputLine Is Nothing

            readcontents = sInputLine
            splitout = Split(readcontents, textdelimiter)

            Dim cr, k As Integer
            cr = splitout.GetLength(0)
            Dim rw As Data.DataRow = objDataTable.NewRow
            For k = 0 To cr - 1
                rw(k) = splitout(k)
            Next

            objDataTable.Rows.Add(rw)

            sInputLine = filestream.ReadLine()
        Loop
        filestream.Close()
        Return objDataTable

    End Function

    Public Function filldatatableexcel() As Data.DataTable



        Dim fn As String = txtgrid.Text
        Dim Path As String = ConfigurationManager.AppSettings("DocPath")
        Dim dat As New Data.DataTable
        Try
            Dim excelConnectionString As String = ("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Path & "\" & fn & ";" & "Extended Properties=Excel 8.0;")
            Dim connection As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(excelConnectionString)
            Dim command As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand("Select * FROM [Sheet1$]", connection)
            Dim da As New Data.OleDb.OleDbDataAdapter(command)
            da.Fill(dat)
        Catch ex As Exception

        End Try
        Return dat


    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
            Dim CompanyID As String = CType(SessionKey("CompanyID"), String)
            Dim EmployeeID As String = CType(SessionKey("EmployeeID"), String)
            Dim DivisionID As String = CType(SessionKey("DivisionID"), String)
            Dim DepartmentID As String = CType(SessionKey("DepartmentID"), String)


            Dim securitycheck As Boolean = False
            If CompanyID.ToUpper = "QuickfloraDemo".ToUpper Then
                securitycheck = True
            End If
            If CompanyID.ToUpper = "FMW" Then
                securitycheck = True
            End If
            If CompanyID.ToUpper = "JWF"  Or CompanyID.ToUpper = "NEWWF"  Then
                securitycheck = True
            End If
            If securitycheck = False Then
                Response.Redirect("SecurityAcessPermission.aspx?MOD=PO")
            End If



        End If
        dvvalidation.InnerHtml = ""



    End Sub

    Protected Sub Upload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Upload.Click

        If Not Me.FileUpload1.PostedFile Is Nothing And FileUpload1.PostedFile.ContentLength > 0 Then
            Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
            Dim CompanyID As String = CType(SessionKey("CompanyID"), String)
            Dim EmployeeID As String = CType(SessionKey("EmployeeID"), String)
            Dim DivisionID As String = CType(SessionKey("DivisionID"), String)
            Dim DepartmentID As String = CType(SessionKey("DepartmentID"), String)

            Panel0.Visible = False
            Panel4.Visible = False
            Panel1.Visible = True


            Dim fn As String = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName)
            Dim str As String
            Dim cmpid As String
            cmpid = CompanyID.Replace(" ", "-")
            str = Date.Now.Day & "-" & Date.Now.Month & "-" & Date.Now.Year & "-" & Date.Now.Hour & "-" & Date.Now.Minute & "-" & Date.Now.Millisecond & "-" & cmpid & "-" & fn
            txtgrid.Text = str


            Dim Path As String = ConfigurationManager.AppSettings("DocPath")




            Dim SaveLocation As String = Path & "\" & str
            Try
                FileUpload1.PostedFile.SaveAs(SaveLocation)
                filldropdown()
                Dim obj As New clsinventoryitem
                obj.ComanyID = CompanyID
                obj.DivisionID = DivisionID
                obj.DepartmentID = DepartmentID
                If drpfiletype.SelectedValue = "Excel" Then
                    obj.delimiter = "Excel"
                Else
                    obj.delimiter = drpdelemitedfiletype.SelectedValue
                End If



                obj.Filename = str
                obj.dateup = Date.Now
                '''''''''''''''''
                Try

                    Dim filename As String
                    filename = hiddenfilename.Text

                    Dim TheFile As FileInfo = New FileInfo(Path & filename)
                    If TheFile.Exists Then
                        File.Delete(Path & filename)
                    Else
                        Throw New FileNotFoundException()
                    End If


                Catch ex As FileNotFoundException
                    'lblmsg.Text = ex.Message
                Catch ex As Exception
                    'lblmsg.Text = ex.Message
                End Try
                '''''''''''''''''''''
                obj.deletefilename()
                obj.Insertfilename()

            Catch ex As Exception
                lblerror.Text = ex.Message
            End Try

        Else
            lblerror.Text = "Please select a file to upload."
        End If


    End Sub

    Private Sub filldropdown()

        Try

            Dim dat As New Data.DataTable
            Try

                If drpfiletype.SelectedValue = "Excel" Then
                    dat = filldatatableexcel()
                Else
                    dat = filldatatable()
                End If

                drpItemID.Items.Clear()
                drpItemName.Items.Clear()
                drpPrice.Items.Clear()
                drpWholesalePrice.Items.Clear()
                drpMTPrice.Items.Clear()
                drpAVGCost.Items.Clear()
                drpPack.Items.Clear()
                Dim n As Integer = dat.Columns.Count
                Dim lst As New ListItem
                lst.Text = "--Select One--"
                lst.Value = ""
                Dim i As Integer
                For i = 0 To n - 1
                    Dim lstdrp As New ListItem
                    lstdrp.Text = dat.Columns(i).Caption
                    lstdrp.Value = i

                    drpItemID.Items.Add(lstdrp)
                    drpItemName.Items.Add(lstdrp)
                    drpPrice.Items.Add(lstdrp)
                    drpWholesalePrice.Items.Add(lstdrp)
                    drpMTPrice.Items.Add(lstdrp)
                    drpAVGCost.Items.Add(lstdrp)
                    drpPack.Items.Add(lstdrp)


                Next

                drpItemID.Items.Insert(0, lst)
                drpItemName.Items.Insert(0, lst)
                drpPrice.Items.Insert(0, lst)
                drpWholesalePrice.Items.Insert(0, lst)
                drpMTPrice.Items.Insert(0, lst)
                drpAVGCost.Items.Insert(0, lst)
                drpPack.Items.Insert(0, lst)


            Catch ex As Exception
                lblerror.Text = "Error: " & ex.Message
            End Try


        Catch Exc As Exception
            lblerror.Text = "Error: " & Exc.Message

        End Try


    End Sub

    Protected Sub btnPreview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPreview.Click

        Panel1.Visible = False
        Panel2.Visible = True
        filldetaisl()

    End Sub

    Private Sub filldetaisl()
        Dim fn As String = txtgrid.Text

        Dim dat As New Data.DataTable

        Try
            If drpfiletype.SelectedValue = "Excel" Then
                dat = filldatatableexcel()
            Else
                dat = filldatatable()
            End If
            '''''''''''''''''''''''''''''''
            'Create a new DataTable object

            Dim objDataTable As New System.Data.DataTable

            'Create three columns with string as their type

            objDataTable.Columns.Add("ItemID", String.Empty.GetType())
            objDataTable.Columns.Add("ItemName", String.Empty.GetType())
            objDataTable.Columns.Add("Price", String.Empty.GetType())
            objDataTable.Columns.Add("wholesalePrice", String.Empty.GetType())
            objDataTable.Columns.Add("MTPrice", String.Empty.GetType())
            objDataTable.Columns.Add("AverageCost", String.Empty.GetType())
            objDataTable.Columns.Add("UnitsPerBox", String.Empty.GetType())

            'Adding some data in the rows of this DataTable
            Dim nr As Integer

            For nr = 0 To dat.Rows.Count - 1
                Dim rw As Data.DataRow = objDataTable.NewRow
                If drpItemID.SelectedValue <> "" Then
                    rw("ItemID") = dat.Rows(nr)(drpItemID.SelectedItem.Text)
                Else
                    rw("ItemID") = ""
                End If
                Try
                    If rw("ItemID") = "" Then
                        Continue For
                    End If
                Catch ex As Exception
                    Continue For
                End Try
                If drpItemName.SelectedValue <> "" Then
                    rw("ItemName") = dat.Rows(nr)(drpItemName.SelectedItem.Text)
                Else
                    rw("ItemName") = ""
                End If
                If drpPrice.SelectedValue <> "" Then
                    rw("Price") = dat.Rows(nr)(drpPrice.SelectedItem.Text)
                Else
                    rw("Price") = ""
                End If
                If drpWholesalePrice.SelectedValue <> "" Then
                    rw("wholesalePrice") = dat.Rows(nr)(drpWholesalePrice.SelectedItem.Text)
                Else
                    rw("wholesalePrice") = ""
                End If
                If drpMTPrice.SelectedValue <> "" Then
                    rw("MTPrice") = dat.Rows(nr)(drpMTPrice.SelectedItem.Text)
                Else
                    rw("MTPrice") = ""
                End If
                If drpAVGCost.SelectedValue <> "" Then
                    rw("AverageCost") = dat.Rows(nr)(drpAVGCost.SelectedItem.Text)
                Else
                    rw("AverageCost") = ""
                End If
                ''''''''second''''''''''
                If drpPack.SelectedValue <> "" Then
                    rw("UnitsPerBox") = dat.Rows(nr)(drpPack.SelectedItem.Text)
                Else
                    rw("UnitsPerBox") = ""
                End If

                objDataTable.Rows.Add(rw)
            Next

            GridView1.PageSize = 1000
            GridView1.DataSource = objDataTable
            GridView1.DataBind()

        Catch ex As Exception
            lblmsg.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnback_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnback.Click
        Panel1.Visible = True
        Panel2.Visible = False
        btnImport.Enabled = True
        btnimport2.Enabled = True
    End Sub

    Protected Sub btnImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnimport2.Click

        dvimports.InnerHtml = ""
        'filldetaisl()
        'If checkglobal = False Then
        '    dvimports.InnerHtml = "<b>Imports are not successfully errors details follows please check Excel then upload again </b> <br><br>"
        '    dvimports.InnerHtml = dvimports.InnerHtml & dvvalidation.InnerHtml
        '    Panel2.Visible = False
        '    Panel1.Visible = False
        '    Panel3.Visible = True
        '    Exit Sub
        'End If

        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        Dim CompanyID As String = CType(SessionKey("CompanyID"), String)
        Dim EmployeeID As String = CType(SessionKey("EmployeeID"), String)
        Dim DivisionID As String = CType(SessionKey("DivisionID"), String)
        Dim DepartmentID As String = CType(SessionKey("DepartmentID"), String)

        Dim obj As New clsinventoryitem
        obj.ComanyID = CompanyID
        obj.DivisionID = DivisionID
        obj.DepartmentID = DepartmentID


        'Dim fn As String = txtgrid.Text
        '' Response.Redirect("Importinventroysecond.aspx?fn=" & fn)
        Dim Path As String = ConfigurationManager.AppSettings("DocPath")
        'Dim excelConnectionString As String = ("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Path & "\" & fn & ";" & "Extended Properties=Excel 8.0;")
        'Dim connection As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(excelConnectionString)
        'Dim command As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand("Select * FROM [Sheet1$]", connection)
        'Dim da As New Data.OleDb.OleDbDataAdapter(command)

        Dim dat As New Data.DataTable

        Try
            If drpfiletype.SelectedValue = "Excel" Then
                dat = filldatatableexcel()
            Else
                dat = filldatatable()
            End If
            '''''''''''''''''''''''''''''''
            'Create a new DataTable object

            Dim objDataTable As New System.Data.DataTable
            'Create three columns with string as their type
            objDataTable.Columns.Add("ItemID", String.Empty.GetType())
            objDataTable.Columns.Add("ItemName", String.Empty.GetType())
            objDataTable.Columns.Add("Price", String.Empty.GetType())
            objDataTable.Columns.Add("wholesalePrice", String.Empty.GetType())
            objDataTable.Columns.Add("MTPrice", String.Empty.GetType())
            objDataTable.Columns.Add("AverageCost", String.Empty.GetType())
            objDataTable.Columns.Add("UnitsPerBox", String.Empty.GetType())


            'Adding some data in the rows of this DataTable
            Dim nr As Integer
            Dim count As Integer = 0

            ' dverrror.InnerHtml = dverrror.InnerHtml & "<b>List Of items Not imports due to ItemID already exists</b><br>"
            dvimports.InnerHtml = dvimports.InnerHtml & "<b>List Of items not updated as itemID not exist</b>"
            dvupdate.InnerHtml = dvupdate.InnerHtml & "<b>List Of items successfully updated</b><br>"

            For nr = 0 To dat.Rows.Count - 1
                Dim rw As Data.DataRow = objDataTable.NewRow
                If drpItemID.SelectedValue <> "" Then
                    rw("ItemID") = dat.Rows(nr)(drpItemID.SelectedItem.Text)
                    Try
                        obj.ItemID = dat.Rows(nr)(drpItemID.SelectedItem.Text)
                    Catch ex As Exception
                        obj.ItemID = ""
                    End Try
                Else
                    rw("ItemID") = ""
                    obj.ItemID = ""
                End If

                'If obj.ItemID = "" Then
                '    Continue For
                'End If


                If drpItemName.SelectedValue <> "" Then
                    rw("ItemName") = dat.Rows(nr)(drpItemName.SelectedItem.Text)
                    Try
                        obj.ItemName = dat.Rows(nr)(drpItemName.SelectedItem.Text)
                        Update_InventoryItems(obj.ItemID, "ItemName", obj.ItemName)
                    Catch ex As Exception
                        obj.ItemName = ""
                    End Try
                Else
                    rw("ItemName") = ""
                End If

                If drpPrice.SelectedValue <> "" Then
                    rw("Price") = dat.Rows(nr)(drpPrice.SelectedItem.Text)
                    Try
                        obj.Price = dat.Rows(nr)(drpPrice.SelectedItem.Text)
                        Update_InventoryItems(obj.ItemID, "Price", obj.Price)
                        'Response.Write(obj.ItemID)
                        'Response.Write(obj.Price)
                    Catch ex As Exception
                        obj.Price = ""
                    End Try
                Else
                    obj.Price = ""
                    rw("Price") = ""
                End If
                If drpWholesalePrice.SelectedValue <> "" Then
                    rw("wholesalePrice") = dat.Rows(nr)(drpWholesalePrice.SelectedItem.Text)
                    Try
                        Update_InventoryItems(obj.ItemID, "wholesalePrice", rw("wholesalePrice"))
                    Catch ex As Exception
                        obj.Price = ""
                    End Try
                Else
                    rw("wholesalePrice") = ""
                End If

                If drpMTPrice.SelectedValue <> "" Then
                    rw("MTPrice") = dat.Rows(nr)(drpMTPrice.SelectedItem.Text)
                    Try
                        Update_InventoryItems(obj.ItemID, "MTPrice", rw("MTPrice"))
                    Catch ex As Exception
                        obj.Price = ""
                    End Try
                Else
                    rw("MTPrice") = ""
                End If
                If drpAVGCost.SelectedValue <> "" Then
                    rw("AverageCost") = dat.Rows(nr)(drpAVGCost.SelectedItem.Text)
                    Try
                        Update_InventoryItems(obj.ItemID, "AverageCost", rw("AverageCost"))
                    Catch ex As Exception
                        obj.Price = ""
                    End Try
                Else
                    rw("AverageCost") = ""
                End If


                ''''''''second''''''''''
                If drpPack.SelectedValue <> "" Then
                    rw("UnitsPerBox") = dat.Rows(nr)(drpPack.SelectedItem.Text)
                    Try
                        Update_InventoryItems(obj.ItemID, "UnitsPerBox", rw("UnitsPerBox"))
                    Catch ex As Exception
                        obj.Price = ""
                    End Try
                Else
                    rw("UnitsPerBox") = ""
                End If


                If IsItemIdExist(obj.ItemID) Then
                    dvupdate.InnerHtml = dvupdate.InnerHtml & "<br>"
                    dvupdate.InnerHtml = dvupdate.InnerHtml & "<b>ItemID</b> = " & obj.ItemID
                    'dvupdate.InnerHtml = dvupdate.InnerHtml & "<br>"
                Else
                    'dvimports.InnerHtml = dvimports.InnerHtml & "<br>Following Items Not updated because items not exist:<br>"
                    dvimports.InnerHtml = dvimports.InnerHtml & "<br>"
                    dvimports.InnerHtml = dvimports.InnerHtml & "<b>ItemID</b> = " & obj.ItemID
                    'dvimports.InnerHtml = dvimports.InnerHtml & "<br>"
                End If

                objDataTable.Rows.Add(rw)
            Next

            GridView2.PageSize = 1000
            GridView2.DataSource = objDataTable
            GridView2.DataBind()

            Try

                Dim filename As String
                filename = txtgrid.Text

                Dim TheFile As FileInfo = New FileInfo(Path & filename)
                If TheFile.Exists Then
                    File.Delete(Path & filename)
                    obj.deletefilename()
                Else
                    Throw New FileNotFoundException()
                End If


            Catch ex As FileNotFoundException
                lblmsg.Text = ex.Message
            Catch ex As Exception
                lblmsg.Text = ex.Message
            End Try


        Catch ex As Exception
            lblmsg.Text = ex.Message
        End Try

        Panel2.Visible = False
        Panel1.Visible = False
        Panel3.Visible = True

    End Sub

    Public Function IsItemIdExist(ByVal ItemID As String) As Boolean
        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        Dim company As String = CType(SessionKey("CompanyID"), String)
        Dim UserName As String = CType(SessionKey("EmployeeID"), String)
        Dim DivID As String = CType(SessionKey("DivisionID"), String)
        Dim DeptID As String = CType(SessionKey("DepartmentID"), String)
        Dim ConnectionString As String = ""
        ConnectionString = EnterpriseCommon.Configuration.ConfigSettings.ConnectionString
        Dim connec As New SqlConnection(ConnectionString)
        Dim qry As String
        qry = "select * from InventoryItems where  CompanyID=@f1 and DivisionID=@f2 and DepartmentID=@f3 and ItemID=@f4"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Dim dt As New DataTable
        Dim da As New SqlDataAdapter

        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = company
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = DivID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = DeptID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = ItemID

            da.SelectCommand = com
            da.Fill(dt)

            If dt.Rows.Count <> 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return False
        End Try
    End Function

    Public Function Update_InventoryItems(ByVal ItemID As String, ByVal name As String, ByVal value As String) As Boolean
        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        Dim company As String = CType(SessionKey("CompanyID"), String)
        Dim UserName As String = CType(SessionKey("EmployeeID"), String)
        Dim DivID As String = CType(SessionKey("DivisionID"), String)
        Dim DeptID As String = CType(SessionKey("DepartmentID"), String)
        Dim ConnectionString As String = ""
        ConnectionString = EnterpriseCommon.Configuration.ConfigSettings.ConnectionString
        'Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(ConnectionString)
        Dim qry As String
        Dim com As SqlCommand
        qry = "Update InventoryItems SET  " & name & " =@value Where ItemID = @ItemID  AND CompanyID=@CompanyID AND DivisionID=@DivisionID AND DepartmentID=@DepartmentID  "
        'Response.Write(qry)
        'Response.Write(name)
        'Response.Write(value)
        'Response.Write(ItemID)
        'Response.Write(company)
        'Response.Write(DivID)
        'Response.Write(DeptID)
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.AddWithValue("@value", value)
            com.Parameters.AddWithValue("@ItemID", ItemID)
            com.Parameters.AddWithValue("@CompanyID", company)
            com.Parameters.AddWithValue("@DivisionID", DivID)
            com.Parameters.AddWithValue("@DepartmentID", DeptID)

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

    Protected Sub btnselect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnselect.Click

    End Sub

    Protected Sub btnreload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnreload.Click
        Response.Redirect("Itemimports.aspx")
    End Sub

    Protected Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        Dim CompanyID As String = CType(SessionKey("CompanyID"), String)
        Dim EmployeeID As String = CType(SessionKey("EmployeeID"), String)
        Dim DivisionID As String = CType(SessionKey("DivisionID"), String)
        Dim DepartmentID As String = CType(SessionKey("DepartmentID"), String)
        Dim obj As New clsinventoryitem
        obj.ComanyID = CompanyID
        obj.DivisionID = DivisionID
        obj.DepartmentID = DepartmentID

        Dim dt As New Data.DataTable
        dt = obj.IsfileExist

        If dt.Rows.Count <> 0 Then


            Try
                If dt.Rows(0)("filename") <> "" Then
                    Dim filename As String
                    Dim Path As String = ConfigurationManager.AppSettings("DocPath")
                    filename = dt.Rows(0)("filename")

                    Dim TheFile As FileInfo = New FileInfo(Path & filename)
                    If TheFile.Exists Then
                        File.Delete(Path & filename)
                        obj.deletefilename()
                        Response.Redirect("Itemimport.aspx")
                    Else

                        Throw New FileNotFoundException()
                    End If

                End If
            Catch ex As FileNotFoundException
                lblfiledelete.Text = ex.Message
            Catch ex As Exception
                lblfiledelete.Text = ex.Message
            End Try
        End If


    End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        filldetaisl()
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    Dim chk As Boolean = True

        '    Dim ItemID As String = e.Row.Cells(0).Text
        '    Dim IsActive As String = e.Row.Cells(1).Text

        '    Dim EnableItemPrice As String = e.Row.Cells(2).Text
        '    Dim EnableAddtoCart As String = e.Row.Cells(3).Text
        '    Dim EnabledFrontEndItem As String = e.Row.Cells(4).Text


        '    Dim ItemTypeID As String = e.Row.Cells(5).Text
        '    Dim ItemName As String = e.Row.Cells(6).Text
        '    Dim ItemDescription As String = e.Row.Cells(7).Text
        '    Dim ItemLongDescription As String = e.Row.Cells(8).Text
        '    Dim ItemCategoryID As String = e.Row.Cells(9).Text
        '    Dim ItemFamilyID As String = e.Row.Cells(10).Text

        '    Dim ItemCategoryID2 As String = e.Row.Cells(11).Text
        '    Dim ItemFamilyID2 As String = e.Row.Cells(12).Text
        '    Dim ItemFamilyID2IsActive As String = e.Row.Cells(13).Text

        '    Dim ItemCategoryID3 As String = e.Row.Cells(14).Text
        '    Dim ItemFamilyID3 As String = e.Row.Cells(15).Text
        '    Dim ItemFamilyID3IsActive As String = e.Row.Cells(16).Text

        '    Dim PictureURL As String = e.Row.Cells(17).Text
        '    Dim LargePictureURL As String = e.Row.Cells(18).Text
        '    Dim MediumPictureURL As String = e.Row.Cells(19).Text

        '    Dim Price As String = e.Row.Cells(20).Text
        '    Dim PremiumPrice As String = e.Row.Cells(21).Text
        '    Dim DeluxePrice As String = e.Row.Cells(22).Text


        '    Dim WireServiceProducts As String = e.Row.Cells(23).Text
        '    Dim WireServiceID As String = e.Row.Cells(24).Text


        '    Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        '    Dim CompanyID As String = CType(SessionKey("CompanyID"), String)
        '    Dim EmployeeID As String = CType(SessionKey("EmployeeID"), String)
        '    Dim DivisionID As String = CType(SessionKey("DivisionID"), String)
        '    Dim DepartmentID As String = CType(SessionKey("DepartmentID"), String)
        '    Dim obj As New clsinventoryitem
        '    obj.ComanyID = CompanyID
        '    obj.DivisionID = DivisionID
        '    obj.DepartmentID = DepartmentID

        '    obj.ItemFamilyID = ItemFamilyID
        '    Dim dt As New Data.DataTable
        '    dt = obj.FillDetailsfamily


        '    If drpPrice.SelectedValue <> "" Then
        '        If Not IsNumeric(Price) Then
        '            chk = False
        '            dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '            dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<b>Item ID</b> " & ItemID & " Price value should be numeric"
        '            dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '        End If
        '    End If

        '    If drpWholesalePrice.SelectedValue <> "" Then
        '        If Not IsNumeric(PremiumPrice) Then
        '            chk = False
        '            dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '            dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<b>Item ID</b> " & ItemID & " Premium Price value should be numeric"
        '            dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '        End If
        '    End If

        '    If drpDeluxePrice.SelectedValue <> "" Then
        '        If Not IsNumeric(DeluxePrice) Then
        '            chk = False
        '            dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '            dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<b>Item ID</b> " & ItemID & " Deluxe Price value should be numeric"
        '            dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '        End If
        '    End If

        '    If drpIsActive.SelectedValue <> "" Then
        '        If IsNumeric(IsActive) Then
        '            If IsActive = 0 Or IsActive = 1 Then
        '            Else
        '                chk = False
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<b>Item ID</b> " & ItemID & " IsActive value should be 1 or 0 if numeric"
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '            End If
        '        Else
        '            If IsActive.ToLower = "true" Or IsActive.ToLower = "false" Then
        '            Else

        '                chk = False
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<b>Item ID</b> " & ItemID & " IsActive value should be true or false"
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '            End If
        '        End If

        '    End If

        '    If drpIsActiveIInd.SelectedValue <> "" Then
        '        If IsNumeric(ItemFamilyID2IsActive) Then
        '            If ItemFamilyID2IsActive = 0 Or ItemFamilyID2IsActive = 1 Then
        '            Else
        '                chk = False
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<b>Item ID</b> " & ItemID & " Item FamilyID2 IsActive value should be 1 or 0 if numeric"
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '            End If
        '        Else
        '            If ItemFamilyID2IsActive.ToLower = "true" Or ItemFamilyID2IsActive.ToLower = "false" Then
        '            Else

        '                chk = False
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<b>Item ID</b> " & ItemID & " Item FamilyID2 IsActive value should be true or false"
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '            End If
        '        End If

        '    End If

        '    If drpIsActiveIIIrd.SelectedValue <> "" Then
        '        If IsNumeric(ItemFamilyID3IsActive) Then
        '            If ItemFamilyID3IsActive = 0 Or ItemFamilyID3IsActive = 1 Then
        '            Else
        '                chk = False
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<b>Item ID</b> " & ItemID & " Item FamilyID3 IsActive value should be 1 or 0 if numeric"
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '            End If
        '        Else
        '            If ItemFamilyID3IsActive.ToLower = "true" Or ItemFamilyID3IsActive.ToLower = "false" Then
        '            Else

        '                chk = False
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<b>Item ID</b> " & ItemID & " Item FamilyID3 IsActive value should be true or false"
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '            End If
        '        End If

        '    End If

        '    If drpEnableAddtoCart.SelectedValue <> "" Then
        '        If IsNumeric(EnableAddtoCart) Then
        '            If EnableAddtoCart = 0 Or EnableAddtoCart = 1 Then
        '            Else
        '                chk = False
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<b>Item ID</b> " & ItemID & " Enable Addto Cart value should be 1 or 0 if numeric"
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '            End If
        '        Else
        '            If EnableAddtoCart.ToLower = "true" Or EnableAddtoCart.ToLower = "false" Then
        '            Else

        '                chk = False
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<b>Item ID</b> " & ItemID & " Enable Addto Cart value should be true or false"
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '            End If
        '        End If

        '    End If

        '    If drpEnabledFrontEndItem.SelectedValue <> "" Then
        '        If IsNumeric(EnabledFrontEndItem) Then
        '            If EnabledFrontEndItem = 0 Or EnabledFrontEndItem = 1 Then
        '            Else
        '                chk = False
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<b>Item ID</b> " & ItemID & " Enabled FrontEnd Item value should be 1 or 0 if numeric"
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '            End If
        '        Else
        '            If EnabledFrontEndItem.ToLower = "true" Or EnabledFrontEndItem.ToLower = "false" Then
        '            Else

        '                chk = False
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<b>Item ID</b> " & ItemID & " Enabled FrontEnd Item value should be true or false"
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '            End If
        '        End If

        '    End If

        '    If drpEnableItemPrice.SelectedValue <> "" Then
        '        If IsNumeric(EnableItemPrice) Then
        '            If EnableItemPrice = 0 Or EnableItemPrice = 1 Then
        '            Else
        '                chk = False
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<b>Item ID</b> " & ItemID & " Enable Item Price value should be 1 or 0 if numeric"
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '            End If
        '        Else
        '            If EnableItemPrice.ToLower = "true" Or EnableItemPrice.ToLower = "false" Then
        '            Else

        '                chk = False
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<b>Item ID</b> " & ItemID & " Enable Item Price value should be true or false"
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '            End If
        '        End If

        '    End If

        '    If drpWireServiceProducts.SelectedValue <> "" Then
        '        If IsNumeric(WireServiceProducts) Then
        '            If WireServiceProducts = 0 Or WireServiceProducts = 1 Then
        '            Else

        '                chk = False
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<b>Item ID</b> " & ItemID & " WireService Products value should be 1 or 0 if numeric"
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '            End If
        '        Else
        '            If WireServiceProducts.ToLower = "true" Or WireServiceProducts.ToLower = "false" Then
        '            Else

        '                chk = False
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<b>Item ID</b> " & ItemID & " WireService Products value should be true or false"
        '                dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '            End If
        '        End If
        '    End If


        '    If ItemName.Length > 100 Then

        '        chk = False
        '        dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '        dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<b>Item ID</b> " & ItemID & " Item Name Length should less than or equal to 100"
        '        dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"

        '    End If

        '    If ItemDescription.Length > 4000 Then
        '        chk = False
        '        dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<b>Item ID</b> " & ItemID & " Item Description Length should less than or equal to 4000"

        '    End If

        '    If ItemLongDescription.Length > 4000 Then
        '        chk = False
        '        dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '        dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<b>Item ID</b> " & ItemID & " Item Long Description Length should less than or equal to 4000"
        '        dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '    End If


        '    If PictureURL.Length > 80 Then
        '        chk = False
        '        dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '        dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<b>Item ID</b> " & ItemID & " Picture URL Length should less than or equal to 80"
        '        dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"

        '    End If

        '    If LargePictureURL.Length > 80 Then
        '        chk = False
        '        dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '        dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<b>Item ID</b> " & ItemID & " Large Picture URL Length should less than or equal to 80"
        '        dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"

        '    End If

        '    If MediumPictureURL.Length > 80 Then
        '        chk = False
        '        dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"
        '        dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<b>Item ID</b> " & ItemID & " Medium Picture URL Length should less than or equal to 80"
        '        dvvalidation.InnerHtml = dvvalidation.InnerHtml & "<br>"

        '    End If

        '    If chk = False Then
        '        btnImport.Enabled = False
        '        btnimport2.Enabled = False
        '        e.Row.BackColor = Drawing.Color.Red
        '        checkglobal = False
        '    End If


        'End If

    End Sub

    Protected Sub drpfiletype_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpfiletype.SelectedIndexChanged

        If drpfiletype.SelectedValue = "Delimited" Then

            drpdelemitedfiletype.Enabled = True
        Else
            drpdelemitedfiletype.Enabled = False

        End If

    End Sub

    Protected Sub btnImport_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImport.Click
        btnPreview_Click(sender, e)
    End Sub

End Class
