using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Library
{
    public class Post
    {
        [Key]
        public int? ID { get; set; }
        public string  Email { get; set; }
        public string userName { get; set; } 
        public string postText { get; set; } 
        public DateTime Date { get; set; }
        public int discussionID { get; set; }
        public string discussionEmail { get; set; }
        [NotMapped]
        public int numberOfComments { get; set; }

        public string SendPost()
        {
            var one = '"' + this.ID.ToString() + '"';
            var two = '"' + this.Email + '"';
            var thr = '"' + this.userName + '"';
            var fou = '"' + this.postText + '"';
            var fiv = '"' + this.Date.ToString() + '"';
            var six = this.discussionID;
            var sev = '"' + this.discussionEmail + '"';

            var comma = ",";

            string query = "INSERT into POSTS (ID, Email, userName, postText, Date, discussionID, discussionEmail) VALUES (" + one + comma + two + comma + thr + comma + fou + comma + fiv + comma + six + comma + sev + ")";
            return query;
        }

        public string EditPost(string oldtext)
        {
            var one = '"' + this.ID.ToString() + '"';
            var two = '"' + this.userName + '"';
            var thr = '"' + this.postText + '"';
            var fou = '"' + this.Date.ToString() + '"';
            var fiv = this.discussionID;

            oldtext = '"' + oldtext + '"';

            string query = $"UPDATE POSTS SET postTEXT={thr} WHERE id={one} AND Username={two} AND postText={oldtext}";

            return query;
        }

        public string DeletePost()
        {
            var one = '"' + this.ID.ToString() + '"';
            var two = '"' + this.userName + '"';
            var thr = '"' + this.postText + '"';
            var fou = '"' + this.Date.ToString() + '"';
            var fiv = this.discussionID;

            string query = $"DELETE from POSTS WHERE ID={one} AND Username={two} AND postText={thr}";

            return query;
        }

    }
}
