using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BillMicroService.Helper;
using BillMicroService.Models;


namespace BillMicroService.DataAccess
{
    public class BillDB
    {
        // !!!! Citanje reda
        private static Bill ReadRow(SqlDataReader reader)
        {
            Bill retVal = new Bill();

            retVal.Id = (int)reader["Id"];
            retVal.Date = (DateTime)reader["Date"];
            retVal.BillNumber = reader["BillNumber"] as string;
            retVal.TotalPrice = (decimal)reader["TotalPrice"];
            retVal.BuyerId = SqlHelper.Instance().DBCheckIntNull(reader, "BuyerId");
            retVal.Valute = reader["Valute"] as string;
            retVal.isIssued = (int)reader["isIssued"];
            retVal.BuyerId = SqlHelper.Instance().DBCheckIntNull(reader, "BuyerId");

            return retVal;
        }

        // !!!! naziv tabele
        private static string table = "[shopcart].[Bill]";

        // !!!! polja tabele              
        private static string[] filds = { "Id", "Date", "BillNumber", "TotalPrice", "BuyerId", "Valute", "isIssued"};

        // Baza string
        private static string AllColumnSelect
        {
            get
            {
                return SqlHelper.Instance().formatAllColumnSelect(table, filds);
            }
        }

        // Get All
        public static List<Bill> GetAll()
        {
            try
            {
                // !!!!
                List<Bill> retVal = new List<Bill>();

                using (SqlConnection connection = new SqlConnection(DBFunctions.ConnectionString))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = String.Format(@"
                        SELECT
                            {0}
                        FROM
                            {1}
                    ", AllColumnSelect, table);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            // !!!!
                            Bill b = ReadRow(reader);
                            retVal.Add(b);
                            b.BillItems = BillItemDB.GetAllByBillId((int)b.Id);
                            foreach (BillItem i in b.BillItems) {
                                if (i.ItemType == "Product")
                                {
                                    i.Product = BillItemDB.GetProduct("product/Product", i.IdProduct);
                                }
                                else if (i.ItemType == "Service")
                                {
                                    i.Service = BillItemDB.GetService("service/Service", i.IdService);
                                    i.Appointment = BillItemDB.GetAppointment("calendar/Appointment", i.IdAppointment);
                                }
                                else
                                {
                                    // return null;
                                }
                            }
                            //p.Address = GetObj("address/Address", p.IdAddress);
                            // !!!!
                        }
                    }
                }
                return retVal;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // get by id
        public static Bill GetById(int Id)
        {
            try
            {

                Bill retVal = new Bill();


                using (SqlConnection connection = new SqlConnection(DBFunctions.ConnectionString))
                {

                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = String.Format(@"
                        SELECT
                            {0}
                        FROM
                            {1}
                        WHERE
                            [Id] = @Id
                    ", AllColumnSelect, table);

                    command.AddParameter("@Id", SqlDbType.Int, Id);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            retVal = ReadRow(reader);
                            retVal.BillItems = BillItemDB.GetAllByBillId((int)retVal.Id);
                            //retVal.Category = BillCategoryDB.GetById(retVal.IdCategory);
                            //retVal.Address = GetObj("address/Address", retVal.IdAddress);
                            foreach (BillItem i in retVal.BillItems)
                            {
                                if (i.ItemType == "Product")
                                {
                                    i.Product = BillItemDB.GetProduct("product/Product", i.IdProduct);
                                }
                                else if (i.ItemType == "Service")
                                {
                                    i.Service = BillItemDB.GetService("service/Service", i.IdService);
                                    i.Appointment = BillItemDB.GetAppointment("calendar/Appointment", i.IdAppointment);
                                }
                                else
                                {
                                    // return null;
                                }
                            }

                        }
                    }
                }
                return retVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // add
        public static void Add(Bill newItem)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(DBFunctions.ConnectionString))
                {


                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = SqlHelper.Instance().InsertStr(table, filds);

                    // !!!!
                    command.AddParameter("@Date", SqlDbType.Date, newItem.Date);
                    command.AddParameter("@BillNumber", SqlDbType.NVarChar, newItem.BillNumber);
                    command.AddParameter("@TotalPrice", SqlDbType.Money, newItem.TotalPrice);
                    command.AddParameter("@BuyerId", SqlDbType.Int, SqlHelper.Instance().NullCheck(newItem.BuyerId));
                    command.AddParameter("@Valute", SqlDbType.NVarChar, newItem.Valute);
                    command.AddParameter("@isIssued", SqlDbType.Int, newItem.isIssued);

                    
                    // !!!!

                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // update
        public static void Update(Bill newItem)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(DBFunctions.ConnectionString))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = SqlHelper.Instance().UpdateStr(table, filds);

                    command.AddParameter("@Id", SqlDbType.Int, newItem.Id);
                    command.AddParameter("@Date", SqlDbType.Date, newItem.Date);
                    command.AddParameter("@BillNumber", SqlDbType.NVarChar, newItem.BillNumber);
                    command.AddParameter("@TotalPrice", SqlDbType.Money, newItem.TotalPrice);
                    command.AddParameter("@BuyerId", SqlDbType.Int, SqlHelper.Instance().NullCheck(newItem.BuyerId));
                    command.AddParameter("@Valute", SqlDbType.NVarChar, newItem.Valute);
                    command.AddParameter("@isIssued", SqlDbType.Int, newItem.isIssued);
                    // !!!!

                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*// update
        public static void UpdatePrice(decimal price, )
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(DBFunctions.ConnectionString))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = SqlHelper.Instance().UpdateStr(table, filds);

                    command.AddParameter("@Id", SqlDbType.Int, newItem.Id);
                    command.AddParameter("@Date", SqlDbType.Date, newItem.Date);
                    command.AddParameter("@BillNumber", SqlDbType.NVarChar, newItem.BillNumber);
                    command.AddParameter("@TotalPrice", SqlDbType.Money, newItem.TotalPrice);
                    command.AddParameter("@BuyerId", SqlDbType.Int, SqlHelper.Instance().NullCheck(newItem.BuyerId));
                    command.AddParameter("@Valute", SqlDbType.NVarChar, newItem.Valute);
                    command.AddParameter("@isIssued", SqlDbType.Int, newItem.isIssued);
                    // !!!!

                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/

        // delete
        public static void Delete(int id)
        {
            SqlHelper.Instance().Delete(id, table, filds);
        }

       
    }
}