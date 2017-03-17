using HKSupply.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Services.Interfaces
{
    /// <summary>
    /// Interface para el sevice de Store
    /// </summary>
    public interface IStore
    {
        IEnumerable<Store> GetAllStores();
        Store GetStoreById(string idStore);
        Store NewStore(Store newStore);
        bool UpdateStore(IEnumerable<Store> storesToUpdate);
    }
}
