using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.nfocus6.BDDproject.Utils
{
    public class Wrapper
    {
        private IWebDriver? _driver;
        private bool _loggedIn;
        public string _couponName = "";
        private string _orderNumber = "";
        public IWebDriver Driver { get => _driver!; set => _driver = value; }

        public bool LoggedIn { get => _loggedIn; set => _loggedIn = value; }

        public string CouponName { get => _couponName; set => _couponName = value; }
        public string OrderNumber { get => _orderNumber; set => _orderNumber = value; }

    }
}
