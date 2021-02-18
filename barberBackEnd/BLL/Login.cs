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
            string password = getPassword(type);

           // T user = db.Login(type);


            if (type is Barber)
            {
                Barber b = db.Login(type) as Barber;
                if (b.Password == password)
                {
                    b.Services = db.GetServices(b.Email);
                    return (T)Convert.ChangeType(b, typeof(T));
                }
            }
            else if (type is Customer)
            {
                Customer c = db.Login(type) as Customer;
                if (c.Password == password)
                {
                    return (T)Convert.ChangeType(c, typeof(T));
                }
            }
            return default;

        }

        private static string getPassword<T>(T type)
        {
            if (type is Barber)
            {
                Barber b = type as Barber;
                return b.Password;
            }
            else if (type is Customer)
            {
                Customer c = type as Customer;
                return c.Password;
            }
            return null;
        }
    }
}