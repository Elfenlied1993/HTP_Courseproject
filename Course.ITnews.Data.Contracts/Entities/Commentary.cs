using System;
using System.Collections.Generic;
using System.Text;

namespace Course.ITnews.Data.Contracts.Entities
{
    public class Commentary
    {
        public virtual string Id { get; set; }
        public virtual string Title { get; set; }
        public virtual User Author { get; set; }
        public virtual string AuthorId { get; set; }
        public virtual News News { get; set; }
        public virtual int? NewsId { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual string Description { get; set; }
    }
}
