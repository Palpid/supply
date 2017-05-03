using HKSupply.Models;
using System.Collections.Generic;

namespace HKSupply.Services.Interfaces
{
    public interface IItemMt
    {
        List<ItemMt> GetItems();
        ItemMt GetItem(string idItemBcn);
        bool UpdateItem(ItemMt updateItem, bool newVersion = false);
        bool UpdateItemWithDoc(ItemMt updateItem, ItemDoc itemDoc, bool newVersion = false);
        List<ItemMtHistory> GetItemMtHistory(string idItemBcn);
    }
}
