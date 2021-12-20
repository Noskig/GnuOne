using Library;
using Library.HelpClasses;
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
                    //Vi behöver inte skicka råa SQL satser. kan istället
                    ///Decrypt mail
                    ///Deserialse object -> ex Disucssion disscuission
                    ///discussion 
                    ///context.discussion.add(discussio)
                    ///contect.savechanged()


                    var message = client.GetMessage(i);
                    var subjet = message.Subject;

                    var body = message.GetTextBody(MimeKit.Text.TextFormat.Text);


                    
                    string[] relativData = body.Split("XYXY/(/(XYXY7");

                    string decrypted = AesCryption.Decrypt(relativData[0], me.Secret);

                    string[] Data = decrypted.Split("\"");
                    var LocalDate = _newContext.LastUpdates.First();
                    string[] Sub = subjet.Split("/()/");
                    DateTime IncomeDate = Convert.ToDateTime(Sub[0]);
                    if (IncomeDate > LocalDate.TimeSet)
                    {
                        switch (Sub[1])
                        {
                            case "DELETE":

                                DbCommand.CreateCommand(decrypted, ConnectionString);
                                break;

                            case "PUT":
                                DbCommand.CreateCommand(decrypted, ConnectionString);
                                break;

                            case "friendRequest":
                                var bodymessage = decrypted.Split("/()/");
                                var potentialfriend = new MyFriend(bodymessage);
                                _newContext.MyFriends.Add(potentialfriend);
                                _newContext.SaveChangesAsync();

                                break;
                            case "DeniedfriendRequest":
                                var bodymessage1 = decrypted.Split("/()/");
                                var myfriend = _newContext.MyFriends.Where(x => x.Email == bodymessage1[1]).FirstOrDefault();
                                _newContext.MyFriends.Remove(myfriend);
                                _newContext.SaveChangesAsync();
                                break;
                            case "AcceptedfriendRequest":
                                var bodymessages = decrypted.Split("/()/");
                                var friend = _newContext.MyFriends.Where(x => x.Email == bodymessages[0]).FirstOrDefault();
                                friend.IsFriend = true;
                                _newContext.Update(friend);

                                var deserializedItemsFromItems = JsonConvert.DeserializeObject<List<Discussion>>(File.ReadAllText(bodymessages[1]));
                                foreach (Discussion x in deserializedItemsFromItems) { _newContext.Discussions.Add(x); };

                                var deserializedItemsFromItems1 = JsonConvert.DeserializeObject<List<Post>>(File.ReadAllText(bodymessages[2]));
                                foreach (Post x in deserializedItemsFromItems1) { _newContext.Posts.Add(x); };

                                var deserializedItemsFromItems2 = JsonConvert.DeserializeObject<List<MyFriend>>(File.ReadAllText(bodymessages[3]));
                                foreach (MyFriend x in deserializedItemsFromItems2) { _newContext.MyFriends.Add(x); };

                                break;

                            case "deleteFriend":
                                var bodymessage3 = decrypted.Split("/()/");
                                var deleteFriend = new MyFriend(bodymessage3);
                                _newContext.MyFriends.Remove(deleteFriend);
                                var allDiscussions = _newContext.Discussions.Where(x => x.user == bodymessage3[0]).ToList();
                                _newContext.RemoveRange(allDiscussions);
                                _newContext.SaveChangesAsync();
                                break;

                            default:
                                DbCommand.CreateCommand(decrypted, ConnectionString);
                                break;


                        }
                        LocalDate.TimeSet = IncomeDate;
                        _newContext.LastUpdates.Update(LocalDate);
                        _newContext.SaveChanges();
                    }

                }
            }
        }
    }

}
