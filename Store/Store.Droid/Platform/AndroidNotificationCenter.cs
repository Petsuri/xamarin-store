
using System;
using Android.App;
using Android.Content;
using Plugin.CurrentActivity;
using Store.Domain;
using Store.Model;
using Android.Graphics;

namespace Store.Droid
{
    class AndroidNotifications : INotifications
    {
        public void BookPurchased(Book book)
        {

            var currentContext = Application.Context;
            var appIcon = BitmapFactory.DecodeResource(currentContext.Resources, Resource.Drawable.petrinkauppa);

            var pictureStyle = new Notification.BigPictureStyle();
            pictureStyle.BigPicture(BitmapFactory.DecodeByteArray(book.Image, 0, book.Image.Length));
            pictureStyle.BigLargeIcon(appIcon);
            pictureStyle.SetBigContentTitle(book.Name);
            pictureStyle.SetSummaryText(string.Format("Ostettu hintaan {1} €", book.Name, book.Price));

            var notification = new Notification.Builder(currentContext)
                .SetStyle(pictureStyle)
                .SetPriority((int)NotificationPriority.High)
                .SetCategory(Notification.CategoryPromo)
                .SetSmallIcon(Resource.Drawable.ic_collections_bookmark_black_18dp)
                .SetLargeIcon(appIcon)
                .SetContentText("Kirja ostettu")
                .SetContentTitle(book.Name)
                .Build();
            

            var manager = currentContext.GetSystemService(Context.NotificationService) as NotificationManager;
            manager.Notify(0, notification);

        }
        

    }
}