using System;
using System.Collections.Generic;
using System.Text;

namespace Course.ITnews.Data.Contracts.Entities
{
    public class Tag
    {
        public virtual string Id { get; set; }
        public virtual string Title{ get; set; }
        public virtual ICollection<NewsTag> NewsTags { get; set; }
    }
}
