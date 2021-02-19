using barberBackEnd.DAL;
using barberBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace barberBackEnd.BLL
{
    public class ShopQueueSQL
    {
        DBService db = new DBService();
        public List<ShopQueue> GetAllQueues()
        {
            return db.GetShopQueue();
        }
        public void Add2Queue(ShopQueue sq)
        {
            db.Insert2DB(sq);
        }
        public void RemoveFromQueue(ShopQueue sq)
        {
            db.RemoveFromQueue(sq);
        }
    }
}