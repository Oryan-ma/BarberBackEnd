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
    public class CustomerController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] Customer c)
        {
            new CustomerSQL().RegisterCustomer(c);
        }
        //LOGIN
        [HttpPost]
        [Route("api/customer/login")]
        public object Login([FromBody] Customer c)
        {
            return new Login().LoginUser(c);
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