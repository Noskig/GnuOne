using Library;
using Library.HelpClasses;
using Library.Models;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;
using MailKit.Search;
using MailKit.Security;
using Newtonsoft.Json;
using System.Text.Json;

namespace GnuOne.Data
{
    public static class MailReader
    {
        public static async Task ReadUnOpenEmails(MariaContext _newContext, string ConnectionString)
        {
            var myInfo = _newContext.MySettings.First();
           
            using (var client = new ImapClient()) //**** new ProtocolLogger("imap.log")) om vi vill logga
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
                client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect); //****
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(myInfo.Email, myInfo.Password);

                client.Inbox.Open(FolderAccess.ReadWrite);
                var emails = client.Inbox.Search(SearchQuery.All);

                foreach (var mail in emails)
                {
                    var message = client.Inbox.GetMessage(mail);
                    var subject = message.Subject;
                    string body = message.GetTextBody(MimeKit.Text.TextFormat.Plain);
                    string[] splittedBody = body.Split("XYXY/(/(XYXY7");
                    string[] Sub;

                    if (subject.Contains("/()/"))
                    {
                        Sub = subject.Split("/()/");
                        string decryptedMessage = AesCryption.Decrypt(splittedBody[0], myInfo.Secret);
                        string[] Data = decryptedMessage.Split("\"");

                        switch (Sub[1])
                        {
                            case "PostedDiscussion":

                                 await RecieveAndSaveDiscussion(decryptedMessage, _newContext);
                                break;

                            case "PostedPost":

                                await RecieveAndSavePost(decryptedMessage, _newContext);
                                break;

                            case "Delete":
                                DbCommand.CreateCommand(decryptedMessage, ConnectionString);
                                break;
                            case "Put":
                                DbCommand.CreateCommand(decryptedMessage, ConnectionString);
                                break;
                            case "friendRequest":
                                var bodymessage = decryptedMessage.Split("/()/");
                                var potentialfriend = new MyFriend(bodymessage);
                                _newContext.MyFriends.AddAsync(potentialfriend);
                                break;
                            case "DeniedfriendRequest":
                                var bodymessage1 = decryptedMessage.Split("/()/");
                                var myfriend = _newContext.MyFriends.Where(x => x.Email == bodymessage1[1]).FirstOrDefault();
                                _newContext.MyFriends.Remove(myfriend);
                                break;

                            //Studsar en gång för mycket, men ingen större problem.
                            case "AcceptedfriendRequest":
                                var bodymessages = decryptedMessage.Split("/()/");
                                var friend = _newContext.MyFriends.Where(x => x.Email == bodymessages[0]).FirstOrDefault();
                                if (friend.isFriend == false)
                                {
                                    friend.userName = bodymessages[4];
                                    friend.isFriend = true;
                                    _newContext.Update(friend);
                                    _newContext.SaveChanges();

                                    try
                                    {
                                        var deserializedItemsFromItems = System.Text.Json.JsonSerializer.Deserialize<List<Discussion>>(bodymessages[1]);
                                        if (deserializedItemsFromItems != null)
                                        {
                                            foreach (Discussion x in deserializedItemsFromItems)
                                            {
                                                Discussion discussion = new Discussion() { ID = x.ID, Email = x.Email, userName = x.userName, Headline = x.Headline, discussionText = x.discussionText, Date = x.Date };
                                                _newContext.Discussions.Add(discussion);
                                            };
                                        }
                                        var deserializedItemsFromItems1 = System.Text.Json.JsonSerializer.Deserialize<List<Post>>(bodymessages[2]);
                                        if (deserializedItemsFromItems1 != null)
                                        {
                                            foreach (Post x in deserializedItemsFromItems1)
                                            {
                                                Post post = new Post() { ID = x.ID, Email = x.Email, userName = x.userName, Date = x.Date, postText = x.postText };
                                                _newContext.Posts.Add(x);
                                            };
                                        }
                                        var deserializedItemsFromItems2 = System.Text.Json.JsonSerializer.Deserialize<List<MyFriendsFriends>>(bodymessages[3]);
                                        if (deserializedItemsFromItems2 != null)
                                        {
                                            var myName = _newContext.MySettings.FirstOrDefault();
                                            foreach (MyFriendsFriends x in deserializedItemsFromItems2)
                                            {
                                                MyFriendsFriends friendsFriend = new MyFriendsFriends() { Email = x.Email, userName = x.userName, myFriendID = friend.ID };
                                                if (friendsFriend.Email != myName.Email)
                                                {
                                                    _newContext.MyFriendsFriends.Add(friendsFriend);
                                                }
                                            };
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }

                                    _newContext.SaveChanges();

                                    var myData = _newContext.MySettings.FirstOrDefault();
                                    var allMyDiscussions = _newContext.Discussions.Where(x => x.Email == myData.Email).ToList();
                                    string myDiscussionsJson = System.Text.Json.JsonSerializer.Serialize(allMyDiscussions);
                                    var allMyPosts = _newContext.Posts.Where(x => x.Email == myData.Email).ToList();
                                    string myPostsJson = System.Text.Json.JsonSerializer.Serialize(allMyPosts);
                                    var allMyFriends = _newContext.MyFriends.ToList();
                                    string myFriendsJson = System.Text.Json.JsonSerializer.Serialize(allMyFriends);

                                    //Gör specifik funktion för att skicka data till ny vän
                                    MailSender.SendBackData(myData, bodymessages[0], myDiscussionsJson, myPostsJson, myFriendsJson);
                                    //MailSender.SendAcceptedRequest(myData, bodymessages[0], myDiscussionsJson, myPostsJson, myFriendsJson);
                                }

                                break;

                            //För att läsa in den data min nya vän har skickat till mig
                            case "SendBackData":
                                var bodymessages4 = decryptedMessage.Split("/()/");
                                var newFriend = _newContext.MyFriends.Where(x => x.Email == bodymessages4[0]).FirstOrDefault();

                                try
                                {
                                    var deserializedItemsFromItems = System.Text.Json.JsonSerializer.Deserialize<List<Discussion>>(bodymessages4[1]);
                                    if (deserializedItemsFromItems != null)
                                    {
                                        foreach (Discussion x in deserializedItemsFromItems)
                                        {
                                            Discussion discussion = new Discussion() { ID = x.ID, Email = x.Email, userName = x.userName, Headline = x.Headline, discussionText = x.discussionText, Date = x.Date };
                                            _newContext.Discussions.Add(discussion);
                                        };
                                    }
                                    var deserializedItemsFromItems1 = System.Text.Json.JsonSerializer.Deserialize<List<Post>>(bodymessages4[2]);
                                    if (deserializedItemsFromItems1 != null)
                                    {
                                        foreach (Post x in deserializedItemsFromItems1)
                                        {
                                            Post post = new Post() { ID = x.ID, Email = x.Email, userName = x.userName, Date = x.Date, postText = x.postText };
                                            _newContext.Posts.Add(x);
                                        };
                                    }
                                    var deserializedItemsFromItems2 = System.Text.Json.JsonSerializer.Deserialize<List<MyFriendsFriends>>(bodymessages4[3]);
                                    if (deserializedItemsFromItems2 != null)
                                    {
                                        var myName = _newContext.MySettings.FirstOrDefault();
                                        foreach (MyFriendsFriends x in deserializedItemsFromItems2)
                                        {
                                            MyFriendsFriends friendsFriend = new MyFriendsFriends() { Email = x.Email, userName = x.userName, myFriendID = newFriend.ID };
                                            if (friendsFriend.Email != myName.Email)
                                            {
                                                _newContext.MyFriendsFriends.Add(friendsFriend);
                                            }
                                        };
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                break;

                            case "deleteFriend":
                                var bodymessage3 = decryptedMessage.Split("/()/");
                                var allDiscussions = _newContext.Discussions.Where(x => x.Email == bodymessage3[1]).ToList();
                                _newContext.RemoveRange(allDiscussions);
                                var deletemyFriend = _newContext.MyFriends.Where(y => y.Email == bodymessage3[1]).FirstOrDefault();
                                _newContext.MyFriends.Remove(deletemyFriend);
                                break;

                            default:
                                break;
                        }

                        _newContext.SaveChanges();
                        // Flaggar meddelandet att det skall tas bort.
                        client.Inbox.AddFlags(mail, MessageFlags.Deleted, true);
                    }

                    //Spammail kmr hit och tas bort
                    else
                    {
                        client.Inbox.AddFlags(mail, MessageFlags.Deleted, true);
                        continue;
                    }
                }


                client.Disconnect(true);

            }

            //for (int i = 0; i < client.Count; i++)
            //{
            //    var message = client.GetMessage(i);
            //    var subject = message.Subject;
            //    var bbody = message.GetTextBody(MimeKit.Text.TextFormat.Text);
            //    string[] splittedBody = bbody.Split("XYXY/(/(XYXY7");
            //    string[] Sub;
            //    if (subject.Contains("/()/"))
            //    {
            //        Sub = subject.Split("/()/");
            //        string decryptedMessage = AesCryption.Decrypt(splittedBody[0], myInfo.Secret);
            //        string[] Data = decryptedMessage.Split("\"");
            //        var LocalDate = _newContext.LastUpdates.First();
            //        DateTime IncomeDate = Convert.ToDateTime(Sub[0]);
            //        if (IncomeDate > LocalDate.timeSet)
            //        {
            //            switch (Sub[1])
            //            {
            //                case "Delete":
            //                    DbCommand.CreateCommand(decryptedMessage, ConnectionString);
            //                    break;
            //                case "Put":
            //                    DbCommand.CreateCommand(decryptedMessage, ConnectionString);
            //                    break;
            //                case "friendRequest":
            //                    var bodymessage = decryptedMessage.Split("/()/");
            //                    var potentialfriend = new MyFriend(bodymessage);
            //                    _newContext.MyFriends.AddAsync(potentialfriend);
            //                    break;
            //                case "DeniedfriendRequest":
            //                    var bodymessage1 = decryptedMessage.Split("/()/");
            //                    var myfriend = _newContext.MyFriends.Where(x => x.Email == bodymessage1[1]).FirstOrDefault();
            //                    _newContext.MyFriends.Remove(myfriend);
            //                    break;

            //                //Studsar en gång för mycket, men ingen större problem.
            //                //
            //                case "AcceptedfriendRequest":
            //                    var bodymessages = decryptedMessage.Split("/()/");
            //                    var friend = _newContext.MyFriends.Where(x => x.Email == bodymessages[0]).FirstOrDefault();
            //                    if (friend.isFriend == false)
            //                    {
            //                        friend.userName = bodymessages[4];
            //                        friend.isFriend = true;
            //                        _newContext.Update(friend);
            //                        _newContext.SaveChangesAsync();
            //                        try
            //                        {
            //                            var deserializedItemsFromItems = System.Text.Json.JsonSerializer.Deserialize<List<Discussion>>(bodymessages[1]);
            //                            if (deserializedItemsFromItems != null)
            //                            {
            //                                foreach (Discussion x in deserializedItemsFromItems)
            //                                {
            //                                    Discussion discussion = new Discussion() { ID = x.ID, Email = x.Email, userName = x.userName, Headline = x.Headline, discussionText = x.discussionText, Date = x.Date };
            //                                    _newContext.Discussions.Add(discussion);
            //                                };
            //                            }
            //                            var deserializedItemsFromItems1 = System.Text.Json.JsonSerializer.Deserialize<List<Post>>(bodymessages[2]);
            //                            if (deserializedItemsFromItems1 != null)
            //                            {
            //                                foreach (Post x in deserializedItemsFromItems1)
            //                                {
            //                                    Post post = new Post() { ID = x.ID, Email = x.Email, userName = x.userName, Date = x.Date, postText = x.postText };
            //                                    _newContext.Posts.Add(x);
            //                                };
            //                            }
            //                            var deserializedItemsFromItems2 = System.Text.Json.JsonSerializer.Deserialize<List<MyFriendsFriends>>(bodymessages[3]);
            //                            if (deserializedItemsFromItems2 != null)
            //                            {
            //                                var myName = _newContext.MySettings.FirstOrDefault();
            //                                foreach (MyFriendsFriends x in deserializedItemsFromItems2)
            //                                {
            //                                    MyFriendsFriends friendsFriend = new MyFriendsFriends() { Email = x.Email, userName = x.userName, myFriendID = friend.ID };
            //                                    if (friendsFriend.Email != myName.Email)
            //                                    {
            //                                        _newContext.MyFriendsFriends.Add(friendsFriend);
            //                                    }
            //                                };
            //                            }
            //                        }
            //                        catch (Exception ex)
            //                        {
            //                            Console.WriteLine(ex.Message);
            //                        }

            //                        var myData = _newContext.MySettings.FirstOrDefault();
            //                        var allMyDiscussions = _newContext.Discussions.Where(x => x.Email == myData.Email).ToList();
            //                        string myDiscussionsJson = System.Text.Json.JsonSerializer.Serialize(allMyDiscussions);
            //                        var allMyPosts = _newContext.Posts.Where(x => x.Email == myData.Email).ToList();
            //                        string myPostsJson = System.Text.Json.JsonSerializer.Serialize(allMyPosts);
            //                        var allMyFriends = _newContext.MyFriends.ToList();
            //                        string myFriendsJson = System.Text.Json.JsonSerializer.Serialize(allMyFriends);

            //                        MailSender.SendAcceptedRequest(myData, bodymessages[0], myDiscussionsJson, myPostsJson, myFriendsJson);
            //                    }

            //                    break;

            //                case "deleteFriend":
            //                    var bodymessage3 = decryptedMessage.Split("/()/");
            //                    var allDiscussions = _newContext.Discussions.Where(x => x.Email == bodymessage3[1]).ToList();
            //                    _newContext.RemoveRange(allDiscussions);
            //                    var deletemyFriend = _newContext.MyFriends.Where(y => y.Email == bodymessage3[1]).FirstOrDefault();
            //                    _newContext.MyFriends.Remove(deletemyFriend);
            //                    break;
            //                default:
            //                    DbCommand.CreateCommand(decryptedMessage, ConnectionString);
            //                    break;
            //            }
            //            LocalDate.timeSet = IncomeDate;
            //            _newContext.Update(LocalDate);
            //            _newContext.SaveChangesAsync();
            //        }
            //    }
            //    else
            //    {
            //        continue;
            //    }
        }

        private static async Task RecieveAndSavePost(string decryptedbody, MariaContext context)
        {
            var post = JsonConvert.DeserializeObject<Post>(decryptedbody);

            await context.Posts.AddAsync(post);
            await context.SaveChangesAsync();
        }

        private static async Task RecieveAndSaveDiscussion(string decryptedbody, MariaContext context)
        {
            ///kanske try?
            
            var discussion = JsonConvert.DeserializeObject<Discussion>(decryptedbody);

            await context.Discussions.AddAsync(discussion);
            await context.SaveChangesAsync();
            
        }
    }
}



