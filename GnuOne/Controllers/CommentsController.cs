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

            var comment = await _context.Comments.Where(x => x.postid == id).ToListAsync();
            if (comment.Count == 0)
            {
                return NotFound();
            }

            var converted = JsonConvert.SerializeObject(comment);

            return Ok(converted);
        }

        // POST: api/Comments
        /// <summary>
        /// Lägger upp en kommentar och skickar ut mail
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostComment([FromBody] Comment comment)
        {
            comment.date = DateTime.Now;

            //För att matcha ID i alla Databaser så sätts den manuellt.
            if (_context.Comments.Any())
            {
                var HighestID = await _context.Comments.Select(x => x.commentid).MaxAsync();
                comment.commentid = HighestID + 1;
            }
            else
            {
                comment.commentid = 1;
            }
            var query = comment.SendComments();
            //skickar ut mail
            foreach (var user in _context.MyFriends)
            {
                MailSender.SendEmail(user.Email, query, "Post", _settings);
            }


            return CreatedAtAction("GetComment", new { id = comment.commentid }, comment);
        }

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
            if (id != comment.commentid)
            {
                return BadRequest();
            }
            if (!CommentExists(id))
            {
                return NotFound();
            }

            //hittar gamla texten för att skicka med 
            //och hitta den unika kommentaren i databasen hos de andra användare
            var oldcommentText = await _context.Comments.Where(x => x.commentid == comment.commentid)
                                                    .Select(x => x.comment_text)
                                                    .FirstOrDefaultAsync();

            var query = comment.EditComment(oldcommentText);
            //Skickar ut mailen

            foreach (var user in _context.MyFriends)
            {
                MailSender.SendEmail(user.Email, query, "PUT", _settings);
            }

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

            var query = comment.DeleteComments();
            //skickar ut mail
            foreach (var user in _context.MyFriends)
            {
                MailSender.SendEmail(user.Email, query, "Delete", _settings);
            }
            return Accepted(comment);
        }
        private bool CommentExists(int? id)
        {
            return _context.Comments.Any(e => e.commentid == id);
        }
    }
}
