using System.Threading.Tasks;
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
            await _obiletService.GetSessionAndSetToCacheAsync();

            return View();
        }
    }
}

