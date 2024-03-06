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
    internal class ShopPOM
    {
        private IWebDriver _driver;
        public ShopPOM(IWebDriver driver)
        {
            this._driver = driver;
            _orderBy.Click(); //wait
            string headingText = _headingText.Text;
            //HelperLib.StaticWaitForElement(_driver, By.Name("orderby")); //waits for dropdown selection
            Assert.That(headingText, Does.Contain("Shop")); //checks that user is on shop page
            Console.WriteLine("Viewing shop page");
        }
        private string _itemName = "";

        //locators
        private IWebElement _headingText => HelperLib.WaitForElement(_driver, By.TagName("h1"));
        private IWebElement _scroll => HelperLib.WaitForElement(_driver, By.TagName("body"));

        private IWebElement _theItem => HelperLib.WaitForElement(_driver, By.CssSelector("a[aria-label=\"Add “" + _itemName + "” to your cart\"]"));

        private IWebElement _viewCart => HelperLib.WaitForElement(_driver, By.LinkText("View cart"));

        private IWebElement _orderBy => HelperLib.WaitForElement(_driver, By.Name("orderby"));
        public bool AddItemToCart(string item)
        {
            _itemName = item; //places the item that user wants into _itemName
            try
            {
                _theItem.Click();
                return true;
            }
            catch (Exception)
            {

            }
            return false;

        }

        public void ViewCart()
        {
            HelperLib.StaticTakeScreenshot(_driver, _viewCart, "item_added_to_cart", false);
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
