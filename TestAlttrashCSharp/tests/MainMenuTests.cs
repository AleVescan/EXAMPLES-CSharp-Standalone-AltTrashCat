using Altom.AltDriver;
using alttrashcat_tests_csharp.pages;
using System;
using System.Threading;
using NUnit.Framework;
using System.Threading.Tasks;

namespace alttrashcat_tests_csharp.tests
{
    public class MainMenuTests
    {
        AltDriver altDriver;
        MainMenuPage mainMenuPage;
        StorePage storePage;
        GamePlay gamePlay;
        SettingsPage settingsPage;
        GetAnotherChancePage getAnotherChancePage;

        [SetUp]
        public void Setup()
        {
            altDriver = new AltDriver(port: 13000);
            mainMenuPage = new MainMenuPage(altDriver);
            gamePlay= new GamePlay(altDriver);
            mainMenuPage.LoadScene();
            settingsPage = new SettingsPage(altDriver);
            storePage = new StorePage(altDriver);
            getAnotherChancePage = new GetAnotherChancePage(altDriver);
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
            mainMenuPage.PressSettings();
            settingsPage.PressDeleteData();
            settingsPage.PressYesDeleteData();
            settingsPage.PressClosePopUp();
            mainMenuPage.PressStore();
            Assert.True(storePage.CountersReset());
        }

        [Test]

        public void TestMagnetIsUsedInGameplay()
        {
            mainMenuPage.LoadScene();
            mainMenuPage.PressStore();
            bool buttonState = storePage.BuyButtonsAreEnabled();
            if(buttonState == true)
                storePage.BuyMagnet();
            else
                {storePage.PressStore();
                 storePage.PressCharactersTab();
                 storePage.ReloadItems();
                 storePage.BuyMagnet();
                }

            storePage.CloseStore();

            mainMenuPage.MovePowerUpLeft();
            mainMenuPage.PressRun();
            //Assert.IsTrue(gamePlay.InventoryItemIsDisplayed());

            gamePlay.SelectInventoryIcon();
            Assert.IsTrue(gamePlay.PowerUpIconIsDisplayed());
        }
         [Test]
         public void TestThatLifePowerUpAddsALife()
         {
            mainMenuPage.LoadScene();
            mainMenuPage.PressStore();
            bool buttonState = storePage.BuyButtonsAreEnabled();
            if(buttonState == true)
                storePage.BuyLife();
            else
                {storePage.PressStore();
                 storePage.PressCharactersTab();
                 storePage.ReloadItems();
                 storePage.BuyLife();
                }
            storePage.CloseStore();
            mainMenuPage.MovePowerUpLeft();
            mainMenuPage.PressRun();

            while (gamePlay.GetCurrentLife() > 1)
                {Thread.Sleep(5);}
            Console.WriteLine (" I got out of the while loop");   
            gamePlay.SelectInventoryIcon();       
            Assert.AreEqual(gamePlay.GetCurrentLife(), 2);
 
         }

         [Test]

         public void TestNightTimeThemeisApplied()
         {
            mainMenuPage.LoadScene();
            mainMenuPage.PressStore();
            bool buttonState = storePage.BuyButtonsAreEnabled();
            if(buttonState == true)
                {
                storePage.OpenThemes();
                storePage.BuyNightTheme();
                }
            else
                {storePage.PressStore();
                storePage.OpenThemes();
                storePage.BuyNightTheme();
                }
         
            storePage.CloseStore();
            Thread.Sleep(100);
            mainMenuPage.ChangeTheme();
            Thread.Sleep(100);
            mainMenuPage.PressRun();
            Assert.IsTrue(mainMenuPage.NightLightsAreDisplayed());

         }

        

        [TestCase("MasterSlider")]
        [TestCase("MusicSlider")]
        [TestCase("MasterSFXSlider")]

         public void SliderValuesChangeAsExpected(string sliderName)
         {
            //SliderName can be one of the three values : Master, Music,SFX
            mainMenuPage.LoadScene();
            mainMenuPage.PressSettings();
            settingsPage.MoveSliderToStart(sliderName);

           float initialSliderValue= settingsPage.GetSliderValue(sliderName);
            //slide handle
            settingsPage.MoveSlider(sliderName);

            float finalSliderValue = settingsPage.GetSliderValue(sliderName);


            Console.WriteLine ("intial value is " + initialSliderValue);
           Assert.AreNotSame(initialSliderValue, finalSliderValue);

        }




    }
}