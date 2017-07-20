using HKSupply.Models;
using System.Collections.Generic;

namespace HKSupply.Services.Interfaces
{
    public interface ISupplierFactoryCoeff
    {
        List<SupplierFactoryCoeff> GetAllSupplierFactoryCoeff();
        SupplierFactoryCoeff NewSupplierFactoryCoeff(SupplierFactoryCoeff newSupplierFactoryCoeff);
        bool UpdateSupplierFactoryCoeff(IEnumerable<SupplierFactoryCoeff> SupplierFactoryCoeffsToUpdate);
    }
}
