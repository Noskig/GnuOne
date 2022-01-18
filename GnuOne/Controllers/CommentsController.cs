using GnuOne.Data;
using Library;
using Library.HelpClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Welcome_Settings;

namespace GnuOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly MySettings _settings;

        public CommentsController(ApiContext context)
        {
            _context = context;
            _settings = _context.MySettings.First();

        }
        // GET: api/Comments
        /// <summary>
        /// Hämtar kommentarer från DB. Konverterar dom till JSON och skickar tillbaka requesten.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var lista = await _context.Comments.ToListAsync();
            var converted = JsonConvert.SerializeObject(lista);

            return Ok(converted);
        }

        // GET: api/Comments/5
        /// <summary>
        /// Hämtar information på ett specifikt ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment(int? id)
        {

            var comment = await _context.Comments.Where(x => x.ID == id).ToListAsync();
            if (comment.Count == 0)
            {
                return NotFound();
            }

            var converted = JsonConvert.SerializeObject(comment);

            return Ok(converted);
        }

        //POST: api/Comments
        /// <summary>
        /// Lägger upp en kommentar och skickar ut mail
        /// </summary>
        /// <param name = "comment" ></ param >
        /// < returns ></ returns >
        [HttpPost]
        public async Task<IActionResult> PostComment([FromBody] Comment comment)
        {
            comment.Date = DateTime.Now;
            DateTime foo = DateTime.Now;
            long unixID = ((DateTimeOffset)foo).ToUnixTimeSeconds();
            comment.ID = Convert.ToInt32(unixID);
            comment.Email = _settings.Email;
            comment.userName = _settings.userName;

            var jsonComment = JsonConvert.SerializeObject(comment);

            var postDiscussionId = await _context.Posts.Where(x => x.ID == comment.postID).Select(x => x.discussionID).SingleAsync();
            var authorEmail = await _context.Discussions.Where(x => x.ID == postDiscussionId).Select(x => x.Email).SingleAsync();

            MailSender.SendObject(jsonComment, authorEmail, _settings, "PostComment");

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostedComment", new { id = comment.ID }, comment);
        }

        //[HttpPost]
        //public async Task<IActionResult> PostComment([FromBody] string comment)
        //{

        //    var megacrypt = new MegaCrypt(comment);
        //    megacrypt.RSAEncryptIt(Global.MyPrivatekey, Global.ericPublicKey);

        //    var jsoncrypt = JsonConvert.SerializeObject(megacrypt);



        //    MailSender.SendObject(jsoncrypt, "mailconsolejonatan@gmail.com", _settings, "TestMail");




        //    return Ok("GetComment");
        //}
        //    }
        //}

        // PUT: api/Comments
        /// <summary>
        /// Ändrar en kommentar
        /// </summary>
        /// <param name="id"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, [FromBody] Comment comment)
        {
            if (id != comment.ID)
            {
                return BadRequest();
            }
            if (!CommentExists(id))
            {
                return NotFound();
            }

            var jsonComment = JsonConvert.SerializeObject(comment);
            var postDiscussionId = await _context.Posts.Where(x => x.ID == comment.postID).Select(x => x.discussionID).SingleAsync();
            var authorEmail = await _context.Discussions.Where(x => x.ID == postDiscussionId).Select(x => x.Email).SingleAsync();

            MailSender.SendObject(jsonComment, authorEmail, _settings, "PutComment");

            _context.Update(comment);
            await _context.SaveChangesAsync();
            return Accepted(comment);
        }

        // DELETE: api/Comments/5
        /// <summary>
        /// Tar bort kommentar med ett specifikt ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int? id)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            comment.commentText = "Deleted comment";
            comment.userName = "Deleted comment";
            
            _context.Update(comment);
            await _context.SaveChangesAsync();

            var jsonComment = JsonConvert.SerializeObject(comment);
            var postDiscussionId = await _context.Posts.Where(x => x.ID == comment.postID).Select(x => x.discussionID).SingleAsync();
            var authorEmail = await _context.Discussions.Where(x => x.ID == postDiscussionId).Select(x => x.Email).SingleAsync();

            MailSender.SendObject(jsonComment, authorEmail, _settings, "PutComment");

            return Accepted(comment);
        }
        private bool CommentExists(int? id)
        {
            return _context.Comments.Any(e => e.ID == id);
        }
    }
}
