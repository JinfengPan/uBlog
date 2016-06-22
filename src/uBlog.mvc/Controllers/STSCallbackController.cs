using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityModel.Client;
using uBlog.Constants;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace uBlog.mvc.Controllers
{
    public class STSCallbackController : Controller
    {
        public async Task<IActionResult> Index()
        {
            //get the authorization code from the query string
            var authCode = Request.Query["code"];

            //with the auth code, we can request an access token.

            var client = new TokenClient(
                Constants.Constants.UBlogSTSTokenEndpoint,
                "ublogauthcode",
                Constants.Constants.UBlogClientSecret);

            var tokenResponse = await client.RequestAuthorizationCodeAsync(
                authCode,
                Constants.Constants.UBlogMVCSTSCallback);

            Response.Cookies.Append("ACAccessTokenCookie", tokenResponse.AccessToken);
            
            //redirect to the URI saved in state
            return Redirect(Request.Query["state"]);
        }
    }
}
