using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace Course.ITnews.Data.Contracts.Entities
{
    public class Rating
    {
        public virtual int Id { get; set; }
        public virtual News News { get; set; }
        public virtual int? NewsId { get; set; }
        public virtual User Author { get; set; }
        public virtual int? AuthorId { get; set; }
        public virtual int RatingNumber { get; set; }
    }
}
