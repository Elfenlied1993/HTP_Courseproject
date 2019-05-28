using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Course.ITnews.Domain.Contracts.ViewModels
{
    public class NewsViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string AuthorId { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public string FullDescription { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string CategoryId { get; set; }
        public ICollection<string> CommentariesIds { get; set; }
        public ICollection<string> TagsIds { get; set; }

        public NewsViewModel()
        {
            CommentariesIds=new List<string>();
            TagsIds=new List<string>();
        }
    }
}
