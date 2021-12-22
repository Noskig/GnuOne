using GnuOne.Data;
using Library;
using Library.HelpClasses;
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
            string subject = "friendRequest";
            MailSender.SendFriendMail(_settings, Email.Email, subject);
            await _context.MyFriends.AddAsync(potentialnewfriend);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var listaDiscussion = await _context.MyFriends.ToListAsync();
            var converted = JsonConvert.SerializeObject(listaDiscussion);

            return Ok(converted);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] MyFriend MyFriend)
        {
            var friend = await _context.MyFriends.Where(x => x.Email == MyFriend.Email).FirstAsync(); 
            if (friend == null)
            {
                return BadRequest("Could not find friend with this email");
            }
            if (MyFriend.isFriend == false)
            {
                string subject = "DeniedfriendRequest";
                MailSender.SendFriendMail(_settings, MyFriend.Email, subject); 
                _context.MyFriends.Remove(friend);
                await _context.SaveChangesAsync();
                return Ok("Dont want to be friends");
            }
            else
            {
                string myName = _settings.userName;
                var allMyDiscussion = _context.Discussions.Where(x => x.userName == myName).ToList();
                string myDiscussionJson = System.Text.Json.JsonSerializer.Serialize(allMyDiscussion);
                var allMyPost = _context.Posts.Where(x => x.userName == myName).ToList();
                string myPostJson = System.Text.Json.JsonSerializer.Serialize(allMyPost);
                var allMyFriends = _context.MyFriends.Where(x => x.isFriend == true).ToList();
                string myFriendJson = System.Text.Json.JsonSerializer.Serialize(allMyFriends);
                MailSender.SendAcceptedRequest(_settings, MyFriend.Email, myDiscussionJson, myPostJson, myFriendJson);
            }
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] MyFriend MyFriend)
        {
            var MyDiscussions = _context.Discussions.Where(x => x.Email == MyFriend.Email).ToList(); 
            _context.Discussions.RemoveRange(MyDiscussions);
            var MyFriends = _context.MyFriends.Where(x => x.Email == MyFriend.Email).ToList();
            _context.MyFriends.RemoveRange(MyFriends);
            await _context.SaveChangesAsync();
            string subject = "deleteFriend";
            MailSender.SendFriendMail(_settings, MyFriend.Email, subject);
            return Ok();
        }
    }
}
