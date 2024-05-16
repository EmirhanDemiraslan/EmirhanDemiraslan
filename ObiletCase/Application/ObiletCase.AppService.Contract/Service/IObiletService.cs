using System;
using ObiletCase.AppService.Contract.Model.Session;
using System.Threading.Tasks;
using ObiletCase.AppService.Contract.Model.BusLocation;
using ObiletCase.AppService.Contract.Model.Journey;

namespace ObiletCase.AppService.Contract.Service
{
	public interface IObiletService
	{
        Task GetSessionAndSetToCacheAsync();
        Task<BusLocationResponseModel> GetAvailableBusLocations(BusLocationSearchModel model);
        Task<JourneyResponseModel> GetJourneysByParamsAsync(JourneyParamsModel model);
    }
}

