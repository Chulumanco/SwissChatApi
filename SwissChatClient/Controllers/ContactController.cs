using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using SwissChatClient.Models;
using System.Text;
using static SwissChatClient.Models.UserViewModel;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using static SwissChatClient.Models.ContactViewModel;
using System.Linq;
using SwissChatClient.Helpers;
using System.Net.WebSockets;
using System;
using System.Reflection;
using System.Reflection.Metadata;
using System.Dynamic;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Xml.Linq;
using static SwissChatClient.Models.ChatsViewModel;

namespace SwissChatClient.Controllers
{
    public class ContactController : Controller
    {

        string baseUrl = "https://localhost:7163/Contact/";
        private readonly HttpClient _httpClientFactory;
        private ISessionHelpers _sessionHelpers;

        public ContactController(ISessionHelpers sessionHelpers)
        {

            _sessionHelpers = sessionHelpers;
        }

        [HttpGet]
        public async Task<IActionResult> Contacts(string id)
        {
            var token = GetObject(3);
            var contents = await ApiHelper.GetAsync(baseUrl + "contacts" + $"/{id}", token);

            var hasObject = HasProperty(contents);
            if (hasObject == "Authorized")
            {

                var newModel = GetObjects(contents);
                return View("Index", newModel);
            }
            if (hasObject == "Unauthorized")
            {
                ModelState.AddModelError("CustomError", "Invalid login attempt.");
                return View("~/Views/Home/Index.cshtml");
            }
           

            ModelState.AddModelError("CustomError", "Something went wrong.");
            return View("~/Views/Home/Index.cshtml");
           
        }
    
            


        
        [HttpPost]
        public async Task<JsonResult> Create(string username)
        {
            //var client = _httpClientFactory.CreateClient();
            var token = GetObject(3);

            var contents = await ApiHelper.PostAsync(baseUrl + "addcontact", username,token);
           
            return Json(contents);

        }
        [HttpGet]
        public async Task<IActionResult> GetUser(string username)
        {
           
            //var mysession = new List<string> { username };
            _sessionHelpers.Set(HttpContext.Session, "contact", username);
            return Json(new { result = true });

        }
        [HttpGet]
        public async Task<IActionResult> SendMessage()
        {
           var sessionData = _sessionHelpers.Get<string>(HttpContext.Session, "contact");
         
            ViewBag.SessionData = sessionData;

            return View();
            // return Json(model);
        }
        private IEnumerable<ContactResponse> GetObjects(string content)
        {
           if(content== "Unauthorized")
            {
                return null;
            }
       
            IEnumerable<ContactResponse> objList = JsonConvert.DeserializeObject<IEnumerable<ContactResponse>>(content);
            return objList;
          
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
    
        public static string HasProperty(string data)
        {
            var jObj = JsonConvert.DeserializeObject<ContactResponse[]>(data);
            //JObject jsonArray = JObject.Parse(data);

            //JObject jObject = new JObject(jsonArray.Children().Cast<JObject>().SelectMany(obj => obj.Properties()));

            //var model =
            var model = new ContactResponse() { };
            foreach (var item in jObj)
            {
                model.Id = item?.Id;

                model.Unauthorized = item?.Unauthorized;
            }

            if (model?.IsAuthenticated != null)
            {
                return "Unauthorized";
            }
           else if (model?.Id != null || model?.Id != Guid.Empty)
            {
               return "Authorized";
            }
            return "Other";
        }
        public T DeserializeJson<T>(string json)
        {
            try
            {
                // Attempt to deserialize as an array
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (JsonSerializationException)
            {
                try
                {
                    // Attempt to deserialize as a single object
                    var singleObjectArray = new[] { JsonConvert.DeserializeObject<T>(json) };
                    return singleObjectArray[0];
                }
                catch (JsonSerializationException ex)
                {
                    // Handle any deserialization errors
                    Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                    return default;
                }
            }
        }



    }
}
