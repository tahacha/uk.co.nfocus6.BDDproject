using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V120.Media;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.co.nfocus6.BDDproject.Utils;

namespace uk.co.nfocus6.BDDproject.POM
{
    
    internal class MyAccountPOM
    {
        private IWebDriver _driver;
        public MyAccountPOM(IWebDriver driver) //constructor that takes in a driver from the test case
        {
            this._driver = driver;
            HelperLib.WaitForElement(_driver, By.XPath("//h1[contains(.,'My account')]");
            Console.WriteLine("Viewing My Account Page");
        }

        //locators
        private IWebElement _usernameField => HelperLib.WaitForElement(_driver, By.CssSelector("#username"));
        private IWebElement _passwordField => HelperLib.WaitForElement(_driver, By.CssSelector("#password"));
        private IWebElement _loginButton => HelperLib.WaitForElement(_driver, By.Name("login")); //locates log in button

        private IWebElement _orderLink => HelperLib.WaitForElement(_driver, By.LinkText("Orders")); //loactes orders link 

        private IWebElement _logoutLink => HelperLib.WaitForElement(_driver, By.LinkText("Log out"));
        private IWebElement _header => HelperLib.WaitForElement(_driver, By.XPath("//h1[contains(.,'My account')]"));
        
        //service methods

        public void SetUsername(string username)
        {
            _usernameField.Clear(); //clears any text in the username field
            _usernameField.SendKeys(username);
        }

        
        public void SetPassword(string password)
        {
            _passwordField.Clear(); //clears any text in the password field
            _passwordField.SendKeys(password);
        }

        public void ClickLogin()
        {
            _loginButton.Click(); //clicks log in button
            //SuccessfulLogin();
        }

        public bool IsLoggedIn()
        {
           
            try
            {
                if(_logoutLink.Displayed)
                {
                    HelperLib.StaticTakeScreenshot(_driver, _header, "loggedIn_status");
                    return true; //if logged in 
                }

            }
            catch (Exception)
            {
                HelperLib.StaticTakeScreenshot(_driver, _header, "loggedIn_status");
                //do nothing 
            }

            return false;
        }

        public void ClickOrders()
        {
            _orderLink.Click();
        }

        public void ClickLogout()
        {
            _logoutLink.Click();
        }

        
    }
}
