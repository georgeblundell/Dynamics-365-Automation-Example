using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using Microsoft.Dynamics365.UIAutomation.Browser;
using Microsoft.Dynamics365.UIAutomation.Tests.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pages
{
    public class BasePage : Element
    {
        protected readonly WebClient _client;

        public BasePage(WebClient driver)
        {
            _client = driver;
        }

        public void passStep(String msg)
        {
            TestModellerLogger.PassStep(_client.Browser.Driver, msg);
        }

        public void passStepWithScreenshot(String msg)
        {
            TestModellerLogger.PassStepWithScreenshot(_client.Browser.Driver, msg);
        }

        public void failStep(String msg)
        {
            TestModellerLogger.FailStepWithScreenshot(_client.Browser.Driver, msg);

            throw new Exception(msg);
        }

        public void failStep(String msg, String details)
        {
            TestModellerLogger.FailStepWithScreenshot(_client.Browser.Driver, msg + "; " + details);

            throw new Exception(msg + "; " + details);
        }

        protected IWebElement getWebElement(By by)
        {
            waitForLoaded(by, 10);
            waitForVisible(by, 10);

            try
            {
                return _client.Browser.Driver.FindElement(by);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        protected IReadOnlyCollection<IWebElement> getWebElements(By by)
        {
            waitForLoaded(by, 2);
            waitForVisible(by, 2);

            try
            {
                return _client.Browser.Driver.FindElements(by);
            }
            catch (Exception e)
            {
                return null;
            }
        }


        protected void waitForLoaded(By by, int waitTime)
        {
            WebDriverWait wait = new WebDriverWait(_client.Browser.Driver, TimeSpan.FromMilliseconds(waitTime));

            for (int attempt = 0; attempt < waitTime; attempt++)
            {
                try
                {
                    _client.Browser.Driver.FindElement(by);
                    break;
                }
                catch (Exception e)
                {
                    _client.Browser.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
                }
            }
        }

        protected void waitForVisible(By by, int waitTime)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(_client.Browser.Driver, TimeSpan.FromMilliseconds(waitTime));

                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
            }
            catch (Exception e)
            {

            }
        }
    }
}
