using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;

namespace SHEProject
{
    class LoginPage
    {
        readonly string SubmitLoginButtonLocator = "//button[@id='SubmitLogin']";
        readonly string loginEmailFormLocator = "//input[@id='email']";
        readonly string loginPassFormLocator = "//input[@id='passwd']";
        readonly string accountInfoLocator = "//*[@title='Information']";
        private IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public void UserLogin(string user, string pass)
        {

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            IWebElement submitLoginButton = driver.FindElement(By.XPath(SubmitLoginButtonLocator));
            wait.Until(ExpectedConditions.ElementToBeClickable(submitLoginButton));
            IWebElement loginEmailForm = driver.FindElement(By.XPath(loginEmailFormLocator));
            IWebElement loginPassForm = driver.FindElement(By.XPath(loginPassFormLocator));
            loginEmailForm.SendKeys(user);
            loginPassForm.SendKeys(pass);
            submitLoginButton.Click();
        }

    }
}
