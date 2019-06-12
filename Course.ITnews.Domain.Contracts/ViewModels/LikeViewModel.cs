using System;
using System.Collections.Generic;
using System.Text;

namespace Course.ITnews.Domain.Contracts.ViewModels
{
    public class LikeViewModel
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int CommentId { get; set; }
    }
}
