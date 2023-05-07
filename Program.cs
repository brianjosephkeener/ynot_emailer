using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Collections.Generic;

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
        while (true)
        {
            IWebElement allLeadsBx = wait.Until(driver => driver.FindElement(By.Id("allleads")));
            allLeadsBx.Click();
            IWebElement? email = driver.FindElement(By.XPath($"//*[text()='{emailChoice.selection}']"));
            js.ExecuteScript("arguments[0].setAttribute('class', 'screwynot')", email);
            js.ExecuteScript("arguments[0].setAttribute('style', 'position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%); display: block; font-size: 64px')", email);
            js.ExecuteScript("document.body.appendChild(arguments[0].parentNode.removeChild(arguments[0]))", email);
            Thread.Sleep(3000);
            email.Click(); 
            // ›
            Thread.Sleep(3000);
            try {
                IWebElement? nextPage = driver.FindElement(By.XPath("//*[text()='›']"));
                IReadOnlyCollection<IWebElement> navigationBar = driver.FindElements(By.ClassName("pagination"));
                IReadOnlyCollection<IWebElement> communicationFailedPopUp = driver.FindElements(By.XPath("//*[contains(text(),'Send Communication failed')]"));
                string ariaHiddenValue = nextPage.GetAttribute("aria-hidden");
                if(ariaHiddenValue == "true" || navigationBar.Count == 0 || communicationFailedPopUp.Count == 0)
                {
                    Console.WriteLine("All Done!");
                    Environment.Exit(0);
                }
                js.ExecuteScript("arguments[0].setAttribute('class', 'screwynot')", nextPage);
                js.ExecuteScript("arguments[0].setAttribute('style', 'position: fixed; top: 15%; left: 25%; transform: translate(-50%, -50%); display: block; font-size: 72px')", nextPage);
                js.ExecuteScript("document.body.appendChild(arguments[0].parentNode.removeChild(arguments[0]))", nextPage);

                nextPage.Click();
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("ALL DONE :)");
                driver.Quit();
            }
            catch (UnhandledAlertException)
            {
                try
                {
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