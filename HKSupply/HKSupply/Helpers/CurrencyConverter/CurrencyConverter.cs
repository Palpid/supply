using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Globalization;
using System.Xml;
using HKSupply.Models;
using HKSupply.General;

namespace HKSupply.Helpers.CurrencyConverter
{
    /// <summary>
    /// Helper Class for Currencies conversions. 
    /// Connect to European Central Bank to get a XML with rates.
    /// Options to update DB with rates
    /// </summary>
    public class CurrencyConverter : ICurrencyConverter
    {
        #region Private Members
        private string sourceUrl = @"http://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";
        private XmlTextReader xml;
        private string baseCurrency;
        private Dictionary<string, decimal> exchangeRates = new Dictionary<string, decimal>();
        private DateTime date = new DateTime();
        private string currency;
        private decimal rate;
        #endregion

        #region Public Properties
        /// <summary>
        /// Use this readonly property to check the actual date for the rates
        /// </summary>
        public DateTime Date { get { return date; } }

        /// <summary>
        /// Use this property to get or set base currency
        /// Base currency is used for displaying rates table and convertions. All calculations are performed according to base currency!
        /// EUR by default
        /// Throws ApplicationException if value is not in currency list
        /// </summary>
        public string BaseCurrency
        {
            get
            {
                return baseCurrency;
            }
            set
            {
                if (value == null)
                {
                    value = "EUR";
                }
                value = value.ToUpper();
                value = value.Trim();
                CheckCurrency(value);
                baseCurrency = value;
                decimal factor = exchangeRates[baseCurrency];
                List<string> keys = new List<string>(exchangeRates.Keys);
                foreach (string key in keys)
                {
                    exchangeRates[key] /= factor;
                }
            }
        }

        #endregion

        #region Constructor
        public CurrencyConverter()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            try
            {
                xml = new XmlTextReader(sourceUrl); //tries to download XML file and create the Reader object
            }
            catch (WebException we) // if download is imposible, creates defalt value for USD and EUR and throws an exception
            {
                baseCurrency = "EUR";
                exchangeRates.Add(baseCurrency, 1M);
                exchangeRates.Add("USD", 1.1810M);
                date = DateTime.Now;
                throw new WebException("Error downloading XML, exchange rate created for USD only, base currency EUR!", we);
            }
            try
            {
                while (xml.Read())
                {
                    if (xml.Name == "Cube")
                    {
                        if (xml.AttributeCount == 1)
                        {
                            xml.MoveToAttribute("time");
                            date = DateTime.Parse(xml.Value); // gets the date on which this rates are valid
                        }
                        if (xml.AttributeCount == 2)
                        {
                            xml.MoveToAttribute("currency");
                            currency = xml.Value;
                            xml.MoveToAttribute("rate");
                            try
                            {
                                rate = decimal.Parse(xml.Value);
                            }
                            catch (FormatException fe)
                            {
                                throw new FormatException("Urecognised format!", fe);
                            }
                            exchangeRates.Add(currency, rate); //add currency and rate to exchange rate table
                        }
                        xml.MoveToNextAttribute();
                    }
                }
            }
            catch (XmlException xe)
            {
                throw new XmlException("Unable to parse Euro foreign exchange reference rates XML!", xe);
            }
            baseCurrency = "EUR"; // if XML parsed, add base currency
            exchangeRates.Add(baseCurrency, 1M);
        }
        #endregion

        #region Override
        /// <summary>
        /// Converts Exchange Rate Table to String
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append($"Reference rates of European Central Bank{Environment.NewLine}All rates are for 1 {baseCurrency}{Environment.NewLine}{Environment.NewLine}");
            foreach (KeyValuePair<string, decimal> kvp in exchangeRates)
            {
                str.Append(String.Format("{0}{1,15:0.0000}" + Environment.NewLine, kvp.Key, kvp.Value));
            }
            return str.ToString();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// checks if currency is in currency list and throws exception if not
        /// </summary>
        /// <param name="currency"></param>
        private void CheckCurrency(string currency)  
        {
            if (!exchangeRates.ContainsKey(currency))
            {
                throw new ApplicationException($"Unknown currency '{currency}', please use GetCurrencyList() to get list of available currencies!", new KeyNotFoundException());
            }
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Exchanges the givven amount from one currency to the other
        /// </summary>
        /// <param name="amount">The amount to be exchanged</param>
        /// <param name="from">Currency of the amount (three letter code)</param>
        /// <param name="to">Currency to witch we wish to exchange. Base currency if not specified.</param>
        /// <returns>the exchanged amount on success</returns>
        /// <remarks>Throws ApplicationException if currency is not in currency list</remarks>
        public decimal Exchange(decimal amount, string from, string to = null)
        {
            try
            {
                decimal result = 0M;
                if (to == null)
                {
                    to = baseCurrency;
                }
                from = from.ToUpper().Trim();
                to = to.ToUpper().Trim();
                CheckCurrency(from);
                CheckCurrency(to);
                result = amount * exchangeRates[to] / exchangeRates[from];
                return result;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the rates table based on Base currency
        /// </summary>
        /// <param name="currencyList">List of comma separated Currencies to be included in the table. All currencies by default</param>
        /// <returns>IEnumerable<Rates> containing desired currencies and rates</returns>
        /// <remarks>Throws ApplicationException if currency is not in currency list</remarks>
        public IEnumerable<Rates> GetRatesTable(string currencyList = null)
        {
            try
            {
                List<Rates> result = new List<Rates>();
                Rates tempRate = new Rates();
                if (currencyList == null)
                {
                    foreach (string currency in this.exchangeRates.Keys)
                    {
                        tempRate.Currency = currency;
                        tempRate.Rate = String.Format("{0:0.0000}", this.exchangeRates[currency]);
                        result.Add(tempRate);
                    }
                }
                else
                {
                    currencyList = currencyList.ToUpper();
                    char[] delimiter = { ',', ' ', ';' }; //just in case some one don't know what comma separated is
                    string[] list = currencyList.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string currency in list)
                    {
                        currency.Trim();
                        CheckCurrency(currency);
                        tempRate.Currency = currency;
                        tempRate.Rate = String.Format("{0:0.0000}", this.exchangeRates[currency]);
                        result.Add(tempRate);
                    }
                }
                return result;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the list of currencies.
        /// </summary>
        /// <param name="sorted">If sorted is true, the returned list is sorted. False by default</param>
        /// <returns>IEnumerable<string> of all available currencies </returns>
        public IEnumerable<string> GetCurrencyList(bool sorted = false)
        {
            try
            {
                List<string> currencyList = new List<string>(exchangeRates.Keys);
                if (sorted)
                {
                    currencyList.Sort();
                }
                return currencyList;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Update echange rates on data base
        /// </summary>
        /// <returns>True if update is success</returns>
        public bool UpdateDbExchangeRates()
        {
            try
            {
                List<ExchangeRate> exchangeRates = new List<ExchangeRate>();

                foreach (KeyValuePair<string, decimal> kvp in this.exchangeRates)
                {
                    ExchangeRate rate = new ExchangeRate()
                    {
                        Date = Date,
                        IdCurrency1 = baseCurrency,
                        IdCurrency2 = kvp.Key,
                        Ratio = kvp.Value
                    };
                    exchangeRates.Add(rate);
                }

                return GlobalSetting.EchangeRateService.UpdateEchangeRates(exchangeRates);

            }
            catch
            {
                throw;
            }
        }

        #endregion

    }
}
