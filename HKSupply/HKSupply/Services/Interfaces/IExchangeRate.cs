using HKSupply.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Services.Interfaces
{
    public interface IExchangeRate
    {
        List<ExchangeRate> GetEchangeRates();
        bool UpdateEchangeRates(IEnumerable<ExchangeRate> echangeRates);
    }
}
