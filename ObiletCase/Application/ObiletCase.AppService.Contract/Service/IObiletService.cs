using System;
using ObiletCase.AppService.Contract.Model.Session;
using System.Threading.Tasks;

namespace ObiletCase.AppService.Contract.Service
{
	public interface IObiletService
	{
        Task<SessionResponseModel> GetSessionAsync();
    }
}

