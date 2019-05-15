using System;
using System.Collections.Generic;
using System.Text;

namespace Course.ITnews.Data.Contracts.Entities
{
    public class Tag
    {
        public virtual int Id { get; set; }
        public virtual string Title{ get; set; }
        public virtual List<User> Users { get; set; }

        public Tag()
        {
            Users = new List<User>();
        }
    }
}
