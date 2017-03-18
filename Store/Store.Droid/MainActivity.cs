﻿
using Android.App;
using Android.Content.PM;
using Android.OS;

using Store.Ui;
using Microsoft.Practices.Unity;
using Store.Domain;

namespace Store.Droid
{
    [Activity(Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
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

