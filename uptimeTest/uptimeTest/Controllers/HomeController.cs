using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Ninject;
using uptimeTest.AmazonReference1;
using uptimeTest.CurrencyReference1;
using uptimeTest.Models;
using uptimeTest.Repositories;

namespace uptimeTest.Controllers
{
    public class HomeController : Controller
    {

        [Inject]
        public IAmazonRepository Repository { get; set; }

        [Inject]
        public ICurrencyRepository CurrencyRepository { get; set; }
        public ActionResult Index(SearchModel model, string page, string search)
        {
            foreach (var value in Enum.GetValues(typeof(Currency)))
            {
                if ((Currency)value == Currency.ALL)
                    continue;
                model.AllCurrencies.Add((Currency)value);
            }

            model.Search = search;
            int pageInt = 1;
            if (page != null)
                int.TryParse(page, out pageInt);
            model.CurrentPage = pageInt;
            var Currencies = "";
            if (!string.IsNullOrEmpty(model.Search))
            {
                int totalPages = 0;
                model.Items = Repository.ItemSearch(model.Search, pageInt, 13, out totalPages);
                foreach (var item in model.Items)
                {
                    if (item.Offers != null && item.Offers.Offer != null)
                    {
                        foreach (var offer in item.Offers.Offer)
                        {
                            var firstoffer = offer.OfferListing.FirstOrDefault();
                            if (firstoffer != null && !Currencies.Contains(firstoffer.Price.CurrencyCode))
                            {
                                Currencies += firstoffer.Price.CurrencyCode + ";";
                                break;
                            }

                        }
                    }
                    else if (item.ItemAttributes != null && item.ItemAttributes.ListPrice != null)
                    {
                        if (!Currencies.Contains(item.ItemAttributes.ListPrice.CurrencyCode))
                            Currencies += item.ItemAttributes.ListPrice.CurrencyCode + ";";

                    }
                }
                model.Currencies = Currencies.TrimEnd(';');
                model.TotalPages = totalPages;
            }
            return View(model);
        }


        [HttpPost]
        public JsonResult ChangeCurrency(string Currencies, string SelectedCurrency)
        {
            var currencies = Currencies.Split(';');
            IDictionary<string, double> conversionRates = new Dictionary<string, double>();
            foreach (var currency in currencies)
            {

                var rate = CurrencyRepository.ConversionRate(currency, SelectedCurrency);
                conversionRates[currency] = rate;
            }
            var json = JsonConvert.SerializeObject(conversionRates);
            return Json(json);
        }
    }
}
