using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using uk.co.nfocus6.BDDproject.POM;
using TechTalk.SpecFlow.Infrastructure;

namespace uk.co.nfocus6.BDDproject.Utils
{
    [Binding]
    public class Hooks
    {
        private static IWebDriver? _driver;
        private readonly ScenarioContext _scenarioContext;
        private readonly ShopContainer _container;
        private readonly ISpecFlowOutputHelper _outputHelper;

        public Hooks(ShopContainer container, ScenarioContext scenarioContext, ISpecFlowOutputHelper outputHelper)
        {
            _container = container;
            _scenarioContext = scenarioContext;
            _outputHelper = outputHelper;
        }

        [Before("@GUI")]
        public void SetUp()
        {
            string? browser = Environment.GetEnvironmentVariable("BROWSER");

            switch (browser) //switchs type of driver depending on "browser"
            {
                case "edge":
                    _driver = new EdgeDriver();
                    break;
                case "firefox":
                    _driver = new FirefoxDriver();
                    break;
                case "chrome":
                    _driver = new ChromeDriver();
                    break;
                default: //if browser is null or not recognised 
                    _driver = new EdgeDriver();
                    _outputHelper.WriteLine("BROWSER Env Missing/Not Recognised, driver set to Edge");
                    break;
            }

            string? startPage = TestContext.Parameters["WebAppURL"]; //fetch URL from run settings

            //once browser set, navigate to this page
            try
            {
                _driver.Url = startPage;
            }
            catch (Exception) //problem with URL declared in run settings
            {
                throw new Exception("Error with setting URL, please check WebAppURL in run settings");
            }

            _container.Driver = _driver;
            _driver.Manage().Window.Maximize(); //fullscreen

            //Home page of ecommerce site
            HomePOM home = new HomePOM(_driver);
            home.DismissBanner();
            _outputHelper.WriteLine("Closed banner at the bottom of the page");

            //Navigate to my account page
            NavBarPOM nav = new NavBarPOM(_driver);
            nav.ViewMyAccount();
        }

        [After("@GUI")]
        public void TearDown()
        {
            if (_scenarioContext.TestError != null)
            {
                string failedTest = HelperLib.StaticTakeScreenshot(_driver!, "test-fail");
                _outputHelper.AddAttachment(failedTest);
            }
            string? startPage = TestContext.Parameters["WebAppURL"];
            if(startPage == string.Empty)
            {
                _outputHelper.WriteLine("Driver closed to missing URL, please check WebAppURL");
                _driver!.Quit(); //closes driver 
                return;
            }

            bool login = _container.LoggedIn;
            if (login)
            {
                CheckCart(); //check if cart needs to be emptied
                Logout();
                _outputHelper.WriteLine("Logged out");
            }

            else
            {
                _outputHelper.WriteLine("User not logged in, logout process not needed");
            }

            _driver!.Quit();
            
        }
        
        private void CheckCart()
        {
            //navigate to cart
            NavBarPOM nav = new NavBarPOM(_driver!);


            //could implement try catch for click interception if scroll doesn't work
            bool cartClick = false;
            while(!cartClick)
            {
                try
                {
                    nav.ViewCart();
                    _outputHelper.WriteLine("Cart clicked from nav");
                    cartClick = true;
                }
                catch (Exception e) //if click fails 
                {
                    _outputHelper.WriteLine(e.Message);
                    _outputHelper.WriteLine("Click failed, trying to click Cart again");
                    continue;
                }
            }

            //check cart
            CartPOM cart = new CartPOM(_driver!);
            cart.EmptyCart();
        }
        private void Logout()
        {
            //navigate to the my account page
            NavBarPOM nav = new NavBarPOM(_driver!);

            //try catch for click interception if scroll doesn't prevent ClickInterception
            bool accountClick = false;
            while(!accountClick)
            {
                try
                {
                    nav.ViewMyAccount();
                    _outputHelper.WriteLine("My account clicked from nav");
                    accountClick = true; //exit loop
                }
                catch (Exception e) //if click fails
                {
                    _outputHelper.WriteLine(e.Message);
                    _outputHelper.WriteLine("Click failed, trying to click My account again");
                    continue;
                }
            }
            
            //clicks the logout link
            MyAccountPOM myAccount = new MyAccountPOM(_driver!);
            myAccount.ClickLogout();
        }

    }

}
