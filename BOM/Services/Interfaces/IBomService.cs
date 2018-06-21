using BOM.Classes;
using BOM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOM.Services.Interfaces
{
    public interface IBomService
    {
        List<Bom> GetItemBom(string itemCode);
        List<BomBreakdown> GetBromBreakdown();
        bool EditBom(List<Bom> itemBoms);
        bool ImportBom(List<BomImportTmp> bomImportRows);
        List<BomImportTmp> GetImportBomByGuid(string guid);
        int MassiveItemChange(string originalItemCode, string changeItemcode);
    }
}
