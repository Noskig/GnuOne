using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class MyFriendsFriends
    {
        public int ID { get; set; }
        public string myFriendEmail { get; set; }
        public string userName { get; set; }
        public string Email { get; set; }

        public MyFriendsFriends()
        {

        }        
        public MyFriendsFriends(MyFriend mf,string closeFriendEmail)
        {
            //myFriendID = mf.ID;
            myFriendEmail = closeFriendEmail;
            userName = mf.userName;
            Email = mf.Email;
        }
        public MyFriendsFriends(string[] bodymessage)
        {
            this.userName = bodymessage[0];
            this.Email = bodymessage[1];
        }
    }
}
