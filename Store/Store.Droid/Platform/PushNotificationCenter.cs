
using Android.App;
using Android.Content;
using Plugin.CurrentActivity;
using Store.Domain;

namespace Store.Droid
{
    class PushNotificationCenter : INotificationCenter
    {
        public void Publish(string title, string message)
        {
            var currentContext = Application.Context;
            var manager = currentContext.GetSystemService(Context.NotificationService) as NotificationManager;

            var builder = new Notification.Builder(currentContext);
            var notification = builder.SetContentTitle(title)
                                      .SetContentText(message)
                                      .SetSmallIcon(Resource.Drawable.ic_collections_bookmark_black_18dp)
                                      .Build();

            manager.Notify(0, notification);
        }
         
    }
}