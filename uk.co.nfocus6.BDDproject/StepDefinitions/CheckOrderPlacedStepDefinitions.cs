﻿using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Assist;
using uk.co.nfocus6.BDDproject.POM;
using uk.co.nfocus6.BDDproject.Utils;

namespace uk.co.nfocus6.BDDproject.StepDefinitions
{
    [Binding]
    public class CheckOrderPlacedStepDefinitions
    {
        private readonly ShopContainer _container;
        private IWebDriver _driver;

        public CheckOrderPlacedStepDefinitions(ShopContainer container)
        {
            _container = container;
            this._driver = _container.Driver;
        }

        [When(@"I proceed to checkout")]
        public void WhenIProceedToCheckout()
        {
            //proceed to checkout from cart
            CartPOM cartPage = new CartPOM(_driver);
            cartPage.ProceedToCheckout();
        }

        [When(@"Fill in my billing details with")]
        public void FillInMyBillingDetailsWith(Table table)
        {
            BillingPOCO customerDetails;
            //fill in details and press payment by cheque
            //checkout page
            CheckoutPOM checkout = new CheckoutPOM(_driver);

            //new instance of table
            customerDetails = table.CreateInstance<BillingPOCO>();
            checkout.EnterFirstName(customerDetails.FirstName);
            checkout.EnterLastName(customerDetails.LastName);
            checkout.EnterCompany(customerDetails.Company);
            checkout.EnterStreetAddress(customerDetails.Street);
            checkout.EnterCity(customerDetails.City);
            checkout.EnterCounty(customerDetails.County);
            checkout.EnterPostcode(customerDetails.Postcode);
            checkout.EnterPhone(customerDetails.Phone);
            checkout.EnterEmail(customerDetails.Email);
            checkout.EnterOrderNotes(customerDetails.OrderNotes);
            Console.WriteLine("Customer details entered");

            string billingFieldsData = checkout.GetBillingFieldsText();

            //assert for each field that is compulsory
            bool clickChequePayment = false;
            while(!clickChequePayment)
            {
                try
                {
                    checkout.ClickChequePayment(); //clicks cheque payment
                    Console.WriteLine("Cheque payment clicked");
                    clickChequePayment = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Clicking Cheque Payments again");
                    checkout.ClickChequePayment(); //clicks cheque payment
                    clickChequePayment = true;
                }
            }
        }

        [Then(@"I can place my order")]
        public void ThenICanPlaceMyOrder()
        {
            //places order and captures the order number 
            CheckoutPOM checkout = new CheckoutPOM(_driver);
            bool placedOrder = false;
            
            while(!placedOrder)
            {
                try
                {
                    checkout.ClickPlaceOrder();
                    placedOrder = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Clicking place order again");
                    checkout.ClickPlaceOrder();
                    placedOrder = true;
                }
            }
            
            string orderNo = checkout.GetOrderNo();
            Console.WriteLine("Order Placed, Order No: " + "#" + orderNo); //writes order no to console
            _container.OrderNumber = orderNo; //stores
        }

        [Then(@"Verify my order has been placed by checking the orders page")]
        public void ThenVerifyMyOrderHasBeenPlacedByCheckingTheOrdersPage()
        {
            //takes order no from summary and compares it to the order no that appears in the order table
            NavBarPOM navBar = new NavBarPOM(_driver);
            navBar.ViewMyAccount(); //navigate to My Account

            //navigate to orders page
            MyAccountPOM myAccunt = new MyAccountPOM(_driver);
            myAccunt.ClickOrders();

            OrderPOM orders = new OrderPOM(_driver);

            string latestOrder = orders.GetLatestOrder(); //first row in order table
            string orderNum = _container.OrderNumber; //get from container
            Assert.That(latestOrder, Does.Contain(orderNum), "Order not in table");
            Console.WriteLine("Order " + orderNum + " found in orders table");
        }
    }
}
