using System.Threading.Tasks;

namespace Coffe_Order.Infrastructures.Swagger
{
    public interface ISwaggerVersionConverter
    {
        Task<dynamic> ConvertAsync(string jsonString);
    }
}
