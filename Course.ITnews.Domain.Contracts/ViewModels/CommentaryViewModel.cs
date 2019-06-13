using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Course.ITnews.Domain.Contracts.ViewModels
{
    public class CommentaryViewModel
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public int AuthorId { get; set; }
        public int NewsId { get; set; }
        public DateTime Created { get; set; }
        [Required]
        public string Description { get; set; }
        public List<LikeViewModel> Likes { get; set; }

    }
}
