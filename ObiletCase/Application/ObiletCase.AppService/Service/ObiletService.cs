using System;
using System.Threading.Tasks;
using Flurl.Http;
using Newtonsoft.Json;
using ObiletCase.AppService.Contract.Model.Session;
using ObiletCase.AppService.Contract.Service;

namespace ObiletCase.AppService.Service
{
	public class ObiletService : IObiletService
	{
		private const string ApiClientToken = "JEcYcEMyantZV095WVc3G2JtVjNZbWx1";
        public const string ApiUrl = "https://v2-api.obilet.com/api";

        public ObiletService()
        {
        }

        public async Task<SessionResponseModel> GetSessionAsync()
		{
            SessionResponseModel response = null;

            var request = new SessionRequestModel();
			var requestJson = JsonConvert.SerializeObject(request);

            using (var cli = new FlurlClient(ApiUrl))
            {
                response = await cli.Request("/client/getsession")
                                 .WithHeader("Content-Type", "application/json")
                                 .WithHeader("Authorization", "Basic JEcYcEMyantZV095WVc3G2JtVjNZbWx1")
                                 .PostStringAsync(requestJson)
                                 .ReceiveJson<SessionResponseModel>();
            }

            return response;
		}

		//busslocation...
	}
}

