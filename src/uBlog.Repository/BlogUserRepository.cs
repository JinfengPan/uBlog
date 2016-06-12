using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uBlog.Data;

namespace uBlog.Repository
{
    public class BlogUserRepository
    {

        public async Task<BlogUser> GetAsync(string userName)
        {
            var taskFactory = new TaskFactory<BlogUser>();

            return await taskFactory.StartNew(() =>
            {
                return new BlogUser();
            });
        }
    }
}
