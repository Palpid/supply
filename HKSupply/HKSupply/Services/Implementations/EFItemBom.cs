using HKSupply.DB;
using HKSupply.Exceptions;
using HKSupply.General;
using HKSupply.Helpers;
using HKSupply.Models;
using HKSupply.Services.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Services.Implementations
{
    public class EFItemBom : IItemBom
    {
        ILog _log = LogManager.GetLogger(typeof(EFItemBom));

        public ItemBom GetItemBom(int IdBom)
        {
            try
            {
                if (IdBom <= 0)
                    throw new ArgumentNullException(nameof(IdBom));

                using (var db = new HKSupplyContext())
                {
                    var bom = db.ItemsBom
                        .Where(a => a.IdBom.Equals(IdBom))
                        .Include(h => h.Hardwares)
                        .Include(hi => hi.Hardwares.Select(i => i.Item))
                        .Include(m => m.Materials)
                        .Include(mi => mi.Materials.Select(i => i.Item))
                        .FirstOrDefault();

                    if (bom != null)
                    {
                        //En función del tipo cargamos el item, ya que no tiene un tipo definido para tener esta flexibilidad
                        switch (bom.IdItemGroup)
                        {
                            case Constants.ITEM_GROUP_EY:
                                bom.Item = GlobalSetting.ItemEyService.GetItem(bom.IdItemBcn);
                                break;
                        }
                    }

                    return bom;
                }
            }
            catch (ArgumentNullException anex)
            {
                _log.Error(anex.Message, anex);
                throw anex;
            }
            catch (SqlException sqlex)
            {
                for (int i = 0; i < sqlex.Errors.Count; i++)
                {
                    _log.Error("Index #" + i + "\n" +
                        "Message: " + sqlex.Errors[i].Message + "\n" +
                        "Error Number: " + sqlex.Errors[i].Number + "\n" +
                        "LineNumber: " + sqlex.Errors[i].LineNumber + "\n" +
                        "Source: " + sqlex.Errors[i].Source + "\n" +
                        "Procedure: " + sqlex.Errors[i].Procedure + "\n");

                    switch (sqlex.Errors[i].Number)
                    {
                        case -1: //connection broken
                        case -2: //timeout
                            throw new DBServerConnectionException(GlobalSetting.ResManager.GetString("DBServerConnectionError"));
                    }
                }
                throw sqlex;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public ItemBom GetItemBom(string IdItemBcn)
        {
            try
            {
                if (string.IsNullOrEmpty(IdItemBcn))
                    throw new ArgumentNullException(nameof(IdItemBcn));

                using (var db = new HKSupplyContext())
                {
                    var bom = db.ItemsBom
                        .Where(a => a.IdItemBcn.Equals(IdItemBcn))
                        .Include(h => h.Hardwares)
                        .Include(hi => hi.Hardwares.Select(i => i.Item))
                        .Include(m => m.Materials)
                        .Include(mi => mi.Materials.Select(i => i.Item))
                        .FirstOrDefault();

                    if (bom != null)
                    {
                        //En función del tipo cargamos el item, ya que no tiene un tipo definido para tener esta flexibilidad
                        switch (bom.IdItemGroup)
                        {
                            case Constants.ITEM_GROUP_EY:
                                bom.Item = GlobalSetting.ItemEyService.GetItem(bom.IdItemBcn);
                                break;
                        }
                    }

                    return bom;
                }
            }
            catch (ArgumentNullException anex)
            {
                _log.Error(anex.Message, anex);
                throw anex;
            }
            catch (SqlException sqlex)
            {
                for (int i = 0; i < sqlex.Errors.Count; i++)
                {
                    _log.Error("Index #" + i + "\n" +
                        "Message: " + sqlex.Errors[i].Message + "\n" +
                        "Error Number: " + sqlex.Errors[i].Number + "\n" +
                        "LineNumber: " + sqlex.Errors[i].LineNumber + "\n" +
                        "Source: " + sqlex.Errors[i].Source + "\n" +
                        "Procedure: " + sqlex.Errors[i].Procedure + "\n");

                    switch (sqlex.Errors[i].Number)
                    {
                        case -1: //connection broken
                        case -2: //timeout
                            throw new DBServerConnectionException(GlobalSetting.ResManager.GetString("DBServerConnectionError"));
                    }
                }
                throw sqlex;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public bool EditIteBom(ItemBom bom)
        {
            try
            {
                if (bom == null)
                    throw new ArgumentNullException(nameof(bom));

                using (var db = new HKSupplyContext())
                {
                    using (var dbTrans = db.Database.BeginTransaction())
                    {
                        try
                        {

                            //Nota: Al hacer un add, EF agrega al contexto del inset todas las nested properties y también intenta hacerles un insert (familias, item...)
                            //Las seteo a null para que las ignore, no sé si es muy correcto, pero es la única manera que he encontrado ahora.
                            bom.Item = null;
                            foreach (var h in bom.Hardwares)
                                h.Item = null;

                            foreach (var m in bom.Materials)
                                m.Item = null;


                            if (bom.IdBom == 0)  //insert new
                            {
                                bom.IdVer = 1;
                                bom.IdSubVer = 0;
                                bom.Timestamp = DateTime.Now; 
                                bom.CreateDate = DateTime.Now;

                                //EF insertará la cabecera y los details (Materials, hadrwares...)
                                db.ItemsBom.Add(bom);
                            }
                            else  //update
                            {
                                ItemBom bomToUpdate = GetItemBom(bom.IdBom);
                                if (bomToUpdate == null)
                                    throw new Exception("BOM error");

                                //Hay que agregarlo al contexto actual para que lo actualice
                                db.ItemsBom.Attach(bomToUpdate);

                                //modifico las del original para el historial y reaprovecho tanto si es insert como update
                                bom.IdSubVer +=1;
                                bom.Timestamp = DateTime.Now;

                                bomToUpdate.IdSubVer += 1;
                                bomToUpdate.Timestamp = DateTime.Now;

                                //borramos todos los detalles
                                var detailHw = db.DetailsBomHw.Where(a => a.IdBom.Equals(bom.IdBom));
                                var detailMt = db.DetailsBomMt.Where(a => a.IdBom.Equals(bom.IdBom));

                                foreach (var h in detailHw)
                                    db.DetailsBomHw.Remove(h);

                                foreach (var m in detailMt)
                                    db.DetailsBomMt.Remove(m);

                                //e insertamos los nuevos
                                foreach (var h in bom.Hardwares)
                                    db.DetailsBomHw.Add(h);

                                foreach (var m in bom.Materials)
                                    db.DetailsBomMt.Add(m);
                            }


                            //History
                            foreach (var h in bom.Hardwares)
                            {
                                DetailBomHwHistory histHw = h.Clone();
                                histHw.IdVer = bom.IdVer;
                                histHw.IdSubVer = bom.IdSubVer;
                                histHw.Timestamp = bom.Timestamp;

                                db.DetailsBomHwHistory.Add(histHw);
                            }

                            foreach (var m in bom.Materials)
                            {
                                DetailBomMtHistory histMt = m.Clone();
                                histMt.IdVer = bom.IdVer;
                                histMt.IdSubVer = bom.IdSubVer;
                                histMt.Timestamp = bom.Timestamp;

                                db.DetailsBomMtHistory.Add(histMt);
                            }


                            db.SaveChanges();

                            dbTrans.Commit();
                            return true;

                        }
                        catch (SqlException sqlex)
                        {
                            dbTrans.Rollback();

                            for (int i = 0; i < sqlex.Errors.Count; i++)
                            {
                                _log.Error("Index #" + i + "\n" +
                                    "Message: " + sqlex.Errors[i].Message + "\n" +
                                    "Error Number: " + sqlex.Errors[i].Number + "\n" +
                                    "LineNumber: " + sqlex.Errors[i].LineNumber + "\n" +
                                    "Source: " + sqlex.Errors[i].Source + "\n" +
                                    "Procedure: " + sqlex.Errors[i].Procedure + "\n");

                                switch (sqlex.Errors[i].Number)
                                {
                                    case -1: //connection broken
                                    case -2: //timeout
                                        throw new DBServerConnectionException(GlobalSetting.ResManager.GetString("DBServerConnectionError"));
                                }
                            }
                            throw sqlex;
                        }
                        catch (Exception ex)
                        {
                            dbTrans.Rollback();
                            _log.Error(ex.Message, ex);
                            throw ex;
                        }
                    }
                }

            }
            catch (ArgumentNullException anex)
            {
                _log.Error(anex.Message, anex);
                throw anex;
            }
        }
    }
}
