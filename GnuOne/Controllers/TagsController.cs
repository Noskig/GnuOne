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
            var tags = _context.Tags.ToListAsync();
            if (tags is not null)
            {
                var jsonTags = JsonConvert.SerializeObject(tags);
                return Ok(jsonTags);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> PostTags([FromBody] Tag tag)
        {
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();


            return Ok();
        }



    }
}
