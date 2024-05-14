using System;
namespace ObiletCase.AppService.Contract.Model.BusLocation
{
	public class BusLocationSearchModel
	{
		public string SearchText { get; set; }
		public string SessionId { get; set; }
		public string DeviceId { get; set; }
    }
}

