using HKSupply.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Services.Interfaces
{
    /// <summary>
    /// Interface para el sevice de Item
    /// </summary>
    public interface IItemEy
    {
        List<ItemEy> GetItems();
        ItemEy GetItem(string idItemBcn);
        bool UpdateItem(ItemEy updateItem, bool newVersion = false);
        bool UpdateItemWithDoc(ItemEy updateItem, ItemDoc itemDoc, bool newVersion = false);
        //bool newItem(Item newItem);
        List<ItemEyHistory> GetItemEyHistory(string idItemBcn);
    }
}
