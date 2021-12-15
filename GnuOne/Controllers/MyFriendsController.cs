using GnuOne.Data;
using Library;
using Library.HelpClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> SendFriendRequest([FromBody] string ToEmail)
        {

            var potentialnewfriend = new MyFriend();
            potentialnewfriend.Email = ToEmail;

            MailSender.SendFriendRequest(_settings, ToEmail);

            await _context.MyFriends.AddAsync(potentialnewfriend);
            await _context.SaveChangesAsync();

            return Ok();
        }



        ///Skicka tillbaka att jag accepterat eller nekat vänförfrågan
        [HttpPost]
        [Route("api/[controller]/answerrequest")]
        public async Task<IActionResult> AnswerRequest([FromBody] string newFriendsEmail, bool decicison)
        {
            var friend = await _context.MyFriends.Where(x => newFriendsEmail == x.Email).FirstAsync(); /// vet inte om den blir null om man inte hittar en vän
            if (friend == null)
            {
                return BadRequest("Could not find friend with this email");
            }
            if (decicison == false)
            {
                
                MailSender.SendDeniedRequest(_settings, newFriendsEmail); //kan den misslyckas?
             
                _context.MyFriends.Remove(friend);
                await _context.SaveChangesAsync();
                return Ok("Dont want to be friends");
            }

            //skicka tillbaka info att vi är vänner 
            //skicka med mina inlägg (discussions + underliggande posts)
            
            ///Vi skapade friendspeople tabell. Lägg in i dbcontext.
            ///forsätta skicka alla mina discussions,posts och vänner i mailet.


            MailSender.SendAcceptedRequest();


            friend.IsFriend = true;
            _context.MyFriends.Update(friend);
            await _context.SaveChangesAsync();



            //ändra isfriend till true


            return Ok();
        }


    }
}
