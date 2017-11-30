using System.Collections.Generic;
using HKSupply.Models.Supply;

namespace HKSupply.Services.Interfaces
{
    public interface IBox
    {
        List<Box> GetBoxes();
    }
}
