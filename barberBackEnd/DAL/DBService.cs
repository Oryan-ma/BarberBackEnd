using barberBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace barberBackEnd.DAL
{
    public class DBService
    {
        public SqlDataAdapter da;
        public DataTable dt;

        public DBService()
        {

        }

        public int Insert2DB<T>(T type)
        {
            SqlConnection con;
            SqlCommand cmd;
            string command = "";
            switch (type)
            {
                case Barber b:
                    b = type as Barber;
                    command += $"Insert Into barber_tbl (Name,Last_Name,Customer_Gender,city_Id,Phone,Password,Email) " +
                        $"values('{b.Name}','{b.Last_Name}','{b.Customer_Gender}'," +
                        $"'{b.City.Id}','{b.Phone}','{b.Password}', '{b.Email}')";
                    break;
                case Customer c:
                    c = type as Customer;
                    command += $"Insert Into customer_tbl (Name,Last_Name,Gender,Phone,Password,Email) " +
                        $"values('{c.Name}','{c.Last_Name}','{c.Gender}'," +
                        $"'{c.Phone}','{c.Password}', '{c.Email}')";
                    break;
                case Queue q:
                case City ci:

                default:
                    break;
            }
            con = CreateConnction();
            cmd = CreateCommand(command, con);             // create the command
            ExeSQLCommand(cmd, con);

            return 1;
        }

        public T Login<T>(T type)
        {
            SqlConnection con;
            SqlCommand cmd;
            string query = "";
            switch (type)
            {
                case Barber b:
                    b = type as Barber;
                    query = $"select * " +
                        $"from barber_tbl b inner join city_tbl c on b.city_id=c.city_id " +
                        $"where Email='{b.Email}'";
                    break;
                case Customer c:
                    c = type as Customer;
                    query = $"select * from customer_tbl where Email='{c.Email}'";
                    break;
                default:
                    break;
            }
            con = CreateConnction();
            cmd = new SqlCommand(query, con);

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            switch (type)
            {
                case Barber b:
                    b = new Barber();
                    if (dr.Read())
                    {   // Read till the end of the data into a row
                        b.Name = (string)dr["Name"];
                        b.Last_Name = (string)dr["Last_Name"];
                        b.Customer_Gender = Convert.ToChar(dr["Customer_Gender"]);
                        b.City = new City();
                        b.City.Id = (string)dr["city_Id"];
                        b.City.Name = (string)dr["city_name"];
                        b.Password = (string)dr["Password"];
                        b.Email = (string)dr["Email"];
                    }
                    return (T)Convert.ChangeType(b, typeof(T));
                case Customer c:
                    c = new Customer();
                    if (dr.Read())
                    {   // Read till the end of the data into a row
                        c.Name = (string)dr["Name"];
                        c.Last_Name = (string)dr["Last_Name"];
                        c.Gender = Convert.ToChar(dr["Gender"]);
                        c.Phone = (string)dr["Phone"];
                        c.Password = (string)dr["Password"];
                        c.Email = (string)dr["Email"];
                    }
                    return (T)Convert.ChangeType(c, typeof(T));
                //break;
                default:
                    break;
            }


            con.Close();
            cmd.Connection.Close();
            return type;
        }
        public static SqlConnection connect(String conString)
        {
            // read the connection string from the configuration file
            string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }

        private void ExeSQLCommand(SqlCommand cmd, SqlConnection con)
        {
            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
            }
            catch (Exception ex)
            {
                // write to log
                //return 0;
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        private static SqlConnection CreateConnction()
        {
            SqlConnection con;
            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            return con;
        }

        private static SqlCommand CreateCommand(string CommandSTR, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

            return cmd;
        }
    }
}