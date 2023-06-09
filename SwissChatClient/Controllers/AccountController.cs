using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static SwissChatClient.Models.UserViewModel;
using System.Net.Http;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Xml.Linq;
using SwissChatClient.Helpers;
using System.Security.Policy;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Reflection;

namespace SwissChatClient.Controllers
{
    public class AccountController : Controller
    {
        ApiHelper apiHelper = new ApiHelper();
        string baseUrl = "https://localhost:7163/Users/";
        //private readonly HttpClient _httpClientFactory;
        private ISessionHelpers _sessionHelpers;
       
        public AccountController(ISessionHelpers sessionHelpers)
        {
            
            _sessionHelpers = sessionHelpers;
        }

        [HttpPost]
        public async Task<IActionResult> Login(Authenticate model)
        {
 

                var (postResponseContent, postStatusCode) = await apiHelper.PostAsync(baseUrl + "authenticate", model);
                if (postStatusCode == HttpStatusCode.OK)
                {
               
                      var user = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthenticateResponse>(postResponseContent);
                       SetSessionList(user);
                    return RedirectToAction("Contacts", "Contact", new { id = user.Id.ToString() });
                }
                else
                {
                    ModelState.AddModelError("CustomError", "Invalid login attempt.");
                    return View("~/Views/Home/Index.cshtml");
                }
                //var bodyJson = await request.Content.ReadAsStringAsync();
               
     

        }
        [HttpGet]
        public IActionResult Register()
        {
            try
            {

                return View("~/Views/Home/Register.cshtml");
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return View(new { message = ex.Message });
            }


        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest model)
        {

            var (postResponseContent, postStatusCode) = await apiHelper.PostAsync(baseUrl + "register", model);
            if (postStatusCode == HttpStatusCode.OK)
            {

              
              
                return View("~/Views/Home/Index.cshtml");
            }
            else
            {
                ModelState.AddModelError("CustomError", "Registration unsuccessful, please try again later.");
                return View("~/Views/Home/Index.cshtml");
            }
           

        }


        public void SetSessionList(AuthenticateResponse model)
        {
         
        // Set a list of strings in session
        var mysession = new List<string> {model.Id.ToString(), model.FirstName,  model.LastName, model.Username, model.Token, model.IsAuthenticated.ToString() };
        
          _sessionHelpers.SetListParameterInSession(HttpContext, "UserSession", mysession);
          
        }

     


    }
}






    

