using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffe_Order.Infrastructures.ThirdParty.WeatherSerivce
{
    public class WeatherObject
    {
        public object Coord { get; set; }
        public object Weather { get; set; }
        public string Base { get; set; }
        public Temperature Main { get; set; }
        public int Vsisibility { get; set; }
        public object Wind { get; set; }
        public object Cloud { get; set; }
        public long Dt { get; set; }
        public object Sys { get; set; }
        public int Timezone { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cod { get; set; }
    }


    public class Temperature
    {
        public int Temp { get; set; }
        public int feels_like { get; set;}
        public int Temp_min { get; set; }
        public int Temp_max { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public int Sea_level { get; set; }
        public int Grnd_level { get; set; }
    }
}
