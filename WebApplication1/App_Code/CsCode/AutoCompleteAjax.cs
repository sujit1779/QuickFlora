using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;


/// <summary>
/// Summary description for AutoCompleteAjax
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService()]
public class AutoCompleteAjax : System.Web.Services.WebService {

    public AutoCompleteAjax () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        //if (count == 0)
        //{
        //    count = 10;
        //}

        if (prefixText.Equals("xyz"))
        {
            return new string[0];
        }

        int myint = 12;

        string mycontx = contextKey;
        //Random random = new Random();
        string CompanyID =contextKey.Remove(contextKey.IndexOf("!"));
        string DivisionID = contextKey.Remove(0, contextKey.IndexOf("!") + 1);
        DivisionID =  DivisionID.Remove(DivisionID.IndexOf("*"));
        string DepartmentID =  contextKey.Remove(0, contextKey.IndexOf("*") + 1);
        DepartmentID =DepartmentID.Remove(DepartmentID.IndexOf("~"));
        contextKey = contextKey.Remove(0, contextKey.IndexOf("~") + 1);
        string SqlStr = "";



        SqlStr = "SELECT distinct " + contextKey + "  FROM  InventoryItems WHERE " + contextKey + "  LIKE '" + prefixText + "%' AND CompanyID='" + CompanyID + "' and Divisionid='" + DivisionID + "' and DepartmentID='" + DepartmentID + "' and IsActive=1 and WireserviceIdAllowed = 1";

        List<string> items = new List<string>(myint);
        try
        {

            //for (int i = 0; i < count; i++)
            //{
            //    char c1 = (char)random.Next(65, 90);
            //    char c2 = (char)random.Next(97, 122);
            //    char c3 = (char)random.Next(97, 122);

            //    items.Add(prefixText + c1 + c2 + c3);
            //}


            SqlConnection ConString = new SqlConnection();
            ConString.ConnectionString = ConfigurationManager.ConnectionStrings["EnterpriseConnectionString"].ToString();
            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = ConString;
            //string SqlStr = "SELECT distinct CustomerFirstName  FROM  CustomerInformation WHERE CustomerFirstName LIKE '" + prefixText + "%' AND CompanyID='Greene and Greene' and Divisionid='DEFAULT' and DepartmentID='DEFAULT'";
            ConString.Open();
            Cmd.CommandText = SqlStr;
            SqlDataReader dr;

            dr = Cmd.ExecuteReader();


            while (dr.Read())
            {
                items.Add(dr[contextKey].ToString());
                

            }
            dr.Close();
            ConString.Close();
        }
        catch (Exception ex)
        {


        }
        return items.ToArray();
    }

    //Web  Service Used for Populating Customer Information

    [WebMethod]
    public string[] GetCompletionListCustomers(string prefixText, int count, string contextKey)
    {
        

        if (prefixText.Equals("xyz"))
        {
            return new string[0];
        }

        int myint = 12;

        string mycontx = contextKey;
        string CompanyID = contextKey.Remove(contextKey.IndexOf("!"));
        string DivisionID =contextKey.Remove(0, contextKey.IndexOf("!") + 1);
        DivisionID = DivisionID.Remove(DivisionID.IndexOf("*"));
        string DepartmentID =  contextKey.Remove(0, contextKey.IndexOf("*") + 1);
        DepartmentID = DepartmentID.Remove(DepartmentID.IndexOf("~"));
        contextKey =  contextKey.Remove(0, contextKey.IndexOf("~") + 1);
        string SqlStr = "";



        SqlStr = "SELECT distinct " + contextKey + "  FROM  CustomerInformation WHERE " + contextKey + "  LIKE '" + prefixText + "%' AND CompanyID='" + CompanyID + "' and Divisionid='" + DivisionID + "' and DepartmentID='" + DepartmentID + "'";

        List<string> items = new List<string>(myint);
        try
        {

           


            SqlConnection ConString = new SqlConnection();
            ConString.ConnectionString = ConfigurationManager.ConnectionStrings["EnterpriseConnectionString"].ToString();
            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = ConString;
            ConString.Open();
            Cmd.CommandText = SqlStr;
            SqlDataReader dr;

            dr = Cmd.ExecuteReader();


            while (dr.Read())
            {
                items.Add(dr[contextKey].ToString());

            }
            dr.Close();
            ConString.Close();
        }
        catch (Exception ex)
        {


        }
        return items.ToArray();
    }
    

   [WebMethod]
    public string[] GetCompletionListCustomersAll(string prefixText, int count, string contextKey)
    {
        

          if (prefixText.Equals("xyz"))
        {
            return new string[0];
        }

        int myint = 12;

        string mycontx = contextKey;
        string CompanyID = contextKey.Remove(contextKey.IndexOf("!"));
        string DivisionID = contextKey.Remove(0, contextKey.IndexOf("!") + 1);
        DivisionID = DivisionID.Remove(DivisionID.IndexOf("*"));
        string DepartmentID = contextKey.Remove(0, contextKey.IndexOf("*") + 1);
        DepartmentID = DepartmentID.Remove(DepartmentID.IndexOf("~"));
        contextKey = contextKey.Remove(0, contextKey.IndexOf("~") + 1);
        string SqlStr = "";



        SqlStr = "SELECT distinct " + contextKey + " , CustomerFirstName , CustomerLastName,CustomerCity, CustomerState,CustomerZip,CustomerCompany,CustomerPhone,CustomerEmail  FROM  CustomerInformation WHERE  (" + contextKey + "  LIKE '%" + prefixText + "%' OR CustomerFirstName " + "  LIKE '%" + prefixText + "%' OR CustomerLastName " + "  LIKE '%" + prefixText + "%' OR CustomerCity " + "  LIKE '%" + prefixText + "%' OR CustomerState " + "  LIKE '%" + prefixText + "%' OR CustomerZip " + "  LIKE '%" + prefixText + "%' OR CustomerPhone " + "  LIKE '%" + prefixText + "%' OR CustomerEmail " + "  LIKE '%" + prefixText + "%' OR CustomerCompany " + "  LIKE '%" + prefixText + "%' ) AND CompanyID='" + CompanyID + "' and Divisionid='" + DivisionID + "' and DepartmentID='" + DepartmentID + "'";


        List<string> items = new List<string>(myint);
        try
        {

            SqlConnection ConString = new SqlConnection();
            ConString.ConnectionString = ConfigurationManager.ConnectionStrings["EnterpriseConnectionString"].ToString();
            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = ConString;
            ConString.Open();
            Cmd.CommandText = SqlStr;
            SqlDataReader dr;

            dr = Cmd.ExecuteReader();


            while (dr.Read())
            {
                // items.Add(dr[contextKey].ToString());
                string itemsStr = "";
                //"[" + dr["CustomerID"].ToString() + "] " + dr["CustomerFirstName"].ToString() + " " + dr["CustomerLastName"].ToString() + ", " + dr["CustomerCity"].ToString() + ", " + dr["CustomerState"].ToString() + " - " + dr["CustomerZip"].ToString() + ", " + dr["CustomerCompany"].ToString() + ", " + dr["CustomerPhone"].ToString() + ", " + dr["CustomerEmail"].ToString() + "."
                itemsStr = itemsStr + "[";

                try
                {
                    if (dr["CustomerID"].ToString().Trim() != "")
                    {
                        itemsStr = itemsStr + dr["CustomerID"].ToString();
                    }
                }
                catch (Exception ex)
                {


                }
                itemsStr = itemsStr + "] ";

                try
                {
                    if (dr["CustomerFirstName"].ToString().Trim() != "")
                    {


                        itemsStr = itemsStr + dr["CustomerFirstName"].ToString();
                        

                    }
                }
                catch (Exception ex)
                {


                }

                try
                {
                    if (dr["CustomerLastName"].ToString().Trim() != "")
                    {
			itemsStr = itemsStr + " ";
                        itemsStr = itemsStr + dr["CustomerLastName"].ToString();
                         
                    }

                }
                catch (Exception ex)
                {


                }

                

                
                items.Add(itemsStr);

            }
            dr.Close();
            ConString.Close();
        }
        catch (Exception ex)
        {


        }
        return items.ToArray();
    }


    //Web  Service Used for Populating Common Delivery locations

    [WebMethod]
    public string[] GetCompletionListInterest(string prefixText, int count, string contextKey)
    {


        if (prefixText.Equals("xyz"))
        {
            return new string[0];
        }

        int myint = 12;

        string mycontx = contextKey;
        string CompanyID = contextKey.Remove(contextKey.IndexOf("!"));
        string DivisionID =contextKey.Remove(0, contextKey.IndexOf("!") + 1);
        DivisionID = DivisionID.Remove(DivisionID.IndexOf("*"));
        string DepartmentID =contextKey.Remove(0, contextKey.IndexOf("*") + 1);
        DepartmentID =DepartmentID.Remove(DepartmentID.IndexOf("~"));
        contextKey = contextKey.Remove(0, contextKey.IndexOf("~") + 1);
        string SqlStr = "";



        SqlStr = "SELECT distinct " + contextKey + "  FROM  DeliveryLocations WHERE " + contextKey + "  LIKE '" + prefixText + "%' AND CompanyID='" + CompanyID + "' and Divisionid='" + DivisionID + "' and DepartmentID='" + DepartmentID + "'";

        List<string> items = new List<string>(myint);
        try
        {




            SqlConnection ConString = new SqlConnection();
            ConString.ConnectionString = ConfigurationManager.ConnectionStrings["EnterpriseConnectionString"].ToString();
            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = ConString;
            ConString.Open();
            Cmd.CommandText = SqlStr;
            SqlDataReader dr;

            dr = Cmd.ExecuteReader();


            while (dr.Read())
            {
                items.Add(dr[contextKey].ToString());

            }
            dr.Close();
            ConString.Close();
        }
        catch (Exception ex)
        {


        }
        return items.ToArray();
    }

  
    [WebMethod]
    public string[] GetCompletionListCustomersFL(string prefixText, int count, string contextKey)
    {


        if (prefixText.Equals("xyz"))
        {
            return new string[0];
        }

        int myint = 12;

        string mycontx = contextKey;
        string CompanyID = contextKey.Remove(contextKey.IndexOf("!"));
        string DivisionID = contextKey.Remove(0, contextKey.IndexOf("!") + 1);
        DivisionID = DivisionID.Remove(DivisionID.IndexOf("*"));
        string DepartmentID = contextKey.Remove(0, contextKey.IndexOf("*") + 1);
        DepartmentID = DepartmentID.Remove(DepartmentID.IndexOf("~"));
        contextKey = contextKey.Remove(0, contextKey.IndexOf("~") + 1);
        string SqlStr = "";



        SqlStr = "SELECT distinct " + contextKey + " , CustomerFirstName , CustomerLastName , CustomerCompany  FROM  CustomerInformation WHERE  (" + contextKey + "  LIKE '%" + prefixText + "%' OR CustomerFirstName " + "  LIKE '%" + prefixText + "%' OR CustomerLastName " + "  LIKE '%" + prefixText + "%'  OR CustomerCompany " + "  LIKE '%" + prefixText + "%' )  AND CompanyID='" + CompanyID + "' and Divisionid='" + DivisionID + "' and DepartmentID='" + DepartmentID + "'";


        List<string> items = new List<string>(myint);
        try
        {

            SqlConnection ConString = new SqlConnection();
            ConString.ConnectionString = ConfigurationManager.ConnectionStrings["EnterpriseConnectionString"].ToString();
            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = ConString;
            ConString.Open();
            Cmd.CommandText = SqlStr;
            SqlDataReader dr;

            dr = Cmd.ExecuteReader();


            while (dr.Read())
            {
                string itemsStr = "";
                itemsStr = itemsStr + "[";

                try
                {
                    if (dr["CustomerID"].ToString().Trim() != "")
                    {
                        itemsStr = itemsStr + dr["CustomerID"].ToString();
                    }
                }
                catch (Exception ex)
                {


                }
                itemsStr = itemsStr + "] ";

                try
                {
                    if (dr["CustomerFirstName"].ToString().Trim() != "")
                    {


                        itemsStr = itemsStr + dr["CustomerFirstName"].ToString();
                        itemsStr = itemsStr + " ";

                    }
                }
                catch (Exception ex)
                {


                }

                try
                {
                    if (dr["CustomerLastName"].ToString().Trim() != "")
                    {

                        itemsStr = itemsStr + dr["CustomerLastName"].ToString();

                    }

                }
                catch (Exception ex)
                {


                }


try
                {
                    if (dr["CustomerCompany"].ToString().Trim() != "")
                    {

                        itemsStr = itemsStr + " (";
                        itemsStr = itemsStr + dr["CustomerCompany"].ToString();
                        itemsStr = itemsStr + ") ";

                    }

                }
                catch (Exception ex)
                {


                }

                items.Add(itemsStr);

            }
            dr.Close();
            ConString.Close();
        }
        catch (Exception ex)
        {


        }
        return items.ToArray();
    } 
    
}

