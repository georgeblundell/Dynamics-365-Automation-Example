using CuriositySoftware.DataAllocation.Engine;
using CuriositySoftware.DataAllocation.Entities;
using CuriositySoftware.RunResult.Entities;
using CuriositySoftware.RunResult.Services;
using CuriositySoftware.Utils;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using Microsoft.Dynamics365.UIAutomation.Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dynamics365.UIAutomation.Tests.Tests
{
    public class ModellerConfig
    {
        public static String APIUrl = "https://partner.testinsights.io";

        public static String APIKey = "Tn7lwYot9TrMyYP_CjBQxG_B3";

        public static String ServerName = "{{VIP_SERVER}}";

        public static String JobID = "{{JOB_ID}}";

        public static ConnectionProfile GetConnectionProfile()
        {
            ConnectionProfile cp = new ConnectionProfile();

            cp.APIKey = ModellerConfig.APIKey;
            cp.Url = ModellerConfig.APIUrl;

            return cp;
        }
    }


    [SetUpFixture]
    public class DataAllocSetup
    {
        [OneTimeSetUp]
        public void performAllocations()
        {
            ConnectionProfile cp = new ConnectionProfile();
            cp.APIKey = ModellerConfig.APIKey;
            cp.Url = ModellerConfig.APIUrl;

            DataAllocationEngine dataAllocationEngine = new DataAllocationEngine(cp);

            // Create a list of all the pools that need allocating
            List<AllocationType> allocationTypes = new List<AllocationType>();

            foreach (Test curTest in TestExecutionContext.CurrentContext.CurrentTest.Tests)
            {
                foreach (Test subTest in curTest.Tests)
                {
                    DataAllocation[] allocAttr = subTest.Method.GetCustomAttributes<DataAllocation>(true);
                    if (allocAttr != null && allocAttr.Length > 0)
                    {
                        foreach (String testType in allocAttr[0].groups)
                        {
                            AllocationType allocationType = new AllocationType(allocAttr[0].poolName, allocAttr[0].suiteName, testType);

                            allocationTypes.Add(allocationType);
                        }
                    }
                }
            }

            if (allocationTypes.Count > 0)
            {
                if (!dataAllocationEngine.ResolvePools(ModellerConfig.ServerName, allocationTypes))
                {
                    throw new Exception("Error - " + dataAllocationEngine.getErrorMessage());
                }
            }
        }
    }

    public class TestBase
    {
        public static TestPathRunEntity testPathRun;

        protected DataAllocationEngine dataAllocationEngine;

        protected WebClient driver;

        protected Stopwatch _stopWatch;

        [SetUp]
        public void initDriver()
        {
            TestModellerLogger.steps.Clear();

            testPathRun = new TestPathRunEntity();
            TestModellerLogger.steps.Clear();

            testPathRun = new TestPathRunEntity();

            dataAllocationEngine = new DataAllocationEngine(ModellerConfig.GetConnectionProfile());

            driver = new WebClient(TestSettings.Options);

            _stopWatch = Stopwatch.StartNew();
        }

        [TearDown]
        public void closerDriver()
        {
            TestModellerId[] attr = TestExecutionContext.CurrentContext.CurrentTest.Method.GetCustomAttributes<TestModellerId>(true);
            if (attr != null && attr.Length > 0)
            {
                String guid = attr[0].guid;

                _stopWatch.Stop();

                testPathRun.message = TestExecutionContext.CurrentContext.CurrentResult.Message;
                testPathRun.runTime = (int) _stopWatch.ElapsedMilliseconds;
                testPathRun.runTimeStamp = DateTime.Now;
                testPathRun.testPathGuid = (guid);
                testPathRun.vipRunId = (TestRunIdGenerator.GenerateGuid());

                try {
                    testPathRun.jobId = long.Parse(ModellerConfig.JobID);
                } catch (Exception e) { }

                if (TestExecutionContext.CurrentContext.CurrentResult.ResultState.Equals(ResultState.Success))
                {
                    TestModellerLogger.PassStepWithScreenshot(driver.Browser.Driver, "Test Passed");
                    testPathRun.testStatus = (TestPathRunStatus.Passed);
                }
                else
                {
                    TestModellerLogger.FailStepWithScreenshot(driver.Browser.Driver, "Test Failed");

                    testPathRun.testStatus = (TestPathRunStatus.Failed);
                }

                testPathRun.testPathRunSteps = TestModellerLogger.steps;

                TestRunService runService = new TestRunService(ModellerConfig.GetConnectionProfile());
                runService.PostTestRun(testPathRun);
            }

            driver.Browser.Dispose();
        }
    }
}
