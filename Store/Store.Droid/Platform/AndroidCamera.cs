
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Store.Domain;
using Xamarin.Forms.Platform.Android;
using Android.Hardware.Camera2;
using Plugin.CurrentActivity;
using Android.Provider;
using Java.IO;
using Android.Net;
using Xamarin.Forms;
using Android.Content.PM;
using Plugin.Media;
using Store.Droid.Activities;

namespace Store.Droid.Platform
{

    class AndroidCamera : ICamera
    {
        
        private readonly string PhotoDirectory = Environment.DirectoryPictures;
        private Context m_currentContext;
        private bool m_isCameraAvailable;

        public AndroidCamera() : base()
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