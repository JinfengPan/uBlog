using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using uBlog.Repository;
using MongoDB.Bson;
using MongoDB.Driver;
using uBlog.Data;
using MongoDB.Driver.Linq;
using System.Collections;
using MongoDB.Bson.IO;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace uBlog.Controllers
{
    public class DemoController : Controller
    {
        private UBlogContext blogContext;
        public DemoController(UBlogContext blogContext)
        {
            this.blogContext = blogContext;
        }


        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var filterDefinition = Builders<BlogPost>.Filter.Empty;

            
            filterDefinition &= Builders<BlogPost>.Filter.Where(bp => bp.CreateTime < DateTime.Now);

            filterDefinition &= Builders<BlogPost>.Filter.Lte(bp => bp.CreateTime, DateTime.Now);


            var blogPosts = await blogContext.BlogPosts
                 .Find(filterDefinition)
                 .Project(bp => new {
                     Tags = bp.Tags
                 })
                 .SortBy(bp => bp.CreateTime)
                 .ThenByDescending(bp => bp.CreateTime)
                 .ToListAsync();

            return View(blogPosts);
        }

        [HttpPost]
        public async Task<IActionResult> Post(BlogPost blogPost)
        {
            await blogContext.BlogPosts.InsertOneAsync(blogPost);

            return RedirectToAction("Index");
        }

        private BlogPost GetBlogPost(string id)
        {
            var blogPost = blogContext.BlogPosts
                .Find(bp => bp.BlogPostId == id)
                .FirstOrDefault();
            return blogPost;
        }

        [HttpPost]
        public IActionResult UpdateViaReplacement(string id)
        {
            var blogPost = GetBlogPost(id);

            blogContext.BlogPosts.ReplaceOne(bp => bp.BlogPostId == id, blogPost);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateViaModification(string id)
        {
            var blogPost = GetBlogPost(id);

            var modificationUpdate = Builders<BlogPost>.Update
                .Push(bp => bp.Pros, new ProItem
                {
                    ProerId = "gag",
                    ProerName = "Lisa",
                    CreateTime = DateTime.Now
                })
                .Inc(bp => bp.ProsCount, 1);

            blogContext.BlogPosts.UpdateOne(bp => bp.BlogPostId == id, modificationUpdate);
            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Upsert(string id)
        {
            var blogPost = GetBlogPost(id);

            UpdateOptions options = new UpdateOptions
            {
                IsUpsert = true
            };

            await blogContext.BlogPosts.ReplaceOneAsync<BlogPost>(bp => bp.BlogPostId == id, blogPost, options);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string id)
        {
            await blogContext.BlogPosts.DeleteOneAsync<BlogPost>(bp => bp.BlogPostId == id);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 使用linq查询
        /// </summary>
        /// <returns></returns>
        private IMongoQueryable<BlogPost> FilterBlogPosts()
        {
            IMongoQueryable<BlogPost> blogPosts = blogContext.BlogPosts.AsQueryable();

            blogPosts = blogPosts.Where(bp => bp.CreateTime < DateTime.Now);

            return blogPosts;
        }

        public IEnumerable RunAggregationFluent(IMongoCollection<BlogPost> blogPosts)
        {
            var distributions = blogPosts.Aggregate()
                .Project(bp => new { bp.BlogPostId, TimeRange = bp.CreateTime.Month })
                .Group(bp => bp.TimeRange, g => new {GroupTimeRange = g.Key, Count= g.Count() })
                .SortBy(bp => bp.GroupTimeRange)
                .ToList();
            return distributions;
        }

        public IEnumerable RunAggregationLinq(IMongoCollection<BlogPost> blogPosts)
        {
            var distributions = blogPosts.AsQueryable()
                .Select(bp => new { TimeRange = bp.CreateTime.Month })
                .GroupBy(bp => bp.TimeRange)
                .Select(g => new { GroupTimeRange = g.Key, Count = g.Count() })
                .OrderBy(bp => bp.GroupTimeRange)
                .ToList();
            return distributions;
        }

        public IActionResult JoinWithLookup()
        {
            var report = blogContext.BlogPosts
                .Aggregate()
                .Lookup<BlogPost, BlogUser, BsonDocument>(
                    blogContext.Users,
                    bp => bp.AuthorId,
                    u => u.UserId,
                    d => d["user"]
                )
                .ToList();

            return Content(report.ToJson(new JsonWriterSettings { OutputMode = JsonOutputMode.Strict }));
        }
    }
}
