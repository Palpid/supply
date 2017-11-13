using HKSupply.Models;
using System.Collections.Generic;

namespace HKSupply.Services.Interfaces
{
    public interface IDeliveryTerms
    {
        List<DeliveryTerm> GetDeliveryTerms();
    }
}
