using Coffe_Order.Repositories.Coffee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffe_Order.Base
{
    [ApiController]
    public class AppBaseController : Controller
    {
        public ILogger Logger { get; set; }
        public IConfiguration Configuration { get; set; }
        public ICoffee Coffee { get; set; }
        public AppBaseController() { }
        public AppBaseController(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            Logger = loggerFactory.CreateLogger(this.GetType());
        }
    }
}
