
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
                        .Include(c => c.Customer)
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

        /// <summary>
        /// El packing es un poco especial  y puede cargar más cosas que un documento normal. 
        /// Lo separo para cuando interesa obtener el packing con todo lo asociado
        /// </summary>
        /// <param name="idDoc"></param>
        /// <returns></returns>
        public DocHead GetDocPackingList(string idDoc)
        {
            try
            {
                if (idDoc == null)
                    throw new ArgumentNullException(nameof(idDoc));

                using (var db = new HKSupplyContext())
                {
                    DocHead doc = db.DocsHead
                        .Where(a => a.IdDoc.Equals(idDoc))
                        .Include(l => l.Lines)
                        .Include(s => s.SupplyDocType)
                        .Include(c => c.Customer)
                        .Include(b => b.Boxes)
                        .Include(ib => ib.PackingListItemBatches)
                        .FirstOrDefault();

                    if (doc != null)
                    {
                        foreach (var line in doc.Lines)
                        {
                            line.LineState = DocLine.LineStates.Loaded;

                            object item = null;

                            switch (line.IdItemGroup)
                            {
                                case Constants.ITEM_GROUP_EY:
                                    item = GlobalSetting.ItemEyService.GetItem(line.IdItemBcn);
                                    line.Item = item;
                                    break;

                                case Constants.ITEM_GROUP_MT:
                                    item = GlobalSetting.ItemMtService.GetItem(line.IdItemBcn);
                                    line.Item = item;
                                    break;

                                case Constants.ITEM_GROUP_HW:
                                    item = GlobalSetting.ItemHwService.GetItem(line.IdItemBcn);
                                    line.Item = item;
                                    break;
                            }

                            //aprovechamos y cargamos el item de los batches
                            var itemsBatches = doc
                                .PackingListItemBatches
                                .Where(a => a.IdItemBcn.Equals(line.IdItemBcn) && a.IdItemGroup.Equals(line.IdItemGroup))
                                .ToList();
                            foreach (var itemBatch in itemsBatches)
                            {
                                itemBatch.Item = item;
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
                        .Include(c => c.Customer)
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
                        .Include(c => c.Customer)
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

        public List<DocHead> GetDocsByReference(string manualReference)
        {
            try
            {
                if (manualReference == null)
                    throw new ArgumentNullException(nameof(manualReference));

                using (var db = new HKSupplyContext())
                {
                    var docs = db.DocsHead
                        .Where(a => a.ManualReference.Equals(manualReference))
                        .Include(l => l.Lines)
                        .Include(s => s.SupplyDocType)
                        .Include(c => c.Customer)
                        .Include(b => b.Boxes)
                        .Include(ib => ib.PackingListItemBatches)
                        .ToList();

                    if (docs.Count > 0)
                    {
                        foreach (var doc in docs)
                        {
                            foreach (var line in doc.Lines)
                            {
                                line.LineState = DocLine.LineStates.Loaded;

                                object item = null;

                                switch (line.IdItemGroup)
                                {
                                    case Constants.ITEM_GROUP_EY:
                                        item = GlobalSetting.ItemEyService.GetItem(line.IdItemBcn);
                                        line.Item = item;
                                        break;

                                    case Constants.ITEM_GROUP_MT:
                                        item = GlobalSetting.ItemMtService.GetItem(line.IdItemBcn);
                                        line.Item = item;
                                        break;

                                    case Constants.ITEM_GROUP_HW:
                                        item = GlobalSetting.ItemHwService.GetItem(line.IdItemBcn);
                                        line.Item = item;
                                        break;
                                }

                                //aprovechamos y cargamos el item de los batches
                                var itemsBatches = doc
                                    .PackingListItemBatches
                                    .Where(a => a.IdItemBcn.Equals(line.IdItemBcn) && a.IdItemGroup.Equals(line.IdItemGroup))
                                    .ToList();
                                foreach (var itemBatch in itemsBatches)
                                {
                                    itemBatch.Item = item;
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
                        .Include(c => c.Customer)
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
                        .Include(c => c.Customer)
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

        public List<POSelection> GetPOSelection(string idDocPo, string idSupplyStatus, string idSupplier, DateTime PODateIni, DateTime PODateEnd, bool factory = true)
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
                query.Append($",@pFactory = {Convert.ToInt32(factory).ToString()}");

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
                        List<Models.SupplierPriceList> supplierPriceList = null;
                        if (newDoc.IdSupplyDocType == Constants.SUPPLY_DOCTYPE_PO)
                            supplierPriceList = GlobalSetting.SupplierPriceListService.GetSuppliersPriceList(idItemBcn: null, idSupplier: newDoc.IdSupplier);

                        foreach (var line in newDoc.Lines)
                        {
                            line.NumLin = numLin;
                            line.IdDoc = newDoc.IdDoc;
                            line.QuantityOriginal = line.Quantity;

                            if (newDoc.IdSupplyDocType == Constants.SUPPLY_DOCTYPE_PO)
                            {
                                var supplierPrice = supplierPriceList.Where(a => a.IdItemBcn.Equals(line.IdItemBcn)).FirstOrDefault();
                                line.UnitPrice = (supplierPrice == null ? 0 : (short)supplierPrice.Price);
                                line.UnitPriceBaseCurrency = (supplierPrice == null ? 0 : (short)supplierPrice.PriceBaseCurrency);
                            }

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
                            DocHead docToUpdate = null;
                            DocHead docDataBeforeUpdate = null;

                            if (doc.IdSupplyDocType == Constants.SUPPLY_DOCTYPE_PL)
                            {
                                docToUpdate = GetDocPackingList(doc.IdDoc);
                                docDataBeforeUpdate = GetDocPackingList(doc.IdDoc);
                            }
                            else
                            {
                                docToUpdate = GetDoc(doc.IdDoc);
                                docDataBeforeUpdate = GetDoc(doc.IdDoc);
                            }

                            if (docToUpdate == null)
                                throw new Exception("DOC error");


                            //número de línea
                            int numLin = 1;

                            bool existQP = true;
                            bool existSO = true;

                            if (doc.IdSupplyDocType == Constants.SUPPLY_DOCTYPE_PO)
                            {
                                existQP = (db.DocsHead.Where(a => a.IdDocRelated.Equals(doc.IdDoc) && a.IdSupplyDocType.Equals(Constants.SUPPLY_DOCTYPE_QP)).Count()) > 0;
                            }

                            if (doc.IdSupplyDocType == Constants.SUPPLY_DOCTYPE_QP)
                            {
                                existSO = (db.DocsHead.Where(a => a.IdDocRelated.Equals(doc.IdDoc) && a.IdSupplyDocType.Equals(Constants.SUPPLY_DOCTYPE_SO)).Count()) > 0;
                            }


                            //Actualizar el precio por si han modificado alguno mientras dejaban el formulario de PO abierto (siempre y cuando no exista ya la QP)
                            List<Models.SupplierPriceList> supplierPriceList = null;
                            List<Models.CustomerPriceList> customerPriceList = null;
                            
                            if (existQP == false)
                                supplierPriceList = GlobalSetting.SupplierPriceListService.GetSuppliersPriceList(idItemBcn: null, idSupplier: doc.IdSupplier);

                            if (existSO == false)
                                customerPriceList = GlobalSetting.CustomerPriceListService.GetCustomersPriceList(idItemBcn: null, idCustomer: doc.IdCustomer);


                            foreach (var line in doc.Lines)
                            {
                                line.NumLin = numLin;
                                line.IdDoc = doc.IdDoc;

                                if (line.LineState == DocLine.LineStates.New)
                                    line.QuantityOriginal = line.Quantity;

                                if (doc.IdSupplyDocType == Constants.SUPPLY_DOCTYPE_PO && existQP == false)
                                {
                                    var supplierPrice = supplierPriceList?.Where(a => a.IdItemBcn.Equals(line.IdItemBcn)).FirstOrDefault();

                                    line.UnitPrice = (supplierPrice == null ? 0 : supplierPrice.Price);
                                    line.UnitPriceBaseCurrency = (supplierPrice == null ? 0 : supplierPrice.PriceBaseCurrency);
                                }

                                if (doc.IdSupplyDocType == Constants.SUPPLY_DOCTYPE_QP && existSO == false)
                                {
                                    var customerPrice = customerPriceList?.Where(a => a.IdItemBcn.Equals(line.IdItemBcn)).FirstOrDefault();

                                    line.UnitPrice = (customerPrice == null ? 0 : customerPrice.Price);
                                    line.UnitPriceBaseCurrency = (customerPrice == null ? 0 : customerPrice.PriceBaseCurrency);
                                }

                                numLin++;
                            }

                            //Hay que agregarlo al contexto actual para que lo actualice
                            db.DocsHead.Attach(docToUpdate);

                            //actualizamos el estado, manual ref y remarks
                            docToUpdate.IdSupplyStatus = doc.IdSupplyStatus;
                            docToUpdate.ManualReference = doc.ManualReference;
                            docToUpdate.Remarks = doc.Remarks;

                            /********** DOC LINES **********/
                            //Borramos las líneas de la base de datos para insertarla de nuevo
                            var lines = db.DocsLines.Where(a => a.IdDoc.Equals(doc.IdDoc));

                            foreach (var line in lines)
                                db.DocsLines.Remove(line);

                            //y los insertamos de nuevo
                            foreach (var line in doc.Lines)
                                db.DocsLines.Add(line);

                            /********** BOXES (packing list) **********/
                            var boxes = db.DocBoxes.Where(a => a.IdDoc.Equals(doc.IdDoc));

                            foreach (var box in boxes.EmptyIfNull())
                                db.DocBoxes.Remove(box);

                            foreach (var box in doc.Boxes.EmptyIfNull())
                                db.DocBoxes.Add(box);

                            /********** MATERIAL BATCHES (packing list)  **********/
                            var packingBatches = db.PackingListItemsBatch.Where(a => a.IdDoc.Equals(doc.IdDoc));

                            foreach (var packingBatch in packingBatches.EmptyIfNull())
                                db.PackingListItemsBatch.Remove(packingBatch);

                            foreach (var packingBatch in doc.PackingListItemBatches.EmptyIfNull())
                                db.PackingListItemsBatch.Add(packingBatch);


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
                                var poBcnHk = GetPurchaseOrderBcn2Hk(db, docToUpdate);
                                db.DocsHead.Add(poBcnHk);
                                //QP
                                var quotationProposal =  GetQuotationProposal(db, doc);
                                db.DocsHead.Add(quotationProposal);

                            }

                            /************************************* QP *************************************/
                            //Si es una Quotation Proposal y se finaliza hay que generar la Sales Order de Etnia Hk a la fábrica
                            if (doc.IdSupplyDocType == Constants.SUPPLY_DOCTYPE_QP && finishDoc == true)
                            {
                                //Cerramos la QP
                                docToUpdate.IdSupplyStatus = Constants.SUPPLY_STATUS_CLOSE;


                                //Guardamos los datos porque son necesarios los actualizados para generar la Sales Order
                                db.SaveChanges();

                                DocHead soHkFactory = GetSalesOrderHk2Factory(docToUpdate);
                                db.DocsHead.Add(soHkFactory);

                                AssignStockForDoc(db, soHkFactory, null);


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

                                //Actualizamos las reservas de stock
                                AssignStockForDoc(db, docToUpdate, docDataBeforeUpdate);
                            }


                            /************************************* PK *************************************/
                            //Si es un Packing List y se finaliza hay que generar la Delivery Note, la Invoice
                            //y actualizar los datos en las Sales orders asociadas si es un packing de Etnia HK a las fábricas
                            //o actualizar los datos en la PO de Etnia Barcelona a Etnia HK en el caso de que sea un packing de Etnia HK a Etnia Barcelona

                            if (doc.IdSupplyDocType == Constants.SUPPLY_DOCTYPE_PL && finishDoc == true)
                            {
                                db.SaveChanges();

                                if (docToUpdate.IdCustomer != Constants.ETNIA_BCN_COMPANY_CODE)
                                {
                                    //actualizar los datos en las Sales orders asociadas
                                    UpdateSoAssociatedToPk(db, docToUpdate);
                                }
                                else
                                {
                                    //Actualizar los datos de las PO de Barcelona asociadas
                                    UpdatePoAssociatedToPkBcn(db, docToUpdate);
                                }
                                

                                //Generar la Delivery Note de HK a la fábrica
                                DocHead dnHkFactory = GetDeliveryNoteHk2Factory(docToUpdate);
                                db.DocsHead.Add(dnHkFactory);

                                //Generar la Invice de HK a la fábrica
                                DocHead ivHkFactory = GetInvoiceHk2Factory(dnHkFactory);
                                db.DocsHead.Add(ivHkFactory);

                                if (docToUpdate.IdCustomer != Constants.ETNIA_BCN_COMPANY_CODE)
                                { 
                                    //Hacer los movimientos de stock para el material vendido a las fábricas
                                    MoveStockForDoc(db, docToUpdate);
                                }
                                else
                                {
                                    //insertamos las gafas en el stock de on hand de Etnia Bcn
                                    DirectEntryToWareHouse(db, docToUpdate, Constants.ETNIA_BCN_COMPANY_CODE, PRJ_Stocks.Classes.Stocks.StockWareHousesType.OnHand);
                                }

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

        #region Get Generic Doc Number
        public string GetGenericDocHeadNumber(string idSupplyDocType, string idCustomer, string idSupplier, DateTime date)
        {
            try
            {
                using (var db = new HKSupplyContext())
                {

                    string pkNumber = GetGenericDocHeadNumber(db, idSupplyDocType, idCustomer, idSupplier, date);
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

        private string GetGenericDocHeadNumber(HKSupplyContext db, string idSupplyDocType, string idCustomer, string idSupplier, DateTime date)
        {
            try
            {
                    string strCont;
                    string docNumber = string.Empty;
                    string code = string.Empty;

                code = (string.IsNullOrEmpty(idCustomer) ? idSupplier : idCustomer);

                    var pakingsDocs = db.DocsHead
                           .Where(a => a.IdSupplyDocType.Equals(idSupplyDocType) &&
                           (string.IsNullOrEmpty(idSupplier) == true || a.IdSupplier.Equals(idSupplier)) &&
                           (string.IsNullOrEmpty(idCustomer) == true || a.IdCustomer.Equals(idCustomer)) &&
                           System.Data.Entity.SqlServer.SqlFunctions.DatePart("year", a.DocDate) == System.Data.Entity.SqlServer.SqlFunctions.DatePart("year", date) &&
                           System.Data.Entity.SqlServer.SqlFunctions.DatePart("month", a.DocDate) == System.Data.Entity.SqlServer.SqlFunctions.DatePart("month", date))
                           .ToList();

                    strCont = $"{(pakingsDocs.Count + 1).ToString().PadLeft(3, '0')}";

                    docNumber = $"{idSupplyDocType}{code}{DateTime.Now.Year.ToString()}{DateTime.Now.Month.ToString("d2")}{strCont}";

                    return docNumber;
                
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Supply Materials

        /*****************************************************************************************************************************************  
         *      Por facilidad y para que se va más claro separo los documentos para la gestión del pedido de frames y venta de manterial a fábricas   *
         *      y la compra (aprovisionamiento) de materiales
         *****************************************************************************************************************************************/
        public DocHead UpdateDocSupplyMaterials(DocHead doc, bool finishDoc = false)
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

                            DocHead docToUpdate = null;
                            DocHead docDataBeforeUpdate = null;

                            if (doc.IdSupplyDocType == Constants.SUPPLY_DOCTYPE_PL || doc.IdSupplyDocType == Constants.SUPPLY_DOCTYPE_QCP)
                            {
                                docToUpdate = GetDocPackingList(doc.IdDoc);
                                docDataBeforeUpdate = GetDocPackingList(doc.IdDoc);
                            }
                            else
                            {
                                docToUpdate = GetDoc(doc.IdDoc);
                                docDataBeforeUpdate = GetDoc(doc.IdDoc);
                            }

                            if (docToUpdate == null)
                                throw new Exception("DOC error");

                            //número de línea
                            int numLin = 1;

                            //TODO: Actualizar precios, en qué momento y bajo qué condiciones??

                            foreach (var line in doc.Lines)
                            {
                                line.NumLin = numLin;
                                line.IdDoc = doc.IdDoc;

                                if (line.LineState == DocLine.LineStates.New)
                                    line.QuantityOriginal = line.Quantity;

                                //if (doc.IdSupplyDocType == Constants.SUPPLY_DOCTYPE_PO && existQP == false)
                                //{
                                //    var supplierPrice = supplierPriceList?.Where(a => a.IdItemBcn.Equals(line.IdItemBcn)).FirstOrDefault();

                                //    line.UnitPrice = (supplierPrice == null ? 0 : supplierPrice.Price);
                                //    line.UnitPriceBaseCurrency = (supplierPrice == null ? 0 : supplierPrice.PriceBaseCurrency);
                                //}

                                //if (doc.IdSupplyDocType == Constants.SUPPLY_DOCTYPE_QP && existSO == false)
                                //{
                                //    var customerPrice = customerPriceList?.Where(a => a.IdItemBcn.Equals(line.IdItemBcn)).FirstOrDefault();

                                //    line.UnitPrice = (customerPrice == null ? 0 : customerPrice.Price);
                                //    line.UnitPriceBaseCurrency = (customerPrice == null ? 0 : customerPrice.PriceBaseCurrency);
                                //}

                                numLin++;
                            }

                            //Hay que agregarlo al contexto actual para que lo actualice
                            db.DocsHead.Attach(docToUpdate);

                            //actualizamos el estado, manual ref y remarks
                            docToUpdate.IdSupplyStatus = doc.IdSupplyStatus;
                            docToUpdate.ManualReference = doc.ManualReference;
                            docToUpdate.DeliveryDate = doc.DeliveryDate;
                            docToUpdate.Remarks = doc.Remarks;


                            //Borramos las líneas de la base de datos para insertarla de nuevo
                            var lines = db.DocsLines.Where(a => a.IdDoc.Equals(doc.IdDoc));

                            foreach (var line in lines)
                                db.DocsLines.Remove(line);

                            //y los insertamos de nuevo
                            foreach (var line in doc.Lines)
                                db.DocsLines.Add(line);

                            /********** MATERIAL BATCHES (packing list)  **********/
                            var packingBatches = db.PackingListItemsBatch.Where(a => a.IdDoc.Equals(doc.IdDoc));

                            foreach (var packingBatch in packingBatches.EmptyIfNull())
                                db.PackingListItemsBatch.Remove(packingBatch);

                            foreach (var packingBatch in doc.PackingListItemBatches.EmptyIfNull())
                                db.PackingListItemsBatch.Add(packingBatch);

                            //user
                            docToUpdate.User = GlobalSetting.LoggedUser.UserLogin.ToUpper();

                            db.SaveChanges();

                            //************************************* PK *************************************//
                            //Si es un Packing List, se ha pasado al estado de "TRN" (tránsito) y se finaliza hay que insertar en el stock de tránsito de Etnia HK
                            if (doc.IdSupplyDocType == Constants.SUPPLY_DOCTYPE_PL && 
                                doc.IdSupplyStatus == Constants.SUPPLY_STATUS_TRANSIT &&
                                docDataBeforeUpdate.IdSupplyStatus == Constants.SUPPLY_STATUS_OPEN &&
                                finishDoc == true)
                            {

                                //Hacer los movimientos de stock hacía el almacén de tránsito
                                AddStockToTransit(db, docToUpdate);

                            }

                            //Si es un packing y se cierra:
                            //  - Las items recibidos y aceptados se pasan al almacén de OnHand, sin propietario y con el lote correspondiente
                            //  - Los rechazados se pasan al almacén de QC Pending y se genera un documento de QCPendieng para después gestionar los rechazos
                            //  - Se actualizan los datos en la PO asociada
                            if (doc.IdSupplyDocType == Constants.SUPPLY_DOCTYPE_PL &&
                                doc.IdSupplyStatus == Constants.SUPPLY_STATUS_CLOSE &&
                                docDataBeforeUpdate.IdSupplyStatus == Constants.SUPPLY_STATUS_TRANSIT &&
                                finishDoc == true)
                            {

                                if ((docToUpdate.Lines.Where(a => a.RejectedQuantity > 0).Count()) > 0)
                                {
                                    var docQualityControlPending = GetQualityControlPending(db, docToUpdate);
                                    db.DocsHead.Add(docQualityControlPending);

                                    //Agregamos el material rechazado al almacén de "rejected";
                                    AddRejectedStock(db, docQualityControlPending);
                                }
                                
                                //agregamos las cantidades aceptadas al stock on hand con sus correspondientes lotes
                                AcceptGoodsMaterials(db, docToUpdate);

                                //Ajustamos el tránsito
                                AjustTransitGoodMaterials(db, docToUpdate);

                                //actualizar los datos en las purchase orders asociadas. Aunque en el método ponga SO, 
                                //internamente funcionará igual para las PO asociadas a un packing de compra de materiales
                                UpdatePoAssociatedToPkSupplyMaterial(db, docToUpdate);

                            }

                            //************************************* QCP *************************************//

                            //Si es un Quality Control Pending y se cierra:
                            //  - las cantidades aceptadas pasan al stock on-hand con su correspondiente lote
                            //  - las cantidades rechazadas se genera un documento de devolución
                            if (doc.IdSupplyDocType == Constants.SUPPLY_DOCTYPE_QCP && finishDoc == true)
                            {
                                //Aceptado a on hand y rechazado lo eliminamos
                                AcceptGoodsMaterials(db, docToUpdate);
                                DelRejectedStock(db, docToUpdate);

                                //actualizar los datos en las purchase orders asociadas. Aunque en el método ponga SO, 
                                //internamente funcionará igual para las PO asociadas a un packing de compra de materiales
                                UpdatePoAssociatedToPkSupplyMaterial(db, docToUpdate);

                                //generamos el documento de devolución
                                if ((docToUpdate.Lines.Where(a => a.RejectedQuantity > 0).Count()) > 0)
                                {
                                    var docReturnGoods = GetReturnGoodsDoc(db, docToUpdate);
                                    db.DocsHead.Add(docReturnGoods);
                                }

                            }

                            //********** Save last changes and commit **********//
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

        public List<DocHead> GetAssociatedPoPacking(string idDoc, List<string> idDocRelated, string idSupplyDocType)
        {
            try
            {
                List<DocHead> associatedPoPacking = new List<DocHead>();

                using (var db = new HKSupplyContext())
                {
                    associatedPoPacking = db.DocsHead
                        //.Include(l => l.Lines)
                        .Join(db.DocsLines,
                        head => head.IdDoc,
                        lines => lines.IdDoc,
                        (head, lines) => new { DocHead = head, DocLine = lines })
                        .Where(headAndLines => headAndLines.DocHead.IdSupplyDocType.Equals(Constants.SUPPLY_DOCTYPE_PL) &&
                        headAndLines.DocHead.IdDoc != idDoc &&
                        idDocRelated.Contains(headAndLines.DocLine.IdDocRelated))
                        //.Select(a => a.DocHead.IdDoc)
                        .Select(a => a.DocHead)
                        .Include(x => x.Lines)
                        .Distinct()
                        .ToList();

                }

                return associatedPoPacking;
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

        #endregion


        #region Private Methods

        private DocHead GetPurchaseOrderBcn2Hk(HKSupplyContext db, DocHead purchaseOrderHk2Factory)
        {
            try
            {
                //Nota: finalmente el precio de "venta" a Etnia Barcelona es el mismo que el de fábrica, así que se queda directamente el precio que había en la PO de HK -> factory
                //obtenemos la lista de precios de venta a Etnia Barcelona
                //var etniaBcnPriceList = db.CustomersPriceList
                //    .Where(a => a.IdCustomer.Equals(Constants.ETNIA_BCN_COMPANY_CODE))
                //    .ToList();

                //copiamos las líneas tal cual y actualizamos los importes
                List<DocLine> linesPoBcnHk = new List<DocLine>();

                foreach (var line in purchaseOrderHk2Factory.Lines)
                {
                    DocLine lineSoBcnHk = line.DeepCopyByExpressionTree();
                    lineSoBcnHk.QuantityOriginal = lineSoBcnHk.Quantity;
                    lineSoBcnHk.IdDoc = $"{Constants.SUPPLY_DOCTYPE_PO}{purchaseOrderHk2Factory.IdDoc}";
                    lineSoBcnHk.IdDocRelated = purchaseOrderHk2Factory.IdDoc;
                    //Calculamos el precio
                    //var price = etniaBcnPriceList?.Where(a => a.IdItemBcn.Equals(line.IdItemBcn)).FirstOrDefault();
                    //lineSoBcnHk.UnitPrice = (price != null ? price.Price : 0);
                    //lineSoBcnHk.UnitPriceBaseCurrency = (price != null ? price.PriceBaseCurrency : 0);

                    //agregamos la línea a la lista de lineas de la PO
                    linesPoBcnHk.Add(lineSoBcnHk);
                }

                //Nota: envía la fábrica directamente a BCN, la devlery date es la misma que en la PO de HK -> Factory
                //Calculamos la delivery date para Etnia Barcelona
                //var itemsList = linesPoBcnHk.Select(a => a.IdItemBcn).ToList();
                //float maxLeadTime = etniaBcnPriceList
                //    .Where(a => a.IdCustomer.Equals(Constants.ETNIA_BCN_COMPANY_CODE) && itemsList.Contains(a.IdItemBcn))
                //    .Select(b => b.LeadTime)
                //    .DefaultIfEmpty(0).Max();
                //DateTime deliveryDate = purchaseOrderHk2Factory.DocDate.AddDays(maxLeadTime);

                DocHead poBcnHk = new DocHead()
                {
                    IdDoc = $"{Constants.SUPPLY_DOCTYPE_PO}{purchaseOrderHk2Factory.IdDoc}", 
                    IdDocRelated = purchaseOrderHk2Factory.IdDoc,
                    IdSupplyDocType = Constants.SUPPLY_DOCTYPE_PO,
                    CreationDate = DateTime.Now,
                    DeliveryDate = purchaseOrderHk2Factory.DeliveryDate, //deliveryDate, 
                    DocDate = purchaseOrderHk2Factory.DocDate,
                    User = GlobalSetting.LoggedUser.UserLogin.ToUpper(),
                    //TODO: que datos ponemos ??
                    IdSupplyStatus = Constants.SUPPLY_STATUS_OPEN, 
                    IdSupplier = Constants.ETNIA_HK_COMPANY_CODE, 
                    IdCustomer = Constants.ETNIA_BCN_COMPANY_CODE, 
                    //DeliveryTerm = slueDeliveryTerms.EditValue as string,
                    //IdPaymentTerms = sluePaymentTerm.EditValue as string,
                    IdCurrency = purchaseOrderHk2Factory.IdCurrency,
                    Remarks = purchaseOrderHk2Factory.Remarks,
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
                SqlParameter param2 = new SqlParameter("@pIdCustomer", purchaseOrder.IdSupplier); //El supplier de la PO es un customer en la QP
                List<DocLine> lines = db.Database.SqlQuery<DocLine>("GET_QUOTATIO_PROPOSAL_BOM_EXPLOSION @pIdDocPo, @pIdCustomer", param1, param2).ToList();

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
                    IdCurrency = purchaseOrder.IdCurrency,
                    Remarks = purchaseOrder.Remarks,
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
                    Remarks = quotationProposal.Remarks,
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
                    ManualReference = packingList.ManualReference,
                    Remarks = packingList.Remarks,
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
                    ManualReference = deliveryNote.ManualReference,
                    Remarks = deliveryNote.Remarks,
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

        private bool UpdatePoAssociatedToPkBcn(HKSupplyContext db, DocHead packingList)
        {
            try
            {
                //******* Tenemos que actualizar los datos de la/s Purchase orders de Barcelona asociadas a cada línea del packing *******//
                foreach (var line in packingList.Lines)
                {
                    var doclinePo = db.DocsLines.Where(a => a.IdDoc.Equals(line.IdDocRelated) && a.IdItemBcn.Equals(line.IdItemBcn)).FirstOrDefault();
                    if (doclinePo != null)
                    {
                        doclinePo.DeliveredQuantity += line.Quantity;

                        if (doclinePo.DeliveredQuantity >= doclinePo.Quantity)
                            doclinePo.IdSupplyStatus = Constants.SUPPLY_STATUS_CLOSE;

                        db.SaveChanges();
                    }
                }

                //******* Hay que comprobar si se ha entregado todo para cerrar la/s purchase order/s asociadas al packing *******//
                // Obtenemos el listado de Sales Order
                List<string> purchaseOrderList = packingList.Lines.Select(a => a.IdDocRelated).Distinct().ToList();

                foreach (var purchaseOrder in purchaseOrderList)
                {
                    SqlParameter param1 = new SqlParameter("@pIdDoc", purchaseOrder);
                    bool checkClose = db.Database.SqlQuery<bool>("CHECK_CLOSE_DOC @pIdDoc", param1).FirstOrDefault();

                    if (checkClose == true)
                    {
                        //Cerramos la SO
                        var poDoc = db.DocsHead.Where(a => a.IdDoc.Equals(purchaseOrder)).FirstOrDefault();
                        if (poDoc != null)
                        {
                            poDoc.IdSupplyStatus = Constants.SUPPLY_STATUS_CLOSE;
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

        #region Supply Materials

        //private string GetQualityControlPendingNumber(HKSupplyContext db, string idSupplier, DateTime date)
        //{
        //    try
        //    {
        //        if (idSupplier == null)
        //            throw new ArgumentNullException(nameof(idSupplier));

        //        string strCont;
        //        string pkNumber = string.Empty;

        //        var docs = db.DocsHead
        //            .Where(a => a.IdSupplyDocType.Equals(Constants.SUPPLY_DOCTYPE_QCP) && a.IdSupplier.Equals(idSupplier) &&
        //            System.Data.Entity.SqlServer.SqlFunctions.DatePart("year", a.DocDate) == System.Data.Entity.SqlServer.SqlFunctions.DatePart("year", date) &&
        //            System.Data.Entity.SqlServer.SqlFunctions.DatePart("month", a.DocDate) == System.Data.Entity.SqlServer.SqlFunctions.DatePart("month", date))
        //            .ToList();

        //        strCont = $"{(docs.Count + 1).ToString().PadLeft(3, '0')}";

        //        pkNumber = $"{Constants.SUPPLY_DOCTYPE_QCP}{idSupplier}{DateTime.Now.Year.ToString()}{DateTime.Now.Month.ToString("d2")}{strCont}";

        //        return pkNumber;
                
        //    }
        //    catch (SqlException sqlex)
        //    {
        //        for (int i = 0; i < sqlex.Errors.Count; i++)
        //        {
        //            _log.Error("Index #" + i + "\n" +
        //                "Message: " + sqlex.Errors[i].Message + "\n" +
        //                "Error Number: " + sqlex.Errors[i].Number + "\n" +
        //                "LineNumber: " + sqlex.Errors[i].LineNumber + "\n" +
        //                "Source: " + sqlex.Errors[i].Source + "\n" +
        //                "Procedure: " + sqlex.Errors[i].Procedure + "\n");

        //            switch (sqlex.Errors[i].Number)
        //            {
        //                case -1: //connection broken
        //                case -2: //timeout
        //                    throw new DBServerConnectionException(GlobalSetting.ResManager.GetString("DBServerConnectionError"));
        //            }
        //        }
        //        throw sqlex;
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Error(ex.Message, ex);
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// generar el documento con los items rechazados de un packing de materiales
        /// </summary>
        /// <param name="packingList"></param>
        /// <returns></returns>
        private DocHead GetQualityControlPending(HKSupplyContext db, DocHead packingList)
        {
            try
            {
                List<DocLine> linesQualityControlPending = new List<DocLine>();
                var rejectedLines = packingList.Lines.Where(a => a.RejectedQuantity > 0).ToList();
                //var idDocQcp = GetQualityControlPendingNumber(db, packingList.IdSupplier, DateTime.Now);
                var idDocQcp = GetGenericDocHeadNumber(
                    db: db, 
                    idSupplyDocType: Constants.SUPPLY_DOCTYPE_QCP, 
                    idSupplier: packingList.IdSupplier, 
                    idCustomer: string.Empty, 
                    date: DateTime.Now);

                int lineNum = 1;

                foreach(var line in rejectedLines)
                {
                    DocLine docLine = new DocLine()
                    {
                        IdDoc = idDocQcp,
                        NumLin = lineNum,
                        IdItemBcn = line.IdItemBcn,
                        IdItemGroup = line.IdItemGroup,
                        IdSupplyStatus = Constants.SUPPLY_STATUS_OPEN,
                        Batch = null,
                        Quantity = line.RejectedQuantity,
                        QuantityOriginal = line.RejectedQuantity,
                        DeliveredQuantity = 0,
                        RequestedQuantity = 0,
                        RejectedQuantity = 0,
                        Remarks = line.Remarks, 
                        UnitPrice = line.UnitPrice,
                        UnitPriceBaseCurrency = line.UnitPriceBaseCurrency,
                        IdDocRelated = line.IdDocRelated,
                        BoxNumber = null
                    };

                    linesQualityControlPending.Add(docLine);

                    lineNum++;
                }

                DocHead docQualityControlPending = new DocHead()
                {
                    IdDoc = idDocQcp,
                    IdDocRelated = packingList.IdDoc,
                    IdSupplyDocType = Constants.SUPPLY_DOCTYPE_QCP,
                    CreationDate = DateTime.Now,
                    DeliveryDate = packingList.DeliveryDate,
                    DocDate = DateTime.Now,
                    IdSupplyStatus = Constants.SUPPLY_STATUS_OPEN,
                    IdSupplier = packingList.IdSupplier,
                    IdCustomer = packingList.IdCustomer,
                    DeliveryTerm = null,
                    IdPaymentTerms = null,
                    IdCurrency = null,
                    ManualReference = packingList.ManualReference,
                    Remarks = packingList.Remarks,
                    Lines = linesQualityControlPending,
                    User = GlobalSetting.LoggedUser.UserLogin.ToUpper()
                };

                return docQualityControlPending;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Actualizar los datos en la PO asociada a un packing de proveedor
        /// </summary>
        /// <param name="db"></param>
        /// <param name="packingList"></param>
        /// <returns></returns>
        private bool UpdatePoAssociatedToPkSupplyMaterial(HKSupplyContext db, DocHead packingList)
        {
            try
            {
                // ******* Tenemos que actualizar los datos de la/s Purchase Order asociadas a cada línea del packing ******* //
                foreach (var line in packingList.Lines)
                {
                    var doclinePo = db.DocsLines
                        .Join(db.DocsHead, 
                        lines => lines.IdDoc, 
                        head => head.IdDoc, 
                        (lines, head) => new { DocLine = lines, DocHead = head})
                        .Where(headAndLines => headAndLines.DocHead.IdSupplyDocType.Equals(Constants.SUPPLY_DOCTYPE_PO) &&
                        headAndLines.DocLine.IdDoc.Equals(line.IdDocRelated) &&
                        headAndLines.DocLine.IdItemBcn.Equals(line.IdItemBcn))
                        .Select(s => s.DocLine)
                        .FirstOrDefault();

                    if (doclinePo != null)
                    {
                        doclinePo.DeliveredQuantity += line.DeliveredQuantity;

                        if (doclinePo.DeliveredQuantity >= doclinePo.Quantity)
                            doclinePo.IdSupplyStatus = Constants.SUPPLY_STATUS_CLOSE;

                        db.SaveChanges();
                    }
                }

                // ******* Hay que comprobar si se ha entregado todo para cerrar la/s purchase order asociadas al packing ******* //
                // Obtenemos el listado de Purchase Order
                List<string> purchaseOrdersList = packingList.Lines.Select(a => a.IdDocRelated).Distinct().ToList();

                foreach (var purchaseOrder in purchaseOrdersList)
                {
                    SqlParameter param1 = new SqlParameter("@pIdDoc", purchaseOrder);
                    bool checkClose = db.Database.SqlQuery<bool>("CHECK_CLOSE_DOC @pIdDoc", param1).FirstOrDefault();

                    if (checkClose == true)
                    {
                        //Cerramos la PO
                        var soDoc = db.DocsHead.Where(a => a.IdDoc.Equals(purchaseOrder)).FirstOrDefault();
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
        /// Generar el documento de devolución de material que no pasa el control de calidad
        /// </summary>
        /// <param name="db"></param>
        /// <param name="qualityControlPending"></param>
        /// <returns></returns>
        private DocHead GetReturnGoodsDoc(HKSupplyContext db, DocHead qualityControlPending)
        {
            try
            {
                List<DocLine> linesReturnGoods = new List<DocLine>();
                var rejectedLines = qualityControlPending.Lines.Where(a => a.RejectedQuantity > 0).ToList();

                var idDocQcp = GetGenericDocHeadNumber(
                    db: db,
                    idSupplyDocType: Constants.SUPPLY_DOCTYPE_RT,
                    idSupplier: qualityControlPending.IdSupplier,
                    idCustomer: string.Empty,
                    date: DateTime.Now);

                int lineNum = 1;

                foreach (var line in rejectedLines)
                {
                    DocLine docLine = new DocLine()
                    {
                        IdDoc = idDocQcp,
                        NumLin = lineNum,
                        IdItemBcn = line.IdItemBcn,
                        IdItemGroup = line.IdItemGroup,
                        IdSupplyStatus = Constants.SUPPLY_STATUS_OPEN,
                        Batch = null,
                        Quantity = line.RejectedQuantity,
                        QuantityOriginal = line.RejectedQuantity,
                        DeliveredQuantity = 0,
                        RequestedQuantity = 0,
                        RejectedQuantity = 0,
                        Remarks = line.Remarks,
                        UnitPrice = line.UnitPrice,
                        UnitPriceBaseCurrency = line.UnitPriceBaseCurrency,
                        IdDocRelated = line.IdDocRelated,
                        BoxNumber = null
                    };

                    linesReturnGoods.Add(docLine);

                    lineNum++;
                }

                DocHead docReturnGoods = new DocHead()
                {
                    IdDoc = idDocQcp,
                    IdDocRelated = qualityControlPending.IdDoc,
                    IdSupplyDocType = Constants.SUPPLY_DOCTYPE_RT,
                    CreationDate = DateTime.Now,
                    DeliveryDate = qualityControlPending.DeliveryDate,
                    DocDate = DateTime.Now,
                    IdSupplyStatus = Constants.SUPPLY_STATUS_OPEN,
                    IdSupplier = qualityControlPending.IdSupplier,
                    IdCustomer = qualityControlPending.IdCustomer,
                    DeliveryTerm = null,
                    IdPaymentTerms = null,
                    IdCurrency = null,
                    ManualReference = qualityControlPending.ManualReference,
                    Remarks = qualityControlPending.Remarks,
                    Lines = linesReturnGoods,
                    User = GlobalSetting.LoggedUser.UserLogin.ToUpper()
                };

                return docReturnGoods;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Traspasar del almacén de tránsito al on hand el material de un packing aceptado
        /// </summary>
        /// <param name="db"></param>
        /// <param name="docHead"></param>
        /// <remarks>
        ///     Si se ha aceptado material extra la diferencia se pasa directamente al almacén de on hand
        ///     para no mover de tránsito stock de otro packing o que salte el error de que no hay stock suficiente
        /// </remarks>
        private void AcceptGoodsMaterials(HKSupplyContext db, DocHead docHead)
        {
            try
            {
                var BDSTK = new PRJ_Stocks.DB.BD_Stocks();
                PRJ_Stocks.Classes.Stocks STKAct = BDSTK.GetCurrentStock(db);

                //*************************************************************** V2 INI ***************************************************************//
                //Debido a la que una PO puede ser entregada en diferentes packing, con cantidades dispares y es el usuario el que decide si se acepta o no,
                //Finalmente lo aceptado se hace una entrada directa a On han (no un traspaso de tránsito), después ya eliminaremos de tránsito si se puede

                var whDestinationEtniaHkOnHand = STKAct.GetWareHouse(
                    Constants.ETNIA_HK_COMPANY_CODE,
                    PRJ_Stocks.Classes.Stocks.StockWareHousesType.OnHand); //destino on hand de Etnia HK

                List<string> docs = new List<string>();
                docs.Add(docHead.IdDoc);

                foreach (var line in docHead.Lines.Where(a => a.DeliveredQuantity > 0))
                {
                    STKAct.AddSockItem(
                        MoveType: PRJ_Stocks.Classes.Stocks.StockMovementsType.Entry,
                        ware: whDestinationEtniaHkOnHand,
                        Qtt: line.DeliveredQuantity, 
                        idItem: line.IdItemBcn,
                        idLot: string.Empty,
                        idOwner: string.Empty,
                        Remarks: string.Empty,
                        LstidDoc: docs,
                        IdUser: GlobalSetting.LoggedUser.UserLogin.ToUpper());

                    //buscamos los lotes de cada línea para asignarle el lote
                    var batches = docHead.PackingListItemBatches.Where(a => a.IdItemBcn.Equals(line.IdItemBcn) && a.IdDocRelated.Equals(line.IdDocRelated)).ToList();
                    foreach (var batch in batches)
                    {
                        STKAct.AsgnLotItem(WareORIG: whDestinationEtniaHkOnHand,
                            idItem: batch.IdItemBcn,
                            Qtt: batch.Quantity,
                            idLot: batch.Batch,
                            remarks: line.Remarks,
                            LstidDoc: docs,
                            IdUser: GlobalSetting.LoggedUser.UserLogin.ToUpper());
                    }
                }

                //*************************************************************** V2 FIN ***************************************************************//

                //*************************************************************** V1 INI ***************************************************************//
                //var whEtniaHkTransit = STKAct.GetWareHouse(
                //    Constants.ETNIA_HK_COMPANY_CODE,
                //    PRJ_Stocks.Classes.Stocks.StockWareHousesType.Transit); //origen Tránsito de Etnia HK

                //var whDestinationEtniaHkOnHand = STKAct.GetWareHouse(
                //    Constants.ETNIA_HK_COMPANY_CODE,
                //    PRJ_Stocks.Classes.Stocks.StockWareHousesType.OnHand); //destino on hand de Etnia HK

                //List<string> docs = new List<string>();
                //docs.Add(docHead.IdDoc);

                //foreach (var line in docHead.Lines.Where(a => a.Quantity > 0))
                //{
                //    //tenemos que comprobar si indican más cantidad de la del packing, hay que liberar como tope la cantidad que hemos pasado a tránsito (al generar la PO) 
                //    //para no liberar un posible stock de otra PO. si hay de más el excedente se pasa directamente a On Hand
                //    decimal qty = 0;
                //    decimal extra = 0;
                //    if (line.DeliveredQuantity > line.Quantity)
                //    {
                //        qty = line.Quantity;
                //        extra = line.DeliveredQuantity - line.Quantity;
                //    }
                //    else
                //    {
                //        qty = line.DeliveredQuantity;
                //        extra = 0;

                //    }

                //    //liberamos el stock que tiene owner el proveedor
                //    STKAct.FreeSockItem(WareORIG: whEtniaHkTransit,
                //        idItem: line.IdItemBcn,
                //        idOwner: docHead.IdSupplier,
                //        LstidDoc: docs,
                //        IdUser: GlobalSetting.LoggedUser.UserLogin.ToUpper(),
                //        Qtt: qty);

                //    //Una vez liberado traspasamos la cantidad de tránsito a on hand (sin lote)
                //    STKAct.MoveSockItem(
                //            MoveType: PRJ_Stocks.Classes.Stocks.StockMovementsType.Movement,
                //            WareORIG: whEtniaHkTransit,
                //            WareDEST: whDestinationEtniaHkOnHand,
                //            Qtt: qty,
                //            idItem: line.IdItemBcn,
                //            idOwner: string.Empty,
                //            idlot: string.Empty,
                //            remarks: line.Remarks,
                //            LstidDoc: docs,
                //            IdUser: GlobalSetting.LoggedUser.UserLogin.ToUpper());

                //    //si tenemos un extra lo pasamos directamente a on hand
                //    if (extra > 0)
                //    {
                //        STKAct.AddSockItem(MoveType: PRJ_Stocks.Classes.Stocks.StockMovementsType.Entry,
                //                                ware: whDestinationEtniaHkOnHand,
                //                                Qtt: extra, idItem: line.IdItemBcn,
                //                                idLot: string.Empty,
                //                                idOwner: string.Empty,
                //                                Remarks: "Extra qty accepted",
                //                                LstidDoc: docs,
                //                                IdUser: GlobalSetting.LoggedUser.UserLogin.ToUpper());
                //    }

                //    //buscamos los lotes de cada línea para asignarle el lote
                //    var batches = docHead.PackingListItemBatches.Where(a => a.IdItemBcn.Equals(line.IdItemBcn) && a.IdDocRelated.Equals(line.IdDocRelated)).ToList();
                //    foreach (var batch in batches)
                //    {
                //        STKAct.AsgnLotItem(WareORIG: whDestinationEtniaHkOnHand,
                //            idItem: batch.IdItemBcn,
                //            Qtt: batch.Quantity,
                //            idLot: batch.Batch,
                //            remarks: line.Remarks,
                //            LstidDoc: docs,
                //            IdUser: GlobalSetting.LoggedUser.UserLogin.ToUpper());
                //    }
                //}

                //*************************************************************** V1 FIN ***************************************************************//

                BDSTK.SaveCurrentStockMovs(db, STKAct);

            }
            catch
            {
                throw;
            }
            
        }

        /// <summary>
        /// Agregar material rechazado al almacén de "rejected"
        /// </summary>
        /// <param name="db"></param>
        /// <param name="docHead"></param>
        private void AddRejectedStock(HKSupplyContext db, DocHead docHead)
        {
            try
            {
                var BDSTK = new PRJ_Stocks.DB.BD_Stocks();
                PRJ_Stocks.Classes.Stocks STKAct = BDSTK.GetCurrentStock(db);

                var whEtniaHkRejected = STKAct.GetWareHouse(
                    Constants.ETNIA_HK_COMPANY_CODE,
                    PRJ_Stocks.Classes.Stocks.StockWareHousesType.Rejected);

                List<string> docs = new List<string>();
                docs.Add(docHead.IdDoc);

                foreach (var line in docHead.Lines)
                {
                    STKAct.AddSockItem(
                        MoveType: PRJ_Stocks.Classes.Stocks.StockMovementsType.Entry,
                        ware: whEtniaHkRejected,
                        Qtt: line.Quantity,
                        idItem: line.IdItemBcn,
                        idLot: string.Empty,
                        idOwner: docHead.IdSupplier,
                        Remarks: line.Remarks,
                        LstidDoc: docs,
                        IdUser: GlobalSetting.LoggedUser.UserLogin.ToUpper());
                }

                BDSTK.SaveCurrentStockMovs(db, STKAct);

            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Ajustar el tránsito de una recepción de material.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="docHead"></param>
        /// <remarks>
        ///     Del material recibido (aceptado y rechazado) se mria si hay en tránsito
        ///     para cada item/supplier y si hay suficiente se quita del stock.
        ///     O bien se quita la cantidad que haya disponible, ya que las cantidades
        ///     que llegan pueden no corresponder con lo que se ha pedido y aun así
        ///     se puede aceptar la cantidad
        /// </remarks>
        private void AjustTransitGoodMaterials(HKSupplyContext db, DocHead docHead)
        {
            try
            {
                var BDSTK = new PRJ_Stocks.DB.BD_Stocks();
                PRJ_Stocks.Classes.Stocks STKAct = BDSTK.GetCurrentStock(db);


                var whEtniaHkTransit = STKAct.GetWareHouse(
                    Constants.ETNIA_HK_COMPANY_CODE,
                    PRJ_Stocks.Classes.Stocks.StockWareHousesType.Transit);

                List<string> docs = new List<string>();
                docs.Add(docHead.IdDoc);

                foreach (var line in docHead.Lines)
                {

                    var qty = line.DeliveredQuantity + line.RejectedQuantity;

                    if (qty > 0)
                    {
                        decimal qtyDelTransit = 0;
                        //obtenemos el stock de tránsito para ese item y miramos si tiene algo asignado al proveedor
                        var stockItemWare = STKAct.GetStockItem(ware: whEtniaHkTransit, idItem: line.IdItemBcn);
                        decimal qtyTransitSupplier = stockItemWare.LstDetRes
                            .Where(a => a.idOwner.Equals(docHead.IdSupplier))
                            .Select(b => b.Qtt).FirstOrDefault();

                        //Si hay suficiente quitamos esa cantidad, sino quitamos hasta lo máximo posible
                        if (qtyTransitSupplier > 0)
                        {
                            if (qtyTransitSupplier >= qty)
                                qtyDelTransit = qty;
                            else
                                qtyDelTransit = qtyTransitSupplier;

                            //liberamos la cantidad
                            STKAct.FreeSockItem(
                                WareORIG: whEtniaHkTransit,
                                idItem: line.IdItemBcn,
                                idOwner: docHead.IdSupplier,
                                Qtt: qtyDelTransit,
                                remarks: string.Empty,
                                LstidDoc: docs,
                                IdUser: GlobalSetting.LoggedUser.UserLogin.ToUpper());

                            //la quitamos (un add en negativo)
                            STKAct.AddSockItem(
                                MoveType: PRJ_Stocks.Classes.Stocks.StockMovementsType.Exit,
                                ware: whEtniaHkTransit,
                                Qtt: (qtyDelTransit * -1),
                                idItem: line.IdItemBcn,
                                idLot: string.Empty,
                                idOwner: string.Empty,
                                Remarks: string.Empty,
                                LstidDoc: docs,
                                IdUser: GlobalSetting.LoggedUser.UserLogin.ToUpper());
                        }

                    }
                }

                BDSTK.SaveCurrentStockMovs(db, STKAct);

            }
            catch
            {
                throw;
            }
        }

        private void DelRejectedStock(HKSupplyContext db, DocHead docHead)
        {
            try
            {
                var BDSTK = new PRJ_Stocks.DB.BD_Stocks();
                PRJ_Stocks.Classes.Stocks STKAct = BDSTK.GetCurrentStock(db);

                var whEtniaHkRejected = STKAct.GetWareHouse(
                    Constants.ETNIA_HK_COMPANY_CODE,
                    PRJ_Stocks.Classes.Stocks.StockWareHousesType.Rejected);

                List<string> docs = new List<string>();
                docs.Add(docHead.IdDoc);

                foreach (var line in docHead.Lines)
                {
                    //liberamos
                    STKAct.FreeSockItem(
                                WareORIG: whEtniaHkRejected,
                                idItem: line.IdItemBcn,
                                idOwner: docHead.IdSupplier,
                                Qtt: line.QuantityOriginal,
                                remarks: string.Empty,
                                LstidDoc: docs,
                                IdUser: GlobalSetting.LoggedUser.UserLogin.ToUpper());

                    //eliminamos (añadir en negativo)
                    STKAct.AddSockItem(
                        MoveType: PRJ_Stocks.Classes.Stocks.StockMovementsType.Exit,
                        ware: whEtniaHkRejected,
                        Qtt: (line.QuantityOriginal * -1),
                        idItem: line.IdItemBcn,
                        idLot: string.Empty,
                        idOwner: string.Empty,
                        Remarks: line.Remarks,
                        LstidDoc: docs,
                        IdUser: GlobalSetting.LoggedUser.UserLogin.ToUpper());
                }

                BDSTK.SaveCurrentStockMovs(db, STKAct);

            }
            catch
            {
                throw;
            }
        }

        #endregion

        #endregion

        #region Control de Stock

        /// <summary>
        /// Método para reservar stock (utiliza la API de improvops)
        /// </summary>
        /// <param name="db">database context</param>
        /// <param name="docHead">modified document </param>
        /// <param name="docHeadOriginal">Document with original data before modification, to see the diferences for assignment</param>
        private void AssignStockForDoc(HKSupplyContext db, DocHead docHead, DocHead docHeadOriginal)
        {
            try
            {
                var BDSTK = new PRJ_Stocks.DB.BD_Stocks();
                PRJ_Stocks.Classes.Stocks STKAct = BDSTK.GetCurrentStock(db);

                var whEtniaHkOnHand = STKAct.GetWareHouse(
                    Constants.ETNIA_HK_COMPANY_CODE, 
                    PRJ_Stocks.Classes.Stocks.StockWareHousesType.OnHand); //siempre reservaremos desde el on hand de Etnia HK

                List<string> docs = new List<string>();
                docs.Add(docHead.IdDoc);

                foreach (var line in docHead.Lines)
                {
                    var lineOriginal = docHeadOriginal?.Lines?.Where(a => a.IdItemBcn.Equals(line.IdItemBcn) && a.IdItemGroup.Equals(line.IdItemGroup)).FirstOrDefault();
                    var variationQty = line.Quantity - (lineOriginal == null ? 0 : lineOriginal.Quantity);

                    STKAct.AsgnSockItem(
                       WareORIG: whEtniaHkOnHand,
                       idItem: line.IdItemBcn,
                       Qtt: variationQty,
                       idOwner: docHead.IdCustomer,
                       remarks: string.Empty,
                       LstidDoc: docs,
                       IdUser: GlobalSetting.LoggedUser.UserLogin);
                }

                BDSTK.SaveCurrentStockMovs(db, STKAct);

            }
            catch
            {
                throw;
            }
        }

        private void MoveStockForDoc(HKSupplyContext db, DocHead docHead)
        {
            try
            {
                var BDSTK = new PRJ_Stocks.DB.BD_Stocks();
                PRJ_Stocks.Classes.Stocks STKAct = BDSTK.GetCurrentStock(db);

                var whEtniaHkOnHand = STKAct.GetWareHouse(
                    Constants.ETNIA_HK_COMPANY_CODE,
                    PRJ_Stocks.Classes.Stocks.StockWareHousesType.OnHand); //origen on hand de Etnia HK

                var whDestinationOnHand = STKAct.GetWareHouse(
                    docHead.IdCustomer,
                    PRJ_Stocks.Classes.Stocks.StockWareHousesType.OnHand); //destino on hand del cliente

                List<string> docs = new List<string>();
                docs.Add(docHead.IdDoc);

                foreach (var line in docHead.Lines)
                {
                    var batches = docHead.PackingListItemBatches.Where(a => a.IdItemBcn.Equals(line.IdItemBcn) && a.IdDocRelated.Equals(line.IdDocRelated)).ToList();
                    foreach (var batch in batches)
                    {
                        STKAct.MoveSockItem(
                            MoveType: PRJ_Stocks.Classes.Stocks.StockMovementsType.Movement,
                            WareORIG: whEtniaHkOnHand,
                            WareDEST: whDestinationOnHand,
                            Qtt: batch.Quantity,
                            idItem: line.IdItemBcn,
                            idOwner: docHead.IdCustomer,
                            idlot: batch.Batch,
                            remarks: line.Remarks,
                            LstidDoc: docs,
                            IdUser: GlobalSetting.LoggedUser.UserLogin.ToUpper());
                    }
                }

                BDSTK.SaveCurrentStockMovs(db, STKAct);

            }
            catch
            {
                throw;
            }
        }

        private void DirectEntryToWareHouse(HKSupplyContext db, DocHead docHead, string idWareHouse, PRJ_Stocks.Classes.Stocks.StockWareHousesType wareType)
        {
            try
            {
                var BDSTK = new PRJ_Stocks.DB.BD_Stocks();
                PRJ_Stocks.Classes.Stocks STKAct = BDSTK.GetCurrentStock(db);

                var wareHouseDest = STKAct.GetWareHouse(
                    idWareHouse: idWareHouse, 
                    WareTYpe: wareType);

                List<string> docs = new List<string>();
                docs.Add(docHead.IdDoc);

                foreach(var line in docHead.Lines)
                {
                    STKAct.AddSockItem(
                        MoveType: PRJ_Stocks.Classes.Stocks.StockMovementsType.Entry,
                        ware: wareHouseDest, 
                        Qtt: line.Quantity, 
                        idItem: line.IdItemBcn, 
                        idLot: string.Empty, 
                        idOwner: string.Empty, 
                        Remarks: line.Remarks, 
                        LstidDoc: docs, 
                        IdUser: GlobalSetting.LoggedUser.UserLogin.ToUpper()
                        );
                }
                BDSTK.SaveCurrentStockMovs(db, STKAct);
            }
            catch
            {
                throw;
            }
        }

        private void AddStockToTransit(HKSupplyContext db, DocHead docHead)
        {
            try
            {
                //PRJ_Stocks.DB.Call_DB_Stocks CallDBS = new PRJ_Stocks.DB.Call_DB_Stocks();
                //PRJ_Stocks.Classes.Stocks STKAct = CallDBS.CallCargaStocks();
                var BDSTK = new PRJ_Stocks.DB.BD_Stocks();
                PRJ_Stocks.Classes.Stocks STKAct = BDSTK.GetCurrentStock(db);

                var whEtniaHkTransit = STKAct.GetWareHouse(
                    Constants.ETNIA_HK_COMPANY_CODE,
                    PRJ_Stocks.Classes.Stocks.StockWareHousesType.Transit);

                List<string> docs = new List<string>();
                docs.Add(docHead.IdDoc);

                foreach (var line in docHead.Lines)
                {
                    STKAct.AddSockItem(
                        MoveType: PRJ_Stocks.Classes.Stocks.StockMovementsType.Transit,
                        ware: whEtniaHkTransit,
                        Qtt: line.Quantity,
                        idItem: line.IdItemBcn,
                        idLot: string.Empty,
                        idOwner: docHead.IdSupplier,
                        Remarks: string.Empty,
                        LstidDoc: docs,
                        IdUser: GlobalSetting.LoggedUser.UserLogin
                        );
                }

                BDSTK.SaveCurrentStockMovs(db, STKAct);

            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
