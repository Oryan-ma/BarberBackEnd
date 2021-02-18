﻿using barberBackEnd.Models;
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
                    command += $"Insert Into barber_tbl (Name,Last_Name,Customer_Gender,Phone,Password,Email) " +
                        $"values('{b.Name}','{b.Last_Name}','{b.Customer_Gender}'," +
                        $"'{b.Phone}','{b.Password}', '{b.Email}')";
                    break;
                case Customer c:
                    c = type as Customer;
                    command += $"Insert Into customer_tbl (Name,Last_Name,Gender,Phone,Password,Email) " +
                        $"values('{c.Name}','{c.Last_Name}','{c.Gender}'," +
                        $"'{c.Phone}','{c.Password}', '{c.Email}')";
                    break;
                case List<Service> s:
                    if (s.Count > 0)
                    {
                        command += "insert into Services_tbl (Barber_Id,Service_Name,Service_Price) values";
                        for (int i = 0; i < s.Count; i++)
                        {
                            command += $"('{s[i].Barber_Email}','{s[i].Service_Name}',{s[i].Service_Price}),";
                        }
                        command = command.Remove(command.Length - 1);
                    }
                    break;
                case Queue q:

                default:
                    break;
            }
            con = CreateConnction();
            cmd = CreateCommand(command, con);
            ExeSQLCommand(cmd, con);

            return 1;
        }
        //to chek
        public List<ShopQueue> GetShopQueue()
        {
            SqlConnection con;
            SqlCommand cmd;

            string query = "";
            query = $"select * from queue_tbl";

            SqlDataReader dr;
            CallSQL(out con, out cmd, query, out dr);
            List<ShopQueue> s = new List<ShopQueue>();

            while (dr.Read())
            {   // Read till the end of the data into a row
                ShopQueue sq = new ShopQueue();
                ReadQueue(dr, sq);
                s.Add(sq);
            }
            con.Close();
            //cmd.Connection.Close();
            return s;
        }
        //todo

        public void RemoveAppointmentFromQueue()
        {

        }
        //todo
        public void Add2Queue(ShopQueue sq)
        {

        }
        private static void ReadQueue(SqlDataReader dr, ShopQueue sq)
        {
            sq.Barber_Email = (string)dr["Barber_Email"];
            sq.Customer_Email = (string)dr["Customer_Email"];
            sq.time = (DateTime)dr["Queue_time"];
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
                        $"from barber_tbl " +
                        $"where Email='{b.Email}'";
                    break;
                case Customer c:
                    c = type as Customer;
                    query = $"select * from customer_tbl where Email='{c.Email}'";
                    break;
                default:
                    break;
            }
            SqlDataReader dr;
            CallSQL(out con, out cmd, query, out dr);

            switch (type)
            {
                case Barber b:
                    b = new Barber();
                    if (dr.Read())
                    {   // Read till the end of the data into a row
                        ReadBarber(dr, b);
                    }
                    return (T)Convert.ChangeType(b, typeof(T));
                case Customer c:
                    c = new Customer();
                    if (dr.Read())
                    {   // Read till the end of the data into a row
                        ReadCustomer(dr, c);
                    }
                    return (T)Convert.ChangeType(c, typeof(T));
                //break;
                default:
                    break;
            }


            con.Close();
            //cmd.Connection.Close();
            return type;
        }

        private static void ReadBarber(SqlDataReader dr, Barber b)
        {
            b.Name = (string)dr["Name"];
            b.Last_Name = (string)dr["Last_Name"];
            b.Customer_Gender = Convert.ToChar(dr["Customer_Gender"]);
            b.Password = (string)dr["Password"];
            b.Email = (string)dr["Email"];
        }

        private static void ReadCustomer(SqlDataReader dr, Customer c)
        {
            c.Name = (string)dr["Name"];
            c.Last_Name = (string)dr["Last_Name"];
            c.Gender = Convert.ToChar(dr["Gender"]);
            c.Phone = (string)dr["Phone"];
            c.Password = (string)dr["Password"];
            c.Email = (string)dr["Email"];
        }

        public List<Service> GetServices(string email)
        {
            SqlConnection con;
            SqlCommand cmd;
            string query = "";
            query = $"select * " +
                $"from barber_tbl b inner join Services_tbl s on b.Email= s.barber_Id " +
            $"where Email='{email}'";

            SqlDataReader dr;
            CallSQL(out con, out cmd, query, out dr);
            List<Service> s = new List<Service>();
            while (dr.Read())
            {   // Read till the end of the data into a row
                Service ser = new Service();
                ser.Service_Name = (string)dr["Service_Name"];
                ser.Service_Price = double.Parse(dr["Service_Price"].ToString());
                s.Add(ser);
            }
            con.Close();
            //cmd.Connection.Close();
            return s;
        }

        private static void CallSQL(out SqlConnection con, out SqlCommand cmd, string query, out SqlDataReader dr)
        {
            con = CreateConnction();
            cmd = new SqlCommand(query, con);
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
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