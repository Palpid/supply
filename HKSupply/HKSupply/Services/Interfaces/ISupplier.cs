using HKSupply.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Services.Interfaces
{
    /// <summary>
    /// Interface para el sevice de Supplier
    /// </summary>
    public interface ISupplier
    {
        bool NewSupplier(Supplier newSupplier);
        bool UpdateSupplier(Supplier updateSupplier, bool newVer = false);
        Supplier GetSupplierById(string idSupplier);
        List<Supplier> GetSuppliers();
        List<SupplierHistory> GetSupplierHistory(string idSupplier);
    }
}
