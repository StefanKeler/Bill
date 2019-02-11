using BillMicroService.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace BillMicroService.Helper
{
    public class SqlHelper
    {
        static SqlHelper instance = null;

        private SqlHelper(){

        }

        public static SqlHelper Instance()
        {
            if (instance == null)
            {
                instance = new SqlHelper();
            }
            return instance;
         
        }
        

        public string formatAllColumnSelect(string table, string[] filds)
        {

            string r = @"";

            for (int i = 0; i < filds.Length; i++)
            {
                if (i != filds.Length - 1)
                {
                    r += table + "." + "[" + filds[i] + "], \n";
                }
                else
                {
                    r += table + "." + "[" + filds[i] + "]";
                }
            }
            return r;
        }

        public string InsertStr(string table, string[] filds)
        {
            string ins = @"insert into " + table + " (";

            for (int i = 1; i < filds.Length; i++)
            {
                if (i != filds.Length - 1)
                {
                    ins += filds[i] + ", ";
                }
                else
                {
                    ins += filds[i];
                }
            }

            ins += ") values (";

            for (int i = 1; i < filds.Length; i++)
            {
                if (i != filds.Length - 1)
                {
                    ins += "@" + filds[i] + ", ";
                }
                else
                {
                    ins += "@" + filds[i];
                }
            }

            ins += ");";

            return ins;
        }


        public string UpdateStr(string table, string[] filds)
        {

            string ins = @"update " + table + " set ";

            for (int i = 1; i < filds.Length; i++)
            {
                if (i != filds.Length - 1)
                {
                    ins += filds[i] + "=@" + filds[i] + ", ";
                }
                else
                {
                    ins += filds[i] + "=@" + filds[i];
                }
            }

            ins += " where Id=@Id";


            return ins;
        }

        public void Delete(int id, string table, string[] filds) {
            try
            {
                using (SqlConnection connection = new SqlConnection(DBFunctions.ConnectionString))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = String.Format(@"delete from " + table + " where" + table + ".[Id] = @Id;");

                    command.AddParameter("@Id", SqlDbType.Int, id);

                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object NullCheck(int id)
        {
            if (id == 0)
            {
                return null;
            }
            else
            {
                return id;
            }
        }


        public DateTime DBCheckDateNull(SqlDataReader reader, string atribute)
        {
            DateTime date = new DateTime();
            if (reader[atribute].Equals(DBNull.Value))
            {
                return date;
            }
            else
            {
                return (DateTime)reader[atribute];
            }
        }


        public int DBCheckIntNull(SqlDataReader reader, string atribute)
        {
            int result = 0;
            if (reader[atribute].Equals(DBNull.Value))
            {
                return result;
            }
            else
            {
                return result = Convert.ToInt32(reader[atribute]);
            }

        }
    }
}