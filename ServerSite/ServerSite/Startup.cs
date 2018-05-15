using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using ServerSite.Dependency;
using Ss.Data.Repository.Interfaces;
using System;
using Unity;  //use load Resolve

namespace ServerSite
{
    public class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        public static OAuthAuthorizationServerOptions OAuthServerOptions { get; private set; }

        public void Configuration(IAppBuilder appBuilder)
        {
            OAuthServerOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                //AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                Provider = new OptionOAuthAuthorizationServerProvider(UnityConfig.Container.Resolve<IRepositoryContext>().UserRepository)
            };

            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            appBuilder.UseOAuthAuthorizationServer(OAuthServerOptions);

            appBuilder.UseOAuthBearerAuthentication(OAuthBearerOptions);

            appBuilder.UseWebApi(WebConfig.Configurations());

        }
    }
}
