using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;

namespace Course.ITnews.Data.Contracts.Entities
{
    public class News
    {
        public virtual string Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string ShortDescription { get; set; }
        public virtual string FullDescription { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual User Author { get; set; }
        public virtual string AuthorId { get; set; }
        public virtual Category Category { get; set; }
        public virtual string CategoryId { get; set; }
        public virtual ICollection<NewsTag> NewsTags { get; set; }
        public virtual ICollection<Commentary> Commentaries { get; set; }
    }
}
