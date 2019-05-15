using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Course.ITnews.Data.Contracts.Entities
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string Login { get; set; }
        public virtual string Password { get; set; }
        public virtual string Email { get; set; }
        public virtual int? RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
