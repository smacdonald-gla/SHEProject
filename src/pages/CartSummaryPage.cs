using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SHEProject
{
    class CartSummaryPage
    {
        readonly string deleteItemFromBasketLocator = "//a[@title='Delete']";
        private IWebDriver driver;

        public CartSummaryPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public void RemoveItemFromCart (int itemNumber)
        {
            IWebElement deleteSecondDress = driver.FindElements(By.XPath(deleteItemFromBasketLocator))[--itemNumber];
            deleteSecondDress.Click();
        }

        public int ReturnSizeOfBasket()
        {
            IList<IWebElement> basketList = driver.FindElements(By.XPath(deleteItemFromBasketLocator));
            return basketList.Count();
        }
    }
}
