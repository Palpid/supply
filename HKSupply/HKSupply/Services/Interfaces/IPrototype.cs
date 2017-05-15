using HKSupply.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Services.Interfaces
{
    /// <summary>
    /// Interface para el servicio de Prototypes
    /// </summary>
    public interface IPrototype
    {
        List<Prototype> GetPrototypes();
        bool AddPrototypeDoc(PrototypeDoc doc);
    }
}
