using barberBackEnd.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace barberBackEnd.Models
{
    public class Barber
    {
        public string Name { get; set; }
        public string Last_Name { get; set; }
        public char Customer_Gender { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<Service> Services { get; set; }
    }
}