using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace barberBackEnd.Models
{
    public class ShopQueue
    {
        public string Barber_Email { get; set; }
        public string Customer_Email { get; set; }
        public DateTime time { get; set; }
    }
}