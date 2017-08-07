
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

        public List<DocHead> GetDocs(string idSupplier, string idCustomer, DateTime docDate, string IdSupplyDocType)
        {
            try
            {
                if (docDate == null)
                    throw new ArgumentNullException(nameof(docDate));

                using (var db = new HKSupplyContext())
                {
                    var docs =  db.DocsHead.Where(d =>
                        (d.IdSupplier.Equals(idSupplier) || string.IsNullOrEmpty(idSupplier)) &&
                        (d.IdCustomer.Equals(idSupplier) || string.IsNullOrEmpty(idCustomer)) &&
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

        public DocHead UpdateDoc(DocHead doc, bool finishPO = false)
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

                            //Si es una Purchase Order y se finaliza hay que generar la Sales Order de BCN a HK y la Quotation Proposal
                            if (finishPO == true)
                            {

                                //Guardamos los datos porque son necesarios los actualizados para generar la Sales Order y la Quotation Proposal
                                db.SaveChanges();

                                //copiamos las líneas tal cual
                                List<DocLine> linesSoBcnHk = new List<DocLine>();

                                foreach (var line in doc.Lines)
                                {
                                    //DocLine lineSoBcnHk = line.Clone();
                                    DocLine lineSoBcnHk = line.DeepCopyByExpressionTree();
                                    lineSoBcnHk.QuantityOriginal = lineSoBcnHk.Quantity;
                                    lineSoBcnHk.IdDoc = $"{Constants.SUPPLY_DOCTYPE_SO}{docToUpdate.IdDoc}";
                                    linesSoBcnHk.Add(lineSoBcnHk);
                                }

                                DocHead soBcnHk = new DocHead()
                                {
                                    IdDoc = $"{Constants.SUPPLY_DOCTYPE_SO}{docToUpdate.IdDoc}",
                                    IdSupplyDocType = Constants.SUPPLY_DOCTYPE_SO,
                                    CreationDate = DateTime.Now,
                                    DeliveryDate = docToUpdate.DeliveryDate,
                                    DocDate = docToUpdate.DocDate,
                                    //TODO: que datos ponemos ??
                                    IdSupplyStatus = Constants.SUPPLY_STATUS_OPEN, //STATUS?
                                    //IdSupplier = slueSupplier.EditValue as string, //Etnia HK??
                                    //IdCustomer = //TODO: Etnia a piñon??
                                    //DeliveryTerm = slueDeliveryTerms.EditValue as string,
                                    //IdPaymentTerms = sluePaymentTerm.EditValue as string,
                                    //IdCurrency = slueCurrency.EditValue as string,
                                    Lines = linesSoBcnHk,
                                };

                                db.DocsHead.Add(soBcnHk);

                                var quotationProposal =  GetQuotationProposal(db, doc);

                                db.DocsHead.Add(quotationProposal);

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
                    //IdSupplier = slueSupplier.EditValue as string, //Etnia HK??
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
