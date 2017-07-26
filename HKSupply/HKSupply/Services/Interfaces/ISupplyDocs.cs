using System;
using System.Collections.Generic;
using HKSupply.Models.Supply;


namespace HKSupply.Services.Interfaces
{
    public interface ISupplyDocs
    {
        List<SupplyStatus> GetSupplyStatus();
        List<DocHead> GetDocs(string idSupplier, DateTime docDate);
        DocHead NewDoc(DocHead newDoc);
        DocHead GetDoc(string idDoc);
    }
}
