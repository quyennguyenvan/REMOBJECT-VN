using Coffe_Order.Infrastructures.MessageResponse;
using Coffe_Order.Infrastructures.ThirdParty.WeatherSerivce;
using Coffe_Order.Repositories.Base;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffe_Order.Repositories.Coffee
{
    public class Coffee : BaseRepository<Coffee>, ICoffee
    {
        private IWeatherService _weatherService { get; set; }

        public Coffee(ILogger<Coffee> logger, IWeatherService weatherService):base(logger)
        {
            _weatherService = weatherService;
            _logger.LogInformation($"{DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz")} - Access: {System.Reflection.Assembly.GetEntryAssembly().GetName()} ");
        }


        public async Task<MessageResponse>  GetCoffee()
        {
            _logger.LogInformation($"{DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz")} - Request brew coffee - OK");
            var temp = await _weatherService.GetCurrentTemperature();
            if(temp > 30)
            {
                return new MessageResponse
                {
                    Message = "Your refreshing iced coffee is ready",
                    Prepared = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz")
                };
            }
            return new MessageResponse
            {
                Message = "Your piping hot coffee is ready",
                Prepared = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz")
            };
        }
    }
}
