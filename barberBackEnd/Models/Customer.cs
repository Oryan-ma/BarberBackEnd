using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace barberBackEnd.Models
{
    public class Customer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Last_Name { get; set; }
        public char Gender { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

    }
}