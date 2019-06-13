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
        MyDigEntities db = new MyDigEntities();
        #endregion
        #region GET´s
        [Route("api/GetClients")]
        public IQueryable<EvidenciasModel> GetClients(string identification)
        {
            try
            {
                List<EvidenciasModel> CsReturn = new List<EvidenciasModel>();
                using (MyDigEntities dbContext = new MyDigEntities())
                {
                    var lsTemp = (from C in db.clientes
                                  join E in db.evidencias
                                  on C.idtercero equals E.idIdentifica into EvidenciasNull
                                  from ENL in EvidenciasNull.DefaultIfEmpty()
                                  where C.id == identification && ENL.idIdentifica == C.idtercero
                                  select new
                                  {
                                      IdClient = C.numint,
                                      NumIdenti = C.idtercero,
                                      TypeDoc = C.tipodoc,
                                      Name = C.name,
                                      FileName = ENL.fileName,
                                      Picture = ENL.evidencia1,
                                      Date = ENL.fecha,
                                      User = ENL.usuario,
                                      Description = ENL.descripcion,
                                      CodeTypeEvi = ENL.codeTipoEvid,
                                      CodeFolder = ENL.codeCarpeta,
                                      IdSerialNum = ENL.idSerialNum

                                  }).ToList();
                    foreach (var item in lsTemp)
                    {
                        CsReturn.Add(new EvidenciasModel
                        {
                            codeTipoEvid = item.CodeTypeEvi,
                            codeCarpeta = item.CodeFolder,
                            idIdentifica = item.NumIdenti ?? 0,
                            idSerialNum = item.IdSerialNum,
                            evidencia1 = item.Picture,
                            descripcion = item.Description,
                            fileName = item.FileName,
                            fecha = item.Date,
                            usuario = item.User
                        });
                    }
                }
                return CsReturn.AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
        #region POST´s
        [ResponseType(typeof(EvidenciasModel))]
        [Route("api/PostEvidences")]
        public async Task<bool> PostEvidences(EvidenciasModel evidenciasModel)
        {
            try
            {
                if(!ModelState.IsValid || evidenciasModel == null)
                {
                    return false;
                }
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.evidencias.Add(new API.Models.evidencia
                        {
                            codeTipoEvid = evidenciasModel.codeTipoEvid ??"",
                            codeCarpeta = evidenciasModel.codeCarpeta ?? "",
                            idIdentifica = evidenciasModel.idIdentifica == null ? 0 : evidenciasModel.idIdentifica,
                            idSerialNum = evidenciasModel.idSerialNum == null ? 0 : evidenciasModel.idSerialNum,
                            evidencia1 = evidenciasModel.evidencia1 == null ? new byte[0] : evidenciasModel.evidencia1,
                            descripcion = evidenciasModel.descripcion ?? "",
                            fileName = evidenciasModel.fileName ?? "",
                            fecha = evidenciasModel.fecha == null ? DateTime.Now : evidenciasModel.fecha,
                            usuario = evidenciasModel.usuario ?? ""
                        });
                        db.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion
    }
}