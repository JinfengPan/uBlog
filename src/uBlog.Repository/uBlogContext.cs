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

        public UBlogContext()
        {
            var client = new MongoClient("mongodb://139.196.59.203");
            Database = client.GetDatabase("ublog");
        }


        public IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            return Database.GetCollection<TEntity>(typeof(TEntity).Name.ToLower() + "s");
        }

    }
}
