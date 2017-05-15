using HKSupply.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Services.Interfaces
{
    /// <summary>
    /// Interface para el service de Functionality - Role
    /// </summary>
    public interface IFunctionalityRole
    {
        IEnumerable<FunctionalityRole> GetAllFunctionalitiesRole();
        FunctionalityRole GetFunctionalityRole(int functionalityId, string roleId);
        IEnumerable<FunctionalityRole> GetFunctionalitiesRole(string roleId);
        IEnumerable<string> GetFunctionalitiesCategoriesRole(string roleId);
        FunctionalityRole NewFunctionalityRole(FunctionalityRole newFunctionalityRole);
        FunctionalityRole ModifyFunctionalityRole(FunctionalityRole modFunctionalityRole);
        bool UpdateFunctionalitiesRoles(IEnumerable<FunctionalityRole> functionalitiesRolesToUpdate);
    }
}
