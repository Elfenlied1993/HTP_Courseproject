using System;
using System.Collections.Generic;
using System.Text;

namespace Course.ITnews.Data.Contracts.Entities
{
    public class Like
    {
        public virtual int Id { get; set; }
        public virtual User Author { get; set; }
        public virtual int? AuthorId { get; set; }
        public virtual Commentary Comment { get; set; }
        public virtual int? CommentId { get; set; }
    }
}