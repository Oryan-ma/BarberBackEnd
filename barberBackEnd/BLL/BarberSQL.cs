﻿using barberBackEnd.DAL;
using barberBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace barberBackEnd.BLL
{
    public class BarberSQL
    {
        DBService db = new DBService();


        public Barber RegisterBarber(Barber barber)
        {
            db.Insert2DB(barber);
            return barber;
        }

        
    }
}