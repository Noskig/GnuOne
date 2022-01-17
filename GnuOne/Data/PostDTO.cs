using Library;

namespace GnuOne.Data
{
    public class PostDTO
    {
        public int? ID { get; set; }
        public string Email { get; set; }
        public string userName { get; set; }
        public string postText { get; set; }
        public DateTime Date { get; set; }
        public int discussionID { get; set; }
        public string discussionEmail { get; set; }
        public List<Comment> comments { get; set; }

        public PostDTO()
        {

        }

        public PostDTO(Post post, List<Comment> comments)
        {
            this.ID = post.ID;
            this.Email = post.Email;
            this.userName = post.userName;
            this.postText = post.postText;
            this.Date = post.Date;
            this.discussionID = post.discussionID;
            this.discussionEmail = post.discussionEmail;
            this.comments = comments;
        }
    }
}
