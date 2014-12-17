using System;
using System.Configuration;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using uptimeTest.CurrencyReference1;
using uptimeTest.Repositories;

namespace UnitTestProject1
{
    [TestClass]
    public class CurrencyRepositoryTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            CurrencyRepository repository = new CurrencyRepository();
            var rate = repository.ConversionRate(Currency.USD, Currency.RUB);
            Assert.IsNotNull(rate > 0);
        }
        [TestMethod]
        public void TestMethod2()
        {
            Currency result;
            Currency.TryParse("EuR", true, out result);
            Assert.AreEqual(Currency.EUR, result);

           
        }
    }
}
