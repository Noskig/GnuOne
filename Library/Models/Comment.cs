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

        public string SendComments() 
        { 
            var one = '"' + this.commentid.ToString() + '"';
            var two = '"' + this.user + '"';
            var thr = '"' + this.date.ToString() + '"';
            var fou = '"' + this.comment_text + '"';
            var fiv = this.postid;
            var comma = ",";

            string query = "INSERT into COMMENTS (COMMENTID, USER, Date, COMMENT_TEXT, POSTID ) VALUES (" + one + comma + two + comma + thr + comma + fou + comma + fiv + ")";
            return query;
        }
        public string DeleteComments()
        {
            var one = '"' + this.commentid.ToString() + '"';
            var two = '"' + this.user + '"';
            var thr = '"' + this.date.ToString() + '"';
            var fou = '"' + this.comment_text + '"';
            var fiv = '"' + this.postid;

            string query = $"DELETE from COMMENTS WHERE COMMENTID={one} AND user={two} AND COMMENT_TEXT={fou}";
            return query;
        }
        public string EditComment(string oldcommenttext)
        {
            var one = '"' + this.commentid.ToString() + '"';
            var two = '"' + this.user + '"';
            var thr = '"' + this.date.ToString() + '"';
            var fou = '"' + this.comment_text + '"';
            var fiv = this.postid;

            oldcommenttext = '"' + oldcommenttext + '"';

            string query = $"UPDATE COMMENTS SET COMMENT_TEXT={fou}, date={thr} WHERE commentid={one} AND COMMENT_TEXT={oldcommenttext}";
            return query;
        }

    }
}