using System;
using System.Collections.Generic;
using HKSupply.Models.Supply;
using HKSupply.Classes;

namespace HKSupply.Services.Interfaces
{
    public interface ISupplyDocs
    {
        List<SupplyStatus> GetSupplyStatus();
        List<DocHead> GetDocs(string idSupplier, string idCustomer, DateTime docDate, string IdSupplyDocType, string idSupplyStatus);
        List<DocHead> GetDocs(string idDoc, string idSupplier, string idCustomer, DateTime docDateIni, DateTime docDateEnd, string IdSupplyDocType, string idSupplyStatus);
        List<DocHead> GetSalesOrderFromPackingList(string idDocPK);
        DocHead NewDoc(DocHead newDoc);
        DocHead GetDoc(string idDoc);
        DocHead GetDocPackingList(string idDoc);
        DocHead GetDocByRelated(string idDocRelated);
        List<DocHead> GetDocsByRelated(string idDocRelated);
        DocHead UpdateDoc(DocHead doc, bool finishDoc = false);
        List<POSelection> GetPOSelection(string idDocPo, string idSupplyStatus, string idSupplier, DateTime PODateIni, DateTime PODateEnd);
        bool ValidateBomSupplierLines(string idSupplier, List<DocLine> lines , out List<string> itemWithouBom);
        bool UpdateLinesRemarks(List<DocLine> lines);
        List<SupplyDocType> GetSupplyDocTypes();

        String GetPackingListNumber(string idCustomer, DateTime date);
    }
}
