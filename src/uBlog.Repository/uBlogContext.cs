using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uBlog.Data;

namespace uBlog.Repository
{
    public class UBlogContext
    {
        public IMongoDatabase Database;

        public UBlogContext(string connectionString, string dbName)
        {
            var client = new MongoClient(connectionString);
            Database = client.GetDatabase(dbName);
        }

        public IMongoCollection<BlogPost> BlogPosts 
            => Database.GetCollection<BlogPost>("blogPosts");

    }
}
