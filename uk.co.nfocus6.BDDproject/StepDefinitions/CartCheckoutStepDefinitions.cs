using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using uk.co.nfocus6.BDDproject.POM;
using NUnit.Framework;
using uk.co.nfocus6.BDDproject.Utils;
using TechTalk.SpecFlow.Assist;

namespace uk.co.nfocus6.BDDproject.StepDefinitions
{

    [Binding]
    public class CartCheckoutStepDefinitions
    {
        private readonly Wrapper _wrapper;
        private IWebDriver _driver;

        public CartCheckoutStepDefinitions(Wrapper wrapper)
        {
            _wrapper = wrapper;
            this._driver = _wrapper.Driver;

        }
        [Given(@"I am logged in to the ecommerce site")]
        public void GivenIAmLoggedInToTheEcommerceSite()
        {

            MyAccountPOM myAccount = new MyAccountPOM(_driver);
            string? username = Environment.GetEnvironmentVariable("Secret_Username");
            string? password = Environment.GetEnvironmentVariable("Secret_Password");

            //null
            if (username == string.Empty || password == string.Empty)
            {
                Console.WriteLine("Username and/or password is null, please check Secret_Username & Secret_Password");
                Assert.Fail();
            }

            //enter username and password 
            myAccount.SetUsername(username!);
            Console.WriteLine("Username entered");
            myAccount.SetPassword(password!);
            Console.WriteLine("Password entered");

            //click login
            myAccount.ClickLogin();

            //check to see if logged in
            bool loggedIn = myAccount.SuccessfulLogin();
            _wrapper.LoggedIn = loggedIn;
            Assert.That(myAccount.SuccessfulLogin() == true, "Incorrect User Credentials");
            Console.WriteLine("Logged In");
        }

        [Given(@"I have added a '(.*)' to my cart")]
        public void GivenIHaveAddedAToMyCart(string addItem)
        {
            //navigates to shop page
            NavBarPOM nav = new NavBarPOM(_driver);
            nav.ViewShop();

            //Adds item to cart
            ShopPOM shop = new ShopPOM(_driver);
            bool itemAdded = shop.AddItemToCart(addItem);
            Assert.That(itemAdded == true, "Item does not exist");
            Console.WriteLine("Item added to cart");

            //navigates to cart
            shop.ViewCart();
            Console.WriteLine("View cart clicked");
        }

        [When(@"I input the coupon '(.*)'")]
        public void WhenIInputTheCoupon(string coupon)
        {
            CartPOM cart = new CartPOM(_driver);
            cart.InputCoupon(coupon);
            cart.ApplyCoupon();
            Console.WriteLine("Entered Coupon: " + coupon + " and clicked apply");
            _wrapper.CouponName = coupon;
        }

        [Then(@"A discount of (.*)% is applied to my cart")]
        public void ThenADiscountOfIsAppliedToMyCart(int discount)
        {
            string userCoupon = _wrapper.CouponName;
            //assert discount applied = discount passed to method
            CartPOM cart = new CartPOM(_driver);
            string couponApplied = cart.DiscountApplied(); //stores cart totals table text
            decimal discountAdded = cart.TheDiscount(); //actual discount applied

            Assert.Multiple(() =>
            {
                Assert.That(couponApplied, Does.Contain("Coupon: " + userCoupon), "Discount not applied");
                Assert.That(discountAdded == (decimal)discount, "Not a " + (decimal)discount + "% discount, it's a " + decimal.Truncate(discountAdded) + "% discount");

            });
            Console.WriteLine(userCoupon + " is a valid coupon");
            Console.WriteLine("Correct discount applied - " + discount + "%");

            
        }

        [Then(@"The order total updates accordingly")]
        public void ThenTheOrderTotalUpdatesAccordingly()
        {
            //checks order total is correct
            CartPOM cart = new CartPOM(_driver);
            decimal orderTotal = cart.GetOrderTotal();
            decimal calculatedTotal = cart.GetCalculatedTotal();

            Assert.That(orderTotal == calculatedTotal, "Order total is not correct");
            Console.WriteLine("Order total is correct");
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


            try
            {
                checkout.ClickChequePayment(); //clicks cheque payment
                Console.WriteLine("Cheque payment clicked");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Clicking Cheque Payments again");
                checkout.ClickChequePayment(); //clicks cheque payment
            }
            
        }

        [Then(@"I can place my order")]
        public void ThenICanPlaceMyOrder()
        {
            //places order and captures the order number 
            CheckoutPOM checkout = new CheckoutPOM(_driver);
            try
            {
                checkout.ClickPlaceOrder();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Clicking place order again");
                checkout.ClickPlaceOrder();
            }
            string orderNo = checkout.GetOrderNo(); 
            Console.WriteLine("Order Placed, Order No: " + "#" + orderNo); //writes order no to console
            _wrapper.OrderNumber = orderNo; //stores
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
            
            string orderTable = orders.GetOrderTable(); //table text
            string orderNum = _wrapper.OrderNumber; //get from wrapper
            Assert.That(orderTable, Does.Contain(orderNum), "Order not in table");
            Console.WriteLine("Order " + orderNum + " found in orders table");
        }

    }
}
