using Library;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace GnuOne.Data
{
    public class BigList
    {
        public List<Discussion>? Discussions { get; set; }
        public List<Post>? Posts { get; set; }
        public List<MyFriend>? MyFriends { get; set; }
        public string FromEmail { get; set; }
        public string username { get; set;}
        public MyFriend? myInfo { get; set; }


        public BigList(List<Discussion>? discussions, List<Post>? posts, List<MyFriend>? myFriends, string email, string myUserName, MyFriend myInformation)
        {
            Discussions = discussions;
            Posts = posts;
            MyFriends = myFriends;
            FromEmail = email;
            username = myUserName;
            myInfo = myInformation; 
        }


        public static BigList FillingBigListWithMyInfo(ApiContext _context, string myEmail, bool isApi, myProfile profile)
        {
            var myDiscussions = _context.Discussions.Where(x => x.Email == myEmail).ToList();
            var allRelevantPosts = new List<Post>();

            //fyller listan med rätt posts
            foreach (var discussion in myDiscussions)
            {
                var posts = _context.Posts.Where(x => x.discussionID == discussion.ID).ToList();
                foreach (var post in posts)
                {
                    allRelevantPosts.Add(post);
                }
            }
            var myFriends = _context.MyFriends.Where(x=> x.isFriend == true).ToList();


            var sendUsername = _context.MySettings.Select(x => x.userName).Single();


            
            var myInfoFriend = new MyFriend(profile);


            var manylists = new BigList(myDiscussions, allRelevantPosts, myFriends, myEmail, sendUsername,myInfoFriend);
            return manylists;
        }


        public static BigList FillingBigListWithMyInfo(MariaContext _context, string myEmail, bool isApi, myProfile profile)
        {
            var myDiscussions = _context.Discussions.Where(x => x.Email == myEmail).ToList();
            var allRelevantPosts = new List<Post>();

            //fyller listan med rätt posts
            foreach (var discussion in myDiscussions)
            {
                var posts = _context.Posts.Where(x => x.discussionID == discussion.ID).ToList();
                foreach (var post in posts)
                {
                    allRelevantPosts.Add(post);
                }
            }
            var myFriends = _context.MyFriends.Where(x => x.isFriend == true).ToList();

            var sendUsername = _context.MySettings.Select(x => x.userName).Single();



            var myInfoFriend = new MyFriend(profile);


            var manylists = new BigList(myDiscussions, allRelevantPosts, myFriends, myEmail, sendUsername, myInfoFriend);
            return manylists;
        }

    }
}
