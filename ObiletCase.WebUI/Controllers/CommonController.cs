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

            var result = await _obiletService.GetAvailableBusLocations(searchModel);
            var optionList = new List<SelectOptionModel>();

            foreach (var item in result.data.Where(t => t.id.HasValue))
            {
                optionList.Add(new SelectOptionModel
                {
                    id = item.id.Value,
                    name = item.name
                });
            }
            //Parallel.ForEach(result.data.Where(t => t.id.HasValue).Take(10), x =>
            //{
            //    optionList.Add(new SelectOptionModel
            //    {
            //        id = x.id.Value,
            //        name = x.name
            //    });
            //});

            return Ok(optionList);
        }
    }
}

