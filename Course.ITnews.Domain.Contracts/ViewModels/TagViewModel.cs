using System;
using System.Collections.Generic;
using System.Text;

namespace Course.ITnews.Domain.Contracts.ViewModels
{
    public class TagViewModel
    {
        public List<int> Ids { get; set; }
        public List<string> Titles { get; set; }

        public TagViewModel()
        {
            Ids = new List<int>();
            Titles = new List<string>();
        }
    }
}
