using HKSupply.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Services.Interfaces
{
    public interface IMaterial
    {
        List<MaterialL1> GetMaterialsL1();
        List<MaterialL2> GetMaterialsL2();
        List<MaterialL3> GetMaterialsL3();
    }
}
