using System.Threading.Tasks;
using Android.Content;
using Android.OS;
using Android.Content.PM;
using Store.Droid.Activities;
using Store.Interface.Platform;

namespace Store.Droid.Platform
{

    class AndroidCamera : ICamera
    {
        
        private readonly string PhotoDirectory = Environment.DirectoryPictures;
        private Context m_currentContext;
        private bool m_isCameraAvailable;

        public AndroidCamera()
        {
            var currentContext = Android.App.Application.Context;
            m_currentContext = currentContext;
            m_isCameraAvailable = currentContext.PackageManager.HasSystemFeature(PackageManager.FeatureCamera);
        }

        public bool IsTakePhotoSupported()
        {
            return m_isCameraAvailable;
        }

        public async Task<byte[]> TakePhotoAsync()
        {
            if (!IsTakePhotoSupported())
            {
                throw new System.InvalidOperationException("Camera is not available for device");
            }
            
            var i = new Intent(m_currentContext, typeof(CameraActivity));
            i.SetFlags(ActivityFlags.NewTask);
            m_currentContext.StartActivity(i);

            byte[] photo = null;
            bool isTakePhotoActivityFinished = false;

            System.EventHandler<PhotoTakenEventArgs> handler = null;
            handler = (sender, e) =>
            {
                photo = e.Photo;
                isTakePhotoActivityFinished = true;

                CameraActivity.PhotoTaken -= handler;
            };

            CameraActivity.PhotoTaken += handler;

            while (!isTakePhotoActivityFinished)
            {
                await Task.Delay(50);
            }

            return photo;

        }
        
    }
}