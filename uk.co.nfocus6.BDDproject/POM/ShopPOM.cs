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
            HelperLib.WaitForElement(_driver, By.Name("orderby"));
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
            _itemName = item; //places the item that user wants to add to cart into _itemName
            try
            {
                _theItem.Click();
                return true;
            }
            catch (Exception)
            {

            }
            return false; //item doesn't exist 
        }

        public void ViewCart()
        {
            _viewCart.Click();
        }

    }
}
