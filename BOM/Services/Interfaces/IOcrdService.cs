using BOM.Classes;
using BOM.Models;
using System.Collections.Generic;

namespace BOM.Services.Interfaces
{
    public interface IOcrdService
    {
        List<Supplier> GetFactories();
    }
}
