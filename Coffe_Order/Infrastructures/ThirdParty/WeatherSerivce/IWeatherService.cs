using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffe_Order.Infrastructures.ThirdParty.WeatherSerivce
{
    public interface IWeatherService:IDisposable
    {
        Task<int> GetCurrentTemperature();
    }
}
