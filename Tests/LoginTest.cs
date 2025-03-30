using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Serilog;
using saucedemo.Pages;
using FluentAssertions;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.BiDi.Communication;

namespace saucedemo.Tests
{
    [TestClass]
    public class Tests
    {
        private IWebDriver driver;
        private LoginPage loginPage;

        private static IWebDriver WebDriverFactory(string browser)
        {
            switch (browser.ToLower())
            {
                case "firefox":
                    Log.Information("Starting FirefoxDriver.");
                    return new FirefoxDriver();
                case "edge":
                    Log.Information("Starting EdgeDriver.");
                    return new EdgeDriver();
                default:
                    throw new ArgumentException($"Browser '{browser}' is not supported.");
            }
        }

        [TestCleanup]
        public void TearDown()
        {
            Log.Information("Closing browser.");
            driver.Close();
        }

        [DataTestMethod]
        [DataRow("firefox", "", "", "Username is required", false)]
        [DataRow("firefox","standard_user", "", "Password is required", false)]
        [DataRow("firefox", "standard_user", "secret_sauce", "", true)]
        [DataRow("edge", "", "", "Username is required", false)]
        [DataRow("edge", "standard_user", "", "Password is required", false)]
        [DataRow("edge", "standard_user", "secret_sauce", "", true)]
        [TestMethod]
        public void LoginTest(string browser, string username, string password, string expectedErrorText, bool isSuccessExpected)
        {

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            Log.Information("WebDriver setting up.");

            driver = WebDriverFactory(browser);
            driver.Manage().Window.Maximize();

            loginPage = new LoginPage(driver);

            loginPage.OpenLoginPage();
            loginPage.EnterUserName(username);
            loginPage.EnterPassword(password);
            loginPage.ClickLogin();

            if (isSuccessExpected)
            {
                Log.Information("Verifying successful login by checking page title.");

                //FluentAssertion
                driver.Title.Should().Contain("Swag Labs");
            }

            else
            {
                string errorMsg = loginPage.GetErrorMsg();

                Log.Information($"Verifying error message. Expected error message shoud contain: '{expectedErrorText}'", expectedErrorText);

                //FluentAssertion
                errorMsg.Should().Contain(expectedErrorText);
            }
        }
    }
}