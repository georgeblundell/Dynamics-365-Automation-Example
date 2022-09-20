using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Dynamics365.UIAutomation.Browser;

namespace Microsoft.Dynamics365.UIAutomation.Api.UCI
{
    public class ThinkTime : Element
    {
        private readonly WebClient _client;

        public ThinkTime(WebClient client) : base()
        {
            _client = client;
        }

        public void Think(int timeMS)
        {
            _client.ThinkTime(timeMS);
        }
    }
}
