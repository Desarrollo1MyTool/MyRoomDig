namespace MyRoomDig.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using MyRoomDig.Services;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using System.IO;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class TakePictureViewModel: BaseViewModel
    {
        #region Attribues
        MediaFile file;
        public ImageSource _Imagen;
        #endregion
        #region Properties
        public ImageSource Imagen
        {
            get { return this._Imagen; }
            set { SetValue(ref this._Imagen, value); }
        }
        #endregion
        #region Services
        private ApiServices apiService;
        #endregion
        #region Commands
        public ICommand TakePictureCommand { get { return new RelayCommand(TakePicture); } }
        public ICommand SavePictureCommand { get { return new RelayCommand(SavePicture); } }
        #endregion
        #region Constructors
        public TakePictureViewModel()
        {
            apiService = new ApiServices();
            this.Imagen = "ic_nodispon";
        }
        #endregion
        #region Methods
        public async void TakePicture()
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("Error Cámara", "La cámara del dispositivo no esta disponible.", "Aceptar");
            }
            this.file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg",
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small
            });

            if (this.file != null)
            {
                Imagen = ImageSource.FromStream(() =>
                {
                    MemoryStream stream = (MemoryStream)file.GetStream();
                    return stream;
                });
            }

        }
        public async void SavePicture()
        {

        }
        #endregion
    }
}
