using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace P2BSeleniumTestingApp
{
    public class Login
    {
        private IWebDriver driver;
        private string url;
        private bool acceptNextAlert = true;

        public Login( IWebDriver _driver, string Server) {
            this.driver = _driver;
            this.url = Services.Common.Utils.currrentURL(Server);
        }

        public void login(string UserType, string UserEmail, string UserPassword, bool IsCreate)
        {
            if (IsCreate && UserEmail == null && UserPassword == null)
            {
                switch (UserType.ToUpper())
                {
                    case "KEYADMIN":
                        //Create Key admin and login with that.
                        break;
                    case "KEYUSER":
                        //Create Key user and login with that.
                        break;
                    case "PROADMIN":
                        //Create Pro admin and login with that.
                        break;
                    case "PROUSER":
                        //Create Pro user and login with that.
                        break;
                    case "SYSTEMADMIN":
                        userLogin("systemadmin@cmacspro.com", "Password1234!");
                        break;
                    default:
                        //Create Pro admin and login with that.
                        break;
                }
            }
            else
            {
                userLogin(UserEmail, UserPassword);
            }


        }
        private void userLogin(string email, string password)
        {
            // Open URL;
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(url + "/Account/Login");
            WaitForPresentElement(By.Id("Email"), driver, 60);
            Thread.Sleep(200);
            SetMethods.EnterText(driver, "Email", email, "id");
            SetMethods.EnterText(driver, "Password", password, "id");
            SetMethods.Click(driver, "//input[@value='Log in']", "xpath");
            Thread.Sleep(1000);
            SetMethods.Click(driver, "//a[@id='layout_home_a']/span[2]", "xpath");

            bool requirePasswordReset;
            try
            {
                driver.FindElement(By.XPath("//section[@id='loginForm']/form/div/div/ul/li")).Text.Equals("Invalid login attempt!", StringComparison.OrdinalIgnoreCase);
                requirePasswordReset = true;
            }
            catch
            {
                requirePasswordReset = false;
            }

            if (requirePasswordReset)
            {
                AutoResetPassword(email, password);
            }

            WaitForPresentElement(By.XPath("//div[@id='QUICKBUTTONSWidget']/div/div[2]/div/div[2]"), driver, 60);
        }

        private void AutoResetPassword(string login, string password = "Password1234!")
        {
            // login with default password

            driver.FindElement(By.Id("Password")).Click();
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("Plan!F@b");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();

            //wait for reset page to load
            WaitForPresentElement(By.Id("OldPassword"), driver, 15);

            driver.FindElement(By.Id("OldPassword")).Clear();
            driver.FindElement(By.Id("OldPassword")).SendKeys("Plan!F@b");
            driver.FindElement(By.Id("NewPassword")).Clear();
            driver.FindElement(By.Id("NewPassword")).SendKeys(password);
            driver.FindElement(By.Id("ConfirmPassword")).Click();
            driver.FindElement(By.Id("ConfirmPassword")).Clear();
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys(password);
            driver.FindElement(By.XPath("//form[@id='ChangeDefaultPassword']/div[2]/div[6]/div/div/div/span/span/span[2]")).Click();
            WaitForPresentElement(By.XPath("//ul[@id='TimeZone_listbox']/li[21]"), driver, 15);
            driver.FindElement(By.XPath("//ul[@id='TimeZone_listbox']/li[21]")).Click();
            driver.FindElement(By.Id("submitPassword")).Click();
        }

        public static bool IsElementPresent(By by, IWebDriver driver)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        public IWebElement WaitForPresentElement(By by, int timeout = 5)
        {
            return WaitForPresentElement(by, driver, timeout);
        }

        public static IWebElement WaitForPresentElement(By by, IWebDriver driver, int timeout = 5)
        {
            for (int second = 0; ; second++)
            {
                if (second >= timeout)
                    Assert.Fail("timeout");
                try
                {
                    if (IsElementPresent(by, driver)) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            WaitForLoadingOverlays(driver, timeout);
            return driver.FindElement(by);
        }
        public void WaitForLoadingOverlays(int timeout = 5)
        {
            WaitForLoadingOverlays(driver, timeout);
        }

        public static void WaitForLoadingOverlays(IWebDriver driver, int timeout = 5)
        {

            List<IWebElement> loadingOLelements = driver.FindElements(By.ClassName("loadingoverlay")).ToList();
            loadingOLelements.ForEach(e => WaitForHidingOfElement(e, driver, timeout));
        }

        public static void WaitForHidingOfElement(IWebElement element, IWebDriver driver, int timeout = 5)
        {
            for (int second = 0; ; second++)
            {
                if (second >= timeout)
                    Assert.Fail("timeout");
                try
                {
                    if (!(element.Displayed && element.Enabled)) break;
                }
                catch (StaleElementReferenceException)
                {
                    break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

        }

        public void NavigateIfNeeded(string targetUrl)
        {
            NavigateIfNeeded(driver, targetUrl);
        }

        public static void NavigateIfNeeded(IWebDriver driver, string targetUrl)
        {
            if (driver.Url != targetUrl)
            {
                driver.Navigate().GoToUrl(targetUrl);
            }
        }

        public static IWebElement GetParent(IWebElement e)
        {
            return e.FindElement(By.XPath(".."));
        }

        public void RightClick(IWebElement e)
        {
            RightClick(driver, e);
        }

        public static void RightClick(IWebDriver driver, IWebElement e)
        {
            Actions RClick = new Actions(driver);
            RClick.ContextClick(e).Build().Perform();
            //RClick.ContextClick(e).Build().Perform();
        }

        public void ScrollToElement(IWebElement e)
        {
            ScrollToElement(driver, e);
        }

        public static void ScrollToElement(IWebDriver driver, IWebElement e)
        {
            Actions scrollTo = new Actions(driver);
            scrollTo.MoveToElement(e).Build().Perform();
        }

        private static string validateEmail(string adminLogin)
        {
            try
            {
                adminLogin = (new System.Net.Mail.MailAddress(adminLogin)).Address;
            }
            catch
            {
                adminLogin = null;
            }
            return adminLogin;
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }

    }
}
