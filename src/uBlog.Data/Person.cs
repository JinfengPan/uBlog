using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace uBlog.Data
{
    //[BsonIgnoreExtraElements] 反序列化时，忽略document中存在但model中不存在的字段
    [BsonIgnoreExtraElements]
    public class Person
    {
        public Person()
        {
            this.Address = new List<string>();
        }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        //[BsonId]
        //public int PersonId { get; set; }

        public string FirstName { get; set; }
        public int Age { get; set; }
        public List<string> Address { get; set; }

        [BsonIgnoreIfNull]
        public Contact Contact = new Contact();

        //exclude a field or a property from being stored in a document.
        [BsonIgnore]
        public string IgnoreMe { get; set; }

        [BsonElement("New")]
        public string Old { get; set; }

        [BsonElement]
        private string Encapsulated;

        [BsonRepresentation(BsonType.Double)]
        public decimal NetWorth { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime BirthTime { get; set; }

        [BsonDateTimeOptions(DateOnly = true)]
        public DateTime BirthDate { get; set; }
    }

}
