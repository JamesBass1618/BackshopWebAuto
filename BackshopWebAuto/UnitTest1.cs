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
using System.Collections.Generic;


namespace BackshopWebAuto
{
    [TestClass]
    public class UnitTest1
    {
        class InputInfo
        {
            public string Investor {  get; set; }
            public string Date {  get; set; }
            public string Office {  get; set; }
            public string ExportType { get; set; }
            public bool IsShortForm { get; set; }
        }

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


            /** Initial Login Begin **/

            webDriver.FindElement(emailButton).SendKeys("jbass@acorecapital.com"); ;

            Thread.Sleep(waitingTime);

            // Go to the next button
            webDriver.FindElement(nextButton).Click();

            Thread.Sleep(waitingTime);

            webDriver.FindElement(passwordButton).SendKeys("!Getmein22");

            Thread.Sleep(waitingTime);

            webDriver.FindElement(nextButton).Click();

            Thread.Sleep(waitingTime);

            // Select    call button
            webDriver.FindElement(callButton).Click();

            Thread.Sleep(waitingTime);

            // Special Consideration for yes button 
            // Mot available to click until time elapses
            var wait = new WebDriverWait(webDriver, new TimeSpan(0, 0, 30));
            By yesButton = By.XPath(".//div//input[@class = 'win-button button_primary high-contrast-overrides button ext-button primary ext-primary']");
            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(".//div//input[@class = 'win-button button_primary high-contrast-overrides button ext-button primary ext-primary']")));
           
            webDriver.FindElement(yesButton).Click();

            /** Initial Login End **/


            // Website takes time to load so wait 
            int webWait = 15000;
            Thread.Sleep(webWait);

            webDriver.FindElement(reports).Click();

            Thread.Sleep(waitingTime);

            // Choose pipeline report
            webDriver.FindElement(pipelineRep).Click();

            Thread.Sleep(waitingTime);

            // Start of JSON Parsing
            string json = System.IO.File.ReadAllText(@"C:\Users\JamesBass\Documents\Selenium\BackshopWebAuto\BackshopWebAuto\inputInfo.json");
            InputInfo deserialized = JsonConvert.DeserializeObject<InputInfo>(json);


            // Extract all the information from the JSON File
            string investor = deserialized.Investor;

            string date = deserialized.Date;

            string office = deserialized.Office;

            string exportType = deserialized.ExportType;

            bool isShortForm = deserialized.IsShortForm;

            // Drop down menu options

            // Select the investor using SelectElement
            SelectElement investorSelector = new SelectElement(webDriver.FindElement(By.Id("INVESTORID")));
            investorSelector.SelectByValue("10");

            Thread.Sleep(waitingTime);

            // Select the office using SelectElement
            SelectElement selectElement = new SelectElement(webDriver.FindElement(By.Id("OFFICEREGIONID")));
            selectElement.SelectByText("Office: DAL");

            Thread.Sleep(waitingTime);

            // Select the date
            By chooseDate = By.Id("EFFECTIVEDATE");
            webDriver.FindElement(chooseDate).Clear();
            webDriver.FindElement(chooseDate).SendKeys(date);

            Thread.Sleep(waitingTime);

            // Select the export type
            if (isShortForm)
            {
                By form = By.Id("TWOTABSONLY_");
                webDriver.FindElement(form).Click();
            }

            // Else for uniformity/readability
            else
            {
                // Do nothing, button already unchecked
            }

            Thread.Sleep(waitingTime);

            By export;

            if (exportType.Equals("Excel"))
            {
                export = By.Id("EXPORTTYPE_EXCEL");
                webDriver.FindElement(export).Click();
            }

            else
            {
                export = By.Id("EXPORTTYPE_PDF");
                webDriver.FindElement(export).Click();
 
            }

            //  Run the report
            Thread.Sleep(waitingTime);

            By buttonReport = By.Id("buttonRunReport_1");
            webDriver.FindElement(buttonReport).Click();

            Thread.Sleep(waitingTime);

            // Give time for report to download
            Thread.Sleep(20000);

            webDriver.Quit();
        }
    }
}
