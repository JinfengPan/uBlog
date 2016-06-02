using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uBlog.Repository;

namespace uBlog.Services
{
    public class BaseService
    {
        protected UBlogContext UBlogContext { get; set; }
        public BaseService(UBlogContext uBlogContext)
        {
            this.UBlogContext = UBlogContext;
        }
    }
}
