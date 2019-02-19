using log4net;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2BSeleniumTestingApp
{
    public class SetMethods
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));
        
        // Enter text for input boxes.
        public static void EnterText(IWebDriver driver, string element, string value, string elememtType)
        {
            try
            {
                if (elememtType.ToLower() == "id")
                {
                    driver.FindElement(By.Id(element.ToString())).SendKeys(value);
                }
                if (elememtType.ToLower() == "name")
                {
                    driver.FindElement(By.Name(element.ToString())).SendKeys(value);
                }
                if (elememtType.ToLower() == "xpath")
                {
                    driver.FindElement(By.XPath(element.ToString())).SendKeys(value);
                }
            }
            catch (Exception e)
            {
                log.Error(e);
            }
        }

        // Click on a button, checkbox or option etc.
        public static void Click(IWebDriver driver, string element, string elememtType)
        {
            try
            {
                if (elememtType.ToLower() == "id")
                {
                    driver.FindElement(By.Id(element.ToString())).Click();
                }
                if (elememtType.ToLower() == "name")
                {
                    driver.FindElement(By.Name(element.ToString())).Click();
                }
                if (elememtType.ToLower() == "xpath")
                {
                    driver.FindElement(By.XPath(element.ToString())).Click();
                }
                if (elememtType.ToLower() == "link")
                {
                    driver.FindElement(By.LinkText(element.ToString())).Click();
                }
            }
            catch (Exception e)
            {
                log.Error(e);
            }
        }

        // Clear the inputs.
        public static void Clear(IWebDriver driver, string element, string elememtType)
        {
            try
            {
                if (elememtType.ToLower() == "id")
                {
                    driver.FindElement(By.Id(element)).Clear();
                }
                if (elememtType.ToLower() == "name")
                {
                    driver.FindElement(By.Name(element)).Clear();
                }
                if (elememtType.ToLower() == "xpath")
                {
                    driver.FindElement(By.XPath(element)).Clear();
                }
            }
            catch (Exception e)
            {
                log.Error(e);
            }
        }



    }
}
