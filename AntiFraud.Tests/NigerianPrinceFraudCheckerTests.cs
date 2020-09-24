using AntiFraud.API.FraudCheckers;
using AntiFraud.API.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AntiFraud.Tests
{
    [TestClass]
    public class NigerianPrinceFraudCheckerTests : InMemoryTestDb
    {
        [TestMethod]
        public void Test_NotNigeria_AmountMoreThan1000()
        {
            using (var context = new DataContext(ContextOptions))
            {
                var fc = new NigerianPrinceFraudChecker(context);

                var isFraud = fc.IsFraud(new Purchase("rafal@2.pl", 999999, "PLN", new Address("s", "z", "c", "pl"), new List<Product>() { new Product("n", 2) }));

                Assert.IsFalse(isFraud);
            }
        }

        [TestMethod]
        public void Test_Nigeria_AmountLessThan1000()
        {
            using (var context = new DataContext(ContextOptions))
            {
                var fc = new NigerianPrinceFraudChecker(context);

                var isFraud = fc.IsFraud(new Purchase("rafal@2.pl", 300, "PLN", new Address("s", "z", "c", "nigeria"), new List<Product>() { new Product("n", 2) }));

                Assert.IsFalse(isFraud);
            }
        }

        [TestMethod]
        public void Test_NotNigeria_AmountLessThan1000()
        {
            using (var context = new DataContext(ContextOptions))
            {
                var fc = new NigerianPrinceFraudChecker(context);

                var isFraud = fc.IsFraud(new Purchase("rafal@2.pl", 300, "PLN", new Address("s", "z", "c", "polonia"), new List<Product>() { new Product("n", 2) }));

                Assert.IsFalse(isFraud);
            }
        }

        [TestMethod]
        public void Test_Nigeria_AmountMoreThan1000()
        {
            using (var context = new DataContext(ContextOptions))
            {
                var fc = new NigerianPrinceFraudChecker(context);

                var isFraud = fc.IsFraud(new Purchase("rafal@2.pl", 300000, "PLN", new Address("s", "z", "c", "nigeria"), new List<Product>() { new Product("n", 2) }));

                Assert.IsTrue(isFraud);
            }
        }

        [TestMethod]
        public void Test_Nigeria_AmountMoreThan1000_ExistingCustomer()
        {
            using (var context = new DataContext(ContextOptions))
            {
                var fc = new NigerianPrinceFraudChecker(context);

                var isFraud = fc.IsFraud(new Purchase("nigerian.prince@wp.pl", 300000, "PLN", new Address("s", "z", "c", "nigeria"), new List<Product>() { new Product("n", 2) }));

                Assert.IsFalse(isFraud);
            }
        }

        [TestMethod]
        public void Test_Nigeria_AmountMoreThan1000_NotExistingCustomer()
        {
            using (var context = new DataContext(ContextOptions))
            {
                var fc = new NigerianPrinceFraudChecker(context);

                var isFraud = fc.IsFraud(new Purchase("nigerian.king@wp.pl", 300000, "PLN", new Address("s", "z", "c", "nigeria"), new List<Product>() { new Product("n", 2) }));

                Assert.IsTrue(isFraud);
            }
        }
    }
}
