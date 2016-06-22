using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace uBlog.mvc.Helpers
{
    public class UBlogHttpClient
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public UBlogHttpClient(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public HttpClient GetClient()
        {
            HttpClient client = new HttpClient();

            //client credentials flow
            //var accessToken = RequestAccessTokenClientCredentials();

            var accessToken = RequestAccessTokenAuthorizationCode();

            if(accessToken != null)
            {
                client.SetBearerToken(accessToken);
            }


            client.BaseAddress = new Uri(uBlog.Constants.Constants.UBlogAPI);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
                );
            return client;
        }

        private string RequestAccessTokenClientCredentials()
        {
            var cookieKey = "CCAccessTokenCookie";
            //CCAccessTokenCookie: Client Credential Access Token Cookie
            var accessToken = httpContextAccessor.HttpContext.Request.Cookies[cookieKey];

            if (!string.IsNullOrEmpty(accessToken))
            {
                return accessToken;
            }

            //create a Token Client
            //TokenClient is used to easily access the token endpoint.
            var tokenClient = new TokenClient(
                "tokenEndpoint",
                "ublogmvc",//clientId
                "123" //client secret
                );

            var tokenResponse = tokenClient.RequestClientCredentialsAsync("gallerymanagment").Result;

            //将access token写入cookie
            httpContextAccessor.HttpContext.Response.Cookies.Append(cookieKey, tokenResponse.AccessToken, new CookieOptions
            {
                Expires = DateTime.Now.AddMonths(6)
            });

            return tokenResponse.AccessToken;
        }

        private string RequestAccessTokenAuthorizationCode()
        {
            var cookieKey = "ACAccessTokenCookie";
            //ACAccessTokenCookie: Authorization Code Access Token Cookie
            var accessToken = httpContextAccessor.HttpContext.Request.Cookies[cookieKey];

            if (!string.IsNullOrEmpty(accessToken))
            {
                return accessToken;
            }

            var authorizeRequest = new IdentityModel.Client.AuthorizeRequest(
                uBlog.Constants.Constants.UBlogSTSAuthorizationEndpoint);

            // state will be delivered to the callback endpoint together with your authorization code
            var state = httpContextAccessor.HttpContext.Request.Path;

            var url = authorizeRequest.CreateAuthorizeUrl("ublogauthcode", "code", "scope",
                uBlog.Constants.Constants.UBlogMVCSTSCallback, state);

            httpContextAccessor.HttpContext.Response.Redirect(url);

            return null;
        }
    }
}
