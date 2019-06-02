using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Course.ITnews.Domain.Contracts.ViewModels
{
    public class UserViewModel : IdentityUser
    {
        public ICollection<string> NewsIds { get; set; }
        public ICollection<string> CommentariesIds { get; set; }

    }
}
