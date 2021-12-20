using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library
{
    public class Comment
    {
        [Key]
        public int? ID { get; set; }
        public string userName { get; set; } 
        public DateTime Date { get; set; }
        public string commentText { get; set; }
        public int postID { get; set; }

        public string SendComments() 
        { 
            var one = '"' + this.ID.ToString() + '"';
            var two = '"' + this.userName + '"';
            var thr = '"' + this.Date.ToString() + '"';
            var fou = '"' + this.commentText + '"';
            var fiv = this.postID;
            var comma = ",";

            string query = "INSERT into COMMENTS (COMMENTID, USER, Date, COMMENT_TEXT, POSTID ) VALUES (" + one + comma + two + comma + thr + comma + fou + comma + fiv + ")";
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