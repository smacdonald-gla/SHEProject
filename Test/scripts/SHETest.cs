using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SHETest
{
    class SHETest
    {
 
        string loginEmail = "d1474341@urhen.com";
        string loginPassword = "Password";
        string LoginButtonLocator = "//a[@class='login']";
        string SubmitLoginButtonLocator = "//button[@id='SubmitLogin']";
        string loginEmailFormLocator = "//input[@id='email']";
        string loginPassFormLocator = "//input[@id='passwd']";
        string dressesCategoryLocator = "//ul[contains(@class,'sf-menu')]//a[@title='Dresses'and not(ancestor::ul[contains(@class,'submenu-container')])]";
        string summerDressesCategoryLocator = "//div[@class='subcategory-image']/a[@title='Summer Dresses']";
        string dressListLocator = "//li[contains(@class,'ajax_block_product')]";
        string dressQuickViewLocator = "//a[@class='quick-view-mobile']";
        string iframeLocator = "//iframe[@class='fancybox-iframe']";
        string addToCartButtonLocator = "//button[@class='exclusive']";
        string addedToCartSuccessfulLocator = "//i[@class='icon-ok']";
        string continueShoppingButtonLocator = "//span[contains(@class,'continue')]";
        string checkoutButtonLocator = "//a[@title='Proceed to checkout']";
        string accountInfoLocator = "//*[@title='Information']";
        string summerDressesDescLocator = "//div[@class='cat_desc']//span[(contains(.,'Summer Dresses'))]";
        string deleteItemFromBasketLocator = "//a[@title='Delete']";
        string finalBasketItemLocator = "//tr[contains(@class,'last_item')]";
        string logoutButtonLocator = "//a[@class='logout']";


        IWebDriver driver = new FirefoxDriver();

 
        [Test]
        public void Test()
        {
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
            IList<IWebElement> dressList = driver.FindElements(By.XPath(dressQuickViewLocator));
            //wait.Until(ExpectedConditions.ElementToBeClickable(dressListLocator));
            IWebElement firstDress = dressList[0];
            firstDress.FindElement(By.XPath(dressQuickViewLocator)).Click();
            IWebElement iframeElement = driver.FindElement(By.XPath(iframeLocator));
            driver.SwitchTo().Frame(iframeElement);
            Assert.IsTrue(driver.FindElement(By.XPath(addToCartButtonLocator)).Displayed);

            //add dress to cart
            IWebElement addToCartButton = driver.FindElement(By.XPath(addToCartButtonLocator));
            addToCartButton.Click();
            Assert.IsTrue(driver.FindElement(By.XPath(addedToCartSuccessfulLocator)).Displayed);

            //add another dress to cart
            IWebElement continueShoppingButton = driver.FindElement(By.XPath(continueShoppingButtonLocator));
            continueShoppingButton.Click();
            IWebElement secondDress = dressList[2];
            secondDress.FindElement(By.XPath(dressQuickViewLocator)).Click();
            driver.SwitchTo().Frame(iframeElement);
            Assert.IsTrue(driver.FindElement(By.XPath(addToCartButtonLocator)).Displayed);

            //go to checkout
            IWebElement checkoutButton = driver.FindElement(By.XPath(checkoutButtonLocator));
            checkoutButton.Click();

            //verify correct dress is selected on summary page
            Assert.IsTrue(driver.FindElement(By.XPath("//a[contains(text(),'Printed Summer Dress')]")).Displayed);
            Assert.IsTrue(driver.FindElement(By.XPath("//a[contains(text(),'Printed Chiffon Dress')]")).Displayed);

            //remove 2nd dress from cart
            string deleteSecondDressLocator = finalBasketItemLocator + deleteItemFromBasketLocator;
            IWebElement deleteSecondDress = driver.FindElement(By.XPath(deleteSecondDressLocator));
            Assert.IsFalse(driver.FindElement(By.XPath("//a[contains(text(),'Printed Chiffon Dress')]")).Displayed);

            //sign out
            IWebElement logoutButton = driver.FindElement(By.XPath(logoutButtonLocator));
            logoutButton.Click();
            Assert.IsTrue(driver.FindElement(By.XPath(LoginButtonLocator)).Displayed);

            driver.Close();
        }

    }
}