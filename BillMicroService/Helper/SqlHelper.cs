using BillMicroService.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;


namespace BillMicroService.Helper
{
    public class SqlHelper
    {
        static SqlHelper instance = null;

        private SqlHelper()
        {

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
            StringBuilder bld = new StringBuilder();
            bld.Append(@"");
            for (int i = 0; i < filds.Length; i++)
            {
                if (i != filds.Length - 1)
                {
                    bld.Append(table + "." + "[" + filds[i] + "], \n");

                }
                else
                {
                    bld.Append(table + "." + "[" + filds[i] + "]");

                }
            }
            return bld.ToString();
        }

        public string InsertStr(string table, string[] filds)
        {

            StringBuilder bld = new StringBuilder();
            bld.Append(@"insert into " + table + " (");

            for (int i = 1; i < filds.Length; i++)
            {
                if (i != filds.Length - 1)
                {
                    bld.Append(filds[i] + ", ");
                }
                else
                {
                    bld.Append(filds[i]);
                }
            }

            bld.Append(") values (");

            for (int i = 1; i < filds.Length; i++)
            {
                if (i != filds.Length - 1)
                {
                    bld.Append("@" + filds[i] + ", ");

                }
                else
                {
                    bld.Append("@" + filds[i]);

                }
            }
            bld.Append(");");

            return bld.ToString();
        }


        public string UpdateStr(string table, string[] filds)
        {
            StringBuilder bld = new StringBuilder();
            bld.Append(@"update " + table + " set ");


            for (int i = 1; i < filds.Length; i++)
            {
                if (i != filds.Length - 1)
                {
                    bld.Append(filds[i] + "=@" + filds[i] + ", ");

                }
                else
                {
                    bld.Append(filds[i] + "=@" + filds[i]);

                }
            }

            bld.Append(" where Id=@Id");


            return bld.ToString();
        }

        public void Delete(int id, string table, string[] filds)
        {
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
                throw new Exception("Delete exc", ex);
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

            if (reader[atribute].Equals(DBNull.Value))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(reader[atribute]);
            }

        }
    }
}