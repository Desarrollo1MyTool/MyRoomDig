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
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class TakePictureViewModel: BaseViewModel
    {
        #region Attribues
        private bool isProcessing;
        private int _idIdentifica;
        private string _Identificacion;
        private string _nameClient;
        private string _descripcion;
        private KeyValue _typeDocSelected;
        private EvidenciasModel _Evidencia;
        private ObservableCollection<EvidenciasModel> _Evidences;
        private ObservableCollection<KeyValue> _TypesDoc;
        private ImageSource _Imagen;
        MediaFile file;
        #endregion

        #region Properties
        public int IdIdentifica
        {
            get { return this._idIdentifica; }
            set { SetValue(ref this._idIdentifica, value); }
        }
        public string Identificacion
        {
            get { return this._Identificacion; }
            set { SetValue(ref this._Identificacion, value); }
        }
        public string NameClient
        {
            get { return this._nameClient; }
            set { SetValue(ref this._nameClient, value); }
        }
        public string Descripcion
        {
            get { return this._descripcion; }
            set { SetValue(ref this._descripcion, value); }
        }
        public KeyValue TypeDocSelected
        {
            get { return this._typeDocSelected; }
            set { SetValue(ref this._typeDocSelected, value); }
        }
        public byte[] imgByte { get; set; }
        public EvidenciasModel EvidenceSave
        {
            get { return this._Evidencia; }
            set { SetValue(ref this._Evidencia, value); }
        }
        public ObservableCollection<EvidenciasModel> Evidences 
        {
            get { return this._Evidences; }
            set { SetValue(ref this._Evidences, value); }
        }
        public ObservableCollection<KeyValue> TypesDoc
        {
            get { return this._TypesDoc; }
            set { SetValue(ref this._TypesDoc, value); }
        }
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
                this.IdIdentifica = 0;
                this.NameClient = "";
                this.imgByte = null;
                this.Descripcion = "";
                this.Identificacion = "";
                TypesDoc = new ObservableCollection<KeyValue>();
                GetTypeDoc();
                Evidences = new ObservableCollection<EvidenciasModel>();
                EvidenceSave = new EvidenciasModel();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("TakePicture Instance", ex.ToString(), "Ok");
            }
        }
        public async void GetTypeDoc()
        {
            await Task.Delay(2000);
            var apiRoom = Application.Current.Resources["APISecurity"].ToString();
            var response = await this.apiService.GetList<KeyValue>(apiRoom, "api/GetTypeDoc", "");
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error A-TypeDoc",
                    response.Message,
                    "Ok");
                return;
            }
            foreach(var item in (List<KeyValue>)response.Result)
            {
                TypesDoc.Add(new KeyValue
                {
                    id = item.id,
                    name = item.name
                });
            }
            
        }
        public async void SearchData()
        {
            try
            {

                var apiRoom = Application.Current.Resources["APISecurity"].ToString();
                var response = await this.apiService.GetList<EvidenciasModel>(apiRoom, "api/GetClients?identification=" + Identificacion + TypeDocSelected.id, "");
                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error A-Clients",
                        response.Message,
                        "Ok");
                    return;
                }
                foreach (var item in (List<EvidenciasModel>)response.Result)
                {
                    Evidences.Add(new EvidenciasModel
                    {
                        idClient = item.idClient,
                        numIdenti = item.numIdenti,
                        nameClient = item.nameClient                        
                    });
                    this.IdIdentifica = item.numIdenti??0;
                    this.NameClient = item.nameClient;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("TakePicture SearchData", ex.Message, "Ok");
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
                    Name = this.Identificacion + ".jpg",
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
                    Imagen = file.Path;
                }
                if (file == null) Imagen = "ic_nodispon";
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("TakePicture TakePicture", ex.Message, "Ok");
            }

        }
        public async void SavePicture()
        {
            try
            {
                if(!isProcessing)
                {
                    var resultado = await Application.Current.MainPage.DisplayAlert("Guardar", "Desea guradar la evidencia: " + this.Identificacion + ".jpg" + " ?", "Si", "No");
                    if(resultado)
                    {
                        isProcessing = true;
                        if(this.Identificacion == null || this.Identificacion == string.Empty)
                        {
                            await Application.Current.MainPage.DisplayAlert("", "Ingrese la identificación del cliente", "Ok");
                            isProcessing = false;
                            return;
                        }
                        if (this.TypeDocSelected == null)
                        {
                            await Application.Current.MainPage.DisplayAlert("", "Seleccione un tipo de documento", "Ok");
                            isProcessing = false;
                            return;
                        }
                        EvidenceSave.numIdenti = this.IdIdentifica;  
                        EvidenceSave.evidencia1 = this.imgByte;
                        EvidenceSave.descripcion = this.Descripcion;
                        EvidenceSave.fileName = this.Identificacion + ".jpg";

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
                            return;
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Guardado", "Guardado correctamente", "Ok");
                            Instance();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("TakePicture SavePicture", ex.Message, "Ok");
            }
            isProcessing = false;
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
