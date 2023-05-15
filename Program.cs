using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

/*

TODO:

1. check page link 
<span class="page-link">200</span>

2. 
check 

last row of tbody 
OR 
<td class="Created"> Apr 16, 2018 8:15pm </td>


3. Once information is taken, put in next previous date to avoid overlap 
Example Apr 15, 2018
*/

class Program 
{
    static void Main(string[] args)
    {   
        EdgeOptions options = new EdgeOptions();
        options.AddArgument("ignore-certificate-errors"); 
        options.AddArgument("--inprivate");
        EdgeDriver driver = new EdgeDriver(options);

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(84000));
        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

        // Login Logic
        LoginModule login = new LoginModule(options, driver, wait);

        /*
        Homepage logic
        - Switch to Table View
        - Add Programs of choice
        - Date ranges to email
        - Current Lead Status
        - Targeted Location
        */
        ProgramSelectionBar programSelection = new ProgramSelectionBar();
        CurrentLeadStatus leadStatus = new CurrentLeadStatus();
        DateRange dateRange = new DateRange();
        EmailChoice emailChoice = new EmailChoice();
        LocationChoice locationChoice = new LocationChoice();

        // HOMEPAGE NAV

        IWebElement ClickFilterDropDown = wait.Until(driver => driver.FindElements(By.ClassName("expand"))[2]);
        ClickFilterDropDown.Click();

        // Location 
        IWebElement FilterByLocation = wait.Until(driver => driver.FindElement(By.Id("s2id_autogen3")));      
        foreach (string item in locationChoice.LocationSelected)
        {
            FilterByLocation.SendKeys(item);
            FilterByLocation.SendKeys(Keys.Enter);
        }
        // Program Input
        Thread.Sleep(1000);
        IWebElement SearchByProgramInput = wait.Until(driver => driver.FindElement(By.Id("s2id_autogen4")));
        foreach (string item in programSelection.ProgramsSelected)
        {
            SearchByProgramInput.SendKeys(item);
            SearchByProgramInput.SendKeys(Keys.Enter);
        }

        // Priority to All Buckets
        IWebElement? Bucket = driver.FindElement(By.XPath("//*[text()='Priority']"));
        Bucket?.Click();
        IWebElement? Bucket_DropDown = driver.FindElement(By.XPath("//*[text()='All Buckets']"));
        Bucket_DropDown?.Click();

        IWebElement FilterByLeadStatus = wait.Until(driver => driver.FindElement(By.Id("s2id_autogen11")));      
        foreach (string item in leadStatus.StatusSelected)
        {
            FilterByLeadStatus.SendKeys(item);
            FilterByLeadStatus.SendKeys(Keys.Enter);
        }



        IWebElement DateRange = wait.Until(driver => driver.FindElement(By.Id("form-date-range")));
        DateRange.SendKeys(dateRange.date);
        Thread.Sleep(10000);
        DateRange.SendKeys(Keys.Enter);
//  wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("div.my-class")));
        bool alertPresent = false;
        bool emailClicked = false;
        while (true)
        {
            IWebElement allLeadsBx = wait.Until(driver => driver.FindElement(By.Id("allleads")));
            allLeadsBx.Click();
            IWebElement? email = driver.FindElement(By.XPath($"//*[text()='{emailChoice.selection}']"));
            js.ExecuteScript("arguments[0].setAttribute('class', 'screwynot')", email);
            js.ExecuteScript("arguments[0].setAttribute('style', 'position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%); display: block; font-size: 64px')", email);
            js.ExecuteScript("document.body.appendChild(arguments[0].parentNode.removeChild(arguments[0]))", email);
            Thread.Sleep(3000);
            // email.Click();
            // ›
            // Thread.Sleep(3000);
            try {
                
                if(emailClicked == false)
                {
                    // Console.WriteLine("SENDING EMAIL");
                    email.Click();
                    emailClicked = true;
                }
                Thread.Sleep(3000);
                IWebElement? nextPage = driver.FindElement(By.XPath("//*[text()='›']"));
                IReadOnlyCollection<IWebElement> navigationBar = driver.FindElements(By.ClassName("pagination"));
                IReadOnlyCollection<IWebElement> communicationFailedPopUp = driver.FindElements(By.XPath("//*[contains(text(),'Send Communication failed')]"));
                IReadOnlyCollection<IWebElement> filterDangerPopUp = driver.FindElements(By.XPath("//*[contains(text(),'Invalid filters, please check your search criteria.')]"));
                string ariaHiddenValue = nextPage.GetAttribute("aria-hidden");
                if(ariaHiddenValue == "true" || navigationBar.Count == 0 || communicationFailedPopUp.Count == 1 || filterDangerPopUp.Count == 1)
                {
                    // Console.WriteLine("ariaHiddenValue = " + ariaHiddenValue);
                    // Console.WriteLine($"navigationBar.Count = {navigationBar.Count}");
                    // Console.WriteLine($"communicationFailedPopUp.Count = {communicationFailedPopUp.Count}");
                    Console.WriteLine("All Done!");
                    Environment.Exit(0);
                }
                js.ExecuteScript("arguments[0].setAttribute('class', 'screwynot')", nextPage);
                js.ExecuteScript("arguments[0].setAttribute('style', 'position: fixed; top: 15%; left: 25%; transform: translate(-50%, -50%); display: block; font-size: 72px')", nextPage);
                js.ExecuteScript("document.body.appendChild(arguments[0].parentNode.removeChild(arguments[0]))", nextPage);

                nextPage.Click();
                wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                emailClicked = false;
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("All Done!");
                driver.Quit();
            }
            catch (UnhandledAlertException)
            {
                try
                {
                    // get rid of alert
                    // Console.WriteLine("UnhandledAlertException");
                    alertPresent = true;
                    driver.SwitchTo().Alert().Accept();

                }
                catch (NoAlertPresentException)
                {
                    // Ignore if the alert is already closed
                }
            }
            if (!alertPresent)
            {
                // Send email
            }
            alertPresent = false;
        }
    }
}


// // Remove all other classes from the element
// ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].setAttribute('class', 'your-class-name')", element);

// Apply CSS styles to center the element on the page and display its text
// ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].setAttribute('style', 'position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%); z-index: 9999; background-color: white; padding: 10px; font-size: 20px;'); arguments[0].textContent = 'Your text goes here.';", element);
