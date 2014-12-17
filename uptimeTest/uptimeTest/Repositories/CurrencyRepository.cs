using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using uptimeTest.CurrencyReference1;

namespace uptimeTest.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private CurrencyReference1.CurrencyConvertorSoapClient client = new CurrencyConvertorSoapClient();

        public double ConversionRate(Currency fromCurrency, Currency toCurrency)
        {
           return client.ConversionRate(fromCurrency, toCurrency);
        }

        public double ConversionRate(string fromCurrency, string toCurrency)
        {
            Currency from, to;
            Enum.TryParse(fromCurrency, out from);
            Enum.TryParse(toCurrency, out to);
            return ConversionRate(from, to);
        }
    }

    public interface ICurrencyRepository
    {
        double ConversionRate(Currency fromCurrency, Currency toCurrency);
        double ConversionRate(string fromCurrency, string toCurrency);
    }
}