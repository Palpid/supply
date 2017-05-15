using HKSupply.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Services.Interfaces
{
    public interface IMatType
    {
        List<MatTypeL1> GetMatsTypeL1();
        List<MatTypeL2> GetMatsTypeL2();
        List<MatTypeL3> GetMatsTypeL3();
    }
}
