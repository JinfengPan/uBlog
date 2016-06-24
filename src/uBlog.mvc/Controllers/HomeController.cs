using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using uBlog.mvc.Helpers;
using System.Net.Http;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace uBlog.mvc.Controllers
{
    public class HomeController : Controller
    {
        private UBlogHttpClient uBlogClientHelper;
        private HttpClient uBlogClient;
      

        public HomeController(UBlogHttpClient uBlogClient) {
            this.uBlogClientHelper = uBlogClient;
            this.uBlogClient = uBlogClient.GetClient();
        }

        public async Task<IActionResult> Index()
        {
            var uri = uBlog.Constants.Constants.UBlogAPI + "claims";
            var response = await uBlogClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                return Ok(response.Content);
            }
            return null;
        }
    }
}
