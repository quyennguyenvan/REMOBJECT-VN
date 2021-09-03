using System.Threading.Tasks;

namespace Coffe_Order.Infrastructures.Swagger
{
    public interface ISwaggerProxy
    {
        Task<string> GetSwaggerJsonAsync(string swaggerEndpoint, string upStreamTemplate, string downStreamTemplate);
    }
}
