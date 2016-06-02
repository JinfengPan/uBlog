using MongoDB.Driver;
using MongoDB.Driver.GridFS;
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
        public GridFSBucket docsBucket { get; set; }

        public UBlogContext(string connectionString, string dbName)
        {
            var client = new MongoClient(connectionString);
            Database = client.GetDatabase(dbName);
            docsBucket = new GridFSBucket(Database);
        }

        public IMongoCollection<BlogPost> BlogPosts 
            => Database.GetCollection<BlogPost>("blogPosts");

        public IMongoCollection<User> Users
            => Database.GetCollection<User>("users");

    }
}
