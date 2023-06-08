using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static SwissChatClient.Models.UserViewModel;
using System.Net.Http;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Xml.Linq;
using SwissChatClient.Helpers;

namespace SwissChatClient.Controllers
{
    public class AccountController : Controller
    {
        string baseUrl = "https://localhost:7163/Users/";
        //private readonly HttpClient _httpClientFactory;
        private ISessionHelpers _sessionHelpers;
        
        public AccountController(ISessionHelpers sessionHelpers )
        {
           //_httpClientFactory = new HttpClient();
           // _httpClientFactory.BaseAddress = baseUrl;
            _sessionHelpers = sessionHelpers;
        }

        [HttpPost]
        public async Task<IActionResult> Login(Authenticate model)
        {
            try
            {
             
         
                var contents = await ApiHelper.PostAsync(baseUrl + "authenticate", model, null);
                
                //var res = GetObjects(contents);
               

                return RedirectToAction("Contacts", "Contact", new { id = res.Id });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return View(new { message = ex.Message });
            }
     

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
            try
            {


                var contents = await ApiHelper.PostAsync(baseUrl + "register", model, null);
                //var res = GetObjects(contents);


                //return RedirectToAction("Login", "");
                return View("~/Views/Home/Index.cshtml");
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return View(new { message = ex.Message });
            }


        }

        private AuthenticateResponse GetObjects(string content)
        {
          ///  dynamic json = JsonConvert.DeserializeObject(content);
            var emp = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthenticateResponse>(content);
            SetSessionList(emp);
            return emp;
        }
        public void SetSessionList(AuthenticateResponse model)
        {
            // Set a list of strings in session
            var mysession = new List<string> {model.Username, model.FirstName, model.LastName, model.Token };
        
          _sessionHelpers.SetList(HttpContext.Session, "UserSession", mysession);
          
        }

        public List<string> GetSessionList()
        {
            // Retrieve the list from session
            var session = _sessionHelpers.GetList<string>(HttpContext.Session, "UserSession");
            return session;
        }
        private string GetObject(int obj)
        {
            var token = GetSessionList();
            var i = token.ElementAt(obj);
            return i;

        }


    }
}






    

