using aspm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace aspm.Controllers
{
    public class ValuesController : ApiController
    {
        ASPMDBContext _ASPMDBContext = new ASPMDBContext();
        // GET api/values
        public IEnumerable<Banner> Get()
        {
            var hb= _ASPMDBContext.HomeBanners.ToList();
            return hb;
        }

        [HttpGet]
        [Route("api/value/getb")]
        public IEnumerable<Tcs> GetDetail()
        {
            var hb = _ASPMDBContext.TCs.ToList();
            return hb;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }


    }
}
