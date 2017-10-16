using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Helpers.CurrencyConverter
{
    public interface ICurrencyConverter
    {
        //-----------------Properties--------------------------

        /// <summary>
        /// Use this readonly property to check the actual date for the rates
        /// </summary>
        DateTime Date { get; }

        /// <summary>
        /// Use this property to get or set base currency
        /// Base currency is used for displaying rates table and convertions. All calculations are performed according to base currency!
        /// EUR by default
        /// Throws ApplicationException if value is not in currency list
        /// </summary>
        string BaseCurrency { get; set; }


        //-----------------Methods------------------------------
        /// <summary>
        /// Exchanges the given amount from one currency to the other
        /// </summary>
        /// <param name="amount">The amount to be exchanged</param>
        /// <param name="from">Currency of the amount (three letter code)</param>
        /// <param name="to">Currency to witch we wish to exchange. Base currency if not specified.</param>
        /// <returns>The exchanged amount on success</returns>
        /// <remarks>Throws ApplicationException if currency is not in currency list</remarks>
        decimal Exchange(decimal amount, string from, string to = null);

        /// <summary>
        /// Gets the rates table based on Base currency
        /// </summary>
        /// <param name="currencyList">List of comma separated Currencies to be included in the table. All currencies by default</param>
        /// <returns>IEnumerable<Rates> containing desired currencies and rates</returns>
        /// <remarks>Throws ApplicationException if currency is not in currency list</remarks>
        IEnumerable<Rates> GetRatesTable(string currencyList = null);

        /// <summary>
        /// Gets the list of currencies.
        /// </summary>
        /// <param name="sorted">If sorted is true, the returned list is sorted. False by default</param>
        /// <returns>IEnumerable<string> of all available currencies </returns>
        IEnumerable<string> GetCurrencyList(bool sorted = false);

        /// <summary>
        /// Update echange rates on data base
        /// </summary>
        /// <returns>True if update is success</returns>
        bool UpdateDbExchangeRates();

    }
}
