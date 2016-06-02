using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uBlog.Data
{
    [BsonIgnoreExtraElements]
    public class BlogPost
    {
        public BlogPost()
        {
            this.Pros = new List<ProItem>();
            this.LatestReaders = new List<BlogReader>();
            this.Tags = new List<string>();
        }

        //主键
        [BsonRepresentation(BsonType.ObjectId)]
        public string BlogPostId { get; set; }

        //作者外键
        public string AuthorId { get; set; }

        //作者名称
        public string AuthorName { get; set; }


        public DateTime CreateTime { get; set; }

        //博客内容
        public string Detail { get; set; }

        //博客获得的赞
        public List<ProItem> Pros { get; set; }

        public List<BlogReader> LatestReaders { get; set; }

        //博客标签
        public List<string> Tags { get; set; }
        
    }

    public class ProItem
    {
        //点赞人的外键
        [BsonIgnoreIfNull]
        public string ProerId { get; set; }

        //点赞人的名称
        [BsonIgnoreIfNull]
        public string ProerName { get; set; }

        public DateTime CreateTime { get; set; }

    }

    public class BlogReader
    {
        //读者外键
        public string ReaderId { get; set; }
        //读者名称
        public string ReaderName { get; set; }
        //读者头像Id
        public string ReaderAvartarId { get; set; }
    }




}
