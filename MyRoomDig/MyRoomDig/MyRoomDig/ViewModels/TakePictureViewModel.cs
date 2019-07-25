namespace MyRoomDig.ViewModels
{
    using Android.Graphics;
    using GalaSoft.MvvmLight.Command;
    using MyRoomDig.Domain;
    using MyRoomDig.Services;
    using MyRoomDig.Views;
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
    using System.Linq;
    public class TakePictureViewModel : BaseViewModel
    {
        #region Services
        private ApiServices apiService;
        #endregion

        #region Attribues
        private bool isProcessing;
        private bool withData;
        private bool _withClientSelected;
        private int _idIdentifica;
        private string _Identification;
        private string _nameClient;
        private string _descripcion;
        private string _siglatypeDoc;
        private KeyValue _typeDocSelected;
        private EvidenciasModel _clientSelected;
        private EvidenciasModel _itemListSelected;
        private ObservableCollection<EvidenciasModel> _evidenceSave;
        private EvidenciasModel _evidences;
        private ObservableCollection<KeyValue> _typesDoc;
        private ObservableCollection<EvidenciasModel> _lstEvidClients;
        private ImageSource _Imagen;
        MediaFile file;
        #endregion

        #region Properties

        public bool newClient { get; set; }
        public bool WithClientSelected
        {
            get { return this._withClientSelected; }
            set { SetValue(ref this._withClientSelected, value); }
        }
        public int IdIdentifica
        {
            get { return this._idIdentifica; }
            set { SetValue(ref this._idIdentifica, value); }
        }
        public string Identification
        {
            get { return this._Identification; }
            set { SetValue(ref this._Identification, value); }
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
        public string SiglaTypeDoc
        {
            get { return this._siglatypeDoc; }
            set { SetValue(ref this._siglatypeDoc, value); }
        }
        public byte[] imgByte { get; set; }
        public KeyValue TypeDocSelected
        {
            get { return this._typeDocSelected; }
            set { SetValue(ref this._typeDocSelected, value); }
        }
        public EvidenciasModel ClientSelected
        {
            get { return this._clientSelected; }
            set
            {
                SetValue(ref this._clientSelected, value);
                if (this._clientSelected != null && this._clientSelected.idTercero > 0) this.WithClientSelected = true;
            }
        }
        public ObservableCollection<EvidenciasModel> EvidenceSave
        {
            get { return this._evidenceSave; }
            set { SetValue(ref this._evidenceSave, value); }
        }
        public EvidenciasModel Evidences
        {
            get { return this._evidences; }
            set { SetValue(ref this._evidences, value); }
        }
        public ObservableCollection<KeyValue> TypesDoc
        {
            get { return this._typesDoc; }
            set { SetValue(ref this._typesDoc, value); }
        }
        public ObservableCollection<EvidenciasModel> LstEvidClients
        {
            get { return this._lstEvidClients; }
            set { SetValue(ref this._lstEvidClients, value); }
        }
        public ImageSource Imagen
        {
            get { return this._Imagen; }
            set { SetValue(ref this._Imagen, value); }
        }
        public EvidenciasModel ItemListSelected
        {
            get { return this._itemListSelected; }
            set { SetValue(ref this._itemListSelected, value); }
        }

        #endregion

        #region Commands
        public ICommand TakePictureCommand { get { return new RelayCommand(TakePicture); } }
        public ICommand SavePictureCommand { get { return new RelayCommand(SavePicture); } }
        public ICommand SearchClientCommand { get { return new RelayCommand(CallSearchData); } }
        public ICommand ClearCommand { get { return new RelayCommand(ClearData); } }
        public ICommand AddClientCommand { get { return new RelayCommand(AddClient); } }
        public ICommand DeleteClientCommand { get { return new RelayCommand(DeleteClient); } }
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
                this.NameClient = string.Empty;
                this.imgByte = null;
                this.Descripcion = string.Empty;
                this.Identification = string.Empty;
                this.withData = false;
                this.WithClientSelected = false;
                this.newClient = false;
                this.SiglaTypeDoc = string.Empty;
                this.file = null;
                SearchServices searchService = new SearchServices();
                ItemListSelected = new EvidenciasModel();
                LstEvidClients = null;
                LstEvidClients = new ObservableCollection<EvidenciasModel>();
                Evidences = new EvidenciasModel();
                EvidenceSave = new ObservableCollection<EvidenciasModel>();
                ClientSelected = new EvidenciasModel();
                TypesDoc = await searchService.GetTypeDoc();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("TakePicture Instance", ex.ToString(), "Ok");
            }
        }
        public async void CallSearchData()
        {
            LstEvidClients.Clear();
            EvidenciasModel SearchEvidences = new EvidenciasModel();
            SearchServices searchService = new SearchServices();
            try
            {
                if (TypeDocSelected == null && TypeDocSelected.id < 0)
                {
                    await Application.Current.MainPage.DisplayAlert("", "Seleccione un tipo de documento", "Ok");
                    isProcessing = false;
                    return;
                }

                if (string.IsNullOrEmpty(Identification))
                {
                    await Application.Current.MainPage.DisplayAlert("", "Ingrese la identificación del cliente", "Ok");
                    isProcessing = false;
                    return;
                }

                SearchEvidences.typeDoc = TypeDocSelected.id;
                SearchEvidences.identifClient = this.Identification;

                Evidences = await searchService.SearchData(SearchEvidences);
                this.NameClient = Evidences.nameClient;
                if (Evidences.idTercero > 0)
                {
                    LstEvidClients.Add(new EvidenciasModel
                    {
                        nameClient = Evidences.nameClient,
                        identifClient = Evidences.identifClient,
                        idTercero = Evidences.idTercero,
                        sigla = TypeDocSelected.nameAux,
                        holder = true
                    });
                }
                else
                {
                    MainViewModel.GetInstance().AddClient = new AddClientViewModel();
                    MainViewModel.GetInstance().AddClient.IdTypeDoc = TypeDocSelected.id;
                    MainViewModel.GetInstance().AddClient.Identification = this.Identification;
                    MainViewModel.GetInstance().AddClient.DataNewClient = true;
                    await Application.Current.MainPage.Navigation.PushModalAsync(new AddClientPage());
                }
                withData = true;
                isProcessing = false;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("TakePicture SearchData", ex.Message, "Ok");
            }
        }
        public async void AddClient()
        {
            try
            {
                MainViewModel.GetInstance().AddClient = new AddClientViewModel();
                await Application.Current.MainPage.Navigation.PushModalAsync(new AddClientPage());
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("TakePicture AddClient", ex.ToString(), "Ok");
            }
        }
        public async void TakePicture()
        {
            try
            {
                if (ClientSelected != null && ClientSelected.idTercero > 0)
                {
                    imgByte = null;
                    await CrossMedia.Current.Initialize();
                    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                    {
                        await App.Current.MainPage.DisplayAlert("Error Cámara", "La cámara del dispositivo no esta disponible.", "Aceptar");
                    }
                    this.file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = this.Identification + ".jpg",
                        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small
                    });
                    if (this.file != null)
                    {
                        Imagen = ImageSource.FromStream(() =>
                        {
                            MemoryStream stream = (MemoryStream)file.GetStreamWithImageRotatedForExternalStorage();
                            return stream;
                        });
                        imgByte = ConvertPictureBytes(file.Path);
                        Imagen = file.Path;
                    }
                    if (file == null) Imagen = "ic_nodispon";
                    ObservableCollection<EvidenciasModel> lsTemp = new ObservableCollection<EvidenciasModel>();
                    ItemListSelected = LstEvidClients.FirstOrDefault(x => x.idTercero == ClientSelected.idTercero);
                    ItemListSelected.image = this.Imagen;
                    ItemListSelected.evidencia1 = this.imgByte;
                    foreach (var item in LstEvidClients)
                    {
                        if (item.idTercero == ItemListSelected.idTercero)
                        {
                            lsTemp.Add(ItemListSelected);
                        }
                        else
                        {
                            lsTemp.Add(item);
                        }
                    }
                    LstEvidClients.Clear();
                    foreach (var item in lsTemp)
                    {
                        LstEvidClients.Add(item);
                    }
                    ClientSelected = null;
                    return;
                }
                await Application.Current.MainPage.DisplayAlert("", "Debe seleccionar un cliente", "Ok");
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
                if (LstEvidClients.Count > 0)
                {
                    EvidenceSave.Clear();
                    foreach (var item in LstEvidClients)
                    {
                        if (item.evidencia1 == null || item.idTercero == 0)
                        {
                            await Application.Current.MainPage.DisplayAlert("", "El cliente " + item.nameClient + " no tiene evidencia registrada", "Ok");
                            return;
                        }
                        EvidenceSave.Add(new EvidenciasModel
                        {
                            idTercero = item.idTercero,
                            evidencia1 = item.evidencia1,
                            descripcion = item.descripcion,
                            fileName = item.identifClient,
                            usuario = MainViewModel.GetInstance().Login.User
                        });
                    }
                    var resultado = await Application.Current.MainPage.DisplayAlert("Guardar", "¿Desea guardar la(s) evidencia(s) ?", "Si", "No");
                    if (resultado)
                    {
                        var apiS = Application.Current.Resources["APISecurity"].ToString();

                        var response = await this.apiService.PostBool(
                                              apiS,
                                              "/api",
                                              "/PostEvidences",
                                              EvidenceSave);

                        if (!response.IsSuccess)
                        {
                            await Application.Current.MainPage.DisplayAlert(
                               "Error AP-Evidence",
                               response.Message,
                               "Ok");
                            return;
                        }
                        else
                        {
                            foreach (EvidenciasModel item in LstEvidClients)
                            {
                                if (File.Exists(item.image.ToString().Replace("File: ", ""))) File.Delete(item.image.ToString().Replace("File: ", ""));
                                item.image = null;
                            }

                            await Application.Current.MainPage.DisplayAlert("Guardado", "Evidencia(s) guardada(s) correctamente", "Ok");
                            Instance();
                        }
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("","Debe agregar cliente(s)","Ok");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("TakePicture SavePicture", ex.Message, "Ok");
            }
            isProcessing = false;
        }
        public async void ClearData()
        {
            try
            {
                var resultado = await Application.Current.MainPage.DisplayAlert("Cancelar", "¿Desea limpiar los datos registrados ?", "Si", "No");
                if (resultado)
                {
                    Instance();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("TakePicture ClearData", ex.ToString(), "Ok");
            }
        }
        public async void DeleteClient()
        {
            try
            {
                var resultado = await Application.Current.MainPage.DisplayAlert("Eliminar","¿Desea eliminar de la lista al cliente " + ClientSelected.nameClient + " ?","Si","No");
                if(resultado)
                {
                    if (LstEvidClients.Count > 1 && ClientSelected.holder)
                    {
                        await Application.Current.MainPage.DisplayAlert("Eliminar Titular", "No puede eliminar el titular", "Ok");
                        return;
                    }
                    if (LstEvidClients.Count == 1)
                    {
                        LstEvidClients.Remove(ClientSelected);
                        await Application.Current.MainPage.DisplayAlert("Cliente Eliminado","El cliente " + ClientSelected.nameClient + " ha sido eliminado corredtamente.","Ok");
                        this.TypeDocSelected = null;
                        this.Identification = null;
                        this.NameClient = null;
                        return;
                    }
                    else if(LstEvidClients.Count > 1 && !ClientSelected.holder)
                    {
                        LstEvidClients.Remove(ClientSelected);
                        await Application.Current.MainPage.DisplayAlert("Cliente Eliminado", "El cliente " + ClientSelected.nameClient + " ha sido eliminado corredtamente.", "Ok");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("TakePicture DeleteClient", ex.ToString(),"Ok");
            }
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
