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
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.DataProtection.KeyManagement;


namespace SwissChatClient.Controllers
{
    public class ContactController : Controller
    {

        string baseUrl = "https://localhost:7163/Contact/";
        ApiHelper apiHelper = new ApiHelper();
        private ISessionHelpers _sessionHelpers;

        public ContactController(ISessionHelpers sessionHelpers)
        {

            _sessionHelpers = sessionHelpers;
        }

        [HttpGet]
        public async Task<IActionResult> Contacts(string id)
        {
            var token = GetSessionEliment(Convert.ToInt16(UserSession.Token));
            var (postResponseContent, postStatusCode) = await apiHelper.GetAsync(baseUrl + "contacts" + $"/{id}", token.ToString());
            if(postStatusCode==HttpStatusCode.OK)
            {
                var contacts = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ContactResponse>>(postResponseContent);

                return View("Index", contacts);
            }
         
            return View("~/Views/Home/Index.cshtml");
           
        }
     

        [HttpPost]
        public async Task<IActionResult> Create(string username)
        {
            var model = new CreateRequest();
            model.Username = username.Replace("'",string.Empty);  
            model.UserId = Guid.Parse(GetSessionEliment(Convert.ToInt16(UserSession.Id)));
            var token = GetSessionEliment(Convert.ToInt16(UserSession.Token));
            var (postResponseContent, postStatusCode) = await apiHelper.PostAsync(baseUrl + "addcontact", model,token);

            if (postStatusCode == HttpStatusCode.OK)
            {
              

                return RedirectToAction("Contacts", "Contact", new { id = model.UserId.ToString() });
            }

            else
            {
                var contactResponses = JsonConvert.DeserializeObject(postResponseContent); 
                return Json(new { response = false, message = contactResponses });
               // return Json(message);
            }



        }
       
        public async Task<IActionResult> SendMessage(string id)
        {
            HttpContext.Session.SetString("SessionKey", id);
            return View();

        }
        public async Task<IActionResult> ReceiveMessage()
        {

            return View();

        }
        public string GetSessionEliment(int iteration)
        {
           
            var session = _sessionHelpers.GetListParameterFromSession(HttpContext, "UserSession");
            List<string> sessionList = session;
           // int iteration = 4; // Desired iteration (1-based index)
            var token="";
            if (iteration >= 1 && iteration <= sessionList.Count)
            {
                token = sessionList[iteration - 1].ToString();
            }
            return token;
        }




    }
}
