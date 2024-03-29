﻿using OpenQA.Selenium;
using uk.co.nfocus6.BDDproject.Utils;

namespace uk.co.nfocus6.BDDproject.POM
{
    
    internal class MyAccountPOM
    {
        private IWebDriver _driver;
        public MyAccountPOM(IWebDriver driver) //constructor that takes in a driver from the test case
        {
            this._driver = driver;
            HelperLib.WaitForElement(_driver, By.XPath("//h1[contains(.,'My account')]"));
            Console.WriteLine("Viewing My Account Page");
        }

        //locators
        private IWebElement _usernameField => HelperLib.WaitForElement(_driver, By.CssSelector("#username"));
        private IWebElement _passwordField => HelperLib.WaitForElement(_driver, By.CssSelector("#password"));
        private IWebElement _loginButton => HelperLib.WaitForElement(_driver, By.Name("login")); //locates log in button

        private IWebElement _orderLink => HelperLib.WaitForElement(_driver, By.LinkText("Orders")); //loactes orders link 

        private IWebElement _logoutLink => HelperLib.WaitForElement(_driver, By.LinkText("Log out"));
        
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
        }

        public bool IsLoggedIn()
        {
           
            try
            {
                if(_logoutLink.Displayed)
                {
                    return true; //if logged in 
                }

            }
            catch (Exception)
            {
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
