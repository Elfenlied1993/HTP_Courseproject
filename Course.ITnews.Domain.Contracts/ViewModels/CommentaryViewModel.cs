using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Course.ITnews.Domain.Contracts.ViewModels
{
    public class CommentaryViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string AuthorName { get; set; }
        [Required]
        public int? AuthorId { get; set; }
        [Required]
        public int NewsId { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public string Description { get; set; }

    }
}
