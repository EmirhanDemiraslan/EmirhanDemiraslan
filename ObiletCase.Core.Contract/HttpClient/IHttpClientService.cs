using System;
using ObiletCase.Core.Contract.Model;
using System.Threading.Tasks;

namespace ObiletCase.Core.Contract.HttpClient
{
	public interface IHttpClientService
	{
        Task<BaseResponse> PostStringWithClientTokenAsync(string url, string clientToken, string argStringBody);
    }
}

