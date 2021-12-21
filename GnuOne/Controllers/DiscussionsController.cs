using GnuOne.Data;
using Library;
using Library.HelpClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Welcome_Settings;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GnuOne.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class DiscussionsController : ControllerBase
    {

        private readonly ApiContext _context;
        private readonly MySettings _settings;
        //Contexten laddas inte med connectionstring

        public DiscussionsController(ApiContext context)
        {
            _context = context;
            _settings = _context.MySettings.First();
        }

        // GET: api/Discussions
        /// <summary>
        /// Hämtar Discussionen från DB. Konverterar dom till JSON och skickar tillbaka requesten.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {


            var listaDiscussion = await _context.Discussions.ToListAsync();
            var converted = JsonConvert.SerializeObject(listaDiscussion);

            return Ok(converted);

        }

        // GET: api/Discussions/5
        /// <summary>
        /// Hämtar information på ett specifikt ID. 
        /// görs om till en DTO och skickar tillbaka med extra information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var discussion = await _context.Discussions.FindAsync(id);

            if (discussion == null)
            {
                return NotFound();
            }


            var postlist = await _context.Posts.Where(x => x.ID == id).ToListAsync();

            var dto = new DiscussionDTO(discussion, postlist);

            return Ok(dto);
        }

        // POST: api/Discussions
        /// <summary>
        /// Lägger upp en discussion och skickar ut mail
        /// </summary>
        /// <param name="discussion"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostDiscussion([FromBody] Discussion discussion)
        {
            discussion.Date = DateTime.Now;
            //discussion.user = _settings.Username;

            //Sätter ID manuellt för att matcha i DB hos alla användare. Vill vi ha det så?
            //if (_context.Discussions.Any())
            //{
            //    var HighestID = await _context.Discussions.Select(x => x.ID).MaxAsync();
            //    discussion.ID = HighestID + 1;
            //}
            //else
            //{
            //    discussion.ID = 1;
            //}
            DateTime foo = DateTime.Now;
            long unixTime = ((DateTimeOffset)foo).ToUnixTimeSeconds();
            discussion.ID = Convert.ToInt32(unixTime);
            discussion.Email = _settings.Email;
            

     
            //var JsonDiscussion = JsonConvert.SerializeObject(discussion);

            // Mailsender.sendemail(JsonDiscussion, 

            ///skapa query
            var query = discussion.SendDiscussion();
            //skickar ut mail
            foreach (var user in _context.MyFriends)
            {
                MailSender.SendEmail(user.Email, query, "Post", _settings);
            }
            _context.Add(discussion);
            _context.SaveChanges();
            return CreatedAtAction("GetDiscussion", new { id = discussion.ID }, discussion);
        }

        // PUT: api/Discussions/5
        /// <summary>
        /// Ändrar en discussion
        /// </summary>
        /// <param name="id"></param>
        /// <param name="discussion"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiscussion(int id, Discussion discussion)
        {
            if (id != discussion.ID)
            {
                return BadRequest();
            }
            if (!DiscussionExists(id))
            {
                return NotFound();
            }

            //hittar gamla texten för att skicka med 
            //och hitta den unika kommentaren i databasen hos de andra användare
            var oldtext = await _context.Discussions.Where(x => x.ID == discussion.ID)
                                                    .Select(x => x.discussionText)
                                                    .FirstOrDefaultAsync();



           
            var query = discussion.EditDiscussion(oldtext);
            //skickar ut mail
            foreach (var user in _context.MyFriends)
            {
                ///Skicka mail
                MailSender.SendEmail(user.Email, query, "PUT", _settings);
            }
            _context.Update(discussion);
            _context.SaveChanges();
            return Accepted(discussion);
        }

        // DELETE: api/Discussions/5
        /// <summary>
        /// Tar bort discussionen med ett specifikt ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscussion(int id)
        {
            var discussion = await _context.Discussions.FindAsync(id);
            if (discussion == null)
            {
                return NotFound();
            }

            //skickar ut mail
            ///skapa query
            
            var query = discussion.DeleteDiscussion();
            foreach (var user in _context.MyFriends)
            {
                ///Skicka mail
                MailSender.SendEmail(user.Email, query, "PUT", _settings);
            }
            _context.Remove(discussion);
            _context.SaveChanges();
            return Accepted(discussion);
        }
        private bool DiscussionExists(int? id)
        {
            return _context.Discussions.Any(e => e.ID == id);
        }
    }
}
