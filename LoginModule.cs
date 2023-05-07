using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

class LoginModule 
{
    public LoginModule(EdgeOptions options, EdgeDriver driver, WebDriverWait wait)
    {
        Console.WriteLine("Enter your ynot username");
        string? username = Console.ReadLine();
        Console.WriteLine("Enter your ynot password");
        string? password = Console.ReadLine();


        driver.Navigate().GoToUrl("https://www.ynotlms.com/");

        IWebElement usernameField = wait.Until(driver => driver.FindElement(By.Id("mat-input-0")));
        IWebElement passwordField = wait.Until(driver => driver.FindElement(By.Id("mat-input-1")));
        usernameField.SendKeys(username);
        passwordField.SendKeys(password);

        IWebElement loginButton = wait.Until(driver => driver.FindElement(By.CssSelector(".btn-block.btn-lg.m-t-20.m-b-20.mat-raised-button.mat-primary")));
        loginButton.Click();

        Thread.Sleep(1000);

        driver.Navigate().GoToUrl("https://www.ynotlms.com/leads?bucket_type_id=3&amp;");
    }
}