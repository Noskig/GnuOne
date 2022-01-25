using GnuOne.Data;
using Library;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GnuOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookmarksController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly MySettings _settings;

        public BookmarksController(ApiContext context)
        {
            _context = context;
            _settings = _context.MySettings.First();

        }
        /// <summary>
        /// Get bookmarks
        /// </summary>
        /// <returns>
        /// Bookmarks in JsonFromat
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var listBookmarks = await _context.Bookmarks.ToListAsync();

            var listDiscussion = await _context.Discussions.ToListAsync();
            var listPosts = await _context.Posts.ToListAsync();

            var bookmark = new Bookmark(true);
            foreach (var bookmarked in listBookmarks)
            {
                //if disc
                if (listDiscussion.Where(x => x.ID == bookmarked.ID && x.Email == bookmarked.Email).Any())
                {
                    var foundDisc = listDiscussion.Where(x => x.ID == bookmarked.ID && x.Email == bookmarked.Email).First();
                    bookmark.Discussusions.Add(foundDisc);
                }
                //if post
                if (listPosts.Where(x => x.ID == bookmarked.ID && x.Email == bookmarked.Email).Any())
                {
                    var foundPost = listPosts.Where(x => x.ID == bookmarked.ID && x.Email == bookmarked.Email).First();
                    bookmark.Posts.Add(foundPost);
                }

            }
            var jsonBookmark = JsonConvert.SerializeObject(bookmark);

            return Ok(jsonBookmark);
        }

        /// <summary>
        /// Create and save a new Bookmark
        /// </summary>

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Bookmark mark)
        {
            if (mark == null)
            {
                return BadRequest();
            }
            if (!_context.Bookmarks.Contains(mark))
            {
                await _context.Bookmarks.AddAsync(mark);
                await _context.SaveChangesAsync();

                return Ok("Bookmark saved");
            }
            return Ok("Bookmark is already saved");
        }
        /// <summary>
        /// Remove one of your bookmarks
        /// </summary>
        /// <param name="mark"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Bookmark mark)
        {
            if (mark == null)
            {
                return BadRequest();
            }

            if (_context.Bookmarks.Contains(mark))
            {
                _context.Bookmarks.Remove(mark);
                await _context.SaveChangesAsync();
                return Ok("Bookmark Deleted");
            }
            return NotFound("Couldnt find that bookmark");
        }

    }
}
