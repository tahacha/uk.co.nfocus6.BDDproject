using OpenQA.Selenium;
using uk.co.nfocus6.BDDproject.Utils;

namespace uk.co.nfocus6.BDDproject.POM
{
    internal class HomePOM
    {
        private IWebDriver _driver;
        

        
        public HomePOM(IWebDriver driver)
        {
            this._driver = driver;
            HelperLib.WaitForElement(_driver, By.LinkText("Dismiss")); //wait
            Console.WriteLine("Viewing home page");
        }
        //locators
        private IWebElement _dismissText => HelperLib.WaitForElement(_driver, By.LinkText("Dismiss")); //find dismiss text - banner
        public void DismissBanner()
        {
            _dismissText.Click();
        }
    }
}
