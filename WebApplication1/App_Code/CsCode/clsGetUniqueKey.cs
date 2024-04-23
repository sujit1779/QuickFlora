using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections;
using System.Text;
using System.Security.Cryptography;

/// <summary>
/// Summary description for clsGetUniqueKey
/// </summary>
public class clsGetUniqueKey
{
	public clsGetUniqueKey()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string GetUniqueKey()
          {
              int maxSize = 30;
              int minSize = 10;
              char[] chars = new char[62];
              string a;
              a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
              chars = a.ToCharArray();
              int size = maxSize;
              byte[] data = new byte[1];
              RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
              crypto.GetNonZeroBytes(data);
              size = maxSize;
              data = new byte[size];
              crypto.GetNonZeroBytes(data);
              StringBuilder result = new StringBuilder(size);
              foreach (byte b in data)
              { result.Append(chars[b % (chars.Length - 1)]); }
              return result.ToString();
          }


}
