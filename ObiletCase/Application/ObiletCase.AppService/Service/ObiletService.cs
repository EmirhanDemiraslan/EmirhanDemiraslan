using System;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl.Http;
using Newtonsoft.Json;
using ObiletCase.AppService.Contract.Model.BusLocation;
using ObiletCase.AppService.Contract.Model.Journey;
using ObiletCase.AppService.Contract.Model.Session;
using ObiletCase.AppService.Contract.Service;
using ObiletCase.Core.Contract.AppSettings;

namespace ObiletCase.AppService.Service
{
	public class ObiletService : IObiletService
	{
        private readonly IAppSettings _appSettings;

        public ObiletService(
            IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task<SessionResponseModel> GetSessionAsync()
		{
            SessionResponseModel response = null;

            var request = new SessionRequestModel();
			var requestJson = JsonConvert.SerializeObject(request);

            using (var cli = new FlurlClient(_appSettings.ApiSettings.ApiBaseUrl))
            {
                response = await cli.Request("/client/getsession")
                                 .WithHeader("Content-Type", "application/json")
                                 .WithHeader("Authorization", $"Basic {_appSettings.ApiSettings.ApiClientToken}")
                                 .PostStringAsync(requestJson)
                                 .ReceiveJson<SessionResponseModel>();
            }

            return response;
		}

        public async Task<BusLocationResponseModel> GetAvailableBusLocations(BusLocationSearchModel model)
        {
            BusLocationResponseModel response = null;

            var request = new BusLocationRequestModel();
            request.devicesession.sessionid = model.SessionId;
            request.devicesession.deviceid = model.DeviceId;
            request.data = !string.IsNullOrEmpty(model.SearchText) ? model.SearchText : null;

            var requestJson = JsonConvert.SerializeObject(request);

            using (var cli = new FlurlClient(_appSettings.ApiSettings.ApiBaseUrl))
            {
                response = await cli.Request("/location/getbuslocations")
                                         .WithHeader("Content-Type", "application/json")
                                         .WithHeader("Authorization", $"Basic {_appSettings.ApiSettings.ApiClientToken}")
                                         .PostStringAsync(requestJson)
                                         .ReceiveJson<BusLocationResponseModel>();
            }

            return response;
        }

        public async Task<JourneyResponseModel> GetJourneysByParamsAsync(JourneyParamsModel model)
        {
            JourneyResponseModel response = null;

            var request = new JourneyRequestModel();
            request.devicesession.sessionid = model.sessionId;
            request.devicesession.deviceid = model.deviceId;
            request.data.originid = Convert.ToInt32(model.origin);
            request.data.destinationid = Convert.ToInt32(model.destination);
            request.data.departuredate = model.date;

            var requestJson = JsonConvert.SerializeObject(request);

            using (var cli = new FlurlClient(_appSettings.ApiSettings.ApiBaseUrl))
            {
                response = await cli.Request("/journey/getbusjourneys")
                                         .WithHeader("Content-Type", "application/json")
                                         .WithHeader("Authorization", $"Basic {_appSettings.ApiSettings.ApiClientToken}")
                                         .PostStringAsync(requestJson)
                                         .ReceiveJson<JourneyResponseModel>();
            }

            return response;
        }
	}
}

