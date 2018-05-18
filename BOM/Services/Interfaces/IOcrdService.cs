using BOM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOM.Services.Interfaces
{
    public interface IOcrdService
    {
        List<Ocrd> GetFactories();
    }
}
