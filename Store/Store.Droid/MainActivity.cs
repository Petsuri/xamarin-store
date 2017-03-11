using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Store.Ui;
using Microsoft.Practices.Unity;
using Store.Domain;

namespace Store.Droid
{
    [Activity(Label = "Petrin kauppa", Icon = "@drawable/petrinkauppa", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

            RegisterDependecies(App.Container);


        }

        private void RegisterDependecies(IUnityContainer container)
        {
            container.RegisterType<IApplication, AndroidApplication>();
        }
        
    }
}

