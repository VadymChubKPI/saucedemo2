# saucedemo
This project contains autotests for SauceDemo (https://www.saucedemo.com/)

The tests are written in C# using Selenium WebDriver, MSTest, FluentAssertions, and Serilog. 

The tests are designed to check the login form in three scenarios (UC-1, UC-2, UC-3) in two browsers (Firefox and Edge):

* UC-1: Empty login and password

* UC-2: Valid login, no password

* UC-3: Valid login and password

## Details of main files

### LoginPage.cs
Page Object for the saucedemo.com page.
* Contains methods for interaction: entering a login, password, clicking the Login button, reading an error.
* Uses Serilog for logging actions.

### LoginTest.cs

The main class with MSTest tests.
* Contains a dataprovider method that executes tests with different sets of parameters: browser, login, password, expected error message, etc.
* The tests check for an error if there is no data or a successful login with valid data.
* The WebDriverFactory() is used to initialise the FirefoxDriver/EdgeDriver drivers.
### AssemblyInfo.cs
* Contains the <mark>[assembly: Parallelize(...)]</mark> attribute, which allows you to execute tests in parallel.
* Uses the <mark>[AssemblyInitialize]</mark> and <mark>[AssemblyCleanup]</mark> attributes to globally initialise and close Serilog logging.

## Requirements:

* .NET SDK (version 6.0 or higher)
* Firefox and Edge (to work with the appropriate drivers)
* GeckoDriver (for Firefox)
* EdgeDriver
* Selenium WebDriver and Selenium Support
* MSTest
* FluentAssertions
* Serilog + Serilog.Sinks.Console
