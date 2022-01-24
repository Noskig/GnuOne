
using System.ComponentModel.DataAnnotations.Schema;

namespace Library
{
    public class DiscussionDTO
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
        //[NotMapped]
       
        public List<string>? tags { get; set; } = new List<string>();
        [NotMapped]
        public int numberOfPosts { get; set; }




        public List<Post> posts { get; set; }

        public DiscussionDTO()
        {

        }
        public DiscussionDTO(Discussion discussion, List<Post> posts)
        {
            this.ID = discussion.ID;
            this.Headline = discussion.Headline;
            this.discussionText = discussion.discussionText;
            this.userName = discussion.userName;
            this.Date = discussion.Date;
            this.Email = discussion.Email;
            this.tagOne = discussion.tagOne;
            this.tagTwo = discussion.tagTwo;    
            this.tagThree = discussion.tagThree;    

            this.posts = posts;
        }



    }
}
