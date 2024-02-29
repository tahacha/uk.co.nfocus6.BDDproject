using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.co.nfocus6.BDDproject.Utils;

namespace uk.co.nfocus6.BDDproject.POM
{
    internal class ShopPagePOM
    {
        private IWebDriver _driver;
       public ShopPagePOM(IWebDriver driver) 
        {
            this._driver = driver;
            string headingText = _driver.FindElement(By.TagName("h1")).Text;
            HelperLib.StaticWaitForElement(_driver, By.TagName("h1")); //waits for text
            Assert.That(headingText, Does.Contain("Shop")); //checks that user is on shop page
            Console.WriteLine("Viewing shop page");
        }

        //locators
        private IWebElement _scroll => HelperLib.WaitForElement(_driver, By.TagName("body"));

        private IWebElement _addItemCart => HelperLib.WaitForElement(_driver, By.CssSelector(".post-28 > .button"));

        private IWebElement _viewCart => HelperLib.WaitForElement(_driver, By.LinkText("View cart"));

        public void AddItemToCart()
        {
            _addItemCart.Click();
        }

        public void ViewCart()
        {
            _viewCart.Click();
        }


        /* Works but causes problems with other methods
         * 
        public void ScrollShopPage()
        {
            _scroll.Click(); //click on main body
            _scroll.SendKeys(Keys.Down+Keys.Down+Keys.Down);
        }*/
        
    }
}
