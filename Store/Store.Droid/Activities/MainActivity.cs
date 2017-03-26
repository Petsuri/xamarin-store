
using Android.App;
using Android.Content.PM;
using Android.OS;

using Store.Ui;
using Microsoft.Practices.Unity;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Store.Droid.Platform;
using Store.Interface.Platform;

namespace Store.Droid.Activities
{
    [Activity(Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
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


        }

        private void RegisterDependecies(IUnityContainer container)
        {
            container.RegisterType<IApplication, AndroidApplication>();
            container.RegisterType<INotifications, AndroidNotifications>();
            container.RegisterType<ICamera, AndroidCamera>();
        }
        
    }
}

