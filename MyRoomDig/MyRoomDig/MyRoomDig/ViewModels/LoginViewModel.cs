using GalaSoft.MvvmLight.Command;
using MyRoomDig.Services;
using MyRoomDig.Views;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyRoomDig.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Attributes
        private string _user;
        private string _password;
        private bool _isEnabled;
        #endregion

        #region Properties
        public string User
        {
            get { return this._user; }
            set { SetValue(ref this._user, value); }
        }
        public string Password
        {
            get { return this._password; }
            set { SetValue(ref this._password, value); }
        }
        public bool IsEnabled
        {
            get { return this._isEnabled; }
            set { SetValue(ref this._isEnabled, value); }
        }
        #endregion

        #region Services
        private DataService dataService;
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            Instance();
        }
        #endregion

        #region Commands
        public ICommand LoginCommand { get { return new RelayCommand(Login); } }
        #endregion

        #region Methods
        public async void Instance()
        {
            try
            {
                this.dataService = new DataService();
                this.IsEnabled = true;
            }
            catch (Exception ex)
            {
            }
        }
        private async void Login()
        {
            if (string.IsNullOrEmpty(this.User))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debe especificar un usuario",
                    "ok");
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debe especificar una clave",
                    "ok");
                return;
            }
            if (this.User == "SJUAVEG" && this.Password == "564439")
            {
                MainViewModel.GetInstance().ConfigViewModel = new ConfigViewModel();
                this.Password = string.Empty;
                await Application.Current.MainPage.Navigation.PushAsync(new ConfigPage());
            }
            else
            {
                MainViewModel.GetInstance().TakePictureViewModel = new TakePictureViewModel();
                this.Password = string.Empty;
                await Application.Current.MainPage.Navigation.PushAsync(new TakePicturePage());
            }
        }
        #endregion
    }
}
