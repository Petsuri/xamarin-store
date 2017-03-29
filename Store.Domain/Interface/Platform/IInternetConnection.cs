
using System.Threading.Tasks;

namespace Store.Interface.Platform
{
    public interface IInternetConnection
    {
        bool IsAvailable();
        bool IsConnected();
        Task<bool> RequestConnection();
    }
}
