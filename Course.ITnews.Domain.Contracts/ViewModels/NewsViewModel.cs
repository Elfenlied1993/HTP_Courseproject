using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Course.ITnews.Domain.Contracts.ViewModels
{
    public class NewsViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Author { get; set; }
        public int AuthorId { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public string FullDescription { get; set; }
        public DateTime Created { get; set; }
        public string Category { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public ICollection<int> CommentariesIds { get; set; }
        public ICollection<int> TagsIds { get; set; }
        public List<SelectListItem> Commentaries { get; set; }
        public List<SelectListItem> Tags { get; set; }
        public ICollection<string> TagsTitles { get; set; }
        public List<SelectListItem> Categories { get; set; }
    }
}
