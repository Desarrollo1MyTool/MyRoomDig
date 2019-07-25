using GalaSoft.MvvmLight.Command;
using MyRoomDig.Domain;
using MyRoomDig.Services;
using MyRoomDig.Views;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyRoomDig.ViewModels
{
    public class AddClientViewModel:BaseViewModel
    {
        #region Services
        public ApiServices apiService;
        #endregion

        #region Attributes
        private bool isProcessing;
        private bool withData;
        private bool _dataNewClient;
        private int _idIdentifica;
        private int countList;
        private string _identification;
        private string _firstName;
        private string _secondName;
        private string _firstLastName;
        private string _secondLastName;
        private string _nameClient;
        private string _gustos;
        private KeyValue _typeDocSelected;
        private ClientesModel _newClient;
        private ObservableCollection<KeyValue> _typesDoc;
        private ClientesModel _clientSave;
        private EvidenciasModel _evidences;
        #endregion

        #region Properties
        public bool DataNewClient
        {
            get { return this._dataNewClient; }
            set
            {
                SetValue(ref this._dataNewClient, value);
                if (withData) this._dataNewClient = false;
            }
        }
        public int IdIdentifica
        {
            get {return this._idIdentifica; }
            set {SetValue(ref this._idIdentifica, value); }
        }
        public int IdTypeDoc
        {
            get;
            set;
        }
        public string Identification
        {
            get {return this._identification; }
            set {SetValue(ref this._identification, value); }
        }
        public string FirstName
        {
            get {return this._firstName; }
            set {SetValue(ref this._firstName, value); }
        }
        public string SecondName
        {
            get {return this._secondName; }
            set {SetValue(ref this._secondName, value); }
        }
        public string FirstLastName
        {
            get {return this._firstLastName; }
            set {SetValue(ref this._firstLastName, value); }
        }
        public string SecondLastName
        {
            get {return this._secondLastName; }
            set {SetValue(ref this._secondLastName, value); }
        }
        public string NameClient
        {
            get {return this._nameClient; }
            set {SetValue(ref this._nameClient, value); }
        }
        public string Gustos
        {
            get { return this._gustos; }
            set { SetValue(ref this._gustos, value); }
        }
        public KeyValue TypeDocSelected
        {
            get {return this._typeDocSelected; }
            set {SetValue(ref this._typeDocSelected, value); }
        }
        public ClientesModel NewClient
        {
            get {return this._newClient; }
            set {SetValue(ref this._newClient, value); }
        }
        public ObservableCollection<KeyValue> TypesDoc
        {
            get { return this._typesDoc; }
            set { SetValue(ref this._typesDoc, value); }
        }
        public ClientesModel ClientSave
        {
            get { return this._clientSave; }
            set { SetValue(ref this._clientSave, value); }
        }
        public EvidenciasModel Evidences
        { 
            get { return this._evidences; }
            set { SetValue(ref this._evidences, value); }
        }

        #endregion

        #region Commands
        public ICommand SearchClientCommand { get { return new RelayCommand(CallSearchData); } }
        public ICommand AddClientCommand { get { return new RelayCommand(AddClient); } }
        public ICommand ClearCommand { get { return new RelayCommand(ClearData); } }
        #endregion

        #region Constructors
        public AddClientViewModel()
        {
            Instance();
        }
        #endregion

        #region Methods
        private async void Instance()
        {
            try
            {
                this.apiService = new ApiServices();
                this.NewClient = new ClientesModel();
                this.IdIdentifica = 0;
                this.Identification = string.Empty;
                this.FirstName = string.Empty;
                this.SecondName = string.Empty;
                this.FirstLastName = string.Empty;
                this.SecondLastName = string.Empty;
                this.NameClient = string.Empty;
                this.Gustos = string.Empty;
                this.withData = false;
                this.DataNewClient = false;
                this.IdTypeDoc = 0;
                SearchServices searchService = new SearchServices();
                Evidences = new EvidenciasModel();
                ClientSave = new ClientesModel();
                TypesDoc = await searchService.GetTypeDoc();
                TypeDocSelected = TypesDoc.FirstOrDefault(x => x.id == IdTypeDoc);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("AddClient Instance", ex.ToString(),"Ok");
            }
        }
        public async void CallSearchData()
        {
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
                this.IdIdentifica = Evidences.idTercero;
                if (Evidences.idTercero == 0)
                {
                    this.DataNewClient = true;
                    this.withData = false;
                }
                else
                {
                    withData = true;
                    isProcessing = false;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("TakePicture SearchData", ex.Message, "Ok");
            }
        }
        private async void AddClient()
        {
            try
            {
                if (isProcessing) return;
                isProcessing = true;
                bool ask = false;
                EvidenciasModel objLstEvidClients = new EvidenciasModel();

                if (string.IsNullOrEmpty(this.FirstName) && !this.withData)
                {
                    await Application.Current.MainPage.DisplayAlert("", "Ingrese Primer Nombre", "Ok");
                    isProcessing = false;
                    return;
                }
                if (string.IsNullOrEmpty(this.FirstLastName) && !this.withData)
                {
                    await Application.Current.MainPage.DisplayAlert("", "Ingrese Primer Apellido", "Ok");
                    isProcessing = false;
                    return;
                }

                countList = MainViewModel.GetInstance().Documentation.LstEvidClients.Count();
                objLstEvidClients.nameClient = this.NameClient;
                objLstEvidClients.identifClient = this.Identification;
                objLstEvidClients.idTercero = this.IdIdentifica;
                objLstEvidClients.sigla = this.TypeDocSelected.nameAux;
                objLstEvidClients.holder = countList == 0 ? true : false;

                if (Evidences.idTercero == 0)
                {
                    var resultado = await Application.Current.MainPage.DisplayAlert("Crear Cliente", "¿Desea crear cliente : " + this.FirstName + " " + this.FirstLastName + " ?", "Si", "No");
                    if (resultado)
                    {
                        ClientSave.TypeDoc = this.TypeDocSelected.id;
                        ClientSave.Id = this.Identification;
                        ClientSave.FirstName = this.FirstName;
                        ClientSave.SecondName = this.SecondName == null ? "" : this.SecondName;
                        ClientSave.FirstLastName = this.FirstLastName;
                        ClientSave.SecondLastName = this.SecondLastName == null ? "" : this.SecondLastName;
                        ClientSave.NameComplete = this.FirstName + " " + this.SecondName + " " + this.FirstLastName + " " + this.SecondLastName;
                        ClientSave.Gustos = this.Gustos;
                        ClientSave.fec_Graba = DateTime.Now;
                        ClientSave.Usuario = MainViewModel.GetInstance().Login.User;

                        var apiS = Application.Current.Resources["APISecurity"].ToString();
                        var response = await this.apiService.PostBool(
                                              apiS,
                                              "/api",
                                              "/PostNewClient",
                                              ClientSave);

                        if (!response.IsSuccess)
                        {
                            await Application.Current.MainPage.DisplayAlert(
                               "Error AP-Client",
                               response.Message,
                               "Ok");
                            isProcessing = false;
                            return;
                        }
                        ask = true;
                        this.IdIdentifica = Convert.ToInt32(response.Result);
                        this.NameClient = ClientSave.NameComplete;
                        objLstEvidClients.idTercero = this.IdIdentifica;
                        objLstEvidClients.nameClient = this.NameClient;
                    }
                }
                EvidenciasModel evidenciasAdd = MainViewModel.GetInstance().Documentation.LstEvidClients.FirstOrDefault(x => x.idTercero == this.IdIdentifica);
                if (evidenciasAdd == null)
                {
                    var resultado2 = ask ? true : await Application.Current.MainPage.DisplayAlert("Agregar Cliente", "¿Desea agregar cliente : " + this.NameClient + " ?", "Si", "No");
                    if (resultado2)
                    {
                        if(objLstEvidClients.holder)
                        {
                            MainViewModel.GetInstance().Documentation.NameClient = this.NameClient;
                            MainViewModel.GetInstance().Documentation.Identification = this.Identification;
                            MainViewModel.GetInstance().Documentation.TypeDocSelected = MainViewModel.GetInstance().Documentation.TypesDoc.FirstOrDefault(x => x.id == TypeDocSelected.id);
                        }
                        MainViewModel.GetInstance().Documentation.LstEvidClients.Add(objLstEvidClients);
                        await Application.Current.MainPage.DisplayAlert("Agregado", "Agregado correctamente", "Ok");
                        Instance();
                        await Application.Current.MainPage.Navigation.PopModalAsync();
                    }
                    isProcessing = false;
                    return;
                }
                await Application.Current.MainPage.DisplayAlert("", "El cliente " + this.NameClient + " ya se encuentra en la lista", "Ok");
                Instance();
               
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("AddClient AddClient", ex.ToString(), "Ok");
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
                await Application.Current.MainPage.DisplayAlert("AddClient ClearData", ex.ToString(), "Ok");
            }
        }
        #endregion
    }
}
