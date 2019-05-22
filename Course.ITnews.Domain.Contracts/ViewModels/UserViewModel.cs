using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Course.ITnews.Domain.Contracts.ViewModels
{
    public class UserViewModel : IdentityUser
    {
        public ICollection<int> NewsIds { get; set; }
        public ICollection<int> CommentariesIds { get; set; }
        public UserViewModel()
        {
            CommentariesIds = new List<int>();
            NewsIds = new List<int>();
        }
    }
}
