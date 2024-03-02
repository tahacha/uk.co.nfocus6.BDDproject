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
        private IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Before]
        public void SetUp()
        {
            string browser = Environment.GetEnvironmentVariable("BROWSER");

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

            string startPage = TestContext.Parameters["WebAppURL"]; //fetch URL from run settings

            //once browser set, navigate to this page
            try
            {
                _driver.Url = startPage;
            }
            catch(Exception e) //problem with URL declared in run settings
            {
                Console.WriteLine("Error with setting URL, please check WebAppURL in run settings");
                Console.WriteLine(e.Message);
                Assert.Fail(); //calls teardown since assertion failed
            }
            _scenarioContext["theDriver"] = _driver; //stores driver in _scenarioContext

            //Home page of ecommerce site
            HomePOM home = new HomePOM(_driver);
            home.DismissBanner();
            Console.WriteLine("Closed banner at the bottom of the page");

            _driver.Manage().Window.Maximize(); //fullscreen

            //Navigate to my account page
            NavBarPOM nav = new NavBarPOM(_driver);
            nav.ViewMyAccount();
        }

        [After]
        public void TearDown()
        {
            _driver.Quit();
        }

    }

}
