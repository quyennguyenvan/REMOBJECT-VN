using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffe_Order
{
    public class AppSettings
    {
        public SwaggerConfiguration[] GroupSwaggerConfigs { get; set; }
        public WeatherConfiguration WeatherConfiguration { get; set; }
        public string GatewayUrl { get; set; }
    }

    public class SwaggerConfiguration
    {
        public string SwaggerName { get; set; }
        public string SwaggerEndpoint { get; set; }
        public string UpstreamTemplate { get; set; }
        public string DownstreamTemplate { get; set; }
    }

    public class WeatherConfiguration
    {
        public string Endpoint { get; set; }
        public string Location { get; set; }
        public string Unit { get; set; }
        public string APIKey { get; set; }
    }
}
