using HKSupply.Forms.Supply.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Services.Interfaces
{
    public interface ISupplyDashboard
    {
        List<AuxDashboardQPStoredProcedure> GetDashboardQP(string queryType, string factories, string weeks, string itemGroup);
    }
}
