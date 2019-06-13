namespace MyRoomDig.ViewModels
{
    using Android.Graphics;
    using GalaSoft.MvvmLight.Command;
    using MyRoomDig.Domain;
    using MyRoomDig.Services;
    using Newtonsoft.Json;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using System;
    using System.IO;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class TakePictureViewModel: BaseViewModel
    {
        #region Attribues
        MediaFile file;
        public ImageSource _Imagen;
        public EvidenciasModel _Evidencia;
        public string _Identificacion; 
        public string _FileName;
        public bool isProcessing;
        #endregion

        #region Properties
        public ImageSource Imagen
        {
            get { return this._Imagen; }
            set { SetValue(ref this._Imagen, value); }
        }
        public EvidenciasModel EvidenceSave
        {
            get { return this._Evidencia; }
            set { SetValue(ref this._Evidencia, value); }
        }
        public string FileName
        {
            get { return this._FileName; }
            set { SetValue(ref this._FileName, value); }
        }
        public string Identificacion
        {
            get { return this._Identificacion; }
            set { SetValue(ref this._Identificacion, value); }
        }
        public byte[] imgByte { get; set; }
        #endregion
        
        #region Services
        private ApiServices apiService;
        #endregion
        
        #region Commands
        public ICommand TakePictureCommand { get { return new RelayCommand(TakePicture); } }
        public ICommand SavePictureCommand { get { return new RelayCommand(SavePicture); } }
        public ICommand SearchPictureCommand { get { return new RelayCommand(SearchData); } }
        #endregion
        
        #region Constructors
        public TakePictureViewModel()
        {
            Instance();
        }
        #endregion
        
        #region Methods
        public async void Instance()
        {
            try
            {
                apiService = new ApiServices();
                this.Imagen = "ic_nodispon";
                EvidenceSave = new EvidenciasModel();
            }
            catch (Exception ex)
            {
            }
        }
        public async void TakePicture()
        {
            try
            {
                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await App.Current.MainPage.DisplayAlert("Error Cámara", "La cámara del dispositivo no esta disponible.", "Aceptar");
                }
                this.file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = this.FileName + ".jpg",
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small
                });

                if (this.file != null)
                {
                    Imagen = ImageSource.FromStream(() =>
                    {
                        MemoryStream stream = (MemoryStream)file.GetStream();
                        return stream;
                    });
                    
                    imgByte = ConvertPictureBytes(file.Path);
                }
            }
            catch (Exception ex)
            {
            }

        }
        public async void SavePicture()
        {
            try
            {
                if(!isProcessing)
                {
                    var resultado = await Application.Current.MainPage.DisplayAlert("Guardar", "Desea guradar la evidencia: " + this.FileName + ".jpg" + " ?", "Si", "No");
                    if(resultado)
                    {
                        isProcessing = true;
                        if(this.Identificacion == null || this.Identificacion == string.Empty)
                        {
                            await Application.Current.MainPage.DisplayAlert("", "Ingrese la identificación del cliente", "Ok");
                            isProcessing = false;
                            return;
                        }
                        //EvidenceSave.codeTipoEvid = ;
                        //EvidenceSave.codeCarpeta = ;
                        //EvidenceSave.idIdentifica = ;
                        //EvidenceSave.idSerialNum = ;
                        EvidenceSave.evidencia1 = this.imgByte;
                        //EvidenceSave.descripcion = ;
                        EvidenceSave.fileName = this.FileName + ".jpg";
                        EvidenceSave.fecha = DateTime.Now;
                        //EvidenceSave.usuario = ;
                        var apiS = Application.Current.Resources["APISecurity"].ToString();

                        var response = await this.apiService.PostBool(
                                              apiS, 
                                              "/api", 
                                              "/PostEvidences", 
                                              EvidenceSave);

                        if(!response.IsSuccess)
                        {
                            await Application.Current.MainPage.DisplayAlert(
                               "Error AP-Evidence",
                               response.Message,
                               "Ok");
                            await Application.Current.MainPage.Navigation.PopModalAsync();
                            return;
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Guardado", "Guardado correctamente", "Ok");
                            KeyValue temp = JsonConvert.DeserializeObject<KeyValue>(response.Result.ToString());
                        }
                    }
                    isProcessing = false;
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        public async void SearchData()
        {

        }

        public static byte[] ConvertPictureBytes(string imagen)
        {
            string sTemp = System.IO.Path.GetTempFileName();
            FileStream fs = new FileStream(imagen, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
            return bytes;
        }

        #endregion
    }
}
