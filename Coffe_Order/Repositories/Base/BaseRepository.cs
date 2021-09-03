using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffe_Order.Repositories.Base
{
    public class BaseRepository<T>
    {
        protected readonly ILogger<T> _logger;

        public BaseRepository(ILogger<T> logger)
        {
            _logger = logger;
        }
    }
}
