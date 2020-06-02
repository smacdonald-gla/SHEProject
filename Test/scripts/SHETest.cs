using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using SHEProject;
using SHEProject.src.pages;
using System;


namespace SHETest
{
    [TestFixture(typeof(ChromeDriver))]
    [TestFixture(typeof(FirefoxDriver))]

    class SHETest<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        readonly string loginEmail = "d1474341@urhen.com";
        readonly string loginPassword = "Password";
        readonly string homeURL = "http://automationpractice.com/index.php";
        private IWebDriver driver;

        [SetUp]
        public void CreateDriver()
        {
            this.driver = new TWebDriver();
        }

        [Test]
        public void Test()
        {
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            driver.Url = homeURL;

            //login to site
            HomePage home_page = new HomePage(driver);
            home_page.clickLogin();
            LoginPage login_page = new LoginPage(driver);
            login_page.UserLogin(loginEmail, loginPassword);
            Assert.IsTrue(login_page.IsAccountInfoVisible());

            //Navigate to Summer Dresses category
            ShoppingPage shopping_page = new ShoppingPage(driver);
            shopping_page.SelectDressCategory();
            shopping_page.SelectSummerDressCategory();
            Assert.IsTrue(shopping_page.IsSummerDressesDescVisible());

            //Quick-View a dress
            shopping_page.QuickViewDress(1);
            Assert.IsTrue(shopping_page.IsAddToCartButtonVisible());

            //Add first dress to cart
            shopping_page.AddToCart();

            //Add second dress to cart
            shopping_page.ClickContinueShoppingButton();
            shopping_page.QuickViewDress(3);
            Assert.IsTrue(shopping_page.IsAddToCartButtonVisible());
            shopping_page.AddToCart();

            //go to checkout
            shopping_page.ClickCheckoutButton();

            //verify correct dress is selected on summary page
            CartSummaryPage cart_summary_page = new CartSummaryPage(driver);
            Assert.IsTrue(cart_summary_page.IsItemPresentInBasket("demo_5"));
            Assert.IsTrue(cart_summary_page.IsItemPresentInBasket("demo_7"));

            //remove 2nd dress from cart
            cart_summary_page.RemoveItemFromCart("demo_7");

            //Check that only the first dress is remaining in the basket
            Assert.IsTrue(cart_summary_page.ReturnSizeOfBasket() == 1);
            Assert.IsTrue(cart_summary_page.IsItemPresentInBasket("demo_5"));

            //sign out
            home_page.clickLogout();
            Assert.IsTrue(home_page.IsLoginButtonVisible());

            driver.Close();
        }

    }
}