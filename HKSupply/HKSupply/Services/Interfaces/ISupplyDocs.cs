using System;
using System.Collections.Generic;
using HKSupply.Models.Supply;


namespace HKSupply.Services.Interfaces
{
    public interface ISupplyDocs
    {
        List<SupplyStatus> GetSupplyStatus();
        List<DocHead> GetDocs(string idSupplier, string idCustomer, DateTime docDate, string IdSupplyDocType);
        DocHead NewDoc(DocHead newDoc);
        DocHead GetDoc(string idDoc);
        DocHead UpdateDoc(DocHead doc, bool finishPO = false);
    }
}
