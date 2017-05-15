using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKSupply.Models;

namespace HKSupply.Services.Interfaces
{
    /// <summary>
    /// Interface para el service de Functionality
    /// </summary>
    public interface IFunctionality
    {
        IEnumerable<Functionality> GetAllFunctionalities();
        Functionality GetFunctionalityById(int functionalityId);
        Functionality GetFunctionalityByName(string functionalityName);
        Functionality NewFunctionality(Functionality newFunctionality);
        Functionality ModifyFunctionality(Functionality modFunctionality);
        bool UpdateFunctionalities(IEnumerable<Functionality> functionalitiesToUpdate);
    }
}
