using HKSupply.Models;
using System.Collections.Generic;

namespace HKSupply.Services.Interfaces
{
    /// <summary>
    /// Interface para el service de Layout
    /// </summary>
    public interface ILayout
    {
        IEnumerable<Layout> GetLayouts(int functionalityId, string user);
        IEnumerable<Layout> GetLayouts(int functionalityId, string objectName, string user);
        bool SaveLayout(Layout newLayout);
        bool SaveLayout(List<Layout> layouts);
        bool UpdateLayout(Layout updateLayout);
    }
}
