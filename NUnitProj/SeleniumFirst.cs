using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace NUnitProj
{
    internal class SeleniumFirst
    {
#pragma warning disable NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method
        IWebDriver driver;
#pragma warning restore NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method
        [SetUp]
        public void StartBrowser()
        {
            /*new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();*/

            //new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
            driver = new EdgeDriver();
        }

        [Test]
        public void Test1() 
        {
            //driver.Url = "https://rahulshettyacademy.com";
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            driver.Manage().Window.Maximize();
            TestContext.WriteLine(driver.Title);
            TestContext.WriteLine(driver.Url);
            TestContext.WriteLine(driver.PageSource);
        }

        [TearDown]
        public void closeBrowser()
        {
            Thread.Sleep(5000);
            driver.Quit();
        }


    }
}
