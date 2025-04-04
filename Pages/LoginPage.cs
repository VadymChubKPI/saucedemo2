using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Serilog;

namespace saucedemo.Pages
{
    public class LoginPage(IWebDriver driver)
    {
        private readonly IWebDriver driver = driver;
        private readonly string LogPageUrl = "https://www.saucedemo.com/";

        private IWebElement UserNameField => driver.FindElement(By.Id("user-name"));
        private IWebElement PasswordField => driver.FindElement(By.Id("password"));
        private IWebElement LoginBtn => driver.FindElement(By.Id("login-button"));
        private IWebElement ErrorMsg => driver.FindElement(By.CssSelector("div.error-message-container.error"));

        public void OpenLoginPage()
        {
            Log.Information($"Navigating to {LogPageUrl}", LogPageUrl);
            driver.Navigate().GoToUrl(LogPageUrl);
        }

        public void EnterUserName(string username)
        {
            UserNameField.SendKeys(username);
            Log.Information($"Entering username: {username}", username);
        }
        public void EnterPassword(string password)
        {
            Log.Information($"Entering password: {password}", password);
            PasswordField.SendKeys(password);
        }

        public void ClickLogin()
        {
            Log.Information("Clicking Login button.");
            LoginBtn.Click();
        }

        public string GetErrorMsg()
        {
            string errorText = ErrorMsg.Text;
            Log.Information("Error message displayed: {errorText}", errorText);
            return ErrorMsg.Text;
        }

        public void ClearUserName()
        {
            UserNameField.Clear();
            Log.Information("Username field was cleared.");
        }
        public void CleaPassword()
        {
            PasswordField.Clear();
            Log.Information("Password field was cleared.");
        }
    }
}
