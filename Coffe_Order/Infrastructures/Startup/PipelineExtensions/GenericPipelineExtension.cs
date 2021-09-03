using Coffe_Order.Infrastructures.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffe_Order.Infrastructures.Startup.PipelineExtensions
{
    public static class GenericPipelineExtension
    {
        public static void UseGenericPipelineExtension(this IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwaggerProxy();

            app.UseRouting();

            app.UseHealthChecks("/healthcheck");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", context =>
                {
                    context.Response.Redirect("/swagger");
                    return Task.FromResult(0);
                });
                endpoints.MapControllers();
            });
        }
    }
}
