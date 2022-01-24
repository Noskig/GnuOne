using Library;

namespace GnuOne.Data.Models
{
    public class FriendDto
    {
        public string? userName { get; set; }
        public string Email { get; set; }
        public string pubKey { get; set; }
        public int pictureID { get; set; }

        public Message lastmessage { get; set; }

        public FriendDto(MyFriend friend, Message message)
        {
            userName = friend.userName;
            Email = friend.Email;
            pubKey = friend.pubKey;
            pictureID = friend.pictureID;

            lastmessage = message;
        }
    }

}
