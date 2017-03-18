using System.Threading.Tasks;

namespace Store.Domain
{
    public interface ICamera
    {
        bool IsTakePhotoSupported();
        Task<byte[]> TakePhotoAsync();
    }
}
