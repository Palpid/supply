using BOM.Classes;
using System.Collections.Generic;

namespace BOM.Services.Interfaces
{
    public interface IOitmService
    {
        List<OitmExt> GetItems();
        List<OitmExt> GetPossibleItemsForBom();
    }
}
