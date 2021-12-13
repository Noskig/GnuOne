using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Discussion
    {
        [Key]
        public int? discussionid { get; set; }
        public string headline { get; set; } 
        public string discussiontext { get; set; } 
        public string user { get; set; } 
        public DateTime createddate { get; set; }

        public string SendDiscussion()
        {
            var one = '"' + this.discussionid.ToString() + '"';
            var two = '"' + this.headline + '"';

            var thr = '"' + this.discussiontext + '"';
            var fou = '"' + this.user + '"';
            var fiv = '"' + this.createddate.ToString() + '"';

            var comma = ",";

            string query = "INSERT into DISCUSSION (DISCUSSIONID, HEADLINE, DISCUSSIONTEXT, USER, CREATEDDATE) VALUES (" + one + comma + two + comma + thr + comma + fou + comma + fiv + ")";


            return query;

        }

        public string EditDiscussion(string oldtext)
        {
            var one = '"' + this.discussionid.ToString() + '"';
            var two = '"' + this.headline + '"';
            var thr = '"' + this.discussiontext + '"';
            var fou = '"' + this.user + '"';
            var fiv = '"' + this.createddate.ToString() + '"';

            oldtext = '"' + oldtext + '"';

            string query = $"UPDATE DISCUSSION SET DISCUSSIONTEXT={thr}, HEADLINE={two} " +
                $"WHERE discussionid={one} AND DISCUSSIONTEXT={oldtext} AND USER={fou}";

            return query;
        }

        public string DeleteDiscussion()
        {
            var one = '"' + this.discussionid.ToString() + '"';
            var two = '"' + this.headline + '"';
            var thr = '"' + this.discussiontext + '"';
            var fou = '"' + this.user + '"';
            var fiv = '"' + this.createddate.ToString() + '"';

            string query = $"DELETE from DISCUSSION WHERE DISCUSSIONID={one} AND Headline={two} AND DiscussionText={thr} AND User={fou}";

            return query;
        }

    }
}
