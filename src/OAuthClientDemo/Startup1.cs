using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.OpenIdConnect;
using System.Security.Claims;
using Microsoft.Owin.Security;
using Microsoft.IdentityModel.Protocols;

[assembly: OwinStartup(typeof(OAuthClientDemo.Startup1))]

namespace OAuthClientDemo
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888

            JwtSecurityTokenHandler.InboundAlgorithmMap =
                new Dictionary<string, string>();

            app.UseCookieAuthentication(new CookieAuthenticationOptions {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = "socialnetwork_implicit",
                Authority="http://localhost:22710",
                RedirectUri = "http://localhost:28037",
                ResponseType = "token id_token",
                Scope = "openid profile",
                PostLogoutRedirectUri = "http://localhost:28037",
                SignInAsAuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                Notifications = new OpenIdConnectAuthenticationNotifications {
                    //将id_token和access_token写入到claim
                    SecurityTokenValidated = notification => {
                        var identity = notification.AuthenticationTicket.Identity;

                        identity.AddClaim(new Claim("id_token", 
                            notification.ProtocolMessage.IdToken));
                        identity.AddClaim(new Claim("access_token",
                            notification.ProtocolMessage.AccessToken));

                        notification.AuthenticationTicket =
                        new AuthenticationTicket(identity, notification.AuthenticationTicket.Properties);
                        return Task.FromResult(0);
                    },
                    RedirectToIdentityProvider = notification => {
                        //在resource owner登出的时候，将claim中的id_token赋值到IdTokenHint
                        if(notification.ProtocolMessage.RequestType != OpenIdConnectRequestType.LogoutRequest) {
                            return Task.FromResult(0);
                        }
                        notification.ProtocolMessage.IdTokenHint = 
                        notification.OwinContext.Authentication.User.FindFirst("id_token")
                            .Value;

                        return Task.FromResult(0);
                    }
                }
            });
                    
        }
    }
}
