using barberBackEnd.DAL;
using barberBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace barberBackEnd.BLL
{
    public class Login
    {
        DBService db = new DBService();

        public T LoginUser<T>(T type)
        {
            string password = "";
            password = getPassword(type, password);

           // T user = db.Login(type);


            if (type is Barber)
            {
                Barber b = db.Login(type) as Barber;
                if (b.Password == password)
                {
                    return type;
                }
            }
            else if (type is Customer)
            {
                Customer c = db.Login(type) as Customer;
                if (c.Password == password)
                {
                    return type;
                }
            }
            return default;

        }

        private static string getPassword<T>(T type, string password)
        {
            if (type is Barber)
            {
                Barber b = type as Barber;
                password = b.Password;
            }
            else if (type is Customer)
            {
                Customer c = type as Customer;
                password = c.Password;

            }

            return password;
        }
    }
}