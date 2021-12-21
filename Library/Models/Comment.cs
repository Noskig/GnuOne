using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library
{
    //Ändra klassen samt sendComments.
    public class Comment
    {
        [Key]
        public int? ID { get; set; }
        public string Email { get; set; }
        public string userName { get; set; } 
        public DateTime Date { get; set; }
        public string commentText { get; set; }
        public int postID { get; set; }
        public string postEmail { get; set; }

        public string SendComments() 
        { 
            var one = '"' + this.ID.ToString() + '"';
            var two = '"' + this.Email + '"';
            var thr = '"' + this.userName + '"';
            var fou = '"' + this.Date.ToString() + '"';
            var fiv = '"' + this.commentText + '"';
            var six = this.postID;
            var sev = this.postEmail;

            var comma = ",";

            string query = "INSERT into COMMENTS (ID, Email, userName, Date, commentText, postID, postEmail ) VALUES (" + one + comma + two + comma + thr + comma + fou + comma + fiv + comma + six + comma + sev + ")";
            return query;
        }
        public string DeleteComments()
        {
            var one = '"' + this.ID.ToString() + '"';
            var two = '"' + this.userName + '"';
            var thr = '"' + this.Date.ToString() + '"';
            var fou = '"' + this.commentText + '"';
            var fiv = '"' + this.postID;

            string query = $"DELETE from COMMENTS WHERE COMMENTID={one} AND user={two} AND COMMENT_TEXT={fou}";
            return query;
        }
        public string EditComment(string oldcommenttext)
        {
            var one = '"' + this.ID.ToString() + '"';
            var two = '"' + this.userName + '"';
            var thr = '"' + this.Date.ToString() + '"';
            var fou = '"' + this.commentText + '"';
            var fiv = this.postID;

            oldcommenttext = '"' + oldcommenttext + '"';

            string query = $"UPDATE COMMENTS SET COMMENT_TEXT={fou}, date={thr} WHERE commentid={one} AND COMMENT_TEXT={oldcommenttext}";
            return query;
        }

    }
}