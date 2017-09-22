using System.Collections.Generic;
using HKSupply.Models;

namespace HKSupply.Services.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFunctionalityReport
    {
        List<FunctionalityReport> GetFunctionalityReports(int idFunctionality);
    }
}
