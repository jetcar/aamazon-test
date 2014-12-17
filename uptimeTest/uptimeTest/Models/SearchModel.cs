using System.Collections;
using System.Collections.Generic;
using uptimeTest.AmazonReference1;
using uptimeTest.CurrencyReference1;

namespace uptimeTest.Models
{
    public class SearchModel
    {
        private IList<Item> _items = new List<Item>();
        private IList<Currency> _allCurrencies = new List<Currency>();
        public string Search { get; set; }

        public IList<Item> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        public int TotalPages { get; set; }
        public string Currencies { get; set; }
        public int CurrentPage { get; set; }

        public IList<Currency> AllCurrencies
        {
            get { return _allCurrencies; }
            set { _allCurrencies = value; }
        }
    }
}