using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uBlog.IServices;
using uBlog.Repository;

namespace uBlog.Services
{
    public class BlogPostService : BaseService, IBlogPostService
    {
        public BlogPostService(UBlogContext uBlogContext): base(uBlogContext)
        {
            
        }
    }
}
