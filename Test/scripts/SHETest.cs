using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SHETest
{
    class SHETest
    {
 
        readonly string loginEmail = "d1474341@urhen.com";
        readonly string loginPassword = "Password";
        readonly string LoginButtonLocator = "//a[@class='login']";
        readonly string SubmitLoginButtonLocator = "//button[@id='SubmitLogin']";
        readonly string loginEmailFormLocator = "//input[@id='email']";
        readonly string loginPassFormLocator = "//input[@id='passwd']";
        readonly string dressesCategoryLocator = "//ul[contains(@class,'sf-menu')]//a[@title='Dresses'and not(ancestor::ul[contains(@class,'submenu-container')])]";
        readonly string summerDressesCategoryLocator = "//div[@class='subcategory-image']/a[@title='Summer Dresses']";
        readonly string dressImageLocator = "//a[@class='product_img_link']";
        readonly string dressQuickViewLocator = "//li[contains(@class,'hovered')]//a[@class='quick-view']";
        readonly string iframeLocator = "//iframe[@class='fancybox-iframe']";
        readonly string addToCartButtonLocator = "//button[@class='exclusive']";
        readonly string addedToCartSuccessfulLocator = "//i[@class='icon-ok']";
        readonly string continueShoppingButtonLocator = "//span[contains(@class,'continue')]";
        readonly string checkoutButtonLocator = "//a[@title='Proceed to checkout']";
        readonly string accountInfoLocator = "//*[@title='Information']";
        readonly string summerDressesDescLocator = "//div[@class='cat_desc']//span[(contains(.,'Summer Dresses'))]";
        readonly string deleteItemFromBasketLocator = "//a[@title='Delete']";
        readonly string logoutButtonLocator = "//a[@class='logout']";


        IWebDriver driver = new FirefoxDriver();

 
        [Test]
        public void Test()
        {
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            driver.Url = "http://automationpractice.com/index.php";

            //login to site 
            IWebElement loginButton = driver.FindElement(By.XPath(LoginButtonLocator));
            loginButton.Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            IWebElement submitLoginButton = driver.FindElement(By.XPath(SubmitLoginButtonLocator));
            wait.Until(ExpectedConditions.ElementToBeClickable(submitLoginButton));
            IWebElement loginEmailForm = driver.FindElement(By.XPath(loginEmailFormLocator));
            IWebElement loginPassForm = driver.FindElement(By.XPath(loginPassFormLocator));
            loginEmailForm.SendKeys(loginEmail);
            loginPassForm.SendKeys(loginPassword);
            submitLoginButton.Click();
            Assert.IsTrue(driver.FindElement(By.XPath(accountInfoLocator)).Displayed);

            //select Dresses->Summer Dresses
            IWebElement dressesCategoryLink = driver.FindElement(By.XPath(dressesCategoryLocator));
            wait.Until(ExpectedConditions.ElementToBeClickable(dressesCategoryLink));
            dressesCategoryLink.Click();
            IWebElement summerDressesCategoryLink = driver.FindElement(By.XPath(summerDressesCategoryLocator));
            summerDressesCategoryLink.Click();
            Assert.IsTrue(driver.FindElement(By.XPath(summerDressesDescLocator)).Displayed);

            //Quick-View a dress
            IWebElement firstDress = driver.FindElements(By.XPath(dressImageLocator))[0];
            Actions firstAction = new Actions(driver);
            firstAction.MoveToElement(firstDress).Build().Perform();
            IWebElement dressQuickView = driver.FindElement(By.XPath(dressQuickViewLocator));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(dressQuickViewLocator)));
            dressQuickView.Click();
            IWebElement iframeElement = driver.FindElement(By.XPath(iframeLocator));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(iframeLocator)));
            driver.SwitchTo().Frame(iframeElement);
            Assert.IsTrue(driver.FindElement(By.XPath(addToCartButtonLocator)).Displayed);

            //add dress to cart
            IWebElement addToCartButton = driver.FindElement(By.XPath(addToCartButtonLocator));
            addToCartButton.Click();
            driver.SwitchTo().DefaultContent();
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath(iframeLocator)));
            Assert.IsTrue(driver.FindElement(By.XPath(addedToCartSuccessfulLocator)).Displayed);

            //add another dress to cart
            IWebElement continueShoppingButton = driver.FindElement(By.XPath(continueShoppingButtonLocator));
            continueShoppingButton.Click();
            IWebElement secondDress = driver.FindElements(By.XPath(dressImageLocator))[2];
            wait.Until(ExpectedConditions.ElementToBeClickable(secondDress));
            Actions secondAction = new Actions(driver);
            secondAction.MoveToElement(secondDress).Build().Perform();
            IWebElement secondDressQuickView = driver.FindElement(By.XPath(dressQuickViewLocator));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(dressQuickViewLocator)));
            secondDressQuickView.Click();
            IWebElement newiFrameElement = driver.FindElement(By.XPath(iframeLocator));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(iframeLocator)));
            driver.SwitchTo().Frame(newiFrameElement);
            Assert.IsTrue(driver.FindElement(By.XPath(addToCartButtonLocator)).Displayed);
            IWebElement newAddToCartButton = driver.FindElement(By.XPath(addToCartButtonLocator));
            newAddToCartButton.Click();
            driver.SwitchTo().DefaultContent();
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath(iframeLocator)));
            Assert.IsTrue(driver.FindElement(By.XPath(addedToCartSuccessfulLocator)).Displayed);

            //go to checkout
            IWebElement checkoutButton = driver.FindElement(By.XPath(checkoutButtonLocator));
            checkoutButton.Click();

            //verify correct dress is selected on summary page
            Assert.IsTrue(driver.FindElement(By.XPath("//small[contains(text(),'SKU : demo_5')]")).Displayed);
            Assert.IsTrue(driver.FindElement(By.XPath("//small[contains(text(),'SKU : demo_7')]")).Displayed);

            //remove 2nd dress from cart
            IWebElement deleteSecondDress = driver.FindElements(By.XPath(deleteItemFromBasketLocator))[1];
            deleteSecondDress.Click();
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//small[contains(text(),'SKU : demo_7')]")));
            IList<IWebElement> basketList = driver.FindElements(By.XPath(deleteItemFromBasketLocator));
            int basketSize = basketList.Count();
            Assert.IsTrue(basketSize == 1);
            Assert.IsTrue(driver.FindElement(By.XPath("//small[contains(text(),'SKU : demo_5')]")).Displayed);

            //sign out
            IWebElement logoutButton = driver.FindElement(By.XPath(logoutButtonLocator));
            logoutButton.Click();
            Assert.IsTrue(driver.FindElement(By.XPath(LoginButtonLocator)).Displayed);

            driver.Close();
        }

    }
}