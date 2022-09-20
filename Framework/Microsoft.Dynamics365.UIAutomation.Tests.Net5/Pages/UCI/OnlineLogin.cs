// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
using System;
using System.Security;
using Microsoft.Dynamics365.UIAutomation.Browser;

namespace Microsoft.Dynamics365.UIAutomation.Api.UCI
{
    public class OnlineLogin : Element
    {
        private WebClient _client;

        public OnlineLogin(WebClient client) 
        {
            _client = client;
        }

        /// <summary>
        /// Logs into the organization with the user and password provided
        /// </summary>
        /// <param name="orgUrl">URL of the organization</param>
        /// <param name="username">User name</param>
        /// <param name="password">Password</param>
        public void Login(String orgUrl, String username, String password)
        {
            _client.Login(new Uri(orgUrl), username.ToSecureString(), password.ToSecureString());

            if (_client.Browser.Options.UCITestMode)
            {
                _client.InitializeTestMode(true);
            }
        }
    }
}
