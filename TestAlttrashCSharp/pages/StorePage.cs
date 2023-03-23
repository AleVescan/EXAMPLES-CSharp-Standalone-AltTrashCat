using Altom.AltDriver;
using System;
using System.Collections;
using System.Collections.Generic;

namespace alttrashcat_tests_csharp.pages
{
    public class StorePage : BasePage
    {
        public StorePage(AltDriver driver) : base(driver)
        {
        }
        public void LoadScene()
        {
            Driver.LoadScene("Shop");
        }


        public AltObject StoreTitle { get => Driver.WaitForObject(By.PATH,"/Canvas/Background/StoreTitle"); }
        public AltObject CloseButton { get => Driver.WaitForObject(By.PATH,"/Canvas/Background/Button");}
        public AltObject ItemsTab { get => Driver.WaitForObject(By.PATH,"/Canvas/Background/TabsSwitch/Item");}
        public AltObject CharactersTab { get => Driver.WaitForObject(By.PATH,"/Canvas/Background/TabsSwitch/Character");}
        public AltObject AccessoriesTab { get => Driver.WaitForObject(By.PATH,"/Canvas/Background/TabsSwitch/Accesories");}
        public AltObject ThemesTab { get => Driver.WaitForObject(By.PATH,"/Canvas/Background/TabsSwitch/Themes");}
        public AltObject BuyButton { get => Driver.WaitForObject(By.PATH,"/Canvas/Background/ItemsList/Container/ItemEntry(Clone)/NamePriceButtonZone/PriceButtonZone/BuyButton");}
        public AltObject PremiumPlusButton { get => Driver.WaitForObject(By.PATH,"/Canvas/Background/Premium/Button");}
        public AltObject CoinImage { get => Driver.WaitForObject(By.PATH,"/Canvas/Background/Coin/Image");}
        public AltObject PremiumCoinImage { get => Driver.WaitForObject(By.PATH,"/Canvas/Background/Premium/Image");}

        public string componentName = "UnityEngine.UI.Button"; 
        public string assemblyName = "UnityEngine.UI";
        public string propertyName = "interactable";

       // public List<> allBuyButtons = new List<>(Driver.FindObjectsWhichContain(By.NAME, "BuyButton"));
        public List<AltObject> allBuyButtons { get => Driver.FindObjectsWhichContain(By.NAME, "BuyButton");}

         public AltObject BuyMagnetButton {get => Driver.FindObjectsWhichContain(By.NAME, "BuyButton")[0] ;}
         public AltObject BuyMultiplierButton {get => Driver.FindObjectsWhichContain(By.NAME, "BuyButton")[1] ;}
         public AltObject BuyInvincibleButton {get => Driver.FindObjectsWhichContain(By.NAME, "BuyButton")[2] ;}
         public AltObject BuyLifeButton {get => Driver.FindObjectsWhichContain(By.NAME, "BuyButton")[3] ;}


        public bool BuyButtonsAreEnabled()
        {
        
            var BuyMagnetEnabled = BuyMagnetButton.GetComponentProperty<string>(componentName, propertyName, assemblyName);
            var BuyMultiplierEnabled = BuyMultiplierButton.GetComponentProperty<string>(componentName, propertyName, assemblyName);
            var BuyInvincibleEnabled = BuyInvincibleButton.GetComponentProperty<string>(componentName, propertyName, assemblyName);
            var BuyLifeEnabled = BuyLifeButton.GetComponentProperty<string>(componentName, propertyName, assemblyName);
           
        
            if (BuyMagnetEnabled == "true" && BuyInvincibleEnabled=="true" && BuyMultiplierEnabled=="true" && BuyLifeEnabled=="true")
                return true;
            else 
                return false; 

        }

        public void BuyMagnet()
        {
   
            BuyMagnetButton.Tap();
        }


         public bool StoreIsDisplayed()
        {
            if (StoreTitle != null && CloseButton != null && ItemsTab != null && CharactersTab != null && AccessoriesTab != null && ThemesTab != null && BuyButton != null && PremiumPlusButton !=null && CoinImage !=null && PremiumCoinImage !=null)
                return true;
            return false;   
        }


        public AltObject PremiumCounter {get => Driver.WaitForObject(By.PATH,"/Canvas/Background/Premium/PremiumCounter");}

        public string PremiumCoinsValue { get =>PremiumCounter.GetText(); }

        public AltObject CoinsCounter {get => Driver.WaitForObject(By.PATH,"/Canvas/Background/Coin/CoinsCounter");}

        public string CoinsCounterValue { get =>CoinsCounter.GetText(); }

        public bool CountersReset()
        {
            if (CoinsCounterValue == "0" && PremiumCoinsValue == "0")
                return true;
            return false;
        }

        public void PressStore()
        {
            StoreTitle.Tap();
        }

        public void ReloadItems()
        {
            ItemsTab.Tap();
        }

        public void PressCharactersTab()
        {
            CharactersTab.Tap();
        }

        public void CloseStore()
        {
            CloseButton.Tap();
        }

      
    }}