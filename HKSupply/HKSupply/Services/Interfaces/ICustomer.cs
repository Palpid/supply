using HKSupply.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Services.Interfaces
{
    public interface ICustomer
    {
        bool NewCustomer(Customer newCustomer);
        bool UpdateCustomer(Customer updateCustomer, bool newVer = false);
        Customer GetCustomerById(string idCustomer);
        List<Customer> GetCustomers();
    }
}
