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

namespace uk.co.nfocus6.BDDproject.Utils
{
    [Binding]
    public class Hooks
    {
        private static IWebDriver? _driver;
        private readonly Wrapper _wrapper;

        public Hooks(Wrapper wrapper)
        {
            _wrapper = wrapper;
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
                    Console.WriteLine("BROWSER Env Missing/Not Recognised, driver set to Edge");
                    break;
            }

            string? startPage = TestContext.Parameters["WebAppURL"]; //fetch URL from run settings

            //once browser set, navigate to this page
            try
            {
                _driver.Url = startPage;
            }
            catch (Exception e) //problem with URL declared in run settings
            {
                Console.WriteLine("Error with setting URL, please check WebAppURL in run settings");
                Console.WriteLine(e.Message);
                Assert.Fail(); //calls teardown since assertion failed
            }
            _wrapper.Driver = _driver;

            //Home page of ecommerce site
            HomePOM home = new HomePOM(_driver);
            home.DismissBanner();
            Console.WriteLine("Closed banner at the bottom of the page");

            _driver.Manage().Window.Maximize(); //fullscreen

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

            bool login = _wrapper.LoggedIn;
            if (login)
            {
                CheckCart(); //check if cart needs to be emptied
                Logout();
                Console.WriteLine("Logged out");
                _driver!.Quit();
            }

            else
            {
                Console.WriteLine("User not logged in, logout process not needed");
                _driver!.Quit();
            }
            
        }
        private static void CheckCart()
        {
            //navigate to cart
            NavBarPOM nav = new NavBarPOM(_driver!);
            nav.ViewCart();
            Console.WriteLine("Cart clicked from nav");
            
           
            //check cart
            CartPOM cart = new CartPOM(_driver!);
            cart.EmptyCart();
        }
        private static void Logout()
        {
            //navigate to the my account page
            NavBarPOM nav = new NavBarPOM(_driver!);
            nav.ViewMyAccount();
            Console.WriteLine("My account clicked from nav");
            

            //clicks the logout link
            MyAccountPOM myAccount = new MyAccountPOM(_driver!);
            myAccount.ClickLogout();
        }

    }

}
