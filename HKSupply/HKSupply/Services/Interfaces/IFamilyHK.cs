using HKSupply.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Services.Interfaces
{
    public interface IFamilyHK
    {
        List<FamilyHK> GetFamiliesHK();
        FamilyHK GetFamilyHKById(string idFamilyHk);
        FamilyHK NewFamilyHK(FamilyHK newFamilyHK);
        bool UpdateFamilyHK(IEnumerable<FamilyHK> familiesHkToUpdate);
    }
}
