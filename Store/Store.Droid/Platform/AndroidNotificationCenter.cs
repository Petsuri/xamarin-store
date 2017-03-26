using Android.App;
using Android.Content;
using Store.Model;
using Android.Graphics;
using Store.Interface.Platform;

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

            long[] vibrationPattern = { 100, 300, 500, 700, 1000 };

            var notification = new Notification.Builder(currentContext)
                .SetStyle(pictureStyle)
                .SetPriority((int)NotificationPriority.High)
                .SetCategory(Notification.CategoryPromo)
                .SetSmallIcon(Resource.Drawable.ic_collections_bookmark_black_18dp)
                .SetVisibility(NotificationVisibility.Public)
                .SetLargeIcon(appIcon)
                .SetContentText("Kirja ostettu")
                .SetContentTitle(book.Name)
                .SetVibrate(vibrationPattern)
                .Build();
            

            var manager = currentContext.GetSystemService(Context.NotificationService) as NotificationManager;
            manager.Notify(0, notification);

        }
        

    }
}