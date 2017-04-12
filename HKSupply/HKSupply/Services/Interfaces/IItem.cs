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
    public interface IItem
    {
        List<Item> GetItems(string idItemGroup);
        Item GetItem(string idItemGroup, string idPrototype, string idItemBcn);
        bool UpdateItem(Item updateItem, bool newVersion = false);
        //bool newItem(Item newItem);
    }
}
