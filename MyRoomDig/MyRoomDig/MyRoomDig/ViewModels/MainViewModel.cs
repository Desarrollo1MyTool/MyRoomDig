using MyRoomDig.Models;
using System.Collections.Generic;

namespace MyRoomDig.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Attributes
        private UsersModel _user;
        #endregion

        #region Properties
        public UsersModel User {get { return this._user; } set { SetValue(ref this._user, value); }}
        //public SetupApp mySetupApp { get; set; }
        //public List<SetupMain> mySetupMain { get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.LoginViewModel = new LoginViewModel();
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

        #region ViewModels
        public TakePictureViewModel TakePictureViewModel { get; set; }
        public LoginViewModel LoginViewModel { get; set; }
        public ConfigViewModel ConfigViewModel { get; set; }
        public AddClientViewModel AddClientViewModel { get; set; }
        #endregion
    }
}
