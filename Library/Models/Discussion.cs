using Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Bara ändra SendDiscussion Email/ID to unix.
namespace Library
{
    public class Discussion
    {
        [Key]
        public int? ID { get; set; }
        public string Email { get; set; }
        public string Headline { get; set; } 
        public string discussionText { get; set; } 
        public string userName { get; set; } 
        public DateTime Date { get; set; }
        
        public int? tagOne { get; set; }
        public int? tagTwo { get; set; }
        public int? tagThree { get; set; }
        [NotMapped]
        public int? sumOfPosts { get; set; }
        [NotMapped]
        public List<string>? tags { get; set; } = new List<string>();


        public string SendDiscussion()
        {
            var one = '"' + this.ID.ToString() + '"';
            var two = '"' + this.Email + '"';

            var thr = '"' + this.Headline + '"';

            var fou = '"' + this.discussionText + '"';
            var fiv = '"' + this.userName + '"';
            var six = '"' + this.Date.ToString() + '"';

            var comma = ",";

            string query = "INSERT into discussions (ID, Email, Headline, discussionText, userName, Date) VALUES (" + one + comma + two + comma + thr + comma + fou + comma + fiv + comma + six + ")";

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

            string query = $"UPDATE DISCUSSIONs SET DISCUSSIONTEXT={thr}, HEADLINE={two} " +
                $"WHERE ID={one} AND DISCUSSIONTEXT={oldtext} AND userName={fou}";
            //Where ID And Email?
            return query;
        }

        public string DeleteDiscussion()
        {
            var one = '"' + this.ID.ToString() + '"';
            var two = '"' + this.Headline + '"';
            var thr = '"' + this.discussionText + '"';
            var fou = '"' + this.userName + '"';
            var fiv = '"' + this.Date.ToString() + '"';

            string query = $"DELETE from DISCUSSIONs WHERE ID={one} AND Headline={two} AND DiscussionText={thr} AND UserName={fou}";

            return query;
        }

    }
}
