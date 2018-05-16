using ServerSite.ActionFilters;
using ServerSite.Dependency;
using Ss.Data.Models;
using Ss.Data.ModelViews;
using Ss.Data.Repository.Interfaces;
using Ss.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Unity;

namespace ServerSite.Controllers
{
    [RoutePrefix("api/sample")]
    public class SampleController : BaseController<User,UserView>
    {
        public SampleController(IUserService userService) : base(userService)
        {

        }

        [RbacAuthorize]
        public override IHttpActionResult GetAll()
        {
            try
            {
                //return Ok(this.context.UserRepository.Get());
                return Ok(_unityContainer.Resolve<IUserService>().Get());
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
