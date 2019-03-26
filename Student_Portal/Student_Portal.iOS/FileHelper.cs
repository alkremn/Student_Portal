using System;
using System.IO;

using Student_Portal.Data;
using Student_Portal.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace Student_Portal.iOS
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library");

            if (!Directory.Exists(libFolder))
                Directory.CreateDirectory(libFolder);

            return Path.Combine(libFolder, filename);
        }
    }
}