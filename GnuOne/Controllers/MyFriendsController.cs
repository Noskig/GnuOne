using GnuOne.Data;
using Library;
using Library.HelpClasses;
using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace GnuOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyFriendsController : ControllerBase
    {
        private ApiContext _context;
        private MySettings _settings;

        public MyFriendsController(ApiContext context)
        {
            _context = context;
            _settings = _context.MySettings.First();
        }
        ///Frontend - Klicka på knapp för att skicka en vänförfrågan
        /// <summary>
        /// Send FriendRequest
        /// </summary>
        /// <param name="ToEmail"></param>
        /// <returns></returns>
        [HttpPost]
        //[Route("api/[controller]/SendFriendRequest")]
        public async Task<IActionResult> PostSendFriendRequest([FromBody] MyFriend Email)
        {
            
            var potentialnewfriend = new MyFriend();
            potentialnewfriend.Email = Email.Email;

            //skickar min information till vännen
            var myInfo = new MyFriend();
            myInfo.Email = _settings.Email;
            myInfo.userName = _settings.userName;
            myInfo.isFriend = false; //vi har inte blivit vänner än

            var jsonMyInfoInObject = JsonConvert.SerializeObject(myInfo);
            
            MailSender.SendObject(jsonMyInfoInObject, Email.Email, _settings, "FriendRequest");

            _context.MyFriends.Add(potentialnewfriend);
            await _context.SaveChangesAsync();

            return Ok();
        }
        /// <summary>
        /// Letar efter vänner & gör till JSON
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var listaDiscussion = await _context.MyFriends.ToListAsync();
            var converted = JsonConvert.SerializeObject(listaDiscussion);

            return Ok(converted);
        }
        /// <summary>
        /// Svarar på en vänförfrågan
        /// </summary>
        /// <param name="potentialFriend"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> PutDeniedOrAcceptFriendRequest([FromBody] MyFriend potentialFriend)
        {
            var friend = await _context.MyFriends.Where(x => x.Email == potentialFriend.Email).FirstAsync();
            if (friend == null)
            {
                return BadRequest("Could not find friend with this email");
            }
            //min info
            var myInfo = new MyFriend();
            myInfo.Email = _settings.Email.ToString();
            myInfo.userName = _settings.userName.ToString();

            var jsonMyInfoInObject = JsonConvert.SerializeObject(myInfo);


            if (potentialFriend.isFriend == false)
            {
                
                MailSender.SendObject(jsonMyInfoInObject, friend.Email, _settings, "DeniedFriendRequest");

                _context.MyFriends.Remove(friend);
                await _context.SaveChangesAsync();

                return Ok("Dont want to be friends");
            }
            //Annars Accepterar vi vännen
                //Gör om ens discuss,vänner och post till JSON och sänder iväg ett mail
            else
            {
                friend.isFriend = true;

                var bigListWithMyInfo = BigList.FillingBigListWithMyInfo(_context, myInfo.Email, true);
                bigListWithMyInfo.username = _settings.userName.ToString();

                var jsonBigListObject = JsonConvert.SerializeObject(bigListWithMyInfo);

                MailSender.SendObject(jsonBigListObject, friend.Email, _settings, "AcceptedFriendRequest");

                var newFriendFriendForMyFriend = new MyFriendsFriends(friend, myInfo.Email);
                var jsonFriendFriendToSend = JsonConvert.SerializeObject(newFriendFriendForMyFriend);

                foreach (var user in _context.MyFriends)
                {
                    if (user.isFriend == false) { continue; }
                    MailSender.SendObject(jsonFriendFriendToSend, user.Email, _settings, "FriendGotAFriend");
                }


                _context.MyFriends.Update(friend);
                await _context.SaveChangesAsync();

                return Ok("You have accepted the Friend Request");
            }
        }
        /// <summary>
        /// Tar bort en vän
        /// </summary>
        /// <param name="MyFriend"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteFriend([FromBody] MyFriend MyFriend)
        {
            //hittat den vännen som vi ska ta bort
            var notFriend = _context.MyFriends.Where(x => x.Email == MyFriend.Email).FirstOrDefault();

            //TaBortAllt(notFriend)
            var theirDiscussion = _context.Discussions.Where(x => x.Email == MyFriend.Email).ToList();
            _context.Discussions.RemoveRange(theirDiscussion);
            _context.MyFriends.Remove(notFriend);
            await _context.SaveChangesAsync();

            //skickas till vännen som tas bort
            var myInfo = _context.MySettings.FirstOrDefault();
            var jsonmyInfo = JsonConvert.SerializeObject(myInfo);
            MailSender.SendObject(jsonmyInfo, notFriend.Email, _settings, "ItsNotMeItsYou");

            var jsonNotFriend = JsonConvert.SerializeObject(notFriend);
            //skickas ut till mina vänner och tas bort i deras friendsfriend
            foreach (var user in _context.MyFriends)
            {
                if (user.isFriend == false) { continue; }
                MailSender.SendObject(jsonNotFriend, user.Email, _settings, "FriendsFriendGotRemoved");
            }

            return Ok();
        }
    }
}
