using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Support.UI;

namespace NUnitProj
{
    internal class Locators
    {

#pragma warning disable NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method
        IWebDriver driver;
#pragma warning restore NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method

        [SetUp]

        public void StartBrowser()

        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            //Implicit wait 5seconds can be decalred globally
            //3 seconds
          
            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            


        }

        [Test]
        public void LocatorsIdentification()

        {

            driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            driver.FindElement(By.Id("username")).Clear();
            driver.FindElement(By.Id("username")).SendKeys("rahulshetty");
            driver.FindElement(By.Name("password")).SendKeys("123456");
            //css selector & xpath
            //  tagname[attribute ='value']
            //    #id  #terms  - class name -> css .classname
            //    driver.FindElement(By.CssSelector("input[value='Sign In']")).Click();

            //    //tagName[@attribute = 'value']

            // CSS - .text-info span:nth-child(1) input
            //xpath - //label[@class='text-info']/span/input
            
            driver.FindElement(By.XPath("//label[@for='terms']/span/input")).Click(); //div[@class='form-group'][5]/label/span/input

            driver.FindElement(By.XPath("//input[@value='Sign In']")).Click();

            WebDriverWait w = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            w.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .TextToBePresentInElementValue(driver.FindElement(By.CssSelector("#signInBtn")), "Sign In"));

            IWebElement link = driver.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material"));
            String hrefAttr = link.GetDomAttribute("href");
            String expectedUrl = "https://rahulshettyacademy.com/documents-request";
           
            Assert.That(expectedUrl, Is.EqualTo(hrefAttr));
        }


        [TearDown]
        public void closeBrowser()
        {
            //Thread.Sleep(5000);
            driver.Quit();


        }
    }
    }
