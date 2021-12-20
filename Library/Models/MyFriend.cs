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
        public int ID { get; set; }
        public string? userName { get; set; }
        public string Email { get; set; }
        public bool isFriend { get; set; } = false;

        public MyFriend()
        {

        }
        public MyFriend(string[] bodymessage)
        {
            this.userName = bodymessage[0];
            this.Email = bodymessage[1];
        }
    }

}
