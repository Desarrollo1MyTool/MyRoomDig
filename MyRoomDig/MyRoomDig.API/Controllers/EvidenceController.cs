namespace MyRoomDig.API.Controllers
{
    using Domain;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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
                                      IdTercero = C.idtercero ?? 0,
                                      IdentifClient = C.id,
                                      NameClient = C.name,
                                      FirstName = C.primname,
                                      SecondName = C.segname,
                                      FirstLastName = C.primapel,
                                      SeconLastName = C.segapel

                                  }).ToList();

                    foreach (var item in lsTemp)
                    {
                        CsReturn.Add(new EvidenciasModel
                        {
                            idTercero = item.IdTercero,
                            identifClient = item.IdentifClient,
                            nameClient = item.NameClient,
                            firstName = item.FirstName,
                            secondName = item.SecondName,
                            firstLastName = item.FirstLastName,
                            secondLastName = item.SeconLastName
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
                            name = item.name,
                            nameAux = item.sigla
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
        public async Task<Response> PostEvidences(ObservableCollection<EvidenciasModel> evidenciasModel)
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
                        string codeTipoEvid = "DOCUMENTO";
                        string codeCarpeta = "Soporte";
                        foreach(var item in evidenciasModel)
                        {
                            List<evidencia> lsTemp = db.evidencias.Where(x => x.idIdentifica == item.idTercero).ToList();
                            if(lsTemp.Count == 0)
                            {
                                db.evidencias.Add(new API.Models.evidencia
                                {
                                    id = Guid.NewGuid(),
                                    codeTipoEvid = codeTipoEvid,
                                    codeCarpeta = codeCarpeta,
                                    idSerialNum = 1,
                                    idIdentifica = item.idTercero,
                                    evidencia1 = item.evidencia1,
                                    descripcion = item.descripcion ?? "",
                                    fileName = item.fileName + "_" + 1 + ".jpg" ?? "",
                                    fecha = DateTime.Now,
                                    usuario = item.usuario ?? ""
                                });
                                //db.SaveChanges();
                                //dbContextTransaction.Commit();
                            }
                            else
                            {
                                evidencia evidence = lsTemp.Last();
                                db.evidencias.Add(new API.Models.evidencia
                                {
                                    id = Guid.NewGuid(),
                                    codeTipoEvid = codeTipoEvid,
                                    codeCarpeta = codeCarpeta,
                                    idSerialNum = evidence.idSerialNum + 1,
                                    idIdentifica = item.idTercero,
                                    evidencia1 = item.evidencia1,
                                    descripcion = item.descripcion ?? "",
                                    fileName = item.fileName + "_" + (evidence.idSerialNum + 1) + ".jpg" ?? "",
                                    fecha = DateTime.Now,
                                    usuario = item.usuario ?? ""
                                });
                            }
                        }
                        db.SaveChanges();
                        dbContextTransaction.Commit();
                        //evidenciasModel.codeTipoEvid = "DOCUMENTO";
                        //evidenciasModel.codeCarpeta = "Soporte";
                        //List<evidencia> lsTemp = db.evidencias.Where(x => x.idIdentifica == evidenciasModel.idTercero).ToList();
                        //if (lsTemp.Count == 0)
                        //{
                        //    db.evidencias.Add(new API.Models.evidencia
                        //    {
                        //        id = Guid.NewGuid(),
                        //        codeTipoEvid = evidenciasModel.codeTipoEvid,
                        //        codeCarpeta = evidenciasModel.codeCarpeta,
                        //        idSerialNum = 1,
                        //        idIdentifica = evidenciasModel.idTercero,
                        //        evidencia1 = evidenciasModel.evidencia1,
                        //        descripcion = evidenciasModel.descripcion ?? "",
                        //        fileName = evidenciasModel.fileName + "_" + 1 + ".jpg" ?? "",
                        //        fecha = DateTime.Now,
                        //        usuario = evidenciasModel.usuario ?? ""
                        //    });
                        //    db.SaveChanges();
                        //    dbContextTransaction.Commit();
                        //    response.IsSuccess = true;
                        //    response.Message = "Agregado correctamente";
                        //}
                        //else
                        //{
                        //    evidencia evidence = lsTemp.Last();
                        //        db.evidencias.Add(new API.Models.evidencia
                        //        {
                        //            id = Guid.NewGuid(),
                        //            codeTipoEvid = evidenciasModel.codeTipoEvid,
                        //            codeCarpeta = evidenciasModel.codeCarpeta,
                        //            idSerialNum = evidence.idSerialNum + 1,
                        //            idIdentifica = evidenciasModel.idTercero,
                        //            evidencia1 = evidenciasModel.evidencia1,
                        //            descripcion = evidenciasModel.descripcion ?? "",
                        //            fileName = evidenciasModel.fileName + "_" + (evidence.idSerialNum + 1) + ".jpg" ?? "",
                        //            fecha = DateTime.Now,
                        //            usuario = evidenciasModel.usuario ?? ""
                        //        });
                        //        db.SaveChanges();
                        //        dbContextTransaction.Commit();
                        //        response.IsSuccess = true;
                        //        response.Message = "Agregado correctamente";
                        //}

                        response.IsSuccess = true;
                        response.Message = "Agregado correctamente";
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