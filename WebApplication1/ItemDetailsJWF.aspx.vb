

Imports System.Data
Imports System.Data.SqlClient

Partial Class ItemDetailsJWF
    Inherits System.Web.UI.Page
    Public ItemID As String = ""
    Public NewItemID As String = ""
    Dim obj As New clsItems
    Public CompanyID As String, DivisionID As String, DepartmentID As String

    Private Sub ItemDetailsJWF_Load(sender As Object, e As EventArgs) Handles Me.Load



        Dim filters As EnterpriseCommon.Core.FilterSet

        filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
        CompanyID = filters!CompanyID
        DivisionID = filters!DivisionID
        DepartmentID = filters!DepartmentID
        Try
            ItemID = Request.QueryString("ItemID")
            If ItemID = "" Then
                ItemID = ""
            End If
        Catch ex As Exception
            ItemID = ""
        End Try
        If ItemID = "" Then
            NewItemID = CreateNewItemID()
        End If
        ' Dim CustomerIDNew As String = CreateNewCustomerID()

    End Sub

#Region "GetNextCustomerID"
    Public Function GetNextitemrID(ByVal CompanyID As String, ByVal DeptID As String, ByVal DivID As String) As SqlDataReader
        Dim ConnectionString As String = ""

        ConnectionString = ConfigurationManager.AppSettings("ConnectionString")
        Dim ConString As New SqlConnection
        ConString.ConnectionString = ConnectionString
        Dim sqlStr As String = "SELECT Count(ItemID)as ItemID from InventoryItems where CompanyID='" & CompanyID & "' and 	DivisionID='" & DivID & "' and	DepartmentID='" & DeptID & "' and ItemID LIKE '%FLW%' "

        Dim Cmd As New SqlCommand
        Cmd.CommandText = sqlStr
        Cmd.Connection = ConString
        Dim rs As SqlDataReader
        ConString.Open()
        rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)
        Return rs
    End Function
#End Region


#Region "GetItemID"
    Public Function GetItemID(ByVal CompanyID As String, ByVal DeptID As String, ByVal DivID As String, ByVal ItemID As String) As Integer
        Dim ConnectionString As String = ""
        ConnectionString = ConfigurationManager.AppSettings("ConnectionString")
        Dim ConString As New SqlConnection
        ConString.ConnectionString = ConnectionString
        Dim CustCount As Integer = 0
        Dim sqlStr As String = "SELECT Count(ItemID) from InventoryItems where ItemID='" & ItemID & "' AND	CompanyID='" & CompanyID & "' and 	DivisionID='" & DivID & "' and	DepartmentID='" & DeptID & "'  "
        Dim Cmd As New SqlCommand
        Cmd.CommandText = sqlStr
        Cmd.Connection = ConString
        Dim rs As SqlDataReader
        ConString.Open()
        rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)
        While rs.Read()
            Dim str As String = rs(0).ToString()
            If str = "0" Then
                CustCount = 0
            Else
                CustCount = 1
            End If
        End While

        If rs.IsClosed Then
        Else
            rs.Close()
        End If
        ConString.Close()
        Return CustCount
    End Function
#End Region

#Region "CreateNewCustomerID"
    Protected Function CreateNewItemID() As String

        Dim rs As SqlDataReader
        Dim CustIDNew As String = ""
        Dim CustID As String = "0000"
        Dim CustIDCreated As String = ""
        Dim CustMaxCount As Integer = 0
        Dim Flg As Boolean = True
        Try
            rs = GetNextitemrID(CompanyID, DepartmentID, DivisionID)
        Catch ex As Exception

        End Try



        While rs.Read()
            CustID = rs("ItemID").ToString()
        End While

        Dim CustIDint As Integer = Convert.ToInt32(CustID)
        CustIDint = CustIDint + 1
        Dim CustIDString As String = Convert.ToString(CustIDint)
        CustIDNew = CustIDString.Insert(0, "FLW")

        Try
            CustMaxCount = GetItemID(CompanyID, DepartmentID, DivisionID, CustIDNew)
        Catch ex As Exception

        End Try


        While CustMaxCount = 1
            CustIDint += 1
            CustIDString = Convert.ToString(CustIDint)
            CustIDNew = CustIDString.Insert(0, "FLW")
            CustMaxCount = GetItemID(CompanyID, DepartmentID, DivisionID, CustIDNew)

        End While

        Try
            If rs.IsClosed Then
            Else
                rs.Close()
            End If
        Catch ex As Exception

        End Try
        Return CustIDNew
    End Function
#End Region


End Class
