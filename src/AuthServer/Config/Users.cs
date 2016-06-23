using IdentityServer3.Core.Services.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthServer.Config
{
    public class Users
    {
        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser>()
            {
                new InMemoryUser
                {
                    Username = "Kevin",
                    Password = "secret",
                    Subject = "b05d3546-6ca8-4d32-b95c-77e94d705ddf"
                },
                new InMemoryUser
                {
                    Username = "Patrick",
                    Password = "secret",
                    Subject = "bb61e881-3a49-42a7-8b62-c13be102018"
                }
            };
        }
    }
}