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
    internal class FunctionalTests
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
        public void dropdown()

        {

            IWebElement dropdown = driver.FindElement(By.CssSelector("select.form-control"));

            SelectElement s = new SelectElement(dropdown);
            s.SelectByText("Teacher");
            s.SelectByValue("consult");
            s.SelectByIndex(1);

            IList<IWebElement> rdos = driver.FindElements(By.CssSelector("input[type='radio']"));

            foreach (IWebElement radioButton in rdos)
            {
                if (radioButton.GetDomAttribute("value").Equals("user"))

                {

                    radioButton.Click();
                }

            }
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("okayBtn")));


            driver.FindElement(By.Id("okayBtn")).Click();
            bool result = driver.FindElement(By.Id("usertype")).Selected;

            //Assert.That(result, Is.True);


        }

        [TearDown]
        public void closeBrowser()
        {
            Thread.Sleep(3000);
            driver.Quit();
        }
    }
}
