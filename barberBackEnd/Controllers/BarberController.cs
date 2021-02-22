using barberBackEnd.BLL;
using barberBackEnd.DAL;
using barberBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace barberBackEnd.Controllers
{
    public class BarberController : ApiController
    {
        // GET api/<controller>
        public List<Barber> Get()
        {
            return new BarberSQL().GetAllBarbers();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public Barber Post(Barber barber)
        {
            return new BarberSQL().RegisterBarber(barber);
        }
        //LOGIN
        [HttpPost]
        [Route("api/barber/login")]
        public object Login([FromBody] Barber b)
        {
            return new Login().LoginUser(b);
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