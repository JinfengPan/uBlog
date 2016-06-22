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

            var accessToken = RequestAccessTokenClientCredentials();
            client.SetBearerToken(accessToken);

            client.BaseAddress = new Uri("localhost:8081/api");
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

        private static string RequestAccessTokenAuthorizationCode()
        {
            var cookieKey = "CCAccessTokenCookie";
            //CCAccessTokenCookie: Client Credential Access Token Cookie
            var accessToken = httpContextAccessor.HttpContext.Request.Cookies[cookieKey];

            if (!string.IsNullOrEmpty(accessToken))
            {
                return accessToken;
            }

            var authorizeRequest = new IdentityModel.Client.AuthorizeRequest();
        }
    }
}
