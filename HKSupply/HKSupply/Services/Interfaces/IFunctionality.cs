using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKSupply.Models;

namespace HKSupply.Services.Interfaces
{
    interface IFunctionality
    {
        Functionality GetFunctionalityByIdRol(string functionalityName, string roleId);
        Functionality NewFunctionality(Functionality newFunctionality);
        Functionality ModifyFunctionality(Functionality modFunctionality);
    }
}
