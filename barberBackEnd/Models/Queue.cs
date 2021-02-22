using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace barberBackEnd.Models
{
    public class Queue
    {
        public string BarberId { get; set; }
        public string CustomerId { get; set; }
        public City city { get; set; }
        public DateTime date { get; set; }

    }
}