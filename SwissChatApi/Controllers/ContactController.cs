using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.Extensions.Options;
using SwissChatApi.Authorization;
using SwissChatApi.Services;
using SwissChatApi.Helpers;
using SwissChatApi.Model.Users;

namespace SwissChatApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]

    public class ContactController : ControllerBase
    {

        private IContactService _contactService;
     
        private readonly AppSettings _appSettings;

        public ContactController(
            IContactService contactService,
      
            IOptions<AppSettings> appSettings)
        {
            _contactService = contactService;
            _appSettings = appSettings.Value;
        }
        [AllowAnonymous]
        [HttpPost("addcontact")]
        public async Task<IActionResult> Create(string username)
        {
            try

            {
                //Don't allow user to add themselves ascontacts
                username = "Chulz";

                //var email = User.FindFirst("sub")?.Value;
                // create contact
              await  _contactService.Create(username);
             
                return Ok(new { message = "Contact has been successfully added" });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }
        [AllowAnonymous]
        [HttpGet("contacts")]
        public async Task<IActionResult> GetAll(Guid id)
        {
            try
            {
                //id = Guid.Parse("3CC84A07-0130-4639-B97B-7EAAAD86D320");
              var users =  await _contactService.GetUserContact(id);

                return Ok(users);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }
        [AllowAnonymous]
        [HttpPost("removecontact")]
        public async Task<IActionResult> Remove(Guid id)
        {
            try
            {
                //id = Guid.Parse("3CC84A07-0130-4639-B97B-7EAAAD86D320");
                var users = await _contactService.RemoveContact("Chulz", id);

                return Ok(users);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }

    }
}
