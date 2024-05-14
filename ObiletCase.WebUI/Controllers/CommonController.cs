using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObiletCase.AppService.Contract.Model.BusLocation;
using ObiletCase.AppService.Contract.Service;
using ObiletCase.WebUI.Models;

namespace ObiletCase.WebUI.Controllers
{
    public class CommonController : Controller
    {
        private readonly IObiletService _obiletService;

        public CommonController(
            IObiletService obiletService)
        {
            _obiletService = obiletService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAvailableLocations(string searchText)
        {
            var searchModel = new BusLocationSearchModel();
            searchModel.SearchText = !string.IsNullOrEmpty(searchText) ? searchText : null;
            searchModel.SessionId = HttpContext.Session.GetString("SessionId");
            searchModel.DeviceId = HttpContext.Session.GetString("DeviceId");

            var result = await _obiletService.GetAvailableBussLocations(searchModel);
            var optionList = new List<SelectOptionModel>();
            Parallel.ForEach(result.data.Take(10), x =>
            {
                optionList.Add(new SelectOptionModel
                {
                    Value = x.id.ToString(),
                    Text = x.name
                });
            });

            return Ok(optionList);
        }
    }
}

