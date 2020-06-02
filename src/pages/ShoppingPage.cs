﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace SHEProject
{
    class ShoppingPage
    {
        readonly string dressesCategoryLocator = "//ul[contains(@class,'sf-menu')]//a[@title='Dresses'and not(ancestor::ul[contains(@class,'submenu-container')])]";
        readonly string summerDressesCategoryLocator = "//div[@class='subcategory-image']/a[@title='Summer Dresses']";
        readonly string summerDressesDescLocator = "//div[@class='cat_desc']//span[(contains(.,'Summer Dresses'))]";
        readonly string dressImageLocator = "//a[@class='product_img_link']";
        readonly string dressQuickViewLocator = "//li[contains(@class,'hovered')]//a[@class='quick-view']";
        readonly string iframeLocator = "//iframe[@class='fancybox-iframe']";
        readonly string addToCartButtonLocator = "//button[@class='exclusive']";
        readonly string addedToCartSuccessfulLocator = "//i[@class='icon-ok']";
        readonly string continueShoppingButtonLocator = "//span[contains(@class,'continue')]";
        readonly string checkoutButtonLocator = "//a[@title='Proceed to checkout']";
        private IWebDriver driver;
        private WebDriverWait wait;

        public ShoppingPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public void SelectDressCategory()
        {
            IWebElement dressesCategoryLink = driver.FindElement(By.XPath(dressesCategoryLocator));
            dressesCategoryLink.Click();
        }

        public void SelectSummerDressCategory()
        {
            IWebElement summerDressesCategoryLink = driver.FindElement(By.XPath(summerDressesCategoryLocator));
            summerDressesCategoryLink.Click();
        }

        public void QuickViewDress(int itemNumber)
        {
            IWebElement dress = driver.FindElements(By.XPath(dressImageLocator))[--itemNumber];
            //wait.Until(ExpectedConditions.ElementToBeClickable(dress));
            Actions firstAction = new Actions(driver);
            firstAction.MoveToElement(dress).Build().Perform();
            IWebElement dressQuickView = driver.FindElement(By.XPath(dressQuickViewLocator));
            //wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(dressQuickViewLocator)));
            dressQuickView.Click();
        }

        public void AddToCart()
        {
            IWebElement iframeElement = driver.FindElement(By.XPath(iframeLocator));
            //wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(iframeLocator)));
            driver.SwitchTo().Frame(iframeElement);
            IWebElement addToCartButton = driver.FindElement(By.XPath(addToCartButtonLocator));
            addToCartButton.Click();
            driver.SwitchTo().DefaultContent();
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath(iframeLocator)));
        }

        public void ClickContinueShoppingButton()
        {
            IWebElement continueShoppingButton = driver.FindElement(By.XPath(continueShoppingButtonLocator));
            continueShoppingButton.Click();
        }

        public void ClickCheckoutButton()
        {
            IWebElement checkoutButton = driver.FindElement(By.XPath(checkoutButtonLocator));
            checkoutButton.Click();
        }

    }
}
