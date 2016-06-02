using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uBlog.Data
{
    public class Comment
    {
        public Commenter Commenter { get; set; }

        //评论内容
        public string Detail { get; set; }

        public DateTime CreateDate { get; set; }
    }

    public class Commenter
    {
        public string CommenterId { get; set; }
        public string CommenterName { get; set; }
        public string CommenterAvatarId { get; set; }


    }
}
