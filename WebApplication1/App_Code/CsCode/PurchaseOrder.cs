using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace PurchaseModuleUI
{
    public class PurchaseOrder
    {
        //public PurchaseHeaderModel purchaseHeader { get; set; }
        //public PurchaseDetailModel puchaseDetail { get; set; }

        private string connectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();

        public bool AddPurchseHeader(string companyID, string divisionID, string departmentID, string PurchaseOrderNumber, ref long purchaseNumber, string transactionTypeID,
                                    DateTime purchaseDate, DateTime purchaseDateRequested, string orderedBy, string taxExemptID, string taxGroupID, string vendorID,
                                    string currencyID, decimal currencyExchangeRate, decimal subtotal, decimal discountPers, decimal discountAmount, decimal taxPercent,
                                    decimal taxAmount, decimal taxableSubTotal, decimal freight, decimal handling, decimal total, string shipMethodID, bool posted,
                                    DateTime postedDate, string locationID, string ShipLocationID, string InternalNotes, string PaymentMethodID, string TrackingNumber, string OrderNumber)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("[enterprise].[AddPurchaseOrderHeader]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    #region parameters

                    command.Parameters.AddWithValue("companyID", companyID);
                    command.Parameters.AddWithValue("divisionID", divisionID);

                    command.Parameters.AddWithValue("departmentID", departmentID);
                    command.Parameters.AddWithValue("PurchaseOrderNumber", PurchaseOrderNumber);

                    command.Parameters.AddWithValue("TrackingNumber", TrackingNumber);
                    command.Parameters.AddWithValue("OrderNumber", OrderNumber);

                    command.Parameters.AddWithValue("transactionTypeID", transactionTypeID);
                    command.Parameters.AddWithValue("purchaseDate", purchaseDate);

                    command.Parameters.AddWithValue("purchaseDateRequested", purchaseDateRequested);
                    command.Parameters.AddWithValue("orderedBy", orderedBy);
                    command.Parameters.AddWithValue("taxExemptID", taxExemptID);
                    command.Parameters.AddWithValue("taxGroupID", taxGroupID);

                    command.Parameters.AddWithValue("vendorID", vendorID);
                    command.Parameters.AddWithValue("currencyID", currencyID);
                    command.Parameters.AddWithValue("currencyExchangeRate", currencyExchangeRate);
                    command.Parameters.AddWithValue("subtotal", subtotal);

                    command.Parameters.AddWithValue("discountPers", discountPers);
                    command.Parameters.AddWithValue("discountAmount", discountAmount);
                    command.Parameters.AddWithValue("taxPercent", taxPercent);
                    command.Parameters.AddWithValue("taxAmount", taxAmount);

                    command.Parameters.AddWithValue("taxableSubTotal", taxableSubTotal);
                    command.Parameters.AddWithValue("freight", freight);
                    command.Parameters.AddWithValue("handling", handling);
                    command.Parameters.AddWithValue("total", total);

                    command.Parameters.AddWithValue("shipMethodID", shipMethodID);
                    command.Parameters.AddWithValue("posted", posted);
                    command.Parameters.AddWithValue("postedDate", postedDate);
                    command.Parameters.AddWithValue("locationID", locationID);
                    command.Parameters.AddWithValue("ShipLocationID", ShipLocationID);
                    command.Parameters.AddWithValue("InternalNotes", InternalNotes);
                    command.Parameters.AddWithValue("PaymentMethodID", PaymentMethodID);
                    

                  

                    SqlParameter pn = new SqlParameter("purchaseNumber", purchaseNumber);
                    pn.Direction = ParameterDirection.Output;

                    command.Parameters.Add(pn);

                    #endregion

command.Connection.Open();
                        command.ExecuteNonQuery();

                        purchaseNumber = (long)pn.Value;


                    try
                    {
                        
                        return true;
                    }
                    catch
                    {
                        purchaseNumber = -1;
                        return false;
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }
            }
        }
        
         public bool UpdatePurchseHeader(string companyID, string divisionID, string departmentID, string PurchaseOrderNumber, ref long purchaseNumber, string transactionTypeID,
                                    DateTime purchaseDate, DateTime purchaseDateRequested, string orderedBy, string taxExemptID, string taxGroupID, string vendorID,
                                    string currencyID, decimal currencyExchangeRate, decimal subtotal, decimal discountPers, decimal discountAmount, decimal taxPercent,
                                    decimal taxAmount, decimal taxableSubTotal, decimal freight, decimal handling, decimal total, string shipMethodID, bool posted,
                                    DateTime postedDate, string locationID, string ShipLocationID, string InternalNotes, string PaymentMethodID, string TrackingNumber, string OrderNumber)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("[enterprise].[UpdatePurchaseOrderHeader]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    #region parameters

                    command.Parameters.AddWithValue("companyID", companyID);
                    command.Parameters.AddWithValue("divisionID", divisionID);

                    command.Parameters.AddWithValue("departmentID", departmentID);
                    command.Parameters.AddWithValue("PurchaseOrderNumber", PurchaseOrderNumber);

                    command.Parameters.AddWithValue("TrackingNumber", TrackingNumber);
                    command.Parameters.AddWithValue("OrderNumber", OrderNumber);

                    command.Parameters.AddWithValue("transactionTypeID", transactionTypeID);
                    command.Parameters.AddWithValue("purchaseDate", purchaseDate);

                    command.Parameters.AddWithValue("purchaseDateRequested", purchaseDateRequested);
                    command.Parameters.AddWithValue("orderedBy", orderedBy);
                    command.Parameters.AddWithValue("taxExemptID", taxExemptID);
                    command.Parameters.AddWithValue("taxGroupID", taxGroupID);

                    command.Parameters.AddWithValue("vendorID", vendorID);
                    command.Parameters.AddWithValue("currencyID", currencyID);
                    command.Parameters.AddWithValue("currencyExchangeRate", currencyExchangeRate);
                    command.Parameters.AddWithValue("subtotal", subtotal);

                    command.Parameters.AddWithValue("discountPers", discountPers);
                    command.Parameters.AddWithValue("discountAmount", discountAmount);
                    command.Parameters.AddWithValue("taxPercent", taxPercent);
                    command.Parameters.AddWithValue("taxAmount", taxAmount);

                    command.Parameters.AddWithValue("taxableSubTotal", taxableSubTotal);
                    command.Parameters.AddWithValue("freight", freight);
                    command.Parameters.AddWithValue("handling", handling);
                    command.Parameters.AddWithValue("total", total);

                    command.Parameters.AddWithValue("shipMethodID", shipMethodID);
                    command.Parameters.AddWithValue("posted", posted);
                    command.Parameters.AddWithValue("postedDate", postedDate);
                    command.Parameters.AddWithValue("locationID", locationID);
                    command.Parameters.AddWithValue("ShipLocationID", ShipLocationID);
                    command.Parameters.AddWithValue("InternalNotes", InternalNotes);
                    command.Parameters.AddWithValue("PaymentMethodID", PaymentMethodID);
                    

                  

                    SqlParameter pn = new SqlParameter("purchaseNumber", purchaseNumber);
                    pn.Direction = ParameterDirection.Output;

                    command.Parameters.Add(pn);

                    #endregion

command.Connection.Open();
                        command.ExecuteNonQuery();

                        purchaseNumber = (long)pn.Value;


                    try
                    {
                        
                        return true;
                    }
                    catch
                    {
                        purchaseNumber = -1;
                        return false;
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }
            }
        }
        
        public bool AddPurchaseDetail(string companyID, string divisionID, string departmentID, long purchaseNumber,
										ref long purchaseLineNumber, string itemID, string vendorItemID, string description, decimal orderQty,
										string itemUOM, decimal packSize, decimal qty, decimal itemUnitPrice, decimal subTotal, decimal total, 
										decimal discountPerc, decimal taxable, decimal itemCost, string currencyID,
										string currencyExchangeRate, string taxGroupID, decimal taxAmount, decimal taxPercent,
										string GLPurchaseAccount, string color)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("[enterprise].[AddPurchaseOrderDetail]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    #region Parameters

                    command.Parameters.AddWithValue("companyID", companyID);
                    command.Parameters.AddWithValue("divisionID", divisionID);
                    command.Parameters.AddWithValue("departmentID", departmentID);
                    command.Parameters.AddWithValue("purchaseNumber", purchaseNumber);

                    command.Parameters.AddWithValue("itemID", itemID);
                    command.Parameters.AddWithValue("vendorItemID", vendorItemID);
                    command.Parameters.AddWithValue("description", description);
                    command.Parameters.AddWithValue("orderQty", orderQty);

                    command.Parameters.AddWithValue("itemUOM", itemUOM);
                    command.Parameters.AddWithValue("packSize", packSize);
                    command.Parameters.AddWithValue("qty", qty);
                    command.Parameters.AddWithValue("itemUnitPrice", itemUnitPrice);

                    command.Parameters.AddWithValue("subTotal", subTotal);
                    command.Parameters.AddWithValue("total", total);
                    command.Parameters.AddWithValue("discountPerc", discountPerc);
                    command.Parameters.AddWithValue("taxable", taxable);

                    command.Parameters.AddWithValue("itemCost", itemCost);
                    command.Parameters.AddWithValue("currencyID", currencyID);
                    command.Parameters.AddWithValue("currencyExchangeRate", currencyExchangeRate);
                    command.Parameters.AddWithValue("taxGroupID", taxGroupID);

                    command.Parameters.AddWithValue("taxAmount", taxAmount);
                    command.Parameters.AddWithValue("taxPercent", taxPercent);
                    command.Parameters.AddWithValue("GLPurchaseAccount", GLPurchaseAccount);
                    command.Parameters.AddWithValue("color", color);

                    SqlParameter pln = new SqlParameter("PurchaseLineNumber", purchaseLineNumber);
                    pln.Direction = ParameterDirection.Output;
                    command.Parameters.Add(pln);

                    try
                    {
                        command.Connection.Open();
                        command.ExecuteNonQuery();

                        purchaseLineNumber = (long)pln.Value;

                        return true;
                    }
                    catch (Exception ex)
                    {
                        purchaseLineNumber = -1;
                        return false;
                    }
                    finally
                    {
                        command.Connection.Close();
                    }

                    #endregion
                }
            }
        }

      
        
        public bool UpdatePurchaseDetail(string companyID, string divisionID, string departmentID, long purchaseNumber,
                                        long purchaseLineNumber, string itemID, string vendorItemID, string description, decimal orderQty,
                                        string itemUOM, decimal packSize, decimal qty, decimal itemUnitPrice, decimal subTotal, decimal total,
                                        decimal discountPerc, decimal taxable, decimal itemCost, string currencyID,
                                        string currencyExchangeRate, string taxGroupID, decimal taxAmount, decimal taxPercent,
                                        string GLPurchaseAccount, string color)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("[enterprise].[UpdatePurchaseOrderDetail]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    #region Parameters

                    command.Parameters.AddWithValue("companyID", companyID);
                    command.Parameters.AddWithValue("divisionID", divisionID);
                    command.Parameters.AddWithValue("departmentID", departmentID);
                    command.Parameters.AddWithValue("purchaseNumber", purchaseNumber);

                    command.Parameters.AddWithValue("PurchaseLineNumber", purchaseLineNumber);
                    command.Parameters.AddWithValue("itemID", itemID);
                    command.Parameters.AddWithValue("vendorItemID", vendorItemID);
                    command.Parameters.AddWithValue("description", description);
                    command.Parameters.AddWithValue("orderQty", orderQty);

                    command.Parameters.AddWithValue("itemUOM", itemUOM);
                    command.Parameters.AddWithValue("packSize", packSize);
                    command.Parameters.AddWithValue("qty", qty);
                    command.Parameters.AddWithValue("itemUnitPrice", itemUnitPrice);

                    command.Parameters.AddWithValue("subTotal", subTotal);
                    command.Parameters.AddWithValue("total", total);
                    command.Parameters.AddWithValue("discountPerc", discountPerc);
                    command.Parameters.AddWithValue("taxable", taxable);

                    command.Parameters.AddWithValue("itemCost", itemCost);
                    command.Parameters.AddWithValue("currencyID", currencyID);
                    command.Parameters.AddWithValue("currencyExchangeRate", currencyExchangeRate);
                    command.Parameters.AddWithValue("taxGroupID", taxGroupID);

                    command.Parameters.AddWithValue("taxAmount", taxAmount);
                    command.Parameters.AddWithValue("taxPercent", taxPercent);
                    command.Parameters.AddWithValue("GLPurchaseAccount", GLPurchaseAccount);
                    command.Parameters.AddWithValue("color", color);
                    
                    try
                    {
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                    finally
                    {
                        command.Connection.Close();
                    }

                    #endregion
                }
            }
        }

        public bool DeletePurchseHeader(string companyID, string divisionID, string departmentID, long purchaseNumber)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("[enterprise].[DeletePurchaseOrderHeader]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    #region parameters

                    command.Parameters.AddWithValue("companyID", companyID);
                    command.Parameters.AddWithValue("divisionID", divisionID);
                    command.Parameters.AddWithValue("departmentID", departmentID);
                    command.Parameters.AddWithValue("purchaseNumber", purchaseNumber);
                    
                    #endregion

                    try
                    {
                        command.Connection.Open();
                        command.ExecuteNonQuery();
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
        
        public bool DeletePurchaseDetail(string companyID, string divisionID, string departmentID, long purchaseNumber, long purchaseLineNumber)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("[enterprise].[DeletePurchaseOrderDetail]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    #region Parameters

                    command.Parameters.AddWithValue("companyID", companyID);
                    command.Parameters.AddWithValue("divisionID", divisionID);
                    command.Parameters.AddWithValue("departmentID", departmentID);
                    command.Parameters.AddWithValue("purchaseNumber", purchaseNumber);
                    command.Parameters.AddWithValue("PurchaseLineNumber", purchaseLineNumber);

                    try
                    {
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                    finally
                    {
                        command.Connection.Close();
                    }

                    #endregion
                }
            }
        }
        
    }
    
}