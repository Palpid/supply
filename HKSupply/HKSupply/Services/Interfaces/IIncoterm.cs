using HKSupply.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Services.Interfaces
{
    public interface IIncoterm
    {
        List<Incoterm> GetIIncoterms();
        Incoterm GetIncotermById(string idIncoterm);
        Incoterm NewIncoterm(Incoterm newIncoterm);
        bool UpdateIncoterm(IEnumerable<Incoterm> incotermsToUpdate);
    }
}
