using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.ITnews.Web.Models
{
    public class TagModel
    {
        public string Tag { get; set; }
        public int Count { get; set; }
        public string HTMLStyle { get; set; }

        public TagModel(string val, int num)
        {
            Tag = val;
            Count = num;
            HTMLStyle = SetHTMLSize(num);
        }

        public string SetHTMLSize(int num)
        {
            string ret = "medium"; // in case things go 'over' the size, use medium.

            if (num <= 1)
            {
                ret = "x-small";
            }
            else if (num < 2)
            {
                ret = "small";
            }
            else if (num < 3)
            {
                ret = "medium";
            }
            else if (num < 4)
            {
                ret = "large";
            }
            else if (num < 5)
            {
                ret = "x-large";
            }
            else if (num < 6)
            {
                ret = "xx-large";
            }
            return ret;
        }
    }
}
