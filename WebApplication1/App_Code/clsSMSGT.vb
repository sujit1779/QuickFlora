Imports Microsoft.VisualBasic

Imports System.Web
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Collections


Public Class clsSMSGT

    Public Shared SendResponse As String = ""
    Public Shared SendResponseErr As String = ""

    Public Shared Sub SendSMS(ByVal msg As String, ByVal MobleNumber As String)

        Dim request As HttpWebRequest
        Dim response As HttpWebResponse = Nothing
        Dim reader As StreamReader
        Dim address As Uri

        Dim username As String
        Dim password As String
        Dim message As String
        Dim msisdn As String

        Dim data As New StringBuilder
        Dim byteData() As Byte
        Dim postStream As Stream = Nothing

        ' If your firewall blocks access to port 5567, you can fall back to port 80:
        ' address = New Uri("http://usa.bulksms.com/eapi/submission/send_sms/2/2.0?")
        ' (See FAQ for more details.)
        address = New Uri("http://usa.bulksms.com:5567/eapi/submission/send_sms/2/2.0")

        ' Create the web request  
        request = DirectCast(WebRequest.Create(address), HttpWebRequest)

        ' Set type to POST  
        request.Method = "POST"
        request.ContentType = "application/x-www-form-urlencoded"

        ' Create the data we want to send
        username = "imtiyaz"
        password = "123456789"
        message = msg '"Test message: Testing SMS from Imtiyaz for QF"
        msisdn = MobleNumber '"44123123123"

        data.Append("username=" + HttpUtility.UrlEncode(username, System.Text.Encoding.GetEncoding("ISO-8859-1")))
        data.Append("&password=" + HttpUtility.UrlEncode(password, System.Text.Encoding.GetEncoding("ISO-8859-1")))
        data.Append("&message=" + HttpUtility.UrlEncode(character_map(message), System.Text.Encoding.GetEncoding("ISO-8859-1")))
        data.Append("&msisdn=" + HttpUtility.UrlEncode(msisdn, System.Text.Encoding.GetEncoding("ISO-8859-1")))

        ' Create a byte array of the data we want to send
        byteData = System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(data.ToString())
        'byteData = UTF8Encoding.UTF8.GetBytes(data.ToString())  

        ' Set the content length in the request headers  
        request.ContentLength = byteData.Length


        ' Write data  
        Try
            postStream = request.GetRequestStream()
            postStream.Write(byteData, 0, byteData.Length)
        Catch ex As Exception
            'Console.WriteLine(ex.ToString())
            SendResponseErr = ex.Message
        Finally
            If Not postStream Is Nothing Then postStream.Close()
        End Try

        Try
            ' Get response
            response = DirectCast(request.GetResponse(), HttpWebResponse)

            ' Get the response stream into a reader
            reader = New StreamReader(response.GetResponseStream())

            ' Console application output
            ' Console.WriteLine(reader.ReadToEnd())

            Dim result As String = reader.ReadToEnd()
            Dim tokens() As String
            tokens = result.Split("|")

            If tokens.Length() <> 3 Then
                'Console.WriteLine("Error: could not parse valid return data from server")
                SendResponseErr = "Error: could not parse valid return data from server"
            Else
                If String.Compare(tokens(0).ToString, "0") = 0 Then
                    'Console.WriteLine("Message sent: batch " & tokens(2).ToString())
                    SendResponse = "Message sent: batch " & tokens(2).ToString()
                Else
                    'Console.WriteLine("Error sending message: " & tokens(0) & " " & tokens(1))
                    SendResponseErr = "Error sending message: " & tokens(0) & " " & tokens(1)
                End If
            End If
        Catch ex As Exception
            'Console.WriteLine(ex.ToString())
            SendResponseErr = ex.ToString()
        Finally
            If Not response Is Nothing Then response.Close()
        End Try
    End Sub

    Public Shared Function character_map(ByVal msg As String) As String
        Dim chrs As Hashtable = New Hashtable
        ' Greek characters are mapped onto extended ASCII characters which are unused in the GSM character set
        chrs.Add("Ω", "Û")
        chrs.Add("Θ", "Ô")
        chrs.Add("Δ", "Ð")
        chrs.Add("Φ", "Þ")
        chrs.Add("Γ", "¬")
        chrs.Add("Λ", "Â")
        chrs.Add("Π", "º")
        chrs.Add("Ψ", "Ý")
        chrs.Add("Σ", "Ê")
        chrs.Add("Ξ", "±")

        Dim ret_str As String = ""
        Dim key As String
        Dim chrArray() As Char
        Dim nCnt As Integer
        chrArray = msg.ToCharArray

        For nCnt = 0 To chrArray.Length - 1
            key = chrArray(nCnt)
            If chrs.ContainsKey(key) Then
                ret_str = ret_str + chrs.Item(key)
            Else
                ret_str = ret_str + chrArray(nCnt)
            End If
        Next
        character_map = ret_str
    End Function

End Class
