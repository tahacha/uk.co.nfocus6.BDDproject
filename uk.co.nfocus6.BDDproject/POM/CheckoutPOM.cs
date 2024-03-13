using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.co.nfocus6.BDDproject.Utils;

namespace uk.co.nfocus6.BDDproject.POM
{
    internal class CheckoutPOM
    {
        private IWebDriver _driver;
        public CheckoutPOM(IWebDriver driver)
        {
            this._driver = driver;
            _firstName.Click(); //wait
            string headingText = _headingText.Text;
            Assert.That(headingText, Does.Contain("Checkout"), "Not viewing checkout page"); //checks if user is on checkout page
            Console.WriteLine("Viewing checkout page"); 

        }
        //locators 
        private IWebElement _headingText => HelperLib.WaitForElement(_driver, By.TagName("h1"));
        private IWebElement _firstName => HelperLib.WaitForElement(_driver, By.Id("billing_first_name"));
        private IWebElement _lastName => HelperLib.WaitForElement(_driver, By.Id("billing_last_name"));
        private IWebElement _company => HelperLib.WaitForElement(_driver, By.Id("billing_company")); //optional field
        private IWebElement _streetAddress => HelperLib.WaitForElement(_driver, By.Id("billing_address_1"));
        private IWebElement _city => HelperLib.WaitForElement(_driver, By.Id("billing_city"));
        private IWebElement _county => HelperLib.WaitForElement(_driver, By.Id("billing_state")); //optional field
        private IWebElement _postcode => HelperLib.WaitForElement(_driver, By.Id("billing_postcode"));
        private IWebElement _phone => HelperLib.WaitForElement(_driver, By.Id("billing_phone"));

        private IWebElement _email => HelperLib.WaitForElement(_driver, By.Id("billing_email"));

        private IWebElement _orderNotes => HelperLib.WaitForElement(_driver, By.Name("order_comments")); //optional field
        private IWebElement _paymentByCheque => HelperLib.WaitForElement(_driver,By.CssSelector(".payment_method_cheque > label"));

        private IWebElement _placeOrder => HelperLib.WaitForElement(_driver, By.CssSelector("#place_order"));

        private IWebElement _orderNo => HelperLib.WaitForElement(_driver, By.CssSelector(".woocommerce-order-overview__order > strong")); //order no after summary

        private IWebElement _orderSummary => HelperLib.WaitForElement(_driver, By.CssSelector(".woocommerce-order-overview woocommerce-thankyou-order-details order_details")); //full summary


        //methods
        public void EnterFirstName(string firstName)
        {
            _firstName.Clear(); //clears field and enters name
            _firstName.SendKeys(firstName);
        }

        public void EnterLastName(string lastName)
        {
            _lastName.Clear();
            _lastName.SendKeys(lastName);
        }

        public void EnterCompany(string company)
        {
            _company.Clear();
            _company.SendKeys(company);
        }
        public void EnterStreetAddress(string streetAddress)
        {
            _streetAddress.Clear();
            _streetAddress.SendKeys(streetAddress);
        }

        public void EnterCity(string city)
        {
            _city.Clear();
            _city.SendKeys(city);
        }

        public void EnterCounty(string county)
        {
            _county.Clear();
            _county.SendKeys(county);    
        }
        public void EnterPostcode(string postcode)
        {
            _postcode.Clear();
            _postcode.SendKeys(postcode);
        }

        public void EnterPhone(string phone)
        {
            _phone.Clear();
            _phone.SendKeys(phone);
        }

        public void EnterEmail(string email)
        {
            _email.Clear();
            _email.SendKeys(email);
        }

        public void EnterOrderNotes(string notes)
        {
            _orderNotes.Clear();
            _orderNotes.SendKeys(notes);   
        }

        public void ClickChequePayment()
        {
            _paymentByCheque.Click();
            
        }

        public void ClickPlaceOrder()
        {
            _placeOrder.Click();
        }

        public string GetOrderNo()
        {
            HelperLib.StaticTakeScreenshot(_driver, _orderNo, "order_summary"); //screenshot of summary
            return _orderNo.Text;
        }
    }
}
