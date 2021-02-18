using barberBackEnd.BLL;
using barberBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace barberBackEnd.Controllers
{
    public class QueueController : ApiController
    {
        // GET api/<controller>
        public List<ShopQueue> Get()
        {
            return  new ShopQueueSQL().GetAllQueues();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] ShopQueue sq)
        {
            new ShopQueueSQL().Add2Queue(sq);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}