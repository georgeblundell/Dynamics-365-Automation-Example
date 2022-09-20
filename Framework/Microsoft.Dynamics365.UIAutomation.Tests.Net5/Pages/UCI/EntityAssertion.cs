using Microsoft.Dynamics365.UIAutomation.Browser;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dynamics365.UIAutomation.Api.UCI
{
    public class EntityAssertion : Element
    {
        private readonly WebClient _client;

        public EntityAssertion(WebClient client) : base()
        {
            _client = client;
        }

        public void AssertErrorMessage(String field)
        {
            var fieldContainer = _client.Browser.Driver.WaitUntilAvailable(By.XPath(AppElements.Xpath[AppReference.Entity.TextFieldContainer].Replace("[NAME]", field)));

            if (fieldContainer.FindElements(By.XPath("//label[@title='Required fields must be filled in.']")).Count == 0)
            {
                throw new Exception("Failure - Error message not present");
            }
        }


        public void AssertSaveDisabled()
        {
            var save = _client.Browser.Driver.FindElement(By.XPath(AppElements.Xpath[AppReference.Entity.Save]));

            if (save.Enabled) {
                throw new Exception("Failure - Save enabled");
            }
        }
    }
}
