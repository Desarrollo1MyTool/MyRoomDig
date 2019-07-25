using System;
using System.Collections.Generic;
using System.Text;

namespace MyRoomDig.ViewModels
{
    using MyRoomDig.Models;
    using MyRoomDig.Services;
    using MyRoomDig.Tools;
    using MyRoomDig.Views;
    using Rg.Plugins.Popup.Services;
    using System;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;

    public class MasterDetailViewModel : BaseViewModel
    {
        #region Attributes
        private bool _IsPresented;
        private MasterDetailModel _ItemSelected;
        private ObservableCollection<MasterDetailModel> _CsItems;
        public static readonly BindableProperty CurrentPageProperty = BindableProperty.Create("CurrentPage", typeof(Page), typeof(Page), null);
        #endregion

        #region Properties
        public bool IsPresented
        {
            get { return _IsPresented; }
            set { SetValue(ref this._IsPresented, value); }
        }
        public ObservableCollection<MasterDetailModel> CsItems
        {
            get { return _CsItems; }
            set { SetValue(ref this._CsItems, value); }
        }
        public MasterDetailModel ItemSelected
        {
            get { return _ItemSelected; }
            set
            {
                SetValue(ref this._ItemSelected, value);
                OpenPage();
            }
        }
        #endregion

        #region Commands

        #endregion

        #region Constructor
        public MasterDetailViewModel()
        {
            Instance();
        }
        #endregion

        #region Methods
        public async void Instance()
        {
            try
            {
                CsItems = new ObservableCollection<MasterDetailModel>();
                ItemSelected = new MasterDetailModel();
                if (MainViewModel.GetInstance().MySetUpMain == null || MainViewModel.GetInstance().MySetUpMain.Count == 0) MainViewModel.GetInstance().MySetUpMain = await App.DatabaseSetUp.GetItemsSetupMainAsync();

                foreach (var item in MainViewModel.GetInstance().MySetUpMain)
                {
                    if (item.IsVisible)
                    {
                        CsItems.Add(new MasterDetailModel
                        {
                            IdOpcion = item.IdOption,
                            Icon = item.Icon,
                            ItemMenu = item.NameOption,
                            TargetType = typeof(ContentPage)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("MasterDetail Instance", ex.ToString(), "Ok");
            }
        }
        public async void OpenPage()
        {
            try
            {
                bool isOpenPage = true;
                if (ItemSelected != null && ItemSelected.TargetType != null)
                {
                    switch (ItemSelected.IdOpcion)
                    {
                        case 1:
                            ItemSelected.TargetType = typeof(AddClientPage);
                            MainViewModel.GetInstance().AddClient = new AddClientViewModel();
                            break;
                        case 2:
                            ItemSelected.TargetType = typeof(TakePicturePage);
                            MainViewModel.GetInstance().Documentation = new TakePictureViewModel();
                            break;
                        case -1:
                            isOpenPage = false;
                            if (MainViewModel.GetInstance().Login.User.Trim().ToUpper() == "SJUAVEG" && MainViewModel.GetInstance().Login.Password == "564439")
                            {
                                MainViewModel.GetInstance().Config = new ConfigViewModel();
                                await Application.Current.MainPage.Navigation.PushAsync(new ConfigPage());
                            }
                            break;
                        //case -2:
                        //    isOpenPage = false;
                        //    MainViewModel.GetInstance().TextPopUp = "Versión " + MainViewModel.GetInstance().gNumberVersionApp;
                        //    await PopupNavigation.Instance.PushAsync(new PopUp());
                        //    break;
                        case -3:
                            ItemSelected.TargetType = typeof(LoginPage);
                            Application.Current.MainPage = new LoginPage();
                            MainViewModel.GetInstance().Login.IsEnabled = false;
                            isOpenPage = false;
                            break;
                        default:
                            break;
                    }
                    if (isOpenPage) App.MasterDetailPage.Detail = new NavigationPage((Page)Activator.CreateInstance(ItemSelected.TargetType));
                    ItemSelected = null;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("MasterDetail OpenPage", ex.ToString(), "Ok");
            }
            IsPresented = false;
        }
        #endregion
    }
}
