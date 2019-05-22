using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Course.ITnews.Domain.Contracts.ViewModels
{
    public class NewsViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public int? AuthorId { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public string FullDescription { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public int? CategoryId { get; set; }
        public ICollection<int> CommentariesIds { get; set; }
        public ICollection<int> TagsIds { get; set; }

        public NewsViewModel()
        {
            CommentariesIds=new List<int>();
            TagsIds=new List<int>();
        }
    }
}
