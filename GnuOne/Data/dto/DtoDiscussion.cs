using Library;

namespace GnuOne.Data.dto
{
    public class DtoDiscussion
    {

        public int? ID { get; set; }
        public string Email { get; set; }
        public string Headline { get; set; }
        public string discussionText { get; set; }
        public string userName { get; set; }
        public DateTime Date { get; set; }

        public int? tagOne { get; set; }
        public int? tagTwo { get; set; }
        public int? tagThree { get; set; }

        public List<string>? tags { get; set; } = new List<string>();

        public int? numberOfPosts { get; set; }

        public DtoDiscussion(Discussion disc)
        {
            ID = disc.ID;
            Email = disc.Email;
            Headline = disc.Headline;
            discussionText = disc.discussionText;
            userName = disc.userName;
            Date = disc.Date;
            tagOne = disc.tagOne;
            tagTwo = disc.tagTwo;
            tagThree = disc.tagThree;
            tags = disc.tags;
        }



    }
}
