using OpenQA.Selenium;
using uk.co.nfocus6.BDDproject.POM;
using NUnit.Framework;
using uk.co.nfocus6.BDDproject.Utils;
using TechTalk.SpecFlow.Infrastructure;

namespace uk.co.nfocus6.BDDproject.StepDefinitions
{

    [Binding]
    public class CheckDiscountStepDefinitions
    {
        private readonly ShopContainer _container;
        private IWebDriver _driver;
        private readonly ISpecFlowOutputHelper _outputHelper;

        public CheckDiscountStepDefinitions(ShopContainer container, ISpecFlowOutputHelper outputHelper)
        {
            _container = container;
            this._driver = _container.Driver;
            _outputHelper = outputHelper;
        }
        
        [When(@"I input the coupon '(.*)'")]
        public void WhenIInputTheCoupon(string coupon)
        {
            CartPOM cart = new CartPOM(_driver);
            cart.InputCoupon(coupon);
            cart.ApplyCoupon();
            _outputHelper.WriteLine("Entered Coupon: " + coupon + " and clicked apply");
            _container.CouponName = coupon;
        }

        [Then(@"A discount of (.*)% is applied to my cart")]
        public void ThenADiscountOfIsAppliedToMyCart(decimal discount) //passed from feature file
        {
            string userCoupon = _container.CouponName;
            //assert discount applied = discount passed to method
            CartPOM cart = new CartPOM(_driver);
            string couponApplied = cart.DiscountApplied(); //the name of coupon applied after clicking apply
            decimal discountAdded = cart.TheDiscount(); //actual discount applied on the ecommerce site 

            Assert.Multiple(() =>
            {
                Assert.That(couponApplied, Does.Contain("Coupon: " + userCoupon), "Discount not applied");
                Assert.That(discountAdded, Is.EqualTo(discount), "Not a " + discount + "% discount, it's a " + discountAdded + "% discount");
            });
            _outputHelper.WriteLine(userCoupon + " is a valid coupon");
            _outputHelper.WriteLine("Correct discount applied - " + discount + "%");

            
        }

        [Then(@"The order total updates accordingly")]
        public void ThenTheOrderTotalUpdatesAccordingly()
        {
            //checks order total is correct
            CartPOM cart = new CartPOM(_driver);
            decimal orderTotal = cart.GetOrderTotal(); //Order total on ecommerce site 
            decimal calculatedTotal = cart.GetCalculatedTotal(); //calculation on values captured

            Assert.That(orderTotal, Is.EqualTo(calculatedTotal), "Order total is not correct");
            _outputHelper.WriteLine("Order total is correct");
        }

    }
}
