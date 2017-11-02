using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Serilog;
using IdentityServer3.Core.Configuration;
using IdentityModel;
using System.Linq;
using System.Collections;
using IdentityServer3.Core.Services.InMemory;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer3.Core.Models;

[assembly: OwinStartup(typeof(TTL.Identity.Server.Startup))]

namespace TTL.Identity.Server
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.ColoredConsole()
                .CreateLogger();

            var cert = X509.LocalMachine
                .My
                .SubjectDistinguishedName
                .Find("CN=DESKTOP-A9I8TDP")
                .First();


            var factory = new IdentityServerServiceFactory();
            factory.UseInMemoryClients(CreateClients());
            factory.UseInMemoryScopes(CreateScopes());
            factory.UseInMemoryUsers(CreateUsers());

            app.UseIdentityServer(new IdentityServerOptions
            {
                SiteName = "Test IS",
                SigningCertificate = cert,
                Factory = factory
            });
        }

        private List<InMemoryUser> CreateUsers()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Username = "test",
                    Password = "test",
                    Subject =Guid.NewGuid().ToString(),
                    Claims = new List<Claim>
                    {
                        new Claim("name", "Test"),
                        new Claim("email", "test@test.com"),
                        new Claim("role", "admin")
                    }
                }
            };
        }

        private ICollection<Client> CreateClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "mvc demo",
                    Flow = Flows.Implicit,
                    RedirectUris = new List<string>
                    {
                        "http://localhost:49286/"
                    },
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "email",
                        "profile",
                        "roles"
                    }
                }
            };
        }

        private ICollection<Scope> CreateScopes()
        {
            return new List<Scope>
            {
                StandardScopes.OpenId,
                StandardScopes.ProfileAlwaysInclude,
                StandardScopes.EmailAlwaysInclude,
                new Scope
                {
                    Name = "roles",
                    Type = ScopeType.Identity,
                    DisplayName = "Role",
                    Claims = new List<ScopeClaim>
                    {
                        new ScopeClaim("role")
                    }
                }
            };
        }
    }
}
