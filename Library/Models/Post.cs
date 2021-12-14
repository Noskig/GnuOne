using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Post
    {
        [Key]
        public int? postid { get; set; }
        public string User { get; set; } 
        public string Text { get; set; } 
        public DateTime DateTime { get; set; }
        public int discussionid { get; set; }

        public string SendPost()
        {
            var one = '"' + this.postid.ToString() + '"';
            var two = '"' + this.User + '"';
            var thr = '"' + this.Text + '"';
            var fou = '"' + this.DateTime.ToString() + '"';
            var fiv = this.discussionid;

            var comma = ",";

            string query = "INSERT into POSTS (POSTID, User, Text, DateTime, discussionid) VALUES (" + one + comma + two + comma + thr + comma + fou + comma + fiv + ")";
            return query;

            
        }

        public string EditPost(string oldtext)
        {
            var one = '"' + this.postid.ToString() + '"';
            var two = '"' + this.User + '"';
            var thr = '"' + this.Text + '"';
            var fou = '"' + this.DateTime.ToString() + '"';
            var fiv = this.discussionid;

            oldtext = '"' + oldtext + '"';

            string query = $"UPDATE POSTS SET TEXT={thr} WHERE postid={one} AND User={two} AND Text={oldtext}";

            return query;
        }

        public string DeletePost()
        {
            var one = '"' + this.postid.ToString() + '"';
            var two = '"' + this.User + '"';
            var thr = '"' + this.Text + '"';
            var fou = '"' + this.DateTime.ToString() + '"';
            var fiv = this.discussionid;

            string query = $"DELETE from POSTS WHERE POSTID={one} AND User={two} AND Text={thr}";

            return query;

        }

    }
}
