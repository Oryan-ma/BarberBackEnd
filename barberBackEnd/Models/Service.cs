using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace barberBackEnd.Models
{
    public class Service
    {
        public string Barber_Email { get; set; }
        public string Service_Name { get; set; }
        public double Service_Price { get; set; }
    }
}