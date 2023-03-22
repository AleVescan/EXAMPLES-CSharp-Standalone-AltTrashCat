using Altom.AltDriver;
using alttrashcat_tests_csharp.pages;
using System;
using System.Threading;
using NUnit.Framework;
namespace alttrashcat_tests_csharp.tests
{
    public class StoreMenuTests
    {
        AltDriver altDriver;
        StorePage storePage;
        [SetUp]
        public void Setup()
        {
            altDriver = new AltDriver(port: 13000);
            storePage = new StorePage(altDriver);
            storePage.LoadScene();
        }

        [TearDown]
        public void Dispose()
        {
            altDriver.Stop();
            Thread.Sleep(1000);
        }

        [Test]
        public void TestStoreMenuPageLoadedCorrectly()
        {
            Assert.True(storePage.StoreIsDisplayed());
        }

        [Test]

        public void TestPressingStoreIncreasesCoins()
        {
            string initialPremiumCoinsValue = storePage.PremiumCounter.GetText(); 
            string initialCoinsValue = storePage.CoinsCounter.GetText();

            storePage.PressStore();

            string updatedPremiumCoinsValue = storePage.PremiumCounter.GetText();  
            string updatedCoinsValue = storePage.CoinsCounter.GetText();

            Assert.AreNotEqual(initialCoinsValue, updatedCoinsValue);
            Assert.AreNotEqual(initialPremiumCoinsValue, updatedPremiumCoinsValue);
        }
    }
}