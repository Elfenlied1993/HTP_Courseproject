using System;
using System.Collections.Generic;
using System.Text;

namespace Course.ITnews.Data.Contracts.Entities
{
    public class NewsTag
    {
        public string Id { get; set; }
        public string NewsId { get; set; }
        public News News { get; set; }
        public string TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
