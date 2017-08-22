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

        public ItemBom GetItemSupplierBom(int IdBom, string idSupplier, bool getPoco = false)
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
                        //.Include(hf => hf.HalfFinished)
                        //.Include(hfi => hfi.HalfFinished.Select(i => i.DetailItemBom))
                        .FirstOrDefault();

                    if (bom != null)
                    {
                        //En función del tipo cargamos el item, ya que no tiene un tipo definido para tener esta flexibilidad
                        switch (bom.IdItemGroup)
                        {
                            case Constants.ITEM_GROUP_EY:
                                bom.Item = GlobalSetting.ItemEyService.GetItem(bom.IdItemBcn);
                                break;
                            case Constants.ITEM_GROUP_HF:
                                bom.Item = GlobalSetting.ItemHfService.GetItem(bom.IdItemBcn);
                                break;
                        }

                        //cargamos los half-finished
                        bom.HalfFinished = new List<DetailBomHf>();
                        bom.HalfFinished = null;
                        bom.HalfFinished = db.DetailsBomHf.Where(a => a.IdBom.Equals(bom.IdBom)).ToList();
                        foreach (var hf in bom.HalfFinished)
                        {
                            hf.DetailItemBom = GetItemSupplierBom(db, hf.IdBomDetail, bom.IdSupplier);
                        }
                    }

                    if (getPoco)
                    {
                        ItemBom pocoBom = EntityFrameworkHelper.UnProxy(db, bom);
                        pocoBom.Item = bom.Item.DeepCopyByExpressionTree();
                        pocoBom.Hardwares = new List<DetailBomHw>(bom.Hardwares.Select(x => x.Clone()));
                        pocoBom.Materials = new List<DetailBomMt>(bom.Materials.Select(x => x.Clone()));
                        return pocoBom;
                    }
                    else
                    {
                        return bom;
                    }
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

        public ItemBom GetItemSupplierBom(string IdItemBcn, string idSupplier, bool getPoco = false)
        {
            try
            {
                if (string.IsNullOrEmpty(IdItemBcn))
                    throw new ArgumentNullException(nameof(IdItemBcn));

                using (var db = new HKSupplyContext())
                {
                    var bom = db.ItemsBom
                        .Where(a => a.IdItemBcn.Equals(IdItemBcn) && a.IdSupplier.Equals(idSupplier))
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
                            case Constants.ITEM_GROUP_HF:
                                bom.Item = GlobalSetting.ItemHfService.GetItem(bom.IdItemBcn);
                                break;
                        }

                        //cargamos los half-finished
                        bom.HalfFinished = new List<DetailBomHf>();
                        bom.HalfFinished = null;
                        bom.HalfFinished = db.DetailsBomHf.Where(a => a.IdBom.Equals(bom.IdBom)).ToList();
                        foreach (var hf in bom.HalfFinished)
                        {
                            hf.DetailItemBom = GetItemSupplierBom(db, hf.IdBomDetail, bom.IdSupplier);
                        }
                    }

                    if (getPoco)
                    {
                        ItemBom pocoBom = EntityFrameworkHelper.UnProxy(db, bom);
                        pocoBom.Item = bom.Item.DeepCopyByExpressionTree();
                        pocoBom.Hardwares = new List<DetailBomHw>(bom.Hardwares.Select(x => x.Clone()));
                        pocoBom.Materials = new List<DetailBomMt>(bom.Materials.Select(x => x.Clone()));
                        return pocoBom;
                    }
                    else
                    {
                        return bom;
                    }
                   
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

        public List<ItemBom> GetItemBom(string idItemBcn)
        {
            try
            {
                if (string.IsNullOrEmpty(idItemBcn))
                    throw new ArgumentNullException(nameof(idItemBcn));

                using (var db = new HKSupplyContext())
                {
                    var bomList = db.ItemsBom
                        .Where(a => a.IdItemBcn.Equals(idItemBcn))
                        .Include(h => h.Hardwares)
                        .Include(hi => hi.Hardwares.Select(i => i.Item))
                        .Include(m => m.Materials)
                        .Include(mi => mi.Materials.Select(i => i.Item))

                        .Include(hf => hf.HalfFinished)
                        //.Include(hfi => hfi.HalfFinished.Select(i => i.DetailItemBom))
                        //.Where(hf => hf.IdBom.Equals(1))
                        .ToList();

                    if (bomList != null)
                    {
                        foreach(var bom in bomList)
                        {
                            //En función del tipo cargamos el item, ya que no tiene un tipo definido para tener esta flexibilidad
                            switch (bom.IdItemGroup)
                            {
                                case Constants.ITEM_GROUP_EY:
                                    bom.Item = GlobalSetting.ItemEyService.GetItem(bom.IdItemBcn);
                                    break;
                                case Constants.ITEM_GROUP_HF:
                                    bom.Item = GlobalSetting.ItemHfService.GetItem(bom.IdItemBcn);
                                    break;
                            }

                            //cargamos los half-finished
                            bom.HalfFinished = db.DetailsBomHf.Where(a => a.IdBom.Equals(bom.IdBom)).ToList();
                            foreach(var hf in bom.HalfFinished)
                            {
                                hf.DetailItemBom = GetItemSupplierBom(db, hf.IdBomDetail, bom.IdSupplier);
                            }
                        }
                      
                    }

                    return bomList;

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

        //public bool EditItemBom(ItemBom bom)
        //{
        //    try
        //    {
        //        if (bom == null)
        //            throw new ArgumentNullException(nameof(bom));

        //        using (var db = new HKSupplyContext())
        //        {
        //            using (var dbTrans = db.Database.BeginTransaction())
        //            {
        //                try
        //                {

        //                    //Nota: Al hacer un add, EF agrega al contexto del insert todas las nested properties y también intenta hacerles un insert (familias, item...)
        //                    //Las seteo a null para que las ignore, no sé si es muy correcto, pero es la única manera que he encontrado ahora.
        //                    bom.Item = null;
        //                    foreach (var h in bom.Hardwares)
        //                        h.Item = null;

        //                    foreach (var m in bom.Materials)
        //                        m.Item = null;


        //                    if (bom.IdBom == 0)  //insert new
        //                    {
        //                        bom.IdVer = 1;
        //                        bom.IdSubVer = 0;
        //                        bom.Timestamp = DateTime.Now; 
        //                        bom.CreateDate = DateTime.Now;

        //                        //EF insertará la cabecera y los details (Materials, hadrwares...)
        //                        db.ItemsBom.Add(bom);
        //                    }
        //                    else  //update
        //                    {
        //                        ItemBom bomToUpdate = GetItemSupplierBom(bom.IdBom, bom.IdSupplier);
        //                        if (bomToUpdate == null)
        //                            throw new Exception("BOM error");

        //                        //Hay que agregarlo al contexto actual para que lo actualice
        //                        db.ItemsBom.Attach(bomToUpdate);

        //                        //modifico las del original para el historial y reaprovecho tanto si es insert como update
        //                        bom.IdSubVer +=1;
        //                        bom.Timestamp = DateTime.Now;

        //                        bomToUpdate.IdSubVer += 1;
        //                        bomToUpdate.Timestamp = DateTime.Now;

        //                        //borramos todos los detalles
        //                        var detailHw = db.DetailsBomHw.Where(a => a.IdBom.Equals(bom.IdBom));
        //                        var detailMt = db.DetailsBomMt.Where(a => a.IdBom.Equals(bom.IdBom));

        //                        foreach (var h in detailHw)
        //                            db.DetailsBomHw.Remove(h);

        //                        foreach (var m in detailMt)
        //                            db.DetailsBomMt.Remove(m);

        //                        //e insertamos los nuevos
        //                        foreach (var h in bom.Hardwares)
        //                            db.DetailsBomHw.Add(h);

        //                        foreach (var m in bom.Materials)
        //                            db.DetailsBomMt.Add(m);
        //                    }


        //                    //History
        //                    foreach (var h in bom.Hardwares)
        //                    {
        //                        DetailBomHwHistory histHw = h.Clone();
        //                        histHw.IdVer = bom.IdVer;
        //                        histHw.IdSubVer = bom.IdSubVer;
        //                        histHw.Timestamp = bom.Timestamp;

        //                        db.DetailsBomHwHistory.Add(histHw);
        //                    }

        //                    foreach (var m in bom.Materials)
        //                    {
        //                        DetailBomMtHistory histMt = m.Clone();
        //                        histMt.IdVer = bom.IdVer;
        //                        histMt.IdSubVer = bom.IdSubVer;
        //                        histMt.Timestamp = bom.Timestamp;

        //                        db.DetailsBomMtHistory.Add(histMt);
        //                    }


        //                    db.SaveChanges();

        //                    dbTrans.Commit();
        //                    return true;

        //                }
        //                catch (SqlException sqlex)
        //                {
        //                    dbTrans.Rollback();

        //                    for (int i = 0; i < sqlex.Errors.Count; i++)
        //                    {
        //                        _log.Error("Index #" + i + "\n" +
        //                            "Message: " + sqlex.Errors[i].Message + "\n" +
        //                            "Error Number: " + sqlex.Errors[i].Number + "\n" +
        //                            "LineNumber: " + sqlex.Errors[i].LineNumber + "\n" +
        //                            "Source: " + sqlex.Errors[i].Source + "\n" +
        //                            "Procedure: " + sqlex.Errors[i].Procedure + "\n");

        //                        switch (sqlex.Errors[i].Number)
        //                        {
        //                            case -1: //connection broken
        //                            case -2: //timeout
        //                                throw new DBServerConnectionException(GlobalSetting.ResManager.GetString("DBServerConnectionError"));
        //                        }
        //                    }
        //                    throw sqlex;
        //                }
        //                catch (Exception ex)
        //                {
        //                    dbTrans.Rollback();
        //                    _log.Error(ex.Message, ex);
        //                    throw ex;
        //                }
        //            }
        //        }

        //    }
        //    catch (ArgumentNullException anex)
        //    {
        //        _log.Error(anex.Message, anex);
        //        throw anex;
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemSuppliersBom"></param>
        /// <returns></returns>
        /// <remarks>
        /// Como con el Entity no he conseguido hacer bien la relacíón entre ITEM_BOM y DETAIL_BOM_HF, ya que la ha cogido desde ITEM_BOM.ID_BOM -> DETAIL_BOM_HF.ID_DETAIL_BOM
        /// tengo que ir limpiando las nested propoerties para que no intente hacer los insert/update anidados y de error
        /// </remarks>
        public bool EditItemSuppliersBom(List<ItemBom> itemSuppliersBom)
        {
            try
            {
                if (itemSuppliersBom == null)
                    throw new ArgumentNullException(nameof(itemSuppliersBom));

                using (var db = new HKSupplyContext())
                {
                    using (var dbTrans = db.Database.BeginTransaction())
                    {
                        try
                        {

                            foreach(ItemBom supplierBom in itemSuppliersBom)
                            {
                                //Nota: Al hacer un add, EF agrega al contexto del insert todas las nested properties y también intenta hacerles un insert (familias, item...)
                                //Las seteo a null para que las ignore, no sé si es muy correcto, pero es la única manera que he encontrado ahora.
                                supplierBom.Item = null;
                                foreach (var h in supplierBom.Hardwares)
                                    h.Item = null;

                                foreach (var m in supplierBom.Materials)
                                    m.Item = null;

                                //Los semielaborados me los guardo para las adelante
                                List<DetailBomHf> detailBomHfListTmp = new List<DetailBomHf>();
                                foreach(var hf in supplierBom.HalfFinished)
                                {
                                    DetailBomHf tmp = new DetailBomHf()
                                    {
                                        IdBom = hf.IdBom,
                                        IdBomDetail = hf.IdBomDetail,
                                        DetailItemBom = new ItemBom()
                                        {
                                            IdItemBcn = hf.DetailItemBom.IdItemBcn,
                                            IdVer = hf.DetailItemBom.IdVer,
                                            IdSubVer = hf.DetailItemBom.IdSubVer,
                                            Timestamp = hf.DetailItemBom.Timestamp
                                        },
                                        Quantity = hf.Quantity,
                                    };
                                    detailBomHfListTmp.Add(tmp);
                                    hf.DetailItemBom = null;
                                }

                                //--------INSERT--------//
                                if (supplierBom.IdBom == 0) 
                                {
                                    supplierBom.IdVer = 1;
                                    supplierBom.IdSubVer = 0;
                                    supplierBom.Timestamp = DateTime.Now;
                                    supplierBom.CreateDate = DateTime.Now;

                                    supplierBom.HalfFinished = null;
                                    //EF insertará la cabecera y los details de Materials y hardwares
                                    db.ItemsBom.Add(supplierBom);

                                    db.SaveChanges(); //para poder recuperar el id insertado

                                    db.Entry(supplierBom).GetDatabaseValues();

                                    //Los semielaborados los trato a parte
                                    foreach (var hf in detailBomHfListTmp)
                                    {
                                        //Detail
                                        DetailBomHf detailTmp = new DetailBomHf
                                        {
                                            IdBom = supplierBom.IdBom,
                                            IdBomDetail = hf.IdBomDetail,
                                            Quantity = hf.Quantity
                                        };

                                        hf.IdBom = supplierBom.IdBom;

                                        //History
                                        DetailBomHfHistory histHf = hf.Clone();
                                        histHf.IdVerBom = supplierBom.IdVer;
                                        histHf.IdSubVerBom = supplierBom.IdSubVer;
                                        histHf.TimestampBom = supplierBom.Timestamp;

                                        histHf.IdVerBomDetail = hf.DetailItemBom.IdVer;
                                        histHf.IdSubVerBomDetail = hf.DetailItemBom.IdSubVer;
                                        histHf.TimestampBomDetail = hf.DetailItemBom.Timestamp;

                                        histHf.User = GlobalSetting.LoggedUser.UserLogin;

                                        //Inserts into DB
                                        db.DetailsBomHf.Add(detailTmp);
                                        db.DetailsBomHfHistory.Add(histHf);

                                        

                                    }
                                    db.SaveChanges();
                                }
                                //--------UPDATE--------//
                                else
                                {
                                    //GetItemSupplierBom
                                    ItemBom bomToUpdate = db.ItemsBom.Where(a => a.IdBom.Equals(supplierBom.IdBom)).Single(); //OK

                                    if (bomToUpdate == null)
                                        throw new Exception("BOM error");

                                    //modifico las del original para el historial y reaprovecho tanto si es insert como update
                                    supplierBom.IdSubVer += 1;
                                    supplierBom.Timestamp = DateTime.Now;

                                    bomToUpdate.IdSubVer += 1;
                                    bomToUpdate.Timestamp = DateTime.Now;

                                    //borramos todos los detalles
                                    var detailHw = db.DetailsBomHw.Where(a => a.IdBom.Equals(supplierBom.IdBom));
                                    var detailMt = db.DetailsBomMt.Where(a => a.IdBom.Equals(supplierBom.IdBom));
                                    var detailHf = db.DetailsBomHf.Where(a => a.IdBom.Equals(supplierBom.IdBom));

                                    foreach (var h in detailHw)
                                        db.DetailsBomHw.Remove(h);

                                    foreach (var m in detailMt)
                                        db.DetailsBomMt.Remove(m);

                                    foreach (var hf in detailHf)
                                        db.DetailsBomHf.Remove(hf);

                                    //e insertamos los nuevos
                                    foreach (var h in supplierBom.Hardwares)
                                        db.DetailsBomHw.Add(h);

                                    foreach (var m in supplierBom.Materials)
                                        db.DetailsBomMt.Add(m);


                                    foreach (var hf in supplierBom.HalfFinished)
                                    {
                                        //Detail
                                        db.DetailsBomHf.Add(hf);

                                        //History
                                        var tmpHf = detailBomHfListTmp.Where(a => a.IdBomDetail.Equals(hf.IdBomDetail)).Single();
                                        DetailBomHfHistory histHf = hf.Clone();
                                        histHf.IdVerBom = supplierBom.IdVer;
                                        histHf.IdSubVerBom = supplierBom.IdSubVer;
                                        histHf.TimestampBom = supplierBom.Timestamp;

                                        histHf.IdVerBomDetail = tmpHf.DetailItemBom.IdVer;
                                        histHf.IdSubVerBomDetail = tmpHf.DetailItemBom.IdSubVer;
                                        histHf.TimestampBomDetail = tmpHf.DetailItemBom.Timestamp;

                                        histHf.User = GlobalSetting.LoggedUser.UserLogin;

                                        db.DetailsBomHfHistory.Add(histHf);
                                    }
                                        
                                    
                                }

                                //History de Hardware y materiales, comnún para insert y update
                                foreach (var h in supplierBom.Hardwares)
                                {
                                    DetailBomHwHistory histHw = h.Clone();
                                    histHw.IdVer = supplierBom.IdVer;
                                    histHw.IdSubVer = supplierBom.IdSubVer;
                                    histHw.Timestamp = supplierBom.Timestamp;
                                    histHw.User = GlobalSetting.LoggedUser.UserLogin;

                                    db.DetailsBomHwHistory.Add(histHw);
                                }

                                foreach (var m in supplierBom.Materials)
                                {
                                    DetailBomMtHistory histMt = m.Clone();
                                    histMt.IdVer = supplierBom.IdVer;
                                    histMt.IdSubVer = supplierBom.IdSubVer;
                                    histMt.Timestamp = supplierBom.Timestamp;
                                    histMt.User = GlobalSetting.LoggedUser.UserLogin;

                                    db.DetailsBomMtHistory.Add(histMt);
                                }


                                db.SaveChanges();

                                //Test Crear BOM de Frontal y varillas
                                if(supplierBom.IdItemGroup == Constants.ITEM_GROUP_EY && supplierBom.IdSupplier != Constants.INTRANET_ETNIA_BCN)
                                    CreateFrontTempleBom(db, supplierBom);


                                //Si es un semielaborado tenemos que actualizar la versión de todo lo que lo incluya
                                UpdateVersionRelatedItems(db, supplierBom);
                            }


                            //db.SaveChanges();

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

        public List<ItemBom> GetRelatedItemBom(int idBomn)
        {
           try
            {
                using (var db = new DB.HKSupplyContext())
                {
                    var list = db.ItemsBom
                        .Join(
                            db.DetailsBomHf,
                            itemBom => itemBom.IdBom,
                            detail => detail.IdBom,
                            (itemBom, detail) => new { ItemBom = itemBom, DetailBomHf = detail }
                            )
                        .Where(a => a.DetailBomHf.IdBomDetail.Equals(idBomn))
                        .Select(a => a.ItemBom)
                        .ToList();

                    return list;


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

        #region Private Methods

        /// <summary>
        /// Setear a null los propiedades que con clases complejas
        /// </summary>
        /// <param name="bom"></param>
        /// <remarks>
        /// Al hacer un add por ejemplo, EF agrega al contexto del inset todas las nested properties y también intenta hacerles un insert (familias, item...)
        /// Las seteo a null para que las ignore, no sé si es muy correcto, pero es la única manera que he encontrado ahora.
        /// </remarks>
        //private void SetComplexPropertiesToNUll(ItemBom bom)
        //{
        //    try
        //    {
        //        bom.Item = null;
        //        foreach (var h in bom.Hardwares)
        //            h.Item = null;

        //        foreach (var m in bom.Materials)
        //            m.Item = null;

        //        foreach(var hf in bom.HalfFinished)
        //            SetComplexPropertiesToNUll(hf.DetailItemBom);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        //private void EditItemSupplierBom(HKSupplyContext db, ItemBom bom)
        //{
        //    try
        //    {
        //        if (bom.IdBom == 0)  //insert new
        //        {

        //        }
        //        else  //update
        //        {
        //            ItemBom bomToUpdate = GetItemSupplierBom(bom.IdBom, bom.IdSupplier);
        //            if (bomToUpdate == null)
        //                throw new Exception("BOM error");

        //            //Hay que agregarlo al contexto actual para que lo actualice
        //            db.ItemsBom.Attach(bomToUpdate);

        //            bomToUpdate.IdSubVer += 1;
        //            bomToUpdate.Timestamp = DateTime.Now;

        //            //-------Primero tratamos Material y Hardware, que es más simple, sólo hay que borrar e insertar de nuevo -------//

        //            //borramos todos los detalles
        //            var detailHw = db.DetailsBomHw.Where(a => a.IdBom.Equals(bom.IdBom));
        //            var detailMt = db.DetailsBomMt.Where(a => a.IdBom.Equals(bom.IdBom));

        //            foreach (var h in detailHw)
        //                db.DetailsBomHw.Remove(h);

        //            foreach (var m in detailMt)
        //                db.DetailsBomMt.Remove(m);

        //            //e insertamos los nuevos
        //            foreach (var h in bom.Hardwares)
        //                db.DetailsBomHw.Add(h);

        //            foreach (var m in bom.Materials)
        //                db.DetailsBomMt.Add(m);

        //            //------- -------//
        //            foreach (var hf in bom.HalfFinished)
        //            {
        //                if (hf.IdBomDetail == 0)
        //                {
        //                    //insertarlo y recuperar su id
        //                }

        //            }

        //        }

        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}




        private ItemBom GetItemSupplierBom(HKSupplyContext db, int IdBom, string idSupplier, bool getPoco = false)
        {
            try
            {
                if (IdBom <= 0)
                    throw new ArgumentNullException(nameof(IdBom));


                var bom = db.ItemsBom
                    .Where(a => a.IdBom.Equals(IdBom))
                    .Include(h => h.Hardwares)
                    .Include(hi => hi.Hardwares.Select(i => i.Item))
                    .Include(m => m.Materials)
                    .Include(mi => mi.Materials.Select(i => i.Item))
                    //.Include(hf => hf.HalfFinished)
                    //.Include(hfi => hfi.HalfFinished.Select(i => i.DetailItemBom))
                    .FirstOrDefault();

                if (bom != null)
                {
                    //En función del tipo cargamos el item, ya que no tiene un tipo definido para tener esta flexibilidad
                    switch (bom.IdItemGroup)
                    {
                        case Constants.ITEM_GROUP_EY:
                            bom.Item = GlobalSetting.ItemEyService.GetItem(bom.IdItemBcn);
                            break;
                        case Constants.ITEM_GROUP_HF:
                            bom.Item = GlobalSetting.ItemHfService.GetItem(bom.IdItemBcn);
                            break;
                    }

                    //cargamos los half-finished
                    bom.HalfFinished = new List<DetailBomHf>();
                    bom.HalfFinished = null;
                    bom.HalfFinished = db.DetailsBomHf.Where(a => a.IdBom.Equals(bom.IdBom)).ToList();
                    foreach (var hf in bom.HalfFinished)
                    {
                        hf.DetailItemBom = GetItemSupplierBom(db, hf.IdBomDetail, bom.IdSupplier);
                    }
                }

                if (getPoco)
                {
                    ItemBom pocoBom = EntityFrameworkHelper.UnProxy(db, bom);
                    pocoBom.Item = bom.Item.DeepCopyByExpressionTree();
                    pocoBom.Hardwares = new List<DetailBomHw>(bom.Hardwares.Select(x => x.Clone()));
                    pocoBom.Materials = new List<DetailBomMt>(bom.Materials.Select(x => x.Clone()));
                    return pocoBom;
                }
                else
                {
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

        /// <summary>
        /// Actualizar la versión de los items que incluyen un semielaborado que ha sido modificado
        /// </summary>
        private void UpdateVersionRelatedItems(HKSupplyContext db, ItemBom itemBom)
        {
            try
            {
                if (itemBom.IdItemGroup == Constants.ITEM_GROUP_HF)
                {
                    var detailsHf = db.DetailsBomHf.
                        Where(a => a.IdBomDetail.Equals(itemBom.IdBom)).
                        ToList();

                    if(detailsHf != null && detailsHf.Count > 0)
                    {
                        foreach (var detail in detailsHf)
                        {
                            //buscamos la cabecera
                            var itemBomUv = db.ItemsBom.Where(a => a.IdBom.Equals(detail.IdBom)).Single();

                            itemBomUv.IdSubVer += 1;
                            itemBomUv.Timestamp = DateTime.Now;

                            //Historificamos Harware
                            foreach (var h in itemBomUv.Hardwares)
                            {
                                DetailBomHwHistory histHw = h.Clone();
                                histHw.IdVer = itemBomUv.IdVer;
                                histHw.IdSubVer = itemBomUv.IdSubVer;
                                histHw.Timestamp = itemBomUv.Timestamp;
                                histHw.User = GlobalSetting.LoggedUser.UserLogin;

                                db.DetailsBomHwHistory.Add(histHw);
                            }
                            db.SaveChanges();

                            foreach (var m in itemBomUv.Materials)
                            {
                                DetailBomMtHistory histMt = m.Clone();
                                histMt.IdVer = itemBomUv.IdVer;
                                histMt.IdSubVer = itemBomUv.IdSubVer;
                                histMt.Timestamp = itemBomUv.Timestamp;
                                histMt.User = GlobalSetting.LoggedUser.UserLogin;

                                db.DetailsBomMtHistory.Add(histMt);
                            }

                            db.SaveChanges();

                            //Half-finished (semielaborados). Por el tema de que no he sabido hacer bien la relación con el EF hay que buscarlos por separado
                            itemBomUv.HalfFinished = null;

                            var itemBomUvDetailHf = db.DetailsBomHf.Where(a => a.IdBom.Equals(itemBomUv.IdBom)).ToList();

                            foreach (var hf in itemBomUvDetailHf)
                            {
                                hf.DetailItemBom = db.ItemsBom.Where(a => a.IdBom.Equals(hf.IdBomDetail)).Single();

                                DetailBomHfHistory histHf = new DetailBomHfHistory();

                                histHf.IdBom = hf.IdBom;
                                histHf.IdBomDetail = hf.IdBomDetail;
                                histHf.Quantity = hf.Quantity;


                                histHf.IdVerBom = itemBomUv.IdVer;
                                histHf.IdSubVerBom = itemBomUv.IdSubVer;
                                histHf.TimestampBom = itemBomUv.Timestamp;

                                if (hf.IdBomDetail == itemBom.IdBom)
                                {
                                    histHf.IdVerBomDetail = itemBom.IdVer;
                                    histHf.IdSubVerBomDetail = itemBom.IdSubVer;
                                    histHf.TimestampBomDetail = itemBom.Timestamp;
                                    
                                }
                                else
                                {
                                    histHf.IdVerBomDetail = hf.DetailItemBom.IdVer;
                                    histHf.IdSubVerBomDetail = hf.DetailItemBom.IdSubVer;
                                    histHf.TimestampBomDetail = hf.DetailItemBom.Timestamp;
                                }

                                histHf.User = GlobalSetting.LoggedUser.UserLogin;

                                db.DetailsBomHfHistory.Add(histHf);
                            }
                            db.SaveChanges();
                        }
                    }

                    db.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }

        #region Test Generación BOM frontal/varillas a partir del breakdown de la gafa
        private void CreateFrontTempleBom(HKSupplyContext db, ItemBom eyBomSupplier)
        {
            try
            {
                //Buscamos el frontal y las varillas. Vienen en el BOM de la intranet
                /* Ejemplo de query de lo que buscamos
                 select ihf.ID_ITEM_BCN 
                    from ITEMS_BOM  ib
                    inner join DETAIL_BOM_HF dbhf on dbhf.ID_BOM = ib.ID_BOM
                    inner join ITEMS_BOM ibDetail on ibDetail.ID_BOM = dbhf.ID_BOM_DETAIL
                    inner join ITEMS_HF ihf on ihf.ID_ITEM_BCN = ibDetail.ID_ITEM_BCN and ihf.ID_ITEM_TYPE = '02' -- 02 = FRONTAL
                    where ib.ID_ITEM_BCN like '5 AKANE BLBE'
                    and ib.ID_SUPPLIER = 'INTRANET'
                 */

                List<string> parts = new List<string>();
                parts.Add(Constants.ITEM_TYPE_FRONT); 
                parts.Add(Constants.ITEM_TYPE_LEFT_TEMPLE); 
                parts.Add(Constants.ITEM_TYPE_RIGHT_TEMPLE); 

                var queryParts = (from ib in db.ItemsBom
                                  where ib.IdItemBcn.Equals((eyBomSupplier.IdItemBcn))
                                  join dbhf in db.DetailsBomHf on ib.IdBom equals dbhf.IdBom              
                                  join ibDetail in db.ItemsBom on dbhf.IdBomDetail equals ibDetail.IdBom  
                                  join ihf in db.ItemsHf on ibDetail.IdItemBcn equals ihf.IdItemBcn       
                                  where parts.Contains(ihf.IdItemType)
                                  select (new { ihf.IdItemBcn, ihf.IdItemType })).ToList();

                var hfFront = queryParts.Where(a => a.IdItemType.Equals(Constants.ITEM_TYPE_FRONT)).FirstOrDefault();
                var hfTempleLeft = queryParts.Where(a => a.IdItemType.Equals(Constants.ITEM_TYPE_LEFT_TEMPLE)).FirstOrDefault();
                var hfTempleRight = queryParts.Where(a => a.IdItemType.Equals(Constants.ITEM_TYPE_RIGHT_TEMPLE)).FirstOrDefault();

                if (hfFront == null)
                    throw new Exception("Front not exist");

                if (hfTempleLeft == null)
                    throw new Exception("Left Temple not exist");

                if (hfTempleRight == null)
                    throw new Exception("Right Temple not exist");

                /********************************************************************************************
                 *****                                      FRONT                                       *****
                 ********************************************************************************************/
                //Buscamos si ya existe el BOM para ese HF/Supplier
                ItemBom bomHfFront = new ItemBom();
                //Uso dos objetos  porque lo genera como un proxy de EF y luego complica la vida si modificamos sobre el mismo
                ItemBom bomHfFrontTmp = db.ItemsBom.Where(a => a.IdItemBcn.Equals(hfFront.IdItemBcn) && a.IdSupplier.Equals(eyBomSupplier.IdSupplier)).FirstOrDefault();

                //Buscamos los raw materials y el hardware que pertecen al frontal
                var frontBreakdown = db.BomBreakdown.Where(a => a.SubGroup.Equals(Constants.BREAKDOWN_SUBGROUP_FRONT)).Select(b => b.IdBomBreakdown).ToList();

                var rawMaterialsFront = eyBomSupplier.Materials.Where(a => frontBreakdown.Contains(a.IdBomBreakdown)).ToList();
                var hardwareFront = eyBomSupplier.Hardwares.Where(a => frontBreakdown.Contains(a.IdBomBreakdown)).ToList();

                if (bomHfFrontTmp == null)
                {
                    bomHfFront.IdBom = 0;
                    bomHfFront.IdItemBcn = hfFront.IdItemBcn;
                    bomHfFront.IdVer = 1;
                    bomHfFront.IdSubVer = 0;
                    bomHfFront.Timestamp = DateTime.Now;
                    bomHfFront.CreateDate = DateTime.Now;
                    bomHfFront.IdItemGroup = Constants.ITEM_GROUP_HF;
                    bomHfFront.IdSupplier = eyBomSupplier.IdSupplier;
                }
                else
                {
                    bomHfFront.IdBom = bomHfFrontTmp.IdBom;
                    bomHfFront.IdItemBcn = bomHfFrontTmp.IdItemBcn;
                    bomHfFront.IdVer = bomHfFrontTmp.IdVer;
                    bomHfFront.IdSubVer = bomHfFrontTmp.IdSubVer;
                    bomHfFront.Timestamp = bomHfFrontTmp.Timestamp;
                    bomHfFront.CreateDate = bomHfFrontTmp.CreateDate;
                    bomHfFront.IdItemGroup = bomHfFrontTmp.IdItemGroup;
                    bomHfFront.IdSupplier = bomHfFrontTmp.IdSupplier;
                }

                //hardware y raw material, común tanto si es nuevo como un update
                bomHfFront.Materials = new List<DetailBomMt>();
                bomHfFront.Hardwares = new List<DetailBomHw>();

                foreach (var m in rawMaterialsFront)
                {
                    DetailBomMt tmpMt = m.Clone();
                    tmpMt.IdBom = bomHfFront.IdBom;
                    bomHfFront.Materials.Add(tmpMt);
                }

                foreach (var h in hardwareFront)
                {
                    DetailBomHw tmphw = h.Clone();
                    tmphw.IdBom = bomHfFront.IdBom;
                    bomHfFront.Hardwares.Add(tmphw);
                }

                //Save to DB
                EditBomFrontTemple(db, bomHfFront);

                /********************************************************************************************
                 *****                                     TEMPLES                                      *****
                 ********************************************************************************************/

                //Buscamos si ya existe los BOM para ese HF/Supplier
                ItemBom bomHfTempleRight = new ItemBom();
                ItemBom bomHfTempleLeft = new ItemBom();
                ItemBom bomHfTempleRightTmp = db.ItemsBom.Where(a => a.IdItemBcn.Equals(hfTempleRight.IdItemBcn) && a.IdSupplier.Equals(eyBomSupplier.IdSupplier)).FirstOrDefault();
                ItemBom bomHfTempleLeftTmp = db.ItemsBom.Where(a => a.IdItemBcn.Equals(hfTempleLeft.IdItemBcn) && a.IdSupplier.Equals(eyBomSupplier.IdSupplier)).FirstOrDefault();

                //Si no existe creamos el objeto
                if (bomHfTempleRightTmp == null)
                {
                    bomHfTempleRight.IdBom = 0;
                    bomHfTempleRight.IdItemBcn = hfTempleRight.IdItemBcn;
                    bomHfTempleRight.IdVer = 1;
                    bomHfTempleRight.IdSubVer = 0;
                    bomHfTempleRight.Timestamp = DateTime.Now;
                    bomHfTempleRight.CreateDate = DateTime.Now;
                    bomHfTempleRight.IdItemGroup = Constants.ITEM_GROUP_HF;
                    bomHfTempleRight.IdSupplier = eyBomSupplier.IdSupplier;
                }
                else
                {
                    bomHfTempleRight.IdBom = bomHfTempleRightTmp.IdBom;
                    bomHfTempleRight.IdItemBcn = bomHfTempleRightTmp.IdItemBcn;
                    bomHfTempleRight.IdVer = bomHfTempleRightTmp.IdVer;
                    bomHfTempleRight.IdSubVer = bomHfTempleRightTmp.IdSubVer;
                    bomHfTempleRight.Timestamp = bomHfTempleRightTmp.Timestamp;
                    bomHfTempleRight.CreateDate = bomHfTempleRightTmp.CreateDate;
                    bomHfTempleRight.IdItemGroup = bomHfTempleRightTmp.IdItemGroup;
                    bomHfTempleRight.IdSupplier = bomHfTempleRightTmp.IdSupplier;
                }

                if (bomHfTempleLeftTmp == null)
                {
                    bomHfTempleLeft.IdBom = 0;
                    bomHfTempleLeft.IdItemBcn = hfTempleLeft.IdItemBcn;
                    bomHfTempleLeft.IdVer = 1;
                    bomHfTempleLeft.IdSubVer = 0;
                    bomHfTempleLeft.Timestamp = DateTime.Now;
                    bomHfTempleLeft.CreateDate = DateTime.Now;
                    bomHfTempleLeft.IdItemGroup = Constants.ITEM_GROUP_HF;
                    bomHfTempleLeft.IdSupplier = eyBomSupplier.IdSupplier;
                }
                else
                {
                    bomHfTempleLeft.IdBom = bomHfTempleLeftTmp.IdBom;
                    bomHfTempleLeft.IdItemBcn = bomHfTempleLeftTmp.IdItemBcn;
                    bomHfTempleLeft.IdVer = bomHfTempleLeftTmp.IdVer;
                    bomHfTempleLeft.IdSubVer = bomHfTempleLeftTmp.IdSubVer;
                    bomHfTempleLeft.Timestamp = bomHfTempleLeftTmp.Timestamp;
                    bomHfTempleLeft.CreateDate = bomHfTempleLeftTmp.CreateDate;
                    bomHfTempleLeft.IdItemGroup = bomHfTempleLeftTmp.IdItemGroup;
                    bomHfTempleLeft.IdSupplier = bomHfTempleLeftTmp.IdSupplier;
                }


                /********************* RAW MATERIAL & HARDWARE COMMON (BOTH TEMPLES) *********************/

                //Buscamos los raw materials y el hardware que pertecen a las varillas y con comunes a ambas
                var templeBreakdown = db.BomBreakdown.Where(a => a.SubGroup.Equals(Constants.BREAKDOWN_SUBGROUP_TEMPLE)).Select(b => b.IdBomBreakdown).ToList();

                var rawMaterialsTemples = eyBomSupplier.Materials.Where(a => templeBreakdown.Contains(a.IdBomBreakdown)).ToList();
                var hardwareTemples = eyBomSupplier.Hardwares.Where(a => templeBreakdown.Contains(a.IdBomBreakdown)).ToList();

                //Tenemos que ir dividiendo la cantidad entre dos para dividirlo entre las dos varillas
                List<DetailBomMt> rawMaterialsTempleRight = new List<DetailBomMt>();
                List<DetailBomMt> rawMaterialsTempleLeft = new List<DetailBomMt>();

                List<DetailBomHw> hardwareTempleRight = new List<DetailBomHw>();
                List<DetailBomHw> hardwareTempleLeft = new List<DetailBomHw>();

                foreach (var rm in rawMaterialsTemples)
                {
                    DetailBomMt rmRight = new DetailBomMt()
                    {
                        IdBom = bomHfTempleRight.IdBom,
                        IdItemBcn = rm.IdItemBcn,
                        IdBomBreakdown = rm.IdBomBreakdown,
                        Length = null,
                        Width = null,
                        Height = null,
                        Density = null,
                        NumberOfParts = null,
                        Coefficient1 = null,
                        Coefficient2 = null,
                        Scrap = null,
                        Quantity = (rm.Quantity / 2)
                    };

                    //Clonamos la varilla izquierda
                    DetailBomMt rmLeft = rmRight.Clone();
                    rmLeft.IdBom = bomHfTempleLeft.IdBom;

                    rawMaterialsTempleRight.Add(rmRight);
                    rawMaterialsTempleLeft.Add(rmLeft);
                }

                foreach (var hw in hardwareTemples)
                {
                    DetailBomHw hwRight = new DetailBomHw()
                    {
                        IdBom = bomHfTempleRight.IdBom,
                        IdItemBcn = hw.IdItemBcn,
                        IdBomBreakdown = hw.IdBomBreakdown,
                        Scrap = hw.Scrap,
                        Quantity = (hw.Quantity / 2)
                    };

                    //Clonamos la varilla izquierda
                    DetailBomHw hwLeft = hwRight.Clone();
                    hwLeft.IdBom = bomHfTempleLeft.IdBom;

                    hardwareTempleRight.Add(hwRight);
                    hardwareTempleLeft.Add(hwLeft);

                }

                /********************* RAW MATERIAL & HARDWARE ONLY TEMPLE LEFT *********************/

                var leftTempleBreakdown = db.BomBreakdown.Where(a => a.SubGroup.Equals(Constants.BREAKDOWN_SUBGROUP_TEMPLE_LEFT)).Select(b => b.IdBomBreakdown).ToList();

                var rawMaterialsLeftTemple = eyBomSupplier.Materials.Where(a => leftTempleBreakdown.Contains(a.IdBomBreakdown)).ToList();
                var hardwareLeftTemple = eyBomSupplier.Hardwares.Where(a => leftTempleBreakdown.Contains(a.IdBomBreakdown)).ToList();

                foreach(var m in rawMaterialsLeftTemple)
                {
                    DetailBomMt tmpMt = m.Clone();
                    tmpMt.IdBom = bomHfTempleLeft.IdBom;
                    rawMaterialsTempleLeft.Add(tmpMt);
                }

                foreach(var h in hardwareLeftTemple)
                {
                    DetailBomHw tmphw = h.Clone();
                    tmphw.IdBom = bomHfTempleLeft.IdBom;
                    hardwareTempleLeft.Add(tmphw);
                }

                /********************* RAW MATERIAL & HARDWARE ONLY TEMPLE RIGHT *********************/

                var rightTempleBreakdown = db.BomBreakdown.Where(a => a.SubGroup.Equals(Constants.BREAKDOWN_SUBGROUP_TEMPLE_RIGHT)).Select(b => b.IdBomBreakdown).ToList();

                var rawMaterialsRightTemple = eyBomSupplier.Materials.Where(a => rightTempleBreakdown.Contains(a.IdBomBreakdown)).ToList();
                var hardwareRightTemple = eyBomSupplier.Hardwares.Where(a => rightTempleBreakdown.Contains(a.IdBomBreakdown)).ToList();

                foreach(var m in rawMaterialsRightTemple)
                {
                    DetailBomMt tmpMt = m.Clone();
                    tmpMt.IdBom = bomHfTempleRight.IdBom;
                    rawMaterialsTempleRight.Add(tmpMt);
                }

                foreach(var h in hardwareRightTemple)
                {
                    DetailBomHw tmpHw = h.Clone();
                    tmpHw.IdBom = bomHfTempleRight.IdBom;
                    hardwareTempleRight.Add(tmpHw);
                }
                /********************* SAVE TO DB TEMPLES BOM *********************/

                //Agregamos el hardware y el material a su BOM
                bomHfTempleRight.Materials = rawMaterialsTempleRight;
                bomHfTempleRight.Hardwares = hardwareTempleRight;

                bomHfTempleLeft.Materials = rawMaterialsTempleLeft;
                bomHfTempleLeft.Hardwares = hardwareTempleLeft;

                //Save both temples to DB
                EditBomFrontTemple(db, bomHfTempleLeft);
                EditBomFrontTemple(db, bomHfTempleRight);

            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Crear/Modificar el BOM del frontal y las varillas. 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="bom"></param>
        /// <remarks>Es la versíón "simplificada" sin los semielaborados (HF), ya que actualmente no se van a usar y complican el proceso.</remarks>
        private void EditBomFrontTemple(HKSupplyContext db, ItemBom bom)
        {
            try
            {
                //--------INSERT--------//
                if (bom.IdBom == 0)
                {
                    bom.IdVer = 1;
                    bom.IdSubVer = 0;
                    bom.Timestamp = DateTime.Now;
                    bom.CreateDate = DateTime.Now;

                    db.ItemsBom.Add(bom);

                    db.SaveChanges(); //para poder recuperar el id insertado

                    db.Entry(bom).GetDatabaseValues();
                    
                    db.SaveChanges();
                }
                //--------UPDATE--------//
                else
                {
                    //GetItemSupplierBom
                    ItemBom bomToUpdate = db.ItemsBom.Where(a => a.IdBom.Equals(bom.IdBom)).Single();

                    if (bomToUpdate == null)
                        throw new Exception("BOM error");

                    //modifico las del original para el historial y reaprovecho tanto si es insert como update
                    bom.IdSubVer += 1;
                    bom.Timestamp = DateTime.Now;

                    bomToUpdate.IdSubVer += 1;
                    bomToUpdate.Timestamp = DateTime.Now;

                    //borramos todos los detalles
                    var detailHw = db.DetailsBomHw.Where(a => a.IdBom.Equals(bom.IdBom));
                    var detailMt = db.DetailsBomMt.Where(a => a.IdBom.Equals(bom.IdBom));

                    db.DetailsBomHw.RemoveRange(detailHw);
                    db.DetailsBomMt.RemoveRange(detailMt);


                    //e insertamos los nuevos
                    foreach (var h in bom.Hardwares)
                        db.DetailsBomHw.Add(h);

                    foreach (var m in bom.Materials)
                        db.DetailsBomMt.Add(m);
                }

                //History de Hardware y materiales, común para insert y update
                foreach (var h in bom.Hardwares)
                {
                    DetailBomHwHistory histHw = h.Clone();
                    histHw.IdVer = bom.IdVer;
                    histHw.IdSubVer = bom.IdSubVer;
                    histHw.Timestamp = bom.Timestamp;
                    histHw.User = GlobalSetting.LoggedUser.UserLogin;

                    db.DetailsBomHwHistory.Add(histHw);
                }

                foreach (var m in bom.Materials)
                {
                    DetailBomMtHistory histMt = m.Clone();
                    histMt.IdVer = bom.IdVer;
                    histMt.IdSubVer = bom.IdSubVer;
                    histMt.Timestamp = bom.Timestamp;
                    histMt.User = GlobalSetting.LoggedUser.UserLogin;

                    db.DetailsBomMtHistory.Add(histMt);
                }

                db.SaveChanges();

            }
            catch
            {

                throw;
            }
        }

        #endregion

        #endregion

    }
}
