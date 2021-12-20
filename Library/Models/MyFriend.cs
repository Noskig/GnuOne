using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class MyFriend
    {
        [Key]
        public int userid { get; set; }
        public string? username { get; set; }
        public string Email { get; set; }
        public bool IsFriend { get; set; } = false;

        public MyFriend()
        {

        }
        public MyFriend(string[] bodymessage)
        {
            this.username = bodymessage[0];
            this.Email = bodymessage[1];
        }
    }

}
