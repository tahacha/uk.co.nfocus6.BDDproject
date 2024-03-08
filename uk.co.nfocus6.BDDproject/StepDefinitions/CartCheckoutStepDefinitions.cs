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
            //assert discount applied and it's 15% discount
            CartPOM cart = new CartPOM(_driver);
            string couponApplied = cart.DiscountApplied();
            decimal discountAdded = cart.TheDiscount(); //actual discount applied
            decimal discountAddedPercent = Decimal.Truncate(discountAdded * 100);

            Assert.Multiple(() =>
            {
                Assert.That(couponApplied, Does.Contain("Coupon:"), "Discount not applied");
                Assert.That(discountAdded == (decimal)discount / 100, "Not a " + discount + "% discount, it's a " + discountAddedPercent + "% discount");

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
        { 
            //proceed to checkout from cart
            CartPOM cartPage = new CartPOM(_driver);
            cartPage.ProceedToCheckout(); 
        }

        [When(@"Fill in my address and click the payment by cheque option")]
        public void FillInMyAddressAndClickThePaymentByChequeOption()
        {
            //fill in details and press payment by cheque
            //checkout page 
            CustomerDetails theCustomer = new CustomerDetails("Smith", "Jones", "1 Oxford Street", "London", "W1B 3AG", "123456789"); //instantiates a new object of the CustomerDetails class

            //enter all details
            CheckoutPOM checkout = new CheckoutPOM(_driver);
            checkout.EnterFirstName(theCustomer.GetFirstName());
            checkout.EnterLastName(theCustomer.GetLastName());
            checkout.EnterStreetAddress(theCustomer.GetAddress());
            checkout.EnterCity(theCustomer.GetCity());
            checkout.EnterPostcode(theCustomer.GetPostcode());
            checkout.EnterPhone(theCustomer.GetPhone());
            Console.WriteLine("Customer details entered");

            checkout.ClickChequePayment(); //clicks cheque payment
        }

        [Then(@"I can place my order and see a summary of my order including an order number")]
        public void ThenICanPlaceMyOrderAndSeeASummaryOfMyOrderIncludingAnOrderNumber()
        {
            //places order and captures the order number 
            CheckoutPOM checkout = new CheckoutPOM(_driver);
            checkout.ClickChequePayment(); 
            checkout.ClickPlaceOrder();
            string orderNo = checkout.GetOrderNo(); 
            Console.WriteLine("Order Placed, Order No: " + "#" + orderNo); //writes order no to console
            _wrapper.OrderNumber = orderNo;
        }

        [Then(@"Verify my order has been placed my checking the same order number appears on the orders page")]
        public void ThenVerifyMyOrderHasBeenPlacedMyCheckingTheSameOrderNumberAppearsOnTheOrdersPage()
        {
            //takes order no from summary and compares it to the order no that appears in the order table
            NavBarPOM navBar = new NavBarPOM(_driver);
            navBar.ViewMyAccount(); //navigate to My Account

            //navigate to orders page
            MyAccountPOM myAccunt = new MyAccountPOM(_driver);
            myAccunt.ClickOrders(); 

            OrderPOM orders = new OrderPOM(_driver);
            
            string orderTable = orders.GetOrderTable(); //table text
            string orderNum = _wrapper.OrderNumber;
            Assert.That(orderTable, Does.Contain(orderNum), "Order not in table");
        }

    }
}
