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
        //public MyFriend? MyFriend { get; set; }
        public string Type { get; set; }
        public int? ID { get; set; }
        public string Headline { get; set; }
        public int? Counter { get; set; }
        public string? userName { get; set; }


        public NotificationDTO(Notification notification, Discussion? discussion)
        {
            Type = "discussion";
            Headline = discussion.Headline;
            ID = discussion.ID;
            Counter = notification.counter;
        }
        public NotificationDTO(Notification notification, Post? post)
        {
            Type = "post";
            Headline = post.postText;
            ID = post.ID;
            Counter = notification.counter;
        }
        public NotificationDTO(MyFriend? myFriend, string sub)
        {
            Type = sub;
            Headline = myFriend.Email;
            ID = myFriend.ID;
            userName = myFriend.userName;
        }
        //public NotificationDTO(Notification notification, MyFriend? myFriend)
        //{
        //    Type = "friend";
        //    Headline = notification.messageType;
        //    ID = post.ID;
        //    Counter = notification.counter;
        //}
        //public NotificationDTO(Notification notification, Post post)
        //{
        //    Post = post;
        //    postCounter = notification.counter;
        //}
        //public NotificationDTO(Notification notification, MyFriend myFriend)
        //{
        //    MyFriend = myFriend;
        //    friendMethod = notification.messageType;
        //}
    }
}

