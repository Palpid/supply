
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
                        .Include(s => s.SupplyDocType)
                        .FirstOrDefault();

                    if (doc != null)
                    {
                        foreach(var line in doc.Lines)
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

        public DocHead GetDocByRelated(string idDocRelated)
        {
            try
            {
                if (idDocRelated == null)
                    throw new ArgumentNullException(nameof(idDocRelated));

                using (var db = new HKSupplyContext())
                {
                    DocHead doc = db.DocsHead
                        .Where(a => a.IdDocRelated.Equals(idDocRelated))
                        .Include(l => l.Lines)
                        .Include(s => s.SupplyDocType)
                        .FirstOrDefault();

                    if (doc != null)
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

        public List<DocHead> GetDocsByRelated(string idDocRelated)
        {
            try
            {
                if (idDocRelated == null)
                    throw new ArgumentNullException(nameof(idDocRelated));

                using (var db = new HKSupplyContext())
                {
                    var docs = db.DocsHead
                        .Where(a => a.IdDocRelated.Equals(idDocRelated))
                        .Include(l => l.Lines)
                        .Include(s => s.SupplyDocType)
                        .ToList();

                    if (docs.Count > 0)
                    {
                        foreach (var doc in docs)
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

        public List<DocHead> GetDocs(string idSupplier, string idCustomer, DateTime docDate, string IdSupplyDocType, string idSupplyStatus)
        {
            try            {
                if (docDate == null)
                    throw new ArgumentNullException(nameof(docDate));

                using (var db = new HKSupplyContext())
                {
                    DateTime dateZero = new DateTime(1, 1, 1);

                    var docs =  db.DocsHead.Where(d =>
                        (d.IdSupplier.Equals(idSupplier) || string.IsNullOrEmpty(idSupplier)) &&
                        (d.IdCustomer.Equals(idCustomer) || string.IsNullOrEmpty(idCustomer)) &&
                        (d.IdSupplyStatus.Equals(idSupplyStatus) || string.IsNullOrEmpty(idSupplyStatus)) &&
                        (System.Data.Entity.SqlServer.SqlFunctions.DatePart("week", d.DocDate) == System.Data.Entity.SqlServer.SqlFunctions.DatePart("week", docDate) || docDate == dateZero) &&
                        (d.IdSupplyDocType.Equals(IdSupplyDocType))
                        )
                        .Include(l => l.Lines)
                        .Include(s => s.SupplyDocType)
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

        public List<DocHead> GetDocs(string idDoc, string idSupplier, string idCustomer, DateTime docDateIni, DateTime docDateEnd, string IdSupplyDocType, string idSupplyStatus)
        {
            try
            {
                using (var db = new HKSupplyContext())
                {
                    DateTime dateZero = new DateTime(1, 1, 1);

                    var docs = db.DocsHead.Where(d =>
                        
                        (d.IdDoc.Equals(idDoc) || string.IsNullOrEmpty(idDoc)) &&
                        (d.IdSupplier.Equals(idSupplier) || string.IsNullOrEmpty(idSupplier)) &&
                        (d.IdCustomer.Equals(idCustomer) || string.IsNullOrEmpty(idCustomer)) &&
                        ((d.DocDate >= docDateIni && d.DocDate <= docDateEnd) || docDateIni == dateZero) &&
                        (d.IdSupplyDocType.Equals(IdSupplyDocType) || string.IsNullOrEmpty(IdSupplyDocType)) &&
                        (d.IdSupplyStatus.Equals(idSupplyStatus) || string.IsNullOrEmpty(idSupplyStatus)) 
                        )
                        .Include(l => l.Lines)
                        .Include(s => s.SupplyDocType)
                        .OrderBy(o => o.DocDate)
                        .ToList();

                    foreach (var doc in docs)
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

        public List<DocHead> GetSalesOrderFromPackingList(string idDocPK)
        {
            try
            {
                if (string.IsNullOrEmpty(idDocPK))
                    throw new ArgumentNullException(nameof(idDocPK));

                using (var db = new HKSupplyContext())
                {
                    var packingSalesOrders = db.DocsLines.Where(a => a.IdDoc.Equals(idDocPK)).Select(b => b.IdDocRelated).ToList();

                    var docs = db.DocsHead.Where(a => packingSalesOrders.Contains(a.IdDoc)).ToList();

                    foreach (var doc in docs)
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

        public List<SupplyDocType> GetSupplyDocTypes()
        {
            try
            {
                using (var db = new HKSupplyContext())
                {
                    return db.SupplyDocTypes.ToList();
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

                            bool existQP = (db.DocsHead.Where(a => a.IdDocRelated.Equals(doc.IdDoc) && a.IdSupplyDocType.Equals(Constants.SUPPLY_DOCTYPE_QP)).Count()) > 0;

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

                            //actualizamos el estado
                            docToUpdate.IdSupplyStatus = doc.IdSupplyStatus;

                            //Borramos las líneas de la base de datos para insertarla de nuevo
                            var lines = db.DocsLines.Where(a => a.IdDoc.Equals(doc.IdDoc));

                            foreach (var line in lines)
                                db.DocsLines.Remove(line);

                            //y los insertamos de nuevo
                            foreach (var line in doc.Lines)
                                db.DocsLines.Add(line);

                            //user
                            docToUpdate.User = GlobalSetting.LoggedUser.UserLogin.ToUpper();

                            db.SaveChanges();

                            /************************************* PO *************************************/
                            // Al modificar hay que actualizar las cantidades de la PO de BCN a HK y la QP
                            if (doc.IdSupplyDocType == Constants.SUPPLY_DOCTYPE_PO && existQP == true)
                            {
                                db.SaveChanges();

                                //Obtenemos la PO de BCN a HK
                                var idPoBcnHk = db.DocsHead.Where(a => a.IdSupplyDocType.Equals(Constants.SUPPLY_DOCTYPE_PO) && a.IdDocRelated.Equals(docToUpdate.IdDoc)).Select(b => b.IdDoc).FirstOrDefault();
                                if (string.IsNullOrEmpty(idPoBcnHk) == false)
                                {
                                    UpdateLineQty(db, idPoBcnHk, docToUpdate.Lines);
                                }

                                //Obtenemos la QP asociada
                                var idQP = db.DocsHead.Where(a => a.IdSupplyDocType.Equals(Constants.SUPPLY_DOCTYPE_QP) && a.IdDocRelated.Equals(docToUpdate.IdDoc)).Select(b => b.IdDoc).FirstOrDefault();
                                if (string.IsNullOrEmpty(idQP) == false)
                                {
                                    //tenememos que obtener los nuevos datos en la explosión del BOM
                                    //Sólo nos interesan las líneas en esta llamada, la cabecera no
                                    //updatamos la qty original, ya que la qty la puede haber modificado el usuario
                                    var quotationProposalLines = GetQuotationProposal(db, doc).Lines;
                                    UpdateLineQtyOriginal(db, idQP, quotationProposalLines);
                                }
                            }

                            //Si es una Purchase Order y se finaliza hay que generar la Quotation Proposal y la PO de Etnia BCN a Etnia HK
                            if (doc.IdSupplyDocType == Constants.SUPPLY_DOCTYPE_PO && finishDoc == true)
                            {

                                //Guardamos los datos porque son necesarios los actualizados para generar la otra Purcharse ORder y la Quotation Proposal
                                db.SaveChanges();

                                //PO Bcn to HK
                                var poBcnHk = GetPurchaseOrderBcn2Hk(docToUpdate);
                                db.DocsHead.Add(poBcnHk);
                                //QP
                                var quotationProposal =  GetQuotationProposal(db, doc);
                                db.DocsHead.Add(quotationProposal);

                            }

                            /************************************* QP *************************************/
                            //Si es una Quotation Proposal y se finaliza hay que generar la Sales Order de Etnia Hk a la fábrica
                            if (doc.IdSupplyDocType == Constants.SUPPLY_DOCTYPE_QP && finishDoc == true)
                            {
                                //Guardamos los datos porque son necesarios los actualizados para generar la Sales Order
                                db.SaveChanges();

                                DocHead soHkFactory = GetSalesOrderHk2Factory(docToUpdate);
                                db.DocsHead.Add(soHkFactory);
                                
                            }

                            /************************************* SO *************************************/
                            //Si todas las líneas están canceladas o cerradas cerramos el documento
                            if (doc.IdSupplyDocType == Constants.SUPPLY_DOCTYPE_SO)
                            {
                                db.SaveChanges();

                                //Comprobamos si hay que cerrar el documento
                                if (docToUpdate.IdSupplyStatus == Constants.SUPPLY_STATUS_OPEN)
                                {
                                    SqlParameter param1 = new SqlParameter("@pIdDoc", docToUpdate.IdDoc);
                                    bool checkClose = db.Database.SqlQuery<bool>("CHECK_CLOSE_DOC @pIdDoc", param1).FirstOrDefault();
                                    if (checkClose == true)
                                    {
                                        docToUpdate.IdSupplyStatus = Constants.SUPPLY_STATUS_CLOSE;
                                    }
                                }
                            }


                            /************************************* PK *************************************/
                            //Si es un Packing List y se finaliza hay que generar la Delivery Note, la Invoice
                            //y actualizar los datos en las Sales orders asociadas

                            if (doc.IdSupplyDocType == Constants.SUPPLY_DOCTYPE_PL && finishDoc == true)
                            {
                                db.SaveChanges();

                                //actualizar los datos en las Sales orders asociadas
                                UpdateSoAssociatedToPk(db, docToUpdate);

                                //Generar la Delivery Note de HK a la fábrica
                                DocHead dnHkFactory = GetDeliveryNoteHk2Factory(docToUpdate);
                                db.DocsHead.Add(dnHkFactory);

                                //Generar la Invice de HK a la fábrica
                                DocHead ivHkFactory = GetInvoiceHk2Factory(dnHkFactory);
                                db.DocsHead.Add(ivHkFactory);

                            }


                            //Save last changes and commit
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
                 //Ejemplo de query que se quiere conseguir con linq
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
                                SupplierBomCount = g.Count(x => x.itemBom != null) 
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

        public string GetPackingListNumber(string idCustomer, DateTime date)
        {
            try
            {
                if (idCustomer == null)
                    throw new ArgumentNullException(nameof(idCustomer));

                using (var db = new HKSupplyContext())
                {

                    string strCont;
                    string pkNumber = string.Empty;

                    var pakingsDocs = db.DocsHead
                        .Where(a => a.IdSupplyDocType.Equals(Constants.SUPPLY_DOCTYPE_PL) && a.IdCustomer.Equals(idCustomer) &&
                        System.Data.Entity.SqlServer.SqlFunctions.DatePart("year", a.DocDate) == System.Data.Entity.SqlServer.SqlFunctions.DatePart("year", date) &&
                        System.Data.Entity.SqlServer.SqlFunctions.DatePart("month", a.DocDate) == System.Data.Entity.SqlServer.SqlFunctions.DatePart("month", date))
                        .ToList();

                    strCont = $"{(pakingsDocs.Count + 1).ToString().PadLeft(3, '0')}";

                    pkNumber = $"{Constants.SUPPLY_DOCTYPE_PL}{idCustomer}{DateTime.Now.Year.ToString()}{DateTime.Now.Month.ToString("d2")}{strCont}";

                    return pkNumber;
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

        private DocHead GetPurchaseOrderBcn2Hk(DocHead purchaseOrderHk2Factory)
        {
            try
            {
                //copiamos las líneas tal cual
                List<DocLine> linesPoBcnHk = new List<DocLine>();

                foreach (var line in purchaseOrderHk2Factory.Lines)
                {
                    DocLine lineSoBcnHk = line.DeepCopyByExpressionTree();
                    lineSoBcnHk.QuantityOriginal = lineSoBcnHk.Quantity;
                    lineSoBcnHk.IdDoc = $"{Constants.SUPPLY_DOCTYPE_PO}{purchaseOrderHk2Factory.IdDoc}";
                    lineSoBcnHk.IdDocRelated = purchaseOrderHk2Factory.IdDoc;
                    linesPoBcnHk.Add(lineSoBcnHk);
                }

                DocHead poBcnHk = new DocHead()
                {
                    IdDoc = $"{Constants.SUPPLY_DOCTYPE_PO}{purchaseOrderHk2Factory.IdDoc}", //ID? Cómo se linka por el la PO de fábrica
                    IdDocRelated = purchaseOrderHk2Factory.IdDoc,
                    IdSupplyDocType = Constants.SUPPLY_DOCTYPE_PO,
                    CreationDate = DateTime.Now,
                    DeliveryDate = purchaseOrderHk2Factory.DeliveryDate, //?'
                    DocDate = purchaseOrderHk2Factory.DocDate,
                    User = GlobalSetting.LoggedUser.UserLogin.ToUpper(),
                    //TODO: que datos ponemos ??
                    IdSupplyStatus = Constants.SUPPLY_STATUS_OPEN, //STATUS?
                    IdSupplier = Constants.ETNIA_HK_COMPANY_CODE, //Etnia HK??
                    IdCustomer = Constants.ETNIA_BCN_COMPANY_CODE, //Etnia BCN a piñon??
                    //DeliveryTerm = slueDeliveryTerms.EditValue as string,
                    //IdPaymentTerms = sluePaymentTerm.EditValue as string,
                    //IdCurrency = slueCurrency.EditValue as string,
                    Lines = linesPoBcnHk,
                };

                return poBcnHk;
            }
            catch
            {
                throw;
            }
        }

        private DocHead GetQuotationProposal(HKSupplyContext db, DocHead purchaseOrder)
        {
            try
            {
                SqlParameter param1 = new SqlParameter("@pIdDocPo", purchaseOrder.IdDoc);
                List<DocLine> lines = db.Database.SqlQuery<DocLine>("GET_QUOTATIO_PROPOSAL_BOM_EXPLOSION @pIdDocPo", param1).ToList();

                DocHead quotationProposal = new DocHead()
                {
                    IdDoc = $"{Constants.SUPPLY_DOCTYPE_QP}{purchaseOrder.IdDoc}",
                    IdDocRelated = purchaseOrder.IdDoc,
                    IdSupplyDocType = Constants.SUPPLY_DOCTYPE_QP,
                    CreationDate = DateTime.Now,
                    DeliveryDate = purchaseOrder.DeliveryDate,
                    DocDate = purchaseOrder.DocDate,
                    User = GlobalSetting.LoggedUser.UserLogin.ToUpper(),
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

        private DocHead GetSalesOrderHk2Factory(DocHead quotationProposal)
        {
            try
            {
                //copiamos las líneas tal cual
                List<DocLine> linesSoHkFactory = new List<DocLine>();

                foreach (var line in quotationProposal.Lines)
                {
                    DocLine lineSoHkFactory = line.DeepCopyByExpressionTree();
                    lineSoHkFactory.QuantityOriginal = lineSoHkFactory.Quantity;
                    lineSoHkFactory.IdDoc = $"{Constants.SUPPLY_DOCTYPE_SO}{line.IdDocRelated}";
                    lineSoHkFactory.IdDocRelated = quotationProposal.IdDoc;
                    linesSoHkFactory.Add(lineSoHkFactory);
                }

                DocHead soHkFactory = new DocHead()
                {
                    IdDoc = quotationProposal.IdDoc.Replace(Constants.SUPPLY_DOCTYPE_QP, Constants.SUPPLY_DOCTYPE_SO), //ID? Cómo se linka por el la PO de fábrica
                    IdDocRelated = quotationProposal.IdDoc,
                    IdSupplyDocType = Constants.SUPPLY_DOCTYPE_SO,
                    CreationDate = DateTime.Now,
                    User = GlobalSetting.LoggedUser.UserLogin.ToUpper(),
                    //    //TODO: que datos ponemos ??
                    DeliveryDate = quotationProposal.DeliveryDate,
                    DocDate = quotationProposal.DocDate,
                    IdSupplyStatus = Constants.SUPPLY_STATUS_OPEN, //STATUS?
                    IdSupplier = Constants.ETNIA_HK_COMPANY_CODE, //Etnia HK??
                    IdCustomer = quotationProposal.IdCustomer,
                    //DeliveryTerm = slueDeliveryTerms.EditValue as string,
                    //IdPaymentTerms = sluePaymentTerm.EditValue as string,
                    IdCurrency = quotationProposal.IdCurrency,
                    Lines = linesSoHkFactory,
                };

                return soHkFactory;
            }
            catch
            {
                throw;
            }
        }

        private DocHead GetDeliveryNoteHk2Factory(DocHead packingList)
        {
            try
            {
                //copiamos las líneas tal cual
                List<DocLine> linesDnHkFactory = new List<DocLine>();

                foreach (var line in packingList.Lines)
                {
                    DocLine lineDnHkFactory = line.DeepCopyByExpressionTree();
                    lineDnHkFactory.QuantityOriginal = lineDnHkFactory.Quantity;
                    lineDnHkFactory.IdDoc = line.IdDoc.Replace(Constants.SUPPLY_DOCTYPE_PL, Constants.SUPPLY_DOCTYPE_DN);
                    //lineDnHkFactory.IdDocRelated = newDoc.IdDoc; //Mantenemos el original referenciado a la SO
                    lineDnHkFactory.IdSupplyStatus = Constants.SUPPLY_STATUS_CLOSE;
                    linesDnHkFactory.Add(lineDnHkFactory);
                }

                DocHead dnHkFactory = new DocHead()
                {
                    IdDoc = packingList.IdDoc.Replace(Constants.SUPPLY_DOCTYPE_PL, Constants.SUPPLY_DOCTYPE_DN), //ID? Cómo se linka por el la PO de fábrica
                    IdDocRelated = packingList.IdDoc,
                    IdSupplyDocType = Constants.SUPPLY_DOCTYPE_DN,
                    CreationDate = DateTime.Now,
                    DeliveryDate = packingList.DeliveryDate,
                    DocDate = packingList.DocDate,
                    IdSupplyStatus = Constants.SUPPLY_STATUS_CLOSE,
                    IdSupplier = Constants.ETNIA_HK_COMPANY_CODE,
                    IdCustomer = packingList.IdCustomer,
                    DeliveryTerm = packingList.DeliveryTerm,
                    IdPaymentTerms = packingList.IdPaymentTerms,
                    IdCurrency = packingList.IdCurrency,
                    Lines = linesDnHkFactory,
                };

                return dnHkFactory;
            }
            catch
            {
                throw;
            }
        }

        private DocHead GetInvoiceHk2Factory(DocHead deliveryNote)
        {
            try
            {
                //copiamos las líneas tal cual
                List<DocLine> linesIvHkFactory = new List<DocLine>();

                foreach (var line in deliveryNote.Lines)
                {
                    DocLine lineIvHkFactory = line.DeepCopyByExpressionTree();
                    lineIvHkFactory.IdDoc = line.IdDoc.Replace(Constants.SUPPLY_DOCTYPE_DN, Constants.SUPPLY_DOCTYPE_IV);
                    //lineIvHkFactory.IdDocRelated = deliveryNote.IdDoc; //mantenemos la relación a la SO al igual que la DN
                    lineIvHkFactory.IdSupplyStatus = Constants.SUPPLY_STATUS_CLOSE;

                    linesIvHkFactory.Add(lineIvHkFactory);
                }

                DocHead ivHkFactory = new DocHead()
                {
                    IdDoc = deliveryNote.IdDoc.Replace(Constants.SUPPLY_DOCTYPE_DN, Constants.SUPPLY_DOCTYPE_IV), 
                    IdDocRelated = deliveryNote.IdDoc,
                    IdSupplyDocType = Constants.SUPPLY_DOCTYPE_IV,
                    CreationDate = DateTime.Now,
                    DeliveryDate = deliveryNote.DeliveryDate,
                    DocDate = deliveryNote.DocDate,
                    IdSupplyStatus = Constants.SUPPLY_STATUS_CLOSE,
                    IdSupplier = Constants.ETNIA_HK_COMPANY_CODE,
                    IdCustomer = deliveryNote.IdCustomer,
                    DeliveryTerm = deliveryNote.DeliveryTerm,
                    IdPaymentTerms = deliveryNote.IdPaymentTerms,
                    IdCurrency = deliveryNote.IdCurrency,
                    Lines = linesIvHkFactory,
                };

                return ivHkFactory;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Actualizar las Sales Orders asociadas a un packing list
        /// </summary>
        /// <returns></returns>
        private bool UpdateSoAssociatedToPk(HKSupplyContext db, DocHead packingList)
        {
            try
            {
                //******* Tenemos que actualizar los datos de la/s Sales Order asociadas a cada línea del packing *******//

                foreach (var line in packingList.Lines)
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
                List<string> salesOrderList = packingList.Lines.Select(a => a.IdDocRelated).Distinct().ToList();

                foreach (var salesOrder in salesOrderList)
                {
                    SqlParameter param1 = new SqlParameter("@pIdDoc", salesOrder);
                    bool checkClose = db.Database.SqlQuery<bool>("CHECK_CLOSE_DOC @pIdDoc", param1).FirstOrDefault();

                    if (checkClose == true)
                    {
                        //Cerramos la SO
                        var soDoc = db.DocsHead.Where(a => a.IdDoc.Equals(salesOrder)).FirstOrDefault();
                        if (soDoc != null)
                        {
                            soDoc.IdSupplyStatus = Constants.SUPPLY_STATUS_CLOSE;
                            db.SaveChanges();
                        }
                    }

                }

                return true;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Actualizar las cantidades de un documento, cuando se modifica uno y hay que modificar también los documentos relacionados que tiene
        /// </summary>
        /// <param name="db"></param>
        /// <param name="idDoc"></param>
        /// <param name="lines"></param>
        private void UpdateLineQty(HKSupplyContext db, string idDoc, List<DocLine> lines)
        {
            try
            {
                foreach (DocLine line in lines)
                {
                    DocLine lineToUpdate = db.DocsLines.Where(a => a.IdDoc.Equals(idDoc) && a.IdItemBcn.Equals(line.IdItemBcn)).FirstOrDefault();
                    if (lineToUpdate == null)
                        throw new Exception("Line not found");

                    if(lineToUpdate.Quantity != line.Quantity /*|| lineToUpdate.QuantityOriginal != line.QuantityOriginal*/)
                    {
                        lineToUpdate.Quantity = line.Quantity;
                        db.SaveChanges();
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        private void UpdateLineQtyOriginal(HKSupplyContext db, string idDoc, List<DocLine> lines)
        {
            try
            {
                foreach (DocLine line in lines)
                {
                    DocLine lineToUpdate = db.DocsLines.Where(a => a.IdDoc.Equals(idDoc) && a.IdItemBcn.Equals(line.IdItemBcn)).FirstOrDefault();
                    if (lineToUpdate == null)
                        throw new Exception("Line not found");

                    if(lineToUpdate.QuantityOriginal != line.QuantityOriginal)
                    {
                        lineToUpdate.QuantityOriginal = line.QuantityOriginal;
                        db.SaveChanges();
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
