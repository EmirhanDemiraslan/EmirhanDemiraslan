using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObiletCase.AppService.Contract.Service;

namespace ObiletCase.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IObiletService _obiletService;

        public HomeController(
            IObiletService obiletService)
        {
            _obiletService = obiletService;
        }

        public async Task<IActionResult> Index()
        {
            string sessionId = HttpContext.Session.GetString("SessionId");

            if (string.IsNullOrEmpty(sessionId))
            {
                var sessionResponse = await _obiletService.GetSessionAsync();
                HttpContext.Session.SetString("SessionId", sessionResponse.data.sessionid);
                HttpContext.Session.SetString("DeviceId", sessionResponse.data.deviceid);
            }

            return View();
        }
    }
}

