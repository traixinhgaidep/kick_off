using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace ServerSite.ActionFilters
{
    public class RbacAuthorizeAttribute : AuthorizeAttribute
    {

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //TODO implement check authorize
            base.OnAuthorization(actionContext);

            //Create permission string based on the requested controller name and action name in the format 'controllername-action'
            string requiredPermission = String.Format("{0}-{1}", actionContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower(), actionContext.ActionDescriptor.ActionName.ToLower());



            //string url =  string.Format("{0}://{1}:{2}", actionContext.Request.RequestUri.Scheme, actionContext.Request.RequestUri.Host, actionContext.Request.RequestUri.Port);
            //var response = actionContext.Request.CreateResponse(HttpStatusCode.Redirect);
            //response.Headers.Location = new Uri(url+ "/api/unauthorised");
            //actionContext.Response = response;
        }
    }
}
