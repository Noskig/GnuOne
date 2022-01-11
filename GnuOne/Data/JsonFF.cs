using Library;
using Library.Models;

namespace GnuOne.Data
{
    public class JsonFF
    {
        public MyFriend MyFriend { get; set; }
        public List<MyFriendsFriends>? MyFriendsFriends { get; set; }

        public JsonFF(MyFriend myFriend, List<MyFriendsFriends> myFriendsFriends)
        {
            MyFriend = myFriend;
            MyFriendsFriends = myFriendsFriends;
        }
    }
}
