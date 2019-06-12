using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text;

namespace Course.ITnews.Data.Contracts.Entities
{
    public class User : IdentityUser<int>
    {
        public virtual List<News> News { get; set; }
        public virtual ICollection<Commentary> Commentaries { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Like> Likes { get; set; }

    }
}
