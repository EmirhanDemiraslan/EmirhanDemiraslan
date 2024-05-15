using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ObiletCase.AppService.Contract.Model.Journey;
using ObiletCase.AppService.Contract.Service;
using ObiletCase.WebUI.Models;
using System;

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
			model.sessionId = HttpContext.Session.GetString("SessionId");
			model.deviceId = HttpContext.Session.GetString("DeviceId");

			var result = await _obiletService.GetJourneysByParamsAsync(model);
			var journeys = new List<JourneyIndexModel>();

			//foreach (var item in result.data.OrderBy(t => t.journey.departure))
			//{
			//             journeys.Add(new JourneyIndexModel
			//             {
			//                 DepartureTime = item.journey.departure.ToString(),
			//                 ArrivalTime = item.journey.arrival.ToString(),
			//                 Destination = item.journey.destination,
			//                 Origin = item.journey.origin,
			//                 Price = Convert.ToDecimal(item.journey.originalprice)
			//             });
			//         }

			Parallel.ForEach(result.data.OrderBy(t => t.journey.departure), x =>
			{
				journeys.Add(new JourneyIndexModel
				{
					DepartureTime = x.journey.departure.ToString(),
					ArrivalTime = x.journey.arrival.ToString(),
					Destination = x.journey.destination,
					Origin = x.journey.origin,
					Price = Convert.ToDecimal(x.journey.originalprice)
				});
			});

			return View(journeys);
        }
    }
}

