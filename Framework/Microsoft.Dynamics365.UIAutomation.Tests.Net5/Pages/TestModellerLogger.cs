using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Dynamics365.UIAutomation.Browser;
using Microsoft.Dynamics365.UIAutomation.Tests.Utilities;
using Pages;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;

namespace Microsoft.Dynamics365.UIAutomation.Tests.Net5.Pages
{
    public class TestModellerLogger : BasePage
    {
        public TestModellerLogger(WebClient browser)
            : base(browser)
        {

        }

        public void LogMessage(String name, String desc)
        {
            Microsoft.Dynamics365.UIAutomation.Tests.Utilities.TestModellerLogger.LogMessage(name, desc, CuriositySoftware.RunResult.Entities.TestPathRunStatus.Passed);
        }

        public void LogMessageWithScreenshot(String name, String desc)
        {
            Microsoft.Dynamics365.UIAutomation.Tests.Utilities.TestModellerLogger.LogMessageWithScreenshot(name, desc, (GetScreenShot.captureAsByteArray(_client.Browser.Driver)), CuriositySoftware.RunResult.Entities.TestPathRunStatus.Passed);
        }
    }
}
