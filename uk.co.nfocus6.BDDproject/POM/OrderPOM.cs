﻿using NUnit.Framework;
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
            HelperLib.WaitForElement(_driver, By.LinkText("View")); //wait
            Console.WriteLine("Viewing Orders page");
        }
        //locators
        private IWebElement _headingText => HelperLib.WaitForElement(_driver, By.TagName("h1"));
        private IWebElement _orderTable => HelperLib.WaitForElement(_driver, By.TagName("tbody"));

        private IWebElement _latestOrder => HelperLib.WaitForElement(_driver, By.CssSelector("#post-7 > div > div > div > table > tbody > tr:nth-child(1)"));

        public string GetLatestOrder()
        {
            string latestOrderText = _latestOrder.Text;
            return latestOrderText;
        }
    }
}
