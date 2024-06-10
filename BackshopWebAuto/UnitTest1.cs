using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Security.Cryptography;
using System.Threading;
using static OpenQA.Selenium.Support.UI.WebDriverWait;
using static SeleniumExtras.WaitHelpers.ExpectedConditions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace BackshopWebAuto
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int waitingTime = 2000;

            // Testing by name in inspect element
            // Name is q in HTML
            // Google search button name
            By googleSearchBar = By.Name("q"), googleSearchButton = By.Name("btnK");

            // Address of dev website
            string bbAddress = "https://acore-app-backshop-dev.azurewebsites.net/";

            // Acore employee login button
            By acoreButton = By.Id("btnSubmitEmployeeLogin");

            // Acore email
            By emailButton = By.Id("i0116");

            // Next selection button
            By nextButton = By.Id("idSIButton9");

            // Password button
            By passwordButton = By.Id("i0118");

            By googleResultText = By.XPath(".//a//h3[text()='ACORE Capital: Welcome']"); // h3 inside a;

            By callButton = By.XPath(".//div[@class = 'row tile']//div[@data-value = 'TwoWayVoiceMobile']");

            // Report header tab
            By reports = By.XPath(".//div//a[@href = '/acore/pages/reports/main.aspx']");

            // ACORE Capital Pipeline Report (new)
            By pipelineRep = By.XPath("//*[@id=\"RPT-1\"]/div[1]");

            // Drop down menu option
            By investor = By.Id("INVESTORID");
            

            /** Start of navigation **/
            IWebDriver webDriver = new ChromeDriver();

            Thread.Sleep(waitingTime);

            webDriver.Navigate().GoToUrl("https://www.google.com");

            Thread.Sleep(waitingTime);

            webDriver.Manage().Window.Maximize();

            Thread.Sleep(waitingTime);

            // Input ACORE Capital into the search bar
            webDriver.FindElement(googleSearchBar).SendKeys("ACORE Capital");

            Thread.Sleep(waitingTime);

            // Click/Enter query or search
            webDriver.FindElement(googleSearchButton).Click();

            // Give time to see
            Thread.Sleep(waitingTime);

            // Compare result to what we are actually seeing
            var actualResultText = webDriver.FindElement(googleResultText);

            Assert.IsTrue(actualResultText.Text.Equals("ACORE Capital: Welcome"));

            webDriver.Navigate().GoToUrl(bbAddress);

            Thread.Sleep(waitingTime);

            webDriver.FindElement(acoreButton).Click();

            // Give time to see
            Thread.Sleep(waitingTime);

            webDriver.FindElement(emailButton).SendKeys("jbass@acorecapital.com"); ;

            Thread.Sleep(waitingTime);

            // Go to the next button
            webDriver.FindElement(nextButton).Click();

            Thread.Sleep(waitingTime);

            webDriver.FindElement(passwordButton).SendKeys("!Getmein22");

            Thread.Sleep(waitingTime);

            webDriver.FindElement(nextButton).Click();

            Thread.Sleep(waitingTime);

            // Click call button
            webDriver.FindElement(callButton).Click();

            Thread.Sleep(waitingTime);

            // Special Consideration for yes button 
            // Mot available to click until time elapses
            var wait = new WebDriverWait(webDriver, new TimeSpan(0, 0, 30));
            By yesButton = By.XPath(".//div//input[@class = 'win-button button_primary high-contrast-overrides button ext-button primary ext-primary']");
            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(".//div//input[@class = 'win-button button_primary high-contrast-overrides button ext-button primary ext-primary']")));
           
            webDriver.FindElement(yesButton).Click();

            // Website takes time to load so wait 
            int webWait = 15000;
            Thread.Sleep(webWait);

            webDriver.FindElement(reports).Click();

            Thread.Sleep(waitingTime);

            webDriver.FindElement(pipelineRep).Click();

            Thread.Sleep(waitingTime);

            webDriver.FindElement(investor).SendKeys("Equitrust");



            webDriver.Quit();
        }
    }
}
