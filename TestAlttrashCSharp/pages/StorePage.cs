using Altom.AltDriver;

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
        public AltObject ItemsTab { get => Driver.WaitForObject(By.PATH,"/Canvas/Background/TabsSwitch/Item/Text");}
        public AltObject CharactersTab { get => Driver.WaitForObject(By.PATH,"/Canvas/Background/TabsSwitch/Character/Text");}
        public AltObject AccessoriesTab { get => Driver.WaitForObject(By.PATH,"/Canvas/Background/TabsSwitch/Accesories/Text");}
        public AltObject ThemesTab { get => Driver.WaitForObject(By.PATH,"/Canvas/Background/TabsSwitch/Themes/Text");}
        public AltObject BuyButton { get => Driver.WaitForObject(By.PATH,"/Canvas/Background/ItemsList/Container/ItemEntry(Clone)/NamePriceButtonZone/PriceButtonZone/BuyButton/Text");}
        public AltObject PremiumPlusButton { get => Driver.WaitForObject(By.PATH,"/Canvas/Background/Premium/Button/Text");}
        public AltObject CoinImage { get => Driver.WaitForObject(By.PATH,"/Canvas/Background/Coin/Image");}
        public AltObject PremiumCoinImage { get => Driver.WaitForObject(By.PATH,"/Canvas/Background/Premium/Image");}

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
    }}