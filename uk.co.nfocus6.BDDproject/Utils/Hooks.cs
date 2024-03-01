using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        }

        [After]
        public void TearDown()
        {

        }

    }

}
