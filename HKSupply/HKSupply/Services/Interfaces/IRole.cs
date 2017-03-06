using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKSupply.Models;

namespace HKSupply.Services.Interfaces
{
    public interface IRole
    {
        IEnumerable<Role> GetRoles(bool all = true);
        Role GetRoleById(string roleId);
        Role NewRole(Role newRole);
        bool DisableRole(string roleId, string remarks);
        bool UpdateRoles(IEnumerable<Role> rolesToUpdate);
    }
}
