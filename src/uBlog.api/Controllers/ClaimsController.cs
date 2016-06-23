using Microsoft.AspNet.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace uBlog.api.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class ClaimsController : ControllerBase
    {
        public IActionResult Get()
        {
            return new JsonResult(new {Name = "patrick", Age="33" });
        }
    }
}
