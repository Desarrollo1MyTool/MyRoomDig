﻿namespace MyRoomDig.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Attributes

        #endregion
        #region Properties

        #endregion
        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.TakePictureviewModel = new TakePictureViewModel();
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
        public TakePictureViewModel TakePictureviewModel { get; set; }
        #endregion
    }
}
