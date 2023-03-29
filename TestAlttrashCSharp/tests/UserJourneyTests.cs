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

        }

        [Test]

        public void UserJourneyPlayandPause()
        {
        Assert.Multiple(() =>
        {
           //User opens the game
            mainMenuPage.LoadScene();
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


        //User opens the game 
        //Delete all data 
        //Run to avoid obstacles 
        //Pause 
        // Continue running until you die

        //User opens the game
        //Add money in account 
        //Buy all the available options once 
        //Go to main menu and customize the play 
        //Run until you die 

        //user opens the game
        //Run until you die then get another chance 
          //Run until you die again 
    
        [Test]
        public void UserJourneyBuyItems()
        {
            Assert.Multiple(() =>
        {
           mainMenuPage.LoadScene();
           mainMenuPage.PressSettings();
           settingsPage.PressDeleteData();
           settingsPage.PressYesDeleteData();
           settingsPage.PressClosePopUp();
           mainMenuPage.PressStore();
           Assert.IsFalse(storePage.BuyButtonsAreEnabled());
           storePage.PressStore(); 
           Thread.Sleep(1000);
           storePage.PressCharactersTab();
           storePage.ReloadItems();
           Thread.Sleep(1000); 
           Assert.IsTrue(storePage.BuyButtonsAreEnabled());
           storePage.BuyMagnet();
           storePage.OpenThemes();
            storePage.BuyNightTheme();
            storePage.CloseStore();
            mainMenuPage.MovePowerUpLeft();
            mainMenuPage.ChangeTheme();
            Thread.Sleep(100);

            mainMenuPage.PressRun();
            Assert.IsTrue(gamePlay.InventoryItemIsDisplayed());

            Assert.IsTrue(mainMenuPage.NightLightsAreDisplayed());
            gamePlay.SelectInventoryIcon();
            Assert.IsTrue(gamePlay.PowerUpIconIsDisplayed());

         });

        }
    }
}