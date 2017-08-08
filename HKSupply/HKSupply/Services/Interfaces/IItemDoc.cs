using HKSupply.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Services.Interfaces
{
    public interface IItemDoc
    {
        List<ItemDoc> GetItemsDocs(string idItemBcn, string idItemGroup);
        List<ItemDoc> GetLastItemsDocs(string idItemBcn, string idItemGroup);
        List<ItemDoc> GetLastItemsDocsListItems(List<string> idItemBcnList, string idItemGroup);
    }
}
