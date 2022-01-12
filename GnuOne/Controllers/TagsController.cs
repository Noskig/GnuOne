using GnuOne.Data;
using Library;
using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GnuOne.Controllers
{
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

        [HttpGet]
        public async Task<IActionResult> GetTags()
        {
            var tags = _context.tags.ToListAsync();
            if (tags is not null)
            {
                var jsonTags = JsonConvert.SerializeObject(tags);
                return Ok(jsonTags);
            }
            return NotFound();
        }

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


        [HttpPost]
        public async Task<IActionResult> PostTags([FromBody] Tag tag)
        {
            _context.tags.Add(tag);
            await _context.SaveChangesAsync();


            return Ok();
        }



    }
}
