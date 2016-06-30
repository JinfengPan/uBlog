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
            this.LatestReaders = new List<BlogReader>();
            this.Tags = new List<string>();
        }

        /// <summary>
        /// 主键
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// 作者外键
        /// </summary>
        public string AuthorId { get; set; }

        /// <summary>
        /// 作者名称
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 博客标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 博客内容
        /// </summary>
        public string Detail { get; set; }


        /// <summary>
        /// 博客获得的赞的数目
        /// </summary>
        public int ProsCount { get; set; }

        /// <summary>
        /// 最近的读者
        /// </summary>
        public List<BlogReader> LatestReaders { get; set; }

        //博客标签
        public List<string> Tags { get; set; }
        
    }

    


    /// <summary>
    /// 点赞
    /// </summary>
    public class ProItem
    {


        //点赞人的外键
        [BsonIgnoreIfNull]
        public string ProerId { get; set; }

        //点赞人的名称
        [BsonIgnoreIfNull]
        public string ProerName { get; set; }

        //点赞时间
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
