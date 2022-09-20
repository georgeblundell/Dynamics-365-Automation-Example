// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
using Microsoft.Dynamics365.UIAutomation.Api;
using Microsoft.Dynamics365.UIAutomation.Browser;
using System;

namespace Microsoft.Dynamics365.UIAutomation.Tests
{
    public static class TestSettings
    {
        private static readonly string Type = "Chrome";

        public static BrowserOptions Options = new BrowserOptions
        {
            BrowserType = (BrowserType)Enum.Parse(typeof(BrowserType), Type),
        };
    }

    public static class UCIAppName
    {
        public static string Sales = "Sales Hub";
        public static string CustomerService = "Customer Service Hub";
        public static string Project = "Project Resource Hub";
        public static string FieldService = "Field Resource Hub";
    }
}
