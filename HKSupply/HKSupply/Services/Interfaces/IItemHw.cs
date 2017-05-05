using HKSupply.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Services.Interfaces
{
    public interface IItemHw
    {
        List<ItemHw> GetItems();
        ItemHw GetItem(string idItemBcn);
        bool UpdateItem(ItemHw updateItem, bool newVersion = false);
        bool UpdateItemWithDoc(ItemHw updateItem, ItemDoc itemDoc, bool newVersion = false);
        bool UpdateItems(IEnumerable<ItemHw> itemsToUpdate);
        List<ItemHwHistory> GetItemHwHistory(string idItemBcn);
    }
}
