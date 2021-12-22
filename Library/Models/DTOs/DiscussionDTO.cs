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

            this.posts = posts;
        }

    }
}
