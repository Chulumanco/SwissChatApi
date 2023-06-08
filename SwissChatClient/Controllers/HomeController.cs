using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using SwissChatClient.Models;
using System.Text;
using static SwissChatClient.Models.UserViewModel;

namespace SwissChatClient.Controllers
{
    public class HomeController : Controller
    {
     
        public HomeController()
        {
           
        }
      
        public IActionResult Index()
        {
            return View();
        }
    }
}
