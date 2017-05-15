using HKSupply.Models;
using System.Collections.Generic;

namespace HKSupply.Services.Interfaces
{
    public interface IPrototypeDoc
    {
        List<PrototypeDoc> GetPrototypeDocs(string idPrototype);
        List<PrototypeDoc> GetLastPrototypeDocs(string idPrototype);
    }
}
