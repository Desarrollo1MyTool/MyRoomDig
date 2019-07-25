namespace MyRoomDig.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using MyRoomDig.DependencyServices;
    using MyRoomDig.Models;
    using MyRoomDig.Services;
    using MyRoomDig.Views;
    using Rg.Plugins.Popup.Services;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class MainViewModel : BaseViewModel
    {
        #region Service
        private ApiServices apiServices;
        #endregion

        #region Attributes
        private bool _IsRunning;
        private UsersModel _user;
        private Page _MainDetailPage;
        #endregion

        #region Properties
        public bool IsRunning
        {
            get { return this._IsRunning; }
            set { SetValue(ref this._IsRunning, value); }
        }
        public int gApp { get; set; }
        public int gIdMaquina { get; set; }
        public string gVersionApp { get; set; }
        public string gNumberVersionApp { get; set; }
        public Page MainDetailPage
        {
            get { return _MainDetailPage; }
            set { SetValue(ref this._MainDetailPage, value); }
        }
        public UsersModel User {get { return this._user; } set { SetValue(ref this._user, value); }}
        public SetupApp mySetUpApp { get; set; }
        public List<SetupMain> MySetUpMain { get; set; }
        public AppData MyApp { get; set; }
        #endregion

        #region Commands
        public ICommand ClosePopUpCommand { get { return new RelayCommand(ClosePopUp); } }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            this.MyApp = new AppData();
            this.mySetUpApp = new SetupApp();
            this.User = new UsersModel();
            this.Login = new LoginViewModel();
            ReadDataBase();
            this.MyApp.IdApp = this.mySetUpApp.IdApp;
            this.MyApp.IntranetOrder = (this.mySetUpApp.IntranetRoomDig == null) ? "" : this.mySetUpApp.IntranetRoomDig;
            this.MyApp.NameUser = "inicial";
            instance = this;
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }
        #endregion

        #region Methods
        public async void Instance(bool Flag = true)
        {
            try
            {
                apiServices = new ApiServices();
                this.IsRunning = false;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Main Instance", ex.ToString(), "Ok");
            }
        }

        private async void ReadDataBase()
        {
            try
            {
                this.mySetUpApp = await App.DatabaseSetUp.GetItemSetupAppAsync(1);
                if (this.mySetUpApp == null)
                {
                    CreateRegInitial(1);
                    this.mySetUpApp = await App.DatabaseSetUp.GetItemSetupAppAsync(1);
                }
                Application.Current.Resources["APISecurity"] = "http://" + this.mySetUpApp.IntranetRoomDig;
                this.MySetUpMain = await App.DatabaseSetUp.GetItemsSetupMainAsync();
                if (this.MySetUpMain == null || MySetUpMain.Count == 0)
                {
                    CreateRegInitial(2);
                    this.MySetUpMain = await App.DatabaseSetUp.GetItemsSetupMainAsync();
                }
                this.Login.IsEnabled = true;

            }
            catch (Exception ex)
            {
            }
        }
        private async void CreateRegInitial(int id)
        {
            try
            {
                switch (id)
                {
                    case 1:
                        SetupApp setupApp = new SetupApp();
                        setupApp.Id = 0;
                        setupApp.IdApp = 33;
                        setupApp.IdLugar = 1;
                        setupApp.IdMaquina = 1;
                        setupApp.IntranetRoomDig = string.IsNullOrEmpty(Application.Current.Resources["APISecurity"].ToString()) ? "192.168.0.2:49800" : Application.Current.Resources["APISecurity"].ToString();
                        await App.DatabaseSetUp.SaveItemAsync(setupApp);
                        break;
                    case 2:
                        List<SetupMain> setupMains = new List<SetupMain>()
                        {
                            new SetupMain{IdOption = 1, NameOption = "Cíclicos", IsMain = true, Icon = "Ic_scanner", IsVisible = true},
                            new SetupMain{IdOption = 2, NameOption = "Recibo de Mercancia", IsMain = false, Icon = "Ic_recibo", IsVisible = true},
                            new SetupMain{IdOption = 3, NameOption = "Inventario Periódico", IsMain = false, Icon = "Ic_conteo", IsVisible = true},
                            new SetupMain{IdOption = 4, NameOption = "Producto", IsMain = false, Icon = "Ic_crear", IsVisible = true},
                            new SetupMain{IdOption = -1, NameOption = "Configuración", IsMain = false, Icon = "Ic_config", IsVisible = true},
                            new SetupMain{IdOption = -2, NameOption = "Acerca de", IsMain = false, Icon = "Ic_acerca_de", IsVisible = true},
                            new SetupMain{IdOption = -3, NameOption = "Cerrar Sesión", IsMain = false, Icon = "Ic_cerrar_sesion", IsVisible = true}
                        };
                        await App.DatabaseSetUp.SaveListItemMainAsync(setupMains);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Main CreateRegInitial", ex.ToString(), "Ok");
            }
        }
        public async void GetMainDetail()
        {
            try
            {
                SetupMain setupMain = await App.DatabaseSetUp.GetMainAsync();
                if (setupMain != null)
                {
                    do
                    {
                        await Task.Delay(500);
                    }
                    while (!Login.IsEnabled);

                    this.MainDetailPage = (Page)Activator.CreateInstance(await GetTypePage(setupMain.IdOption));
                    return;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Main GetMainDetail", ex.ToString(), "Ok");
            }
        }
        private async Task<Type> GetTypePage(int idOption)
        {
            try
            {
                switch (idOption)
                {
                    case 1:
                        MainViewModel.GetInstance().AddClient = new AddClientViewModel();
                        return typeof(AddClientPage);
                    case 2:
                        MainViewModel.GetInstance().Documentation = new TakePictureViewModel();
                        return typeof(AddClientPage);
                    case -3:
                        return typeof(LoginPage);
                    default:
                        await App.Current.MainPage.DisplayAlert("Tipo de página inválido", idOption.ToString(), "Ok");
                        break;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Main GetTypePage", ex.ToString(), "Ok");
            }
            return null;
        }
        public ImageSource ConvertImageToSource(Byte[] Image)
        {
            ImageSource imageSource = null;
            try
            {
                imageSource = Xamarin.Forms.ImageSource.FromStream(() => new System.IO.MemoryStream(Image));
                if (Image == null) imageSource = "ic_nodispon";
            }

            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Main ConvertImage", ex.ToString(), "Ok");
            }
            return imageSource;
        }
        public byte[] ConvertImageToBytes(string imagen)
        {
            string sTemp = System.IO.Path.GetTempFileName();
            FileStream fs = new FileStream(imagen, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
            return bytes;
        }
        private async void GetVersion()
        {
            try
            {
                this.gNumberVersionApp = DependencyService.Get<IAppVersionAndBuild>().GetVersionNumber();
                this.gVersionApp = DependencyService.Get<IAppVersionAndBuild>().GetBuildNumber();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error GetVersion", ex.ToString(), "Ok");
            }
        }
        private async void ClosePopUp()
        {
            try
            {
                await PopupNavigation.Instance.PopAsync();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Main ClosePopUp", ex.ToString(), "Ok");
            }
        }
        #endregion

        #region ViewModels
        public TakePictureViewModel Documentation { get; set; }
        public LoginViewModel Login { get; set; }
        public ConfigViewModel Config { get; set; }
        public AddClientViewModel AddClient { get; set; }
        #endregion
    }
}
