using System;
using System.Threading;
using Altom.AltDriver;
using alttrashcat_tests_csharp.pages;
using NUnit.Framework;
using Allure;
using NUnit.Allure.Core;
using NUnit.Allure.Attributes;
using Allure.Commons;
using Newtonsoft.Json;

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

         [Test]
         public void TestThatTrashCatBecomesInvincible()
         {
           
            gamePlayPage.SetCharacterInvincible("True");
            Thread.Sleep(20000);
            altDriver.WaitForObjectNotBePresent(By.NAME, "GameOver"); 
            //if this fails, at timeout of 20, it means that the object is displayed, thus exit with a timeout

             gamePlayPage.SetCharacterInvincible("False");
             Thread.Sleep(10000);
             Assert.True(getAnotherChancePage.IsDisplayed());
            
         }

         [Test]

         public void TestPremiumButtonColor()
         {
            mainMenuPage.LoadScene();
            mainMenuPage.PressRun();
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
   
            var initialState = getAnotherChancePage.GetPremiumButtonState();
            Console.WriteLine("Intial button state code: " + initialState);


            getAnotherChancePage.PremiumButton.PointerDownFromObject();
            Thread.Sleep(1000);
            var afterPointerDown = getAnotherChancePage.GetPremiumButtonState();
            Console.WriteLine("Button state after pointer down " + afterPointerDown);

    

            getAnotherChancePage.PremiumButton.PointerUpFromObject();
            Thread.Sleep(1000);
            var afterPointerUp = getAnotherChancePage.GetPremiumButtonState();
            Console.WriteLine("Button state after pointer up " + afterPointerUp);

            getAnotherChancePage.PremiumButton.PointerEnterObject();
            Thread.Sleep(1000);
            var afterPointerEnter = getAnotherChancePage.GetPremiumButtonState();
            Console.WriteLine("Button state after pointer enter " + afterPointerEnter);

            getAnotherChancePage.PremiumButton.PointerExitObject();
            Thread.Sleep(1000);
            var afterPointerExit = getAnotherChancePage.GetPremiumButtonState();
            Console.WriteLine("Button state after pointer exit " + afterPointerExit);


         }

        //  [Test]

        //  public void TestDistanceModifieddWithinGame()
        //  {
        //     gamePlayPage.SetCharacterInvincible("True");
        //     Thread.Sleep(20000);
        //     // var RunnerMultiplier= altDriver.WaitForObject(By.PATH, "/UICamera/Game/WholeUI/ScoreZone/ScoreLabel/ScoreText/MultiplierText");
        //     // var InitialRunnerMultiplier=RunnerMultiplier.GetText();
        //     // RunnerMultiplier.SetText("x 5");
        //     // var SetRunnerMultiplierText = altDriver.WaitForObject(By.PATH, "/UICamera/Game/WholeUI/ScoreZone/ScoreLabel/ScoreText/MultiplierText").GetText();
        //     // Assert.AreEqual("x 5", SetRunnerMultiplierText);

        //     var Distance = altDriver.WaitForObject(By.PATH, "/UICamera/Game/WholeUI/DistanceZone/DistanceText");
        //     var intialDistance = Distance.GetText();
        //     var finalDistance=  Distance.SetText("3000m");
        //      Assert.AreEqual(Distance.GetText(), "3000m");
    
        // // //     gamePlayPage.SetRunnerMultiplier();

        // //     Assert.AreEqual(RunnerMultiplier.GetText(), "x 5");
        // //    // Assert.AreNotEqual(initialRunnerMultiplier, finalRunnerMultiplier);

        //     //   gamePlayPage.SetRunnerMultiplier();
        //     // Thread.Sleep(20000);


        //  }



        [TearDown]
        public void Dispose()
        {
            altDriver.Stop();
            Thread.Sleep(1000);
        }
    }
}