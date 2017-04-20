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
        SupplierPriceList GetSupplierPriceList(string idItemBcn, string idSupplier);
        List<SupplierPriceList> GetSuppliersPriceList();
        List<SupplierPriceListHistory> GetSupplierPriceListHistory(string idItemBcn, string idSupplier);
    }
}
