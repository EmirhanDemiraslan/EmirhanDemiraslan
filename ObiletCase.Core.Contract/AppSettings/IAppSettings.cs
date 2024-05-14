using System;
namespace ObiletCase.Core.Contract.AppSettings
{
	public interface IAppSettings
	{
		public ApiSettings ApiSettings { get; set; }
	}

	public class ApiSettings
	{
		public string ApiBaseUrl { get; set; }
		public string ApiClientToken { get; set; }
    }
}

