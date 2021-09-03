using Coffe_Order.Base;
using Coffe_Order.Infrastructures.Middlewares;
using Coffe_Order.Repositories.Coffee;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Coffe_Order.Controllers
{
    [ServiceFilter(typeof(RequestHandle))]
    [Route("v1/coffee")]
    public class CofferOrderController : AppBaseController
    {
        public CofferOrderController(ICoffee coffee)
        {
            Coffee = coffee;  
        }
        [HttpGet]
        [Route("brew-coffee")]
        public async Task<IActionResult> BrewCoffee()
        {   
            //"yyyy-MM-ddTHH:mm:ss"
            return Ok(await Coffee.GetCoffee());
        }
    }
}
