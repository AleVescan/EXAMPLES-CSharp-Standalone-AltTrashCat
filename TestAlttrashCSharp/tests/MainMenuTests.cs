using Altom.AltDriver;
using alttrashcat_tests_csharp.pages;
using System;
using System.Threading;
using NUnit.Framework;
namespace alttrashcat_tests_csharp.tests
{
    public class MainMenuTests
    {
        AltDriver altDriver;
        MainMenuPage mainMenuPage;

        StorePage storePage;
        
        SettingsPage settingsPage;

        [SetUp]
        public void Setup()
        {
            altDriver = new AltDriver(port: 13000);
            mainMenuPage = new MainMenuPage(altDriver);
            mainMenuPage.LoadScene();
            settingsPage = new SettingsPage(altDriver);
            storePage = new StorePage(altDriver);
           
        }

        [TearDown]
        public void Dispose()
        {
            altDriver.Stop();
            Thread.Sleep(1000);
        }

        [Test]
        public void TestMainMenuPageLoadedCorrectly()
        {
            Assert.True(mainMenuPage.IsDisplayed());
        }

        public SettingsPage GetSettingsPage()
        {
            return settingsPage;
        }

        [Test]
        public void TestDeleteData()
        
        {
            mainMenuPage.LoadScene();

            Assert.True(mainMenuPage.IsDisplayed());

            mainMenuPage.PressSettings();

            Assert.True(settingsPage.PopUpisDisplayed());

            settingsPage.PressDeleteData();
        

            Assert.True(settingsPage.ConfirmationPopUpisDisplayed());

            settingsPage.PressYesDeleteData();

            Assert.True(settingsPage.PopUpisDisplayed());

            settingsPage.PressClosePopUp();

            mainMenuPage.PressStore();


            Assert.True(storePage.StoreIsDisplayed());

            Assert.True(storePage.CountersReset());
         
        }
    }
}