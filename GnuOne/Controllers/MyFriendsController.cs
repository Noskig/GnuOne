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
            MailSender.SendFriendRequest(_settings, Email.Email);
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


        ///Skicka tillbaka att jag accepterat eller nekat vänförfrågan
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] MyFriend MyFriend)
        {
            var friend = await _context.MyFriends.Where(x => x.Email == MyFriend.Email).FirstAsync(); /// vet inte om den blir null om man inte hittar en vän
            if (friend == null)
            {
                return BadRequest("Could not find friend with this email");
            }
            if (MyFriend.IsFriend == false)
            {
                MailSender.SendDeniedRequest(_settings, MyFriend.Email); //kan den misslyckas?
                _context.MyFriends.Remove(friend);
                await _context.SaveChangesAsync();
                return Ok("Dont want to be friends");
            }
            else
            {
                friend.IsFriend = true;
                _context.Update(friend);
                _context.SaveChanges();

                string myName = _settings.Username;

                var allMyDiscussion = _context.Discussion.Where(x => x.user == myName).ToList();
                foreach (var item in allMyDiscussion)
                {
                    item.discussionid = null;
                }
                string myDiscussionJson = System.Text.Json.JsonSerializer.Serialize(allMyDiscussion);

                var allMyPost = _context.Posts.Where(x => x.User == myName).ToList();
                foreach (var item in allMyPost)
                {
                    item.postid = null;
                }
                string myPostJson = System.Text.Json.JsonSerializer.Serialize(allMyPost);

                var allMyFriends = _context.MyFriends.ToList();
                //foreach (var item in allMyFriends)
                //{
                //    item.userid = null;
                //}
                string myFriendJson = System.Text.Json.JsonSerializer.Serialize(allMyFriends);
                MailSender.SendAcceptedRequest(_settings, MyFriend.Email, myDiscussionJson, myPostJson, myFriendJson);

            }
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] MyFriend MyFriend)
        {
            var MyDiscussions = _context.Discussion.Where(x => x.user == MyFriend.username).ToList(); //lägga till Email kolumn i discussion? Unikt ID?
            _context.Discussion.RemoveRange(MyDiscussions);
            var MyFriends = _context.MyFriends.Where(x => x.Email == MyFriend.Email).ToList();
            _context.MyFriends.RemoveRange(MyFriends);
            await _context.SaveChangesAsync();

            MailSender.DeleteFriend(_settings, MyFriend.Email);
            return Ok();
        }


    }
}
