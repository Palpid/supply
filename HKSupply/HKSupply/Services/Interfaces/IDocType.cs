using HKSupply.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Services.Interfaces
{
    public interface IDocType
    {
        List<DocType> GetDocsType(string idItemGroup);
    }
}
