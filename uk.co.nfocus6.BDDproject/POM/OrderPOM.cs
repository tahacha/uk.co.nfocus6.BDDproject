using OpenQA.Selenium;
using uk.co.nfocus6.BDDproject.Utils;

namespace uk.co.nfocus6.BDDproject.POM
{
    internal class OrderPOM
    {
        private IWebDriver _driver;
        public OrderPOM(IWebDriver driver)
        {
            this._driver = driver;
            HelperLib.WaitForElement(_driver, By.LinkText("View")); //wait
            Console.WriteLine("Viewing Orders page");
        }
        //locators
        private IWebElement _latestOrder => HelperLib.WaitForElement(_driver, By.CssSelector("#post-7 > div > div > div > table > tbody > tr:nth-child(1)"));

        public string GetLatestOrder()
        {
            string latestOrderText = _latestOrder.Text;
            return latestOrderText;
        }
    }
}
