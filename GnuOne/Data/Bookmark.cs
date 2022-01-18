using Library;
using System.ComponentModel.DataAnnotations.Schema;

namespace GnuOne.Data
{
    public class Bookmark
    {
        public int ID { get; set; }
        public string Email { get; set; }
        [NotMapped]
        public List<Discussion>? Discussusions { get; set; }
        [NotMapped]
        public List<Post>? Posts { get; set; }


        public Bookmark()
        {

        }
        public Bookmark(bool createdwithlists)
        {
            Discussusions = new List<Discussion>();
            Posts = new List<Post>();
        }

    }
}
