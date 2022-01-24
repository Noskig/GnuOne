using GnuOne.Data;
using GnuOne.Data.Models;
using Library;
using Library.HelpClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;

namespace GnuOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly MySettings? _settings;

        public MessagesController(ApiContext context)
        {
            _context = context;
            _settings = _context.MySettings.FirstOrDefault();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var myfriends = await _context.MyFriends.Where(x=> x.isFriend == true).ToListAsync();
            var allmessages = await _context.Messages.ToListAsync();

            var dtoList = new List<FriendDto>();
            
            foreach ( var friend in myfriends)
            {
                var lastMessage = allmessages.Where(x => x.To == friend.Email || x.From == friend.Email).OrderBy(x => x.Sent).TakeLast(1).FirstOrDefault();
                var dtoFriend = new FriendDto(friend, lastMessage);
                dtoList.Add(dtoFriend);
            }

            var jsonDtoList = JsonConvert.SerializeObject(dtoList);

            return Ok(jsonDtoList);
        }

        [HttpGet("dm")]

        public async Task<IActionResult> GetFriendsMessages([FromBody] MyFriend friend)
        {
            var friendsMessages = await _context.Messages.Where(x => x.From == friend.Email).OrderBy(x => x.Sent).ToListAsync();
            var myMessageToFriend = await _context.Messages.Where(x=> x.To == friend.Email).OrderBy(x=>x.Sent).ToListAsync();

            var allMessages = new List<Message>();
            allMessages.AddRange(friendsMessages);
            allMessages.AddRange(myMessageToFriend);
            allMessages.OrderByDescending(x => x.Sent).ToList();

            var jsonmessages = JsonConvert.SerializeObject(allMessages); //osäker på om de är sorterade
            
            return Ok(jsonmessages);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(Message message)
        {
            if (message == null)
            {
                return BadRequest("it seems like you didn't include a message");
            }
            message.Sent = DateTime.Now;
            
            DateTime unixTime = DateTime.Now;
            long unixID = ((DateTimeOffset)unixTime).ToUnixTimeSeconds();
            message.ID = Convert.ToInt32(unixID);

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            var jsonMessage = JsonConvert.SerializeObject(message);

            MailSender.SendObject(jsonMessage, message.To, _settings, "DirectMessage");

            return Ok("Message Sent");
        }

    }
}
