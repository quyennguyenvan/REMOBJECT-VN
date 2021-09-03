using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace Coffe_Order.Infrastructures.Swagger
{
    public static class SwaggerProxyExtension
    {
        public static IApplicationBuilder UseSwaggerProxy(this IApplicationBuilder app)
        {
            var setting = app.ApplicationServices.GetService<IOptions<AppSettings>>().Value;
            var swaggerProxy = app.ApplicationServices.GetService<ISwaggerProxy>();
            var swaggerEndpoints = new Dictionary<string, string>();

            foreach (var config in setting.GroupSwaggerConfigs)
            {
                var swaggerPath = $"/swagger/{config.SwaggerName}.json";
                app.Map(swaggerPath, o =>
                {
                    o.Run(async x =>
                    {
                        var text = await swaggerProxy.GetSwaggerJsonAsync(config.SwaggerEndpoint, config.UpstreamTemplate, config.DownstreamTemplate);
                        await x.Response.WriteAsync(text);
                    });
                });

                swaggerEndpoints.Add(config.SwaggerName, swaggerPath);
            }
            app.UseSwaggerUI(c =>
            {
                foreach (var item in swaggerEndpoints)
                {
                    c.SwaggerEndpoint(item.Value, item.Key);
                }
            });
            return app;
        }

        /// <summary>
        /// Add swagger proxy to serivce container. Which allow you to call UseSwaggerProxy()
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerProxy(this IServiceCollection services)
        {
            services.AddTransient<ISwaggerProxy, SwaggerProxy>();
            services.AddTransient<ISwaggerVersionConverter, SwaggerVersionConverter>();
            return services;
        }
    }
}
