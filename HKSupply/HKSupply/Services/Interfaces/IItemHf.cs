using HKSupply.Models;
using System.Collections.Generic;

namespace HKSupply.Services.Interfaces
{
    /// <summary>
    /// Interface para el sevice de ItemHf (half-finished)
    /// </summary>
    public interface IItemHf
    {
        List<ItemHf> GetItems();
        ItemHf GetItem(string idItemBcn);
        bool UpdateItem(ItemHf updateItem, bool newVersion = false);
        bool UpdateItemWithDoc(ItemHf updateItem, ItemDoc itemDoc, bool newVersion = false);
        bool UpdateItems(IEnumerable<ItemHf> itemsToUpdate);
        List<ItemHfHistory> GetItemHfHistory(string idItemBcn);
        List<ItemHf> GetISupplierHfItems(string idSupplier);
    }
}
