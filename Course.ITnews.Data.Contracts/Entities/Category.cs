using System;
using System.Collections.Generic;
using System.Text;

namespace Course.ITnews.Data.Contracts.Entities
{
    public class Category
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual List<News> News { get; set; }

    }
}
