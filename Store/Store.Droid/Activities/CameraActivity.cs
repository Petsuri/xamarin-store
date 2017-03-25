
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Android.Provider;
using System.IO;
using Android.Net;

namespace Store.Droid.Activities
{
    [Activity]
    class CameraActivity : Activity
    {

        internal static event System.EventHandler<PhotoTakenEventArgs> PhotoTaken;

        private Uri m_photoFileUri;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            m_photoFileUri = CreateNewPhotoFileUri();

            var i = new Intent(MediaStore.ActionImageCapture);
            i.PutExtra(MediaStore.ExtraOutput, m_photoFileUri);

            StartActivityForResult(i, 0);

        }

        private Uri CreateNewPhotoFileUri()
        {

            var photoName = "IMG_" + System.DateTime.Now.Ticks + ".jpg";
            var directory = new Java.IO.File(Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures), string.Empty);
            if (!directory.Exists())
            {
                if (!directory.Mkdir())
                {
                    throw new IOException("Can't create folder for photos");
                }
            }

            return Uri.FromFile(new Java.IO.File(Path.Combine(directory.Path, photoName)));

        }


        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            byte[] photo;
            if (resultCode == Result.Ok)
            {
                photo = File.ReadAllBytes(m_photoFileUri.Path);
            }
            else
            {
                photo = null;
            }

            PhotoTaken?.Invoke(this, new PhotoTakenEventArgs(photo));
            Finish();
        }

    }
}