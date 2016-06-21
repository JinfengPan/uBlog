using IdentityServer3.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthServer.Config
{
    public static class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "ublogmvc",
                    ClientName="uBlog (MVC)",
                    Flow = Flows.ClientCredentials,
                    ClientSecrets = new List<Secret>()
                    {
                        new Secret ("123".Sha256() )
                    },
                    AllowAccessToAllScopes = true
                },
                new Client
                {
                    ClientId = "ublogauthcode",
                    ClientName = "uBlog (Authorization Code)",
                    Flow = Flows.AuthorizationCode,
                    AllowAccessToAllScopes = true,
                    //用来返回token或者auth code
                    RedirectUris = new List<string>
                    {
                        "localhost:8080/STSCallback"
                    },
                    ClientSecrets = new List<Secret>()
                    {
                         new Secret ("123".Sha256() )
                    }
                }
            };
        }
    }
}