using System;
using System.Collections.Generic;
using System.Text;

namespace Course.ITnews.Data.Contracts.Entities
{
    public class NewsTag
    {
        public string NewsId { get; set; }
        public News News { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
