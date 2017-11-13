using HKSupply.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Services.Interfaces
{
    public interface ICustomerPriceList
    {
        bool NewCustomerPriceList(CustomerPriceList newCustomerPriceList);
        bool UpdateCustomerPriceList(CustomerPriceList updateCustomerPriceList, bool newVer = false);
        bool UpdateCustomersPricesList(IEnumerable<CustomerPriceList> pricesListToUpdate);
        CustomerPriceList GetCustomerPriceList(string idItemBcn, string idCustomer);
        List<CustomerPriceList> GetCustomersPriceList(string idItemBcn = null, string idCustomer = null);
        List<CustomerPriceListHistory> GetCustomerPriceListHistory(string idItemBcn, string idCustomer);
    }
}
