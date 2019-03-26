using System.IO;
using Student_Portal.Data;
using Student_Portal.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace Student_Portal.Droid
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}