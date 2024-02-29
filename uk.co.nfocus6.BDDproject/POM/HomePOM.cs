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
    internal class HomePOM
    {
        private IWebDriver _driver;
        private IWebElement _dismissText => HelperLib.WaitForElement(_driver, By.LinkText("Dismiss")); //find dismiss text - banner
        public HomePOM(IWebDriver driver)
        {
            this._driver = driver;
            string headingText = _driver.FindElement(By.TagName("h1")).Text;
            HelperLib.StaticWaitForElement(_driver, By.LinkText("Edgewords Shop")); //waits for page to load
            Assert.That(headingText, Does.Contain("Welcome")); //checks to see if it's the home page
            Console.WriteLine("Viewing home page");
        }

        public void DismissBanner()
        {
            _dismissText.Click();
        }
    }
}
