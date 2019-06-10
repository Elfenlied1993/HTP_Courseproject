using System;
using System.Collections.Generic;
using System.Text;

namespace Course.ITnews.Domain.Contracts.ViewModels
{
    public class RatingViewModel
    {
        public int Id { get; set; }
        public int NewsId { get; set; }
        public int UserId { get; set; }
        public int RatingNumber { get; set; }
    }
}
