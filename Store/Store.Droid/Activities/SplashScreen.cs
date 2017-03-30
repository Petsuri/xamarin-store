
using Android.App;
using Android.Content;
using Android.OS;
using Android.Content.PM;

namespace Store.Droid.Activities
{
    [Activity(
        Label = "Petrin kauppa", 
        Icon = "@drawable/petrinkauppa", 
        Theme = "@style/MyTheme.Splash", 
        MainLauncher = true, 
        NoHistory = true,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        protected override void OnResume()
        {
            base.OnResume();

            var startApplication = new Intent(this, typeof(MainActivity));
            StartActivity(startApplication);

        }
    }
}