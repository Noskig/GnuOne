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

        //GET: api/<SettingController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var settings = await _context.MySettings.FirstOrDefaultAsync();

            if (settings != null)
            {
                var jsonsettings = JsonConvert.SerializeObject(settings);
                return Ok(settings);
            }
            return NotFound();
        }


        // PUT api/<SettingController>/5
        [HttpPut]
        public async Task<IActionResult> updateSettingsPut([FromBody] MySettings mySettings)
        {
            _context.MySettings.Update(mySettings);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{username}")]
        public async Task<IActionResult> updateSettingsPut([FromBody] string username)
        {
            var settings = await _context.MySettings.FirstOrDefaultAsync();
            settings.userName = username;
            _context.MySettings.Update(settings);
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
