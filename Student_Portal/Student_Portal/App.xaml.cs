using Student_Portal.Data;
using Student_Portal.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Student_Portal
{
    public partial class App : Application
    {
        private const string DatabaseName = "PortalSQLite.db3";
        public static string saved = "saved;";
        public static string updated = "updated";
        static PortalDataBase database;

        public App()
        {
            InitializeComponent();
            
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        
        public static PortalDataBase Database
        {
            get
            {
                if(database == null)
                {
                    database = new PortalDataBase(
                        DependencyService.Get<IFileHelper>().GetLocalFilePath(DatabaseName));
                }
                return database;
            }
        }
    }
}
