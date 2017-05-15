using HKSupply.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HKSupply.Services.Interfaces
{
    public interface IItemBcn
    {
        List<ItemBcn> GetItemsBcn();
    }
}
