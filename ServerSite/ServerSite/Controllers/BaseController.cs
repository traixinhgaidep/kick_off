using Ss.Data.Models;
using Ss.Data.ModelViews;
using Ss.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ServerSite.Controllers
{
    public abstract class BaseController<TModel, TView> : ApiController
       where TModel : BaseEntity, new()
       where TView : BaseViewEntity, new()
    {
        protected IService<TModel, TView> _service { get; set; }
        protected BaseController(IService<TModel, TView> service)
        {
            _service = service;
        }

        [HttpGet]
        [ActionName("getall")]
        public IHttpActionResult GetAll()
        {
            try
            {
                return Ok(_service.Get());
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
