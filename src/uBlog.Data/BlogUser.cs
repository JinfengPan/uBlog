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
    public class BlogUser
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        public string Name { get; set; }

        [BsonDateTimeOptions(DateOnly = true)]
        public DateTime BirthDay { get; set; }

        public Gender Gender { get; set; }

        //头像
        public string AvartarId { get; set; }

        public DateTime CreateTime { get; set; }
    }

    public enum Gender
    {
        Male =1,
        Female =0
    }
}
