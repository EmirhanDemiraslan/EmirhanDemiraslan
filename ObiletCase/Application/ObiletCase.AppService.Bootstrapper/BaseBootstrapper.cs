using System;
using Microsoft.Extensions.DependencyInjection;
using ObiletCase.AppService.Contract.Service;
using ObiletCase.AppService.Service;
using ObiletCase.Core.Contract.HttpClient;
using ObiletCase.Core.HttpClient;

namespace ObiletCase.AppService.Bootstrapper
{
	public class BaseBootstrapper
	{
        private readonly IServiceCollection services;

        public BaseBootstrapper(
            IServiceCollection services)
        {
            this.services = services;

            Install();
        }

        private void Install()
        {
            services.AddTransient<IObiletService, ObiletService>();
            services.AddTransient<IHttpClientService, HttpClientService>();
            //services.AddTransient<Func<IObiletService>>(provider => () => provider.GetRequiredService<IObiletService>());
        }
    }
}

