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

namespace uk.co.nfocus6.BDDproject.StepDefinitions
{

    [Binding]
    public class CartCheckoutStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver _driver;

        public CartCheckoutStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            this._driver = (IWebDriver)_scenarioContext["theDriver"];

        }
        [Given(@"I am logged in to the ecommerce site")]
        public void GivenIAmLoggedInToTheEcommerceSite()
        {

            MyAccountPOM myAccount = new MyAccountPOM(_driver);
            string username = Environment.GetEnvironmentVariable("Secret_Username");
            string password = Environment.GetEnvironmentVariable("Secret_Password");

            //null
            if (username == string.Empty || password == string.Empty)
            {
                Console.WriteLine("Username and/or password is null, please check Secret_Username & Secret_Password");
                Assert.Fail();
            }

            //enter username and password 
            myAccount.SetUsername(username);
            Console.WriteLine("Username entered");
            myAccount.SetPassword(password);
            Console.WriteLine("Password entered");

            //click login
            myAccount.ClickLogin();

            //check to see if logged in
            bool loggedIn = myAccount.SuccessfulLogin();
            _scenarioContext["loggedIn"] = loggedIn; //boolean of whether logged in or not 
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
            nav.ViewCart();
        }

        [When(@"I input the coupon '(.*)' and click apply")]
        public void WhenIInputTheCouponAndClickApply(string coupon)
        {
            CartPOM cart = new CartPOM(_driver);
            cart.InputCoupon(coupon);
            cart.ApplyCoupon();
            Console.WriteLine("added Coupon: " + coupon + " and clicked apply");

        }

        [Then(@"A discount of (.*)% is applied to my cart")]
        public void ThenADiscountOfIsAppliedToMyCart(int discount)
        {
            //assert disocunt applied and it's 15% discount
            CartPOM cart = new CartPOM(_driver);
            string couponApplied = cart.DiscountApplied();
            decimal discountAdded = cart.TheDiscount();

            Assert.Multiple(() =>
            {
                Assert.That(couponApplied, Does.Contain("Coupon:"), "Discount not applied");
                Assert.That(discountAdded == (decimal)discount / 100, "Not a " + discount + "% discount, it's a " + discountAdded * 100 + "% discount");

            });
            Console.WriteLine("Correct discount applied");

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
        { //proceed to checkout from cart
            _scenarioContext.Pending();
        }

        [When(@"Fill in my address and click the payment by cheque option")]
        public void FillInMyAddressAndClickThePaymentByChequeOption()
        {
            //fill in details and press payment by cheque
            _scenarioContext.Pending();
        }

        [Then(@"I can place my order and see a summary of my order including an order number")]
        public void ThenICanPlaceMyOrderAndSeeASummaryOfMyOrderIncludingAnOrderNumber()
        {
            //sees summary of order, takes order no and prints to console 
            _scenarioContext.Pending();
        }

        [Then(@"Verify my order has been placed my checking the same order number appears on the orders page")]
        public void ThenVerifyMyOrderHasBeenPlacedMyCheckingTheSameOrderNumberAppearsOnTheOrdersPage()
        {
            //takes order no from summary and compares it to the order no that appears in the order table
            _scenarioContext.Pending();
        }

    }
}
