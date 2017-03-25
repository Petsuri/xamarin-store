using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Store.Droid
{
    internal class PhotoTakenEventArgs
    {
        
        public bool IsPhotoTaken { get; private set; }
        public byte[] Photo { get; private set; }

        public PhotoTakenEventArgs(byte[] photo)
        {
            this.Photo = photo;
            IsPhotoTaken = (photo != null && 0 < photo.Length);
        }

    }
}