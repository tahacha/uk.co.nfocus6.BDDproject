using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.nfocus6.BDDproject.Utils
{
    internal static class HelperLib
    {
        
        public static void StaticWaitForElement(IWebDriver driver, By locator, int timeoutInSeconds = 5)
        {
            WebDriverWait myWait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            myWait.Until(drv => drv.FindElement(locator).Displayed);
        }
        
        public static IWebElement WaitForElement(IWebDriver driver, By locator, int timeoutInSeconds = 5)
        {
            WebDriverWait myWait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            myWait.Until(drv => drv.FindElement(locator).Displayed);
            IWebElement foundElement = driver.FindElement(locator);
            return foundElement;
        }

        public static void ScrollUntilElement(IWebDriver driver, IWebElement item) //java script to scroll until certain element is in view 
        {
            IJavaScriptExecutor? javaScriptDriver = driver as IJavaScriptExecutor;
            javaScriptDriver?.ExecuteScript("arguments[0].scrollIntoView()", item);
        }
        public static void StaticTakeScreenshot(IWebDriver driver, IWebElement element, string fileName, bool scroll = false)
        {
            if (scroll)
            {
                ScrollUntilElement(driver, element); //scroll until desired element
            }

            ITakesScreenshot? screenshotElement = driver as ITakesScreenshot;
            Screenshot screenshot = screenshotElement!.GetScreenshot();

            string projectDirectory = Directory.GetCurrentDirectory(); //gets the path of the directory of project
            //C:\Users\TahaChaudhry\source\repos\uk.co.nfocus.ecommerceproject\uk.co.nfocus.ecommerceproject\bin\Debug\net6.0

            string newPath = Path.Combine(projectDirectory, @"..\..\..\Project_Screenshots\" + $"{fileName}.png");

            screenshot.SaveAsFile(newPath); //saves in Project_Screenshots folder 
        }



        public static decimal ConvertToDecimal(IWebElement theValue) //converts captured string to decimal
        {
            decimal decValue = decimal.Parse(theValue.Text.TrimStart('£'));
            return decValue;
        }

        
    }
}
