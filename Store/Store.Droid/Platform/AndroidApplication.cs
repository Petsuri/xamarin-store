using Store.Interface.Platform;

namespace Store.Droid
{
    class AndroidApplication : IApplication
    {
        public void Close()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}