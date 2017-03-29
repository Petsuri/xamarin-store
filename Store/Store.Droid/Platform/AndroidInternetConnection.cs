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
using Store.Interface.Platform;
using Android.Net;
using System.Threading.Tasks;

namespace Store.Droid.Platform
{
    class AndroidInternetConnection : IInternetConnection
    {

        private Context m_currentContext;

        public AndroidInternetConnection()
        {
            m_currentContext = Application.Context;
        }

        public bool IsAvailable()
        {
            var activeNetwork = GetActiveNetworkInformation();
            return activeNetwork != null && activeNetwork.IsAvailable;
        }

        public bool IsConnected()
        {

            var activeNetwork = GetActiveNetworkInformation();
            return activeNetwork != null && activeNetwork.IsConnected;


        }

        private NetworkInfo GetActiveNetworkInformation()
        {
            var manager = (ConnectivityManager)m_currentContext.GetSystemService(Context.ConnectivityService);
            return manager.ActiveNetworkInfo;
        }

        public async Task<bool> RequestConnection()
        {
            bool? isAccepted = null;

            var requestConnection = new AlertDialog.Builder(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity)
                .SetCancelable(false)               
                .SetTitle("Petrin kauppa vaatii internetin")
                .SetMessage("Siirry laittamaan internet p‰‰lle")
                .SetPositiveButton("OK", new EventHandler<DialogClickEventArgs>((sender, e) =>
                {

                    Intent i = new Intent(Android.Provider.Settings.ActionWirelessSettings);
                    m_currentContext.StartActivity(i);

                    isAccepted = true;

                }))
                .SetNegativeButton("Peruuta", new EventHandler<DialogClickEventArgs>((sender, e) =>
                {
                    isAccepted = false;
                }));
            
            requestConnection.Create().Show();


            while (!isAccepted.HasValue)
            {
                await Task.Delay(50);
            }

            return isAccepted.Value;

        }
    }
}