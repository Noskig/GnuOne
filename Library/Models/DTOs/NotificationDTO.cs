using GnuOne.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.DTOs
{
    public class NotificationDTO
    {
        public MyFriend? MyFriend { get; set; }
        public string? friendMethod { get; set; }
        public Discussion? Discussion { get; set; }
        public int? discussionCounter { get; set; }
        public Post? Post { get; set; }
        public int? postCounter { get; set; }

        public NotificationDTO(Notification notification, Discussion discussion)
        {
            Discussion = discussion;
            discussionCounter = notification.counter;
        }
        public NotificationDTO(Notification notification, Post post)
        {
            Post = post;
            postCounter = notification.counter;
        }
        public NotificationDTO(Notification notification, MyFriend myFriend)
        {
            MyFriend = myFriend;
            friendMethod = notification.messageType;
        }
    }
}
