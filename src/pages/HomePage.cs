﻿using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SHEProject.src.pages
{
    class HomePage
    {
        readonly string LoginButtonLocator = "//a[@class='login']";
        readonly string logoutButtonLocator = "//a[@class='logout']";
        private IWebDriver driver;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public void clickLogin()
        {
            IWebElement loginButton = driver.FindElement(By.XPath(LoginButtonLocator));
            loginButton.Click();
        }

        public void clickLogout()
        {
            IWebElement logoutButton = driver.FindElement(By.XPath(logoutButtonLocator));
            logoutButton.Click();
        }

        public bool IsLoginButtonVisible()
        {
            return driver.FindElement(By.XPath(LoginButtonLocator)).Displayed;
        }

    }
}
