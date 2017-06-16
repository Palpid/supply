using HKSupply.Models;
using System.Collections.Generic;

namespace HKSupply.Services.Interfaces
{
    public interface IBomBreakdown
    {
        List<BomBreakdown> GetBomBreakdowns();
        BomBreakdown NewBomBreakdown(BomBreakdown newBomBreakdown);
        bool UpdateBomBreakdown(IEnumerable<BomBreakdown> bomBreakdownsToUpdate);
    }
}
