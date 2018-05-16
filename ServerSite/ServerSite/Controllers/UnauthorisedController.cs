using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ServerSite.Controllers
{
    [RoutePrefix("api/unauthorised")]
    public class UnauthorisedController : ApiController
    {

        [HttpGet]
        [ActionName("error")]
        public virtual IHttpActionResult GetAll()
        {
            try
            {
                return Ok(" Test Reject ");
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
