using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKSupply.Models;

namespace HKSupply.Services.Interfaces
{
    public interface IFunctionality
    {
        Functionality GetFunctionalityById(int functionalityId);
        Functionality GetFunctionalityByName(string functionalityName);
        Functionality NewFunctionality(Functionality newFunctionality);
        Functionality ModifyFunctionality(Functionality modFunctionality);
    }
}
