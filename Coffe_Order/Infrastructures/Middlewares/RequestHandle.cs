using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffe_Order.Infrastructures.Middlewares
{
    public class RequestHandle: Attribute, IAsyncActionFilter
    {
        private IHttpContextAccessor _httpContextAccessor { get; set; }
        private IDistributedCache _distributedCache { get; set; }
        private IConfiguration _configuration { get; set; }
        private  ILogger _logger { get; set; }
        
        private int _maxQuotaService { get; set; }

        public RequestHandle(IHttpContextAccessor httpContextAccessor, IDistributedCache distributedCache, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _distributedCache = distributedCache;
            _configuration = configuration;
            _logger = loggerFactory.CreateLogger<RequestHandle>();
            _maxQuotaService = int.Parse(_configuration["ServiceQuota:Max"]);
            _logger.LogInformation($"{DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz")} - Access: {System.Reflection.Assembly.GetEntryAssembly().GetName()} ");

        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var conditionCheck = context.HttpContext.Request.Path.Value.Contains("brew-coffee") && context.HttpContext.Request.Method.Equals("GET");
            _logger.LogInformation($"{DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz")} - Condition check: {conditionCheck} - OK");
            if (conditionCheck)
            {
                _logger.LogInformation($"{DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz")} - Accessing REDIS - OK");
                //get the value in redis cache
                var totalRequest = await _distributedCache.GetStringAsync("totalRequest");
                if(!string.IsNullOrEmpty(totalRequest))
                {
                    int currentIndex = int.Parse(totalRequest);
                    if (currentIndex > _maxQuotaService)
                    {
                        _logger.LogInformation($"{DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz")} - Out of servicce - OK");
                        ContentResult contentResult = new ContentResult();
                        contentResult.ContentType = "application/json";
                        contentResult.Content = "Service Unavailable";
                        contentResult.StatusCode = 503;
                        context.Result = contentResult;
                        return;
                    }
                    //add data into cache
                    currentIndex += 1;
                    await _distributedCache.SetStringAsync("totalRequest", currentIndex.ToString());
                    _logger.LogInformation($"{DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz")} - Add the request - OK");
                }
                else
                {
                    await _distributedCache.SetStringAsync("totalRequest", "1");
                    _logger.LogInformation($"{DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz")} - Write index first request - OK");
                }

            }
            await next();
        }
    }
}
