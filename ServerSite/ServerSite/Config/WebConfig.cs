using ServerSite.Dependency;
using System.Web.Http;


namespace ServerSite
{
    public class WebConfig
    {
        public static HttpConfiguration Configurations()
        {
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.DependencyResolver = new UnityDependencyResolver(UnityConfig.Container);
            config.Routes.MapHttpRoute("ApiById", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            return config;
        }
    }
}
