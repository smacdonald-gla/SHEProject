using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace SHEProject
{
    class CartSummaryPage
    {
        readonly string deleteItemFromBasketLocator = "//a[@title='Delete']";
        readonly string cartItemIdentifier = "//small[contains(text(),'SKU')]";
        private IWebDriver driver;

        public CartSummaryPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        /// <summary>
        /// Searches the current Shopping Cart list and removes requested item
        /// </summary>
        /// <param name="item">String identifier for requested item to be deleted eg. "demo_6</param>
        public void RemoveItemFromCart (string item)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            IList<IWebElement> basketList = driver.FindElements(By.XPath(cartItemIdentifier));

            int i = basketList.Count();
            while (i == basketList.Count())
            {
                for (int j =0; j <= i; j++)
                {
                    if (basketList.ElementAt(j).Text.Contains(item))
                    {
                        IWebElement deleteItem = driver.FindElements(By.XPath(deleteItemFromBasketLocator))[j];
                        deleteItem.Click();
                        --i;
                    }
                }
            }
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//small[contains(text(),'"+item+"')]")));
        }

        public int ReturnSizeOfBasket()
        {
            IList<IWebElement> basketList = driver.FindElements(By.XPath(deleteItemFromBasketLocator));
            return basketList.Count();
        }

        public bool IsItemPresentInBasket(string Item)
        {
            return driver.FindElement(By.XPath("//small[contains(text(),'"+Item+"')]")).Displayed;
        }
    }
}
