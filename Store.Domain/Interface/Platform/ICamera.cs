using System.Threading.Tasks;

namespace Store.Interface.Platform
{
    public interface ICamera
    {
        bool IsTakePhotoSupported();
        Task<byte[]> TakePhotoAsync();
    }
}
