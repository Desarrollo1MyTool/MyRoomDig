using MyRoomDig.Domain;
using MyRoomDig.ViewModels;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MyRoomDig.Services
{
    public class SearchServices 
    {
        #region Services
        private ApiServices apiService;
        #endregion

        #region Attributes
        #endregion

        #region Properties
        #endregion

        #region Constructor
        public SearchServices()
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
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("SearchService Instance", ex.ToString(), "Ok");
            }
        }
        public async Task<ObservableCollection<KeyValue>> GetTypeDoc()
        {
            ObservableCollection<KeyValue> TypesDoc = new ObservableCollection<KeyValue>();
            try
            {
                //await Task.Delay(500);
                var apiRoom = Application.Current.Resources["APISecurity"].ToString();
                var response = await this.apiService.GetList<KeyValue>(apiRoom, "api/GetTypeDoc", "");
                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error A-TypeDoc",
                        response.Message,
                        "Ok");
                    return TypesDoc;
                }
                foreach (var item in (List<KeyValue>)response.Result)
                {
                    TypesDoc.Add(new KeyValue
                    {
                        id = item.id,
                        name = item.name,
                        nameAux = item.nameAux
                    });
                }
            }
            catch (Exception ex)
            {
            }
            return TypesDoc;
        }
        public async Task<EvidenciasModel> SearchData(EvidenciasModel SearchEvidences)
        {
            EvidenciasModel EvidenceData = new EvidenciasModel();
            try
            {
                var apiRoom = Application.Current.Resources["APISecurity"].ToString();
                var response = await this.apiService.GetList<EvidenciasModel>(apiRoom, "api/GetClients?identification=" + SearchEvidences.identifClient + "&idTypeDoc=" + SearchEvidences.typeDoc, "");
                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error A-Clients",
                        response.Message,
                        "Ok");
                    return EvidenceData;
                }
                List<EvidenciasModel> lsTemp = (List<EvidenciasModel>)response.Result;
                foreach (var item in (List<EvidenciasModel>)response.Result)
                {
                    EvidenceData.identifClient = item.identifClient;
                    EvidenceData.idTercero = item.idTercero;
                    EvidenceData.firstName = item.firstName;
                    EvidenceData.secondName = item.secondName;
                    EvidenceData.firstLastName = item.firstLastName;
                    EvidenceData.secondLastName = item.secondLastName;
                    EvidenceData.nameClient = item.nameClient;
                }
                if (lsTemp.Count == 0)
                {
                    EvidenceData.nameClient = "No se encuentra ningun cliente con los datos ingresados.";
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("TakePicture SearchData", ex.Message, "Ok");
            }
            return EvidenceData;
        }
        #endregion
    }
}
