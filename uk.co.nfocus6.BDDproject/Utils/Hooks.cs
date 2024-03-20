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
        private readonly ShopContainer _container;
        private readonly ISpecFlowOutputHelper _outputHelper;

        public Hooks(ShopContainer container, ISpecFlowOutputHelper outputHelper)
        {
            _container = container;
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
            string? startPage = TestContext.Parameters["WebAppURL"];
            if(startPage == string.Empty)
            {
                Console.WriteLine("Driver closed to missing URL, please check WebAppURL");
                _driver!.Quit(); //closes driver 
                return;
            }

            bool login = _container.LoggedIn;
            if (login)
            {
                CheckCart(); //check if cart needs to be emptied
                Logout();
                Console.WriteLine("Logged out");
            }

            else
            {
                Console.WriteLine("User not logged in, logout process not needed");
            }

            _driver!.Quit();
            
        }
        private static void CheckCart()
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
                    Console.WriteLine("Cart clicked from nav");
                    cartClick = true;
                }
                catch (Exception e) //if click fails 
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Trying to click Cart again");
                    nav.ViewCart();
                    cartClick = true;
                }
            }

            //check cart
            CartPOM cart = new CartPOM(_driver!);
            cart.EmptyCart();
        }
        private static void Logout()
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
                    Console.WriteLine("My account clicked from nav");
                    accountClick = true; //exit loop
                }
                catch (Exception e) //if click fails
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Trying to click My account again");
                    nav.ViewMyAccount();
                    accountClick = true;
                }
            }
            
            //clicks the logout link
            MyAccountPOM myAccount = new MyAccountPOM(_driver!);
            myAccount.ClickLogout();
        }

    }

}
