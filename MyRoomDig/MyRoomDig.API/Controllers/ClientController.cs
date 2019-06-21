namespace MyRoomDig.API.Controllers
{
    using MyRoomDig.API.Models;
    using MyRoomDig.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    public class ClientController : ApiController
    {
        #region Attributes
        MyRoomDigEntities db = new MyRoomDigEntities(); 
        #endregion

        #region GET´s

        #endregion

        #region POST´s
        [ResponseType(typeof(ClientesModel))]
        [Route("api/PostNewClient")]
        public async Task<Response> PostNewClient(ClientesModel clienteModel)
        {
            Response response = new Response();
            try
            {
                if(!ModelState.IsValid || clienteModel == null)
                {
                    return response;
                }
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var lastClient = db.clientes.OrderByDescending(x => x.idtercero).FirstOrDefault();
                        db.clientes.Add(new API.Models.cliente
                        {
                            tipodoc = clienteModel.TypeDoc,
                            id = clienteModel.Id,
                            primname = clienteModel.FirstName.ToUpper(),
                            segname = clienteModel.SecondName.ToUpper(),
                            primapel = clienteModel.FirstLastName.ToUpper(),
                            segapel = clienteModel.SecondLastName.ToUpper(),
                            name = clienteModel.NameComplete.ToUpper(),
                            idtercero = lastClient.idtercero + 1,
                            Gustos = clienteModel.Gustos
                        });
                        clienteModel.IdTercero = (lastClient.idtercero + 1) ?? 0;
                        db.SaveChanges();
                        dbContextTransaction.Commit();
                        response.IsSuccess = true;
                        response.Message = "Agregado correctamente";
                        response.Result = clienteModel.IdTercero;
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        response.Message = ex.ToString();
                        return response;
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
                return response;
            }
        }
        #endregion

        #region Methods

        #endregion
    }
}