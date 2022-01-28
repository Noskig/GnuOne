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
        public int ID { get; set; }
        public string Type { get; set; }
        public bool hasBeenRead { get; set; }
        public int? infoID { get; set; }
        public string Headline { get; set; }
        public int? Counter { get; set; }
        public string? userName { get; set; }
        public string? Email { get; set; }
        public int? DiscussionID { get; set; }


        public NotificationDTO(Notification notification, Discussion? discussion)
        {
            ID = notification.ID;
            Type = "discussion";
            Headline = discussion.Headline;
            infoID = discussion.ID;
            Counter = notification.counter;
            hasBeenRead = notification.hasBeenRead;
            Email = discussion.Email;
            userName = discussion.userName;
        }
        public NotificationDTO(Notification notification, Post? post)
        {
            ID = notification.ID;
            Type = "post";
            Headline = post.postText;
            infoID = post.ID;
            Counter = notification.counter;
            hasBeenRead = notification.hasBeenRead;
            DiscussionID = post.discussionID;
            Email = post.discussionEmail;
            userName= post.userName;

        }
        public NotificationDTO(Notification notification ,MyFriend? myFriend, string sub)
        {
            ID = notification.ID;
            Type = sub;
            Headline = myFriend.Email;
            //infoID = myFriend.ID;
            userName = myFriend.userName;
            hasBeenRead = notification.hasBeenRead;
            
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

