
using Android.App;
using Android.Content.PM;
using Android.OS;

using Store.Ui;
using Microsoft.Practices.Unity;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Store.Droid.Platform;
using Store.Interface.Platform;
using Store.LocalDatabase.Connection;
using Android.Content;
using Android.Net;

namespace Store.Droid.Activities
{
    [Activity(
        Theme = "@style/MainTheme", 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Forms.Init(this, bundle);
            LoadApplication(new App());

            RegisterDependecies(App.Container);

            RegisterReceiver(
                new AndroidInternetConnection(),
                new IntentFilter(ConnectivityManager.ConnectivityAction));

        }

        private void RegisterDependecies(IUnityContainer container)
        {
            container.RegisterType<ICamera, AndroidCamera>();
            container.RegisterType<IApplication, AndroidApplication>();
            container.RegisterType<INotifications, AndroidNotifications>();
            container.RegisterType<IFileInformation, AndroidFileInformation>();
            container.RegisterType<IInternetConnection, AndroidInternetConnection>();

            var fileInformation = container.Resolve<IFileInformation>();
            container.RegisterInstance<IDatabase>(new Database(fileInformation));
        }
        
    }
}

