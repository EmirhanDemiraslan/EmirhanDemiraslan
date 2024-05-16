using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ObiletCase.AppService.Contract.Model.Journey;
using ObiletCase.AppService.Contract.Service;
using ObiletCase.WebUI.Models;
using System;
using System.Globalization;

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

		public async Task<IActionResult> Index(JourneyParamsModel model)
		{
			var result = await _obiletService.GetJourneysByParamsAsync(model);
            //if (result.status.Contains("DeviceSessionError"))
            //{
            //    return Redirect("/Home/Index?sessionError=true");
            //}

            var journeys = new List<JourneyIndexModel>();
			var cultureInfo = new CultureInfo("tr-TR");

            foreach (var item in result.data.OrderBy(t => t.journey.departure))
			{
				journeys.Add(new JourneyIndexModel
				{
					DepartureTime = item.journey.departure.ToString("t", cultureInfo),
					ArrivalTime = item.journey.arrival.ToString("t", cultureInfo),
					Destination = item.journey.destination,
					Origin = item.journey.origin,
					Price = Convert.ToDecimal(item.journey.originalprice)
				});
			}

			ViewBag.Origin = model.originName;
			ViewBag.Destination = model.destinationName;
			ViewBag.Date = DateTime.ParseExact(model.date, "yyyy-MM-dd", cultureInfo).ToString("dd MMMM dddd", cultureInfo);

   //         var orederedItems = result.data.OrderBy(t => t.journey.departure).ToList();
   //         Parallel.ForEach(orederedItems, x =>
			//{
			//	journeys.Add(new JourneyIndexModel
			//	{
			//		DepartureTime = x.journey.departure.ToString(),
			//		ArrivalTime = x.journey.arrival.ToString(),
			//		Destination = x.journey.destination,
			//		Origin = x.journey.origin,
			//		Price = Convert.ToDecimal(x.journey.originalprice)
			//	});
			//});

			return View(journeys);
        }
    }
}

