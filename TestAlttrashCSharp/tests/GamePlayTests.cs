using System;
using System.Threading;
using Altom.AltDriver;
using alttrashcat_tests_csharp.pages;
using NUnit.Framework;
using Allure;
using NUnit.Allure.Core;
using NUnit.Allure.Attributes;
using Allure.Commons;

namespace alttrashcat_tests_csharp.tests
{

    [TestFixture]
    [AllureNUnit]
    [AllureSuite("Gameplay")]


    public class GamePlayTests
    {
        AltDriver altDriver;
        MainMenuPage mainMenuPage;
        GamePlay gamePlayPage;
        PauseOverlayPage pauseOverlayPage;
        GetAnotherChancePage getAnotherChancePage;
        GameOverScreen gameOverScreen;
        SettingsPage settingsPage;


        [SetUp]
        public void Setup()
        {

            altDriver = new AltDriver();
            mainMenuPage = new MainMenuPage(altDriver);
            mainMenuPage.LoadScene();
            mainMenuPage.PressRun();
            gamePlayPage = new GamePlay(altDriver);
            pauseOverlayPage = new PauseOverlayPage(altDriver);
            getAnotherChancePage = new GetAnotherChancePage(altDriver);
            gameOverScreen = new GameOverScreen(altDriver);
            settingsPage = new SettingsPage(altDriver);

        }
        [Test]
      
        [AllureSeverity(SeverityLevel.critical)]
        [AllureIssue("ABC-1")]
        [AllureOwner("AleV")]
        [AllureDescription("Test to see if the gameplay is started by verifyig specific elements")]
       
        public void TestGamePlayDisplayedCorrectly()
        {
            Assert.True(gamePlayPage.IsDisplayed());
        }

        [Test]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureIssue("ABC-2")]
        [AllureOwner("AleV")]
        [AllureDescription("Test to see if the gameplay can be paused and resumes correctly afterwards")]
        public void TestGameCanBePausedAndResumed()
        {
            gamePlayPage.PressPause();
            Assert.True(pauseOverlayPage.IsDisplayed());
            pauseOverlayPage.PressResume();
            Assert.True(gamePlayPage.IsDisplayed());
        }
        [Test]
         [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("AleV")]
        [AllureDescription("Test to see if the gameplay can be paused and then stopped")]
        public void TestGameCanBePausedAndStopped()
        {
            gamePlayPage.PressPause();
            pauseOverlayPage.PressMainMenu();
            Assert.True(mainMenuPage.IsDisplayed());
        }

        [Test]
         [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("AleV")]
        [AllureDescription("The player can avoid a certain number of obstacles without dying")]
        public void TestAvoidingObstacles()
        {
            gamePlayPage.AvoidObstacles(5);
            Assert.True(gamePlayPage.GetCurrentLife() > 0);
        }
        [Test]
         [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("AleV")]
        [AllureDescription("The player dies if it collides into obstacles")]
        public void TestPlayerDiesWhenObstacleNotAvoided()
        {
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
        }

        [Test]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("AleV")]
        [AllureDescription("Game over screen is displayed after the player dies")]
        public void TestGameOverScreenIsAceesible()
        {
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
            getAnotherChancePage.PressGameOver();
            Assert.True(gameOverScreen.IsDisplayed());
        
        }

        [Test]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureOwner("AleV")]
        [AllureDescription("The player can not select to get another chance when there aren`t enough premim coins")]
         
         public void TestGetAnotherChangeDisabledWhenNotEnoughCoins()
         {
            gamePlayPage.PressPause();
            pauseOverlayPage.PressMainMenu();
            //delete all data 
            settingsPage.DeleteData();
            mainMenuPage.PressRun();

            //play game until get another chance is displayed
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

            Assert.IsFalse(getAnotherChancePage.GetAnotherChangeObjectState);
         }



        [TearDown]
        public void Dispose()
        {
            altDriver.Stop();
            Thread.Sleep(1000);
        }
    }
}