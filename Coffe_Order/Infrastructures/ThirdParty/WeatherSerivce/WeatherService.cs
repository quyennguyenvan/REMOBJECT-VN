using Coffe_Order.Infrastructures.Helpers;
using Coffe_Order.Infrastructures.Resiliences;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Coffe_Order.Infrastructures.ThirdParty.WeatherSerivce
{
    public class WeatherService : IWeatherService
    {
        private ILogger<WeatherService> _logger { get; set; }
        private readonly IHttpClient _httpClient;
        private IOptions<AppSettings> _options { get; set; }
        public WeatherService(IOptions<AppSettings> options, IHttpClient httpClient, ILoggerFactory loggerFactory) 
        {
            _httpClient = httpClient;
            _options = options;
            _logger = loggerFactory.CreateLogger<WeatherService>();
            _logger.LogInformation($"{DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz")} - Access: {System.Reflection.Assembly.GetEntryAssembly().GetName()}");
        }

        public async Task<int> GetCurrentTemperature()
        {
            try
            {
                _logger.LogInformation($"{DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz")} - Request Weather Service - OK");
                var endpoint = _options.Value.WeatherConfiguration.Endpoint;
                var location = _options.Value.WeatherConfiguration.Location;
                var unit = _options.Value.WeatherConfiguration.Unit;
                var apiKey = _options.Value.WeatherConfiguration.APIKey;
                var urlBinding = $"https://{endpoint}?q={location}&units={unit}&appid={apiKey}";
                var jsonString = await _httpClient.GetStringAsync(urlBinding);
                var json = JsonHelper.Deserialize<WeatherObject>(jsonString);

                return json.Main.Temp;
            }
            catch(Exception ex)
            {
                _logger.LogInformation($"{DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz")} - Request Weather Service - Exception: ${ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return 0;
            }
        }

        public void Dispose()
        {
            //NOTHING TO DO
        }
    }
}
