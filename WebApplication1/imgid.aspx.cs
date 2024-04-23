using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Windows.Forms;

public partial class imgid : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string itemname = "";

        string CompanyID = "";
        string DivisionID = "DEFAULT";
        string DepartmentID = "DEFAULT";

        CompanyID = Request.QueryString["CompanyID"].ToString();
        itemname = Request.QueryString["ItemID"].ToString();
        string imgname = "";
        imgname = UpdateHomeImage(CompanyID, DivisionID, DepartmentID, itemname);
        Response.ContentType = "image/png";

        System.Net.WebClient wc = new System.Net.WebClient();

        // byte[] data = wc.DownloadData("https://secure.quickflora.com/itemimages/" + imgname );



        try
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile("D:\\WebApps\\QuickFloraFrontEnd\\itemimages\\" + imgname);

            byte[] data = imgToByteArray(img);

            Response.OutputStream.Write(data, 0, data.Length);
            Response.OutputStream.Flush();
            Response.End();
        }
        catch
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile("D:\\WebApps\\QuickFloraFrontEnd\\itemimages\\noimagenew.png");

            byte[] data = imgToByteArray(img);

            Response.OutputStream.Write(data, 0, data.Length);
            Response.OutputStream.Flush();
            Response.End();
        }


    }
    public byte[] imgToByteArray(System.Drawing.Image img)
    {
        using (MemoryStream mStream = new MemoryStream())
        {
            img.Save(mStream, img.RawFormat);
            return mStream.ToArray();
        }
    }
    private string connectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
    public string UpdateHomeImage(string companyID, string divisionID, string departmentID, string ItemID)
    {
        string imgname = "";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand("Select ISNULL(PictureURL ,'noimagenew.png') AS ThumbnailImage FROM [Enterprise].[dbo].[InventoryItems]    Where ItemID=@ItemID AND  [CompanyID] =@CompanyID AND [DivisionID] = @DivisionID AND [DepartmentID] =@DepartmentID  ", connection))
            {
                command.CommandType = CommandType.Text;

                #region parameters

                command.Parameters.AddWithValue("CompanyID", companyID);
                command.Parameters.AddWithValue("DivisionID", divisionID);
                command.Parameters.AddWithValue("DepartmentID", departmentID);
                command.Parameters.AddWithValue("ItemID", ItemID);

                #endregion
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    imgname = dt.Rows[0][0].ToString();
                }


            }
        }


        return imgname;
    }


}