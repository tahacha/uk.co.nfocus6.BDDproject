﻿using OpenQA.Selenium;
using uk.co.nfocus6.BDDproject.Utils;

namespace uk.co.nfocus6.BDDproject.POM
{
    internal class CartPOM
    {
        private IWebDriver _driver;
        public CartPOM(IWebDriver driver) //constructor 
        {
            this._driver = driver;
            HelperLib.WaitForElement(_driver, By.XPath("//h1[contains(.,'Cart')]"));
            Console.WriteLine("Viewing Cart page");
        }

        //locators
        private IWebElement _couponInput => HelperLib.WaitForElement(_driver, By.Id("coupon_code"));

        private IWebElement _originalTotal => HelperLib.WaitForElement(_driver, By.CssSelector(".cart-subtotal > td")); //gets original price 
     
        private IWebElement _applyCoupon => HelperLib.WaitForElement(_driver, By.Name("apply_coupon"));

        private IWebElement _checkCouponDiscount => HelperLib.WaitForElement(_driver, By.CssSelector(".cart-discount .woocommerce-Price-amount"));
        
        private IWebElement _orderTotal => HelperLib.WaitForElement(_driver, By.CssSelector("strong bdi"));

        private IWebElement _shippingPrice => HelperLib.WaitForElement(_driver, By.CssSelector("label bdi"));
        
        private IWebElement _removeFromCart => HelperLib.WaitForElement(_driver, By.LinkText("×"));

        private IWebElement _proceedToCheckout => HelperLib.WaitForElement(_driver, By.LinkText("Proceed to checkout"));

        private IWebElement _cartDiscount => HelperLib.WaitForElement(_driver, By.CssSelector(".cart-discount > th"));
        private IWebElement _returnToShop => HelperLib.WaitForElement(_driver, By.LinkText("Return to shop"),10);

        private IWebElement _bodyText => HelperLib.WaitForElement(_driver, By.TagName("body"));

        //decimals
        private decimal _originalTotalDecimal => HelperLib.ConvertToDecimal(_originalTotal); //original price as decimal
        private decimal _checkCouponDiscountDecimal => HelperLib.ConvertToDecimal(_checkCouponDiscount); //coupon discount as decimal

        private decimal _orderTotalDecimal => HelperLib.ConvertToDecimal(_orderTotal); //total price with coupon and shipping 

        private decimal _shippingPriceDecimal => HelperLib.ConvertToDecimal(_shippingPrice); //shipping price 



        //methods
        public void InputCoupon(string coupon)
        {
            _couponInput.Clear(); //make sure it's clear
            _couponInput.SendKeys(coupon);

        }

        public void ApplyCoupon()
        {
            _applyCoupon.Click(); //applies coupon 
        }

        public string DiscountApplied()
        {
            try
            {
                if(_cartDiscount.Displayed) 
                {
                    string couponText = _cartDiscount.Text;
                    return couponText;
                }

            }
            catch (Exception)
            {

            }
            
            return "coupon invalid";
        }
        public decimal TheDiscount()
        {
            decimal orderTotalNoShipWithDiscount = _orderTotalDecimal - _shippingPriceDecimal;
            decimal discountApplied = (_originalTotalDecimal - orderTotalNoShipWithDiscount) / _originalTotalDecimal; //calculated percentage change
            return discountApplied*100; //returns the discount decimal 
        }

        public decimal GetOrderTotal()
        {
            return _orderTotalDecimal;
        }

        public decimal GetCalculatedTotal()
        {
            decimal calculatedTotal = (_originalTotalDecimal + _shippingPriceDecimal) - _checkCouponDiscountDecimal;
            return calculatedTotal;
        }

       
        private void RemoveItem()
        {
            _removeFromCart.Click(); //removes from cart
        }

        public void ProceedToCheckout()
        {
            _proceedToCheckout.Click(); //proceeds to checkout by clicking on link text
        }

        public void EmptyCart()
        {
            string bodyText = _bodyText.Text;
            //clicks x
            if (bodyText.Contains("Product"))
            {
                RemoveItem();
            }

        }

        public void ReturnToShop()
        {
            _returnToShop.Click();
        }

        public string GetSummary()
        {
            return "Subtotal: " + _originalTotalDecimal + "\nCoupon Amount: " + _checkCouponDiscountDecimal + "\nOrder Total: " + _orderTotalDecimal;
        }
            
    }
}
