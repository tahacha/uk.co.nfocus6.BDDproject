using OpenQA.Selenium;

namespace uk.co.nfocus6.BDDproject.Utils
{
    public class ShopContainer
    {
        private IWebDriver? _driver;
        private bool _loggedIn;
        private string _couponName = "";
        private string _orderNumber = "";
        public IWebDriver Driver { get => _driver!; set => _driver = value; }

        public bool LoggedIn { get => _loggedIn; set => _loggedIn = value; }

        public string CouponName { get => _couponName; set => _couponName = value; }
        public string OrderNumber { get => _orderNumber; set => _orderNumber = value; }

    }
}
