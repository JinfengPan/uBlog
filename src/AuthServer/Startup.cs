using AuthServer.Config;
using IdentityServer3.Core.Configuration;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace AuthServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/identity", idsrvApp =>
            {
                var idServerServiceFactory = new IdentityServerServiceFactory()
                    .UseInMemoryClients(Clients.Get())
                    .UseInMemoryScopes(Scopes.Get())
                    .UseInMemoryUsers(Users.Get());

                var options = new IdentityServerOptions
                {
                    Factory = idServerServiceFactory,
                    SiteName = "uBlog Security Token Service",
                    IssuerUri = uBlog.Constants.Constants.UBlogIssuerUri,
                    PublicOrigin = uBlog.Constants.Constants.UBlogSTSOrigin,
                    SigningCertificate = LoadCertificate()
                    
                };

                idsrvApp.UseIdentityServer(options);
            });
        }

        X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                string.Format(@"{0}\certificates\ublog.pfx",
                AppDomain.CurrentDomain.BaseDirectory),"");
        }
    }
}