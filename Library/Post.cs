using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Post
    {
        public int? postid { get; set; }
        public string User { get; set; } 
        public string Text { get; set; } 
        public DateTime DateTime { get; set; }
        public int discussionid { get; set; }

    }
}
