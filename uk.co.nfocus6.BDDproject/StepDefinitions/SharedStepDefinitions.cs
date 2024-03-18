using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.co.nfocus6.BDDproject.POM;
using uk.co.nfocus6.BDDproject.Utils;

namespace uk.co.nfocus6.BDDproject.StepDefinitions
{
    [Binding]
    public class SharedStepDefinitions
    {
        private readonly ShopContainer _container;
        private IWebDriver _driver;

        public SharedStepDefinitions(ShopContainer container)
        {
            _container = container;
            this._driver = _container.Driver;
        }

        [Given(@"I am logged in to the ecommerce site")]
        public void GivenIAmLoggedInToTheEcommerceSite()
        {

            MyAccountPOM myAccount = new MyAccountPOM(_driver);
            string? username = Environment.GetEnvironmentVariable("Secret_Username");
            string? password = Environment.GetEnvironmentVariable("Secret_Password");

            //null
            if (username == string.Empty || password == string.Empty)
            {
                Console.WriteLine("Username and/or password is null, please check Secret_Username & Secret_Password");
                Assert.Fail();
            }

            //enter username and password 
            myAccount.SetUsername(username!);
            Console.WriteLine("Username entered");
            myAccount.SetPassword(password!);
            Console.WriteLine("Password entered");

            //click login
            myAccount.ClickLogin();

            //check to see if logged in
            bool loggedIn = myAccount.SuccessfulLogin();
            _container.LoggedIn = loggedIn;
            Assert.That(myAccount.SuccessfulLogin() == true, "Incorrect User Credentials");
            Console.WriteLine("Logged In");
        }

        [Given(@"I have added '(.*)' to my cart")]
        public void GivenIHaveAddedAToMyCart(string addItem)
        {
            //navigates to shop page
            NavBarPOM nav = new NavBarPOM(_driver);
            nav.ViewShop();

            //Adds item to cart
            ShopPOM shop = new ShopPOM(_driver);
            bool itemAdded = shop.AddItemToCart(addItem);
            Assert.That(itemAdded == true, "Item does not exist");
            Console.WriteLine("Item added to cart");

            //navigates to cart
            shop.ViewCart();
            Console.WriteLine("View cart clicked");
        }

    }
}
