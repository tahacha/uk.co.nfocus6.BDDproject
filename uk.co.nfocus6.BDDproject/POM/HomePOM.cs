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
        

        
        public HomePOM(IWebDriver driver)
        {
            this._driver = driver;
            _body.Click(); //wait
            string headingText = _headingText.Text;
            Assert.That(headingText, Does.Contain("Welcome")); //checks to see if it's the home page
            Console.WriteLine("Viewing home page");
        }
        //locators
        private IWebElement _headingText => HelperLib.WaitForElement(_driver, By.TagName("h1"));
        private IWebElement _body => HelperLib.WaitForElement(_driver, By.TagName("body"));
        private IWebElement _dismissText => HelperLib.WaitForElement(_driver, By.LinkText("Dismiss")); //find dismiss text - banner
        public void DismissBanner()
        {
            _dismissText.Click();
        }
    }
}
