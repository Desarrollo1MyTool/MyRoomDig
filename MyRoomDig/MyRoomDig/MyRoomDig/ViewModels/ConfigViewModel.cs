using GalaSoft.MvvmLight.Command;
using MyRoomDig.Models;
using MyRoomDig.Services;
using MyRoomDig.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyRoomDig.ViewModels
{
    public class ConfigViewModel:BaseViewModel
    {
        #region Attributes
        private bool _IsMain;
        private bool _IsVisible;
        private int _port;
        private string _host;
        private string _maquina;
        private ConfigModel _configM;
        private DataService DataService;
        private List<SetupMain> _CsOptions;
        private SetupMain _OptionSelected;
        #endregion

        #region Properties
        private bool Flag { get; set; }
        public bool IsVisible
        {
            get { return this._IsVisible; }
            set { SetValue(ref this._IsVisible, value); UpdateOption(); }
        }
        public bool IsMain
        {
            get { return this._IsMain; }
            set { SetValue(ref this._IsMain, value); UpdateOption(); }
        }
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
        public SetupMain OptionSelected
        {
            get { return this._OptionSelected; }
            set
            {
                SetValue(ref this._OptionSelected, value);
                Flag = false;
                this.IsMain = value.IsMain;
                Flag = false;
                this.IsVisible = value.IsVisible;
            }
        }
        public List<SetupMain> CsOptions
        {
            get { return _CsOptions; }
            set { SetValue(ref this._CsOptions, value); }
        }
        #endregion

        #region Commands
        public ICommand SaveCommand { get { return new RelayCommand(SaveConfig); } }
        public ICommand ExitCommand { get { return new RelayCommand(Cancel); } }
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
            try
            {
                SetupApp mySetUp = MainViewModel.GetInstance().mySetUpApp;
                CsOptions = MainViewModel.GetInstance().MySetUpMain;
                this.Maquina = (mySetUp.IdMaquina).ToString();
                this.Host = mySetUp.IdHost.ToString();
                this.Port = mySetUp.IdPort;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Config Instance", ex.ToString(), "Ok");
            }
            
        }
        private async void UpdateOption()
        {
            try
            {
                if (OptionSelected == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Configuración", "Debe seleccionar una opción", "Ok");
                    return;
                }
                if (Flag)
                {
                    SetupMain setupTemp = CsOptions.FirstOrDefault(x => x.Id == OptionSelected.Id);
                    setupTemp.IsMain = this.IsMain;
                    setupTemp.IsVisible = this.IsVisible;
                    return;
                }
                Flag = true;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Config UpdateOption", ex.ToString(), "Ok");
            }
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
            if (await ValidateOptions())
            {
                int respSetupApp, respSetupMain;
                SetupApp setupApp = new SetupApp();
                setupApp.Id = 1;
                setupApp.IdApp = 33;
                setupApp.IdMaquina = Convert.ToInt32(this.Maquina);
                setupApp.IdHost = this.Host;
                setupApp.IdPort = Convert.ToInt32(this.Port);
                setupApp.IntranetRoomDig = this.Host + ":" + this.Port;
                respSetupApp = await App.DatabaseSetUp.SaveItemAsync(setupApp);
                respSetupMain = await App.DatabaseSetUp.SaveListItemMainAsync(CsOptions);
                if (respSetupApp == 1 && respSetupMain > 0)
                {
                    await Application.Current.MainPage.DisplayAlert("MyOrder", "Operación exitosa, reinicie la aplicación", "Ok");
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Setup Alerta", "Imposible grabar", "ok");
                }
            }
        }
        private async Task<bool> ValidateOptions()
        {
            bool response = false;
            try
            {
                int CantMains = CsOptions.Where(x => x.IsMain && x.IsVisible).Count();
                if (CantMains > 1)
                {
                    await Application.Current.MainPage.DisplayAlert("Validación Vistas", "Se han estalecido varias páginas de inicio", "Ok");
                    return response;
                }
                else if (CantMains == 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Validación Vistas", "Debe establecer una página  de inicio", "Ok");
                    return response;
                }
                response = true;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Config ValidateOptions", ex.ToString(), "Ok");
            }
            return response;
        }
        private async void Cancel()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Config Cancel", ex.ToString(), "Ok");
            }
        }
        #endregion
    }
}
