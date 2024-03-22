using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow.Assist;
using TechTalk.SpecFlow.Infrastructure;
using uk.co.nfocus6.BDDproject.POM;
using uk.co.nfocus6.BDDproject.Utils;

namespace uk.co.nfocus6.BDDproject.StepDefinitions
{
    [Binding]
    public class CheckOrderPlacedStepDefinitions
    {
        private readonly ShopContainer _container;
        private IWebDriver _driver;
        private readonly ISpecFlowOutputHelper _outputHelper;

        public CheckOrderPlacedStepDefinitions(ShopContainer container, ISpecFlowOutputHelper outputHelper)
        {
            _container = container;
            this._driver = _container.Driver;
            _outputHelper = outputHelper;
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
            CheckMandatoryFields(customerDetails);
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
            _outputHelper.WriteLine("Customer details entered");

            string billingFieldsData = checkout.GetBillingFieldsText();

            //assert for each field that is compulsory

            bool clickChequePayment = false;
            while(!clickChequePayment)
            {
                try
                {
                    checkout.ClickChequePayment(); //clicks cheque payment
                    _outputHelper.WriteLine("Cheque payment clicked");
                    clickChequePayment = true;
                }
                catch (Exception e) //if click intercepted or stale element 
                {
                    _outputHelper.WriteLine(e.Message);
                    _outputHelper.WriteLine("Click failed, clicking Cheque Payments again");
                    continue;
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
                    checkout.ClickPlaceOrder(); //clicks place order
                    placedOrder = true;
                }
                catch (Exception e) //if click intercepted or stale element 
                {
                    _outputHelper.WriteLine(e.Message);
                    _outputHelper.WriteLine("Click failed, clicking place order again");
                    continue;
                }
            }
            
            string orderNo = checkout.GetOrderNo();
            _outputHelper.WriteLine("Order Placed, Order No: " + "#" + orderNo); //writes order no to console
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
            _outputHelper.WriteLine("Order " + orderNum + " found in orders table");
        }

        private void CheckMandatoryFields(BillingPOCO customer)
        {
            Assert.Multiple(() =>
            {
                Assert.That(customer.FirstName, Is.Not.Empty, "FirstName is mandatory");
                Assert.That(customer.LastName, Is.Not.Empty, "LastName is mandatory");
                Assert.That(customer.LastName, Is.Not.Empty, "LastName is mandatory");
                Assert.That(customer.Street, Is.Not.Empty, "Street Address is mandatory");
                Assert.That(customer.City, Is.Not.Empty, "City is mandatory");
                Assert.That(customer.Postcode, Is.Not.Empty, "Postcode is mandatory");
                Assert.That(customer.Phone, Is.Not.Empty, "Phone is mandatory");
                Assert.That(customer.Email, Is.Not.Empty, "Email is mandatory");
            });
        }
    }
}
