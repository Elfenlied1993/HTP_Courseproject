using System;
using System.Collections.Generic;
using System.Text;

namespace Course.ITnews.Data.Contracts.Entities
{
    public class Role
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual List<User> Users { get; set; }

        public Role()
        {
            Users = new List<User>();
        }
    }
}
