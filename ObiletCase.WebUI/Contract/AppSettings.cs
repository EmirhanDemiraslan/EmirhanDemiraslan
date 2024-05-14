using System;
using ObiletCase.Core.Contract.AppSettings;

namespace ObiletCase.WebUI.Contract
{
	public class AppSettings : IAppSettings
	{
		public ApiSettings ApiSettings { get; set; }
	}
}

