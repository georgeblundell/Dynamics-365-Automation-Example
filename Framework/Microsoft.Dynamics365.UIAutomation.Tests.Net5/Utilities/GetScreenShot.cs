using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dynamics365.UIAutomation.Tests.Utilities
{
    public class GetScreenShot
    {
        public static byte[] captureAsByteArray(IWebDriver driver)
        {
            return ((ITakesScreenshot)driver).GetScreenshot().AsByteArray;
        }
    }
}
