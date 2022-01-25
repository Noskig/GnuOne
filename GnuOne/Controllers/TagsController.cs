using GnuOne.Data;
using Library;
using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GnuOne.Controllers
{
    /// <summary>
    /// Controller for handling tags (categories) 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private ApiContext _context;
        private MySettings _settings;

        public TagsController(ApiContext context)
        {
            _context = context;
            _settings = _context.MySettings.First();
        }
        /// <summary>
        /// Gets all tags
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetTags()
        {
            var tags = await _context.tags.ToListAsync();
            if (tags is not null)
            {
                var jsonTags = JsonConvert.SerializeObject(tags);
                return Ok(jsonTags);
            }
            return NotFound();
        }
        /// <summary>
        /// Gets all Discussions with a specfic tag
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var dis = await _context.Discussions.Where(x => x.tagOne == id || x.tagTwo == id || x.tagThree == id).ToListAsync();
            if (dis.Count > 0)
            {
                var json = JsonConvert.SerializeObject(dis);
                return Ok(json);
            }
            return Ok("Couldnt find any discussion by this tag");
        }
        /// <summary>
        /// Gets a specific tag's information (name)
        /// </summary>
        [HttpPatch("{id}")]
        public async Task<IActionResult> Gettagname(int id)
        {
            var dis = _context.tags.Where(x => x.ID == id).Select(x => x.tagName).Single();
            var json = JsonConvert.SerializeObject(dis);
            return Ok(json);
        }

        /// <summary>
        /// Create a new tag
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostTags([FromBody] Tag tag)
        {
            _context.tags.Add(tag);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
