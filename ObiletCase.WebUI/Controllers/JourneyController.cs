using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObiletCase.AppService.Contract.Service;

namespace ObiletCase.WebUI.Controllers
{
	public class JourneyController : Controller
	{
		private readonly IObiletService _obiletService;

		public JourneyController(
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
				sessionId = sessionResponse.data.sessionid;
                HttpContext.Session.SetString("SessionId", sessionId);
            }

            return View();
        }

        public async Task<IActionResult> Results()
        {
            string sessionId = HttpContext.Session.GetString("SessionId");

            if (string.IsNullOrEmpty(sessionId))
            {
                var sessionResponse = await _obiletService.GetSessionAsync();
                sessionId = sessionResponse.data.sessionid;
                HttpContext.Session.SetString("SessionId", sessionId);
            }

            return View();
        }
    }
}

