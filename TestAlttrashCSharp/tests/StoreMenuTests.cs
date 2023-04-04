using Altom.AltDriver;
using alttrashcat_tests_csharp.pages;
using System;
using System.Threading;
using NUnit.Framework;
using NUnit.Allure.Core;
using NUnit.Allure.Attributes;

namespace alttrashcat_tests_csharp.tests
{
    [TestFixture]
    [AllureNUnit]

    public class StoreMenuTests
    {
        
  
        AltDriver altDriver;
        StorePage storePage;

        MainMenuPage mainMenuPage;

        SettingsPage settingsPage;

        [SetUp]
        public void Setup()
        {
            altDriver = new AltDriver(port: 13000);
            storePage = new StorePage(altDriver);
            storePage.LoadScene();
            mainMenuPage = new MainMenuPage(altDriver);
            settingsPage = new SettingsPage(altDriver);
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

        [Test]

        public void TestBuyButtonsBecomeActiveOnlyWhenEnoughCoins()
        {
           mainMenuPage.LoadScene();
           settingsPage.DeleteData();
           mainMenuPage.PressStore();          
           Assert.IsFalse(storePage.BuyButtonsAreEnabled());
           storePage.PressStore(); 
           Thread.Sleep(1000);
           storePage.PressCharactersTab();
           storePage.ReloadItems();
           Thread.Sleep(1000);
        
           Assert.IsTrue(storePage.BuyButtonsAreEnabled());
        }


        
    }
}