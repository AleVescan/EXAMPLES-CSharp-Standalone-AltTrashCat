using System;
using System.Threading;
using Altom.AltDriver;
using alttrashcat_tests_csharp.pages;
using NUnit.Framework;

namespace alttrashcat_tests_csharp.tests
{
    public class UserJourneyTests
    {
        AltDriver altDriver;
        MainMenuPage mainMenuPage;
        GamePlay gamePlay;
        PauseOverlayPage pauseOverlayPage;
        GetAnotherChancePage getAnotherChancePage;
        GameOverScreen gameOverScreen;
        SettingsPage settingsPage;
        StartPage startPage;
        StorePage storePage;
   

        [SetUp]
        public void Setup()
        {

            altDriver = new AltDriver();
            mainMenuPage = new MainMenuPage(altDriver);
            gamePlay = new GamePlay(altDriver);
            pauseOverlayPage = new PauseOverlayPage(altDriver);
            getAnotherChancePage = new GetAnotherChancePage(altDriver);
            gameOverScreen = new GameOverScreen(altDriver);
            settingsPage = new SettingsPage(altDriver);
            startPage = new StartPage(altDriver);
            storePage = new StorePage(altDriver); 
            mainMenuPage.LoadScene();

        }

        [Test]

        public void UserJourneyPlayandPause()
        {
             Assert.Multiple(() =>
        {
           //User opens the game
           // mainMenuPage.LoadScene();
            mainMenuPage.PressRun();
            Assert.True(gamePlay.IsDisplayed());
            gamePlay.AvoidObstacles(5);
            Assert.True(gamePlay.GetCurrentLife() > 0);
            //user pauses the game 
            gamePlay.PressPause();
            Assert.True(pauseOverlayPage.IsDisplayed());
            pauseOverlayPage.PressResume();
            Assert.True(gamePlay.IsDisplayed());
            float timeout = 20;
            while (timeout > 0)
            {
                try
                {
                    getAnotherChancePage.IsDisplayed();
                    break;
                }
                catch (Exception)
                {
                    timeout -= 1;
                }
            }
            //user dies and game over screen is displayed
            getAnotherChancePage.PressGameOver();
            Assert.True(gameOverScreen.IsDisplayed());
            });
         }



    
        [Test]
        public void UserJourneyBuyItems()
        {
            Assert.Multiple(() =>
        {

            //delete current game data
          // mainMenuPage.LoadScene();
           mainMenuPage.PressSettings();
           settingsPage.PressDeleteData();
           settingsPage.PressYesDeleteData();
           settingsPage.PressClosePopUp();
           mainMenuPage.PressStore();
           // verify if buttons are disabled when no money
           Assert.IsFalse(storePage.BuyButtonsAreEnabled());
           storePage.PressStore(); 
           Thread.Sleep(1000);
           storePage.PressCharactersTab();
           storePage.ReloadItems();
           Thread.Sleep(1000); 
           //get coins by pressing Store and verify buttons get enabled 
           Assert.IsTrue(storePage.BuyButtonsAreEnabled());

           //buy magnet and night theme
           storePage.BuyMagnet();
           storePage.OpenThemes();
            storePage.BuyNightTheme();
            storePage.CloseStore();
            mainMenuPage.MovePowerUpLeft();
            mainMenuPage.ChangeTheme();
            Thread.Sleep(100);
            //verify bought items are available in game
            mainMenuPage.PressRun();
            Assert.IsTrue(gamePlay.InventoryItemIsDisplayed());

            Assert.IsTrue(mainMenuPage.NightLightsAreDisplayed());
            gamePlay.SelectInventoryIcon();
            Assert.IsTrue(gamePlay.PowerUpIconIsDisplayed());

         });
        }

         [Test]

         public void UserJourneyReviveAndGetASecondChance()
        {
            Assert.Multiple(() =>
            {
                mainMenuPage.PressSettings();
           settingsPage.PressDeleteData();
           settingsPage.PressYesDeleteData();
           settingsPage.PressClosePopUp();
           mainMenuPage.PressStore();
           // verify if buttons are disabled when no money
           Assert.IsFalse(storePage.BuyButtonsAreEnabled());
           storePage.PressStore(); 
           Thread.Sleep(1000);
           storePage.PressCharactersTab();
           storePage.ReloadItems();
           Thread.Sleep(1000); 
           //get coins by pressing Store and verify buttons get enabled 
           Assert.IsTrue(storePage.BuyButtonsAreEnabled());
           storePage.BuyLife();
           storePage.CloseStore();
            mainMenuPage.MovePowerUpLeft();
            mainMenuPage.PressRun();

            while (gamePlay.GetCurrentLife() > 1)
                {Thread.Sleep(5);}
             
            gamePlay.SelectInventoryIcon();       
            Assert.AreEqual(gamePlay.GetCurrentLife(), 2);
            float timeout = 20;
            while (timeout > 0)
            {
                try
                {
                    getAnotherChancePage.IsDisplayed();
                    break;
                }
                catch (Exception)
                {
                    timeout -= 1;
                }
            }
            Assert.True(getAnotherChancePage.IsDisplayed());
            getAnotherChancePage.PressPremiumButton();
            while (timeout > 0)
            {
                try
                {
                    getAnotherChancePage.IsDisplayed();
                    break;
                }
                catch (Exception)
                {
                    timeout -= 1;
                }
            }

            Assert.True(gameOverScreen.IsDisplayed());

            }
            );
        }
                [TearDown]
        public void Dispose()
        {
            altDriver.Stop();
            Thread.Sleep(1000);
        }
            
        }
    
}