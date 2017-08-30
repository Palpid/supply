
using HKSupply.DB;
using HKSupply.Exceptions;
using HKSupply.General;
using HKSupply.Services.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using HKSupply.Models.Supply;
using HKSupply.Helpers;
using HKSupply.Classes;
using System.Data;

namespace HKSupply.Services.Implementations
{
    public class EFSupplyDocs : ISupplyDocs
    {
        ILog _log = LogManager.GetLogger(typeof(EFSupplyDocs));

        public DocHead GetDoc(string idDoc)
        {
            try
            {
                if (idDoc == null)
                    throw new ArgumentNullException(nameof(idDoc));

                using (var db = new HKSupplyContext())
                {
                    DocHead doc =  db.DocsHead
                        .Where(a => a.IdDoc.Equals(idDoc))
                        .Include(l => l.Lines)
                        .FirstOrDefault();

                    if (doc != null)
                    {
                        foreach(var line in doc.Lines)
                        {
                            switch (line.IdItemGroup)
                            {
                                case Constants.ITEM_GROUP_EY:
                                    line.Item = GlobalSetting.ItemEyService.GetItem(line.IdItemBcn);
                                    break;

                                case Constants.ITEM_GROUP_MT:
                                    line.Item = GlobalSetting.ItemMtService.GetItem(line.IdItemBcn);
                                    break;

                                case Constants.ITEM_GROUP_HW:
                                    line.Item = GlobalSetting.ItemHwService.GetItem(line.IdItemBcn);
                                    break;
                            }
                        }
                    }

                    return doc;
                }
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

        public List<DocHead> GetDocs(string idSupplier, string idCustomer, DateTime docDate, string IdSupplyDocType, string idSupplyStatus)
        {
            try            {
                if (docDate == null)
                    throw new ArgumentNullException(nameof(docDate));

                using (var db = new HKSupplyContext())
                {
                    var docs =  db.DocsHead.Where(d =>
                        (d.IdSupplier.Equals(idSupplier) || string.IsNullOrEmpty(idSupplier)) &&
                        (d.IdCustomer.Equals(idCustomer) || string.IsNullOrEmpty(idCustomer)) &&
                        (d.IdSupplyStatus.Equals(idSupplyStatus) || string.IsNullOrEmpty(idSupplyStatus)) &&
                        System.Data.Entity.SqlServer.SqlFunctions.DatePart("week", d.DocDate) == System.Data.Entity.SqlServer.SqlFunctions.DatePart("week", docDate) &&
                        (d.IdSupplyDocType.Equals(IdSupplyDocType))
                        )
                        .Include(l => l.Lines)
                        .ToList();

                    foreach(var doc in docs)
                    {
                        foreach (var line in doc.Lines)
                        {
                            line.LineState = DocLine.LineStates.Loaded;

                            switch (line.IdItemGroup)
                            {
                                case Constants.ITEM_GROUP_EY:
                                    line.Item = GlobalSetting.ItemEyService.GetItem(line.IdItemBcn);
                                    break;

                                case Constants.ITEM_GROUP_MT:
                                    line.Item = GlobalSetting.ItemMtService.GetItem(line.IdItemBcn);
                                    break;

                                case Constants.ITEM_GROUP_HW:
                                    line.Item = GlobalSetting.ItemHwService.GetItem(line.IdItemBcn);
                                    break;
                            }
                        }
                    }

                    return docs;
                }
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

        public List<POSelection> GetPOSelection(string idDocPo, string idSupplyStatus, string idSupplier, DateTime PODateIni, DateTime PODateEnd)
        {
            try
            {
                List<POSelection> poSelectionList = new List<POSelection>();

                string txtPODateIni = string.Empty;
                string txtPODateEnd = string.Empty;
                DateTime dateZero = new DateTime(1, 1, 1);
                if (PODateIni != dateZero)
                {
                    var culture = new System.Globalization.CultureInfo("en-US");
                    txtPODateIni = PODateIni.ToString("MM/dd/yyyy", culture);
                    txtPODateEnd = PODateEnd.ToString("MM/dd/yyyy", culture);
                }

                StringBuilder query = new StringBuilder();
                query.Append($"EXEC GET_PURCHASE_ORDER_SELECTION ");
                query.Append($" @pIdDocPo = '{idDocPo}'");
                query.Append($",@pIdSupplyStatus = '{idSupplyStatus}'");
                query.Append($",@pIdSupplier = '{idSupplier}'");
                query.Append($",@pPODateIni = '{txtPODateIni}'");
                query.Append($",@pPODateEnd = '{txtPODateEnd}'");

                using (var db = new HKSupplyContext())
                {
                    DataTable dataTable = db.DataTable(query.ToString());
                    poSelectionList = dataTable.DataTableToList<POSelection>();
                }

                return poSelectionList;
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

        public List<SupplyStatus> GetSupplyStatus()
        {
            try
            {
                using (var db = new HKSupplyContext())
                {
                    return db.SupplyStatus.OrderBy(a => a.Description).ToList();
                }
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

        public DocHead NewDoc(DocHead newDoc)
        {
            try
            {
                if (newDoc == null)
                    throw new ArgumentNullException(nameof(newDoc));

                using (var db = new HKSupplyContext())
                {
                    using (var dbTrans = db.Database.BeginTransaction())
                    {
                        //TODO: comprobar si es necesario recalcular el batch y el PO Number por si se ha creado alguna otra PO desde otro pc (es poco probable, pero es una opción)

                        //número de línea
                        int numLin = 1;

                        //Actualizar el precio por si han modificado alguno mientras dejaban el formulario de PO abierto
                        var supplierPriceList = GlobalSetting.SupplierPriceListService.GetSuppliersPriceList(idItemBcn: null, idSupplier: newDoc.IdSupplier);

                        foreach (var line in newDoc.Lines)
                        {
                            line.NumLin = numLin;
                            line.IdDoc = newDoc.IdDoc;
                            line.QuantityOriginal = line.Quantity;

                            var supplierPrice = supplierPriceList.Where(a => a.IdItemBcn.Equals(line.IdItemBcn)).FirstOrDefault();

                            line.UnitPrice = (supplierPrice == null ? 0 : (short)supplierPrice.Price);
                            line.UnitPriceBaseCurrency = (supplierPrice == null ? 0 : (short)supplierPrice.PriceBaseCurrency);

                            numLin++;
                        }

                        newDoc.User = GlobalSetting.LoggedUser.UserLogin.ToUpper();

                        db.DocsHead.Add(newDoc);

                        /************************************* PK *************************************/
                        
                        if (newDoc.IdSupplyDocType == Constants.SUPPLY_DOCTYPE_PK)
                        {
                            //******* Tenemos que actualizar los datos de la/s Sales Order asociadas a cada línea del packing *******//

                            foreach (var line in newDoc.Lines)
                            {
                                var doclineSo = db.DocsLines.Where(a => a.IdDoc.Equals(line.IdDocRelated) && a.IdItemBcn.Equals(line.IdItemBcn)).FirstOrDefault();
                                if (doclineSo != null)
                                {
                                    doclineSo.DeliveredQuantity += line.Quantity;

                                    if (doclineSo.Quantity == doclineSo.DeliveredQuantity)
                                        doclineSo.IdSupplyStatus = Constants.SUPPLY_STATUS_CLOSE;

                                    db.SaveChanges();
                                }
                            }

                            //******* Hay que comprobar si se ha entregado todo para cerrar la/s sale order asociadas al packing *******//
                            // Obtenemos el listado de Sales Order
                            List<string> salesOrderList = newDoc.Lines.Select(a => a.IdDocRelated).Distinct().ToList();

                            foreach(var salesOrder in salesOrderList)
                            {
                                SqlParameter param1 = new SqlParameter("@pIdDoc", salesOrder);
                                bool checkClose = db.Database.SqlQuery<bool>("CHECK_CLOSE_DOC @pIdDoc", param1).FirstOrDefault();

                                if (checkClose == true)
                                {
                                    //Cerramos la SO
                                    var soDoc = db.DocsHead.Where(a => a.IdDoc.Equals(salesOrder)).FirstOrDefault();
                                    if(soDoc != null)
                                    {
                                        soDoc.IdSupplyDocType = Constants.SUPPLY_STATUS_CLOSE;
                                        db.SaveChanges();
                                    }
                                }

                            }


                            //******* Si es un packing list (PK) hay que generar la Delivery Note asociada *******//
                            //Guardamos los datos porque son necesarios los actualizados para generar la Delivery Note
                            db.SaveChanges();

                            //copiamos las líneas tal cual
                            List<DocLine> linesDnHkFactory = new List<DocLine>();

                            foreach (var line in newDoc.Lines)
                            {
                                DocLine lineDnHkFactory = line.DeepCopyByExpressionTree();
                                lineDnHkFactory.QuantityOriginal = lineDnHkFactory.Quantity;
                                lineDnHkFactory.IdDoc = line.IdDoc.Replace(Constants.SUPPLY_DOCTYPE_PK, Constants.SUPPLY_DOCTYPE_DN);
                                //lineDnHkFactory.IdDocRelated = newDoc.IdDoc; //Mantenemos el original referenciado a la SO
                                lineDnHkFactory.IdSupplyStatus = Constants.SUPPLY_STATUS_CLOSE;
                                linesDnHkFactory.Add(lineDnHkFactory);
                            }

                            DocHead dnHkFactory = new DocHead()
                            {
                                IdDoc = newDoc.IdDoc.Replace(Constants.SUPPLY_DOCTYPE_PK, Constants.SUPPLY_DOCTYPE_DN), //ID? Cómo se linka por el la PO de fábrica
                                IdSupplyDocType = Constants.SUPPLY_DOCTYPE_DN,
                                CreationDate = DateTime.Now,
                                DeliveryDate = newDoc.DeliveryDate,
                                DocDate = newDoc.DocDate,
                                IdSupplyStatus = Constants.SUPPLY_STATUS_CLOSE, 
                                IdSupplier = Constants.ETNIA_HK_COMPANY_CODE, 
                                IdCustomer = newDoc.IdCustomer,
                                DeliveryTerm = newDoc.DeliveryTerm,
                                IdPaymentTerms = newDoc.IdPaymentTerms,
                                IdCurrency = newDoc.IdCurrency,
                                Lines = linesDnHkFactory,
                            };

                            db.DocsHead.Add(dnHkFactory);
                        }

                        db.SaveChanges();
                        dbTrans.Commit();

                        db.Entry(newDoc).GetDatabaseValues();
                        return newDoc;
                    }
                }
            }
            catch (ArgumentNullException nrex)
            {
                _log.Error(nrex.Message, nrex);
                throw nrex;
            }
            catch (DbEntityValidationException e)
            {
                _log.Error(e.Message, e);
                throw e;
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

        public DocHead UpdateDoc(DocHead doc, bool finishDoc = false)
        {
           try
            {
                if (doc == null)
                    throw new ArgumentNullException(nameof(doc));

                using (var db = new HKSupplyContext())
                {
                    using (var dbTrans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            DocHead docToUpdate = GetDoc(doc.IdDoc);

                            if (docToUpdate == null)
                                throw new Exception("DOC error");


                            //número de línea
                            int numLin = 1;

                            string idQP = $"{Constants.SUPPLY_DOCTYPE_QP}{doc.IdDoc}";
                            bool existQP = (db.DocsHead.Where(a => a.IdDoc.Equals(idQP)).Count()) > 0;
                            //bool existQP = false;

                            //Actualizar el precio por si han modificado alguno mientras dejaban el formulario de PO abierto (siempre y cuando no exista ya la QP)
                            List<Models.SupplierPriceList> supplierPriceList = null;

                            if (existQP == false)
                                supplierPriceList = GlobalSetting.SupplierPriceListService.GetSuppliersPriceList(idItemBcn: null, idSupplier: doc.IdSupplier);
                            
                            foreach (var line in doc.Lines)
                            {
                                line.NumLin = numLin;
                                line.IdDoc = doc.IdDoc;

                                if (line.LineState == DocLine.LineStates.New)
                                    line.QuantityOriginal = line.Quantity;

                                if (existQP == false)
                                {
                                    var supplierPrice = supplierPriceList?.Where(a => a.IdItemBcn.Equals(line.IdItemBcn)).FirstOrDefault();

                                    line.UnitPrice = (supplierPrice == null ? 0 : (short)supplierPrice.Price);
                                    line.UnitPriceBaseCurrency = (supplierPrice == null ? 0 : (short)supplierPrice.PriceBaseCurrency);
                                }

                                numLin++;
                            }

                            //Hay que agregarlo al contexto actual para que lo actualice
                            db.DocsHead.Attach(docToUpdate);

                            //Borramos las líneas de la base de datos para insertarla de nuevo
                            var lines = db.DocsLines.Where(a => a.IdDoc.Equals(doc.IdDoc));

                            foreach (var line in lines)
                                db.DocsLines.Remove(line);

                            //y los insertamos de nuevo
                            foreach (var line in doc.Lines)
                                db.DocsLines.Add(line);

                            //user
                            docToUpdate.User = GlobalSetting.LoggedUser.UserLogin.ToUpper();

                            /************************************* PO *************************************/

                            //Si es una Purchase Order y se finaliza hay que generar la Quotation Proposal y la PO de Etnia BCN a Etnia HK
                            if (doc.IdSupplyDocType == Constants.SUPPLY_DOCTYPE_PO && finishDoc == true)
                            {

                                //Guardamos los datos porque son necesarios los actualizados para generar la otra Purcharse ORder y la Quotation Proposal
                                db.SaveChanges();

                                //copiamos las líneas tal cual
                                List<DocLine> linesPoBcnHk = new List<DocLine>();

                                foreach (var line in doc.Lines)
                                {
                                    //DocLine lineSoBcnHk = line.Clone();
                                    DocLine lineSoBcnHk = line.DeepCopyByExpressionTree();
                                    lineSoBcnHk.QuantityOriginal = lineSoBcnHk.Quantity;
                                    lineSoBcnHk.IdDoc = $"{Constants.SUPPLY_DOCTYPE_PO}{docToUpdate.IdDoc}";
                                    lineSoBcnHk.IdDocRelated = docToUpdate.IdDoc;
                                    linesPoBcnHk.Add(lineSoBcnHk);
                                }

                                DocHead poBcnHk = new DocHead()
                                {
                                    IdDoc = $"{Constants.SUPPLY_DOCTYPE_PO}{docToUpdate.IdDoc}", //ID? Cómo se linka por el la PO de fábrica
                                    IdSupplyDocType = Constants.SUPPLY_DOCTYPE_PO,
                                    CreationDate = DateTime.Now,
                                    DeliveryDate = docToUpdate.DeliveryDate,
                                    DocDate = docToUpdate.DocDate,
                                    //TODO: que datos ponemos ??
                                    IdSupplyStatus = Constants.SUPPLY_STATUS_OPEN, //STATUS?
                                    IdSupplier = Constants.ETNIA_HK_COMPANY_CODE, //Etnia HK??
                                    IdCustomer = Constants.ETNIA_BCN_COMPANY_CODE, //Etnia BCN a piñon??
                                    //DeliveryTerm = slueDeliveryTerms.EditValue as string,
                                    //IdPaymentTerms = sluePaymentTerm.EditValue as string,
                                    //IdCurrency = slueCurrency.EditValue as string,
                                    Lines = linesPoBcnHk,
                                };

                                db.DocsHead.Add(poBcnHk);

                                var quotationProposal =  GetQuotationProposal(db, doc);

                                db.DocsHead.Add(quotationProposal);

                            }

                            /************************************* QP *************************************/
                            //Si es una Quotation Proposal y se finaliza hay que generar la Sales Order de Etnia Hk a la fábrica
                            if (doc.IdSupplyDocType == Constants.SUPPLY_DOCTYPE_QP && finishDoc == true)
                            {
                                //Guardamos los datos porque son necesarios los actualizados para generar la Sales Order
                                db.SaveChanges();

                                //copiamos las líneas tal cual
                                List<DocLine> linesSoHkFactory = new List<DocLine>();

                                foreach (var line in doc.Lines)
                                {
                                    DocLine lineSoHkFactory = line.DeepCopyByExpressionTree();
                                    lineSoHkFactory.QuantityOriginal = lineSoHkFactory.Quantity;
                                    lineSoHkFactory.IdDoc = $"{Constants.SUPPLY_DOCTYPE_SO}{line.IdDocRelated}";
                                    lineSoHkFactory.IdDocRelated = docToUpdate.IdDoc;
                                    linesSoHkFactory.Add(lineSoHkFactory);
                                }

                                DocHead soHkFactory = new DocHead()
                                {
                                    IdDoc = docToUpdate.IdDoc.Replace(Constants.SUPPLY_DOCTYPE_QP, Constants.SUPPLY_DOCTYPE_SO), //ID? Cómo se linka por el la PO de fábrica
                                    IdSupplyDocType = Constants.SUPPLY_DOCTYPE_SO,
                                    CreationDate = DateTime.Now,
                                    //    //TODO: que datos ponemos ??
                                    DeliveryDate = docToUpdate.DeliveryDate,
                                    DocDate = docToUpdate.DocDate,
                                    IdSupplyStatus = Constants.SUPPLY_STATUS_OPEN, //STATUS?
                                    IdSupplier = Constants.ETNIA_HK_COMPANY_CODE, //Etnia HK??
                                    IdCustomer =docToUpdate.IdCustomer,
                                    //DeliveryTerm = slueDeliveryTerms.EditValue as string,
                                    //IdPaymentTerms = sluePaymentTerm.EditValue as string,
                                    IdCurrency = docToUpdate.IdCurrency,
                                    Lines = linesSoHkFactory,
                                };

                                db.DocsHead.Add(soHkFactory);
                            }


                            db.SaveChanges();
                            dbTrans.Commit();

                            db.Entry(docToUpdate).GetDatabaseValues();
                            return docToUpdate;


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

        public bool UpdateLinesRemarks(List<DocLine> lines)
        {
            try
            {
                if (lines == null)
                    throw new ArgumentNullException(nameof(lines));

                using (var db = new HKSupplyContext())
                {
                    using (var dbTrans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach(DocLine line in lines)
                            {
                                DocLine lineToUpdate = db.DocsLines.Where(a => a.IdDoc.Equals(line.IdDoc) && a.NumLin.Equals(line.NumLin)).FirstOrDefault();
                                if (lineToUpdate == null)
                                    throw new Exception("Line not found");

                                if (lineToUpdate.Remarks != line.Remarks)
                                {
                                    lineToUpdate.Remarks = line.Remarks;
                                    db.SaveChanges();
                                }
                            }

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

        public bool ValidateBomSupplierLines(string idSupplier, List<DocLine> lines, out List<string> itemWithouBom)
        {
            try
            {
                if (string.IsNullOrEmpty(idSupplier))
                    throw new ArgumentNullException(nameof(idSupplier));

                if (lines == null || lines.Count == 0)
                    throw new ArgumentNullException(nameof(lines));

                /*
                 select 
	                 ey.ID_ITEM_BCN, count(bom.ID_BOM) as SUPPLIER_BOM
                from ITEMS_EY ey
                left join ITEMS_BOM bom on bom.ID_ITEM_BCN = ey.ID_ITEM_BCN and bom.ID_SUPPLIER = 'CR'
                where ey.ID_ITEM_BCN in('4 BORN BZBK','4 BRANDON BLBK 53')
                group by ey.ID_ITEM_BCN, bom.ID_BOM
                 */

                List<string> itemsList = lines.Select(a => a.IdItemBcn).ToList();

                using (var db = new HKSupplyContext())
                {
                    var query = (
                            from itemsEy in db.ItemsEy
                                .Where(itemsEy => itemsList.Contains(itemsEy.IdItemBcn))
                            from itemBom in db.ItemsBom
                                .Where(itemBom => itemBom.IdItemBcn == itemsEy.IdItemBcn && itemBom.IdSupplier.Equals(idSupplier))
                                .DefaultIfEmpty()
                            group new { itemsEy, itemBom } by new { itemsEy.IdItemBcn, itemBom.IdBom } into g
                            select new
                            {
                                IdItemBcn = g.Key.IdItemBcn,
                                SupplierBomCount = g.Count(x => x.itemBom.IdBom != null) // Si puede ser NULL porque es un left join
                            }
                            ).ToList();
                            
                    itemWithouBom = query.Where(a => a.SupplierBomCount == 0).Select(b => b.IdItemBcn).ToList();

                    return (itemWithouBom.Count == 0);

                }

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

        private DocHead GetQuotationProposal(HKSupplyContext db, DocHead purchaseOrder)
        {
            try
            {
                SqlParameter param1 = new SqlParameter("@pIdDocPo", purchaseOrder.IdDoc);
                List<DocLine> lines = db.Database.SqlQuery<DocLine>("GET_QUOTATIO_PROPOSAL_BOM_EXPLOSION @pIdDocPo", param1).ToList();

                DocHead quotationProposal = new DocHead()
                {
                    IdDoc = $"{Constants.SUPPLY_DOCTYPE_QP}{purchaseOrder.IdDoc}",
                    IdSupplyDocType = Constants.SUPPLY_DOCTYPE_QP,
                    CreationDate = DateTime.Now,
                    DeliveryDate = purchaseOrder.DeliveryDate,
                    DocDate = purchaseOrder.DocDate,
                    //TODO: que datos ponemos ??
                    IdSupplyStatus = Constants.SUPPLY_STATUS_OPEN, //STATUS?
                    IdSupplier = Constants.ETNIA_HK_COMPANY_CODE,
                    IdCustomer = purchaseOrder.IdSupplier, //Se invierte el proveedor/cliente?
                    //DeliveryTerm = slueDeliveryTerms.EditValue as string,
                    //IdPaymentTerms = sluePaymentTerm.EditValue as string,
                    //IdCurrency = slueCurrency.EditValue as string,
                    Lines = lines,
                };

                return quotationProposal;
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
