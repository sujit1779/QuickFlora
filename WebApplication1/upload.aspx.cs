using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class upload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string type = "";
        string itemid = "";
        string uniqfilename = "";
        uniqfilename = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
        type = Request.QueryString["type"].ToString() ;
        itemid = Request.QueryString["itemid"].ToString();

        foreach (string file in Request.Files)
        {
            HttpPostedFile  fileContent = Request.Files[file];
            if (fileContent != null && fileContent.ContentLength > 0)
            {
                // get a stream
                Stream stream = fileContent.InputStream;
                // and optionally write the file to disk
                string  fileName = Path.GetFileName(file);
                fileName = fileContent.FileName;
                uniqfilename = uniqfilename + fileName;
                string  path = Path.Combine("D:\\WebApps\\QuickFloraFrontEnd\\itemimages\\", uniqfilename);
                using (FileStream  fileStream = File.Create(path))
                {
                    CopyTo(stream,fileStream);
                     
                }
                string CompanyID = Session["CompanyID"].ToString();
                string DivisionID = "DEFAULT";
                string DepartmentID = "DEFAULT";

                if(type== "sm")
                    UpdateHomeImage(CompanyID, DivisionID, DepartmentID, uniqfilename, itemid, "PictureURL");

                if (type == "md")
                    UpdateHomeImage(CompanyID, DivisionID, DepartmentID, uniqfilename, itemid, "MediumPictureURL");

                if (type == "lg")
                    UpdateHomeImage(CompanyID, DivisionID, DepartmentID, uniqfilename, itemid, "LargePictureURL");

                if (type == "thm")
                    UpdateHomeImage(CompanyID, DivisionID, DepartmentID, uniqfilename, itemid, "ThumbnailImage");
            }
        }

        Response.Clear();
        Response.Write(uniqfilename);
        Response.End();  

    }

    public  void CopyTo(Stream input, Stream outputStream)
    {
        byte[] buffer = new byte[16 * 1024]; // Fairly arbitrary size
        int bytesRead;
        while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
        {
            outputStream.Write(buffer, 0, bytesRead);
        }
    }

    private string connectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();


    public bool UpdateHomeImage(string companyID, string divisionID, string departmentID, string homeimg, string itemid, string type)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand("Update [Enterprise].[dbo].[InventoryItems]  SET " + type + " =@homeimg  Where ItemID=@ItemID AND  [CompanyID] =@CompanyID AND [DivisionID] = @DivisionID AND [DepartmentID] =@DepartmentID  ", connection))
            {
                command.CommandType = CommandType.Text;

                #region parameters

                command.Parameters.AddWithValue("CompanyID", companyID);
                command.Parameters.AddWithValue("DivisionID", divisionID);
                command.Parameters.AddWithValue("DepartmentID", departmentID);
                command.Parameters.AddWithValue("homeimg", homeimg);
                command.Parameters.AddWithValue("ItemID", itemid);

                #endregion

                command.Connection.Open();
                command.ExecuteNonQuery();
                 
                try
                {

                    return true;
                }
                catch
                {
                    
                    return false;
                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }
    }


}