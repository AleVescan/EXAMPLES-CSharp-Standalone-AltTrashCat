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
using System.Drawing;

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

         public void TestPremiumButtonColorChangesAsExpectedPerState()
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
            var initialButtonColorR = getAnotherChancePage.GetPremiumButtonCurrentColorRGB("r");
            var initialButtonColorG = getAnotherChancePage.GetPremiumButtonCurrentColorRGB("g");
            var initialButtonColorB = getAnotherChancePage.GetPremiumButtonCurrentColorRGB("b");

            var normalColorR = getAnotherChancePage.GetPremiumButtonStateColorRGB("normalColor", "r");
            var normalColorG = getAnotherChancePage.GetPremiumButtonStateColorRGB("normalColor", "g");
            var normalColorB = getAnotherChancePage.GetPremiumButtonStateColorRGB("normalColor", "b");

            Assert.AreEqual(initialButtonColorR,normalColorR ); 
            Assert.AreEqual(initialButtonColorG,normalColorG ); 
            Assert.AreEqual(initialButtonColorB,normalColorB ); 


            Console.WriteLine("Intial button state code: " + initialState);
            Console.WriteLine("Intial button color RGB : " + initialButtonColorR+ "  "+ initialButtonColorG + "  "+ initialButtonColorB );
            Console.WriteLine("Normal color RGB : " + normalColorR+ "  "+ normalColorG + "  "+ normalColorB );


            getAnotherChancePage.PremiumButton.PointerDownFromObject();
            Thread.Sleep(1000);
            object afterPointerDownButtonColorR = getAnotherChancePage.GetPremiumButtonCurrentColorRGB("r"); 
            object afterPointerDownButtonColorG = getAnotherChancePage.GetPremiumButtonCurrentColorRGB("g"); 
            object afterPointerDownButtonColorB = getAnotherChancePage.GetPremiumButtonCurrentColorRGB("b"); 

            var pressedColorR = getAnotherChancePage.GetPremiumButtonStateColorRGB("pressedColor", "r");
            var pressedColorG = getAnotherChancePage.GetPremiumButtonStateColorRGB("pressedColor", "g");
            var pressedColorB = getAnotherChancePage.GetPremiumButtonStateColorRGB("pressedColor", "b"); 
            var afterPointerDown = getAnotherChancePage.GetPremiumButtonState();

            Assert.AreEqual(afterPointerDownButtonColorR ,pressedColorR ); 
            Assert.AreEqual(afterPointerDownButtonColorG ,pressedColorG ); 
            Assert.AreEqual(afterPointerDownButtonColorB ,pressedColorB );

            Console.WriteLine("Button state after pointer down " + afterPointerDown);
            Console.WriteLine("Intial button colorRGB : " + afterPointerDownButtonColorR+ "  "+ afterPointerDownButtonColorG + "  "+ afterPointerDownButtonColorB );
            Console.WriteLine("Pressed color RGB : " + pressedColorR+ "  "+ pressedColorG + "  "+ pressedColorB );
            
            getAnotherChancePage.PremiumButton.PointerUpFromObject();
            Thread.Sleep(1000);
            var afterPointerUpButtonColorR = getAnotherChancePage.GetPremiumButtonCurrentColorRGB("r");
            var afterPointerUpButtonColorG = getAnotherChancePage.GetPremiumButtonCurrentColorRGB("g");
            var afterPointerUpButtonColorB = getAnotherChancePage.GetPremiumButtonCurrentColorRGB("b");

            var selectedColorR = getAnotherChancePage.GetPremiumButtonStateColorRGB("selectedColor", "r");
            var selectedColorG = getAnotherChancePage.GetPremiumButtonStateColorRGB("selectedColor", "g");
            var selectedColorB = getAnotherChancePage.GetPremiumButtonStateColorRGB("selectedColor", "b");
            var afterPointerUp = getAnotherChancePage.GetPremiumButtonState();

            Assert.AreEqual(afterPointerUpButtonColorR ,selectedColorR ); 
            Assert.AreEqual(afterPointerUpButtonColorG ,selectedColorG ); 
            Assert.AreEqual(afterPointerUpButtonColorB ,selectedColorB ); 
            
            Console.WriteLine("Button color after pointer up RGB " + afterPointerUpButtonColorR+ "  "+ afterPointerUpButtonColorG + "  "+ afterPointerUpButtonColorB);
            Console.WriteLine("Selected color RGB : " + selectedColorR+ "  "+ selectedColorG + "  "+ selectedColorB );
            Console.WriteLine("Button state after pointer up " + afterPointerUp);



         }

         [Test]

         public void TestDistanceModifieddWithinGame()
         {
            gamePlayPage.SetCharacterInvincible("True");
            Thread.Sleep(20000);
            // var RunnerMultiplier= altDriver.WaitForObject(By.PATH, "/UICamera/Game/WholeUI/ScoreZone/ScoreLabel/ScoreText/MultiplierText");
            // var InitialRunnerMultiplier=RunnerMultiplier.GetText();
            // RunnerMultiplier.SetText("x 5");
            // var SetRunnerMultiplierText = altDriver.WaitForObject(By.PATH, "/UICamera/Game/WholeUI/ScoreZone/ScoreLabel/ScoreText/MultiplierText").GetText();
            // Assert.AreEqual("x 5", SetRunnerMultiplierText);

            var Distance = altDriver.WaitForObject(By.PATH, "/UICamera/Game/WholeUI/DistanceZone/DistanceText");
            var intialDistance = Distance.GetText();
            var finalDistance=  Distance.SetText("3000m");
             Assert.AreEqual(Distance.GetText(), "3000m");
    
        // //     gamePlayPage.SetRunnerMultiplier();

        //     Assert.AreEqual(RunnerMultiplier.GetText(), "x 5");
        //    // Assert.AreNotEqual(initialRunnerMultiplier, finalRunnerMultiplier);

            //   gamePlayPage.SetRunnerMultiplier();
            // Thread.Sleep(20000);


         }



        [TearDown]
        public void Dispose()
        {
            altDriver.Stop();
            Thread.Sleep(1000);
        }
    }
}