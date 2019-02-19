using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2BSeleniumTestingApp.Services.Common
{
    public static class Utils
    {
        public static string currrentURL(string serverType) {
            string url = null;
            switch (serverType.ToUpper())
            {
                case "DEV": url = "https://dev.cmacspro.com"; break;
                case "USSTAGING": url = "https://usstaging.cmacspro.com"; break;
                case "UAT": url = "https://uat.cmacspro.com"; break;
                default: url = "https://usstaging.cmacspro.com"; break;
            }
            return url;
        }
    }
}
