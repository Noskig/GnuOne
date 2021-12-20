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
        public int? ID { get; set; }
        public string Headline { get; set; } 
        public string discussionText { get; set; } 
        public string userName { get; set; } 
        public DateTime Date { get; set; }

        public string SendDiscussion()
        {
            var one = '"' + this.ID.ToString() + '"';
            var two = '"' + this.Headline + '"';

            var thr = '"' + this.discussionText + '"';
            var fou = '"' + this.userName + '"';
            var fiv = '"' + this.Date.ToString() + '"';

            var comma = ",";

            string query = "INSERT into DISCUSSION (DISCUSSIONID, HEADLINE, DISCUSSIONTEXT, USER, CREATEDDATE) VALUES (" + one + comma + two + comma + thr + comma + fou + comma + fiv + ")";


            return query;

        }

        public string EditDiscussion(string oldtext)
        {
            var one = '"' + this.ID.ToString() + '"';
            var two = '"' + this.Headline + '"';
            var thr = '"' + this.discussionText + '"';
            var fou = '"' + this.userName + '"';
            var fiv = '"' + this.Date.ToString() + '"';

            oldtext = '"' + oldtext + '"';

            string query = $"UPDATE DISCUSSION SET DISCUSSIONTEXT={thr}, HEADLINE={two} " +
                $"WHERE discussionid={one} AND DISCUSSIONTEXT={oldtext} AND USER={fou}";

            return query;
        }

        public string DeleteDiscussion()
        {
            var one = '"' + this.ID.ToString() + '"';
            var two = '"' + this.Headline + '"';
            var thr = '"' + this.discussionText + '"';
            var fou = '"' + this.userName + '"';
            var fiv = '"' + this.Date.ToString() + '"';

            string query = $"DELETE from DISCUSSION WHERE DISCUSSIONID={one} AND Headline={two} AND DiscussionText={thr} AND User={fou}";

            return query;
        }

    }
}
