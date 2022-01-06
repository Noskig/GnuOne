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
        public static async void ReadUnOpenEmails(MariaContext _newContext, string ConnectionString)
        {
            var myInfo = _newContext.MySettings.First();

            using (var client = new ImapClient()) //**** new ProtocolLogger("imap.log")) om vi vill logga
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
                client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect); //****
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(myInfo.Email, myInfo.Password);                                                 //vad händer om man har fel lösen?
                client.Inbox.Open(FolderAccess.ReadWrite);


                var emails = client.Inbox.Search(SearchQuery.All);

                foreach (var mail in emails)
                {
                    var message = client.Inbox.GetMessage(mail);
                    var emailFrom = message.From.ToString();
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

                                var done = RecieveAndSaveDiscussion(decryptedMessage, _newContext);

                                if (done == 1)
                                { break; }
                                else
                                {
                                    ///try again?
                                    break;
                                }

                            case "PostedPost":

                                var doing = RecieveAndSavePost(decryptedMessage, _newContext);
                                if (doing == 1)
                                { break; }
                                else
                                {
                                    ///try again?
                                    break;
                                }

                            case "DeleteDiscussion":

                                var deed = RecieveAndDeleteDiscussion(decryptedMessage, _newContext);
                                if (deed == 1)
                                { break; }
                                else
                                {
                                    ///try again?
                                    break;
                                }

                            case "DeletePost":
                                var deeding = ReceiveAndDeletePost(decryptedMessage, _newContext);
                                if (deeding == 1)
                                { break; }
                                else
                                {
                                    ///try again?
                                    break;
                                }


                            case "PutPost":
                                var deedb = ReceiveAndPutPost(decryptedMessage, _newContext);
                                if (deedb == 1)
                                { break; }
                                else
                                {
                                    ///try again?
                                    break;
                                }

                            case "PutDiscussion":
                                var deedc = RecieveAndPutDiscussion(decryptedMessage, _newContext);
                                break;

                            case "FriendRequest":
                                var deedd = RecieveFriendRequest(decryptedMessage, _newContext);
                                if (deedd == 1)
                                { break; }
                                else
                                {
                                    ///try again?
                                    break;
                                }

                            case "DeniedFriendRequest":

                                var deede = RecieveDeniedFriendRequest(decryptedMessage, _newContext);
                                if (deede == 1)
                                { break; }
                                else
                                {
                                    ///try again?
                                    break;
                                }

                            case "AcceptedFriendRequest":
                                //var deedf = await Task.Run(() => ReceieveInfoAndAcceptFriend(decryptedMessage, _newContext, false, myInfo.Email));
                                var deedf = ReceieveInfoAndAcceptFriend(decryptedMessage, _newContext, false,myInfo.Email);
                                if (deedf == 1)
                                {
                                    //await Task.Run(() => GiveBackMyInformation(_newContext, emailFrom));
                                    var returnInfo = GiveBackMyInformation(_newContext, emailFrom);
                                    break;
                                }
                                else { break; }

                            case "GiveBackInformation":
                                var deedg = ReceieveInfoAndAcceptFriend(decryptedMessage, _newContext, true,myInfo.Email);
                                if (deedg == 1) { break; }
                                else { break; }

                            case "ItsNotMeItsYou":
                                var deedh = RemoveFriendAndTheirInfoFromDb(decryptedMessage, _newContext);
                                if (deedh == 1) { break; }
                                else { break; }

                            case "FriendsFriendGotRemoved":
                                var deedi = RemoveFriendsFriend(decryptedMessage, _newContext, emailFrom);
                                if (deedi == 1) { break; }
                                else { break; }

                            default:
                                break;
                        }

                        // Flaggar meddelandet att det skall tas bort.
                        client.Inbox.AddFlags(mail, MessageFlags.Deleted, true);  //false? testa
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
        }

        private static int RemoveFriendsFriend(string decryptedMessage, MariaContext context, string fromEmail)
        {
            var friend = context.MyFriends.Where(x => x.Email == fromEmail).FirstOrDefault();

            var friendNotfriend = JsonConvert.DeserializeObject<MyFriend>(decryptedMessage);

            var removeablefriendfirend = context.MyFriendsFriends.Where(x => x.ID == friend.ID && x.Email == fromEmail && x.userName == friend.userName).FirstOrDefault();
            if (removeablefriendfirend is not null)
            {
                context.MyFriendsFriends.Remove(removeablefriendfirend);
                context.SaveChangesAsync().Wait();
                return 1;
            }
            return -1;
        }

        private static int RemoveFriendAndTheirInfoFromDb(string decryptedMessage, MariaContext context)
        {
            var theirSettings = JsonConvert.DeserializeObject<MySettings>(decryptedMessage);

            if (theirSettings is not null)
            {
                var stupidFriend = context.MyFriends.Where(x => x.Email == theirSettings.Email).FirstOrDefault();
                var theirDiscussion = context.Discussions.Where(x => x.Email == theirSettings.Email).ToList();
                context.Discussions.RemoveRange(theirDiscussion);
                context.MyFriends.Remove(stupidFriend);
                context.SaveChangesAsync().Wait();
                ///skicka ut mail till mina vänner att dom är borta
                var jsonStupidFriend = JsonConvert.SerializeObject(stupidFriend);

                var mySettings = context.MySettings.FirstOrDefault();

                foreach (var user in context.MyFriends)
                {
                    MailSender.SendObject(jsonStupidFriend, user.Email, mySettings, "FriendsFriendGotRemoved");
                }
                return 1;
            }
            return -1;
        }

        private static int GiveBackMyInformation(MariaContext context, string toEmail)
        {

            var mysettingsEmail = context.MySettings.Select(x => x.Email).Single();

            var bigListWithMyInfo = BigList.FillingBigListWithMyInfo(context, mysettingsEmail);
            var jsonBigList = JsonConvert.SerializeObject(bigListWithMyInfo);
            var mySettings = context.MySettings.FirstOrDefault();
            MailSender.SendObject(jsonBigList, toEmail, mySettings, "GiveBackInformation");

            return 1;

        }

        private static int ReceieveInfoAndAcceptFriend(string decryptedMessage, MariaContext context, bool isSendBack,string myEmail)
        {
            var theirLists = JsonConvert.DeserializeObject<BigList>(decryptedMessage);

            var email = theirLists.FromEmail;
            var username = theirLists.username;
            var friend = context.MyFriends.Where(x => x.Email == email).FirstOrDefault();


            if (theirLists is not null)
            {
                friend.isFriend = true;
                friend.userName = username.ToString();
                context.MyFriends.Update(friend);
                var theirFriends = theirLists.MyFriends;
                if (theirFriends is not null)
                {

                    var myFriendFriendsList = new List<MyFriendsFriends>();
                    foreach (var theirfriend in theirFriends)
                    {
                        if (theirfriend.Email != myEmail)
                        {
                            var bff = new MyFriendsFriends(theirfriend,friend.Email);

                            myFriendFriendsList.Add(bff);
                        }

                    }
                    context.MyFriendsFriends.AddRangeAsync(myFriendFriendsList);
                    //context.MyFriends.AddRange(theirFriends);
                }
                var theirDiscussion = theirLists.Discussions;
                if (theirDiscussion is not null)
                {
                    context.Discussions.AddRangeAsync(theirDiscussion);
                    //context.SaveChanges();
                }
                var theirPosts = theirLists.Posts;
                if (theirPosts is not null)
                {
                    context.Posts.AddRangeAsync(theirPosts);
                    //context.SaveChanges();
                }

                //if (!isSendBack)
                //{

                //}
                    context.SaveChangesAsync().Wait();
                //context.SaveChangesAsync();
                return 1;
            }
            return -1;
        }

        private static int RecieveDeniedFriendRequest(string decryptedMessage, MariaContext context)
        {
            var notNewFriend = JsonConvert.DeserializeObject<MyFriend>(decryptedMessage);

            if (notNewFriend is not null)
            {
                var myfriend = context.MyFriends.Where(x => x.Email == notNewFriend.Email).FirstOrDefault();
                context.MyFriends.Remove(myfriend);
                context.SaveChangesAsync();
                return 1;
            }
            return -1;
        }

        private static int RecieveFriendRequest(string decryptedMessage, MariaContext context)
        {
            var potentialfriend = JsonConvert.DeserializeObject<MyFriend>(decryptedMessage);

            if (potentialfriend is not null)
            {
                context.MyFriends.Add(potentialfriend);
                context.SaveChangesAsync();
                return 1;
            }
            return -1;

        }

        private static int RecieveAndPutDiscussion(string decryptedMessage, MariaContext context)
        {
            var discussion = JsonConvert.DeserializeObject<Discussion>(decryptedMessage);
            if (discussion is not null)
            {
                context.Update(discussion);
                context.SaveChangesAsync();
                return 1;
            }
            return -1;
        }

        private static int ReceiveAndPutPost(string decryptedMessage, MariaContext context)
        {
            var post = JsonConvert.DeserializeObject<Post>(decryptedMessage);
            if (post is not null)
            {
                context.Update(post);
                context.SaveChangesAsync();
                return 1;
            }
            return -1;
        }

        private static int ReceiveAndDeletePost(string decryptedMessage, MariaContext context)
        {
            var post = JsonConvert.DeserializeObject<Post>(decryptedMessage);
            if (post is not null)
            {
                context.Remove(post);
                context.SaveChangesAsync();
                return 1;
            }
            return -1;
        }

        private static int RecieveAndDeleteDiscussion(string decryptedMessage, MariaContext context)
        {
            var discussion = JsonConvert.DeserializeObject<Discussion>(decryptedMessage);
            if (discussion is not null)
            {
                context.Remove(discussion);
                context.SaveChangesAsync();
                return 1;
            }
            return -1;
        }
        private static int RecieveAndSavePost(string decryptedbody, MariaContext context)
        {
            var post = JsonConvert.DeserializeObject<Post>(decryptedbody);
            if (post is not null)
            {
                context.Posts.Add(post);
                context.SaveChangesAsync();
                return 1;
            }
            return -1;
        }

        private static int RecieveAndSaveDiscussion(string decryptedbody, MariaContext context)
        {
            ///kanske try?

            var discussion = JsonConvert.DeserializeObject<Discussion>(decryptedbody);
            if (discussion is not null)
            {
                context.Discussions.Add(discussion);
                context.SaveChangesAsync();

                return 1;
            }
            return -1;
        }
    }
}


//case "AcceptedfriendRequest":
//    var bodymessages = decryptedMessage.Split("/()/");
//    var friend = _newContext.MyFriends.Where(x => x.Email == bodymessages[0]).FirstOrDefault();
//    if (friend.isFriend == false)
//    {
//        friend.userName = bodymessages[4];
//        friend.isFriend = true;
//        _newContext.Update(friend);
//        _newContext.SaveChanges();

//        try
//        {
//            var deserializedItemsFromItems = System.Text.Json.JsonSerializer.Deserialize<List<Discussion>>(bodymessages[1]);
//            if (deserializedItemsFromItems != null)
//            {
//                foreach (Discussion x in deserializedItemsFromItems)
//                {
//                    Discussion discussion = new Discussion() { ID = x.ID, Email = x.Email, userName = x.userName, Headline = x.Headline, discussionText = x.discussionText, Date = x.Date };
//                    _newContext.Discussions.Add(discussion);
//                };
//            }
//            var deserializedItemsFromItems1 = System.Text.Json.JsonSerializer.Deserialize<List<Post>>(bodymessages[2]);
//            if (deserializedItemsFromItems1 != null)
//            {
//                foreach (Post x in deserializedItemsFromItems1)
//                {
//                    Post post = new Post() { ID = x.ID, Email = x.Email, userName = x.userName, Date = x.Date, postText = x.postText };
//                    _newContext.Posts.Add(x);
//                };
//            }
//            var deserializedItemsFromItems2 = System.Text.Json.JsonSerializer.Deserialize<List<MyFriendsFriends>>(bodymessages[3]);
//            if (deserializedItemsFromItems2 != null)
//            {
//                var myName = _newContext.MySettings.FirstOrDefault();
//                foreach (MyFriendsFriends x in deserializedItemsFromItems2)
//                {
//                    MyFriendsFriends friendsFriend = new MyFriendsFriends() { Email = x.Email, userName = x.userName, myFriendID = friend.ID };
//                    if (friendsFriend.Email != myName.Email)
//                    {
//                        _newContext.MyFriendsFriends.Add(friendsFriend);
//                    }
//                };
//            }
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine(ex.Message);
//        }

//        _newContext.SaveChanges();

//        var myData = _newContext.MySettings.FirstOrDefault();
//        var allMyDiscussions = _newContext.Discussions.Where(x => x.Email == myData.Email).ToList();
//        string myDiscussionsJson = System.Text.Json.JsonSerializer.Serialize(allMyDiscussions);
//        var allMyPosts = _newContext.Posts.Where(x => x.Email == myData.Email).ToList();
//        string myPostsJson = System.Text.Json.JsonSerializer.Serialize(allMyPosts);
//        var allMyFriends = _newContext.MyFriends.ToList();
//        string myFriendsJson = System.Text.Json.JsonSerializer.Serialize(allMyFriends);

//        //Gör specifik funktion för att skicka data till ny vän
//        MailSender.SendBackData(myData, bodymessages[0], myDiscussionsJson, myPostsJson, myFriendsJson);
//        //MailSender.SendAcceptedRequest(myData, bodymessages[0], myDiscussionsJson, myPostsJson, myFriendsJson);
//    }

//    break;


