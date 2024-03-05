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
    internal class OrderPOM
    {
        private IWebDriver _driver;
        public OrderPOM(IWebDriver driver)
        {
            this._driver = driver;
            string headingText = _headingText.Text;
            _orderTable.Click();
            Assert.That(headingText, Does.Contain("Orders"), "Not viewing orders page"); //checks user is on orders page
            Console.WriteLine("Viewing Orders page");
        }
        //locators
        private IWebElement _headingText => HelperLib.WaitForElement(_driver, By.TagName("h1"));
        private IWebElement _orderTable => HelperLib.WaitForElement(_driver, By.TagName("tbody"));

        //methods 
        public string GetOrderTable()
        {
            string orderTableText = _orderTable.Text; //text of the table
            HelperLib.StaticTakeScreenshot(_driver, _orderTable, "order_table");
            return orderTableText;
        }
    }
}
