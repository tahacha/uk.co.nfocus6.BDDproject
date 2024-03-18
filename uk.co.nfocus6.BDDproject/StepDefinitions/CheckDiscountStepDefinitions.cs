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
    public class CheckDiscountStepDefinitions
    {
        private readonly ShopContainer _container;
        private IWebDriver _driver;

        public CheckDiscountStepDefinitions(ShopContainer container)
        {
            _container = container;
            this._driver = _container.Driver;

        }
        
        [When(@"I input the coupon '(.*)'")]
        public void WhenIInputTheCoupon(string coupon)
        {
            CartPOM cart = new CartPOM(_driver);
            cart.InputCoupon(coupon);
            cart.ApplyCoupon();
            Console.WriteLine("Entered Coupon: " + coupon + " and clicked apply");
            _container.CouponName = coupon;
        }

        [Then(@"A discount of (.*)% is applied to my cart")]
        public void ThenADiscountOfIsAppliedToMyCart(int discount)
        {
            string userCoupon = _container.CouponName;
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

    }
}
