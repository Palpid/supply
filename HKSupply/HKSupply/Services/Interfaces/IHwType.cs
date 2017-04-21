using HKSupply.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Services.Interfaces
{
    public interface IHwType
    {
        List<HwTypeL1> GetHwsTypeL1();
        List<HwTypeL2> GetHwsTypeL2();
        List<HwTypeL3> GetHwsTypeL3();
    }
}
