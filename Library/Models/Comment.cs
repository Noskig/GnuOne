using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library
{
    public class Comment
    {
        [Key]
        public int? commentid { get; set; }
        public string user { get; set; } 
        public DateTime date { get; set; }
        public string comment_text { get; set; }
        public int postid { get; set; }


    }
}