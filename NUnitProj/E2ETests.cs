using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace NUnitProj
{
    internal class E2ETests
    {
#pragma warning disable NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method
        IWebDriver driver;
#pragma warning restore NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method
        [SetUp]

        public void StartBrowser()

        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";


        }

        [Test]
        public void EndToEndFlow()

        {

            String[] expectedProducts = { "iphone X", "Blackberry" };
            String[] actualProducts = new string[2];
            driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            driver.FindElement(By.Name("password")).SendKeys("learning");
            driver.FindElement(By.XPath("//div[@class='form-group'][5]/label/span/input")).Click();
            driver.FindElement(By.XPath("//input[@value='Sign In']")).Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));

            IList<IWebElement> products = driver.FindElements(By.TagName("app-card"));

            foreach (IWebElement product in products)
            {

                if (expectedProducts.Contains(product.FindElement(By.CssSelector(".card-title a")).Text))
                {
                    product.FindElement(By.CssSelector(".card-footer button")).Click();
                }
                TestContext.WriteLine(product.FindElement(By.CssSelector(".card-title a")).Text);

            }
            driver.FindElement(By.PartialLinkText("Checkout")).Click();
            IList<IWebElement> checkoutCards = driver.FindElements(By.CssSelector("h4 a"));

            for (int i = 0; i < checkoutCards.Count; i++)

            {
                actualProducts[i] = checkoutCards[i].Text;
            }

            Assert.That(expectedProducts, Is.EqualTo(actualProducts));            

            driver.FindElement(By.CssSelector(".btn-success")).Click();

            driver.FindElement(By.Id("country")).SendKeys("ind");

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("India")));
            driver.FindElement(By.LinkText("India")).Click();


            driver.FindElement(By.CssSelector("label[for*='checkbox2']")).Click();

            bool isSelected = driver.FindElement(By.CssSelector("#checkbox2")).Selected;

            Assert.That(isSelected, Is.True);

            driver.FindElement(By.CssSelector("[value='Purchase']")).Click();
            String confirText = driver.FindElement(By.CssSelector(".alert-success")).Text;

            StringAssert.Contains("Success", confirText);

        }

        [TearDown]
        public void CloseBrowser()
        {
            Thread.Sleep(1000);
            driver.Quit();
        }

    }
}
