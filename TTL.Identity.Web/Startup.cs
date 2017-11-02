using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;

[assembly: OwinStartup(typeof(TTL.Identity.Web.Startup))]

namespace TTL.Identity.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                AuthenticationType = "oidc",
                SignInAsAuthenticationType = "cookies",
                Authority = "https://localhost:44333/",
                ClientId = "mvc",
                RedirectUri = "http://localhost:49286/",
                ResponseType = "id_token",
                Scope = "openid email profile roles"
            });
        }
    }
}
