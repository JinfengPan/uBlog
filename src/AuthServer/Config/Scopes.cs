using IdentityServer3.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthServer.Config
{
    public class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            return new List<Scope>
            {
                new Scope
                {
                    Name = "blogmanagement",
                    DisplayName = "Blog Management",
                    Description = "Allow the application to manage blogs on your behalf.",
                    Type = ScopeType.Resource

                }
            };
        }
    }
}