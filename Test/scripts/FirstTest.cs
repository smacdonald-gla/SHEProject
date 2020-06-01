using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;

namespace FirstTest
{
    class FirstTest
    {
 
        string loginEmail = "d1474341@urhen.com";
        string loginPassword = "Password";
        string LoginButtonlocator = "//*[@class='login']";
        string SubmitLoginButtonlocator = "//*[@id='SubmitLogin']";
        string loginEmailFormlocator = "//*[@id='email']";
        string loginPassFormlocator = "//*[@id='passwd']";
        string dressesCategoryLocator = "//ul[contains(@class,'sf-menu')]//a[@title='Dresses'and not(ancestor::ul[contains(@class,'submenu-container')])]";
        string summerDressesCategoryLocator = "//div[@class='subcategory-image']/a[@title='Summer Dresses']";
        string dressListLocator = "//*[@class='product-image-container']";
        string dressQuickViewLocator = "//*[@class='quick-view-mobile']";
        string ViewCartButtonLocator = "//*[@class='quick-view-mobile']";
        string accountInfoLocator = "//*[@title='Information']";
        string summerDressesDescLocator = "//div[@class='cat_desc']//span[(contains(.,'Summer Dresses'))]";


        IWebDriver driver = new FirefoxDriver();

 
        [Test]
        public void test()
        {
            driver.Url = "http://automationpractice.com/index.php";

            //login to site 
            IWebElement loginButton = driver.FindElement(By.XPath(LoginButtonlocator));
            loginButton.Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            IWebElement submitLoginButton = driver.FindElement(By.XPath(SubmitLoginButtonlocator));
            wait.Until(ExpectedConditions.ElementToBeClickable(submitLoginButton));
            IWebElement loginEmailForm = driver.FindElement(By.XPath(loginEmailFormlocator));
            IWebElement loginPassForm = driver.FindElement(By.XPath(loginPassFormlocator));
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

            //Quick View a dress
            IReadOnlyCollection<IWebElement> dressList = driver.FindElements(By.XPath(dressListLocator));
            //IWebElement firstDress = dressList[0];

            //add dress to cart

            //add another dress to cart

            //go to checkout


            //verify correct dress is selected on summary page

            //remove 2nd dress from cart

            //sign out


            driver.Close();
        }

    }
}