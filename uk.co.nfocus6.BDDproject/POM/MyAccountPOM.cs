using OpenQA.Selenium;
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
            string headingText = driver.FindElement(By.TagName("h1")).Text;
            //HelperLib.StaticWaitForElement(_driver, By.TagName("h1")); //waits for it to appear

            
            //Assert.That(headingText, Does.Contain("My account"), "Not viewing My account page"); //checks to see if the current page is My account
            Console.WriteLine("Viewing My account page");
        }

        //locators

        private IWebElement _usernameField => HelperLib.WaitForElement(_driver, By.CssSelector("#username"));
        //private IWebElement _usernameField => _driver.FindElement(By.CssSelector("#username")); //locates username field
        private IWebElement _passwordField => HelperLib.WaitForElement(_driver, By.CssSelector("#password"));
        private IWebElement _loginButton => HelperLib.WaitForElement(_driver, By.Name("login")); //locates log in button

        private IWebElement _orderLink => HelperLib.WaitForElement(_driver, By.LinkText("Orders")); //loactes orders link 

        private IWebElement _logoutLink => HelperLib.WaitForElement(_driver, By.LinkText("Log out"));

        private IWebElement _myAccountBody => HelperLib.WaitForElement(_driver, By.TagName("div"));
        private IWebElement _errorAlert => HelperLib.WaitForElement(_driver, By.CssSelector(".woodcommerce-error::before"));
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

        public string SuccessfuLogin()
        {
           
            try
            {
                //HelperLib.StaticWaitForElement(_driver, By.LinkText("Log out"));
                if(_logoutLink.Displayed)
                {
                    return "login successful";
                }

            }
            catch (Exception e)
            {

                //do nothing 
            }

            return "login unsuccessful";
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
