using Coffe_Order.Infrastructures.Middlewares;
using Coffe_Order.Infrastructures.ThirdParty.WeatherSerivce;
using Coffe_Order.Repositories.Coffee;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffe_Order.Infrastructures.Startup.ServicesExtensions
{
    public static class DIServicesExtension
    {
        public static IList<IServiceCollection> DIServicesExtensionRegistrer(this IServiceCollection services)
        {
            IList<IServiceCollection> listDIRegister = new List<IServiceCollection>
            {
                //INJECTION THE REPOSITORY HERE
                services.AddTransient<ICoffee, Coffee>(),
                services.AddSingleton<RequestHandle>(),
                services.AddTransient<IWeatherService,WeatherService>()
            };
            return listDIRegister;
        }
    }
}
