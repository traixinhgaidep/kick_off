using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Routing;

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

            var principal = actionContext.RequestContext.Principal as ClaimsPrincipal;
            var userName = principal.Identity.Name;

            if (!string.IsNullOrEmpty(userName))
            {
                RbacUser requestingUser = new RbacUser(userName);

                if (!requestingUser.HasPermission(requiredPermission) & !requestingUser.IsSysAdmin)
                {
                    string url = string.Format("{0}://{1}:{2}", actionContext.Request.RequestUri.Scheme, actionContext.Request.RequestUri.Host, actionContext.Request.RequestUri.Port);
                    var response = actionContext.Request.CreateResponse(HttpStatusCode.Redirect);
                    response.Headers.Location = new Uri(url + "/api/unauthorised");
                    actionContext.Response = response;
                }
            }
            else
            {
                string url = string.Format("{0}://{1}:{2}", actionContext.Request.RequestUri.Scheme, actionContext.Request.RequestUri.Host, actionContext.Request.RequestUri.Port);
                var response = actionContext.Request.CreateResponse(HttpStatusCode.Redirect);
                response.Headers.Location = new Uri(url + "/api/unauthorised");
                actionContext.Response = response;
            }

        }
    }

}
