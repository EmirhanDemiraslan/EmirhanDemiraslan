using System;
namespace ObiletCase.WebUI.Models
{
	public class JourneyIndexModel
	{
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public decimal Price { get; set; }
    }
}

