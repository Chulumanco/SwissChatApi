using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.Extensions.Options;
using SwissChatApi.Authorization;
using SwissChatApi.Services;
using SwissChatApi.Helpers;
using SwissChatApi.Model.Contacts;

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

        [HttpPost("addcontact")]
        public async Task<IActionResult> Create(CreateRequest model)
        {
            try

            {

                await _contactService.Create(model);

                return Ok(new { message = "Contact has been successfully added" });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpGet("contacts/{id}")]

        public async Task<IActionResult> GetAll(Guid id)
        {
            try
            {
                //id = Guid.Parse("3CC84A07-0130-4639-B97B-7EAAAD86D320");
                var users = await _contactService.GetUserContact(id);

                return Ok(users);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }
        // [HttpGet("contacts/{username}")]

        // public async Task<IActionResult> GetUserByName(string id)
        // {
        //     try
        //     {
        //         //id = Guid.Parse("3CC84A07-0130-4639-B97B-7EAAAD86D320");
        //         var users = await _contactService.GetUserByID(id);

        //         return Ok(users);
        //     }
        //     catch (AppException ex)
        //     {
        //         // return error message if there was an exception
        //         return BadRequest(new { message = ex.Message });
        //     }

        // }

        [HttpPost("removecontact")]
        public async Task<IActionResult> Remove(Guid id)
        {
            try
            {
                //id = Guid.Parse("3CC84A07-0130-4639-B97B-7EAAAD86D320");
                //var users = await _contactService.RemoveContact("Chulz", id);

                return Ok(id);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }

    }
}
