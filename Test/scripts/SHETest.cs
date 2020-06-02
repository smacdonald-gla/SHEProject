using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SHEProject;
using SHEProject.src.pages;
using System;
using System.Collections.Generic;
using System.Linq;


namespace SHETest
{
    class SHETest
    {
        readonly string loginEmail = "d1474341@urhen.com";
        readonly string loginPassword = "Password";
        readonly string homeURL = "http://automationpractice.com/index.php";




        [Test]
        public void FirefoxTest()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            driver.Url = homeURL;

            //login to site
            HomePage home_page = new HomePage(driver);
            home_page.clickLogin();

            LoginPage login_page = new LoginPage(driver);
            login_page.UserLogin(loginEmail, loginPassword);
            //Assert.IsTrue(driver.FindElement(By.XPath(accountInfoLocator)).Displayed);

            //select Dresses->Summer Dresses
            ShoppingPage shopping_page = new ShoppingPage(driver);
            shopping_page.SelectDressCategory();
            shopping_page.SelectSummerDressCategory();
            //Assert.IsTrue(driver.FindElement(By.XPath(summerDressesDescLocator)).Displayed);

            //Quick-View a dress
            shopping_page.QuickViewDress(1);
            //Assert.IsTrue(driver.FindElement(By.XPath(addToCartButtonLocator)).Displayed);

            //add dress to cart
            shopping_page.AddToCart();
            //Assert.IsTrue(driver.FindElement(By.XPath(addedToCartSuccessfulLocator)).Displayed);

            //add another dress to cart
            shopping_page.ClickContinueShoppingButton();

            shopping_page.QuickViewDress(3);
            shopping_page.AddToCart();
            //Assert.IsTrue(driver.FindElement(By.XPath(addedToCartSuccessfulLocator)).Displayed);

            //go to checkout
            shopping_page.ClickCheckoutButton();

            //verify correct dress is selected on summary page
            Assert.IsTrue(driver.FindElement(By.XPath("//small[contains(text(),'SKU : demo_5')]")).Displayed);
            Assert.IsTrue(driver.FindElement(By.XPath("//small[contains(text(),'SKU : demo_7')]")).Displayed);

            //remove 2nd dress from cart
            CartSummaryPage cart_summary_page = new CartSummaryPage(driver);
            cart_summary_page.RemoveItemFromCart(2);

            //wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//small[contains(text(),'SKU : demo_7')]")));
            
            Assert.IsTrue(cart_summary_page.ReturnSizeOfBasket() == 1);
            Assert.IsTrue(driver.FindElement(By.XPath("//small[contains(text(),'SKU : demo_5')]")).Displayed);

            //sign out
            home_page.clickLogout();
            //Assert.IsTrue(driver.FindElement(By.XPath(LoginButtonLocator)).Displayed);

            driver.Close();
        }

        [Test]
        public void ChromeTest()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            driver.Url = homeURL;

            //login to site
            HomePage home_page = new HomePage(driver);
            home_page.clickLogin();

            LoginPage login_page = new LoginPage(driver);
            login_page.UserLogin(loginEmail, loginPassword);
            //Assert.IsTrue(driver.FindElement(By.XPath(accountInfoLocator)).Displayed);

            //select Dresses->Summer Dresses
            ShoppingPage shopping_page = new ShoppingPage(driver);
            shopping_page.SelectDressCategory();
            shopping_page.SelectSummerDressCategory();
            //Assert.IsTrue(driver.FindElement(By.XPath(summerDressesDescLocator)).Displayed);

            //Quick-View a dress
            shopping_page.QuickViewDress(1);
            //Assert.IsTrue(driver.FindElement(By.XPath(addToCartButtonLocator)).Displayed);

            //add dress to cart
            shopping_page.AddToCart();
            //Assert.IsTrue(driver.FindElement(By.XPath(addedToCartSuccessfulLocator)).Displayed);

            //add another dress to cart
            shopping_page.ClickContinueShoppingButton();

            shopping_page.QuickViewDress(3);
            shopping_page.AddToCart();
            //Assert.IsTrue(driver.FindElement(By.XPath(addedToCartSuccessfulLocator)).Displayed);

            //go to checkout
            shopping_page.ClickCheckoutButton();

            //verify correct dress is selected on summary page
            Assert.IsTrue(driver.FindElement(By.XPath("//small[contains(text(),'SKU : demo_5')]")).Displayed);
            Assert.IsTrue(driver.FindElement(By.XPath("//small[contains(text(),'SKU : demo_7')]")).Displayed);

            //remove 2nd dress from cart
            CartSummaryPage cart_summary_page = new CartSummaryPage(driver);
            cart_summary_page.RemoveItemFromCart(2);

            //wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//small[contains(text(),'SKU : demo_7')]")));

            Assert.IsTrue(cart_summary_page.ReturnSizeOfBasket() == 1);
            Assert.IsTrue(driver.FindElement(By.XPath("//small[contains(text(),'SKU : demo_5')]")).Displayed);

            //sign out
            home_page.clickLogout();
            //Assert.IsTrue(driver.FindElement(By.XPath(LoginButtonLocator)).Displayed);

            driver.Close();
        }

    }
}