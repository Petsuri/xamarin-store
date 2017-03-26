using System;
using Store.Interface.Platform;
using System.IO;

namespace Store.Droid.Platform
{
    public class AndroidFileInformation : IFileInformation
    {
        public string GetPath(string fileName)
        {
            var directory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(directory, fileName);
        }
    }
}