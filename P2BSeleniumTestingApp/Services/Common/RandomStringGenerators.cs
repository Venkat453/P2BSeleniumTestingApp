using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2BSeleniumTestingApp
{
    public class RandomStringGenerators : IDisposable
    {
        private IWebDriver driver;
        private bool acceptNextAlert = true;
        private const string pool_numbers = "0123456789";
        private const string pool_alpha_small = "abcdefghijklmnopqrstuvwxyz";

        public RandomStringGenerators(IWebDriver driver)
        {
            this.driver = driver;
        }

        public static string randomEmailGenerator(int size)
        {
            char[] chars = new char[size];
            Random random = new Random();
            string alphanum = pool_alpha_small + pool_numbers;

            for (var i = 0; i < size; i++)
            {
                chars[i] = alphanum[random.Next(0, alphanum.Length)];
            }
            var email = new string(chars);
            var emailType = "@gmail.com";
            return (email + emailType);
        }
        public static string randomStringGenerator(int size, string type = null)
        {
            char[] chars = new char[size];
            Random random = new Random();
            switch (type.ToLower())
            {
                case "small":
                    for (var i = 0; i < size; i++)
                    {
                        chars[i] = pool_alpha_small[random.Next(0, pool_alpha_small.Length)];
                    }
                    break;
                case "caps":
                    for (var i = 0; i < size; i++)
                    {
                        chars[i] = pool_alpha_small.ToUpper()[random.Next(0, pool_alpha_small.Length)];
                    }
                    break;
                case "numeric":
                    for (var i = 0; i < size; i++)
                    {
                        chars[i] = pool_numbers[random.Next(0, pool_numbers.Length)];
                    }
                    break;
                case "aplhanumeric":
                    string alphanum = pool_alpha_small + pool_numbers;
                    for (var i = 0; i < size; i++)
                    {
                        chars[i] = alphanum[random.Next(0, alphanum.Length)];
                    }
                    break;
                
                default:
                    break;
            }
            return new string(chars);
        }


        private bool IsElementPresent(By by)
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

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
