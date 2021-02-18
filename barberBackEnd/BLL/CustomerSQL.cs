using barberBackEnd.DAL;
using barberBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace barberBackEnd.BLL
{
    public class CustomerSQL
    {
        DBService db = new DBService();

        public Customer RegisterCustomer(Customer customer)
        {
            db.Insert2DB(customer);
            return customer;
        }
    }
}