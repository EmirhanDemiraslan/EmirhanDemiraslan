using Microsoft.Extensions.DependencyInjection;
using ObiletCase.AppService.Contract.Service;
using ObiletCase.AppService.Service;

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
        }
    }
}

