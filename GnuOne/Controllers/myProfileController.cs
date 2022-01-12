using GnuOne.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace GnuOne.Controllers
{
   

    [Route("api/[controller]")]
    [ApiController]
    public class myProfileController : ControllerBase
    {
        private readonly ApiContext _context;

        public myProfileController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var a = await _context.Myprofile.ToListAsync();
            var json = JsonConvert.SerializeObject(a);
            return Ok(json);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPicture(int? id)
        {
            var a = await _context.Standardpictures.Where(x => x.pictureID == id).Select(x => x.PictureSrc).FirstAsync();
            var json = JsonConvert.SerializeObject(a);
            return Ok(json);
        }
    }
}
    