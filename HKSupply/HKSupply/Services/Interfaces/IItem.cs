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
        List<Item> GetItems();
        Item GetItemByItemCode(string itemCode);
        bool UpdateItem(Item updateItem, bool newVersion = false);
        bool newItem(Item newItem);
    }
}
