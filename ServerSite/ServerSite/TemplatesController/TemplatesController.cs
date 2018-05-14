using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ServerSite
{
    [RoutePrefix("api/templates")]
    public class TemplatesController : ApiController
    {
        protected DataFake _data;
        public TemplatesController(DataFake data)
        {
            _data = data;
        }
        // GET api/templates 
        public IEnumerable<User> Get()
        {
            return _data.GetDataUser();
        }

        // GET api/templates/5 
        public string Get(int id)
        {
            return "value";
        }

        // POST api/templates 
        public void Post([FromBody]string value)
        {
        }

        // PUT api/templates/5 
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/templates/5 
        public void Delete(int id)
        {
        }
    }
}
