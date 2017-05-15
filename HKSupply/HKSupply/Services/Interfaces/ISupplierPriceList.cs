using HKSupply.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Services.Interfaces
{
    public interface ISupplierPriceList
    {
        bool NewSupplierPriceList(SupplierPriceList newSupplierPriceList);
        bool UpdateSupplierPriceList(SupplierPriceList updateSupplierPriceList, bool newVer = false);
        bool UpdateSuppliersPricesList(IEnumerable<SupplierPriceList> pricesListToUpdate);
        SupplierPriceList GetSupplierPriceList(string idItemBcn, string idSupplier);
        List<SupplierPriceList> GetSuppliersPriceList(string idItemBcn = null, string idSupplier = null);
        List<SupplierPriceListHistory> GetSupplierPriceListHistory(string idItemBcn, string idSupplier);
    }
}
