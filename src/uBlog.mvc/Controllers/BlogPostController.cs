using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using uBlog.Data;
using MongoDB.Driver;
using uBlog.IRepository;
using uBlog.Repository;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace uBlog.mvc.Controllers
{
    public class BlogPostController : Controller
    {
        private readonly IBaseRepository repository = new BaseRepository();
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var filterDefinition = Builders<BlogPost>.Filter.Empty;
            filterDefinition &= Builders<BlogPost>.Filter.Where(bp => bp.CreateTime < DateTime.Now);
            filterDefinition &= Builders<BlogPost>.Filter.Lte(bp => bp.CreateTime, DateTime.Now);

            var blogpostsRes = await repository.GetAll<BlogPost>();
            if (blogpostsRes.Success)
            {
                return View(blogpostsRes.Entities);
            }
            
            return View(null);
        }


        public async Task<IActionResult> Post()
        {
            BlogPost blogPost = new BlogPost
            {
                Title = "ASP.NET CORE",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,


            };

            await repository.AddOne<BlogPost>(blogPost);
            return RedirectToAction("Index");
            

        }
    }
}
