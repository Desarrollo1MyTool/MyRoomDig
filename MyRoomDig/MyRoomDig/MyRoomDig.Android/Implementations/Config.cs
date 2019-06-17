[assembly: Xamarin.Forms.Dependency(typeof(MyRoomDig.Droid.Implementations.Config))]
namespace MyRoomDig.Droid.Implementations
{
    using MyRoomDig.Interfaces;
    using SQLite.Net.Interop;
    public class Config:IConfig
    {
        private string directoryDB;
        private ISQLitePlatform platform;
        public string DirectoryDB
        {
            get
            {
                if (string.IsNullOrEmpty(directoryDB))
                {
                    directoryDB = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                }
                return directoryDB;
            }
        }

        public ISQLitePlatform Platform
        {
            get
            {
                if (platform == null)
                {
                    platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
                }
                return platform;
            }
        }
    }
}