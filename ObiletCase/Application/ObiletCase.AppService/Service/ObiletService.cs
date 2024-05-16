using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using ObiletCase.AppService.Contract.Model.BusLocation;
using ObiletCase.AppService.Contract.Model.Journey;
using ObiletCase.AppService.Contract.Model.Session;
using ObiletCase.AppService.Contract.Service;
using ObiletCase.Core.Contract.AppSettings;
using ObiletCase.Core.Contract.HttpClient;
using ObiletCase.Core.Helper;

namespace ObiletCase.AppService.Service
{
    public class ObiletService : IObiletService
	{
        private readonly IAppSettings _appSettings;
        private readonly IHttpClientService _httpClientService;
        private readonly IMemoryCache _memoryCache;

        public ObiletService(
            IAppSettings appSettings,
            IHttpClientService httpClientService,
            IMemoryCache memoryCache)
        {
            _appSettings = appSettings;
            _httpClientService = httpClientService;
            _memoryCache = memoryCache;
        }

        public async Task GetSessionAndSetToCacheAsync()
		{
            if (_memoryCache.Get("sessionId") != null)
                return;

            var request = new SessionRequestModel();
			var requestStr = JsonConvert.SerializeObject(request);
            var httpResponse = await _httpClientService.PostStringWithClientTokenAsync($"{_appSettings.ApiSettings.ApiBaseUrl}/client/getsession", _appSettings.ApiSettings.ApiClientToken, requestStr);
            SessionResponseModel response = JsonConvert.DeserializeObject<SessionResponseModel>(httpResponse.ResponseJson);
            _memoryCache.Set("sessionId", response.data.sessionid);
            _memoryCache.Set("deviceId", response.data.deviceid);
        }

        public async Task<BusLocationResponseModel> GetAvailableBusLocations(BusLocationSearchModel model)
        {
            return await RetryHelper.RetryOnExceptionAsync(2, async () =>
            {
                var request = new BusLocationRequestModel();
                request.devicesession.sessionid = _memoryCache.Get<string>("sessionId");
                request.devicesession.deviceid = _memoryCache.Get<string>("deviceId");
                request.data = !string.IsNullOrEmpty(model.SearchText) ? model.SearchText : null;
                var requestStr = JsonConvert.SerializeObject(request);
                var httpResponse = await _httpClientService.PostStringWithClientTokenAsync($"{_appSettings.ApiSettings.ApiBaseUrl}/location/getbuslocations", _appSettings.ApiSettings.ApiClientToken, requestStr);

                if (!string.IsNullOrEmpty(httpResponse.ResponseJson) && httpResponse.ResponseJson.Contains("DeviceSessionError"))
                {
                    RemoveSessionAndDeviceId();
                    throw new Exception();
                }   
                    
                BusLocationResponseModel response = JsonConvert.DeserializeObject<BusLocationResponseModel>(httpResponse.ResponseJson);
                return response;
            }, GetSessionAndSetToCacheAsync);
        }

        public async Task<JourneyResponseModel> GetJourneysByParamsAsync(JourneyParamsModel model)
        {
            return await RetryHelper.RetryOnExceptionAsync(2, async () =>
            {
                var request = new JourneyRequestModel();
                request.devicesession.sessionid = _memoryCache.Get<string>("sessionId");
                request.devicesession.deviceid = _memoryCache.Get<string>("deviceId");
                request.data.originid = Convert.ToInt32(model.origin);
                request.data.destinationid = Convert.ToInt32(model.destination);
                request.data.departuredate = model.date;

                var requestStr = JsonConvert.SerializeObject(request);
                var httpResponse = await _httpClientService.PostStringWithClientTokenAsync($"{_appSettings.ApiSettings.ApiBaseUrl}/journey/getbusjourneys", _appSettings.ApiSettings.ApiClientToken, requestStr);

                if (!string.IsNullOrEmpty(httpResponse.ResponseJson) && httpResponse.ResponseJson.Contains("DeviceSessionError"))
                {
                    RemoveSessionAndDeviceId();
                    throw new Exception();
                }
                    
                JourneyResponseModel response = JsonConvert.DeserializeObject<JourneyResponseModel>(httpResponse.ResponseJson);
                return response;
            }, GetSessionAndSetToCacheAsync);
        }

        private void RemoveSessionAndDeviceId()
        {
            _memoryCache.Remove("sessionId");
            _memoryCache.Remove("deviceId");
        }
	}
}

