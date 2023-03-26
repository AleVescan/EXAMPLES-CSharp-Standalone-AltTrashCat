using Altom.AltDriver;

namespace alttrashcat_tests_csharp.pages
{
    public class SettingsPage : BasePage
    {
        public SettingsPage(AltDriver driver) : base(driver)
        {
        }
        public void LoadScene()
        {
            Driver.LoadScene("Main");
        }

    public AltObject SettingsButton { get => Driver.WaitForObject(By.PATH, "/UICamera/Loadout/SettingButton", timeout: 10); }
    public AltObject SettingsPopUp { get => Driver.WaitForObject(By.PATH, "/UICamera/Loadout/SettingPopup/Background", timeout: 10 );}

    public AltObject DeleteDataButton { get => Driver.WaitForObject(By.PATH, "/UICamera/Loadout/SettingPopup/Background/DeleteData", timeout: 10 );  }
    public AltObject ConfirmationPopUp { get=> Driver.WaitForObject(By.PATH, "/UICamera/Loadout/SettingPopup/ConfirmPopup/Image", timeout: 10);}

    public AltObject ConfirmYesButton { get => Driver.WaitForObject(By.PATH, "/UICamera/Loadout/SettingPopup/ConfirmPopup/Image/YESButton", timeout: 10);}
    public AltObject ClosePopUpButton { get => Driver.WaitForObject(By.PATH, "/UICamera/Loadout/SettingPopup/Background/CloseButton");}
    public void PressSettings ()
    {
        SettingsButton.Tap();
    }
        
     public bool PopUpisDisplayed()
    {
            if (SettingsPopUp != null && DeleteDataButton != null)
                return true;
            return false;
    }

    public void PressDeleteData()
    {
        DeleteDataButton.Tap();
    }

    public bool ConfirmationPopUpisDisplayed()
    {
        if (ConfirmationPopUp != null && ConfirmYesButton != null)
                return true;
            return false;
    }

    public void PressYesDeleteData()
    {
        ConfirmYesButton.Tap();
    }

    public void PressClosePopUp()
    {
        ClosePopUpButton.Tap();
    }

    public void DeleteData()
    {
        SettingsButton.Tap();
        DeleteDataButton.Tap();
        ConfirmYesButton.Tap();
        ClosePopUpButton.Tap();
    }

    }}