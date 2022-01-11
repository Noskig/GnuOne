using GnuOne.Data;
using Library;
using Library.HelpClasses;
using Library.Models;
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
        private readonly MySettings _settings;

        public myProfileController(ApiContext context)
        {
             _context = context;
            _settings = _context.MySettings.First();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var a = await _context.MyProfile.ToListAsync();
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

        [HttpPut]
        public async Task<IActionResult> PutMyProfile([FromBody]myProfile Profile)
        {
            var myprofile = await _context.MyProfile.FirstOrDefaultAsync();

            myprofile.myUserInfo = Profile.myUserInfo;
            myprofile.pictureID = Profile.pictureID;
            myprofile.tagOne = Profile.tagOne;
            myprofile.tagTwo = Profile.tagTwo;  
            myprofile.tagThree = Profile.tagThree;  
            

            var myInfo = new MyFriend();
            myInfo.Email = _settings.Email;
            myInfo.userName = _settings.userName;
            myInfo.userInfo = Profile.myUserInfo;
            myInfo.pictureID = Profile.pictureID;
            myInfo.tagOne = Profile.tagOne;
            myInfo.tagTwo = Profile.tagTwo;
            myInfo.tagThree = Profile.tagThree;

            var jsonProfileInfo = JsonConvert.SerializeObject(myInfo); 
            try
            {
                foreach (var user in _context.MyFriends)
                {
                    //if (user.isFriend == false) { continue; }
                    MailSender.SendObject(jsonProfileInfo, user.Email, _settings, "PutFriendsProfile");

                }
            }
            catch (Exception ex) 
            { }
            
            _context.MyProfile.Update(myprofile); 
            await _context.SaveChangesAsync(); 

            return Ok("Updated profile");
        }
    }
}
    