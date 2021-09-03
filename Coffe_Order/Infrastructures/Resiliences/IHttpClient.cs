using System.Net.Http;
using System.Threading.Tasks;

namespace Coffe_Order.Infrastructures.Resiliences
{
    public interface IHttpClient
    {
        Task<string> GetStringAsync(string uri);
        Task<T> PostAsync<T>(string uri, HttpContent data);
        Task<T> GetAsync<T>(string uri);
    }
}
