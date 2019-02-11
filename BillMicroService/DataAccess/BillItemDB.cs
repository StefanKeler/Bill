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
    public class BillItemDB
    {
        // !!!! Citanje reda
        private static BillItem ReadRow(SqlDataReader reader)
        {
            BillItem retVal = new BillItem();

            retVal.Id = (int)reader["Id"];
            retVal.BillItemNumber = reader["BillItemNumber"] as string;
            retVal.Amount = (int)reader["Amount"];
            retVal.Discount = (int)reader["Discount"];
            retVal.ItemType = reader["ItemType"] as string;
            retVal.IdBill = SqlHelper.Instance().DBCheckIntNull(reader, "IdBill");
            retVal.IdService = SqlHelper.Instance().DBCheckIntNull(reader, "IdService");
            retVal.IdProduct = SqlHelper.Instance().DBCheckIntNull(reader, "IdProduct");
            retVal.IdAppointment = SqlHelper.Instance().DBCheckIntNull(reader, "IdAppointment");

            return retVal;
        }

        // !!!! naziv tabele
        private static string table = "[shopcart].[BillItem]";

        // !!!! polja tabele              
        private static string[] filds = { "Id", "BillItemNumber", "Amount", "Discount", "ItemType", "IdBill", "IdService", "IdProduct", "IdAppointment" };


        // Baza string
        private static string AllColumnSelect
        {
            get
            {
                return SqlHelper.Instance().formatAllColumnSelect(table, filds);
            }
        }

        // Get All
        public static List<BillItem> GetAll()
        {
            try
            {
                // !!!!
                List<BillItem> retVal = new List<BillItem>();

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
                            BillItem b = ReadRow(reader);
                            retVal.Add(b);

                            if (b.ItemType == "Product")
                            {
                                b.Product = GetProduct("product/Product", b.IdProduct);
                               

                            }
                            else if (b.ItemType == "Service")
                            {
                                b.Service = GetService("service/Service", b.IdService);
                                b.Appointment = GetAppointment("calendar/Appointment", b.IdAppointment);
                            }
                            else
                            {
                                // return null;
                            }
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




        public static List<BillItem> GetAllByBillId(int IdBill)
        {
            try
            {
                // !!!!
                List<BillItem> retVal = new List<BillItem>();

                using (SqlConnection connection = new SqlConnection(DBFunctions.ConnectionString))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = String.Format(@"
                        SELECT
                            {0}
                        FROM
                            {1}
                        WHERE
                            [IdBill] = @IdBill
                    ", AllColumnSelect, table);

                    command.AddParameter("@IdBill", SqlDbType.Int, IdBill);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            // !!!!
                            BillItem b = ReadRow(reader);
                            retVal.Add(b);

                            if (b.ItemType == "Product")
                            {
                                b.Product = GetProduct("product/Product", b.IdProduct);
                            }
                            else if (b.ItemType == "Service")
                            {
                                b.Service = GetService("service/Service", b.IdService);
                                b.Appointment = GetAppointment("calendar/Appointment", b.IdAppointment);
                            }
                            else
                            {
                                // return null;
                            }
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
        public static BillItem GetById(int Id)
        {
            try
            {

                BillItem retVal = new BillItem();


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

                            if (retVal.ItemType == "Product") {
                                retVal.Product = GetProduct("product/Product", retVal.IdProduct);
                            } else if (retVal.ItemType == "Service") {
                                retVal.Service = GetService("service/Service", retVal.IdService);
                            }
                            else{
                               // return null;
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
        public static void Add(BillItem newItem)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(DBFunctions.ConnectionString))
                {


                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = SqlHelper.Instance().InsertStr(table, filds);

                    // !!!!
                    command.AddParameter("@BillItemNumber", SqlDbType.NVarChar, newItem.BillItemNumber);
                    command.AddParameter("@Amount", SqlDbType.Int, newItem.Amount);
                    command.AddParameter("@Discount", SqlDbType.Int, newItem.Discount);
                    command.AddParameter("@ItemType", SqlDbType.NVarChar, newItem.ItemType);
                    command.AddParameter("@IdBill", SqlDbType.Int, SqlHelper.Instance().NullCheck(newItem.IdBill));
                    command.AddParameter("@IdService", SqlDbType.Int, SqlHelper.Instance().NullCheck(newItem.IdService));
                    command.AddParameter("@IdProduct", SqlDbType.Int, SqlHelper.Instance().NullCheck(newItem.IdProduct));
                    command.AddParameter("@IdAppointment", SqlDbType.Int, SqlHelper.Instance().NullCheck(newItem.IdAppointment));

                    decimal price = newItem.Amount * (100 - newItem.Discount) / 100;
                    

                    if (newItem.ItemType == "Product")
                    {
                        price = price * GetProduct("product/Product", newItem.IdProduct).Price;
                    }
                    else if (newItem.ItemType == "Service")
                    {
                        price = price * GetService("service/Service", newItem.IdService).Price;
                    }
                    else
                    {
                        // return null;
                    }
                   


                    // !!!!

                    connection.Open();

                    command.ExecuteNonQuery();

                    Bill b = BillDB.GetById(newItem.IdBill);
                    b.TotalPrice = b.TotalPrice + price;
                    BillDB.Update(b);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // update
        public static void Update(BillItem newItem)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(DBFunctions.ConnectionString))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = SqlHelper.Instance().UpdateStr(table, filds);

                    command.AddParameter("@Id", SqlDbType.Int, newItem.Id);
                    command.AddParameter("@BillItemNumber", SqlDbType.NVarChar, newItem.BillItemNumber);
                    command.AddParameter("@Amount", SqlDbType.Int, newItem.Amount);
                    command.AddParameter("@Discount", SqlDbType.Int, newItem.Discount);
                    command.AddParameter("@ItemType", SqlDbType.NVarChar, newItem.ItemType);
                    command.AddParameter("@IdBill", SqlDbType.Int, SqlHelper.Instance().NullCheck(newItem.IdBill));
                    command.AddParameter("@IdService", SqlDbType.Int, SqlHelper.Instance().NullCheck(newItem.IdService));
                    command.AddParameter("@IdProduct", SqlDbType.Int, SqlHelper.Instance().NullCheck(newItem.IdProduct));
                    command.AddParameter("@IdAppointment", SqlDbType.Int, SqlHelper.Instance().NullCheck(newItem.IdAppointment));
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

        // delete
        public static void Delete(int id)
        {
            SqlHelper.Instance().Delete(id, table, filds);
        }

        // GET PREKO HTTPA
        public static Service GetService(string uri, int id)
        {

            var obj = JsonConvert.DeserializeObject<Service>(HttpHelper.Instance().GetById(uri, 50070, id));
            return obj;
        }

        public static Product GetProduct(string uri, int id)
        {

            var obj = JsonConvert.DeserializeObject<Product>(HttpHelper.Instance().GetById(uri, 50069, id));
            return obj;
        }

        // GET PREKO HTTPA
        public static Appointment GetAppointment(string uri, int id)
        {

            var obj = JsonConvert.DeserializeObject<Appointment>(HttpHelper.Instance().GetById(uri, 50072, id));
            return obj;
        }
    }
}