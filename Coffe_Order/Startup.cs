using Coffe_Order.Infrastructures.Startup.PipelineExtensions;
using Coffe_Order.Infrastructures.Startup.ServicesExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Coffe_Order
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Startup(Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddEnvironmentVariables("coffee_")
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = configBuilder.Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGenericStartupServiceExtension(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseGenericPipelineExtension(env, Configuration);
        }
    }
}
