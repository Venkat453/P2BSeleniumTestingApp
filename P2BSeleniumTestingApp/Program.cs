using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using P2BSeleniumTestingApp.Dataobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2BSeleniumTestingApp
{
    class Program
    {
        public static IWebDriver _driver;
        public static string Browser;
        public static string Server;
        public static string UserType;
        public static string UserEmail;
        public static string UserPassword;
        public static bool IsCreate;

        public static void Main(string[] args)
        {
            Landing lndg = new Landing();
            lndg.landing(out Browser, out Server, out UserType, out UserEmail, out UserPassword, out IsCreate);
            switch (Browser.ToUpper())
            {
                case "CHROME": _driver = new ChromeDriver(); break;
                case "MOZILLA": _driver = new FirefoxDriver(); break;
                case "IE": _driver = new InternetExplorerDriver(); break;
                default: _driver = new ChromeDriver(); break;
            }

            Login lgn = new Login(_driver, Server);
            lgn.login(UserType, UserEmail, UserPassword, IsCreate);
        }
    }
}
