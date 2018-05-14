using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using ServerSite.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Provider = new OptionOAuthAuthorizationServerProvider(new DataFake())
            };

            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            appBuilder.UseOAuthAuthorizationServer(OAuthServerOptions);

            appBuilder.UseOAuthBearerAuthentication(OAuthBearerOptions);

            appBuilder.UseWebApi(WebConfig.Configurations());

        }
    }
}
