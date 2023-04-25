using Altom.AltDriver;
using alttrashcat_tests_csharp.pages;
using System;
using System.Threading;
using NUnit.Framework;
using NUnit.Allure.Core;
using NUnit.Allure.Attributes;
using Allure.Commons;

namespace alttrashcat_tests_csharp.tests
{
    [TestFixture]
    [AllureNUnit]
    [AllureSuite("Store")]

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

       

        [Test]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("AleV")]
        [AllureDescription("The store page is displayed as expected")]
        public void TestStoreMenuPageLoadedCorrectly()
        {
            Assert.True(storePage.StoreIsDisplayed());
        }

        [Test]
        [AllureSeverity(SeverityLevel.minor)]
        [AllureOwner("AleV")]
        [AllureDescription("Pressing the store, testing helper method, increases the coins")]
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
        [AllureSeverity(SeverityLevel.normal)]
        [AllureOwner("AleV")]
        [AllureDescription("The buy buttons are active only if there are enough coins available")]
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

        [Test]
        public void TestBuyMagnetCanBeSetInteractableWithoutEnoughCoins()
        {
            mainMenuPage.LoadScene();
            settingsPage.DeleteData();
            mainMenuPage.PressStore();
            Assert.IsFalse(storePage.BuyButtonsAreEnabled());
            Thread.Sleep(1000);
            storePage.EnableMagnetBuyButton();
            Thread.Sleep(1000);
            Assert.IsTrue(storePage.BuyMagnetButtonIsEnabled());
        

        }

        [Test]

        public void TestThatPremiumButtonAtCoordinatesIsFound()
        {
            mainMenuPage.LoadScene();
            mainMenuPage.PressStore();
            Assert.AreEqual(storePage.PremiumButtonAtCoordinates.GetText(), "+");


        }

        [Test]
        public void TestCharactersTabChangesColorPointEnterExit()
        {
            mainMenuPage.LoadScene();
            mainMenuPage.PressStore();
            storePage.CharactersTabPointerEnterExitStateColors();
        }

         [TearDown]
        public void Dispose()
        {
            altDriver.Stop();
            Thread.Sleep(1000);
        }

        
    }
}