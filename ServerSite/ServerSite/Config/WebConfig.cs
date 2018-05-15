using ServerSite.Dependency;
using System.Web.Http;


namespace ServerSite
{
    public class WebConfig
    {
        public static HttpConfiguration Configurations()
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
            config.DependencyResolver = new UnityDependencyResolver(UnityConfig.Container);
            config.Routes.MapHttpRoute("ApiById", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            return config;
        }
    }
}
