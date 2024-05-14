using System;
using ObiletCase.AppService.Contract.Model.Session;
using System.Threading.Tasks;
using ObiletCase.AppService.Contract.Model.BusLocation;

namespace ObiletCase.AppService.Contract.Service
{
	public interface IObiletService
	{
        Task<SessionResponseModel> GetSessionAsync();
        Task<BusLocationResponseModel> GetAvailableBussLocations(BusLocationSearchModel model);
    }
}

