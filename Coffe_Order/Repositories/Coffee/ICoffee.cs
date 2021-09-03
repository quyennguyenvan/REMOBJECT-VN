using Coffe_Order.Infrastructures.MessageResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffe_Order.Repositories.Coffee
{
    public interface ICoffee
    {
        Task<MessageResponse> GetCoffee();
    }
}
