using System;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.IO;
using Store.Interface.Platform;

namespace Store.Ui.Common
{
    internal class Camera : ICamera
    {
        public bool IsTakePhotoSupported()
        {
            return CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported;
        }

        public async Task<byte[]> TakePhotoAsync()
        {
            var photoOptions = new StoreCameraMediaOptions()
            {
                Directory = "Receipts",
                Name = $"{DateTime.UtcNow}.jpg"
            };


            var photo = await CrossMedia.Current.TakePhotoAsync(photoOptions);
            var isPhotoTaken = (photo != null);
            if (isPhotoTaken)
            {
                using (var ms = new MemoryStream())
                {
                    using (var stream = photo.GetStream())
                    {
                        stream.CopyTo(ms);
                        return ms.ToArray();
                    }
                }
            }
            else
            {
                return null;
            }            
        }
    }
}
