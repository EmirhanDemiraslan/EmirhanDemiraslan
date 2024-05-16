using System.Threading.Tasks;
using Flurl.Http;
using Microsoft.Extensions.Caching.Memory;
using ObiletCase.Core.Contract.HttpClient;
using ObiletCase.Core.Contract.Model;

namespace ObiletCase.Core.HttpClient
{
    public class HttpClientService : IHttpClientService
    {
        public HttpClientService()
		{
		}

        public async Task<BaseResponse> PostStringWithClientTokenAsync(string url, string clientToken, string argStringBody)
		{
            BaseResponse response = new BaseResponse();

            using (var cli = new FlurlClient())
            {
                response.ResponseJson = await cli.Request(url)
                                 .WithHeader("Content-Type", "application/json")
                                 .WithHeader("Authorization", $"Basic {clientToken}")
                                 .PostStringAsync(argStringBody)
                                 .ReceiveString();
            }

            return response;
        }
    }
}

