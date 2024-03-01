using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.nfocus6.BDDproject.StepDefinitions
{
    using System;
    using TechTalk.SpecFlow;

    namespace MyNamespace
    {
        [Binding]
        public class CartCheckoutStepDefinitions
        {
            private readonly ScenarioContext _scenarioContext;

            public CartCheckoutStepDefinitions(ScenarioContext scenarioContext)
            {
                _scenarioContext = scenarioContext;
            }
            [Given(@"I am logged in to the ecommerce site")]
            public void GivenIAmLoggedInToTheEcommerceSite()
            {
                //login process
                _scenarioContext.Pending();
            }

            [Given(@"I have added a '(.*)' to my cart")]
            public void GivenIHaveAddedAToMyCart(string item)
            {
                //add item to cart - takes in passed paramter
                //aria label
                _scenarioContext.Pending();
            }

            [When(@"I input the coupon '(.*)' and click apply")]
            public void WhenIInputTheCouponAndClickApply(string coupon)
            {
                //enter coupon and click apply
                _scenarioContext.Pending();
            }

            [Then(@"A discount of (.*)% is applied to my cart")]
            public void ThenADiscountOfIsAppliedToMyCart(int discount)
            {
                //assert disocunt applied and it's 15% discount
                _scenarioContext.Pending();
            }

            [Then(@"The order total updates accordingly")]
            public void ThenTheOrderTotalUpdatesAccordingly()
            {
                //assert correct total 
                _scenarioContext.Pending();
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
}
