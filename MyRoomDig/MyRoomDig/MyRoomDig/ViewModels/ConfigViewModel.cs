using GalaSoft.MvvmLight.Command;
using MyRoomDig.Models;
using MyRoomDig.Services;
using MyRoomDig.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyRoomDig.ViewModels
{
    public class ConfigViewModel:BaseViewModel
    {
        #region Attributes
        private string _host;
        private int _port;
        private string _maquina;
        private ConfigModel _configM;
        private DataService DataService;
        #endregion

        #region Properties
        public int Port
        {
            get { return this._port; }
            set { SetValue(ref this._port, value); }
        }
        public string Host
        {
            get { return this._host; }
            set { SetValue(ref this._host, value); }
        }
        public string Maquina
        {
            get { return this._maquina; }
            set { SetValue(ref this._maquina, value); }
        }
        public ConfigModel ConfigM
        {
            get { return this._configM; }
            set { SetValue(ref this._configM, value); }
        }
        #endregion

        #region Commands
        public ICommand SaveCommand { get { return new RelayCommand(SaveConfig); } }
        #endregion

        #region Constructors
        public ConfigViewModel()
        {
            Instance();
        }
        #endregion

        #region Methods
        public async void Instance()
        {
            this.Host = "";
            this.Port = 49800;
            this.Maquina = "1";
            DataService = new DataService();
            ConfigM = new ConfigModel();
        }
        public async void SaveConfig()
        {
            if (string.IsNullOrEmpty(this.Host))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debe especificar una dirección IP",
                    "ok");
                return;
            }
            if (this.Port <= 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Escriba un puerto válido",
                    "ok");
                return;
            }
            if (string.IsNullOrEmpty(this.Maquina))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Escriba un número válido de máquina",
                    "ok");
                return;
            }
            ConfigM.HostIP = this.Host;
            ConfigM.HostPort = this.Port;
            ConfigM.Maquina = this.Maquina;
            DataService.InsertOrUpdate<ConfigModel>(ConfigM);
            await Application.Current.MainPage.Navigation.PushAsync(new TakePicturePage());
        }
        #endregion
    }
}
