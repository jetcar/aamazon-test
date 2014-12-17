using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using uptimeTest.AmazonReference1;

namespace uptimeTest.Extensions
{
    public static class ItemExtension
    {
        public static string GetDetailPageURL(this Item item)
        {
            return item.DetailPageURL;
        }
        public static string GenerateOldCurrencyId(this Item item)
        {
            return "oldCurrency"+item.ASIN;
        }
        public static string GeneratenewPriceId(this Item item)
        {
            return "pricenew" + item.ASIN;
        }
        public static string GeneratenewCurrencyId(this Item item)
        {
            return "currencynew" + item.ASIN;
        }
        public static Tuple<string, string> GetPrice(this Item item)
        {
            if (item.Offers != null && item.Offers.Offer != null)
            {
                foreach (var offer in item.Offers.Offer)
                {
                    foreach (var offerListing in offer.OfferListing)
                    {
                        if (offerListing.Price != null)
                        {
                            var amount = 0m;
                            decimal.TryParse(offerListing.Price.Amount, out amount);
                            if (amount == 0)
                                return new Tuple<string, string>("free", "");

                            amount /= 100;
                            return new Tuple<string, string>(amount.ToString("F2"), offerListing.Price.CurrencyCode);
                        }
                    }
                }
            }
            if (item.ItemAttributes != null && item.ItemAttributes.ListPrice != null)
            {
                var amount = 0m;
                decimal.TryParse(item.ItemAttributes.ListPrice.Amount, out amount);
                if (amount == 0)
                    return new Tuple<string, string>("free", "");
                amount /= 100;

                return new Tuple<string, string>(amount.ToString("F2"), item.ItemAttributes.ListPrice.CurrencyCode);
            }

            return new Tuple<string, string>("", "");
        }
    }
}