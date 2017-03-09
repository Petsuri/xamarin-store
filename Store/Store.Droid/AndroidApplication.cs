
using System;
using Store.Domain;

namespace Store.Droid
{
    class AndroidApplication : IApplication
    {
        public void close()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}