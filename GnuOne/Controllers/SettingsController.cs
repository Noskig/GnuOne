using GnuOne.Data;
using Library;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Library.HelpClasses;


namespace GnuOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly MySettings _settings;

        public SettingsController(ApiContext context)
        {
            _context = context;
            _settings = _context.MySettings.First();
        }

        // GET: api/<SettingController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        

        //public async Task<IActionResult> PutPost(int? id, Post post)
        // PUT api/<SettingController>/5
        [HttpPut]
        public async Task<IActionResult> updateSettingsPut ( [FromBody] MySettings mySettings)
        {
            _context.MySettings.Update(mySettings);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE api/<SettingController>/5
        //kanske ta bort konto?
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
