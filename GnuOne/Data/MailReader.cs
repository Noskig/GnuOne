using Library;
using Library.HelpClasses;
using Library.Models;
using MailKit.Net.Pop3;
using Newtonsoft.Json;
using System.Text.Json;

namespace GnuOne.Data
{
    public static class MailReader
    {

        public static void ReadUnOpenEmails(MariaContext _newContext, string ConnectionString)
        {
            var me = _newContext.MySettings.First();
            using (var client = new Pop3Client())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
                client.Connect("pop.gmail.com", 995, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(me.Email, me.Password);
                for (int i = 0; i < client.Count; i++)
                {
                    var message = client.GetMessage(i);
                    var subjet = message.Subject;
                    var body = message.GetTextBody(MimeKit.Text.TextFormat.Text);
                    string[] relativData = body.Split("XYXY/(/(XYXY7");
                    string[] Sub;
                    if (subjet.Contains("/()/"))
                    {
                        Sub = subjet.Split("/()/");
                        string decrypted = AesCryption.Decrypt(relativData[0], me.Secret);
                        string[] Data = decrypted.Split("\"");
                        var LocalDate = _newContext.LastUpdates.First();
                        DateTime IncomeDate = Convert.ToDateTime(Sub[0]);
                        if (IncomeDate > LocalDate.timeSet)
                        {
                            switch (Sub[1])
                            {
                                case "Delete":
                                    DbCommand.CreateCommand(decrypted, ConnectionString);
                                    break;
                                case "Put":
                                    DbCommand.CreateCommand(decrypted, ConnectionString);
                                    break;
                                case "friendRequest":
                                    var bodymessage = decrypted.Split("/()/");
                                    var potentialfriend = new MyFriend(bodymessage);
                                    _newContext.MyFriends.AddAsync(potentialfriend);
                                    break;
                                case "DeniedfriendRequest":
                                    var bodymessage1 = decrypted.Split("/()/");
                                    var myfriend = _newContext.MyFriends.Where(x => x.Email == bodymessage1[1]).FirstOrDefault();
                                    _newContext.MyFriends.Remove(myfriend);
                                    break;
                                case "AcceptedfriendRequest":
                                    var bodymessages = decrypted.Split("/()/");
                                    var friend = _newContext.MyFriends.Where(x => x.Email == bodymessages[0]).FirstOrDefault();
                                    if(friend.isFriend == false)
                                    {
                                        friend.userName = bodymessages[4];
                                        friend.isFriend = true;
                                        _newContext.Update(friend);
                                        try
                                        {
                                            var deserializedItemsFromItems = System.Text.Json.JsonSerializer.Deserialize<List<Discussion>>(bodymessages[1]);
                                            if (deserializedItemsFromItems != null)
                                            {
                                                foreach (Discussion x in deserializedItemsFromItems)
                                                {
                                                    Discussion discdisc = new Discussion() { ID = x.ID, Email = x.Email, userName = x.userName, Headline = x.Headline, discussionText = x.discussionText, Date = x.Date };
                                                    _newContext.Discussions.Add(discdisc);
                                                };
                                            }
                                            var deserializedItemsFromItems1 = System.Text.Json.JsonSerializer.Deserialize<List<Post>>(bodymessages[2]);
                                            if (deserializedItemsFromItems1 != null)
                                            {
                                                foreach (Post x in deserializedItemsFromItems1)
                                                {
                                                    Post pospos = new Post() { ID = x.ID, Email = x.Email, userName = x.userName, Date = x.Date, postText = x.postText }; 
                                                    _newContext.Posts.Add(x);
                                                };
                                            }
                                            var deserializedItemsFromItems2 = System.Text.Json.JsonSerializer.Deserialize<List<MyFriendsFriends>>(bodymessages[3]);
                                            if (deserializedItemsFromItems2 != null)
                                            {
                                                var myName = _newContext.MySettings.FirstOrDefault();
                                                foreach (MyFriendsFriends x in deserializedItemsFromItems2)
                                                {
                                                    MyFriendsFriends friefrie = new MyFriendsFriends() { Email = x.Email, userName = x.userName, myFriendID = friend.ID };
                                                    if(friefrie.Email != myName.Email)
                                                    {
                                                       _newContext.MyFriendsFriends.Add(friefrie);
                                                    }
                                                };
                                            }
                                        }
                                        catch (Exception)
                                        {
                                            throw;
                                        }
                                        var my = _newContext.MySettings.FirstOrDefault();
                                        var allMyDiscussion = _newContext.Discussions.Where(x => x.Email == my.Email).ToList();
                                        string myDiscussionJson = System.Text.Json.JsonSerializer.Serialize(allMyDiscussion);
                                        var allMyPost = _newContext.Posts.Where(x => x.Email == my.Email).ToList();
                                        string myPostJson = System.Text.Json.JsonSerializer.Serialize(allMyPost);
                                        var allMyFriends = _newContext.MyFriends.ToList();
                                        string myFriendJson = System.Text.Json.JsonSerializer.Serialize(allMyFriends);
                                        MailSender.SendAcceptedRequest(my, bodymessages[0], myDiscussionJson, myPostJson, myFriendJson);
                                    }

                                    break;

                                case "deleteFriend":
                                    var bodymessage3 = decrypted.Split("/()/");
                                    var allDiscussions = _newContext.Discussions.Where(x => x.Email == bodymessage3[1]).ToList();
                                    _newContext.RemoveRange(allDiscussions);
                                    var deletemyFriend = _newContext.MyFriends.Where(y => y.Email == bodymessage3[1]).FirstOrDefault();
                                    _newContext.MyFriends.Remove(deletemyFriend);
                                    break;
                                default:
                                    DbCommand.CreateCommand(decrypted, ConnectionString);
                                    break;
                            }
                            LocalDate.timeSet = IncomeDate;
                            _newContext.Update(LocalDate);
                            _newContext.SaveChangesAsync();
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
    }
}
