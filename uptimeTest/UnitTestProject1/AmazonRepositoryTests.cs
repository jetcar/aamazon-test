using System;
using System.Configuration;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using uptimeTest.Repositories;

namespace UnitTestProject1
{
    [TestClass]
    public class AmazonRepositoryTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            AmazonRepository repository = new AmazonRepository();
            int totalpages;
            var items = repository.ItemSearch("potter stone yellow", 1, 13, out totalpages);
            Assert.IsNotNull(items);
            Assert.AreEqual(13,items.Count);
            Assert.IsTrue(totalpages == 2);
            Assert.IsNotNull(items[0].ItemAttributes.ListPrice.FormattedPrice);
            Assert.IsNotNull(items[0].Offers.Offer[0].OfferListing[0].Price.FormattedPrice);
            

        }

        
    }
}
