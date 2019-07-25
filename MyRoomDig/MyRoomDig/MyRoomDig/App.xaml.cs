using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MyRoomDig
{
    using MyRoomDig.Data;
    using System.IO;
    using Views;
    public partial class App : Application
    {
        public static Xamarin.Forms.MasterDetailPage MasterDetailPage { get; set; }
        static setupDatabase databaseSetUp;
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
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
        public static setupDatabase DatabaseSetUp
        {
            get
            {
                if (databaseSetUp == null)
                {
                    databaseSetUp = new setupDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "myRoomDigSetUp.db3"));
                }
                return databaseSetUp;
            }
        }
    }
}
