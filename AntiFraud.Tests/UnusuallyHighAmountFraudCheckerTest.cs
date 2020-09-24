using AntiFraud.API.FraudCheckers;
using AntiFraud.API.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AntiFraud.Tests
{
    [TestClass]
    public class UnusuallyHighAmountFraudCheckerTest : InMemoryTestDb
    {
        [TestMethod]
        public void TestUnusuallyHighAmount()
        {
            using (var context = new DataContext(ContextOptions))
            {
                var fc = new UnusuallyHighAmountFraudChecker(context);

                var isFraud = fc.IsFraud(new Purchase("rafal@2.pl", 999999, "PLN", new Address("s", "z", "c", "pl"), new List<Product>() { new Product("n", 2) }));

                Assert.IsTrue(isFraud);
            }
        }

        [TestMethod]
        public void TestNormalAmount()
        {
            using (var context = new DataContext(ContextOptions))
            {
                var fc = new UnusuallyHighAmountFraudChecker(context);

                var isFraud = fc.IsFraud(new Purchase("rafal@2.pl", 300, "PLN", new Address("s", "z", "c", "pl"), new List<Product>() { new Product("n", 2) }));

                Assert.IsFalse(isFraud);
            }
        }
    }
}
