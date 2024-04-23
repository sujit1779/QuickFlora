Imports Microsoft.VisualBasic

Public Class clsCCreturn


    Public Function CCreturnprocess(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal OrderNumber As String, ByVal AmttotalRefund As String, ByRef CCResponse As String) As String

        Dim objcheckCardChanged As New clsCreditCardChargeRefund
        objcheckCardChanged.CompanyID = CompanyID
        objcheckCardChanged.DivisionID = DivisionID
        objcheckCardChanged.DepartmentID = DepartmentID
        objcheckCardChanged.OrderNumber = OrderNumber

        objcheckCardChanged.AmttotalCharge = 0 ' 
        objcheckCardChanged.AmttotalRefund = AmttotalRefund
        objcheckCardChanged.REMOTE_ADDR = HttpContext.Current.Request.ServerVariables("REMOTE_ADDR")
        objcheckCardChanged.processfrom = "Credit Card Change Return Process From Back Office."

        Dim ApprovalNumber As String
        ' ApprovalNumber = objcheckCardChanged.RefundCreditCardCHarge()
        If CompanyID = "Greene and Greene" Then
            Dim obj_refund As New SecondTimeBookPaymentCheck2nd
            obj_refund.CompanyID = CompanyID
            obj_refund.DivisionID = DivisionID
            obj_refund.DepartmentID = DepartmentID
            ApprovalNumber = obj_refund.ProcessRefund(OrderNumber, AmttotalRefund)
            objcheckCardChanged.lblerrormessag.Text = obj_refund.lblCCMessage.Text
        Else
            ApprovalNumber = objcheckCardChanged.RefundCreditCardCHarge()
        End If

        Dim obj_mail As New clsErrorMailHandling
        obj_mail.OrderNumber = OrderNumber
        obj_mail.ErrorMailHandling("Refund process second step ", OrderNumber, objcheckCardChanged.lblerrormessag.Text)

        If ApprovalNumber.Trim = "Offline" Then
            CCResponse = objcheckCardChanged.lblerrormessag.Text
            Return ""
        Else
            Return ApprovalNumber
        End If

        Return ""
    End Function



End Class
