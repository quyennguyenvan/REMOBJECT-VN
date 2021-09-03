using Coffe_Order.Infrastructures.Helpers;
using Coffe_Order.Infrastructures.Resiliences;
using Coffe_Order.Infrastructures.Swagger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffe_Order.Infrastructures.Startup.ServicesExtensions
{
    public static class GenericServicesStartupExtension
    {
        public static void AddGenericStartupServiceExtension(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddHttpClient();

            services.AddHealthChecks();

            services.AddHttpContextAccessor();

            services.Configure<AppSettings>(configuration);

            services.AddTransient<IHttpClient, HttpClient>();

            services.DIServicesExtensionRegistrer();

            services.AddSwaggerProxy();

            services.AddControllers();
            services.AddMvc().AddNewtonsoftJson( o => {
                o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddStackExchangeRedisCache(o =>
            {
                o.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions
                {
                    SyncTimeout = 5000,
                    EndPoints =
                    {
                        configuration["Redis:Host"],configuration["Redis:Port"]
                    },
                    AbortOnConnectFail = true,
                    AllowAdmin = true,
                    Password = configuration["Redis:Password"]
                };
            });
        }
    }
}
