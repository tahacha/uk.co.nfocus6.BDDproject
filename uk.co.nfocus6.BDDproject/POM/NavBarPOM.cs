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
            Console.WriteLine("Nav Bar - Shop clicked");
        }

        public void ViewMyAccount()
        {
            try
            {
                _accountLink.Click();
                Console.WriteLine("Nav Bar - My Account clicked");
            }
            catch (Exception) //click intercepted 
            {
                Console.WriteLine("Nav Bar - My Account clicked failed, trying again");
                _accountLink.Click();
                Console.WriteLine("Nav Bar - My Account clicked");
            }
            
        }

        public void ViewCart()
        {
            
            try
            {
                _cartLink.Click();
                Console.WriteLine("Nav Bar - Cart clicked");
            }
            catch (Exception)
            {
                Console.WriteLine("Nav Bar - Cart clicked failed, trying again");
                _cartLink.Click();
                Console.WriteLine("Nav Bar - Cart clicked");
            }
        }
    }
}
