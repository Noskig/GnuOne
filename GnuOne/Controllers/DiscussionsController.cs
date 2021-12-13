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
        //Contexten laddas inte med connectionstring

        public DiscussionsController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Discussions
        /// <summary>
        /// Hämtar Discussionen från DB. Konverterar dom till JSON och skickar tillbaka requesten.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
       
  
            var listaDiscussion = await _context.Discussion.ToListAsync();
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
            var discussion = await _context.Discussion.FindAsync(id);

            if (discussion == null)
            {
                return NotFound();
            }


            var postlist = await _context.Posts.Where(x => x.discussionid == id).ToListAsync();

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
            discussion.createddate = DateTime.Now;

            //Sätter ID manuellt för att matcha i DB hos alla användare. Vill vi ha det så?
            if (_context.Discussion.Any())
            {
                var HighestID = await _context.Discussion.Select(x => x.discussionid).MaxAsync();
                discussion.discussionid = HighestID + 1;
            }
            else
            {
                discussion.discussionid = 1;
            }

            var settings = await _context.MySettings.FirstAsync();
            //skickar ut mail
            foreach (var user in _context.Users)
            {
                ///skapa query
                var query = discussion.SendDiscussion();
                ///Skicka mail
                MailSender.SendEmail(user.Email, query, "Post", settings);
            }


            return CreatedAtAction("GetDiscussion", new { id = discussion.discussionid }, discussion);
        }

        // PUT api/<DiscussionsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DiscussionsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
