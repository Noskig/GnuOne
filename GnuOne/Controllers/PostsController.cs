using GnuOne.Data;
using Library;
using Library.HelpClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GnuOne.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly MySettings _settings;
        public PostsController(ApiContext context)
        {
            _context = context;
            _settings = _context.MySettings.First();
        }
        // GET: api/Posts
        /// <summary>
        /// Hämtar Posten från DB. Konverterar dom till JSON och skickar tillbaka.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var result = await _context.Posts.ToArrayAsync();
            var converted = JsonConvert.SerializeObject(result);

            return Ok(converted);
        }

        // GET: api/Posts/5
        /// <summary>
        /// Hämtar post med ett specifikt ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int? id)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        // POST: api/Posts
        /// <summary>
        /// Lägger upp en post och skickar ut mail
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostPost([FromBody] Post post)
        {
            post.Date = DateTime.Now;
            DateTime foo = DateTime.Now;
            long unixID = ((DateTimeOffset)foo).ToUnixTimeSeconds();
            post.ID = Convert.ToInt32(unixID);
            post.Email = _settings.Email;
            post.userName = _settings.userName;

            var jsonPost = JsonConvert.SerializeObject(post);


            //skickar vidare till författaren
            MailSender.SendObject(jsonPost, post.discussionEmail, _settings, "PostedPost");

            //foreach (var user in _context.MyFriends)
            //{
            //    if (user.isFriend == false) { continue; }
            //    MailSender.SendObject(jsonPost, user.Email, _settings, "PostedPost");
            //}

            await _context.AddAsync(post);
            await _context.SaveChangesAsync();


            return CreatedAtAction("GetPost", new { id = post.ID }, post);
        }

        // PUT: api/Posts/5
        /// <summary>
        /// Ändrar en kommentar med ett specifikt ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int? id, Post post)
        {
            if (id != post.ID)
            {
                return BadRequest();
            }

            if (!PostExists(id))
            {
                return NotFound();
            }


            //hittar gamla texten för att skicka med 
            //och hitta den unika kommentaren i databasen hos de andra användare
            //var oldtext = await _context.Posts.Where(x => x.ID == post.ID).Select(x => x.postText).FirstOrDefaultAsync();

            var jsonPost = JsonConvert.SerializeObject(post);

            MailSender.SendObject(jsonPost, post.discussionEmail, _settings, "PutPost");

            //foreach (var user in _context.MyFriends)
            //{
            //    if (user.isFriend == false) { continue; }
            //    MailSender.SendObject(jsonPost, user.Email, _settings, "PutPost");
            //}
            _context.Update(post);
            await _context.SaveChangesAsync();

            return Accepted(post);
        }

        // DELETE: api/Posts/5
        /// <summary>
        /// Deletar en post med specifikt ID och skickar ut mail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            //json
            //krypter
            //skicka mail
            //deleta

            var jsonPost = JsonConvert.SerializeObject(post);

            MailSender.SendObject(jsonPost, post.discussionEmail, _settings, "DeletePost");

            //foreach (var user in _context.MyFriends)
            //{
            //    if (user.isFriend == false) { continue; }
            //    MailSender.SendObject(jsonPost, user.Email, _settings, "DeletePost");
            //}

            _context.Remove(post);
            await _context.SaveChangesAsync();
            return Accepted(post);
        }
        private bool PostExists(int? id)
        {
            return _context.Posts.Any(e => e.ID == id);
        }
    }
}
