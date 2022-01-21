using GnuOne.Data;
using Library;
using Library.HelpClasses;
using Library.Models;
using Library.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Welcome_Settings;


namespace GnuOne.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class DiscussionsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly MySettings _settings;
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
            var listaDiscussion = _context.Discussions.ToList();
            var taglista = _context.tags.ToList();

            foreach (var discussion in listaDiscussion)
            {
                if (discussion.tagOne is not null)
                {
                    var tagOne = taglista.Where(x => x.ID == discussion.tagOne).Select(x => x.tagName).Single();
                    discussion.tags.Add(tagOne);
                }
                if (discussion.tagTwo is not null)
                {
                    var tagTwo = taglista.Where(y => y.ID == discussion.tagTwo).Select(x => x.tagName).Single();
                    discussion.tags.Add(tagTwo);

                }
                if (discussion.tagThree is not null)
                {
                    var tagThree = taglista.Where(z => z.ID == discussion.tagThree).Select(x => x.tagName).Single();
                    discussion.tags.Add(tagThree);
                }

                discussion.sumOfPosts = await _context.Posts.Where(x => x.discussionID == discussion.ID).CountAsync();
                //DiscussionWithPostSumDTO dWPost = new DiscussionWithPostSumDTO(discussion, a);
            }



            //hämta antal poster på varje diskussion.
            //var a = _context.Posts.Count();
            return Ok(listaDiscussion);
        }

      
        //Skickar in ett id (tag)


        // GET: api/Discussions/5
        /// <summary>
        /// Hämtar information på ett specifikt ID. 
        /// görs om till en DTO och skickar tillbaka med extra information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")] //sök på user med Email-adress bla bla.
        public async Task<IActionResult> Get(int id)
        {
            var discussion = await _context.Discussions.FindAsync(id);

            if (discussion == null)
            {
                return NotFound();
            }
            var taglista = await _context.tags.ToListAsync();
        
                if (discussion.tagOne is not null)
                {
                    var tagOne =  taglista.Where(x => x.ID == discussion.tagOne).Select(x => x.tagName).Single();
                    discussion.tags.Add(tagOne);
                }
                if (discussion.tagTwo is not null)
                {
                    var tagTwo = taglista.Where(y => y.ID == discussion.tagTwo).Select(x => x.tagName).Single();
                    discussion.tags.Add(tagTwo);

                }
                if (discussion.tagThree is not null)
                {
                    var tagThree = taglista.Where(z => z.ID == discussion.tagThree).Select(x => x.tagName).Single();
                    discussion.tags.Add(tagThree);
                }

            var postlist = await _context.Posts.Where(x => x.discussionID == id).ToListAsync();
            //var commentList = await _context.Comments.Where(x => x.postID == id).ToListAsync();

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
            DateTime unixTime = DateTime.Now;
            long unixID = ((DateTimeOffset)unixTime).ToUnixTimeSeconds();
            discussion.ID = Convert.ToInt32(unixID);
            discussion.Email = _settings.Email;
            discussion.userName = _settings.userName;

            var jsonDiscussion = JsonConvert.SerializeObject(discussion);

            foreach (var user in _context.MyFriends)
            {
                if (user.isFriend == false) { continue; }
                MailSender.SendObject(jsonDiscussion, user.Email, _settings, "PostedDiscussion");
            }

            await _context.AddAsync(discussion);
            await _context.SaveChangesAsync();

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


            var jsonDiscussion = JsonConvert.SerializeObject(discussion);
            try
            {
                _context.Update(discussion);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Could not update discussion");
            }

            foreach (var user in _context.MyFriends)
            {
                if (user.isFriend == false) { continue; }
                MailSender.SendObject(jsonDiscussion, user.Email, _settings, "PutDiscussion");
            }
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

            var jsonDiscussion = JsonConvert.SerializeObject(discussion);

            foreach (var user in _context.MyFriends)
            {
                if (user.isFriend == false) { continue; }
                MailSender.SendObject(jsonDiscussion, user.Email, _settings, "DeleteDiscussion");
            }

            _context.Remove(discussion);
            await _context.SaveChangesAsync();

            //var query = discussion.DeleteDiscussion();

            //foreach (var user in _context.MyFriends)
            //{
            //    MailSender.SendEmail(user.Email, query, "Delete", _settings);
            //}
            return Accepted(discussion);
        }
        private bool DiscussionExists(int? id)
        {
            return _context.Discussions.Any(e => e.ID == id);
        }
    }
}
