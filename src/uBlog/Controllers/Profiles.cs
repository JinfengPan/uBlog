using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using uBlog.Data;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace uBlog.Controllers
{
    [Route("api/[controller]")]
    public class Profiles : Controller
    {


       [HttpGet]
       [Authorize]
       public async Task<IActionResult> GetAsync(string username, string password)
        {
            //获取用户
            BlogUser user = null;

            //根据用户获取用户的Profile
            Profile profile = null;

            return Ok(profile);
        }


    }
}
