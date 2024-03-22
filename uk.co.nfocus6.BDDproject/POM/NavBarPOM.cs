using OpenQA.Selenium;
using uk.co.nfocus6.BDDproject.Utils;

namespace uk.co.nfocus6.BDDproject.POM
{
    internal class NavBarPOM
    {
        private IWebDriver _driver;

        public NavBarPOM(IWebDriver driver)
        {
            this._driver = driver;
            HelperLib.WaitForElement(_driver, By.LinkText("Blog"));
            HelperLib.ScrollUntilElement(_driver, _nameLink);
        }

        //locators
        private IWebElement _shopLink => HelperLib.WaitForElement(_driver, By.LinkText("Shop")); //locates shop link in nav bar
        private IWebElement _accountLink => HelperLib.WaitForElement(_driver, By.LinkText("My account"));

        private IWebElement _cartLink => HelperLib.WaitForElement(_driver, By.LinkText("Cart"));
        private IWebElement _nameLink => HelperLib.WaitForElement(_driver, By.LinkText("nFocus Shop"));

        //service methods
        public void ViewShop()
        {
            _shopLink.Click(); //navigates to shop page
        }

        public void ViewMyAccount()
        {
            //HelperLib.ScrollUntilElement(_driver, _accountLink);
            _accountLink.Click();
        }

        public void ViewCart()
        {
            //HelperLib.ScrollUntilElement(_driver, _cartLink);
            _cartLink.Click();
        }

        
    }
}
