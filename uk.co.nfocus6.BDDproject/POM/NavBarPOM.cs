using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.co.nfocus6.BDDproject.Utils;

namespace uk.co.nfocus6.BDDproject.POM
{
    internal class NavBarPOM
    {
        private IWebDriver _driver;

        public NavBarPOM(IWebDriver driver)
        {
            this._driver = driver;
        }

        //locators
        private IWebElement _shopLink => HelperLib.WaitForElement(_driver, By.LinkText("Shop")); //locates shop link in nav bar
        private IWebElement _accountLink => HelperLib.WaitForElement(_driver, By.LinkText("My account"));

        private IWebElement _cartLink => HelperLib.WaitForElement(_driver, By.LinkText("Cart"));

        //service methods
        public void ViewShop()
        {
            _shopLink.Click(); //navigates to shop page
        }

        public void ViewMyAccount()
        {
            try
            {
                _accountLink.Click(); //navigates to my account page

            }
            catch (Exception)
            {
                _accountLink.Click(); //navigates to my account page, avoid click interception
            }
            
        }

        public void ViewCart()
        {
            try
            {
                _cartLink.Click(); //navigates to cart page 
            }
            catch (Exception)
            {

                _cartLink.Click(); //navigates to cart page, avoid click interception
            }
        }
    }
}
