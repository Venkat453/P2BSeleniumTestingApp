using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2BSeleniumTestingApp.Dataobjects
{
    public class Landing
    {
        public void landing(out string Browser, out string Server, out string UserType, out string UserEmail, out string UserPassword, out bool IsCreate)
        {
            Console.WriteLine("Welcome to P2B testing. \n");

            Browser = null;
            Server = null;
            UserType = null;
            UserEmail = null;
            UserPassword = null;
            IsCreate = false;

            selectBrowser(out Browser);
            selectServer(out Server);
            selectUser(out UserType, out UserEmail, out UserPassword, out IsCreate);

        }

        private void selectBrowser(out string Browser)
        {
            Browser = null;
            Console.WriteLine("1. Chrome");
            Console.WriteLine("2. Mozilla FireFox");
            Console.WriteLine("3. Internet Explorer");
            Console.WriteLine("Please select the browser from above list :");
            int browser = Convert.ToInt32(Console.ReadLine());
            if (browser > 3 && browser < 0)
            {
                Console.WriteLine("Error - Entered option is not in the above list. So, Please type correct input.");
                selectBrowser(out Browser);
            }
            else
            {
                switch (browser)
                {
                    case 1: Browser = "CHROME"; break;
                    case 2: Browser = "MOZILLA"; break;
                    case 3: Browser = "IE"; break;
                    case 4: Browser = "SAFARI"; break;
                    default: Browser = "CHROME"; break;
                }
            }
            
        }
        private void selectServer(out string Server)
        {
            Server = null;
            Console.WriteLine("1. Dev server");
            Console.WriteLine("2. Usstaging server");
            Console.WriteLine("3. UAT server");
            Console.WriteLine("Please select the server from above list :");
            int server = Convert.ToInt32(Console.ReadLine());
            if (server > 3 && server < 0)
            {
                Console.WriteLine("Error - Entered option is not in the above list. So, Please type correct input.");
                selectBrowser(out Server);
            }
            else
            {
                switch (server)
                {
                    case 1: Server = "DEV"; break;
                    case 2: Server = "USSTAGING"; break;
                    case 3: Server = "UAT"; break;
                    default: Server = "USSTAGING"; break;
                }
            }
        }


        private void selectUser(out string UserType, out string UserEmail, out string UserPassword, out bool IsCreate)
        {
            UserType = null;
            UserEmail = null;
            UserPassword = null;
            IsCreate = false;

            Console.WriteLine("1. KeyAdmin");
            Console.WriteLine("2. KeyUser");
            Console.WriteLine("3. ProAdmin");
            Console.WriteLine("4. ProUser");
            Console.WriteLine("Please select the User from above list which you want to login :");
            int userType = Convert.ToInt32(Console.ReadLine());
            if (userType > 5 && userType < 0)
            {
                Console.WriteLine("Error - Entered option is not in the above list. So, Please type correct input.");
                selectUser(out UserType, out UserEmail, out UserPassword, out IsCreate);
            }
            else
            {
                switch (userType)
                {
                    case 1: UserType = "KEYADMIN"; break;
                    case 2: UserType = "KEYUSER"; break;
                    case 3: UserType = "PROADMIN"; break;
                    case 4: UserType = "PROUSER"; break;
                    case 5: UserType = "SYSTEMADMIN"; break;
                    default: UserType = "PROADMIN"; break;
                }
            }

            Console.WriteLine("Create a new one or Use existing one? (Y/N)");
            string credentials = Console.ReadLine();
            if (credentials.ToUpper().Equals("Y"))
            {
                IsCreate = false;
                Console.WriteLine("Please enter the email :");
                UserEmail = Console.ReadLine();
                Console.WriteLine("Please enter the password :");
                UserPassword = Console.ReadLine();
            }
            else if (credentials.ToUpper().Equals("N"))
            {
                IsCreate = true;
            }
            else
            {
                Console.WriteLine("Error - Entered option is not in the above list. So, Please type correct input.");
                selectUser(out UserType, out UserEmail, out UserPassword, out IsCreate);
            }
        }
    }
}
