namespace MyRoomDig.API.Controllers
{
    using Domain;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    public class EvidenceController: ApiController
    {
        #region Attributes
        MyRoomDigEntities db = new MyRoomDigEntities();
        #endregion
        #region GET´s
        [Route("api/GetClients")]
        public IQueryable<EvidenciasModel> GetClients(string identification, int idTypeDoc)
        {
           List<EvidenciasModel> CsReturn = new List<EvidenciasModel>();
            try
            {
                using (MyRoomDigEntities dbContext = new MyRoomDigEntities())
                {
                    var lsTemp = (from C in db.clientes
                                  where C.id == identification && C.tipodoc == idTypeDoc
                                  select new
                                  {
                                      NumIdenti = C.idtercero,
                                      IdClient = C.id,
                                      Name = C.name,

                                  }).ToList();

                    foreach (var item in lsTemp)
                    {
                        CsReturn.Add(new EvidenciasModel
                        {
                            numIdenti = item.NumIdenti,
                            idClient = item.IdClient,
                            nameClient = item.Name,
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
          return CsReturn.AsQueryable();
        }
        [Route("api/GetTypeDoc")]
        public IQueryable<KeyValue> GetTypeDoc()
        {
            List<KeyValue> CsReturn = new List<KeyValue>();
            try
            {
                using (MyRoomDigEntities dbContext = new MyRoomDigEntities())
                {
                    var lsTemp = (from TD in db.tipdocs
                                  select TD).ToList();
                    foreach(var item in lsTemp)
                    {
                        CsReturn.Add(new KeyValue
                        {
                            id = item.id,
                            name = item.name
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return CsReturn.AsQueryable();
        }
        #endregion
        #region POST´s
        [ResponseType(typeof(EvidenciasModel))]
        [Route("api/PostEvidences")]
        public async Task<Response> PostEvidences(EvidenciasModel evidenciasModel)
        {
           Response response = new Response();
            try
            {
                if (!ModelState.IsValid || evidenciasModel == null)
                {
                    return response;
                }
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        evidenciasModel.codeTipoEvid = "DOCUMENTO";
                        evidenciasModel.codeCarpeta = "Soporte";
                        List<evidencia> lsTemp = db.evidencias.Where(x => x.idIdentifica == evidenciasModel.numIdenti).ToList();
                        if (lsTemp.Count == 0)
                        {
                            db.evidencias.Add(new API.Models.evidencia
                            {
                                id = Guid.NewGuid(),
                                codeTipoEvid = evidenciasModel.codeTipoEvid,
                                codeCarpeta = evidenciasModel.codeCarpeta,
                                idSerialNum = 1,
                                idIdentifica = evidenciasModel.numIdenti ?? 0,
                                evidencia1 = evidenciasModel.evidencia1,
                                descripcion = evidenciasModel.descripcion ?? "",
                                fileName = evidenciasModel.fileName ?? "",
                                fecha = DateTime.Now,
                                usuario = evidenciasModel.usuario ?? ""
                            });
                            db.SaveChanges();
                            dbContextTransaction.Commit();
                        }
                        else
                        {
                            evidencia evidence = lsTemp.Last();
                                db.evidencias.Add(new API.Models.evidencia
                                {
                                    id = Guid.NewGuid(),
                                    codeTipoEvid = evidenciasModel.codeTipoEvid,
                                    codeCarpeta = evidenciasModel.codeCarpeta,
                                    idSerialNum = evidence.idSerialNum + 1,
                                    idIdentifica = evidenciasModel.numIdenti ?? 0,
                                    evidencia1 = evidenciasModel.evidencia1,
                                    descripcion = evidenciasModel.descripcion ?? "",
                                    fileName = evidenciasModel.fileName ?? "",
                                    fecha = DateTime.Now,
                                    usuario = evidenciasModel.usuario ?? ""
                                });
                                db.SaveChanges();
                                dbContextTransaction.Commit();
                                response.IsSuccess = true;
                                response.Message = "Agregado correctamente";
                        }
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

    }
}